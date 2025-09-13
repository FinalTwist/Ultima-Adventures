using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName("a magma snake corpse")]
	[TypeAlias( "Server.Mobiles.Lavasnake" )]
	public class LavaSnake : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public LavaSnake() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a magma snake";
			Hue = 0xB17;
			Body = 21;
			BaseSoundID = 219;

			SetStr( 216, 245 );
			SetDex( 26, 50 );
			SetInt( 66, 85 );

			SetHits( 130, 147 );
			SetMana( 0 );

			SetDamage( 7, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Anatomy, 27.5, 50.0 );
			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 75.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 3500;
			Karma = -3500;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 63.9;

			VirtualArmor = 32;

			PackItem( new SulfurousAsh() );
			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool DeathAdderCharmable{ get{ return true; } }
		public override int Meat{ get{ return 4; } }
		public override int Hides{ get{ return 15; } }
		public override HideType HideType{ get{ return HideType.Volcanic; } }

		public override FoodType FavoriteFood{ get{ return FoodType.Fire; } }
		public override bool CanAngerOnTame { get { return true; } }

		public LavaSnake(Serial serial) : base(serial)
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
		}
	}
}