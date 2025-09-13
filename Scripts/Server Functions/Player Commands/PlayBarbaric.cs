using System;
using Server;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;

namespace Server.Items
{
    class PlayBarbaric
    {
        public static void Initialize()
        {
            CommandSystem.Register("barbaric", AccessLevel.Player, new CommandEventHandler(OnTogglePlayBarbaric));
        }

        [Usage("barbaric")]
        [Description("Enables or disables the barbaric play style.")]
        private static void OnTogglePlayBarbaric(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB != null )
			{
				if ( DB.CharacterBarbaric == 1 && m.Female )
				{
					m.SendMessage(68, "You have enabled the barbaric play style with amazon fighter titles.");
					DB.CharacterBarbaric = 2;
				}
				else if ( DB.CharacterBarbaric > 0 )
				{
					m.SendMessage(38, "You have disabled the barbaric play style.");
					DB.CharacterBarbaric = 0;
					Server.Items.BarbaricSatchel.GetRidOf( m );
				}
				else
				{
					m.SendMessage(68, "You have enabled the barbaric play style.");
					DB.CharacterEvil = 0;
					DB.CharacterOriental = 0;
					DB.CharacterBarbaric = 1;
					Server.Items.BarbaricSatchel.GivePack( m );
				}
			}
        }
    }
}
