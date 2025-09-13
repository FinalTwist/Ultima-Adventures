using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MarlinEastAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {17644, 0, 0, 0}, {17645, 0, 1, 0}, {17646, 0, -1, 0}// 1	2	3	
		};
    
		public override BaseAddonDeed Deed
		{
			get
			{
				return new MarlinEastAddonDeed();
			}
		}

		[ Constructable ]
		public MarlinEastAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public MarlinEastAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class MarlinEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new MarlinEastAddon();
			}
		}

		[Constructable]
		public MarlinEastAddonDeed()
		{
			Name = "Marlin Trophy (east)";
			ItemID = 0x4305;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1049644, "Double-Click To Place In Home"); // PARENTHESIS
        }

		public MarlinEastAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}