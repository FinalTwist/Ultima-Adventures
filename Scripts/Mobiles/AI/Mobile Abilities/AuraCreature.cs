// Created by Peoharen
using System;
using Server.Mobiles;
using Server.Spells;
//using Server.Spells.BlueMagic;
using Server.Targeting;

namespace Server.Mobiles
{
	public class AuraCreature : BaseCreature
	{
		public DateTime m_AuraDelay;
		private int m_MinAuraDelay;
		private int m_MaxAuraDelay;
		private int m_MinAuraDamage;
		private int m_MaxAuraDamage;
		private int m_AuraRange;
		private ResistanceType m_AuraType = ResistanceType.Physical;
		private Poison m_AuraPoison = null;
		private string m_AuraMessage = "";

		#region publicprops
		[CommandProperty( AccessLevel.GameMaster )]
		public int MinAuraDelay
		{
			get { return m_MinAuraDelay; }
			set { m_MinAuraDelay = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxAuraDelay
		{
			get { return m_MaxAuraDelay; }
			set { m_MaxAuraDelay = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MinAuraDamage
		{
			get { return m_MinAuraDamage; }
			set { m_MinAuraDamage = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxAuraDamage
		{
			get { return m_MaxAuraDamage; }
			set { m_MaxAuraDamage = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int AuraRange
		{
			get { return m_AuraRange; }
			set { m_AuraRange = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ResistanceType AuraType
		{
			get { return m_AuraType; }
			set { m_AuraType = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Poison AuraPoison
		{
			get { return m_AuraPoison; }
			set { m_AuraPoison = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string AuraMessage
		{
			get { return m_AuraMessage; }
			set { m_AuraMessage = value; }
		}
		#endregion

		public AuraCreature( AIType aitype, FightMode fightmode, int spot, int meleerange, double passivespeed, double activespeed ) : base( aitype, fightmode, spot, meleerange, passivespeed, activespeed )
		{
			m_AuraDelay = DateTime.UtcNow;
			/*
			Default is ?
			MinAuraDelay = 5;
			MaxAuraDelay = 15;
			MinAuraDamage = 15;
			MaxAuraDamage = 25;
			AuraRange = 3;
			*/
		}

		public override void OnThink()
		{
			if ( !this.Alive || this.Deleted || this == null )
				return;

			if ( DateTime.UtcNow > m_AuraDelay )
			{
				DebugSay( "Auraing" );
				Ability.Aura( this, m_MinAuraDamage, m_MaxAuraDamage, m_AuraType, m_AuraRange, m_AuraPoison, m_AuraMessage );

				m_AuraDelay = DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( m_MinAuraDelay, m_MaxAuraDelay ) );
			}

			base.OnThink();
		}

		public AuraCreature( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);
			writer.Write( (int)0 );
			writer.Write( m_MinAuraDelay );
			writer.Write( m_MaxAuraDelay );
			writer.Write( m_MinAuraDamage );
			writer.Write( m_MaxAuraDamage );
			writer.Write( m_AuraRange );
			writer.Write( (int)m_AuraType );
			Poison.Serialize( m_AuraPoison, writer );
			writer.Write( m_AuraMessage );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_MinAuraDelay = reader.ReadInt();
			m_MaxAuraDelay = reader.ReadInt();
			m_MinAuraDamage = reader.ReadInt();
			m_MaxAuraDamage = reader.ReadInt();
			m_AuraRange = reader.ReadInt();
			m_AuraType = (ResistanceType)reader.ReadInt();
			m_AuraPoison = Poison.Deserialize( reader );
			m_AuraMessage = reader.ReadString();
			m_AuraDelay = DateTime.UtcNow;
		}
	}
}