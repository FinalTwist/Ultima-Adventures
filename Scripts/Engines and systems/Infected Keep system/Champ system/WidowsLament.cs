using System; 
using Server.Mobiles; 
using Server.Gumps;
using System.Collections;

namespace Server.Items 
{ 
	public class WidowsLament
	{
		private static int m_Kills;
		private static bool m_DoCounting;
		private const int m_KillsToAppear = 20;
		public static bool DoCountingWidow
		{
			get {return m_DoCounting;}
			set {m_DoCounting = value;}
		}
		public static Mobile widow;
		public static int KillsWidow
		{
			get{return m_Kills;}
			set
			{
				if (WidowsLament.widow == null && m_DoCounting == false)
					m_DoCounting = true;

				if (m_DoCounting = true && WidowsLament.widow != null)
					m_DoCounting = false;

				if(m_DoCounting)
				{
					if(value >= m_KillsToAppear)
					{
                        Map map = Map.Ilshenar;

                        ArrayList list = new ArrayList();
                        Point3D loc1 = new Point3D(1175,1418,0);

                        foreach (Mobile mob in map.GetMobilesInRange(loc1, 100))
                        {
                            if (mob.Player)
                                list.Add(mob);
                        }

                        foreach (Mobile mob in list)
                        {
                            mob.SendMessage( "The Widow's languished cries echo from her resting place." );                                
                        }
						m_Kills = 0;
						SpawnChampionWidow();
					}
					else
					{
						m_Kills = value;
					}
				}
			}
		}
		public static void SpawnChampionWidow()
		{
			Widow m = new Widow();
			m_DoCounting = false;

			WidowsLament.widow = (Mobile)m;
			
			m.MoveToWorld(new Point3D(1181,1524,0),Map.Ilshenar);

            WidowsConcubine mc = new WidowsConcubine();
            mc.MoveToWorld(new Point3D(1181, 1532, 0), Map.Ilshenar);
			
            WidowsConcubine md = new WidowsConcubine();
            md.MoveToWorld(new Point3D(1181, 1515, 0), Map.Ilshenar);
			
            WidowsConcubine me = new WidowsConcubine();
            me.MoveToWorld(new Point3D(1117, 1346, 0), Map.Ilshenar);
			
		}

		public static void Initialize()
		{
			m_Kills = 0;
			if (WidowsLament.widow == null)
				m_DoCounting = true;
			else
				WidowsLament.widow.Delete();
		}
	}
}
