using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{

	public class MagicClothDeed : Item
	{

		[Constructable]
		public MagicClothDeed() : this( null )
		{
		}

		[Constructable]
		public MagicClothDeed ( string name ) : base ( 0x14F0 )
		{
			Name = "Magical Cloth Deed";
			Hue = 86;
		}

		public MagicClothDeed ( Serial serial ) : base ( serial )
		{
		}

      		public override void OnDoubleClick( Mobile from ) 
      		{
			if ( !IsChildOf( from.Backpack ) )
			{
                from.SendLocalizedMessage(1042001);
            }
            else
            {
                switch (Utility.Random(19))
                {
                    case 0: from.AddToBackpack(new MagicBodySash()); break;
                    case 1: from.AddToBackpack(new MagicCloak()); break;
                    case 2: from.AddToBackpack(new MagicDoublet()); break;
                    case 3: from.AddToBackpack(new MagicFancyDress()); break;
                    case 4: from.AddToBackpack(new MagicFancyShirt()); break;
                    case 5: from.AddToBackpack(new MagicFullApron()); break;
                    case 6: from.AddToBackpack(new MagicHalfApron()); break;
                    case 7: from.AddToBackpack(new MagicJesterSuit()); break;
                    case 8: from.AddToBackpack(new MagicKilt()); break;
                    case 9: from.AddToBackpack(new MagicLongPants()); break;
                    case 10: from.AddToBackpack(new MagicPlainDress()); break;
                    case 11: from.AddToBackpack(new MagicRobe()); break;
                    case 12: from.AddToBackpack(new MagicSandals()); break;
                    case 13: from.AddToBackpack(new MagicShirt()); break;
                    case 14: from.AddToBackpack(new MagicShoes()); break;
                    case 15: from.AddToBackpack(new MagicShortPants()); break;
                    case 16: from.AddToBackpack(new MagicSkirt()); break;
                    case 17: from.AddToBackpack(new MagicSurcoat()); break;
                    case 18: from.AddToBackpack(new MagicTunic()); break;
                    
                }
                this.Delete();
			}

		}

		public override void Serialize ( GenericWriter writer)
		{
			base.Serialize ( writer );

			writer.Write ( (int) 0);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize ( reader );

			int version = reader.ReadInt();
		}
	}
}