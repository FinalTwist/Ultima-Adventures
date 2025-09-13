using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using System.Text;
using Server;
using Server.Commands;
using Server.Commands.Generic;
using System.IO;
using Server.Mobiles;
using Server.Gumps;
using Server.Accounting;

namespace Server
{
    public class Announce
    {
        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(World_Login);
            EventSink.Logout += new LogoutEventHandler(World_Logout);
            EventSink.Disconnected += new DisconnectedEventHandler(World_Leave);
            EventSink.PlayerDeath += new PlayerDeathEventHandler(OnDeath);
        }

        private static void World_Login(LoginEventArgs args)
        {
            Mobile m = args.Mobile;

			( ( PlayerMobile )m ).BankBox.MaxItems = 201;

			if ( m.Hue >= 33770 ){ m.Hue = m.Hue - 32768; }

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			if ( DB == null )
			{
				CharacterDatabase MyDB = new CharacterDatabase();
				MyDB.CharacterOwner = m;
				m.BankBox.DropItem( MyDB );
			}

			CharacterDatabase DataBase = Server.Items.CharacterDatabase.GetDB( m ); // LET US SET THEIR ORIGINAL COLORS FOR LATER USE

			if ( DataBase.CharHue > 0 )
			{
				// DO NOTHING
			}
			else
			{
				DataBase.CharHue = m.Hue;
				DataBase.CharHairHue = m.HairHue;
			}

			LoggingFunctions.LogAccess( m, "login" );

			if ( m.Region.GetLogoutDelay( m ) == TimeSpan.Zero && !m.Poisoned ){ m.Hits = 1000; m.Stam = 1000; m.Mana = 1000; } // FULLY REST UP ON LOGIN

			if ( m.FindItemOnLayer( Layer.Shoes ) != null && m is PlayerMobile && !((PlayerMobile)m).SoulBound )
			{
				Item shoes = m.FindItemOnLayer( Layer.Shoes );
				if ( shoes is BootsofHermes  )
				{
					if ( Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( Region.Find( m.Location, m.Map ) ) )
					{
						m.Send(SpeedControl.Disable);
						shoes.Weight = 5.0;
						m.SendMessage( "These boots seem to have their magic diminished here." );
						Server.Spells.Mystic.WindRunner.RemoveEffect( m );
						Server.Spells.Syth.SythSpeed.RemoveEffect( m );
							Server.Spells.Jedi.Celerity.RemoveEffect( m );
					}
					else
					{
						m.Send(SpeedControl.MountSpeed);
						shoes.Weight = 3.0;
					}
				}
				
			}
			else if ( m is PlayerMobile && ((PlayerMobile)m).SoulBound && ((PlayerMobile)m).sbmaster)
			{
				if ( Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( Region.Find( m.Location, m.Map ) ) )
				{
					m.SendMessage( "Your speed is diminished here." );
					((PlayerMobile)m).sbmasterspeed = false;
					m.Send(SpeedControl.Disable);
				}
				else
				{
					m.Send(SpeedControl.MountSpeed);
					((PlayerMobile)m).sbmasterspeed = true;
				}
			}
        }

        private static void World_Leave(DisconnectedEventArgs args)
        {
			if ( Server.Misc.MyServerSettings.SaveOnCharacterLogout() ){ World.Save( true, false ); }
        }

        private static void World_Logout(LogoutEventArgs args)
        {
            Mobile m = args.Mobile;
			LoggingFunctions.LogAccess( m, "logout" );
        }
		
        public static void OnDeath(PlayerDeathEventArgs args)
        {
            Mobile m = args.Mobile;
			GhostHelper.OnGhostWalking( m );
        }
    }
}