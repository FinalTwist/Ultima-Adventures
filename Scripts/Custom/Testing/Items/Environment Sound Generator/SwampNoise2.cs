using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class SwampNoise2 : Item
	{
		[Constructable]
		public SwampNoise2(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public SwampNoise2(Direction direction, bool small) : base(3144)
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
			SwampNoise2Timer.SwampNoise2List.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x00F);
		}

		public SwampNoise2(Serial serial) : base(serial)
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

			SwampNoise2Timer.SwampNoise2List.Add(this);
		}
	}

	public class SwampNoise2Timer : Timer 
	{
		public const bool Enabled = true;
		public static List<SwampNoise2> SwampNoise2List = new List<SwampNoise2>();

		public static void Initialize() 
		{
			if (Enabled)
				new SwampNoise2Timer().Start();
		}

		public SwampNoise2Timer() : base(TimeSpan.FromSeconds( 3.00 ), TimeSpan.FromSeconds( 3.00 ) ) 
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick() 
		{
			foreach (SwampNoise2 swampnoise2 in SwampNoise2List)
				swampnoise2.OnTick();
		}
	}
}