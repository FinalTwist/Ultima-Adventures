/* This file was created with
Ilutzio's Questmaker. Enjoy! */
/* Created by Hammerhand */
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
namespace Server.Mobiles
{[CorpseName( "AGhostlyBlacksmith's Corpse" )]public class AGhostlyBlacksmith : Mobile{public virtual bool IsInvulnerable{ get{ return true; } }
[Constructable]public AGhostlyBlacksmith(){

InitStats( 31, 41, 51 );

Name = "A Ghostly Blacksmith";

Title = "";

Body = 0x190;
Hue = 1072;


AddItem( new Server.Items.DeathShroud() );
AddItem(new SmithHammer());



Blessed = true;
CantWalk = true;


Container pack = new Backpack();
pack.DropItem( new Gold( 250, 300 ) );
pack.Movable = false;
AddItem( pack );
}

public AGhostlyBlacksmith(Serial serial)
    : base(serial)
{
}


public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
{
    base.GetContextMenuEntries(from, list);
    list.Add(new AGhostlyBlacksmithEntry(from, this));
}

public override void Serialize(GenericWriter writer)
{
    base.Serialize(writer); writer.Write((int)0);
}


public override void Deserialize(GenericReader reader)
{
    base.Deserialize(reader); int version = reader.ReadInt();
}

public class AGhostlyBlacksmithEntry : ContextMenuEntry
{
    private Mobile m_Mobile; private Mobile m_Giver;

    public AGhostlyBlacksmithEntry(Mobile from, Mobile giver)
        : base(6146, 3)
    {
        m_Mobile = from; m_Giver = giver;
    }


    public override void OnClick()
    {

        if (!(m_Mobile is PlayerMobile)) return;
        PlayerMobile mobile = (PlayerMobile)m_Mobile;
        {

            if (!mobile.HasGump(typeof(GhostlyBlacksmithGump)))
            {
                mobile.SendGump(new GhostlyBlacksmithGump(mobile));
                mobile.AddToBackpack(new AncientSmeltingBox());
            }
        }
    }


}

public override bool OnDragDrop(Mobile from, Item dropped)
{

    Mobile m = from;
    PlayerMobile mobile = m as PlayerMobile;

    if (mobile != null)
    {
        if (dropped is EnergizedSosarianIngots)
        {
            dropped.Delete();
            mobile.AddToBackpack(new Gold(2000));
            mobile.AddToBackpack(new LegendarySwordOfAmbrose());
            this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Thank you. May this sword server you well against Evil.", mobile.NetState);
            return true;
        }


        else
        {
            this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need for this...", mobile.NetState); return true;
        }


    }

    return false;
}
}
}