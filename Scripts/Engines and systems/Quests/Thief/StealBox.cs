using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Misc;

namespace Server.Items
{
	public class StealBox : WoodenBox
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
		public StealBox()
		{
			Hue = BoxColor;
			Name = BoxName;
		}

		public StealBox( Serial serial ) : base( serial )
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