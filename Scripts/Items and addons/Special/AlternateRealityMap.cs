using System;
using Server.Network;
using Server.Gumps;

namespace Server.Items
{
	[FlipableAttribute( 0x4D07, 0x4D08 )] 
	public class AlternateRealityMap : Item
	{
		[Constructable]
		public AlternateRealityMap() : base( 0x4D08 )
		{
			Weight = 5.0;
			Name = "Map of an Alternate Reality";
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 5528);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Use The Map To Examine It");
        }

		public AlternateRealityMap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}