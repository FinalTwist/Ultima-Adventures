using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an elemental corpse" )]
	public class AnyElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }
		public override int BreathPhysicalDamage{ get{ return 100; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x96D; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSound{ get{ return 0x664; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 22 ); }

		public string RealName;
		[CommandProperty( AccessLevel.GameMaster )]
		public string p_RealName { get{ return RealName; } set{ RealName = value; } }

		[Constructable]
		public AnyElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a grue";
			BaseSoundID = 0x56F;
			Body = 142;

			switch ( Utility.RandomMinMax( 1, 10 ) )
			{
				case 1:		RealName = "an iron grue"; break;
				case 2:		RealName = "a golden grue"; break;
				case 3:		RealName = "a shadow iron grue"; break;
				case 4:		RealName = "a valorite grue"; break;
				case 5:		RealName = "a verite grue"; break;
				case 6:		RealName = "an agapite grue"; break;
				case 7:		RealName = "a bronze grue"; break;
				case 8:		RealName = "a copper grue"; break;
				case 9:		RealName = "a dull copper grue"; break;
				case 10:	RealName = "a stone grue"; break;
			}

			if ( RealName == "an iron grue" )
			{
				Hue = 0x430;

				SetStr( 126, 155 );
				SetDex( 66, 85 );
				SetInt( 71, 92 );

				SetHits( 76, 93 );

				SetDamage( 9, 16 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 35 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 15, 25 );
				SetResistance( ResistanceType.Energy, 15, 25 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 3500;
				Karma = -3500;

				VirtualArmor = 34;
			}
			else if ( RealName == "a golden grue" )
			{
				Hue = MaterialInfo.GetMaterialColor( "gold", "monster", 0 );

				SetStr( 226, 255 );
				SetDex( 126, 145 );
				SetInt( 71, 92 );

				SetHits( 136, 153 );

				SetDamage( 9, 16 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 60, 75 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 30, 40 );
				SetResistance( ResistanceType.Poison, 30, 40 );
				SetResistance( ResistanceType.Energy, 30, 40 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 3500;
				Karma = -3500;

				VirtualArmor = 60;
			}
			else if ( RealName == "a shadow iron grue" )
			{
				Hue = MaterialInfo.GetMaterialColor( "shadow iron", "monster", 0 );

				SetStr( 226, 255 );
				SetDex( 126, 145 );
				SetInt( 71, 92 );

				SetHits( 136, 153 );

				SetDamage( 9, 16 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 40 );
				SetResistance( ResistanceType.Fire, 30, 40 );
				SetResistance( ResistanceType.Cold, 20, 30 );
				SetResistance( ResistanceType.Poison, 10, 20 );
				SetResistance( ResistanceType.Energy, 30, 40 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 4500;
				Karma = -4500;

				VirtualArmor = 23;
			}
			else if ( RealName == "a valorite grue" )
			{
				Hue = MaterialInfo.GetMaterialColor( "valorite", "monster", 0 );

				SetStr( 226, 255 );
				SetDex( 126, 145 );
				SetInt( 71, 92 );

				SetHits( 136, 153 );

				SetDamage( 28 );

				SetDamageType( ResistanceType.Physical, 25 );
				SetDamageType( ResistanceType.Fire, 25 );
				SetDamageType( ResistanceType.Cold, 25 );
				SetDamageType( ResistanceType.Energy, 25 );

				SetResistance( ResistanceType.Physical, 65, 75 );
				SetResistance( ResistanceType.Fire, 50, 60 );
				SetResistance( ResistanceType.Cold, 50, 60 );
				SetResistance( ResistanceType.Poison, 50, 60 );
				SetResistance( ResistanceType.Energy, 40, 50 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 3500;
				Karma = -3500;

				VirtualArmor = 38;
			}
			else if ( RealName == "a verite grue" )
			{
				Hue = MaterialInfo.GetMaterialColor( "verite", "monster", 0 );

				SetStr( 226, 255 );
				SetDex( 126, 145 );
				SetInt( 71, 92 );

				SetHits( 136, 153 );

				SetDamage( 9, 16 );

				SetDamageType( ResistanceType.Physical, 50 );
				SetDamageType( ResistanceType.Energy, 50 );

				SetResistance( ResistanceType.Physical, 30, 40 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 50, 60 );
				SetResistance( ResistanceType.Poison, 50, 60 );
				SetResistance( ResistanceType.Energy, 50, 60 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 3500;
				Karma = -3500;

				VirtualArmor = 35;
			}
			else if ( RealName == "an agapite grue" )
			{
				Hue = MaterialInfo.GetMaterialColor( "agapite", "monster", 0 );

				SetStr( 226, 255 );
				SetDex( 126, 145 );
				SetInt( 71, 92 );

				SetHits( 136, 153 );

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

				Fame = 3500;
				Karma = -3500;

				VirtualArmor = 32;
			}
			else if ( RealName == "a bronze grue" )
			{
				Hue = MaterialInfo.GetMaterialColor( "bronze", "monster", 0 );

				SetStr( 226, 255 );
				SetDex( 126, 145 );
				SetInt( 71, 92 );

				SetHits( 136, 153 );

				SetDamage( 9, 16 );

				SetDamageType( ResistanceType.Physical, 30 );
				SetDamageType( ResistanceType.Fire, 70 );

				SetResistance( ResistanceType.Physical, 30, 40 );
				SetResistance( ResistanceType.Fire, 30, 40 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 70, 80 );
				SetResistance( ResistanceType.Energy, 20, 30 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 5000;
				Karma = -5000;

				VirtualArmor = 29;
			}
			else if ( RealName == "a copper grue" )
			{
				Hue = MaterialInfo.GetMaterialColor( "copper", "monster", 0 );

				SetStr( 226, 255 );
				SetDex( 126, 145 );
				SetInt( 71, 92 );

				SetHits( 136, 153 );

				SetDamage( 9, 16 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 40 );
				SetResistance( ResistanceType.Fire, 30, 40 );
				SetResistance( ResistanceType.Cold, 30, 40 );
				SetResistance( ResistanceType.Poison, 20, 30 );
				SetResistance( ResistanceType.Energy, 10, 20 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 4800;
				Karma = -4800;

				VirtualArmor = 26;
			}
			else if ( RealName == "a dull copper grue" )
			{
				Hue = MaterialInfo.GetMaterialColor( "dull copper", "monster", 0 );

				SetStr( 226, 255 );
				SetDex( 126, 145 );
				SetInt( 71, 92 );

				SetHits( 136, 153 );

				SetDamage( 9, 16 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 40 );
				SetResistance( ResistanceType.Fire, 30, 40 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 20, 30 );
				SetResistance( ResistanceType.Energy, 20, 30 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 3500;
				Karma = -3500;

				VirtualArmor = 20;
			}
			else if ( RealName == "a stone grue" )
			{
				Hue = 0x31C;

				SetStr( 126, 155 );
				SetDex( 66, 85 );
				SetInt( 71, 92 );

				SetHits( 76, 93 );

				SetDamage( 9, 16 );

				SetDamageType( ResistanceType.Physical, 100 );

				SetResistance( ResistanceType.Physical, 30, 35 );
				SetResistance( ResistanceType.Fire, 10, 20 );
				SetResistance( ResistanceType.Cold, 10, 20 );
				SetResistance( ResistanceType.Poison, 15, 25 );
				SetResistance( ResistanceType.Energy, 15, 25 );

				SetSkill( SkillName.MagicResist, 50.1, 95.0 );
				SetSkill( SkillName.Tactics, 60.1, 100.0 );
				SetSkill( SkillName.Wrestling, 60.1, 100.0 );

				Fame = 3500;
				Karma = -3500;

				VirtualArmor = 34;
			}
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( this.Body == 13 && willKill == false && Utility.Random( 4 ) == 1 )
			{
				this.BaseSoundID = 0x56F;
				this.Body = 142;
			}
			else if ( willKill == false && Utility.Random( 4 ) == 1 )
			{
				this.Body = 13;
				this.BaseSoundID = 655;
			}

			base.OnDamage( amount, from, willKill );
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 13;
			this.BaseSoundID = 655;
			return base.OnBeforeDeath();
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( this.RealName == "an iron grue" )
			{
				Item ore = new IronOre( Utility.RandomMinMax( 3, 8 ) );
				ore.ItemID = 0x19B7;
				c.DropItem( ore );
			}
			else if ( this.RealName == "a golden grue" )
			{
				Item ore = new GoldOre( Utility.RandomMinMax( 3, 8 ) );
				ore.ItemID = 0x19B9;
				c.DropItem( ore );
			}
			else if ( this.RealName == "a shadow iron grue" )
			{
				Item ore = new ShadowIronOre( Utility.RandomMinMax( 3, 8 ) );
				ore.ItemID = 0x19B9;
				c.DropItem( ore );
			}
			else if ( this.RealName == "a valorite grue" )
			{
				Item ore = new ValoriteOre( Utility.RandomMinMax( 3, 8 ) );
				ore.ItemID = 0x19B9;
				c.DropItem( ore );
			}
			else if ( this.RealName == "a verite grue" )
			{
				Item ore = new VeriteOre( Utility.RandomMinMax( 3, 8 ) );
				ore.ItemID = 0x19B9;
				c.DropItem( ore );
			}
			else if ( this.RealName == "an agapite grue" )
			{
				Item ore = new AgapiteOre( Utility.RandomMinMax( 3, 8 ) );
				ore.ItemID = 0x19B9;
				c.DropItem( ore );
			}
			else if ( this.RealName == "a bronze grue" )
			{
				Item ore = new BronzeOre( Utility.RandomMinMax( 3, 8 ) );
				ore.ItemID = 0x19B9;
				c.DropItem( ore );
			}
			else if ( this.RealName == "a copper grue" )
			{
				Item ore = new CopperOre( Utility.RandomMinMax( 3, 8 ) );
				ore.ItemID = 0x19B9;
				c.DropItem( ore );
			}
			else if ( this.RealName == "a dull copper grue" )
			{
				Item ore = new DullCopperOre( Utility.RandomMinMax( 3, 8 ) );
				ore.ItemID = 0x19B9;
				c.DropItem( ore );
			}
			else if ( this.RealName == "a stone grue" )
			{
				Granite granite = new Granite();
				granite.Amount = Utility.RandomMinMax( 1, 3 );
				c.DropItem(granite);
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, 2 );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public AnyElemental( Serial serial ) : base( serial )
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
