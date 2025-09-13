using System; using Server; using Server.Commands;using Server.Gumps; using Server.Network;using Server.Items;using Server.Mobiles;namespace Server.Gumps
{ public class GoldenThreadQuestGump : Gump { 
public static void Initialize() { 
CommandSystem.Register( "GoldenThreadQuestGump", AccessLevel.GameMaster, new CommandEventHandler( GoldenThreadQuestGump_OnCommand ) ); 
}
private static void GoldenThreadQuestGump_OnCommand( CommandEventArgs e ) 
{
e.Mobile.SendGump( new GoldenThreadQuestGump( e.Mobile ) ); } 
public GoldenThreadQuestGump( Mobile owner ) : base( 50,50 ) 
{
//----------------------------------------------------------------------------------------------------
AddPage( 0 );AddImageTiled(  54, 33, 369, 400, 2624 );AddAlphaRegion( 54, 33, 369, 400 );AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
AddImage( 97, 49, 9005 );AddImageTiled( 58, 39, 29, 390, 10460 );AddImageTiled( 412, 37, 31, 389, 10460 );
AddLabel( 140, 60, 0x34, "Golden Thread Quest" );
//----------------------/----------------------------------------------/
AddHtml( 107, 140, 300, 230, " < BODY > " + 
"<BASEFONT COLOR=YELLOW>Search for Scottie's attacker, a wolf<BR>" +
"<BASEFONT COLOR=YELLOW>in the clearing SouthWest of Brittain.<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW>Kill the wolf and retrieve the 10 <BR>" +
"<BASEFONT COLOR=YELLOW>special threads Scottie needs to use <BR>" +
"<BASEFONT COLOR=YELLOW>in order to make his special items.<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW>Once you have killed the wolf and <BR>" +
"<BASEFONT COLOR=YELLOW>retrieved the threads, bring them back <BR>" +
"<BASEFONT COLOR=YELLOW> to Scottie for the reward!<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"</BODY>", false, true);
//----------------------/----------------------------------------------/
AddImage( 430, 9, 10441);AddImageTiled( 40, 38, 17, 391, 9263 );AddImage( 6, 25, 10421 );AddImage( 34, 12, 10420 );AddImageTiled( 94, 25, 342, 15, 10304 );AddImageTiled( 40, 427, 415, 16, 10304 );AddImage( -10, 314, 10402 );AddImage( 56, 150, 10411 );AddImage( 155, 120, 2103 );AddImage( 136, 84, 96 );AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); }
//----------------------/----------------------------------------------/
public override void OnResponse( NetState state, RelayInfo info ){ Mobile from = state.Mobile; switch ( info.ButtonID ) { case 0:{ break; }}}}}
