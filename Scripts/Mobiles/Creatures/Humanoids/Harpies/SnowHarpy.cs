using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a snow harpy corpse" )]
	public class SnowHarpy : BaseCreature
	{
		private DateTime m_NextPickup;

		[Constructable]
		public SnowHarpy() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a snow harpy";
			Body = 30;
			BaseSoundID = 402;
			Hue = 1150;

			SetStr( 296, 320 );
			SetDex( 86, 110 );
			SetInt( 51, 75 );

			SetHits( 178, 192 );
			SetMana( 0 );

			SetDamage( 8, 16 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 50, 70 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 50.1, 65.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 70.1, 100.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 50;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.Gems, 2 );
		}

		public override int GetAttackSound()
		{
			return 916;
		}

		public override int GetAngerSound()
		{
			return 916;
		}

		public override int GetDeathSound()
		{
			return 917;
		}

		public override int GetHurtSound()
		{
			return 919;
		}

		public override int GetIdleSound()
		{
			return 918;
		}

		public override void OnThink()
		{
			base.OnThink();
			if ( DateTime.UtcNow < m_NextPickup )
				return;

			m_NextPickup = DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 10, 20 ) );

			Peace( Combatant );
		}

		private DateTime m_NextPeace;

		public void Peace( Mobile target )
		{
			if ( target == null || Deleted || !Alive || m_NextPeace > DateTime.UtcNow || 0.1 < Utility.RandomDouble() )
				return;

			PlayerMobile p = target as PlayerMobile;

			if ( p != null && p.PeacedUntil < DateTime.UtcNow && !p.Hidden && CanBeHarmful( p ) )
			{
				p.PeacedUntil = DateTime.UtcNow + TimeSpan.FromSeconds( 20.0 );
				p.SendLocalizedMessage( 500616 ); // You hear lovely music, and forget to continue battling!
				p.FixedParticles( 0x376A, 1, 32, 0x15BD, EffectLayer.Waist );
				p.Combatant = null;

				PlaySound( 0x58C );
			}

			m_NextPeace = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 4; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 50; } }

		public SnowHarpy( Serial serial ) : base( serial )
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