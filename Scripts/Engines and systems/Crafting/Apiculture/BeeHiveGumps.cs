using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Multis;
using Server.Targeting;

namespace Server.Engines.Apiculture
{	
	public class apiBeeHiveMainGump : Gump
	{
		apiBeeHive m_hive;

		public apiBeeHiveMainGump( Mobile from, apiBeeHive hive ) : base( 20, 20 )
		{
			m_hive = hive;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			
			AddPage(0);
			AddBackground(37, 26, 205, 161, 3600);
			
			//vines
			AddItem(12, 91, 3307);
			AddItem(11, 24, 3307);
			AddItem(206, 87, 3307);
			AddItem(205, 20, 3307);

			AddImage(101, 66, 1417);  //circle thing
			AddItem(118, 89, 2330);   //beehive

			//potions
			AddItem(193, 46, 3848);
			AddItem(193, 96, 3847);
			AddItem(193, 71, 3850);
			AddItem(193, 121, 3852);
			AddItem(193, 146, 3849);

			//status icons
			AddItem(-5, 76, 882); //little bug thing
			AddItem(41, 121, 4088);
			AddItem(45, 148, 3336);
			AddItem(44, 49, 5154);
			AddItem(46, 100, 6884);
			
			//corner boxes
			AddImage(34, 20, 210);
			AddImage(228, 20, 210);
			AddImage(34, 172, 210);
			AddImage(228, 172, 210);
			
			//boxes around status icons
			AddImage(58, 71, 212);  //infestation
			AddImage(58, 96, 212);  //disease
			AddImage(58, 121, 212); //water
			AddImage(58, 146, 212); //flower

			//potion lables
			AddLabel(190, 46, 0x481,  hive.potAgility.ToString() );  //agility
			AddLabel(190, 72, 0x481,  hive.potPoison.ToString() );   //poison
			AddLabel(190, 96, 0x481,  hive.potCure.ToString() );     //cure
			AddLabel(190, 121, 0x481, hive.potHeal.ToString() );     //heal
			AddLabel(190, 146, 0x481, hive.potStrength.ToString() ); //strength	

			//status labels
			switch( hive.ParasiteLevel )  //parasites
			{
				case 1: AddLabel(81, 71, 52, @"-"); break;
				case 2: AddLabel(81, 71, 37, @"-"); break;
			}
			switch( hive.DiseaseLevel )  //disease
			{
				case 1: AddLabel(81, 96, 52, @"-");break;
				case 2: AddLabel(81, 96, 37, @"-");break;
			}
			switch( hive.ScaleWater() ) //water
			{
				case ResourceStatus.None    : AddLabel(81, 121, 37, @"X"); break;
				case ResourceStatus.VeryLow : AddLabel(81, 121, 37, @"-"); break;
				case ResourceStatus.Low     : AddLabel(81, 121, 52, @"-"); break;
				case ResourceStatus.High    : AddLabel(81, 121, 67, @"+"); break;
				case ResourceStatus.VeryHigh: AddLabel(81, 121, 52, @"+"); break;
			    case ResourceStatus.TooHigh : AddLabel(81, 121, 37, @"+"); break;
			}
			switch( hive.ScaleFlower() ) //flowers
			{
				case ResourceStatus.None    : AddLabel(81, 145, 37, @"X"); break;
				case ResourceStatus.VeryLow : AddLabel(81, 145, 37, @"-"); break;
				case ResourceStatus.Low     : AddLabel(81, 145, 52, @"-"); break;
				case ResourceStatus.High    : AddLabel(81, 145, 67, @"+"); break;
				case ResourceStatus.VeryHigh: AddLabel(81, 145, 52, @"+"); break;
				case ResourceStatus.TooHigh : AddLabel(81, 145, 37, @"+"); break;
			}
			
			//corner labels
			AddLabel(40, 20, 0x481, ((int)hive.HiveStage).ToString() ); //top left (stage)
			
			//last growth
			switch( m_hive.LastGrowth )
			{
				case HiveGrowthIndicator.PopulationDown: AddLabel(234, 20, 37, "-"); break; //red -
				case HiveGrowthIndicator.PopulationUp  : AddLabel(234, 20, 67, "+"); break; //green +
				case HiveGrowthIndicator.NotHealthy    : AddLabel(234, 20, 37, "!"); break; //red !
				case HiveGrowthIndicator.LowResources  : AddLabel(234, 20, 52, "!"); break; //yellow !
				case HiveGrowthIndicator.Grown         : AddLabel(234, 20, 92, "+"); break; //blue +
			}
			
			AddLabel(40, 172, 0x481, "?"); //help
			AddLabel(232, 172, 37, @"\");   //destroy
			AddItem(214, 176, 6256, 0);  //destroy

			//misc labels
			if( hive.HiveStage >= HiveStatus.Producing )
				AddLabel(100, 42, 92, "Colony : "+hive.Population.ToString()+"0k" );
			else if( hive.HiveStage >= HiveStatus.Brooding )
				AddLabel(100, 42, 92, "   Brooding");
			else
				AddLabel(100, 42, 92, "  Colonizing");
			switch( hive.OverallHealth ) //overall health
			{
				case HiveHealth.Dying: AddLabel(116, 146, 37, "Dying"); break;
				case HiveHealth.Sickly: AddLabel(116, 146, 52, "Sickly"); break;
				case HiveHealth.Healthy: AddLabel(116, 146, 67, "Healthy"); break;
				case HiveHealth.Thriving: AddLabel(116, 146, 92, "Thriving"); break;
			}  

			//resource
			AddButton(58, 46, 212, 212, (int)Buttons.butResource, GumpButtonType.Reply, 0);
			//help
			AddButton(34, 172, 212, 212, (int)Buttons.butHelp, GumpButtonType.Reply, 0);
			//destroy
			AddButton(228, 172, 212, 212, (int)Buttons.butDestroy, GumpButtonType.Reply, 0);
			//agility
			AddButton(202, 46, 212, 212, (int)Buttons.butAgil, GumpButtonType.Reply, 0);
			//poison
			AddButton(202, 71, 212, 212, (int)Buttons.butPois, GumpButtonType.Reply, 0);
			//cure
			AddButton(202, 96, 212, 212, (int)Buttons.butCure, GumpButtonType.Reply, 0);
			//heal
			AddButton(202, 121, 212, 212, (int)Buttons.butHeal, GumpButtonType.Reply, 0);
			//strength
			AddButton(202, 146, 212, 212, (int)Buttons.butStr, GumpButtonType.Reply, 0);

		}

		public enum Buttons
		{
			butResource = 1,
			butDestroy,
			butHelp,
			butAgil,
			butPois,
			butCure,
			butHeal,
			butStr,
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == 0 || m_hive.Deleted || !from.InRange( m_hive.GetWorldLocation(), 3 ) )
				return;

			if( !m_hive.IsAccessibleTo( from ) )
			{
				m_hive.LabelTo( from, "You cannot use that." );
				return;
			}

			switch ( info.ButtonID )
			{
				case 1: //Resources
				{
					from.SendGump( new apiBeeHiveProductionGump(from,m_hive) );
					break;
				}
				case 2: //Destroy
				{
					from.SendGump( new apiBeeHiveDestroyGump( from, m_hive ) );
					break;
				}
				case 3: //Help
				{
					from.SendGump( new apiBeeHiveMainGump( from, m_hive ) );
					from.SendGump( new apiBeeHiveHelpGump( from, 0 ) );
					break;
				}
				case 4: //Agility Potion
				{
					AddPotion( from, PotionEffect.AgilityGreater );

					break;
				}
				case 5:	//Poison Potion
				{
					AddPotion( from, PotionEffect.PoisonGreater, PotionEffect.PoisonDeadly );

					break;
				}
				case 6:	//Cure Potion
				{
					AddPotion( from, PotionEffect.CureGreater );

					break;
				}
				case 7:	//Heal Potion
				{
					AddPotion( from, PotionEffect.HealGreater );

					break;
				}
				case 8: //Strength Potion
				{
					AddPotion( from, PotionEffect.StrengthGreater );

					break;
				}
			}
		}

		private void AddPotion( Mobile from, params PotionEffect[] effects )
		{
			Item item = GetPotion( from, effects );

			if ( item != null )
			{
				m_hive.Pour( from, item );
			}
			else
			{
				from.SendLocalizedMessage( 1061884 ); // You don't have any strong potions of that type in your pack.
			}

			from.SendGump( new apiBeeHiveMainGump( from,m_hive) );
		}

		public static Item GetPotion( Mobile from, PotionEffect[] effects )
		{
			if ( from.Backpack == null )
				return null;

			Item[] items = from.Backpack.FindItemsByType( new Type[] { typeof( BasePotion ), typeof( PotionKeg ) } );

			foreach ( Item item in items )
			{
				if ( item is BasePotion )
				{
					BasePotion potion = (BasePotion)item;

					if ( Array.IndexOf( effects, potion.PotionEffect ) >= 0 )
						return potion;
				}
				else
				{
					PotionKeg keg = (PotionKeg)item;

					if ( keg.Held > 0 && Array.IndexOf( effects, keg.Type ) >= 0 )
						return keg;
				}
			}

			return null;
		}
	}

	public class apiBeeHiveProductionGump : Gump
	{
		public static readonly bool NeedHiveTool = true; //need a hivetool to harvest resources?

		public static readonly bool PureWax = false; //does the hive produce pure (instead of raw) wax?

		apiBeeHive m_hive;

		public apiBeeHiveProductionGump( Mobile from, apiBeeHive hive ) : base( 20, 20 )
		{
			m_hive = hive;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(37, 133, 205, 54, 3600);
			AddBackground(37, 67, 205, 80, 3600);
			AddBackground(37, 26, 205, 55, 3600);
			
			
			AddItem(12, 91, 3307);
			AddItem(11, 24, 3307);
			AddItem(206, 87, 3307);
			AddItem(205, 20, 3307);
			
			AddItem(76, 99, 5154);
			AddItem(149, 97, 2540);

			//honey
			if( m_hive.HiveStage < HiveStatus.Producing )
			{//too early to produce
				AddLabel(185, 97, 37, "X");
			}
			else
			{
				AddLabel(185, 97, 0x481, m_hive.Honey.ToString() );
			}

			//wax
			if( m_hive.HiveStage < HiveStatus.Producing )
			{//too early to produce
				AddLabel(113, 97, 37, "X");
			}
			else
			{
				AddLabel(113, 97, 0x481, m_hive.Wax.ToString() );
			}			

			AddLabel(110, 43, 92, "Production");  //title

			AddItem(44, 47, 6256);
			AddItem(191, 151, 2540);
			
			AddItem(42, 153, 5154);

			AddImage(162, 96, 212);
			AddImage(90, 96, 212);
			
			AddButton(204, 150, 212, 212, (int)Buttons.butHoney, GumpButtonType.Reply, 0);
			AddButton(57, 43, 212, 212, (int)Buttons.butExit, GumpButtonType.Reply, 0);
			AddButton(56, 150, 212, 212, (int)Buttons.butWax, GumpButtonType.Reply, 0);
		}
		
		public enum Buttons
		{
			butHoney = 1,
			butExit,
			butWax,
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == 0 || m_hive.Deleted || !from.InRange( m_hive.GetWorldLocation(), 3 ) )
				return;

			if( !m_hive.IsAccessibleTo( from ) )
			{
				m_hive.LabelTo( from, "You cannot use that." );
				return;
			}

			switch ( info.ButtonID )
			{
				case (int)Buttons.butExit: //Exit back to main gump
				{
					from.SendGump( new apiBeeHiveMainGump(from,m_hive) );
					break;
				}
				case (int)Buttons.butHoney: //Honey
				{
					//ToDo: get hurt or poisoned when harvesting

					Item hivetool = GetHiveTool( from );

					if( NeedHiveTool )
					{
						if( hivetool == null || !(hivetool is HiveTool) )
						{
							m_hive.LabelTo( from, "You need a hive tool to extract the excess honey!" );
							from.SendGump( new apiBeeHiveProductionGump( from, m_hive ) );
							return;
						}
					}

					if( m_hive.Honey < 3 )
					{
						m_hive.LabelTo( from, "There isn't enough honey in the hive to fill a bottle!" );
						from.SendGump( new apiBeeHiveProductionGump( from, m_hive ) );
						break;
					}

					Container pack = from.Backpack;

					if ( pack != null && pack.ConsumeTotal( typeof( Bottle ), 1 ) )
					{
						JarHoney honey = new JarHoney();

						if ( !from.PlaceInBackpack( honey ) )
						{
							honey.Delete();
							from.PlaceInBackpack( new Bottle() ); //add the consumed bottle
							m_hive.LabelTo( from, "There is not enough room in your backpack for the honey!" );
							from.SendGump( new apiBeeHiveProductionGump( from, m_hive ) );
							break;
						}

						if( NeedHiveTool )
						{
							((HiveTool)hivetool).UsesRemaining--;
							if( ((HiveTool)hivetool).UsesRemaining < 1 )
							{
								from.SendMessage("You wear out the hive tool.");
								hivetool.Delete();
							}
						}

						m_hive.Honey -= 3;
						m_hive.LabelTo( from, "You fill a bottle with golden honey and place it in your pack." );
						from.SendGump( new apiBeeHiveProductionGump(from,m_hive) );
						break;
					}
					else
					{
						m_hive.LabelTo( from, "You need a bottle to fill with honey!" );
						from.SendGump( new apiBeeHiveProductionGump( from, m_hive ) );
						break;
					}
				}
				case (int)Buttons.butWax: //Wax
				{
					//ToDo: get hurt or poisoned when harvesting

					Item hivetool = GetHiveTool( from );

					if( NeedHiveTool )
					{
						if( hivetool == null || !(hivetool is HiveTool) )
						{
							m_hive.LabelTo( from, "You need a hive tool to scrape the excess beeswax!" );
							from.SendGump( new apiBeeHiveProductionGump( from, m_hive ) );
							return;
						}
					}

					if( m_hive.Wax < 1 )
					{
						m_hive.LabelTo( from, "There isn't enough excess wax in the hive to harvest!" );
						return;
					}

					Item wax;

					if( PureWax )
					{
						wax = new Beeswax(m_hive.Wax);
					}
					else
					{
						wax = new RawBeeswax(m_hive.Wax);
					}

					if ( !from.PlaceInBackpack( wax ) )
					{
						wax.Delete();

						m_hive.LabelTo( from, "There is not enough room in your backpack for the wax!" );
						from.SendGump( new apiBeeHiveProductionGump( from, m_hive ) );
						break;
					}

					if( NeedHiveTool )
					{
						((HiveTool)hivetool).UsesRemaining--;
						if( ((HiveTool)hivetool).UsesRemaining < 1 )
						{
							from.SendMessage("You wear out the hive tool.");
							hivetool.Delete();
						}
					}

					m_hive.Wax = 0;
					m_hive.LabelTo( from, "You collect the excess beeswax and place it in your pack." );
					from.SendGump( new apiBeeHiveProductionGump(from,m_hive) );
					break;
				}
			}
		}

		public static Item GetHiveTool( Mobile from )
		{
			if ( from.Backpack == null )
				return null;

			Item item = from.Backpack.FindItemByType( typeof( HiveTool ) );

			if( item != null )
				return item;

			return null;
		}
	}

	public class apiBeeHiveDestroyGump : Gump
	{
		apiBeeHive m_hive;

		public apiBeeHiveDestroyGump( Mobile from, apiBeeHive hive ) : base( 20, 20 )
		{
			m_hive = hive;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			
			AddPage(0);
			
			AddBackground(37, 26, 205, 137, 3600);
			
			AddItem(11, 20, 3307);
			AddItem(205, 20, 3307);
			AddItem(12, 65, 3307);
			AddItem(206, 69, 3307);

			AddLabel(84, 43, 92, "Destory the hive?");
					
			AddItem(73, 68, 2330);
			AddItem(160, 68, 5359);	
			
			AddImage(131, 74, 5601);  //arrow

			AddButton(83, 114, 1150, 1152, (int)Buttons.butCancel, GumpButtonType.Reply, 0);
			AddButton(166, 115, 1153, 1155, (int)Buttons.butOkay, GumpButtonType.Reply, 0);
		}
		
		public enum Buttons
		{
			butCancel = 1,
			butOkay,
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == 0 || m_hive.Deleted || !from.InRange( m_hive.GetWorldLocation(), 3 ) )
				return;

			if( !m_hive.IsAccessibleTo( from ) )
			{
				m_hive.LabelTo( from, "You cannot use that." );
				return;
			}

			switch ( info.ButtonID )
			{
				case (int)Buttons.butCancel: //cancel
				{
					from.SendGump( new apiBeeHiveMainGump( from, m_hive ) );
					break;
				}
				case (int)Buttons.butOkay: //okay
				{
					apiBeeHiveDeed deed = new apiBeeHiveDeed();

					if ( !from.PlaceInBackpack( deed ) )
					{
						deed.Delete();

						m_hive.LabelTo( from, "You cannot destroy the hive with a full backpack!" );
						from.SendGump( new apiBeeHiveMainGump( from, m_hive ) );

						break;
					}

					m_hive.Delete();

					break;
				}
			}
		}
	}
}
