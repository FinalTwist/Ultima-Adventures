using System;
using Server;
using Server.Regions;
using Server.Mobiles;

namespace Server.Items
{
	[Furniture]
	[Flipable( 0xE3D, 0xE3C )]
    public class MovingBox : LargeCrate
    {
		private int m_MaxWeightDefault = 10000;
		public override int DefaultMaxWeight{ get{ return m_MaxWeightDefault; } }

		public static bool IsEnabled()
		{
			return true;
		}

		[Constructable]
		public MovingBox() : base()
		{
			Weight = 1.0;
			MaxItems = 10000;
			Name = "housing crate";
			Hue = 0xAC0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Region reg = Region.Find( from.Location, from.Map );

			if ( !IsEnabled() )
			{
                from.SendMessage("The crate doesn't seem to open as the wood is warped.");
			}
			else if ( !Movable || IsSecure )
			{
                from.SendMessage("The crate cannot be locked down if you want to open it.");
				return;
			}
			else if (from.Backpack != null && this.Parent == from.Backpack && !reg.IsPartOf( "the Bank" ) )
			{
				from.SendMessage("The crate is too heavy to open from your backpack here, maybe at the bank?");
				return;
			}
			else if ( from == owner && ( from.Region is HouseRegion || reg.IsPartOf( "the Bank" ) ) )
			{
				Open( from );
			}
			else
            {
                from.SendMessage("Only the crate owner can open this, and while in a home or bank.");
            }
		}

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
        {
			Region reg = Region.Find( from.Location, from.Map );

			if ( !IsEnabled() )
			{
                from.SendMessage("The crate doesn't seem to open as the wood is warped.");
                return false;
			}
			else if ( !Movable || IsSecure )
			{
                from.SendMessage("The crate cannot be locked down if you want to open it.");
				return false;
			}
			else if (from.Backpack != null && this.Parent == from.Backpack && !reg.IsPartOf( "the Bank" ))
			{
				from.SendMessage("The crate is too heavy use from your backpack here, maybe at the bank?");
				return false;
			}
			else if ( from == owner && ( from.Region is HouseRegion || reg.IsPartOf( "the Bank" ) ) )
			{
				return base.OnDragDropInto(from, dropped, p);
			}
			else
            {
                from.SendMessage("Only the crate owner can open this, and while in a home or bank.");
                return false;
            }

            return base.OnDragDropInto(from, dropped, p);
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
        {
			Region reg = Region.Find( from.Location, from.Map );

			if ( !IsEnabled() )
			{
                from.SendMessage("The crate doesn't seem to open as the wood is warped.");
                return false;
			}
			else if ( !Movable )
			{
                from.SendMessage("The crate cannot be locked down if you want to open it.");
				return false;
			}
			else if (from.Backpack != null && this.Parent == from.Backpack && !reg.IsPartOf( "the Bank" ))
			{
				from.SendMessage("The crate is too heavy use from your backpack here, maybe at the bank?");
				return false;
			}
			else if ( from == owner && ( from.Region is HouseRegion || reg.IsPartOf( "the Bank" ) ) )
			{
				return base.OnDragDrop(from, dropped);
			}
			else
            {
                from.SendMessage("Only the crate owner can open this, and while in a home or bank.");
                return false;
            }

            return base.OnDragDrop(from, dropped);
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( owner != null ){ list.Add( 1070722, "Belongs to " + owner.Name + "" ); }
        }

		public override bool OnDragLift( Mobile from )
		{
			Region reg = Region.Find( from.Location, from.Map );
			if (from.Backpack != null && this.Parent == from.Backpack && !reg.IsPartOf( "the Bank" ))
			{
				from.SendMessage("The crate is too heavy use from your backpack here, maybe at the bank?");
				return false;
			}
			else if ( owner == null ){ owner = from; }
			return true;
		}

		public MovingBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (Mobile)owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
			Weight = 1.0;
			MaxItems = 10000;
			Name = "housing crate";
			Hue = 0xAC0;
		}

		public override int GetTotal(TotalType type)
        {
			bool weightless = false;
			bool inBank = false;
			bool cycleItems = true;
			Item bank = null;
				if ( ParentEntity is Item ){ bank = ParentEntity as Item; }

			if ( RootParentEntity is PlayerMobile )
			{
				while ( cycleItems )
				{
					if ( bank is BankBox ){ inBank = true; cycleItems = false; } else if ( bank.ParentEntity is Item ){ bank = bank.ParentEntity as Item; } else { cycleItems = false; }
				}
			}

			if ( !Movable || IsSecure || inBank ){ weightless = false; }
			else if ( RootParentEntity is PlayerMobile ){ weightless = true; }
			else if ( ParentEntity == null ){ weightless = true; }

			if ( type == TotalType.Items && weightless )
				return 0;
			else if ( type == TotalType.Weight && weightless )
				return 0;
			else
				return base.GetTotal(type);
        }

		public override void UpdateTotal(Item sender, TotalType type, int delta)
        {
			bool weightless = false;
			bool inBank = false;
			bool cycleItems = true;
			Item bank = null;
				if ( ParentEntity is Item ){ bank = ParentEntity as Item; }

			if ( RootParentEntity is PlayerMobile )
			{
				while ( cycleItems )
				{
					if ( bank is BankBox ){ inBank = true; cycleItems = false; } else if ( bank.ParentEntity is Item ){ bank = bank.ParentEntity as Item; } else { cycleItems = false; }
				}
			}

			if ( !Movable || IsSecure || inBank ){ weightless = false; }
			else if ( RootParentEntity is PlayerMobile ){ weightless = true; }
			else if ( ParentEntity == null ){ weightless = true; }

			if ( type == TotalType.Items && weightless )
                base.UpdateTotal(sender, type, 0);
			else if ( type == TotalType.Weight && weightless )
                base.UpdateTotal(sender, type, 0);
			else
                base.UpdateTotal(sender, type, delta);
        }

		public Mobile owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }
	}
}