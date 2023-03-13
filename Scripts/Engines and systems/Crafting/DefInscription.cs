using System;
using Server.Items;
using Server.Spells;

namespace Server.Engines.Craft
{
	public class DefInscription : CraftSystem
	{
		public override SkillName MainSkill
		{
			get { return SkillName.Inscribe; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044009; } // <CENTER>INSCRIPTION MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if (m_CraftSystem == null)
					m_CraftSystem = new DefInscription();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin(CraftItem item)
		{
			return 0.0; // 0%
		}

		private DefInscription()
			: base(1, 1, 1.25)// base( 1, 1, 3.0 )
		{
		}

		public override int CanCraft(Mobile from, BaseTool tool, Type typeItem)
		{
			if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
				return 1044038; // You have worn out your tool!
			else if (!BaseTool.CheckAccessible(tool, from))
				return 1044263; // The tool must be on your person to use.

			if (typeItem != null)
			{
				object o = Activator.CreateInstance(typeItem);

				if (o is SpellScroll)
				{
					SpellScroll scroll = (SpellScroll)o;
					Spellbook book = Spellbook.Find(from, scroll.SpellID);

					bool hasSpell = (book != null && book.HasSpell(scroll.SpellID));

					scroll.Delete();

					return (hasSpell ? 0 : 1042404); // null : You don't have that spell!
				}
				else if (o is Item)
				{
					((Item)o).Delete();
				}
			}

			return 0;
		}

		public override void PlayCraftEffect(Mobile from)
		{
			from.PlaySound(0x249);
		}

		private static Type typeofSpellScroll = typeof(SpellScroll);

		public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
		{
			if (toolBroken)
				from.SendLocalizedMessage(1044038); // You have worn out your tool

			if (!typeofSpellScroll.IsAssignableFrom(item.ItemType)) //  not a scroll
			{
				if (failed)
				{
					if (lostMaterial)
						return 1044043; // You failed to create the item, and some of your materials are lost.
					else
						return 1044157; // You failed to create the item, but no materials were lost.
				}
				else
				{
					if (quality == 0)
						return 502785; // You were barely able to make this item.  It's quality is below average.
					else if (makersMark && quality == 2)
						return 1044156; // You create an exceptional quality item and affix your maker's mark.
					else if (quality == 2)
						return 1044155; // You create an exceptional quality item.
					else
						return 1044154; // You create the item.
				}
			}
			else
			{
				if (failed)
					return 501630; // You fail to inscribe the scroll, and the scroll is ruined.
				else
					return 501629; // You inscribe the spell and put the scroll in your backpack.
			}
		}

		private int m_Circle, m_Mana;

		private enum Reg { BlackPearl, Bloodmoss, Garlic, Ginseng, MandrakeRoot, Nightshade, SulfurousAsh, SpidersSilk }

		private Type[] m_RegTypes = new Type[]
			{
				typeof( BlackPearl ),
				typeof( Bloodmoss ),
				typeof( Garlic ),
				typeof( Ginseng ),
				typeof( MandrakeRoot ),
				typeof( Nightshade ),
				typeof( SulfurousAsh ),
				typeof( SpidersSilk )
			};

		private int m_Index;

		private void AddSpell(Type type, params Reg[] regs)
		{
			double minSkill, maxSkill;

			switch (m_Circle)
			{
				default:
				case 0: minSkill = -25.0; maxSkill = 25.0; break;
				case 1: minSkill = -10.8; maxSkill = 39.2; break;
				case 2: minSkill = 03.5; maxSkill = 53.5; break;
				case 3: minSkill = 17.8; maxSkill = 67.8; break;
				case 4: minSkill = 32.1; maxSkill = 82.1; break;
				case 5: minSkill = 46.4; maxSkill = 96.4; break;
				case 6: minSkill = 60.7; maxSkill = 110.7; break;
				case 7: minSkill = 75.0; maxSkill = 125.0; break;
			}

			int index = AddCraft(type, 1044369 + m_Circle, 1044381 + m_Index++, minSkill, maxSkill, m_RegTypes[(int)regs[0]], 1044353 + (int)regs[0], 1, 1044361 + (int)regs[0]);

			for (int i = 1; i < regs.Length; ++i)
				AddRes(index, m_RegTypes[(int)regs[i]], 1044353 + (int)regs[i], 1, 1044361 + (int)regs[i]);

			AddRes(index, typeof(BlankScroll), 1044377, 1, 1044378);

			SetManaReq(index, m_Mana);
		}

		private void AddNecroSpell(int spell, int mana, double minSkill, Type type, params Type[] regs)
		{
			int id = CraftItem.ItemIDOf(regs[0]);

			int index = AddCraft(type, 1061677, 1060509 + spell, minSkill, minSkill + 1.0, regs[0], id < 0x4000 ? 1020000 + id : 1078872 + id, 1, 501627);	//Yes, on OSI it's only 1.0 skill diff'.  Don't blame me, blame OSI.

			for (int i = 1; i < regs.Length; ++i)
			{
				id = CraftItem.ItemIDOf(regs[i]);
				AddRes(index, regs[i], id < 0x4000 ? 1020000 + id : 1078872 + id, 1, 501627);
			}

			AddRes(index, typeof(BlankScroll), 1044377, 1, 1044378);

			SetManaReq(index, mana);
		}

		public override void InitCraftList()
		{
			m_Circle = 0;
			m_Mana = 4;

			AddSpell(typeof(ReactiveArmorScroll), Reg.Garlic, Reg.SpidersSilk, Reg.SulfurousAsh);
			AddSpell(typeof(ClumsyScroll), Reg.Bloodmoss, Reg.Nightshade);
			AddSpell(typeof(CreateFoodScroll), Reg.Garlic, Reg.Ginseng, Reg.MandrakeRoot);
			AddSpell(typeof(FeeblemindScroll), Reg.Nightshade, Reg.Ginseng);
			AddSpell(typeof(HealScroll), Reg.Garlic, Reg.Ginseng, Reg.SpidersSilk);
			AddSpell(typeof(MagicArrowScroll), Reg.SulfurousAsh);
			AddSpell(typeof(NightSightScroll), Reg.SpidersSilk, Reg.SulfurousAsh);
			AddSpell(typeof(WeakenScroll), Reg.Garlic, Reg.Nightshade);

			m_Circle = 1;
			m_Mana = 6;

			AddSpell(typeof(AgilityScroll), Reg.Bloodmoss, Reg.MandrakeRoot);
			AddSpell(typeof(CunningScroll), Reg.Nightshade, Reg.MandrakeRoot);
			AddSpell(typeof(CureScroll), Reg.Garlic, Reg.Ginseng);
			AddSpell(typeof(HarmScroll), Reg.Nightshade, Reg.SpidersSilk);
			AddSpell(typeof(MagicTrapScroll), Reg.Garlic, Reg.SpidersSilk, Reg.SulfurousAsh);
			AddSpell(typeof(MagicUnTrapScroll), Reg.Bloodmoss, Reg.SulfurousAsh);
			AddSpell(typeof(ProtectionScroll), Reg.Garlic, Reg.Ginseng, Reg.SulfurousAsh);
			AddSpell(typeof(StrengthScroll), Reg.Nightshade, Reg.MandrakeRoot);

			m_Circle = 2;
			m_Mana = 9;

			AddSpell(typeof(BlessScroll), Reg.Garlic, Reg.MandrakeRoot);
			AddSpell(typeof(FireballScroll), Reg.BlackPearl);
			AddSpell(typeof(MagicLockScroll), Reg.Bloodmoss, Reg.Garlic, Reg.SulfurousAsh);
			AddSpell(typeof(PoisonScroll), Reg.Nightshade);
			AddSpell(typeof(TelekinisisScroll), Reg.Bloodmoss, Reg.MandrakeRoot);
			AddSpell(typeof(TeleportScroll), Reg.Bloodmoss, Reg.MandrakeRoot);
			AddSpell(typeof(UnlockScroll), Reg.Bloodmoss, Reg.SulfurousAsh);
			AddSpell(typeof(WallOfStoneScroll), Reg.Bloodmoss, Reg.Garlic);

			m_Circle = 3;
			m_Mana = 11;

			AddSpell(typeof(ArchCureScroll), Reg.Garlic, Reg.Ginseng, Reg.MandrakeRoot);
			AddSpell(typeof(ArchProtectionScroll), Reg.Garlic, Reg.Ginseng, Reg.MandrakeRoot, Reg.SulfurousAsh);
			AddSpell(typeof(CurseScroll), Reg.Garlic, Reg.Nightshade, Reg.SulfurousAsh);
			AddSpell(typeof(FireFieldScroll), Reg.BlackPearl, Reg.SpidersSilk, Reg.SulfurousAsh);
			AddSpell(typeof(GreaterHealScroll), Reg.Garlic, Reg.SpidersSilk, Reg.MandrakeRoot, Reg.Ginseng);
			AddSpell(typeof(LightningScroll), Reg.MandrakeRoot, Reg.SulfurousAsh);
			AddSpell(typeof(ManaDrainScroll), Reg.BlackPearl, Reg.SpidersSilk, Reg.MandrakeRoot);
			AddSpell(typeof(RecallScroll), Reg.BlackPearl, Reg.Bloodmoss, Reg.MandrakeRoot);

			m_Circle = 4;
			m_Mana = 14;

			AddSpell(typeof(BladeSpiritsScroll), Reg.BlackPearl, Reg.Nightshade, Reg.MandrakeRoot);
			AddSpell(typeof(DispelFieldScroll), Reg.BlackPearl, Reg.Garlic, Reg.SpidersSilk, Reg.SulfurousAsh);
			AddSpell(typeof(IncognitoScroll), Reg.Bloodmoss, Reg.Garlic, Reg.Nightshade);
			AddSpell(typeof(MagicReflectScroll), Reg.Garlic, Reg.MandrakeRoot, Reg.SpidersSilk);
			AddSpell(typeof(MindBlastScroll), Reg.BlackPearl, Reg.MandrakeRoot, Reg.Nightshade, Reg.SulfurousAsh);
			AddSpell(typeof(ParalyzeScroll), Reg.Garlic, Reg.MandrakeRoot, Reg.SpidersSilk);
			AddSpell(typeof(PoisonFieldScroll), Reg.BlackPearl, Reg.Nightshade, Reg.SpidersSilk);
			AddSpell(typeof(SummonCreatureScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);

			m_Circle = 5;
			m_Mana = 20;

			AddSpell(typeof(DispelScroll), Reg.Garlic, Reg.MandrakeRoot, Reg.SulfurousAsh);
			AddSpell(typeof(EnergyBoltScroll), Reg.BlackPearl, Reg.Nightshade);
			AddSpell(typeof(ExplosionScroll), Reg.Bloodmoss, Reg.MandrakeRoot);
			AddSpell(typeof(InvisibilityScroll), Reg.Bloodmoss, Reg.Nightshade);
			AddSpell(typeof(MarkScroll), Reg.Bloodmoss, Reg.BlackPearl, Reg.MandrakeRoot);
			AddSpell(typeof(MassCurseScroll), Reg.Garlic, Reg.MandrakeRoot, Reg.Nightshade, Reg.SulfurousAsh);
			AddSpell(typeof(ParalyzeFieldScroll), Reg.BlackPearl, Reg.Ginseng, Reg.SpidersSilk);
			AddSpell(typeof(RevealScroll), Reg.Bloodmoss, Reg.SulfurousAsh);

			m_Circle = 6;
			m_Mana = 40;

			AddSpell(typeof(ChainLightningScroll), Reg.BlackPearl, Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SulfurousAsh);
			AddSpell(typeof(EnergyFieldScroll), Reg.BlackPearl, Reg.MandrakeRoot, Reg.SpidersSilk, Reg.SulfurousAsh);
			AddSpell(typeof(FlamestrikeScroll), Reg.SpidersSilk, Reg.SulfurousAsh);
			AddSpell(typeof(GateTravelScroll), Reg.BlackPearl, Reg.MandrakeRoot, Reg.SulfurousAsh);
			AddSpell(typeof(ManaVampireScroll), Reg.BlackPearl, Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);
			AddSpell(typeof(MassDispelScroll), Reg.BlackPearl, Reg.Garlic, Reg.MandrakeRoot, Reg.SulfurousAsh);
			AddSpell(typeof(MeteorSwarmScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SulfurousAsh, Reg.SpidersSilk);
			AddSpell(typeof(PolymorphScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);

			m_Circle = 7;
			m_Mana = 50;

			AddSpell(typeof(EarthquakeScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.Ginseng, Reg.SulfurousAsh);
			AddSpell(typeof(EnergyVortexScroll), Reg.BlackPearl, Reg.Bloodmoss, Reg.MandrakeRoot, Reg.Nightshade);
			AddSpell(typeof(ResurrectionScroll), Reg.Bloodmoss, Reg.Garlic, Reg.Ginseng);
			AddSpell(typeof(SummonAirElementalScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);
			AddSpell(typeof(SummonDaemonScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk, Reg.SulfurousAsh);
			AddSpell(typeof(SummonEarthElementalScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);
			AddSpell(typeof(SummonFireElementalScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk, Reg.SulfurousAsh);
			AddSpell(typeof(SummonWaterElementalScroll), Reg.Bloodmoss, Reg.MandrakeRoot, Reg.SpidersSilk);

			if (Core.SE)
			{
				AddNecroSpell(0, 23, 39.6, typeof(AnimateDeadScroll), Reagent.GraveDust, Reagent.DaemonBlood);
				AddNecroSpell(1, 13, 19.6, typeof(BloodOathScroll), Reagent.DaemonBlood);
				AddNecroSpell(2, 11, 19.6, typeof(CorpseSkinScroll), Reagent.BatWing, Reagent.GraveDust);
				AddNecroSpell(3, 7, 19.6, typeof(CurseWeaponScroll), Reagent.PigIron);
				AddNecroSpell(4, 11, 19.6, typeof(EvilOmenScroll), Reagent.BatWing, Reagent.NoxCrystal);
				AddNecroSpell(5, 11, 39.6, typeof(HorrificBeastScroll), Reagent.BatWing, Reagent.DaemonBlood);
				AddNecroSpell(6, 23, 69.6, typeof(LichFormScroll), Reagent.GraveDust, Reagent.DaemonBlood, Reagent.NoxCrystal);
				AddNecroSpell(7, 17, 29.6, typeof(MindRotScroll), Reagent.BatWing, Reagent.DaemonBlood, Reagent.PigIron);
				AddNecroSpell(8, 5, 19.6, typeof(PainSpikeScroll), Reagent.GraveDust, Reagent.PigIron);
				AddNecroSpell(9, 17, 49.6, typeof(PoisonStrikeScroll), Reagent.NoxCrystal);
				AddNecroSpell(10, 29, 64.6, typeof(StrangleScroll), Reagent.DaemonBlood, Reagent.NoxCrystal);
				AddNecroSpell(11, 17, 29.6, typeof(SummonFamiliarScroll), Reagent.BatWing, Reagent.GraveDust, Reagent.DaemonBlood);
				AddNecroSpell(12, 23, 98.6, typeof(VampiricEmbraceScroll), Reagent.BatWing, Reagent.NoxCrystal, Reagent.PigIron);
				AddNecroSpell(13, 41, 79.6, typeof(VengefulSpiritScroll), Reagent.BatWing, Reagent.GraveDust, Reagent.PigIron);
				AddNecroSpell(14, 23, 59.6, typeof(WitherScroll), Reagent.GraveDust, Reagent.NoxCrystal, Reagent.PigIron);
				AddNecroSpell(15, 17, 79.6, typeof(WraithFormScroll), Reagent.NoxCrystal, Reagent.PigIron);
				AddNecroSpell(16, 40, 79.6, typeof(ExorcismScroll), Reagent.NoxCrystal, Reagent.GraveDust);
			}

			int index;

			// Blank Scrolls
			index = AddCraft( typeof( BlankScroll ), 1044294, 1044377, 40.0, 70.0, typeof( BarkFragment ), 1073477, 1, 1073478 );
			SetUseAllRes( index, true );

			// Runebook
			index = AddCraft( typeof( Runebook ), 1044294, 1041267, 45.0, 95.0, typeof( BlankScroll ), 1044377, 8, 1044378 );
			AddRes( index, typeof( RecallScroll ), 1044445, 1, 1044253 );
			AddRes( index, typeof( Beeswax ), 1025154, 5, 1044253 );
			AddRes( index, typeof( GateTravelScroll ), 1044446, 1, 1044253 );

			index = AddCraft(typeof(Engines.BulkOrders.BulkOrderBook), 1044294, 1028793, 65.0, 115.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 5, 1044253 );

			index = AddCraft(typeof(Spellbook), 1044294, 1023834, 50.0, 126, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 5, 1044253 );

			index = AddCraft(typeof(NecromancerSpellbook), 1044294, 1028787, 50.0, 126, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 5, 1044253 );

			index = AddCraft(typeof(SongBook), 1044294, 1028787, 50.0, 126, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 5, 1044253 );

			MarkOption = true;

			index = AddCraft(typeof(ArmysPaeonScroll), 1044294, "Armys Paeon Scroll", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(EnchantingEtudeScroll), 1044294, "Enchanting Etude", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(EnergyCarolScroll), 1044294, "Energy Carol", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(EnergyThrenodyScroll), 1044294, "Energy Threnody", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(FireCarolScroll), 1044294, "Fire Carol", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(FireThrenodyScroll), 1044294, "Fire Threnody", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(FoeRequiemScroll), 1044294, "Foe Requiem", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(IceCarolScroll), 1044294, "Ice Carol", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(IceThrenodyScroll), 1044294, "Ice Threnody", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(KnightsMinneScroll), 1044294, "Knights Minne", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(MagesBalladScroll), 1044294, "Mages Ballad", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(MagicFinaleScroll), 1044294, "Magic Finale", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(PoisonCarolScroll), 1044294, "Poison Carol", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(PoisonThrenodyScroll), 1044294, "Poison Threnody", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(SheepfoeMamboScroll), 1044294, "Sheepfoe Mambo", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			index = AddCraft(typeof(SinewyEtudeScroll), 1044294, "Sinewy Etude", 75.0, 95.0, typeof (BlankScroll), 1044377, 1, 1044378);
			AddRes( index, typeof(Lute), "lute", 1, 1044253);
			AddSkill( index, SkillName.Musicianship, 95.0, 120.0);

			//Expert Study Books
			index = AddCraft( typeof(StandardAlchemyStudyBook), "Expert Study Books", "Expert Alchemy", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Alchemy, 70.0, 80.0 );

			index = AddCraft( typeof(StandardAnatomyStudyBook), "Expert Study Books", "Expert Anatomy", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Anatomy, 70.0, 80.0 );

			index = AddCraft( typeof(StandardAnimalLoreStudyBook), "Expert Study Books", "Expert Animal Lore", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.AnimalLore, 70.0, 80.0 );

			index = AddCraft( typeof(StandardAnimalTamingStudyBook), "Expert Study Books", "Expert Animal Taming", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.AnimalTaming, 70.0, 80.0 );

			index = AddCraft( typeof(StandardArcheryStudyBook), "Expert Study Books", "Expert Archery", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Archery, 70.0, 80.0 );

			index = AddCraft( typeof(StandardBeggingStudyBook), "Expert Study Books", "Expert Begging", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Begging, 70.0, 80.0 );

			index = AddCraft( typeof(StandardBlacksmithStudyBook), "Expert Study Books", "Expert Blacksmithing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Blacksmith, 70.0, 80.0 );	

			index = AddCraft( typeof(StandardBushidoStudyBook), "Expert Study Books", "Expert Bushido", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Bushido, 70.0, 80.0 );	

			index = AddCraft( typeof(StandardCampingStudyBook), "Expert Study Books", "Expert Camping", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Camping, 70.0, 80.0 );

			index = AddCraft( typeof(StandardCartographyStudyBook), "Expert Study Books", "Expert Cartography", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Cartography, 70.0, 80.0 );

			index = AddCraft( typeof(StandardChivalryStudyBook), "Expert Study Books", "Expert Chivlary", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Chivalry, 70.0, 80.0 );

			index = AddCraft( typeof(StandardCookingStudyBook), "Expert Study Books", "Expert Cooking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Cooking, 70.0, 80.0 );

			index = AddCraft( typeof(StandardDetectHiddenStudyBook), "Expert Study Books", "Expert Detecting Hidden", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.DetectHidden, 70.0, 80.0 );

			index = AddCraft( typeof(StandardDiscordanceStudyBook), "Expert Study Books", "Expert Discordance", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Discordance, 70.0, 80.0 );

			index = AddCraft( typeof(StandardEvalIntStudyBook), "Expert Study Books", "Expert Evaluating Intelligence", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.EvalInt, 70.0, 80.0 );

			index = AddCraft( typeof(StandardFencingStudyBook), "Expert Study Books", "Expert Fencing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Fencing, 70.0, 80.0 );

			index = AddCraft( typeof(StandardFishingStudyBook), "Expert Study Books", "Expert Fishing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Fishing, 70.0, 80.0 );

			index = AddCraft( typeof(StandardFocusStudyBook), "Expert Study Books", "Expert Focus", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Focus, 70.0, 80.0 );

			index = AddCraft( typeof(StandardForensicsStudyBook), "Expert Study Books", "Expert Forensic Evaluation", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Forensics, 70.0, 80.0 );

			index = AddCraft( typeof(StandardHealingStudyBook), "Expert Study Books", "Expert Healing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Healing, 70.0, 80.0 );

			index = AddCraft( typeof(StandardHerdingStudyBook), "Expert Study Books", "Expert Herding", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Herding, 70.0, 80.0 );

			index = AddCraft( typeof(StandardHidingStudyBook), "Expert Study Books", "Expert Hiding", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Hiding, 70.0, 80.0 );

			index = AddCraft( typeof(StandardInscribeStudyBook), "Expert Study Books", "Expert Inscription", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Inscribe, 70.0, 80.0 );

			index = AddCraft( typeof(StandardItemIDStudyBook), "Expert Study Books", "Expert Item Identification", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.ItemID, 70.0, 80.0 );

			index = AddCraft( typeof(StandardLockpickingStudyBook), "Expert Study Books", "Expert Lockpicking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Lockpicking, 70.0, 80.0 );

			index = AddCraft( typeof(StandardLumberjackingStudyBook), "Expert Study Books", "Expert Lumberjacking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Lumberjacking, 70.0, 80.0 );

			index = AddCraft( typeof(StandardMacingStudyBook), "Expert Study Books", "Expert Mace Fighting", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Macing, 70.0, 80.0 );

			index = AddCraft( typeof(StandardMageryStudyBook), "Expert Study Books", "Expert Magery", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Magery, 70.0, 80.0 );

			index = AddCraft( typeof(StandardMagicResistStudyBook), "Expert Study Books", "Expert Magic Resist", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.MagicResist, 70.0, 80.0 );

			index = AddCraft( typeof(StandardMeditationStudyBook), "Expert Study Books", "Expert Meditation", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Meditation, 70.0, 80.0 );

			index = AddCraft( typeof(StandardMiningStudyBook), "Expert Study Books", "Expert Mining", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Mining, 70.0, 80.0 );

			index = AddCraft( typeof(StandardMusicianshipStudyBook), "Expert Study Books", "Expert Musicianship", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Musicianship, 70.0, 80.0 );

			index = AddCraft( typeof(StandardNecromancyStudyBook), "Expert Study Books", "Expert Necromancy", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Necromancy, 70.0, 80.0 );

			index = AddCraft( typeof(StandardNinjitsuStudyBook), "Expert Study Books", "Expert Ninjitsu", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Ninjitsu, 70.0, 80.0 );

			index = AddCraft( typeof(StandardParryStudyBook), "Expert Study Books", "Expert Parry", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Parry, 70.0, 80.0 );

			index = AddCraft( typeof(StandardPeacemakingStudyBook), "Expert Study Books", "Expert Peacemaking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Peacemaking, 70.0, 80.0 );

			index = AddCraft( typeof(StandardPoisoningStudyBook), "Expert Study Books", "Expert Poisoning", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Poisoning, 70.0, 80.0 );

			index = AddCraft( typeof(StandardProvocationStudyBook), "Expert Study Books", "Expert Provocation", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Provocation, 70.0, 80.0 );

			index = AddCraft( typeof(StandardRemoveTrapStudyBook), "Expert Study Books", "Expert Remove Trap", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.RemoveTrap, 70.0, 80.0 );

			index = AddCraft( typeof(StandardSnoopingStudyBook), "Expert Study Books", "Expert Snooping", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Snooping, 70.0, 80.0 );

			index = AddCraft( typeof(StandardSpiritSpeakStudyBook), "Expert Study Books", "Expert SpiritSpeak", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.SpiritSpeak, 70.0, 80.0 );

			index = AddCraft( typeof(StandardStealingStudyBook), "Expert Study Books", "Expert Stealing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Stealing, 70.0, 80.0 );

			index = AddCraft( typeof(StandardStealthStudyBook), "Expert Study Books", "Expert Stealth", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Stealth, 70.0, 80.0 );

			index = AddCraft( typeof(StandardSwordsStudyBook), "Expert Study Books", "Expert Swordsmanship", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Swords, 70.0, 80.0 );

			index = AddCraft( typeof(StandardTacticsStudyBook), "Expert Study Books", "Expert Tactics", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Tactics, 70.0, 80.0 );

			index = AddCraft( typeof(StandardTailoringStudyBook), "Expert Study Books", "Expert Tailoring", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Tailoring, 70.0, 80.0 );

			index = AddCraft( typeof(StandardTasteIDStudyBook), "Expert Study Books", "Expert Taste Identification", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.TasteID, 70.0, 80.0 );

			index = AddCraft( typeof(StandardThrowingStudyBook), "Expert Study Books", "Expert Throwing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Throwing, 70.0, 80.0 );

			index = AddCraft( typeof(StandardTinkeringStudyBook), "Expert Study Books", "Expert Tinkering", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Tinkering, 70.0, 80.0 );

			index = AddCraft( typeof(StandardTrackingStudyBook), "Expert Study Books", "Expert Tracking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Tracking, 70.0, 80.0 );

			index = AddCraft( typeof(StandardVeterinaryStudyBook), "Expert Study Books", "Expert Veterinary", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Veterinary, 70.0, 80.0 );

			index = AddCraft( typeof(StandardWrestlingStudyBook), "Expert Study Books", "Expert Wrestling", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 1, 1044253 );
			AddRes( index, typeof( Leather ),1044462, 2, 1044253 );
			AddSkill( index, SkillName.Wrestling, 70.0, 80.0 );



			//Grandmaster Study Books
			
			index = AddCraft( typeof(AdvancedAlchemyStudyBook), "Grandmaster Study Books", "Grandmaster Alchemy", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirAlchemy ), "alchemy elixir", 1, 1044253 );
			AddSkill( index, SkillName.Alchemy, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedAnatomyStudyBook), "Grandmaster Study Books", "Grandmaster Anatomy", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirAnatomy ), "anatomy elixir", 1, 1044253 );
			AddSkill( index, SkillName.Anatomy, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedAnimalLoreStudyBook), "Grandmaster Study Books", "Grandmaster Animal Lore", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirAnimalLore ), "animal lore elixir", 1, 1044253 );
			AddSkill( index, SkillName.AnimalLore, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedAnimalTamingStudyBook), "Grandmaster Study Books", "Grandmaster Animal Taming", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirAnimalTaming ), "animal taming elixir", 1, 1044253 );
			AddSkill( index, SkillName.AnimalTaming, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedArcheryStudyBook), "Grandmaster Study Books", "Grandmaster Archery", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirArchery ), "archery elixir", 1, 1044253 );
			AddSkill( index, SkillName.Archery, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedBeggingStudyBook), "Grandmaster Study Books", "Grandmaster Begging", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirBegging ), "begging elixir", 1, 1044253 );
			AddSkill( index, SkillName.Begging, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedBlacksmithStudyBook), "Grandmaster Study Books", "Grandmaster Blacksmithing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirBlacksmith ), "blacksmithing elixir", 1, 1044253 );
			AddSkill( index, SkillName.Blacksmith, 100.0, 100.0 );	

			index = AddCraft( typeof(AdvancedBushidoStudyBook), "Grandmaster Study Books", "Grandmaster Bushido", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirParry ), "parry elixir", 1, 1044253 );
			AddSkill( index, SkillName.Bushido, 100.0, 100.0 );	

			index = AddCraft( typeof(AdvancedCampingStudyBook), "Grandmaster Study Books", "Grandmaster Camping", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirCamping ), "camping elixir", 1, 1044253 );
			AddSkill( index, SkillName.Camping, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedCartographyStudyBook), "Grandmaster Study Books", "Grandmaster Cartography", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirCartography ), "cartography elixir", 1, 1044253 );
			AddSkill( index, SkillName.Cartography, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedChivalryStudyBook), "Grandmaster Study Books", "Grandmaster Chivlary", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( Diamond ), "diamond", 10, 1044253 );
			AddSkill( index, SkillName.Chivalry, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedCookingStudyBook), "Grandmaster Study Books", "Grandmaster Cooking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirCooking ), "cooking elixir", 1, 1044253 );
			AddSkill( index, SkillName.Cooking, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedDetectHiddenStudyBook), "Grandmaster Study Books", "Grandmaster Detecting Hidden", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirDetectHidden ), "detecting hidden elixir", 1, 1044253 );
			AddSkill( index, SkillName.DetectHidden, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedDiscordanceStudyBook), "Grandmaster Study Books", "Grandmaster Discordance", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirDiscordance ), "discordance elixir", 1, 1044253 );
			AddSkill( index, SkillName.Discordance, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedEvalIntStudyBook), "Grandmaster Study Books", "Grandmaster Evaluating Intelligence", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirEvalInt ), "evaluating intelligence elixir", 1, 1044253 );
			AddSkill( index, SkillName.EvalInt, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedFencingStudyBook), "Grandmaster Study Books", "Grandmaster Fencing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirFencing ), "fencing elixir", 1, 1044253 );
			AddSkill( index, SkillName.Fencing, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedFishingStudyBook), "Grandmaster Study Books", "Grandmaster Fishing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirFishing ), "fishing elixir", 1, 1044253 );
			AddSkill( index, SkillName.Fishing, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedFocusStudyBook), "Grandmaster Study Books", "Grandmaster Focus", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirFocus ), "focus elixir", 1, 1044253 );
			AddSkill( index, SkillName.Focus, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedForensicsStudyBook), "Grandmaster Study Books", "Grandmaster Forensic Evaluation", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirForensics ), "forensic evaluation elixir", 1, 1044253 );
			AddSkill( index, SkillName.Forensics, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedHealingStudyBook), "Grandmaster Study Books", "Grandmaster Healing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirHealing ), "healing elixir", 1, 1044253 );
			AddSkill( index, SkillName.Healing, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedHerdingStudyBook), "Grandmaster Study Books", "Grandmaster Herding", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirHerding ), "herding elixir", 1, 1044253 );
			AddSkill( index, SkillName.Herding, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedHidingStudyBook), "Grandmaster Study Books", "Grandmaster Hiding", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirHiding ), "hiding elixir", 1, 1044253 );
			AddSkill( index, SkillName.Hiding, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedInscribeStudyBook), "Grandmaster Study Books", "Grandmaster Inscription", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirInscribe ), "inscription elixir", 1, 1044253 );
			AddSkill( index, SkillName.Inscribe, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedItemIDStudyBook), "Grandmaster Study Books", "Grandmaster Item Identification", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirItemID ), "item identification elixir", 1, 1044253 );
			AddSkill( index, SkillName.ItemID, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedLockpickingStudyBook), "Grandmaster Study Books", "Grandmaster Lockpicking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirLockpicking ), "lockpicking elixir", 1, 1044253 );
			AddSkill( index, SkillName.Lockpicking, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedLumberjackingStudyBook), "Grandmaster Study Books", "Grandmaster Lumberjacking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirLumberjacking ), "lumberjacking elixir", 1, 1044253 );
			AddSkill( index, SkillName.Lumberjacking, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedMacingStudyBook), "Grandmaster Study Books", "Grandmaster Mace Fighting", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirMacing ), "mace fighting elixir", 1, 1044253 );
			AddSkill( index, SkillName.Macing, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedMageryStudyBook), "Grandmaster Study Books", "Grandmaster Magery", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( LiquidFire ), "liquid fire", 1, 1044253 );
			AddSkill( index, SkillName.Magery, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedMagicResistStudyBook), "Grandmaster Study Books", "Grandmaster Magic Resist", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirMagicResist ), "magic resistance elixir", 1, 1044253 );
			AddSkill( index, SkillName.MagicResist, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedMeditationStudyBook), "Grandmaster Study Books", "Grandmaster Meditation", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirMeditation ), "meditation elixir", 1, 1044253 );
			AddSkill( index, SkillName.Meditation, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedMiningStudyBook), "Grandmaster Study Books", "Grandmaster Mining", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirMining ), "mining elixir", 1, 1044253 );
			AddSkill( index, SkillName.Mining, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedMusicianshipStudyBook), "Grandmaster Study Books", "Grandmaster Musicianship", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirMusicianship ), "musicianship elixir", 1, 1044253 );
			AddSkill( index, SkillName.Musicianship, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedNecromancyStudyBook), "Grandmaster Study Books", "Grandmaster Necromancy", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirSpiritSpeak ), "spirit speak elixir", 1, 1044253 );
			AddSkill( index, SkillName.Necromancy, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedNinjitsuStudyBook), "Grandmaster Study Books", "Grandmaster Ninjitsu", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirStealth ), "stealth elixir", 1, 1044253 );
			AddSkill( index, SkillName.Ninjitsu, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedParryStudyBook), "Grandmaster Study Books", "Grandmaster Parry", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirParry ), "parry elixir", 1, 1044253 );
			AddSkill( index, SkillName.Parry, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedPeacemakingStudyBook), "Grandmaster Study Books", "Grandmaster Peacemaking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirPeacemaking ), "peacemaking elixir", 1, 1044253 );
			AddSkill( index, SkillName.Peacemaking, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedPoisoningStudyBook), "Grandmaster Study Books", "Grandmaster Poisoning", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirPoisoning ), "poisoning elixir", 1, 1044253 );
			AddSkill( index, SkillName.Poisoning, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedProvocationStudyBook), "Grandmaster Study Books", "Grandmaster Provocation", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirProvocation ), "provocation elixir", 1, 1044253 );
			AddSkill( index, SkillName.Provocation, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedRemoveTrapStudyBook), "Grandmaster Study Books", "Grandmaster Remove Trap", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirRemoveTrap ), "remove trap elixir", 1, 1044253 );
			AddSkill( index, SkillName.RemoveTrap, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedSnoopingStudyBook), "Grandmaster Study Books", "Grandmaster Snooping", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirSnooping ), "snooping elixir", 1, 1044253 );
			AddSkill( index, SkillName.Snooping, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedSpiritSpeakStudyBook), "Grandmaster Study Books", "Grandmaster SpiritSpeak", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirSpiritSpeak ), "spirit speak elixir", 1, 1044253 );
			AddSkill( index, SkillName.SpiritSpeak, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedStealingStudyBook), "Grandmaster Study Books", "Grandmaster Stealing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirStealing ), "stealing elixir", 1, 1044253 );
			AddSkill( index, SkillName.Stealing, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedStealthStudyBook), "Grandmaster Study Books", "Grandmaster Stealth", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirStealth ), "stealth elixir", 1, 1044253 );
			AddSkill( index, SkillName.Stealth, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedSwordsStudyBook), "Grandmaster Study Books", "Grandmaster Swordsmanship", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirSwords ), "swordsmanship elixir", 1, 1044253 );
			AddSkill( index, SkillName.Swords, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedTacticsStudyBook), "Grandmaster Study Books", "Grandmaster Tactics", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirTactics ), "tactics elixir", 1, 1044253 );
			AddSkill( index, SkillName.Tactics, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedTailoringStudyBook), "Grandmaster Study Books", "Grandmaster Tailoring", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirTailoring ), "tailoring elixir", 1, 1044253 );
			AddSkill( index, SkillName.Tailoring, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedTasteIDStudyBook), "Grandmaster Study Books", "Grandmaster Taste Identification", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirTasteID ), "taste identification elixir", 1, 1044253 );
			AddSkill( index, SkillName.TasteID, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedThrowingStudyBook), "Grandmaster Study Books", "Grandmaster Throwing", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirArchery ), "archery elixir", 1, 1044253 );
			AddSkill( index, SkillName.Throwing, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedTinkeringStudyBook), "Grandmaster Study Books", "Grandmaster Tinkering", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirTinkering ), "tinkering elixir", 1, 1044253 );
			AddSkill( index, SkillName.Tinkering, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedTrackingStudyBook), "Grandmaster Study Books", "Grandmaster Tracking", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirTracking ), "tracking elixir", 1, 1044253 );
			AddSkill( index, SkillName.Tracking, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedVeterinaryStudyBook), "Grandmaster Study Books", "Grandmaster Veterinary", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirVeterinary ), "veterninary elixir", 1, 1044253 );
			AddSkill( index, SkillName.Veterinary, 100.0, 100.0 );

			index = AddCraft( typeof(AdvancedWrestlingStudyBook), "Grandmaster Study Books", "Grandmaster Wrestling", 65.0, 85.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 3, 1044253 );
			AddRes( index, typeof( ArcaneGem ),"arcane gem", 2, 1044253 );
			AddRes( index, typeof( ElixirWrestling ), "wrestling elixir", 1, 1044253 );
			AddSkill( index, SkillName.Wrestling, 100.0, 100.0 );


			//Legendary Study Books
			index = AddCraft( typeof(AdvancedAlchemyStudyBook), "Legendary Study Books", "Legendary Alchemy", 120.0, 120.0, typeof(BlankScroll), 1044377, 10, 1044378);
			AddRes( index, typeof( Beeswax ), 1025154, 5, 1044253 );
			AddRes( index, typeof( ArcaneGem ), "arcane gem", 5, 1044253 );
			AddRes( index, typeof( ElixirAlchemy ), "alchemy elixir", 1, 1044253 );
			AddSkill( index, SkillName.Alchemy, 120.0, 120.0 );
			SetManaReq(index, 150);		



		}
	}
}