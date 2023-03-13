using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Items
{
	public enum MagicObjectEffect
	{
		Charges
	}

	public abstract class BaseMagicObject : BaseBashing
	{
		public override WeaponAbility PrimaryAbility { get { return WeaponAbility.Dismount; } }
		public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

		public override int AosStrengthReq { get { return 5; } }
		public override int AosMinDamage { get { return 7; } }
		public override int AosMaxDamage { get { return 9; } }
		public override int AosSpeed { get { return 40; } }

		public override int InitMinHits { get { return 31; } }
		public override int InitMaxHits { get { return 110; } }

		public override float MlSpeed{ get{ return 2.00f; } }

		private MagicObjectEffect m_MagicObjectEffect;
		private int m_Charges;

		public virtual TimeSpan GetUseDelay{ get{ return TimeSpan.FromSeconds( 4.0 ); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public MagicObjectEffect Effect
		{
			get{ return m_MagicObjectEffect; }
			set{ m_MagicObjectEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		public BaseMagicObject( MagicObjectEffect effect, int minCharges, int maxCharges ) : base( Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ) )
		{
			Weight = 1.0;
			Effect = effect;
			Charges = Utility.RandomMinMax( minCharges, maxCharges );
			Attributes.SpellChanneling = 1;
		}

		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			if ( Charges == 0 )
			{
				from.SendLocalizedMessage( 1019073 ); // This item is out of charges.
				this.Attributes.SpellChanneling = 0;
			}

			ApplyDelayTo( from );
		}

		public BaseMagicObject( Serial serial ) : base( serial )
		{
		}

		public virtual void ApplyDelayTo( Mobile from )
		{
			from.BeginAction( typeof( BaseMagicObject ) );
			Timer.DelayCall( GetUseDelay, new TimerStateCallback( ReleaseMagicObjectLock_Callback ), from );
		}

		public virtual void ReleaseMagicObjectLock_Callback( object state )
		{
			((Mobile)state).EndAction( typeof( BaseMagicObject ) );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.CanBeginAction( typeof( BaseMagicObject ) ) )
				return;

			if ( Parent == from )
			{
				if ( Charges > 0 )
				{
					OnMagicObjectUse( from );
					ConsumeCharge( from );
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
			writer.Write( (int) m_MagicObjectEffect );
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
					m_MagicObjectEffect = (MagicObjectEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();

					break;
				}
			}
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

		public virtual void OnMagicObjectUse( Mobile from )
		{
			from.Target = new MagicObjectTarget( this );
		}

		public virtual void DoMagicObjectTarget( Mobile from, object o )
		{
			if ( Deleted || Charges <= 0 || Parent != from || o is StaticTarget || o is LandTarget )
				return;
		}

		public virtual bool OnMagicObjectTarget( Mobile from, object o )
		{
			return true;
		}
	}
}