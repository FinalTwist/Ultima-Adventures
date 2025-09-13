using System;
using Server.Mobiles;
using Server.OneTime;
using System.Collections;
using System.Collections.Generic;  

namespace Server.Items
{
    public class BurningFire : Item, IOneTime
    {

        private int m_duration;
        public int Duration
        {
            get{ return m_duration; }
            set{ m_duration = value; }
        }
        private DateTime m_End;

        private int ticktock;


        private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

		[Constructable]
        public BurningFire()
            : base( Utility.RandomList(0x398C, 0x3996) )
        {
            this.Visible = true;
            this.Movable = false;
            m_OneTimeType = 3;
            m_duration = Utility.RandomMinMax(20,40);
            ticktock = 0;


        }

        public BurningFire(Serial serial)
            : base(serial)
        {
        }

        public override void OnDelete()
        {
            base.OnDelete();



        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_OneTimeType = 3;
        }

        public void OneTimeTick()
        {
            if (ticktock < m_duration)
            {
                ArrayList targets = new ArrayList();
			    foreach ( Mobile m in this.GetMobilesInRange( 0 ) )
                {
                    

                    if ( ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned ) ) || (m.Player && AccessLevel.Player == m.AccessLevel) || (m is Rikktor && m.Hits < (m.HitsMax - m.HitsMax/4) ) )
                        targets.Add( m );
                }
                for ( int i = 0; i < targets.Count; ++i )
			    {
                    Mobile m = (Mobile)targets[i];
                    if (m is PlayerMobile)
                    {
                        m.SendMessage( 0, "It burns!  It Burns!" );
                    }
                    //AOS.Damage( m, Utility.RandomMinMax(m.HitsMax/20, m.HitsMax/5), 0, 100, 0, 0, 0 );
                    if (m is Rikktor)
                    {
                        m.Hits += Utility.RandomMinMax(m.HitsMax/20, m.HitsMax/5);
                    }
                    else
                        m.Damage( Utility.RandomMinMax(m.HitsMax/20, m.HitsMax/5), m);
                }
                ticktock += 1;
            }
            else 
            {
                this.Delete();
            }

                
        }

        public override bool OnMoveOver(Mobile m)
        {

            if (Utility.RandomDouble() > 0.85 && AccessLevel.Player == m.AccessLevel && ( m is PlayerMobile || (m is BaseCreature && ((BaseCreature)m).Controlled)) )
            {
                if (m is PlayerMobile)
                    m.SendMessage( 0, "Ouch!" );

                //AOS.Damage( m, Utility.RandomMinMax(m.HitsMax/20, m.HitsMax/5), 0, 100, 0, 0, 0 );
                m.Damage( Utility.RandomMinMax(m.HitsMax/20, m.HitsMax/5),m);
            }

            return true;
        }

    }
}