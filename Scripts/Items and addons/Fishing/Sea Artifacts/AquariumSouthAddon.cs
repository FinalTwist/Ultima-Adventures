using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class AquariumSouthAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {15694, -1, 0, 0}, {15698, 0, 0, 0}, {15683, 1, 0, 0}// 1	2	3	
					};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new AquariumSouthAddonDeed();
			}
		}

		[ Constructable ]
		public AquariumSouthAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public AquariumSouthAddon( Serial serial ) : base( serial )
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

	public class AquariumSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new AquariumSouthAddon();
			}
		}

		[Constructable]
		public AquariumSouthAddonDeed()
		{
			Name = "aquarium (south)";
			ItemID = 0x22BD;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public AquariumSouthAddonDeed( Serial serial ) : base( serial )
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