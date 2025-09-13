using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class ShieldOfEarthPotion : SpellScroll
	{
		[Constructable]
		public ShieldOfEarthPotion() : this( 1 )
		{
		}

		[Constructable]
		public ShieldOfEarthPotion( int amount ) : base( 147, 0x282F, amount )
		{
			Name = "shield of earth liquid";
			Hue = 0x300;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Causes a wall of foliage to grow, blocking the way" );
			list.Add( 1049644, "Requires 20 Animal Lore"); // PARENTHESIS
		}

		public ShieldOfEarthPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class WoodlandProtectionPotion : SpellScroll
	{
		[Constructable]
		public WoodlandProtectionPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public WoodlandProtectionPotion( int amount ) : base( 148, 0x282F, amount )
		{
			Name = "woodland protection oil";
			Hue = 0x7E2;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Increases one's protection by making their skin like bark" );
			list.Add( 1049644, "Requires 25 Animal Lore"); // PARENTHESIS
		}

		public WoodlandProtectionPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class ProtectiveFairyPotion : SpellScroll
	{
		[Constructable]
		public ProtectiveFairyPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public ProtectiveFairyPotion( int amount ) : base( 149, 0x282F, amount )
		{
			Name = "fairy in a bottle";
			Hue = 0x9FF;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Releases the fairy in the bottle to a help the adventurer" );
			list.Add( 1049644, "Requires 70 Animal Lore"); // PARENTHESIS
		}

		public ProtectiveFairyPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class HerbalHealingPotion : SpellScroll
	{
		[Constructable]
		public HerbalHealingPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public HerbalHealingPotion( int amount ) : base( 150, 0x282F, amount )
		{
			Name = "herbal healing elixir";
			Hue = 0x279;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Heals the target of all ailments" );
			list.Add( 1049644, "Requires 45 Animal Lore"); // PARENTHESIS
		}

		public HerbalHealingPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class GraspingRootsPotion : SpellScroll
	{
		[Constructable]
		public GraspingRootsPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public GraspingRootsPotion( int amount ) : base( 151, 0x282F, amount )
		{
			Name = "grasping roots mixture";
			Hue = 0x83F;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Releases roots from the ground to entangle a single target" );
			list.Add( 1049644, "Requires 35 Animal Lore"); // PARENTHESIS
		}

		public GraspingRootsPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class BlendWithForestPotion : SpellScroll
	{
		[Constructable]
		public BlendWithForestPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public BlendWithForestPotion( int amount ) : base( 152, 0x282F, amount )
		{
			Name = "forest blending oil";
			Hue = 0x59C;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Allows one to blend seamlessly with the forest" );
			list.Add( 1049644, "Requires 50 Animal Lore"); // PARENTHESIS
		}

		public BlendWithForestPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class SwarmOfInsectsPotion : SpellScroll
	{
		[Constructable]
		public SwarmOfInsectsPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public SwarmOfInsectsPotion( int amount ) : base( 153, 0x282F, amount )
		{
			Name = "bottle of insects";
			Hue = 0xA70;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Releases a swarm of insects from the bottle that bite and sting enemies" );
			list.Add( 1049644, "Requires 65 Animal Lore"); // PARENTHESIS
		}

		public SwarmOfInsectsPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class VolcanicEruptionPotion : SpellScroll
	{
		[Constructable]
		public VolcanicEruptionPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public VolcanicEruptionPotion( int amount ) : base( 154, 0x282F, amount )
		{
			Name = "volcanic fluid";
			Hue = 0x54E;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Causes molten lava to burst from the ground, hitting every enemy nearby" );
			list.Add( 1049644, "Requires 80 Animal Lore"); // PARENTHESIS
		}

		public VolcanicEruptionPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class TreefellowPotion : SpellScroll
	{
		[Constructable]
		public TreefellowPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public TreefellowPotion( int amount ) : base( 155, 0x282F, amount )
		{
			Name = "treant fertilizer";
			Hue = 0x223;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Causes a living tree to grow and assist the adventurer" );
			list.Add( 1049644, "Requires 75 Animal Lore"); // PARENTHESIS
		}

		public TreefellowPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class StoneCirclePotion : SpellScroll
	{
		[Constructable]
		public StoneCirclePotion() : this( 1 )
		{
		}
		
		[Constructable]
		public StoneCirclePotion( int amount ) : base( 156, 0x282F, amount )
		{
			Name = "stone rising concoction";
			Hue = 0x396;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Causes stones to push up from the ground, trapping enemies" );
			list.Add( 1049644, "Requires 30 Animal Lore"); // PARENTHESIS
		}

		public StoneCirclePotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DruidicRunePotion : SpellScroll
	{
		[Constructable]
		public DruidicRunePotion() : this( 1 )
		{
		}
		
		[Constructable]
		public DruidicRunePotion( int amount ) : base( 157, 0x282F, amount )
		{
			Name = "druidic marking oil";
			Hue = 0x487;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Marks a rune with the one's location" );
			list.Add( 1049644, "Requires 40 Animal Lore"); // PARENTHESIS
		}

		public DruidicRunePotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class LureStonePotion : SpellScroll
	{
		[Constructable]
		public LureStonePotion() : this( 1 )
		{
		}
		
		[Constructable]
		public LureStonePotion( int amount ) : base( 158, 0x282F, amount )
		{
			Name = "stone in a bottle";
			Hue = 0x967;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Dumps out a magical stone that calls all nearby animals to it" );
			list.Add( 1049644, "Requires 10 Animal Lore"); // PARENTHESIS
		}

		public LureStonePotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class NaturesPassagePotion : SpellScroll
	{
		[Constructable]
		public NaturesPassagePotion() : this( 1 )
		{
		}
		
		[Constructable]
		public NaturesPassagePotion( int amount ) : base( 159, 0x282F, amount )
		{
			Name = "nature passage mixture";
			Hue = 0x48B;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Turns one into flower petals and carries them on the wind to a recall rune location" );
			list.Add( 1049644, "Requires 15 Animal Lore"); // PARENTHESIS
		}

		public NaturesPassagePotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class MushroomGatewayPotion : SpellScroll
	{
		[Constructable]
		public MushroomGatewayPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public MushroomGatewayPotion( int amount ) : base( 160, 0x282F, amount )
		{
			Name = "mushroom gateway growth";
			Hue = 0x3B7;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Causes magical mushrooms to grow a portal to another location" );
			list.Add( 1049644, "Requires 60 Animal Lore"); // PARENTHESIS
		}

		public MushroomGatewayPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class RestorativeSoilPotion : SpellScroll
	{
		[Constructable]
		public RestorativeSoilPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public RestorativeSoilPotion( int amount ) : base( 161, 0x282F, amount )
		{
			Name = "bottle of magical mud";
			Hue = 0x479;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Dumping this magical mud can restore life to the dead that walk on it" );
			list.Add( 1049644, "Requires 85 Animal Lore"); // PARENTHESIS
		}

		public RestorativeSoilPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class FireflyPotion : SpellScroll
	{
		[Constructable]
		public FireflyPotion() : this( 1 )
		{
		}
		
		[Constructable]
		public FireflyPotion( int amount ) : base( 162, 0x282F, amount )
		{
			Name = "bottle of fireflies";
			Hue = 0x491;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Releases fireflies to distract one from battle" );
			list.Add( 1049644, "Requires 55 Animal Lore"); // PARENTHESIS
		}

		public FireflyPotion( Serial serial ) : base( serial )
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
			ItemID = 0x282F;
		}
	}
}
