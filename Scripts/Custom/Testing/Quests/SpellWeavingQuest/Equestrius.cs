using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "Equestrius' corpse" )]
	public class Equestrius : BaseCreature
	{
		[Constructable]
		public Equestrius () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Equestrius";
		        Body = 101;
			BaseSoundID = 679;

			SetStr( 488, 620 );
			SetDex( 121, 170 );
			SetInt( 498, 657 );

			SetHits( 312, 353 );

			SetDamage( 18, 28 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );
			SetSkill( SkillName.Anatomy, 95.1, 115.0 );
			SetSkill( SkillName.Archery, 95.1, 100.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 60;

			AddItem( new Bow() );
			PackItem( new Arrow( Utility.RandomMinMax( 80, 90 ) ) );
			//if( Utility.RandomDouble() <= 0.01 ) PackItem( new CarvedWoodenScreen2() );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.MedScrolls, 5 );
                        AddLoot( LootPack.Gems, 5 );
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 8; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
				m.PlaySound( 0x231 );

				m.SendMessage( "You feel the life drain out of you!" );

				int toDrain = Utility.RandomMinMax( 10, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
			
			c.DropItem( new AmanitaMuscaria() );
		}

		public Equestrius( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 678 )
				BaseSoundID = 679;
		}
	}
}