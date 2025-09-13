using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class FireNoise : Item
	{
		[Constructable]
		public FireNoise(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public FireNoise(Direction direction, bool small) : base(3144)
		{
			Visible = false;
			Movable = false;

			switch(direction)
			{
				case Direction.North:
				{
					if(small)
						ItemID = 3144;
					else
						ItemID = 3144;
					break;
				}
				case Direction.South:
				{
					if(small)
						ItemID = 3144;
					else
						ItemID = 3144;
					break;
				}
				case Direction.East:
				{
					if(small)
						ItemID = 3144;
					else
						ItemID = 3144;
					break;
				}
				case Direction.West:
				{
					if(small)
						ItemID = 3144;
					else
						ItemID = 3144;
					break;
				}
				default:
				{
					ItemID = 0x1FA3;
					break;
				}
			}
			FireNoiseTimer.FireNoiseList.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x345);
		}

		public FireNoise(Serial serial) : base(serial)
		{
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

			FireNoiseTimer.FireNoiseList.Add(this);
		}
	}

	public class FireNoiseTimer : Timer 
	{
		public const bool Enabled = true;
		public static List<FireNoise> FireNoiseList = new List<FireNoise>();

		public static void Initialize() 
		{
			if (Enabled)
				new FireNoiseTimer().Start();
		}

		public FireNoiseTimer() : base(TimeSpan.FromSeconds( 0.0 ), TimeSpan.FromSeconds( 0.0 ) ) 
		{
			Priority = TimerPriority.TenMS;
		}

		protected override void OnTick() 
		{
			foreach (FireNoise firenoise in FireNoiseList)
				firenoise.OnTick();
		}
	}
}