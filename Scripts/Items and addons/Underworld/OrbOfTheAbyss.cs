using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class OrbOfTheAbyss : MagicTalisman
	{
		public Mobile owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		[Constructable]
		public OrbOfTheAbyss()
		{
			Name = "orb of the abyss";
			ItemID = 0x2C84;
			Hue = 0x489;
		}

		public override bool OnEquip( Mobile from )
		{
			if ( owner != from )
			{
				from.SendMessage ("This is not your orb!");
				return false;
			}

			return base.OnEquip( from );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            if ( owner != null ){ list.Add( 1049644, "Belongs to " + owner.Name + ""); }
        } 

		public OrbOfTheAbyss( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
			writer.Write( (Mobile)owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
		}

		public static bool ChangeOrb( Mobile m, Mobile tinker, Item orb )
		{
			if ( orb.ItemID == 0x2C84 ){ orb.ItemID = 0x4CFA; orb.Name = "ring of the abyss"; orb.Layer = Layer.Ring; }
			else if ( orb.ItemID == 0x4CFA ){ orb.ItemID = 0x4CFF; orb.Name = "amulet of the abyss"; orb.Layer = Layer.Neck; }
			else if ( orb.ItemID == 0x4CFF ){ orb.ItemID = 0x4CF0; orb.Name = "bracelet of the abyss"; orb.Layer = Layer.Bracelet; }
			else if ( orb.ItemID == 0x4CF0 ){ orb.ItemID = 0x4CFB; orb.Name = "earrings of the abyss"; orb.Layer = Layer.Earrings; }
			else { orb.ItemID = 0x2C84; orb.Name = "orb of the abyss"; orb.Layer = Layer.Talisman; }

			tinker.Say( "Here. You now have the " + orb.Name + "." );

			m.PlaySound( 0x542 );

			return false;
		}
	}
}