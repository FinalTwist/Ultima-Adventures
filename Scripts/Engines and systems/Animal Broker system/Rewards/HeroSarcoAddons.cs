using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Targeting;
using Server.Misc;
using Server.Accounting;
using System.Xml;
using Server.Mobiles; 

namespace Server.Items
{

	public class HeroSarcoWE : BaseAddon
	{

		public override BaseAddonDeed Deed { get { return new HeroSarcoWEDeed(); } }

		private bool named;

		[Constructable]
		public HeroSarcoWE() : base( 0x14F0 )
		{
			
			AddComponent( new AddonComponent( 0x1C62 ), -2, 0, 2 );
			AddComponent( new AddonComponent( 0x1CC5 ), -1, 0, 2 );
			AddComponent( new AddonComponent( 0x1C60 ), 0, 0, 2 );
			AddComponent( new AddonComponent( 0x1C65 ), 0, -1, 2 );
			AddComponent( new AddonComponent( 0x1C64 ), -1, -1, 2 );
			AddComponent( new AddonComponent( 0x1C63 ), -2, -1, 2 );

			base.Weight = 0;
			Movable = false;
			named = false;
			base.Name = "double click to name";
		}	

		public override void OnDoubleClick( Mobile from )
		{
			if (named)
				return;
			else 
			{
				from.CloseGump(typeof(SarcoNameGump));
				from.SendGump(new SarcoNameGump(from, this));
			}

			
		}
		
		public HeroSarcoWE( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (bool) named );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			named = reader.ReadBool();
		}
		
	}

	public class HeroSarcoWEDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HeroSarcoWE(); } }

		[Constructable]
		public HeroSarcoWEDeed()
		{
			Name = "Hero Sarcophagus West";
		}

		public HeroSarcoWEDeed( Serial serial ) : base( serial )
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

	public class HeroSarcoNS : BaseAddon
	{

		private static int[,] m_AddOnSimpleComponents = new int[,] {
			{7337, 0, -2, 1}, {7336, 0, -1, 1}, {7335, 0, 0, 1},
			{7338, -1, -2, 1}, {7339, -1, -1, 1}, {7340, -1, 0, 1}
		};

		public override BaseAddonDeed Deed { get { return new PeaPatchDeed(); } }

		private bool named;

		[Constructable]
		public HeroSarcoNS() : base( 0x14F0 )
		{
			for (int i = 0; i < m_AddOnSimpleComponents.Length / 3; i++)
				AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );

			base.Weight = 0;
			Movable = false;
			named = false;
			base.Name = "double click to name";
		}		

		public override void OnDoubleClick( Mobile from )
		{
			if (named)
				return;
			else 
			{
				from.CloseGump(typeof(SarcoNameGump));
				from.SendGump(new SarcoNameGump(from, (Item)this));
			}

			
		}
		
		public HeroSarcoNS( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (bool) named );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			named = reader.ReadBool();
		}
		
	}
}
