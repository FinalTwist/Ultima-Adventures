using System;
using System.Collections;
using Server.Mobiles;

namespace Server.Items
{
    public class AcidPoolItem : CustomAbilityItem
    {

	private Timer m_Timer;
	private DateTime m_End;
	private TimeSpan m_Duration;
	private int m_Damage;
	private Mobile Caster;
	private int hue = 63;
	//private int id = Utility.RandomList( 0x122A, 0x122B, 0x122C, 0x122D, 0x122E, 0x122F, 0x9D80 );

	private bool m_Active = true;

	[CommandProperty(AccessLevel.GameMaster)]
	public bool Active
	{
	    get { return m_Active; }
	    set { m_Active = value; }
	}

	[CommandProperty(AccessLevel.GameMaster)]
	public int Damage
	{
	    get { return m_Damage; }
	    set { m_Damage = value; }
	}

	[CommandProperty(AccessLevel.GameMaster)]
    	public DateTime End
    	{
    	    get { return m_End; }
    	    set { m_End = value; }
	}

        [Constructable]
	public AcidPoolItem()
	    : base (Utility.RandomList( 0x122A, 0x122B, 0x122C, 0x122D, 0x122E, 0x122F, 0x9D80 ))
	{
    	    Movable = false;
	    Name = "Acid pool";
	    m_Damage = 4;
    	    m_End = DateTime.UtcNow + TimeSpan.FromSeconds(30);
	    Hue = hue;
    	    m_Timer = new InternalTimer(this);
    	    m_Timer.Start();
	}

        [Constructable]
	public AcidPoolItem(Mobile caster, TimeSpan duration, int damage)
	    : base (Utility.RandomList( 0x122A, 0x122B, 0x122C, 0x122D, 0x122E, 0x122F, 0x9D80 ))
	{
    	    Movable = false;
	    Name = "Acid pool";
	    Hue = hue;
	    Caster = caster;
	    m_Damage = damage;
    	    m_End = DateTime.UtcNow + duration;

    	    m_Timer = new InternalTimer(this);
    	    m_Timer.Start();
	}

	public AcidPoolItem(Serial serial)
	    : base(serial)
	{
	}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
	    writer.Write(m_Damage);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
	    m_Damage = reader.ReadInt();

	    if (version < 2)
		m_Damage = 2;

        }

	public override bool OnMoveOver(Mobile m)
	{
	    if (Active == true && (m is PlayerMobile || (m is BaseCreature && ((BaseCreature)m).GetMaster() is PlayerMobile)))
	    {
		int damage = m_Damage;

		if (!Core.AOS && m.CheckSkill(SkillName.MagicResist, 0.0, 30.0))
		{
		    damage = 1;

		    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
		}

		if(Utility.RandomDouble() < 0.25 && Caster != null)
			m.ApplyPoison(Caster, Poison.GetPoison(2));

		AOS.Damage(m, damage, 0, 0, 0, 100, 0);
		m.PlaySound(0x22A);

	    }

	    return true;
	}

	private class InternalTimer : Timer
	{
            private static readonly Queue m_Queue = new Queue();
	    private readonly AcidPoolItem m_Item;
	
	    public InternalTimer(AcidPoolItem item)
		: base(TimeSpan.FromMilliseconds(500), TimeSpan.FromSeconds(1.0))
	    {
		m_Item = item;
	    }

	    protected override void OnTick()
	    {
		if (m_Item.Deleted)
                        return;

		if (DateTime.UtcNow > m_Item.End)
		{
		    m_Item.Delete();
		    Stop();
		}
		else
		{
		    Map map = m_Item.Map;

		    if(map != null)
		    {
			IPooledEnumerable eable = m_Item.GetMobilesInRange(0);

			foreach (Mobile m in eable)
			{
			    if(m is PlayerMobile || (m is BaseCreature && ((BaseCreature)m).GetMaster() is PlayerMobile))
			    {
                                if ((m.Z + 16) > m_Item.Z && (m_Item.Z + 12) > m.Z )
                                    m_Queue.Enqueue(m);
			    }
			}

			eable.Free();

			while (m_Queue.Count > 0)
			{
			    Mobile m = (Mobile)m_Queue.Dequeue();

			    int damage = m_Item.Damage;

			    if (!Core.AOS && m.CheckSkill(SkillName.MagicResist, 0.0, 30.0))
			    {
				damage = 1;

				m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
			    }

			    AOS.Damage(m, damage, 0, 0, 0, 100, 0);
			    m.PlaySound(0x22A);
			}
		    }
		}
	    }
	}
    }
}