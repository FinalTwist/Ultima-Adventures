using System;
using Server;

namespace Server.Items
{
           public class ZhouYuBow : Yumi
           {
                   public override int ArtifactRarity{ get{ return 10; } }

                   public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
                   public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }
  
                   [Constructable]
                   public ZhouYuBow()
                   {
                           Hue = 1892;
                           Name = "Zhou Yu's Bow";
                           Weight = 4.0;
                           WeaponAttributes.HitLightning = 65;
                           Attributes.SpellChanneling = 1;
                           Attributes.WeaponDamage = 40;
                   }

                   public ZhouYuBow( Serial serial ) : base( serial )
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