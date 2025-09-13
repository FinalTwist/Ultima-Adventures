using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class MagicLantern : GoldRing
	{
		[Constructable]
		public MagicLantern()
		{
			Resource = CraftResource.None;
			Name = "lantern";

			//Name = LootPackEntry.MagicItemAdj( "start", false, false, ItemID ) + " " + Name;

			Hue = RandomThings.GetRandomColor(0);
			Light = LightType.Circle300;
			Weight = 2.0;
			ItemID = 0xA18;
			Layer = Layer.TwoHanded;
            Attributes.NightSight = 1;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( this.ItemID == 0xA15 ){ list.Add( 1049644, "Double-Click to Unequip"); }
			else { list.Add( 1049644, "Double-Click to Equip"); }
        } 

		public override bool AllowEquipedCast( Mobile from )
		{
			return true;
		}

		public override bool OnEquip( Mobile from )
		{
			this.ItemID = 0xA15;
			return base.OnEquip( from );
		}

		public override void OnRemoved(IEntity parent )
		{
			this.ItemID = 0xA18;
			base.OnRemoved( parent );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Item lantern = from.FindItemOnLayer( Layer.TwoHanded );
			if ( lantern == this )
			{
				from.AddToBackpack(this);
				this.ItemID = 0xA18;
				from.PlaySound( 0x4BB );
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
				from.SendMessage( "You put the lantern in your left hand." );
				from.AddItem(this);
				this.ItemID = 0xA15;
				from.PlaySound( 0x47 );
				base.OnEquip( from );
			}
		}

		public MagicLantern( Serial serial ) : base( serial )
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