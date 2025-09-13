using System;
using Server;
using Server.Mobiles;
using System.Collections;
using Server.Gumps;

namespace Server.Items
{
	public class Sounder : Item
	{
		private bool m_Active = false; // default starting settings
		private string m_Desc = null; 
		private int m_Range = 10; 
		private bool m_Random = true; 
		private double m_Chance = 1; 
		private int m_MinPause = 10; 
		private int m_MaxPause = 20; 
		private string m_Creature = null; 
		private int m_CreatureRange = 6; 
		private bool m_CreaturePlays = false;

		private DateTime m_NextPlay; 
		private int m_lastIndex; 
		private int m_validCount; 

		private int[] m_Sounds = new int[10]; 

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get { return m_Active; }
			set { m_Active = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public String Description
		{
			get { return m_Desc; }
			set { m_Desc = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PlayerRange
		{
			get { return m_Range; }
			set 
			{ 
				if ( value < 0 ) 
					m_Range = 0;
				else if ( value > 20 ) 
					m_Range = 20;
				else 
					m_Range = value; 
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Random
		{
			get { return m_Random; }
			set { m_Random = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Chance
		{
			get { return (int)( m_Chance * 100 ); }
			set 
			{
				int num = value;

				if ( num > 100 ) 
					num = 100;
				else if ( num < 1 )
				{
					m_Chance = 0;
					m_Active = false;
					return;
				}

				m_Chance = (double)num / 100 ; 
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MinPause
		{
			get { return m_MinPause; }
			set 
			{ 
				if ( value > m_MaxPause )
					m_MinPause = m_MaxPause;
				else
                    m_MinPause = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxPause
		{
			get { return m_MaxPause; }
			set 
			{ 
				if ( value < m_MinPause )
					m_MaxPause = m_MinPause;
				else
					m_MaxPause = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public String Creature
		{
			get { return m_Creature; }
			set 
			{ 
				string str = value;

				if ( str != null )
				{
					str = str.ToLower();
					str = str.Trim();

					Type type = SpawnerType.GetType( str );

					if ( type != null )
						m_Creature = str;
					else 
						m_Creature = null;
				}
				else 
					m_Creature = null;
			} 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CreatureRange
		{
			get { return m_CreatureRange; }
			set 
			{ 
				if ( value < 0 ) 
					m_CreatureRange = 0;
				else if ( value > 20 ) 
					m_CreatureRange = 20;
				else 
					m_CreatureRange = value; 
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CreaturePlaysSnd
		{
			get { return m_CreaturePlays; }
			set { m_CreaturePlays = value; }
		}

		public int[] Sounds
		{
			get { return m_Sounds; }
			set 
			{ 
				m_Sounds = Compact( value );

				m_validCount = 0;
				for ( int i = 0;  i < 10; i++ )
				{
					if ( m_Sounds[i] != -1 )
						m_validCount++;
				}

				if ( m_validCount == 0 )
					m_Active = false;

				m_lastIndex = m_validCount - 1;
			}
		}

		static int[] Compact( int[] array )
		{
			int cnt;
			bool swapped = true;

			cnt = array.Length;
			if ( cnt > 1 )
			{
				int tmp;
				while ( swapped )
				{
					swapped = false;

					for ( int i = 0;  i < (cnt - 1); i++ )
					{
						if ( array[i] == -1 && array[i + 1] != -1 )
						{
							//Console.WriteLine( "Swapping {0},{1} at {2}", array[i], array[i + 1], i );

							tmp = array[i + 1];
							array[i + 1] = array[i];
							array[i] = tmp;

							swapped = true;
						}
					}
				}
			}

			return array;
		}

		[Constructable]
		public Sounder() : base( 0x13A8 )
		{
			Visible = false;
			Name = "Sound Maker";
			m_NextPlay = DateTime.UtcNow;

			for ( int i = 0;  i < 10; i++ )
				m_Sounds[i] = -1;

			InvalidateProperties();
		}

		public override bool HandlesOnMovement{ get{ return true; } }
		public override void OnMovement( Mobile m, Point3D oldLocation ) 
		{                                                    
			if( m_Active && m.Player && !m.Hidden && DateTime.UtcNow > m_NextPlay && m.InRange( this, m_Range ) ) 
			{ 
				if ( Utility.RandomDouble() < m_Chance ) 
				{                
					// Test for Creature...
					if ( m_Creature != null )
					{
						try
						{
							foreach ( Mobile creature in GetMobilesInRange( m_CreatureRange ) )
							{
								if ( Insensitive.Equals( m_Creature, creature.GetType().Name ) )
								{
									m_NextPlay = (DateTime.UtcNow).AddSeconds( Utility.RandomMinMax( m_MinPause, m_MaxPause ) );

									if ( m_CreaturePlays && creature.Alive )
										Effects.PlaySound( creature.Location, creature.Map, GetNextSound() );
									else
										Effects.PlaySound( this.Location, this.Map, GetNextSound() );

									return;
								}
							}
						}
						catch
						{
							Console.WriteLine( String.Format( "Exception caught in Sounder at {0},(1),{2) while checking for {3}.", X, Y, Z, m_Creature ) );
						}
					}
					else
					{
						m_NextPlay = (DateTime.UtcNow).AddSeconds( Utility.RandomMinMax( m_MinPause, m_MaxPause ) );
						Effects.PlaySound( Location, Map, GetNextSound() );
					}
				} 
			} 
		} 

		private int GetNextSound()
		{ 
			if ( m_Random )
			{
				return m_Sounds[Utility.Random( m_validCount )];
			}
			else
			{
				if ( ++m_lastIndex >= m_validCount )
					m_lastIndex = 0;

				return m_Sounds[m_lastIndex];
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( this.Name );

			if ( m_Active )
				list.Add( 1060742 ); // active
			else
				list.Add( 1060743 ); // inactive

			if ( m_Desc != null && m_Desc.Length > 0 )
				list.Add( 1042971, m_Desc );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( m_Active )
			{
				LabelTo( from, m_Desc );
			}
			else
			{
				LabelTo( from, "(inactive)" );
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel < AccessLevel.GameMaster )
				return;

			SounderGump g = new SounderGump( from, this );
			from.SendGump( g );
		}

		public Sounder( Serial serial ) : base( serial )
		{
			m_NextPlay = DateTime.UtcNow;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( m_CreaturePlays );

			writer.Write( m_Active );
			writer.Write( m_Desc );
			writer.Write( m_Range );
			writer.Write( m_Random );
			writer.Write( m_Chance );
			writer.Write( m_MinPause );
			writer.Write( m_MaxPause );
			writer.Write( m_Creature );
			writer.Write( m_CreatureRange );
			
			for ( int i = 0; i < m_Sounds.Length; ++i )
				writer.Write( m_Sounds[i] );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_CreaturePlays = reader.ReadBool();
					goto case 0;
				}
				case 0:
				{
					m_Active = reader.ReadBool();
					m_Desc = reader.ReadString();
					m_Range = reader.ReadInt();
					m_Random = reader.ReadBool();
					m_Chance = reader.ReadDouble();
					m_MinPause = reader.ReadInt();
					m_MaxPause = reader.ReadInt();
					m_Creature = reader.ReadString();
					m_CreatureRange = reader.ReadInt();

					for ( int i = 0; i < m_Sounds.Length; ++i ) 
						m_Sounds[i] = reader.ReadInt();
					this.Sounds = m_Sounds; // required for initialization on World Load

					break;
				}
			}
		}
	}
}