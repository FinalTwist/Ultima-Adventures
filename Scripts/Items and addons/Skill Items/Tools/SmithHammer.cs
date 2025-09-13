using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x0FB4, 0x0FB5 )]
	public class SmithHammer : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBlacksmithy.CraftSystem; } }

		[Constructable]
		public SmithHammer() : base( 0x0FB4 )
		{
			Name = "smith hammer";
			Weight = 8.0;
			Layer = Layer.OneHanded;
		}

		[Constructable]
		public SmithHammer( int uses ) : base( uses, 0x0FB4 )
		{
			Weight = 8.0;
			Layer = Layer.OneHanded;
		}

		public SmithHammer( Serial serial ) : base( serial )
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

			if ( ItemID != 0x0FB4 && ItemID != 0x0FB5 ){ ItemID = 0x0FB4; }
		}
	}
}