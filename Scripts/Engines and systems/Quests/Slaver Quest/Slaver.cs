using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Accounting;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "the Stranger corpse" )]
	public class Slaver : Mobile
	{
		
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
		public DateTime m_NextTalk;
	
        public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Slaver()
		{
			Name = "a Slaver";
			CantWalk = true;
			Body = 136;
		}

		public Slaver( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
	        { 
	                base.GetContextMenuEntries( from, list ); 
					if (from.Karma < -5000)
						list.Add( new SlaverEntry( from, this ) ); 
	        } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public class SlaverEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SlaverEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
                if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( SlaversGump ) ) )
					{
						mobile.SendGump( new SlaversGump( mobile ));
						
					} 
				}
			}
		}
			
			
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if (m == null || m.Map == null)
				return;
			
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) && m is PlayerMobile && !m.Hidden && DateTime.UtcNow >= m_NextTalk)
			{
					m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
					switch (Utility.Random(9))
					{
					case 0: Say("Ai laik my meet laik mai wowman, furm."); break;
					case 1: Say("Ai hayt wayting E'ere fur that doosh.  Wher de fuk iss hie."); break;
					case 2: Say("*scratches his ass*"); break;
					case 3: Say("OOooo.... yoo'r nawt bawd... want too gow fur a rayd down bielow??"); break;
					case 4: Say("Cum with mie, Ay'll show yoo a reel gewd taym."); break;
					case 5: Say("*farts*"); break;
					case 6: Say("Wherr is thawt dam deliverie?"); break;
					case 7: Say("Mawch eesier to wayt E'ere than go oot in thu fukin sawn. "); break;
					case 8: Say("*rubs his groin against his horns*"); break;
					}

				
			}
			if (InRange( m, 8 ) && InLOS( m ) && m is BaseChild )
			{
				if (((BaseCreature)m).Controlled && ((BaseCreature)m).ControlMaster is PlayerMobile && ((BaseChild)m).type == 5)
				{
					Mobile slavemaster = ((BaseCreature)m).ControlMaster;
					Say("Djems!  Yessss..... Mawr Djems!!! E'ere's yoor stowne."); 
					this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "The creature grabs your slave and throws it in his boat." ) );
					m.Delete();
					slavemaster.AddToBackpack( new MoorStone() );
					Titles.AwardKarma( slavemaster, -500, true);
					AetherGlobe.ChangeCurse( 50 );
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		

			this.Say("Na, I kahnt eet our fawk thaht." );
				return false;
		}
	}
}
