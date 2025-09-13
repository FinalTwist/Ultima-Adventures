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

namespace Server.Mobiles
{
	public class SquireNewNickname : Prompt
	{
		private Squire m_Squire;

		public SquireNewNickname( Squire squire )
		{
			m_Squire = squire;
		}

		public override void OnResponse( Mobile from, string text )
		{
			m_Squire.SetNickname( from, text );
		}
	}
	
	public class NewSquireNickname : Prompt
	{
		private Squire m_Squire;

		public NewSquireNickname( Squire squire )
		{
			m_Squire = squire;
		}

		public override void OnResponse( Mobile from, string text )
		{
			m_Squire.SetSNickname( from, text );
		}
	}
	
	public class SquirePack
	{
		public static bool CheckAccess( BaseCreature squire, Mobile from )
		{
			if ( from == squire || from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			if ( from.Alive && squire.Controlled && !squire.IsDeadPet && from == squire.ControlMaster )
				return true;

			return false;
		}

		public static void TryPackOpen( BaseCreature squire, Mobile from )
		{
			if ( squire.IsDeadPet )
				return;

			Container item = squire.Backpack;

			if ( CheckAccess( squire, from ) )
				if ( item != null )
					from.Use( item );
		}
	}
		
	public class SquireSpiritSpeakTimer : Timer
	{
		private Squire m_Squire;
		public SquireSpiritSpeakTimer( Squire squire ) : base( TimeSpan.FromMinutes( 2.0 ) )
		{
			m_Squire = squire;
			Priority = TimerPriority.FiveSeconds;
		}

		protected override void OnTick()
		{
			m_Squire.CanHearGhosts = false;
			m_Squire.m_SpiritWorldConnected = false;
			m_Squire.FixedParticles( 0x375A, 1, 15, 9501, 2100, 4, EffectLayer.Waist );
			if( m_Squire.m_SquireBeQuiet == false )
			{
				SquireDialog.DoSquireDialog( m_Squire.ControlMaster, m_Squire, SquireDialogTree.SpiritChannelFades, null, null );
			}
		}
	}
	
	public class SquireSpiritAnimTimer : Timer
	{
		private Squire m_Squire;
		public SquireSpiritAnimTimer( Squire squire ) : base( TimeSpan.FromSeconds( 0.1 ) )
		{
			m_Squire = squire;
			Priority = TimerPriority.FiftyMS;
		}

		protected override void OnTick()
		{
			m_Squire.Animate( 17, 7, 1, true, false, 0 );
		}
	}
	
	public class SquireQuiver
	{
		public static bool CheckAccess( BaseCreature squire, Mobile from )
		{
			if ( from == squire || from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			if ( from.Alive && squire.Controlled && !squire.IsDeadPet && from == squire.ControlMaster )
				return true;

			return false;
		}

		public static void TryPackOpen( BaseCreature squire, Mobile from )
		{
			if ( squire.IsDeadPet )
				return;
			
			Item QuiverCheck = squire.FindItemOnLayer( Layer.Cloak );
			
			if( QuiverCheck is BaseQuiver )
			{
				Container item = (Container)QuiverCheck;

				if ( CheckAccess( squire, from ) )
				{
					if ( item != null )
					{
						from.Use( item );
						if( ((Squire)squire).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)squire), SquireDialogTree.OpenQuiver, null, null );
						}
					}
				}
			}
			else
			{
				if( ((Squire)squire).m_SquireBeQuiet == false )
				{
					SquireDialog.DoSquireDialog( from, ((Squire)squire), SquireDialogTree.NotAQuiver, null, null );
				}
			}
		}
	}
	
	public class NewSquireTeam : Prompt // Added 1.9.7
	{
		private Squire m_Squire;

		public NewSquireTeam( Squire squire )
		{
			m_Squire = squire;
		}

		public override void OnResponse( Mobile from, string text )
		{
			m_Squire.SetSquireTeam( from, text );
		}
	}
}