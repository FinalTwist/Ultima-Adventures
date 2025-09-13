using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class MagicTorch : GoldRing
	{
		[Constructable]
		public MagicTorch()
		{
			Resource = CraftResource.None;
			Name = "torch";

			//Name = LootPackEntry.MagicItemAdj( "start", false, false, ItemID ) + " " + Name;

			Hue = RandomThings.GetRandomColor(0);
			Light = LightType.Circle300;
			Weight = 1.0;
			ItemID = 0xF6B;
			Layer = Layer.TwoHanded;
            Attributes.NightSight = 1;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( this.ItemID == 0xA12 ){ list.Add( 1049644, "Double-Click to Unequip"); }
			else { list.Add( 1049644, "Double-Click to Equip"); }
        } 

		public override bool AllowEquipedCast( Mobile from )
		{
			return true;
		}

		public override bool OnEquip( Mobile from )
		{
			this.ItemID = 0xA12;
			return base.OnEquip( from );
		}

		public override void OnRemoved(IEntity parent )
		{
			this.ItemID = 0xF6B;
			base.OnRemoved( parent );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Item torch = from.FindItemOnLayer( Layer.TwoHanded );
			if ( torch == this )
			{
				from.AddToBackpack(this);
				from.PlaySound( 0x4BB );
				this.ItemID = 0xF6B;
				base.OnRemoved( from );
			}
			else if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				if ( from.FindItemOnLayer( Layer.TwoHanded ) != null )
				{
					from.AddToBackpack( from.FindItemOnLayer( Layer.TwoHanded ) );
				}
				from.SendLocalizedMessage( 502971 ); // You put the torch in your left hand.
				from.AddItem(this);
				from.PlaySound( 0x54 );
				this.ItemID = 0xA12;
				base.OnEquip( from );
			}
		}

		public MagicTorch( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}