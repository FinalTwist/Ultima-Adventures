using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class EmptyCanopicJar : Item
	{
		public string RelicOwner;

		[CommandProperty(AccessLevel.Owner)]
		public string Relic_Owner { get { return RelicOwner; } set { RelicOwner = value; InvalidateProperties(); } }

		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

        [Constructable]
        public EmptyCanopicJar() : base(0x2FEE)
		{
			ItemID = Utility.RandomList( 0x2FEE, 0x2FEF, 0x2FF0, 0x2FF1 );
			Weight = 5.0;

			if ( Weight < 10.0 )
			{
				RelicGoldValue = Server.Misc.RelicItems.RelicValue();
				Weight = 10.0;

				string who = NameList.RandomName( "drakkul" );
				string title = "Pharaoh";
				string era = "First";

				switch ( Utility.RandomMinMax( 0, 4 ) )
				{
					case 0: title = "Pharaoh"; break;
					case 1: title = "King"; break;
					case 2: title = "Queen"; break;
					case 3: title = "Priest"; break;
					case 4: title = "Priestess"; break;
				}

				switch ( Utility.RandomMinMax( 0, 12 ) )
				{
					case 0: era = "first"; break;
					case 1: era = "second"; break;
					case 2: era = "third"; break;
					case 3: era = "fourth"; break;
					case 4: era = "fifth"; break;
					case 5: era = "sixth"; break;
					case 6: era = "seventh"; break;
					case 7: era = "eighth"; break;
					case 8: era = "ninth"; break;
					case 9: era = "tenth"; break;
					case 10: era = "eleventh"; break;
					case 11: era = "twelfth"; break;
					case 12: era = "thirteenth"; break;
				}

				Name = "canopic jar from the " + era + " dynasty";
				RelicOwner = "belonged to " + who + " the " + title;
			}
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, RelicOwner);
        } 

        public EmptyCanopicJar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( RelicOwner );
            writer.Write( RelicGoldValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            RelicOwner = reader.ReadString();
            RelicGoldValue = reader.ReadInt();
	    }
    }
}