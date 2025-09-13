using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a harpy corpse" )]
	public class HarpyElder : BaseCreature
	{
		private DateTime m_NextPickup;

		[Constructable]
		public HarpyElder() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an elder harpy";
			Body = 153;
			BaseSoundID = 402;

			SetStr( 296, 325 );
			SetDex( 86, 105 );
			SetInt( 291, 385 );

			SetHits( 178, 195 );

			SetDamage( 8, 19 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 62.0, 100.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 115.1, 130.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 12500;
			Karma = -12500;

			VirtualArmor = 50;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( Utility.RandomMinMax( 1, 10 ) );
				PackItem( egg );
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
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

		public HarpyElder( Serial serial ) : base( serial )
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