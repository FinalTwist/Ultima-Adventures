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
	public class SleeperSmallSouthAddon: SleeperBaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SleeperSmallSouthAddonDeed();
			}
		}
	
		public SleeperSmallSouthAddon( Serial serial ) : base( serial )
		{
		}
	
		[Constructable]
		public SleeperSmallSouthAddon( ) 
		{
			Visible = true;
			Name = "Sleeper";
			AddComponent( new SleeperSmallSouthPiece(  this,0xA63 ), 0, 0, 0 );
			AddComponent( new SleeperSmallSouthPiece(  this,0xA5C ), 0, 1, 0 );
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			UseBed(from, new Point3D(this.Location.X, this.Location.Y+2, this.Location.Z+8), Direction.South);
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
	
	public class SleeperSmallSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SleeperSmallSouthAddon ();
			}
		}
	
		[Constructable]
		public SleeperSmallSouthAddonDeed ()
		{
			Name = "a small sleepable bed facing south deed";
		}
	
		public SleeperSmallSouthAddonDeed ( Serial serial )
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
	public class SleeperSmallSouthPiece : SleeperPiece
	{
		public SleeperSmallSouthPiece( SleeperBaseAddon sleeper, int itemid ) : base( sleeper, itemid )
		{
		}
	
		public SleeperSmallSouthPiece( Serial serial ): base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); }
	}
}
