//slotmachine by zulu, updated Feb 2004
using System;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
   public class SlotMachine : Item 
   {
         private int Slot1Graphic = 3823;
         private int Slot2Graphic = 3823;
         private int Slot3Graphic = 3823;

         private int Slot1_x = 108;
         private int Slot1_y = 119;
         private int Slot2_x = 108;
         private int Slot2_y = 119;
         private int Slot3_x = 108;
         private int Slot3_y = 119;

         private int Prize1WinTotal = 0;
         private int Prize2WinTotal = 0;
         private int Prize3WinTotal = 0;
         private int Prize4WinTotal = 0;
         private int Prize5WinTotal = 0;
         private int Prize6WinTotal = 0;
         private int Prize7WinTotal = 0;
         private int Prize8WinTotal = 0;

      [Constructable]
      public SlotMachine() : base( 0xED4 )
      {
         Movable = false;
         Hue = 0x56;
         Name = "a slot machine";
      }

      public override void OnDoubleClick( Mobile from )
      {
	 from.SendGump( new SlotMachineGump( (Mobile)from,this,0) );
      }

      public SlotMachine( Serial serial ) : base( serial )
      {
      }

      public override void Serialize( GenericWriter writer )
      {
         base.Serialize( writer );

         writer.Write( (int) 0 ); // version

         writer.Write( (int) Slot1Graphic );
         writer.Write( (int) Slot2Graphic );
         writer.Write( (int) Slot3Graphic );

         writer.Write( (int) Slot1_x );
         writer.Write( (int) Slot1_y );
         writer.Write( (int) Slot2_x );
         writer.Write( (int) Slot2_y );
         writer.Write( (int) Slot3_x );
         writer.Write( (int) Slot3_y );
         writer.Write( (int) Prize1WinTotal );
         writer.Write( (int) Prize2WinTotal );
         writer.Write( (int) Prize3WinTotal );
         writer.Write( (int) Prize4WinTotal );
         writer.Write( (int) Prize5WinTotal );
         writer.Write( (int) Prize6WinTotal );
         writer.Write( (int) Prize7WinTotal );
         writer.Write( (int) Prize8WinTotal );
      }

      public override void Deserialize( GenericReader reader )
      {
         base.Deserialize( reader );

         int version = reader.ReadInt();

         switch ( version )
         {
            case 0:
            {
               Slot1Graphic = reader.ReadInt();
               Slot2Graphic = reader.ReadInt();
               Slot3Graphic = reader.ReadInt();
               Slot1_x = reader.ReadInt();
               Slot1_y = reader.ReadInt();
               Slot2_x = reader.ReadInt();
               Slot2_y = reader.ReadInt();
               Slot3_x = reader.ReadInt();
               Slot3_y = reader.ReadInt();
               Prize1WinTotal = reader.ReadInt();
               Prize2WinTotal = reader.ReadInt();
               Prize3WinTotal = reader.ReadInt();
               Prize4WinTotal = reader.ReadInt();
               Prize5WinTotal = reader.ReadInt();
               Prize6WinTotal = reader.ReadInt();
               Prize7WinTotal = reader.ReadInt();
	       Prize8WinTotal = reader.ReadInt();
               break;
            }
         }
      }


   public class SlotMachineGump : Gump
   {
      private SlotMachine m_From;

      public SlotMachineGump( Mobile from, SlotMachine machine, int spin) : base(0,0) 
      {
	m_From = machine;

	Closable = false;

	from.CloseGump( typeof( SlotMachineGump ) );

	if (!from.Alive)
		return;

        if ( !from.InRange( machine.Location, 4 ) )
   		return;

	string message = "500 gold per spin.";

	int Image1 = 9300;
	int Image2 = 9300;
	int Image3 = 9300;

	if (spin==1)
	{
	Effects.PlaySound( from.Location, from.Map, 0x2e1 );
	message = "You did not win.";

       	// 3823 gold		3826 silver	6225 scales
       	// 3629 crystal ball	6218 hstand	5912 wizard
       	// 6186 P Flask		6188 R Flask

	int [] prize = {3823,3826,6225,3629,6218,5912,6186,6188};
	int [] slotx = {108,108,108,109,108,108,108,108};
	int [] sloty = {119,119,118,124,120,124,120,120};

	int modifier = 64;  // number of virtual wheel locations - 32,64,128,256,512

	int temp = Utility.Random( 100000,100000000 );
	int hold1, hold2 = (temp / modifier);
	hold1 = modifier*hold2;
	hold2 = (temp-hold1);
	int Slot1 = findreelspot(hold2);

	temp = Utility.Random( 100000,100000000 );
	hold2 = (temp / modifier);
	hold1 = modifier*hold2;
	hold2 = (temp-hold1);
	int Slot2 = findreelspot(hold2);

	temp = Utility.Random( 100000,100000000 );
	hold2 = (temp / modifier);
	hold1 = modifier*hold2;
	hold2 = (temp-hold1);
	int Slot3 = findreelspot(hold2);
	bool bc = false;

	 	m_From.Slot1Graphic = prize[Slot1-1];
 		m_From.Slot1_x      = slotx[Slot1-1];
	 	m_From.Slot1_y      = sloty[Slot1-1];

		m_From.Slot2Graphic = prize[Slot2-1];
 		m_From.Slot2_x      = slotx[Slot2-1];
 		m_From.Slot2_y      = sloty[Slot2-1];

 		m_From.Slot3Graphic = prize[Slot3-1];
 		m_From.Slot3_x      = slotx[Slot3-1];
 		m_From.Slot3_y      = sloty[Slot3-1];

            if (Slot1 == Slot2 && Slot2 == Slot3) // All Three the same 
            {
	Image1 = 9350;
	Image2 = 9350;
	Image3 = 9350;
               if (Slot1 == 1) // Gold
               {
                  from.AddToBackpack( new BankCheck( 1000000 ) );
                  m_From.Prize1WinTotal += 1;
                  message = "$1,000,000 gold";
                  bc = true;
               }
               else if (Slot1 == 2) // Silver
               {
                  from.AddToBackpack( new BankCheck( 100000 ) );
                  m_From.Prize2WinTotal += 1;
                  message = "$100,000 gold";
                  bc = true;
               }
               else if (Slot1 == 3) // scales
               {
                  from.AddToBackpack( new BankCheck( 50000 ) );
                  m_From.Prize3WinTotal += 1;
                  message = "$50,000 gold";
                  bc = true;
               }
               else if (Slot1 == 4) // crystal ball
               {
                  from.AddToBackpack( new Gold( 10000 ) );
                  m_From.Prize4WinTotal += 1;
                  message = "$10,000 gold";
               }
               else if (Slot1 == 5) // hstand
               {
                  from.AddToBackpack( new Gold( 5000 ) );
                  m_From.Prize5WinTotal += 1;
                  message = "$5,000 gold";
               }
               else if (Slot1 == 6) // wizard's hat
               {
               	m_From.Prize6WinTotal += 1;
		message = "Magic Show";
		from.SendGump( new MagicShowGump(from,m_From) );
		return;
               }
               else // flask
               {
                  from.AddToBackpack( new Gold( 2500 ) );
                  m_From.Prize7WinTotal += 1;
                  message = "$2,500 gold";
               }
            }
            else if (Slot1 == Slot2) // Only two the same
            {
               if (Slot1 == 4 || Slot1 == 5) // crystal ball, burner
               {
                  from.AddToBackpack( new Gold( 1000 ) );
                  m_From.Prize8WinTotal += 1;
                  message = "$1,000 gold";
	Image1 = 9350;
	Image2 = 9350;
               }
            }
            else if (Slot2 == Slot3) // Only two the same
            {
               if (Slot2 == 4 || Slot2 == 5) // crystal ball, burner
               {
                  from.AddToBackpack( new Gold( 1000 ) );
                  m_From.Prize8WinTotal += 1;
                  message = "$1,000 gold";
	Image2 = 9350;
	Image3 = 9350;
               }
            }
            else if (Slot1 == Slot3) // Only two the same
            {
               if (Slot1 == 4 || Slot1 == 5) // crystal ball, burner
               {
                  from.AddToBackpack( new Gold( 1000 ) );
                  m_From.Prize8WinTotal += 1;
                  message = "$1,000 gold";
	Image1 = 9350;
	Image3 = 9350;
               }
            }


if (message!="You did not win.")
{
Effects.PlaySound( from.Location, from.Map, 0x36 );
string message1 = String.Format( "{0} just won - {1} playing slots.",from.Name,message );
message = String.Format( "You won {0}!", message );
from.SendMessage( 0x22,  message );

if (bc)
{
foreach ( NetState state in NetState.Instances )
{
Mobile m = state.Mobile;

if ( m != null && m.NetState != from.NetState )
m.SendMessage( 0x482, message1 );
} //foreach
} //if

}

         }

	else if (spin==2)
		message = "You need more money!";

	AddPage(0);

	AddImageTiled( 74, 78, 250, 184, 2624 ); //full
	AddBackground( 90, 95, 220, 150, 9350 );
	AddAlphaRegion( 74, 78, 250, 184 );

	AddButton(  180, 175, 2642,2643, 101, GumpButtonType.Reply, 0 );
	AddLabel( 187, 200, 152, "Spin");

	AddButton( 74, 78, 3, 4, 666, GumpButtonType.Reply, 0 );
	
	AddLabel( 106, 190, 152, "Paytable");
	AddButton( 125, 180, 2117, 2118, 102, GumpButtonType.Reply, 0 );

	if ( from.AccessLevel >= AccessLevel.Seer )
	{
	AddLabel( 253, 190, 152, "Stats");
	AddButton( 265, 180, 2117, 2118, 103, GumpButtonType.Reply, 0 );
	}

	AddLabel( 162, 84, 152, "Slot Machine");
	AddLabel( 136, 220, 34, message);

	AddBackground( 100, 105, 60, 60, Image1); // Slot Machine Box 1
	AddBackground( 170, 105, 60, 60, Image2); // Slot Machine Box 2
	AddBackground( 240, 105, 60, 60, Image3); // Slot Machine Box 3

	AddItem( m_From.Slot1_x, m_From.Slot1_y, m_From.Slot1Graphic );
	AddItem( m_From.Slot2_x+70, m_From.Slot2_y, m_From.Slot2Graphic );
	AddItem( m_From.Slot3_x+140, m_From.Slot3_y, m_From.Slot3Graphic );

      }

      public int findreelspot(int hold)
      {
	int [] reel1 = {1};							// gold
	int [] reel2 = {2,8};		  					// silver
	int [] reel3 = {3,9,14};						// scales
	int [] reel4 = {4,10,15,19};						// crystal ball
	int [] reel5 = {5,11,16,20,23,26,29,32};				// head stand
	int [] reel6 = {6,12,17,21,24,27,30,33,35,36,37,59};			// wizards hat
	int [] reel7 = {7,13,18,22,25,28,31,34,49,50,51,52,53,57,58,61,62};	// Flask

	foreach ( int item in reel1 )
	  if (item==hold)
	    return 1;

	foreach ( int item in reel2 )
	  if (item==hold)
	    return 2;

	foreach ( int item in reel3 )
	  if (item==hold)
	    return 3;

	foreach ( int item in reel4 )
	  if (item==hold)
	    return 4;

	foreach ( int item in reel5 )
	  if (item==hold)
	    return 5;

	foreach ( int item in reel6 )
	  if (item==hold)
	    return 6;

	foreach ( int item in reel7 )
	  if (item==hold)
	    return 7;

	    return 8;
      }


      public override void OnResponse( Server.Network.NetState sender, RelayInfo info ) 
      {
	Mobile from = sender.Mobile;

	switch ( info.ButtonID )
	{
	  case 101:
	  { // spin
	    Container pack = from.Backpack;

	    if ( pack != null && pack.ConsumeTotal( typeof( Gold ), 500 ) )
		from.SendGump( new SlotMachineGump(from,m_From,1) );
	    else
		from.SendGump( new SlotMachineGump(from,m_From,2) );

            break;
	  }
	  case 102:
	  { // help
	    from.CloseGump( typeof( HelpStatsGump ) );
	    from.SendGump( new HelpStatsGump(m_From) );
	    from.SendGump( new SlotMachineGump(from,m_From,0) );
	    break;
	  }
	  case 103:
	  { // stats
	    from.CloseGump( typeof( SlotsStatsGump ) );
	    from.SendGump( new SlotsStatsGump(m_From) );
	    from.SendGump( new SlotMachineGump(from,m_From,0) );
	    break;
	  }  
	  case 666:
	  { // quit
	    break;
	  }
	}
      } // OnResponse

   } // SlotMachineGump


	public class HelpStatsGump : Gump
	{

      private SlotMachine m_From;

		public HelpStatsGump( SlotMachine machine ) : base( 240, 0 )
		{
			m_From = machine;

			AddPage( 0 );

	AddImageTiled( 85, 80, 230, 340, 2624 ); //5120
	AddAlphaRegion( 85, 80, 230, 340 );

 	AddLabel( 153, 85, 152, "Match all three");

	AddBackground( 100, 105, 60, 30, 9300);
	AddBackground( 170, 105, 60, 30, 9300);
	AddBackground( 240, 105, 60, 30, 9300);
	AddItem( 108,108,3823 );
	AddItem( 178,108,3823 );
	AddItem( 248,108,3823 );
	AddLabel( 150, 110, 152, "One Million Gold!");

	AddBackground( 100, 140, 60, 30, 9300);
	AddBackground( 170, 140, 60, 30, 9300);
	AddBackground( 240, 140, 60, 30, 9300);
	AddItem( 108,143,3826 );
	AddItem( 178,143,3826 );
	AddItem( 248,143,3826 );
	AddLabel( 120, 145, 152, "One Hundred Thousand Gold!");

	AddBackground( 100, 175, 60, 30, 9300);
	AddBackground( 170, 175, 60, 30, 9300);
	AddBackground( 240, 175, 60, 30, 9300);
	AddItem( 108,176,6225 );
	AddItem( 178,176,6225 );
	AddItem( 248,176,6225 );
	AddLabel( 140, 180, 152, "Fifty Thousand Gold!");

	AddBackground( 100, 210, 60, 30, 9300);
	AddBackground( 170, 210, 60, 30, 9300);
	AddBackground( 240, 210, 60, 30, 9300);
	AddItem( 108,213,3629 );
	AddItem( 178,213,3629 );
	AddItem( 248,213,3629 );
	AddLabel( 144, 215, 152, "Ten Thousand gold!");

	AddBackground( 100, 245, 60, 30, 9300);
	AddBackground( 170, 245, 60, 30, 9300);
	AddBackground( 240, 245, 60, 30, 9300);
	AddItem( 108,249,6218 );
	AddItem( 178,249,6218 );
	AddItem( 248,249,6218 );
	AddLabel( 143, 250, 152, "Five Thousand gold!");

	AddBackground( 100, 280, 60, 30, 9300);
	AddBackground( 170, 280, 60, 30, 9300);
	AddBackground( 240, 280, 60, 30, 9300);
	AddItem( 108,285,5912 );
	AddItem( 178,285,5912 );
	AddItem( 248,285,5912 );
	AddLabel( 145, 285, 152, "Magic Show Bonus!");

	AddBackground( 100, 315, 29, 30, 9300);
	AddBackground( 170, 315, 29, 30, 9300);
	AddBackground( 240, 315, 29, 30, 9300);
	AddBackground( 130, 315, 29, 30, 9300);
	AddBackground( 200, 315, 29, 30, 9300);
	AddBackground( 270, 315, 29, 30, 9300);
	AddItem( 95,318,6186 );
	AddItem( 165,318,6186 );
	AddItem( 235,318,6186 );
	AddItem( 125,318,6188 );
	AddItem( 195,318,6188 );
	AddItem( 265,318,6188 );

	AddLabel( 120, 320, 152, "Twenty Five Hundred Gold!");

	AddLabel( 154, 355, 152, "Match any two");
	AddBackground( 100, 375, 29, 30, 9300);
	AddBackground( 170, 375, 29, 30, 9300);
	AddBackground( 240, 375, 29, 30, 9300);
	AddBackground( 130, 375, 29, 30, 9300);
	AddBackground( 200, 375, 29, 30, 9300);
	AddBackground( 270, 375, 29, 30, 9300);
	AddItem( 92,378,6218 );
	AddItem( 162,379,6218 );
	AddItem( 232,378,6218 );
	AddItem( 122,379,3629 );
	AddItem( 192,378,3629 );
	AddItem( 262,379,3629 );

	AddLabel( 144, 380, 152, "One Thousand gold!");
	}

	}

	public class MagicShowGump : Gump
	{

      private SlotMachine m_From;

		public MagicShowGump( Mobile from, SlotMachine machine ) : base( 10, 100 )
		{
			m_From = machine;

         		Closable = false;
         		Dragable = false;

			AddPage( 0 );

			AddBackground( 70, 100, 258, 120, 5120 );

			AddLabel(  90, 105, 1500, "Click on a magic hat for your bonus" );

			int [] rb = {0,0,0};

			int i=0,j,temp;
			bool doit;

			while (i<3)
			{
			  temp = Utility.Random( 3 );
			  doit = true;
			  for (  j=0;j<3;++j )
			    if (rb[j]==101+temp)
			    {
				doit = false;
				break;
			    }

			    if (doit)
			    {
			    	rb[i] = 101+temp;
				++i;
			    }
			}

			AddButton( 100, 130, 5569, 5570, rb[0], GumpButtonType.Reply, 0 );
			AddButton( 170, 130, 5569, 5570, rb[1], GumpButtonType.Reply, 0 );
			AddButton( 240, 130, 5569, 5570, rb[2], GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{

		Mobile from = sender.Mobile;
		Container pack = from.Backpack;
		int temp;

			switch ( info.ButtonID )
			{
				case 101:
				{ // gold
				  temp = Utility.Random( 25000,50000 );
				  from.AddToBackpack( new BankCheck( temp ) );
				  from.SendMessage( 0x22, String.Format("You won {0} gold!",temp) );
				  from.SendGump( new SlotMachineGump(from,m_From,0) );
				  break;
				}
				case 102:
				{ // reagents,armor
				temp = Utility.Random( 6 );
				if (temp==0)
				{
		                  BaseArmor chest;
		                  chest = new PlateChest();
		                  chest.Resource = CraftResource.Iron;
		                  chest.Hue = 0x845;
		                  pack.AddItem( chest );
		                  from.SendMessage( 0x22,"You won Platemail Chest!");
				}
				else if (temp==1)
				{
		                  BaseArmor gloves;
		                  gloves = new PlateGloves();
		                  gloves.Resource = CraftResource.Iron;
		                  gloves.Hue = 0x845;
		                  pack.AddItem( gloves );
		                  from.SendMessage( 0x22,"You won Platemail Gloves!");
				}
				else if (temp==2)
				{
		                  BaseArmor arms;
		                  arms = new PlateArms();
		                  arms.Resource = CraftResource.Iron;
		                  arms.Hue = 0x845;
		                  pack.AddItem( arms );
		                  from.SendMessage( 0x22,"You won Platemail Arms!");
				}
				else if (temp==3)
				{
		                  BaseArmor helm;
        		          helm = new PlateHelm();
	        	          helm.Resource = CraftResource.Iron;
	        	          helm.Hue = 0x845;
	        	          pack.AddItem( helm );
		                  from.SendMessage( 0x22,"You won Platemail Helmet!");
				}
				else if (temp==4)
				{
		                  BaseArmor gorget;
		                  gorget = new PlateGorget();
		                  gorget.Resource = CraftResource.Iron;
		                  gorget.Hue = 0x845;
		                  pack.AddItem( gorget );
		                  from.SendMessage( 0x22,"You won Platemail Gorget!");
				}
				else if (temp==5)
				{
		                  BaseArmor legs;
		                  legs = new PlateLegs();
		                  legs.Resource = CraftResource.Iron;
		                  legs.Hue = 0x845;
		                  pack.AddItem( legs );
		                  from.SendMessage( 0x22,"You won Platemail Legs!");
				}
				else
				{
		                  from.AddToBackpack( new BagOfReagents( 100 ) );
		                  from.SendMessage( 0x22, String.Format("You won a Bag of Reagents!") );
				}
				from.SendGump( new SlotMachineGump(from,m_From,0) );
				  break;
				}
				case 103:
				{ // 
				  temp = Utility.Random( 500,5000 );
				  from.AddToBackpack( new Gold( temp ) );
	  			  from.SendMessage( 0x22, String.Format("You won {0} gold!",temp) );
	  			  from.SendGump( new SlotMachineGump(from,m_From,0) );
				  break;
				}
			} //switch
		}
	}

	public class SlotsStatsGump : Gump
	{

	      private SlotMachine m_From;

		public SlotsStatsGump( SlotMachine machine ) : base( 50, 160 )
		{
			m_From = machine;

			AddPage( 0 );

			AddBackground( 30, 100, 90, 160, 5120 );

			AddLabel( 45, 100, 70, "Slot Stats" );
			AddLabel( 48, 115, 600, "1: "+m_From.Prize1WinTotal );
			AddLabel( 45, 130, 600, "2: "+m_From.Prize2WinTotal );
			AddLabel( 45, 145, 600, "3: "+m_From.Prize3WinTotal );
			AddLabel( 45, 160, 600, "4: "+m_From.Prize4WinTotal );
			AddLabel( 45, 175, 600, "5: "+m_From.Prize5WinTotal );
			AddLabel( 45, 190, 600, "6: "+m_From.Prize6WinTotal );
			AddLabel( 45, 205, 600, "7: "+m_From.Prize7WinTotal );
			AddLabel( 45, 220, 600, "8: "+m_From.Prize8WinTotal );

			AddLabel(  48, 234, 1500, "Reset" );

			AddButton( 85, 235, 2117, 2118, 101, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{

			switch ( info.ButtonID )
			{
				case 101:
				{ // reset
				  m_From.Prize1WinTotal = 0;
				  m_From.Prize2WinTotal = 0;
				  m_From.Prize3WinTotal = 0;
				  m_From.Prize4WinTotal = 0;
				  m_From.Prize5WinTotal = 0;
				  m_From.Prize6WinTotal = 0;
				  m_From.Prize7WinTotal = 0;
				  m_From.Prize8WinTotal = 0;
         			  break;
				}
			}
		}
	}

  }
}
