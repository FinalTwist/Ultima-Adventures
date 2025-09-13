using System;
using Server;
using Server.Items;
using System.Collections; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles
{
	
	public class VikingHermit : BaseCreature
	{
		[Constructable]
		public VikingHermit() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a viking hermit";
			Body = 400;
                                               Hue = 33786;

                        Boots b = new Boots();
                        b.Hue = 1708;
                        b.Name = "Viking Hermit Boots";
                        AddItem( b );

                        LongPants p = new LongPants();
                        p.Hue = 1708;
                        p.Name = "Viking Hermit Pants";
                        AddItem( p );

                        HakamaShita hs = new  HakamaShita();
                        hs.Hue = 1708;
                        hs.Name = "Viking Hermit Rugged Jacket";
                        AddItem( hs );

                        this.HairItemID = 0x203C;
                         this.HairHue = 956;


                          this.FacialHairItemID = 0x204C;
                          this.FacialHairHue = 956;

                        

                                                     
	                     Item club = new Club();
                                    club.LootType = LootType.Blessed;
                                      AddItem(club);

			

			SetStr( 100, 110 );
			SetDex( 95, 100 );
			SetInt( 90, 105 );

			SetHits( 600, 700 );

			SetDamage( 30, 40 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Fire, 90 );

			SetResistance( ResistanceType.Physical, 90, 95 );
			SetResistance( ResistanceType.Fire, 80, 85 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 90, 95 );
			SetResistance( ResistanceType.Energy, 100, 110 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 70.0 );
			SetSkill( SkillName.Wrestling, 90.0 );



           
            
					
			VirtualArmor = 40;

			PackGold( 200, 300 );
			PackItem( new Steel() );
		}

		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }

		public  VikingHermit( Serial serial ) : base( serial )
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

