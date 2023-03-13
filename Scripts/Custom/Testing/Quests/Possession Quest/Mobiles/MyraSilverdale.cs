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
{
    [CorpseName("Myra Silverdale's Corpse")]
    public class MyraSilverdale : Mobile
    {
        public virtual bool IsInvulnerable { get { return true; } }
        [Constructable]
        public MyraSilverdale()
        {

            InitStats(31, 41, 51);

            Name = "Lady Myra Silverdale";
            Title = "Worried Wife";

            Body = 0x191;
            Hue = Utility.RandomSkinHue();
            Utility.AssignRandomHair(this);


            AddItem(new Server.Items.Shirt(Utility.RandomBlueHue()));
            AddItem(new Server.Items.Skirt(Utility.RandomBlueHue()));
            AddItem(new Server.Items.Sandals(Utility.RandomBlueHue()));


            Blessed = true;
            CantWalk = true;
        }

        public MyraSilverdale(Serial serial) : base(serial) { }
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        { base.GetContextMenuEntries(from, list); list.Add(new MyraSilverdaleEntry(from, this)); }
        public override void Serialize(GenericWriter writer)
        { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
        public class MyraSilverdaleEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;
            public MyraSilverdaleEntry(Mobile from, Mobile giver) : base(6146, 3) { m_Mobile = from; m_Giver = giver; }
            public override void OnClick(){if( !( m_Mobile is PlayerMobile ) )return;
        PlayerMobile mobile = (PlayerMobile) m_Mobile;{


       if ( ! mobile.HasGump( typeof(MyraSilverdaleGump  ) ) ){
        mobile.SendGump( new MyraSilverdaleGump( mobile ));}
        }
      }
   }
 }
}