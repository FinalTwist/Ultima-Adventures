using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Engines.CannedEvil;
using Server.Misc;

namespace Server.Items
{
	public class GetTrueBestSkill
	{
		public static SkillName GetSkill ( Mobile from ) { return GetSkill(from, 0); }
		public static SkillName GetSkill ( Mobile from, int spot )
		{
			int spot2 = spot;
			if (spot2 < 0) spot2 = 0;
			SkillName sk = SkillName.Swords;
			List<LGSkillLists> skilllist = new List<LGSkillLists>();
			skilllist.Sort(delegate(LGSkillLists p1, LGSkillLists p2) { return p2.LGSkillVal.CompareTo(p1.LGSkillVal); });
			if ( spot2 > (skilllist.Count -1) ) spot2 = skilllist.Count -1;
			sk = skilllist[spot2].LGSkill;
			return sk;
		}

		public static void GetSkillTop5( Mobile from, ref SkillName skill1, ref SkillName skill2, ref SkillName skill3, ref SkillName skill4, ref SkillName skill5 )
		{
			List<LGSkillLists> skilllist = new List<LGSkillLists>();
			skilllist.Sort(delegate(LGSkillLists p1, LGSkillLists p2) { return p2.LGSkillVal.CompareTo(p1.LGSkillVal); });
			skill1 = skilllist[0].LGSkill;
			skill2 = skilllist[1].LGSkill;
			skill3 = skilllist[2].LGSkill;
			skill4 = skilllist[3].LGSkill;
			skill5 = skilllist[4].LGSkill;
			return;
		}

		public static void GetSkillCRTop5( Mobile from, ref SkillName skill1, ref SkillName skill2, ref SkillName skill3, ref SkillName skill4, ref SkillName skill5 )
		{
			List<LGSkillLists> skilllist = new List<LGSkillLists>();
			skilllist.Add(new LGSkillLists(SkillName.ItemID, from.Skills.ItemID.Value));
			skilllist.Add(new LGSkillLists(SkillName.Blacksmith, from.Skills.Blacksmith.Value));
			skilllist.Add(new LGSkillLists(SkillName.Fletching, from.Skills.Fletching.Value));
			skilllist.Add(new LGSkillLists(SkillName.Carpentry, from.Skills.Carpentry.Value));
			skilllist.Add(new LGSkillLists(SkillName.Tailoring, from.Skills.Tailoring.Value));
			skilllist.Add(new LGSkillLists(SkillName.Tinkering, from.Skills.Tinkering.Value));
			skilllist.Add(new LGSkillLists(SkillName.Inscribe, from.Skills.Inscribe.Value));
			skilllist.Add(new LGSkillLists(SkillName.Alchemy, from.Skills.Alchemy.Value));
			skilllist.Add(new LGSkillLists(SkillName.Cartography, from.Skills.Cartography.Value));
			skilllist.Add(new LGSkillLists(SkillName.Cooking, from.Skills.Cooking.Value));
			skilllist.Add(new LGSkillLists(SkillName.ArmsLore, from.Skills.ArmsLore.Value));
			skilllist.Add(new LGSkillLists(SkillName.Lumberjacking, from.Skills.Lumberjacking.Value));
			skilllist.Add(new LGSkillLists(SkillName.Mining, from.Skills.Mining.Value));
			skilllist.Add(new LGSkillLists(SkillName.Fishing, from.Skills.Fishing.Value));
			skilllist.Sort(delegate(LGSkillLists p1, LGSkillLists p2) { return p2.LGSkillVal.CompareTo(p1.LGSkillVal); });
			skill1 = skilllist[0].LGSkill;
			skill2 = skilllist[1].LGSkill;
			skill3 = skilllist[2].LGSkill;
			skill4 = skilllist[3].LGSkill;
			skill5 = skilllist[4].LGSkill;
			return;
		}

		public static void GetSkillCRBot5( Mobile from, ref SkillName skill1, ref SkillName skill2, ref SkillName skill3, ref SkillName skill4, ref SkillName skill5 )
		{
			List<LGSkillLists> skilllist = new List<LGSkillLists>();
			skilllist.Add(new LGSkillLists(SkillName.ItemID, from.Skills.ItemID.Value));
			skilllist.Add(new LGSkillLists(SkillName.Blacksmith, from.Skills.Blacksmith.Value));
			skilllist.Add(new LGSkillLists(SkillName.Fletching, from.Skills.Fletching.Value));
			skilllist.Add(new LGSkillLists(SkillName.Carpentry, from.Skills.Carpentry.Value));
			skilllist.Add(new LGSkillLists(SkillName.Tailoring, from.Skills.Tailoring.Value));
			skilllist.Add(new LGSkillLists(SkillName.Tinkering, from.Skills.Tinkering.Value));
			skilllist.Add(new LGSkillLists(SkillName.Inscribe, from.Skills.Inscribe.Value));
			skilllist.Add(new LGSkillLists(SkillName.Alchemy, from.Skills.Alchemy.Value));
			skilllist.Add(new LGSkillLists(SkillName.Cartography, from.Skills.Cartography.Value));
			skilllist.Add(new LGSkillLists(SkillName.Cooking, from.Skills.Cooking.Value));
			skilllist.Add(new LGSkillLists(SkillName.ArmsLore, from.Skills.ArmsLore.Value));
			skilllist.Add(new LGSkillLists(SkillName.Lumberjacking, from.Skills.Lumberjacking.Value));
			skilllist.Add(new LGSkillLists(SkillName.Mining, from.Skills.Mining.Value));
			skilllist.Add(new LGSkillLists(SkillName.Fishing, from.Skills.Fishing.Value));
			skilllist.Sort(delegate(LGSkillLists p1, LGSkillLists p2) { return p1.LGSkillVal.CompareTo(p2.LGSkillVal); });
			skill1 = skilllist[0].LGSkill;
			skill2 = skilllist[1].LGSkill;
			skill3 = skilllist[2].LGSkill;
			skill4 = skilllist[3].LGSkill;
			skill5 = skilllist[4].LGSkill;
			return;
		}

		public static void GetSkillFITop5( Mobile from, ref SkillName skill1, ref SkillName skill2, ref SkillName skill3, ref SkillName skill4, ref SkillName skill5 )
		{
			List<LGSkillLists> skilllist = new List<LGSkillLists>();
			skilllist.Add(new LGSkillLists(SkillName.Anatomy, from.Skills.Anatomy.Value));
			skilllist.Add(new LGSkillLists(SkillName.Tactics, from.Skills.Tactics.Value));
			skilllist.Add(new LGSkillLists(SkillName.Parry, from.Skills.Parry.Value));
			skilllist.Add(new LGSkillLists(SkillName.ArmsLore, from.Skills.ArmsLore.Value));
			skilllist.Add(new LGSkillLists(SkillName.MagicResist, from.Skills.MagicResist.Value));
			skilllist.Add(new LGSkillLists(SkillName.Archery, from.Skills.Archery.Value));
			skilllist.Add(new LGSkillLists(SkillName.Swords, from.Skills.Swords.Value));
			skilllist.Add(new LGSkillLists(SkillName.Macing, from.Skills.Macing.Value));
			skilllist.Add(new LGSkillLists(SkillName.Fencing, from.Skills.Fencing.Value));
			skilllist.Add(new LGSkillLists(SkillName.Lumberjacking, from.Skills.Lumberjacking.Value));
			skilllist.Add(new LGSkillLists(SkillName.Wrestling, from.Skills.Wrestling.Value));
			skilllist.Add(new LGSkillLists(SkillName.Chivalry, from.Skills.Chivalry.Value));
			skilllist.Add(new LGSkillLists(SkillName.Bushido, from.Skills.Bushido.Value));
			skilllist.Add(new LGSkillLists(SkillName.Ninjitsu, from.Skills.Ninjitsu.Value));
			skilllist.Sort(delegate(LGSkillLists p1, LGSkillLists p2) { return p2.LGSkillVal.CompareTo(p1.LGSkillVal); });
			skill1 = skilllist[0].LGSkill;
			skill2 = skilllist[1].LGSkill;
			skill3 = skilllist[2].LGSkill;
			skill4 = skilllist[3].LGSkill;
			skill5 = skilllist[4].LGSkill;
			return;
		}
	}

	public class ManaRedux
	{
		public static int CalculateSpecialMana(Mobile from, int BaseMana)
		{
			int mana = BaseMana;
			double skillTotal = GetReduxSkill(from, SkillName.Swords) + GetReduxSkill(from, SkillName.Macing)
				+ GetReduxSkill(from, SkillName.Fencing) + GetReduxSkill(from, SkillName.Archery) + GetReduxSkill(from, SkillName.Parry)
				+ GetReduxSkill(from, SkillName.Lumberjacking) + GetReduxSkill(from, SkillName.Stealth)
				+ GetReduxSkill(from, SkillName.Poisoning) + GetReduxSkill(from, SkillName.Bushido) + GetReduxSkill(from, SkillName.Ninjitsu);

			if (skillTotal >= 300.0) mana -= 10;
			else if (skillTotal >= 200.0) mana -= 5;
			double scalar = 1.0;
			if (!Server.Spells.Necromancy.MindRotSpell.GetMindRotScalar(from, ref scalar)) scalar = 1.0;
			int lmc = AosAttributes.GetValue(from, AosAttribute.LowerManaCost);
			int LMCCap = MyServerSettings.LowerManaCostCap();
			if (lmc > LMCCap) lmc = LMCCap;
			scalar -= (double)lmc / 100;
			mana = (int)(mana * scalar);
			return mana;
		}

		public static double GetReduxSkill(Mobile from, SkillName skillName)
		{
			Skill skill = from.Skills[skillName];
			if (skill == null) return 0.0;
			return skill.Value;
		}

	}

	public class WeaponStrikes
	{
		public static void AchillesStrike( Mobile mobile, TimeSpan duration )
		{
			if( !mobile.CantWalk )
			{
				mobile.CantWalk = true;
				CantWalkTimer m_CantWalkTimer = new CantWalkTimer( mobile, duration );
				m_CantWalkTimer.Start();
			}
		}

		private class CantWalkTimer : Timer
		{
			private Mobile m_Mobile;
			public CantWalkTimer( Mobile m, TimeSpan duration ) : base( duration )
			{
				this.Priority = TimerPriority.TwentyFiveMS;
				m_Mobile = m;
			}
			protected override void OnTick()
			{
				m_Mobile.CantWalk = false;
			}
		}
	}

	public class LGSkillLists
	{
		private SkillName m_LGSkill;
		public SkillName LGSkill
		{
			get { return m_LGSkill; }
			set { m_LGSkill = value; }
		}
		private double m_LGSkillVal;
		public double LGSkillVal
		{
			get { return m_LGSkillVal; }
			set { m_LGSkillVal = value; }
		}

		public LGSkillLists(SkillName skillname, double skval)
		{
			m_LGSkill = skillname;
			m_LGSkillVal = skval;
		}
	}

	public class LGStrValLists
	{
		private string m_LGStr;
		public string LGStr
		{
			get { return m_LGStr; }
			set { m_LGStr = value; }
		}
		private int m_LGVal;
		public int LGVal
		{
			get { return m_LGVal; }
			set { m_LGVal = value; }
		}

		public LGStrValLists(string strname, int amount)
		{
			m_LGStr = strname;
			m_LGVal = amount;
		}
	}

	public class GetSpawnRegion
	{
		public static bool IsChampSpawn( Mobile from )
		{
			if (Region.Find( from.Location, from.Map ).IsPartOf( typeof( Engines.CannedEvil.ChampionSpawnRegion ) ) ) return true;
			return false;
		}
	}
}