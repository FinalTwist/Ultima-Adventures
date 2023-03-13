using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MarlinSouthAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {17641, 0, 0, 0}, {17643, 1, 0, 0}, {17642, -1, 0, 0}// 1	2	3	
		};
 
		public override BaseAddonDeed Deed
		{
			get
			{
				return new MarlinSouthAddonDeed();
			}
		}

		[ Constructable ]
		public MarlinSouthAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public MarlinSouthAddon( Serial serial ) : base( serial )
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

	public class MarlinSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new MarlinSouthAddon();
			}
		}

		[Constructable]
		public MarlinSouthAddonDeed()
		{
			Name = "Marlin Trophy (south)";
			ItemID = 0x4304;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1049644, "Double-Click To Place In Home"); // PARENTHESIS
        }

		public MarlinSouthAddonDeed( Serial serial ) : base( serial )
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