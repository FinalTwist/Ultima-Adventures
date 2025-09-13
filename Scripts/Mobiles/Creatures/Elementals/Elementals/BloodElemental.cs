using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "an elemental corpse" )]
	public class BloodElemental : BaseCreature
	{
		[Constructable]
		public BloodElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a blood elemental";
			Body = 16;
			Hue = 37;
			BaseSoundID = 278;

			SetStr( 526, 615 );
			SetDex( 66, 85 );
			SetInt( 226, 350 );

			SetHits( 316, 369 );

			SetDamage( 17, 27 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Poison, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.Meditation, 10.4, 50.0 );
			SetSkill( SkillName.MagicResist, 80.1, 95.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 12500;
			Karma = -12500;

			VirtualArmor = 60;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
		}

		public override int TreasureMapLevel{ get{ return 5; } }

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 4 ) == 1 && this.Fame > 12500 )
			{
				int goo = 0;

				string Goo = "thick blood";
				int Color = 0x485;

				if ( this.Name == "a coolant elemental" ){ Goo = "engine coolant"; Color = 0xB73; }
				else if ( this.Name == "a contaminated elemental" ){ Goo = "contamination"; Color = 0xBAD; }

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == Goo ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, Goo, Color, 0 );
				}
			}
		}

		public BloodElemental( Serial serial ) : base( serial )
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