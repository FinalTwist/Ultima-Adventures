using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class HairDyePotion : BasePotion
	{
        [Constructable]
        public HairDyePotion() : base( 0x180F, PotionEffect.HairDye )
		{
            Name = "hair dye potion";
			Hue = 0;
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Dye This Mixture Before Using");
			list.Add( 1049644, "Use To Change Your Hair Color");
        }

		public void ConsumeCharge( HairDyePotion potion, Mobile from )
		{
			potion.Consume();
			from.RevealingAction();
			BasePotion.PlayDrinkEffect( from );
			from.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) );
		}

        public override void Drink( Mobile from )
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
				from.SendMessage("Your hair changes color.");
				ConsumeCharge( this, from );
			}
			else
			{
				from.HairHue = this.Hue;
				from.FacialHairHue = this.Hue;
				from.SendMessage("Your hair changes color.");
				ConsumeCharge( this, from );
			}
        }

        public HairDyePotion( Serial serial ) : base( serial )
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