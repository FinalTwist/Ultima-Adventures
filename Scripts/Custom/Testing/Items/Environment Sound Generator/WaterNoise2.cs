using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class WaterNoise2 : Item
	{
		[Constructable]
		public WaterNoise2(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public WaterNoise2(Direction direction, bool small) : base(0x1FB7)
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
			WaterNoise2Timer.WaterNoise2List.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x011);
		}

		public WaterNoise2(Serial serial) : base(serial)
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

			WaterNoise2Timer.WaterNoise2List.Add(this);
		}
	}

	public class WaterNoise2Timer : Timer 
	{
		public const bool Enabled = true;
		public static List<WaterNoise2> WaterNoise2List = new List<WaterNoise2>();

		public static void Initialize() 
		{
			if (Enabled)
				new WaterNoise2Timer().Start();
		}

		public WaterNoise2Timer() : base(TimeSpan.FromSeconds( 3.0 ), TimeSpan.FromSeconds( 3.0 )) 
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick() 
		{
			foreach (WaterNoise2 waternoise2 in WaterNoise2List)
				waternoise2.OnTick();
		}
	}
}