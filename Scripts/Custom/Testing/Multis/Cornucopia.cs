using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Gumps;
using Server.Multis;
using Server.ContextMenus;

namespace Server.Items
{
	[FlipableAttribute( 0x46A0, 0x46A1 )]
	public class Cornucopia  : Item, ISecurable
	{

		private int m_Food;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Food
		{
			get{ return m_Food; }
			set{ m_Food = value; InvalidateProperties(); }
		}

		private Timer m_Timer;
		
		private SecureLevel m_Level;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level { get { return m_Level; } set { m_Level = value; } }

		[Constructable]
		public Cornucopia () : base(0x46A0)
		{
			LootType = LootType.Blessed;

			m_Timer = Timer.DelayCall( TimeSpan.FromDays( 1 ), TimeSpan.FromDays( 1 ), new TimerCallback( GiveFood ) );
		}
		
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public Cornucopia ( Serial serial ) : base( serial )
		{
		}

		private void GiveFood()
		{
			m_Food = Math.Min( 100, m_Food + 10 );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
			
			else if ( m_Food > 0 )
			{
				Item Food = null;

				switch ( Utility.Random( 13 ) )
				{
					case 0: Food = new EarOfCorn(); break;
					case 1: Food = new Watermelon(); break;
					case 2: Food = new SlabOfBacon(); break;
					case 3: Food = new CookedBird(); break;
					case 4: Food = new SplitCoconut(); break;
					case 5: Food = new FrenchBread(); break;
					case 6: Food = new Squash(); break;
					case 7: Food = new Turnip(); break;
					case 8: Food = new Ham(); break;
					case 9: Food = new CheeseWheel(); break;
					case 10: Food = new Onion(); break;
					case 11: Food = new Carrot(); break;
					case 12: Food = new Banana(); break;

				}

				int amount = Math.Min( 10, m_Food );
				Food.Amount = amount;

				if ( !from.PlaceInBackpack( Food ) )
				{
					Food.Delete();
					from.SendLocalizedMessage( 1078837 ); // Your backpack is full! Please make room and try again.
				}
				else
				{
					m_Food -= amount;
					PublicOverheadMessage( MessageType.Regular, 0x3B2, 1114114 ); // You take some food from the cornucopia.
				}
			}
			else
				from.SendMessage( "There is no more food available." ); // There are no more Food available.
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.Write( (int) m_Food );

			if ( m_Timer != null )
				writer.Write( (DateTime) m_Timer.Next );
			else
				writer.Write( (DateTime) DateTime.UtcNow + TimeSpan.FromDays( 1 ) );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Food = reader.ReadInt();

			DateTime next = reader.ReadDateTime();

			if ( next < DateTime.UtcNow )
				next = DateTime.UtcNow;

			m_Timer = Timer.DelayCall( next - DateTime.UtcNow, TimeSpan.FromDays( 1 ), new TimerCallback( GiveFood ) );
		}
	}

}