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
	[CorpseName( "an evil corpse" )]
	public class Orphan : BaseChild
	{

		public bool captured;
		
		[Constructable]
		public Orphan()
			: base( AIType.AI_Fearful, FightMode.None, 10, 1, 0.2, 0.4 )
		{

			Title = "the Orphan";
			
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
			captured = false;
			
			type = 5;
			


		}

		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor, 1 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

		}

		public override void OnThink()
		{
			if ( ControlMaster != null && captured && Map == Map.Trammel && X > 3368 && Y > 1551 && X < 3373 && Y < 1554 && !(ControlMaster.X > 5328 && ControlMaster.Y > 1287 && ControlMaster.X < 5338 && ControlMaster.Y < 1294))
			{
				MoveToWorld(ControlMaster.Location, ControlMaster.Map);
			}
			if ( ControlMaster != null && captured && Map == Map.Trammel && X > 5239 && Y > 1097 && X < 5249 && Y < 1108 && !(ControlMaster.X > 2658 && ControlMaster.Y > 3330 && ControlMaster.X < 2668 && ControlMaster.Y < 3338))
			{
				MoveToWorld(ControlMaster.Location, ControlMaster.Map);
			}
		}
		
		
		public override bool OnBeforeDeath()
		{

			return base.OnBeforeDeath();
		}


		public Orphan( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
			writer.Write( (bool) captured );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			bool captured = reader.ReadBool();

		}
	}
}
