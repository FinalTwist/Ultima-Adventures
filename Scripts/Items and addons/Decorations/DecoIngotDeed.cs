using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Multis;
using Server.Items;

namespace Server.Items
{
	public class DecoStatueDeed : Item
	{
		public override string DefaultName
		{
			get { return "decoration ingot deed"; }
		}

		[Constructable]
		public DecoStatueDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
		}

		public DecoStatueDeed( Serial serial ) : base( serial )
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
				switch ( Utility.RandomMinMax( 0, 19 ) )
				{
					case 0: from.AddToBackpack(new DecoSilverIngots() ); break;
					case 1: from.AddToBackpack(new DecoSilverIngots2() ); break;
					case 2: from.AddToBackpack(new DecoSilverIngots3() ); break;
					case 3: from.AddToBackpack(new DecoSilverIngots4() ); break;
					case 4: from.AddToBackpack(new DecoSilverIngot2() ); break;
					case 5: from.AddToBackpack(new DecoSilverIngot() ); break;
					case 6: from.AddToBackpack(new DecoGoldIngot() ); break;
					case 7: from.AddToBackpack(new DecoGoldIngot2() ); break;
					case 8: from.AddToBackpack(new DecoGoldIngots() ); break;
					case 9: from.AddToBackpack(new DecoGoldIngots2() ); break;
					case 10: from.AddToBackpack(new DecoGoldIngots3() ); break;
					case 11: from.AddToBackpack(new DecoGoldIngots4() ); break;
					case 12: from.AddToBackpack(new DecoIronIngot() ); break;
					case 13: from.AddToBackpack(new DecoIronIngots2() ); break;
					case 14: from.AddToBackpack(new DecoIronIngots() ); break;
					case 15: from.AddToBackpack(new DecoIronIngots2() ); break;
					case 16: from.AddToBackpack(new DecoIronIngots3() ); break;
					case 17: from.AddToBackpack(new DecoIronIngots4() ); break;
					case 18: from.AddToBackpack(new DecoIronIngots5() ); break;
					case 19: from.AddToBackpack(new DecoIronIngots6() ); break;
				}

				this.Delete();
			}
		}
	}
}