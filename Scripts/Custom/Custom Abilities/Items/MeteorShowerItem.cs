using System;
using System.Collections;
namespace Server.Items
{
    public class MeteorShowerItem : CustomAbilityItem
    {
	private Timer m_Timer;
	private TimeSpan m_Duration;
	private TimeSpan m_Frequency;
	private DateTime m_NextCast;
	private DateTime m_End;
	private MeteorItem m_Item;
	private Point3D p;
	private int m_Damage;
	private int m_Range;

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
	public TimeSpan Frequency
    	{
    	    get { return m_Frequency; }
    	    set { m_Frequency = value; }
	}

	[CommandProperty(AccessLevel.GameMaster)]
    	public DateTime NextCast
    	{
    	    get { return m_NextCast; }
    	    set { m_NextCast = value; }
	}

	[CommandProperty(AccessLevel.GameMaster)]
	public int Range
	{
    	    get { return m_Range; }
    	    set { m_Range = value; }
	}

        [CommandProperty(AccessLevel.GameMaster)]
        public override TimeSpan DecayDelay { get { return m_Duration; } }
	

        [Constructable]
	public MeteorShowerItem()
	    :base(0x1BC3)
	{
	    Visible = false;
	    m_Duration = TimeSpan.FromSeconds(90);
	    m_Frequency = TimeSpan.FromSeconds(4);
	    m_NextCast = DateTime.UtcNow + m_Frequency;
	    m_Damage = Utility.RandomMinMax(8, 28);
	    m_End = DateTime.UtcNow + m_Duration;
	    m_Range = 8;

    	    m_Timer = new InternalTimer(this);
    	    m_Timer.Start();
	}

	public MeteorShowerItem(TimeSpan duration, int damage)
	    :base(0x1BC3)
	{
	    Visible = false;
	    m_Duration = duration;
	    m_Frequency = TimeSpan.FromSeconds(4);
	    m_NextCast = DateTime.UtcNow + m_Frequency;
	    m_Damage = damage;
	    m_End = DateTime.UtcNow + duration;
	    m_Range = 8;

    	    m_Timer = new InternalTimer(this);
    	    m_Timer.Start();
	}

	public MeteorShowerItem(Serial serial)
	    :base(serial)
	{
	}

	public void Cast()
	{

	    p = this.Location;
	    p.X = this.X + Utility.RandomMinMax(-m_Range, m_Range);
	    p.Y = this.Y + Utility.RandomMinMax(-m_Range, m_Range);
	    p.Z = this.Z;
		
	    m_Item = new MeteorItem();
	    m_Item.Location = p;
	    m_Item.Map = Map;

	    m_NextCast = DateTime.UtcNow + m_Frequency;
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
	    private readonly MeteorShowerItem m_Item;

	    public InternalTimer(MeteorShowerItem item)
		:base(TimeSpan.FromMilliseconds(500), TimeSpan.FromSeconds(1.0))
	    {
		m_Item = item;
	    }

	    protected override void OnTick()
	    {
		if(m_Item.Deleted)
		    return;

		if(DateTime.UtcNow > m_Item.NextCast)
		{
		    m_Item.Cast();
		}

		if(DateTime.UtcNow > m_Item.End)
		{
		    m_Item.Delete();
		    Stop();
		}

		
	    }
	}

    }
}