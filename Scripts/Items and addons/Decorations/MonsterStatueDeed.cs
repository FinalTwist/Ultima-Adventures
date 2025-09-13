using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Multis;
using Server.Items;

namespace Server.Items
{
	public class MonsterStatueDeed : Item
	{
		public override string DefaultName
		{
			get { return "monster statuette deed"; }
		}

		[Constructable]
		public MonsterStatueDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
		}

		public MonsterStatueDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			int tempNumber = -1;
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				tempNumber = Utility.RandomMinMax(0,110);
				switch ( Utility.RandomMinMax( 0, 24 ) )
				{
					case 0: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Crocodile)); break;
					case 1: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Daemon)); break;
					case 2: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Dragon)); break;
					case 3: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.EarthElemental)); break;
					case 4: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Ettin)); break;
					case 5: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Gargoyle)); break;
					case 6: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Gorilla)); break;
					case 7: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Lich)); break;
					case 8: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Lizardman)); break;
					case 9: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Ogre)); break;
					case 10: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Orc)); break;
					case 11: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Ratman)); break;
					case 12: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Skeleton)); break;
					case 13: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Troll)); break;
					case 14: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Cow)); break;
					case 15: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Zombie)); break;
					case 16: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Llama)); break;
					case 17: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Ophidian)); break;
					case 18: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Reaper)); break;
					case 19: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Mongbat)); break;
					case 20: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Gazer)); break;
					case 21: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.FireElemental)); break;
					case 22: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Wolf)); break;
					case 23: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Harrower)); break;
					case 24: from.AddToBackpack(new MonsterStatuette(MonsterStatuetteType.Efreet)); break;
				}

				this.Delete();
			}
		}
	}
}