using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class Wave2 : Item
	{
		[Constructable]
		public Wave2(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public Wave2(Direction direction, bool small) : base(0x1FB7)
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
			Wave2Timer.Wave2List.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x13);
		}

		public Wave2(Serial serial) : base(serial)
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

			Wave2Timer.Wave2List.Add(this);
		}
	}

	public class Wave2Timer : Timer 
	{
		public const bool Enabled = true;
		public static List<Wave2> Wave2List = new List<Wave2>();

		public static void Initialize() 
		{
			if (Enabled)
				new Wave2Timer().Start();
		}

		public Wave2Timer() : base(TimeSpan.FromSeconds( 3.0 ), TimeSpan.FromSeconds( 3.0 )) 
		{
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick() 
		{
			foreach (Wave2 wave2 in Wave2List)
				wave2.OnTick();
		}
	}
}