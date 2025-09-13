using System;
using Server;

namespace Server.Items
{
	public enum HeadType
	{
		Regular,
		Duel,
		Tournament
	}

	public class Head : Item
	{
		private string m_PlayerName;
		public string m_Job;
		private HeadType m_HeadType;

		public Mobile m_killer;

		[CommandProperty( AccessLevel.GameMaster )]
		public string PlayerName
		{
			get { return m_PlayerName; }
			set { m_PlayerName = value; }
		}


		private int m_midrace;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midrace
		{
			get { return m_midrace; }
			set { m_midrace = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Job
		{
			get { return m_Job; }
			set { m_Job = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public HeadType HeadType
		{
			get { return m_HeadType; }
			set { m_HeadType = value; }
		}

		public override string DefaultName
		{
			get
			{
				if ( m_PlayerName == null )
					return base.DefaultName;

				switch ( m_HeadType )
				{
					default:
						return String.Format( "the head of {0}", m_PlayerName );

					case HeadType.Duel:
						return String.Format( "the head of {0}, taken in a duel", m_PlayerName );

					case HeadType.Tournament:
						return String.Format( "the head of {0}, taken in a tournament", m_PlayerName );
				}
			}
		}

		[Constructable]
		public Head() : this( null )
		{
		}

		[Constructable]
		public Head( string playerName ) : this( HeadType.Regular, playerName, 0 )
		{
		}

		[Constructable]
		public Head( HeadType headType, string playerName , int race) : base( 0x1DA0 )
		{
			m_HeadType = headType;
			m_PlayerName = playerName;
			m_midrace = race;
		}

		public Head( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( m_Job != "" && m_Job != null ){ list.Add( 1070722, "" + m_Job + ""); }
        }

		// WIZARD ADDED THIS SO YOU CAN HAVE A CHOICE OF HEAD //
		public override void OnDoubleClick( Mobile from )
		{
			if ( this.ItemID == 7584 ){ this.ItemID = 7393; }
			else if ( this.ItemID == 0x1AE0 ){ this.ItemID = 0x1AE1; }
			else if ( this.ItemID == 0x1AE1 ){ this.ItemID = 0x1AE2; }
			else if ( this.ItemID == 0x1AE2 ){ this.ItemID = 0x1AE3; }
			else if ( this.ItemID == 0x1AE3 ){ this.ItemID = 0x1AE0; }
			else if ( this.ItemID == 7584 ){ this.ItemID = 7393; }
			else { this.ItemID = 7584; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( (Mobile) m_killer );
			writer.Write( (string) m_PlayerName );
			writer.Write( (string) m_Job );
			writer.WriteEncodedInt( (int) m_HeadType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
					m_killer = reader.ReadMobile();
					goto case 1;
				case 1:
					m_PlayerName = reader.ReadString();
					m_Job = reader.ReadString();
					m_HeadType = (HeadType) reader.ReadEncodedInt();
					break;

				case 0:
					string format = this.Name;

					if ( format != null )
					{
						if ( format.StartsWith( "the head of " ) )
							format = format.Substring( "the head of ".Length );

						if ( format.EndsWith( ", taken in a duel" ) )
						{
							format = format.Substring( 0, format.Length - ", taken in a duel".Length );
							m_HeadType = HeadType.Duel;
						}
						else if ( format.EndsWith( ", taken in a tournament" ) )
						{
							format = format.Substring( 0, format.Length - ", taken in a tournament".Length );
							m_HeadType = HeadType.Tournament;
						}
					}

					m_PlayerName = format;
					m_Job = reader.ReadString();
					this.Name = null;

					break;
			}
		}
	}
}