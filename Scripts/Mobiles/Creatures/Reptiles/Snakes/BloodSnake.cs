using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Regions;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName("a giant snake corpse")]
	public class BloodSnake : BaseCreature
	{
		[Constructable]
		public BloodSnake() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 21;
			Hue = 0x5B5;
			Name = "a blood snake";
			BaseSoundID = 219;

			SetStr( 161, 360 );
			SetDex( 151, 300 );
			SetInt( 21, 40 );

			SetHits( 97, 216 );

			SetDamage( 5, 21 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.Poisoning, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 95.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 95.0 );
			SetSkill( SkillName.Wrestling, 85.1, 100.0 );

			Fame = 7000;
			Karma = -7000;

			VirtualArmor = 40;
			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 63.9;

			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );

			switch ( Utility.Random( 7 ) )
			{
				case 0: PackItem( new BatWing( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 1: PackItem( new NoxCrystal( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 2: PackItem( new GraveDust( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 3: PackItem( new PigIron( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 4: PackItem( new DaemonBlood( Utility.RandomMinMax( 1, 10 ) ) ); break;
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, 2 );
		}

		public override bool DeathAdderCharmable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int Hides{ get{ return 15; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }

		public override bool IsEnemy( Mobile m )
		{
			Region reg = Region.Find( m.Location, m.Map );

			if ( reg.IsPartOf( typeof( NecromancerRegion ) ) && m.Skills[SkillName.Necromancy].Base >= 50.0 )
				return false;

			if ( m is BaseCreature )
				return false;

			return true;
		}

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
				if ( !Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
				{
					DoHarmful( m );

					m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
					m.PlaySound( 0x231 );

					m.SendMessage( "You feel the blood drain from you!" );

					int toDrain = Utility.RandomMinMax( 10, 40 );

					Hits += toDrain;
					m.Damage( toDrain, this );
				}
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

		public BloodSnake(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			if ( BaseSoundID == -1 )
				BaseSoundID = 219;
		}
	}
}