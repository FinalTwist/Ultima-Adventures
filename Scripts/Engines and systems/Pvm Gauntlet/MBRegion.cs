using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public class MBRegion : Region
	{
		private GauntletMaster m_EventNPC;

		public GauntletMaster EventNPC { get { return m_EventNPC; } set { m_EventNPC = value; } }
		public MBRegion(GauntletMaster npc, string name, Map map, int priority, params Rectangle2D[] area) : base(name, map, priority, area)
		{
			m_EventNPC = npc;
		}

		public override void OnRegister()
		{
			base.OnRegister();

			ShowRegionBounds(this, m_EventNPC);
		}

		public override void OnUnregister()
		{
			List<Item> removableItems = new List<Item>();

			foreach (var item in World.Items)
			{
				if ((item.Value is Blocker || item.Value is LOSBlocker) && !String.IsNullOrEmpty(item.Value.Name) && item.Value.Name.Equals(m_EventNPC.Serial.ToString()))
					removableItems.Add(item.Value);
			}

			foreach (var item in removableItems)
			{
				item.Delete();
			}

			base.OnUnregister();
		}

		public static void ShowRectBounds(Rectangle3D r, Map m, GauntletMaster npc)
		{
			if (m == Map.Internal || m == null)
				return;

			Point3D p1 = new Point3D(r.Start.X, r.Start.Y - 1, 0); //So we dont' need to create a new one each point
			Point3D p2 = new Point3D(r.Start.X, r.Start.Y + r.Height - 1, 0);  //So we dont' need to create a new one each point

			var blocker = new Blocker() { Name = npc.Serial.ToString()};
			var losBlocker = new LOSBlocker() { Name = npc.Serial.ToString() };

			losBlocker.MoveToWorld(p1, m);
			blocker.MoveToWorld(p1, m);

			for (int x = r.Start.X; x <= (r.Start.X + r.Width - 1); x++)
			{
				p1.X = x;
				p2.X = x;

				p1.Z = 0;
				p2.Z = 0;

				 blocker = new Blocker() { Name = npc.Serial.ToString() };
				 losBlocker = new LOSBlocker() { Name = npc.Serial.ToString() };

				losBlocker.MoveToWorld(p1, m);
				blocker.MoveToWorld(p1, m);

				blocker = new Blocker() { Name = npc.Serial.ToString() };
				losBlocker = new LOSBlocker() { Name = npc.Serial.ToString() };

				losBlocker.MoveToWorld(p2, m);
				blocker.MoveToWorld(p2, m);
			}

			p1 = new Point3D(r.Start.X - 1, r.Start.Y - 1, 0);
			p2 = new Point3D(r.Start.X + r.Width - 1, r.Start.Y, 0);

			for (int y = r.Start.Y; y <= (r.Start.Y + r.Height - 1); y++)
			{
				p1.Y = y;
				p2.Y = y;

				p1.Z = 0;
				p2.Z = 0;

				 blocker = new Blocker() { Name = npc.Serial.ToString() };
				 losBlocker = new LOSBlocker() { Name = npc.Serial.ToString() };

				losBlocker.MoveToWorld(p1, m);
				blocker.MoveToWorld(p1, m);


				blocker = new Blocker() { Name = npc.Serial.ToString() };
				losBlocker = new LOSBlocker() { Name = npc.Serial.ToString() };

				losBlocker.MoveToWorld(p2, m);
				blocker.MoveToWorld(p2, m);
			}
		}

		public static void ShowRegionBounds(Region r, GauntletMaster npc)
		{
			if (r == null)
				return;

			foreach (Rectangle3D rect in r.Area)
			{
				ShowRectBounds(rect, r.Map, npc);
			}
		}

		public override void OnEnter(Mobile m)
		{
			m.SendMessage("Welcome to {0}", Name);

			base.OnEnter(m);

		}


		public override void OnExit(Mobile m)
		{
			m.SendMessage("You have left {0}", Name);

			base.OnExit(m);
		}
	}
}
