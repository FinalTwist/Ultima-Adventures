using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;
using Server.Targeting;

namespace Server.Items
{
	public class MalletStake : Item
	{
		public int VampiresSlain;

		[CommandProperty(AccessLevel.Owner)]
		public int Vampires_Slain { get { return VampiresSlain; } set { VampiresSlain = value; InvalidateProperties(); } }

		[Constructable]
		public MalletStake() : base( 0x12B3 )
		{
			Weight = 1.0;
			Name = "wooden mallet and stake";
			Hue = 0x96D;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Double Click For Information");
            list.Add( 1049644, "Single Click To Use");
        }

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new StakeGump( from, this ) );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060738, VampiresSlain.ToString() );
		}

		public class StakeGump : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private MalletStake m_Stake;
			
			public StakeGump( Mobile from, MalletStake stake ) : base( 6132, 3 )
			{
				m_Mobile = from;
				m_Stake = stake;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;

				if ( m_Stake.VampiresSlain >= 10000 )
				{
					m_Mobile.SendMessage("This has killed enough vampires.");
					return;
				}
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					m_Mobile.SendMessage("What vampire do you want to stake?");
					m_Mobile.Target = new CorpseTarget( m_Stake );
				}
            }
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Vampire Reward: " + VampiresSlain + " Gold!");
			from.SendGump(new SpeechGump( "The Vampire Scourge", SpeechFunctions.SpeechText( from.Name, from.Name, "Stake" ) ));
		}

		private class CorpseTarget : Target
		{
			private MalletStake m_Stake;

			public CorpseTarget( MalletStake stake ) : base( 3, false, TargetFlags.None )
			{
				m_Stake = stake;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Stake.Deleted )
					return;

				object obj = targeted;

				if ( obj is Corpse )
				{
					Corpse c = (Corpse)targeted;
					
					if (c.Owner == null)
						return;

					if ( c.VisitedByTaxidermist == true )
					{
						from.SendMessage("You don't need to do that!");
						return;
					}
					else
					{
						int score = 0;

						if ( typeof( VampireWoods ) == c.Owner.GetType() ){ score = 10; }
						else if ( typeof( Vampire ) == c.Owner.GetType() ){ score = 20; }
						else if ( typeof( VampireLord ) == c.Owner.GetType() ){ score = 40; }
						else if ( typeof( VampirePrince ) == c.Owner.GetType() ){ score = 60; }
						else if ( typeof( Dracula ) == c.Owner.GetType() ){ score = 400; }
						else if ( typeof( VampiricDragon ) == c.Owner.GetType() ){ score = 500; }

						if ( score > 0 )
						{
							m_Stake.VampiresSlain = m_Stake.VampiresSlain + score;
								if ( m_Stake.VampiresSlain > 10000 ){ m_Stake.VampiresSlain = 10000; }
							from.SendMessage("Vampire Reward: " + m_Stake.VampiresSlain + " Gold!");
							c.VisitedByTaxidermist = true;
							from.PlaySound( 0x13E );
							m_Stake.InvalidateProperties();
						}
						else 
						{
							from.SendMessage("You don't need to do that!");
							return;
						}
					}
				}
				else
				{
					from.SendMessage("You don't need to do that!");
					return;
				}
			}
		}

		public MalletStake( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( VampiresSlain );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			VampiresSlain = reader.ReadInt();
		}
	}
}