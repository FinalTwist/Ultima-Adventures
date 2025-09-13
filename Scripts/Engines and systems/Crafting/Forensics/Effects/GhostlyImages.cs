using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;
using Server.Items;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;

namespace Server.Spells.Undead
{
	public class GhostlyImagesSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override double RequiredSkill{ get{ return 45.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		public GhostlyImagesSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( (Caster.Followers + (Core.SE ? 2 : 1)) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop( ref p );

			if ( map == null || !map.CanSpawnMobile( p.X, p.Y, p.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				Point3D from = Caster.Location;
				TimeSpan duration;
				duration = TimeSpan.FromSeconds( ( Caster.Skills[SkillName.Necromancy].Value + Caster.Skills[SkillName.SpiritSpeak].Value ) / 2 );
				BaseCreature.Summon( new dj_nc_decoy( Caster ), false, Caster, new Point3D( p ), 0x657, duration );
				Effects.SendLocationParticles( EffectItem.Create( new Point3D( p ), Caster.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );
				Effects.SendLocationParticles( EffectItem.Create( from, Caster.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );
				Caster.Hidden = true;
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private GhostlyImagesSpell m_Owner;

			public InternalTarget( GhostlyImagesSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetOutOfLOS( Mobile from, object o )
			{
				from.SendLocalizedMessage( 501943 ); // Target cannot be seen. Try again.
				from.Target = new InternalTarget( m_Owner );
				from.Target.BeginTimeout( from, TimeoutTime - DateTime.UtcNow );
				m_Owner = null;
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_Owner != null )
					m_Owner.FinishSequence();
			}
		}
	}
}

namespace Server.Mobiles
{
	public class dj_nc_decoy : BaseCreature
	{
		private Mobile m_Caster;
		public override bool DeleteCorpseOnDeath { get { return Summoned; } }

		public dj_nc_decoy( Mobile caster ) : base( AIType.AI_Melee, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			m_Caster = caster;

			Body = caster.Body;

			Hue = 0x47E;
			Female = caster.Female;

			Name = caster.Name;
			NameHue = caster.NameHue;

			Title = caster.Title;
			Kills = caster.Kills;
			RawStr = caster.Hits;
			Str = caster.Hits;
			Hits = caster.Hits;

			HairItemID = caster.HairItemID;
			HairHue = 0x47E;

			FacialHairItemID = caster.FacialHairItemID;
			FacialHairHue = 0x47E;

			for ( int i = 0; i < caster.Skills.Length; ++i )
			{
				Skills[i].Base = caster.Skills[i].Base;
				Skills[i].Cap = caster.Skills[i].Cap;
			}

			for( int i = 0; i < caster.Items.Count; i++ )
			{
				AddItem( dj_nc_decoyItem( caster.Items[i] ) );
			}

			Warmode = true;

			Fame = 0;
			Karma = 0;

			VirtualArmor = 0;
			ControlSlots = ( Core.SE ) ? 2 : 1;
		}

		private Item dj_nc_decoyItem( Item item )
		{
			Item newItem = new Item( item.ItemID );
			newItem.Hue = 0x47E;
			newItem.Layer = item.Layer;

			return newItem;
		}

		public override bool IsDispellable { get { return false; } }
		public override bool Commandable { get { return false; } }

		public dj_nc_decoy( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}