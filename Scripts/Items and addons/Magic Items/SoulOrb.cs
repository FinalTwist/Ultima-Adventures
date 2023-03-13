using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Prompts;
using Server.Network;
using System.Collections;
using System.Collections.Generic;

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
					BuffInfo.AddBuff( owner, new BuffInfo( BuffIcon.GiftOfLife, 1015222,TimeSpan.FromSeconds(30), owner, String.Format("You will resurrect within 30 seconds of your death"),true ) );
					arp.m_Timer = Timer.DelayCall(m_Delay, new TimerStateCallback(Resurrect_OnTick), new object[] { owner, arp });
					m_ResList.Remove(owner);
				}
            }
        }

        private static void Resurrect_OnTick(object state)
        {
            object[] states = (object[])state;
            PlayerMobile owner = (PlayerMobile)states[0];
			SoulOrb arp = (SoulOrb)states[1];
            if (owner != null && !owner.Deleted && arp != null && !arp.Deleted)
            {
                if (owner.Alive)
                    return;

				if ( arp.Name == "blood of a vampire" ){ owner.SendMessage("The blood pours out of the bottle, restoring your life."); }
				else if ( arp.Name == "cloning crystal" ){ owner.SendMessage("The crystal forms a clone of your body, restoring your life."); }
                else { owner.SendMessage("The orb glows, releasing your soul."); }
                owner.Resurrect();
                owner.FixedEffect( 0x376A, 10, 16, Server.Items.CharacterDatabase.GetMySpellHue( owner, 0 ), 0 );
                Server.Misc.Death.Penalty( owner, false );
                BuffInfo.RemoveBuff(owner, BuffIcon.GiftOfLife);
                arp.Delete();
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