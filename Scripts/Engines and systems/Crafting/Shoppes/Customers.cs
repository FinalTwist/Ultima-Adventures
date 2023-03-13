using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;

namespace Server.Misc
{
    class Customers
    {
		public static void AddCustomers( Mobile from, BaseShoppe shoppe, int customer )
		{
			string client = shoppe.Customer01;

			if ( customer == 1 ){ client = shoppe.Customer01; }
			else if ( customer == 2 ){ client = shoppe.Customer02; }
			else if ( customer == 3 ){ client = shoppe.Customer03; }
			else if ( customer == 4 ){ client = shoppe.Customer04; }
			else if ( customer == 5 ){ client = shoppe.Customer05; }
			else if ( customer == 6 ){ client = shoppe.Customer06; }
			else if ( customer == 7 ){ client = shoppe.Customer07; }
			else if ( customer == 8 ){ client = shoppe.Customer08; }
			else if ( customer == 9 ){ client = shoppe.Customer09; }
			else if ( customer == 10 ){ client = shoppe.Customer10; }
			else if ( customer == 11 ){ client = shoppe.Customer11; }
			else if ( customer == 12 ){ client = shoppe.Customer12; }

			int exist = Convert.ToInt32( GetDataElement( client, 3 ) );

			if ( from != null && exist == 0 )
			{
				int repMax = (int)(GetReputation( from, shoppe )/10);
				int repMin = (int)(repMax/5);
					if ( repMin < 10 ){ repMin = 10; }
					if ( repMin >= repMax ){ repMax = repMin + 10; }

				int gold = Utility.RandomMinMax( repMin, repMax );

				gold = (int)(( gold * 2 ) * (MyServerSettings.GetGoldCutRate(from, null) * .01)); //FINAL change this for gold payout

				int fame = Utility.RandomMinMax( ((int)(gold/20)), ((int)(gold/20)+Utility.RandomMinMax( 0, 3 )) );
					if ( fame < 5 ){ fame = 5; }
					else if ( fame > 50 ){ fame = 50; }

				int skill = Utility.RandomMinMax( ((int)((gold/125)+35)), ((int)((gold/125)+35)+Utility.RandomMinMax( 0, 5 )) );
					if ( skill < 30 ){ skill = 30; }
					else if ( skill > 120 ){ skill = 120; }

				int tools = Utility.RandomMinMax( ((int)(gold/100)), ((int)(gold/100)+Utility.RandomMinMax( 0, 2 )) );
					if ( tools < 1 ){ tools = 1; }
					else if ( tools > 10 ){ tools = 10; }

				int resource = Utility.RandomMinMax( ((int)(gold/20)), ((int)(gold/20)+Utility.RandomMinMax( 0, 5 )) );
					if ( resource < 5 ){ resource = 5; }

				string customerName = NameList.RandomName( "male" );
					if ( Utility.RandomBool() ){ customerName = NameList.RandomName( "female" ); }

				string customerTitle = HenchmanFunctions.GetTitle();

				string customerTask = Server.Items.BaseShoppe.MakeTask( shoppe );

				string finalCustomer = customerTask + "#" + customerName + " " + customerTitle + "#";
				finalCustomer = finalCustomer + "1" + "#" + gold + "#" + tools + "#" + resource + "#" + skill + "#" + fame + "";

				if ( customer == 1 ){ shoppe.Customer01 = finalCustomer; }
				else if ( customer == 2 ){ shoppe.Customer02 = finalCustomer; }
				else if ( customer == 3 ){ shoppe.Customer03 = finalCustomer; }
				else if ( customer == 4 ){ shoppe.Customer04 = finalCustomer; }
				else if ( customer == 5 ){ shoppe.Customer05 = finalCustomer; }
				else if ( customer == 6 ){ shoppe.Customer06 = finalCustomer; }
				else if ( customer == 7 ){ shoppe.Customer07 = finalCustomer; }
				else if ( customer == 8 ){ shoppe.Customer08 = finalCustomer; }
				else if ( customer == 9 ){ shoppe.Customer09 = finalCustomer; }
				else if ( customer == 10 ){ shoppe.Customer10 = finalCustomer; }
				else if ( customer == 11 ){ shoppe.Customer11 = finalCustomer; }
				else if ( customer == 12 ){ shoppe.Customer12 = finalCustomer; }
			}
		}

		public static int GetReputation( Mobile from, BaseShoppe shoppe )
		{
			int value = shoppe.ShoppeReputation;
			if ( ((PlayerMobile)from).NpcGuild == shoppe.ShelfGuild ){ value = value + 500 + (int)(Server.Items.BaseShoppe.GetSkillValue( shoppe.ShelfSkill, from ) * 5 ); }

			return value;
		}

		public static int GetChance( int skill, int difficulty )
		{
			int rate = (int)(skill - difficulty);
				if ( rate < 1 ){ rate = 0; }
				if ( rate > 0 ){ rate = rate + 25; }
				if ( rate > 100 ){ rate = 100; }

			return rate;
		}

		public static void CustomerCycle( Mobile from, BaseShoppe shoppe )
		{
			AddCustomers( from, shoppe, 1 );
			AddCustomers( from, shoppe, 2 );
			AddCustomers( from, shoppe, 3 );
			AddCustomers( from, shoppe, 4 );
			AddCustomers( from, shoppe, 5 );
			AddCustomers( from, shoppe, 6 );
			AddCustomers( from, shoppe, 7 );
			AddCustomers( from, shoppe, 8 );
			AddCustomers( from, shoppe, 9 );
			AddCustomers( from, shoppe, 10 );
			AddCustomers( from, shoppe, 11 );
			AddCustomers( from, shoppe, 12 );
			shoppe.InvalidateProperties();
		}

		public static void FillOrder( Mobile from, BaseShoppe shoppe, int customer )
		{

				string cust = "";
				if ( customer == 1 ){ cust = shoppe.Customer01; }
				else if ( customer == 2 ){ cust = shoppe.Customer02; }
				else if ( customer == 3 ){ cust = shoppe.Customer03; }
				else if ( customer == 4 ){ cust = shoppe.Customer04; }
				else if ( customer == 5 ){ cust = shoppe.Customer05; }
				else if ( customer == 6 ){ cust = shoppe.Customer06; }
				else if ( customer == 7 ){ cust = shoppe.Customer07; }
				else if ( customer == 8 ){ cust = shoppe.Customer08; }
				else if ( customer == 9 ){ cust = shoppe.Customer09; }
				else if ( customer == 10 ){ cust = shoppe.Customer10; }
				else if ( customer == 11 ){ cust = shoppe.Customer11; }
				else if ( customer == 12 ){ cust = shoppe.Customer12; }

				int taskGold = 0;
				int taskTools = 0;
				int taskResources = 0;
				int taskDifficulty = 0;
				
					taskGold = Convert.ToInt32( Server.Misc.Customers.GetDataElement( cust, 4 ) ); //FINAL payout is in customers.cs 
					taskTools = Convert.ToInt32( Server.Misc.Customers.GetDataElement( cust, 5 ) );
					taskResources = Convert.ToInt32( Server.Misc.Customers.GetDataElement( cust, 6 ) );
					taskDifficulty = Server.Misc.Customers.GetChance( Server.Items.BaseShoppe.GetSkillValue( shoppe.ShelfSkill, from ), Convert.ToInt32( Server.Misc.Customers.GetDataElement( cust, 7 ) ) );
					

			if ( (taskDifficulty <= 0) || (shoppe.ShoppeTools < taskTools) || (shoppe.ShoppeResources < taskResources) || (( shoppe.ShoppeGold + taskGold ) > 100001) )
			{
				
				
				from.SendMessage( "You lack the resources or tools to complete this order." );

				return;
			}

			string client = shoppe.Customer01;

			if ( customer == 1 ){ client = shoppe.Customer01; }
			else if ( customer == 2 ){ client = shoppe.Customer02; }
			else if ( customer == 3 ){ client = shoppe.Customer03; }
			else if ( customer == 4 ){ client = shoppe.Customer04; }
			else if ( customer == 5 ){ client = shoppe.Customer05; }
			else if ( customer == 6 ){ client = shoppe.Customer06; }
			else if ( customer == 7 ){ client = shoppe.Customer07; }
			else if ( customer == 8 ){ client = shoppe.Customer08; }
			else if ( customer == 9 ){ client = shoppe.Customer09; }
			else if ( customer == 10 ){ client = shoppe.Customer10; }
			else if ( customer == 11 ){ client = shoppe.Customer11; }
			else if ( customer == 12 ){ client = shoppe.Customer12; }

			int difficulty = Server.Misc.Customers.GetChance( Server.Items.BaseShoppe.GetSkillValue( shoppe.ShelfSkill, from ), Convert.ToInt32( Server.Misc.Customers.GetDataElement( client, 7 ) ) );

			int tools = Convert.ToInt32( GetDataElement( client, 5 ) );
			int resources = Convert.ToInt32( GetDataElement( client, 6 ) );
			int fame = Convert.ToInt32( GetDataElement( client, 8 ) );

			if ( difficulty >= Utility.RandomMinMax( 1, 110 ) )
			{
				from.PlaySound( 0x2E6 );
				from.PlaySound( shoppe.ShelfSound );

				int gold = Convert.ToInt32( GetDataElement( client, 4 ) );

				shoppe.ShoppeReputation = shoppe.ShoppeReputation + fame;
					if ( shoppe.ShoppeReputation > 10000 ){ shoppe.ShoppeReputation = 10000; }

				shoppe.ShoppeGold = shoppe.ShoppeGold + gold;
					if ( shoppe.ShoppeGold > 500000 ){ shoppe.ShoppeGold = 500000; }
			}
			else
			{
				from.PlaySound( from.Female ? 812 : 1086 );
				from.PlaySound( shoppe.ShelfSound );
                int gold = Convert.ToInt32(GetDataElement(client, 4));

                shoppe.ShoppeReputation = shoppe.ShoppeReputation - fame;
					if ( shoppe.ShoppeReputation < 0 ){ shoppe.ShoppeReputation = 0; }
                shoppe.ShoppeGold = shoppe.ShoppeGold - gold; //Sygun - Compensation for failing the job
                    if (shoppe.ShoppeGold < 0) { shoppe.ShoppeGold = 0; }
            }

			shoppe.ShoppeTools = shoppe.ShoppeTools - tools;
				if ( shoppe.ShoppeTools < 0 ){ shoppe.ShoppeTools = 0; }

			shoppe.ShoppeResources = shoppe.ShoppeResources - resources;
				if ( shoppe.ShoppeResources < 0 ){ shoppe.ShoppeResources = 0; }

			RemoveEntry( shoppe, customer );

			int poolofsnot = 1;

			if (difficulty <= 25)
				poolofsnot = 2;
			else if (difficulty <= 50)
				poolofsnot = 3;
			else if (difficulty <= 75)
				poolofsnot = 4;
			else if (difficulty <= 100)
				poolofsnot = 5;
            else if (difficulty <= 120)
                poolofsnot = 6;

            while (poolofsnot > 0)
			{
				poolofsnot -= 1;
				Server.Items.BaseShoppe.ProgressSkill( from, shoppe, difficulty );
			}
		}

		public static void CancelOrder( Mobile from, BaseShoppe shoppe, int customer )
		{
			from.PlaySound( from.Female ? 802 : 1074 );

			string client = shoppe.Customer01;

			if ( customer == 1 ){ client = shoppe.Customer01; }
			else if ( customer == 2 ){ client = shoppe.Customer02; }
			else if ( customer == 3 ){ client = shoppe.Customer03; }
			else if ( customer == 4 ){ client = shoppe.Customer04; }
			else if ( customer == 5 ){ client = shoppe.Customer05; }
			else if ( customer == 6 ){ client = shoppe.Customer06; }
			else if ( customer == 7 ){ client = shoppe.Customer07; }
			else if ( customer == 8 ){ client = shoppe.Customer08; }
			else if ( customer == 9 ){ client = shoppe.Customer09; }
			else if ( customer == 10 ){ client = shoppe.Customer10; }
			else if ( customer == 11 ){ client = shoppe.Customer11; }
			else if ( customer == 12 ){ client = shoppe.Customer12; }

			int lose = Convert.ToInt32( GetDataElement( client, 8 ) );

			shoppe.ShoppeReputation = shoppe.ShoppeReputation - lose;
				if ( shoppe.ShoppeReputation < 0 ){ shoppe.ShoppeReputation = 0; }

			RemoveEntry( shoppe, customer );
		}

		public static void RemoveEntry( BaseShoppe shoppe, int customer )
		{
			if ( customer == 1 ){ shoppe.Customer01 = ""; }
			else if ( customer == 2 ){ shoppe.Customer02 = ""; }
			else if ( customer == 3 ){ shoppe.Customer03 = ""; }
			else if ( customer == 4 ){ shoppe.Customer04 = ""; }
			else if ( customer == 5 ){ shoppe.Customer05 = ""; }
			else if ( customer == 6 ){ shoppe.Customer06 = ""; }
			else if ( customer == 7 ){ shoppe.Customer07 = ""; }
			else if ( customer == 8 ){ shoppe.Customer08 = ""; }
			else if ( customer == 9 ){ shoppe.Customer09 = ""; }
			else if ( customer == 10 ){ shoppe.Customer10 = ""; }
			else if ( customer == 11 ){ shoppe.Customer11 = ""; }
			else if ( customer == 12 ){ shoppe.Customer12 = ""; }
			shoppe.InvalidateProperties();
		}

		public static string GetDataElement( string customer, int section )
		{
			string value = "";

			if ( customer.Length > 0 )
			{
				string[] customers = customer.Split('#');
				int element = 1;
				foreach ( string info in customers )
				{
					if ( element == 1 && element == section ){ value = info; }		// TASK
					else if ( element == 2 && element == section ){ value = info; } // FOR WHO
					else if ( element == 3 && element == section ){ value = info; } // TASK STATUS
					else if ( element == 4 && element == section ){ value = info; } // GOLD
					else if ( element == 5 && element == section ){ value = info; } // TOOLS
					else if ( element == 6 && element == section ){ value = info; } // RESOURCES
					else if ( element == 7 && element == section ){ value = info; } // DIFFICULTY
					else if ( element == 8 && element == section ){ value = info; } // REPUTATION

					element++;
				}
			}

			if ( value == "" ){ value = "0"; }

			return value;
		}

	}
}