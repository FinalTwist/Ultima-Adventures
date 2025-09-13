using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HalloweenDeco6 : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {3178, 0, 0, 0}// 1	
		};

		[ Constructable ]
		public HalloweenDeco6()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public HalloweenDeco6( Serial serial ) : base( serial )
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
}