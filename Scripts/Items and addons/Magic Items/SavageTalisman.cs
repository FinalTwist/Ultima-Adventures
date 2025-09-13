using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Misc;

namespace Server.Items
{
	public class SavageTalisman : MagicTalisman
	{
		public Mobile ItemOwner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Item_Owner { get{ return ItemOwner; } set{ ItemOwner = value; } }

		[Constructable]
		public SavageTalisman()
		{
			Name = "barbaric talisman";
			ItemID = 0x2F5A;
			Resource = CraftResource.None;
			Layer = Layer.Talisman;
			Weight = 1.0;
			Hue = 0;
			SkillBonuses.SetValues(0, SkillName.Camping, 80);
			SkillBonuses.SetValues(1, SkillName.Cooking, 50);
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( ItemOwner != null ){ list.Add( 1070722, "Talisman for " + ItemOwner.Name + "" ); } else { list.Add( 1070722, "Trinket"); }
        }

		public override bool OnEquip( Mobile from )
		{
			if ( this.ItemOwner != from )
			{
				from.LocalOverheadMessage( MessageType.Emote, 0xB1F, true, "This talisman belongs to another!" );
				return false;
			}
			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "Talismans are worn in the upper right slot." );
			return;
		}

		public SavageTalisman( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
			writer.Write( (Mobile)ItemOwner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			ItemOwner = reader.ReadMobile();
		}
	}
}