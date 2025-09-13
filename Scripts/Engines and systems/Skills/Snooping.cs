using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Regions;
using System.Collections;
using System.Collections.Generic;


namespace Server.SkillHandlers
{
	public class Snooping
	{
		public static void Configure()
		{
			Container.SnoopHandler = new ContainerSnoopHandler( Container_Snoop );
		}
		
		static List<Mobile> snoopingtargets = new List<Mobile>();

		public static bool CheckSnoopAllowed( Mobile from, Mobile to )
		{
			Map map = from.Map;

			if (!Snooping.CheckSnoopingTarget( to, false ) )
			{
				return false;
				from.SendMessage( "The target is guarding his posessions." );
			}

			if ( from is Citizens )
				return true;

			if ( from.Blessed )
			{
				from.SendMessage( "You cannot snoop while in this state." );
				return false;
			}

			if ( to.Player )
				return from.CanBeHarmful( to, false, true ); // normal restrictions

			if ( map != null && (map.Rules & MapRules.HarmfulRestrictions) == 0 )
				return true; // felucca you can snoop anybody

			BaseCreature cret = to as BaseCreature;

			if ( to.Body.IsHuman && (cret == null || (!cret.AlwaysAttackable && !cret.AlwaysMurderer)) )
				return false; // in town we cannot snoop blue human npcs

			return true;
		}

		public static bool CheckSnoopingTarget (Mobile target, bool check)
		{
			if (snoopingtargets == null)
                snoopingtargets = new List<Mobile>();

			if (check)
			{
				snoopingtargets.Add( target );
				return false;
			}
			else 
			{
				for ( int i = 0; i < snoopingtargets.Count; i++ ) // check if mobile is in list
				{			
					Mobile m = (Mobile)snoopingtargets[i];
					if (m == target) //already in the list
						return false;
				}
				
				return true; //not in the list, proceed
			}
		}
		
		public static void WipeSnoopingList ()
		{
			Snooping.snoopingtargets.Clear();

		}

		public static void Container_Snoop( Container cont, Mobile from )
		{
			if ( from.AccessLevel > AccessLevel.Player || from.InRange( cont.GetWorldLocation(), 1 ) )
			{
				Mobile root = cont.RootParent as Mobile;

				if ( root != null && !root.Alive )
					return;

				if ( cont.ParentEntity is Citizens )
				{
					cont.DisplayTo( from );
					return;
				}

				if ( root != null && root.AccessLevel > AccessLevel.Player && from.AccessLevel == AccessLevel.Player )
				{
					from.SendLocalizedMessage( 500209 ); // You can not peek into the container.
					return;
				}

				if ( root != null && from.AccessLevel == AccessLevel.Player && !CheckSnoopAllowed( from, root ) )
				{
					from.SendLocalizedMessage( 1001018 ); // You cannot perform negative acts on your target.
					return;
				}

				if ( root != null && from.AccessLevel == AccessLevel.Player && from.Skills[SkillName.Snooping].Value < Utility.Random( 100 ) )
				{
					Map map = from.Map;

					if ( map != null )
					{
						string message = String.Format( "You notice {0} attempting to peek into {1}'s belongings.", from.Name, root.Name );

						IPooledEnumerable eable = map.GetClientsInRange( from.Location, 8 );

						foreach ( NetState ns in eable )
						{
							if ( ns.Mobile != from )
								ns.Mobile.SendMessage( message );
						}

						eable.Free();
					}
				}

				if ( from.AccessLevel == AccessLevel.Player )
					Titles.AwardKarma( from, -4, true );

				if ( from.AccessLevel > AccessLevel.Player || from.CheckTargetSkill( SkillName.Snooping, cont, Utility.RandomMinMax(0, 60), 100.0 ) )
				{
					if ( cont is TrapableContainer && ((TrapableContainer)cont).ExecuteTrap( from ) )
						return;

					cont.GumpID = 60;
					if ( root is PlayerMobile )
					{
						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( root );
						if ( DB != null && DB.Hue == 3 )
						{
							cont.GumpID = 10913;
						}
					}

					cont.DisplayTo( from );
				}
				else
				{
					from.SendLocalizedMessage( 500210 ); // You failed to peek into the container.
					
					if (Utility.RandomDouble() < 0.05)
						Snooping.CheckSnoopingTarget( root, true);
					
					if ( from.Skills[SkillName.Hiding].Value / 2 < Utility.Random( 100 ) )
						from.RevealingAction();
				}
			}
			else
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
			}
		}
	}
}