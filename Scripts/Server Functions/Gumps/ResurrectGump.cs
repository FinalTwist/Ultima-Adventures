using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
	public enum ResurrectMessage
	{
		ChaosShrine = 0,
		VirtueShrine = 1,
		Healer = 2,
		Generic = 3,
	}

	public class ResurrectGump : Gump
	{
		private Mobile m_Healer;
		private int m_Price;
		private bool m_FromSacrifice;
		private double m_HitsScalar;

		public ResurrectGump( Mobile owner ): this( owner, owner, ResurrectMessage.Generic, false )
		{
		}

		public ResurrectGump( Mobile owner, double hitsScalar ): this( owner, owner, ResurrectMessage.Generic, false, hitsScalar )
		{
		}

		public ResurrectGump( Mobile owner, bool fromSacrifice ): this( owner, owner, ResurrectMessage.Generic, fromSacrifice )
		{
		}

		public ResurrectGump( Mobile owner, Mobile healer ): this( owner, healer, ResurrectMessage.Generic, false )
		{
		}

		public ResurrectGump( Mobile owner, ResurrectMessage msg ): this( owner, owner, msg, false )
		{
		}

		public ResurrectGump( Mobile owner, Mobile healer, ResurrectMessage msg ): this( owner, healer, msg, false )
		{
		}

		public ResurrectGump( Mobile owner, Mobile healer, ResurrectMessage msg, bool fromSacrifice ): this( owner, healer, msg, fromSacrifice, 0.0 )
		{
		}

		public ResurrectGump( Mobile owner, Mobile healer, ResurrectMessage msg, bool fromSacrifice, double hitsScalar ): base( 25, 25 )
		{

			if (healer is PlayerMobile && owner is PlayerMobile && ((PlayerMobile)owner).Avatar)
			{
				bool proceed = false;
				PlayerMobile healed = (PlayerMobile)owner;
				PlayerMobile hEaler = (PlayerMobile)healer;

				if (hEaler.BalanceEffect <= -10)
				{
					proceed = true;
					hEaler.BalanceEffect += 10;
				}
				else if (hEaler.BalanceEffect >= 10)
				{
					proceed = true;
					hEaler.BalanceEffect -= 10;
				}

				if (proceed)
				{
					hEaler.SendMessage( "You sacrifice your influence on the balance to resurrect this adventurer." );
					healed.SendMessage( "The other adventurer sacrifices some influence on the balance to bring you back to life." );
				}
				else
				{
					hEaler.SendMessage( "You have insufficient influence over the balance to sacrifice." );
					healed.SendMessage( "The other adventurer does not have sufficient influence over the balance to help you." );
					healed.CloseGump( typeof( ResurrectGump ) );
					return;
				}

			}

			m_Healer = healer;
			m_FromSacrifice = fromSacrifice;
			m_HitsScalar = hitsScalar;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 154);
			AddImage(300, 99, 154);
			AddImage(0, 99, 154);
			AddImage(300, 0, 154);
			AddImage(298, 97, 129);
			AddImage(2, 97, 129);
			AddImage(298, 2, 129);
			AddImage(2, 2, 129);
			AddImage(7, 6, 145);
			AddImage(5, 143, 142);
			AddImage(255, 171, 144);
			AddImage(171, 47, 132);
			AddImage(379, 8, 134);
			AddImage(167, 7, 156);
			AddImage(209, 11, 156);
			AddImage(189, 10, 156);
			AddImage(170, 44, 159);
			AddItem(156, 67, 2);
			AddItem(178, 67, 3);
			AddItem(185, 118, 3244);
			AddButton(146, 260, 4023, 4023, 1, GumpButtonType.Reply, 0);
			AddButton(374, 260, 4020, 4020, 0, GumpButtonType.Reply, 0);
			AddHtml( 267, 95, 224, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG>RESURRECTION</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 93, 163, 400, 76, @"<BODY><BASEFONT Color=#FCFF00><BIG>It is possible for you to be resurrected here by this healer. Do you want to return to the land of the living? If not, you can remain in the spirit realm.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddImage(36, 124, 162);
			AddImage(33, 131, 162);
			AddImage(45, 138, 162);
			AddImage(17, 135, 162);
		}

		public ResurrectGump( Mobile owner, Mobile healer, int price ): base( 25, 25 )
		{


			if (healer is PlayerMobile && owner is PlayerMobile && ((PlayerMobile)owner).Avatar)
			{
				bool proceed = false;
				PlayerMobile healed = (PlayerMobile)owner;
				PlayerMobile hEaler = (PlayerMobile)healer;

				if (hEaler.BalanceEffect <= -10)
				{
					proceed = true;
					hEaler.BalanceEffect += 10;
				}
				else if (hEaler.BalanceEffect >= 10)
				{
					proceed = true;
					hEaler.BalanceEffect -= 10;
				}

				if (proceed)
				{
					hEaler.SendMessage( "You sacrifice your influence on the balance to resurrect this adventurer." );
					healed.SendMessage( "The other adventurer sacrifices some influence on the balance to bring you back to life." );
				}
				else
				{
					hEaler.SendMessage( "You have insufficient influence over the balance to sacrifice." );
					healed.SendMessage( "The other adventurer does not have sufficient influence over the balance to help you." );
					healed.CloseGump( typeof( ResurrectGump ) );
					return;
				}

			}

			m_Healer = healer;
			m_Price = price;

			Closable = false;

			AddPage( 0 );

			AddImage( 0, 0, 3600 );

			AddImageTiled( 0, 14, 15, 200, 3603 );
			AddImageTiled( 380, 14, 14, 200, 3605 );

			AddImage( 0, 201, 3606 );

			AddImageTiled( 15, 201, 370, 16, 3607 );
			AddImageTiled( 15, 0, 370, 16, 3601 );

			AddImage( 380, 0, 3602 );

			AddImage( 380, 201, 3608 );

			AddImageTiled( 15, 15, 365, 190, 2624 );

			AddRadio( 30, 140, 9727, 9730, true, 1 );
			AddHtmlLocalized( 65, 145, 300, 25, 1060015, 0x7FFF, false, false ); // Grudgingly pay the money

			AddRadio( 30, 175, 9727, 9730, false, 0 );
			AddHtmlLocalized( 65, 178, 300, 25, 1060016, 0x7FFF, false, false ); // I'd rather stay dead, you scoundrel!!!

			AddHtmlLocalized( 30, 20, 360, 35, 1060017, 0x7FFF, false, false ); // Wishing to rejoin the living, are you?  I can restore your body... for a price of course...

			AddHtmlLocalized( 30, 105, 345, 40, 1060018, 0x5B2D, false, false ); // Do you accept the fee, which will be withdrawn from your bank?

			AddImage( 65, 72, 5605 );

			AddImageTiled( 80, 90, 200, 1, 9107 );
			AddImageTiled( 95, 92, 200, 1, 9157 );

			AddLabel( 90, 70, 1645, price.ToString() );
			AddHtmlLocalized( 140, 70, 100, 25, 1023823, 0x7FFF, false, false ); // gold coins

			AddButton( 290, 175, 247, 248, 2, GumpButtonType.Reply, 0 );

			AddImageTiled( 15, 14, 365, 1, 9107 );
			AddImageTiled( 380, 14, 1, 190, 9105 );
			AddImageTiled( 15, 205, 365, 1, 9107 );
			AddImageTiled( 15, 14, 1, 190, 9105 );
			AddImageTiled( 0, 0, 395, 1, 9157 );
			AddImageTiled( 394, 0, 1, 217, 9155 );
			AddImageTiled( 0, 216, 395, 1, 9157 );
			AddImageTiled( 0, 0, 1, 217, 9155 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			from.CloseGump( typeof( ResurrectGump ) );

			if (from is PlayerMobile && ((PlayerMobile)from).SoulBound)
			{
				((PlayerMobile)from).ResetPlayer();
				return;
			}

			if( info.ButtonID == 1 || info.ButtonID == 2 )
			{
				if( from.Map == null || !from.Map.CanFit( from.Location, 16, false, false ) )
				{
					from.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
					return;
				}

				if( m_Price > 0 )
				{
					if( info.IsSwitched( 1 ) )
					{
						if( Banker.Withdraw( from, m_Price ) )
						{
							from.SendLocalizedMessage( 1060398, m_Price.ToString() ); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
							from.SendLocalizedMessage( 1060022, Banker.GetBalance( from ).ToString() ); // You have ~1_AMOUNT~ gold in cash remaining in your bank box.
							Server.Misc.Death.Penalty( from, false );
						}
						else
						{
							from.SendLocalizedMessage( 1060020 ); // Unfortunately, you do not have enough cash in your bank to cover the cost of the healing.
							return;
						}
					}
					else
					{
						from.SendLocalizedMessage( 1060019 ); // You decide against paying the healer, and thus remain dead.
						return;
					}
				}

				from.PlaySound( 0x214 );
				from.FixedEffect( 0x376A, 10, 16 );

				if (from.Criminal)
					from.Criminal = false;
					
				from.Resurrect();

				if( m_Healer != null && from != m_Healer )
				{
					VirtueLevel level = VirtueHelper.GetLevel( m_Healer, VirtueName.Compassion );

					switch( level )
					{
						case VirtueLevel.Seeker: from.Hits = AOS.Scale( from.HitsMax, 20 ); break;
						case VirtueLevel.Follower: from.Hits = AOS.Scale( from.HitsMax, 40 ); break;
						case VirtueLevel.Knight: from.Hits = AOS.Scale( from.HitsMax, 80 ); break;
					}
				}

				if( m_FromSacrifice && from is PlayerMobile )
				{
					((PlayerMobile)from).AvailableResurrects -= 1;

					Container pack = from.Backpack;
					Container corpse = from.Corpse;

					if( pack != null && corpse != null )
					{
						List<Item> items = new List<Item>( corpse.Items );

						for( int i = 0; i < items.Count; ++i )
						{
							Item item = items[i];

							if( item.Layer != Layer.Hair && item.Layer != Layer.FacialHair && item.Movable )
								pack.DropItem( item );
						}
					}
				}

				if( from.Fame > 0 )
				{
					int amount = from.Fame / 10;

					Misc.Titles.AwardFame( from, -amount, true );
				}

				if( !Core.AOS && from.ShortTermMurders >= 5 )
				{
					double loss = (100.0 - (4.0 + (from.ShortTermMurders / 5.0))) / 100.0; // 5 to 15% loss

					if( loss < 0.85 )
						loss = 0.85;
					else if( loss > 0.95 )
						loss = 0.95;

					if( from.RawStr * loss > 10 )
						from.RawStr = (int)(from.RawStr * loss);
					if( from.RawInt * loss > 10 )
						from.RawInt = (int)(from.RawInt * loss);
					if( from.RawDex * loss > 10 )
						from.RawDex = (int)(from.RawDex * loss);

					for( int s = 0; s < from.Skills.Length; s++ )
					{
						if( from.Skills[s].Base * loss > 35 )
							from.Skills[s].Base *= loss;
					}
				}

				if( from.Alive && m_HitsScalar > 0 )
					from.Hits = (int)(from.HitsMax * m_HitsScalar);
			}
		}
	}
}