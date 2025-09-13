using System; using Server; using Server.Commands;using Server.Gumps; using Server.Network;using Server.Items;using Server.Mobiles;namespace Server.Gumps
{ public class SpellWeavingQuestGump : Gump { 
public static void Initialize() { 
CommandSystem.Register( "SpellWeavingQuestGump", AccessLevel.GameMaster, new CommandEventHandler( SpellWeavingQuestGump_OnCommand ) ); 
}
private static void SpellWeavingQuestGump_OnCommand( CommandEventArgs e ) 
{
e.Mobile.SendGump( new SpellWeavingQuestGump( e.Mobile ) ); } 
public SpellWeavingQuestGump( Mobile owner ) : base( 50,50 ) 
{
//----------------------------------------------------------------------------------------------------
AddPage( 0 );AddImageTiled(  54, 33, 369, 400, 2624 );AddAlphaRegion( 54, 33, 369, 400 );AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
AddImage( 97, 49, 9005 );AddImageTiled( 58, 39, 29, 390, 10460 );AddImageTiled( 412, 37, 31, 389, 10460 );
AddLabel( 140, 60, 0x34, "Arcane Knowledge" );
//----------------------/----------------------------------------------/
AddHtml( 107, 140, 300, 230, " < BODY > " + 
"<BASEFONT COLOR=YELLOW>Hail and well met stranger.<BR>" +
"<BASEFONT COLOR=YELLOW>I see you are of a interest in the Arcane Arts?<BR>" +
"<BASEFONT COLOR=YELLOW>Wanting to know a bit more of the forbidden magics of nature itself?<BR>" +
"<BASEFONT COLOR=YELLOW>Don't care the cost or the dangers in your quest for greater knowledge?<BR>" +
"<BASEFONT COLOR=YELLOW>Then we have each met the right person indeed. I have what you seek.<BR>" +
"<BASEFONT COLOR=YELLOW>I am crafting some books in secret. Spell Weaving Books. Arcane knowledge.<BR>" +
"<BASEFONT COLOR=YELLOW>Shhhh! Don't look around. Pretend we are talking about something else.<BR>" +
"<BASEFONT COLOR=YELLOW>Well anyways there was this evil Centaur. Rude bugger truly. And he guards the last places I could harvest Amanita Muscaria.<BR>" +
"<BASEFONT COLOR=YELLOW>It is a very rare mushroom left over from the first cataclysm, but that is another matter.<BR>" +
"<BASEFONT COLOR=YELLOW>Go North of the Shrine of Sacrifice and look under the trees there. If you find no Amanita Muscaria growing then evil Equestrius has already picked it all.<BR>" +
"<BASEFONT COLOR=YELLOW>Slay him and surely he will have some of this rare fungi on him. Return some to me and I will reward you in kind.<BR>" +
"</BODY>", false, true);
//----------------------/----------------------------------------------/
AddImage( 430, 9, 10441);AddImageTiled( 40, 38, 17, 391, 9263 );AddImage( 6, 25, 10421 );AddImage( 34, 12, 10420 );AddImageTiled( 94, 25, 342, 15, 10304 );AddImageTiled( 40, 427, 415, 16, 10304 );AddImage( -10, 314, 10402 );AddImage( 56, 150, 10411 );AddImage( 155, 120, 2103 );AddImage( 136, 84, 96 );AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); }
//----------------------/----------------------------------------------/
public override void OnResponse( NetState state, RelayInfo info ){ Mobile from = state.Mobile; switch ( info.ButtonID ) { case 0:{ break; }}}}}
