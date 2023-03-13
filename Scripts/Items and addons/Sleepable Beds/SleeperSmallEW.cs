using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Multis;

namespace Server.Items
{
	// version 1.1.1 Bed coordinates of 0,0,0 will cause npc to sleep and wake at it's current location.
	// version 1.0 initial release.
	public class SleeperSmallEWAddon: SleeperBaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SleeperSmallEWAddonDeed();
			}
		}
	
		public SleeperSmallEWAddon( Serial serial ) : base( serial )
		{
		}
	
		[Constructable]
		public SleeperSmallEWAddon( ) 
		{
			Visible = true;
			Name = "Sleeper";
			AddComponent( new SleeperSmallEWPiece(this,   0xA5D ), 0, 0, 0 );
			AddComponent( new SleeperSmallEWPiece(this,   0xA62 ), 1, 0, 0 );
		}

		public override void OnDoubleClick( Mobile from )
		{
			UseBed(from, new Point3D(this.Location.X+2, this.Location.Y, this.Location.Z+8), Direction.East);
		}
	
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			// don't read any serialization for old scripts, it's read in the base class
			if (OldStyleSleepers)
				return;
			
			int version = reader.ReadInt();
		}
	}
	
	public class SleeperSmallEWAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SleeperSmallEWAddon ();
			}
		}
	
		[Constructable]
		public SleeperSmallEWAddonDeed ()
		{
			Name = "a small sleepable bed facing east deed";
		}
	
		public SleeperSmallEWAddonDeed ( Serial serial )
		: base ( serial )
		{
		}
	
		public override void Serialize ( GenericWriter writer )
		{
			base.Serialize ( writer );
			writer.Write ( 0 ); // Version
		}
	
		public override void Deserialize ( GenericReader reader )
		{
			base.Deserialize ( reader );
			int version = reader.ReadInt ();
		}
	}
	
	// Eni - the below is necesary to be compatible with older versions of the script
	// also because of furniture dyability.
	[Furniture]
	public class SleeperSmallEWPiece : SleeperPiece
	{
		public SleeperSmallEWPiece( SleeperBaseAddon sleeper, int itemid ) : base( sleeper, itemid )
		{
		}
	
		public SleeperSmallEWPiece( Serial serial ): base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); }
	}
}
