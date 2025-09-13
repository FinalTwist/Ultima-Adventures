using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
	public class Crickets1 : Item
	{
		[Constructable]
		public Crickets1(Direction direction) : this(direction, false)
		{
		}

		[Constructable]
		public Crickets1(Direction direction, bool small) : base(3144)
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
					ItemID = 3144;
					break;
				}
			}
			Crickets1Timer.Crickets1List.Add(this);
		}

		public void OnTick()
		{
			Effects.PlaySound(Location, Map, 0x00A);
		}

		public Crickets1(Serial serial) : base(serial)
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

			Crickets1Timer.Crickets1List.Add(this);
		}
	}

	public class Crickets1Timer : Timer 
	{
		public const bool Enabled = true;
		public static List<Crickets1> Crickets1List = new List<Crickets1>();

		public static void Initialize() 
		{
			if (Enabled)
				new Crickets1Timer().Start();
		}

		public Crickets1Timer() : base(TimeSpan.FromSeconds( 0.0 ), TimeSpan.FromSeconds( 0.0 ) ) 
		{
			Priority = TimerPriority.TenMS;
		}

		protected override void OnTick() 
		{
			foreach (Crickets1 crickets1 in Crickets1List)
				crickets1.OnTick();
		}
	}
}