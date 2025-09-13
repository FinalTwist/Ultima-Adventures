using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Engines.Harvest;
using Server.Mobiles;

namespace Server.Targets
{
	public class BladedItemTarget : Target
	{
		private Item m_Item;

		public BladedItemTarget( Item item ) : base( 2, false, TargetFlags.None )
		{
			m_Item = item;
		}

		protected override void OnTargetOutOfRange( Mobile from, object targeted )
		{
			base.OnTargetOutOfRange (from, targeted);
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( m_Item.Deleted )
				return;

			if ( targeted is ICarvable )
			{
				((ICarvable)targeted).Carve( from, m_Item );
			}
			else if ( targeted is Container )
			{
				Container body = (Container)targeted;

				if ( body.ItemID >= 0x4B5A && body.ItemID <= 0x4BAB )
				{
					body.ItemID = Utility.RandomList( 0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0, 0xED1, 0xED2 );
					body.Hue = 0;

					from.CriminalAction( true );
					Misc.Titles.AwardKarma( from, -50, true );

					body.DropItem( new BodyPart( 0x1D9F ) );
					body.DropItem( new BodyPart( 0x1DA4 ) );
					body.DropItem( new BodyPart( 0x1DA2 ) );
					body.DropItem( new BodyPart( 0x1DA3 ) );
					body.DropItem( new BodyPart( 0x1DA1 ) );
					body.DropItem( new BodyPart( 0x1DA0 ) );

					from.SendMessage( "You hack up the body into bloody pieces." );
				}
			}
			else if ( targeted is SwampDragon && ((SwampDragon)targeted).HasBarding )
			{
				SwampDragon pet = (SwampDragon)targeted;

				if ( !pet.Controlled || pet.ControlMaster != from )
					from.SendLocalizedMessage( 1053022 ); // You cannot remove barding from a swamp dragon you do not own.
				else
					pet.HasBarding = false;
			}
			else
			{
				HarvestSystem system = Lumberjacking.System;
				HarvestDefinition def = Lumberjacking.System.Definition;

				int tileID;
				Map map;
				Point3D loc;

				if ( !system.GetHarvestDetails( from, m_Item, targeted, out tileID, out map, out loc ) )
				{
					from.SendLocalizedMessage( 500494 ); // You can't use a bladed item on that!
				}
				else if ( !def.Validate( tileID ) )
				{
					from.SendLocalizedMessage( 500494 ); // You can't use a bladed item on that!
				}
				else
				{
					HarvestBank bank = def.GetBank( map, loc.X, loc.Y );

					if ( bank == null )
						return;

					if ( bank.Current < 5 )
					{
						from.SendMessage( "There is not enough here to harvest." );
						//from.SendLocalizedMessage( 500493 ); // There's not enough wood here to harvest.
					}
					else
					{
						bank.Consume( 5, from );

						if ( tileID == 0x4D96 || tileID == 0x4D9A ) // apples
						{
							Item item = new Apple();

							if ( from.PlaceInBackpack( item ) )
							{
								from.SendMessage( "You put an apple into your backpack." );
							}
							else
							{
								from.SendMessage( "You can't place any apples into your backpack!" );
								item.Delete();
							}
						}
						else if ( tileID == 0x4DA6 || tileID == 0x4DAA ) // pears
						{
							Item item = new Pear();

							if ( from.PlaceInBackpack( item ) )
							{
								from.SendMessage( "You put a pear into your backpack." );
							}
							else
							{
								from.SendMessage( "You can't place any pears into your backpack!" );
								item.Delete();
							}
						}
						else if ( tileID == 0x4D9E || tileID == 0x4DA2 ) // peaches
						{
							Item item = new Peach();

							if ( from.PlaceInBackpack( item ) )
							{
								from.SendMessage( "You put a peach into your backpack." );
							}
							else
							{
								from.SendMessage( "You can't place any peaches into your backpack!" );
								item.Delete();
							}
						}
						else if ( tileID == 0x4CDA || tileID == 0x4CDB || tileID == 0x4CDC || tileID == 0x4CDD || tileID == 0x4CDE || tileID == 0x4CDF ) // acorns
						{
							Item item = new Acorn();

							if ( from.PlaceInBackpack( item ) )
							{
								from.SendMessage( "You put an acorn into your backpack." );
							}
							else
							{
								from.SendMessage( "You can't place any acorns into your backpack!" );
								item.Delete();
							}
						}
						else if ( tileID == 0x4CA8 || tileID == 0x4CAA || tileID == 0x4CAB ) // bananas
						{
							Item item = new Banana();

							if ( from.PlaceInBackpack( item ) )
							{
								from.SendMessage( "You put a banana into your backpack." );
							}
							else
							{
								from.SendMessage( "You can't place any bananas into your backpack!" );
								item.Delete();
							}
						}
						else if ( tileID == 0x4C95 ) // coconut
						{
							Item item = new Coconut();

							if ( from.PlaceInBackpack( item ) )
							{
								from.SendMessage( "You put a coconut into your backpack." );
							}
							else
							{
								from.SendMessage( "You can't place any coconuts into your backpack!" );
								item.Delete();
							}
						}
						else if ( tileID == 0x4C96 ) // dates
						{
							Item item = new Dates();

							if ( from.PlaceInBackpack( item ) )
							{
								from.SendMessage( "You put some dates into your backpack." );
							}
							else
							{
								from.SendMessage( "You can't place any dates into your backpack!" );
								item.Delete();
							}
						}
						else
						{
							Item item = new Kindling();

							if ( from.PlaceInBackpack( item ) )
							{
								from.SendLocalizedMessage( 500491 ); // You put some kindling into your backpack.
								from.SendLocalizedMessage( 500492 ); // An axe would probably get you more wood.
							}
							else
							{
								from.SendLocalizedMessage( 500490 ); // You can't place any kindling into your backpack!

								item.Delete();
							}
						}
					}
				}
			}
		}
	}
}