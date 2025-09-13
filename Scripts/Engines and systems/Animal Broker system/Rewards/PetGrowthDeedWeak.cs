using System; 
using Server.Network; 
using Server.Prompts; 
using Server.Items; 
using Server.Mobiles;
using Server.Targeting; 

namespace Server.Items 
{ 
   public class GrowthTargetWeak : Target 
   { 
      private PetGrowthDeedWeak m_Deed; 

      public GrowthTargetWeak( PetGrowthDeedWeak deed ) : base( 1, false, TargetFlags.None ) 
      { 
         m_Deed = deed; 
      } 

      protected override void OnTarget( Mobile from, object target ) 
      { 
         if ( target != null && target is BaseCreature ) 
         { 
            BaseCreature t = ( BaseCreature ) target; 

            if ( t.ControlMaster != from ) 
            { 
               from.SendMessage( "That is not your pet!" ); 
            } 
            else  
             
               { 

                  from.SendMessage( "Your pet grows stronger!" );

                  int growth = Utility.RandomMinMax(1,10);
                  int amnt = Utility.RandomMinMax(1,2);

                  if (growth <= 5)
                  {
                     int count = 0;
                     bool didit = false;
                     while (!didit && count <= 5)
                     {


										switch ( Utility.Random( 5 ) ) 
										{
												case 0: 
                                    {
                                       int blah = t.PoisonResistance + amnt;
                                       if (blah < 80)
                                       {
                                          t.SetResistance( ResistanceType.Poison, blah );
                                          didit = true;
                                       } 
                                       count ++;
                                    } break;
                                    case 1: 
                                    {
                                       int blah = t.EnergyResistance+ amnt;
                                       if (blah < 80)
                                       {
                                          t.SetResistance( ResistanceType.Energy, blah );
                                          didit = true;
                                       } 
                                       count ++;
                                    } break;
                                    case 2: 
                                    {
                                       int blah = t.ColdResistance+ amnt;
                                       if (blah < 80)
                                       {
                                          t.SetResistance( ResistanceType.Cold, blah );
                                          didit = true;
                                       } 
                                       count ++;
                                    } break;
                                    case 3: 
                                    {
                                       int blah = t.FireResistance+ amnt;
                                       if (blah < 80)
                                       {
                                          t.SetResistance( ResistanceType.Fire, blah );
                                          didit = true;
                                       } 
                                       count ++;
                                    } break;
                                    case 4: 
                                    {
                                       int blah = t.PhysicalResistance+ amnt;
                                       if (blah < 80)
                                       {
                                          t.SetResistance( ResistanceType.Physical, blah );
                                          didit = true;
                                       } 
                                       count ++;
                                    } break;
										} 
                     }
                     if (count > 5)
                        growth = Utility.RandomMinMax(6, 10);  
                  }     
                  else if (growth <= 8)
                  {
 										switch ( Utility.Random( 3 ) ) 
										{
												case 0: t.RawStr += amnt; break;
												case 1: t.RawDex += amnt; break;
												case 2: t.RawInt += amnt; break;
										}                       
                  }    
                  else if (growth <= 9)
                  {
                     int blah = t.HitsMax + amnt;
                     t.SetHits( blah );
                  }  
                  else if (growth <= 10)
                  {
 							t.DamageMax += amnt;  
                  }      
                  m_Deed.Delete(); // Delete the deed 

               } 
            
         } 
         else 
         { 
            from.SendMessage( "That is not a valid traget." );  
         } 
      } 
   } 

   public class PetGrowthDeedWeak : Item // Create the item class which is derived from the base item class 
   { 
      [Constructable] 
      public PetGrowthDeedWeak() : base( 0x2827 ) 
      { 
         Weight = 1.0; 
         Name = "a weak experimental growth potion"; 
         Stackable = true;
         //LootType = LootType.Blessed; 
	   Hue = 1175;
      } 

      public PetGrowthDeedWeak( Serial serial ) : base( serial ) 
      { 
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 0 ); // version 
      } 

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			
				list.Add("Adds 1 to 2 points to one of your pet's attributes." ); 
		}

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 
         //LootType = LootType.Blessed; 

         int version = reader.ReadInt(); 
      } 

      public override bool DisplayLootType{ get{ return false; } } 

      public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target 
      { 
         if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack 
         { 
             from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it. 
         } 
         else 
         { 
            from.SendMessage( "Choose the pet you wish to feed this to." );  
            from.Target = new GrowthTargetWeak( this ); // Call our target 
          } 
      }    
   } 
}