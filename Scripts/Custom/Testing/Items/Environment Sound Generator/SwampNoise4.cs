using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class SwampNoise4 : Item
	{
		[Constructable]
		public SwampNoise4(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public SwampNoise4(Direction direction, bool small) : base(3144)
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
			SwampNoise4Timer.SwampNoise4List.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x007);
		}

		public SwampNoise4(Serial serial) : base(serial)
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

			SwampNoise4Timer.SwampNoise4List.Add(this);
		}
	}

	public class SwampNoise4Timer : Timer 
	{
		public const bool Enabled = true;
		public static List<SwampNoise4> SwampNoise4List = new List<SwampNoise4>();

		public static void Initialize() 
		{
			if (Enabled)
				new SwampNoise4Timer().Start();
		}

		public SwampNoise4Timer() : base(TimeSpan.FromSeconds( 3.00 ), TimeSpan.FromSeconds( 3.00 ) ) 
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick() 
		{
			foreach (SwampNoise4 swampnoise4 in SwampNoise4List)
				swampnoise4.OnTick();
		}
	}
}