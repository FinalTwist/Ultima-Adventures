using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a beetle corpse" )]
	public class GlowBeetleRiding : BaseMount
	{
		[Constructable]
		public GlowBeetleRiding() : this( "a glow beetle" )
		{
		}

		[Constructable]
		public GlowBeetleRiding( string name ) : base( name, 0xA9, 0x3E95, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.25, 0.5 )
		{
			BaseSoundID = 0x388;
			Hue = Utility.RandomList( 0xB33, 0xB34, 0xB35, 0xB36, 0xB37 );

			SetStr( 156, 180 );
			SetDex( 86, 105 );
			SetInt( 6, 10 );

			SetHits( 110, 150 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 90, 100 );

			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 75.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 26;

			AddItem( new LighterSource() );

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 39.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 4 ) == 1 )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "glowing goo" ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "glowing goo", 0xB93, 1 );
				}
			}
		}

		public GlowBeetleRiding( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 1170;
		}
	}
}