using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ExorcismScroll : SpellScroll
	{
		[Constructable]
		public ExorcismScroll() : this( 1 )
		{
		}

		[Constructable]
		public ExorcismScroll( int amount ) : base( 116, 0x2270, amount )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1049644, "Banishes demons and undead from the realm");
        } 

		public ExorcismScroll( Serial serial ) : base( serial )
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