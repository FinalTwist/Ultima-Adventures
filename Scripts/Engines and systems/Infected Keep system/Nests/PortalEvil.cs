using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.OneTime;

namespace Server.Items
{
	public class PortalEvil : MonsterNest, IOneTime
	{

		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

		private Mobile m_owner;
		[CommandProperty( AccessLevel.GameMaster )]
        public Mobile Owner
        {
            get{ return m_owner; }
            set{ m_owner = value; }
        }

		private bool m_sleep;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool Sleep
        {
            get{ return m_sleep; }
            set{ m_sleep = value; }
        }

		private int m_portaltick;
		[CommandProperty( AccessLevel.GameMaster )]
        public int portaltick
        {
            get{ return m_portaltick; }
            set{ m_portaltick = value; }
        }	

		private bool m_knockout;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool knockout
        {
            get{ return m_knockout; }
            set{ m_knockout = value; }
        }

		[Constructable]
		public PortalEvil(Mobile from) : base()
		{
			m_OneTimeType = 5;
			Name = "A Balance Shard (double click to attack)";
			Hue = 1793;
			MaxCount = 1;
			RespawnTime = TimeSpan.FromSeconds( 15.0 );
			HitsMax = 8500;
			Hits = 8500;
			NestSpawnType = "Praetor";
			ItemID = 0x3D5E;
			LootLevel = 0;
			RangeHome = 13;
			m_portaltick = 0;
			m_owner = from;
			m_sleep = false;
		}

		public override void AddLoot()
		{
			MonsterNestLoot loot = new MonsterNestLoot( 6927, 0, this.LootLevel, "Scattered Bones" );//+++
			loot.MoveToWorld( this.Location, this.Map );
		}

		public PortalEvil( Serial serial ) : base( serial )
		{
		}

        public void OneTimeTick()
        {
			if (!m_sleep)
			{
				if (m_knockout )
				{
					if ( Utility.RandomMinMax(1, 3) == 1 )
						m_knockout = false;
				}
				else if (m_portaltick <= 72)
					m_portaltick += 1;
				else 
				{
					m_portaltick = 0;
					
					if (this.MaxCount <= 4)
						this.MaxCount += Utility.RandomMinMax(1, 2);

					int check = this.HitsMax;
					if (this.HitsMax <= 60000)
						this.HitsMax += 12500;

					if (check == this.Hits)
						this.Hits = this.HitsMax;

					this.DoSpawn();

					this.InvalidateProperties();

				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( (int) m_portaltick );

			writer.Write( (Mobile) m_owner);
			writer.Write( (bool) m_sleep);

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_portaltick = reader.ReadInt();

			if (version >= 1)
			{
				m_owner = reader.ReadMobile();
				m_sleep = reader.ReadBool();
			}


			m_OneTimeType = 5;
		}
	}
}