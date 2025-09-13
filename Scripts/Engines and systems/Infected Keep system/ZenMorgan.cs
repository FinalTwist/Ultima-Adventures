using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Mobiles;
namespace Server.Mobiles

{[CorpseName( "Odd Corpse" )]public class ZenMorgan : Mobile{public virtual bool IsInvulnerable{ get{ return true; } }
[Constructable]public ZenMorgan(){
//////////////////////////////name
Name = "Morgan the Zen";
/////////////////////////////////title
Title = "*Infected?*";
////////sex
Body = 994;
//////skincolor
Hue = 768;
}
////////haircolor
//int hairHue = 0x47E;
////////clothing and other apperance
//AddItem( new ShortHair( hairHue ) );
//AddItem( new Server.Items.HoodedShroudOfShadows(  ) );AddItem( new Server.Items.ShortPants( 5 ) );AddItem( new Server.Items.Boots() );Blessed = true;CantWalk = true;}
public ZenMorgan( Serial serial ) : base( serial ){}
public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
{ base.GetContextMenuEntries( from, list ); list.Add( new ZenMorganEntry( from, this ) ); } 
public override void Serialize( GenericWriter writer ){base.Serialize( writer );writer.Write( (int) 0 );}
public override void Deserialize( GenericReader reader ){base.Deserialize( reader );int version = reader.ReadInt();}
public class ZenMorganEntry : ContextMenuEntry{private Mobile m_Mobile;private Mobile m_Giver;
public ZenMorganEntry( Mobile from, Mobile giver ) : base( 6146, 3 ){m_Mobile = from;m_Giver = giver;}
public override void OnClick(){if( !( m_Mobile is PlayerMobile ) )return;
PlayerMobile mobile = (PlayerMobile) m_Mobile;{
///////////////////////////////////////////////// gumpname
if ( ! mobile.HasGump( typeof( ZenMorganGump ) ) ){
mobile.SendGump( new ZenMorganGump( mobile ));}}}}
public override bool OnDragDrop( Mobile from, Item dropped ){               Mobile m = from;PlayerMobile mobile = m as PlayerMobile;
if ( mobile != null){
/////////////////////////////////////////////////////////// item to be dropped
if( dropped is PlantHerbalism_Flower ){if(dropped.Amount!=10)
{this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "There's not the right amount here, I need 10", mobile.NetState );return false;}
dropped.Delete(); 
///////////////the reward
(((PlayerMobile)from).IsZen) = true;
////////////////////////////////////////////////////////// thanksmessage
this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I've taught you all I know... Remember:  WALK, do NOT run.  They are attracted to your fear.", mobile.NetState );
return true;}else if ( dropped is Whip){this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );return false;}else{this.PrivateOverheadMessage( MessageType.Regular, 1153, false,"I have no need for this...", mobile.NetState );}}return false;}}}
