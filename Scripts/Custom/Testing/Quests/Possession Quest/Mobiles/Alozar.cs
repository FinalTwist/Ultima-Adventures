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
      {[CorpseName( "Alozar's Corpse" )]public class Alozar : Mobile
      {
       public virtual bool IsInvulnerable{ get{ return true; } }
       [Constructable]public Alozar(){


       InitStats( 31, 41, 51 );


       Name = "Alozar";
       Title = "The Ancient Mage";
       Body = 0x190;
       Hue = Utility.RandomSkinHue();

       Utility.AssignRandomHair( this );
       int hairHue = 1001;

       AddItem( new Server.Items.Robe( Utility.RandomNeutralHue() ) );
       AddItem( new Server.Items.Sandals( Utility.RandomNeutralHue() ) );
       AddItem( new Server.Items.GnarledStaff() ) ;


       Blessed = true;
       CantWalk = true;
      }

       public Alozar( Serial serial ) : base( serial ){}
       public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
     { 
           base.GetContextMenuEntries( from, list );
           list.Add( new AlozarEntry( from, this ) );
       } 
       public override void Serialize( GenericWriter writer ){base.Serialize( writer );
           writer.Write( (int) 0 );}
       public override void Deserialize( GenericReader reader ){base.Deserialize( reader );
           int version = reader.ReadInt();}
       public class AlozarEntry : ContextMenuEntry{private Mobile m_Mobile;
           private Mobile m_Giver;
       public AlozarEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
       {
           m_Mobile = from;
           m_Giver = giver;
       }
       public override void OnClick(){if( !( m_Mobile is PlayerMobile ) 
           )
           return;
       PlayerMobile mobile = (PlayerMobile) m_Mobile;
           {

 
       if ( ! mobile.HasGump( typeof( AlozarGump  ) ) )
       {
       mobile.SendGump( new AlozarGump( mobile ));
       }
      }
    }
  }

      }
   }

