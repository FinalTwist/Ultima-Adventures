/////////Kaza Wuz H3r3/////////
using System;
using Server;

namespace Server.Items
{
        public class ZhouYuRobe : Kamishimo
        {
                public override int ArtifactRarity{ get{ return 10; } }

                public override int BasePhysicalResistance{ get{ return 10; } }
                public override int BaseFireResistance{ get{ return 10; } }
                public override int BaseColdResistance{ get{ return 10; } }
                public override int BasePoisonResistance{ get{ return 10; } }
                public override int BaseEnergyResistance{ get{ return 10; } }

                [Constructable]
                public ZhouYuRobe()
                {
                         Name = "Zhou Yu's Robe";
                         Hue = 39;
                         Weight = 1.0;
                         Attributes.DefendChance = 10;
                         Attributes.ReflectPhysical = 10;
                }

                public ZhouYuRobe( Serial serial ) : base( serial )
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