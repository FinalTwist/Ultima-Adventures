using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
    public abstract class CustomAbilityItem : Item
    {
        public static void Initialize()
        {
            Timer.DelayCall(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), OnTick);
        }

	private bool m_RestrictDecay = false;

        public static List<CustomAbilityItem> _Items = new List<CustomAbilityItem>();

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime TimeOfDecay { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool RestrictDecay 
	{ 
	    get { return m_RestrictDecay; }
	    set { m_RestrictDecay = value; }
	}

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual TimeSpan DecayDelay { get { return TimeSpan.FromSeconds(30.0); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Decaying { get { return TimeOfDecay != DateTime.MinValue; } }

       [CommandProperty(AccessLevel.GameMaster)]
        public bool ForceDecay
        {
            get { return false; }
            set { SetDecayTime(); }
        }

	public CustomAbilityItem(int itemID)
	    : base(itemID)
	{
	    _Items.Add(this);
	}

	public CustomAbilityItem(Serial serial)
	    : base(serial)
	{
	}

        public virtual void CheckDecay()
        {
            if (RestrictDecay)
                return;

            if (!Decaying)
            {
		SetDecayTime();
            }
            else if(TimeOfDecay < DateTime.UtcNow)
            {
                Delete();
            }
        }

        public virtual void SetDecayTime()
        {
            if (Deleted || RestrictDecay)
                return;

            TimeOfDecay = DateTime.UtcNow + DecayDelay;
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            _Items.Remove(this);
        }


	public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version

            writer.WriteDeltaTime(TimeOfDecay);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

  
            TimeOfDecay = reader.ReadDeltaTime();

            if (version == 0 && ItemID == 0x10EE)
            {
                ItemID = 0x1F6D;
            }

            if (version == 1)
                Delete();

            _Items.Add(this);
        }

        public static void OnTick()
        {
            List<CustomAbilityItem> list = new List<CustomAbilityItem>(_Items);

            list.ForEach(c =>
                {
                    if (!c.Deleted && c.Map != null && c.Map != Map.Internal && !c.RestrictDecay)
                        c.CheckDecay();
                });

            list.Clear();
			list.TrimExcess();
            //list.Clear();
        }


    }
}