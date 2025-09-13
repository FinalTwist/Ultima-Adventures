using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Engines.Craft;

namespace Server.Items
{
	public class TrapKit : Item
	{
		public string m_Metal;
		
		[CommandProperty(AccessLevel.Owner)]
		public string Metal { get { return m_Metal; } set { m_Metal = value; InvalidateProperties(); } }

		public int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		[Constructable]
		public TrapKit( ) : base( 0x1EBB )
		{
			if ( m_Metal == "" || m_Metal == null )
			{
				Hue = 0;
				m_Metal = "Iron";
				m_Charges = 25;
			}
			Weight = 5.0;
			Name = "trapping tools";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "These tools must be in your backpack to use." );
				return;
			}
			else if ( Charges > 0 )
			{
				int traps = 0;

				foreach ( Item m in from.GetItemsInRange( 10 ) )
				{
					if ( m is SetTrap )
						++traps;
				}

				if ( traps > 2 )
				{
					from.SendMessage( "There are too many traps in the area!" );
				}
				else if ( !from.Region.AllowHarmful( from, from ) )
				{
					from.SendMessage( "That doesn't feel like a good idea." ); 
					return;
				}
				else if ( from.Skills[SkillName.RemoveTrap].Value > 0 )
				{
					ConsumeCharge( from );

					int Power = (int)(from.Skills[SkillName.RemoveTrap].Value / 2) + 1;

					from.SendSound( 0x55 );

					if ( m_Metal == "Dull Copper" ){ Power = Power + 3; }
					else if ( m_Metal == "Shadow Iron" ){ Power = Power + 6; }
					else if ( m_Metal == "Copper" ){ Power = Power + 9; }
					else if ( m_Metal == "Bronze" ){ Power = Power + 12; }
					else if ( m_Metal == "Gold" ){ Power = Power + 15; }
					else if ( m_Metal == "Agapite" ){ Power = Power + 18; }
					else if ( m_Metal == "Verite" ){ Power = Power + 21; }
					else if ( m_Metal == "Valorite" ){ Power = Power + 24; }
					else if ( m_Metal == "Nepturite" ){ Power = Power + 27; }
					else if ( m_Metal == "Obsidian" ){ Power = Power + 30; }
					else if ( m_Metal == "Steel" ){ Power = Power + 33; }
					else if ( m_Metal == "Brass" ){ Power = Power + 36; }
					else if ( m_Metal == "Mithril" ){ Power = Power + 39; }
					else if ( m_Metal == "Xormite" ){ Power = Power + 42; }
					else if ( m_Metal == "Dwarven" ){ Power = Power + 78; }

					SetTrap trap = new SetTrap( from, Power ); 
					trap.Map = from.Map; 
					trap.Hue = this.Hue;
					trap.Location = from.Location;
				}
				else
				{
					from.SendMessage( "You cannot figure out how these tools work!" );
					return;
				}
			}
		}

		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			if ( Charges == 0 )
			{
				from.SendMessage( "These tools have been worn out." );
				this.Delete();
			}
		}

		public TrapKit( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, m_Metal );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060741, m_Charges.ToString() );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( m_Metal );
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            m_Metal = reader.ReadString();
			m_Charges = (int)reader.ReadInt();
		}
	}
}