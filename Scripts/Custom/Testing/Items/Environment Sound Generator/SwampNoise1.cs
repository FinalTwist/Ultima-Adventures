using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class SwampNoise1 : Item
	{
		[Constructable]
		public SwampNoise1(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public SwampNoise1(Direction direction, bool small) : base(3144)
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
			SwampNoise1Timer.SwampNoise1List.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x00E);
		}

		public SwampNoise1(Serial serial) : base(serial)
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

			SwampNoise1Timer.SwampNoise1List.Add(this);
		}
	}

	public class SwampNoise1Timer : Timer 
	{
		public const bool Enabled = true;
		public static List<SwampNoise1> SwampNoise1List = new List<SwampNoise1>();

		public static void Initialize() 
		{
			if (Enabled)
				new SwampNoise1Timer().Start();
		}

		public SwampNoise1Timer() : base(TimeSpan.FromSeconds( 3.00 ), TimeSpan.FromSeconds( 3.00 ) ) 
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick() 
		{
			foreach (SwampNoise1 swampnoise1 in SwampNoise1List)
				swampnoise1.OnTick();
		}
	}
}