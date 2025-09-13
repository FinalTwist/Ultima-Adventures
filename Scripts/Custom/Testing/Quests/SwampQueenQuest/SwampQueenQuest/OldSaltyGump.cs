using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class OldSaltyGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "OldSaltyGump", AccessLevel.GameMaster, new CommandEventHandler( OldSaltyGump_OnCommand ) ); 
      } 

      private static void OldSaltyGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new OldSaltyGump( e.Mobile ) ); 
      } 

      public OldSaltyGump( Mobile owner ) : base( 50,50 ) 
      { 
//----------------------------------------------------------------------------------------------------

				AddPage( 0 );
			AddImageTiled(  54, 33, 369, 400, 2624 );
			AddAlphaRegion( 54, 33, 369, 400 );

			AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
			
			AddImage( 97, 49, 9005 );
			AddImageTiled( 58, 39, 29, 390, 10460 );
			AddImageTiled( 412, 37, 31, 389, 10460 );
			AddLabel( 140, 60, 0x34, "Old man Salty" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=White><I>The old man looks up from his ale, and mumbles 'what do you want stranger? Buy me an ale, I'll Talk.</I><br><br>" +
"<BASEFONT COLOR=White>Well young one I know why you are here, and I'll let ye know, that what you ask is no easy task. Years ago, I barely escaped with my life trapping that nasty old Swamp Queen.<br><br>" + 
"<BASEFONT COLOR=White>Many years ago I was exploring and found the nasty old Queen deep in a dungeon swamp. She was lying in wait for unsuspecting explorers, to kill and feed her lillte swamp subjects.<br><br>" +
"<BASEFONT COLOR=White>I also saw down there, she had Mother nature trapped and turned Evil, and had some evil sailors guarding her ships. Well before I could gather any treasure I took off running, with<br><br>" +
"<BASEFONT COLOR=White>all her nasty subjects chasing me down. I was almost dead, when I managed to find an old escape tunnel, and climbed up it. When I got to the top, I barely got out and it all collapsed!<br><br>" +
"<BASEFONT COLOR=White>Luckily it trapped em' all down there for eternity. But all is not lost, 'cause while I was exploring, I did manage to mark a spot down there, if I ever got crazy enough to return!<br><br>" +
"</BODY>", false, true);
			
			AddImage( 430, 9, 10441);
			AddImageTiled( 40, 38, 17, 391, 9263 );
			AddImage( 6, 25, 10421 );
			AddImage( 34, 12, 10420 );
			AddImageTiled( 94, 25, 342, 15, 10304 );
			AddImageTiled( 40, 427, 415, 16, 10304 );
			AddImage( -10, 314, 10402 );
			AddImage( 56, 150, 10411 );
			AddImage( 155, 120, 2103 );
			AddImage( 136, 84, 96 );
            AddButton(270, 390, 241, 242, 1, GumpButtonType.Reply, 1);
            AddButton(180, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0);

//--------------------------------------------------------------------------------------------------------------

        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    if (!OldSaltyGateExists())
                    {
                        from.SendMessage("So It's up to you, You want to go or not?");
                        from.CloseGump(typeof(OldSaltyGump));

                        OldSaltyGate g = new OldSaltyGate();
                        g.MoveToWorld(new Point3D( 2665, 2233, 2 ), Map.Trammel);
                    }
                    else
                    {
                        from.SendMessage("That is already in use.");
                    }
                    break;
                             
                    
                case 1:
                    {
                        from.SendMessage("Such a pity, but if you change your mind, I'll be here.");
                        from.CloseGump(typeof(OldSaltyGump));
                        break;
                    }
            }
        }

        private bool OldSaltyGateExists()
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is OldSaltyGate)
                    return true;
            }
            return false;
        }

    }
}
    
