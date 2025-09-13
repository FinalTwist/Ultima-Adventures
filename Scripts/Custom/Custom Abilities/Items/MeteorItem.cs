using System;
using System.Collections;
namespace Server.Items
{
   public class MeteorItem : CustomAbilityItem
    {
	private Timer m_Timer;
	private TimeSpan m_Duration;
	private int m_Damage;
	private DateTime m_End;
	private DateTime m_Impact;
	private FireItem m_Item;
	private bool m_Active = true;

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

	[CommandProperty(AccessLevel.GameMaster)]
	public DateTime Impact
	{
	    get { return m_Impact; }
	    set { m_Impact = value; }
	}

        [Constructable]
        public MeteorItem()
            : base(0x5728)
        {
	    Name = "shadow";
	    Movable = false;
	    Hue = 1;
            Weight = 1.0;

	    m_Damage = 10;
	    m_Duration = TimeSpan.FromMinutes(1);
	    m_End = DateTime.UtcNow + TimeSpan.FromSeconds(3);
	    m_Impact = DateTime.UtcNow + TimeSpan.FromSeconds(2.5);

    	    m_Timer = new InternalTimer(this);
    	    m_Timer.Start();

        }


        [Constructable]
        public MeteorItem(TimeSpan duration, int damage)
            : base(0x5728)
        {
	    Name = "shadow";
	    Movable = false;
	    Hue = 1;
            Weight = 1.0;

	    m_Damage = damage;
	    m_Duration = duration;
	    m_End = DateTime.UtcNow + TimeSpan.FromSeconds(3);
	    m_Impact = DateTime.UtcNow + TimeSpan.FromSeconds(2);

    	    m_Timer = new InternalTimer(this);
    	    m_Timer.Start();

        }
        public MeteorItem(Serial serial)
            : base(serial)
        {
        }

	public void TurnOn()
	{
	    if(m_Item == null)
	    {
		this.Visible = false;
		m_Item = new FireItem(m_Duration, m_Damage);
                m_Item.Location = new Point3D(X, Y, Z+1);
		m_Item.Map = Map;
	    }

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

        }

	private class InternalTimer : Timer
	{
	    private readonly MeteorItem m_Item;

	    public InternalTimer(MeteorItem item)
		:base(TimeSpan.FromMilliseconds(500), TimeSpan.FromSeconds(1.0))
	    {
		m_Item = item;
	    }

	    protected override void OnTick()
	    {
		if(m_Item.Deleted)
		    return;

		if(DateTime.UtcNow > m_Item.Impact && m_Item.Active == true)
		{
                    Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(m_Item.X - 6, m_Item.Y - 6, m_Item.Z + 15), m_Item.Map), m_Item, 0x36D4, 8, 0, false, false, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
                    Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(m_Item.X - 4, m_Item.Y - 6, m_Item.Z + 15), m_Item.Map), m_Item, 0x36D4, 8, 0, false, false, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
                    Effects.SendMovingParticles(new Entity(Serial.Zero, new Point3D(m_Item.X - 6, m_Item.Y - 4, m_Item.Z + 15), m_Item.Map), m_Item, 0x36D4, 8, 0, false, false, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
		    Effects.PlaySound(m_Item.Location, m_Item.Map, 0x160);

		    IPooledEnumerable eable = m_Item.GetMobilesInRange(0);

		    foreach (Mobile m in eable)
		    {
                            if ((m.Z + 16) > m_Item.Z && (m_Item.Z + 12) > m.Z )
			    {
            	    		AOS.Damage(m, m_Item.Damage, 80, 20, 0, 0, 0);
			    }
		    }

		    eable.Free();

		    m_Item.Active = false;
		}

		if(DateTime.UtcNow > m_Item.End)
		{
		    m_Item.TurnOn();
		    m_Item.Delete();
		    Stop();
		}
	    }
	}
    }
}