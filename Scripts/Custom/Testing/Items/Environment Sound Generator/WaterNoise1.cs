using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class WaterNoise1 : Item
	{
		[Constructable]
		public WaterNoise1(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public WaterNoise1(Direction direction, bool small) : base(0x1FB7)
		{
			Movable = false;
			switch(direction)
			{
				case Direction.North:
				{
					if(small)
						ItemID = 0x1FA3;
					else
						ItemID = 0x1FB7;
					break;
				}
				case Direction.South:
				{
					if(small)
						ItemID = 0x1FB2;
					else
						ItemID = 0x1FC6;
					break;
				}
				case Direction.East:
				{
					if(small)
						ItemID = 0x1FAD;
					else
						ItemID = 0x1FC1;
					break;
				}
				case Direction.West:
				{
					if(small)
						ItemID = 0x1FA8;
					else
						ItemID = 0x1FBC;
					break;
				}
				default:
				{
					ItemID = 0x1FA3;
					break;
				}
			}
			WaterNoise1Timer.WaterNoise1List.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x010);
		}

		public WaterNoise1(Serial serial) : base(serial)
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

			WaterNoise1Timer.WaterNoise1List.Add(this);
		}
	}

	public class WaterNoise1Timer : Timer 
	{
		public const bool Enabled = true;
		public static List<WaterNoise1> WaterNoise1List = new List<WaterNoise1>();

		public static void Initialize() 
		{
			if (Enabled)
				new WaterNoise1Timer().Start();
		}

		public WaterNoise1Timer() : base(TimeSpan.FromSeconds( 3.0 ), TimeSpan.FromSeconds( 3.0 )) 
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick() 
		{
			foreach (WaterNoise1 waternoise1 in WaterNoise1List)
				waternoise1.OnTick();
		}
	}
}