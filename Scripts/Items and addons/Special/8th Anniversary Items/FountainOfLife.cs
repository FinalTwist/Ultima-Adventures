using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Multis;

namespace Server.Items
{
	public class EnhancedBandage : Bandage
	{
		public static int HealingBonus { get { return 10; } }

		[Constructable]
		public EnhancedBandage()
			: this( 1 )
		{
		}

		[Constructable]
		public EnhancedBandage( int amount )
			: base( amount )
		{
			Hue = 0x8A5;
		}

		public EnhancedBandage( Serial serial )
			: base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			return false;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1075216 ); // these bandages have been enhanced
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	[FlipableAttribute( 0x2AC0, 0x2AC3 )]
	public class FountainOfLife : BaseAddonContainer
	{
		public override BaseAddonContainerDeed Deed
		{
			get { return new FountainOfLifeDeed( m_Charges ); }
		}

		public override bool OnDragLift( Mobile from )
		{
			return false;
		}

		public virtual TimeSpan RechargeTime { get { return TimeSpan.FromDays( 1 ); } }

		public override int LabelNumber { get { return 1075197; } } // Fountain of Life
		public override int DefaultGumpID { get { return 0x11A; } } // 0x484
		public override int DefaultDropSound { get { return 66; } }
		public override int DefaultMaxItems { get { return 125; } }

		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get { return m_Charges; }
			set { m_Charges = Math.Min( value, 10 ); InvalidateProperties(); }
		}

		private Timer m_Timer;

		[Constructable]
		public FountainOfLife()
			: this( 10 )
		{
		}

		[Constructable]
		public FountainOfLife( int charges )
			: base( 0x2AC0 )
		{
			m_Charges = charges;

			m_Timer = Timer.DelayCall( RechargeTime, RechargeTime, new TimerCallback( Recharge ) );
		}

		public FountainOfLife( Serial serial )
			: base( serial )
		{
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if( dropped is Bandage )
			{
				bool allow = base.OnDragDrop( from, dropped );

				if( allow )
					Enhance();

				return allow;
			}
			else
			{
				from.SendLocalizedMessage( 1075209 ); // Only bandages may be dropped into the fountain.
				return false;
			}
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			if( item is Bandage )
			{
				bool allow = base.OnDragDropInto( from, item, p );

				if( allow )
					Enhance();

				return allow;
			}
			else
			{
				from.SendLocalizedMessage( 1075209 ); // Only bandages may be dropped into the fountain.
				return false;
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1075217, m_Charges.ToString() ); // ~1_val~ charges remaining
		}

		public override void OnDelete()
		{
			if( m_Timer != null )
				m_Timer.Stop();

			base.OnDelete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version

			writer.Write( m_Charges );
			writer.Write( (DateTime)m_Timer.Next );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Charges = reader.ReadInt();

			DateTime next = reader.ReadDateTime();

			if( next < DateTime.UtcNow )
				m_Timer = Timer.DelayCall( TimeSpan.Zero, RechargeTime, new TimerCallback( Recharge ) );
			else
				m_Timer = Timer.DelayCall( next - DateTime.UtcNow, RechargeTime, new TimerCallback( Recharge ) );
		}

		public void Recharge()
		{
			m_Charges = 10;

			Enhance();
		}

		public void Enhance()
		{
			for( int i = Items.Count - 1; i >= 0 && m_Charges > 0; i-- )
			{
				Bandage bandage = Items[ i ] as Bandage;

				if( bandage != null )
				{
					if( bandage.Amount > m_Charges )
					{
						bandage.Amount -= m_Charges;
						DropItem( new EnhancedBandage( m_Charges ) );
						m_Charges = 0;
					}
					else
					{
						DropItem( new EnhancedBandage( bandage.Amount ) );
						m_Charges -= bandage.Amount;
						bandage.Delete();
					}

					InvalidateProperties();
				}
			}
		}
	}

	public class FountainOfLifeDeed : BaseAddonContainerDeed
	{
		public override int LabelNumber { get { return 1075197; } } // Fountain of Life
		public override BaseAddonContainer Addon { get { return new FountainOfLife( m_Charges ); } }

		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get { return m_Charges; }
			set { m_Charges = Math.Min( value, 10 ); InvalidateProperties(); }
		}

		[Constructable]
		public FountainOfLifeDeed()
			: this( 10 )
		{
		}

		[Constructable]
		public FountainOfLifeDeed( int charges )
			: base()
		{
			LootType = LootType.Blessed;
			m_Charges = charges;
		}

		public FountainOfLifeDeed( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version

			writer.Write( m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Charges = reader.ReadInt();
		}
	}
}
