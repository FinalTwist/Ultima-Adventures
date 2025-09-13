using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Misc;
using Server.SkillHandlers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Server.Targeting;
using Server.ContextMenus;
using Server.HuePickers;
using Server.Prompts;

namespace Server.Commands
{
	public class SquireSkills
	{
		public static void Initialize()
		{
			CommandSystem.Register( "SquireSkills", AccessLevel.GameMaster, new CommandEventHandler( SquireStats_OnCommand ) );
		}

		[Usage( "SquireSkills" )]
		[Description( "Investigates the skills of a creature using the Squire Lore Gump window." )]
		private static void SquireStats_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new SSTarget();
			e.Mobile.SendMessage( "Who's skills do you wish to see?" );
		}

		private class SSTarget : Target
		{
			public SSTarget()
				: base( 15, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				bool done = false;
				if ( targ is PlayerMobile )
				{
					from.SendMessage( "You cannot use this gump to view PlayerMobile's skills." );
					return;
				}
				else if ( targ != null && targ is Squire )
				{
					from.SendGump( new SquireLoreGump( ((BaseCreature)targ), from, SquireLorePage.Skills ) );
					from.SendMessage( "Displaying Squire Lore Gump" );
					done = true;
				}

				if ( !done )
				{
					from.SendMessage( "Unable to display gump." );
				}
			}
		}
	}
}
