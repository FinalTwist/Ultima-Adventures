using System;
using Server;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;

namespace Server.Items
{
    class PlayEvil
    {
        public static void Initialize()
        {
            CommandSystem.Register("evil", AccessLevel.Player, new CommandEventHandler(OnTogglePlayOriental));
        }

        [Usage("evil")]
        [Description("Enables or disables the evil play style.")]
        private static void OnTogglePlayOriental(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB != null )
			{
				if ( DB.CharacterEvil == 1 )
				{
					m.SendMessage(38, "You have disabled the evil play style.");
					DB.CharacterEvil = 0;
				}
				else
				{
					m.SendMessage(68, "You have enabled the evil play style.");
					DB.CharacterEvil = 1;
					DB.CharacterOriental = 0;
					DB.CharacterBarbaric = 0;
				}
			}
        }
    }
}
