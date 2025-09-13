using System; 
using System.Collections.Generic; 
using Server; 
using Server.Misc;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Regions;
using Server.Spells;

namespace Server.Mobiles 
{ 
	public class Shipwright : BaseVendor 
	{ 
		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } } 

		public override NpcGuild NpcGuild{ get{ return NpcGuild.FishermensGuild; } }

		[Constructable]
		public Shipwright() : base( "the shipwright" ) 
		{ 
			Job = JobFragment.shipwright;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Carpentry, 60.0, 83.0 );
			SetSkill( SkillName.Macing, 36.0, 68.0 );
		} 

		public override void InitSBInfo() 
		{ 
			m_SBInfos.Add( new SBShipwright() ); 
			m_SBInfos.Add( new SBSailor() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.SmithHammer() );
			AddItem( new Server.Items.TricorneHat( Utility.RandomDyedHue() ) );
		}

		///////////////////////////////////////////////////////////////////////////

		public override bool OnDragDrop( Mobile from, Item o )
		{
			if ( o is Key && ((Key)o).KeyValue != 0 && ((Key)o).Link is BaseBoat )
			{
				BaseBoat boat = ((Key)o).Link as BaseBoat;
				Container pack = from.Backpack;

				if ( !boat.Deleted && boat.CheckKey( ((Key)o).KeyValue ) )
				{
                    if (pack.ConsumeTotal(typeof(Gold), 1000))
                    {
						Point3D location = boat.GetMarkedLocation();
						string region = Worlds.GetMyWorld(boat.Map, location, location.X, location.Y);
                        from.SendMessage(String.Format("You pay 1,000 gold."));
                        this.SayTo(from, string.Format("This vessel was last seen in {0} around ({1}, {2}).", region, location.X, location.Y));
					}
					else
					{
                        this.SayTo(from, "It would cost you 1,000 gold for me to find your ship.");
                        from.SendMessage("You do not have enough gold.");
					}
				}
				else
				{
					this.SayTo(from, "There is nothing I can do with that.");
				}
			}

			return base.OnDragDrop( from, o );
		}

		///////////////////////////////////////////////////////////////////////////
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "The High Seas", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Shipwright" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public Shipwright( Serial serial ) : base( serial ) 
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
	} 
}