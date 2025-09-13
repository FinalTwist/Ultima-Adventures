using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a pile of bones" )]
	public class BoneGolem : BaseCreature
	{
		private bool m_Stunning;

		[Constructable]
		public BoneGolem() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a bone golem";
			Body = 308;
			BaseSoundID = 0x48D;

			SetStr( 151, 180 );
			SetDex( 71, 90 );
			SetInt( 31, 62 );

			SetHits( 101, 118 );

			SetDamage( 14, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 4250;
			Karma = -4250;

			VirtualArmor = 39;

			switch ( Utility.Random( 5 ))
			{
				case 0: PackItem( new BonePile() ); break;
				case 1: PackItem( new BonePile() ); break;
				case 2: PackItem( new BonePile() ); break;
				case 3: PackItem( new BonePile() ); break;
				case 4: PackItem( new BoneContainer() ); break;
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( !Server.Items.HiddenTrap.IAmAWeaponSlayer( defender, this ) )
			{
				if ( !m_Stunning && 0.3 > Utility.RandomDouble() )
				{
					m_Stunning = true;

					defender.Animate( 21, 6, 1, true, false, 0 );
					this.PlaySound( 0xEE );
					defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You have been stunned by a colossal blow!" );

					BaseWeapon weapon = this.Weapon as BaseWeapon;
					if ( weapon != null )
						weapon.OnHit( this, defender );

					if ( defender.Alive )
					{
						defender.Frozen = true;
						Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( Recover_Callback ), defender );
					}
				}
			}
		}

		private void Recover_Callback( object state )
		{
			Mobile defender = state as Mobile;

			if ( defender != null )
			{
				defender.Frozen = false;
				defender.Combatant = null;
				defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You recover your senses." );
			}

			m_Stunning = false;
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( Controlled || Summoned )
			{
				Mobile master = ( this.ControlMaster );

				if ( master == null )
					master = this.SummonMaster;

				if ( !Server.Items.HiddenTrap.IAmAWeaponSlayer( from, this ) && master != null && master.Player && master.Map == this.Map && master.InRange( Location, 20 ) )
				{
					if ( master.Mana >= amount )
					{
						master.Mana -= amount;
					}
					else
					{
						amount -= master.Mana;
						master.Mana = 0;
						master.Damage( amount );
					}
				}
			}

			base.OnDamage( amount, from, willKill );
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			reflect = true; // Every spell is reflected back to the caster
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						switch( Utility.RandomMinMax( 0, 5 ) )
						{
							case 0: MagicBoneLegs leg = new MagicBoneLegs();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)leg, false, 1000, 5, 25, 100 );	c.DropItem( leg );		break;
							case 1: MagicBoneGloves glv = new MagicBoneGloves();	BaseRunicTool.ApplyAttributesTo( (BaseArmor)glv, false, 1000, 5, 25, 100 );	c.DropItem( glv );		break;
							case 2: MagicBoneArms arm = new MagicBoneArms();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)arm, false, 1000, 5, 25, 100 );	c.DropItem( arm );		break;
							case 3: MagicBoneChest tun = new MagicBoneChest();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)tun, false, 1000, 5, 25, 100 );	c.DropItem( tun );		break;
							case 4: MagicBoneHelm hlm = new MagicBoneHelm();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)hlm, false, 1000, 5, 25, 100 );	c.DropItem( hlm );		break;
							case 5: MagicBoneSkirt skt = new MagicBoneSkirt();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)skt, false, 1000, 5, 25, 100 );	c.DropItem( skt );		break;
						}
					}
				}
			}
		}

		public BoneGolem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}