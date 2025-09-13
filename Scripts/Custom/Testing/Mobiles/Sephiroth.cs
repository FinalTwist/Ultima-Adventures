using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
	public class Sephiroth : BaseCreature 
	{ 
		[Constructable] 
		public Sephiroth() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{  
			Name = "Sephiroth";
			Body = Utility.RandomList( 400 );
			HairItemID = 12237;
			HairHue = 1150; 
			Hue = 33770; 

			PlateChest chest = new PlateChest(); 
			chest.Hue = 1150; 
			AddItem( chest ); 
			PlateArms arms = new PlateArms(); 
			arms.Hue = 1150; 
			AddItem( arms ); 
			PlateGloves gloves = new PlateGloves(); 
			gloves.Hue = 1150; 
			AddItem( gloves ); 
			PlateGorget gorget = new PlateGorget(); 
			gorget.Hue = 1150; 
			AddItem( gorget ); 
			PlateLegs legs = new PlateLegs(); 
			legs.Hue = 1150; 
			AddItem( legs ); 
          		Robe robe = new Robe();
            		robe.Hue = 1175;
            		robe.Name = "Sephiroth's Useless Robe";
            		robe.LootType = LootType.Blessed;
            		robe.Movable = true;
            		AddItem(robe);  
			AddItem( new NoDachi() );

			SetStr( 521, 847 );
			SetDex( 758 );
			SetInt( 362 );

			SetHits( 5000 );

			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 59 );
			SetResistance( ResistanceType.Fire, 55 );
			SetResistance( ResistanceType.Cold, 65 );
			SetResistance( ResistanceType.Poison, 85 );
			SetResistance( ResistanceType.Energy, 74 );

			SetSkill( SkillName.Anatomy, 85.0 );
			SetSkill( SkillName.MagicResist, 83.5, 92.5 );
			SetSkill( SkillName.Swords, 85.0 );
			SetSkill( SkillName.Tactics, 85.0 );

			Fame = 50000;
			Karma = -50000;

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 5 );
		}


		public override bool AlwaysMurderer{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public Sephiroth( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 
}