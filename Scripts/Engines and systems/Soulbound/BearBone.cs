using System;
using Server;

namespace Server.Items
{
	public class BearBone : PhylacteryComponent
	{
		public override int LabelNumber{ get{ return 1061203; } } // bear bone

		[Constructable]
		public BearBone() : this( 1 )
		{
		}

		[Constructable]
		public BearBone( int amount )
		{
			BoundEssence = "BearEssence";
			ComponentType = ComponentType.Regular;
			
			Amount = amount;
			ItemID =  0x0F7E;
			Light = LightType.Circle150;
		}

		public BearBone( Serial serial ) : base( serial )
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
			Light = LightType.Circle150;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1061200, "{0}\t{1}","defensive", "properties" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}