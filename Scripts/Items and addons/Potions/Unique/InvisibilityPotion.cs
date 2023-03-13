using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class InvisibilityPotion : BasePotion
	{
		[Constructable]
		public InvisibilityPotion() : base( 0x180F, PotionEffect.Invisibility )
		{
			Name = "invisibility potion";
			Hue = Server.Items.PotionKeg.GetPotionColor( this );
		}

		public InvisibilityPotion( Serial serial ) : base( serial )
		{
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

		private static Hashtable m_Table = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( m_Table[m] != null );
		}
		
		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])m_Table[m];
			
			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
				m.RemoveSkillMod( (SkillMod)mods[1] );
			}
			
			m_Table.Remove( m );
			m.EndAction( typeof( InvisibilityPotion ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
		    if ( !m.CanBeginAction( typeof( InvisibilityPotion ) ) )
		    {
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another invisibility potion yet", m.NetState);
		    }
		    else if ( !m.CanBeginAction( typeof( LesserInvisibilityPotion ) ) )
		    {
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another invisibility potion yet", m.NetState);
		    }
		    else if ( !m.CanBeginAction( typeof( GreaterInvisibilityPotion ) ) )
		    {
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another invisibility potion yet", m.NetState);
		    }
		    else
		    {
				int MyHide = 100 - (int)m.Skills[SkillName.Hiding].Base;
					if ( MyHide < 0 ){ MyHide = 0; }
				int MyStealth = 100 - (int)m.Skills[SkillName.Stealth].Base;
					if ( MyStealth < 0 ){ MyStealth = 0; }

			object[] mods = new object[]
			{
				new DefaultSkillMod( SkillName.Hiding, true, MyHide ),
				new DefaultSkillMod( SkillName.Stealth, true, MyStealth ),
			};

			m_Table[m] = mods;

			m.AddSkillMod( (SkillMod)mods[0] );
			m.AddSkillMod( (SkillMod)mods[1] );

				foreach ( Mobile pet in World.Mobiles.Values )
				{
					if ( pet is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)pet;
						if ( bc.Controlled && bc.ControlMaster == m )
							pet.Hidden = true;
					}
				}

			new InternalTimer( m, TimeSpan.FromMinutes( 2 ) ).Start();

			BasePotion.PlayDrinkEffect( m );

			m.Hidden = true;

			m.BeginAction( typeof( InvisibilityPotion ) );

			this.Amount--;
				if (this.Amount <= 0)
				this.Delete();
		    }
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;
			
			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
				
			}
			
			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					InvisibilityPotion.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}
}