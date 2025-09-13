using System;

namespace Server.Items
{
	public class crushingtarget : Item
	{
		public Timer	m_Timer;

		[Constructable]
		public crushingtarget() : base(0x9D)
		{
			Name = "a log post"; 
			Weight = 1.0;
			Movable = false;
			m_Timer = new AutokillTimer(this);
			m_Timer.Start();
		}

		public crushingtarget(Serial serial) : base(serial)
		{
			m_Timer = new AutokillTimer(this);
			m_Timer.Start();
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		private class AutokillTimer : Timer
		{
			private Item m_crush;

			public AutokillTimer( Item crush ) : base( TimeSpan.FromMinutes(30.0) ) // <--------- Respawn time
			{
			m_crush = crush;
			}

			protected override void OnTick()
			{
				m_crush.Delete();
				Stop();
			}
		}

	}
}