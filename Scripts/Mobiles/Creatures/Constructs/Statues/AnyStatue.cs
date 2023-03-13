using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a broken sculpture" )]
	public class AnyStatue : BaseCreature
	{
		private bool m_Stunning;

		public string RealName;
		[CommandProperty( AccessLevel.GameMaster )]
		public string p_RealName { get{ return RealName; } set{ RealName = value; } }

		[Constructable]
		public AnyStatue() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 0x4C;
			BaseSoundID = 268;
			Name = "an animated statue";

			switch ( Utility.RandomMinMax( 1, 8 ) )
			{
				case 1:		RealName = "a golden sculpture"; break;
				case 2:		RealName = "an iron sculpture"; break;
				case 3:		RealName = "a jade sculpture"; break;
				case 4:		RealName = "a marble sculpture"; break;
				case 5:		RealName = "a shadow iron sculpture"; break;
				case 6:		RealName = "a silver sculpture"; break;
				case 7:		RealName = "a stone sculpture"; break;
				case 8:		RealName = "a bronze sculpture"; break;
			}

			if ( RealName == "a golden sculpture" )
			{
				Hue = 0x499;

				SetStr( 196, 225 );
				SetDex( 116, 135 );
				SetInt( 71, 102 );

				SetHits( 146, 163 );

				SetDamage( 23, 30 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 35 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 100 );
				SetResistance( ResistanceType.Energy, 15, 25 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 4500;
				Karma = -4500;

				VirtualArmor = 42;
			}
			else if ( RealName == "an iron sculpture" )
			{
				Hue = 2401;

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
			}
			else if ( RealName == "a jade sculpture" )
			{
				Hue = 0xB93;

				SetStr( 176, 205 );
				SetDex( 96, 115 );
				SetInt( 51, 82 );

				SetHits( 126, 143 );

				SetDamage( 19, 26 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 35 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 100 );
				SetResistance( ResistanceType.Energy, 15, 25 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 4500;
				Karma = -4500;

				VirtualArmor = 42;
			}
			else if ( RealName == "a marble sculpture" )
			{
				Hue = 0xB92;

				SetStr( 166, 195 );
				SetDex( 86, 105 );
				SetInt( 41, 72 );

				SetHits( 116, 133 );

				SetDamage( 17, 24 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 35 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 100 );
				SetResistance( ResistanceType.Energy, 15, 25 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 4500;
				Karma = -4500;

				VirtualArmor = 42;
			}
			else if ( RealName == "a shadow iron sculpture" )
			{
				Hue = 0x966;

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
			}
			else if ( RealName == "a silver sculpture" )
			{
				Hue = 0x835;

				SetStr( 186, 215 );
				SetDex( 106, 125 );
				SetInt( 61, 92 );

				SetHits( 136, 153 );

				SetDamage( 21, 28 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 35 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 100 );
				SetResistance( ResistanceType.Energy, 15, 25 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 4500;
				Karma = -4500;

				VirtualArmor = 42;
			}
			else if ( RealName == "a stone sculpture" )
			{
				Hue = 2959;

				SetStr( 146, 175 );
				SetDex( 66, 85 );
				SetInt( 31, 62 );

				SetHits( 96, 113 );

				SetDamage( 12, 19 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 35 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 100 );
				SetResistance( ResistanceType.Energy, 15, 25 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 4000;
				Karma = -4000;

				VirtualArmor = 40;
			}
			else if ( RealName == "a bronze sculpture" )
			{
				Hue = 2968;

				SetStr( 156, 185 );
				SetDex( 76, 95 );
				SetInt( 31, 62 );

				SetHits( 106, 123 );

				SetDamage( 15, 22 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 35 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 100 );
				SetResistance( ResistanceType.Energy, 15, 25 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 4500;
				Karma = -4500;

				VirtualArmor = 42;
			}
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( this.RealName == "a golden sculpture" )
			{
				Item ore = new GoldOre( Utility.RandomMinMax( 5, 10 ) );
				ore.ItemID = 0x19B7;
				c.DropItem(ore);
			}
			else if ( this.RealName == "an iron sculpture" )
			{
				Item ore = new IronOre( Utility.RandomMinMax( 5, 10 ) );
				ore.ItemID = 0x19B7;
				c.DropItem(ore);
			}
			else if ( this.RealName == "a jade sculpture" )
			{
				RareMetals stones = new RareMetals( Utility.RandomMinMax( 5, 10 ), "jade stones" );
				c.DropItem( stones );
			}
			else if ( this.RealName == "a marble sculpture" )
			{
				RareMetals stones = new RareMetals( Utility.RandomMinMax( 5, 10 ), "gargish marble stones" );
				c.DropItem( stones );
			}
			else if ( this.RealName == "a shadow iron sculpture" )
			{
				Item ore = new ShadowIronOre( Utility.RandomMinMax( 5, 10 ) );
				ore.ItemID = 0x19B7;
				c.DropItem(ore);
			}
			else if ( this.RealName == "a silver sculpture" )
			{
				RareMetals stones = new RareMetals( Utility.RandomMinMax( 5, 10 ), "silver stones" );
				c.DropItem( stones );
			}
			else if ( this.RealName == "a stone sculpture" )
			{
				Granite granite = new Granite();
				granite.Amount = 1;
				c.DropItem(granite);
			}
			else if ( this.RealName == "a bronze sculpture" )
			{
				Item ore = new BronzeOre( Utility.RandomMinMax( 5, 10 ) );
				ore.ItemID = 0x19B7;
				c.DropItem(ore);
			}
		}

		public override int GetAttackSound(){ return 0x626; }	// A
		public override int GetDeathSound(){ return 0x627; }	// D
		public override int GetHurtSound(){ return 0x628; }		// H

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
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
			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ reflect = true; } // 50% spells are reflected back to the caster
			else { reflect = false; }
		}

		public AnyStatue( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( RealName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			RealName = reader.ReadString();
		}
	}
}