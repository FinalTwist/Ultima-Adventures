using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Items
{
	public class BardHarkynBox : Item
	{
		[Constructable]
		public BardHarkynBox() : base( 0x9AB )
		{
			Name = "Harkyn's Treasure Chest";
			Movable = false;
			Hue = 0x556;
		}

		public BardHarkynBox( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				PlayerMobile pm = (PlayerMobile)from;

				if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSilverSquare" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You find nothing of interest.", from.NetState);
				}
				else
				{
					CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleSilverSquare", true );
					CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleCrystalSword", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You found a silver square and a crystal sword.", from.NetState);
					from.CloseGump( typeof(Server.Gumps.ClueGump) );
					from.SendGump(new Server.Gumps.ClueGump( "You found a silver square and a crystal sword. You will need all three silver shapes to obtain the silver key to Mangar's chamber door. When you have all three, seek the golden skull and use it to place the shapes upon it. The sword looks strong enough to shatter crystal sculptures.", "The Silver Square" ) );
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class BardKylearanBox : Item
	{
		[Constructable]
		public BardKylearanBox() : base( 0xE40 )
		{
			Name = "Kylearan's Treasure Chest";
			Movable = false;
			Hue = 0x48E;
		}

		public BardKylearanBox( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSilverTriangle" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You find nothing of interest.", from.NetState);
				}
				else
				{
					CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleSilverTriangle", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You found a silver triangle.", from.NetState);
					from.CloseGump( typeof(Server.Gumps.ClueGump) );
					from.SendGump(new Server.Gumps.ClueGump( "You found a silver triangle. You will need all three silver shapes to obtain the silver key to Mangar's chamber door. When you have all three, seek the golden skull and use it to place the shapes upon it.", "The Silver Triangle" ) );
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class BardMangarBox : Item
	{
		[Constructable]
		public BardMangarBox() : base( 0xE40 )
		{
			Name = "Mangar's Treasure Chest";
			Movable = false;
			Hue = 0x489;
		}

		public BardMangarBox( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				PlayerMobile pm = (PlayerMobile)from;

				if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSilverCircle" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You find nothing of interest.", from.NetState);
				}
				else
				{
					CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleSilverCircle", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You found a silver circle.", from.NetState);
					from.CloseGump( typeof(Server.Gumps.ClueGump) );
					from.SendGump(new Server.Gumps.ClueGump( "You found a silver circle. You will need all three silver shapes to obtain the silver key to Mangar's chamber door. When you have all three, seek the golden skull and use it to place the shapes upon it.", "The Silver Circle" ) );
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class BardGoldSkull : Item
	{
		[Constructable]
		public BardGoldSkull() : base( 0x2203 )
		{
			Name = "a golden skull";
			Movable = false;
			Hue = 1281;
		}

		public BardGoldSkull( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				PlayerMobile pm = (PlayerMobile)from;

				if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleMangarKey" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "This golden skull has an eerie glow.", from.NetState);
				}
				else if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSilverSquare" ) && 
					CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSilverTriangle" ) && 
					CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSilverCircle" ) )
				{
					CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleMangarKey", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "Placing the 3 silver shapes on the skull, the mouth opens to reveal a silver key.", from.NetState);
					from.CloseGump( typeof(Server.Gumps.ClueGump) );
					from.SendGump(new Server.Gumps.ClueGump( "You have obtained the silver key from the golden skull. Perhaps it works on that dark door to the west of you.", "The Silver Key" ) );
				}
				else
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "This golden skull has an eerie glow, and there seems to be 3 different shapes carved on it.", from.NetState);
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class CrystalStatueBoxKyl : Item
	{
		[Constructable]
		public CrystalStatueBoxKyl() : base( 0xE80 )
		{
			Name = "jade box";
			Movable = false;
			Hue = 0xB95;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		}

		public CrystalStatueBoxKyl( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleBedroomKey" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You find nothing of interest.", from.NetState);
				}
				else
				{
					CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleBedroomKey", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You found a key with a symbol of a tree on it.", from.NetState);
					from.CloseGump( typeof(Server.Gumps.ClueGump) );
					from.SendGump(new Server.Gumps.ClueGump( "You found a key with a symbol of a tree on it.", "The Forest Key" ) );
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
			CrystalStatueKyl MyStatue = new CrystalStatueKyl();
			Point3D loc = new Point3D( 5840, 2337, 0 );
			MyStatue.MoveToWorld( loc, this.Map );
			this.Delete();
		}

		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 
			public ItemRemovalTimer( Item item ) : base( TimeSpan.FromMinutes( 10.0 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = item; 
			} 

			protected override void OnTick() 
			{ 
				if (( i_item != null ) && ( !i_item.Deleted ))
				{
					CrystalStatueKyl MyStatue = new CrystalStatueKyl();
					Point3D loc = new Point3D( 5840, 2337, 0 );
					MyStatue.MoveToWorld( loc, i_item.Map );
					i_item.Delete();
				}
			} 
		} 
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class CrystalStatueKyl : Item
	{
		[Constructable]
		public CrystalStatueKyl() : base( 0x40BC )
		{
			Name = "a crystal statue";
			Movable = false;
			Hue = 0x480;
			Weight = -2;
		}

		public CrystalStatueKyl( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleBedroomKey" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "This crystal statue looks beautiful.", from.NetState);
				}
				else if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleCrystalSword" ) && !( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleBedroomKey" ) ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You hit the statue with the crystal sword, shattering both the statue and the sword.", from.NetState);
					from.PlaySound( 0x040 );

					CrystalStatueBoxKyl MyChest = new CrystalStatueBoxKyl();

					Map map = this.Map;

					bool validLocation = false;
					Point3D loc = this.Location;

					for ( int j = 0; !validLocation && j < 10; ++j )
					{
						int x = X + Utility.Random( 3 ) - 1;
						int y = Y + Utility.Random( 3 ) - 1;
						int z = map.GetAverageZ( x, y );

						if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
							loc = new Point3D( x, y, Z );
						else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
							loc = new Point3D( x, y, z );
					}

					MyChest.MoveToWorld( loc, map );

					this.Delete();
				}
				else
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You can make out a small jade box within the crystal statue.", from.NetState);
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DwarvenBox : Item
	{
		[Constructable]
		public DwarvenBox() : base( 0x2DF1 )
		{
			Name = "Ancient Dwarven Chest";
			Movable = false;
		}

		public DwarvenBox( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( CharacterDatabase.GetKeys( from, "UndermountainKey" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You find nothing of interest.", from.NetState);
				}
				else
				{
					CharacterDatabase.SetKeys( from, "UndermountainKey", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You found a dwarven key.", from.NetState);
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SkullGateBook : Item
	{
		[Constructable]
		public SkullGateBook() : base( 0x27BA )
		{
			Name = "Manual of Skull Gate";
			Movable = false;
			Hue = 0x9C4;
		}

		public SkullGateBook( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( CharacterDatabase.GetKeys( from, "SkullGate" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You already learned this book's secrets.", from.NetState);
				}
				else
				{
					CharacterDatabase.SetKeys( from, "SkullGate", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You learned the secrets of the Skull Gate.", from.NetState);
				}
				from.SendMessage( "Check your quest log for details on the locations." );
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SerpentPillarBook : Item
	{
		[Constructable]
		public SerpentPillarBook() : base( 0x5689 )
		{
			Name = "The Serpent Pillars";
			Movable = false;
			Hue = 0xB20;
		}

		public SerpentPillarBook( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( CharacterDatabase.GetKeys( from, "SerpentPillars" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You already learned this book's secrets.", from.NetState);
				}
				else
				{
					CharacterDatabase.SetKeys( from, "SerpentPillars", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You learned the secrets of the Serpent Pillars.", from.NetState);
				}
				from.SendMessage( "Check your quest log for details on the locations." );
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DragonRidingScroll : Item
	{
		[Constructable]
		public DragonRidingScroll() : base( 0x02DD )
		{
			Name = "The Dragon Riders";
		}

		public DragonRidingScroll( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( !Server.Misc.MyServerSettings.ClientVersion() )
				{
					CharacterDatabase.SetKeys( from, "DragonRiding", true );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You learned of the Zuluu's ability to ride dragyns.", from.NetState);
					this.Delete();
				}
				else if ( CharacterDatabase.GetKeys( from, "DragonRiding" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You already learned these secrets so you toss it out.", from.NetState);
					this.Delete();
				}
				else
				{
					CharacterDatabase.SetKeys( from, "DragonRiding", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You learned the secrets of dragon riding.", from.NetState);
					this.Delete();
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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