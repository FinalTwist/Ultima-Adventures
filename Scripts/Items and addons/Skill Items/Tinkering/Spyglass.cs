using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Mobiles;
using Server.Regions;

namespace Server.Items
{
	public enum SpyglassEffect
	{
		Charges
	}

    public class Spyglass : Item
	{
		private SpyglassEffect m_SpyglassEffect;
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public SpyglassEffect Effect
		{
			get{ return m_SpyglassEffect; }
			set{ m_SpyglassEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

        [Constructable]
        public Spyglass() : base(0x14F5)
		{
            Name = "spyglass";
			Charges = 20;
			Weight = 1.0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060741, m_Charges.ToString() );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Aids One In Tracking");
        }

		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			if ( Charges == 0 )
			{
				from.SendMessage( "The lenses in this are too worn from time." );
				this.Delete();
			}
		}

		private static Hashtable m_Table = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( m_Table[m] != null );
		}
		
		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])m_Table[m];
			
			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}
			
			m_Table.Remove( m );
			m.EndAction( typeof( Spyglass ) );
			m.Hidden = false;
		}

        public override void OnDoubleClick( Mobile m )
		{
		    if ( !m.CanBeginAction( typeof( Spyglass ) ) )
		    {
				m.SendMessage( "You are already using the this." );
		    }
			else if (	!Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) ) && 
						!m.Region.IsPartOf( typeof( OutDoorRegion ) ) && 
						!m.Region.IsPartOf( typeof( OutDoorBadRegion ) ) && 
						!m.Region.IsPartOf( typeof( VillageRegion ) ) )
			{
				m.SendMessage( "You can only use this outdoors." ); 
				return;
			}
			else if ( Charges > 0 )
			{
				ConsumeCharge( m );

				object[] mods = new object[]
				{
					new DefaultSkillMod( SkillName.Tracking, true, 25 ),
				};

				m_Table[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( 2 ) ).Start();

				m.BeginAction( typeof( Spyglass ) );
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;
			
			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}
			
			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					Spyglass.RemoveEffect( m_m );
					Stop();
				}
			}
		}

        public Spyglass( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_SpyglassEffect );
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
					m_SpyglassEffect = (SpyglassEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();

					break;
				}
			}
	    }
    }
}