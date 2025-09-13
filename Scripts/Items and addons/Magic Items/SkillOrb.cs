using System;
using Server.Mobiles;
using Server.Misc;
using System.Collections;
using System.Collections.Generic;
using Server.OneTime; //onetime edit

namespace Server.Items
{
    public class SkillOrb : SelfDeletingItem, IOneTime //onetime edit
    {
        private int m_quality;

        [Constructable]
        public SkillOrb(int quality) : base(14265, "Soul Essence", 50)  //Duration 50 sec
        {
            Movable = false;
            Hue = 1384;
            Name = "Soul Essence"; //We must specify Name again, because in SelfDeletingItem is little bug

            OneTimeType = 3; //onetime edit
            MorphCounter = 0; //onetime edit
            m_quality = quality;

        }

        public int OneTimeType { get; set; } //onetime edit

        private int MorphCounter { get; set; } //onetime edit

        public void OneTimeTick() //onetime edit
        {
            if (MorphCounter == 30)
            {
                ItemID = 14270;
                Effects.PlaySound(Location, Map, 553);
            }
            else
            {
                MorphCounter++;
            }
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile from, Point3D oldLocation)
        {
		if (!from.Player || from.AccessLevel > AccessLevel.Player || from.Hidden || !from.Alive || from.Blessed)
					return;
					
			if (from.Player)
			{
				if (from.AccessLevel == AccessLevel.Player && !from.Hidden && from.Alive && !from.Blessed && from.GetDistanceToSqrt(this) <= 1)
					Skill(from);
			}
            base.OnMovement(from, oldLocation);
        }

        public override void OnDoubleClick(Mobile from)
        {
			if (from != null && from is PlayerMobile)
			{			
				if (!from.Player || from.AccessLevel > AccessLevel.Player || from.Hidden || !from.Alive || from.Blessed)
					return;

				if (!from.InRange(this.GetWorldLocation(), 1))
					from.SendLocalizedMessage(502138);
				else
					Skill(from);
			}
        }

        public virtual void Skill(Mobile from)
        {

            if (this.Deleted || from == null) //from = player who triggered skill (not used yet)
                return;		

            if (from is PlayerMobile && ((PlayerMobile)from).SoulBound)
            {
                ((PlayerMobile)from).SoulForce += m_quality*10;
            }
	    else
	    {

			Skill skill = from.Skills[Utility.Random(Skills.Count)];
			
			double valueskill = skill.Value;
			double cap = skill.Cap;

			if ( cap == 0 || skill.Lock != SkillLock.Up ) // || 
				return;
					
			
			while (m_quality > 0)
			{
				m_quality --;
				SkillCheck.Gain(from, skill);
			}
				
			
		}	
            from.FixedParticles(0x376A, 9, 32, 5005, EffectLayer.Waist);
            from.PlaySound(0x1F2);

            this.Delete();
        }

		private static readonly SkillName[] m_Skills = new SkillName[]
        {
            SkillName.Blacksmith,
            SkillName.Tailoring,
            SkillName.Swords,
            SkillName.Fencing,
            SkillName.Macing,
            SkillName.Archery,
            SkillName.Wrestling,
            SkillName.Parry,
            SkillName.Tactics,
            SkillName.Anatomy,
            SkillName.Healing,
            SkillName.Magery,
            SkillName.Meditation,
            SkillName.EvalInt,
            SkillName.MagicResist,
            SkillName.AnimalTaming,
            SkillName.AnimalLore,
            SkillName.Veterinary,
            SkillName.Musicianship,
            SkillName.Provocation,
            SkillName.Discordance,
            SkillName.Peacemaking,
            SkillName.Chivalry,
            SkillName.Focus,
            SkillName.Necromancy,
            SkillName.Stealing,
            SkillName.Stealth,
            SkillName.SpiritSpeak,
            SkillName.Ninjitsu,
            SkillName.Bushido,
            SkillName.Throwing,
			SkillName.Alchemy,
			SkillName.Cooking,
			SkillName.Fishing,
			SkillName.Lumberjacking,
			SkillName.Mining,
			SkillName.Tinkering,
			SkillName.Begging,
			SkillName.Forensics,
			SkillName.ItemID,
			SkillName.TasteID,
			SkillName.Camping,
			SkillName.Hiding,
			SkillName.Inscribe,
			SkillName.DetectHidden,
			SkillName.RemoveTrap,
			SkillName.Lockpicking,
			SkillName.Poisoning,
			SkillName.Snooping,
			SkillName.Cartography,
			SkillName.Herding,
			SkillName.Tracking
        };
		private static readonly List<SkillName> _Skills = new List<SkillName>();
		public static List<SkillName> Skills
        {
            get
            {
                if (_Skills.Count == 0)
                {
                    _Skills.AddRange(m_Skills);
                }
                return _Skills;
            }
        }

        public override void OnDelete()
        {
			if (this.Deleted) // trying to catch null exception error
				return;
			
            Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x374A, 9, 32, 5024);
            Effects.PlaySound(Location, Map, 0x5C9);
            base.OnDelete();
        }

        public SkillOrb(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            this.Delete();
        }

        public static void DropSkill(Mobile harmed)
        {
            if ( harmed == null || harmed.Map == null ) 
                return;

            Point3D loc = harmed.Location;
            int jetsoncanblowhimself = 0;

			if (harmed.Map.CanFit(harmed.X, harmed.Y, harmed.Z, 6, false, false) )
			{
				if (harmed.Fame < 5000)
					jetsoncanblowhimself = 1;
				else if (harmed.Fame < 12000)
					jetsoncanblowhimself = 2;
				else if (harmed.Fame < 18000)
					jetsoncanblowhimself = 4;
				else if (harmed.Fame < 25000)
					jetsoncanblowhimself = 8;
				else if (harmed.Fame >= 25000)
					jetsoncanblowhimself = 12;

				SkillOrb orb = new SkillOrb( jetsoncanblowhimself );
				if (orb != null)
				{
					orb.MoveToWorld(loc, harmed.Map);
				}
			}
        }
    }
}
