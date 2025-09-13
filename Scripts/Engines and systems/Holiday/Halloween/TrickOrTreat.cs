using System;
using Server;
using Server.Items;
using Server.Guilds;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Misc
{
	public class TrickOrTreat
	{
		public static void Initialize()
		{
			EventSink.Speech += new SpeechEventHandler( EventSink_Speech );
		}

		public static void EventSink_Speech( SpeechEventArgs args )
		{
			Mobile from = args.Mobile;
			int[] keywords = args.Keywords;

			if (from == null )
				return;

			if ( from is PlayerMobile )
			{
				PlayerMobile player = from as PlayerMobile;
				
				if ( args.Speech.ToLower().Equals("trick or treat"))
				{
					Item[] items = from.Backpack.FindItemsByType( typeof( TrickOrTreatBag ) );

					if ( items.Length == 0 )
					{
						from.SendMessage ("You need a goodie basket to go trick or treating!");
					}
					else
					{
						bool foundbag = false;
						
						foreach( TrickOrTreatBag tb in items )
						{
							if ( tb.Uses > 0 )
							{ 
								foreach ( Mobile m in from.GetMobilesInRange( 2 ) ) // TODO: Validate range
								{
									Container cont = m.Backpack;
									if (m == null || cont == null || m.Map == null)
										return;

									if ( m is KungFu) 
									{
										from.Direction = from.GetDirectionTo( m );
										m.Direction = m.GetDirectionTo( from );
										m.Say ("Sorry, I do not celebrate Halloween.");
									}
									else if ( ( m is BaseVendor ) && ( player.BodyMod == 0 ) )
									{
										from.Direction = from.GetDirectionTo( m );
										m.Direction = m.GetDirectionTo( from );
										m.Say ("You are not in costume so how can you go trick or treating?");
									}
									else if ( ( m is BaseVendor ) && ( Utility.Random ( 100 ) > 80 ) )
									{
										Gold m_Gold = (Gold)m.Backpack.FindItemByType( typeof( Gold ) );
										int m_Amount = m.Backpack.GetAmount( typeof( Gold ) );
										from.Direction = from.GetDirectionTo( m );
										m.Direction = m.GetDirectionTo( from );
										cont.ConsumeTotal( typeof( Gold ), m_Amount );
										m.Say ("Sorry, I don't have anything to give you at the moment.");
									}
									else if ( ( m is BaseVendor ) && ( cont.ConsumeTotal( typeof( Gold ), 1 ) ) )
									{
										Gold m_Gold = (Gold)m.Backpack.FindItemByType( typeof( Gold ) );
										int m_Amount = m.Backpack.GetAmount( typeof( Gold ) );
										from.Direction = from.GetDirectionTo( m );
										m.Direction = m.GetDirectionTo( from );
										TrickOrTreat.GiveTreat( from, m, tb );
										tb.ConsumeUse( from );
										cont.ConsumeTotal( typeof( Gold ), m_Amount );
											
										return;
									}
									else if ( m is BaseVendor ) 
									{
										from.Direction = from.GetDirectionTo( m );
										m.Direction = m.GetDirectionTo( from );
										m.Say ("Sorry, I don't have anything to give you at the moment.");
									}
								}

								foundbag = true;

								break;
							}
						}
						if ( !foundbag )
						{
							from.SendMessage("Your trick-or-treat bag seems to be worn out from all the things it was filled with.");
						}
					}
				}
			}
		}

		private static void PlaceItemIn( Container parent, Item item )
		{
			parent.AddItem( item );
		}
		
		public static void GiveTreat ( Mobile from, Mobile vendor, Container gb)
		{
			if ( Utility.Random ( 100 ) <= 5 )
			{
				vendor.Say ("Well, I am out of goodies, but let me give this instead. Happy Halloween.");

					int myGift = Utility.Random ( 11 );

					if ( myGift == 1 )
					{
						int nGift = Utility.Random ( 2 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new LargeDyingPlant() ); }
						else { PlaceItemIn( gb, new DyingPlant() ); }
					}
					else if ( myGift == 2 )
					{
						int nGift = Utility.Random ( 5 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new GrimWarning() ); }
						else if ( nGift == 2 ) { PlaceItemIn( gb, new HalloweenStoneSpike() ); }
						else if ( nGift == 3 ) { PlaceItemIn( gb, new HalloweenStoneSpike2() ); }
						else if ( nGift == 4 ) { PlaceItemIn( gb, new HalloweenSkullPole() ); }
						else { PlaceItemIn( gb, new SkullsOnPike() ); }
					}
					else if ( myGift == 3 )
					{
						int nGift = Utility.Random ( 6 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new HalloweenTree1() ); }
						else if ( nGift == 2 ) { PlaceItemIn( gb, new HalloweenTree2() ); }
						else if ( nGift == 3 ) { PlaceItemIn( gb, new HalloweenTree3() ); }
						else if ( nGift == 4 ) { PlaceItemIn( gb, new HalloweenTree4() ); }
						else if ( nGift == 5 ) { PlaceItemIn( gb, new HalloweenTree5() ); }
						else { PlaceItemIn( gb, new HalloweenTree6() ); }
					}
					else if ( myGift == 4 )
					{
						int nGift = Utility.Random ( 3 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new HalloweenGrave1() ); }
						else if ( nGift == 2 ) { PlaceItemIn( gb, new HalloweenGrave2() ); }
						else { PlaceItemIn( gb, new HalloweenGrave3() ); }
					}
					else if ( myGift == 5 )
					{
						int nGift = Utility.Random ( 2 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new MongbatDartBoardEastDeed() ); }
						else { PlaceItemIn( gb, new MongbatDartBoardSouthDeed() ); }
					}
					else if ( myGift == 6 )
					{
						int nGift = Utility.Random ( 5 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new BloodPentagramDeed() ); }
						else if ( nGift == 2 ) { PlaceItemIn( gb, new BloodyTableAddonDeed() ); }
						else if ( nGift == 3 ) { PlaceItemIn( gb, new EvilFireplaceSouthFaceAddonDeed() ); }
						else if ( nGift == 4 ) { PlaceItemIn( gb, new HalloweenBonePileDeed() ); }
						else { PlaceItemIn( gb, new HalloweenShrineChaosDeed() ); }
					}
					else if ( myGift == 7 )
					{
						int nGift = Utility.Random ( 11 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new DeadBodyEWDeed() ); }
						else if ( nGift == 2 ) { PlaceItemIn( gb, new HalloweenTortSkel() ); }
						else if ( nGift == 3 ) { PlaceItemIn( gb, new HalloweenBlood() ); }
						else if ( nGift == 4 ) { PlaceItemIn( gb, new HalloweenMaiden() ); }
						else if ( nGift == 5 ) { PlaceItemIn( gb, new halloween_shackles() ); }
						else if ( nGift == 6 ) { PlaceItemIn( gb, new halloween_block_southAddonDeed() ); }
						else if ( nGift == 7 ) { PlaceItemIn( gb, new halloween_block_eastAddonDeed() ); }
						else if ( nGift == 8 ) { PlaceItemIn( gb, new halloween_coffin_eastAddonDeed() ); }
						else if ( nGift == 9 ) { PlaceItemIn( gb, new halloween_coffin_southAddonDeed() ); }
						else if ( nGift == 10 ) { PlaceItemIn( gb, new HalloweenChopper() ); }
						else { PlaceItemIn( gb, new DeadBodyNSDeed() ); }
					}
					else if ( myGift == 8 )
					{
						int nGift = Utility.Random ( 4 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new HalloweenColumn() ); }
						else if ( nGift == 2 ) { PlaceItemIn( gb, new HalloweenPylon() ); }
						else if ( nGift == 3 ) { PlaceItemIn( gb, new HalloweenPylonFire() ); }
						else { PlaceItemIn( gb, new HalloweenStoneColumn() ); }
					}
					else if ( myGift == 9 )
					{
						PlaceItemIn( gb, new SlayerDeed() );
					}
					else if ( myGift == 10 )
					{
						int nGift = Utility.Random ( 6 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new halloween_ruined_bookcase() ); }
						else if ( nGift == 2 ) { PlaceItemIn( gb, new halloween_covered_chair() ); }
						else if ( nGift == 3 ) { PlaceItemIn( gb, new halloween_HauntedMirror1() ); }
						else if ( nGift == 4 ) { PlaceItemIn( gb, new halloween_HauntedMirror2() ); }
						else if ( nGift == 5 ) { PlaceItemIn( gb, new halloween_devil_face() ); }
						else { PlaceItemIn( gb, new HalloweenGift() ); }
					}
					else
					{
						int nGift = Utility.Random ( 6 );
						if ( nGift == 1 ) { PlaceItemIn( gb, new BlackCatStatue() ); }
						else if ( nGift == 2 ) { PlaceItemIn( gb, new RuinedTapestry() ); }
						else if ( nGift == 3 ) { PlaceItemIn( gb, new PumpkinScarecrow() ); }
						else if ( nGift == 4 ) { PlaceItemIn( gb, new HalloweenWeb() ); }
						else if ( nGift == 5 ) { PlaceItemIn( gb, new CarvedPumpkin() ); }
						else { PlaceItemIn( gb, new CarvedPumpkin2() ); }
					}
			}
			else
			{
				vendor.Say ("Here is some candy for you. Happy Halloween.");
				PlaceItemIn( gb, new ChocolateMonster() );
			}
		}
	}
}