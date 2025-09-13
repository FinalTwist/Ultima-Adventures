using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
	public class SlayerTarget : Target
	{
		private SlayerDeed m_Deed;

		public SlayerTarget( SlayerDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target )
		{
			int SlayType = m_Deed.SlayerType;

			if ( target is BaseWeapon)
			{
				BaseWeapon item = (BaseWeapon)target;

				SlayerName slaying = SlayerDeed.GetDeedSlayer( SlayType );

				if ( item.RootParent != from )
				{
					from.SendMessage( "The weapon must be in your pack." );
				}
				else if ( item.Slayer != SlayerName.None && item.Slayer2 != SlayerName.None )
				{
					from.SendMessage( "The weapon is already a slayer.");
				}
				else if ( item.Slayer == slaying || item.Slayer2 == slaying )
				{
					from.SendMessage( "The weapon already slays these creatures.");
				}
				else
				{
					if (item.Slayer == SlayerName.None)
					{
						item.Slayer = slaying;
						from.SendMessage( "The weapon now slays these creatures." );
						m_Deed.Delete();
					}
					else if (item.Slayer2 == SlayerName.None)
					{
						item.Slayer2 = slaying;
						from.SendMessage( "You weapon now slays these creatures." );
						m_Deed.Delete();
					}
				}
			}
			else if ( target is BaseInstrument)
			{
				BaseInstrument item = (BaseInstrument)target;

				SlayerName slaying = SlayerDeed.GetDeedSlayer( SlayType );

				if ( item.RootParent != from )
				{
					from.SendMessage( "The instrument must be in your pack." );
				}
				else if ( item.Slayer != SlayerName.None && item.Slayer2 != SlayerName.None )
				{
					from.SendMessage( "The instrument is already a slayer.");
				}
				else if ( item.Slayer == slaying || item.Slayer2 == slaying )
				{
					from.SendMessage( "The instrument already slays these creatures.");
				}
				else
				{
					if (item.Slayer == SlayerName.None)
					{
						item.Slayer = slaying;
						from.SendMessage( "The instrument now slays these creatures." );
						m_Deed.Delete();
					}
					else if (item.Slayer2 == SlayerName.None)
					{
						item.Slayer2 = slaying;
						from.SendMessage( "You instrument now slays these creatures." );
						m_Deed.Delete();
					}
				}
			}
			else if ( target is Spellbook && !(target is HolyManSpellbook) )
			{
				Spellbook item = (Spellbook)target;

				SlayerName slaying = SlayerDeed.GetDeedSlayer( SlayType );

				if ( item.RootParent != from )
				{
					from.SendMessage( "The book must be in your pack." );
				}
				else if ( item.Slayer != SlayerName.None && item.Slayer2 != SlayerName.None )
				{
					from.SendMessage( "The book is already a slayer.");
				}
				else if ( item.Slayer == slaying || item.Slayer2 == slaying )
				{
					from.SendMessage( "The book already slays these creatures.");
				}
				else
				{
					if (item.Slayer == SlayerName.None)
					{
						item.Slayer = slaying;
						from.SendMessage( "The book now slays these creatures." );
						m_Deed.Delete();
					}
					else if (item.Slayer2 == SlayerName.None)
					{
						item.Slayer2 = slaying;
						from.SendMessage( "You book now slays these creatures." );
						m_Deed.Delete();
					}
				}
			}
		}
	}

	public class SlayerDeed : Item
	{
		public int SlayerType;
		public string SlayerNames;

		[CommandProperty(AccessLevel.Owner)]
		public int Slayer_Type { get { return SlayerType; } set { SlayerType = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Slayer_Names { get { return SlayerNames; } set { SlayerNames = value; InvalidateProperties(); } }

		[Constructable]
		public SlayerDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
			Name = "a slayer stone";
			SlayerType = Utility.Random( 35 );
			ItemID = 0x400B;

			switch ( SlayerType )
			{
				case 0: SlayerNames = "animal hunter"; break;
				case 1: SlayerNames = "arachnid doom"; break;
				case 2: SlayerNames = "avian hunter"; break;
				case 3: SlayerNames = "devilish death"; break;
				case 4: SlayerNames = "blood bane"; break;
				case 5: SlayerNames = "demonic dismissal"; break;
				case 6: SlayerNames = "dragon slayer"; break;
				case 7: SlayerNames = "stone smashing"; break;
				case 8: SlayerNames = "elemental slayer"; break;
				case 9: SlayerNames = "poison cleansing"; break;
				case 10: SlayerNames = "abysmal banishment"; break;
				case 11: SlayerNames = "fey slayer"; break;
				case 12: SlayerNames = "flame extinguishing"; break;
				case 13: SlayerNames = "gargoyle bane"; break;
				case 14: SlayerNames = "giant killer"; break;
				case 15: SlayerNames = "golem destruction"; break;
				case 16: SlayerNames = "lizardman death"; break;
				case 17: SlayerNames = "neptune's bane"; break;
				case 18: SlayerNames = "ogre extinction"; break;
				case 19: SlayerNames = "ophidian slayer"; break;
				case 20: SlayerNames = "orcish demise"; break;
				case 21: SlayerNames = "humanoid assassination"; break;
				case 22: SlayerNames = "reptile slayer"; break;
				case 23: SlayerNames = "scorpion slayer"; break;
				case 24: SlayerNames = "supernatural vanquishing"; break;
				case 25: SlayerNames = "slimy scourge"; break;
				case 26: SlayerNames = "serpent eradication"; break;
				case 27: SlayerNames = "spider eradication"; break;
				case 28: SlayerNames = "arctic destruction"; break;
				case 29: SlayerNames = "terathan slayer"; break;
				case 30: SlayerNames = "troll killer"; break;
				case 31: SlayerNames = "windy wrath"; break;
				case 32: SlayerNames = "watery grave"; break;
				case 33: SlayerNames = "weed ruin"; break;
				case 34: SlayerNames = "wizard slayer"; break;
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, SlayerNames );
        }

		public SlayerDeed(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( SlayerType );
            writer.Write( SlayerNames );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			SlayerType = reader.ReadInt();
			SlayerNames = reader.ReadString();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
			}
			else
			{
				from.SendMessage( "What item would you like to make a slayer?" );
				from.Target = new SlayerTarget( this );
			}
		}

		public static SlayerName GetDeedSlayer( int slays )
		{
			SlayerName slayer = SlayerName.AnimalHunter;

			if ( slays == 1 ){ slayer = SlayerName.ArachnidDoom; }
			else if ( slays == 2 ){ slayer = SlayerName.AvianHunter; }
			else if ( slays == 3 ){ slayer = SlayerName.BalronDamnation; }
			else if ( slays == 4 ){ slayer = SlayerName.BloodDrinking; }
			else if ( slays == 5 ){ slayer = SlayerName.DaemonDismissal; }
			else if ( slays == 6 ){ slayer = SlayerName.DragonSlaying; }
			else if ( slays == 7 ){ slayer = SlayerName.EarthShatter; }
			else if ( slays == 8 ){ slayer = SlayerName.ElementalBan; }
			else if ( slays == 9 ){ slayer = SlayerName.ElementalHealth; }
			else if ( slays == 10 ){ slayer = SlayerName.Exorcism; }
			else if ( slays == 11 ){ slayer = SlayerName.Fey; }
			else if ( slays == 12 ){ slayer = SlayerName.FlameDousing; }
			else if ( slays == 13 ){ slayer = SlayerName.GargoylesFoe; }
			else if ( slays == 14 ){ slayer = SlayerName.GiantKiller; }
			else if ( slays == 15 ){ slayer = SlayerName.GolemDestruction; }
			else if ( slays == 16 ){ slayer = SlayerName.LizardmanSlaughter; }
			else if ( slays == 17 ){ slayer = SlayerName.NeptunesBane; }
			else if ( slays == 18 ){ slayer = SlayerName.OgreTrashing; }
			else if ( slays == 19 ){ slayer = SlayerName.Ophidian; }
			else if ( slays == 20 ){ slayer = SlayerName.OrcSlaying; }
			else if ( slays == 21 ){ slayer = SlayerName.Repond; }
			else if ( slays == 22 ){ slayer = SlayerName.ReptilianDeath; }
			else if ( slays == 23 ){ slayer = SlayerName.ScorpionsBane; }
			else if ( slays == 24 ){ slayer = SlayerName.Silver; }
			else if ( slays == 25 ){ slayer = SlayerName.SlimyScourge; }
			else if ( slays == 26 ){ slayer = SlayerName.SnakesBane; }
			else if ( slays == 27 ){ slayer = SlayerName.SpidersDeath; }
			else if ( slays == 28 ){ slayer = SlayerName.SummerWind; }
			else if ( slays == 29 ){ slayer = SlayerName.Terathan; }
			else if ( slays == 30 ){ slayer = SlayerName.TrollSlaughter; }
			else if ( slays == 31 ){ slayer = SlayerName.Vacuum; }
			else if ( slays == 32 ){ slayer = SlayerName.WaterDissipation; }
			else if ( slays == 33 ){ slayer = SlayerName.WeedRuin; }
			else if ( slays == 34 ){ slayer = SlayerName.WizardSlayer; }

			return slayer;
		}
	}
}