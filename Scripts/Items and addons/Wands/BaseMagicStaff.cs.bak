using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Items
{
	public enum MagicStaffEffect
	{
		Charges
	}

	public abstract class BaseMagicStaff : BaseBashing
	{
		public override int AosStrengthReq { get { return 5; } }
		public override int AosMinDamage { get { return 7; } }
		public override int AosMaxDamage { get { return 9; } }
		public override int AosSpeed { get { return 40; } }

		public override int InitMinHits { get { return 50; } }
		public override int InitMaxHits { get { return 50; } }

		public override float MlSpeed{ get{ return 2.00f; } }

		private MagicStaffEffect m_MagicStaffEffect;
		private int m_Charges;

		public virtual TimeSpan GetUseDelay{ get{ return TimeSpan.FromSeconds( 4.0 ); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public MagicStaffEffect Effect
		{
			get{ return m_MagicStaffEffect; }
			set{ m_MagicStaffEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		public BaseMagicStaff( MagicStaffEffect effect, int minCharges, int maxCharges ) : base( Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ) )
		{
			Weight = 1.0;
			Effect = effect;
			Charges = Utility.RandomMinMax( minCharges, maxCharges );
			Attributes.SpellChanneling = 0;
			Resource = CraftResource.None;
		}

		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			if ( Charges == 0 )
			{
				from.SendLocalizedMessage( 1019073 ); // This item is out of charges.
			}
		}

		public BaseMagicStaff( Serial serial ) : base( serial )
		{
		}

		public virtual void ApplyDelayTo( Mobile from )
		{
			from.BeginAction( typeof( BaseMagicStaff ) );
			Timer.DelayCall( GetUseDelay, new TimerStateCallback( ReleaseMagicStaffLock_Callback ), from );
		}

		public virtual void ReleaseMagicStaffLock_Callback( object state )
		{
			((Mobile)state).EndAction( typeof( BaseMagicStaff ) );
		}

		public override bool OnEquip( Mobile from )
		{
			this.Attributes.SpellChanneling = 0;
			return base.OnEquip( from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			this.Attributes.SpellChanneling = 0;

			if ( !from.CanBeginAction( typeof( BaseMagicStaff ) ) )
				return;

			if ( Parent == from )
			{
				if ( Charges > 0 )
				{
					OnMagicStaffUse( from );
				}
			}
			else
			{
				from.SendLocalizedMessage( 502641 ); // You must equip this item to use it.
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_MagicStaffEffect );
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_MagicStaffEffect = (MagicStaffEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();

					break;
				}
			}
			Attributes.SpellChanneling = 0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060741, m_Charges.ToString() );
		}

		public override void OnSingleClick( Mobile from )
		{
			ArrayList attrs = new ArrayList();

			if ( DisplayLootType )
			{
				if ( LootType == LootType.Blessed )
					attrs.Add( new EquipInfoAttribute( 1038021 ) ); // blessed
				else if ( LootType == LootType.Cursed )
					attrs.Add( new EquipInfoAttribute( 1049643 ) ); // cursed
			}

			if ( !Identified )
			{
				attrs.Add( new EquipInfoAttribute( 1038000 ) ); // Unidentified
			}
			else
			{
				int num = 0;
				num = 1011296;
				if ( num > 0 )
					attrs.Add( new EquipInfoAttribute( num, m_Charges ) );
			}

			int number;

			if ( Name == null )
			{
				number = 1017085;
			}
			else
			{
				this.LabelTo( from, Name );
				number = 1041000;
			}

			if ( attrs.Count == 0 && Crafter == null && Name != null )
				return;

			EquipmentInfo eqInfo = new EquipmentInfo( number, Crafter, false, (EquipInfoAttribute[])attrs.ToArray( typeof( EquipInfoAttribute ) ) );

			from.Send( new DisplayEquipmentInfo( this, eqInfo ) );
		}

		public void Cast( Spell spell )
		{
			bool m = Movable;

			Movable = false;
			spell.Cast();
			Movable = m;
		}

		public virtual void OnMagicStaffUse( Mobile from )
		{
			from.Target = new MagicStaffTarget( this );
		}

		public virtual void DoMagicStaffTarget( Mobile from, object o )
		{
			if ( Deleted || Charges <= 0 || Parent != from || o is StaticTarget || o is LandTarget )
				return;
		}

		public virtual bool OnMagicStaffTarget( Mobile from, object o )
		{
			return true;
		}
	}
}