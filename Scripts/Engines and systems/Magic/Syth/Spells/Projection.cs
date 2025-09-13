using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;

namespace Server.Spells.Syth
{
	public class Projection : SythSpell
	{
		public override int spellIndex { get { return 272; } }
		public int CirclePower = 3;
		public static int spellID = 272;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.75 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Syth.SythSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Syth.SythSpell.SpellInfo( spellID, 4 ) ),
				-1,
				0
			);

		public Projection( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendMessage( "You have too many followers to creat a projection." );
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( Caster.Mounted )
			{
				Caster.SendLocalizedMessage( 1063132 ); // You cannot use this ability while mounted.
			}
			else if ( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendMessage( "You have too many followers to be SythProjectioning around." );
			}
			else if( TransformationSpellHelper.UnderTransformation( Caster, typeof( HorrificBeastSpell ) ) )
			{
				Caster.SendMessage( "You cannot summon a projection while you look like that." );
			}
			else if ( CheckSequence() && CheckFizzle() )
			{
				new SythProjection( Caster ).MoveToWorld( Caster.Location, Caster.Map );
				Caster.Hidden = true;
			}

			FinishSequence();
		}
	}
}

namespace Server.Mobiles
{
	public class SythProjection : BaseCreature
	{
		private Mobile m_Caster;

		public SythProjection( Mobile caster ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			m_Caster = caster;

			Body = caster.Body;

			Hue = caster.Hue;
			Female = caster.Female;

			Name = caster.Name;
			NameHue = caster.NameHue;

			Title = caster.Title;
			Kills = caster.Kills;

			HairItemID = caster.HairItemID;
			HairHue = caster.HairHue;

			FacialHairItemID = caster.FacialHairItemID;
			FacialHairHue = caster.FacialHairHue;

			for ( int i = 0; i < caster.Skills.Length; ++i )
			{
				Skills[i].Base = caster.Skills[i].Base;
				Skills[i].Cap = caster.Skills[i].Cap;
			}

			for( int i = 0; i < caster.Items.Count; i++ )
			{
				AddItem( SythProjectionItem( caster.Items[i] ) );
			}

			Warmode = true;

			Summoned = true;
			SummonMaster = caster;

			int min = 60;
			int max = (int)(Server.Spells.Syth.SythSpell.GetSythDamage( m_Caster ) );
				if ( max < min ){ max = min; }

			int hits = Utility.RandomMinMax( min, max );

			SetHits( hits );

			SetDamage( 1, 1 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 1, 2 );
			SetResistance( ResistanceType.Fire, 1, 2 );
			SetResistance( ResistanceType.Cold, 1, 2 );
			SetResistance( ResistanceType.Poison, 1, 2 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 1;

			ControlSlots = 3;

			TimeSpan duration = TimeSpan.FromSeconds( Server.Spells.Syth.SythSpell.GetSythDamage( m_Caster ) / 2 );

			new UnsummonTimer( caster, this, duration ).Start();
			SummonEnd = DateTime.UtcNow + duration;

			MirrorImage.AddClone( m_Caster );
		}

		public override bool IsHumanInTown() { return false; }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			if ( m_Caster != null ){ m_Caster.DoHarmful( defender ); }
		}

		private Item SythProjectionItem( Item item )
		{
			Item newItem = new Item( item.ItemID );
			newItem.Hue = item.Hue;
			newItem.Layer = item.Layer;

			return newItem;
		}

		public override bool DeleteCorpseOnDeath { get { return true; } }

		public override void OnDelete()
		{
			PlaySound( 0x3EA );
			this.FixedParticles( 0x3709, 10, 30, 5052, 0xB00, 0, EffectLayer.LeftFoot );
			base.OnDelete();
		}

		public override void OnAfterDelete()
		{
			MirrorImage.RemoveClone( m_Caster );
			base.OnAfterDelete();
		}

		public override bool IsDispellable { get { return false; } }
		public override bool Commandable { get { return false; } }

		public SythProjection( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
			writer.Write( m_Caster );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
			m_Caster = reader.ReadMobile();
			MirrorImage.AddClone( m_Caster );
		}
	}
}