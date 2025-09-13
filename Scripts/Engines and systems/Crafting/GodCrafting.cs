using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x0FB4, 0x0FB5 )]
	public class GodSmithing : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefGodSmithing.CraftSystem; } }

		[Constructable]
		public GodSmithing() : base( 0x0FB4 )
		{
			Weight = 20.0;
			Name = "Magical Golden Smithing Hammer";
			UsesRemaining = 10;
			Hue = 0x501;
		}

		[Constructable]
		public GodSmithing( int uses ) : base( uses, 0x0FB4 )
		{
			Weight = 20.0;
			Name = "Magical Golden Smithing Hammer";
			UsesRemaining = 10;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "The Legendary Smithing Hammer");
			list.Add( 1049644, "Can Only Be Used At The Dragon Head Forge");
        } 

		public GodSmithing( Serial serial ) : base( serial )
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
			if ( ItemID != 0x0FB4 && ItemID != 0x0FB5 ){ ItemID = 0x0FB4; }
		}
	}
	public class GodSewing : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefGodSewing.CraftSystem; } }

		[Constructable]
		public GodSewing() : base( 0x0F9F )
		{
			Weight = 2.0;
			Name = "Magical Golden Scissors";
			UsesRemaining = 10;
			Hue = 0x501;
		}

		[Constructable]
		public GodSewing( int uses ) : base( uses, 0x0F9F )
		{
			Weight = 2.0;
			Name = "Magical Golden Scissors";
			UsesRemaining = 10;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "The Legendary Sewing Scissors");
			list.Add( 1049644, "Can Only Be Used At The Enchanted Spinning Wheel");
        } 

		public GodSewing( Serial serial ) : base( serial )
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
	public class GodBrewing : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefGodBrewing.CraftSystem; } }

		[Constructable]
		public GodBrewing() : base( 0x0E28 )
		{
			Weight = 2.0;
			Name = "Magical Golden Alchemy Bottle";
			UsesRemaining = 10;
			Hue = 0x501;
		}

		[Constructable]
		public GodBrewing( int uses ) : base( uses, 0x0E28 )
		{
			Weight = 2.0;
			Name = "Magical Golden Alchemy Bottle";
			UsesRemaining = 10;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "The Legendary Alchemy Bottle");
			list.Add( 1049644, "Can Only Be Used At The Golden Alchemist");
        } 

		public GodBrewing( Serial serial ) : base( serial )
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