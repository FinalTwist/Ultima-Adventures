using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( " corpse of a Soul of Ice" )]
	public class SoulofIce : BaseCreature
    {
    	[Constructable]
        public SoulofIce() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
        	Name = "Soul of Ice";
            Hue = 1152;
            Body = Core.AOS ? 180 : 49;
       		BaseSoundID = 362;
          
           	SetStr( 900, 1000 );
            SetDex( 250, 350 );
            SetInt( 700, 850 );
                                               
            SetHits( 2000, 2500 );
            SetMana( 1000, 1250 );
            SetStam(500, 750);
                                               
           	SetDamage( 15, 30 );
                                               
            SetDamageType( ResistanceType.Physical, 50 );
            SetDamageType( ResistanceType.Cold, 50 );
            SetDamageType( ResistanceType.Energy, 40 );

            SetResistance( ResistanceType.Physical, 60, 70 );
            SetResistance( ResistanceType.Cold, 80, 90 );
            SetResistance( ResistanceType.Fire, 15, 30 );
            SetResistance( ResistanceType.Energy, 40, 60 );
            SetResistance( ResistanceType.Poison, 50, 60 );
                                               
            SetSkill( SkillName.EvalInt, 110, 120 );
            SetSkill( SkillName.Magery, 110, 115 );
            SetSkill( SkillName.EvalInt, 110, 115 );
            SetSkill( SkillName.Meditation, 110, 120 );
            SetSkill( SkillName.MagicResist, 95, 100 );
            SetSkill( SkillName.Wrestling, 110, 120 );
                                               
            Fame = 25000;
            Karma = 25000;
            VirtualArmor = 70;
            Tamable = false;
            
            switch ( Utility.Random( 10 ))
            {                                   
            	case 0: AddItem( new SwordOfIce() ); break;
            	case 1: AddItem( new ShieldOfIce() ); break;
            }
        }
            
            public override void GenerateLoot()
            {
            	PackGold( 5000, 8000 );
            	AddLoot( LootPack.FilthyRich, 2);
            	AddLoot( LootPack.Gems, Utility.Random( 1, 5));
            }
          
        public override bool ReacquireOnMovement{ get{ return true; }}
        public override int TreasureMapLevel{ get{ return 4; }}
        public override int Meat{ get{ return 19; }}
        public override int Hides{ get{ return 20; }}
        public override HideType HideType{ get{ return HideType.Barbed; }}
        public override int Scales{ get{ return 9; }}
        public override ScaleType ScaleType{ get{ return ScaleType.White; }}
        public override bool BardImmune{ get{ return true; } }

		public SoulofIce( Serial serial ) : base( serial )
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
             
             if ( Core.AOS && Body == 49 )
             	Body = 180;
         }
    }
}
