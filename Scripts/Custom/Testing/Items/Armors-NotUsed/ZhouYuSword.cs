//////////Kaza wuz h3r3/////////
using System;
using Server;

namespace Server.Items
{
        public class ZhouYuSword : NoDachi
        {
                public override int ArtifactRarity{ get{ return 11; } }

                public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
                public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

                [Constructable]
                public ZhouYuSword()
                {
                       Name = "Zhou Yu's Blade";
                       Weight = 5.0;
                       
                       Attributes.WeaponDamage = 50;
                       Attributes.WeaponSpeed = 20;
                       Attributes.SpellChanneling = 1;
                       WeaponAttributes.HitLightning = 50;
                       WeaponAttributes.HitLowerDefend = 50;
                }
 
                public ZhouYuSword( Serial serial ) : base( serial )
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