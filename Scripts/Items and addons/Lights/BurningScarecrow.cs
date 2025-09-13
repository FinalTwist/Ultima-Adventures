using System;
using Server;

namespace Server.Items
{
	public class BurningScarecrowA : BaseLight
	{
		public override int LitItemID{ get { return 0x23AA; } }
		public override int UnlitItemID{ get { return 0x23A9; } }

		[Constructable]
		public BurningScarecrowA() : base( 0x23A9 )
		{
			Name = "burning scarecrow";
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1049644, "Double Click To Burn");
        } 

		public BurningScarecrowA( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class BurningScarecrowB : BaseLight
	{
		public override int LitItemID{ get { return 0x2401; } }
		public override int UnlitItemID{ get { return 0x2400; } }

		[Constructable]
		public BurningScarecrowB() : base( 0x2400 )
		{
			Name = "burning scarecrow";
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;
		}

		public BurningScarecrowB( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}