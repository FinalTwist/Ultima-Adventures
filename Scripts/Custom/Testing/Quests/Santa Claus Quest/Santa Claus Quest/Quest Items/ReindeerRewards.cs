/*
 * Created by SharpDevelop.
 * User: Shazzy
 * Date: 11/16/2005
 * Time: 3:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */


using System;
using Server;

namespace Server.Items
{
	public class ReindeerReward1 : Item
	{
		[Constructable]
		public ReindeerReward1() : this( 1 )
		{
		}

		[Constructable]
		public ReindeerReward1( int amount ) : base( 0x2565)
		{
			Name = "Rudolph's Special Reindeer Hammer";
			LootType = LootType.Blessed;
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
		}
		 public override void OnDoubleClick( Mobile m )

		{
		 	if ( !IsChildOf (m.Backpack))
		 	{
				m.SendMessage( "You must have this in your backpack in order make Santas Gift!" );
		 	}
		 	else
		 		
		 	{
		        Item a = m.Backpack.FindItemByType( typeof(ReindeerReward2) );
			    Item b = m.Backpack.FindItemByType( typeof(ReindeerReward3) );
				Item c = m.Backpack.FindItemByType( typeof(ReindeerReward4) );
			    Item d = m.Backpack.FindItemByType( typeof(ReindeerReward5) );
				Item e = m.Backpack.FindItemByType( typeof(ReindeerReward6) );
				Item f = m.Backpack.FindItemByType( typeof(ReindeerReward7) );				
				Item g = m.Backpack.FindItemByType( typeof(ReindeerReward8) );				
				Item h = m.Backpack.FindItemByType( typeof(ReindeerReward9) );
			
			if ( a != null && b != null && c != null && d != null && e != null && f != null && g != null && h != null )
			{				
				m.AddToBackpack( new SantasGift2011() );
				a.Delete();
				b.Delete();
				c.Delete();
				d.Delete();
				e.Delete();
				f.Delete();
				g.Delete();
				h.Delete();
				
				m.SendMessage( "You use the hammer to create Santas Gift." );
				this.Delete();
			}				
				else 
			{
				m.SendMessage( "You do not have all Reindeer's Shoes to use the hammer." );
			}
		 	}
		 	
		 }
				
		public ReindeerReward1( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReindeerReward1( amount ), amount );
		//}

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
	

	public class ReindeerReward2 : Item
	{
		[Constructable]
		public ReindeerReward2() : this( 1 )
		{
		}

		[Constructable]
		public ReindeerReward2( int amount ) : base( 0x0FB6 )
		{
			Name = "Dasher's Reindeer Shoes";
			LootType = LootType.Blessed;
			Hue = 1151;
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
		}

		public ReindeerReward2( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReindeerReward2( amount ), amount );
		//}

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
	
	public class ReindeerReward3 : Item
	{
		[Constructable]
		public ReindeerReward3() : this( 1 )
		{
		}

		[Constructable]
		public ReindeerReward3( int amount ) : base( 0x0FB6 )
		{
			Name = "Dancer's Reindeer Shoes";
			LootType = LootType.Blessed;
			Hue = 1151;
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
		}

		public ReindeerReward3( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReindeerReward3( amount ), amount );
		//}

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
	

	public class ReindeerReward4 : Item
	{
		[Constructable]
		public ReindeerReward4() : this( 1 )
		{
		}

		[Constructable]
		public ReindeerReward4( int amount ) : base( 0x0FB6 )
		{
			Name = "Prancer's Reindeer Shoes";
			LootType = LootType.Blessed;
			Hue = 1151;
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
		}

		public ReindeerReward4( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReindeerReward4( amount ), amount );
		//}

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
	

	public class ReindeerReward5 : Item
	{
		[Constructable]
		public ReindeerReward5() : this( 1 )
		{
		}

		[Constructable]
		public ReindeerReward5( int amount ) : base( 0x0FB6 )
		{
			Name = "Vixen's Reindeer Shoes";
			LootType = LootType.Blessed;
			Hue = 1151;
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
		}

		public ReindeerReward5( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReindeerReward5( amount ), amount );
		//}

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
	

	public class ReindeerReward6 : Item
	{
		[Constructable]
		public ReindeerReward6() : this( 1 )
		{
		}

		[Constructable]
		public ReindeerReward6( int amount ) : base( 0x0FB6 )
		{
			Name = "Comet's Reindeer Shoes";
			LootType = LootType.Blessed;
			Hue = 1151;
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
		}

		public ReindeerReward6( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReindeerReward6( amount ), amount );
		//}

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
	

	public class ReindeerReward7 : Item
	{
		[Constructable]
		public ReindeerReward7() : this( 1 )
		{
		}

		[Constructable]
		public ReindeerReward7( int amount ) : base( 0x0FB6 )
		{
			Name = "Cupid's Reindeer Shoes";
			LootType = LootType.Blessed;
			Hue = 1151;
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
		}

		public ReindeerReward7( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReindeerReward7( amount ), amount );
		//}

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

	public class ReindeerReward8 : Item
	{
		[Constructable]
		public ReindeerReward8() : this( 1 )
		{
		}

		[Constructable]
		public ReindeerReward8( int amount ) : base( 0x0FB6 )
		{
			Name = "Donner's Reindeer Shoes";
			LootType = LootType.Blessed;
			Hue = 1151;
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
		}

		public ReindeerReward8( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReindeerReward8( amount ), amount );
		//}

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

	public class ReindeerReward9 : Item
	{
		[Constructable]
		public ReindeerReward9() : this( 1 )
		{
		}

		[Constructable]
		public ReindeerReward9( int amount ) : base( 0x0FB6 )
		{
			Name = "Blitzen's Reindeer Shoes";
			LootType = LootType.Blessed;
			Hue = 1151;
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
		}

		public ReindeerReward9( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReindeerReward9( amount ), amount );
		//}

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
}
