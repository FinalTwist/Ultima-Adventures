using System;
using Server;
using Server.Network;
using Server.Multis;
using Server.Regions;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class BaseFruitTreeAddon : BaseAddon
	{
		public override abstract BaseAddonDeed Deed { get; }
		public abstract Item Fruit { get; }

		private int m_Fruits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Fruits
		{
			get { return m_Fruits; }
			set
			{
				if ( value < 0 )
					m_Fruits = 0;
				else
					m_Fruits = value;
			}
		}

		public BaseFruitTreeAddon() : base()
		{
			Timer.DelayCall( TimeSpan.FromMinutes( 5 ), new TimerCallback( Respawn ) );
		}

		public BaseFruitTreeAddon( Serial serial ) : base( serial )
		{
		}

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			/*BaseHouse house = BaseHouse.FindHouseAt( from );

			if ( house == null )
			{
				from.SendMessage("You must be on the property to pick from this tree.");
			}
			else */if ( from.InRange( c.Location, 2 ) )
			{
				//if ( house.IsOwner( from ) || house.IsCoOwner( from ) || house.IsFriend( from ) )
				//{
					if ( m_Fruits > 0 )
					{
						Item fruit = Fruit;

						if ( fruit == null )
							return;

						if ( !from.PlaceInBackpack( fruit ) )
						{
							fruit.Delete();
							from.SendLocalizedMessage( 501015 ); // There is no room in your backpack for the fruit.					
						}
						else
						{
							if ( --m_Fruits == 0 )
								Timer.DelayCall( TimeSpan.FromMinutes( 30 ), new TimerCallback( Respawn ) );

							from.SendLocalizedMessage( 501016 ); // You pick some fruit and put it in your backpack.
						}
					}
					else
						from.SendLocalizedMessage( 501017 ); // There is no more fruit on this tree
				//}
				//else
				//	from.SendMessage("This is not your fruit tree.");
			}
			else
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
		}

		public void Harvest( object state )
		{
			if (this.Deleted || this == null)
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			
			if (!(from is PlayerMobile) || from == null)
				return;
			/*BaseHouse house = BaseHouse.FindHouseAt( from );

			if ( house == null )
			{
				from.SendMessage("You must be on the property to pick from this tree.");
			}
			else */if ( from.InRange( this.Location, 1 ) )
			{
				//if ( house.IsOwner( from ) || house.IsCoOwner( from ) || house.IsFriend( from ) )
				//{
					if ( m_Fruits > 0 )
					{
						Item fruit = Fruit;

						if ( fruit == null )
							return;

						if ( !from.PlaceInBackpack( fruit ) )
						{
							fruit.Delete();
							from.SendLocalizedMessage( 501015 ); // There is no room in your backpack for the fruit.					
						}
						else
						{
							if ( --m_Fruits == 0 )
								Timer.DelayCall( TimeSpan.FromMinutes( 30 ), new TimerCallback( Respawn ) );

							from.SendLocalizedMessage( 501016 ); // You pick some fruit and put it in your backpack.
						}
					}
					else
						from.SendLocalizedMessage( 501017 ); // There is no more fruit on this tree
				//}
				//else
				//	from.SendMessage("This is not your fruit tree.");
			}

		}

		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m_Fruits > 0 && m is PlayerMobile && m.Alive && m.InRange( this.Location, 1 ) && m.InLOS( this ) )
				Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( Harvest ), new object[]{ m }  );
		}

		private void Respawn()
		{
			m_Fruits = Utility.RandomMinMax( 1, 4 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.Write( (int) m_Fruits );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Fruits = reader.ReadInt();

			if ( m_Fruits == 0 )
				Respawn();
		}
	}

	public class AppleTreeAddon : BaseFruitTreeAddon
	{
		public override BaseAddonDeed Deed { get { return new AppleTreeDeed(); } }
		public override Item Fruit { get { return new Apple(); } }

		[Constructable]
		public AppleTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0xD98, 1076269 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x3124, 1076269 ), 0, 0, 0 );
		}

		public AppleTreeAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class AppleTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new AppleTreeAddon(); } }
		public override int LabelNumber { get { return 1076269; } } // Apple Tree

		[Constructable]
		public AppleTreeDeed() : base()
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Grows Fruit" );
		}

		public AppleTreeDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class PearTreeAddon : BaseFruitTreeAddon
	{
		public override BaseAddonDeed Deed { get { return new PearTreeDeed(); } }
		public override Item Fruit { get { return new Pear(); } }

		[Constructable]
		public PearTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0xDA4, 1023492 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0xDA6, 1023492 ), 0, 0, 0 );
		}

		public PearTreeAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class PearTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new PearTreeAddon(); } }
		public override int LabelNumber { get { return 1023492; } } // Apple Tree

		[Constructable]
		public PearTreeDeed() : base()
		{
		}

		public PearTreeDeed( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Grows Fruit" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class PeachTreeAddon : BaseFruitTreeAddon
	{
		public override BaseAddonDeed Deed { get { return new PeachTreeDeed(); } }
		public override Item Fruit { get { return new Peach(); } }

		[Constructable]
		public PeachTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0xD9C, 1076270 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x3123, 1076270 ), 0, 0, 0 );
		}

		public PeachTreeAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class PeachTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new PeachTreeAddon(); } }
		public override int LabelNumber { get { return 1076270; } } // Peach Tree

		[Constructable]
		public PeachTreeDeed() : base()
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Grows Fruit" );
		}

		public PeachTreeDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}




}