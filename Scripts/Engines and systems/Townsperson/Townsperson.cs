using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Mobiles.Data;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace Server.Mobiles
{
    #region Some Objects
    public struct SpeechResponse
    {
        public string Response;
        public Mobile Speaker;
        public int Animation;
        public int Reaction;
        public string Reward;
        public string DelObject;

        public SpeechResponse(string response, Mobile speaker, int animationID, int reactionID, string rewardObject, string QuestObject2Delete)
        {
            Response = response;
            Speaker = speaker;
            Animation = animationID;
            Reaction = reactionID;
            Reward = rewardObject;
            DelObject = QuestObject2Delete;
        }
    }

    public class ReactionCallBackState
    {
        private Mobile m_Mobile;
        private int m_Reaction;

        public Mobile Mobile { get { return m_Mobile; } }
        public int Reaction { get { return m_Reaction; } }

        public ReactionCallBackState(Mobile speaker, int reactNum)
        {
            m_Mobile = speaker;
            m_Reaction = reactNum;
        }
    }
    #endregion

    public class Townsperson : BaseCreature
    {
        #region Variables
		
        // change to true for non-Threaded operation.
        // for debugging use only!
        private static bool synchronousCall = true;
        public static bool Synchronous
        {
            get { return synchronousCall; }
        }

        public enum LogLevel
        {
            None,	/* No Logging other than caught Exceptions */
            Basic,	/* Speech Events and Create/Delete Objects */
            Debug	/* includes Asynchronous and Database calls */
        }
        public static LogLevel Logging { get { return LogLevel.None; } }

        public enum Attitude { Good = 1, Bad, Indifferent };
        public enum Wealth { Poor, Normal, Rich };

        private Attitude m_attitude;
        private Wealth m_wealth;
        private string[] m_greetings;
        private PauseTimer m_pausetimer;
        private GreetTimer m_greettimer;
        private Mobile inConversation;
        private bool wasFrozen;
        private Direction oldDirection;
        private BaseWeapon m_weapon;
        private BaseWeapon m_staff;
        private Timer m_combattimer;
        private string m_tagText;
        private AccessLevel m_accessLevel;

        // these greetings should work coming or going
        private static string[] goodGreetings = 
		{
			"*waves*", "*nods*", "*smiles*", "g'day", 
			"*waves*", "*nods*", "*smiles*", "g'day", 
			"*waves*", "*nods*", "*smiles*", "g'day", 
			"good day", "Well met", "Good to see you.", 
			"good day", "Well met", "It's good to see you.", 
			"Peace be with you", "May the Virtues guide you" 
		};
        private static string[] badGreetings = 
		{
			"*nods*", "*frowns*", "*nods*", "*frowns*", 
			"*coughs*", "*hrumph*", "*grunts*", "yeah"
		};
        private static string[] indifGreetings = 
		{
			"*waves*", "*nods*", "*waves*", "*nods*", 
			"*smiles*", "*coughs*", "g'day" 
		};
        // How close the Player must be
        public virtual int ConverseRange { get { return 3; } }

        // How long the NPC stands still waiting for another speech event
        public virtual TimeSpan PauseDelay { get { return TimeSpan.FromSeconds(9); } }

        public override bool CanOpenDoors { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override bool Commandable { get { return false; } }
        public override bool CanTeach { get { return true; } }
		public override bool InitialInnocent{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Attitude attitude
        {
            get { return m_attitude; }
            set
            {
                m_attitude = value;
                UpdateGreetings();
                UpdateKarmaFame();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Wealth wealth
        {
            get { return m_wealth; }
            set
            {
                m_wealth = value;
                if (m_staff != null) m_staff.Delete();
                if (m_weapon != null) m_weapon.Delete();
                Strip(this);
                InitOutfit();
                PackRandomWeapon();
            }
        }

        // flag to prevent spam
        private bool m_busy = false;
        public virtual bool Busy
        {
            get { return m_busy; }
            set { m_busy = value; }
        }

        // Property to use to filter responses
        [CommandProperty(AccessLevel.GameMaster)]
        public string Tag
        {
            get { return m_tagText; }
            set { m_tagText = value; }
        }

        // Does the Attacker become Criminal?
        private bool m_criminalAction = false; //default

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AttackIsCriminal
        {
            get { return m_criminalAction; }
            set { m_criminalAction = value; }
        }
        #endregion

        # region Constructors
        [Constructable]
        public Townsperson()
            : this(null, null, null)
        {
        }

        [Constructable]
        public Townsperson(string name)
            : this(name, null, null)
        {
        }

        [Constructable]
        public Townsperson(string name, string title)
            : this(name, title, null)
        {
        }

        [Constructable]
        public Townsperson(string name, string title, string tag)
            : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.8, 1.6)
        {
            Name = name;
            Title = title;
			
			if (Title == null)
			{
				if (Utility.RandomDouble() > 0.85)
				{
					switch (Utility.Random(5))
					{
						case 0: Title = "The Thief"; break;
						case 1: Title = "The Brute"; break;
						case 2: Title = "The Drunk"; break;
						case 3: Title = "The Joker"; break;
						case 4: Title = "The Loco"; break;
					}
				}
			}
			
            Tag = tag;

            SetStr(45, 90);
            SetDex(35, 70);
            SetInt(35, 70);

            SetHits(35, 120);
            SetStam(60, 80);
            SetMana(25, 50);

            SetDamage(7, 25);
            SetDamageType(ResistanceType.Physical, 100);
            SetResistance(ResistanceType.Physical, 10, 20);

            VirtualArmor = 30;

            // set NPC wealth
            Double dbl = Utility.RandomDouble();
            if (dbl > .875)
                m_wealth = Wealth.Rich;
            else if (dbl < .25)
                m_wealth = Wealth.Poor;
            else
                m_wealth = Wealth.Normal;

            //set NPC attitude
            dbl = Utility.RandomDouble();
            if (dbl > .45)
                m_attitude = Attitude.Good;
            else if (dbl < .2)
                m_attitude = Attitude.Bad;
            else
                m_attitude = Attitude.Indifferent;

            UpdateGreetings();
            UpdateKarmaFame();

            InitBody();
            InitOutfit();

            // Poor will never train, Rich might train 2 skills
            SetSkills(this);
            for (int x = 0; x < (int)m_wealth; x++)
                AddTrainingSkill();

            Container pack = new Backpack();
            pack.DropItem(new Gold(Utility.Random(3) + (Utility.Random(14) * (int)m_wealth)));
            pack.DropItem(new Bandage(Utility.Random(4, 16)));

            switch (Utility.Random(3))
            {
                case 0: pack.DropItem(new Torch()); break;
                case 1: pack.DropItem(new Apple(2)); break;
                case 2: pack.DropItem(new Candle()); break;
            }
            pack.Movable = false;
            AddItem(pack);

            PackRandomWeapon();
        }
        # endregion

        #region Initialize
        public virtual void InitBody()
        {
            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                if (Name == null)
                    Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                if (Name == null)
                    Name = NameList.RandomName("male");

                // add beard
                switch (Utility.Random(7))
                {
                    default: FacialHairItemID = 0x00; break; //None
                    case 0: FacialHairItemID = 0x2041; break; //Mustache
                    case 1: FacialHairItemID = 0x203F; break; //ShortBeard
                    case 2: FacialHairItemID = 0x204D; break; //Vandyke
                    case 3:
                        {
                            if (m_wealth == Wealth.Poor)
                                FacialHairItemID = 0x203E; //LongBeard
                            else if (m_wealth == Wealth.Rich)
                                FacialHairItemID = 0x2040; //Goatee
                            break;
                        }
                }
            }

            switch (Utility.Random(7))
            {
                case 0: HairItemID = 0x2047; break; //Afro
                case 1: HairItemID = 0x2045; break; //PageboyHair
                case 2: HairItemID = 0x203D; break; //PonyTail
                case 3: HairItemID = 0x203B; break; //ShortHair
                case 4: HairItemID = 0x2049; break; //TwoPigTails
                case 5: HairItemID = 0x203C; break; //LongHair
                case 6:
                    {
                        if (Female)
                            HairItemID = 0x2046; //BunsHair
                        else
                            HairItemID = 0x2048; //ReceedingHair
                        break;
                    }
            }

            Hue = Utility.RandomSkinHue();
            HairHue = Utility.RandomHairHue();
            FacialHairHue = HairHue;
            SpeechHue = Utility.RandomDyedHue();
        }

        public virtual void InitOutfit()
        {
            int hueRange;

                    BodySash sash = new BodySash();
                    sash.Hue = 1161;
                    sash.Name = "Initiate, League of Jetsons";
                    AddItem(sash);
                    sash.Movable = false;

            // shoes (hehe - figure this one out)
            switch ((Utility.Random(3) + 1) * ((int)m_wealth + 1))
            {
                case 1: break; // barefoot poor
                case 2: AddItem(new Shoes(GetShoeHue())); break; // poor, normal
                case 3: AddItem(new Sandals(GetShoeHue())); break; // poor, rich
                default:
                case 4: AddItem(new Shoes(GetShoeHue())); break; // normal
                case 6: AddItem(new Boots(GetShoeHue())); break; // normal, rich
                case 9: AddItem(new ThighBoots(GetShoeHue())); break; // rich
            }

            if (Female)
            {
                hueRange = Utility.Random(5); //get a color scheme
                switch ((int)m_wealth)
                {
                    case 0: // Poor
                        {
                            switch (Utility.Random(2))
                            {
                                case 0: AddItem(new ShortPants(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new Kilt(GetRandomHueRange(hueRange))); break;
                            }

                            DoShirt(hueRange);

                            switch (Utility.Random(7))
                            {
                                default: break;
                                case 0: AddItem(new Bandana(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new FloppyHat(Utility.RandomNeutralHue())); break;
                                case 2: AddItem(new StrawHat(Utility.RandomNeutralHue())); break;
                            }
                            break;
                        }
                    case 1: // Normal
                        {
                            switch (Utility.Random(4))
                            {
                                case 0:
                                    {
                                        AddItem(new PlainDress(GetRandomHueRange(hueRange)));
                                        break;
                                    }
                                case 1:
                                    {
                                        AddItem(new Skirt(GetRandomHueRange(hueRange)));
                                        AddItem(new Shirt(GetRandomHueRange(hueRange)));
                                        break;
                                    }
                                case 2:
                                    {
                                        AddItem(new LongPants(GetRandomHueRange(hueRange)));
                                        DoShirt(hueRange);
                                        break;
                                    }
                                case 3:
                                    {
                                        AddItem(new ShortPants(GetRandomHueRange(hueRange)));
                                        DoShirt(hueRange);
                                        break;
                                    }
                            }

                            switch (Utility.Random(5))
                            {
                                default: break;
                                case 0: AddItem(new Bonnet(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new FloppyHat(GetRandomHueRange(hueRange))); break;
                                case 2: AddItem(new Cap(GetRandomHueRange(hueRange))); break;
                            }

                            if (Utility.RandomDouble() < .08)
                                AddItem(new FullApron(Utility.RandomNeutralHue()));

                            if (Utility.RandomBool())
                                AddItem(new GoldRing());

                            break;
                        }
                    case 2: // Rich
                        {
                            switch (Utility.Random(2))
                            {
                                case 0:
                                    {
                                        AddItem(new Skirt(GetRandomHueRange(hueRange)));
                                        switch (Utility.Random(2))
                                        {
                                            case 0: AddItem(new FancyShirt(GetRandomHueRange(hueRange))); break;
                                            case 1: AddItem(new Shirt(GetRandomHueRange(hueRange))); break;
                                        }
                                        break;
                                    }
                                case 1:
                                    {
                                        AddItem(new FancyDress(GetRandomHueRange(hueRange)));
                                        if (Utility.RandomDouble() < .667)
                                            AddItem(new Cloak(GetRandomHueRange(hueRange)));

                                        break;
                                    }
                            }

                            switch (Utility.Random(3))
                            {
                                default: break;
                                case 0: AddItem(new Bonnet(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new FeatheredHat(GetRandomHueRange(hueRange))); break;
                            }

                            if (Utility.RandomDouble() < .333)
                            {
                                m_staff = new GnarledStaff();
                                EquipItem(m_staff);
                            }

                            if (Utility.RandomBool())
                                AddItem(new GoldRing());
                            if (Utility.RandomBool())
                                AddItem(new GoldEarrings());
                            if (Utility.RandomDouble() < .2)
                                AddItem(new GoldBracelet());
                            if (Utility.RandomDouble() < .2)
                                AddItem(new GoldBeadNecklace());
                            else if (Utility.RandomDouble() < .2)
                                AddItem(new GoldNecklace());

                            break;
                        }



                }
            }

            else // Male 
            {
                hueRange = Utility.Random(3);
                switch ((int)m_wealth)
                {
                    case 0: // Poor
                        {
                            switch (Utility.Random(2))
                            {
                                case 0: AddItem(new LongPants(Utility.RandomNeutralHue())); break;
                                case 1: AddItem(new ShortPants(Utility.RandomNeutralHue())); break;
                            }
                            DoShirt(0);
                            break;
                        }
                    case 1: // Normal
                        {
                            switch (Utility.Random(3))
                            {
                                case 0: AddItem(new FancyShirt(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new Doublet(GetRandomHueRange(hueRange))); break;
                                case 2: AddItem(new Shirt(GetRandomHueRange(hueRange))); break;
                            }

                            switch (Utility.Random(2))
                            {
                                case 0: AddItem(new LongPants(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new ShortPants(GetRandomHueRange(hueRange))); break;
                            }

                            switch (Utility.Random(5))
                            {
                                default: break;
                                case 0: AddItem(new FloppyHat(Utility.RandomNeutralHue())); break;
                                case 1: AddItem(new FeatheredHat(GetRandomHueRange(hueRange))); break;
                            }

                            if (Utility.RandomDouble() < .16)
                                AddItem(new FullApron(Utility.RandomNeutralHue()));

                            if (Utility.RandomBool())
                                AddItem(new GoldRing());

                            break;
                        }
                    case 2: // Rich 
                        {
                            AddItem(new LongPants(GetRandomHueRange(hueRange)));

                            switch (Utility.Random(2))
                            {
                                case 0: AddItem(new FancyShirt(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new Shirt(GetRandomHueRange(hueRange))); break;
                            }

                            int accyHue = GetRandomHueRange(hueRange);

                            if (Utility.RandomBool())
                                AddItem(new Cloak(accyHue));
                            if (Utility.RandomBool())
                                AddItem(new BodySash(accyHue));

                            if (Utility.RandomBool())
                            {
                                BaseHat hat = new TricorneHat(accyHue);
                                if (Utility.RandomBool())
                                    hat = new FeatheredHat(accyHue);
                                AddItem(hat);
                            }

                            if (Utility.RandomDouble() < .333)
                            {
                                m_staff = new GnarledStaff();
                                EquipItem(m_staff);
                            }

                            if (Utility.RandomBool())
                                AddItem(new GoldRing());
                            if (Utility.RandomDouble() < .2)
                                AddItem(new GoldBracelet());

                            break;
                        }
                }
            }
        }
        #endregion

        #region Utility
        public static bool CheckTOD(Mobile m, int i)
        {
            if (i < 1) return true; // Zero = Any
            if (i > 6) return false; // Out of Range
            if (m == null || m.Deleted) return false;

            int hours, minutes;
            Map map = m.Map;
            int x = m.X;
            int y = m.Y;

            Clock.GetTime(map, x, y, out hours, out minutes);

            /* RunUO times: (from LightCycle.cs)
             * 
             * 10:00 PM -> 11:59 PM : Scale to night
             * Midnight ->  3:59 AM : Night
             *  4:00 AM ->  5:59 AM : Scale to day
             *  6:00 AM ->  9:59 PM : Day
             */

            switch (i)
            {
                case 1: return (hours >= 6 && hours < 12); // morning
                case 2: return (hours >= 12 && hours < 18); // afternoon
                case 3: return (hours >= 18 && hours < 24); // evening
                case 4: return (hours >= 0 && hours < 6); // night
                case 5: return (hours >= 5 && hours < 23); // daytime
                case 6: return (!(hours >= 5 && hours < 23)); // nighttime
            }
            return false;
        }

        public override void OnAfterDelete()
        {
            if (m_pausetimer != null)
                m_pausetimer.Stop();
            m_pausetimer = null;

            if (m_greettimer != null)
                m_greettimer.Stop();
            m_greettimer = null;
        }

        public virtual void UpdateGreetings()
        {
            switch ((int)m_attitude)
            {
                case 1: m_greetings = goodGreetings; break;
                case 2: m_greetings = badGreetings; break;
                default:
                case 3: m_greetings = indifGreetings; break;
            }
        }

        public virtual void UpdateKarmaFame()
        {
            this.Fame = (int)m_wealth * 533 + Utility.RandomMinMax(0, 533); // 0 to 1599 based on Wealth
            this.Karma = (int)m_attitude * 533 + Utility.RandomMinMax(0, 533) - 1333; // -800 to 799 based on Attitude
        }

        public static void SetSkills(Townsperson m)
        {
            // Sets all skills to just below trainable level.
            Server.Skills skills = m.Skills;
            for (int i = 0; i < skills.Length; ++i)
                skills[i].Base = 59.9;
        }

        public virtual void AddTrainingSkill() // Adds one random skill to Train (overridable)
        {
            // Training: if ( skill >= 60 ) can teach skill/3 points to max of 42
            // i.e. if (skill == 60 ) can teach 60/3 = 20 points.
            // Utility.Random( 0, 90 ) = 0 to 90 gives 33% chance to teach 20 to 30 points
            // Utility.Random( 30, 60 ) = 30 to 90 gives 50% chance to teach 20 to 30 points
            // Utility.Random( 60, 30 ) = 60 to 90 gives 100% chance to teach 20 to 30 points
            int index = Utility.Random(SkillInfo.Table.Length);
            Skills[index].Base = Utility.Random(30, 60);
        }

        public virtual int GetShoeHue()
        {
            // slight chance of black shoes based on wealth
            if (0.001 > Utility.RandomDouble() * (int)m_wealth)
                return 0;

            return Utility.RandomNeutralHue();
        }

        public virtual int GetRandomHueRange(int range)
        {
            // Used to create color coordinated outfits.
            // Passing 0-4 will return a random hue in a set range
            // 5-9 will return a Netural hue, above 9 is modded to 0-9
            switch (range % 10)
            {
                default:
                case 0: return Utility.RandomNeutralHue();
                case 1: return Utility.RandomBlueHue();
                case 2: return Utility.RandomGreenHue();
                case 3: return Utility.RandomRedHue();
                case 4: return Utility.RandomYellowHue();
            }
        }

        public virtual void DoShirt(int hues)
        {
            switch (Utility.Random(2))
            {
                case 0: AddItem(new Doublet(GetRandomHueRange(hues))); break;
                case 1: AddItem(new Shirt(GetRandomHueRange(hues))); break;
            }
        }

        private static Type typeofItem = typeof(Item);
        public static Item CheckInventory(Mobile from, string str)
        {
            str = str.Trim();
            if (str == null || str == "") return null;

            Type type = SpawnerType.GetType(str);

            if (type.IsSubclassOf(typeofItem))
            {
                // check equiped
                foreach (Item item in from.Items)
                    if (item != null && item.GetType() == type)
                        return item;

                // check pack
                return from.Backpack.FindItemByType(type, true);
            }
            else return null;
        }

        private static Type[] weaponTypes = new Type[]
			{
                typeof( Dagger ), // 0
				typeof( Dagger ),				typeof( Dagger ),			    typeof( Dagger ),//poor good 
				typeof( Club ),			    	typeof( Club ),			        typeof( Club ),//poor bad 
				typeof( Dagger ),				typeof( Club ),			        typeof( Hatchet ),//poor indif
				typeof( QuarterStaff ),			typeof( Kryss ),			    typeof( Katana ),//norm good 
				typeof( BattleAxe ),			typeof( Broadsword ),			typeof( GnarledStaff ),//norm bad
				typeof( Kryss ),			    typeof( Cutlass ),			    typeof( Broadsword ),//norm indif
				typeof( Scimitar ),				typeof( Katana ),			    typeof( QuarterStaff ),//rich good 
				typeof( Kryss ),				typeof( Scimitar ),			    typeof( Broadsword ),//rich bad
				typeof( Katana ),			    typeof( Scimitar ),			    typeof( Longsword ),//rich indif
            };

        public virtual Type GetRandomWeaponType()
        {
            Type type;
            int index = (int)m_wealth * 3 + (int)m_attitude; // 0, 3, 6 + 1, 2, 3
            int num = Utility.Random(((index - 1) * 3 + 1), 3);

            try { type = weaponTypes[num]; }
            catch { type = typeof(Spear); }

            return type;
        }

        public virtual void PackRandomWeapon()
        {
            Item item = Loot.Construct(GetRandomWeaponType());
            if (item is BaseWeapon)
                m_weapon = (BaseWeapon)item;
            else
                m_weapon = new Cleaver();
            PackItem(m_weapon);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, m_criminalAction);

            if (m_combattimer != null)
                return;

            ClearHands();
            EquipItem(m_weapon);

            m_combattimer = Timer.DelayCall(TimeSpan.FromSeconds(60.0), new TimerCallback(CombatCallBack));
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            base.OnDamage(amount, from, willKill);

            if (Hits < HitsMax * .25)
                BeginFlee(TimeSpan.FromSeconds(12));
        }

        private void AddGreetTime(TimeSpan delay)
        {
            if (m_greettimer != null)
                m_greettimer.AddTime(delay);
            else
            {
                m_greettimer = new GreetTimer(this, delay);
                m_greettimer.Start();
            }
        }

        public static void Strip(Mobile from)
        {
            DeleteByLayer(from, Layer.OneHanded);
            DeleteByLayer(from, Layer.TwoHanded);
            DeleteByLayer(from, Layer.Shoes);
            DeleteByLayer(from, Layer.Pants);
            DeleteByLayer(from, Layer.Shirt);
            DeleteByLayer(from, Layer.Helm);
            DeleteByLayer(from, Layer.Gloves);
            DeleteByLayer(from, Layer.Ring);
            DeleteByLayer(from, Layer.Neck);
            DeleteByLayer(from, Layer.Talisman);
            DeleteByLayer(from, Layer.Waist);
            DeleteByLayer(from, Layer.InnerTorso);
            DeleteByLayer(from, Layer.Bracelet);
            DeleteByLayer(from, Layer.MiddleTorso);
            DeleteByLayer(from, Layer.Earrings);
            DeleteByLayer(from, Layer.Arms);
            DeleteByLayer(from, Layer.Cloak);
            DeleteByLayer(from, Layer.OuterTorso);
            DeleteByLayer(from, Layer.OuterLegs);
            DeleteByLayer(from, Layer.InnerLegs);
        }

        private static void DeleteByLayer(Mobile from, Layer layer)
        {
            Item item = from.FindItemOnLayer(layer);

            if (item != null && item.Movable)
                item.Delete();
        }

        private static string nz(string test)
        {
            return test == null ? "" : test;
        }

        private static bool isEmpty(string str)
        {
            return (str == null || str == "");
        }

        #endregion

        #region SpeechHandlers
        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (Utility.RandomBool() && !m.Player) return;

            if (!Hidden && !m_busy && Utility.RandomDouble() < .06 && m.Alive && !m.Hidden && m.InRange(this, ConverseRange))
            {
                Say(m_greetings[Utility.Random(m_greetings.Length)]);

                //timer to prevent spam
                AddGreetTime(PauseDelay);

                return;
            }
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            if (!Hidden && from.Alive && InLOS(from))
			//if (!Hidden && from.Player && from.Alive && InLOS(from))+++
                return true;
            else
                return false;
        }

        public void ResetState()
        {
            if (inConversation == null)
                return;

            inConversation = null;
            this.Direction = oldDirection;
            Frozen = wasFrozen;
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            base.OnSpeech(e);

            Mobile from = e.Mobile;
            int[] keywords = e.Keywords;
            string lc_speech = (e.Speech).ToLower();

            string arg0 = this.Name;
            string arg1 = from.NameMod == null ? from.Name : from.NameMod;
            string arg2 = this.Region.Name;

            if (from.Hidden)
            {
                // TODO: Enable Localization
                Emote("*looks startled*");
                e.Handled = true;
                return;
            }

            if (!e.Handled && from.InRange(this, ConverseRange))
            {
                e.Handled = true;
                if (inConversation == null)
                {
                    inConversation = from;
                    oldDirection = this.Direction;
                    wasFrozen = Frozen;
                }

                if (m_pausetimer != null && m_pausetimer.Running)
                {
                    m_pausetimer.EndTime = DateTime.UtcNow + PauseDelay;
                }
                else
                {
                    m_pausetimer = new PauseTimer(this, PauseDelay);
                    m_pausetimer.Start();
                }
                this.Direction = GetDirectionTo(from);

                if (synchronousCall)
                {
                    Response response = new Response();
                    SpeechResponse ret = response.GetResponse(lc_speech, from, this);
                    this.SpeechHandler(ret);
                }
                else // Make Asynchronous call to Datahandler
                {
                    // Create the object to do the work, and a delegate to the worker method.
                    Response response = new Response();
                    GetResponseDelegate rd = new GetResponseDelegate(response.GetResponse);

                    // Define the AsyncCallback delegate.
                    AsyncCallback cb = new AsyncCallback(this.SpeechCallback);

                    if (Logging == LogLevel.Debug)
                        TownspersonLogging.WriteLine(this, "Making Asynchronous call on: \"{0}\"", lc_speech);

                    // Asynchronously invoke the GetResponse method.
                    IAsyncResult ar = rd.BeginInvoke(lc_speech, from, this, cb, null);
                }
            }
        }

        // Return method of Asynchronous call to Datahandler
        public void SpeechCallback(IAsyncResult ar)
        {
            // Retrieve the delegate.
            GetResponseDelegate rd = (GetResponseDelegate)((AsyncResult)ar).AsyncDelegate;

            // Call EndInvoke on the delegate to retrieve the results.
            SpeechResponse ret = rd.EndInvoke(ar);

            // Process the return value
	    try
	    {
		this.SpeechHandler(ret);
	    }
	    catch
	    {
		if (Logging == LogLevel.Basic || Logging == LogLevel.Debug)
		    TownspersonLogging.WriteLine(this, "Averted a crash trying to process Speech Event: \"{0}\"", ret.Response);
	    }

            if (Logging == LogLevel.Debug)
                TownspersonLogging.WriteLine(ret.Speaker, "Return from Asynchronous call: \"{0}\"", ret.Response);
        }

        public virtual void SpeechHandler(SpeechResponse sr)
        {
            string m_name = (sr.Speaker.NameMod == null ? sr.Speaker.Name : sr.Speaker.NameMod);
            string m_response = String.Format(sr.Response, this.Name, m_name, this.Region);

            if (!isEmpty(sr.Response))
                Say(m_response);

            if (sr.Animation > 0)
                Animate(sr.Animation, 5, 1, true, false, 0);

            if (sr.Reaction > 0)
            {
                ReactionCallBackState rcbs = new ReactionCallBackState(sr.Speaker, sr.Reaction);
                Timer.DelayCall(TimeSpan.FromMilliseconds(1800), new TimerStateCallback(ReactionCallBack), rcbs);
            }

            if (Logging == LogLevel.Basic || Logging == LogLevel.Debug)
                TownspersonLogging.WriteLine(this, "Responding to Speech Event: \"{0}\"", m_response);

            if (!isEmpty(sr.Reward))
            {
                if (Logging >= LogLevel.Basic)
                    TownspersonLogging.WriteLine(sr.Speaker, "{0} in {1} Creating {2}", this.Name, this.Region, sr.Reward);

                Type type = SpawnerType.GetType(sr.Reward);

                try
                {
                    object o = Activator.CreateInstance(type);

                    if (o is Item)
                    {
                        Item item = (Item)o;
                        sr.Speaker.AddToBackpack(item);
                    }
                    else if (o is Mobile)
                    {
                        Mobile mob = (Mobile)o;
                        mob.MoveToWorld(this.Location, this.Map);
                    }
                }
                catch
                {
                    TownspersonLogging.WriteLine(sr.Speaker, "{0} Exception Caught creating {1}", this.Name, sr.Reward);
                    sr.Speaker.SendMessage("Exception Caught creating " + sr.Reward); // debugging
                }

            }

            if (!isEmpty(sr.DelObject))
            {
                if (Logging >= LogLevel.Basic)
                    TownspersonLogging.WriteLine(sr.Speaker, "{0} in {1} Deleting {2}", this.Name, this.Region, sr.DelObject);

                Type type = SpawnerType.GetType(sr.DelObject);

                bool ActionTaken = false;

                try
                {
                    for (int i = 0; i < sr.Speaker.Items.Count; ++i)
                    {
                        Item item = (Item)sr.Speaker.Items[i];

                        if (item.GetType() == type)
                        {
                            item.Consume();
                            ActionTaken = true;
                            break;
                        }
                    }
                    if (!ActionTaken)
                    {
                        sr.Speaker.Backpack.ConsumeTotal(type, 1, true);
                    }
                }
                catch
                {
                    TownspersonLogging.WriteLine(sr.Speaker, "{0} Exception Caught consuming {1}", this.Name, sr.DelObject);
                    sr.Speaker.SendMessage("Exception Caught consuming " + sr.DelObject); // debugging
                }
            }
        }
        #endregion

        # region Serialize
        public Townsperson(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)3);
            writer.Write((string)m_tagText);
            writer.Write((Item)m_weapon);
            writer.Write((Item)m_staff);
            writer.Write((bool)m_criminalAction);
            writer.Write((int)m_attitude);
            writer.Write((int)m_wealth);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            Item i1 = null;
            Item i2 = null;

            switch (version)
            {
                case 3:
                    {
                        m_tagText = reader.ReadString();
                        goto case 2;
                    }
                case 2:
                    {
                        i1 = reader.ReadItem();
                        i2 = reader.ReadItem();
                        m_criminalAction = reader.ReadBool();
                        goto case 1;
                    }
                case 1:
                    {
                        m_attitude = (Attitude)reader.ReadInt();
                        m_wealth = (Wealth)reader.ReadInt();
                        break;
                    }
                case 0:
                    {
                        // obsolete version
                        break;
                    }
            }

            if (i1 != null && i1 is BaseWeapon)
                m_weapon = (BaseWeapon)i1;
            else
                m_weapon = new ButcherKnife();

            if (i2 != null && i2 is BaseWeapon)
                m_staff = (BaseWeapon)i2;

            UpdateGreetings();
        }
        # endregion

        # region Overrides
        public override void OnThink()
        {
            if (Combatant == null && Hits < HitsMax && Utility.RandomBool())
                Hits++;

            base.OnThink(); 
        }
        # endregion

        # region Timer CallBacks
        private void ReactionCallBack(object obj)
        {
            ReactionCallBackState state;

            if (obj is ReactionCallBackState)
                state = (ReactionCallBackState)obj;
            else
                return;

            Mobile speaker = state.Mobile;
            int reactID = state.Reaction;
            state = null;

            if (reactID < 1 || reactID > 6 || !speaker.Player) return;
            switch (reactID)
            {
                default: return;
                case 1: // Attack
                    AttackIsCriminal = false;
                    Combatant = speaker;
                    AddGreetTime(TimeSpan.FromSeconds(60));
                    break;
                case 2: // Flee
                    FocusMob = speaker;
                    BeginFlee(TimeSpan.FromSeconds(18));
                    AddGreetTime(TimeSpan.FromSeconds(18));
                    break;
                case 3: // Criminal
                    AttackIsCriminal = false;
                    Criminal = true;
                    break;
                case 4: // Hide
                    //set to GM and allow to roam
                    m_accessLevel = this.AccessLevel;
                    AccessLevel = AccessLevel.GameMaster;
                    Hidden = true;
                    Timer.DelayCall(TimeSpan.FromMinutes(5), new TimerCallback(UnHideCallBack));
                    AddGreetTime(TimeSpan.FromMinutes(5));
                    //if (m_pausetimer != null)
                    //    m_pausetimer.EndTime = DateTime.UtcNow + TimeSpan.FromMinutes(5);
                    break;
                case 5: // Die
                    Kill();
                    break;
                case 6: // Delete
                    Delete();
                    break;
            }
        }

        private void UnHideCallBack()
        {
            Hidden = false;
            AccessLevel = m_accessLevel;
        }

        private void CombatCallBack()
        {
            if (Combatant != null)
            {
                m_combattimer = Timer.DelayCall(TimeSpan.FromSeconds(30.0), new TimerCallback(CombatCallBack));
                return;
            }

            m_combattimer = null;
            ClearHand(m_weapon);
            Warmode = false;

            if (m_staff != null)
                EquipItem(m_staff);
        }
        # endregion

        # region Timers
        private class PauseTimer : Timer
        {
            private Mobile m_from;
            private DateTime m_endtime;

            public DateTime EndTime
            {
                get { return m_endtime; }
                set { m_endtime = value; }
            }

            public PauseTimer(Mobile from, TimeSpan delay)
                : base(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
            {
                m_from = from;
                m_endtime = DateTime.UtcNow + delay;

                from.Frozen = true;

                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                if (DateTime.UtcNow >= m_endtime)
                {
                    if (m_from is Townsperson)
                        ((Townsperson)m_from).ResetState();

                    this.Stop();
                }
            }
        }

        private class GreetTimer : Timer
        {
            private Townsperson m_from;
            private DateTime m_endtime;

            public void AddTime(TimeSpan value)
            {
                m_endtime += value;
            }

            public GreetTimer(Townsperson from, TimeSpan delay)
                : base(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
            {
                m_from = from;
                m_endtime = DateTime.UtcNow + delay;

                from.Busy = true;

                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                if (DateTime.UtcNow >= m_endtime)
                {
                    if (m_from is Townsperson)
                        ((Townsperson)m_from).Busy = false;

                    this.Stop();
                }
            }
        }
        # endregion
    }
}
