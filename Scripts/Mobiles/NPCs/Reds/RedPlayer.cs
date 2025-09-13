using System; 
using Server;
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
    public class RedPlayer : BaseRed
    { 
	public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 10.0 );
	public DateTime m_NextTalk;

	public static int GetRandomHue()
	{
	    switch ( Utility.Random( 6 ) )
	    {
		case 0: return 0;
		case 1: return Utility.RandomRedHue();
		case 2: return Utility.RandomGreenHue();
		case 3: return Utility.RandomRedHue();
		case 4: return Utility.RandomYellowHue();
		case 5: return Utility.RandomNeutralHue();
	    }

	    return 0;
	}

	[Constructable] 
	    public RedPlayer() : base( AIType.AI_Melee, FightMode.Closest, 25, 1, 0.4, 0.3 ) 
	{ 

	    SpeechHue = Utility.RandomDyedHue();

	    switch (Utility.Random(6))
	    {
		case 0: Title = "[PK]"; break;
		case 1: Title = "[FUK]"; break;
		case 2: Title = "[KAOS]"; break;
		case 3: Title = "[PKR]"; break;
		case 4: Title = "[DIE]"; break;
		case 5: Title = "[KIL]"; break;

	    } 

	    SetStr(70, 150);
	    SetDex(60, 200);
	    SetInt(125, 150);
	    ActiveSpeed = 0.2;
	    PassiveSpeed = 0.1;

	    SetHits(100, 250);

	    SetDamage(30, 40);

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

	    if (Utility.Random(1, 5) == 2) // 20% chance to have an OmniAI skill set
		OmniAI.SetRandomSkillSet(this, 70.0, 110.0);

	    Fame = 1000;
	    Karma = -5000;
	    Criminal = true;
		Kills = Utility.RandomMinMax(2, 7);
	    VirtualArmor = 20;

	    switch (Utility.Random(4))
	    {
		case 0: 
			Longsword longsword = new Longsword();
			longsword.Attributes.SpellChanneling = 1;
			AddItem( longsword ); 
			break;
		case 1: 
			Axe axe = new Axe();
			axe.Attributes.SpellChanneling = 1;
			AddItem( axe ); 
			break;
		case 2: 
			Bardiche bardiche = new Bardiche();
			bardiche.Attributes.SpellChanneling = 1;
			AddItem( bardiche ); 
			break;
		case 3: 
			Hatchet hatchet = new Hatchet();
			hatchet.Attributes.SpellChanneling = 1;
			AddItem( hatchet ); 
			break;
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
		if (this is RedPlayer)
				PackItem( new ExplosionPotion() );
	    }

	    PackItem(new Bandage(Utility.RandomMinMax(10, 40)));
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

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.Average );
	}

	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool CanHeal { get { return true; } }

	public override void OnDeath(Container c)
	{
	    base.OnDeath(c);
	    if (Utility.RandomDouble() > 0.98)
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
		c.DropItem(new Gold(Utility.RandomMinMax(500, 1000)));
	}

	public override void OnMovement( Mobile m, Point3D oldLocation )
	{
	    if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) && !m.Hidden && (this.Combatant == null || this.Combatant == m) )
	    {

			if ( DateTime.UtcNow >= m_NextTalk && m is PlayerMobile ) // check if its time to talk
			{
				if (m.Kills < 5)
				{
					m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
					switch (Utility.Random(11))
					{
						case 0: Say("Hah " + m.Name + " I'm gonna get you"); break;
						case 1: Say("" + m.Name + "!!!"); break;
						case 2: Say("" + m.Name + " where do you think your going?  Shit, I meant you're."); break;
						case 3: Say("Hey " + m.Name + ", Why so serious?"); break;
						case 4: Say("Found a Blue!"); break;
						case 5: Say("" + m.Name + ", you better run"); break;
						case 6: Say("Die, " + m.Name + "."); break;
						case 7: Say("" + m.Name + "!!"); break;
						case 8: Say("hahahah a noob !"); break;
						case 9: Say("What a trammy!"); break;
						case 10: Say("Hey nice gear you got.  Come closer!"); break;
					}
				}
				else 
				{
					m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
					switch (Utility.Random(9))
					{
						case 0: Say("Hah " + m.Name + " You think you're one of us?"); break;
						case 1: Say("" + m.Name + "!!!"); break;
						case 2: Say("" + m.Name + ", your crossbow sucks ass"); break;
						case 3: Say("Hey " + m.Name + ", I could take you on anyday"); break;
						case 4: Say("Just wait till you get below 5 kills!"); break;
						case 5: Say("" + m.Name + ", you're nothing, i have more kills than you!"); break;
						case 6: Say("hey, " + m.Name + "."); break;
						case 7: Say("" + m.Name + " fuck the blues"); break;
						case 8: Say("you're still a noob"); break;
					}
				}
			}
	    }

	}



	public RedPlayer( Serial serial ) : base( serial ) 
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
