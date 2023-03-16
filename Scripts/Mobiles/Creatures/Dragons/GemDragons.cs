using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a dragyn corpse" )]
	[TypeAlias( "Server.Mobiles.GemDragon" )]
	public class GemDragon : BaseMount
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override bool CanChew { get{return true;}}
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }

		[Constructable]
		public GemDragon() : this( "a dragyn" )
		{
		}

		[Constructable]
		public GemDragon( string name ) : base( name, 61, MountBody(585), AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dragyn";
			BaseSoundID = 362;

			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );

			SetHits( 650, 750 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 60;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 93.9;

			switch ( Utility.Random( 12 ) )
			{
				case 0: Hue = MaterialInfo.GetMaterialColor( "jade", "monster", 0 ); break;  // jade
				case 1: Hue = MaterialInfo.GetMaterialColor( "onyx", "monster", 0 ); break;  // onyx
				case 2: Hue = MaterialInfo.GetMaterialColor( "quartz", "monster", 0 ); break;  // quartz
				case 3: Hue = MaterialInfo.GetMaterialColor( "ruby", "monster", 0 ); break;  // ruby
				case 4: Hue = MaterialInfo.GetMaterialColor( "sapphire", "monster", 0 ); break;  // sapphire
				case 5: Hue = MaterialInfo.GetMaterialColor( "spinel", "monster", 0 ); break;  // spinel
				case 6: Hue = MaterialInfo.GetMaterialColor( "topaz", "monster", 0 ); break;  // topaz
				case 7: Hue = MaterialInfo.GetMaterialColor( "amethyst", "monster", 0 ); break;  // amethyst
				case 8: Hue = MaterialInfo.GetMaterialColor( "emerald", "monster", 0 ); break;  // emerald
				case 9: Hue = MaterialInfo.GetMaterialColor( "garnet", "monster", 0 ); break;  // garnet
				case 10: Hue = MaterialInfo.GetMaterialColor( "silver", "monster", 0 ); break;  // silver
				case 11: Hue = MaterialInfo.GetMaterialColor( "star ruby", "monster", 0 ); break; // star ruby
			}
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			if (!this.Controlled)
			{

			if ( this.Hue == MaterialInfo.GetMaterialColor( "onyx", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "onyx scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "quartz", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "quartz scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "ruby", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "ruby scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "sapphire", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "sapphire scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "spinel", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "spinel scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "topaz", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "topaz scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "amethyst", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "amethyst scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "emerald", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "emerald scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "garnet", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "garnet scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "silver", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "silver scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "star ruby", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "star ruby scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "jade", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 5, 10 ), "jade scales" );
				c.DropItem(scale);
			}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich, 1 );
			AddLoot( LootPack.Average, 1 );
		}

		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }

		public GemDragon( Serial serial ) : base( serial )
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
			if (Body != 61 && Body != 59)
			{
			    Body = 61;
			    ItemID = MountBody(585);
			}

			if (!Server.Misc.MyServerSettings.NewMounts() && Body == 59)
			{
			    ItemID = MountBody(585);
			}
		}
	}
}
