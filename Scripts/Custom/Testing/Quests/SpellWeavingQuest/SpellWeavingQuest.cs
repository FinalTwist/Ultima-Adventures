// Created by Malice_Molaka
// For script support contact Malice at Malice_Molaka@hotmail.com
using System;using System.Collections;using System.Collections.Generic;using Server.Items;using Server.Targeting;using Server.ContextMenus;using Server.Gumps;using Server.Misc;using Server.Network;using Server.Spells;namespace Server.Mobiles
{[CorpseName( "GreyHawk's Corpse" )]public class GreyHawk : Mobile{public virtual bool IsInvulnerable{ get{ return true; } }
[Constructable]public GreyHawk(){

// STR/DEX/INT
InitStats( 91, 91, 91 );

// name
Name = "GreyHawk";

Body = 0x190;
Hue = 1111;

// immortal and frozen to-the-spot features below:
Blessed = true;
CantWalk = true;

// Adding a backpack
Container pack = new Backpack();
pack.DropItem( new Gold( 250, 300 ) );
pack.Movable = false;
AddItem( pack );
}

public GreyHawk( Serial serial ) : base( serial ){}
public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
{ base.GetContextMenuEntries( from, list ); list.Add( new GreyHawkEntry( from, this ) ); } 
public override void Serialize( GenericWriter writer ){base.Serialize( writer );writer.Write( (int) 0 );}
public override void Deserialize( GenericReader reader ){base.Deserialize( reader );int version = reader.ReadInt();}
public class GreyHawkEntry : ContextMenuEntry{private Mobile m_Mobile;private Mobile m_Giver;
public GreyHawkEntry( Mobile from, Mobile giver ) : base( 6146, 3 ){m_Mobile = from;m_Giver = giver;}
public override void OnClick(){if( !( m_Mobile is PlayerMobile ) )return;
PlayerMobile mobile = (PlayerMobile) m_Mobile;{

// gump name
if ( ! mobile.HasGump( typeof( SpellWeavingQuestGump ) ) ){
mobile.SendGump( new SpellWeavingQuestGump( mobile ));}}}}
public override bool OnDragDrop( Mobile from, Item dropped ){               Mobile m = from;PlayerMobile mobile = m as PlayerMobile;
if ( mobile != null){

// item to be dropped
if( dropped is AmanitaMuscaria ){if(dropped.Amount!=1)
{this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the right amount!", mobile.NetState );return false;}
dropped.Delete();

// the reward
mobile.AddToBackpack( new Gold( 2000 ) );
mobile.AddToBackpack( new SpellWeavingBandana( ) );
mobile.AddToBackpack( new SpellweavingBook( ) );

// thanks message
this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "My gracious thanks. Now I can combine these to craft more books. Here take one and teach others what you have learned here.", mobile.NetState );


return true;}else if ( dropped is Whip){this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );return false;}else{this.PrivateOverheadMessage( MessageType.Regular, 1153, false,"That is not the type of mushrooms I seek...", mobile.NetState );}}return false;}}}
