using System;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Items;

namespace Server.Spells.Herbalist
{
	public class ProtectiveFairySpell : HerbalistSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override int HerbalistSpellCircle{ get{ return 4; } }
		public override double CastDelay{ get{ return 3.0; } }
		public override double RequiredSkill{ get{ return 70.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }

		public ProtectiveFairySpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			else if ( Caster.Skills[CastSkill].Value < RequiredSkill )
			{
				Caster.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You lack the understanding to even attract a fairy.", Caster.NetState);
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				TimeSpan duration = TimeSpan.FromSeconds( ( Caster.Skills[SkillName.AnimalLore].Value + Caster.Skills[SkillName.AnimalTaming].Value ) * 9 );

				SpellHelper.Summon( new DruidFairy(), Caster, 0x217, duration, false, false );
			}

			FinishSequence();
		}
	}
}

namespace Server.Mobiles
{
	[CorpseName( "a fairy corpse" )]
	public class DruidFairy : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public DruidFairy() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fairy";
			Body = 58;
			BaseSoundID = 466;
			Hue = 0x9FF;

			SetStr( 100 );
			SetDex( 150 );
			SetInt( 150 );

			SetHits( 200 );
			SetStam( 300 );
			SetMana( 300 );

			SetDamage( 6, 9 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 70, 80 );

			SetSkill( SkillName.Meditation, 90.0 );
			SetSkill( SkillName.EvalInt, 70.0 );
			SetSkill( SkillName.Magery, 70.0 );
			SetSkill( SkillName.MagicResist, 60.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 80.0 );

			VirtualArmor = 30;
			ControlSlots = 2;
		}

		public DruidFairy( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}