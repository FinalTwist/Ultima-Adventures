using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Misc;

namespace Server.Items
{
	public class StealMetalBox : WoodenBox
	{
		public int BoxColor;
		public string BoxName;
		public string BoxMarkings;

		[CommandProperty(AccessLevel.Owner)]
		public int Box_Color { get { return BoxColor; } set { BoxColor = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Box_Name { get { return BoxName; } set { BoxName = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Box_Markings { get { return BoxMarkings; } set { BoxMarkings = value; InvalidateProperties(); } }

		[Constructable]
		public StealMetalBox()
		{
			Hue = BoxColor;
			Name = BoxName;
			Weight = 15.0;
			GumpID = 0x4B;
		}

		public override void Open( Mobile from )
		{
			if ( this.Weight > 10.0 )
			{
				int nStolen = Utility.RandomMinMax( 1, 15 );

				if ( nStolen == 1 )
				{
					Item i = Loot.RandomArty();
					DropItem(i);
					//BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				}
				else if ( nStolen == 2 )
				{
					Item i = DungeonLoot.RandomSlayer();
					DropItem(i);
					//BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				}
				else if ( nStolen < 5 )
				{
					Item i = Loot.RandomSArty();
					DropItem(i);
					//BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				}
				else if ( nStolen < 9 )
				{
					Item i = Loot.RandomRelic();
					DropItem(i);
					//BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				}
				else
				{
					Item idropped = DungeonLoot.RandomRare();
					if ( idropped is OilLeather || idropped is OilMetal ){ idropped.Amount = Utility.RandomMinMax( 1, 8 ); }
					else if ( idropped is MagicalDyes ){ idropped.Amount = Utility.RandomMinMax( 3, 10 ); }
					else if (idropped.Stackable == true){ idropped.Amount = Utility.RandomMinMax( 5, 20 ); }
					DropItem( idropped );
					//BaseContainer.DropItemFix( idropped, from, ItemID, GumpID );
				}

				int money = Utility.RandomMinMax( 1000, 4000 );

					double w = money * (MyServerSettings.GetGoldCutRate( null, this ) * .01);
					money = (int)w;
					Item g = new Gold( money );
					DropItem( g );
					//BaseContainer.DropItemFix( g, from, ItemID, GumpID );

				this.Weight = 10.0;
			}

			base.Open( from );
		}
		public StealMetalBox( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, BoxMarkings );
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( BoxColor );
            writer.Write( BoxName );
            writer.Write( BoxMarkings );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            BoxColor = reader.ReadInt();
            BoxName = reader.ReadString();
            BoxMarkings = reader.ReadString();
		}
	}
}