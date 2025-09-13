/* Massapequa's Custom Abilities:
This is an attempt to make fun and cool custom abilities that are (relatively) easy
for others to implement into their server. Many of these abilities have modifiers that
allow you to change how they look for each mob that uses it. Each ability shows an example
of how it can be used within the code for the ability itself. In Addition, this comes with 
custom mobiles as emaples to help you see how it works.
You can call the ability through: CustomAbility.CheckTrigger(caster, ability)
or you can use the CustomAbilityList if your creature is using multiple abilities at a time.
*/

using System;
using Server;
using System.Collections.Generic;
using System.Linq;
using Server.Items;
using Server.Network;
using System.Collections;
using Server.Spells;

namespace Server.Mobiles
{
	public abstract class CustomAbility
	{

            [CommandProperty(AccessLevel.GameMaster)]
            public CustomAbility[] CustomAbilities { get; private set; }

	    public virtual int MaxRange{ get { return 1; } }
	    public virtual double TriggerChance { get { return 0.1; } }
	    public virtual TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(30); } }
	    public DateTime NextAbility;


	    public abstract void DoEffects(BaseCreature creature, Mobile defender, int min, int max);
	    public abstract void Trigger(BaseCreature bc, Mobile defender, int min, int max);
		
	    public CustomAbility()
	    {
	    }

	    public static void CheckTrigger(BaseCreature creature, CustomAbility ability)
	    {

                if (!(creature.Combatant is Mobile))
                    return;

                Mobile combatant = creature.Combatant as Mobile;

			var defender = combatant;

			if(defender == null)
				return;

			if(defender is Mobile)
			{
				ability.Trigger(creature, defender, 0, 0);
			}
	    }

	    protected List<Mobile> _Cooldown;
		
	    public bool IsInCooldown(Mobile m)
	    {
	 	return _Cooldown != null && _Cooldown.Contains(m);
	    }

		
	    public virtual void AddToCooldown(BaseCreature m)
	    {
		if(CooldownDuration != TimeSpan.MinValue)
		{
                    if (_Cooldown == null)
                    	_Cooldown = new List<Mobile>();

		    _Cooldown.Add(m);
		    Timer.DelayCall<Mobile>(CooldownDuration, RemoveFromCooldown, m);
		}
	    }
		
	    public void RemoveFromCooldown(Mobile m)
	    {
		_Cooldown.Remove(m);
	    }

            public static CustomAbility[] Abilities { get { return _Abilities; } }
            private static CustomAbility[] _Abilities;

	    static CustomAbility()
	    {
            	_Abilities = new CustomAbility[16];

            	_Abilities[0] = new FlameStrikeTargeted();
            	_Abilities[1] = new Charge();
		_Abilities[2] = new FlameStrikeAoe();
		_Abilities[3] = new Firebolt();
		_Abilities[4] = new Ambush();
		_Abilities[5] = new Geyser();
		_Abilities[6] = new IcePrison();	
		_Abilities[7] = new WalkingBomb();
		_Abilities[8] = new MeteorStrike();
		_Abilities[9] = new MeteorShower();
		_Abilities[10] = new Thunderstorm();
		_Abilities[11] = new ThrowBoulder();
		_Abilities[12] = new Zap();
		_Abilities[13] = new ToxicRain();
		_Abilities[14] = new ToxicSpores();
		_Abilities[15] = new ImpaleAoe();
	    }

	    public static CustomAbility FlameStrikeTargeted
            {
            	get
            	{
		    return _Abilities[0];
            	}
            }
	    public static CustomAbility Charge
            {
            	get
            	{
                    return _Abilities[1];
            	}
            }
	    public static CustomAbility FlameStrikeAoe
            {
            	get
            	{
		    return _Abilities[2];
            	}
            }
	    public static CustomAbility Firebolt
            {
            	get
            	{
                    return _Abilities[3];
            	}
            }
	    public static CustomAbility Ambush
            {
            	get
            	{
                    return _Abilities[4];
            	}
            }

	    public static CustomAbility Geyser
            {
            	get
            	{
                    return _Abilities[5];
            	}
            }

	    public static CustomAbility IcePrison
            {
            	get
            	{
                    return _Abilities[6];
            	}
            }

	    public static CustomAbility WalkingBomb
            {
            	get
            	{
                    return _Abilities[7];
            	}
            }

	    public static CustomAbility MeteorStrike
            {
            	get
            	{
                    return _Abilities[8];
            	}
            }

	    public static CustomAbility MeteorShower
            {
            	get
            	{
                    return _Abilities[9];
            	}
            }
	    public static CustomAbility Thunderstorm
            {
            	get
            	{
                    return _Abilities[10];
            	}
            }

	    public static CustomAbility ThrowBoulder
            {
            	get
            	{
                    return _Abilities[11];
            	}
            }

	    public static CustomAbility Zap
            {
            	get
            	{
                    return _Abilities[12];
            	}
            }

	    public static CustomAbility ToxicRain
            {
            	get
            	{
                    return _Abilities[13];
            	}
            }
	    public static CustomAbility ToxicSpores
            {
            	get
            	{
                    return _Abilities[14];
            	}
            }

	    public static CustomAbility ImpaleAoe
            {
            	get
            	{
                    return _Abilities[14];
            	}
            }
	}

	public class FlameStrikeTargeted : CustomAbility
	{
	    public override double TriggerChance { get { return 0.5; } }
            public override int MaxRange { get { return 12; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(15); } }

	    /* usage example: 
	    FlameStrikeTargeted fst = new FlameStrikeTargeted();
	    public override void OnThink()
	    {
	        base.OnThink();

		fst.SetDamage(14, 21); // optional to change the default damage
    		fst.Type = FlameStrikeTargeted.StrikeType.Poison; //elemental type poison
		CustomAbility.CheckTrigger(this, fst); //CustomAbility.CheckTrigger(caster, ability);

	    }
	    */

	    public enum StrikeType
	    {
		Fire,
		Ice,
		Poison,
		Energy,
		Water,
		Steam,
		Necrotic,
		Holy
	    }

	    private StrikeType m_Type = StrikeType.Fire;
	    private int hue, rm, phy, fir, col, poi, ene;

	    public StrikeType Type
	    {
		get { return m_Type; }
		set { m_Type = value; }
	    }

	    public FlameStrikeTargeted()
	    {
	    }

	    public FlameStrikeTargeted(int min, int max, StrikeType type)
	    {
		m_Min = min;
		m_Max = max;
		m_Type = type;
	    }

	    private int m_Min = 0;
	    private int m_Max = 0;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }


	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}


		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {

		switch(Type)
		{
		    case StrikeType.Fire: hue = 0; rm = 0;
		    phy = 0; fir = 100; col = 0; poi = 0; ene = 0; break;
		    case StrikeType.Ice: hue = 1151; rm = 0;
		    phy = 0; fir = 0; col = 100; poi = 0; ene = 0; break;
		    case StrikeType.Poison: hue = 1366; rm = 0;
		    phy = 0; fir = 0; col = 0; poi = 100; ene = 0; break;
		    case StrikeType.Energy: hue = 1169; rm = 7;
		    phy = 0; fir = 0; col = 0; poi = 0; ene = 100; break;
		    case StrikeType.Water: hue = 1365; rm = 0;
		    phy = 50; fir = 0; col = 50; poi = 0; ene = 0; break;
		    case StrikeType.Steam: hue = 2103; rm = 7;
		    phy = 50; fir = 50; col = 0; poi = 0; ene = 0; break;
		    case StrikeType.Necrotic: hue = 1174; rm = 7;
		    phy = 50; fir = 0; col = 0; poi = 50; ene = 0; break;
		    case StrikeType.Holy: hue = 1280; rm = 0;
		    phy = 50; fir = 0; col = 0; poi = 0; ene = 50; break;
		}
		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

            	if (defender == null || defender.Deleted || !defender.Alive || !creature.Alive || !creature.CanBeHarmful(defender))
                    return;

		defender.FixedParticles(0x3709, 10, 30, 5052, hue, rm, EffectLayer.LeftFoot); //7
            	defender.PlaySound(0x208);
	    	creature.Frozen = true;

	    	Timer.DelayCall(TimeSpan.FromSeconds(1), () => 
	    	{
		    int d = Utility.RandomMinMax(min, max); //Utility.RandomMinMax(45, 48);

            	    AOS.Damage(defender, d, phy, fir, col, poi, ene);
	    	    creature.Frozen = false;
	    	});

	    }
	}

	public class Charge : CustomAbility
	{

	    public override int MaxRange{ get { return 12; } }
	    public override double TriggerChance { get { return 1.0; } }
	    public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(30); } }


	    public Charge()
	    {
	    }


	    //default damage
	    private int m_Min = 12;
	    private int m_Max = 18;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {
	    	creature.Hidden = true;
	    	creature.Frozen = true;

	    	Misc.Geometry.Line2D(creature.Location, defender.Location, creature.Map, (pnt, map) =>
	    	{
		    Effects.SendLocationParticles(EffectItem.Create(pnt, map, EffectItem.DefaultDuration), 0x3728, 10, 30, 5052);
	    	});




	    	Timer.DelayCall(TimeSpan.FromSeconds(creature.GetDistanceToSqrt(defender) / 20.0), () => 
	    	{
            	    creature.MoveToWorld(defender.Location, defender.Map);

		    int d = Utility.RandomMinMax(min, max);

           	    AOS.Damage(defender, creature, d, 100, 0, 0, 0, 0);

	    	    creature.Hidden = false;
		    creature.Frozen = false;

                    creature.ControlOrder = OrderType.Attack;
                    creature.Combatant = defender;
                    creature.Warmode = true;

	    	});
	    }
	}

	public class FlameStrikeAoe : CustomAbility
	{
	    public override double TriggerChance { get { return 0.2; } }
            public override int MaxRange { get { return 5; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(10); } }

	    /* usage example: 
	    FlameStrikeAoe fsa = new FlameStrikeAoe();
	    public override void OnThink()
	    {
	        base.OnThink();

		fsa.SetDamage(14, 21); // optional to change the default damage
    		fsa.Type = FlameStrikeAoe.StrikeType.Ice; //elemental type ice
		CustomAbility.CheckTrigger(this, fsa); //CustomAbility.CheckTrigger(caster, ability);

	    }
	    */

	    public enum StrikeType
	    {
		Fire,
		Ice,
		Poison,
		Energy,
		Water,
		Steam,
		Necrotic,
		Holy
	    }

	    private StrikeType m_Type = StrikeType.Fire;
	    private int hue, rm, phy, fir, col, poi, ene;

	    private int m_Range = 5;

	    public StrikeType Type
	    {
		get { return m_Type; }
		set { m_Type = value; }
	    }

	    public int Range
	    {
		get { return m_Range; }
		set { m_Range = value; }
	    }

	    public FlameStrikeAoe()
	    {
	    }


	    //default damage
	    private int m_Min = 12;
	    private int m_Max = 16;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(m_Range == null)
			m_Range = MaxRange;

		if(creature.InRange(defender.Location, m_Range) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {    

		switch(Type)
		{
		    case StrikeType.Fire: hue = 0; rm = 0;
		    phy = 0; fir = 100; col = 0; poi = 0; ene = 0; break;
		    case StrikeType.Ice: hue = 1151; rm = 0;
		    phy = 0; fir = 0; col = 100; poi = 0; ene = 0; break;
		    case StrikeType.Poison: hue = 1366; rm = 0;
		    phy = 0; fir = 0; col = 0; poi = 100; ene = 0; break;
		    case StrikeType.Energy: hue = 1169; rm = 7;
		    phy = 0; fir = 0; col = 0; poi = 0; ene = 100; break;
		    case StrikeType.Water: hue = 1365; rm = 0;
		    phy = 50; fir = 0; col = 50; poi = 0; ene = 0; break;
		    case StrikeType.Steam: hue = 2103; rm = 7;
		    phy = 50; fir = 50; col = 0; poi = 0; ene = 0; break;
		    case StrikeType.Necrotic: hue = 1174; rm = 7;
		    phy = 50; fir = 0; col = 0; poi = 50; ene = 0; break;
		    case StrikeType.Holy: hue = 1280; rm = 0;
		    phy = 50; fir = 0; col = 0; poi = 0; ene = 50; break;
		}

		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

		if (!creature.Alive || creature.Map == null)
		return;

		Effects.PlaySound(creature.Location, creature.Map, 0x349);

		for (int i = 0; i < m_Range; i++)
		{
                	Misc.Geometry.Circle2D(creature.Location, creature.Map, i, (pnt, map) =>
                	{
            			Effects.SendLocationEffect(EffectItem.Create(pnt, map, EffectItem.DefaultDuration), creature.Map, 0x3709, 30, 10, hue, rm);
                	});
            	}

            	IPooledEnumerable eable = creature.GetMobilesInRange(m_Range);

            	foreach (Mobile m in eable)
            	{
                	if ((m is PlayerMobile || (m is BaseCreature && ((BaseCreature)m).GetMaster() is PlayerMobile)) && creature.CanBeHarmful(m))
			{
                    	    Timer.DelayCall(TimeSpan.FromSeconds(0.75), () => 
			    {
		    		int d = Utility.RandomMinMax(min, max);
            	    		AOS.Damage(m, d, phy, fir, col, poi, ene);
	    		    });
			}
            	}

            	eable.Free();
	    }
	}

	public class Firebolt : CustomAbility
	{
	    public override double TriggerChance { get { return 0.1; } }
            public override int MaxRange { get { return 12; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(5); } }

	    /* usage example: 
	    Firebolt fb = new Firebolt();
	    public override void OnThink()
	    {
	        base.OnThink();

		fb.SetDamage(14, 21); // optional to change the default damage
    		fb.Type = Firebolt.BoltType.Ice; //elemental type ice
		CustomAbility.CheckTrigger(this, fb); //CustomAbility.CheckTrigger(caster, ability);

	    }
	    */

	    public enum BoltType
	    {
		Fire,
		Ice,
		Poison,
		Energy,
		Water,
		Steam,
		Necrotic,
		Holy
	    }

	    private BoltType m_Type = BoltType.Fire;
	    private int hue, rm, phy, fir, col, poi, ene;

	    public BoltType Type
	    {
		get { return m_Type; }
		set { m_Type = value; }
	    }

	    public Firebolt()
	    {
	    }

	    //default damage
	    private int m_Min = 8;
	    private int m_Max = 12;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    { 

		switch(Type)
		{
		    case BoltType.Fire: hue = 0; rm = 0;
		    phy = 0; fir = 100; col = 0; poi = 0; ene = 0; break;
		    case BoltType.Ice: hue = 1151; rm = 0;
		    phy = 0; fir = 0; col = 100; poi = 0; ene = 0; break;
		    case BoltType.Poison: hue = 1366; rm = 0;
		    phy = 0; fir = 0; col = 0; poi = 100; ene = 0; break;
		    case BoltType.Energy: hue = 1169; rm = 7;
		    phy = 0; fir = 0; col = 0; poi = 0; ene = 100; break;
		    case BoltType.Water: hue = 1365; rm = 0;
		    phy = 50; fir = 0; col = 50; poi = 0; ene = 0; break;
		    case BoltType.Steam: hue = 2103; rm = 7;
		    phy = 50; fir = 50; col = 0; poi = 0; ene = 0; break;
		    case BoltType.Necrotic: hue = 1174; rm = 7;
		    phy = 50; fir = 0; col = 0; poi = 50; ene = 0; break;
		    case BoltType.Holy: hue = 1280; rm = 0;
		    phy = 50; fir = 0; col = 0; poi = 0; ene = 50; break;
		}

		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

             	if (defender == null || defender.Deleted || !defender.Alive || !creature.Alive || !creature.CanBeHarmful(defender))
                    return;

            	defender.PlaySound(0x15E);
            	creature.MovingEffect(defender, 0x36D4, 7, 0, false, true, hue, 0);
	    	creature.Frozen = true;

                Timer.DelayCall(TimeSpan.FromSeconds(creature.GetDistanceToSqrt(defender) / 5.0), () => 
		{
		    int d = Utility.RandomMinMax(min, max);
		    AOS.Damage(defender, d, phy, fir, col, poi, ene);
	    	    creature.Frozen = false;
	    	});


	    }	    
	}

	public class Ambush : CustomAbility
	{

	    public override int MaxRange{ get { return 12; } }
	    public override double TriggerChance { get { return 0.5; } }
	    public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(30); } }

	    public Ambush()
	    {
	    }

	    /* usage example: 
	    Ambush ambush = new Ambush();
	    public override void OnThink()
	    {
	        base.OnThink();

		ambush.SetDamage(8, 14); // optional to change the default damage
		CustomAbility.CheckTrigger(this, ambush); // CustomAbility.CheckTrigger(caster, ability)

	    }
	    */

	    //default damage
	    private int m_Min = 12;
	    private int m_Max = 24;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {
		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {
	    	creature.Hidden = true;
	    	creature.Frozen = true;
		creature.Blessed = true;


		Effects.SendLocationParticles(EffectItem.Create(creature.Location, creature.Map, EffectItem.DefaultDuration), 0x3728, 10, 30, 5052);


	    	Timer.DelayCall(TimeSpan.FromSeconds(Utility.RandomMinMax(3, 6)), () => 
	    	{
            	    creature.MoveToWorld(defender.Location, defender.Map);

		    int d = Utility.RandomMinMax(min, max);

           	    AOS.Damage(defender, creature, d, 100, 0, 0, 0, 0);

	    	    creature.Hidden = false;
		    creature.Frozen = false;
		    creature.Blessed = false;

                    creature.ControlOrder = OrderType.Attack;
                    creature.Combatant = defender;
                    creature.Warmode = true;

	    	});
	    }
	}

	public class Geyser : CustomAbility
	{
	    public override double TriggerChance { get { return 0.5; } }
            public override int MaxRange { get { return 10; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(10); } }

	    public enum GeyserType
	    {
		Fire,
		Ice,
		Poison,
		Energy,
		Water,
		Steam,
		Necrotic,
		Holy
	    }

	    private GeyserType m_Type = GeyserType.Water;
	    private int hue, rm, phy, fir, col, poi, ene;
	    private int soundOne = 0x011;
	    private int soundTwo = 0x026;
	    private string msg = "";

	    public string Message
	    {
		get { return msg; }
		set { msg = value; }
	    }

	    public GeyserType Type
	    {
		get { return m_Type; }
		set { m_Type = value; }
	    }

	    public Geyser()
	    {
	    }

	    /* usage example: 
	    Geyser geyser = new Geyser();
	    public override void OnThink()
	    {
	        base.OnThink();

		geyser.SetDamage(8, 14); // optional to change the default damage
		CustomAbility.CheckTrigger(this, geyser); // CustomAbility.CheckTrigger(caster, ability)

	    }
	    */


	    //default damage
	    private int m_Min = 18;
	    private int m_Max = 24;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {

		switch(Type)
		{
		    case GeyserType.Fire: hue = 1359; rm = 0;
		    phy = 0; fir = 100; col = 0; poi = 0; ene = 0; soundOne = 840; soundTwo = 0x208; break;
		    case GeyserType.Ice: hue = 1151; rm = 0;
		    phy = 0; fir = 0; col = 100; poi = 0; ene = 0; break;
		    case GeyserType.Poison: hue = 1366; rm = 0;
		    phy = 0; fir = 0; col = 0; poi = 100; ene = 0; break;
		    case GeyserType.Energy: hue = 1169; rm = 7;
		    phy = 0; fir = 0; col = 0; poi = 0; ene = 100; soundOne = 840; soundTwo = 0x208; break;
		    case GeyserType.Water: hue = 1365; rm = 0;
		    phy = 50; fir = 0; col = 50; poi = 0; ene = 0; break;
		    case GeyserType.Steam: hue = 1360; rm = 0;
		    phy = 50; fir = 50; col = 0; poi = 0; ene = 0; soundOne = 840; soundTwo = 0x208; break;
		    case GeyserType.Necrotic: hue = 1174; rm = 7;
		    phy = 50; fir = 0; col = 0; poi = 50; ene = 0; soundOne = 840; soundTwo = 0x208; break;
		    case GeyserType.Holy: hue = 1280; rm = 0;
		    phy = 50; fir = 0; col = 0; poi = 0; ene = 50; soundOne = 840; soundTwo = 0x208; break;
		}

            	if (defender == null || defender.Deleted || !defender.Alive || !creature.Alive || !creature.CanBeHarmful(defender))
                    return;

		Point3D p = defender.Location;
		Point3D s = defender.Location;
		s.X = defender.X+1;
		s.Y = defender.Y+1;
		s.Z = defender.Z;
		
            	Effects.SendLocationEffect(EffectItem.Create(s, defender.Map, EffectItem.DefaultDuration), creature.Map, 0x3789, 90, 10, hue, rm);


		Effects.PlaySound(defender.Location, defender.Map, soundOne);
	    	creature.Frozen = true;

	    	Timer.DelayCall(TimeSpan.FromSeconds(4), () => 
	    	{
		    int d;

		    if(msg == "")
			msg = "The geyser erupts beneath you!";

		    if((defender.X == p.X) && (defender.Y == p.Y))
		    {
			d = Utility.RandomMinMax(min, max);
			defender.SendMessage(msg);
			Effects.PlaySound(defender.Location, defender.Map, soundTwo);
		    }
		    else
			d = 0;

            	    Effects.SendLocationEffect(EffectItem.Create(p, defender.Map, EffectItem.DefaultDuration), creature.Map, 0x3709, 30, 10, hue, rm);
            	    AOS.Damage(defender, d, phy, fir, col, poi, ene);
	    	    creature.Frozen = false;
	    	});

	    }
	}

	public class IcePrison : CustomAbility
	{

	    public override int MaxRange{ get { return 12; } }
	    public override double TriggerChance { get { return 0.2; } }
	    public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(30); } }

	    public IcePrison()
	    {
	    }

	    /* usage example: 
	    IcePrison ip = new IcePrison();
	    public override void OnThink()
	    {
	        base.OnThink();

		ip.SetDamage(4, 8); // ice prison does 0 damage by default
		CustomAbility.CheckTrigger(this, ip); // CustomAbility.CheckTrigger(caster, ability)

	    }
	    */

	    //default damage
	    private int m_Min = 0;
	    private int m_Max = 0;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }


	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {
		min = m_Min;
		max = m_Max;


		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {
		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

	    	defender.Frozen = true;
		defender.Hidden = true;
		defender.Blessed = true;

		Effects.PlaySound(defender.Location, defender.Map, 0x64F);
		Effects.PlaySound(defender.Location, defender.Map, 0x10D);

		for (int i = 0; i < 2; i++)
		{
                	Misc.Geometry.Circle2D(defender.Location, creature.Map, i, (pnt, map) =>
                	{
            			Effects.SendLocationEffect(EffectItem.Create(pnt, map, EffectItem.DefaultDuration), creature.Map, 0x35F7, 150, 10, 0xAA8, 0);
                	});
            	}



	    	Timer.DelayCall(TimeSpan.FromSeconds(8), () => 
	    	{
		    int d = Utility.RandomMinMax(min, max);

           	    AOS.Damage(defender, creature, d, 100, 0, 0, 0, 0);

		    defender.Frozen = false;
		    defender.Hidden = false;
		    defender.Blessed = false;
	    	});
	    }
	}

	public class WalkingBomb : CustomAbility
	{

	    public override int MaxRange{ get { return 10; } }
	    public override double TriggerChance { get { return 0.2; } }
	    public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(20); } }

	    public WalkingBomb()
	    {
	    }

	    /* usage example: 
	    WalkingBomb wb = new WalkingBomb();
	    public override void OnThink()
	    {
	        base.OnThink();

		wb.SetDamage(8, 14); // optional to change the default damage
		CustomAbility.CheckTrigger(this, wb); // CustomAbility.CheckTrigger(caster, ability)

	    }
	    */


	    //default damage
	    private int m_Min = 12;
	    private int m_Max = 18;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

		int hue = 0x485;

		defender.FixedParticles(0x374A, 10, 90, 5052, hue, 0, EffectLayer.Waist);

	    	Timer.DelayCall(TimeSpan.FromSeconds(5), () => 
	    	{

                    defender.FixedParticles(0x36BD, 20, 10, 5044, hue, 0, EffectLayer.Head);
                    //defender.PlaySound(0x307);
		    Effects.PlaySound(defender.Location, defender.Map, 0x207);

            	    IPooledEnumerable eable = defender.GetMobilesInRange(4);

            	    foreach (Mobile m in eable)
            	    {
                	if ((m is PlayerMobile || (m is BaseCreature && ((BaseCreature)m).GetMaster() is PlayerMobile)) && creature.CanBeHarmful(m))
			{
            		    defender.MovingEffect(m, 0x36D4, 5, 0, false, true, hue, 0);
			    int d = Utility.RandomMinMax(min, max);
			    AOS.Damage(m, d, 20, 20, 20, 20, 20);
			}
            	    }

            	    eable.Free();

	    	});
	    }
	}

	public class MeteorStrike : CustomAbility
	{
	    public override double TriggerChance { get { return 1.0; } }
            public override int MaxRange { get { return 10; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(10); } }

	    private string msg = "";

	    public string Message
	    {
		get { return msg; }
		set { msg = value; }
	    }

	    public MeteorStrike()
	    {
	    }

	    /* usage example: 
	    MeteorStrike ms = new MeteorStrike();
	    public override void OnThink()
	    {
	        base.OnThink();

		ms.SetDamage(5, 10); // optional to change the default damage
		CustomAbility.CheckTrigger(this, ms); // CustomAbility.CheckTrigger(caster, ability)

	    }
	    */


	    //default damage
	    private int m_Min = 18;
	    private int m_Max = 24;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {    

		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

            	if (defender == null || defender.Deleted || !defender.Alive || !creature.Alive || !creature.CanBeHarmful(defender))
                    return;

		Point3D p = defender.Location;
		
		MeteorItem m_Item = new MeteorItem(TimeSpan.FromMinutes(1.0), 8);
		m_Item.Location = p;
		m_Item.Map = defender.Map;


		    if(msg == "")
			msg = "Something in the sky casts a shadow over you...";

		defender.SendMessage(msg);

	    	Timer.DelayCall(TimeSpan.FromSeconds(4), () => 
	    	{
		    int d;
		    Effects.PlaySound(p, defender.Map, 0x160);
		    m_Item.TurnOn();

		    if((defender.X == p.X) && (defender.Y == p.Y))
		    {
			d = Utility.RandomMinMax(min, max);
		    }
		    else
			d = 0;

		    m_Item.Damage = d;

	    	});
	    }

	}

	public class MeteorShower : CustomAbility
	{
	    public override double TriggerChance { get { return 0.1; } }
            public override int MaxRange { get { return 10; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(60); } }

	    public MeteorShower()
	    {
	    }

	    /* usage example: 
	    MeteorShower ms = new MeteorShower();
	    public override void OnThink()
	    {
	        base.OnThink();

		ms.SetDamage(5, 10); // optional to change the default damage
		CustomAbility.CheckTrigger(this, ms); // CustomAbility.CheckTrigger(caster, ability)

	    }
	    */

	    //default damage
	    private int m_Min = 14;
	    private int m_Max = 18;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {
		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

		if (!creature.Alive || creature.Map == null)
		return;

		MeteorShowerItem m_Item = new MeteorShowerItem(TimeSpan.FromSeconds(60), Utility.RandomMinMax(min, max));
		m_Item.Location = defender.Location;
		m_Item.Map = defender.Map;
		m_Item.Range = 6;
		m_Item.Damage = Utility.RandomMinMax(min, max);
	    }
	}

	public class Thunderstorm : CustomAbility
	{
	    public override double TriggerChance { get { return 0.1; } }
            public override int MaxRange { get { return 8; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(8); } }

	    public Thunderstorm()
	    {
	    }

	    /* usage example: 
	    Thunderstorm ts = new Thunderstorm();
	    public override void OnThink()
	    {
	        base.OnThink();

		ts.SetDamage(5, 10); // optional to change the default damage
		CustomAbility.CheckTrigger(this, ts); // CustomAbility.CheckTrigger(caster, ability)

	    }
	    */

	    //default damage
	    private int m_Min = 6;
	    private int m_Max = 10;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }
	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {    
		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

		if (!creature.Alive || creature.Map == null)
		return;

                ArrayList list = new ArrayList();
                IPooledEnumerable eable = creature.GetMobilesInRange(MaxRange);

                foreach (Mobile m in eable)
                {
		if( m != creature)
                      list.Add(m);
                }
                eable.Free();


                for (int i = 0; i < list.Count; ++i)
                {
                    Mobile m = (Mobile)list[i];

                    creature.DoHarmful(m);
		    Effects.PlaySound(defender.Location, defender.Map, 0x5CE);
            	    Effects.SendBoltEffect(m, true, 0);

                    AOS.Damage(m, Utility.RandomMinMax(min, max), 0, 0, 0, 0, 100);

                }
	    }
	}
	public class ThrowBoulder : CustomAbility
	{
	    public override double TriggerChance { get { return 0.2; } }
            public override int MaxRange { get { return 12; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(5); } }

	    /* usage example: 
	    ThrowBoulder tb = new ThrowBoulder();
	    public override void OnThink()
	    {
	        base.OnThink();

		tb.SetDamage(5, 10); // optional to change the default damage
		CustomAbility.CheckTrigger(this, tb); // CustomAbility.CheckTrigger(caster, ability)

	    }
	    */

	    public ThrowBoulder()
	    {
	    }

	    //default damage
	    private int m_Min = 12;
	    private int m_Max = 16;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {

             	if (defender == null || defender.Deleted || !defender.Alive || !creature.Alive || !creature.CanBeHarmful(defender))
                    return;

                int itemID = 0x1363;
		TargetLocationItem m_Item = new TargetLocationItem();
		m_Item.Location = defender.Location;
		m_Item.Map = defender.Map;
            	creature.MovingEffect(m_Item, itemID, 1, 0, true, false, 0, 0);


                Timer.DelayCall(TimeSpan.FromSeconds(creature.GetDistanceToSqrt(m_Item) / 8.0), () => 
		{
	    	    Effects.SendLocationParticles(EffectItem.Create(m_Item.Location, m_Item.Map, EffectItem.DefaultDuration), 0x3728, 10, 20, 5052);

            	    IPooledEnumerable eable = m_Item.GetMobilesInRange(1);

            	    foreach (Mobile m in eable)
            	    {
                	if ((m is PlayerMobile || (m is BaseCreature && ((BaseCreature)m).GetMaster() is PlayerMobile)) && creature.CanBeHarmful(m))
			{
	   		    m.Animate( 22, 5, 1, true, false, 0 );
			    m.Frozen = true;
			    m.SendMessage("You've been hit by an enormous boulder!");

			    Effects.PlaySound(m_Item.Location, defender.Map, 0x308);
			    int d = Utility.RandomMinMax(min, max);
			    AOS.Damage(m, d, 100, 0, 0, 0, 0);

			    Timer.DelayCall(TimeSpan.FromSeconds(2), () =>
			    {
				m.Frozen = false;
			    	m.SendMessage("You have recovered.");
			    });
			}
            	    }

            	    eable.Free();
	    	});

	    }	    
	}

	public class Zap : CustomAbility
	{
	    public override double TriggerChance { get { return 0.2; } }
            public override int MaxRange { get { return 15; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(12); } }

	    /* usage example: 
	    Zap zap = new Zap();
	    public override void OnThink()
	    {
	        base.OnThink();

		zap.SetDamage(5, 10);
		CustomAbility.CheckTrigger(this, zap); // CustomAbility.CheckTrigger(caster, ability)

	    }
	    */

	    public Zap()
	    {
	    }

	    private int m_Min = 8;
	    private int m_Max = 12;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    { 
		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

             	if (defender == null || defender.Deleted || !defender.Alive || !creature.Alive || !creature.CanBeHarmful(defender))
                    return;

            	defender.PlaySound(0x2F4);
            	creature.MovingEffect(defender, 0x3818, 7, 0, false, false, 0, 0);
	    	creature.Frozen = true;

                Timer.DelayCall(TimeSpan.FromSeconds(creature.GetDistanceToSqrt(defender) / 8.0), () => 
		{
		    int d = Utility.RandomMinMax(min, max);
		    AOS.Damage(defender, d, 0, 0, 0, 0, 100);
	    	    creature.Frozen = false;
		    defender.Paralyzed = true;
		    Timer.DelayCall(TimeSpan.FromSeconds(3), () =>
		    {
		        defender.Paralyzed = false;
		    });
	    	});


	    }	    
	}

	public class ToxicRain : CustomAbility
	{
	    public override double TriggerChance { get { return 0.1; } }
            public override int MaxRange { get { return 12; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(15); } }

            private static readonly Dictionary<Mobile, RainTimer> m_RainTable = new Dictionary<Mobile, RainTimer>();

	    public static bool IsRaining(Mobile m)
            {
            	return m_RainTable.ContainsKey(m);
            }


	    /* usage example: 
	    ToxicRain tr = new ToxicRain();
	    public override void OnThink()
	    {
	        base.OnThink();

		CustomAbility.CheckTrigger(this, tr); // CustomAbility.CheckTrigger(caster, ability)
	    }
	    */

	    public ToxicRain()
	    {
	    }

	    private int m_Min = 0;
	    private int m_Max = 0;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    private int hue = 64;

	    public int Hue
	    {
		get { return hue; }
		set { hue = value; }
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {
		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {
		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

                RainTimer timer = null;

		if (!creature.Alive || creature.Map == null)
		return;

		//Effects.PlaySound(defender.Location, defender.Map, 0x5CC); //bees
		Effects.PlaySound(defender.Location, defender.Map, 0x011);
                    //m.PlaySound(0x5CC); 

		Effects.SendLocationParticles(EffectItem.Create(defender.Location, defender.Map, EffectItem.DefaultDuration), 0x9F89, 10, 30, hue, 0, 5052, 0);

/*
		TargetLocationItem m_Item = new TargetLocationItem();
		m_Item.Location = defender.Location;
		m_Item.Map = defender.Map;
            	creature.MovingEffect(m_Item, 0x91B, 1, 0, false, false, 63, 0);
*/

                Timer.DelayCall(TimeSpan.FromSeconds(0.75), () => //creature.GetDistanceToSqrt(m_Item) / 8.0
		{

            	    IPooledEnumerable eable = defender.GetMobilesInRange(1);

            	    foreach (Mobile m in eable)
            	    {
                	if ((m is PlayerMobile || (m is BaseCreature && ((BaseCreature)m).GetMaster() is PlayerMobile)) && creature.CanBeHarmful(m))
			{
			    int d = Utility.RandomMinMax(min, max);
			    AOS.Damage(m, d, 50, 0, 0, 50, 0);

           		    	timer = new RainTimer(creature, m);
           		    	m_RainTable[m] = timer;
           		    	timer.Start();
			}
            	    }

            	    eable.Free();
	    	});
	    }

            public static void DoRain(Mobile m, Mobile from, int damage)
            {
            	if (m.Alive && !m.IsDeadBondedPet)
            	{

                    if (!m.Player)
                        damage *= 2;

		    //rain effect
		    //Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x9F89, 10, 30, hue, 0, 5052, 0);

		    //dmg
		    //m.PlaySound(0x11F); //original sound
		    m.PlaySound(0x011);
                    m.FixedParticles(0x9F89, 10, 30, 5030, 64, 0, EffectLayer.Waist);
                    AOS.Damage(m, from, damage, false, 0, 0, 0, 0, 0, 0, 100, false, false, false);

		    //acid pool on ground
                    AcidPoolItem acid = new AcidPoolItem();
                    //acid.ItemID = 0x9D80;
                    acid.MoveToWorld(m.Location, m.Map);
                }
                else
                {
		    EndRain(m, true);
                }
            }

            public static void EndRain(Mobile m, bool message)
            {
            	Timer t = null;

            	if (m_RainTable.ContainsKey(m))
            	{
		    t = m_RainTable[m];
                    m_RainTable.Remove(m);
		}

            	if (t == null)
                    return;

            	t.Stop();
            	    BuffInfo.RemoveBuff(m, BuffIcon.Bleed);

                if (message)
		    m.SendMessage("You are no longer ToxicRaind."); 
            }

            private class RainTimer : Timer
            {
            	private readonly Mobile m_From;
                private readonly Mobile m_Mobile;
                private int m_Count;
                private int m_MaxCount;

                public RainTimer(Mobile from, Mobile m)
                : base(TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(2.0))
            	{
                    m_From = from;
                    m_Mobile = m;
                    Priority = TimerPriority.TwoFiftyMS;

                    m_MaxCount = 2;
	        }

                protected override void OnTick()
                {
                    if (!m_Mobile.Alive || m_Mobile.Deleted)
                    {
                        EndRain(m_Mobile, false);
                    }
                    else
                    {
                    	int damage = 0;

                         damage = Math.Max(1, Utility.RandomMinMax(5 - m_Count, (5 - m_Count) * 2));

                        DoRain(m_Mobile, m_From, damage);

                        if (++m_Count == m_MaxCount)
                            EndRain(m_Mobile, true);
                    }
            	}
            }
	}

	public class ToxicSpores: CustomAbility
	{
	    public override double TriggerChance { get { return 0.5; } }
            public override int MaxRange { get { return 12; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(35); } }

	    private int m_Range = 5;

	    public int Radius
	    {
		get { return m_Range; }
		set { m_Range = value; }
	    }

	    /* usage example: 
	    ToxicSpores ts = new ToxicSpores();
	    public override void OnThink()
	    {
	        base.OnThink();

                if (!(Combatant is Mobile))
                    return;

                Mobile combatant = Combatant as Mobile;

    		ts.Radius = 3; // alteres the radius of the aoe; default is 5
		ts.Trigger(this, combatant, 10, 15); //between 10-15 damage
	    }
	    */


	    public ToxicSpores()
	    {
	    }

	    private int m_Min = 0;
	    private int m_Max = 0;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(creature.InRange(defender.Location, MaxRange) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {
		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );

            	if (defender == null || defender.Deleted || !defender.Alive || !creature.Alive || !creature.CanBeHarmful(defender))
                    return;

		Point3D p = defender.Location;


		Misc.Geometry.Circle2D(p, defender.Map, m_Range, (pnt, map) =>
		{
		    Effects.SendLocationEffect(EffectItem.Create(pnt, map, EffectItem.DefaultDuration), creature.Map, 0x1126, 30, 10, 0, 0);
		});
	    	creature.Frozen = true;

	    	Timer.DelayCall(TimeSpan.FromSeconds(1), () => 
	    	{

		    int d = Utility.RandomMinMax(min, max);

		    for (int i = 0; i < m_Range; i++)
		    {
                	Misc.Geometry.Circle2D(p, defender.Map, i, (pnt, map) =>
                	{
			    Effects.SendLocationEffect(EffectItem.Create(pnt, map, EffectItem.DefaultDuration), creature.Map, 0x36BD, 30, 10, 63, 0);
		    	    AcidPoolItem m_Item = new AcidPoolItem(creature, TimeSpan.FromSeconds(15), d/2);
		    	    m_Item.Location = pnt;
		    	    m_Item.Map = map;

                	});
            	    }

            	    AOS.Damage(defender, d, 0, 0, 0, 100, 0);
	    	    creature.Frozen = false;
	    	});

	    }
	}

	public class ImpaleAoe : CustomAbility
	{
	    public override double TriggerChance { get { return 0.2; } }
            public override int MaxRange { get { return 5; } }
            public override TimeSpan CooldownDuration { get { return TimeSpan.FromSeconds(25); } }

            private static readonly Dictionary<Mobile, BleedTimer> m_BleedTable = new Dictionary<Mobile, BleedTimer>();

	    public static bool IsBleeding(Mobile m)
            {
            	return m_BleedTable.ContainsKey(m);
            }

	    /* usage example: 
	    FlameStrikeAoe fsa = new FlameStrikeAoe();
	    public override void OnThink()
	    {
	        base.OnThink();

                if (!(Combatant is Mobile))
                    return;

                Mobile combatant = Combatant as Mobile;

    		fsa.Type = FlameStrikeAoe.StrikeType.Poison; //elemental type poison
		fsa.Trigger(this, combatant, 5, 10); //between 5-10 damage
	    }
	    */

	    private int m_Range = 5;
	    private int itemID = Utility.RandomList(0x8E0, 0x8E7, 0x8E1);
	    private int m_Hue = 0;

	    public int Range
	    {
		get { return m_Range; }
		set { m_Range = value; }
	    }

	    public int ItemID
	    {
		get { return itemID; }
		set { itemID = value; }
	    }

	    public int Hue
	    {
		get { return m_Hue; }
		set { m_Hue = value; }
	    }

	    public ImpaleAoe()
	    {
	    }

	    private int m_Min = 0;
	    private int m_Max = 0;

	    public void SetDamage(int min, int max)
	    {
		m_Min = min;
		m_Max = max;
	    }

	    public override void Trigger(BaseCreature creature, Mobile defender, int min, int max)
	    {

		if(min == 0 && max == 0)
		{
		    min = m_Min;
		    max = m_Max;
		}

		if(m_Range == null)
			m_Range = MaxRange;

		if(creature.InRange(defender.Location, m_Range) && TriggerChance >= Utility.RandomDouble() && !IsInCooldown(creature))
		{
		    DoEffects(creature, defender, min, max);
                    AddToCooldown(creature);
		}
	    }

	    public override void DoEffects(BaseCreature creature, Mobile defender, int min, int max)
	    {    
		if( creature.Body == 0x191 || creature.Body == 0x190)
		    creature.Animate( 22, 5, 1, true, false, 0 );
			

                BleedTimer timer = null;

		if (!creature.Alive || creature.Map == null)
		return;

		Effects.PlaySound(defender.Location, defender.Map, 0x21D);

            	IPooledEnumerable eable = creature.GetMobilesInRange(m_Range);

            	foreach (Mobile m in eable)
            	{
                	if ((m is PlayerMobile || (m is BaseCreature && ((BaseCreature)m).GetMaster() is PlayerMobile)) && creature.CanBeHarmful(m))
			{


			    Effects.SendLocationEffect(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), m.Map, itemID, 150, 10, 0, 0); //0xAA8 is ice

                    	    Timer.DelayCall(TimeSpan.FromSeconds(0.75), () => 
			    {
		    		int d = Utility.RandomMinMax(min, max);
            	    		AOS.Damage(m, d, 100, 0, 0, 0, 0);

            			    BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Bleed, 1075829, 1075830, TimeSpan.FromSeconds(10), m, String.Format("{0}\t{1}\t{2}", "1", "10", "2")));

            			    timer = new BleedTimer(creature, m);
            			    m_BleedTable[m] = timer;
            			    timer.Start();

            			    m.SendLocalizedMessage(1060160); // You are bleeding!

            			    if (m is PlayerMobile)
            			    {
				    	m.LocalOverheadMessage(MessageType.Regular, 0x21, 1060757); // You are bleeding profusely
                		      	m.NonlocalOverheadMessage(MessageType.Regular, 0x21, 1060758, m.Name); // ~1_NAME~ is bleeding profusely    
            			    }
           			         m.FixedParticles(0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist);

	    		    });
			}
            	}

            	eable.Free();
	    }

            public static void DoBleed(Mobile m, Mobile from, int damage)
            {
            	if (m.Alive && !m.IsDeadBondedPet)
            	{
                    if (!m.Player)
                        damage *= 2;

                    m.PlaySound(0x133);
                    AOS.Damage(m, from, damage, false, 0, 0, 0, 0, 0, 0, 100, false, false, false);

                    Blood blood = new Blood();
                    blood.ItemID = Utility.Random(0x122A, 5);
                    blood.MoveToWorld(m.Location, m.Map);
                }
                else
                {
		    EndBleed(m, false);
                }
            }

            public static void EndBleed(Mobile m, bool message)
            {
            	Timer t = null;

            	if (m_BleedTable.ContainsKey(m))
            	{
		    t = m_BleedTable[m];
                    m_BleedTable.Remove(m);
            	}

            	if (t == null)
                    return;

            	t.Stop();
            	    BuffInfo.RemoveBuff(m, BuffIcon.Bleed);

                if (message)
		    m.SendLocalizedMessage(1060167); // The bleeding wounds have healed, you are no longer bleeding!
            }

            private class BleedTimer : Timer
            {
            	private readonly Mobile m_From;
                private readonly Mobile m_Mobile;
                private int m_Count;
                private int m_MaxCount;

                public BleedTimer(Mobile from, Mobile m)
                : base(TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(2.0))
            	{
                    m_From = from;
                    m_Mobile = m;
                    Priority = TimerPriority.TwoFiftyMS;

                    m_MaxCount = 5;
	        }

                protected override void OnTick()
                {
                    if (!m_Mobile.Alive || m_Mobile.Deleted)
                    {
                        EndBleed(m_Mobile, true);
                    }
                    else
                    {
                    	int damage = 0;

                        damage = Math.Max(1, Utility.RandomMinMax(5 - m_Count, (5 - m_Count) * 2));

                        DoBleed(m_Mobile, m_From, damage);

                        if (++m_Count == m_MaxCount)
                            EndBleed(m_Mobile, true);
                    }
            	}
            }
	}
}