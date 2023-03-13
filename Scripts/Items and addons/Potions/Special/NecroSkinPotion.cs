using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class NecroSkinPotion : Item
	{
        [Constructable]
        public NecroSkinPotion() : base(0x1006)
		{
            Name = "jar of skull dust";
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Will Turn A Grandmaster Necromancer's Skin & Hair Ghostly White");
			list.Add( 1049644, "Double Click To Eat The Dust");
        } 

        public override void OnDoubleClick(Mobile from)
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else if ( from.Hue == 0x47E )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
				from.Hue = DB.CharHue;
				from.HairHue = DB.CharHairHue;
				from.FacialHairHue = DB.CharHairHue;
				from.SendMessage("Your body turns back to the colors of life.");
			}
			else if ( from.Skills[SkillName.Necromancy].Base >= 100 )
			{
				from.Hue = 0x47E;
				from.HairHue = 0x47E;
				from.FacialHairHue = 0x47E;
				from.SendMessage("Your body turns a ghostly white.");
			}
			else
			{
				from.SendMessage("You eat the skull dust, leaving your mouth dry.");
				from.Thirst = 0;
			}
			this.Delete();
			from.AddToBackpack( new Jar() );
        }

        public NecroSkinPotion( Serial serial ) : base( serial )
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