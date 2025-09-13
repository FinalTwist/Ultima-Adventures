using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class MonsterNestLoot : Item
	{
		private int m_LootLevel;
		[Constructable]
		public MonsterNestLoot(int itemid, int hue, int lootlevel, string name) : base()
		{
			Name = name + " (double click to loot)".ToString();
			Hue = hue;
			ItemID = itemid;
			Movable = false;
			m_LootLevel = lootlevel;
		}

		public override void OnDoubleClick( Mobile from )
		{
			ArrayList alist = new ArrayList();
			IPooledEnumerable eable = this.Map.GetMobilesInRange( this.Location, 20 );
			foreach( Mobile m in eable )
				alist.Add( m );
			eable.Free();
			if ( alist.Count > 0 )
			{
				for( int i = 0; i < alist.Count; i++ )
				{
					Mobile m = (Mobile)alist[i];
					if ( m is PlayerMobile )
						AddLoot( m );
				}
			}

			this.Delete();
		}

		public void AddLoot( Mobile m )
		{
			int chance = Utility.Random( 1, 18 ) * m_LootLevel;
			if ( chance < 10 )
				m.AddToBackpack( new Gold( Utility.Random( 200, 500 )));
			else if ( chance < 20 )
				m.AddToBackpack( new Gold( Utility.Random( 500, 750 )));
			else if ( chance < 30 )
			{
				m.AddToBackpack( new Gold( Utility.Random( 750, 1000 )));
			}
			else if ( chance < 40 )
			{
				m.AddToBackpack( new Gold( Utility.Random( 1000, 1500 )));
			}
			else if ( chance < 55 )
			{
				m.AddToBackpack( new Gold( Utility.Random( 1500, 2000 )));
			}
			else if ( chance < 65 )
			{
				m.AddToBackpack( new Gold( Utility.Random( 2000, 2500 )));
			}
			else if ( chance < 70 )
			{
				m.AddToBackpack( new Gold( Utility.Random( 3000, 3500 )));
			}
			else if ( chance < 85 )
			{
				m.AddToBackpack( new Gold( Utility.Random( 3500, 5000 )));
			}
			else if ( chance < 95 )
			{
				m.AddToBackpack( new BankCheck( Utility.Random( 5000, 7500 )));
			}
			else
			{
				m.AddToBackpack( new BankCheck( Utility.Random( 7500, 10000 )));
			}
			Server.Misc.Titles.AwardFame( m, (m_LootLevel*50), true );
		}

		public MonsterNestLoot( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_LootLevel );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_LootLevel = reader.ReadInt();
		}

		private class RegenTimer : Timer
		{
			private MonsterNest nest;
			public RegenTimer( MonsterNest n ) : base( TimeSpan.FromSeconds( 30.0 ))
			{
				nest = n;
			}
			protected override void OnTick()
			{
				if ( nest != null && !nest.Deleted )
				{
					if ( nest.Hits < 0 )
					{
						nest.Destroy();
						return;
					}
					nest.Hits += nest.HitsMax / 10;
					if ( nest.Hits > nest.HitsMax )
						nest.Hits = nest.HitsMax;
					new RegenTimer( nest ).Start();
				}
			}
		}

		private class SpawnTimer : Timer
		{
			private MonsterNest nest;
			public SpawnTimer( MonsterNest n ) : base( n.RespawnTime )
			{
				nest= n;
			}
			protected override void OnTick()
			{
				if ( nest != null && !nest.Deleted )
				{
					nest.DoSpawn();
					new SpawnTimer( nest ).Start();
				}
			}
		}
	}
}