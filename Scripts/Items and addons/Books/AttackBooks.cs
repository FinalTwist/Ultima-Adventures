using System;
using Server;

namespace Server.Items
{
	public class MyNinjaBook : BookOfNinjitsu
	{
		[Constructable]
		public MyNinjaBook()
		{
			Name = NameList.RandomName( "tokuno male" );
			if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ Name = NameList.RandomName( "tokuno female" ); }

			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ ItemID = 0x2254; }

			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			string book = "Book";
			switch ( Utility.RandomMinMax( 1, 7 ) ) 
			{
				case 1: book = "Manual"; break;
				case 2: book = "Tome"; break;
				case 3: book = "Volume"; break;
				case 4: book = "Codex"; break;
				case 5: book = "Lexicon"; break;
				case 6: book = "Omnibus"; break;
			}

			switch ( Utility.RandomMinMax( 0, 7 ) ) 
			{
				case 0: Name = Name + "'s " + book + " of Ninjitsu"; break;
				case 1: Name = Name + "'s " + book + " of the Ninja"; break;
				case 2: Name = Name + "'s " + book + " of the Ninja Arts"; break;
				case 3: Name = Name + "'s " + book + " of Ninja Way"; break;
				case 4: Name = Name + "'s " + book + " of Ninja Secrets"; break;
				case 5: Name = Name + "'s " + book + " of the Ninja Code"; break;
				case 6: Name = Name + "'s " + book + " of the Ninjitsu"; break;
				case 7: Name = Name + "'s " + book + " of the Ninja Path"; break;
			}
		}

		public MyNinjaBook( Serial serial ) : base( serial )
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
	public class MyBushidoBook : BookOfBushido
	{
		[Constructable]
		public MyBushidoBook()
		{
			Name = NameList.RandomName( "tokuno male" );
			if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ Name = NameList.RandomName( "tokuno female" ); }

			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ ItemID = 0x2254; }

			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			string book = "Book";
			switch ( Utility.RandomMinMax( 1, 7 ) ) 
			{
				case 1: book = "Manual"; break;
				case 2: book = "Tome"; break;
				case 3: book = "Volume"; break;
				case 4: book = "Codex"; break;
				case 5: book = "Lexicon"; break;
				case 6: book = "Omnibus"; break;
			}

			switch ( Utility.RandomMinMax( 0, 7 ) ) 
			{
				case 0: Name = Name + "'s " + book + " of Bushido"; break;
				case 1: Name = Name + "'s " + book + " of the Samurai"; break;
				case 2: Name = Name + "'s " + book + " of the Bushido Arts"; break;
				case 3: Name = Name + "'s " + book + " of Samurai Way"; break;
				case 4: Name = Name + "'s " + book + " of Bushido Secrets"; break;
				case 5: Name = Name + "'s " + book + " of the Samurai Code"; break;
				case 6: Name = Name + "'s " + book + " of the Samurai"; break;
				case 7: Name = Name + "'s " + book + " of the Samurai Path"; break;
			}
		}

		public MyBushidoBook( Serial serial ) : base( serial )
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
	public class MyChivalryBook : BookOfChivalry
	{
		[Constructable]
		public MyChivalryBook()
		{
			Name = NameList.RandomName( "male" );
			if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ Name = NameList.RandomName( "female" ); }

			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ ItemID = 0x22C5; }

			Hue = Server.Misc.RandomThings.GetRandomColor(0);

			string book = "Book";
			switch ( Utility.RandomMinMax( 1, 7 ) ) 
			{
				case 1: book = "Manual"; break;
				case 2: book = "Tome"; break;
				case 3: book = "Volume"; break;
				case 4: book = "Codex"; break;
				case 5: book = "Lexicon"; break;
				case 6: book = "Omnibus"; break;
			}

			switch ( Utility.RandomMinMax( 0, 7 ) ) 
			{
				case 0: Name = Name + "'s " + book + " of Chivalry"; break;
				case 1: Name = Name + "'s " + book + " of the Cavalier"; break;
				case 2: Name = Name + "'s " + book + " of the Chivalric Code"; break;
				case 3: Name = Name + "'s " + book + " of Chivalric Way"; break;
				case 4: Name = Name + "'s " + book + " of the Cavelier's Path"; break;
				case 5: Name = Name + "'s " + book + " of the Knight's Code"; break;
				case 6: Name = Name + "'s " + book + " of the Knight"; break;
				case 7: Name = Name + "'s " + book + " of the Knight's Path"; break;
			}
		}

		public MyChivalryBook( Serial serial ) : base( serial )
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