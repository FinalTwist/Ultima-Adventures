using System; using Server; using Server.Commands;using Server.Gumps; using Server.Network;using Server.Items;using Server.Mobiles;namespace Server.Gumps
{ public class BillGump : Gump { 
public static void Initialize() { 
CommandSystem.Register( "BillGump", AccessLevel.GameMaster, new CommandEventHandler( BillGump_OnCommand ) ); 
}
private static void BillGump_OnCommand( CommandEventArgs e ) 
{
e.Mobile.SendGump( new BillGump( e.Mobile ) ); } 
public BillGump( Mobile owner ) : base( 50,50 ) 
{
//----------------------------------------------------------------------------------------------------
AddPage( 0 );AddImageTiled(  54, 33, 369, 400, 2624 );AddAlphaRegion( 54, 33, 369, 400 );AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
AddImage( 97, 49, 9005 );AddImageTiled( 58, 39, 29, 390, 10460 );AddImageTiled( 412, 37, 31, 389, 10460 );
AddLabel( 140, 60, 0x34, "Bill & Ted's Bogus Adventure" );
//----------------------/----------------------------------------------/
AddHtml( 107, 140, 300, 230, " < BODY > " + 
"<BASEFONT COLOR=YELLOW>Whats Goin On Dude!? Put it there man!<BR>" +
"<BASEFONT COLOR=YELLOW>Man dude Im glad your here. I'm in a<BR>" +
"<BASEFONT COLOR=YELLOW>most unexpected exbidition. See I died <BR>" +
"<BASEFONT COLOR=YELLOW>and went to Hell. I went through my <BR>" +
"<BASEFONT COLOR=YELLOW>time machine with my best friend Ted.<BR>" +
"<BASEFONT COLOR=YELLOW>Oh and by the way my name's Bill. Sweet<BR>" +
"<BASEFONT COLOR=YELLOW>dude. Now there are 2 Keys that Unlock <BR>" +
"<BASEFONT COLOR=YELLOW>Hell's door. If you can retrieve them, <BR>" +
"<BASEFONT COLOR=YELLOW>bring each of them to me and Ted, that <BR>" +
"<BASEFONT COLOR=YELLOW>would be So Excelent dude! Remeber that<BR>" +
"<BASEFONT COLOR=YELLOW>the keys are located in 1 Arcane Demon<BR>" +
"<BASEFONT COLOR=YELLOW>And 1 in the ReaperGuy.<BR>" +
"<BASEFONT COLOR=YELLOW>Please bring them to me, Good Luck Dude<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"</BODY>", false, true);
//----------------------/----------------------------------------------/
AddImage( 430, 9, 10441);AddImageTiled( 40, 38, 17, 391, 9263 );AddImage( 6, 25, 10421 );AddImage( 34, 12, 10420 );AddImageTiled( 94, 25, 342, 15, 10304 );AddImageTiled( 40, 427, 415, 16, 10304 );AddImage( -10, 314, 10402 );AddImage( 56, 150, 10411 );AddImage( 155, 120, 2103 );AddImage( 136, 84, 96 );AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); }
//----------------------/----------------------------------------------/
public override void OnResponse( NetState state, RelayInfo info ){ Mobile from = state.Mobile; switch ( info.ButtonID ) { case 0:{ break; }}}}}
