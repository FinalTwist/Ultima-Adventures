using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 
using Server.Network;
using Server.Regions;

namespace Server.Mobiles 
{
    [CorpseName( "a henchman corpse" )] 
    public class HenchmanArcher : BaseCreature
    {
	private bool m_Bandaging = false;
	public bool Bandaging { get { return m_Bandaging; } set { m_Bandaging = value; } }

	private DateTime m_NextMorale;
	public DateTime NextMorale{ get{ return m_NextMorale; } set{ m_NextMorale = value; } }

	public override void OnMovement( Mobile m, Point3D oldLocation )
	{
	    bool GoAway = HenchmanFunctions.OnMoving( m, oldLocation, this, m_NextMorale );
	    if ( GoAway == true ){ Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerCallback( Delete ) ); }
	    else { m_NextMorale = (DateTime.UtcNow + TimeSpan.FromSeconds( 60 )); }
	}

	public override void OnThink()
	{
	    base.OnThink();

	    // Use bandages
	    HenchmanFunctions.UseBandages(this);
	}


	[Constructable] 
	    public HenchmanArcher( int myBody, int nMounted, double nSkills, int nStats ) : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "henchman";
	    Body = myBody;
	    RangeFight = 7;

	    if ( Body == 401 ){ this.Female = true; }

	    int nStr = (int)((nStats / 6) * 2);
	    int nDex = (int)((nStats / 6) * 3);
	    int nInt = (int)((nStats / 6) * 1);
	    int nArmor = (int)(nStats / 6); if ( nArmor > 70 ){ nArmor = 70; }
	    int nProtect = (int)(nStats / 4); if ( nProtect > 70 ){ nProtect = 70; }
	    int nDamage = (int)(nStats / 10);

	    SetStr( nStr );
	    SetDex( nDex );
	    SetInt( nInt );

	    SetHits( nStr*2 );
	    SetStam( nDex*2 );
	    SetMana( nInt*2 );

	    SetDamage( (int)(nDamage/2), nDamage );

	    ControlSlots = 1;

	    VirtualArmor = (int)(nStats / 5);

	    SetDamageType( ResistanceType.Physical, 40 );
	    SetDamageType( ResistanceType.Fire, 10 );
	    SetDamageType( ResistanceType.Cold, 10 );
	    SetDamageType( ResistanceType.Poison, 10 );
	    SetDamageType( ResistanceType.Energy, 10 );

	    SetResistance( ResistanceType.Physical, nProtect );
	    SetResistance( ResistanceType.Fire, nArmor );
	    SetResistance( ResistanceType.Cold, nArmor );
	    SetResistance( ResistanceType.Poison, nArmor );
	    SetResistance( ResistanceType.Energy, nArmor );

	    SetSkill(SkillName.Archery, nSkills );
	    SetSkill(SkillName.MagicResist, nSkills );
	    SetSkill(SkillName.Tactics, nSkills );
	    SetSkill(SkillName.Parry, nSkills );
	    SetSkill(SkillName.Anatomy, nSkills );
	    SetSkill(SkillName.Focus, nSkills );
	    SetSkill(SkillName.Healing, 50, 70 );

	    if ( nMounted > 0 )
	    {
		new HenchHorse().Rider = this;
		ActiveSpeed = 0.1;
		PassiveSpeed = 0.2;
	    }
	}

	public override void OnSpeech( SpeechEventArgs e )
	{
	    if (!e.Handled)
	    {
		if (Insensitive.Equals(e.Speech, "report"))
		{
		    HenchmanFunctions.ReportStatus(this);
		}
		else if (Insensitive.StartsWith(e.Speech, "all") || Insensitive.StartsWith(e.Speech, this.Name))
		{
		    if (Insensitive.Contains(e.Speech, "speed"))
		    {
			HenchmanFunctions.ChangeSpeed(this);
		    }
		    else if (Insensitive.Contains(e.Speech, "run"))
		    {
			HenchmanFunctions.ChangeSpeed(this, true);
		    }
		    else if (Insensitive.Contains(e.Speech, "walk"))
		    {
			HenchmanFunctions.ChangeSpeed(this, false);
		    }
		}
	    }
	    base.OnSpeech(e);
	}

	public override bool ClickTitle{ get{ return false; } }
	public override bool ShowFameTitle{ get{ return false; } }
	public override bool AlwaysAttackable{ get{ return true; } }
	public override bool ReacquireOnMovement{ get{ return true; } }
	public override bool InitialInnocent{ get{ return true; } }
	public override bool DeleteOnRelease{ get{ return true; } }
	public override bool DeleteCorpseOnDeath{ get{ return true; } }
	public override bool IsDispellable { get { return false; } }
	public override bool IsBondable{ get{ return false; } }
	public override bool CanBeRenamedBy( Mobile from ){ return false; }

	public override void OnGaveMeleeAttack( Mobile defender )
	{
	    HenchmanFunctions.OnGaveAttack( this );
	}

	public override void OnDamagedBySpell(Mobile attacker)
	{
	    base.OnDamagedBySpell(attacker);
	    HenchmanFunctions.OnSpellAttack( this );
	}

	public override void OnGotMeleeAttack( Mobile defender )
	{
	    HenchmanFunctions.OnGotAttack( this );
	}

	public override bool OnBeforeDeath()
	{
	    HenchmanFunctions.OnDead( this );
	    if ( !base.OnBeforeDeath() )
		return false;

	    return true;
	}

	public override bool OnDragDrop( Mobile from, Item dropped )
	{
	    HenchmanFunctions.OnGive( from, dropped, this );
	    return base.OnDragDrop( from, dropped );
	}

	public HenchmanArcher( Serial serial ) : base( serial ) 
	{ 
	} 

	public override void Serialize( GenericWriter writer ) 
	{ 
	    base.Serialize( writer ); 
	    writer.Write( (int) 0 ); // version
	    Loyalty = 100;
	} 

	public override void Deserialize( GenericReader reader ) 
	{ 
	    base.Deserialize( reader ); 
	    int version = reader.ReadInt();
	    Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerCallback( Delete ) );
	} 
    } 
}   
