using System;
using Server.Network;
using Server;
using Server.Targets;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;
using Server.Misc;
using Server.Gumps;

namespace Server.Items
{
	public class ResurrectPotion : BasePotion
	{
		[Constructable]
		public ResurrectPotion() : base( 0x180F, PotionEffect.Resurrect )
		{
			Hue = 0xB6D;
			Name = "resurrection potion";
		}

		public ResurrectPotion( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Resurrects Others");
        } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	  
	  	public override void Drink( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 1 ) ) 
			{
				m.Target = new InternalTarget( m, this );
				m.SendMessage( "Who would you like to resurrect!" );
			} 
			else 
			{ 
				m.LocalOverheadMessage( MessageType.Regular, 906, 1019045 ); // I can't reach that. 
			} 
		}

        public void Target( Mobile m, Mobile from, ResurrectPotion potion )
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
 
                m.CloseGump( typeof( ResurrectGump ) );
                m.SendGump( new ResurrectGump( m, from ) );

				ConsumeCharge( potion, from );
            }
            else if (m is BaseCreature )
			{
				BaseCreature pet = (BaseCreature)m;
				Mobile master = pet.GetMaster();
 
                m.PlaySound( 0x214 );
                m.FixedEffect( 0x376A, 10, 16 );
 
                master.CloseGump(typeof(PetResurrectGump));
                master.SendGump(new PetResurrectGump(master, pet));

				ConsumeCharge( potion, from );
            }
        }

        public void ItemTarget( Item hench, Mobile from, ResurrectPotion potion )
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
					ConsumeCharge( potion, from );
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
					ConsumeCharge( potion, from );
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
					ConsumeCharge( potion, from );
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
					ConsumeCharge( potion, from );
				}
				else
				{
					from.SendMessage("They are not dead.");
				}
			}
			else
			{
				from.SendMessage("This potion didn't seem to work.");
			}
		}
 
        private class InternalTarget : Target
        {
            private Mobile m_Owner;
            private ResurrectPotion m_Potion;
 
            public InternalTarget( Mobile owner, ResurrectPotion potion ) : base( 1, false, TargetFlags.Beneficial )
            {
                m_Owner = owner;
				m_Potion = potion;
            }
 
            protected override void OnTarget( Mobile from, object o )
            {
                if ( o is Mobile )
                {
                    m_Potion.Target( (Mobile)o, from, m_Potion );
                }
                else if ( o is Item )
                {
                    m_Potion.ItemTarget( (Item)o, from, m_Potion );
                }
            }
        }

		public void ConsumeCharge( ResurrectPotion potion, Mobile from )
		{
			potion.Consume();
			from.AddToBackpack( new Bottle() );
			from.RevealingAction();
			from.PlaySound( 0x23E );
		}
	}
}
