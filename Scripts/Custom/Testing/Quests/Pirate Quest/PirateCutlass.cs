using System;
using Server;

namespace Server.Items
{

        public class PirateCutlass : Cutlass
        {

        public override int InitMinHits{ get{ return 200; } }
        public override int InitMaxHits{ get{ return 300; } }
                
                [Constructable]
                public PirateCutlass()
                {
                        Name = "Pirate Cutlass";
                        Hue = 1158;
                        Attributes.WeaponSpeed = 40;
                        Attributes.WeaponDamage = 60;
			Attributes.SpellDamage = 10;
			Attributes.BonusStam = 20;
                        Attributes.Luck = 200;
                        
                }

                public PirateCutlass( Serial Serial ) : base ( Serial )
                {
                }

                public override void Serialize( GenericWriter writer )
                {
                        base.Serialize( writer );

                        writer.Write( (int) 0 );
                }

                public override void Deserialize(GenericReader reader)
                {
                        base.Deserialize( reader );

                        int version = reader.ReadInt();
                }
       }
}
                        