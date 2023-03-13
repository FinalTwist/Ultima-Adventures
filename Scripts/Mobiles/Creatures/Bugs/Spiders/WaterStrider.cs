using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a spider corpse" )]
	public class WaterStrider : BaseCreature
	{
		[Constructable]
		public WaterStrider() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a water strider";
			Body = 140;
			BaseSoundID = 0x388;
			Hue = 488;
			CanSwim = true;

			SetStr( 176, 200 );
			SetDex( 226, 245 );
			SetInt( 136, 160 );

			SetHits( 146, 160 );
			SetMana( 0 );

			SetDamage( 9, 20 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 80 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 55.1, 60.0 );
			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 70.1, 95.0 );

			Fame = 2175;
			Karma = -2175;

			VirtualArmor = 36; 
			PackItem( new SpidersSilk( 12 ) );
			Item Venom = new VenomSack();
				Venom.Name = "deadly venom sack";
				AddItem( Venom );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Average );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Arachnid; } }
		public override bool BleedImmune{ get{ return true; } }

		public override int GetAttackSound(){ return 0x601; }	// A
		public override int GetDeathSound(){ return 0x602; }	// D
		public override int GetHurtSound(){ return 0x603; }		// H

		public WaterStrider( Serial serial ) : base( serial )
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
			if ( BaseSoundID == 387 )
				BaseSoundID = 0x388;
		}
	}
}