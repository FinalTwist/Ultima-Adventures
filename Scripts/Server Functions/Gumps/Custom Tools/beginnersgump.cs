//September 2009 - Grom

using System;
using Server;
using System.Collections;
using Server.Guilds;
using Server.Network;
using Server.Items;
using Server.Gumps;

namespace Server.Gumps
{
	public class BeginnersGump : Gump
	{
		public BeginnersGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 100, 350, 300, 9270);
			this.AddBackground(120, 120, 96, 260, 9350);
			this.AddBackground(218, 120, 212, 260, 9350);
			this.AddButton(125, 180, 2118, 2117, (int)Buttons.B1, GumpButtonType.Page, 1);
			this.AddButton(125, 200, 2118, 2117, (int)Buttons.B2, GumpButtonType.Page, 2);
			this.AddButton(125, 220, 2118, 2117, (int)Buttons.B3, GumpButtonType.Page, 3);
			this.AddButton(125, 240, 2118, 2117, (int)Buttons.B4, GumpButtonType.Page, 4);
			this.AddButton(125, 260, 2118, 2117, (int)Buttons.B5, GumpButtonType.Page, 5);
			this.AddButton(125, 280, 2118, 2117, (int)Buttons.B6, GumpButtonType.Page, 6);
			this.AddButton(125, 300, 2118, 2117, (int)Buttons.B7, GumpButtonType.Page, 7);
			this.AddLabel(145, 180, 0, @"News");
			this.AddLabel(145, 200, 0, @"Rules");
			this.AddLabel(145, 220, 0, @"Shard Info");
			this.AddLabel(145, 240, 0, @"World");
			this.AddLabel(145, 260, 0, @"Commands");
			this.AddLabel(145, 280, 0, @"Artifacts");
			this.AddLabel(145, 300, 0, @"Contacts");
			this.AddLabel(295, 360, 0, @"www.uoice.webs.com");
			this.AddLabel(175, 360, 0, @"Grom");
			this.AddPage(1);
			this.AddLabel(290, 140, 0, @"News");
			this.AddLabel(230, 180, 0, @"This is a beginners guide for");
			this.AddLabel(230, 200, 0, @"UO Armageddon - Ice Era.");
			this.AddLabel(230, 220, 0, @"Read monthly newspapers which");
			this.AddLabel(230, 240, 0, @"can be purchased from the bank.");
			this.AddLabel(230, 260, 0, @"Extra information in town");
			this.AddLabel(230, 280, 0, @"Great New PvP shard");
			this.AddLabel(230, 300, 0, @"No Giveaways, No Shit");
			this.AddPage(2);
			this.AddLabel(290, 140, 0, @"Rules");
			this.AddLabel(230, 180, 0, @"This is one of the only shards ");
			this.AddLabel(230, 200, 0, @"where anything goes.");
			this.AddLabel(230, 220, 0, @"Res-killing allowed");
			this.AddLabel(230, 240, 0, @"Un-attended macroing allowed");
			this.AddLabel(230, 260, 0, @"Blocking allowed");
			this.AddLabel(230, 280, 0, @"Only 1 account allowed");
			this.AddLabel(230, 300, 0, @"Enjoy");
			this.AddPage(3);
			this.AddLabel(290, 140, 0, @"Shard Info");
			this.AddLabel(230, 180, 0, @"UO Armageddon - Ice Era");
			this.AddLabel(230, 200, 0, @"client patch 6.0.0.0");
			this.AddLabel(230, 220, 0, @"68.148.226.141 : 2593");
			this.AddLabel(230, 240, 0, @"No patching required");
			this.AddLabel(230, 260, 0, @"fc-fcr 2/6");
			this.AddLabel(230, 280, 0, @"1 account, 2 houses");
			this.AddLabel(230, 300, 0, @"700skill 225stats + ps+ss");
			this.AddPage(4);
			this.AddLabel(290, 140, 0, @"World");
			this.AddLabel(230, 180, 0, @"Shard area - apx 140 houses");
			this.AddLabel(230, 200, 0, @"Felucca Only - Deceit Only");
			this.AddLabel(230, 220, 0, @"Doom dungeon + Dread dungeon");
			this.AddLabel(230, 240, 0, @"and Blighted Grove");
			this.AddLabel(230, 300, 0, @"PVP-PK Everywhere");
			this.AddPage(5);
			this.AddLabel(290, 140, 0, @"Commands");
			this.AddLabel(230, 180, 0, @"[help - main command for a list");
			this.AddLabel(230, 200, 0, @"[who - check who is online");
			this.AddLabel(230, 220, 0, @"[totals - check your properties");
			this.AddLabel(230, 240, 0, @"[www - opens shard website");
			this.AddLabel(230, 260, 0, @"[jailinfo - check your jail time");
			this.AddLabel(230, 280, 0, @"[hunger - check your hunger");
			this.AddPage(6);
			this.AddLabel(290, 140, 0, @"Artifacts");
			this.AddLabel(230, 180, 0, @"Artifact chances are set to 1%");
			this.AddLabel(230, 200, 0, @"15 Doom Artifacts");
			this.AddLabel(230, 220, 0, @"12 Dread Artifacts");
			this.AddLabel(230, 240, 0, @"Paragon Bricks");
			this.AddLabel(230, 260, 0, @"Tokuno Urn");
			this.AddLabel(230, 280, 0, @"For more deco and artifacts");
			this.AddLabel(230, 300, 0, @"please see artifact house");
			this.AddPage(7);
			this.AddLabel(290, 140, 0, @"Contacts");
			this.AddLabel(230, 180, 0, @"For Admin - Mike");
			this.AddLabel(230, 200, 0, @"For GMs - ");
			this.AddLabel(230, 240, 0, @"Webpage - www.uoice.webs.com");
			this.AddLabel(230, 280, 0, @"For Questions contact Owner");
			this.AddLabel(230, 300, 0, @"Mike - michal_09@hotmail.com");

		}
		
		public enum Buttons
		{
			B1,
			B2,
			B3,
			B4,
			B5,
			B6,
			B7,
		}

	}
}

	public class BeginnerScroll : Item
	{ 
		[Constructable] 
		public  BeginnerScroll() : base( 0x14F0 ) 
		{ 
			Weight = 1.0; 
			Name = "Beginner Scroll"; 
         		LootType = LootType.Blessed; 
		}

		public override void OnDoubleClick( Mobile m ) 
		{				
			if 	( m.InRange( this.GetWorldLocation(), 2 ))
                     		m.SendGump( new BeginnersGump() );
			else
				m.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.

                     //m.CloseGump( typeof( BeginnersGump ) );
                     //m.SendGump( new BeginnersGump() );
		} 

		public BeginnerScroll( Serial serial ) : base( serial ) 
		{ 
		} 
       
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version 
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}