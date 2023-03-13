using System;
using Server;
using Server.Spells;

namespace Server.Items
{
	public class SoulShard : Item
	{
		private int m_Charges;
		private int m_MaxCharges;
		private bool m_SuccessfuLCast;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges{ get{ return m_Charges; } set{ m_Charges = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxCharges{ get{ return m_MaxCharges; } set{ m_MaxCharges = value;  InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
		public bool SuccessfulCast{ get{ return m_SuccessfuLCast; } set{ m_SuccessfuLCast = value; InvalidateProperties(); } }
		
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061195; } } // soul shard	

		[Constructable]
		public SoulShard() : this( 1 )
		{
		}

		[Constructable]
		public SoulShard( int amount ) : base( 0x023E )
		{
			
			SuccessfulCast = false;
			Charges = 0;
			MaxCharges = 5;
			Stackable = false;
			Amount = amount;
			Light = LightType.Circle150;
		}

		public SoulShard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write(m_Charges);
			writer.Write(m_MaxCharges);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Light = LightType.Circle150;
			m_Charges = reader.ReadInt();
			m_MaxCharges = reader.ReadInt();
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1061199 ); //A soft untamed voice emanates from the shard
		}

		public bool HasCharges(Mobile from) {
			if (Charges == 0) {
				from.SendLocalizedMessage( 1061207 ); // This type of soul shard needs recharging
			}
			return Charges > 0;
		}

		public void RemoveCharge() {
			--Charges;
		}

		public void Cast( Spell spell )
		{
			bool m = Movable;

			Movable = false;
			spell.Cast();
			Movable = m;
		}
	}
}