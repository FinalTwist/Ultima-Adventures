using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a goliath corpse" )]
	public class CrystalGoliath : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 120.5; } }
		public override double DispelFocus{ get{ return 35.0; } }

		[Constructable]
		public CrystalGoliath() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 753;
			BaseSoundID = 268;
			Name = "a goliath";

			SetStr( 526, 555 );
			SetDex( 126, 145 );
			SetInt( 71, 92 );

			SetHits( 336, 453 );

			SetDamage( 28 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 7500;
			Karma = -7500;

			VirtualArmor = 32;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );
			AddLoot( LootPack.Gems, 4 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			switch ( Utility.RandomMinMax( 0, 25 ) )
			{
				case 0:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline onyx" ) ); break;
				case 1:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline quartz" ) ); break;
				case 2:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline ruby" ) ); break;
				case 3:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline sapphire" ) ); break;
				case 4:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline spinel" ) ); break;
				case 5:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline topaz" ) ); break;
				case 6:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline amethyst" ) ); break;
				case 7:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline emerald" ) ); break;
				case 8:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline garnet" ) ); break;
				case 9:		c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline silver" ) ); break;
				case 10:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline star ruby" ) ); break;
				case 11:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline jade" ) ); break;
				case 12:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline copper" ) ); break;
				case 13:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline verite" ) ); break;
				case 14:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline valorite" ) ); break;
				case 15:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline agapite" ) ); break;
				case 16:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline bronze" ) ); break;
				case 17:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline dull copper" ) ); break;
				case 18:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline gold" ) ); break;
				case 19:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline shadow iron" ) ); break;
				case 20:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline brass" ) ); break;
				case 21:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline steel" ) ); break;
				case 22:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline mithril" ) ); break;
				case 23:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline xormite" ) ); break;
				case 24:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline obsidian" ) ); break;
				case 25:	c.DropItem( new HardCrystals( Utility.RandomMinMax( 10, 20 ), "crystalline nepturite" ) ); break;
			}
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public override int GetAttackSound(){ return 0x626; }	// A
		public override int GetDeathSound(){ return 0x627; }	// D
		public override int GetHurtSound(){ return 0x628; }		// H

		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)from;

				if ( bc.Controlled || bc.BardTarget == this )
					damage = 0; // Immune to pets and provoked creatures
			}
			else if ( from != null )
			{
				int hitback = (int)(damage/2); if (hitback > 50){ hitback = 50; }
				AOS.Damage( from, this, hitback, 100, 0, 0, 0, 0 );
			}
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if ( 0.5 >= Utility.RandomDouble() )
			{
				double positionChance = Utility.RandomDouble();
				BaseArmor armor;

				if ( positionChance < 0.07 )
					armor = to.NeckArmor as BaseArmor;
				else if ( positionChance < 0.14 )
					armor = to.HandArmor as BaseArmor;
				else if ( positionChance < 0.21 )
					armor = to.ArmsArmor as BaseArmor;
				else if ( positionChance < 0.35 )
					armor = to.HeadArmor as BaseArmor;
				else if ( positionChance < 0.49 )
					armor = to.LegsArmor as BaseArmor;
				else if( positionChance < 0.56 && to.FindItemOnLayer( Layer.Shoes ) is BaseArmor )
					armor = (BaseArmor)(to.FindItemOnLayer( Layer.Shoes ));
				else if( positionChance < 0.63 && to.FindItemOnLayer( Layer.Cloak ) is BaseArmor )
					armor = (BaseArmor)(to.FindItemOnLayer( Layer.Cloak ));
				else if( positionChance < 0.70 && to.FindItemOnLayer( Layer.OuterTorso ) is BaseArmor )
					armor = (BaseArmor)(to.FindItemOnLayer( Layer.OuterTorso ));
				else 
					armor = to.ChestArmor as BaseArmor;

				if ( armor != null )
				{
					int ruin = Utility.RandomMinMax( 1, 4 );
					armor.HitPoints -= ruin;
				}
			}
		}

		public CrystalGoliath( Serial serial ) : base( serial )
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
