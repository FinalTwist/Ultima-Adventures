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
	public class SleeperElvenSouthAddon: SleeperBaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SleeperElvenSouthAddonDeed ();
			}
		}
	
		public SleeperElvenSouthAddon( Serial serial ) : base( serial )
		{
		}
	
		[Constructable]
		public SleeperElvenSouthAddon( ) 
		{
			Visible = true;
			Name = "Sleeper";
			AddComponent( new SleeperPiece(this, 0x3050 ), 0, 0, 0 );
			AddComponent( new SleeperPiece(this, 0x3051 ), 0, -1, 0 );
		}

		public override void OnDoubleClick( Mobile from )
		{
			UseBed(from, new Point3D(this.Location.X, this.Location.Y+1, this.Location.Z+8), Direction.South);
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
	
	public class SleeperElvenSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
			return new SleeperElvenSouthAddon ();
			}
		}
	
		[Constructable]
		public SleeperElvenSouthAddonDeed ()
		{
			Name = "an elven sleepable bed facing south deed";
		}
	
		public SleeperElvenSouthAddonDeed ( Serial serial )
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
	public class SleeperElvenSouthPiece : SleeperPiece
	{
		public SleeperElvenSouthPiece( Serial serial ): base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); }
	}
}