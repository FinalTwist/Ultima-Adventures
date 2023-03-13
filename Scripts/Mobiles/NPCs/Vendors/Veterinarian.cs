using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.ContextMenus;
using Server.Misc;

namespace Server.Mobiles
{
	public class Veterinarian : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.DruidsGuild; } }

		[Constructable]
		public Veterinarian() : base( "the vet" )
		{
			Job = JobFragment.vet;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.AnimalLore, 85.0, 100.0 );
			SetSkill( SkillName.Veterinary, 90.0, 100.0 );
		}

		///////////////////////////////////////////////////////////////////////////

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
			list.Add( new RidingGumpEntry( from, this ) ); 
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
						mobile.SendGump(new SpeechGump( "Animal Companions", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Pets" ) ));
					}
				}
            }
        }

		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			return true; 
		} 

		public override void OnSpeech( SpeechEventArgs e ) 
		{
			if ( !(e.Mobile is PlayerMobile) )
				return;
			  
			if( e.Mobile.InRange( this, 4 ))
			{
				if (  Insensitive.Contains( e.Speech, "find" ) || Insensitive.Contains( e.Speech, "fetch" ) )
				{
					Say("*sigh*");
					Say("Of course, let me herewith venture forth in dangerous lands, risking my life because you are too lazy to do it yourself!"); 
					
					Mobile from = e.Mobile;

					int i_Bank;
					i_Bank = Banker.GetBalance( from );				
					Container bank = from.FindBankNoCreate();				
					if ( ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( Gold ), 1000 ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), 1000 ) ) )
					{
						AdventuresFunctions.FetchFollowers(from);
					}
					else
					{
						from.SendMessage("I only work for gold, Sire.");
						from.SendMessage("Make sure you have 1,000 gold (coins) in your pack or bank.");
					}

				}
			}
			base.OnSpeech(e);
		}

		///////////////////////////////////////////////////////////////////////////

		public class RidingGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public RidingGumpEntry( Mobile from, Mobile giver ) : base( 6098, 3 )
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
					if ( ! mobile.HasGump( typeof( Server.Mobiles.Veterinarian.RidingGump ) ) )
					{
						mobile.SendGump(new Server.Mobiles.Veterinarian.RidingGump());
					}
				}
            }
        }

		public class RidingGump : Gump
		{
			public RidingGump() : base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(600, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(600, 300, 155);
				AddImage(2, 2, 129);
				AddImage(302, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(300, 298, 129);
				AddImage(598, 298, 129);
				AddImage(7, 65, 20766);
				AddImage(839, 356, 131);
				AddImage(817, 537, 156);
				AddHtml( 10, 10, 876, 43, @"<BODY><BASEFONT Color=#FCFF00><BIG>Below are creatures that can be ridden if they are tamed. If you see any creature that looks like these below, regardless of color, they can be used as mounts to travel with.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 599, 511, 231, 73, @"<BODY><BASEFONT Color=#FCFF00><BIG>* Only dragyns can be ridden if the Zuluu legends are true. Young dragons, however, cannot be ridden.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 648, 176, 24, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>*</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		///////////////////////////////////////////////////////////////////////////

		public override void InitSBInfo()
		{
			if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Serpent Island" )
			{
				m_SBInfos.Add( new SBGargoyleAnimalTrainer() );
			}
			else if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Land of Lodoria" )
			{
				m_SBInfos.Add( new SBElfAnimalTrainer() );
			}
			else if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Isles of Dread" )
			{
				m_SBInfos.Add( new SBBarbarianAnimalTrainer() );
			}
			else if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Savaged Empire" )
			{
				m_SBInfos.Add( new SBOrkAnimalTrainer() );
			}
			else
			{
				m_SBInfos.Add( new SBHumanAnimalTrainer() );
			}
			m_SBInfos.Add( new SBVeterinarian() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public Veterinarian( Serial serial ) : base( serial )
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