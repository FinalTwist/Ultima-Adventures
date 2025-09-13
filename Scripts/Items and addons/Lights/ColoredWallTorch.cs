using System;
using Server;

namespace Server.Items
{
	[Flipable]
	public class ColoredWallTorch : BaseLight
	{
		public override int LitItemID
		{
			get
			{
				if ( ItemID == 0x3D89 )
					return 0x3D77;
				else
					return 0x3D74;
			}
		}
		
		public override int UnlitItemID
		{
			get
			{
				if ( ItemID == 0x3D77 )
					return 0x3D89;
				else
					return 0x3D88;
			}
		}
		
		[Constructable]
		public ColoredWallTorch() : base( 0x3D89 )
		{
			Name = "wall torch";
			Movable = true;
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.WestBig;
			Weight = 3.0;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1049644, "Dye To Color Flame");
        } 

		public ColoredWallTorch( Serial serial ) : base( serial )
		{
		}

		public void Flip()
		{
			if ( Light == LightType.WestBig )
				Light = LightType.NorthBig;
			else if ( Light == LightType.NorthBig )
				Light = LightType.WestBig;

			switch ( ItemID )
			{
				case 0x3D89: ItemID = 0x3D88; break;
				case 0x3D77: ItemID = 0x3D74; break;

				case 0x3D88: ItemID = 0x3D89; break;
				case 0x3D74: ItemID = 0x3D77; break;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );
			writer.Write( (int) ItemID);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			if (version == 1)
				ItemID = reader.ReadInt();
		}
	}
}