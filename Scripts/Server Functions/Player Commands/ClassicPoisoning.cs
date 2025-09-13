using System;
using Server;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;

namespace Server.Items
{
    class ClassicPoison
    {
        public static void Initialize()
        {
            CommandSystem.Register("poisons", AccessLevel.Player, new CommandEventHandler(OnTogglePlayOriental));
        }

        [Usage("poisons")]
        [Description("Enables or disables the classic poisoning.")]
        private static void OnTogglePlayOriental(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( DB != null )
			{
				if ( DB.ClassicPoisoning == 1 )
				{
					m.SendMessage(38, "Poisons are now set for precision with special weapon infectious strikes.");
					DB.ClassicPoisoning = 0;
				}
				else
				{
					m.SendMessage(68, "Poisons are now set with hits from one-handed slashing or piercing weapons.");
					DB.ClassicPoisoning = 1;
				}
			}
        }
    }
}
