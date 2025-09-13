using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Misc;

namespace Server.Items
{
	public class InterestBag : Bag
	{
		public override int MaxWeight{ get{ return 5; } }
		public override int DefaultDropSound{ get{ return 0x42; } }


		[Constructable]
		public InterestBag() : base( )
		{
			Name = "Investment Bag (Good)";
			Movable = true;
			Hue = 2648;
			//LootType = LootType.Blessed;
		}

        public override bool OnDragDrop( Mobile from, Item dropped )
		{
        	Item item = dropped as Item;
			int investment = 0;

			BankBox box = from.FindBankNoCreate();
			if( box != null && IsChildOf( box ) )
			{
				if ( dropped is Gold || dropped is BankCheck ) // if player drops money in the bag create a new investment check or add to an existing investment check
				{
					if (dropped is Gold)
					{
						investment = dropped.Amount;
					}
					
					else if (dropped is BankCheck)
					{
						investment = ((BankCheck)dropped).Worth;
					}
				
					List<Item> ItemsInBag = this.Items;
					if (ItemsInBag != null)
					{
						for( int z = 0; z < ItemsInBag.Count; z++ )
						{
							Item inBag = ItemsInBag[z];
								//from.SendMessage( "checking in bag, found " + inBag );

							if( inBag is InvestmentCheck )
							{
								((InvestmentCheck)inBag).Worth += investment;
								dropped.Delete();
								from.PlaySound( 0x42 );
								from.SendMessage( "You add " + investment + " gold to your investment" );
								return true;
							}

						}
					}
					
					
					this.AddItem(new InvestmentCheck( investment ));
					dropped.Delete();
					from.PlaySound( 0x42 );
					from.SendMessage( "You make a new investment for " + investment + " gold." );
					return true;

				}
				else 
				{
					from.SendMessage( "That's not a bank check or gold" );
					return false;
				}
			}
			else
			{
				from.SendLocalizedMessage( 1047026 ); // That must be in your bank box to use it.
				return false;
			}

		}

        public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p  )
		{
        	Item item = dropped as Item;
			int investment = 0;

			BankBox box = from.FindBankNoCreate();
			if( box != null && IsChildOf( box ) )
			{
				if ( dropped is Gold || dropped is BankCheck ) // if player drops money in the bag create a new investment check or add to an existing investment check
				{
					if (dropped is Gold)
					{
						investment = dropped.Amount;
					}
					
					else if (dropped is BankCheck)
					{
						investment = ((BankCheck)dropped).Worth;
					}

					List<Item> ItemsInBag = this.Items;
					if (ItemsInBag != null)
					{
						for( int z = 0; z < ItemsInBag.Count; z++ )
						{
							Item inBag = ItemsInBag[z];

							if( inBag is InvestmentCheck )
							{
								((InvestmentCheck)inBag).Worth += investment;
								dropped.Delete();
								from.PlaySound( 0x42 );
								from.SendMessage( "You add " + investment + " gold to your investment" );
								return true;
							}

						}
					}
					
					
					this.AddItem(new InvestmentCheck( investment ));
					dropped.Delete();
					from.PlaySound( 0x42 );
					from.SendMessage( "You make a new investment for " + investment + " gold." );
					return true;

				}
				else 
				{
					from.SendMessage( "That's not a bankcheck or gold" );
					return false;
				}
			}
			else
			{
				from.SendLocalizedMessage( 1047026 ); // That must be in your bank box to use it.
				return false;
			}


		}
		



		
		public InterestBag( Serial serial ) : base( serial )
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
}
