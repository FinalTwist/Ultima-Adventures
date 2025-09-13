using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class HairDyeBottle : Item
	{
        [Constructable]
        public HairDyeBottle() : base(0xE0F)
		{
            Name = "hair dye mixture";
			Hue = 0;
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Dye This Mixture Before Using");
			list.Add( 1049644, "Use To Change Your Hair Color");
        } 

        public override void OnDoubleClick(Mobile from)
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else if ( this.Hue == 0 )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
				from.HairHue = DB.CharHairHue;
				from.FacialHairHue = DB.CharHairHue;
				from.SendMessage("You use the neutral dye to color your hair back to normal.");
				from.PlaySound( 0x5A4 );
			}
			else
			{
				from.HairHue = this.Hue;
				from.FacialHairHue = this.Hue;
				from.SendMessage("You dye your hair a new color.");
				from.PlaySound( 0x5A4 );
			}
			this.Delete();
        }

        public HairDyeBottle( Serial serial ) : base( serial )
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