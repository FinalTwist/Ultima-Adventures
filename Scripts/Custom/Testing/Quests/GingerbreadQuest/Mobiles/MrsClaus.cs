/* Created by Hammerhand*/

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
    [CorpseName( "Mrs.Claus's Corpse" )]
    public class MrsClaus : BaseCreature
    {
        public virtual bool IsInvulnerable { get { return true; } }
        [Constructable]
        public MrsClaus()
            : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4)
        {


            InitStats(31, 41, 51);


            Name = "Mrs. Claus";
            Body = 0x191;
            Hue = 1016;


            AddItem(new Server.Items.FancyDress(Utility.RandomRedHue()));
            AddItem(new Server.Items.Sandals());
            int hairHue = 1072;

            HairItemID = 0x2046;

            Blessed = true;
          }

            public MrsClaus( Serial serial ) : base( serial ){}
            public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
{            base.GetContextMenuEntries( from, list );
                list.Add( new MrsClausEntry( from, this ) );
            } 
            public override void Serialize( GenericWriter writer )
            {
                base.Serialize( writer );
                writer.Write( (int) 0 );
            }
            public override void Deserialize( GenericReader reader )
            {
                base.Deserialize( reader );
                int version = reader.ReadInt();
            }
            public class MrsClausEntry : ContextMenuEntry
            {
                private Mobile m_Mobile;
                private Mobile m_Giver;
            public MrsClausEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
            {
                m_Mobile = from;
                m_Giver = giver;
            }
            public override void OnClick()
            {
                if( !( m_Mobile is PlayerMobile ) )return;
            PlayerMobile mobile = (PlayerMobile) m_Mobile;
                {

                if ( ! mobile.HasGump( typeof( MrsClausGump ) ) )
            {
                mobile.SendGump(new MrsClausGump(mobile));
                mobile.AddToBackpack(new RecipeBox());
            }
        }
    }
}
            public override bool OnDragDrop( Mobile from, Item dropped )
            { 
                Mobile m = from;
                PlayerMobile mobile = m as PlayerMobile;
                if ( mobile != null){


                    if (dropped is SpecialGingerbreadRecipe)
                    {
                        if (dropped.Amount != 1)
                        {
                            this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "This isnt the recipe!", mobile.NetState);
                            return false;
                        }
                        dropped.Delete();


                        mobile.AddToBackpack(new Gold(20000));
                        switch (Utility.Random(3))
                        {

                            case 0: mobile.AddToBackpack(new SpecialGingerbreadCookie1()); break;
                            case 1: mobile.AddToBackpack(new SpecialGingerbreadCookie2()); break;
                            case 2: mobile.AddToBackpack(new SpecialGingerbreadCookie3()); break;


                                this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Thankyou for getting my recipe back! Merry Christmas!", mobile.NetState);


                                return true;
                        }
                    }
                    else if (dropped is Whip)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, 1054071, mobile.NetState);
                        return false;
                    }
                    else
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need for this...", mobile.NetState);
                    }
                }
                return false;
            }
    }
}
