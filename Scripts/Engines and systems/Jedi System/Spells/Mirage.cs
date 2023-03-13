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

namespace Server.Spells.Jedi
{
	public class Mirage : JediSpell
	{
		public override int spellIndex { get { return 282; } }
		public int CirclePower = 3;
		public static int spellID = 282;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.75 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Jedi.JediSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Jedi.JediSpell.SpellInfo( spellID, 4 ) ),
				-1,
				0
			);

		public Mirage( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendMessage( "You have too many followers to create a mirage." );
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
				Caster.SendMessage( "You have too many followers to create a mirage." );
			}
			else if( TransformationSpellHelper.UnderTransformation( Caster, typeof( HorrificBeastSpell ) ) )
			{
				Caster.SendMessage( "You cannot create a mirage while you look like that." );
			}
			else if ( CheckSequence() && CheckFizzle() )
			{
				new JediMirage( Caster ).MoveToWorld( Caster.Location, Caster.Map );
				Caster.Hidden = true;
			}

			FinishSequence();
		}
	}
}

namespace Server.Mobiles
{
	public class JediMirage : BaseCreature
	{
		private Mobile m_Caster;

		public JediMirage( Mobile caster ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
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
				AddItem( JediMirageItem( caster.Items[i] ) );
			}

			Warmode = true;

			Summoned = true;
			SummonMaster = caster;

			int min = 60;
			int max = (int)(Server.Spells.Jedi.JediSpell.GetJediDamage( m_Caster ) );
				if ( max < min ){ max = min; }

			int hits = Utility.RandomMinMax( min, max );

			SetHits( hits );

			SetDamage( 1, 1 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 20, 40 );
			SetResistance( ResistanceType.Fire, 20, 40 );
			SetResistance( ResistanceType.Cold, 20, 40 );
			SetResistance( ResistanceType.Poison, 20, 40 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 1;

			ControlSlots = 3;

			TimeSpan duration = TimeSpan.FromSeconds( Server.Spells.Jedi.JediSpell.GetJediDamage( m_Caster ) / 2 );

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

		private Item JediMirageItem( Item item )
		{
			Item newItem = new Item( item.ItemID );
			newItem.Hue = item.Hue;
			newItem.Layer = item.Layer;

			return newItem;
		}

		public override bool DeleteCorpseOnDeath { get { return true; } }

		public override void OnDelete()
		{
			PlaySound( 0x0FD );
			this.FixedParticles( 0x375A, 10, 30, 5052, 0xB41, 0, EffectLayer.LeftFoot );
			base.OnDelete();
		}

		public override void OnAfterDelete()
		{
			MirrorImage.RemoveClone( m_Caster );
			base.OnAfterDelete();
		}

		public override bool IsDispellable { get { return false; } }
		public override bool Commandable { get { return false; } }

		public JediMirage( Serial serial ) : base( serial )
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