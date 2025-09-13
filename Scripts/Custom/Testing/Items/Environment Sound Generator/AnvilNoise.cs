using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class AnvilNoise : Item
	{
		[Constructable]
		public AnvilNoise(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public AnvilNoise(Direction direction, bool small) : base(3144)
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
			AnvilNoiseTimer.AnvilNoiseList.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x02A);
		}

		public AnvilNoise(Serial serial) : base(serial)
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

			AnvilNoiseTimer.AnvilNoiseList.Add(this);
		}
	}

	public class AnvilNoiseTimer : Timer 
	{
		public const bool Enabled = true;
		public static List<AnvilNoise> AnvilNoiseList = new List<AnvilNoise>();

		public static void Initialize() 
		{
			if (Enabled)
				new AnvilNoiseTimer().Start();
		}

		public AnvilNoiseTimer() : base(TimeSpan.FromSeconds( 3.00 ), TimeSpan.FromSeconds( 3.00 ) ) 
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick() 
		{
			foreach (AnvilNoise anvilnoise in AnvilNoiseList)
				anvilnoise.OnTick();
		}
	}
}