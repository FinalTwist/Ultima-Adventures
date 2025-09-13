using System;
using Server;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{
	[CorpseName( "a rotting puddle" )] // TODO: Corpse name?
	public class FailedExperiment : BaseUndead
	{
		[Constructable]
		public FailedExperiment() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; // Force full speed
			Name = "a viscous pile of flesh";
			Body = 775;
			BaseSoundID = 898;
			Hue = 768;
			CanInfect = true;

			SetStr( 61, 70 );
			SetDex( 61, 70 );
			SetInt( 10 );

			SetMana( 0 );

			SetDamage( 10, 30 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Poison, 100 );

			SetSkill( SkillName.Tactics, 50.0 );
			SetSkill( SkillName.Wrestling, 70.1, 90.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 24;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Gems );
		}

		public override void OnDeath( Container c )
		{

			if (Utility.RandomMinMax(1, 500) == 69)
				PackItem(new InfectionPotion());
			base.OnDeath( c );


		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public FailedExperiment( Serial serial ) : base( serial )
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
			
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; // Force full speed
		}
	}
}