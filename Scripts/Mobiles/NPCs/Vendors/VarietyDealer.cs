using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Mobiles
{
	public class VarietyDealer : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.MerchantsGuild; } }

		[Constructable]
		public VarietyDealer() : base( "the art collector" )
		{
			Job = JobFragment.shopkeep;
			Karma = Utility.RandomMinMax( 13, -45 );
		}

		///////////////////////////////////////////////////////////////////////////

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "The Hunt For Relics", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Variety" ) ));
					}
				}
            }
        }
		
		///////////////////////////////////////////////////////////////////////////

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Gold )
			{
				string sMessage = "";

				if ( dropped.Amount == 500 && Server.Items.MuseumBook.IsEnabled() )
				{
					if (	Server.Items.CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" ) && 
							Server.Items.CharacterDatabase.GetDiscovered( from, "the Land of Lodoria" ) && 
							Server.Items.CharacterDatabase.GetDiscovered( from, "the Island of Umber Veil" ) && 
							Server.Items.CharacterDatabase.GetDiscovered( from, "the Land of Ambrosia" ) && 
							Server.Items.CharacterDatabase.GetDiscovered( from, "the Serpent Island" ) && 
							Server.Items.CharacterDatabase.GetDiscovered( from, "the Isles of Dread" ) && 
							Server.Items.CharacterDatabase.GetDiscovered( from, "the Savaged Empire" ) && 
							Server.Items.CharacterDatabase.GetDiscovered( from, "the Bottle World of Kuldar" ) && 
							Server.Items.CharacterDatabase.GetDiscovered( from, "the Underworld" )
					)
					{
						if ( AlreadyHasBook( from ) )
						{
							this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Here. I see you already have a book." ) ); 
						}
						else if ( CharacterDatabase.GetKeys( from, "Antiques" ) )
						{
							this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Thank you, but you already done that for me." ) ); 
						}
						else
						{
							MuseumBook book = new MuseumBook();
							from.PlaySound( 0x2E6 );
							book.ArtOwner = from;
							from.AddToBackpack( book );
							this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Good luck with the search." ) ); 
							CharacterDatabase.SetKeys( from, "Antiques", true );
							dropped.Delete();
						}
					}
					else
					{
						sMessage = "You need to discover the nine lands before I share this with you.";
						from.AddToBackpack ( dropped );
					}
				}
				else
				{
					sMessage = "You look like you need this more than I do.";
					from.AddToBackpack ( dropped );
				}

				this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
			}
			else if ( dropped is MuseumBook )
			{
				MuseumBook book = (MuseumBook)dropped;
				string sMessage = "";
				if ( book.ArtOwner != from )
				{
					sMessage = "This book doesn't belong to you so I will just get rid of it.";
					bool remove = true;
					foreach ( Account a in Accounts.GetAccounts() )
					{
						if (a == null)
							break;

						int index = 0;

						for (int i = 0; i < a.Length; ++i)
						{
							Mobile m = a[i];

							if (m == null)
								continue;

							if ( m == book.ArtOwner )
							{
								m.AddToBackpack( dropped );
								remove = false;
							}

							++index;
						}
					}
					if ( remove )
					{
						dropped.Delete();
					}
				}
				else if ( MuseumBook.GetNext( book ) > 60 )
				{
					CharacterDatabase.SetKeys( from, "Museums", true );
					from.SendSound( 0x3D );
					from.AddToBackpack ( new BankCheck( MuseumBook.QuestValue() ) );
					sMessage = "You have done the museum a great service. Here is " + MuseumBook.QuestValue() + " gold we promised.";
					from.Fame = 15000;
					from.SendMessage( "You have gained a really large amount of fame." );
					dropped.Delete();
				}
				else
				{
					sMessage = "You have not finished your search yet.";
				}
				this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
			}

			return base.OnDragDrop( from, dropped );
		}

		public static bool AlreadyHasBook( Mobile from ) /////////////////////////////////////////////////////////////////////////////////////////////
		{
			bool HasBook = false;

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is MuseumBook )
				{
					MuseumBook book = (MuseumBook)item;
					if ( book.ArtOwner == from )
						targets.Add( item );
				}
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];
				from.AddToBackpack( item );
				HasBook = true;
			}

			return HasBook;
		}

		///////////////////////////////////////////////////////////////////////////

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBVarietyDealer() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.WizardsHat( Utility.RandomRedHue() ) );
			AddItem( new Server.Items.Robe( Utility.RandomRedHue() ) );
		}

		public VarietyDealer( Serial serial ) : base( serial )
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
	}
}