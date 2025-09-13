using System;
using System.Collections;
using System.Linq;
using Server;
using Server.Mobiles;
using Server.Engines.Quests;
using Server.Misc;
using Server.Regions;

namespace Server.Items
{
    public class StudyBook : Item
    {
		//Remember: 1 SkillGainedPerTick below would translate to a 0.1 skill increase!
		private static readonly int SkillGainInterval = 10;//This is the number of minutes that pass before a skill gain happens while studying.
		private static readonly int SkillGainedPerTick = 1;//This is the skill gain for each gain interval that has passed.
		private static readonly int SkillGainMax = 200;//This is the maximum gain that can be achieved from one study session.
		private static readonly int HoursTilAcceleratedSkillGain = 5;//This is the amount of study hours required to gain a Scroll of Alacrity effect.
		private static readonly TimeSpan AcceleratedSkillGainTime = TimeSpan.FromMinutes(30.0);//This is the duration of the accellerated skill gain.
		private static readonly bool AllowSkillAcceleration = true;//Set this to false if you don't want your players to receive accellerated skill gain.
		private SkillName _TrainingSkill;
		private int _MaxSkillTrained;
		private Mobile _Reader;
		private bool _IsInUse;
		private DateTime _StartStudy;
		private bool _WillStartStudyNextLogout;
		
		private static readonly Hashtable _Table = new Hashtable();
		
		[CommandProperty(AccessLevel.GameMaster)]
		public SkillName TrainingSkill
		{
			get { return _TrainingSkill; }
			set { _TrainingSkill = value; ValidateName(); }
		}
		[CommandProperty(AccessLevel.GameMaster)]
		public int MaxSkillTrained
		{
			get { return _MaxSkillTrained; }
			set { _MaxSkillTrained = value; ValidateName(); }
		}
		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile Reader
		{
			get { return _Reader; }
			set { _Reader = value; ValidateName(); }
		}
		
        [Constructable]
        public StudyBook()
            : this(SkillName.Alchemy, 700)
        {
        }

        [Constructable]
        public StudyBook(SkillName skill, int maxtrain)
            : base(0x225A)
        {
            this._TrainingSkill = skill;
			this._MaxSkillTrained = maxtrain;
			this.Weight = 0;
			ValidateName();
        }

        public StudyBook(Serial serial)
            : base(serial)
        {
        }
		
		public void ValidateName()
		{
			string skillName = this.GetName();
			if (this._MaxSkillTrained >= 1200 )
			{
				this.Name = "A tome of lost " + skillName.ToString() + " techniques";
				this.Hue = 1260;
			}
			else if (this._MaxSkillTrained >= 1000)
			{
				this.Name = "an advanced " + skillName.ToString() + " manual";
				this.Hue = 1258;
			}
			else
			{
				this.Name = "a standard " + skillName.ToString() + " manual";
				this.Hue = 1256;
			}
		}
		
		public void StartStudy(Mobile from)
		{
			this._IsInUse = true;
			this._Reader = from;
			this.Movable = false;//The book cannot be moved while it is being used.
			this._WillStartStudyNextLogout = false;
			this._StartStudy = DateTime.UtcNow;
		} 
		
		public void EndStudy(Mobile from)
		{
			TimeSpan difference = DateTime.UtcNow - this._StartStudy;
			int toGain = (int)((difference.TotalMinutes / SkillGainInterval) * SkillGainedPerTick);		
			
			if (from.Skills[this._TrainingSkill].BaseFixedPoint + toGain > this._MaxSkillTrained)
				toGain = this._MaxSkillTrained - from.Skills[this._TrainingSkill].BaseFixedPoint;//Cannot gain above the max for your study book.
			//Skills gained in excess of 100 are gained at half-rate. To disable this, comment out below.
			if (from.Skills[_TrainingSkill].BaseFixedPoint + toGain > 1000)
			{
				if (from.Skills[this._TrainingSkill].BaseFixedPoint <= 1000)
					toGain -= (int)((toGain - (1000 - from.Skills[this._TrainingSkill].BaseFixedPoint)) / 2);//only reduces amount gained over 100
				else
					toGain /= 2; //Divides any remaining skill gains by 2 before proceeding
			}

			//from.SendMessage(0, "Debug: toGain is " + toGain);

			if (toGain > SkillGainMax)
				toGain = SkillGainMax;
			
			if (toGain > 0)
			{
				Skill skill = from.Skills[this._TrainingSkill];
				
				if ( skill == null )
					return;
				
				double initialvalue = skill.Value;
				double valueskill = skill.Value;
				double chance = 0;
				double cap = skill.Cap;
				
			    for (int x = 0; x < toGain; ++x)
                {

					valueskill = skill.Value;
					
					chance = ((this._MaxSkillTrained - valueskill) / this._MaxSkillTrained) * ((cap - valueskill) / cap); 
					
					if (this._TrainingSkill == SkillName.AnimalTaming || this._TrainingSkill == SkillName.Lockpicking || this._TrainingSkill == SkillName.Provocation || this._TrainingSkill == SkillName.Magery || this._TrainingSkill == SkillName.Bushido || this._TrainingSkill == SkillName.Ninjitsu || this._TrainingSkill == SkillName.Chivalry || this._TrainingSkill == SkillName.Necromancy ) // skills that are harder to gain by studying
						chance /= 2;
					else
						chance /= 1.5; // tweak this value fo difficulty.  higher the harder to gain.
					
					if ( from.Skills.Cap == 0 )
						return;
					if ( valueskill >= this._MaxSkillTrained )
						return;
					if ( (valueskill - initialvalue) >= (SkillGainMax/10) )
						return;
					if ( valueskill >= skill.Cap)
						return;
					
					if (chance <= 0.01)
						chance = 0.01;
					
					bool success = ( chance >= Utility.RandomDouble() );

					if ( success )
						SkillCheck.Gain(from, skill);
				}
				from.SendMessage(0, "You gained " + String.Format("{0:0.0}", (valueskill - initialvalue)) + " skill in your studies");
				LoggingFunctions.LogStudy( from, this._TrainingSkill, (valueskill - initialvalue) );
			}
			else
				from.SendMessage(0, "You did not study for long enough to gain any benefit. ");
			
			this._IsInUse = false;
			this._Reader = null;
			this._WillStartStudyNextLogout = false;
			this.Movable = true;
			this._StartStudy = DateTime.UtcNow;
			int TotalStudyTime = (int)difference.TotalMinutes;
			int StudyTimeHours = (int)(TotalStudyTime / 60);
			int StudyTimeMinutes = (int)(TotalStudyTime % 60);
			from.SendMessage(0, "You studied for {0} hours and {1} minutes.", StudyTimeHours, StudyTimeMinutes);

			if (Utility.RandomDouble() > 0.92)
			{
				from.SendMessage(0, "Unfortunately, the book disentigrates into dust after you close it. ");
				this.Delete();
				return;
			}

			if (difference.TotalHours >= HoursTilAcceleratedSkillGain && AllowSkillAcceleration && from is PlayerMobile)
			{
				from.SendMessage(0, "Due to the length time you studied, you are gaining {0} skill at an accelerated rate!", GetName());
				PlayerMobile pm = from as PlayerMobile;
				Effects.PlaySound(from.Location, from.Map, 0x1E9);
				Effects.SendTargetParticles(from, 0x373A, 35, 45, 0x00, 0x00, 9502, (EffectLayer)255, 0x100);
				pm.AcceleratedStart = DateTime.UtcNow + AcceleratedSkillGainTime;

				Timer t = (Timer)_Table[from];

				_Table[from] = Timer.DelayCall(AcceleratedSkillGainTime, new TimerStateCallback(Expire_Callback), from);

				pm.AcceleratedSkill = _TrainingSkill;

				BuffInfo.AddBuff(pm, new BuffInfo(BuffIcon.ArcaneEmpowerment, 1078511, 1078512, AcceleratedSkillGainTime, pm, GetName(), true));
			}
		}
		
		public bool CheckStudy()
		{
			if (this._IsInUse == true || this._WillStartStudyNextLogout == true)
				return true;
			else
				return false;
		}
		
		public static void OnLogout(LogoutEventArgs args)
		{
			Mobile from = args.Mobile;
			for (int i = 0; i < from.Backpack.Items.Count(); i++)
			{
				StudyBook book = from.Backpack.Items[i] as StudyBook;
				if (from != null && book != null && book.IsChildOf(from.Backpack) && book.CheckStudy() && book.Reader == from )
					book.StartStudy(from);
			}
		}
		
		public static void OnLogin(LoginEventArgs args)
		{
			Mobile from = args.Mobile;
			for (int i = 0; i < from.Backpack.Items.Count(); i++)
			{
				StudyBook book = from.Backpack.Items[i] as StudyBook;
				if (from != null && book != null && book.IsChildOf(from.Backpack) && book.CheckStudy() && book.Reader == from)
					book.EndStudy(from);
			}
		}
		
        public virtual string GetName()
        {
            int index = (int)_TrainingSkill;
            SkillInfo[] table = SkillInfo.Table;

            if (index >= 0 && index < table.Length)
                return table[index].Name.ToLower();
            else
                return "???";
        }
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			double skillTrained = this._MaxSkillTrained / 10;
			list.Add("Allows offline training up to level " + skillTrained.ToString());
		}
		
		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
				from.SendMessage(0, "You may only use this item if it is in your backpack.");
			else if (this._Reader != from && this._Reader != null)
			{
				from.SendMessage(0, "Someone else was using this book, but you take out their bookmark.  You can now use this book.");
				this._Reader = null;
				this._IsInUse = false;
			}

			else if (!this._IsInUse && this._Reader == null && from.Skills[this._TrainingSkill].BaseFixedPoint >= this._MaxSkillTrained)
				from.SendMessage(0, "You cannot learn anything further from studying this book.");
			else if (!this._IsInUse && this._Reader == null && from.Skills[this._TrainingSkill].BaseFixedPoint >= from.Skills[this._TrainingSkill].CapFixedPoint)
				from.SendMessage(0, "You cannot study a skill that you have already mastered.");
			else if (from.Skills[this._TrainingSkill].Lock != SkillLock.Up)
				from.SendMessage(0, "You cannot study a skill unless it is set to increase.");
			//else if (this._IsInUse && this._Reader == from && this._WillStartStudyNextLogout)
			//	this.EndStudy(from);
			else if (from.Combatant != null)
				from.SendMessage(0, "You cannot use this in combat.");

			else if (!this._IsInUse && this._Reader == null)
			{
				for( int i = 0; i < from.Backpack.Items.Count(); i++)
				{
					Item item = from.Backpack.Items[i];
					if (item != null && item is StudyBook)
					{
						StudyBook book = item as StudyBook;
						if (book != this && book.Reader == from)
						{
							from.SendMessage(0, "You can only study one book at a time.");
							return;
						}
					}
				}
				this._Reader = from;
				this._WillStartStudyNextLogout = true;
				from.SendMessage(0, "You will begin studying this book the next time you log out.");
			}
			else if (!this._IsInUse && this._Reader == from )
			{
				from.SendMessage(0, "You decide not to study the book.");
				this._Reader = null;
				this._IsInUse = false;
				this._WillStartStudyNextLogout = false;
			}
		}
		
		private static void Expire_Callback(object state)
        {
            AcceleratedGainEnd((Mobile)state);
        }
		
		public static bool AcceleratedGainEnd(Mobile m)
        {
            _Table.Remove(m);

            m.PlaySound(0x1F8);

            BuffInfo.RemoveBuff(m, BuffIcon.ArcaneEmpowerment);

            m.SendMessage(0, "You are no longer gaining skills at an accelerated rate.");

            return true;
        }
		
		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( StudyBook.OnLogin );
			EventSink.Logout += new LogoutEventHandler( StudyBook.OnLogout );			
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
			writer.Write((int)_TrainingSkill);
			writer.Write((int)_MaxSkillTrained);
			writer.Write((Mobile)_Reader);
			writer.Write((bool)_IsInUse);
			writer.Write((DateTime)_StartStudy);
			writer.Write((bool)_WillStartStudyNextLogout);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
			_TrainingSkill = (SkillName)reader.ReadInt();
			_MaxSkillTrained = reader.ReadInt();
			_Reader = reader.ReadMobile();
			_IsInUse = reader.ReadBool();
			_StartStudy = reader.ReadDateTime();
			_WillStartStudyNextLogout = reader.ReadBool();
        }
    }
}
