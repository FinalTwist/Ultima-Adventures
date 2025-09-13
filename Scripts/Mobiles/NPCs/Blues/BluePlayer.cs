using System; 
using Server;
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
    public class BluePlayer : BaseBlue
    { 
	private bool m_Bandaging;
	public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
	public DateTime m_NextTalk;

	public static int GetRandomHue()
	{
	    switch ( Utility.Random( 6 ) )
	    {
		case 0: return 0;
		case 1: return Utility.RandomBlueHue();
		case 2: return Utility.RandomGreenHue();
		case 3: return Utility.RandomRedHue();
		case 4: return Utility.RandomYellowHue();
		case 5: return Utility.RandomNeutralHue();
	    }

	    return 0;
	}

	[Constructable] 
	public BluePlayer() : base( AIType.AI_Melee, FightMode.Closest, 25, 1, 0.4, 0.3 ) 
	{ 

	    SpeechHue = Utility.RandomDyedHue();

	    switch (Utility.Random(6))
	    {
		case 0: Title = "[BEC]"; break;
		case 1: Title = "[COOL]"; break;
		case 2: Title = "[FUN]"; break;
		case 3: Title = "[PYR]"; break;
		case 4: Title = "[FRN]"; break;
		case 5: Title = "[CRA]"; break;

	    } 

	    SetStr(80, 125);
	    SetDex(80, 200);
	    SetInt(60, 100);
	    ActiveSpeed = 0.2;
	    PassiveSpeed = 0.1;

	    SetHits(100, 250);

	    SetDamage(10, 25);

	    SetDamageType(ResistanceType.Physical, 100);
		
	    SetResistance(ResistanceType.Physical, 30, 50);
	    SetResistance(ResistanceType.Fire, 40, 60);
	    SetResistance(ResistanceType.Cold, 40, 60);
	    SetResistance(ResistanceType.Poison, 40, 60);
	    SetResistance(ResistanceType.Energy, 40, 60);

	    SetSkill(SkillName.Swords, 79.0, 110.0);
	    SetSkill(SkillName.Tactics, 79.0, 110.0);
	    SetSkill(SkillName.MagicResist, 79.0, 110.0);
	    SetSkill(SkillName.Tactics, 79.0, 110.0);
	    SetSkill(SkillName.Parry, 79.0, 110.0);
	    SetSkill(SkillName.Anatomy, 70.0, 110.0);
	    SetSkill(SkillName.Healing, 70.0, 110.0);

	    if (Utility.Random(1, 2) == 2) // 50% chance to have an OmniAI skill set
		OmniAI.SetRandomSkillSet(this, 70.0, 110.0);

	    Fame = 1000;
	    Karma = 1000;

	    VirtualArmor = 20;

	    switch (Utility.Random(4))
	    {
		case 0: AddItem( new Longsword() ); break;
		case 1: AddItem( new Axe() ); break;
		case 2: AddItem( new Bardiche() ); break;
		case 3: AddItem( new Hatchet() ); break;
	    } 


	    switch (Utility.Random(5))
	    {
		case 0: Item cloak = new Cloak();
			cloak.Movable = false;
			cloak.Hue = GetRandomHue();
			AddItem( cloak );
			break;
		case 1: Item shroud = new HoodedShroudOfShadows();
			shroud.Movable = false;
			shroud.Hue = GetRandomHue();
			AddItem( shroud );
			break;
		case 2: break;
		case 3: Item monkrobe = new MonkRobe();
			monkrobe.Movable = false;
			monkrobe.Hue = GetRandomHue();
			AddItem( monkrobe );
			break;
		case 4: break;
	    } 


	    switch (Utility.Random(4))
	    {
		case 0: AddItem( new LongPants() ); break;
		case 1: Item LegsOfBane = new LeggingsOfBane();
			LegsOfBane.Hue = GetRandomHue();
			LegsOfBane.LootType = LootType.Newbied;
			LegsOfBane.Movable = false;
			AddItem( LegsOfBane );
			break;
		case 2: Item ShortPantss = new ShortPants();
			ShortPantss.Hue = GetRandomHue();
			ShortPantss.LootType = LootType.Newbied;
			ShortPantss.Movable = false;
			AddItem( ShortPantss );
			break;
		case 3: Item PlateLegsv = new PlateLegs();
			PlateLegsv.Hue = GetRandomHue();
			PlateLegsv.LootType = LootType.Newbied;
			PlateLegsv.Movable = false;
			AddItem( PlateLegsv );
			break;
	    } 

	    switch (Utility.Random(5))
	    {
		case 0:	AddItem( new Boots() ); break;
		case 1: Item Sandals = new Sandals();
			Sandals.Hue = GetRandomHue();
			Sandals.LootType = LootType.Blessed;
			Sandals.Movable = false;
			AddItem( Sandals );
			break;
		case 2: Item Bootsv = new Boots();
			Bootsv.Hue = GetRandomHue();
			Bootsv.LootType = LootType.Blessed;
			Bootsv.Movable = false;
			AddItem( Bootsv );
			break;
		case 3: Item Shoesv = new Shoes();
			Shoesv.Hue = GetRandomHue();
			Shoesv.LootType = LootType.Blessed;
			Shoesv.Movable = false;
			AddItem( Shoesv );
			break;
		case 4: break;
	    } 

	    switch (Utility.Random(6))
	    {
		case 0:	AddItem( new ClothNinjaHood(  ) ); break;
		case 1: Item SpiritOfTheTotem = new SpiritOfTheTotem();
			SpiritOfTheTotem.Hue = GetRandomHue();
			SpiritOfTheTotem.Movable = false;
			SpiritOfTheTotem.LootType = LootType.Blessed;
			AddItem( SpiritOfTheTotem );
			break;
		case 2:	Item BoneHelmv = new BoneHelm();
			BoneHelmv.Hue = GetRandomHue();
			BoneHelmv.Movable = false;
			BoneHelmv.LootType = LootType.Blessed;
			AddItem( BoneHelmv );
			break;
		case 3:	Item CloseHelmv = new CloseHelm();
			CloseHelmv.Hue = GetRandomHue();
			CloseHelmv.Movable = false;
			CloseHelmv.LootType = LootType.Blessed;
			AddItem( CloseHelmv );
			break;
		case 4: AddItem( new SkullCap( ) );break;
		case 5: Item TricorneHatv = new TricorneHat();
			TricorneHatv.Hue = GetRandomHue();
			TricorneHatv.Movable = false;
			TricorneHatv.LootType = LootType.Blessed;
			AddItem( TricorneHatv );
			break;
	    } 

	    switch (Utility.Random(3))
	    {
		case 0:	Item LeatherGlovesv = new LeatherGloves();
			LeatherGlovesv.Hue = GetRandomHue();
			LeatherGlovesv.Movable = false;
			LeatherGlovesv.LootType = LootType.Blessed;
			AddItem( LeatherGlovesv );
			break;
		case 1: Item PlateGlovesv = new PlateGloves();
			PlateGlovesv.Hue = GetRandomHue();
			PlateGlovesv.Movable = false;
			PlateGlovesv.LootType = LootType.Blessed;
			AddItem( PlateGlovesv );
			break;
		case 2: AddItem( new RingmailGloves() ); break;
	    } 



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

	    Utility.AssignRandomHair( this );

	    for (int i = 0; i < 15; i++)
	    {
		PackItem( new GreaterCurePotion() );
		PackItem( new GreaterHealPotion() );
		PackItem( new TotalRefreshPotion() );
	    }

	    PackItem(new Bandage(Utility.RandomMinMax(10, 20)));

	}

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

	// + OmniAI support +
        protected override BaseAI ForcedAI
        {
            get
            {
                return new OmniAI(this);
            }
        }
	// - OmniAI support -

	public override bool CanRummageCorpses{ get{ return true; } }
	//       		 public override bool CanHeal { get { return true; } }

	public override void OnThink()
	{
	    base.OnThink();

	    // Use bandages
	    if ( (this.IsHurt() || this.Poisoned) && m_Bandaging == false )
	    {
		Bandage m_Band = (Bandage)this.Backpack.FindItemByType( typeof ( Bandage ) );

		if ( m_Band != null )
		{
		    m_Bandaging = true;

		    if ( BandageContext.BeginHeal( this, this ) != null )
			m_Band.Consume();
		    BandageTimer bt = new BandageTimer( this );
		    bt.Start();
		}
	    }

	    // Dismount if in no-mount area
	    if (this.Mounted && Server.Mobiles.AnimalTrainer.IsNoMountRegion(this.Region) && MyServerSettings.NoMountsInCertainRegions())
	    {
		Server.Mobiles.AnimalTrainer.DismountPlayer(this);
	    }
	}

	private class BandageTimer : Timer 
	{ 
	    private BluePlayer pk;

	    public BandageTimer( BluePlayer o ) : base( TimeSpan.FromSeconds( 6 ) ) 
	    { 
		pk = o;
		Priority = TimerPriority.OneSecond; 
	    } 

	    protected override void OnTick() 
	    { 
		pk.m_Bandaging = false; 
	    } 
	}

	public override void OnMovement( Mobile m, Point3D oldLocation )
	{
	    if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) && m is PlayerMobile )
	    {
			if ( !m.Frozen && DateTime.UtcNow >= m_NextResurrect && !m.Alive )
			{
				m_NextResurrect = DateTime.UtcNow + ResurrectDelay;

				if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
				{
				m.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
				}
				else if ( CheckResurrect( m ) )
				{
				OfferResurrection( m );
				}
			}
			else if ( !m.Hidden && DateTime.UtcNow >= m_NextResurrect && m.Hits < (m.HitsMax/2) && m is PlayerMobile || DateTime.UtcNow >= m_NextResurrect && m.Hits < (m.HitsMax/2) && m is BaseBlue )
			{
				OfferHeal( (PlayerMobile) m );
			}
			else if ( !m.Hidden && this.Combatant == null && DateTime.UtcNow >= m_NextTalk && m is PlayerMobile ) // check if its time to talk
			{
				m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
				switch (Utility.Random(9))
				{
					case 0: Say("Hello " + m.Name + " have you come to play?"); break;
					case 1: Say("" + m.Name + "?"); break;
					case 2: Say("" + m.Name + " where do you think your going?"); break;
					case 3: Say("Hey " + m.Name + " this is my spawn"); break;
					case 4: Say(" Hey " + m.Name + " , did you hear about Moonglow?"); break;
					case 5: Say("" + m.Name + " How are ya?"); break;
					case 6: Say("To adventure, " + m.Name + "."); break;
					case 7: Say("" + m.Name + "!!"); break;
					case 8: Say("Hi " + m.Name + "!"); break;
				}

			}
	    }

	}

	public override bool CheckResurrect( Mobile m )
	{
	    if ( m.Criminal )
	    {
		Say("You did something wrong, wait a bit"); // Thou art a criminal.  I shall not resurrect thee.
		return false;
	    }
	    else if ( m.Kills >= 5 )
	    {
		Say("I don't help reds"); // Thou'rt not a decent and good person. I shall not resurrect thee.
		return false;
	    }
	    else if ( m.Karma < 0 )
	    {
		Say("You have bad Karma, but OK."); // Thou hast strayed from the path of virtue, but thou still deservest a second chance.
	    }
	    Say("An Corp");
	    return true;
	}


	public BluePlayer( Serial serial ) : base( serial ) 
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
