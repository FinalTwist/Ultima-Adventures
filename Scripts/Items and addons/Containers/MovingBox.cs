using System;
using Server;
using Server.Regions;
using Server.Mobiles;
using Server.Multis;

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

		private bool CanOpen(Mobile from)
		{
			if ( !IsEnabled() )
			{
                from.SendMessage("The crate doesn't seem to open as the wood is warped.");
                return false;
			}

			if (owner != null && from != owner)
			{
				from.SendMessage("Only the crate owner can open this, and only while in a home or bank.");
				return false;
			}
			
			if ( !Movable || IsSecure )
			{
                from.SendMessage("The crate cannot be locked down if you want to use it.");
				return false;
			}

			// Must at least be in the bank or a House they're co-owner of
			Region region = Region.Find( GetWorldLocation(), Map );
			if ( !region.IsPartOf( "the Bank" ) )
			{
				BaseHouse house = BaseHouse.FindHouseAt(this);
				if (house == null || !house.IsCoOwner(from))
				{
					from.SendMessage("The crate is magically sealed.");
					return false;
				}
			}

			return true;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( owner != null ){ list.Add( 1070722, "Belongs to " + owner.Name + "" ); }
			list.Add( 1049644, "Only usable in a house or bank"); // PARENTHESIS
        }

		public override void OnDoubleClick( Mobile from )
		{
			if (!from.IsStaff() && !CanOpen(from)) return;

			// If there is a root container, it has to be the bank
			if (RootParent != null)
			{
				var bankbox = from.FindBankNoCreate();
				if (bankbox != null)
				{
					// Check ancestors
					var success = false;
					Container container = this;
					while (container != null)
					{
						if (container == bankbox)
						{
							success = true;
							break;
						}

						container = container.Parent as Container;
					}

					if (!success)
					{
						from.SendMessage("You should put this down before you open it.");
						return;
					}
				}
			}

			Open( from );
		}

		public bool CheckDrop(Mobile from)
		{

			return true;
		}

		public override bool OnDragLift( Mobile from )
		{
			if (from.IsStaff()) return true;
			if (!CanOpen(from)) return false;

			if ( owner == null ){ owner = from; }

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