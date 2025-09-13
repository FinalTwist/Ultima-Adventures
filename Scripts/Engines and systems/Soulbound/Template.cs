using System;
using Server;
using Server.Misc;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Engines.Soulbound
{
	public class Template {
		public static string StatModAffix { get {return "SoulboundTemplate"; }}
		public static Template GetRandomTemplate(PlayerMobile player) {
			Array enums = Enum.GetValues(typeof(TemplateType));
			Random random = new Random();
			TemplateType template = (TemplateType)enums.GetValue(random.Next(enums.Length));
			switch (template) {
				case TemplateType.Warrior:
					return new WarriorTemplate(player);
				
				case TemplateType.Thief:
					return new ThiefTemplate(player);

				case TemplateType.Necromancer:
					return new NecromancerTemplate(player);

				case TemplateType.Mage:
					return new MageTemplate(player);

				case TemplateType.Healer:
					return new HealerTemplate(player);

				case TemplateType.Archer:
					return new ArcherTemplate(player);

				case TemplateType.Monk:
					return new MonkTemplate(player);

				case TemplateType.Ninja:
					return new NinjaTemplate(player);

				case TemplateType.Samurai:
					return new SamuraiTemplate(player);

				case TemplateType.Tamer:
					return new TamerTemplate(player);

				default:
					return new BardTemplate(player);

			}
		}
		public enum TemplateType : byte
		{
			Bard = 0,
			Warrior = 1,
			Thief = 2,
			Necromancer = 3,
			Mage = 4,
			Healer = 5,
			Archer = 6,
			Monk = 7,
			Ninja = 8,
			Samurai = 9,
			Tamer = 10
		}		
		private List<Item> m_Items;
		public List<Item> Items {
			get { return m_Items; }
			set { m_Items = value;}
		}

		private int m_NumberOfSkills;
		public int NumberOfSkills {
			get { return m_NumberOfSkills; }
			set { m_NumberOfSkills = value; }
		}

		public Template(PlayerMobile player) {
			Items = new List<Item>();
			NumberOfSkills = 0;
			if ( Utility.RandomBool() ) 
			{
				player.Body = 0x191;
			} else {
				player.Body = 0x190; 
			}
			player.OriginalBody = player.Body;
		}
		public void SetWizardStats(PlayerMobile player) {
			player.RawStr = 50;
			player.RawInt = 75;
		}
		public void SetDexerStats(PlayerMobile player) {
			player.RawStr = 50;
			player.RawDex = 55;
		}
		public void SetAllRounderStats(PlayerMobile player) {
			player.RawStr = 55;
			player.RawDex = 55;
			player.RawInt = 55;
		}
		public void SetWizardSkills(PlayerMobile player) {
			player.Skills[SkillName.Meditation].Base = 75;
			player.Skills[SkillName.EvalInt].Base = 75;
		}
		public int NonSamuraiFighterArmor() {
			bool condition = false;
			int iArmor = 0;
			while (!condition) {
				iArmor = Utility.RandomMinMax( 1, 6 );
				if (iArmor != 5) {
					condition = true;
				}
			}
			return iArmor;
		}

		public void FixLayerStr(PlayerMobile player) {
			Array layerEnums = Enum.GetValues(typeof(Layer));
			foreach (Layer layer in layerEnums) {
				Item item = player.FindItemOnLayer(layer);
				if (item != null) {
					if (item is BaseArmor) {
						((BaseArmor)item).StrRequirement = player.Str;				
					} else if (item is BaseWeapon) {
						((BaseWeapon)item).StrRequirement = player.Str;				
					}
				} 
			}
		}

		public void UndressStartingGear(PlayerMobile player) {
			Array layerEnums = Enum.GetValues(typeof(Layer));
			foreach (Layer layer in layerEnums) {
				if (layer == Layer.Shirt || layer == Layer.Pants || layer == Layer.Shoes) {
					Item item = player.FindItemOnLayer(layer);
					if (item != null) {
						item.Delete();
					} 
				}
			}
		}

		public void AddWeaponSkill(PlayerMobile player) {
			if (player.Backpack != null) {
				BaseWeapon weapon = player.FindItemOnLayer( Layer.OneHanded ) as BaseWeapon;
				if( weapon == null )
					weapon = player.FindItemOnLayer( Layer.TwoHanded ) as BaseWeapon;

				BaseShield shield =  player.FindItemOnLayer( Layer.TwoHanded ) as BaseShield;

				if (weapon != null) {
					++NumberOfSkills;
					player.Skills[weapon.DefSkill].Base = 75;
				}
				if (shield != null) {
					++NumberOfSkills;
					player.Skills[SkillName.Parry].Base = 75;
				}
			}
		}
		public void DeleteShield(PlayerMobile player) {
			BaseShield shield =  player.FindItemOnLayer( Layer.TwoHanded ) as BaseShield;
			if (shield != null) {
				shield.Delete();
			}
		}
		public void DeleteWeapon(PlayerMobile player) {
			BaseWeapon weapon = player.FindItemOnLayer( Layer.OneHanded ) as BaseWeapon;
			if( weapon == null )
				weapon = player.FindItemOnLayer( Layer.TwoHanded ) as BaseWeapon;
			if (weapon != null) {
				weapon.Delete();
			}
		}
	}
	public class WarriorTemplate : Template {
		public WarriorTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			int iArmor = this.NonSamuraiFighterArmor();
			Server.Misc.IntelligentAction.DressUpFighters( player, "", false, 1);
			this.DeleteWeapon(player);
			this.DeleteShield(player);
			player.AddItem(new Halberd());
			this.FixLayerStr(player);
			player.RawStr = 75;
			player.RawDex = 50;
			player.Skills[SkillName.Tactics].Base = 75;
			int random = Utility.RandomMinMax(1,3);
			this.AddWeaponSkill(player);
			Items.Add(new Bandage(250));
			if (NumberOfSkills < 3) {
				switch (random) {
					case 1:
						player.Skills[SkillName.Anatomy].Base = 75;
					break;
					case 2:
						player.Skills[SkillName.Blacksmith].Base = 75;
						Items.Add(new SmithHammer());
						Items.Add(new Shovel(30));
					break;
					case 3:
						player.Skills[SkillName.ArmsLore].Base = 75;
					break;
				}
			}
			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 15, 5, 5, 2, 1);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 15, 5, 5, 2, 1);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 15, 5, 5, 2, 1);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 15, 5, 5, 2, 1);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 15, 5, 5, 2, 1);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 15, 5, 5, 2, 1);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);


			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 1, 5, 5, 1, 5);
			}
	
			//Item firstvalid = player.FindItemOnLayer( Layer.FirstValid );
		}
	}

	public class TamerTemplate : Template {
		public TamerTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			Server.Misc.IntelligentAction.DressUpRogues( player, "", false, 3, "" );
			this.DeleteWeapon(player);
			this.DeleteShield(player);
			this.SetAllRounderStats(player);
			player.Skills[SkillName.AnimalTaming].Base = 75;
			player.Skills[SkillName.AnimalLore].Base = 75;
			player.Skills[SkillName.Veterinary].Base = 75;
			Items.Add(new Bandage(250));
			player.AddItem(new ShepherdsCrook());

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 8, 9, 9, 4, 4);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 8, 9, 9, 4, 4);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 8, 9, 9, 4, 4);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 8, 9, 9, 4, 4);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 8, 9, 9, 4, 4);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor)
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 8, 9, 9, 4, 4);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);


			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 10, 5, 5, 1, 5);
			}
		}
	}

	public class ChivalryTemplate : Template {
		public ChivalryTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			int iArmor = this.NonSamuraiFighterArmor();
			Server.Misc.IntelligentAction.DressUpFighters( player, "", false, iArmor );
			this.SetAllRounderStats(player);
			Items.Add(new BookOfChivalry());
			this.AddWeaponSkill(player);
			player.Skills[ SkillName.Chivalry].Base = 75;
			if (NumberOfSkills < 3) {
				player.Skills[ SkillName.MagicResist].Base = 75;
			}

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 15, 5, 5, 2, 1);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 15, 5, 5, 2, 1);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 15, 5, 5, 2, 1);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 15, 5, 5, 2, 1);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 15, 5, 5, 2, 1);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 15, 5, 5, 2, 1);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 1, 8, 8, 1, 5);
			}

		}
	}

	public class NinjaTemplate : Template {
		public NinjaTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			Server.Misc.IntelligentAction.DressUpRogues( player, "", false, 2, "ninja" );
			this.DeleteShield(player);
			this.SetAllRounderStats(player);
			Items.Add(new BookOfNinjitsu());
			int random = Utility.RandomMinMax(1,4);
			int amount = Utility.RandomMinMax(5,25);
			switch (random) {
				case 1:
				Fukiya fukiya = new Fukiya();
				fukiya.UsesRemaining = amount;
				Items.Add(fukiya);
				break;
				case 2:
				Items.Add(new EggBomb());
				break;
				case 3:
				Items.Add(new SmokeBomb());
				break;
				case 4:
				Shuriken shuriken = new Shuriken();
				shuriken.UsesRemaining = amount;
				Items.Add(shuriken);
				break;
			}
			player.Skills[SkillName.Ninjitsu].Base = 75;
			player.Skills[SkillName.Hiding].Base = 75;
			player.Skills[SkillName.Stealth].Base = 75;

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 8, 9, 9, 4, 4);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 8, 9, 9, 4, 4);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 8, 9, 9, 4, 4);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 8, 9, 9, 4, 4);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 8, 9, 9, 4, 4);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 8, 9, 9, 4, 4);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 8, 8, 8, 1, 8);
			}
		}
	}

	public class SamuraiTemplate : Template {
		public SamuraiTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			Server.Misc.IntelligentAction.DressUpFighters( player, "", true, 5 );
			this.DeleteWeapon(player);
			this.DeleteShield(player);
			this.SetAllRounderStats(player);
			Items.Add(new BookOfBushido());
			int random = Utility.RandomMinMax(1,4);
			switch (random) {
				case 1:
					player.AddItem(new Daisho());
				break;
				case 2:
					player.AddItem(new Tetsubo());
				break;
				case 3:
					player.AddItem(new Lajatang());
				break;
				case 4:
					player.AddItem(new NoDachi());
				break;
			}
			this.FixLayerStr(player);
			this.AddWeaponSkill(player);
			player.Skills[ SkillName.Bushido].Base = 75;
			player.Skills[ SkillName.Parry].Base = 75;

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 9, 6, 6, 8, 4);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 9, 6, 6, 8, 4);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 9, 6, 6, 8, 4);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 9, 6, 6, 8, 4);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 9, 6, 6, 8, 4);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 9, 6, 6, 8, 4);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 8, 8, 8, 1, 5);
			}
		}
	}

	public class ArcherTemplate : Template {
		public ArcherTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			Server.Misc.IntelligentAction.DressUpRogues( player, "", false, 2, "hunter" );
			this.DeleteWeapon(player);
			this.DeleteShield(player);
			int random = Utility.RandomMinMax(1,4);
				switch (random) {
				case 1:
					player.AddItem(new Bow());
					Items.Add(new Arrow(250));
				break;
				case 2:
					player.AddItem(new Crossbow());
				break;
				case 3:
					player.AddItem(new HeavyCrossbow());
				break;
				case 4:
					player.AddItem(new RepeatingCrossbow());
				break;
			}
			if (random > 1) {
				Items.Add(new Bolt(250));
			}
			this.FixLayerStr(player);
			this.AddWeaponSkill(player);
			Items.Add(new FletcherTools(30));
			Items.Add(new Hatchet());
			this.SetDexerStats(player);
			player.Skills[SkillName.Archery].Base = 75;
			player.Skills[SkillName.Tactics].Base = 75;
			player.Skills[SkillName.Fletching].Base = 75;

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 8, 9, 9, 4, 4);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 8, 9, 9, 4, 4);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 8, 9, 9, 4, 4);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 8, 9, 9, 4, 4);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 8, 9, 9, 4, 4);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 8, 9, 9, 4, 4);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 3, 9, 9, 1, 5);
			}	
		}
	}

	public class MonkTemplate : Template {
		public MonkTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			player.AddItem(new Robe(Utility.RandomNeutralHue()));
			player.AddItem(new Sandals(Utility.RandomNeutralHue()));
			this.SetDexerStats(player);
			this.DeleteWeapon(player);
			this.DeleteShield(player);
			Items.Add(new MysticSpellbook());
			Items.Add(new PugilistGloves());
			player.Skills[SkillName.Meditation].Base = 100;
			player.Skills[SkillName.Focus].Base = 100;
			player.Skills[SkillName.Wrestling].Base = 85;
			player.Skills[SkillName.Tactics].Base = 85;
			player.Skills[SkillName.Anatomy].Base = 85;

			Item robes = new VagabondRobe();
			player.AddItem( robes ); 

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 8, 9, 9, 4, 4);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 8, 9, 9, 4, 4);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 8, 9, 9, 4, 4);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 8, 9, 9, 4, 4);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 8, 9, 9, 4, 4);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 8, 9, 9, 4, 4);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 3, 5, 5, 1, 5);
			}
			Item robe = player.FindItemOnLayer( Layer.OuterTorso );
			if (robe != null && robe is BaseArmor)
			{
				((BaseArmor)robe).AddResists( (BaseArmor)robe, 20, 20, 10, 10, 10);
			}
			if (robe != null && robe is BaseClothing)
				((BaseClothing)robe).AddResists( (BaseClothing)robe, 20, 20, 10, 10, 10);

		}
	}

	public class HealerTemplate : Template {
		public HealerTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			Server.Misc.IntelligentAction.DressUpWizards( player );
			this.SetDexerStats(player);
			Items.Add(new Bandage(200));
			Items.Add(new Bottle(25));
			MortarPestle pestle = new MortarPestle();
			pestle.UsesRemaining = 30;
			Items.Add(pestle);
			Items.Add(new BagOfReagents());
			Items.Add(new LesserCurePotion());
			Items.Add(new LesserHealPotion());
			player.Skills[SkillName.Healing].Base = 75;
			player.Skills[SkillName.Anatomy].Base = 75;
			player.Skills[SkillName.Alchemy].Base = 75;	

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 8, 9, 9, 4, 4);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item robe = player.FindItemOnLayer( Layer.OuterTorso );
			if (robe != null && robe is BaseArmor )
			{
				((BaseArmor)robe).AddResists( (BaseArmor)robe, 12, 9, 9, 4, 4);
			}
			if (robe != null && robe is BaseClothing)
				((BaseClothing)robe).AddResists( (BaseClothing)robe, 12, 9, 9, 4, 4);

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 8, 9, 9, 4, 4);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 8, 9, 9, 4, 4);
			}
			if (pants != null && pants is BaseClothing)
				((BaseClothing)pants).AddResists( (BaseClothing)pants, 8, 9, 9, 4, 4);

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 8, 9, 9, 4, 4);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 8, 9, 9, 4, 4);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 8, 9, 9, 4, 4);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 3, 5, 5, 1, 5);
			}

		}
	}
	public class MageTemplate: Template {
		public MageTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			Server.Misc.IntelligentAction.DressUpWizards( player );
			Items.Add(new Spellbook());
			Items.Add(new BagOfReagents());
			this.SetWizardStats(player);
			this.SetWizardSkills(player);
			player.Skills[ SkillName.Magery].Base = 75;
			player.Skills[ SkillName.EvalInt].Base = 75;
			player.Skills[ SkillName.Meditation].Base = 75;
			int counter = 0;
			while (counter < 3) {
				int random = Utility.RandomMinMax(1,6);
				switch (random) {
					case 1:
					FireballScroll fireballScroll = (FireballScroll)player.Backpack.FindItemByType(typeof(FireballScroll));
					if (fireballScroll == null) {
						Items.Add(new FireballScroll());
						++counter;
					}
					break;
					case 2:
					EnergyBoltScroll energyBoltScroll = (EnergyBoltScroll)player.Backpack.FindItemByType(typeof(EnergyBoltScroll));
					if (energyBoltScroll == null) {
						Items.Add(new EnergyBoltScroll());
						++counter;
					}
					break;
					case 3:
					LightningScroll lightningScroll = (LightningScroll)player.Backpack.FindItemByType(typeof(LightningScroll));
					if (lightningScroll == null) {
						Items.Add(new LightningScroll());
						++counter;
					}
					break;
					case 4:
					GreaterHealScroll greaterHealScroll = (GreaterHealScroll)player.Backpack.FindItemByType(typeof(GreaterHealScroll));
					if (greaterHealScroll == null) {
						Items.Add(new GreaterHealScroll());
						++counter;
					}
					break;
					case 5:
					MindBlastScroll mindBlastScroll = (MindBlastScroll)player.Backpack.FindItemByType(typeof(MindBlastScroll));
					if (mindBlastScroll == null) {
						Items.Add(new MindBlastScroll());
						++counter;
					}
					break;
					case 6:
					CureScroll cureScroll = (CureScroll)player.Backpack.FindItemByType(typeof(CureScroll));
					if (cureScroll == null) {
						Items.Add(new CureScroll());
						++counter;
					}
					break;
				}
			}

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 4, 6, 6, 6, 3);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 4, 6, 6, 6, 3);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 4, 6, 6, 6, 3);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 4, 6, 6, 6, 3);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 4, 6, 6, 6, 3);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 4, 6, 6, 6, 3);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 3, 5, 5, 1, 5);
			}
			Item robe = player.FindItemOnLayer( Layer.OuterTorso );
			if (robe != null && robe is BaseArmor)
			{
				((BaseArmor)robe).AddResists( (BaseArmor)robe, 15, 10, 10, 10, 13);
			}
			if (robe != null && robe is BaseClothing)
				((BaseClothing)robe).AddResists( (BaseClothing)robe, 15, 10, 10, 10, 13);
		}
	}

	public class NecromancerTemplate : Template {
		public NecromancerTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			Server.Misc.IntelligentAction.DressUpWizards( player );
			Items.Add(new NecromancerSpellbook());
			Items.Add(new BagOfNecroReagents());
			this.SetWizardStats(player);
			this.SetWizardSkills(player);
			player.Skills[SkillName.Necromancy].Base = 75;
			player.Skills[SkillName.SpiritSpeak].Base = 75;
			player.Skills[SkillName.Forensics].Base = 75;
			int counter = 0;
			while (counter < Utility.RandomMinMax(3,6)) {
				int random = Utility.RandomMinMax(1,6);
				switch (random) {
					case 1:
					WitherScroll witherScroll = (WitherScroll)player.Backpack.FindItemByType(typeof(WitherScroll));
					if (witherScroll == null) {
						Items.Add(new WitherScroll());
						++counter;
					}
					break;
					case 2:
					StrangleScroll strangleScroll = (StrangleScroll)player.Backpack.FindItemByType(typeof(StrangleScroll));
					if (strangleScroll == null) {
						Items.Add(new StrangleScroll());
						++counter;
					}
					break;
					case 3:
					PainSpikeScroll painSpikeScroll = (PainSpikeScroll)player.Backpack.FindItemByType(typeof(PainSpikeScroll));
					if (painSpikeScroll == null) {
						Items.Add(new PainSpikeScroll());
						++counter;
					}
					break;
					case 4:
					MindRotScroll mindRotScroll = (MindRotScroll)player.Backpack.FindItemByType(typeof(MindRotScroll));
					if (mindRotScroll == null) {
						Items.Add(new MindRotScroll());
						++counter;
					}
					break;
					case 5:
					EvilOmenScroll evilOmenScroll = (EvilOmenScroll)player.Backpack.FindItemByType(typeof(EvilOmenScroll));
					if (evilOmenScroll == null) {
						Items.Add(new EvilOmenScroll());
						++counter;
					}
					break;
					case 6:
					AnimateDeadScroll animateDeadScroll = (AnimateDeadScroll)player.Backpack.FindItemByType(typeof(AnimateDeadScroll));
					if (animateDeadScroll == null) {
						Items.Add(new AnimateDeadScroll());
						++counter;
					}
					break;
				}
			}

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 4, 6, 6, 6, 3);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 4, 6, 6, 6, 3);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 4, 6, 6, 6, 3);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 4, 6, 6, 6, 3);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 4, 6, 6, 6, 3);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 4, 6, 6, 6, 3);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 3, 5, 5, 1, 5);
			}
			Item robe = player.FindItemOnLayer( Layer.OuterTorso );
			if (robe != null && robe is BaseArmor)
			{
				((BaseArmor)robe).AddResists( (BaseArmor)robe, 15, 10, 10, 10, 13);
			}
			if (robe != null && robe is BaseClothing)
				((BaseClothing)robe).AddResists( (BaseClothing)robe, 15, 10, 10, 10, 13);

		}
	}

	public class BardTemplate : Template {
		public BardTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			Server.Misc.IntelligentAction.DressUpRogues( player, "", false, 3, "" );
			Items.Add(new SongBook());
			this.DeleteWeapon(player);
			this.DeleteShield(player);
			this.SetAllRounderStats(player);
			player.Skills[SkillName.Musicianship].Base = 85;
			player.Skills[SkillName.Carpentry].Base = 75;
			Items.Add(new Hatchet());
			Saw saw = new Saw();
			saw.UsesRemaining = 30;
			Items.Add(saw);
			int random = Utility.RandomMinMax(1,3);
			switch (random) {
				case 1:
					Items.Add(new Harp());
				break;
				case 2:
					Items.Add(new Drums());
				break;
				case 3:
					Items.Add(new Tambourine());
				break;
			}
			random = Utility.RandomMinMax(1,3);
			switch (random) {
				case 1:
					player.Skills[SkillName.Peacemaking].Base = 75;
				break;
				case 2:
					player.Skills[SkillName.Provocation].Base = 75;
				break;
				case 3:
					player.Skills[SkillName.Discordance].Base = 75;
				break;
			}
			int counter = 0;
			while (counter < 3) {
				random = Utility.RandomMinMax(1,6);
				switch (random) {
					case 1:
					ArmysPaeonScroll armysPaeonScroll = (ArmysPaeonScroll)player.Backpack.FindItemByType(typeof(ArmysPaeonScroll));
					if (armysPaeonScroll == null) {
						Items.Add(new ArmysPaeonScroll());
						++counter;
					}
					break;
					case 2:
					FireCarolScroll fireCarolScroll = (FireCarolScroll)player.Backpack.FindItemByType(typeof(FireCarolScroll));
					if (fireCarolScroll == null) {
						Items.Add(new FireCarolScroll());
						++counter;
					}
					break;
					case 3:
					EnergyCarolScroll energyCarolScroll = (EnergyCarolScroll)player.Backpack.FindItemByType(typeof(EnergyCarolScroll));
					if (energyCarolScroll == null) {
						Items.Add(new EnergyCarolScroll());
						++counter;
					}
					break;
					case 4:
					KnightsMinneScroll knightsMinneScroll = (KnightsMinneScroll)player.Backpack.FindItemByType(typeof(KnightsMinneScroll));
					if (knightsMinneScroll == null) {
						Items.Add(new KnightsMinneScroll());
						++counter;
					}
					break;
					case 5:
					PoisonCarolScroll poisonCarolScroll = (PoisonCarolScroll)player.Backpack.FindItemByType(typeof(PoisonCarolScroll));
					if (poisonCarolScroll == null) {
						Items.Add(new PoisonCarolScroll());
						++counter;
					}
					break;
					case 6:
					IceCarolScroll iceCarolScroll = (IceCarolScroll)player.Backpack.FindItemByType(typeof(IceCarolScroll));
					if (iceCarolScroll == null) {
						Items.Add(new IceCarolScroll());
						++counter;
					}
					break;
				}
			}

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 8, 9, 9, 4, 4);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 8, 9, 9, 4, 4);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 8, 9, 9, 4, 4);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 8, 9, 9, 4, 4);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 8, 9, 9, 4, 4);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 8, 9, 9, 4, 4);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 3, 5, 5, 1, 5);
			}

		}
	}

	public class ThiefTemplate : Template {
		public ThiefTemplate(PlayerMobile player) : base(player) {
			this.UndressStartingGear(player);
			Server.Misc.IntelligentAction.DressUpRogues( player, "", false, 3, "" );
			player.AddItem(new Cloak(Utility.RandomNeutralHue()));
			this.DeleteShield(player);
			this.FixLayerStr(player);
			this.SetDexerStats(player);
			// if (player.Backpack != null) {
			// 	this.AddWeaponSkill(player);
			// }
			Items.Add(new Lockpick(50));
			
			player.Skills[ SkillName.Snooping].Base = 75;
			player.Skills[ SkillName.Stealing].Base = 75;
			
			int random = Utility.RandomMinMax(1,3);
			switch(random) {
				case 1:
					player.Skills[SkillName.RemoveTrap].Base = 75;
				break;
				case 2:
					player.Skills[ SkillName.DetectHidden].Base = 75;
				break;
				case 3:
					player.Skills[SkillName.Lockpicking].Base = 75;
				break;
			}

			Item shirt = player.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && shirt is BaseArmor)
			{
				((BaseArmor)shirt).AddResists( (BaseArmor)shirt, 8, 9, 9, 4, 4);
			}
			else if (shirt != null && (shirt is BaseClothing))
			{
				((BaseClothing)shirt).AddResists( (BaseClothing)shirt, 8, 8, 8, 1, 5);
			}

			Item glove = player.FindItemOnLayer( Layer.Gloves );
			if (glove != null && glove is BaseArmor)
			{
				((BaseArmor)glove).AddResists( (BaseArmor)glove, 8, 9, 9, 4, 4);
			}

			Item pants = player.FindItemOnLayer( Layer.Pants );
			if (pants != null && pants is BaseArmor)
			{
				((BaseArmor)pants).AddResists( (BaseArmor)pants, 8, 9, 9, 4, 4);
			}

			Item neck = player.FindItemOnLayer( Layer.Neck );
			if (neck != null && neck is BaseArmor)
			{
				((BaseArmor)neck).AddResists( (BaseArmor)neck, 8, 9, 9, 4, 4);
			}

			Item arms = player.FindItemOnLayer( Layer.Arms );
			if (arms != null && arms is BaseArmor)
			{
				((BaseArmor)arms).AddResists( (BaseArmor)arms, 8, 9, 9, 4, 4);
			}

			Item helm = player.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm is BaseArmor )
			{
				((BaseArmor)helm).AddResists( (BaseArmor)helm, 8, 9, 9, 4, 4);
			}
			else if (helm is BaseClothing)
				((BaseClothing)helm).AddResists( (BaseClothing)helm, 3, 5, 5, 1, 5);

			Item cloak = player.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && (cloak is BaseClothing))
			{
				((BaseClothing)cloak).AddResists( (BaseClothing)cloak, 3, 5, 5, 1, 5);
			}
		}



	}
}
