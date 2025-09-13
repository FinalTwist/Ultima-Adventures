using System;
using Server;

namespace Server.Items
{

        public class TitanWarriorSandals : Sandals
        {
                public override int BasePhysicalResistance{ get{ return 3; } }
                public override int BaseFireResistance{ get{ return 0; } }
                public override int BaseColdResistance{ get{ return 4; } }
                public override int BasePoisonResistance{ get{ return 0; } }
                public override int BaseEnergyResistance{ get{ return 5; } }
                
                [Constructable]
                public TitanWarriorSandals()
                {
                        Name = "Titan Warrior Sandals";
                        Hue = Utility.RandomList( 1161, 1150, 1266 );
                        Attributes.BonusDex = 10;
			Attributes.BonusStr = 15;
                }

                public TitanWarriorSandals( Serial Serial ) : base ( Serial )
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
                        