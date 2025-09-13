using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Prompts;
using Server.Network;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps;
using System.Globalization;

namespace Server.Items
{
    public class SoulOrb : Item
    {
		private static Dictionary<Mobile, SoulOrb> m_ResList;
		
        public Mobile m_Owner;

        [CommandProperty( AccessLevel.GameMaster )]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; InvalidateProperties(); }
        }

        private Timer m_Timer;
        private static TimeSpan m_Delay = TimeSpan.FromSeconds( 30.0 ); /*TimeSpan.Zero*/

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan Delay { get { return m_Delay; } set { m_Delay = value; } }
	
        public static void Initialize()
        {
            EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_Death);
			EventSink.Login += new LoginEventHandler(e => 
                {
                    PlayerMobile owner = e.Mobile as PlayerMobile;
                    if (owner == null) return;

                    SoulOrb orb;
                    if (m_ResList == null || !m_ResList.ContainsKey(owner) || !m_ResList.TryGetValue(owner, out orb)) return;

                    orb.StartTimer();
                }
            );
        }

        [Constructable]
        public SoulOrb() : base( 0x2C84 ) 
        {
            Name = "soul orb";
            LootType = LootType.Blessed;
			Movable = false;
            Weight = 1.0;
		}

        public SoulOrb(Serial serial) : base(serial){}
		
        public static bool IsProtected(Mobile from)
        {
            return m_ResList != null && m_ResList.ContainsKey(from);
        }

		public static void OnSummoned( Mobile from, SoulOrb orb )
		{
			if( m_ResList != null )
				m_ResList.Remove( from );

			if( m_ResList == null )
				m_ResList = new Dictionary<Mobile, SoulOrb>();

			if(from != null && orb != null && !m_ResList.ContainsValue(orb))
			{
				m_ResList.Add(from, orb);
			}
		}

		private static void EventSink_Death(PlayerDeathEventArgs e)
        {
            PlayerMobile owner = e.Mobile as PlayerMobile;

            if (owner != null && !owner.Deleted)
            {
                if (owner.Alive)
                    return;
				
				if(m_ResList != null && m_ResList.ContainsKey(owner))
				{
					SoulOrb arp = m_ResList[owner];
					if(arp == null || arp.Deleted)
					{
						m_ResList.Remove(owner);
						return;
					}

                    arp.StartTimer();
				}
            }
        }

        private void StartTimer()
        {
            if (Deleted || Owner.Alive) { return; }
            
            if (m_Timer != null)
                m_Timer.Stop();

            BuffInfo.AddBuff( Owner, new BuffInfo( BuffIcon.GiftOfLife, 1015222,TimeSpan.FromSeconds(30), Owner, String.Format("You will resurrect within 30 seconds of your death"),true ) );
            m_Timer = Timer.DelayCall(m_Delay, new TimerStateCallback(Resurrect_OnTick), new object[] { Owner, this });
        }

        private void DoUse()
        {
            if (Deleted || Owner.Alive) { return; }
            
            if ( Name == "blood of a vampire" ){ Owner.SendMessage("The blood pours out of the bottle, restoring your life."); }
            else if ( Name == "cloning crystal" ){ Owner.SendMessage("The crystal forms a clone of your body, restoring your life."); }
            else { Owner.SendMessage("The orb glows, releasing your soul."); }

            Owner.Resurrect();
            Owner.FixedEffect( 0x376A, 10, 16, Server.Items.CharacterDatabase.GetMySpellHue( Owner, 0 ), 0 );
            Server.Misc.Death.Penalty( Owner, false );
            BuffInfo.RemoveBuff(Owner, BuffIcon.GiftOfLife);
            if (m_Timer != null)
                m_Timer.Stop();
            if (m_ResList != null) 
                m_ResList.Remove(Owner);
            Delete();
        }

        private void Resurrect_OnTick(object state)
        {
            object[] states = (object[])state;
            PlayerMobile owner = (PlayerMobile)states[0];
			SoulOrb arp = (SoulOrb)states[1];
            if (owner != null && !owner.Deleted && arp != null && !arp.Deleted)
            {
                if (!owner.Alive)
                {
                    TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
    				var confirmationGump = new ConfirmationGump(
                        owner, 
                        cultInfo.ToTitleCase(arp.Name),
                        "The spirits offer their aid.<br>Do you accept?", 
                        arp.DoUse,
                        arp.StartTimer
                    );
                    owner.SendGump(confirmationGump);
                }
            }
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			if ( this.Name == "blood of a vampire" ){ list.Add( 1049644, "Contains vampire blood for " + m_Owner.Name ); }
			else if ( this.Name == "cloning crystal" ){ list.Add( 1049644, "Contains genetic patterns for " + m_Owner.Name ); }
			else { list.Add( 1049644, "Contains the Soul of " + m_Owner.Name ); }
        } 

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write( (int) 0 ); // version          
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
			this.Delete(); // none when the world starts 
        }
    }
}