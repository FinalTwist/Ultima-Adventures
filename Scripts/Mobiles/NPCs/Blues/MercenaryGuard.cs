using System; 
using Server;
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 
using Server.Targeting;

namespace Server.Mobiles 
{ 
    public class MercenaryGuard : BaseBlue
    { 
	private bool m_Bandaging;
	public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
	public DateTime m_NextTalk;

	public override void OnMovement( Mobile m, Point3D oldLocation )
	{
	    if ( !( m is PlayerMobile ) )
		return;
	    if ((m is PlayerMobile) && (m.AccessLevel == AccessLevel.Player))
	    {
		if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) )
		{
			if ( !m.Hidden && DateTime.UtcNow >= m_NextResurrect && this.HealsYoungPlayers && m.Hits < (m.HitsMax/2) && m is PlayerMobile || DateTime.UtcNow >= m_NextResurrect && m.Hits < (m.HitsMax/2) && m is BaseBlue )
		    {
			OfferHeal( (PlayerMobile) m );
		    }
		    else if ( !m.Hidden && this.Combatant == null && DateTime.UtcNow >= m_NextTalk ) // check if its time to talk
		    {
			m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
			switch (Utility.Random(6))
			{
			    case 0: Emote("Greets, " + m.Name + " "); break;
			    case 1: Emote("" + m.Name + ""); break;
			    case 2: Emote("Move along," + m.Name ); break;
			    case 3: Emote("Behave here " + m.Name ); break;
			    case 4: Emote("We keep the peace here."); break;
			    case 5: Emote("*nods*"); break;
			}
		    }
		}
	    }

	}


	[Constructable] 
	    public MercenaryGuard() : base( AIType.AI_Melee, FightMode.Closest, 25, 1, 0.4, 0.3 ) 
	{

			AIFullSpeedActive = true; // Force full speed
			AIFullSpeedPassive = false;		
	    
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

	    SetStr(400, 550);
	    SetDex(150, 200);
	    SetInt(60, 100);


	    SetHits(200, 300);

	    SetDamage(40, 75);

	    SetDamageType(ResistanceType.Physical, 100);

	    SetResistance(ResistanceType.Physical, 50, 70);
	    SetResistance(ResistanceType.Fire, 50, 70);
	    SetResistance(ResistanceType.Cold, 50, 70);
	    SetResistance(ResistanceType.Poison, 50, 70);
	    SetResistance(ResistanceType.Energy, 50, 70);

	    SetSkill(SkillName.Swords, 89.0, 120.0);
	    SetSkill(SkillName.Tactics, 89.0, 120.0);
	    SetSkill(SkillName.MagicResist, 89.0, 120.0);
	    SetSkill(SkillName.Tactics, 89.0, 120.0);
	    SetSkill(SkillName.Parry, 89.0, 120.0);
	    SetSkill(SkillName.Anatomy, 85.0, 120.0);
	    SetSkill(SkillName.Healing, 85.0, 120.0);
	    SetSkill(SkillName.Magery, 85.0, 120.0);
	    SetSkill(SkillName.EvalInt, 85.0, 120.0);

	    if (Utility.Random(1, 2) == 2) // 50% chance to have an OmniAI skill set
		OmniAI.SetRandomSkillSet(this, 70.0, 110.0);

		Server.Misc.IntelligentAction.DressUpFighters( this, "", false, 0 );
		
	    Fame = 10000;
	    Karma = 7500;

	    VirtualArmor = 60;

	    Utility.AssignRandomHair( this );

	    for (int i = 0; i < 10; i++)
	    {
			PackItem( new GreaterCurePotion() );
			PackItem( new GreaterHealPotion() );
			PackItem( new TotalRefreshPotion() );
	    }

	    PackItem(new Bandage(Utility.RandomMinMax(20, 35)));
		Title = "the mercenary";

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

	public MercenaryGuard( Serial serial ) : base( serial ) 
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

		AIFullSpeedActive = true; // Force full speed
		AIFullSpeedPassive = false;
	} 
    } 
}   
