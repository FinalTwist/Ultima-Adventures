using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class Crickets2 : Item
	{
		[Constructable]
		public Crickets2(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public Crickets2(Direction direction, bool small) : base(3144)
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
			Crickets2Timer.Crickets2List.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x00B);
		}

		public Crickets2(Serial serial) : base(serial)
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

			Crickets2Timer.Crickets2List.Add(this);
		}
	}

	public class Crickets2Timer : Timer 
	{
		public const bool Enabled = true;
		public static List<Crickets2> Crickets2List = new List<Crickets2>();

		public static void Initialize() 
		{
			if (Enabled)
				new Crickets2Timer().Start();
		}

		public Crickets2Timer() : base(TimeSpan.FromSeconds( 0.0 ), TimeSpan.FromSeconds( 0.0 ) ) 
		{
			Priority = TimerPriority.TenMS;
		}

		protected override void OnTick() 
		{
			foreach (Crickets2 crickets2 in Crickets2List)
				crickets2.OnTick();
		}
	}
}