using System;
using Server;
using Server.Mobiles;

namespace Server.Items 
{ 
	public class Warhorse : EtherealMount 
	{ 
		[Constructable] 
		public Warhorse() : base( 0x211F, 0x3EA0 ) 
		{ 
			Name = "warhorse";
			Hue = 0;
			ItemID = 0x211F;	if ( Server.Misc.MyServerSettings.ClientVersion() ){ ItemID = 0x55DC; }
			RegularID = 0x211F;	if ( Server.Misc.MyServerSettings.ClientVersion() ){ RegularID = 0x55DC; }
			MountedID = 0x3EA0;	if ( Server.Misc.MyServerSettings.ClientVersion() ){ MountedID = 594; }
		} 

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Mount For Grandmaster Warriors");
        } 

		public Warhorse( Serial serial ) : base( serial ) 
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
			Name = "warhorse";
			Hue = 0;
			ItemID = 0x211F;	if ( Server.Misc.MyServerSettings.ClientVersion() ){ ItemID = 0x55DC; }
			RegularID = 0x211F;	if ( Server.Misc.MyServerSettings.ClientVersion() ){ RegularID = 0x55DC; }
			MountedID = 0x3EA0;	if ( Server.Misc.MyServerSettings.ClientVersion() ){ MountedID = 594; }
		} 
	}
}