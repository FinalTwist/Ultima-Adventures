/* Created By Hammerhand */

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
          [CorpseName( "Krelnost's Corpse" )]
          public class Krelnost : Mobile
          {
              public virtual bool IsInvulnerable{ get{ return true; } }
          [Constructable]
              public Krelnost()
          {


          InitStats( 31, 41, 51 );

          Name = "Krelnost";
          Title = "The Aging Warrior";
          Body = 0x190;
          Hue = Utility.RandomSkinHue();
          Utility.AssignRandomHair( this );


         AddItem( new Server.Items.Shirt( Utility.RandomNeutralHue() ) );
         AddItem( new Server.Items.LongPants( Utility.RandomRedHue() ) );
         AddItem( new Server.Items.Sandals( Utility.RandomNeutralHue() ) );

         Blessed = true;
         CantWalk = true;

        }

         public Krelnost( Serial serial ) : base( serial )
         {
         }
         public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 

         { 
             base.GetContextMenuEntries( from, list ); 
             list.Add( new KrelnostEntry( from, this ) );
         } 
         public override void Serialize( GenericWriter writer )
         {
             base.Serialize( writer );
             writer.Write( (int) 0 );}
         public override void Deserialize( GenericReader reader )
         {
             base.Deserialize( reader );int version = reader.ReadInt();
         }
         public class KrelnostEntry : ContextMenuEntry{private Mobile m_Mobile;
             private Mobile m_Giver;
         public KrelnostEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
         {
             m_Mobile = from;
             m_Giver = giver;
         }
         public override void OnClick(){if( !( m_Mobile is PlayerMobile ) )return;
            PlayerMobile mobile = (PlayerMobile) m_Mobile;
             {

 
         if ( ! mobile.HasGump( typeof( AgingWarriorGump ) ) )
         {
         mobile.SendGump( new AgingWarriorGump( mobile ));
         }
        }
       }
      }
         public override bool OnDragDrop( Mobile from, Item dropped )
         {         
             Mobile m = from;PlayerMobile mobile = m as PlayerMobile;
         if ( mobile != null){


         if( dropped is BrigandLeadersHead ){if(dropped.Amount!=1)
         {
             this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "There's not the right amount here!", mobile.NetState );
             return false;}
            dropped.Delete();


         mobile.AddToBackpack( new JewelOfUnPossession( ) );


         this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Good! Now maybe I'll sleep better.", mobile.NetState );


         return true;
         }else if ( dropped is Whip){this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
             return false;
         }
         else{this.PrivateOverheadMessage( MessageType.Regular, 1153, false,"I have no need for this...", mobile.NetState );
         }
         }
             return false;
        }
     }
  }
