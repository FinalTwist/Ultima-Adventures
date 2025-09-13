using System; using Server; using Server.Commands;using Server.Gumps; using Server.Network;using Server.Items;using Server.Mobiles;namespace Server.Gumps
{ public class ZenMorganGump : Gump { 
public static void Initialize() { 
CommandSystem.Register( "ZenMorganGump", AccessLevel.GameMaster, new CommandEventHandler( ZenMorganGump_OnCommand ) ); 
}
private static void ZenMorganGump_OnCommand( CommandEventArgs e ) 
{
e.Mobile.SendGump( new ZenMorganGump( e.Mobile ) ); } 
public ZenMorganGump( Mobile owner ) : base( 50,50 ) 
{
//----------------------------------------------------------------------------------------------------
AddPage( 0 );AddImageTiled(  54, 33, 369, 400, 2624 );AddAlphaRegion( 54, 33, 369, 400 );AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
AddImage( 97, 49, 9005 );AddImageTiled( 58, 39, 29, 390, 10460 );AddImageTiled( 412, 37, 31, 389, 10460 );
AddLabel( 140, 60, 0x34, "Infiltrating the dead" );
//----------------------/----------------------------------------------/
AddHtml( 107, 140, 300, 230, " < BODY > " + 
"<BASEFONT COLOR=YELLOW>I knew you would come, I felt it,<BR>" +
"<BASEFONT COLOR=YELLOW>So you want to meet the Widow... <BR>" +
"<BASEFONT COLOR=YELLOW>I used to live in the Keep, and now <BR>" +
"<BASEFONT COLOR=YELLOW>I spend my days trying to cure this  <BR>" +
"<BASEFONT COLOR=YELLOW>Virus before it takes over the lands.<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW>Not too long ago, the Widow came from <BR>" +
"<BASEFONT COLOR=YELLOW>the well we were using at the Keep.<BR>" +
"<BASEFONT COLOR=YELLOW>Her and her concubines took human form <BR>" +
"<BASEFONT COLOR=YELLOW>and walked naked in the streets at night  <BR>" +
"<BASEFONT COLOR=YELLOW>suffice it to say she lured many of us to <BR>" +
"<BASEFONT COLOR=YELLOW>the well where she conducted all manner<BR>" +
"<BASEFONT COLOR=YELLOW>of experiments.<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW>Now, the Widow has a veritable army guarding<BR>" +
"<BASEFONT COLOR=YELLOW>her... but these are my kinsmen, I know them<BR>" +
"<BASEFONT COLOR=YELLOW>I was a Monk who happened to be staying at the <BR>" +
"<BASEFONT COLOR=YELLOW>Keep.  I wasn't swayed by the Widow but entered<BR>" +
"<BASEFONT COLOR=YELLOW>the well to see what was happening here.<BR>" +
"<BASEFONT COLOR=YELLOW>I did not know it then, but there is something<BR>" +
"<BASEFONT COLOR=YELLOW>in the air that fouls the soul of persons.<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW>I turned, but did not turn full.  Now,I seek<BR>" +
"<BASEFONT COLOR=YELLOW>a cure for this evil affliction.  I know <BR>" +
"<BASEFONT COLOR=YELLOW>a technique that calms the mind, and allows<BR>" +
"<BASEFONT COLOR=YELLOW>you to walk among them unnoticed.<BR>" +
"<BASEFONT COLOR=YELLOW>But I also cannot leave to gather ingredients<BR>" +
"<BASEFONT COLOR=YELLOW>for fear of passing this curse to those above<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW>Please sir... Bring me 10 Life Root <BR>" +
"<BASEFONT COLOR=YELLOW>and I'll teach you what I know. <BR>" +
"<BASEFONT COLOR=YELLOW>It's the next ingredient on my list<BR>" +
"<BASEFONT COLOR=YELLOW>with some potential for the cure.<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"</BODY>", false, true);
//----------------------/----------------------------------------------/
AddImage( 430, 9, 10441);AddImageTiled( 40, 38, 17, 391, 9263 );AddImage( 6, 25, 10421 );AddImage( 34, 12, 10420 );AddImageTiled( 94, 25, 342, 15, 10304 );AddImageTiled( 40, 427, 415, 16, 10304 );AddImage( -10, 314, 10402 );AddImage( 56, 150, 10411 );AddImage( 155, 120, 2103 );AddImage( 136, 84, 96 );AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); }
//----------------------/----------------------------------------------/
public override void OnResponse( NetState state, RelayInfo info ){ Mobile from = state.Mobile; switch ( info.ButtonID ) { case 0:{ break; }}}}}
