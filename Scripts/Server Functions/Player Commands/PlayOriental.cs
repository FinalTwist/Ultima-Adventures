using System;
using Server;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;

namespace Server.Items
{
    class PlayOriental
    {
        public static void Initialize()
        {
            CommandSystem.Register("oriental", AccessLevel.Player, new CommandEventHandler(OnTogglePlayOriental));
        }

        [Usage("oriental")]
        [Description("Enables or disables the oriental play style.")]
        private static void OnTogglePlayOriental(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB != null )
			{
				if ( DB.CharacterOriental == 1 )
				{
					m.SendMessage(38, "You have disabled the oriental play style.");
					DB.CharacterOriental = 0;
				}
				else
				{
					m.SendMessage(68, "You have enabled the oriental play style.");
					DB.CharacterOriental = 1;
					DB.CharacterEvil = 0;
					DB.CharacterBarbaric = 0;
				}
			}
        }
    }
}
