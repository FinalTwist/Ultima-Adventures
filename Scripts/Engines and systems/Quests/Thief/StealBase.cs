using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using System.Collections.Generic;
using Server.Network;

namespace Server.Items
{

	public enum PuzzleChestCylinder
    {
        None = 0xE73,
        LightBlue = 0x186F,
        Blue = 0x186A,
        Green = 0x186B,
        Orange = 0x186C,
        Purple = 0x186D,
        Red = 0x186E,
        DarkBlue = 0x1869,
        Yellow = 0x1870
    }

    public class PuzzleChestSolution
    {
        public const int Length = 5;
        private readonly PuzzleChestCylinder[] m_Cylinders = new PuzzleChestCylinder[Length];
        public PuzzleChestSolution()
        {
            for (int i = 0; i < m_Cylinders.Length; i++)
            {
                m_Cylinders[i] = RandomCylinder();
            }
        }

        public PuzzleChestSolution(PuzzleChestCylinder first, PuzzleChestCylinder second, PuzzleChestCylinder third, PuzzleChestCylinder fourth, PuzzleChestCylinder fifth)
        {
            First = first;
            Second = second;
            Third = third;
            Fourth = fourth;
            Fifth = fifth;
        }

        public PuzzleChestSolution(PuzzleChestSolution solution)
        {
            for (int i = 0; i < m_Cylinders.Length; i++)
            {
                m_Cylinders[i] = solution.m_Cylinders[i];
            }
        }

        public PuzzleChestSolution(GenericReader reader)
        {
            int version = reader.ReadEncodedInt();

            int length = reader.ReadEncodedInt();
            for (int i = 0; i < m_Cylinders.Length; i++)
            {
                if (i < length)
                {
                    PuzzleChestCylinder cylinder = (PuzzleChestCylinder)reader.ReadInt();

                    if (i < m_Cylinders.Length)
                        m_Cylinders[i] = cylinder;
                }
                else if (i < m_Cylinders.Length)
                {
                    m_Cylinders[i] = RandomCylinder();
                }
                else
                {
                    break;
                }
            }
        }

        public PuzzleChestCylinder[] Cylinders
        {
            get
            {
                return m_Cylinders;
            }
        }
        public PuzzleChestCylinder First
        {
            get
            {
                return m_Cylinders[0];
            }
            set
            {
                m_Cylinders[0] = value;
            }
        }
        public PuzzleChestCylinder Second
        {
            get
            {
                return m_Cylinders[1];
            }
            set
            {
                m_Cylinders[1] = value;
            }
        }
        public PuzzleChestCylinder Third
        {
            get
            {
                return m_Cylinders[2];
            }
            set
            {
                m_Cylinders[2] = value;
            }
        }
        public PuzzleChestCylinder Fourth
        {
            get
            {
                return m_Cylinders[3];
            }
            set
            {
                m_Cylinders[3] = value;
            }
        }
        public PuzzleChestCylinder Fifth
        {
            get
            {
                return m_Cylinders[4];
            }
            set
            {
                m_Cylinders[4] = value;
            }
        }
        public static PuzzleChestCylinder RandomCylinder()
        {
            switch ( Utility.Random(8) )
            {
                case 0:
                    return PuzzleChestCylinder.LightBlue;
                case 1:
                    return PuzzleChestCylinder.Blue;
                case 2:
                    return PuzzleChestCylinder.Green;
                case 3:
                    return PuzzleChestCylinder.Orange;
                case 4:
                    return PuzzleChestCylinder.Purple;
                case 5:
                    return PuzzleChestCylinder.Red;
                case 6:
                    return PuzzleChestCylinder.DarkBlue;
                default:
                    return PuzzleChestCylinder.Yellow;
            }
        }

        public bool Matches(PuzzleChestSolution solution, out int cylinders, out int colors)
        {
            cylinders = 0;
            colors = 0;

            bool[] matchesSrc = new bool[solution.m_Cylinders.Length];
            bool[] matchesDst = new bool[solution.m_Cylinders.Length];

            for (int i = 0; i < m_Cylinders.Length; i++)
            {
                if (m_Cylinders[i] == solution.m_Cylinders[i])
                {
                    cylinders++;

                    matchesSrc[i] = true;
                    matchesDst[i] = true;
                }
            }

            for (int i = 0; i < m_Cylinders.Length; i++)
            {
                if (!matchesSrc[i])
                {
                    for (int j = 0; j < solution.m_Cylinders.Length; j++)
                    {
                        if (m_Cylinders[i] == solution.m_Cylinders[j] && !matchesDst[j])
                        {
                            colors++;

                            matchesDst[j] = true;
                        }
                    }
                }
            }

            return cylinders == m_Cylinders.Length;
        }

        public virtual void Serialize(GenericWriter writer)
        {
            writer.WriteEncodedInt((int)0); // version

            writer.WriteEncodedInt((int)m_Cylinders.Length);
            for (int i = 0; i < m_Cylinders.Length; i++)
            {
                writer.Write((int)m_Cylinders[i]);
            }
        }
    }

    public class PuzzleChestSolutionAndTime : PuzzleChestSolution
    {
        private readonly DateTime m_When;
        public PuzzleChestSolutionAndTime(DateTime when, PuzzleChestSolution solution)
            : base(solution)
        {
            m_When = when;
        }

        public PuzzleChestSolutionAndTime(GenericReader reader)
            : base(reader)
        {
            int version = reader.ReadEncodedInt();

            m_When = reader.ReadDeltaTime();
        }

        public DateTime When
        {
            get
            {
                return m_When;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version

            writer.WriteDeltaTime(m_When);
        }
    }



	public class StealBase : BaseAddon
	{
		public int BoxType;
		public int BoxColor;
		public int PedType;
		public string BoxOrigin;
		public string BoxCarving;

		[CommandProperty(AccessLevel.Owner)]
		public int Box_Type { get { return BoxColor; } set { BoxColor = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Box_Color { get { return BoxType; } set { BoxType = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Ped_Type { get { return PedType; } set { PedType = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Box_Origin { get { return BoxOrigin; } set { BoxOrigin = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Box_Carving { get { return BoxCarving; } set { BoxCarving = value; InvalidateProperties(); } }

		public int m_Tries;
		public int Tries{ get{ return m_Tries; } set{ m_Tries = value; } }

        public const int HintsCount = 3;
        public readonly TimeSpan CleanupTime = TimeSpan.FromHours(1.0);
        private readonly Dictionary<Mobile, PuzzleChestSolutionAndTime> m_Guesses = new Dictionary<Mobile, PuzzleChestSolutionAndTime>();
        private PuzzleChestSolution m_Solution;
        private PuzzleChestCylinder[] m_Hints = new PuzzleChestCylinder[HintsCount];

        public PuzzleChestSolution Solution
        {
            get
            {
                return m_Solution;
            }
            set
            {
                m_Solution = value;
                InitHints();
            }
        }
        public PuzzleChestCylinder[] Hints
        {
            get
            {
                return m_Hints;
            }
        }
        public PuzzleChestCylinder FirstHint
        {
            get
            {
                return m_Hints[0];
            }
            set
            {
                m_Hints[0] = value;
            }
        }
        public PuzzleChestCylinder SecondHint
        {
            get
            {
                return m_Hints[1];
            }
            set
            {
                m_Hints[1] = value;
            }
        }
        public PuzzleChestCylinder ThirdHint
        {
            get
            {
                return m_Hints[2];
            }
            set
            {
                m_Hints[2] = value;
            }
        }

		[ Constructable ]
		public StealBase()
		{
            Solution = new PuzzleChestSolution();

			int iZ = 0;
			int iZ1 = 0;
			int iZ2 = 0;

			if ( Utility.RandomMinMax( 1, 3 ) > 1 )
			{
				iZ1 = 5;
				iZ2 = 6;
				PedType = 13042;
			}
			else
			{
				iZ1 = 10;
				iZ2 = 10;
				PedType = 0x1223;
			}

				string sEtch = "etched";
				string sPed = "an ornately ";
				switch( Utility.RandomMinMax( 0, 10 ) )
				{
					case 0: sPed = "an ornately ";		break;
					case 1: sPed = "a beautifully ";	break;
					case 2: sPed = "an expertly ";		break;
					case 3: sPed = "an artistically ";	break;
					case 4: sPed = "an exquisitely ";	break;
					case 5: sPed = "a decoratively ";	break;
					case 6: sPed = "an ancient ";		break;
					case 7: sPed = "an old ";			break;
					case 8: sPed = "an unusually ";		break;
					case 9: sPed = "a curiously ";		break;
					case 10: sPed = "an oddly ";		break;
				}
				sPed = sPed + "carved pedestal";

				int iColor = 0;
				int iThing = 0x9A8;
				string sArty = "a strange";
				switch( Utility.RandomMinMax( 0, 6 ) )
				{
					case 0: sArty = "an odd ";		break;
					case 1: sArty = "an unusual ";	break;
					case 2: sArty = "a bizarre ";	break;
					case 3: sArty = "a curious ";	break;
					case 4: sArty = "a peculiar ";	break;
					case 5: sArty = "a strange ";	break;
					case 6: sArty = "a weird ";		break;
				}

				string sThing = "metal box";
				switch( Utility.RandomMinMax( 0, 6 ) )
				{
					case 0: iThing = 0x9AA; sThing = "metal box"; break;
					case 1: iThing = 0xE7D; sThing = "metal box"; break;
					case 2: iThing = 0x9AA; sThing = "wooden box"; break;
					case 3: iThing = 0xE7D; sThing = "wooden box"; break;
					case 4: iThing = 0xE76; sThing = "bag"; break;
					case 5: iThing = 0xE76; sThing = "sack"; break;
					case 6: iThing = 0xE76; sThing = "pouch"; break;
				}

				if ( sThing == "metal box")
				{
					BoxType = 1;
					iZ = iZ1;
					sEtch = "etched";
					switch ( Utility.RandomMinMax( 0, 19 ) ) 
					{
						case 0: iColor = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); sThing = "dull copper box";	break;
						case 1: iColor = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); sThing = "shadow iron box";	break;
						case 2: iColor = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); sThing = "copper box";			break;
						case 3: iColor = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); sThing = "bronze box";			break;
						case 4: iColor = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); sThing = "golden box";				break;
						case 5: iColor = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); sThing = "agapite box";			break;
						case 6: iColor = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); sThing = "verite box";			break;
						case 7: iColor = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); sThing = "valorite box";		break;
						case 8: iColor = MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); sThing = "silver box";			break;
						case 9: iColor = MaterialInfo.GetMaterialColor( "emerald", "classic", 0 ); sThing = "emerald box";			break;
						case 10: iColor = MaterialInfo.GetMaterialColor( "jade", "classic", 0 ); sThing = "jade box";				break;
						case 11: iColor = MaterialInfo.GetMaterialColor( "onyx", "classic", 0 ); sThing = "onyx box";				break;
						case 12: iColor = MaterialInfo.GetMaterialColor( "ruby", "classic", 0 ); sThing = "ruby box";				break;
						case 13: iColor = MaterialInfo.GetMaterialColor( "sapphire", "classic", 0 ); sThing = "sapphire box";		break;
						case 14: iColor = 0x317; sThing = "iron box";																break;
						case 15: iColor = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ); sThing = "mithril box";			break;
						case 16: iColor = MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); sThing = "brass box";				break;
						case 17: iColor = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); sThing = "nepturite box";		break;
						case 18: iColor = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); sThing = "obsidian box";		break;
						case 19: iColor = MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); sThing = "steel box";				break;
					}
				}
				else if ( sThing == "wooden box")
				{
					BoxType = 2;
					iZ = iZ1;
					sEtch = "carved";
					switch ( Utility.RandomMinMax( 0, 14 ) ) 
					{
						case 0: iColor = 0; 													sThing = "wooden box";			break;
						case 1: iColor = MaterialInfo.GetMaterialColor( "oak", "", 0 ); 		sThing = "oak wood box";		break;
						case 2: iColor = MaterialInfo.GetMaterialColor( "ash", "", 0 ); 		sThing = "ash wood box";		break;
						case 3: iColor = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); 		sThing = "cherry wood box";		break;
						case 4: iColor = MaterialInfo.GetMaterialColor( "walnut", "", 0 ); 		sThing = "walnut wood box";		break;
						case 5: iColor = MaterialInfo.GetMaterialColor( "golden oak", "", 0 ); 	sThing = "golden oak wood box";	break;
						case 6: iColor = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); 		sThing = "ebony wood box";		break;
						case 7: iColor = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); 	sThing = "hickory wood box";	break;
						case 8: iColor = MaterialInfo.GetMaterialColor( "pine", "", 0 ); 		sThing = "pine wood box";		break;
						case 9: iColor = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); 	sThing = "rosewood box";		break;
						case 10: iColor = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); 	sThing = "mahogany wood box";	break;
						case 11: iColor = MaterialInfo.GetMaterialColor( "elven", "", 0 ); 		sThing = "elven wood box";		break;
						case 12: iColor = MaterialInfo.GetMaterialColor( "petrified", "", 0 ); 	sThing = "petrified wood box";	break;
						case 13: iColor = MaterialInfo.GetMaterialColor( "ghostwood", "", 0 ); 	sThing = "ghost wood box";		break;
						case 14: iColor = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); 	sThing = "drift wood box";		break;
					}
				}
				else
				{
					BoxType = 3;
					iZ = iZ2;
					sEtch = "etched";
					switch ( Utility.RandomMinMax( 0, 10 ) ) 
					{
						case 0: iColor = MaterialInfo.GetMaterialColor( "frozen", "", 0 ); sThing = "frozen leather " + sThing;	break;
						case 1: iColor = MaterialInfo.GetMaterialColor( "volcanic", "", 0 ); sThing = "volcanic leather " + sThing;	break;
						case 2: iColor = MaterialInfo.GetMaterialColor( "dinosaur", "", 0 ); sThing = "dinosaur leather " + sThing;	break;
						case 3: iColor = MaterialInfo.GetMaterialColor( "serpent", "", 0 ); sThing = "serpent leather " + sThing;	break;
						case 4: iColor = MaterialInfo.GetMaterialColor( "lizard", "", 0 ); sThing = "lizard leather " + sThing;	break;
						case 5: iColor = MaterialInfo.GetMaterialColor( "deep sea", "", 0 ); sThing = "deep sea leather " + sThing;	break;
						case 6: iColor = MaterialInfo.GetMaterialColor( "draconic", "", 0 ); sThing = "draconic leather " + sThing;	break;
						case 7: iColor = MaterialInfo.GetMaterialColor( "hellish", "", 0 ); sThing = "hellish leather " + sThing;	break;
						case 8: iColor = MaterialInfo.GetMaterialColor( "goliath", "", 0 ); sThing = "goliath leather " + sThing;	break;
						case 9: iColor = MaterialInfo.GetMaterialColor( "necrotic", "", 0 ); sThing = "necrotic leather " + sThing;	break;
						case 10: iColor = 0; sThing = "leather " + sThing;	break;
					}
				}
				sThing = sArty + sThing;

			AddComplexComponent( (BaseAddon) this, iThing, 0, 0, iZ, iColor, -1, sThing, 1);
			AddComplexComponent( (BaseAddon) this, 5703, 0, 0, 0, 0, 29, sPed, 1);
			AddComplexComponent( (BaseAddon) this, PedType, 0, 0, 0, 0, -1, "", 1);

			BoxOrigin = sThing;
			BoxColor = iColor;

			///// DO THE CARVINGS ON THE BAG OR BOX ///////////////////////////////////////////////////////////
			string sLanguage = "pixie";
			switch( Utility.RandomMinMax( 0, 28 ) )
			{
				case 0: sLanguage = "balron"; break;
				case 1: sLanguage = "pixie"; break;
				case 2: sLanguage = "centaur"; break;
				case 3: sLanguage = "demonic"; break;
				case 4: sLanguage = "dragon"; break;
				case 5: sLanguage = "dwarvish"; break;
				case 6: sLanguage = "elven"; break;
				case 7: sLanguage = "fey"; break;
				case 8: sLanguage = "gargoyle"; break;
				case 9: sLanguage = "cyclops"; break;
				case 10: sLanguage = "gnoll"; break;
				case 11: sLanguage = "goblin"; break;
				case 12: sLanguage = "gremlin"; break;
				case 13: sLanguage = "druidic"; break;
				case 14: sLanguage = "tritun"; break;
				case 15: sLanguage = "minotaur"; break;
				case 16: sLanguage = "naga"; break;
				case 17: sLanguage = "ogrish"; break;
				case 18: sLanguage = "orkish"; break;
				case 19: sLanguage = "sphinx"; break;
				case 20: sLanguage = "treekin"; break;
				case 21: sLanguage = "trollish"; break;
				case 22: sLanguage = "undead"; break;
				case 23: sLanguage = "vampire"; break;
				case 24: sLanguage = "dark elf"; break;
				case 25: sLanguage = "magic"; break;
				case 26: sLanguage = "human"; break;
				case 27: sLanguage = "symbolic"; break;
				case 28: sLanguage = "runic"; break;
			}

			string sPart = "strange ";
			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0:	sPart = "strange ";	break;
				case 1:	sPart = "odd ";		break;
				case 2:	sPart = "ancient ";	break;
				case 3:	sPart = "long dead ";	break;
				case 4:	sPart = "cryptic ";	break;
				case 5:	sPart = "mystical ";	break;
			}

			string sPart2 = " symbols ";
			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0:	sPart2 = " symbols ";	break;
				case 1:	sPart2 = " words ";		break;
				case 2:	sPart2 = " writings ";	break;
				case 3:	sPart2 = " glyphs ";	break;
				case 4:	sPart2 = " pictures ";	break;
				case 5:	sPart2 = " runes ";		break;
			}

			BoxCarving = "with " + sPart + sLanguage + sPart2 + sEtch + " on it";
		}

		public StealBase( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public override void OnComponentUsed( AddonComponent ac, Mobile from )
		{
			if ( !(from is PlayerMobile))
				return;

			if ( from.Backpack.FindItemByType( typeof ( MuseumBook ) ) != null && !from.Blessed && from.InRange( GetWorldLocation(), 2 ) )
			{
				MuseumBook.FoundItem( from, 2 );
			}

			if ( from.Backpack.FindItemByType( typeof ( QuestTome ) ) != null && !from.Blessed && from.InRange( GetWorldLocation(), 2 ) )
			{
				QuestTome.FoundItem( from, 2, null );
			}

			if ( from.Blessed )
			{
				from.SendMessage( "You cannot open that while in this state." );
			}
			else if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.SendMessage( "You will have to get closer to try and use the mechanism." );
			}
			
			else if ( m_Tries > Utility.RandomMinMax( 15, 20) )
			{
				Item Pedul = new StealBaseEmpty();
				Pedul.ItemID = PedType;
				Pedul.MoveToWorld (new Point3D(this.X, this.Y, this.Z), this.Map);
				from.SendMessage( "You failed too many times and the prize is gone." );
				this.Delete();
			}			
			else 
			{
				StealBase SB = (StealBase)this;
                PuzzleChestSolution solution = SB.GetLastGuess(from);
                if (solution != null)
                    solution = new PuzzleChestSolution(solution);
                else
                    solution = new PuzzleChestSolution(PuzzleChestCylinder.None, PuzzleChestCylinder.None, PuzzleChestCylinder.None, PuzzleChestCylinder.None, PuzzleChestCylinder.None);

                from.CloseGump(typeof(PuzzleGump));
                from.CloseGump(typeof(StatusGump));
                from.SendGump(new PuzzleGump(from, SB, solution, 0));
			}

		}

        public PuzzleChestSolutionAndTime GetLastGuess(Mobile m)
        {
            PuzzleChestSolutionAndTime pcst = null;
            m_Guesses.TryGetValue(m, out pcst);
            return pcst;
        }

        public void SubmitSolution(Mobile m, PuzzleChestSolution solution)
        {
            int correctCylinders, correctColors;

            if (solution.Matches(Solution, out correctCylinders, out correctColors))
            {
                // when you succeed
				SuccessGet ( m );
                
            }
            else
            {
				m_Tries++;
                m_Guesses[m] = new PuzzleChestSolutionAndTime(DateTime.UtcNow, solution);

                m.SendGump(new StatusGump(correctCylinders, correctColors));

                DoDamage(m);
            }
        }

		public virtual void SuccessGet( Mobile m )
		{
					if ( BoxType == 1 )
					{
						Item Bags = new StealMetalBox();
						StealMetalBox bag = (StealMetalBox)Bags;
						bag.BoxColor = BoxColor;
						bag.Hue = BoxColor;
						bag.Name = BoxOrigin;
						bag.BoxName = BoxOrigin;
						bag.BoxMarkings = BoxCarving;
						FillMeUp( bag, m );
						m.AddToBackpack( bag );
					}
					else if ( BoxType == 2 )
					{
						Item Bags = new StealBox();
						StealBox bag = (StealBox)Bags;
						bag.BoxColor = BoxColor;
						bag.Hue = BoxColor;
						bag.Name = BoxOrigin;
						bag.BoxName = BoxOrigin;
						bag.BoxMarkings = BoxCarving;
						FillMeUp( bag, m );
						m.AddToBackpack( bag );
					}
					else
					{
						Item Bags = new StealBag();
						StealBag bag = (StealBag)Bags;
						bag.BagColor = BoxColor;
						bag.Hue = BoxColor;
						bag.Name = BoxOrigin;
						bag.BagName = BoxOrigin;
						bag.BagMarkings = BoxCarving;
						FillMeUp( bag, m );
						m.AddToBackpack( bag );

					}
					Item Pedul = new StealBaseEmpty();
					Pedul.ItemID = PedType;
					Pedul.MoveToWorld (new Point3D(this.X, this.Y, this.Z), this.Map);
					m.SendMessage( "You manage to disarm the devious device." );
					LoggingFunctions.LogStandard( m, "has unlocked a puzzle pedestal." );

                    int difficulty = Misc.MyServerSettings.GetDifficultyLevel( m.Location, m.Map );

					if (difficulty < 1 ) difficulty = 1;

                    int fame = 150 * difficulty;

					Titles.AwardFame( m, fame, true );

					this.Delete();


		}

        public virtual void DoDamage(Mobile from)
        {

				if ( from.CheckSkill( SkillName.RemoveTrap, Utility.RandomMinMax(40, 60), 125 ) )
				{
					from.SendMessage( "You pull back just in time to avoid a trap!" );
					return;
				}

 					int nReaction = Utility.RandomMinMax( 1, 3 );
					int min = from.Hits/3;
					int max = (int)( (10+ (double)from.Hits/ ( 1- ( (double)from.PhysicalResistance / 100))) - Math.Ceiling((double)from.Hits * ((double)from.Skills[SkillName.RemoveTrap].Value /500) ) );
					
					if (max < min)
						max = min +10;

					if ( nReaction == 1 )
					{
						from.FixedParticles( 0x374A, 10, 15, 5021, EffectLayer.Waist );
						from.PlaySound( 0x205 );
						int nPoison = Utility.RandomMinMax( 0, 10 );
							if ( nPoison > 8 ) { from.ApplyPoison( from, Poison.Lethal ); }
							else if ( nPoison > 6 ) { from.ApplyPoison( from, Poison.Deadly ); }
							else if ( nPoison > 3 ) { from.ApplyPoison( from, Poison.Greater ); }
							else { from.ApplyPoison( from, Poison.Regular ); }
						from.SendMessage( "You accidentally trigger a poison trap!" );
						LoggingFunctions.LogTraps( from, "a pedestal poison trap" );
					}
					else if ( nReaction == 2 )
					{
						from.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
						from.PlaySound( 0x208 );
						Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( min, (int)( (max +20) / (1+ (from.Skills[SkillName.MagicResist].Value / 125) ) ) ), 0, 100, 0, 0, 0 );
						from.SendMessage( "You accidentally trigger a flame trap!" );
						LoggingFunctions.LogTraps( from, "a pedestal fire trap" );
					}
					else if ( nReaction == 3 )
					{
						from.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
						from.PlaySound( 0x307 );
						Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( min, (int)( (max) / (1+ (from.Skills[SkillName.RemoveTrap].Value / 125) ) )), 0, 100, 0, 0, 0 );
						from.SendMessage( "You accidentally trigger an explosion trap!" );
						LoggingFunctions.LogTraps( from, "a pedestal explosion trap" );
					}
        }

        public void CleanupGuesses()
        {
            List<Mobile> toDelete = new List<Mobile>();

            foreach (KeyValuePair<Mobile, PuzzleChestSolutionAndTime> kvp in m_Guesses)
            {
                if (DateTime.UtcNow - kvp.Value.When > CleanupTime)
                    toDelete.Add(kvp.Key);
            }

            foreach (Mobile m in toDelete)
                m_Guesses.Remove(m);
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 1 ); // Version
            if (BoxType == null)
                Console.WriteLine ( "BoxType is null");
            writer.Write( BoxType );
            if (BoxColor == null)
                Console.WriteLine ( "Boxcolor is null");
            writer.Write( BoxColor );
            if (PedType== null)
                Console.WriteLine ( "pedtype is null");
            writer.Write( PedType );
            if (BoxOrigin == null)
                Console.WriteLine ( "Boxorigin is null");
            writer.Write( BoxOrigin );
            if (BoxCarving == null)
                Console.WriteLine ( "Boxcarving is null");
            writer.Write( BoxCarving );

			//1
            if (m_Solution == null)
                m_Solution = new PuzzleChestSolution();
            m_Solution.Serialize(writer);

            if (m_Hints == null)
                Console.WriteLine ( "m_hints is null");
            writer.WriteEncodedInt((int)m_Hints.Length);
            for (int i = 0; i < m_Hints.Length; i++)
            {
                writer.Write((int)m_Hints[i]);
            }

            if (m_Guesses == null)
                Console.WriteLine ( "m_guesses is null");
            writer.WriteEncodedInt((int)m_Guesses.Count);
            foreach (KeyValuePair<Mobile, PuzzleChestSolutionAndTime> kvp in m_Guesses)
            {
                writer.Write(kvp.Key);
                kvp.Value.Serialize(writer);
            }
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            BoxType = reader.ReadInt();
            BoxColor = reader.ReadInt();
            PedType = reader.ReadInt();
            BoxOrigin = reader.ReadString();
            BoxCarving = reader.ReadString();

			if (version == 1)
			{
				m_Solution = new PuzzleChestSolution(reader);

				int length = reader.ReadEncodedInt();
				for (int i = 0; i < length; i++)
				{
					PuzzleChestCylinder cylinder = (PuzzleChestCylinder)reader.ReadInt();

					if (length == m_Hints.Length)
						m_Hints[i] = cylinder;
				}
				if (length != m_Hints.Length)
					InitHints();

				int guesses = reader.ReadEncodedInt();
				for (int i = 0; i < guesses; i++)
				{
					Mobile m = reader.ReadMobile();
					PuzzleChestSolutionAndTime sol = new PuzzleChestSolutionAndTime(reader);

					m_Guesses[m] = sol;
				}

			}
		}

		public void FillMeUp( Container box, Mobile from )
		{
			Item i = null;
			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
						double difficulty = (double)Misc.MyServerSettings.GetDifficultyLevel( from.Location, from.Map );

						if (difficulty == 0 ) // divide by zero check
							difficulty = 1;

						double chance = 1 / difficulty; //difficulty is 0-5, so harder dungeons will be harder to steal from
						if (Utility.RandomDouble() > chance)
                        {
                            i = Loot.RandomArty();
				            box.DropItem(i);
                        }

				//BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID );
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				i = DungeonLoot.RandomSlayer();
				box.DropItem(i);
				//BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID );
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{


						double difficulty = (double)Misc.MyServerSettings.GetDifficultyLevel( from.Location, from.Map );

						if (difficulty == 0 ) // divide by zero check
							difficulty = 1;

						double chance = 1 / difficulty; //difficulty is 0-5, so harder dungeons will be greater chance
						if (Utility.RandomDouble() > chance)
                        {
                            i = Loot.RandomSArty();
				            box.DropItem(i);
                        }

				//BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID );
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				if ( Server.Misc.GetPlayerInfo.EvilPlay( from ) == true && Utility.RandomMinMax( 0, 10 ) == 10 )
				{
					i = DungeonLoot.RandomEvil();
					box.DropItem(i);
					//BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID );
				}
				else
				{
					Item relic = Loot.RandomRelic();
					if ( relic is DDRelicWeapon && Server.Misc.GetPlayerInfo.OrientalPlay( from ) == true ){ Server.Items.DDRelicWeapon.MakeOriental( relic ); }
					else if ( relic is DDRelicStatue && Server.Misc.GetPlayerInfo.OrientalPlay( from ) == true ){ Server.Items.DDRelicStatue.MakeOriental( relic ); }
					else if ( relic is DDRelicBanner && relic.ItemID != 0x2886 && relic.ItemID != 0x2887 && Server.Misc.GetPlayerInfo.OrientalPlay( from ) == true ){ Server.Items.DDRelicBanner.MakeOriental( relic ); }
					box.DropItem( relic );
					//BaseContainer.DropItemFix( relic, from, box.ItemID, box.GumpID );
				}
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				Item idropped = DungeonLoot.RandomRare();
				if ( idropped is OilLeather || idropped is OilMetal ){ idropped.Amount = Utility.RandomMinMax( 1, 8 ); }
				else if ( idropped is MagicalDyes ){ idropped.Amount = Utility.RandomMinMax( 3, 10 ); }
				else if (idropped.Stackable == true){ idropped.Amount = Utility.RandomMinMax( 5, 20 ); }
				box.DropItem( idropped );
				//BaseContainer.DropItemFix( idropped, from, box.ItemID, box.GumpID );
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				i = DungeonLoot.RandomLoreBooks();
				box.DropItem(i);
				//BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID );
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				if ( Utility.Random( 4 ) != 1 ) { i = Loot.RandomScroll( 0, 7, SpellbookType.Regular ); box.DropItem(i); //BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID ); 
                }
				else { i = Loot.RandomScroll( 0, 17, SpellbookType.Necromancer ); box.DropItem(i); //BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID ); 
                }
			}

            double halfluck = (double)from.Luck /4;
	    
	    if (halfluck > 2000)
	    	halfluck = 2000;
		
            int goldmin = Utility.RandomMinMax( Convert.ToInt32(halfluck), 2500);
            double goldmax = (double)goldmin * ( (double)Misc.MyServerSettings.GetDifficultyLevel( this.Location, this.Map ));

			i = new Gold( Utility.RandomMinMax( goldmin, Convert.ToInt32(goldmax)) );
			box.DropItem(i);
			//BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID );

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				Item item = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing( Server.LootPackEntry.IsInTokuno( from ) );
				Server.Misc.ContainerFunctions.LootMutate( from, Server.LootPack.GetRegularLuckChance( from ), item, box, Utility.RandomMinMax( 8, 10 ) );
				box.DropItem(item);
				//BaseContainer.DropItemFix( item, from, box.ItemID, box.GumpID );
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				Item lute = Loot.RandomInstrument();
				Server.Misc.ContainerFunctions.LootMutate( from, Server.LootPack.GetRegularLuckChance( from ), lute, box, Utility.RandomMinMax( 8, 10 ) );
				box.DropItem(lute);
				//BaseContainer.DropItemFix( lute, from, box.ItemID, box.GumpID );
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				i = Loot.RandomGem();
				box.DropItem(i);
				//BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID );
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				i = Loot.RandomPotion();
				box.DropItem(i);
				//BaseContainer.DropItemFix( i, from, box.ItemID, box.GumpID );
			}

			if ( Server.Misc.GetPlayerInfo.LuckyPlayer( (int)( 20 + ( from.Luck / 2 ) ), from ) == true )
			{
				Item wand = Loot.RandomWand();
				Server.Misc.MaterialInfo.ColorMetal( wand, 0 );
				string wandOwner = "";
				if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ wandOwner = Server.LootPackEntry.MagicWandOwner() + " "; }
				wand.Name = wandOwner + wand.Name;
				box.DropItem( wand );
				//BaseContainer.DropItemFix( wand, from, box.ItemID, box.GumpID );
			}
		}

		public class PuzzleGump : Gump
        {
            private readonly Mobile m_From;
            private readonly StealBase m_Chest;
            private readonly PuzzleChestSolution m_Solution;
            public PuzzleGump(Mobile from, StealBase chest, PuzzleChestSolution solution, int check)
                : base(50, 50)
            {
                m_From = from;
                m_Chest = chest;
                m_Solution = solution;

                Dragable = false;
                AddBackground(25, 0, 500, 410, 0x53);

                AddImage(62, 20, 0x67);

                AddHtmlLocalized(80, 36, 110, 70, 1018309, true, false); // A Puzzle Lock

                /* Correctly choose the sequence of cylinders needed to open the latch.  Each cylinder
                * may potentially be used more than once.  Beware!  A false attempt could be deadly!
                */
                AddHtmlLocalized(214, 26, 270, 90, 1018310, true, true);

                AddLeftCylinderButton(62, 130, PuzzleChestCylinder.LightBlue, 10);
                AddLeftCylinderButton(62, 180, PuzzleChestCylinder.Blue, 11);
                AddLeftCylinderButton(62, 230, PuzzleChestCylinder.Green, 12);
                AddLeftCylinderButton(62, 280, PuzzleChestCylinder.Orange, 13);

                AddRightCylinderButton(451, 130, PuzzleChestCylinder.Purple, 14);
                AddRightCylinderButton(451, 180, PuzzleChestCylinder.Red, 15);
                AddRightCylinderButton(451, 230, PuzzleChestCylinder.DarkBlue, 16);
                AddRightCylinderButton(451, 280, PuzzleChestCylinder.Yellow, 17);
                double lockpicking = from.Skills.Lockpicking.Base;			
                if (lockpicking >= 80.0)
                {
                    AddHtmlLocalized(160, 125, 230, 24, 1018308, false, false); // Lockpicking hint:
                    AddBackground(159, 150, 230, 95, 0x13EC);

                    if (lockpicking >= 100.0)
                    {
                        AddHtmlLocalized(165, 157, 200, 40, 1018312, false, false); // In the first slot:	
                        AddCylinder(350, 165, m_Chest.Solution.First);					

                        AddHtmlLocalized(165, 197, 200, 40, 1018313, false, false); // Used in unknown slot:
                        AddCylinder(350, 200, chest.FirstHint);
                        if (lockpicking >= 110.0)
                            AddCylinder(350, 212, chest.SecondHint);
                        if (lockpicking >= 120.0)
                            AddCylinder(350, 224, chest.ThirdHint);
                    }
                    else
                    {
                        AddHtmlLocalized(165, 157, 200, 40, 1018313, false, false); // Used in unknown slot:
                        AddCylinder(350, 160, chest.FirstHint);
                        if (lockpicking >= 90.0)
                            AddCylinder(350, 172, chest.SecondHint);
                    }
                }
                PuzzleChestSolution lastGuess = chest.GetLastGuess(from);
                if (lastGuess != null)
                {
                    AddHtmlLocalized(127, 249, 170, 20, 1018311, false, false); // Thy previous guess:

                    AddBackground(290, 247, 115, 25, 0x13EC);

                    AddCylinder(281, 254, lastGuess.First);
                    AddCylinder(303, 254, lastGuess.Second);
                    AddCylinder(325, 254, lastGuess.Third);
                    AddCylinder(347, 254, lastGuess.Fourth);
                    AddCylinder(369, 254, lastGuess.Fifth);
                }
                AddPedestal(140, 270, m_Solution.First, 0, check == 0);
                AddPedestal(195, 270, m_Solution.Second, 1, check == 1);
                AddPedestal(250, 270, m_Solution.Third, 2, check == 2);
                AddPedestal(305, 270, m_Solution.Fourth, 3, check == 3);
                AddPedestal(360, 270, m_Solution.Fifth, 4, check == 4);

                AddButton(258, 370, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (m_Chest.Deleted || info.ButtonID == 0 || !m_From.CheckAlive())
                    return;

               // if (m_From.IsPlayer() && (m_From.Map != m_Chest.Map || !m_From.InRange(m_Chest.GetWorldLocation(), 2)))
               // {
               //     m_From.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500446); // That is too far away.
                //    return;
               // }

                if (info.ButtonID == 1)
                {
					
					
                    m_Chest.SubmitSolution(m_From, m_Solution);
                }
                else
                {
                    if (info.Switches.Length == 0)
                        return;

                    int pedestal = info.Switches[0];
                    if (pedestal < 0 || pedestal >= m_Solution.Cylinders.Length)
                        return;

                    PuzzleChestCylinder cylinder;
                    switch ( info.ButtonID )
                    {
                        case 10:
                            cylinder = PuzzleChestCylinder.LightBlue;
                            break;
                        case 11:
                            cylinder = PuzzleChestCylinder.Blue;
                            break;
                        case 12:
                            cylinder = PuzzleChestCylinder.Green;
                            break;
                        case 13:
                            cylinder = PuzzleChestCylinder.Orange;
                            break;
                        case 14:
                            cylinder = PuzzleChestCylinder.Purple;
                            break;
                        case 15:
                            cylinder = PuzzleChestCylinder.Red;
                            break;
                        case 16:
                            cylinder = PuzzleChestCylinder.DarkBlue;
                            break;
                        case 17:
                            cylinder = PuzzleChestCylinder.Yellow;
                            break;
                        default:
                            return;
                    }

                    m_Solution.Cylinders[pedestal] = cylinder;

                    m_From.SendGump(new PuzzleGump(m_From, m_Chest, m_Solution, pedestal));
                }
            }

            private void AddLeftCylinderButton(int x, int y, PuzzleChestCylinder cylinder, int buttonID)
            {
                AddBackground(x, y, 30, 30, 0x13EC);
                AddCylinder(x - 7, y + 10, cylinder);
                AddButton(x + 38, y + 9, 0x13A8, 0x4B9, buttonID, GumpButtonType.Reply, 0);
            }

            private void AddRightCylinderButton(int x, int y, PuzzleChestCylinder cylinder, int buttonID)
            {
                AddBackground(x, y, 30, 30, 0x13EC);
                AddCylinder(x - 7, y + 10, cylinder);
                AddButton(x - 26, y + 9, 0x13A8, 0x4B9, buttonID, GumpButtonType.Reply, 0);
            }

            private void AddPedestal(int x, int y, PuzzleChestCylinder cylinder, int switchID, bool initialState)
            {
                AddItem(x, y, 0xB10);
                AddItem(x - 23, y + 12, 0xB12);
                AddItem(x + 23, y + 12, 0xB13);
                AddItem(x, y + 23, 0xB11);

                if (cylinder != PuzzleChestCylinder.None)
                {
                    AddItem(x, y + 2, 0x51A);
                    AddCylinder(x - 1, y + 19, cylinder);
                }
                else
                {
                    AddItem(x, y + 2, 0x521);
                }

                AddRadio(x + 7, y + 65, 0x867, 0x86A, initialState, switchID);
            }

            private void AddCylinder(int x, int y, PuzzleChestCylinder cylinder)
            {
                if (cylinder != PuzzleChestCylinder.None)
                    AddItem(x, y, (int)cylinder);
                else
                    AddItem(x + 9, y, (int)cylinder);
            }
        }

        public class StatusGump : Gump
        {
            public StatusGump(int correctCylinders, int correctColors)
                : base(50, 50)
            {
                AddBackground(15, 250, 305, 163, 0x53);
                AddBackground(28, 265, 280, 133, 0xBB8);

                AddHtmlLocalized(35, 271, 270, 24, 1018314, false, false); // Thou hast failed to solve the puzzle!

                AddHtmlLocalized(35, 297, 250, 24, 1018315, false, false); // Correctly placed colors:
                AddLabel(285, 297, 0x44, correctCylinders.ToString());

                AddHtmlLocalized(35, 323, 250, 24, 1018316, false, false); // Used colors in wrong slots:
                AddLabel(285, 323, 0x44, correctColors.ToString());

                AddButton(152, 369, 0xFA5, 0xFA7, 0, GumpButtonType.Reply, 0);
            }
        }

        private void InitHints()
        {
            List<PuzzleChestCylinder> list = new List<PuzzleChestCylinder>(Solution.Cylinders.Length - 1);
            for (int i = 1; i < Solution.Cylinders.Length; i++)
                list.Add(Solution.Cylinders[i]);

            m_Hints = new PuzzleChestCylinder[HintsCount];
			
            for (int i = 0; i < m_Hints.Length; i++)
            {
                int pos = Utility.Random(list.Count);
                m_Hints[i] = list[pos];
                list.RemoveAt(pos);
            }
        }

	}
}
