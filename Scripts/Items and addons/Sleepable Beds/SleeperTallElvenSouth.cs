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
	public class SleeperTallElvenSouthAddon: SleeperBaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SleeperTallElvenSouthAddonDeed();
			}
		}
	
		public SleeperTallElvenSouthAddon( Serial serial ) : base( serial )
		{
		}
	
		[Constructable]
		public SleeperTallElvenSouthAddon( ) 
		{
			Visible = true;
			Name = "Sleeper";
			AddComponent( new SleeperPiece( this,0x3058 ),  0,  0, 0 ); // angolo alto sx
			AddComponent( new SleeperPiece( this,0x3057 ), -1,  1, 0 ); // angolo basso sx
			AddComponent( new SleeperPiece( this,0x3059 ),  0, -1, 0 ); // angolo alto dx
			AddComponent( new SleeperPiece( this,0x3056 ),  0,  1, 0 ); // angolo basso dx
		}

		public override void OnDoubleClick( Mobile from )
		{
			UseBed(from, new Point3D(this.Location.X, this.Location.Y+2, this.Location.Z+14), Direction.South);
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
	
	public class SleeperTallElvenSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SleeperTallElvenSouthAddon ();
			}
		}
	
		[Constructable]
		public SleeperTallElvenSouthAddonDeed ()
		{
			Name = "a tall elven sleepable bed facing south deed";
		}
	
		public SleeperTallElvenSouthAddonDeed ( Serial serial )
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
	public class SleeperTallElvenSouthPiece : SleeperPiece
	{
		public SleeperTallElvenSouthPiece( Serial serial ): base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); }
	}
}
