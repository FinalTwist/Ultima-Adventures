using System;using System.Collections;using System.Collections.Generic;using Server.Items;using Server.Targeting;using Server.ContextMenus;using Server.Gumps;using Server.Misc;using Server.Network;using Server.Spells;namespace Server.Mobiles
{[CorpseName( "Derteran's Corpse" )]public class Derteran : Mobile{public virtual bool IsInvulnerable{ get{ return true; } }
[Constructable]public Derteran(){

///////////STR/DEX/INT
InitStats( 31, 41, 51 );

///////////name
Name = "Derteran";

///////////title
Title = "King Of Sosaria";

///////////sex. 0x191 is female, 0x190 is male.
Body = 0x190;

///////////skincolor
Hue = Utility.RandomSkinHue();

///////////Random hair and haircolor
Utility.AssignRandomHair( this );

///////////clothing and hues
AddItem( new Server.Items.Robe( Utility.RandomBlueHue() ) );
AddItem( new Server.Items.ThighBoots( Utility.RandomBlueHue() ) );
AddItem( new Server.Items.Cloak( Utility.RandomBlueHue() ) );
AddItem( new MondainsBlade() );

///////////immortal and frozen to-the-spot features below:
Blessed = true;
CantWalk = true;

///////////Adding a backpack
Container pack = new Backpack();
pack.DropItem( new Gold( 250, 300 ) );
pack.Movable = false;
AddItem( pack );
}

public Derteran( Serial serial ) : base( serial ){}
public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
{ base.GetContextMenuEntries( from, list ); list.Add( new DerteranEntry( from, this ) ); } 
public override void Serialize( GenericWriter writer ){base.Serialize( writer );writer.Write( (int) 0 );}
public override void Deserialize( GenericReader reader ){base.Deserialize( reader );int version = reader.ReadInt();}
public class DerteranEntry : ContextMenuEntry{private Mobile m_Mobile;private Mobile m_Giver;
public DerteranEntry( Mobile from, Mobile giver ) : base( 6146, 3 ){m_Mobile = from;m_Giver = giver;}
public override void OnClick(){if( !( m_Mobile is PlayerMobile ) )return;
PlayerMobile mobile = (PlayerMobile) m_Mobile;{

///////////gump name
if ( ! mobile.HasGump( typeof( MondaneQuestGump ) ) ){
mobile.SendGump( new MondaneQuestGump( mobile ));}}}}
public override bool OnDragDrop( Mobile from, Item dropped ){               Mobile m = from;PlayerMobile mobile = m as PlayerMobile;
if ( mobile != null){

///////////item to be dropped
if( dropped is HeadOfMondain ){if(dropped.Amount!=1)
{this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "There's not the right amount here!", mobile.NetState );return false;}
dropped.Delete();

///////////the reward
mobile.AddToBackpack( new Gold( 2000 ) );
mobile.AddToBackpack( new MondainsBlade( ) );

///////////thanks message
this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I Thank You And As Promised....", mobile.NetState );


return true;}else if ( dropped is Whip){this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );return false;}else{this.PrivateOverheadMessage( MessageType.Regular, 1153, false,"I have no need for this...", mobile.NetState );}}return false;}}}
