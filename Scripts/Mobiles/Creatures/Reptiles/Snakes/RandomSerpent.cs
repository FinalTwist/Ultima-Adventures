using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName("an anaconda corpse")]
	public class RandomSerpent : BaseCreature
	{
		public static Poison m_Poison;
		public static int m_Treasure;

		[Constructable]
		public RandomSerpent() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 21;
			BaseSoundID = 219;
			Name = "an anaconda";

			int difficulty = 0;

			switch ( Utility.RandomMinMax( 1, 5 ) )
			{
				case 1:	m_Poison = Poison.Lethal; 	difficulty = 60;	m_Treasure = Utility.RandomMinMax( 1, 5 );		Item Venom1 = new VenomSack(); Venom1.Name = "lethal venom sack"; AddItem( Venom1 );	break;
				case 2:	m_Poison = Poison.Deadly; 	difficulty = 45;	m_Treasure = Utility.RandomMinMax( 1, 4 );		Item Venom2 = new VenomSack(); Venom2.Name = "deadly venom sack"; AddItem( Venom2 );	break;
				case 3:	m_Poison = Poison.Greater; 	difficulty = 30;	m_Treasure = Utility.RandomMinMax( 1, 3 );		Item Venom3 = new VenomSack(); Venom3.Name = "greater venom sack"; AddItem( Venom3 );	break;
				case 4:	m_Poison = Poison.Regular; 	difficulty = 15;	m_Treasure = Utility.RandomMinMax( 1, 2 );		Item Venom4 = new VenomSack(); Venom4.Name = "venom sack"; AddItem( Venom4 );			break;
				case 5:	m_Poison = Poison.Lesser; 	difficulty = 0;		m_Treasure = Utility.RandomMinMax( 1, 1 );		Item Venom5 = new VenomSack(); Venom5.Name = "lesser venom sack"; AddItem( Venom5 );	break;
			}

			SetStr( (130+difficulty), (340+difficulty) );
			SetDex( (130+difficulty), (280+difficulty) );
			SetInt( (10+difficulty), (20+difficulty) );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, (10+difficulty) );
			SetResistance( ResistanceType.Fire, (5+((int)(difficulty/2))) );
			SetResistance( ResistanceType.Cold, (5+((int)(difficulty/2))) );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, (5+((int)(difficulty/2))) );

			SetSkill( SkillName.Poisoning, (40+difficulty) );
			SetSkill( SkillName.MagicResist, ((int)(difficulty/2)) );
			SetSkill( SkillName.Tactics, (40+difficulty) );
			SetSkill( SkillName.Wrestling, (40+difficulty));

			Fame = (1400 + (difficulty * 1000));
			Karma = -(1400 + (difficulty * 1000));

			VirtualArmor = (20+((int)(difficulty/2)));
		}

		public override void OnAfterSpawn()
		{
			Hue = MaterialInfo.GetMaterialColor( "copper", "monster", Hue );
			switch ( Utility.RandomMinMax( 0, 20 ) )
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
				case 12: Hue = MaterialInfo.GetMaterialColor( "copper", "monster", Hue ); break; // Copper
				case 13: Hue = MaterialInfo.GetMaterialColor( "verite", "monster", Hue ); break; // Verite
				case 14: Hue = MaterialInfo.GetMaterialColor( "valorite", "monster", Hue ); break; // Valorite
				case 15: Hue = MaterialInfo.GetMaterialColor( "agapite", "monster", Hue ); break; // Agapite
				case 16: Hue = MaterialInfo.GetMaterialColor( "bronze", "monster", Hue ); break; // Bronze
				case 17: Hue = MaterialInfo.GetMaterialColor( "dull copper", "monster", Hue ); break; // Dull Copper
				case 18: Hue = MaterialInfo.GetMaterialColor( "gold", "monster", Hue ); break; // Gold
				case 19: Hue = MaterialInfo.GetMaterialColor( "shadow iron", "monster", Hue ); break; // Shadow Iron
				case 20:
					if ( Worlds.IsExploringSeaAreas( this ) == true ){ Hue = MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Savaged Empire" ){ Hue = MaterialInfo.GetMaterialColor( "steel", "monster", Hue ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Serpent Island" ){ Hue = MaterialInfo.GetMaterialColor( "obsidian", "monster", Hue ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Island of Umber Veil" ){ Hue = MaterialInfo.GetMaterialColor( "brass", "monster", Hue ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" && this.Map == Map.TerMur ){ Hue = MaterialInfo.GetMaterialColor( "xormite", "monster", Hue ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" ){ Hue = MaterialInfo.GetMaterialColor( "copper", "mithril", Hue ); }
					break; // Special
			}

			base.OnAfterSpawn();
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( this.Hue == MaterialInfo.GetMaterialColor( "onyx", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "onyx scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "quartz", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "quartz scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "ruby", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "ruby scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "sapphire", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "sapphire scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "spinel", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "spinel scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "topaz", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "topaz scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "amethyst", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "amethyst scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "emerald", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "emerald scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "garnet", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "garnet scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "silver", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "silver scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "star ruby", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "star ruby scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "jade", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "jade scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "copper", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "copper scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "verite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "verite scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "valorite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "valorite scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "agapite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "agapite scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "bronze", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "bronze scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "dull copper", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "dull copper scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "gold", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "gold scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "shadow iron", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "shadow iron scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "brass", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "brass scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "steel", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 3 ), "steel scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "mithril", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "mithril scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "xormite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "xormite scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "obsidian", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "obsidian scales" );
				c.DropItem(scale);
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "nepturite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 2, 4 ), "nepturite scales" );
				c.DropItem(scale);
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, m_Treasure );
		}

		public override bool DeathAdderCharmable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override Poison PoisonImmune{ get{ return m_Poison; } }
		public override Poison HitPoison{ get{ return m_Poison; } }
		public override int Hides{ get{ return 15; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

		public RandomSerpent(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( BaseSoundID == -1 )
				BaseSoundID = 219;
		}
	}
}