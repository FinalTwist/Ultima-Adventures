using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a pile of wood" )]
	public class WoodenGolem : BaseCreature
	{
		private bool m_Stunning;

		[Constructable]
		public WoodenGolem() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a wooden golem";
			Body = 301;

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			int modifySta = 0;
			int modifyHit = 0;
			int modifyDmg = 0;

			switch ( Utility.Random( 11 ) )
			{
				case 0: PackItem( new Log( Utility.RandomMinMax( 10, 20 ) ) ); 			Hue = 0x000;	break;
				case 1: PackItem( new AshLog( Utility.RandomMinMax( 10, 20 ) ) ); 		Hue = MaterialInfo.GetMaterialColor( "ash", "", 0 );		modifySta = 5;	modifyHit = 10;		modifyDmg = 1;	break;
				case 2: PackItem( new CherryLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "cherry", "", 0 );		modifySta = 10;	modifyHit = 20;		modifyDmg = 2;	break;
				case 3: PackItem( new EbonyLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "ebony", "", 0 );		modifySta = 15;	modifyHit = 30;		modifyDmg = 3;	break;
				case 4: PackItem( new GoldenOakLog( Utility.RandomMinMax( 10, 20 ) ) ); Hue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 );	modifySta = 20;	modifyHit = 40;		modifyDmg = 4;	break;
				case 5: PackItem( new HickoryLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "hickory", "", 0 );	modifySta = 25;	modifyHit = 50;		modifyDmg = 5;	break;
				case 6: PackItem( new MahoganyLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 );	modifySta = 30;	modifyHit = 60;		modifyDmg = 6;	break;
				case 7: PackItem( new OakLog( Utility.RandomMinMax( 10, 20 ) ) ); 		Hue = MaterialInfo.GetMaterialColor( "oak", "", 0 );		modifySta = 35;	modifyHit = 70;		modifyDmg = 7;	break;
				case 8: PackItem( new PineLog( Utility.RandomMinMax( 10, 20 ) ) ); 		Hue = MaterialInfo.GetMaterialColor( "pine", "", 0 );		modifySta = 40;	modifyHit = 80;		modifyDmg = 8;	break;
				case 9: PackItem( new RosewoodLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 );	modifySta = 45;	modifyHit = 90;		modifyDmg = 9;	break;
				case 10: PackItem( new WalnutLog( Utility.RandomMinMax( 10, 20 ) ) ); 	Hue = MaterialInfo.GetMaterialColor( "walnut", "", 0 );		modifySta = 50;	modifyHit = 100;	modifyDmg = 10;	break;
			}

			SetStr( (121+modifySta), (160+modifySta) );
			SetDex( (51+modifySta), (70+modifySta) );
			SetInt( (31+modifySta), (42+modifySta) );

			SetHits( (101+modifyHit), (118+modifyHit) );
			SetStam( 0 );

			SetDamage( (10+modifyDmg), (15+modifyDmg) );

			Fame = 2250;
			Karma = -2250;

			VirtualArmor = 29;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			if ( 1 == Utility.RandomMinMax( 1, 50 ) )
			{
				OilWood loot = new OilWood();
				loot.Amount = 1;
				c.DropItem(loot);
			}
			if ( 1 == Utility.RandomMinMax( 1, 3 ) )
			{
				ReaperOil loot = new ReaperOil();
				loot.Amount = 1;
				c.DropItem(loot);
			}
			if ( 1 == Utility.RandomMinMax( 1, 3 ) )
			{
				MysticalTreeSap loot = new MysticalTreeSap();
				loot.Amount = 1;
				c.DropItem(loot);
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override int GetIdleSound()
		{
			return 443;
		}

		public override int GetDeathSound()
		{
			return 31;
		}

		public override int GetAttackSound()
		{
			return 672;
		}

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
			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public WoodenGolem( Serial serial ) : base( serial )
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