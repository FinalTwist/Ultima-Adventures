using System; 
using Server.Network; 
using Server.Prompts; 
using Server.Items; 
using Server.Mobiles;
using Server.Targeting;
using Server.Gumps; 
using Server.Misc;

namespace Server.Items 
{ 
   public class SlaveTarget : Target 
   { 
      private SlaveWhip m_Whip; 

      public SlaveTarget( SlaveWhip whip ) : base( 1, false, TargetFlags.None ) 
      { 
         m_Whip = whip; 
      } 

      protected override void OnTarget( Mobile from, object target ) 
      { 
         if ( target is Mobile ) 
         { 
            Mobile t = ( Mobile ) target; 

            if ( !(t is BaseChild) )
               from.SendMessage( "Yes... you'd love to whip that wouldn't you?" );
            else if (from.Blessed)
               from.SendMessage( "You can't interact with the whip in your state." );
            else if ( ((BaseChild)t).type != 5 || t.Title != "the Orphan") 
               from.SendMessage( "This child has parents.. Orphans make the best slaves, look in bigger cities to find one." ); 
			   else if ( ((BaseCreature)t).Controlled && ((BaseCreature)t).ControlMaster != null && ((BaseCreature)t).ControlMaster != from ) 
               from.SendMessage( "This slave is someone else's property" );  
			   else 
			   {
               if (Utility.RandomDouble() < 0.80 )
               {
                  Region reg = Region.Find( from.Location, from.Map );

                  from.SendMessage( "You whip the child." ); 
                  t.Hits -= Utility.RandomMinMax(5, 20);
                  switch (Utility.Random(4))
                  {
                           case 0: t.Say( "NO!!  THAT HURTS!!" ); break;
                           case 1: t.Say( "*Cries in pain*" ); break;
                           case 2: t.Yell("Stop, please mister! "); break;
                           case 3: t.Yell("*cowers*"); break;
                           case 4: t.Yell("Why are you doing this???"); break;
                  }
                  if (from.Hidden)
                     from.RevealingAction();
                  if (!from.Criminal && !reg.IsPartOf( "the Undercity of Umbra" ))
                     from.CriminalAction( true );
                  
                  Titles.AwardKarma( from, -100, true);
               }
               else 
               {
                  BaseCreature enslaved = t as BaseCreature;
                  from.SendMessage( "The child submits to your will." ); 
                  ((Orphan)t).captured = true;

                  enslaved.Controlled = true;
                  enslaved.SetControlMaster( from );
                  enslaved.ControlTarget = from;
                  enslaved.ControlOrder = OrderType.Follow; 
                  ((BaseChild)enslaved).freedom = false;
                  enslaved.AIFullSpeedActive = false;
                  enslaved.AIFullSpeedPassive = false;

               }
			   }
         } 
         else 
         { 
            from.SendMessage( "You whip the damn thing and feel much better." );  
         } 
      } 
   } 


   public class SlaveWhip : Item 
   { 
      [Constructable] 
      public SlaveWhip() : base( 0x166E ) 
      { 
         Name = "a bloody whip"; 

      } 

      public SlaveWhip( Serial serial ) : base( serial ) 
      { 
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 0 ); // version 
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 
         int version = reader.ReadInt(); 
      } 

      public override bool DisplayLootType{ get{ return false; } } 

      public override void OnDoubleClick( Mobile from ) 
      { 
         if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack 
         { 
             from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it. 
         } 
        from.SendMessage( "You uncurl the whip and prepare to strike." );  
        from.Target = new SlaveTarget( this ); // Call our target 
      }    
   } 
}