using System; using Server; using Server.Commands;using Server.Gumps; using Server.Network;using Server.Items;using Server.Mobiles;namespace Server.Gumps
{ public class MondaneQuestGump : Gump { 
public static void Initialize() { 
CommandSystem.Register( "MondaneQuestGump", AccessLevel.GameMaster, new CommandEventHandler( MondaneQuestGump_OnCommand ) ); 
}
private static void MondaneQuestGump_OnCommand( CommandEventArgs e ) 
{
e.Mobile.SendGump( new MondaneQuestGump( e.Mobile ) ); } 
public MondaneQuestGump( Mobile owner ) : base( 50,50 ) 
{
//----------------------------------------------------------------------------------------------------
AddPage( 0 );AddImageTiled(  54, 33, 369, 400, 2624 );AddAlphaRegion( 54, 33, 369, 400 );AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
AddImage( 97, 49, 9005 );AddImageTiled( 58, 39, 29, 390, 10460 );AddImageTiled( 412, 37, 31, 389, 10460 );
AddLabel( 140, 60, 0x34, "Mondain Must Die!!!" );
//----------------------/----------------------------------------------/
AddHtml( 107, 140, 300, 230, " < BODY > " + 
"<BASEFONT COLOR=YELLOW>I Am The King Of These Lands And I Wish<BR>" +
"<BASEFONT COLOR=YELLOW>To Ask You To Retrive Mondains Head. <BR>" +
"<BASEFONT COLOR=YELLOW>What Will You Get In Return U Ask Hmm..<BR>" +
"<BASEFONT COLOR=YELLOW>What About My Sword? U Could<BR>" +
"<BASEFONT COLOR=YELLOW>Find Him Deep Within Deceit In Trammel<BR>" +
"<BASEFONT COLOR=YELLOW>I Thank Thee Ahead Of Time.<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW>Now Go And Bring Me His Head<BR>" +
"</BODY>", false, true);
//----------------------/----------------------------------------------/
AddImage( 430, 9, 10441);AddImageTiled( 40, 38, 17, 391, 9263 );AddImage( 6, 25, 10421 );AddImage( 34, 12, 10420 );AddImageTiled( 94, 25, 342, 15, 10304 );AddImageTiled( 40, 427, 415, 16, 10304 );AddImage( -10, 314, 10402 );AddImage( 56, 150, 10411 );AddImage( 155, 120, 2103 );AddImage( 136, 84, 96 );AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); }
//----------------------/----------------------------------------------/
public override void OnResponse( NetState state, RelayInfo info ){ Mobile from = state.Mobile; switch ( info.ButtonID ) { case 0:{ break; }}}}}
