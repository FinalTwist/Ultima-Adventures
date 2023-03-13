using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.ContextMenus;

namespace Server.Items
{
	public class AltarGodsEast : Item
	{
		[Constructable]
		public AltarGodsEast( ) : base( 0x2FF9 )
		{
			Movable = false;
			Name = "Altar of the Gods";
			switch ( Utility.Random( 5 ) )
			{
				case 0: Hue = 0x972; Name = "Bronze " + Name; break;
				case 1: Hue = 0xB92; Name = "Marble " + Name; break;
				case 2: Hue = 0xB8B; Name = "Granite " + Name; break;
				case 3: Hue = 0x966; Name = "Obsidian " + Name; break;
				case 4: Hue = 0x353; Name = "Stone " + Name; break;
			}
		}

		public AltarGodsEast( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarGodsSouth : Item
	{
		[Constructable]
		public AltarGodsSouth( ) : base( 0x2FFA )
		{
			Movable = false;
			Name = "Altar of the Gods";
			switch ( Utility.Random( 5 ) )
			{
				case 0: Hue = 0x972; Name = "Bronze " + Name; break;
				case 1: Hue = 0xB92; Name = "Marble " + Name; break;
				case 2: Hue = 0xB8B; Name = "Granite " + Name; break;
				case 3: Hue = 0x966; Name = "Obsidian " + Name; break;
				case 4: Hue = 0x353; Name = "Stone " + Name; break;
			}
		}

		public AltarGodsSouth( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarShrineEast : Item
	{
		[Constructable]
		public AltarShrineEast( ) : base( 0xED5 )
		{
			Movable = false;
			Name = "Shrine of the Gods";
			switch ( Utility.Random( 5 ) )
			{
				case 0: Hue = 0x972; Name = "Bronze " + Name; break;
				case 1: Hue = 0xB92; Name = "Marble " + Name; break;
				case 2: Hue = 0xB8B; Name = "Granite " + Name; break;
				case 3: Hue = 0x966; Name = "Obsidian " + Name; break;
				case 4: Hue = 0x353; Name = "Stone " + Name; break;
			}
		}

		public AltarShrineEast( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarShrineSouth : Item
	{
		[Constructable]
		public AltarShrineSouth( ) : base( 0xED4 )
		{
			Movable = false;
			Name = "Shrine of the Gods";
			switch ( Utility.Random( 5 ) )
			{
				case 0: Hue = 0x972; Name = "Bronze " + Name; break;
				case 1: Hue = 0xB92; Name = "Marble " + Name; break;
				case 2: Hue = 0xB8B; Name = "Granite " + Name; break;
				case 3: Hue = 0x966; Name = "Obsidian " + Name; break;
				case 4: Hue = 0x353; Name = "Stone " + Name; break;
			}
		}

		public AltarShrineSouth( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarStatue : Item
	{
		[Constructable]
		public AltarStatue( ) : base( 0x40BC )
		{
			Movable = false;
			Name = "Statue of a Goddess";
			switch ( Utility.Random( 5 ) )
			{
				case 0: Hue = 0x972; Name = "Bronze " + Name; break;
				case 1: Hue = 0xB92; Name = "Marble " + Name; break;
				case 2: Hue = 0xB8B; Name = "Granite " + Name; break;
				case 3: Hue = 0x966; Name = "Obsidian " + Name; break;
				case 4: Hue = 0x353; Name = "Stone " + Name; break;
			}
		}

		public AltarStatue( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarSea : Item
	{
		[Constructable]
		public AltarSea( ) : base( 0x507A )
		{
			Movable = false;
			ItemID = Utility.RandomList( 0x507A, 0x507B );
			Name = "Shrine of Amphitrite";
			switch ( Utility.Random( 5 ) )
			{
				case 0: Hue = 0x972; Name = "Bronze " + Name; break;
				case 1: Hue = 0xB92; Name = "Marble " + Name; break;
				case 2: Hue = 0xB8B; Name = "Granite " + Name; break;
				case 3: Hue = 0x966; Name = "Obsidian " + Name; break;
				case 4: Hue = 0x353; Name = "Stone " + Name; break;
			}
		}

		public AltarSea( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarEvil : Item
	{
		[Constructable]
		public AltarEvil( ) : base( 0x52B3 )
		{
			Movable = false;
			ItemID = Utility.RandomList( 0x52B3, 0x52B4 );
			Name = "Shrine of the Grim Reaper";
		}

		public AltarEvil( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarDaemon : Item
	{
		[Constructable]
		public AltarDaemon( ) : base( 0x4B46 )
		{
			Movable = false;
			Name = "Shrine of Azrael";
			switch ( Utility.Random( 8 ) )
			{
				case 0: Hue = 0xB71; Name = "Ember " + Name; break;
				case 1: Hue = 0xB7F; Name = "Bloodstone " + Name; break;
				case 2: Hue = 0xB63; Name = "Darkstone " + Name; break;
				case 3: Hue = 0xB54; Name = "Golden " + Name; break;
				case 4: Hue = 0xB51; Name = "Emerock " + Name; break;
				case 5: Hue = 0xB46; Name = "Icecoal " + Name; break;
				case 6: Hue = 0xB33; Name = "Magecore " + Name; break;
				case 7: Hue = 0xB28; Name = "Sludgerock " + Name; break;
			}
		}

		public AltarDaemon( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarWizard : Item
	{
		[Constructable]
		public AltarWizard( ) : base( 0x5465 )
		{
			Movable = false;
			ItemID = Utility.RandomList( 0x5465, 0x5466 );
			Name = "Shrine of the Archmage";
			Light = LightType.Circle225;
		}

		public AltarWizard( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarPlayer : Item
	{
		[Constructable]
		public AltarPlayer( ) : base( 0x121C )
		{
			Movable = false;
			ItemID = 0x121C;
			Name = "Shrine of the Avatar";
			Light = LightType.Circle225;
		}

		public AltarPlayer( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class AltarGargoyle : Item
	{
		[Constructable]
		public AltarGargoyle( ) : base( 0x4B46 )
		{
			Movable = false;
			Name = "Shrine of Sin'Vraal";
			switch ( Utility.Random( 5 ) )
			{
				case 0: Hue = 0x972; Name = "Bronze " + Name; break;
				case 1: Hue = 0xB92; Name = "Marble " + Name; break;
				case 2: Hue = 0xB8B; Name = "Granite " + Name; break;
				case 3: Hue = 0x966; Name = "Obsidian " + Name; break;
				case 4: Hue = 0x353; Name = "Stone " + Name; break;
			}
		}

		public AltarGargoyle( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			Ankhs.GetContextMenuEntries( from, this, list );
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
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
