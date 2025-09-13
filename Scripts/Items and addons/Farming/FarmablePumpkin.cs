using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FarmablePumpkin : FarmableCrop
	{
		public static int GetCropID()
		{
			return Utility.RandomList( 0x53C9, 0x53CA, 0x53CB, 0x53CC );
		}

		public override Item GetCropObject()
		{
			Pumpkin pumpkin = new Pumpkin();
			return pumpkin;
		}

		public override int GetPickedID()
		{
			return Utility.RandomList( 0x0C5F, 0x0C60 );
		}

		[Constructable]
		public FarmablePumpkin(): base( GetCropID() )
		{
			Name = "pumpkin";
		}

		public FarmablePumpkin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	public class FarmablePumpkinLarge : FarmableCrop
	{
		public static int GetCropID()
		{
			return 0x5559;
		}

		public override Item GetCropObject()
		{
			PumpkinLarge pumpkin = new PumpkinLarge();
			return pumpkin;
		}

		public override int GetPickedID()
		{
			return Utility.RandomList( 0x0C5F, 0x0C60 );
		}

		[Constructable]
		public FarmablePumpkinLarge(): base( GetCropID() )
		{
			Name = "large pumpkin";
		}

		public FarmablePumpkinLarge( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	public class FarmablePumpkinTall : FarmableCrop
	{
		public static int GetCropID()
		{
			return 0x5456;
		}

		public override Item GetCropObject()
		{
			PumpkinTall pumpkin = new PumpkinTall();
			return pumpkin;
		}

		public override int GetPickedID()
		{
			return Utility.RandomList( 0x0C5F, 0x0C60 );
		}

		[Constructable]
		public FarmablePumpkinTall(): base( GetCropID() )
		{
			Name = "tall pumpkin";
		}

		public FarmablePumpkinTall( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	public class FarmablePumpkinGreen : FarmableCrop
	{
		public static int GetCropID()
		{
			return 0x555B;
		}

		public override Item GetCropObject()
		{
			PumpkinGreen pumpkin = new PumpkinGreen();
			return pumpkin;
		}

		public override int GetPickedID()
		{
			return Utility.RandomList( 0x0C5F, 0x0C60 );
		}

		[Constructable]
		public FarmablePumpkinGreen(): base( GetCropID() )
		{
			Name = "green pumpkin";
		}

		public FarmablePumpkinGreen( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	public class FarmablePumpkinGiant : FarmableCrop
	{
		public static int GetCropID()
		{
			return 0x555A;
		}

		public override Item GetCropObject()
		{
			PumpkinGiant pumpkin = new PumpkinGiant();
			return pumpkin;
		}

		public override int GetPickedID()
		{
			return Utility.RandomList( 0x0C5F, 0x0C60 );
		}

		[Constructable]
		public FarmablePumpkinGiant(): base( GetCropID() )
		{
			Name = "giant pumpkin";
		}

		public FarmablePumpkinGiant( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}