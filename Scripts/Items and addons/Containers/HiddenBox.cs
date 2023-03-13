using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using Server.Misc;

namespace Server.Items
{
	[Furniture]
	public class HiddenContainer : LockableContainer
	{
		[Constructable]
		public HiddenContainer() : base( 0xe43 )
		{
		}

		public HiddenContainer( Serial serial ) : base( serial )
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

	public class HiddenBox : HiddenContainer
	{
		public int ContainerID;
		public int ContainerGump;
		public int ContainerHue;
		public int ContainerFlip;
		public double ContainerWeight;
		public string ContainerName;
		public string ContainerOwner;
		public string ContainerLocation;

		[CommandProperty(AccessLevel.Owner)]
		public int Container_ID { get { return ContainerID; } set { ContainerID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Gump { get { return ContainerGump; } set { ContainerGump = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Hue { get { return ContainerHue; } set { ContainerHue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Flip { get { return ContainerFlip; } set { ContainerFlip = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public double Container_Weight { get { return ContainerWeight; } set { ContainerWeight = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Container_Name { get { return ContainerName; } set { ContainerName = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Container_Owner { get { return ContainerOwner; } set { ContainerOwner = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Container_Location { get { return ContainerLocation; } set { ContainerLocation = value; InvalidateProperties(); } }

		[Constructable]
		public HiddenBox( int level, string my_location, Mobile finder ) : base()
		{
			ContainerLocation = my_location;

			string sOwner = "";

			Weight = 5.0;
			Name = "treasure chest";

			int nOwner = 0;

			if ( Server.Misc.Worlds.IsOnSpaceship( finder.Location, finder.Map ) )
			{
				nOwner = ContainerFunctions.BuildContainer( this, 0, 18, 0, 0 );
			}
			else
			{
				nOwner = ContainerFunctions.BuildContainer( this, 0, 0, 0, 0 );
			}

			ContainerFunctions.FillTheContainer( level, this, finder );
			if ( GetPlayerInfo.LuckyPlayer( finder.Luck, finder ) ){ ContainerFunctions.FillTheContainer( level, this, finder ); }

			ContainerFunctions.LockTheContainer( level, this, 1 );

			if ( nOwner == 1 ){ sOwner = "chest"; }
			else if ( nOwner == 2 ){ sOwner = "chest"; }
			else if ( nOwner == 3 ){ sOwner = "footlocker"; }
			else if ( nOwner == 4 ){ sOwner = "trunk"; }
			else if ( nOwner == 5 ){ sOwner = "box"; }
			else if ( nOwner == 6 ){ sOwner = "box"; }
			else if ( nOwner == 7 ){ sOwner = "bag"; Locked = false; LockLevel = 0; MaxLockLevel = 0; RequiredSkill = 0; }
			else if ( nOwner == 8 ){ sOwner = "backpack"; Locked = false; LockLevel = 0; MaxLockLevel = 0; RequiredSkill = 0; }
			else if ( nOwner == 9 ){ sOwner = "crate"; Locked = false; LockLevel = 0; MaxLockLevel = 0; RequiredSkill = 0; }
			else if ( nOwner == 10 ){ sOwner = "crate"; Locked = false; LockLevel = 0; MaxLockLevel = 0; RequiredSkill = 0; }
			else if ( nOwner == 18 ){ sOwner = "cargo"; }
			else { sOwner = "barrel"; Locked = false; LockLevel = 0; MaxLockLevel = 0; RequiredSkill = 0; }

			ContainerID = this.ItemID;
			ContainerGump = this.GumpID;
			ContainerHue = this.Hue;
			ContainerName = this.Name;
			ContainerWeight = this.Weight;
			if ( this.ItemID == 0xE3F ){ ContainerFlip = 0xE3E; }
			else if ( this.ItemID == 0xE3E ){ ContainerFlip = 0xE3F; }
			else if ( this.ItemID == 0xE3D ){ ContainerFlip = 0xE3C; }
			else if ( this.ItemID == 0xE75 ){ ContainerFlip = 0x53D5; }
			else if ( this.ItemID == 0x9A8 ){ ContainerFlip = 0xE80; }
			else if ( this.ItemID == 0x9AA ){ ContainerFlip = 0xE7D; }
			else if ( this.ItemID == 0x2813 ){ ContainerFlip = 0x2814; }
			else if ( this.ItemID == 0x2811 ){ ContainerFlip = 0x2812; }
			else if ( this.ItemID == 0xe40 ){ ContainerFlip = 0xe41; }
			else if ( this.ItemID == 0xe42 ){ ContainerFlip = 0xe43; }
			else if ( this.ItemID == 0xE3C ){ ContainerFlip = 0xE3D; }
			else if ( this.ItemID == 0x53D5 ){ ContainerFlip = 0xE75; }
			else if ( this.ItemID == 0xE80 ){ ContainerFlip = 0x9A8; }
			else if ( this.ItemID == 0xE7D ){ ContainerFlip = 0x9AA; }
			else if ( this.ItemID == 0x2814 ){ ContainerFlip = 0x2813; }
			else if ( this.ItemID == 0x2812 ){ ContainerFlip = 0x2811; }
			else if ( this.ItemID == 0xe41 ){ ContainerFlip = 0xe40; }
			else if ( this.ItemID == 0xe43 ){ ContainerFlip = 0xe42; }
			else if ( this.ItemID == 0xE76 ){ ContainerFlip = 0xE76; }
			else if ( this.ItemID == 0x281D ){ ContainerFlip = 0x281E; }
			else if ( this.ItemID == 0x281F ){ ContainerFlip = 0x2820; }
			else if ( this.ItemID == 0x2821 ){ ContainerFlip = 0x2822; }
			else if ( this.ItemID == 0x2825 ){ ContainerFlip = 0x2826; }
			else if ( this.ItemID == 0x2823 ){ ContainerFlip = 0x2824; }
			else if ( this.ItemID == 0x3330 ){ ContainerFlip = 0x3331; }
			else if ( this.ItemID == 0x3332 ){ ContainerFlip = 0x3333; }
			else if ( this.ItemID == 0x3334 ){ ContainerFlip = 0x3335; }
			else if ( this.ItemID == 0x3336 ){ ContainerFlip = 0x3337; }
			else if ( this.ItemID == 0x10EA ){ ContainerFlip = 0x10EB; }
			else if ( this.ItemID == 0x10EC ){ ContainerFlip = 0x10ED; }
			else if ( this.ItemID == 0x281E ){ ContainerFlip = 0x281D; }
			else if ( this.ItemID == 0x2820 ){ ContainerFlip = 0x281F; }
			else if ( this.ItemID == 0x2822 ){ ContainerFlip = 0x2821; }
			else if ( this.ItemID == 0x2826 ){ ContainerFlip = 0x2825; }
			else if ( this.ItemID == 0x2824 ){ ContainerFlip = 0x2823; }
			else if ( this.ItemID == 0x3331 ){ ContainerFlip = 0x3330; }
			else if ( this.ItemID == 0x3333 ){ ContainerFlip = 0x3332; }
			else if ( this.ItemID == 0x3335 ){ ContainerFlip = 0x3334; }
			else if ( this.ItemID == 0x3337 ){ ContainerFlip = 0x3336; }
			else if ( this.ItemID == 0x10EB ){ ContainerFlip = 0x10EA; }
			else if ( this.ItemID == 0x10ED ){ ContainerFlip = 0x10EC; }
			else if ( this.ItemID == 0x3866 ){ ContainerFlip = 0x3867; }
			else if ( this.ItemID == 0x3867 ){ ContainerFlip = 0x3866; }
			else { ContainerFlip = this.ItemID; }

			ContainerOwner = ContainerFunctions.GetOwner( sOwner );
		}

		public HiddenBox( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, ContainerOwner );
			list.Add( 1049644, "Found In " + ContainerLocation + "" ); // PARENTHESIS
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( ContainerID );
            writer.Write( ContainerGump );
            writer.Write( ContainerHue );
            writer.Write( ContainerFlip );
            writer.Write( ContainerWeight );
            writer.Write( ContainerName );
            writer.Write( ContainerOwner );
            writer.Write( ContainerLocation );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            ContainerID = reader.ReadInt();
            ContainerGump = reader.ReadInt();
            ContainerHue = reader.ReadInt();
            ContainerFlip = reader.ReadInt();
            ContainerWeight = reader.ReadDouble();
            ContainerName = reader.ReadString();
			ContainerOwner = reader.ReadString();
			ContainerLocation = reader.ReadString();
		}
	}
}