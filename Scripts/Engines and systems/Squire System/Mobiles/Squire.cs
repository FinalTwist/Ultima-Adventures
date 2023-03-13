using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Misc;
using Server.SkillHandlers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Server.Targeting;
using Server.ContextMenus;
using Server.HuePickers;
using Server.Prompts;
using Server.Spells; // Added 1.9.2
using Server.Regions; //Adden from Henchman
using Server.Spells.Bushido; //Rafa Added
using Server.Spells.Necromancy; //Rafa Added

namespace Server.Mobiles
{
    [CorpseName( "a squire's corpse" )]
    public class Squire : BaseCreature
    {
	private static Hashtable s_Keywords;

	public DateTime m_Delay;
	public DateTime m_MusicDelay;
	public DateTime m_ThrowDelay;
	public DateTime m_HideDelay;
	public DateTime m_AnnoyanceDelay;
	public DateTime m_PickUpEmoteDelay;
	public DateTime m_StrengthDelay;
	public DateTime m_AgilityDelay;
	public DateTime m_HealthDelay;
	public DateTime m_CureDelay;
	public DateTime m_LockpickDelay; //Added 1.8.5
	public DateTime m_StealingDelay; //Added 1.8.5
	public DateTime m_SpiritDelay; // Added 1.9.2
	public DateTime m_HungerDecay; // Added 1.9.4
	public DateTime m_MeditateDelay; // Added 1.9.6
	public DateTime m_SpellCastDelay; // Added 1.9.6
	public DateTime m_WeaponAbilityDelay; // Added 1.9.7
	public DateTime m_AutoLootDelay; // added unblest

	public Item m_WeaponSetOneSlotOne; //Begin Added 1.9
	public Item m_WeaponSetOneSlotTwo;
	public Item m_WeaponSetTwoSlotOne;
	public Item m_WeaponSetTwoSlotTwo;
	public Item m_WeaponSetThreeSlotOne;
	public Item m_WeaponSetThreeSlotTwo; //End Added 1.9

	public string m_MasterNickname;
	public string m_SquireNickname;
	public string m_SquireTeam; // Added 1.9.7

	public bool m_Inspectable;
	public bool m_AutoHealAnimals;
	public bool m_SquireBeQuiet;
	public bool m_AutoUseHealthPotion;
	public bool m_AutoUseCurePotion;
	public bool m_AutoHealSelf;
	public bool m_AutoHealMaster;
	public bool m_AutoHealOther;
	public bool m_AutoPickupAmmo;
	public bool m_DesperateMasterRun;
	public bool m_AutoUsePowerScroll;
	public bool m_AutoUseTScroll; //Added 1.9.1
	public bool m_AutoEquipLoot; //Added 1.9.1
	public bool m_AutoUseSpiritSpeak; // Added 1.9.2
	public bool m_SpiritWorldConnected; // Added 1.9.2
	public bool m_AutoRezMaster; // Added 1.9.2
	public bool m_AutoRezAlly; // Added 1.9.2
	public bool m_AutoCastCloseWounds; // Added 1.9.6
	public bool m_AutoCastCleanseByFire; // Added 1.9.6
	public bool m_AutoUseWeaponAbility; // Added 1.9.7
	public bool m_AutoCastCloseWoundsMaster; // Added 1.9.7
	public bool m_AutoCastCleanseByFireMaster; // Added 1.9.7
	public bool m_AutoCastCloseWoundsAlly; // Added 1.9.7
	public bool m_AutoCastCleanseByFireAlly; // Added 1.9.7
	public bool m_AutoCastConfidence; //Added Rafa
	public bool m_AutoLightningStrike; //Added Rafa
	public bool m_AutoEvasion; //Added Rafa
	public bool m_AutoCounterAttack; //Added Rafa
	public bool m_AutoMomentumStrike; //Added Rafa
	public bool m_AutoHonorableExecution; //Added Rafa
	public bool m_AutoPainSpike;  //Added Necro Rafa
	public bool m_AutoPoisonStrike;  //Added Necro Rafa
	public bool m_AutoWraithForm;  //Added Necro Rafa
	public bool m_AutoCurseWeapon;  //Added Necro Rafa
	public bool m_AutoWither;  //Added Necro Rafa
	public bool m_AutoLichForm;  //Added Necro Rafa
	public bool m_AutoVampiricEmbrace;  //Added Necro Rafa
	public bool m_OnlyLootGoldAndMap;  //Added unblest
	public bool m_AutoLoot;  // Added unblest
	public bool m_ConvenientStackable;  // added unblest
	public bool m_LootGold; // added unblest
	public bool m_LootMap; // added unblest
	public bool m_LootZoogiFungus; // added unblest
	public bool m_LootDaemonBone; // added unblest

	[CommandProperty( AccessLevel.GameMaster )]
	public string Nickname
	{
	    get{ return m_MasterNickname; }
	    set{ m_MasterNickname = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public string SNickname
	{
	    get{ return m_SquireNickname; }
	    set{ m_SquireNickname = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool Inspectable
	{
	    get{ return m_Inspectable; }
	    set{ m_Inspectable = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool HealAnimals
	{
	    get{ return m_AutoHealAnimals; }
	    set{ m_AutoHealAnimals = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool SquireBeQuiet
	{
	    get{ return m_SquireBeQuiet; }
	    set{ m_SquireBeQuiet = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool AutoUseHealthPotion
	{
	    get{ return m_AutoUseHealthPotion; }
	    set{ m_AutoUseHealthPotion = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool AutoUseCurePotion
	{
	    get{ return m_AutoUseCurePotion; }
	    set{ m_AutoUseCurePotion = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool AutoHealSelf
	{
	    get{ return m_AutoHealSelf; }
	    set{ m_AutoHealSelf = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool AutoHealMaster
	{
	    get{ return m_AutoHealMaster; }
	    set{ m_AutoHealMaster = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool AutoHealOther
	{
	    get{ return m_AutoHealOther; }
	    set{ m_AutoHealOther = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool AutoPickupAmmo
	{
	    get{ return m_AutoPickupAmmo; }
	    set{ m_AutoPickupAmmo = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool DesperateMasterRun
	{
	    get{ return m_DesperateMasterRun; }
	    set{ m_DesperateMasterRun = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public bool AutoUsePowerScroll
	{
	    get{ return m_AutoUsePowerScroll; }
	    set{ m_AutoUsePowerScroll = value; InvalidateProperties(); }
	}

	[CommandProperty( AccessLevel.GameMaster )] //Added 1.9.1
	    public bool AutoUseTScroll
	    {
		get{ return m_AutoUseTScroll; }
		set{ m_AutoUseTScroll = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] //Added 1.9.1
	    public bool AutoEquipLoot
	    {
		get{ return m_AutoEquipLoot; }
		set{ m_AutoEquipLoot = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.2
	    public bool AutoUseSpiritSpeak
	    {
		get{ return m_AutoUseSpiritSpeak; }
		set{ m_AutoUseSpiritSpeak = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.2
	    public bool AutoRezMaster
	    {
		get{ return m_AutoRezMaster; }
		set{ m_AutoRezMaster = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.2
	    public bool AutoRezAlly
	    {
		get{ return m_AutoRezAlly; }
		set{ m_AutoRezAlly = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.6
	    public bool AutoCastCloseWounds
	    {
		get{ return m_AutoCastCloseWounds; }
		set{ m_AutoCastCloseWounds = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.6
	    public bool AutoCastCleanseByFire
	    {
		get{ return m_AutoCastCleanseByFire; }
		set{ m_AutoCastCleanseByFire = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.7
	    public bool AutoUseWeaponAbility
	    {
		get{ return m_AutoUseWeaponAbility; }
		set{ m_AutoUseWeaponAbility = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.7
	    public bool AutoCastCloseWoundsMaster
	    {
		get{ return m_AutoCastCloseWoundsMaster; }
		set{ m_AutoCastCloseWoundsMaster = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.7
	    public bool AutoCastCleanseByFireMaster
	    {
		get{ return m_AutoCastCleanseByFireMaster; }
		set{ m_AutoCastCleanseByFireMaster = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.7
	    public bool AutoCastCloseWoundsAlly
	    {
		get{ return m_AutoCastCloseWoundsAlly; }
		set{ m_AutoCastCloseWoundsAlly = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.7
	    public bool AutoCastCleanseByFireAlly
	    {
		get{ return m_AutoCastCleanseByFireAlly; }
		set{ m_AutoCastCleanseByFireAlly = value; InvalidateProperties(); }
	    }

	[CommandProperty( AccessLevel.GameMaster )] // Added 1.9.7
	    public string STeam
	    {
		get { return m_SquireTeam; }
		set { m_SquireTeam = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoCastConfidence
	    {
		get { return m_AutoCastConfidence; }
		set { m_AutoCastConfidence = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoLightningStrike
	    {
		get { return m_AutoLightningStrike; }
		set { m_AutoLightningStrike = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoEvasion
	    {
		get { return m_AutoEvasion; }
		set { m_AutoEvasion = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoCounterAttack
	    {
		get { return m_AutoCounterAttack; }
		set { m_AutoCounterAttack = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoMomentumStrike
	    {
		get { return m_AutoMomentumStrike; }
		set { m_AutoMomentumStrike = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoHonorableExecution
	    {
		get { return m_AutoHonorableExecution; }
		set { m_AutoHonorableExecution = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoPainSpike
	    {
		get { return m_AutoPainSpike; }
		set { m_AutoPainSpike = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoPoisonStrike
	    {
		get { return m_AutoPoisonStrike; }
		set { m_AutoPoisonStrike = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoWraithForm
	    {
		get { return m_AutoWraithForm; }
		set { m_AutoWraithForm = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoCurseWeapon
	    {
		get { return m_AutoCurseWeapon; }
		set { m_AutoCurseWeapon = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoWither
	    {
		get { return m_AutoWither; }
		set { m_AutoWither = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoLichForm
	    {
		get { return m_AutoLichForm; }
		set { m_AutoLichForm = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] // Added Rafa
	    public bool AutoVampiricEmbrace
	    {
		get { return m_AutoVampiricEmbrace; }
		set { m_AutoVampiricEmbrace = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] //Added unblest
	    public bool OnlyLootGoldAndMap
	    {
		get { return m_OnlyLootGoldAndMap; }
		set { m_OnlyLootGoldAndMap = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] //Added unblest
	    public bool AutoLoot
	    {
		get { return m_AutoLoot; }
		set { m_AutoLoot = value; InvalidateProperties(); }
	    }

	[CommandProperty(AccessLevel.GameMaster)] //Added unblest
	    public bool ConvenientStackable
	    {
		get { return m_ConvenientStackable; }
		set { m_ConvenientStackable = value; InvalidateProperties(); }
	    }
	[CommandProperty(AccessLevel.GameMaster)] //Added unblest
	    public bool LootGold
	    {
		get { return m_LootGold; }
		set { m_LootGold = value; InvalidateProperties(); }
	    }
	[CommandProperty(AccessLevel.GameMaster)] //Added unblest
	    public bool LootMap
	    {
		get { return m_LootMap; }
		set { m_LootMap = value; InvalidateProperties(); }
	    }
	[CommandProperty(AccessLevel.GameMaster)] //Added unblest
	    public bool LootZoogiFungus
	    {
		get { return m_LootZoogiFungus; }
		set { m_LootZoogiFungus = value; InvalidateProperties(); }
	    }
	public bool LootDaemonBone
	{
	    get { return m_LootDaemonBone; }
	    set { m_LootDaemonBone = value; InvalidateProperties(); }
	}

	enum SquireCommands 
	{ 
	    None = 0, Restyle, Stop, Stay, Follow, FollowMe, Come, 
	    Throw, ChangeNickname, ChangeSNickname, Heal, Dress, Undress, 
	    Mount, Dismount, Stats, Unload, List, Arm, Grab, GrabAll, 
	    Loot, LootAll, Attack, Kill, Guard, GuardMe, RenameSelf, 
	    Backpack, PlayMusic, Provoke, Discord, MakePeace, Hide, 
	    BeQuiet, TalkAgain, DrinkAgility, DrinkPoison, DrinkRefresh,
	    DrinkStrength, DrinkCure, DrinkHealth, Steal, Lockpick,
	    CreateSetOne, EquipSetOne, Unarm, CreateSetTwo, EquipSetTwo,
	    CreateSetThree, EquipSetThree, SpiritSpeak, ChangeTitle,
	    Quiver, Poison, Skills, Switches, Tithe, Meditate, Consecrate,
	    DivineFury, DispelEvil, EnemyOfOne, HolyLight, NobleSacrifice,
	    CleanseByFire, CloseWounds, RemoveCurse, ThrowExplosion,
	    WeaponAbility, WeaponAbilityOne, WeaponAbilityTwo,
	    CheckTithingPoints, SetTeam, Confidence, LightningStrike,
	    Evasion, CounterAttack, MomentumStrike, HonorableExecution,
	    PainSpike, PoisonStrike, WraithForm, CurseWeapon, Wither, LichForm, VampiricEmbrace,    // Added 1.9.7 + rafa Bushido + Necro
	}

	static Squire()
	{
	    string [] keyWords = 
	    { 
		" ", "restyle", "stop", "stay", "follow", "follow me", "come", 
		"throw", "change my nickname", "change your nickname", "heal", 
		"dress", "undress", "mount", "dismount", "stats", "unload", "list", 
		"arm", "grab", "grab all", "loot", "loot all", "attack", "kill", 
		"guard", "guard me", "rename yourself", "backpack", "play music", 
		"provoke", "discord", "make peace", "hide", "be quiet", "talk again",
		"drink agility", "drink poison", "drink refresh", "drink strength",
		"drink cure", "drink health", "steal", "lockpick", "create set one",
		"equip set one", "unarm", "create set two", "equip set two", 
		"create set three", "equip set three", "spirit speak", "change title",
		"quiver", "poison", "skills", "switches", "tithe", "meditate", "consecrate weapon",
		"divine fury", "dispel evil", "enemy of one", "holy light", "noble sacrifice",
		"cleanse by fire", "close wounds", "remove curse", "throw explosion",
		"weapon ability", "weapon ability one", "weapon ability two",
		"check tithing points", "set team", "Confidence", "LightningStrike",
		"Evasion", "CounterAttack", "MomentumStrike", "HonorableExecution", "PainSpike",
		"PoisonStrike", "WraithForm", "CurseWeapon", "Wither", "LichForm", "VampiricEmbrace" // Added 1.9.7 + Rafa Bush and Necro
	    }; 

	    s_Keywords = new Hashtable( keyWords.Length, StringComparer.OrdinalIgnoreCase );

	    for ( int i = 0; i < keyWords.Length; ++i )
		s_Keywords[keyWords[i]] = i;
	}

	[Constructable]
	public Squire() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
	{
	    SpeechHue = Utility.RandomDyedHue();
	    Nickname = "Master";
	    SNickname = "Squire";

	    IsBonded = true;
	    ControlSlots = 3;

	    m_AutoUseHealthPotion = true;
	    m_AutoUseCurePotion = true;
	    m_AutoHealSelf = true;
	    m_AutoHealMaster = true;
	    m_AutoHealOther = true;
	    m_AutoPickupAmmo = true;
	    m_DesperateMasterRun = true;
	    m_AutoLoot = false;
	    m_OnlyLootGoldAndMap = false;

	    /* 			SetStr( 700, 800 );
				SetDex( 200, 250 );
				SetInt( 300, 350 ); */
	    SetStr(45, 55);
	    SetDex(15, 25);
	    SetInt(10, 15);
	    SetSkill(SkillName.Wrestling, 20.0, 30.0);
	    SetSkill(SkillName.Anatomy, 20.0, 30.0);
	    SetSkill(SkillName.Tactics, 20.0, 30.0);
	    SetSkill(SkillName.Swords, 20.0, 30.0);
	    SetSkill(SkillName.Macing, 20.0, 30.0);
	    SetSkill(SkillName.Fencing, 20.0, 30.0);
	    SetSkill(SkillName.Archery, 20.0, 30.0);
	    SetSkill(SkillName.MagicResist, 20.0, 30.0);
	    SetSkill(SkillName.Chivalry, 20.0, 30.0);
	    SetSkill(SkillName.Bushido, 20.0, 30.0); //Rafa Added
	    SetSkill(SkillName.Necromancy, 20.0, 30.0); //Rafa Added

	    /* SetResistance( ResistanceType.Physical,50, 60 );
	       SetResistance( ResistanceType.Fire, 50, 60 );
	       SetResistance( ResistanceType.Cold, 50, 60 );
	       SetResistance( ResistanceType.Poison, 50, 60 );
	       SetResistance( ResistanceType.Energy, 50, 60 );

	       Skills.Cap = 50000;
	       StatCap = 2000; */
	    Skills.Cap = 50000;
	    StatCap = 450;

	    AIFullSpeedActive = AIFullSpeedPassive = true; // force full speed

	    if (Female = Utility.RandomBool())
	    {
		switch (Utility.Random(5))
		{
		    case 4: Body = 0x191; Hue = Utility.RandomSkinHue(); Utility.AssignRandomHair(this); break;
		    case 3: Body = 0x191; Hue = Utility.RandomSkinHue(); Utility.AssignRandomHair(this); break;
		    case 2: Body = 0x191; Hue = Utility.RandomSkinHue(); Utility.AssignRandomHair(this); break;
		    case 1: Body = 0x25E; Race = Race.Elf; Hue = Race.RandomSkinHue(); Utility.AssignRandomHair(this, true); break;
		    case 0: Body = 0x191; Hue = Utility.RandomSkinHue(); Utility.AssignRandomHair(this); break;
		}
	    }
	    else
	    {
		switch ( Utility.Random( 5 ) )
		{
		    case 4 : Body = 0x190; Hue = Utility.RandomSkinHue(); Utility.AssignRandomHair( this ); break;
		    case 3 : Body = 0x190; Hue = Utility.RandomSkinHue(); Utility.AssignRandomHair( this ); break;
		    case 2 : Body = 0x190; Hue = Utility.RandomSkinHue(); Utility.AssignRandomHair( this ); break;
		    case 1 : Body = 0x25D; Race = Race.Elf; Hue = Race.RandomSkinHue(); Utility.AssignRandomHair( this, true ); break;
		    case 0 : Body = 0x190; Hue = Utility.RandomSkinHue(); Utility.AssignRandomHair( this ); break;
		}
		if ( Body == 0x190 )
		{
		    Utility.AssignRandomFacialHair( this, HairHue );
		}
	    }

	    if ( Body == 0x191 )
	    {
		Name = NameList.RandomName( "female" );
	    }
	    else if ( Body == 0x25E )
	    {
		Name = NameList.RandomName( "female elf brigand" );
	    }
	    else if ( Body == 0x25D )
	    {
		Name = NameList.RandomName( "male elf brigand" );
	    }
	    else
	    {
		Name = NameList.RandomName( "male" );
	    }

	    Item weapon;
	    switch ( Utility.Random( 6 ) )
	    {
		case 0: weapon = new Kryss(); break;
		case 1: weapon = new Scimitar(); break;
		case 2: weapon = new WarAxe(); break;
		case 3: weapon = new Cutlass(); break;
		case 4: weapon = new HammerPick(); break;
		default: weapon = new WarFork(); break;
	    }
	    AddItem( weapon );
	    AddItem( new RingmailChest() );
	    AddItem( new RingmailLegs() );
	    AddItem( new RingmailArms() );
	    AddItem( new RingmailGloves() );
	    AddItem( new Boots() );

	    if ( null == Backpack )
	    {
		Container pack = new Backpack();
		pack.Movable = false;
		AddItem( pack );
	    }
	}

#region [Stats]Max
	[CommandProperty(AccessLevel.GameMaster)]
	public override int HitsMax
	{
	    get
	    {
		int strBase;
		int strOffs = GetStatOffset(StatType.Str);

		strBase = Str; //Str already includes GetStatOffset/str
		strOffs = AosAttributes.GetValue(this, AosAttribute.BonusHits);

		if (strOffs > 25)
		{
		    strOffs = 25;
		}

		return (strBase / 2) + 50 + strOffs;
	    }
	}//{ get { return false; } }

    [CommandProperty(AccessLevel.GameMaster)]
    public override int StamMax { get { return (base.StamMax + AosAttributes.GetValue(this, AosAttribute.BonusStam)); } }

    [CommandProperty(AccessLevel.GameMaster)]
    public override int ManaMax { get { return (base.ManaMax + AosAttributes.GetValue(this, AosAttribute.BonusMana) +
	    ((Race == Race.Elf) ? 20 : 0) ); } }
#endregion

    public Squire(Serial serial) : base(serial) { }

    public override FoodType FavoriteFood { get { return (FoodType.Eggs | FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies); } }

    public override bool HandlesOnSpeech( Mobile from ) { return true; }

    public override void OnSpeech( SpeechEventArgs e )
    {
	Mobile from = e.Mobile;
	bool isSNickname = false;
	bool isSquireTeam = false; // Added 1.9.7

	if ( ControlMaster != from || IsDeadPet )
	{
	    base.OnSpeech( e );
	    return;
	}

	object command = (int)SquireCommands.None;

	if ( e.Speech.Length > "all ".Length && e.Speech.Substring( 0, "all ".Length ).ToLower() == "all " )
	{
	    command = s_Keywords[ e.Speech.Substring( "all ".Length ) ];
	    isSNickname = false;
	}
	else if ( e.Speech.Length > Name.Length + 1 && e.Speech.Substring( 0, Name.Length + 1 ).ToLower() == Name.ToLower() + ' ' )
	{
	    command = s_Keywords[ e.Speech.Substring( Name.Length + 1 ) ];
	    isSNickname = false;
	}
	else if ( m_SquireNickname != null && e.Speech.Length > m_SquireNickname.Length + 1 && e.Speech.Substring( 0, m_SquireNickname.Length + 1 ).ToLower() == m_SquireNickname.ToLower() + ' ' )
	{
	    command = s_Keywords[ e.Speech.Substring( m_SquireNickname.Length + 1 ) ];
	    isSNickname = true;
	}
	else if ( m_SquireTeam != null && e.Speech.Length > m_SquireTeam.Length + 1 && e.Speech.Substring( 0, m_SquireTeam.Length + 1 ).ToLower() == m_SquireTeam.ToLower() + ' ' ) // Added 1.9.7
	{
	    command = s_Keywords[ e.Speech.Substring( m_SquireTeam.Length + 1 ) ];
	    isSquireTeam = true;
	}

	switch ( (command == null ? (int)SquireCommands.None : (int)command) )
	{
	    case (int)SquireCommands.Restyle: 
		{
		    from.SendGump( new SquireCustomizeGump( this ) );
		    break;
		}
	    case (int)SquireCommands.Dress: 
		{ 
		    if( m_SquireBeQuiet == false ) 
		    {
			Say("I shall attempt to equip all the items in my pack.");
		    }

		    List<Server.Item> items = Backpack.Items;

		    for ( int i = items.Count - 1; i >= 0; --i )
		    {
			Item item = (Item)items[i];

			if (item is BaseWeapon || item is BaseClothing || item is BaseArmor || item is BaseJewel || item is BaseShield || item is BaseQuiver) // Updated 1.9.4
			{
			    Backpack.DropItem( item );
			    OnDragDrop( from, item );
			}
		    }
		    break;
		} 
	    case (int)SquireCommands.Undress: 
		{
		    if( m_SquireBeQuiet == false )
		    {
			Say("I shall give you everything I am wearing.");
		    }

		    List<Server.Item> items = Items;

		    for ( int i = items.Count - 1; i >= 0; --i )
		    {
			Item item = (Item)items[i];
			if ( (item is BaseQuiver || !(item is Container || item is IMountItem)) && item.Layer != Layer.FacialHair && item.Layer != Layer.Hair ) // Updated 1.9.4
			    from.AddToBackpack( item );
		    }

		    break;
		} 
	    case (int)SquireCommands.Mount: 
		if ( null == Mount )
		{
		    IMount mount = FindMyMount( Backpack );

		    if ( null == mount )
			from.Target = new MountTarget( from, this );
		    else
			mount.Rider = this;
		}
		break;
	    case (int)SquireCommands.Dismount:
		if ( null != Mount )
		{
		    IMount mount = FindMyMount( null );

		    if ( null != mount )
		    {
			mount.Rider = null;
			if ( mount is EtherealMount )
			    Backpack.DropItem( mount as EtherealMount );
			else
			    ((BaseMount)mount).ControlOrder = OrderType.Follow;
		    }
		}
		break;
	    case (int)SquireCommands.Stats:
		from.SendGump( new SquireLoreGump( this, from, SquireLorePage.Stats ) );
		break;
	    case (int)SquireCommands.ChangeNickname:
		{
		    if( m_SquireBeQuiet == false )
		    {
			Say( "What would you like me to call you?" );
		    }
		    from.Prompt = new SquireNewNickname( this );
		    break;
		}
	    case (int)SquireCommands.ChangeSNickname:
		{
		    if( m_SquireBeQuiet == false )
		    {
			Say( "What would you like to call me?" );
		    }
		    from.Prompt = new NewSquireNickname( this );
		    break;
		}
	    case (int)SquireCommands.Unload:
		{ 
		    if( m_SquireBeQuiet == false )
		    {
			Say("I shall give you everything in my pack.");
		    }

		    List<Server.Item> items = Backpack.Items;

		    for ( int i = items.Count - 1; i >= 0; --i )
		    {
			from.AddToBackpack( (Item)items[i] );
		    }

		    break;
		}
	    case (int)SquireCommands.List:
		{
		    Say("I am carrying:");

		    foreach( Item item in Backpack.Items )
		    {
			if ( null != item )
			    Say( "{0} {1}", item.Amount, item.GetType().Name );
		    }
		    break;
		}
	    case (int)SquireCommands.Arm:
		{
		    Item weapon = Backpack.FindItemByType( typeof( BaseWeapon ) ); //Renamed from item to weapon 1.9
		    Item shield = Backpack.FindItemByType( typeof( BaseShield ) ); //Added 1.9

		    if ( null == weapon && null == shield ) //Changed 1.9
		    {
			if( m_SquireBeQuiet == false )
			{
			    Say( "I have no weapons to arm myself with." );
			}
		    }
		    else
		    {
			if( shield != null )
			{
			    Backpack.DropItem( shield ); //Added 1.9
			    OnDragDrop( from, shield ); //Added 1.9
			}

			if( weapon != null )
			{
			    Backpack.DropItem( weapon ); //Changed to look for weapon instead of item, 1.9
			    OnDragDrop( from, weapon ); //Changed to look for weapon instead of item, 1.9
			}
		    }
		}
		break;
	    case (int)SquireCommands.Grab:
		from.Target = new GrabItemTarget( from, this );
		break;
	    case (int)SquireCommands.GrabAll:
		GrabItems(false, m_OnlyLootGoldAndMap);
		break;
	    case (int)SquireCommands.Loot:
		from.Target = new LootCorpseTarget( from, this );
		break;
	    case (int)SquireCommands.LootAll:
		GrabItems(true, m_OnlyLootGoldAndMap);
		break;
	    case (int)SquireCommands.Heal:
		from.Target = new HealTarget( from, this );
		break;
	    case (int)SquireCommands.RenameSelf:
		{
		    if ( Body == 0x191 )
		    {
			Name = NameList.RandomName( "female" );
		    }
		    else if ( Body == 0x25E )
		    {
			Name = NameList.RandomName( "female elf brigand" );
		    }
		    else if ( Body == 0x25D )
		    {
			Name = NameList.RandomName( "male elf brigand" );
		    }
		    else
		    {
			Name = NameList.RandomName( "male" );
		    }

		    if( m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SquiresNewName, null, null );
		    }
		    break;
		}
	    case (int)SquireCommands.Backpack:
		{
		    SquirePack.TryPackOpen( this, from );

		    if( m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ShowingOffASquiresBackpack, null, null );
		    }
		    break;
		}
	    case (int)SquireCommands.Hide:
		{
		    if ( DateTime.UtcNow > m_HideDelay )
		    {
			UseSkill(SkillName.Hiding);
			if( Hidden == false )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.HideFailure, null, null );
			    }
			}
			m_HideDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );//10 seconds between each player's hiding attempt, right?
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToHide, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.PlayMusic:
		{
		    Item instrument = Backpack.FindItemByType( typeof( BaseInstrument ) );

		    if ( instrument == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.MissingInstrument, null, null );
			}
		    }
		    else
		    {
			if ( DateTime.UtcNow > m_MusicDelay )
			{
			    instrument.OnDoubleClick( this );
			    ((BaseInstrument)instrument).ConsumeUse( this );
			    m_MusicDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 6 );
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToPlayMusic, null, null );
			    }
			}
		    }

		    break;
		}
	    case (int)SquireCommands.Provoke: 
		{
		    Item instrument = Backpack.FindItemByType( typeof( BaseInstrument ) );

		    if ( instrument == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.MissingInstrument, null, null );
			}
		    }
		    else
		    {
			if ( DateTime.UtcNow > m_MusicDelay )
			{
			    from.Target = new ProvokeTargetOne( from, this, ((BaseInstrument)instrument) );
			    m_MusicDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToPlayMusic, null, null );
			    }
			}
		    }

		    break;
		}
	    case (int)SquireCommands.Discord: 
		{
		    Item instrument = Backpack.FindItemByType( typeof( BaseInstrument ) );

		    if ( instrument == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.MissingInstrument, null, null );
			}
		    }
		    else
		    {
			if ( DateTime.UtcNow > m_MusicDelay )
			{
			    from.Target = new SquireDiscordanceTarget( from, this, ((BaseInstrument)instrument) );
			    m_MusicDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToPlayMusic, null, null );
			    }
			}
		    }

		    break;
		}
	    case (int)SquireCommands.MakePeace: 
		{
		    Item instrument = Backpack.FindItemByType( typeof( BaseInstrument ) );

		    if ( instrument == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.MissingInstrument, null, null );
			}
		    }
		    else
		    {
			if ( DateTime.UtcNow > m_MusicDelay )
			{
			    from.Target = new SquirePeacemakingTarget( from, this, ((BaseInstrument)instrument) );
			    m_MusicDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 6 );
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToPlayMusic, null, null );
			    }
			}
		    }

		    break;
		}
	    case (int)SquireCommands.Throw:
		{
		    Item snowpile = Backpack.FindItemByType( typeof( SnowPile ) );

		    if ( snowpile == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.MissingSnow, null, null );
			}
		    }
		    else
		    {
			if ( DateTime.UtcNow > m_ThrowDelay )
			{
			    from.Target = new ThrowTarget( from, this );
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToPackSnow, null, null );
			    }
			}
		    }

		    break;
		}
	    case (int)SquireCommands.Attack:
		switch ( Utility.Random( 2 ) )
		{
		    case 0: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).PrimaryAbility ); break;
		    case 1: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).SecondaryAbility ); break;
		}
		if ( isSNickname == true || isSquireTeam == true ) // Added SquireTeam Check 1.9.7
		{
		    if (!this.IsDeadPet && this.CheckControlChance(from))
			this.AIObject.BeginPickTarget(from, OrderType.Attack);
		}
		goto default;
	    case (int)SquireCommands.Kill:
		switch ( Utility.Random( 2 ) )
		{
		    case 0: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).PrimaryAbility ); break;
		    case 1: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).SecondaryAbility ); break;
		}
		if ( isSNickname == true || isSquireTeam == true ) // Added SquireTeam Check 1.9.7
		{
		    if (!this.IsDeadPet && this.CheckControlChance(from))
			this.AIObject.BeginPickTarget(from, OrderType.Attack);
		}
		goto default;
	    case (int)SquireCommands.Guard:
		switch ( Utility.Random( 2 ) )
		{
		    case 0: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).PrimaryAbility ); break;
		    case 1: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).SecondaryAbility ); break;
		}
		if ( isSNickname == true || isSquireTeam == true ) // Added SquireTeam Check 1.9.7
		{
		    if (!this.IsDeadPet && this.CheckControlChance(from))
		    {
			this.ControlTarget = null;
			this.ControlOrder = OrderType.Guard;
		    }
		}
		goto default;
	    case (int)SquireCommands.GuardMe:
		switch ( Utility.Random( 2 ) )
		{
		    case 0: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).PrimaryAbility ); break;
		    case 1: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).SecondaryAbility ); break;
		}
		if ( isSNickname == true || isSquireTeam == true ) // Added SquireTeam Check 1.9.7
		{
		    if (!this.IsDeadPet && this.CheckControlChance(from))
		    {
			this.ControlTarget = null;
			this.ControlOrder = OrderType.Guard;
		    }
		}
		goto default;
	    case (int)SquireCommands.Come:
		if ( isSNickname == true || isSquireTeam == true ) // Added SquireTeam Check 1.9.7
		{
		    if (this.CheckControlChance(from))
		    {
			this.ControlTarget = null;
			this.ControlOrder = OrderType.Come;
		    }
		}
		goto default;
	    case (int)SquireCommands.Follow:
		if ( isSNickname == true || isSquireTeam == true ) // Added SquireTeam Check 1.9.7
		{
		    this.AIObject.BeginPickTarget(from, OrderType.Follow);
		}
		goto default;
	    case (int)SquireCommands.FollowMe:
		if ( isSNickname == true || isSquireTeam == true ) // Added SquireTeam Check 1.9.7
		{
		    if (this.CheckControlChance(from))
		    {
			this.ControlTarget = from;
			this.ControlOrder = OrderType.Follow;
		    }
		}
		goto default;
	    case (int)SquireCommands.Stop:
		if ( isSNickname == true || isSquireTeam == true ) // Added SquireTeam Check 1.9.7
		{
		    if (this.CheckControlChance(from))
		    {
			this.ControlTarget = null;
			this.ControlOrder = OrderType.Stop;
		    }
		}
		goto default;
	    case (int)SquireCommands.Stay:
		if ( isSNickname == true || isSquireTeam == true ) // Added SquireTeam Check 1.9.7
		{
		    if (this.CheckControlChance(from))
		    {
			this.ControlTarget = null;
			this.ControlOrder = OrderType.Stay;
		    }
		}
		goto default;
	    case (int)SquireCommands.BeQuiet:
		{
		    if( m_SquireBeQuiet == false )
		    {
			m_SquireBeQuiet = true;
			SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ToldToShutUp, null, null );
		    }
		    break;
		}
	    case (int)SquireCommands.TalkAgain:
		{
		    if( m_SquireBeQuiet == true )
		    {
			m_SquireBeQuiet = false;
			SquireDialog.DoSquireDialog( from, this, SquireDialogTree.CanTalkAgain, null, null );
		    }
		    break;
		}
	    case (int)SquireCommands.DrinkAgility:
		{
		    Item agility = Backpack.FindItemByType( typeof( BaseAgilityPotion ) );
		    if( agility != null )
		    {
			if( DateTime.UtcNow > m_AgilityDelay )
			{
			    ((BasePotion)agility).Drink( this );
			    m_AgilityDelay = DateTime.UtcNow + TimeSpan.FromMinutes( 2.0 );
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.AgilityPotion, null, null );
			    }
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToDrink, null, null );
			    }
			}
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoPotions, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.DrinkPoison:
		{
		    Item poison = Backpack.FindItemByType( typeof( BasePoisonPotion ) );
		    if( poison != null )
		    {
			((BasePotion)poison).Drink( this );
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.PoisonPotion, null, null );
			}
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoPotions, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.DrinkRefresh:
		{
		    Item refresh = Backpack.FindItemByType( typeof( BaseRefreshPotion ) );
		    if( refresh != null )
		    {
			if( this.Stam < this.StamMax )
			{
			    ((BasePotion)refresh).Drink( this );
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.RefreshPotion, null, null );
			    }
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToDrink, null, null );
			    }
			}
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoPotions, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.DrinkStrength:
		{
		    Item strength = Backpack.FindItemByType( typeof( BaseStrengthPotion ) );
		    if( strength != null )
		    {
			if( DateTime.UtcNow > m_StrengthDelay )
			{
			    ((BasePotion)strength).Drink( this );
			    m_StrengthDelay = DateTime.UtcNow + TimeSpan.FromMinutes( 2.0 );
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.StrengthPotion, null, null );
			    }
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToDrink, null, null );
			    }
			}
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoPotions, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.DrinkCure:
		{
		    Item cure = Backpack.FindItemByType( typeof( BaseCurePotion ) );
		    if( cure != null )
		    {
			if( this.Poisoned )
			{
			    ((BasePotion)cure).Drink( this );
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.CurePotion, null, null );
			    }
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.CantCurePotion, null, null );
			    }
			}
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoPotions, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.DrinkHealth:
		{
		    Item heal = Backpack.FindItemByType( typeof( BaseHealPotion ) );
		    if( heal != null )
		    {
			if( this.Hits < this.HitsMax )
			{
			    if( this.Poisoned )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.StillPoisoned, null, null );
				}
			    }
			    else if( MortalStrike.IsWounded( this ) )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.MortallyWoundedHP, null, null );
				}
			    }
			    else if( DateTime.UtcNow > m_HealthDelay )
			    {
				((BasePotion)heal).Drink( this );
				m_HealthDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.HealthPotion, null, null );
				}
			    }
			    else
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToDrink, null, null );
				}
			    }
			}
			else
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.CantHealthPotion, null, null );
			    }
			}
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoPotions, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.Steal: //New 1.8.5
		{
		    if( DateTime.UtcNow > m_StealingDelay )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.WhatShouldISteal, null, null );
			}
			from.Target = new SquireStealingTarget( from, this );
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToSteal, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.Lockpick: //New 1.8.5
		{
		    Item lockpick = Backpack.FindItemByType( typeof( Lockpick ) );

		    if( DateTime.UtcNow > m_LockpickDelay )
		    {
			if( lockpick == null )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.MissingLockpicks, null, null );
			    }
			}
			else
			{
			    from.Target = new SquireLockpickTarget( this, ((Lockpick)lockpick) );
			    m_LockpickDelay = DateTime.UtcNow + TimeSpan.FromSeconds(0.5);
			}
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToLockpick, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.Unarm: //Begin 1.9
		{
		    Item SecondHand = FindItemOnLayer( Layer.TwoHanded );
		    Item FirstHand = FindItemOnLayer( Layer.OneHanded );

		    if( FirstHand != null )
		    {
			Backpack.DropItem( FirstHand );
			if( DateTime.UtcNow > m_AnnoyanceDelay )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.Unarmed, null, null );
			    }
			    m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
			}
		    }

		    if( SecondHand != null )
		    {
			Backpack.DropItem( SecondHand );
			if( DateTime.UtcNow > m_AnnoyanceDelay )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.Unarmed, null, null );
			    }
			    m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.CreateSetOne:
		{
		    Item SecondHand = FindItemOnLayer( Layer.TwoHanded );
		    Item FirstHand = FindItemOnLayer( Layer.OneHanded );

		    if( SecondHand != null && FirstHand != null )
		    {
			m_WeaponSetOneSlotOne = FirstHand;
			m_WeaponSetOneSlotTwo = SecondHand;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SuccessfulSetCreation, null, null );
			}
		    }
		    else if( SecondHand == null && FirstHand != null )
		    {
			m_WeaponSetOneSlotOne = FirstHand;
			m_WeaponSetOneSlotTwo = null;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SuccessfulSetCreation, null, null );
			}
		    }
		    else if( SecondHand != null && FirstHand == null )
		    {
			m_WeaponSetOneSlotOne = null;
			m_WeaponSetOneSlotTwo = SecondHand;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SuccessfulSetCreation, null, null );
			}
		    }
		    else
		    {
			m_WeaponSetOneSlotOne = null;
			m_WeaponSetOneSlotTwo = null;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.EmptyHands, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.EquipSetOne:
		{
		    Item SecondHand = FindItemOnLayer( Layer.TwoHanded );
		    Item FirstHand = FindItemOnLayer( Layer.OneHanded );

		    if( m_WeaponSetOneSlotOne != null )
		    {
			if( m_WeaponSetOneSlotOne.IsChildOf( this.Backpack ) )
			{
			    Backpack.DropItem( m_WeaponSetOneSlotOne );
			    OnDragDrop( this, m_WeaponSetOneSlotOne );
			}
			else
			{
			    if( m_WeaponSetOneSlotOne != FirstHand )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.FirstHandMissing, null, null );
				}
			    }
			}
		    }
		    else
		    {
			Backpack.DropItem( FirstHand );
		    }

		    if( m_WeaponSetOneSlotTwo != null )
		    {
			if( m_WeaponSetOneSlotTwo.IsChildOf( this.Backpack ) )
			{
			    Backpack.DropItem( m_WeaponSetOneSlotTwo );
			    OnDragDrop( this, m_WeaponSetOneSlotTwo );
			}
			else
			{
			    if( m_WeaponSetOneSlotTwo != SecondHand )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SecondHandMissing, null, null );
				}
			    }
			}
		    }
		    else
		    {
			Backpack.DropItem( SecondHand );
		    }
		    break;
		}
	    case (int)SquireCommands.CreateSetTwo:
		{
		    Item SecondHand = FindItemOnLayer( Layer.TwoHanded );
		    Item FirstHand = FindItemOnLayer( Layer.OneHanded );

		    if( SecondHand != null && FirstHand != null )
		    {
			m_WeaponSetTwoSlotOne = FirstHand;
			m_WeaponSetTwoSlotTwo = SecondHand;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SuccessfulSetCreation, null, null );
			}
		    }
		    else if( SecondHand == null && FirstHand != null )
		    {
			m_WeaponSetTwoSlotOne = FirstHand;
			m_WeaponSetTwoSlotTwo = null;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SuccessfulSetCreation, null, null );
			}
		    }
		    else if( SecondHand != null && FirstHand == null )
		    {
			m_WeaponSetTwoSlotOne = null;
			m_WeaponSetTwoSlotTwo = SecondHand;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SuccessfulSetCreation, null, null );
			}
		    }
		    else
		    {
			m_WeaponSetTwoSlotOne = null;
			m_WeaponSetTwoSlotTwo = null;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.EmptyHands, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.EquipSetTwo:
		{
		    Item SecondHand = FindItemOnLayer( Layer.TwoHanded );
		    Item FirstHand = FindItemOnLayer( Layer.OneHanded );

		    if( m_WeaponSetTwoSlotOne != null )
		    {
			if( m_WeaponSetTwoSlotOne.IsChildOf( this.Backpack ) )
			{
			    Backpack.DropItem( m_WeaponSetTwoSlotOne );
			    OnDragDrop( this, m_WeaponSetTwoSlotOne );
			}
			else
			{
			    if( m_WeaponSetTwoSlotOne != FirstHand )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.FirstHandMissing, null, null );
				}
			    }
			}
		    }
		    else
		    {
			Backpack.DropItem( FirstHand );
		    }

		    if( m_WeaponSetTwoSlotTwo != null )
		    {
			if( m_WeaponSetTwoSlotTwo.IsChildOf( this.Backpack ) )
			{
			    Backpack.DropItem( m_WeaponSetTwoSlotTwo );
			    OnDragDrop( this, m_WeaponSetTwoSlotTwo );
			}
			else
			{
			    if( m_WeaponSetTwoSlotTwo != SecondHand )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SecondHandMissing, null, null );
				}
			    }
			}
		    }
		    else
		    {
			Backpack.DropItem( SecondHand );
		    }
		    break;
		}
	    case (int)SquireCommands.CreateSetThree:
		{
		    Item SecondHand = FindItemOnLayer( Layer.TwoHanded );
		    Item FirstHand = FindItemOnLayer( Layer.OneHanded );

		    if( SecondHand != null && FirstHand != null )
		    {
			m_WeaponSetThreeSlotOne = FirstHand;
			m_WeaponSetThreeSlotTwo = SecondHand;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SuccessfulSetCreation, null, null );
			}
		    }
		    else if( SecondHand == null && FirstHand != null )
		    {
			m_WeaponSetThreeSlotOne = FirstHand;
			m_WeaponSetThreeSlotTwo = null;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SuccessfulSetCreation, null, null );
			}
		    }
		    else if( SecondHand != null && FirstHand == null )
		    {
			m_WeaponSetThreeSlotOne = null;
			m_WeaponSetThreeSlotTwo = SecondHand;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SuccessfulSetCreation, null, null );
			}
		    }
		    else
		    {
			m_WeaponSetThreeSlotOne = null;
			m_WeaponSetThreeSlotTwo = null;
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.EmptyHands, null, null );
			}
		    }
		    break;
		}
	    case (int)SquireCommands.EquipSetThree:
		{
		    Item SecondHand = FindItemOnLayer( Layer.TwoHanded );
		    Item FirstHand = FindItemOnLayer( Layer.OneHanded );

		    if( m_WeaponSetThreeSlotOne != null )
		    {
			if( m_WeaponSetThreeSlotOne.IsChildOf( this.Backpack ) )
			{
			    Backpack.DropItem( m_WeaponSetThreeSlotOne );
			    OnDragDrop( this, m_WeaponSetThreeSlotOne );
			}
			else
			{
			    if( m_WeaponSetThreeSlotOne != FirstHand )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.FirstHandMissing, null, null );
				}
			    }
			}
		    }
		    else
		    {
			Backpack.DropItem( FirstHand );
		    }

		    if( m_WeaponSetThreeSlotTwo != null )
		    {
			if( m_WeaponSetThreeSlotTwo.IsChildOf( this.Backpack ) )
			{
			    Backpack.DropItem( m_WeaponSetThreeSlotTwo );
			    OnDragDrop( this, m_WeaponSetThreeSlotTwo );
			}
			else
			{
			    if( m_WeaponSetThreeSlotTwo != SecondHand )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SecondHandMissing, null, null );
				}
			    }
			}
		    }
		    else
		    {
			Backpack.DropItem( SecondHand );
		    }
		    break;
		} //End 1.9
	    case (int)SquireCommands.SpiritSpeak: //Added 1.9.2
		{
		    SquireSpeaksWithSpirits();
		    break;
		}
	    case (int)SquireCommands.ChangeTitle: // Added 1.9.3
		{
		    from.SendGump( new SquireTitleGump( this, from, SquireTitlePage.PageOne ) );
		    break;
		}
	    case (int)SquireCommands.Quiver: // Added 1.9.4
		{
		    SquireQuiver.TryPackOpen( this, from );

		    break;
		}
	    case (int)SquireCommands.Poison: // Added 1.9.5
		{
		    if( m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( from, this, SquireDialogTree.PoisonToApply, null, null );
		    }
		    from.Target = new SquirePoisonTarget( from, this );
		    break;
		}
	    case (int)SquireCommands.Skills: // Added 1.9.5
		from.SendGump( new SquireLoreGump( this, from, SquireLorePage.Skills ) );
		break;
	    case (int)SquireCommands.Switches: // Added 1.9.5
		from.SendGump( new SquireLoreGump( this, from, SquireLorePage.Switches ) );
		break;
	    case (int)SquireCommands.Tithe: // Added 1.9.6
		{
		    SquireTithing();
		    break;
		}
	    case (int)SquireCommands.Meditate: // Added 1.9.6
		{
		    if( DateTime.UtcNow > m_MeditateDelay )
		    {
			UseSkill(SkillName.Meditation);
			m_MeditateDelay = DateTime.Now + TimeSpan.FromSeconds( 10.0 );
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToMeditate, null, null );
			}
		    }
		    break;
		}

	    case (int)SquireCommands.Confidence: // Rafa Bushido
		{
		    Item BushidoBook = Backpack.FindItemByType(typeof(BookOfBushido));

		    if (BushidoBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Bushido.Value > 15.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    new Server.Spells.Bushido.Confidence(this, null).Cast();
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.LightningStrike: // Rafa Bushido
		{
		    Item BushidoBook = Backpack.FindItemByType(typeof(BookOfBushido));

		    if (BushidoBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Bushido.Value > 50.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    SamuraiMove.SetCurrentMove(this, new LightningStrike());
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(4.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.Evasion: // Rafa Bushido
		{
		    Item BushidoBook = Backpack.FindItemByType(typeof(BookOfBushido));

		    if (BushidoBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Bushido.Value > 60.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    new Server.Spells.Bushido.Evasion(this, null).Cast();
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.CounterAttack: // Rafa Bushido 
		{
		    Item BushidoBook = Backpack.FindItemByType(typeof(BookOfBushido));

		    if (BushidoBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Bushido.Value > 40.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    new Server.Spells.Bushido.CounterAttack(this, null).Cast();
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.MomentumStrike: // Rafa Bushido
		{
		    Item BushidoBook = Backpack.FindItemByType(typeof(BookOfBushido));

		    if (BushidoBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Bushido.Value > 70.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    SamuraiMove.SetCurrentMove(this, new MomentumStrike());
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.HonorableExecution: // Rafa Bushido
		{
		    Item BushidoBook = Backpack.FindItemByType(typeof(BookOfBushido));

		    if (BushidoBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Bushido.Value > 25.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    SamuraiMove.SetCurrentMove(this, new HonorableExecution());
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.CurseWeapon: // Rafa Necro
		{
		    Item NecroBook = Backpack.FindItemByType(typeof(NecromancerSpellbook));

		    if (NecroBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Necromancy.Value > 10.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    new Server.Spells.Necromancy.CurseWeaponSpell(this, null).Cast();
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.PainSpike: // Rafa Necro
		{
		    Item NecroBook = Backpack.FindItemByType(typeof(NecromancerSpellbook));

		    if (NecroBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Necromancy.Value > 20.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    //										new PainSpikeSpell( this, null ).OnCast();
				    new Server.Spells.Necromancy.PainSpikeSpell(this, null).Cast();
				    //										m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 6.0 );
				    SquirePainSpikeDelayTimer PainSpikeTimer;
				    PainSpikeTimer = new SquirePainSpikeDelayTimer(this, from, TimeSpan.FromSeconds(1.5));
				    PainSpikeTimer.Start();

				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.WraithForm: // Rafa Necro
		{
		    Item NecroBook = Backpack.FindItemByType(typeof(NecromancerSpellbook));

		    if (NecroBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Necromancy.Value > 20.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    new Server.Spells.Necromancy.WraithFormSpell(this, null).Cast();
				    //										new WraithFormSpell( this, null ).OnCast();
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.PoisonStrike: // Rafa Necro
		{
		    Item NecroBook = Backpack.FindItemByType(typeof(NecromancerSpellbook));

		    if (NecroBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Necromancy.Value > 50.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    //										new PoisonStrikeSpell( this, null ).OnCast();
				    //										m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 6.0 );

				    new Server.Spells.Necromancy.PoisonStrikeSpell(this, null).Cast();
				    SquirePoisonStrikeDelayTimer PoisonStrikeTimer;
				    PoisonStrikeTimer = new SquirePoisonStrikeDelayTimer(this, from, TimeSpan.FromSeconds(1.5));
				    PoisonStrikeTimer.Start();

				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.Wither: // Rafa Necro
		{
		    Item NecroBook = Backpack.FindItemByType(typeof(NecromancerSpellbook));

		    if (NecroBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Necromancy.Value > 60.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    //										new WitherSpell( this, null ).OnCast();
				    new Server.Spells.Necromancy.WitherSpell(this, null).Cast();
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.LichForm: // Rafa Necro
		{
		    Item NecroBook = Backpack.FindItemByType(typeof(NecromancerSpellbook));

		    if (NecroBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Necromancy.Value > 70.0)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    //										new LichFormSpell( this, null ).OnCast();
				    new Server.Spells.Necromancy.LichFormSpell(this, null).Cast();
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.VampiricEmbrace: // Rafa Necro
		{
		    Item NecroBook = Backpack.FindItemByType(typeof(NecromancerSpellbook));

		    if (NecroBook == null)
		    {
			if (m_SquireBeQuiet == false)
			{
			    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NoChivalryBook, null, null);
			}
		    }
		    else
		    {
			if (Mana < 10)
			{
			    if (m_SquireBeQuiet == false)
			    {
				SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughMana, null, null);
			    }
			}
			else
			{
			    if (Skills.Necromancy.Value > 98.9)
			    {
				if (DateTime.UtcNow > m_SpellCastDelay)
				{
				    //										new VampiricEmbraceSpell( this, null ).OnCast();
				    new Server.Spells.Necromancy.VampiricEmbraceSpell(this, null).Cast();
				    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
				}
				else
				{
				    if (m_SquireBeQuiet == false)
				    {
					SquireDialog.DoSquireDialog(from, this, SquireDialogTree.TooSoonToCastASpell, null, null);
				    }
				}
			    }
			    else
			    {
				if (m_SquireBeQuiet == false)
				{
				    SquireDialog.DoSquireDialog(from, this, SquireDialogTree.NotEnoughSpellSkill, null, null);
				}
			    }
			}

		    }
		    break;
		}

	    case (int)SquireCommands.Consecrate: // Added 1.9.6
		{
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
			}
		    }
		    else
		    {
			if ( TithingPoints < 10 )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
			    }
			}
			else
			{
			    if ( Mana < 10 )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
				}
			    }
			    else
			    {
				if ( Skills.Chivalry.Value > 15.0 )
				{
				    if( DateTime.UtcNow > m_SpellCastDelay )
				    {
					new Server.Spells.Chivalry.ConsecrateWeaponSpell( this, null ).Cast();
					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else
				    {
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
					}
				    }
				}
				else
				{
				    if( m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
				    }
				}
			    }
			}
		    }
		    break;
		}
	    case (int)SquireCommands.DivineFury: // Added 1.9.6
		{
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
			}
		    }
		    else
		    {
			if ( TithingPoints < 10 )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
			    }
			}
			else
			{
			    if ( Mana < 15 )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
				}
			    }
			    else
			    {
				if ( Skills.Chivalry.Value > 25.0 )
				{
				    if( DateTime.UtcNow > m_SpellCastDelay )
				    {
					new Server.Spells.Chivalry.DivineFurySpell( this, null ).Cast();
					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else
				    {
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
					}
				    }
				}
				else
				{
				    if( m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
				    }
				}
			    }
			}
		    }
		    break;
		}
	    case (int)SquireCommands.DispelEvil: // Added 1.9.6
		{
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
			}
		    }
		    else
		    {
			if ( TithingPoints < 10 )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
			    }
			}
			else
			{
			    if ( Mana < 10 )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
				}
			    }
			    else
			    {
				if ( Skills.Chivalry.Value > 35.0 )
				{
				    if( DateTime.UtcNow > m_SpellCastDelay )
				    {
					new Server.Spells.Chivalry.DispelEvilSpell( this, null ).Cast();
					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else
				    {
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
					}
				    }
				}
				else
				{
				    if( m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
				    }
				}
			    }
			}
		    }
		    break;
		}
	    case (int)SquireCommands.EnemyOfOne: // Added 1.9.6
		{
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
			}
		    }
		    else
		    {
			if ( TithingPoints < 10 )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
			    }
			}
			else
			{
			    if ( Mana < 20 )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
				}
			    }
			    else
			    {
				if ( Skills.Chivalry.Value > 45.0 )
				{
				    if( DateTime.UtcNow > m_SpellCastDelay )
				    {
					new Server.Spells.Chivalry.EnemyOfOneSpell( this, null ).Cast();
					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else
				    {
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
					}
				    }
				}
				else
				{
				    if( m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
				    }
				}
			    }
			}
		    }
		    break;
		}
	    case (int)SquireCommands.HolyLight: // Added 1.9.6
		{
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
			}
		    }
		    else
		    {
			if ( TithingPoints < 10 )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
			    }
			}
			else
			{
			    if ( Mana < 10 )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
				}
			    }
			    else
			    {
				if ( Skills.Chivalry.Value > 55.0 )
				{
				    if( DateTime.UtcNow > m_SpellCastDelay )
				    {
					new Server.Spells.Chivalry.HolyLightSpell( this, null ).Cast();
					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else
				    {
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
					}
				    }
				}
				else
				{
				    if( m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
				    }
				}
			    }
			}
		    }
		    break;
		}
	    case (int)SquireCommands.NobleSacrifice: // Added 1.9.6
		{
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
			}
		    }
		    else
		    {
			if ( TithingPoints < 30 )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
			    }
			}
			else
			{
			    if ( Mana < 20 )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
				}
			    }
			    else
			    {
				if ( Skills.Chivalry.Value > 65.0 )
				{
				    if( DateTime.UtcNow > m_SpellCastDelay )
				    {
					new Server.Spells.Chivalry.NobleSacrificeSpell( this, null ).Cast();
					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else
				    {
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
					}
				    }
				}
				else
				{
				    if( m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
				    }
				}
			    }
			}
		    }
		    break;
		}
	    case (int)SquireCommands.CleanseByFire: // Added 1.9.6
		{
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
			}
		    }
		    else
		    {
			if ( TithingPoints < 10 )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
			    }
			}
			else
			{
			    if ( Mana < 10 )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
				}
			    }
			    else
			    {
				if ( Skills.Chivalry.Value > 5.0 )
				{
				    if( DateTime.UtcNow > m_SpellCastDelay )
				    {
					new Server.Spells.Chivalry.CleanseByFireSpell( this, null ).Cast();

					SquireCleanseDelayTimer CleanseTimer;
					CleanseTimer = new SquireCleanseDelayTimer( this, from, TimeSpan.FromSeconds( 1.0 ) );
					CleanseTimer.Start();

					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else
				    {
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
					}
				    }
				}
				else
				{
				    if( m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
				    }
				}
			    }
			}
		    }
		    break;
		}
	    case (int)SquireCommands.CloseWounds: // Added 1.9.6
		{
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
			}
		    }
		    else
		    {
			if ( TithingPoints < 10 )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
			    }
			}
			else
			{
			    if ( Mana < 10 )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
				}
			    }
			    else
			    {
				if ( Skills.Chivalry.Value > 0.0 )
				{
				    if( DateTime.UtcNow > m_SpellCastDelay )
				    {
					new Server.Spells.Chivalry.CloseWoundsSpell( this, null ).Cast();

					SquireCloseWoundsDelayTimer CloseWoundsTimer;
					CloseWoundsTimer = new SquireCloseWoundsDelayTimer( this, from, TimeSpan.FromSeconds( 1.5 ) );
					CloseWoundsTimer.Start();

					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else
				    {
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
					}
				    }
				}
				else
				{
				    if( m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
				    }
				}
			    }
			}
		    }
		    break;
		}
	    case (int)SquireCommands.RemoveCurse: // Added 1.9.6
		{
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
			}
		    }
		    else
		    {
			if ( TithingPoints < 10 )
			{
			    if( m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
			    }
			}
			else
			{
			    if ( Mana < 20 )
			    {
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
				}
			    }
			    else
			    {
				if ( Skills.Chivalry.Value > 5.0 )
				{
				    if( DateTime.UtcNow > m_SpellCastDelay )
				    {
					new Server.Spells.Chivalry.RemoveCurseSpell( this, null ).Cast();

					SquireRemoveCurseDelayTimer RemoveCurseTimer;
					RemoveCurseTimer = new SquireRemoveCurseDelayTimer( this, from, TimeSpan.FromSeconds( 1.5 ) );
					RemoveCurseTimer.Start();

					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else
				    {
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
					}
				    }
				}
				else
				{
				    if( m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
				    }
				}
			    }
			}
		    }
		    break;
		}/*
		    case (int)SquireCommands.SacredJourney: // Added 1.9.6
		    {
		    Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) );

		    if ( ChivalryBook == null )
		    {
		    if( m_SquireBeQuiet == false )
		    {
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoChivalryBook, null, null );
		    }
		    }
		    else
		    {
		    if ( TithingPoints < 15 )
		    {
		    if( m_SquireBeQuiet == false )
		    {
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughTithe, null, null );
		    }
		    }
		    else
		    {
		    if ( Mana < 20 )
		    {
		    if( m_SquireBeQuiet == false )
		    {
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughMana, null, null );
		    }
		    }
		    else
		    {
		    if ( Skills.Chivalry.Value > 15.0 )
		    {
		    if( DateTime.UtcNow > m_SpellCastDelay )
		    {
		    new Server.Spells.Chivalry.SacredJourneySpell( this, null ).Cast();

		    SquireSacredJourneyDelayTimer SacredJourneyTimer;
		    SacredJourneyTimer = new SquireSacredJourneyDelayTimer( this, from, TimeSpan.FromSeconds( 1.5 ) );
		    SacredJourneyTimer.Start();

		    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
		    }
		    else
		    {
		    if( m_SquireBeQuiet == false )
		    {
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.TooSoonToCastASpell, null, null );
		    }
		    }
		    }
		    else
		    {
		    if( m_SquireBeQuiet == false )
		    {
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NotEnoughSpellSkill, null, null );
		    }
		    }
		    }
		    }
		    }
		    break;
		    }*/
	    case (int)SquireCommands.ThrowExplosion: // Added 1.9.6
		{
		    Item explosion = Backpack.FindItemByType( typeof( BaseExplosionPotion ) );
		    if( explosion != null )
		    {
			((BasePotion)explosion).Drink( this );
			from.Target = new SquirePassExplosionPotionTarget( from, this );
		    }
		    else
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.NoExplosionPotion, null, null );
			}
		    }
		}
		break;
	    case (int)SquireCommands.WeaponAbility: // Added 1.9.6.3
		{
		    switch( Utility.Random( 2 ) )
		    {
			case 0: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).PrimaryAbility ); break;
			case 1: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).SecondaryAbility ); break;
		    }
		}
		break;
	    case (int)SquireCommands.WeaponAbilityOne: // Added 1.9.6.3
		{
		    WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).PrimaryAbility );
		}
		break;
	    case (int)SquireCommands.WeaponAbilityTwo: // Added 1.9.6.3
		{
		    WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).SecondaryAbility );
		}
		break;
	    case (int)SquireCommands.CheckTithingPoints: // Added 1.9.7
		{
		    Say( "I now have {0} tithing points.",  TithingPoints );
		}
		break;
	    case (int)SquireCommands.SetTeam: // Added 1.9.7
		{
		    if( m_SquireBeQuiet == false )
		    {
			Say( "What team would you like to assign me to?" );
		    }
		    from.Prompt = new NewSquireTeam( this );
		    break;
		}

	    default:
		base.OnSpeech( e );
		return;
	}
	e.Handled = true;
    }

    public void SetNickname( Mobile from, string NewNickname )
    {
	if ( from.AccessLevel < AccessLevel.GameMaster && from != this.ControlMaster )
	{
	    if ( this != null )
		if( m_SquireBeQuiet == false )
		    this.Say( "Why would I call you anything other than " + from.Name + "?" );

	    return;
	}
	else if ( !from.Alive )
	{
	    if ( this != null )
		if( m_SquireBeQuiet == false )
		    this.Say( "What was that?" );

	    return;
	}

	if ( NewNickname.Length == 0 )
	    NewNickname = "Master";

	m_MasterNickname = NewNickname;

	if( m_SquireBeQuiet == false )
	{
	    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.MasterHasANewNickname, null, null );
	}
    }

    public void SetSNickname( Mobile from, string NewSNickname )
    {
	if ( from.AccessLevel < AccessLevel.GameMaster && from != this.ControlMaster )
	{
	    if ( this != null )
		if( m_SquireBeQuiet == false )
		    this.Say( "Who are you to rename me?" );

	    return;
	}
	else if ( !from.Alive )
	{
	    if ( this != null )
		if( m_SquireBeQuiet == false )
		    this.Say( "What was that?" );

	    return;
	}

	if ( NewSNickname.Length == 0 )
	    NewSNickname = "Squire";

	m_SquireNickname = NewSNickname;

	if( m_SquireBeQuiet == false )
	{
	    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SquireHasANewNickname, null, null );
	}
    }

    public override void OnThink()
    {
	base.OnThink();
	if (!this.Controlled)   // should fix null reference exception when released, but dunno why that happen
	    return;

	Mobile m = this.ControlMaster;
	BaseCreature bc = null;
	foreach ( Mobile search in this.GetMobilesInRange( 16 ) )
	{
	    if ( search is BaseCreature && search != this && this.CanSee( search ) )
	    {
		bc = ((BaseCreature)search);
		break;
	    }
	}


	if ( DateTime.UtcNow > m_Delay )
	{
	    Item healthPotion = Backpack.FindItemByType( typeof( BaseHealPotion ) );
	    Item curePotion = Backpack.FindItemByType( typeof( BaseCurePotion ) );
	    Item ChivalryBook = Backpack.FindItemByType(typeof(BookOfChivalry));
	    Item NecroBook = Backpack.FindItemByType(typeof(NecromancerSpellbook));


	    if (Hits > 0 && AutoVampiricEmbrace && !TransformationSpellHelper.UnderTransformation(this, typeof(VampiricEmbraceSpell))
		    && NecroBook != null && DateTime.UtcNow > m_SpellCastDelay && Skills.Necromancy.Value >= 99 && Mana >= 10)
	    {
		new Server.Spells.Necromancy.VampiricEmbraceSpell(this, null).Cast();
		m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds(6.0);
	    }

	    if (Hits > 0 && m_AutoLoot && DateTime.UtcNow > m_AutoLootDelay)
	    {
		m_AutoLootDelay = DateTime.UtcNow + TimeSpan.FromSeconds(0.5);
		GrabItems(true, true, true);
	    }

	    if (m_AutoUseHealthPotion == true && healthPotion != null && DateTime.UtcNow > m_HealthDelay && Hits <= (HitsMax / 2) && this.Poisoned == false
		    && !MortalStrike.IsWounded(this))
	    {
		((BasePotion)healthPotion).Drink( this );
		m_HealthDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m, this, SquireDialogTree.HealthPotion, null, null );
		}
	    }
	    else if (m_AutoCastCloseWounds == true && ChivalryBook != null && !IsDeadPet && Mana >= 10 && TithingPoints >= 10
		    && DateTime.UtcNow > m_SpellCastDelay && Hits <= ((HitsMax / 3) * 2) && this.Poisoned == false && !MortalStrike.IsWounded(this)) // Added 1.9.6
	    {
		new Server.Spells.Chivalry.CloseWoundsSpell( this, null ).Cast();

		SquireSelfCloseWoundsDelayTimer CloseWoundsTimer;
		CloseWoundsTimer = new SquireSelfCloseWoundsDelayTimer( this, TimeSpan.FromSeconds( 1.5 ) );
		CloseWoundsTimer.Start();

		m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
	    }
	    else if ( m_AutoUseCurePotion == true && curePotion != null && this.Poisoned && DateTime.UtcNow > m_CureDelay )
	    {
		((BasePotion)curePotion).Drink( this );
		m_CureDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 ); // So they don't spam when drinking.
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m, this, SquireDialogTree.CurePotion, null, null );
		}
	    }
	    else if (m_AutoCastCleanseByFire == true && ChivalryBook != null && !IsDeadPet && Mana >= 10 && TithingPoints >= 10
		    && DateTime.UtcNow > m_SpellCastDelay && Hits >= ((HitsMax / 3) * 2) && this.Poisoned == true) // Added 1.9.6
	    {
		new Server.Spells.Chivalry.CleanseByFireSpell( this, null ).Cast();

		SquireSelfCleanseByFireDelayTimer CleanseByFireTimer;
		CleanseByFireTimer = new SquireSelfCleanseByFireDelayTimer( this, TimeSpan.FromSeconds( 1.0 ) );
		CleanseByFireTimer.Start();

		m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
	    }
	    else if ( m_AutoHealSelf == true && Hits < HitsMax - 10 && null == BandageContext.GetContext( this ))
	    {
		Item item = Backpack.FindItemByType( typeof(Bandage) );

		if ( null != item && null != BandageContext.BeginHeal( this , this ))
		{
		    item.Consume( 1 );
		    RevealingAction();

		    m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 11 );
		}
	    }
	    else if ( Controlled == true && ControlMaster != null && Hits >= HitsMax - 10 )
	    {
		if ( InRange( ControlMaster, Bandage.Range ) )
		{
		    if ( m_AutoHealMaster == true && m.Hidden == false )
		    {
			if ( m.Alive == true && m.Poisoned )
			{
			    Item item = Backpack.FindItemByType( typeof( Bandage ) );

			    if (m_AutoCastCleanseByFireMaster == true && ChivalryBook != null && !IsDeadPet && Mana >= 10 && TithingPoints >= 10
				    && DateTime.UtcNow > m_SpellCastDelay && Hits >= ((HitsMax / 3) * 2) && m.Poisoned == true) // Added 1.9.7
			    {
				new Server.Spells.Chivalry.CleanseByFireSpell( this, null ).Cast();

				SquireTargetCleanseByFireDelayTimer CleanseByFireTimer;
				CleanseByFireTimer = new SquireTargetCleanseByFireDelayTimer( this, m, TimeSpan.FromSeconds( 1.0 ) );
				CleanseByFireTimer.Start();

				m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
			    }
			    else if (Skills.Healing.Value >= 60.0 && Skills.Anatomy.Value >= 60.0 && null != item && null != BandageContext.BeginHeal(this, m)) // Sent behind an Else 1.9.7
			    {
				item.Consume( 1 );
				RevealingAction();
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( m, this, SquireDialogTree.SquireCuresMaster, null, null );
				}

				m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
			    }
			}
			else if (m_AutoCastCloseWoundsMaster == true && ChivalryBook != null && !IsDeadPet && Mana >= 10
				&& TithingPoints >= 10 && DateTime.UtcNow > m_SpellCastDelay && m.Hits <= ((m.HitsMax / 3) * 2) && m.Poisoned == false
				&& !MortalStrike.IsWounded(m)) // Added 1.9.7
			{
			    new Server.Spells.Chivalry.CloseWoundsSpell( this, null ).Cast();

			    SquireTargetCloseWoundsDelayTimer CloseWoundsTimer;
			    CloseWoundsTimer = new SquireTargetCloseWoundsDelayTimer( this, m, TimeSpan.FromSeconds( 1.5 ) );
			    CloseWoundsTimer.Start();

			    m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
			}
			else if ( m.Alive == true && m.Hits < m.HitsMax - 10 )
			{
			    Item item = Backpack.FindItemByType( typeof( Bandage ) );

			    if ( null != item && null != BandageContext.BeginHeal( this, m ) )
			    {
				item.Consume( 1 );
				RevealingAction();
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( m, this, SquireDialogTree.SquireHealsMaster, null, null );
				}

				m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
			    }
			}
			else if ( m.Alive == false )
			{
			    Item item = Backpack.FindItemByType( typeof( Bandage ) );

			    if ( m_AutoUseSpiritSpeak == true && DateTime.UtcNow > m_SpiritDelay ) // Added 1.9.2
			    {
				SquireSpeaksWithSpirits();
			    }

			    if (m_AutoRezMaster == true && Skills.Healing.Value >= 80.0 && Skills.Anatomy.Value >= 80.0 && null != item
				    && null != BandageContext.BeginHeal(this, m))
			    {
				item.Consume( 1 );
				RevealingAction();
				if( m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( m, this, SquireDialogTree.SquireRezsMaster, null, null );
				}

				m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 9 );
			    }
			}
		    }
		}
		else if (m_AutoHealMaster == true && m.Hidden == true && Controlled == true && ControlMaster != null && Hits >= HitsMax - 10
			&& m.Alive == true && m.Hits < m.HitsMax - 10)
		{
		    if ( DateTime.UtcNow > m_AnnoyanceDelay )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( m, this, SquireDialogTree.ASquiresConcern, null, null );
			}
			m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 30 );
		    }
		}
		else if (m_DesperateMasterRun == true && !(this.InRange(ControlMaster, Bandage.Range)) && Controlled == true &&
			ControlMaster != null && m.Alive == true && m.Hits < m.HitsMax / 2)
		{
		    if( this.ControlOrder == OrderType.Stay && DateTime.UtcNow > m_AnnoyanceDelay )
		    {
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( m, this, SquireDialogTree.ASquiresStay, null, null );
			}
			m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 30 );
		    }
		    else if ( this.ControlOrder != OrderType.Stay && m.Hits < m.HitsMax / 2 )
		    {
			this.AIObject.MoveTo( m, true, 1 );
		    }
		}
		else if ( Controlled == false || ControlMaster == null )
		{
		    if( m_SquireBeQuiet == false )
		    {
			Say( "I lack a " + m_MasterNickname + ", admin?" );
		    }
		    m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 30 );
		}

		if ( m.Hits > m.HitsMax - 10 && m.Poisoned == false && m.Alive == true )
		{
		    foreach ( Mobile search in this.GetMobilesInRange( Bandage.Range ) ) 
		    {
			if (search is BaseCreature && m_AutoHealAnimals == false && (search.Body == 0x191 || search.Body == 0x190 || search.Body == 0x25D
				    || search.Body == 0x25E) && ((BaseCreature)search).ControlMaster == this.ControlMaster && ((BaseCreature)search).Controlled == true
				&& search != this && this.CanSee(search) && search.Hits < search.HitsMax - 10)
			{
			    AutoHealAlly( ( ( BaseCreature )search ) );
			    break;
			}
			else if (search is BaseCreature && m_AutoHealAnimals == true && ((BaseCreature)search).ControlMaster == this.ControlMaster
				&& ((BaseCreature)search).Controlled == true && search != this && this.CanSee(search) && search.Hits < search.HitsMax - 10)
			{
			    AutoHealAlly( ( ( BaseCreature )search ) );
			    break;
			}
		    }
		}
	    }
	}

	// EM 2018.12.18 if ( Hunger < 10 || Loyalty <= BaseCreature.MaxLoyalty / 10 ) 
	if (Loyalty < BaseCreature.MaxLoyalty)
	{
	    CheckFeedSelf();
	}

	Item RangedWeaponCheck = FindItemOnLayer( Layer.TwoHanded );
	Item RangedWeaponCheck2 = FindItemOnLayer( Layer.OneHanded );

	if ( m_AutoPickupAmmo == true && ( RangedWeaponCheck is BaseRanged || RangedWeaponCheck2 is BaseRanged ) )
	{
	    GrabAmmo( false );
	}

	// EM 2018.12.18 if( DateTime.UtcNow > m_HungerDecay )
	if (DateTime.UtcNow > m_HungerDecay && 1 != 1) // Block this routine
	{
	    if( this != null && Hunger >= 1 )
	    {
		Hunger -= 1;
		m_HungerDecay = DateTime.UtcNow + TimeSpan.FromMinutes(50);
	    }
	    else if( DateTime.UtcNow > m_AnnoyanceDelay )
	    {
		Emote( "Stomach Growls Audibly" );
		m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds(300);
		if( Female )
		{
		    PlaySound( 0x313 );
		}
		else
		{
		    PlaySound( 0x42B );
		}
	    }
	}

	if( DateTime.UtcNow > m_WeaponAbilityDelay && m_AutoUseWeaponAbility == true ) // Added 1.9.7
	{
	    switch( Utility.Random( 2 ) )
	    {
		case 0: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).PrimaryAbility ); break;
		case 1: WeaponAbility.SetCurrentAbility( this, ((BaseWeapon)Weapon).SecondaryAbility ); break;
	    }

	    m_WeaponAbilityDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 30 );
	}
    }

    private void CheckFeedSelf()
    {
	if ( !IsDeadPet && null != Backpack )
	{
	    //	Item item = Backpack.FindItemByType( typeof( Food ) );

	    //	if ( null == item )	return;
	    //	((Food)item).Eat( this ); Stam += 15;

	    if ( Loyalty < BaseCreature.MaxLoyalty && 0.5 >= Utility.RandomDouble() )
	    {
		++Loyalty;
		Stam += 15;     // EM 2018.12.18 Eating restors Stamina
		Hunger = 10;    // EM 2018.12.18 Hunger should not decay but just in case
		if (m_SquireBeQuiet == false) this.Emote("*Looks happier.*");
		if ( IsBondable && !IsBonded )
		{
		    Mobile master = ControlMaster;

		    if ( master != null && master.Skills[SkillName.AnimalTaming].Value >= MinTameSkill )
		    {
			if ( BondingBegin == DateTime.MinValue )
			{
			    BondingBegin = DateTime.UtcNow;
			}
			else if ( (BondingBegin + BondingDelay) <= DateTime.UtcNow )
			{
			    IsBonded = true;
			    BondingBegin = DateTime.MinValue;
			    master.SendLocalizedMessage( 1049666 ); 
			    InvalidateProperties();
			}
		    }
		}
	    }
	}
    }


    public void GrabItems(bool ignoreNoteriety, bool GoldAndMapOnly)
    {
	GrabItems(ignoreNoteriety, GoldAndMapOnly, false);
    }

    public void GrabItems(bool ignoreNoteriety, bool GoldAndMapOnly, bool AutoLootMode)
    {
	ArrayList items = new ArrayList();
	ArrayList myitems = new ArrayList();
	bool rejected = false;
	bool canthold = false;
	bool lootAdded = false;

	if (m_SquireBeQuiet == false && !AutoLootMode)
	    Emote( "*Rummages through items on the ground.*" );

	foreach (Item item in GetItemsInRange(3))
	{
	    if ( item.Movable )
	    {
		if (item.Movable
			&& ((m_LootGold && item is Gold)
			    || (m_LootMap && item is TreasureMap)
			    || (m_LootZoogiFungus && item is ZoogiFungus)
			    || (m_LootDaemonBone && item is DaemonBone)))
		{
		    items.Add(item);
		}
		else if (!GoldAndMapOnly)
		{
		    items.Add( item );
		}
	    }
	    else if ( item is Corpse )
	    {
		if ( ignoreNoteriety || NotorietyHandlers.CorpseNotoriety( this, (Corpse)item ) != Notoriety.Innocent )
		{
		    if (((Corpse)item).Owner != null && ((Corpse)item).Owner.Player)
		    {
			if (m_SquireBeQuiet == false && !AutoLootMode)
			{
			    SquireDialog.DoSquireDialog( null, this, SquireDialogTree.RefusesToLootPlayers, null, null );
			}
		    }
		    else if (((Corpse)item).Owner != null && ((Corpse)item).Owner == this)
		    {
			if (m_SquireBeQuiet == false && !AutoLootMode)
			{
			    Emote( "*Rummages through items in a corpse.*" );
			}
			foreach ( Item corpseItem in ((Corpse)item).Items )
			{
			    myitems.Add( corpseItem );
			}
		    }
		    else
		    {
			if (m_SquireBeQuiet == false && !AutoLootMode)
			{
			    Emote( "*Rummages through items in a corpse.*" );
			}
			foreach ( Item corpseItem in ((Corpse)item).Items )
			{
			    if ((m_LootGold && corpseItem is Gold)
				    || (m_LootMap && corpseItem is TreasureMap)
				    || (m_LootZoogiFungus && corpseItem is ZoogiFungus)
				    || (m_LootDaemonBone && corpseItem is DaemonBone))
			    {
				items.Add(corpseItem);
			    }
			    else if (!GoldAndMapOnly)
			    {
				items.Add( corpseItem );
			    }
			}
		    }
		}
	    }
	}
	foreach ( Item item in items )
	{
	    if ( !Backpack.CheckHold( this, item, false, true ) )
	    {
		canthold = true;
	    }
	    else
	    {
		if (!item.Stackable || !m_ConvenientStackable || !Backpack.TryDropItem(this, item, false))
		{
		    bool isRejected;
		    LRReason reason;

		    NextActionTime = Core.TickCount + 5000;
		    Lift( item, item.Amount, out isRejected, out reason );

		    if ( !isRejected )
		    {
			if (AutoEquipLoot == true)
			{
			    Drop(this, Point3D.Zero);
			}
			else
			{
			    Drop(Backpack, new Point3D(Utility.Random(29, 108), Utility.Random(34, 94), 0)); //Randomly set them in their pack.
			}
			lootAdded = true;
		    }
		    else
		    {
			rejected = true;
		    }
		}
	    }
	}
	foreach (Item item in myitems)
	{
	    if ( !Backpack.CheckHold( this, item, false, true ) )
	    {
		canthold = true;
	    }
	    else
	    {
		bool isRejected;
		LRReason reason;

		NextActionTime = Core.TickCount + 5000;;
		Lift( item, item.Amount, out isRejected, out reason );

		if ( !isRejected )
		{
		    Drop( this, Point3D.Zero );
		    lootAdded = true;
		}
		else
		{
		    rejected = true;
		}
	    }
	}

	if (!AutoLootMode)
	{
	    if ( lootAdded )
		PlaySound( 0x2E6 );
	    if (!m_SquireBeQuiet)
	    {
		if (canthold)
		    Say("I can't hold more items.");
		if ( rejected )
		    Say( "I could not pick up all of the items." );
	    }
	}
    }

    public void GrabAmmo( bool ignoreNoteriety )
    {
	ArrayList items = new ArrayList();
	bool rejected = false;
	bool lootAdded = false;

	foreach ( Item item in GetItemsInRange( 2 ) )
	{
	    if ( item.Movable )
	    {
		if ( item is Arrow || item is Bolt )
		{
		    items.Add( item );
		}
	    }
	}
	foreach ( Item item in items )
	{
	    if ( !Backpack.CheckHold( this, item, false, true ) )
		rejected = true;
	    else
	    {
		bool isRejected;
		LRReason reason;

		NextActionTime = Core.TickCount + 5000;
		Lift( item, item.Amount, out isRejected, out reason );

		if ( !isRejected )
		{
		    Drop( this, Point3D.Zero );
		    lootAdded = true;
		    if ( DateTime.UtcNow > m_PickUpEmoteDelay )
		    {
			if( m_SquireBeQuiet == false )
			{
			    Emote( "*Retrieves ammunition.*" );
			}
			m_PickUpEmoteDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
		    }
		}
		else
		{
		    rejected = true;
		}
	    }
	}
    }

    public override bool CheckGold( Mobile from, Item dropped )
    {
	if ( dropped is Gold )
	    return false;

	return base.CheckGold( from, dropped );
    }

    public IMount FindMyMount( Container pack )
    {
	List<Server.Item> items = ( null == pack ) ? Items : pack.Items;

	foreach ( Item item in items )
	{
	    if ( item is IMountItem )
		return ((IMountItem)item).Mount;

	    else if ( item.Layer == Layer.Mount )
		return (IMount)item;
	}
	return null;
    }

    public override bool IsSnoop( Mobile from )
    {
	if ( SquirePack.CheckAccess( this, from ) )
	    return false;

	return base.IsSnoop( from );
    }

    public override bool OnDragDrop( Mobile from, Item dropped )
    {
	Item itemEquipped;

	if (( ControlMaster != from && this != from ) || IsDeadPet )
	    return base.OnDragDrop( from, dropped );

	if ( dropped is Bandage || dropped is Food )
	{
	    Backpack.DropItem( dropped );
	    from.SendMessage( "You give " + Name + " supplies." );
	    return true;
	}
	else if ( dropped is Gold )
	{
	    Backpack.DropItem( dropped );
	    from.SendMessage( "You give " + Name + " gold." );
	    return true;
	}
	else if ( dropped is PowerScroll )
	{
	    Backpack.DropItem( dropped );

	    if( m_AutoUsePowerScroll == true )
	    {
		if( ((PowerScroll)dropped).CanUse( this ) )
		{
		    ((PowerScroll)dropped).Use( this );
		    if( m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( null, this, SquireDialogTree.UsePowerScroll, null, null );
		    }
		}
		else
		{
		    if( m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( null, this, SquireDialogTree.CantUsePowerScroll, null, null );
		    }
		}
	    }

	    return true;
	}
	else if ( dropped is Arrow || dropped is Bolt ) // Added 1.9.7
	{
	    Backpack.DropItem( dropped );
	    from.SendMessage( "You give " + Name + " ammunition." );
	    return true;
	}
	else if (dropped is STrainingContract)
	{
	    Backpack.DropItem( dropped );

	    if( m_AutoUseTScroll == true )
	    {
		((STrainingContract)dropped).OnDoubleClick( this );
	    }

	    return true;
	}
	else if ( dropped is BaseWeapon && ((BaseWeapon)dropped).AosStrengthReq > this.Str )
	{
	    if( DateTime.UtcNow > m_AnnoyanceDelay )
	    {
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ThisIsTooHeavy, null, null );
		}
		m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 20 );
	    }
	    from.AddToBackpack( dropped );
	}
	else if ( dropped is BaseWeapon && ((BaseWeapon)dropped).AosStrengthReq <= this.Str )
	{
	    itemEquipped = FindItemOnLayer( Layer.TwoHanded );

	    if ( null != itemEquipped && ((BaseWeapon)dropped).CheckConflictingLayer( this, itemEquipped, Layer.TwoHanded ))
		Backpack.DropItem(itemEquipped);
	    // from.AddToBackpack(itemEquipped);

	    itemEquipped = FindItemOnLayer( Layer.OneHanded );
	    if ( null != itemEquipped && ((BaseWeapon)dropped).CheckConflictingLayer( this, itemEquipped, Layer.OneHanded ))
		Backpack.DropItem(itemEquipped);
	    // from.AddToBackpack(itemEquipped);

	    itemEquipped = FindItemOnLayer( Layer.FirstValid );
	    if ( null != itemEquipped && ((BaseWeapon)dropped).CheckConflictingLayer( this, itemEquipped, Layer.FirstValid ))
		Backpack.DropItem(itemEquipped);
	    // from.AddToBackpack(itemEquipped);

	    Backpack.DropItem( dropped );

	    if( DateTime.UtcNow > m_AnnoyanceDelay )
	    {
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ThankYou, null, null );
		}
		m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 20 );
	    }
	    AddItem( dropped );
	    from.SendMessage( "You give " + Name + " a weapon." );
	    return true;
	}
	else if (dropped is BaseShield && ((BaseShield)dropped).AosStrReq > this.Str)
	{
	    if( DateTime.UtcNow > m_AnnoyanceDelay )
	    {
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ThisIsTooHeavy, null, null );
		}
		m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 20 );
	    }
	    from.AddToBackpack( dropped );
	}
	else if ( dropped is BaseShield && ((BaseShield)dropped).AosStrReq <= this.Str )
	{
	    itemEquipped = FindItemOnLayer( Layer.TwoHanded );

	    if ( null != itemEquipped && ((BaseShield)dropped).CheckConflictingLayer( this, itemEquipped, Layer.TwoHanded ))
	    {
		from.AddToBackpack( itemEquipped );
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.UnequipsTwoHandedForShield, null, null );
		}
	    }

	    itemEquipped = FindItemOnLayer( Layer.FirstValid );
	    if ( null != itemEquipped && ((BaseShield)dropped).CheckConflictingLayer( this, itemEquipped, Layer.FirstValid ))
		from.AddToBackpack( itemEquipped );

	    Backpack.DropItem( dropped );

	    if( DateTime.UtcNow > m_AnnoyanceDelay )
	    {
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ThankYou, null, null );
		}
		m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 20 );
	    }
	    AddItem( dropped );
	    from.SendMessage( "You give " + Name + " a shield." );
	    return true;
	}
	else if ( dropped is BaseQuiver )
	{
	    itemEquipped = FindItemOnLayer( Layer.Cloak );

	    if ( null != itemEquipped && ((BaseQuiver)dropped).CheckConflictingLayer( this, itemEquipped, Layer.Cloak ))
	    {
		from.AddToBackpack( itemEquipped );
	    }

	    Backpack.DropItem( dropped );

	    if( DateTime.UtcNow > m_AnnoyanceDelay )
	    {
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ThankYou, null, null );
		}
		m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 20 );
	    }
	    AddItem( dropped );
	    from.SendMessage( "You give " + Name + " a quiver." );
	    return true;
	}
	else if ( dropped is BaseArmor && ((BaseArmor)dropped).AosStrReq > this.Str )
	{
	    if( DateTime.UtcNow > m_AnnoyanceDelay )
	    {
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ThisIsTooHeavy, null, null );
		}
		m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 20 );
	    }
	    from.AddToBackpack( dropped );
	}
	else if ( dropped is BaseArmor && ((BaseArmor)dropped).AosStrReq <= this.Str )
	{
	    BaseArmor armor = (BaseArmor)dropped;

	    if ( !armor.AllowMaleWearer && Body.IsMale )
	    {
		from.SendLocalizedMessage( 1010388 ); // Only females can wear this.
		from.AddToBackpack( armor );
	    }
	    else if ( !armor.AllowFemaleWearer && Body.IsFemale )
	    {
		from.SendMessage( "Only males can wear this." );
		from.AddToBackpack( armor );
	    }
	    else
	    {
		itemEquipped = FindItemOnLayer( dropped.Layer );
		if ( null != itemEquipped )
		    from.AddToBackpack( itemEquipped );

		Backpack.DropItem( dropped );

		if( DateTime.UtcNow > m_AnnoyanceDelay )
		{
		    if( m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ThankYou, null, null );
		    }
		    m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 20 );
		}
		AddItem( dropped );
		from.SendMessage( "You give " + Name + " armor." );
		return true;
	    }
	}
	else if ( dropped is BaseClothing || dropped is BaseJewel )
	{
	    if ( null != ( itemEquipped = FindItemOnLayer( dropped.Layer ) ))
		from.AddToBackpack( itemEquipped );

	    if( DateTime.UtcNow > m_AnnoyanceDelay )
	    {
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.ThankYou, null, null );
		}
		m_AnnoyanceDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 20 );
	    }
	    Backpack.DropItem( dropped );
	    AddItem( dropped );
	    from.SendMessage( "You give " + Name + (dropped is BaseJewel ? " jewelry." : " clothing.") );
	    return true;
	}
	else
	{
	    AddToBackpack( dropped );
	    return true;
	}

	return base.OnDragDrop( from, dropped );
    }

    public override bool CheckNonlocalDrop( Mobile from, Item item, Item target )
    {
	return SquirePack.CheckAccess( this, from );
    }

    public override bool AllowEquipFrom( Mobile from )
    {
	return SquirePack.CheckAccess( this, from );
    }

    public override bool CheckNonlocalLift( Mobile from, Item item )
    {
	return SquirePack.CheckAccess( this, from );
    }

    private bool HasRangedEquipped()
    {
	Item item = FindItemOnLayer( Layer.TwoHanded );
	if (item != null && item is BaseRanged)
	{
	    Say("I have a ranged weapon");
	    return true;
	}
	else
	{
	    return false;
	}
    }

    public override void OnItemAdded( Item item )
    {
	base.OnItemAdded( item );
	if ( item is BaseRanged )
	{
	    Say("I will use this ranged weapon from now on");
	    PlaySound(0x511);
	    FixedParticles(0x376A, 1, 14, 5045, EffectLayer.Waist);
	    ChangeAIType(AIType.AI_Archer);
	    //AI = AIType.AI_Archer;
	}
    }

    public override void OnItemRemoved( Item item )
    {
	base.OnItemRemoved( item );
	if ( item is BaseRanged )
	{
	    Say("I will change to melee");
	    PlaySound(0x511);
	    FixedParticles(0x376A, 1, 14, 5045, EffectLayer.Waist);
	    ChangeAIToDefault();
	    //AI = AIType.AI_Melee;
	}
    }

    public override void OnDeath( Container c )
    {
	Item deathShroud = new SquireDeathShroud();
	EquipItem( deathShroud );

	base.OnDeath(c);
    }

    public override void OnAfterResurrect()
    {
	Item deathShroud;
	deathShroud = FindItemOnLayer( Layer.OuterTorso );
	deathShroud.Delete();

	base.OnAfterResurrect();
    }

    private class BackpackEntry : ContextMenuEntry
	{
	    private Squire m_Squire;
	    private Mobile m_From;

	    public BackpackEntry( Squire squire, Mobile from ) : base( 6145, 12 )
	    {
		m_Squire = squire;
		m_From = from;
	    }

	    public override void OnClick()
	    {
		SquirePack.TryPackOpen( m_Squire, m_From );
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.ShowingOffASquiresBackpack, null, null );
		}
	    }
	}

    public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
    {
	if ( from.Alive )
	{
	    if ( SquirePack.CheckAccess( this, from ) )
		list.Add( new BackpackEntry( this, from ) );
	}

	base.AddCustomContextEntries( from, list );
    }

    public void AutoHealAlly( BaseCreature bc )
    {
	Item ChivalryBook = Backpack.FindItemByType( typeof( BookOfChivalry ) ); // Added 1.9.7

	if ( bc != null )
	{	
	    if ( bc is BaseCreature && bc.Controlled == true && bc.ControlMaster != null && bc.ControlMaster == this.ControlMaster )
	    {
		if ( InRange( bc, Bandage.Range ) )
		{
		    if ( bc.Hidden == false )
		    {
			if ( bc.Alive == true && bc.Poisoned )
			{
			    if ( bc.Body == 0x191 || bc.Body == 0x190 || bc.Body == 0x25D || bc.Body == 0x25E )
			    {
				if ( m_AutoHealOther == true )
				{
				    if (m_AutoCastCleanseByFireAlly == true && ChivalryBook != null && !IsDeadPet && Mana >= 10 && TithingPoints >= 10
					    && DateTime.UtcNow > m_SpellCastDelay && Hits >= ((HitsMax / 3) * 2) && bc.Poisoned == true) // Added 1.9.7
				    {
					new Server.Spells.Chivalry.CleanseByFireSpell( this, null ).Cast();

					SquireTargetCleanseByFireDelayTimer CleanseByFireTimer;
					CleanseByFireTimer = new SquireTargetCleanseByFireDelayTimer( this, bc, TimeSpan.FromSeconds( 1.0 ) );
					CleanseByFireTimer.Start();

					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else // Added 1.9.7
				    {
					Item item = Backpack.FindItemByType( typeof( Bandage ) );

					if ( Skills.Healing.Value >= 60.0 && Skills.Anatomy.Value >= 60.0 && null != item && null != BandageContext.BeginHeal( this, bc ) )
					{
					    item.Consume( 1 );
					    RevealingAction();
					    if( m_SquireBeQuiet == false )
					    {
						SquireDialog.DoSquireDialog( this.ControlMaster, this, SquireDialogTree.SquireCuresHumanoid, bc, null );
					    }

					    m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
					}
				    }
				}
			    }
			    else
			    {
				if ( m_AutoHealAnimals == true )
				{
				    if (m_AutoCastCleanseByFireAlly == true && ChivalryBook != null && !IsDeadPet && Mana >= 10 && TithingPoints >= 10
					    && DateTime.UtcNow > m_SpellCastDelay && Hits >= ((HitsMax / 3) * 2) && bc.Poisoned == true) // Added 1.9.7
				    {
					new Server.Spells.Chivalry.CleanseByFireSpell( this, null ).Cast();

					SquireTargetCleanseByFireDelayTimer CleanseByFireTimer;
					CleanseByFireTimer = new SquireTargetCleanseByFireDelayTimer( this, bc, TimeSpan.FromSeconds( 1.0 ) );
					CleanseByFireTimer.Start();

					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else // Added 1.9.7
				    {
					Item item = Backpack.FindItemByType( typeof( Bandage ) );			

					if (Skills.Veterinary.Value >= 60.0 && Skills.AnimalLore.Value >= 60.0 && null != item
						&& null != BandageContext.BeginHeal(this, bc))
					{
					    item.Consume( 1 );
					    RevealingAction();
					    if( m_SquireBeQuiet == false )
					    {
						SquireDialog.DoSquireDialog( this.ControlMaster, this, SquireDialogTree.SquireCuresAnimal, bc, null );
					    }

					    m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
					}
				    }
				}
			    }
			}
			else if ( bc.Alive == true && bc.Hits < bc.HitsMax - 10 )
			{
			    Item item = Backpack.FindItemByType( typeof( Bandage ) );

			    if ( bc.Body == 0x191 || bc.Body == 0x190 || bc.Body == 0x25D || bc.Body == 0x25E )
			    {
				if ( m_AutoHealOther == true )
				{
				    if (m_AutoCastCloseWoundsAlly == true && ChivalryBook != null && !IsDeadPet && Mana >= 10 && TithingPoints >= 10
					    && DateTime.UtcNow > m_SpellCastDelay && bc.Hits <= ((bc.HitsMax / 3) * 2) && bc.Poisoned == false && !MortalStrike.IsWounded(bc)) // Added 1.9.7
				    {
					new Server.Spells.Chivalry.CloseWoundsSpell( this, null ).Cast();

					SquireTargetCloseWoundsDelayTimer CloseWoundsTimer;
					CloseWoundsTimer = new SquireTargetCloseWoundsDelayTimer( this, bc, TimeSpan.FromSeconds( 1.5 ) );
					CloseWoundsTimer.Start();

					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else if ( null != item && null != BandageContext.BeginHeal( this, bc ) )
				    {
					item.Consume( 1 );
					RevealingAction();
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( this.ControlMaster, this, SquireDialogTree.SquireHealsWounded, bc, null );
					}

					m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				    }
				}
			    }
			    else
			    {
				if ( m_AutoHealAnimals == true )
				{
				    if (m_AutoCastCloseWoundsAlly == true && ChivalryBook != null && !IsDeadPet && Mana >= 10 && TithingPoints >= 10
					    && DateTime.UtcNow > m_SpellCastDelay && bc.Hits <= ((bc.HitsMax / 3) * 2) && bc.Poisoned == false && !MortalStrike.IsWounded(bc)) // Added 1.9.7
				    {
					new Server.Spells.Chivalry.CloseWoundsSpell( this, null ).Cast();

					SquireTargetCloseWoundsDelayTimer CloseWoundsTimer;
					CloseWoundsTimer = new SquireTargetCloseWoundsDelayTimer( this, bc, TimeSpan.FromSeconds( 1.5 ) );
					CloseWoundsTimer.Start();

					m_SpellCastDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 4.0 );
				    }
				    else if ( null != item && null != BandageContext.BeginHeal( this, bc ) )
				    {
					item.Consume( 1 );
					RevealingAction();
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( this.ControlMaster, this, SquireDialogTree.SquireHealsWounded, bc, null );
					}

					m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				    }
				}
			    }
			}
			else if ( bc.Alive == false )
			{
			    Item item = Backpack.FindItemByType( typeof( Bandage ) );

			    if ( bc.Body == 0x191 || bc.Body == 0x190 || bc.Body == 0x25D || bc.Body == 0x25E )
			    {
				if ( m_AutoHealOther == true )
				{
				    if (m_AutoRezAlly == true && Skills.Healing.Value >= 80.0 && Skills.Anatomy.Value >= 80.0 && null != item
					    && null != BandageContext.BeginHeal(this, bc))
				    {
					item.Consume( 1 );
					RevealingAction();
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( this.ControlMaster, this, SquireDialogTree.SquireRezsHumanoid, bc, null );
					}

					m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 9 );
				    }
				}
			    }
			    else
			    {
				if ( m_AutoHealAnimals == true )
				{
				    if (m_AutoRezAlly == true && Skills.Veterinary.Value >= 80.0 && Skills.AnimalLore.Value >= 80.0 && null != item
					    && null != BandageContext.BeginHeal(this, bc))
				    {
					item.Consume( 1 );
					RevealingAction();
					if( m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( this.ControlMaster, this, SquireDialogTree.SquireRezsAnimal, bc, null );
					}

					m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 9 );
				    }
				}
			    }
			}
		    }
		}
	    }
	}
    }

    public void SquireSpeaksWithSpirits()
    {
	if( DateTime.UtcNow > m_SpiritDelay )
	{
	    RevealingAction();
	    Timer anim = new SquireSpiritAnimTimer( this );

	    if ( CheckSkill( SkillName.SpiritSpeak, 0, 100 ) )
	    {	
		if ( !CanHearGhosts )
		{
		    Timer t = new SquireSpiritSpeakTimer( this );
		    double secs = this.Skills[SkillName.SpiritSpeak].Base / 50;
		    secs *= 90;
		    if ( secs < 15 )
			secs = 15;

		    t.Delay = TimeSpan.FromSeconds( secs );// 15 seconds to 3 minutes
		    m_SpiritDelay = DateTime.UtcNow + TimeSpan.FromSeconds( secs );
		    t.Start();
		    CanHearGhosts = true;
		    m_SpiritWorldConnected = true;
		}
		PlaySound( 0x24A );
		Emote( "Anh Mi Sah Ko" );
		anim.Start();
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( null, this, SquireDialogTree.SpiritSpeakSuccess, null, null );
		}
	    }
	    else
	    {
		PlaySound( 0x24A );
		Emote( "Anh Mi Sah Ko" );
		anim.Start();
		if( m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( null, this, SquireDialogTree.SpiritSpeakFail, null, null );
		}
		m_SpiritDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
		CanHearGhosts = false;
		m_SpiritWorldConnected = false;
	    }
	}
	else
	{
	    if( m_SpiritWorldConnected == false && m_SquireBeQuiet == false )
	    {
		SquireDialog.DoSquireDialog( null, this, SquireDialogTree.TooSoonToSpiritSpeak, null, null );
	    }
	    else if( m_SpiritWorldConnected == true && m_SquireBeQuiet == true )
	    {
		SquireDialog.DoSquireDialog( null, this, SquireDialogTree.StillConnectedToSpirits, null, null );
	    }
	}
    }

    public void SquireTithing() // Added 1.9.6
    {
	bool AnkhNearby = false;

	foreach (Item item in GetItemsInRange(8))
	{
	    if ( item is AnkhNorth || item is AnkhWest )
	    {
		AnkhNearby = true;
		break;
	    }
	}

	if ( AnkhNearby == false )
	{
	    if( m_SquireBeQuiet == false )
	    {
		SquireDialog.DoSquireDialog( null, this, SquireDialogTree.NoAnkhNearby, null, null );
	    }
	}
	else
	{
	    Container backPack = Backpack;
	    if ( null != backPack )
	    {
		Item gold = backPack.FindItemByType( typeof( Gold ) );
		int devotion;

		if ( TotalGold < 1 )
		{
		    if( m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( null, this, SquireDialogTree.NoGoldToTithe, null, null );
		    }
		}
		else
		{
		    if ( gold != null && gold.Amount > 0 )
		    {
			devotion = gold.Amount;
			gold.Consume( devotion );
			if( m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( null, this, SquireDialogTree.TitheSuccess, null, null );
			}
			TithingPoints += devotion;
			PlaySound( 0x243 );
			PlaySound( 0x2E6 );
			if( m_SquireBeQuiet == false )
			{
			    SayTo(this.ControlMaster, "I now have {0} tithing points.", TithingPoints);
			}
		    }
		}
	    }
	}
    }

    public void SetSquireTeam( Mobile from, string NewTeam ) // Added 1.9.7
    {
	if ( from.AccessLevel < AccessLevel.GameMaster && from != this.ControlMaster )
	{
	    if ( this != null )
		if( m_SquireBeQuiet == false )
		    this.Say( "Who are you to tell me what team I belong to?" );

	    return;
	}
	else if ( !from.Alive )
	{
	    if ( this != null )
		if( m_SquireBeQuiet == false )
		    this.Say( "What was that?" );

	    return;
	}

	if ( NewTeam.Length == 0 )
	    NewTeam = "Squire";

	m_SquireTeam = NewTeam;

	if( m_SquireBeQuiet == false )
	{
	    SquireDialog.DoSquireDialog( from, this, SquireDialogTree.SquireHasANewTeam, null, null );
	}
    }

    public override int GetMaxResistance(ResistanceType type)
    {
	int max = 100;

	base.GetMaxResistance( type );

	if( type == ResistanceType.Energy )
	    max -= 20; 

	if( type == ResistanceType.Fire )
	    max -= 20; 

	if( type == ResistanceType.Cold )
	    max -= 20; 

	if( type == ResistanceType.Poison )
	    max -= 20; 

	if( type == ResistanceType.Physical )
	    max -= 20; 

	if( Core.ML && this.Race == Race.Elf && type == ResistanceType.Energy )
	    max = 85;

	return max;
    }

    public override double RacialSkillBonus { get { return 20; } }

    public override void Serialize( GenericWriter writer )
    {
	base.Serialize(writer);
	writer.Write((int)16); // Changed 1.9.7 from 11 to 15 // increased version by unblest

	writer.Write((bool)m_LootDaemonBone); // Added unblest

	writer.Write((bool)m_LootZoogiFungus); // Added unblest
	writer.Write((bool)m_LootMap); // Added unblest
	writer.Write((bool)m_LootGold); // Added unblest

	writer.Write((bool)m_ConvenientStackable); // Added unblest
	writer.Write((bool)m_OnlyLootGoldAndMap); // Added unblest
	writer.Write((bool)m_AutoLoot); // Added unblest

	writer.Write( (string) m_SquireTeam ); // Added 1.9.7
	writer.Write( (bool) m_AutoCastCloseWoundsMaster ); // Added 1.9.7
	writer.Write( (bool) m_AutoCastCleanseByFireMaster ); // Added 1.9.7
	writer.Write( (bool) m_AutoCastCloseWoundsAlly ); // Added 1.9.7
	writer.Write( (bool) m_AutoCastCleanseByFireAlly ); // Added 1.9.7
	writer.Write( (bool) m_AutoUseWeaponAbility ); // Added 1.9.7
	writer.Write((bool)m_AutoCastConfidence); // Added Rafa
	writer.Write((bool)m_AutoLightningStrike); // Added Rafa
	writer.Write((bool)m_AutoEvasion); // Added Rafa
	writer.Write((bool)m_AutoCounterAttack); // Added Rafa
	writer.Write((bool)m_AutoMomentumStrike); // Added Rafa
	writer.Write((bool)m_AutoHonorableExecution); // Added Rafa
	writer.Write((bool)m_AutoPainSpike); // Added Rafa
	writer.Write((bool)m_AutoPoisonStrike); // Added Rafa
	writer.Write((bool)m_AutoWraithForm); // Added Rafa
	writer.Write((bool)m_AutoCurseWeapon); // Added Rafa
	writer.Write((bool)m_AutoWither); // Added Rafa
	writer.Write((bool)m_AutoLichForm); // Added Rafa
	writer.Write((bool)m_AutoVampiricEmbrace); // Added Rafa
	writer.Write((bool)m_AutoCastCloseWounds);
	writer.Write((bool)m_AutoCastCleanseByFire);
	writer.Write((bool)m_AutoUseSpiritSpeak);
	writer.Write((bool)m_AutoRezMaster);
	writer.Write((bool)m_AutoRezAlly);
	writer.Write((bool)m_AutoEquipLoot);
	writer.Write((bool)m_AutoUseTScroll);
	writer.Write((Item)m_WeaponSetOneSlotOne);
	writer.Write( (Item) m_WeaponSetOneSlotTwo );
	writer.Write( (Item) m_WeaponSetTwoSlotOne );
	writer.Write( (Item) m_WeaponSetTwoSlotTwo );
	writer.Write( (Item) m_WeaponSetThreeSlotOne );
	writer.Write((Item)m_WeaponSetThreeSlotTwo);
	writer.Write( (bool) m_AutoUsePowerScroll );
	writer.Write( (bool) m_DesperateMasterRun );
	writer.Write( (bool) m_AutoPickupAmmo );
	writer.Write( (bool) m_AutoHealOther );
	writer.Write( (bool) m_AutoHealMaster );
	writer.Write( (bool) m_AutoHealSelf );
	writer.Write( (bool) m_AutoUseCurePotion );
	writer.Write( (bool) m_AutoUseHealthPotion );
	writer.Write( (bool) m_SquireBeQuiet );
	writer.Write( (bool) m_AutoHealAnimals );
	writer.Write( (bool) m_Inspectable );
	writer.Write( (string) m_SquireNickname );
	writer.Write( (string) m_MasterNickname );
    }

    public override void Deserialize(GenericReader reader)
    {
	base.Deserialize(reader);
	int version = reader.ReadInt();
	if ( HasRangedEquipped() == true )
	{
	    ChangeAIType(AIType.AI_Archer);
	    //AI = AIType.AI_Archer;
	}
	switch ( version )
	{
	    case 16:
		{
		    m_LootDaemonBone = reader.ReadBool(); // Added unblest
		    goto case 15;
		}
	    case 15:
		{
		    m_LootZoogiFungus = reader.ReadBool(); // Added unblest
		    m_LootMap = reader.ReadBool(); // Added unblest
		    m_LootGold = reader.ReadBool(); // Added unblest
		    goto case 14;
		}
	    case 14:
		{
		    m_ConvenientStackable = reader.ReadBool(); // Added unblest
		    goto case 13;
		}
	    case 13:
		{
		    m_OnlyLootGoldAndMap = reader.ReadBool(); // Added unblest
		    m_AutoLoot = reader.ReadBool(); // Added unblest
		    goto case 12;
		}
	    case 12: // Added 1.9.7 + Rafa
		{
		    m_SquireTeam = reader.ReadString();
		    m_AutoCastCloseWoundsMaster = reader.ReadBool();
		    m_AutoCastCleanseByFireMaster = reader.ReadBool(); 
		    m_AutoCastCloseWoundsAlly = reader.ReadBool();
		    m_AutoCastCleanseByFireAlly = reader.ReadBool(); 
		    m_AutoUseWeaponAbility = reader.ReadBool();
		    m_AutoCastConfidence = reader.ReadBool(); //Added Rafa
		    m_AutoLightningStrike = reader.ReadBool(); //Added Rafa
		    m_AutoEvasion = reader.ReadBool(); //Added Rafa
		    m_AutoCounterAttack = reader.ReadBool(); //Added Rafa
		    m_AutoMomentumStrike = reader.ReadBool(); //Added Rafa
		    m_AutoHonorableExecution = reader.ReadBool(); //Added Rafa
		    m_AutoPainSpike = reader.ReadBool(); //Added Rafa
		    m_AutoPoisonStrike = reader.ReadBool(); //Added Rafa
		    m_AutoWraithForm = reader.ReadBool(); //Added Rafa
		    m_AutoCurseWeapon = reader.ReadBool(); //Added Rafa
		    m_AutoWither = reader.ReadBool(); //Added Rafa
		    m_AutoLichForm = reader.ReadBool(); //Added Rafa
		    m_AutoVampiricEmbrace = reader.ReadBool(); //Added Rafa
		    goto case 11;
		}
	    case 11:
		{
		    m_AutoCastCloseWounds = reader.ReadBool();
		    m_AutoCastCleanseByFire = reader.ReadBool(); 
		    goto case 10;
		}
	    case 10:
		{
		    m_AutoUseSpiritSpeak = reader.ReadBool();
		    m_AutoRezMaster = reader.ReadBool(); 
		    m_AutoRezAlly = reader.ReadBool();
		    goto case 9;
		}
	    case 9:
		{
		    m_AutoEquipLoot = reader.ReadBool();
		    m_AutoUseTScroll = reader.ReadBool(); 
		    goto case 8; 
		}
	    case 8:
		{
		    m_WeaponSetOneSlotOne = reader.ReadItem();
		    m_WeaponSetOneSlotTwo = reader.ReadItem();
		    m_WeaponSetTwoSlotOne = reader.ReadItem();
		    m_WeaponSetTwoSlotTwo = reader.ReadItem();
		    m_WeaponSetThreeSlotOne = reader.ReadItem();
		    m_WeaponSetThreeSlotTwo = reader.ReadItem();
		    goto case 7;
		}
	    case 7: m_AutoUsePowerScroll = reader.ReadBool(); goto case 6; 
	    case 6:
		    {
			m_DesperateMasterRun = reader.ReadBool();
			m_AutoPickupAmmo = reader.ReadBool();
			m_AutoHealOther = reader.ReadBool();
			m_AutoHealMaster = reader.ReadBool();
			m_AutoHealSelf = reader.ReadBool();
			m_AutoUseCurePotion = reader.ReadBool();
			m_AutoUseHealthPotion = reader.ReadBool();
			goto case 5;
		    }
	    case 5: m_SquireBeQuiet = reader.ReadBool(); goto case 4;
	    case 4: m_AutoHealAnimals = reader.ReadBool(); goto case 3;
	    case 3: m_Inspectable = reader.ReadBool(); goto case 2;
	    case 2: m_SquireNickname = reader.ReadString(); goto case 1;
	    case 1: m_MasterNickname = reader.ReadString(); break;
	    case 0: break;
	}
	AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
    }
    }
}
