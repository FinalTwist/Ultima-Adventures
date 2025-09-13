using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class SwampNoise3 : Item
	{
		[Constructable]
		public SwampNoise3(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public SwampNoise3(Direction direction, bool small) : base(3144)
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
			SwampNoise3Timer.SwampNoise3List.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x006);
		}

		public SwampNoise3(Serial serial) : base(serial)
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

			SwampNoise3Timer.SwampNoise3List.Add(this);
		}
	}

	public class SwampNoise3Timer : Timer 
	{
		public const bool Enabled = true;
		public static List<SwampNoise3> SwampNoise3List = new List<SwampNoise3>();

		public static void Initialize() 
		{
			if (Enabled)
				new SwampNoise3Timer().Start();
		}

		public SwampNoise3Timer() : base(TimeSpan.FromSeconds( 3.00 ), TimeSpan.FromSeconds( 3.00 ) ) 
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick() 
		{
			foreach (SwampNoise3 swampnoise3 in SwampNoise3List)
				swampnoise3.OnTick();
		}
	}
}