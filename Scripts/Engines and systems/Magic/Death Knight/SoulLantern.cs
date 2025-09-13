using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class SoulLantern : MagicLantern
	{
		public Mobile owner;
		public int TrappedSouls;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Souls { get { return TrappedSouls; } set { TrappedSouls = value; InvalidateProperties(); } }

		[Constructable]
		public SoulLantern( Mobile gifted )
		{
			Name = "lantern of souls";
			Hue = 0x47E;
			owner = gifted;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( this.ItemID == 0xA15 ){ list.Add( 1049644, "Double-Click to Unequip"); }
			else { list.Add( 1049644, "Double-Click to Equip"); }

			string sPower = string.Format("{0:n0}", TrappedSouls);
            if ( owner != null ){ list.Add( 1070722, "Souls For " + owner.Name + ": " + sPower + ""); }
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
			else if ( this.owner == from )
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
			else
			{
				from.SendMessage( "This is not your lantern!" );
			}
		}

		public SoulLantern( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
			writer.Write( (Mobile)owner);
            writer.Write( TrappedSouls );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
			TrappedSouls = reader.ReadInt();
		}
	}
}