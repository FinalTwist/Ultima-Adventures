using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a bloody pirate's corpse" )]
	public class Rackham : BaseCreature
	{                
		[Constructable]
		public Rackham() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Rackham";
                        Title = "the Bloody Pirate";
			Body = 0x190;
			Hue = Utility.RandomSkinHue();
			
			ThighBoots tb = new ThighBoots();
                        tb.Hue = 0;
                        AddItem( tb );

                        LongPants lp = new LongPants();
                        lp.Hue = 633;
                        AddItem( lp );

		        FancyShirt fs = new FancyShirt();
                        fs.Hue = 907;
                        AddItem( fs );

			SkullCap sk = new SkullCap();
                        sk.Hue = 413;
                        AddItem( sk );

			BodySash bs = new BodySash();
			bs.Hue = 633;
			AddItem( bs );

			Cloak cl = new Cloak();
			cl.Hue = 688;
			AddItem( cl );

	                Scimitar sc = new Scimitar();
			sc.Hue = 533;
                        AddItem( sc );

			GoldBeadNecklace gn = new GoldBeadNecklace();
			AddItem( gn );

			GoldBracelet gb = new GoldBracelet();
			AddItem( gb );

			GoldEarrings ge = new GoldEarrings();
			AddItem( ge );

			GoldRing gr = new GoldRing();
			AddItem( gr );

            HairItemID = 0x203D; // PonyTail
            HairHue = 1149;
            FacialHairItemID = 0x204D; // Vandyke
            FacialHairHue = 1149;

            SetStr( 120, 140 );
			SetDex( 90, 105 );
			SetInt( 25, 40 );

			SetHits( 450, 500 );
			SetMana( 0 );

			SetDamage( 20, 28 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 56, 67 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 45 );
			SetResistance( ResistanceType.Energy, 40, 55 );

			SetSkill( SkillName.MagicResist, 55.1, 65.0 );
			SetSkill( SkillName.Tactics, 85.3, 105.0 );
			SetSkill( SkillName.Wrestling, 90.3, 105.0 );
			SetSkill( SkillName.Swords, 90.3, 105.0 );

			Fame = 9900;
			Karma = -9900;

			VirtualArmor = 25;
					
                        PackItem( new SeafaringBracelet() );
			//PackItem( new TreasureMap( 1, Map.Trammel ) ); 
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );			
		}

		
		public override bool AlwaysMurderer{ get{ return true; } }

		public Rackham( Serial serial ) : base( serial )
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
