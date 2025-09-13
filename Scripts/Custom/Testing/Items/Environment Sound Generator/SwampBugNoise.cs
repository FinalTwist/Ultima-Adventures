using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class SwampBugNoise : Item
	{
		[Constructable]
		public SwampBugNoise(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public SwampBugNoise(Direction direction, bool small) : base(3144)
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
			SwampBugNoiseTimer.SwampBugNoiseList.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 959);
		}

		public SwampBugNoise(Serial serial) : base(serial)
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

			SwampBugNoiseTimer.SwampBugNoiseList.Add(this);
		}
	}

	public class SwampBugNoiseTimer : Timer 
	{
		public const bool Enabled = true;
		public static List<SwampBugNoise> SwampBugNoiseList = new List<SwampBugNoise>();

		public static void Initialize() 
		{
			if (Enabled)
				new SwampBugNoiseTimer().Start();
		}

		public SwampBugNoiseTimer() : base(TimeSpan.FromSeconds( 3.00 ), TimeSpan.FromSeconds( 3.00 ) ) 
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick() 
		{
			foreach (SwampBugNoise swampbugnoise in SwampBugNoiseList)
				swampbugnoise.OnTick();
		}
	}
}