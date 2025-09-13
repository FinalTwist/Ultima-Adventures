using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Misc;
using Server.Items;

namespace Server.Misc
{
    class Skulls
    {
		public static void MakeSkull( Mobile m, Container c, Mobile killer, string where )
		{
			int bone = 0;
			int color = m.Hue;

			if ( m is DemonOfTheSea ){ color = 490; bone = 2; }
			else if ( m is BloodDemon ){ bone = 2; }
			else if ( m is Devil ){ bone = 2; }
			else if ( m is TitanPyros ){ color = 0x846; bone = 2; }
			else if ( m is Balron ){ color = m.Hue; bone = 2; }
			else if ( m is Archfiend ){ color = 0x846; bone = 2; }
			else if ( m is Satan ){ color = 0x4AA; bone = 2; }
			else if ( m is LesserDemon ){ bone = 2; }
			else if ( m is Xurtzar ){ bone = 2; }
			else if ( m is FireDemon ){ bone = 2; }
			else if ( m is Demon ){ bone = 2; }
			else if ( m is IceDevil ){ color = 0x47F; bone = 2; }
			else if ( m is DeepSeaDevil ){ color = 490; bone = 2; }
			else if ( m is Daemon ){ color = m.Hue; bone = 2; }
			else if ( m is Fiend ){ color = 0x846; bone = 2; }
			else if ( m is DaemonTemplate ){ color = 0x846; bone = 2; }
			else if ( m is Daemonic ){ bone = 2; }
			else if ( m is BlackGateDemon ){ color = 0x497; bone = 2; }

			else if ( m is DeepSeaGiant ){ color = 1365; bone = 3; }
			else if ( m is Titan ){ bone = 3; }
			else if ( m is ElderTitan ){ bone = 3; }
			else if ( m is Ettin ){ bone = 3; }
			else if ( m is HillGiant ){ bone = 3; }
			else if ( m is HillGiantShaman ){ bone = 3; }
			else if ( m is AncientEttin ){ bone = 3; }
			else if ( m is AncientCyclops ){ bone = 3; }
			else if ( m is IceGiant ){ bone = 3; }
			else if ( m is LavaGiant ){ bone = 3; }
			else if ( m is MountainGiant ){ bone = 3; }
			else if ( m is ArcticEttin ){ color = 0x47E; bone = 3; }
			else if ( m is EttinShaman ){ bone = 3; }
			else if ( m is StoneGiant ){ bone = 3; }
			else if ( m is FireGiant ){ color = 0x54F; bone = 3; }
			else if ( m is ForestGiant ){ color = 0x7D5; bone = 3; }
			else if ( m is FrostGiant ){ color = 0x482; bone = 3; }
			else if ( m is JungleGiant ){ color = 0x7D1; bone = 3; }
			else if ( m is OrkDemigod ){ bone = 3; }
			else if ( m is UndeadGiant ){ bone = 3; }
			else if ( m is ZombieGiant ){ bone = 3; }
			else if ( m is GiantSkeleton ){ bone = 3; }
			else if ( m is SeaGiant ){ color = 490; bone = 3; }
			else if ( m is ShamanicCyclops ){ bone = 3; }
			else if ( m is Cyclops ){ bone = 3; }
			else if ( m is StormGiant ){ color = 0x9C2; bone = 3; }
			else if ( m is CloudGiant ){ color = 0xBB4; bone = 3; }
			else if ( m is StarGiant ){ color = 0xB73; bone = 3; }
			else if ( m is ZornTheBlacksmith ){ bone = 3; }

			else if ( m is GrayDragon ){ bone = 1; }
			else if ( m is BlueDragon ){ bone = 1; }
			else if ( m is MetalDragon ){ bone = 1; }
			else if ( m is Dragon ){ bone = 1; }
			else if ( m is Dragons ){ bone = 1; }
			else if ( m is RidingDragon ){ bone = 1; }
			else if ( m is StoneDragon ){ bone = 1; }
			else if ( m is WhiteDragon ){ bone = 1; }
			else if ( m is BlackDragon ){ bone = 1; }
			else if ( m is AsianDragon ){ bone = 1; }
			else if ( m is SeaDragon ){ bone = 1; }
			else if ( m is GreenDragon ){ bone = 1; }
			else if ( m is DragonGolem ){ bone = 1; }
			else if ( m is GemDragon ){ bone = 1; }
			else if ( m is Wyvern ){ bone = 1; }
			else if ( m is Wyverns ){ bone = 1; }

			else if ( m is CaddelliteDragon ){ bone = 4; }
			else if ( m is AncientWyrm ){ bone = 4; }
			else if ( m is AncientWyvern ){ bone = 4; }
			else if ( m is SkeletalDragon ){ bone = 4; }
			else if ( m is Dracolich ){ bone = 4; }
			else if ( m is BottleDragon ){ bone = 4; }
			else if ( m is RadiationDragon ){ bone = 4; }
			else if ( m is CrystalDragon ){ bone = 4; }
			else if ( m is VoidDragon ){ bone = 4; }
			else if ( m is ShadowWyrm ){ color = 0x497; bone = 4; }
			else if ( m is ZombieDragon ){ bone = 4; }
			else if ( m is VolcanicDragon ){ bone = 4; }
			else if ( m is PrimevalFireDragon ){ bone = 4; }
			else if ( m is PrimevalGreenDragon ){ bone = 4; }
			else if ( m is PrimevalNightDragon ){ bone = 4; }
			else if ( m is PrimevalRedDragon ){ bone = 4; }
			else if ( m is PrimevalRoyalDragon ){ bone = 4; }
			else if ( m is PrimevalRunicDragon ){ bone = 4; }
			else if ( m is PrimevalSeaDragon ){ bone = 4; }
			else if ( m is ReanimatedDragon ){ bone = 4; }
			else if ( m is VampiricDragon ){ bone = 4; }
			else if ( m is PrimevalAbysmalDragon ){ bone = 4; }
			else if ( m is PrimevalAmberDragon ){ bone = 4; }
			else if ( m is PrimevalBlackDragon ){ bone = 4; }
			else if ( m is PrimevalDragon ){ bone = 4; }
			else if ( m is PrimevalSilverDragon ){ bone = 4; }
			else if ( m is PrimevalVolcanicDragon ){ bone = 4; }
			else if ( m is PrimevalStygianDragon ){ bone = 4; }
			else if ( m is AshDragon ){ bone = 4; }
			else if ( m is DragonKing ){ bone = 4; }
			else if ( m is ElderDragon ){ bone = 4; }
			else if ( m is SlasherOfVoid ){ bone = 4; }

			else if ( m is RottingMinotaur ){ bone = 5; }
			else if ( m is MinotaurCaptain ){ bone = 5; }
			else if ( m is MinotaurScout ){ bone = 5; }
			else if ( m is Minotaur ){ bone = 5; }

			else if ( m is Dracolich ){ bone = 6; }
			else if ( m is SkeletalDragon ){ bone = 6; }
			else if ( m is NightWyrm ){ bone = 6; }
			else if ( m is OnyxWyrm ){ bone = 6; }
			else if ( m is EmeraldWyrm ){ bone = 6; }
			else if ( m is AmethystWyrm ){ bone = 6; }
			else if ( m is SapphireWyrm ){ bone = 6; }
			else if ( m is GarnetWyrm ){ bone = 6; }
			else if ( m is TopazWyrm ){ bone = 6; }
			else if ( m is RubyWyrm ){ bone = 6; }
			else if ( m is SpinelWyrm ){ bone = 6; }
			else if ( m is QuartzWyrm ){ bone = 6; }
			else if ( m is JungleWyrm ){ bone = 6; }
			else if ( m is DesertWyrm ){ bone = 6; }
			else if ( m is MountainWyrm ){ bone = 6; }
			else if ( m is DeepSeaDragon ){ bone = 6; }
			else if ( m is WhiteWyrm ){ color = 1150; bone = 6; }
			else if ( m is Wyrms ){ color = 1150; }
			else if ( m is IceDragon ){ bone = 6; }
			else if ( m is LavaDragon ){ bone = 6; }

			if ( bone == 1 )
			{
				SkullDragon head = new SkullDragon();
				head.Name = "skull of " + m.Name;
					if ( m.Title != "" ){ head.Name = head.Name + " " + m.Title; }

				head.Hue = color;
				head.SkullWhere = where;
				head.SkullKiller = killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( killer );
				c.DropItem( head );
			}
			else if ( bone == 2 )
			{
				SkullDemon head = new SkullDemon();
				head.Name = "skull of " + m.Name;
					if ( m.Title != "" ){ head.Name = head.Name + " " + m.Title; }

				head.Hue = color;
				head.SkullWhere = where;
				head.SkullKiller = killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( killer );
				c.DropItem( head );
			}
			else if ( bone == 3 )
			{
				SkullGiant head = new SkullGiant();
				head.Name = "skull of " + m.Name;
					if ( m.Title != "" ){ head.Name = head.Name + " " + m.Title; }

				head.Hue = color;
				head.SkullWhere = where;
				head.SkullKiller = killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( killer );
				c.DropItem( head );
			}
			else if ( bone == 4 )
			{
				SkullGreatDragon head = new SkullGreatDragon();
				head.Name = "skull of " + m.Name;
					if ( m.Title != "" ){ head.Name = head.Name + " " + m.Title; }

				head.Hue = color;
				head.SkullWhere = where;
				head.SkullKiller = killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( killer );
				c.DropItem( head );
			}
			else if ( bone == 5 )
			{
				SkullMinotaur head = new SkullMinotaur();
				head.Name = "skull of " + m.Name;
					if ( m.Title != "" ){ head.Name = head.Name + " " + m.Title; }

				head.Hue = color;
				head.SkullWhere = where;
				head.SkullKiller = killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( killer );
				c.DropItem( head );
			}
			else if ( bone == 6 )
			{
				SkullWyrm head = new SkullWyrm();
				head.Name = "skull of " + m.Name;
					if ( m.Title != "" ){ head.Name = head.Name + " " + m.Title; }

				head.Hue = color;
				head.SkullWhere = where;
				head.SkullKiller = killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( killer );
				c.DropItem( head );
			}

			if ( m is LavaGiant && Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				HeartOfFire heart = new HeartOfFire();
				heart.Name = "heart of " + m.Name;
					if ( m.Title != "" ){ heart.Name = heart.Name + " " + m.Title; }

				heart.HeartWhere = where;
				heart.HeartKiller = killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( killer );
				c.DropItem( heart );
			}
			else if ( m is IceGiant && Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				HeartOfIce heart = new HeartOfIce();
				heart.Name = "heart of " + m.Name;
					if ( m.Title != "" ){ heart.Name = heart.Name + " " + m.Title; }

				heart.HeartWhere = where;
				heart.HeartKiller = killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( killer );
				c.DropItem( heart );
			}
		}
	}
}

namespace Server.Items
{
	public class HeartOfIce: Item
	{
		public string HeartKiller;
		public string HeartWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Heart_Killer { get { return HeartKiller; } set { HeartKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Heart_Where { get { return HeartWhere; } set { HeartWhere = value; InvalidateProperties(); } }

		[Constructable]
		public HeartOfIce( ) : base( 0x1444 )
		{
			Weight = 10.0;
			Hue = 0x480;
			Movable = true;
			Light = LightType.Circle225;
			Name = "ice heart";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From " + HeartWhere );
			list.Add( 1049644, "Slain by " + HeartKiller );
        }

		public HeartOfIce(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( HeartKiller );
            writer.Write( HeartWhere );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            HeartKiller = reader.ReadString();
            HeartWhere = reader.ReadString();
		}
	}

	public class HeartOfFire: Item
	{
		public string HeartKiller;
		public string HeartWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Heart_Killer { get { return HeartKiller; } set { HeartKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Heart_Where { get { return HeartWhere; } set { HeartWhere = value; InvalidateProperties(); } }

		[Constructable]
		public HeartOfFire( ) : base( 0x81E )
		{
			Weight = 10.0;
			Movable = true;
			Light = LightType.Circle225;
			Name = "fire heart";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From " + HeartWhere );
			list.Add( 1049644, "Slain by " + HeartKiller );
        }

		public HeartOfFire(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( HeartKiller );
            writer.Write( HeartWhere );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            HeartKiller = reader.ReadString();
            HeartWhere = reader.ReadString();
		}
	}

	[Furniture]
	[Flipable(0x3DE0, 0x3DE1)]
	public class SkullMinotaur: Item
	{
		public string SkullKiller;
		public string SkullWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Killer { get { return SkullKiller; } set { SkullKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Where { get { return SkullWhere; } set { SkullWhere = value; InvalidateProperties(); } }

		[Constructable]
		public SkullMinotaur( ) : base( 0x3DE0 )
		{
			Weight = 10.0;
			Name = "minotaur skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From " + SkullWhere );
			list.Add( 1049644, "Slain by " + SkullKiller );
        }

		public SkullMinotaur(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( SkullKiller );
            writer.Write( SkullWhere );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            SkullKiller = reader.ReadString();
            SkullWhere = reader.ReadString();
		}
	}
	[Furniture]
	[Flipable(0x3DCC, 0x3DCD)]
	public class SkullWyrm : Item
	{
		public string SkullKiller;
		public string SkullWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Killer { get { return SkullKiller; } set { SkullKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Where { get { return SkullWhere; } set { SkullWhere = value; InvalidateProperties(); } }

		[Constructable]
		public SkullWyrm( ) : base( 0x3DCC )
		{
			Weight = 10.0;
			Name = "dragon skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From " + SkullWhere );
			list.Add( 1049644, "Slain by " + SkullKiller );
        }

		public SkullWyrm(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( SkullKiller );
            writer.Write( SkullWhere );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            SkullKiller = reader.ReadString();
            SkullWhere = reader.ReadString();
		}
	}
	[Furniture]
	[Flipable(0x1AEE, 0x1AEF)]
	public class SkullGreatDragon : Item
	{
		public string SkullKiller;
		public string SkullWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Killer { get { return SkullKiller; } set { SkullKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Where { get { return SkullWhere; } set { SkullWhere = value; InvalidateProperties(); } }

		[Constructable]
		public SkullGreatDragon( ) : base( 0x1AEE )
		{
			Weight = 20.0;
			Name = "dragon skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From " + SkullWhere );
			list.Add( 1049644, "Slain by " + SkullKiller );
        }

		public SkullGreatDragon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( SkullKiller );
            writer.Write( SkullWhere );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            SkullKiller = reader.ReadString();
            SkullWhere = reader.ReadString();
		}
	}
	[Furniture]
	[Flipable(8782, 8783)]
	public class SkullDragon : Item
	{
		public string SkullKiller;
		public string SkullWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Killer { get { return SkullKiller; } set { SkullKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Where { get { return SkullWhere; } set { SkullWhere = value; InvalidateProperties(); } }

		[Constructable]
		public SkullDragon( ) : base( 8782 )
		{
			Weight = 10.0;
			Name = "dragon skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From " + SkullWhere );
			list.Add( 1049644, "Slain by " + SkullKiller );
        }

		public SkullDragon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( SkullKiller );
            writer.Write( SkullWhere );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            SkullKiller = reader.ReadString();
            SkullWhere = reader.ReadString();
		}
	}
	[Furniture]
	[Flipable(8784, 8785)]
	public class SkullDemon : Item
	{
		public string SkullKiller;
		public string SkullWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Killer { get { return SkullKiller; } set { SkullKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Where { get { return SkullWhere; } set { SkullWhere = value; InvalidateProperties(); } }

		[Constructable]
		public SkullDemon( ) : base( 8784 )
		{
			Weight = 10.0;
			Name = "demon skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From " + SkullWhere );
			list.Add( 1049644, "Slain by " + SkullKiller );
        }

		public SkullDemon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( SkullKiller );
            writer.Write( SkullWhere );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            SkullKiller = reader.ReadString();
            SkullWhere = reader.ReadString();
		}
	}
	[Furniture]
	[Flipable(0x224, 0x225)]
	public class SkullGiant : Item
	{
		public string SkullKiller;
		public string SkullWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Killer { get { return SkullKiller; } set { SkullKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Skull_Where { get { return SkullWhere; } set { SkullWhere = value; InvalidateProperties(); } }

		[Constructable]
		public SkullGiant( ) : base( 0x224 )
		{
			Weight = 10.0;
			Name = "giant skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From " + SkullWhere );
			list.Add( 1049644, "Slain by " + SkullKiller );
        }

		public SkullGiant(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( SkullKiller );
            writer.Write( SkullWhere );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            SkullKiller = reader.ReadString();
            SkullWhere = reader.ReadString();
		}
	}
}