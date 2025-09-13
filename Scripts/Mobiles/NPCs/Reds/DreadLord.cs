using System; 
using Server;
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 
using Server.Targeting;

namespace Server.Mobiles 
{ 
    public class DreadLord : BaseRed
    { 

	public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
	public DateTime m_NextTalk;

	public override void OnMovement( Mobile m, Point3D oldLocation )
	{
#region Tass23/Raist
	    if ( !( m is PlayerMobile ) )
		return;
	    if ((m is PlayerMobile) && (m.AccessLevel == AccessLevel.Player))
	    {
			if ( DateTime.UtcNow >= m_NextTalk && m is PlayerMobile && (this.Combatant == null || this.Combatant == m) ) // check if its time to talk
			{
				if (m.Kills < 5)
				{
					m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
					switch (Utility.Random(16))
					{
						case 0: Say("You better run, " + m.Name + "."); break;
						case 1: Say("Look, " + m.Name + " the noob paid us a visit."); break;

					}
				}
				else 
				{
					m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
					switch (Utility.Random(15))
					{
						case 0: Say("I see you are trying to be one of us, " + m.Name + ".... pityful attempt."); break;
						case 1: Say("I saw maggots scarier than you."); break;

					}
				}
			}
	    }
#endregion
	}




	[Constructable] 
	    public DreadLord() : base( AIType.AI_Melee, FightMode.Closest, 25, 1, 0.4, 0.3 ) 
	{ 
	    Title = "[LORD]";
	    if ( Female = Utility.RandomBool() ) 
	    { 
		Body = 401; 
		Name = NameList.RandomName( "female" );

	    }
	    else 
	    { 
		Body = 400; 			
		Name = NameList.RandomName( "male" ); 


	    }
	    //			Name.Hue = 2002;
	    //			Title.Hue = 2002;


	    SetStr(700, 800);
	    SetDex(150, 200);
	    SetInt(60, 100);
	    ActiveSpeed = 0.2;
	    PassiveSpeed = 0.1;

	    SetHits(900, 1200);

	    SetDamage(70, 95);

	    SetDamageType(ResistanceType.Physical, 100);

	    SetResistance(ResistanceType.Physical, 70, 80);
	    SetResistance(ResistanceType.Fire, 70, 80);
	    SetResistance(ResistanceType.Cold, 70, 80);
	    SetResistance(ResistanceType.Poison, 70, 80);
	    SetResistance(ResistanceType.Energy, 70, 80);

	    SetSkill(SkillName.Swords, 99.0, 120.0);
	    SetSkill(SkillName.Tactics, 99.0, 120.0);
	    SetSkill(SkillName.MagicResist, 99.0, 120.0);
	    SetSkill(SkillName.Tactics, 99.0, 120.0);
	    SetSkill(SkillName.Parry, 99.0, 120.0);
	    SetSkill(SkillName.Anatomy, 99.0, 120.0);
	    SetSkill(SkillName.Healing, 99.0, 120.0);
		SetSkill(SkillName.DetectHidden, 99.0, 120.0);
	    SetSkill(SkillName.Magery, 85.0, 120.0);
	    SetSkill(SkillName.EvalInt, 85.0, 120.0);

	    if (Utility.Random(1, 2) == 2) // 50% chance to have an OmniAI skill set
		OmniAI.SetRandomSkillSet(this, 70.0, 110.0);

	    Fame = 10000;
	    Karma = -7500;
	    Criminal = true;
		Kills = Utility.RandomMinMax(5, 20);

	    VirtualArmor = 60;

	    switch (Utility.Random(4))
	    {
		case 0: BattleAxe weapona = new BattleAxe();
			weapona.Hue = 1168;
			weapona.LootType = LootType.Newbied;
			weapona.Attributes.SpellChanneling = 1;
			weapona.WeaponAttributes.HitLightning = 40;
			weapona.Movable = false;
			AddItem( weapona );
			break;
		case 1: Axe weaponb = new Axe();
			weaponb.Hue = 1260;
			weaponb.LootType = LootType.Newbied;
			weaponb.Attributes.SpellChanneling = 1;
			weaponb.WeaponAttributes.HitLightning = 40;
			weaponb.Movable = false;
			AddItem( weaponb );
			break;
		case 2: Bardiche weaponc = new Bardiche();
			weaponc.Hue = 1266;
			weaponc.LootType = LootType.Newbied;
			weaponc.Attributes.SpellChanneling = 1;
			weaponc.WeaponAttributes.HitLightning = 40;
			weaponc.Movable = false;
			AddItem( weaponc );
			break;
		case 3: Hatchet weapond = new Hatchet();
			weapond.Hue = 1272;
			weapond.LootType = LootType.Newbied;
			weapond.Attributes.SpellChanneling = 1;
			weapond.WeaponAttributes.HitHarm = 40;
			weapond.Movable = false;
			AddItem( weapond );
			break;
	    } 



	    Item Robe =  new Robe( );			
	    Robe.Name = "Dastardly Cowl";
	    Robe.Movable = false; 
	    Robe.Hue = 1269;
	    AddItem( Robe );



	    switch (Utility.Random(3))
	    {
		case 0: AddItem( new LongPants(1050)); break;
		case 1: Item LegsOfBane = new LeggingsOfBane();
			LegsOfBane.Hue = 1269;
			LegsOfBane.LootType = LootType.Newbied;
			LegsOfBane.Movable = false;
			AddItem( LegsOfBane );
			break;
		case 2: break;
	    } 

	    switch (Utility.Random(3))
	    {
		case 0:	AddItem( new Boots() ); break;
		case 1: Item Sandals = new Sandals();
			Sandals.Hue = 1195;
			Sandals.LootType = LootType.Blessed;
			Sandals.Movable = false;
			AddItem( Sandals );
			break;
		case 2: break;
	    } 


	    Item PlateHelm = new PlateHelm();
	    PlateHelm.Hue = 1195;
	    PlateHelm.LootType = LootType.Newbied;
	    PlateHelm.Movable = false;
	    AddItem( PlateHelm );


	    switch (Utility.Random(3))
	    {
		case 0:	AddItem( new LeatherGloves() ); break;
		case 1: break;
		case 2: break;
	    } 


	    AddItem( new Shirt(743) );




	    Utility.AssignRandomHair( this );

	    for (int i = 0; i < 30; i++)
	    {
		PackItem( new GreaterCurePotion() );
		PackItem( new GreaterHealPotion() );
		PackItem( new TotalRefreshPotion() );
	    }

	    PackItem(new Bandage(Utility.RandomMinMax(40, 60)));

	}
	// + OmniAI support +
	protected override BaseAI ForcedAI
	{
	    get
	    {
		return new OmniAI(this);
	    }
	}
	// - OmniAI support -

	public override void OnAfterSpawn()
	{
	    base.OnAfterSpawn();

	    if (!(Server.Mobiles.AnimalTrainer.IsNoMountRegion(this.Region) && MyServerSettings.NoMountsInCertainRegions()))
	    {
		new EtherealHorse().Rider = this;
	    }
	}

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.Average );
	}

	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool CanHeal { get { return true; } }

	public override void OnThink()
	{
	    base.OnThink();

	    // Dismount if in no-mount area
	    if (this.Mounted && Server.Mobiles.AnimalTrainer.IsNoMountRegion(this.Region) && MyServerSettings.NoMountsInCertainRegions())
	    {
		Server.Mobiles.AnimalTrainer.DismountPlayer(this);
	    }
	}

	public override void OnDeath(Container c)
	{
	    base.OnDeath(c);
	    if (Utility.RandomDouble() > 0.75)
	    {
		switch (Utility.Random(6))
		{
		    case 0:
			c.DropItem(new PhoenixGloves());
			break;
		    case 1:
			c.DropItem(new PhoenixGorget());
			break;
		    case 2:
			c.DropItem(new PhoenixHelm());
			break;
		    case 3:
			c.DropItem(new PhoenixLegs());
			break;
		    case 4:
			c.DropItem(new PhoenixSleeves());
			break;
		    case 5:
			c.DropItem(new PhoenixChest());
			break;
		}
	    }
	    if (Utility.RandomDouble() > 0.60)
		c.DropItem(new Gold(Utility.RandomMinMax(1000, 3000)));
	}


	public DreadLord( Serial serial ) : base( serial ) 
	{ 
	} 



	public override void Serialize( GenericWriter writer ) 
	{ 
	    base.Serialize( writer ); 

	    writer.Write( (int) 0 ); // version 
	} 

	public override void Deserialize( GenericReader reader ) 
	{ 
	    base.Deserialize( reader ); 

	    int version = reader.ReadInt(); 
	} 
    } 
}   
