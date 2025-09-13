using System;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;

namespace Server.Items
{
	public class TrashBarrel : Container, IChopable
	{
		public override int LabelNumber{ get{ return 1041064; } } // a trash barrel

		public override int DefaultMaxWeight{ get{ return 0; } } // A value of 0 signals unlimited weight

		private static string[] trashAdj = { "fat", "ugly", "stupid", "clumsy", "hideous", "loose", "malodorous", "cheap", "poor", "unkempt", "frail", "square", "aweful", "ugly", "grotesque", "horrid", "unsightly", "repelling", "repungnant", "repelling", "not beautiful", "disfigured", "foul", "deformed", "dense", "witless", "crazy", "deranged"};
		private static string[] trashAction = { "fart", "defecate", "hug", "smile", "play with dolls", "eat shortcakes", "pick strawberries", "wipe yourself", "bend over", "tie your shoes", "look down at yourself", "unzip", "snort", "laugh", "walk", "hunger", "point", "look at anyone", "talk", "cook", "shop", "yell", "screeche", "yelp", "vociferate", "wobble", "move", "get undressed", "sleep", "are naked", "blow your nose" };
		private static string[] trashWho = { "everyone", "people", "children", "sailors", "daemons", "priests", "men", "women", "righteous foll", "folk", "peasants", "kings", "tamers", "mages", "beggars", "holy monks", "the gods", "jesters", "the blind", "the miserable", "the naked", "the flexible", "the poor", "dogs", "toads", "food", "chickens", "cows", "chairs", "barkeeps", "merchants" };
		private static string[] trashResult = { "run away", "come running", "lie down", "faint", "pass out", "get naked", "puke", "vomit", "retch", "spew everything out", "hurl their insides", "pay 100 gold", "gag", "have nightmares", "dream of it for months", "call the guards", "cover their ears", "close their eyes", "wish they were in hades", "wish they were dead", "stash their gold", "reconsider living", "get dumber", "become insane", "loose their sanity"};

		public override bool IsDecoContainer
		{
			get{ return false; }
		}

		[Constructable]
		public TrashBarrel() : base( 0xE77 )
		{
			Movable = false;
		}

		public TrashBarrel( Serial serial ) : base( serial )
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

			if ( Items.Count > 0 )
			{
				m_Timer = new EmptyTimer( this );
				m_Timer.Start();
			}
		}

		private void SendMessageTo( Mobile to, string text, int hue )
		{
			if ( Deleted || !to.CanSee( this ) )
				return;

			to.Send( new Network.UnicodeMessage( Serial, ItemID, Network.MessageType.Regular, hue, 3, "ENU", "", text ) );
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{

			if (dropped is TombStone || dropped is Corpse || dropped is CorpseItem)
				return false;

			if ( !base.OnDragDrop( from, dropped ) )
				return false;

			if ( !base.TryDropItem( from, dropped, false ) )
				return false;

			if ( TotalItems >= 50 )
			{
				Empty( 501478 ); // The trash is full!  Emptying!
			}
			else
			{
				
				 string trashadj = trashAdj[Utility.Random(trashAdj.Length)].Replace("HIMHER", from.Female ? "her" : "him").Replace("HISHER", from.Female ? "her" : "his");
				 string trashaction = trashAction[Utility.Random(trashAction.Length)].Replace("HIMHER", from.Female ? "her" : "him").Replace("HISHER", from.Female ? "her" : "his");
				 string trashwho = trashWho[Utility.Random(trashWho.Length)].Replace("HIMHER", from.Female ? "her" : "him").Replace("HISHER", from.Female ? "her" : "his");
				 string trashresult = trashResult[Utility.Random(trashResult.Length)].Replace("HIMHER", from.Female ? "her" : "him").Replace("HISHER", from.Female ? "her" : "his");
							
				SendMessageTo( from, String.Format("*You're so {0}, when you {1}, {2} {3}*",trashadj,trashaction,trashwho,trashresult), 1160 );
				//SendLocalizedMessageTo( from, 1010442 ); // The item will be deleted in three minutes

				if ( m_Timer != null )
					m_Timer.Stop();
				else
					m_Timer = new EmptyTimer( this );

				m_Timer.Start();
			}

			return true;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{

			if (item is TombStone || item is Corpse || item is CorpseItem)
				return false;
				
			if ( !base.OnDragDropInto( from, item, p ) )
				return false;

			if ( !base.TryDropItem( from, item, false ) )
				return false;

			if ( TotalItems >= 50 )
			{
				Empty( 501478 ); // The trash is full!  Emptying!
			}
			else
			{
				 string trashadj = trashAdj[Utility.Random(trashAdj.Length)].Replace("HIMHER", from.Female ? "her" : "him").Replace("HISHER", from.Female ? "her" : "his");
				 string trashaction = trashAction[Utility.Random(trashAction.Length)].Replace("HIMHER", from.Female ? "her" : "him").Replace("HISHER", from.Female ? "her" : "his");
				 string trashwho = trashWho[Utility.Random(trashWho.Length)].Replace("HIMHER", from.Female ? "her" : "him").Replace("HISHER", from.Female ? "her" : "his");
				 string trashresult = trashResult[Utility.Random(trashResult.Length)].Replace("HIMHER", from.Female ? "her" : "him").Replace("HISHER", from.Female ? "her" : "his");
							
				SendMessageTo( from, String.Format("*You're so {0}, when you {1}, {2} {3}*",trashadj,trashaction,trashwho,trashresult), 1161 );
				//SendLocalizedMessageTo( from, 1010442 ); // The item will be deleted in three minutes

				if ( m_Timer != null )
					m_Timer.Stop();
				else
					m_Timer = new EmptyTimer( this );

				m_Timer.Start();
			}

			return true;
		}

		public void OnChop( Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( from );

			if ( house != null && house.IsCoOwner( from ) )
			{
				Effects.PlaySound( Location, Map, 0x3B3 );
				from.SendLocalizedMessage( 500461 ); // You destroy the item.
				Destroy();
			}
		}

		public void Empty( int message )
		{
			List<Item> items = this.Items;

			if ( items.Count > 0 )
			{
				PublicOverheadMessage( Network.MessageType.Regular, 0x3B2, message, "" );

				for ( int i = items.Count - 1; i >= 0; --i )
				{
					if ( i >= items.Count )
						continue;

					items[i].Delete();
				}
			}

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}

		private Timer m_Timer;

		private class EmptyTimer : Timer
		{
			private TrashBarrel m_Barrel;

			public EmptyTimer( TrashBarrel barrel ) : base( TimeSpan.FromMinutes( 3.0 ) )
			{
				m_Barrel = barrel;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Barrel.Empty( 501479 ); // Emptying the trashcan!
			}
		}
	}
}
