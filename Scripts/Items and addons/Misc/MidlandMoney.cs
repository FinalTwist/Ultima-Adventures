using System;

namespace Server.Items
{
	public class Sovereign : Item
	{
		public override double DefaultWeight
		{
			get { return ( Core.ML ? ( 0.02 / 3 ) : 0.02 ); }
		}

		[Constructable]
		public Sovereign() : this( 1 )
		{
		}

		[Constructable]
		public Sovereign( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		[Constructable]
		public Sovereign( int amount ) : base( 0xEED )
		{
			Name = "Sovereign";
			Stackable = true;
			if (amount > 60000 || amount <=0 )
				Amount = 1;
			else
				Amount = amount;
			Light = LightType.Circle150;
		}

		public Sovereign( Serial serial ) : base( serial )
		{
		}

		public override int GetDropSound()
		{
			if ( Amount <= 1 )
				return 0x2E4;
			else if ( Amount <= 5 )
				return 0x2E5;
			else
				return 0x2E6;
		}

/*		protected override void OnAmountChange( int oldValue )
		{
			int newValue = this.Amount;

			UpdateTotal( this, TotalType.Sovereign, newValue - oldValue );
		}

		public override int GetTotal( TotalType type )
		{
			int baseTotal = base.GetTotal( type );

			if ( type == TotalType.Sovereign )
				baseTotal += this.Amount;

			return baseTotal;
		}*/

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
	}
	public class Drachma : Item
	{
		public override double DefaultWeight
		{
			get { return ( Core.ML ? ( 0.02 / 3 ) : 0.02 ); }
		}

		[Constructable]
		public Drachma() : this( 1 )
		{
		}

		[Constructable]
		public Drachma( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		[Constructable]
		public Drachma( int amount ) : base( 0xEED )
		{
			Name = "Drachma";
			Hue = 0x83E;
			Stackable = true;
			if (amount > 60000 || amount <=0 )
				Amount = 1;
			else
				Amount = amount;
			Light = LightType.Circle150;
		}

		public Drachma( Serial serial ) : base( serial )
		{
		}

		public override int GetDropSound()
		{
			if ( Amount <= 1 )
				return 0x2E4;
			else if ( Amount <= 5 )
				return 0x2E5;
			else
				return 0x2E6;
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
	}
	public class Sslit : Item
	{
		public override double DefaultWeight
		{
			get { return ( Core.ML ? ( 0.02 / 3 ) : 0.02 ); }
		}

		[Constructable]
		public Sslit() : this( 1 )
		{
		}

		[Constructable]
		public Sslit( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		[Constructable]
		public Sslit( int amount ) : base( 0xEED )
		{
			Name = "Sslit";
			Stackable = true;
			if (amount > 60000 || amount <=0 )
				Amount = 1;
			else
				Amount = amount;
			Light = LightType.Circle150;
		}

		public Sslit( Serial serial ) : base( serial )
		{
		}

		public override int GetDropSound()
		{
			if ( Amount <= 1 )
				return 0x2E4;
			else if ( Amount <= 5 )
				return 0x2E5;
			else
				return 0x2E6;
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
	}
	public class Dubloon : Item
	{
		public override double DefaultWeight
		{
			get { return ( Core.ML ? ( 0.02 / 3 ) : 0.02 ); }
		}

		[Constructable]
		public Dubloon() : this( 1 )
		{
		}

		[Constructable]
		public Dubloon( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		[Constructable]
		public Dubloon( int amount ) : base( 0xEED )
		{
			Name = "Dubloon";
			Hue = 0x9C4;
			Stackable = true;
			if (amount > 60000 || amount <=0 )
				Amount = 1;
			else
				Amount = amount;
			Light = LightType.Circle150;
		}

		public Dubloon( Serial serial ) : base( serial )
		{
		}

		public override int GetDropSound()
		{
			if ( Amount <= 1 )
				return 0x2E4;
			else if ( Amount <= 5 )
				return 0x2E5;
			else
				return 0x2E6;
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
	}
	public class Skaal : Item
	{
		public override double DefaultWeight
		{
			get { return ( Core.ML ? ( 0.02 / 3 ) : 0.02 ); }
		}

		[Constructable]
		public Skaal() : this( 1 )
		{
		}

		[Constructable]
		public Skaal( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		[Constructable]
		public Skaal( int amount ) : base( 0xEED )
		{
			Name = "Skaal";
			Stackable = true;
			if (amount > 60000 || amount <=0 )
				Amount = 1;
			else
				Amount = amount;
			Light = LightType.Circle150;
		}

		public Skaal( Serial serial ) : base( serial )
		{
		}

		public override int GetDropSound()
		{
			if ( Amount <= 1 )
				return 0x2E4;
			else if ( Amount <= 5 )
				return 0x2E5;
			else
				return 0x2E6;
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
	}
}