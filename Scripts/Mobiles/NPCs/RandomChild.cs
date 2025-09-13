using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Spells;
using Server.Network;
using System.Collections.Generic;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
	[CorpseName( "an small corpse" )]
	public class RandomChild : BaseChild
	{

		[Constructable]
		public RandomChild()
			: base( AIType.AI_Fearful, FightMode.None, 10, 1, 0.2, 0.4 )
		{

			SetStr( 4, 5 );
			SetDex( 40, 60 );
			SetInt( 1, 2 );

			SetHits( 100 );

			SetDamage( 0, 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 85 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 55, 85 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 65, 75 );

			Fame = 10;
			Karma = 10;

			VirtualArmor = 80;


		}
		
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor, 1 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

		}
		
		public override bool OnBeforeDeath()
		{

			return base.OnBeforeDeath();
		}


		public RandomChild( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}
	}
}
