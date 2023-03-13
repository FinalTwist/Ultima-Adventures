using System;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Regions;
using Server.Spells.Necromancy;
using Server.Misc;

namespace Server.Spells.Fourth
{
	public class RecallSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Recall", "Kal Ort Por",
				239,
				9031,
				Reagent.BlackPearl,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

		private RunebookEntry m_Entry;
		private Runebook m_Book;

		public RecallSpell( Mobile caster, Item scroll ) : this( caster, scroll, null, null )
		{
		}

		public RecallSpell( Mobile caster, Item scroll, RunebookEntry entry, Runebook book ) : base( caster, scroll, m_Info )
		{
			m_Entry = entry;
			m_Book = book;
		}

		public override void GetCastSkills( out double min, out double max )
		{
			if ( TransformationSpellHelper.UnderTransformation( Caster, typeof( WraithFormSpell ) ) )
				min = max = 0;
			else if( Core.SE && m_Book != null )	//recall using Runebook charge
				min = max = 0;
			else
				base.GetCastSkills( out min, out max );
		}

		public override void OnCast()
		{
			if ( m_Entry == null )
				Caster.Target = new InternalTarget( this );
			else
				Effect( m_Entry.Location, m_Entry.Map, true );
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
			{
				Caster.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
				return false;
			}

			return SpellHelper.CheckTravel( Caster, TravelCheckType.RecallFrom );
		}

		public void Effect( Point3D loc, Map map, bool checkMulti )
		{
			string world = Worlds.GetMyWorld( map, loc, loc.X, loc.Y );
				
			if ( Caster.AccessLevel > AccessLevel.Player )  // Staff recall is never blocked
				{            
					BaseCreature.TeleportPets( Caster, loc, map, true );

					if ( m_Book != null )
						--m_Book.CurCharges;

					Caster.PlaySound( 0x1FC );
					Caster.MoveToWorld( loc, map );
					Caster.PlaySound( 0x1FC );
				}
			else if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.RecallFrom ) )
			{
			}
			else if ( Worlds.AllowEscape( Caster, Caster.Map, Caster.Location, Caster.X, Caster.Y ) == false )
			{
				Caster.SendMessage( "That spell does not seem to work in this place." );
			}
			else if ( Worlds.RegionAllowedRecall( Caster.Map, Caster.Location, Caster.X, Caster.Y ) == false )
			{
				Caster.SendMessage( "That spell does not seem to work in this place." );
			}
			else if ( Worlds.RegionAllowedTeleport( map, loc, loc.X, loc.Y ) == false )
			{
				Caster.SendMessage( "The destination seems magically unreachable." );
			}
			else if ( !SpellHelper.CheckTravel( Caster, map, loc, TravelCheckType.RecallTo ) )
			{
			}
			else if ( Caster is PlayerMobile && !CharacterDatabase.GetDiscovered( Caster, world ))
			{
				Caster.SendMessage( "You don't know this location and change your mind." );
			}
			else if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
			{
				Caster.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
			}

			else if ( (checkMulti && SpellHelper.CheckMulti( loc, map )) )
			{				
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( m_Book != null && m_Book.CurCharges <= 0 )
			{
				Caster.SendLocalizedMessage( 502412 ); // There are no charges left on that item.
			}
			else if ( CheckSequence() )
			{
				BaseCreature.TeleportPets( Caster, loc, map, false );

				if ( m_Book != null )
					--m_Book.CurCharges;

				if (Caster is PlayerMobile && Caster.Karma < -500 && ((PlayerMobile)Caster).Criminal && ( (Server.Misc.Worlds.GetRegionName( map, loc ) == "DarkMoor") || (Server.Misc.Worlds.GetRegionName( map, loc ) == "the Temple of Praetoria") ) && map == Map.Ilshenar)
					((PlayerMobile)Caster).Criminal = false;

				Caster.PlaySound( 0x1FC );
				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, 5024, 0 );
				Caster.MoveToWorld( loc, map );
				Caster.PlaySound( 0x1FC );
				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, 5024, 0 );
				
				AetherGlobe.ApplyCurse( Caster, Caster.Map, map, 1);
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private RecallSpell m_Owner;

			public InternalTarget( RecallSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.None )
			{
				m_Owner = owner;

				owner.Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 501029 ); // Select Marked item.
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					RecallRune rune = (RecallRune)o;

					if ( rune.Marked )
						m_Owner.Effect( rune.Target, rune.TargetMap, true );
					else
						from.SendLocalizedMessage( 501805 ); // That rune is not yet marked.
				}
				else if ( o is Runebook )
				{
					RunebookEntry e = ((Runebook)o).Default;

					if ( e != null )
						m_Owner.Effect( e.Location, e.Map, true );
					else
						from.SendLocalizedMessage( 502354 ); // Target is not marked.
				}
				else if ( o is Key && ((Key)o).KeyValue != 0 && ((Key)o).Link is BaseBoat )
				{
					BaseBoat boat = ((Key)o).Link as BaseBoat;

					if ( !boat.Deleted && boat.CheckKey( ((Key)o).KeyValue ) )
						m_Owner.Effect( boat.GetMarkedLocation(), boat.Map, false );
					else
						from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 502357, from.Name, "" ) ); // I can not recall from that object.
				}
				else if ( o is HouseRaffleDeed && ((HouseRaffleDeed)o).ValidLocation() )
				{
					HouseRaffleDeed deed = (HouseRaffleDeed)o;

					m_Owner.Effect( deed.PlotLocation, deed.PlotFacet, true );
				}
				else
				{
					from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 502357, from.Name, "" ) ); // I can not recall from that object.
				}
			}
			
			protected override void OnNonlocalTarget( Mobile from, object o )
			{
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
