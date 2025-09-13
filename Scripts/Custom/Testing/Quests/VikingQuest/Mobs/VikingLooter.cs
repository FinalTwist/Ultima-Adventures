using System;
using Server;
using Server.Items;
using System.Collections; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles
{
	
	public class VikingLooter : BaseCreature
	{
		[Constructable]
		public  VikingLooter() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a  viking looter";
			Body = 400;
                                                 Female = false;
			Hue = 33774;

			SetStr( 100, 105 );
			SetDex( 66, 75 );
			SetInt( 101, 105 );

			SetHits( 300, 400 );
			SetStam( 0 );

			SetDamage( 20, 30 );

                       

			      Boots b = new Boots();
                        b.Hue = 1701;
                        b.Name = "Viking Looter Boots";
                        AddItem( b );

                        Item Cloak = new Item(5397);
                        Cloak.Name = "Viking Looter Cloak";
                        Cloak.Layer = Layer.Cloak;
                        Cloak.LootType = LootType.Blessed;
                        AddItem(Cloak);

                        LongPants p = new LongPants();
                        p.Hue = 1311;
                        p.Name = "Viking Looter Pants";
                        AddItem( p );

                        this.HairItemID = 0x203D;
                         this.HairHue = 967;


                       

                         Item WoodenShield = new Item( 7035 ); 
	         WoodenShield.Layer = Layer.TwoHanded;
	        WoodenShield.LootType = LootType.Blessed;
	        AddItem( WoodenShield );
                                                   
                                                 
	       Item vikingsword = new VikingSword();
                       vikingsword.LootType = LootType.Blessed;
                         AddItem(vikingsword);
			

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Poison, 90 );

			SetResistance( ResistanceType.Physical, 105, 115 );
			SetResistance( ResistanceType.Fire, 75, 85 );
			SetResistance( ResistanceType.Cold, 90, 95 );
			SetResistance( ResistanceType.Poison, 80, 85 );
			SetResistance( ResistanceType.Energy, 100, 110 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.1, 110.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 95.0 );

			
                                                 
			
			   
			
			
			VirtualArmor = 40;
                        
                        PackGold( 200, 300 );
			PackItem( new PirateAle() );
			
                        
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich );
		}
        public override bool AlwaysMurderer{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override int TreasureMapLevel{ get{ return 2; } }
		//public override bool DisallowAllMoves{ get{ return true; } }

		public  VikingLooter( Serial serial ) : base( serial )
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