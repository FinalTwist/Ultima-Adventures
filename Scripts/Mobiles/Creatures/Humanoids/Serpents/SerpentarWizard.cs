using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a snake man corpse" )]
	public class SerpentarWizard : BaseCreature
	{
		[Constructable]
		public SerpentarWizard() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a serpentar wizard";
			Body = 372;
			BaseSoundID = 639;

			SetStr( 281, 305 );
			SetDex( 191, 215 );
			SetInt( 226, 250 );

			SetHits( 169, 183 );
			SetStam( 36, 45 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 45 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 35, 40 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.EvalInt, 95.1, 100.0 );
			SetSkill( SkillName.Magery, 95.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );

			Fame = 11500;
			Karma = -11500;

			VirtualArmor = 44;

			PackReg( 5, 15 );
			PackNecroReg( 5, 15 );

			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		public SerpentarWizard( Serial serial ) : base( serial )
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