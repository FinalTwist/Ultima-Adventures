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
	public class SleeperTallElvenEastAddon: SleeperBaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SleeperTallElvenEastAddonDeed();
			}
		}
	
		public SleeperTallElvenEastAddon( Serial serial ) : base( serial )
		{
		}
	
		[Constructable]
		public SleeperTallElvenEastAddon( ) 
		{
			Visible = true;
			Name = "Sleeper";
			AddComponent( new SleeperPiece( this, 0x3054 ), 0,  0, 0 );
			AddComponent( new SleeperPiece( this, 0x3053 ), 1,  0, 0 );
			AddComponent( new SleeperPiece( this, 0x3055 ), 2, -1, 0 );
			AddComponent( new SleeperPiece( this, 0x3052 ), 2,  0, 0 );
		}

		public override void OnDoubleClick( Mobile from )
		{
			UseBed(from, new Point3D(this.Location.X+3, this.Location.Y, this.Location.Z+14), Direction.East);
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
	
	public class SleeperTallElvenEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SleeperTallElvenEastAddon ();
			}
		}
	
		[Constructable]
		public SleeperTallElvenEastAddonDeed ()
		{
			Name = "a tall elven sleepable bed facing east deed";
		}
	
		public SleeperTallElvenEastAddonDeed ( Serial serial )
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
	public class SleeperTallElvenEastPiece : SleeperPiece
	{
		public SleeperTallElvenEastPiece( Serial serial ): base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); }
	}
}
