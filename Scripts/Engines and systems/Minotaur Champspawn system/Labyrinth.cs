using System; 
using Server.Mobiles; 
using Server.Gumps;
using System.Collections;

namespace Server.Items 
{ 
	public class Labyrinth
	{
		private static int m_Kills;
		private static bool m_DoCounting;
		private const int m_KillsToAppear = 30;
		public static bool DoCounting
		{
			get {return m_DoCounting;}
			set {m_DoCounting = value;}
		}
		public static int Kills
		{
			get{return m_Kills;}
			set
			{
				if(m_DoCounting)
				{
					if(value >= m_KillsToAppear)
					{
                        Map map = Map.Trammel;

                        ArrayList list = new ArrayList();
                        Point3D loc1 = new Point3D(1798,624,0);

                        foreach (Mobile mob in map.GetMobilesInRange(loc1, 50))
                        {
                            if (mob.Player)
                                list.Add(mob);
                        }

                        foreach (Mobile mob in list)
                        {
                            mob.SendMessage( "A booming warcry comes from a nearby part of the maze. It appears the minotaurs' leader Meraktus is enraged by the slaying of his brethren and has come to their aid." );                                
                        }
						m_Kills = 0;
						SpawnChampion();
					}
					else
					{
						m_Kills = value;
					}
				}
			}
		}
		public static void SpawnChampion()
		{
			Meraktus m = new Meraktus();
			m_DoCounting = false;
			m.MoveToWorld(new Point3D(1798,624,0),Map.Trammel);
		}
        public static void SpawnCreature()
        {
            TormentedMinotaur m = new TormentedMinotaur();
            int Spawn = 4;
            m.MoveToWorld(new Point3D(1792, 635, 0), Map.Trammel);
        }

		public static void Initialize()
		{
			m_Kills = 0;
			m_DoCounting = true;

		}
	}
}