using System;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public enum ResurrectEffect
	{
		Charges
	}

	public class RodOfResurrection : Scepter
	{
		private ResurrectEffect m_ResurrectEffect;
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public ResurrectEffect Effect { get{ return m_ResurrectEffect; } set{ m_ResurrectEffect = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges { get{ return m_Charges; } set{ m_Charges = value; InvalidateProperties(); } }

		[Constructable]
		public RodOfResurrection()
		{
			Name = "Rod Of Resurrection";
			Hue = 0x4AC;
			Charges = 50;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Parent != from )
			{
				from.SendMessage( "You must be holding the rod to resurrect." );
			}
			else if ( Charges < 1 )
			{
				from.SendMessage( "All of the magic has been drained from the rod." );
				this.Delete();
			}
			else
			{
				from.Target = new InternalTarget( from, this );
				from.SendMessage( "Who would you like to resurrect!" );
			}
			return;
		}

        public void Target( Mobile m, Mobile from, RodOfResurrection rod )
        {
            if ( !from.CanSee( m ) )
            {
                from.SendLocalizedMessage( 500237 ); // Target can not be seen.
            }
            else if ( !from.Alive )
            {
                from.SendLocalizedMessage( 501040 ); // The resurrecter must be alive.
            }
            else if (m is PlayerMobile && m.Alive)
            {
                from.SendLocalizedMessage( 501041 ); // Target is not dead.
            }
            else if ( !from.InRange( m, 2 ) )
            {
                from.SendLocalizedMessage( 501042 ); // Target is not close enough.
            }
            else if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
            {
                from.SendLocalizedMessage( 501042 ); // Target can not be resurrected at that location.
                m.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
            }
            else if ( m is PlayerMobile )
            {
                m.PlaySound( 0x214 );
                m.FixedEffect( 0x376A, 10, 16 );

                m.CloseGump( typeof( ResurrectCostGump ) );
				m.SendGump( new ResurrectCostGump( m, 1 ) );

				ConsumeCharge( rod, from );
            }
            else if (m is BaseCreature )
			{
				BaseCreature pet = (BaseCreature)m;
				Mobile master = pet.GetMaster();
 
                m.PlaySound( 0x214 );
                m.FixedEffect( 0x376A, 10, 16 );
 
                master.CloseGump(typeof(PetResurrectGump));
                master.SendGump(new PetResurrectGump(master, pet));

				ConsumeCharge( rod, from );
            }
        }

        public void ItemTarget( Item hench, Mobile from, RodOfResurrection rod )
        {
			if ( hench is HenchmanFighterItem )
			{
				HenchmanFighterItem friend = (HenchmanFighterItem)hench;

				if ( friend.HenchDead > 0 )
				{
					friend.Name = "fighter henchman";
					friend.HenchDead = 0;
					friend.InvalidateProperties();
					from.PlaySound( 0x214 );
					ConsumeCharge( rod, from );
				}
				else
				{
					from.SendMessage("They are not dead.");
				}
			}
			else if ( hench is HenchmanWizardItem )
			{
				HenchmanWizardItem friend = (HenchmanWizardItem)hench;

				if ( friend.HenchDead > 0 )
				{
					friend.Name = "wizard henchman";
					friend.HenchDead = 0;
					friend.InvalidateProperties();
					from.PlaySound( 0x214 );
					ConsumeCharge( rod, from );
				}
				else
				{
					from.SendMessage("They are not dead.");
				}
			}
			else if ( hench is HenchmanArcherItem )
			{
				HenchmanArcherItem friend = (HenchmanArcherItem)hench;

				if ( friend.HenchDead > 0 )
				{
					friend.Name = "archer henchman";
					friend.HenchDead = 0;
					friend.InvalidateProperties();
					from.PlaySound( 0x214 );
					ConsumeCharge( rod, from );
				}
				else
				{
					from.SendMessage("They are not dead.");
				}
			}
			else if (hench is HenchmanMonsterItem )
			{
				HenchmanMonsterItem friend = (HenchmanMonsterItem)hench;

				if ( friend.HenchDead > 0 )
				{
					friend.Name = "creature henchman";
					friend.HenchDead = 0;
					friend.InvalidateProperties();
					from.PlaySound( 0x214 );
					ConsumeCharge( rod, from );
				}
				else
				{
					from.SendMessage("They are not dead.");
				}
			}
			else
			{
				from.SendMessage("This spell didn't seem to work.");
			}
		}
 
        private class InternalTarget : Target
        {
            private Mobile m_Owner;
            private RodOfResurrection m_Rod;
 
            public InternalTarget( Mobile owner, RodOfResurrection rod ) : base( 1, false, TargetFlags.Beneficial )
            {
                m_Owner = owner;
				m_Rod = rod;
            }
 
            protected override void OnTarget( Mobile from, object o )
            {
                if ( o is Mobile )
                {
                    m_Rod.Target( (Mobile)o, from, m_Rod );
                }
                else if ( o is Item )
                {
                    m_Rod.ItemTarget( (Item)o, from, m_Rod );
                }
            }
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public void ConsumeCharge( RodOfResurrection rod, Mobile from )
		{
			--rod.Charges;

			if ( rod.Charges == 0 )
			{
				from.SendMessage( "All of the magic has been drained from the rod." );
				rod.Delete();
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060584, m_Charges.ToString() );
		}

		public RodOfResurrection( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_ResurrectEffect );
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_ResurrectEffect = (ResurrectEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();
					break;
				}
			}
		}
	}
}
