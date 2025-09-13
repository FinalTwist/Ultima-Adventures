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
	public class SquireRoom
	{
		public static void Initialize()
		{
			CommandSystem.Register( "SquireRoom", AccessLevel.GameMaster, new CommandEventHandler( SquireRoom_OnCommand ) );
		}

		[Usage( "SquireRoom" )]
		[Description( "Puts the Squire away as a Carrier Pigeon." )]
		private static void SquireRoom_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new SRoomTarget();
			e.Mobile.SendMessage( "Who's would you like to room?" );
		}

		private class SRoomTarget : Target
		{
			public SRoomTarget() : base( 12, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object s )
			{
				Container bank = from.FindBankNoCreate();
				
				if ( s == from )
				{
					from.SendMessage( "You cannot room yourself." );
				}
				else if ( s is PlayerMobile )
				{
					from.SendMessage( "That is not a Squire." );
				}
				else if ( !(s is Squire) )
				{
					from.SendMessage( "That is not a Squire." );
				}
				else
				{
					BaseCreature squi = ( BaseCreature )s;
					
					if ( squi.Mounted == true )
					{
						from.SendMessage( "Please dismount the Squire first, use [Dismount" );
					}
					else if ( squi.Summoned )
					{
						from.SendMessage( "How did you summon a squire with magic?" );
					}
					else if ( !squi.Alive )
					{
						from.SendMessage( "Please resurrect the Squire first." );
					}
					else
					{
						RoomCommandFunctions.Room( from, squi, true );
					}
				}
			}
		}
	}
	
	public class RoomCommandFunctions 
	{
		public static bool Room( Mobile from, object squi, bool restricted )
		{
			Squire s = ( Squire )squi;
			
			CarrierPigeon carrierPigeon = new CarrierPigeon( s );

			if ( from != null )
			{
				from.SendMessage( "The squire has been sent to a room." );
				if ( !from.AddToBackpack ( carrierPigeon ) )
				{
					carrierPigeon.MoveToWorld( new Point3D( from.X, from.Y, from.Z ), from.Map );
					from.SendMessage( "The carrier pigeon falls to the ground, as your backpack is too full." );
				}
			}
			else
			{
				carrierPigeon.MoveToWorld( new Point3D( s.X, s.Y, s.Z ), s.Map );
			}
			
			s.Controlled = true;
				
			GoToRoom( s );

			return true;
		}

		public static bool Room( Mobile from, object squi )
		{	
			return Room( from, squi, true );
		}

		public static bool Room( object squi )
		{
			return Room( null, squi, false );
		}

		private static void GoToRoom( Squire squire )
		{
			squire.SetControlMaster( null );
			squire.SummonMaster = null;
			squire.Internalize();
			squire.Controlled = true;
		}
	}
}
