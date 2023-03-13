using System;
using Server;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.ContextMenus;

namespace Server.Mobiles
{
	public abstract class BaseGuildmaster : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override bool IsActiveVendor{ get{ return true; } }

		public override bool ClickTitle{ get{ return true; } }

		public virtual int JoinCost{ get{ return 2000; } }

		public override void InitSBInfo()
		{
		}

		public virtual void SayGuildTo( Mobile m )
		{
			SayTo( m, 1008055 + (int)NpcGuild );
		}

		public virtual void SayWelcomeTo( Mobile m )
		{
			SayTo( m, "Welcome to the guild! Thou shalt find it beneficial to your future endeavors." );
		}

		public static void SayPriceTo( Mobile m, Mobile guildmaster )
		{
			m.Send( new MessageLocalizedAffix( guildmaster.Serial, guildmaster.Body, MessageType.Regular, guildmaster.SpeechHue, 3, 1008052, guildmaster.Name, AffixType.Append, JoiningFee( m ).ToString(), "" ) );
		}

		public static int JoiningFee( Mobile m )
		{
			int fee = 2000;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB != null )
			{
				fee = DB.CharacterGuilds;
			}

			if ( fee < 2000 ){ fee = 2000; }

			if ( GetPlayerInfo.isFromSpace( m ) ){ fee = fee * 4; }

			return fee;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{
			PlayerMobile pm = (PlayerMobile)from;
			base.GetContextMenuEntries( from, list ); 
			list.Add( new JoinEntry( from, this ) ); 
			if ( pm.NpcGuild == this.NpcGuild ){ list.Add( new ResignEntry( from, this ) ); }
		} 

		public class JoinEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Guildmaster;
			
			public JoinEntry( Mobile from, Mobile guildmaster ) : base( 6116, 3 )
			{
				m_Mobile = from;
				m_Guildmaster = guildmaster;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				JoinGuild( m_Mobile, m_Guildmaster );
            }
        }

		public class ResignEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Guildmaster;
			
			public ResignEntry( Mobile from, Mobile guildmaster ) : base( 6115, 3 )
			{
				m_Mobile = from;
				m_Guildmaster = guildmaster;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				ResignGuild( m_Mobile, m_Guildmaster );
            }
        }

		public static void JoinGuild( Mobile player, Mobile guildmaster )
		{
			PlayerMobile pm = (PlayerMobile)player;

			if ( player.Blessed )
				guildmaster.SayTo( player, "Speak to me when that strange effect has worn off." );
			else if ( ((PlayerMobile)player).Profession == 1 )
				guildmaster.SayTo( player, "I don't think we could let someone like you join." );
			else if ( ((BaseVendor)guildmaster).NpcGuild == NpcGuild.ThievesGuild && pm.Karma > -500 )
				guildmaster.SayTo( player, "I've heard you like hang out with the good folk... you wouldn't fit in here." );
			else if ( pm.NpcGuild == ((BaseVendor)guildmaster).NpcGuild )
				guildmaster.SayTo( player, 501047 ); // Thou art already a member of our guild.
			else if ( pm.NpcGuild != NpcGuild.None )
				guildmaster.SayTo( player, 501046 ); // Thou must resign from thy other guild first.
			else
				SayPriceTo( player, guildmaster );
		}

		public static void ResignGuild( Mobile player, Mobile guildmaster )
		{
			PlayerMobile pm = (PlayerMobile)player;

			if ( pm.NpcGuild != ((BaseVendor)guildmaster).NpcGuild )
			{
				guildmaster.SayTo( player, 501052 ); // Thou dost not belong to my guild!
			}
			else
			{
				guildmaster.SayTo( player, 501054 ); // I accept thy resignation.
				pm.NpcGuild = NpcGuild.None;

				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( player );

				if ( DB.CharacterGuilds > 0 )
				{
					int fees = DB.CharacterGuilds;
					DB.CharacterGuilds = (int)(fees * 1.5);
				}
				else
				{
					DB.CharacterGuilds = 4000;
				}

				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				if ( item is GuildRings )
				{
					GuildRings guildring = (GuildRings)item;
					if ( guildring.RingOwner == player )
					{
						targets.Add( item );
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];
					item.Delete();
				}
			}

		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			PlayerMobile pm = (PlayerMobile)from;
			if ( dropped is BankCheck && from is PlayerMobile && !from.Blessed && ((PlayerMobile)from).Profession != 1 )
			{
				if ( ((BankCheck)dropped).Worth == JoiningFee( from ) )
				{
					if ( pm.NpcGuild == this.NpcGuild )
					{
						SayTo( from, 501047 ); // Thou art already a member of our guild.
					}
					else if ( pm.NpcGuild != NpcGuild.None )
					{
						SayTo( from, 501046 ); // Thou must resign from thy other guild first.
					}
					else
					{
						SayWelcomeTo( from );

						pm.NpcGuild = this.NpcGuild;
						pm.NpcGuildJoinTime = DateTime.UtcNow;
						pm.NpcGuildGameTime = pm.GameTime;

						dropped.Delete();

						ArrayList targets = new ArrayList();
						foreach ( Item item in World.Items.Values )
						if ( item is GuildRings )
						{
							GuildRings guildring = (GuildRings)item;
							if ( guildring.RingOwner == from )
							{
								targets.Add( item );
							}
						}
						for ( int i = 0; i < targets.Count; ++i )
						{
							Item item = ( Item )targets[ i ];
							item.Delete();
						}

						int GuildType = 1;
						if ( this.NpcGuild == NpcGuild.MagesGuild ){ GuildType = 1; }
						else if ( this.NpcGuild == NpcGuild.WarriorsGuild ){ GuildType = 2; }
						else if ( this.NpcGuild == NpcGuild.ThievesGuild ){ GuildType = 3; }
						else if ( this.NpcGuild == NpcGuild.RangersGuild ){ GuildType = 4; }
						else if ( this.NpcGuild == NpcGuild.HealersGuild ){ GuildType = 5; }
						else if ( this.NpcGuild == NpcGuild.MinersGuild ){ GuildType = 6; }
						else if ( this.NpcGuild == NpcGuild.MerchantsGuild ){ GuildType = 7; }
						else if ( this.NpcGuild == NpcGuild.TinkersGuild ){ GuildType = 8; }
						else if ( this.NpcGuild == NpcGuild.TailorsGuild ){ GuildType = 9; }
						else if ( this.NpcGuild == NpcGuild.FishermensGuild ){ GuildType = 10; }
						else if ( this.NpcGuild == NpcGuild.BardsGuild ){ GuildType = 11; }
						else if ( this.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildType = 12; }
						else if ( this.NpcGuild == NpcGuild.NecromancersGuild ){ GuildType = 13; }
						else if ( this.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildType = 14; }
						else if ( this.NpcGuild == NpcGuild.DruidsGuild ){ GuildType = 15; }
						else if ( this.NpcGuild == NpcGuild.ArchersGuild ){ GuildType = 16; }
						else if ( this.NpcGuild == NpcGuild.CarpentersGuild ){ GuildType = 17; }
						else if ( this.NpcGuild == NpcGuild.CartographersGuild ){ GuildType = 18; }
						else if ( this.NpcGuild == NpcGuild.LibrariansGuild ){ GuildType = 19; }
						else if ( this.NpcGuild == NpcGuild.CulinariansGuild ){ GuildType = 20; }
						else if ( this.NpcGuild == NpcGuild.AssassinsGuild ){ GuildType = 21; }

						from.AddToBackpack( new GuildRings( from, GuildType ) );
						from.SendSound( 0x3D );

						return true;
					}

					return false;
				}
				if ( from is PlayerMobile && ((BankCheck)dropped).Worth == 400 && pm.NpcGuild == this.NpcGuild )
				{
					dropped.Delete();

					ArrayList targets = new ArrayList();
					foreach ( Item item in World.Items.Values )
					if ( item is GuildRings )
					{
						GuildRings guildring = (GuildRings)item;
						if ( guildring.RingOwner == from )
						{
							targets.Add( item );
						}
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						Item item = ( Item )targets[ i ];
						item.Delete();
					}

					int GuildType = 1;
					if ( this.NpcGuild == NpcGuild.MagesGuild ){ GuildType = 1; }
					else if ( this.NpcGuild == NpcGuild.WarriorsGuild ){ GuildType = 2; }
					else if ( this.NpcGuild == NpcGuild.ThievesGuild ){ GuildType = 3; }
					else if ( this.NpcGuild == NpcGuild.RangersGuild ){ GuildType = 4; }
					else if ( this.NpcGuild == NpcGuild.HealersGuild ){ GuildType = 5; }
					else if ( this.NpcGuild == NpcGuild.MinersGuild ){ GuildType = 6; }
					else if ( this.NpcGuild == NpcGuild.MerchantsGuild ){ GuildType = 7; }
					else if ( this.NpcGuild == NpcGuild.TinkersGuild ){ GuildType = 8; }
					else if ( this.NpcGuild == NpcGuild.TailorsGuild ){ GuildType = 9; }
					else if ( this.NpcGuild == NpcGuild.FishermensGuild ){ GuildType = 10; }
					else if ( this.NpcGuild == NpcGuild.BardsGuild ){ GuildType = 11; }
					else if ( this.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildType = 12; }
					else if ( this.NpcGuild == NpcGuild.NecromancersGuild ){ GuildType = 13; }
					else if ( this.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildType = 14; }
					else if ( this.NpcGuild == NpcGuild.DruidsGuild ){ GuildType = 15; }
					else if ( this.NpcGuild == NpcGuild.ArchersGuild ){ GuildType = 16; }
					else if ( this.NpcGuild == NpcGuild.CarpentersGuild ){ GuildType = 17; }
					else if ( this.NpcGuild == NpcGuild.CartographersGuild ){ GuildType = 18; }
					else if ( this.NpcGuild == NpcGuild.LibrariansGuild ){ GuildType = 19; }
					else if ( this.NpcGuild == NpcGuild.CulinariansGuild ){ GuildType = 20; }
					else if ( this.NpcGuild == NpcGuild.AssassinsGuild ){ GuildType = 21; }

					this.Say( "Here is your replacement ring." );
					from.AddToBackpack( new GuildRings( from, GuildType ) );

					return true;
				}
			}
			
			return base.OnDragDrop( from, dropped );

		}


				
		public override bool OnGoldGiven( Mobile from, Gold dropped )
		{
			PlayerMobile pm = (PlayerMobile)from;

			if ( from is PlayerMobile && !from.Blessed && dropped.Amount == JoiningFee( from ) && ((PlayerMobile)from).Profession != 1 )
			{
				if ( pm.NpcGuild == this.NpcGuild )
				{
					SayTo( from, 501047 ); // Thou art already a member of our guild.
				}
				else if ( pm.NpcGuild != NpcGuild.None )
				{
					SayTo( from, 501046 ); // Thou must resign from thy other guild first.
				}
				else
				{
					SayWelcomeTo( from );

					pm.NpcGuild = this.NpcGuild;
					pm.NpcGuildJoinTime = DateTime.UtcNow;
					pm.NpcGuildGameTime = pm.GameTime;

					dropped.Delete();

					ArrayList targets = new ArrayList();
					foreach ( Item item in World.Items.Values )
					if ( item is GuildRings )
					{
						GuildRings guildring = (GuildRings)item;
						if ( guildring.RingOwner == from )
						{
							targets.Add( item );
						}
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						Item item = ( Item )targets[ i ];
						item.Delete();
					}

					int GuildType = 1;
					if ( this.NpcGuild == NpcGuild.MagesGuild ){ GuildType = 1; }
					else if ( this.NpcGuild == NpcGuild.WarriorsGuild ){ GuildType = 2; }
					else if ( this.NpcGuild == NpcGuild.ThievesGuild ){ GuildType = 3; }
					else if ( this.NpcGuild == NpcGuild.RangersGuild ){ GuildType = 4; }
					else if ( this.NpcGuild == NpcGuild.HealersGuild ){ GuildType = 5; }
					else if ( this.NpcGuild == NpcGuild.MinersGuild ){ GuildType = 6; }
					else if ( this.NpcGuild == NpcGuild.MerchantsGuild ){ GuildType = 7; }
					else if ( this.NpcGuild == NpcGuild.TinkersGuild ){ GuildType = 8; }
					else if ( this.NpcGuild == NpcGuild.TailorsGuild ){ GuildType = 9; }
					else if ( this.NpcGuild == NpcGuild.FishermensGuild ){ GuildType = 10; }
					else if ( this.NpcGuild == NpcGuild.BardsGuild ){ GuildType = 11; }
					else if ( this.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildType = 12; }
					else if ( this.NpcGuild == NpcGuild.NecromancersGuild ){ GuildType = 13; }
					else if ( this.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildType = 14; }
					else if ( this.NpcGuild == NpcGuild.DruidsGuild ){ GuildType = 15; }
					else if ( this.NpcGuild == NpcGuild.ArchersGuild ){ GuildType = 16; }
					else if ( this.NpcGuild == NpcGuild.CarpentersGuild ){ GuildType = 17; }
					else if ( this.NpcGuild == NpcGuild.CartographersGuild ){ GuildType = 18; }
					else if ( this.NpcGuild == NpcGuild.LibrariansGuild ){ GuildType = 19; }
					else if ( this.NpcGuild == NpcGuild.CulinariansGuild ){ GuildType = 20; }
					else if ( this.NpcGuild == NpcGuild.AssassinsGuild ){ GuildType = 21; }

					from.AddToBackpack( new GuildRings( from, GuildType ) );
					from.SendSound( 0x3D );

					return true;
				}

				return false;
			}

			if ( from is PlayerMobile && dropped.Amount == 400 && pm.NpcGuild == this.NpcGuild )
			{
				dropped.Delete();

				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				if ( item is GuildRings )
				{
					GuildRings guildring = (GuildRings)item;
					if ( guildring.RingOwner == from )
					{
						targets.Add( item );
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];
					item.Delete();
				}

				int GuildType = 1;
				if ( this.NpcGuild == NpcGuild.MagesGuild ){ GuildType = 1; }
				else if ( this.NpcGuild == NpcGuild.WarriorsGuild ){ GuildType = 2; }
				else if ( this.NpcGuild == NpcGuild.ThievesGuild ){ GuildType = 3; }
				else if ( this.NpcGuild == NpcGuild.RangersGuild ){ GuildType = 4; }
				else if ( this.NpcGuild == NpcGuild.HealersGuild ){ GuildType = 5; }
				else if ( this.NpcGuild == NpcGuild.MinersGuild ){ GuildType = 6; }
				else if ( this.NpcGuild == NpcGuild.MerchantsGuild ){ GuildType = 7; }
				else if ( this.NpcGuild == NpcGuild.TinkersGuild ){ GuildType = 8; }
				else if ( this.NpcGuild == NpcGuild.TailorsGuild ){ GuildType = 9; }
				else if ( this.NpcGuild == NpcGuild.FishermensGuild ){ GuildType = 10; }
				else if ( this.NpcGuild == NpcGuild.BardsGuild ){ GuildType = 11; }
				else if ( this.NpcGuild == NpcGuild.BlacksmithsGuild ){ GuildType = 12; }
				else if ( this.NpcGuild == NpcGuild.NecromancersGuild ){ GuildType = 13; }
				else if ( this.NpcGuild == NpcGuild.AlchemistsGuild ){ GuildType = 14; }
				else if ( this.NpcGuild == NpcGuild.DruidsGuild ){ GuildType = 15; }
				else if ( this.NpcGuild == NpcGuild.ArchersGuild ){ GuildType = 16; }
				else if ( this.NpcGuild == NpcGuild.CarpentersGuild ){ GuildType = 17; }
				else if ( this.NpcGuild == NpcGuild.CartographersGuild ){ GuildType = 18; }
				else if ( this.NpcGuild == NpcGuild.LibrariansGuild ){ GuildType = 19; }
				else if ( this.NpcGuild == NpcGuild.CulinariansGuild ){ GuildType = 20; }
				else if ( this.NpcGuild == NpcGuild.AssassinsGuild ){ GuildType = 21; }

				this.Say( "Here is your replacement ring." );
				from.AddToBackpack( new GuildRings( from, GuildType ) );

				return true;
			}

			return base.OnGoldGiven( from, dropped );
		}

		public BaseGuildmaster( string title ) : base( title )
		{
			Title = String.Format( "the {0} {1}", title, Female ? "guildmistress" : "guildmaster" );
		}

		public BaseGuildmaster( Serial serial ) : base( serial )
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