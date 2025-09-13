using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a gargoyle corpse" )]
	public class StoneGargoyle : BaseCreature
	{
		[Constructable]
		public StoneGargoyle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace =2;
			Name = "a stone gargoyle";
			Body = 4;
			Hue = 0x430;
			BaseSoundID = 0x174;

			SetStr( 246, 275 );
			SetDex( 76, 95 );
			SetInt( 81, 105 );

			SetHits( 148, 165 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 85.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 50;

			if ( 0.05 > Utility.RandomDouble() )
				PackItem( new GargoylesPickaxe() );
		}

		public override void OnAfterSpawn()
		{
			Hue = 0x430; // Iron

			switch ( Utility.RandomMinMax( 0, 12 ) )
			{
				case 0: Hue = MaterialInfo.GetMaterialColor( "copper", "monster", 0 ); break; // Copper
				case 1: Hue = MaterialInfo.GetMaterialColor( "verite", "monster", 0 ); break; // Verite
				case 2: Hue = MaterialInfo.GetMaterialColor( "valorite", "monster", 0 ); break; // Valorite
				case 3: Hue = MaterialInfo.GetMaterialColor( "agapite", "monster", 0 ); break; // Agapite
				case 4: Hue = MaterialInfo.GetMaterialColor( "bronze", "monster", 0 ); break; // Bronze
				case 5: Hue = MaterialInfo.GetMaterialColor( "dull copper", "monster", 0 ); break; // Dull Copper
				case 6: Hue = MaterialInfo.GetMaterialColor( "gold", "monster", 0 ); break; // Gold
				case 7: Hue = MaterialInfo.GetMaterialColor( "shadow iron", "monster", 0 ); break; // Shadow Iron
				case 8:
					if ( Worlds.IsExploringSeaAreas( this ) == true ){ Hue = MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Serpent Island" ){ Hue = MaterialInfo.GetMaterialColor( "obsidian", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" && this.Map == Map.TerMur ){ Hue = MaterialInfo.GetMaterialColor( "xormite", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" ){ Hue = MaterialInfo.GetMaterialColor( "mithril", "monster", 0 ); }
					break; // Special
			}

			base.OnAfterSpawn();
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			Mobile killer = this.LastKiller;
			Item granite = new Granite();

			if ( this.Hue == MaterialInfo.GetMaterialColor( "copper", "monster", 0 ) ){ granite = new CopperGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "verite", "monster", 0 ) ){ granite = new VeriteGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "valorite", "monster", 0 ) ){ granite = new ValoriteGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "agapite", "monster", 0 ) ){ granite = new AgapiteGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "bronze", "monster", 0 ) ){ granite = new BronzeGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "dull copper", "monster", 0 ) ){ granite = new DullCopperGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "gold", "monster", 0 ) ){ granite = new GoldGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "shadow iron", "monster", 0 ) ){ granite = new ShadowIronGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ) ){ granite = new NepturiteGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "obsidian", "monster", 0 ) ){ granite = new ObsidianGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "xormite", "monster", 0 ) ){ granite = new XormiteGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "mithril", "monster", 0 ) ){ granite = new MithrilGranite(); }

   			granite.Amount = Utility.RandomMinMax( 1, 2 );

   			c.DropItem(granite);
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.Gems, 1 );
			AddLoot( LootPack.Potions );
		}

		public override int TreasureMapLevel{ get{ return 2; } }

		public StoneGargoyle( Serial serial ) : base( serial )
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