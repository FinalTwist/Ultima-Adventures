using System;
using System.Collections;
namespace Server.Items
{
   public class TargetLocationItem : CustomAbilityItem
    {
	private Timer m_Timer;
	private DateTime m_End;

        [CommandProperty(AccessLevel.GameMaster)]
        public override TimeSpan DecayDelay { get { return TimeSpan.FromSeconds(4); } }

	[CommandProperty(AccessLevel.GameMaster)]
	public DateTime End
	{
	    get { return m_End; }
	    set { m_End = value; }
	}

        [Constructable]
        public TargetLocationItem ()
            : base(0x1BC3)
        {
	    Movable = false;
	    Hue = 1;
            Weight = 1.0;
	    Visible = false;

	    m_End = DateTime.UtcNow + TimeSpan.FromSeconds(4);

    	    m_Timer = new InternalTimer(this);
    	    m_Timer.Start();

        }

        public TargetLocationItem (Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

        }

	private class InternalTimer : Timer
	{
	    private readonly TargetLocationItem  m_Item;

	    public InternalTimer(TargetLocationItem item)
		:base(TimeSpan.FromMilliseconds(500), TimeSpan.FromSeconds(1.0))
	    {
		m_Item = item;
	    }

	    protected override void OnTick()
	    {
		if(m_Item.Deleted)
		    return;

		if(DateTime.UtcNow > m_Item.End)
		{
		    m_Item.Delete();
		    Stop();
		}
	    }
	}
    }
}