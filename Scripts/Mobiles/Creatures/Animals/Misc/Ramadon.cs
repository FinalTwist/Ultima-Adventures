using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using System.Collections.Generic;
using Server.Mobiles;
using Server.ContextMenus;

namespace Server.Mobiles
{
	[CorpseName( "a ramadon corpse" )]
	public class Ramadon : BaseMount
	{
		public override bool CanChew { get{return true;}}
		[Constructable]
		public Ramadon() : this( "a ramadon" )
		{
		}

		[Constructable]
		public Ramadon( string name ) : base( name, 0x11C, 0x3E92, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0xA3;
			Hue = 0xB70;

			SetStr( 377, 518 );
			SetDex( 87, 103 );
			SetInt( 25, 30 );

			SetHits( 575, 666 );
			SetStam( 87, 103 );
			SetMana( 0 );

			SetDamage( 20, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 67.1, 74.5 );
			SetSkill( SkillName.Anatomy, 96.5, 104.0 );
			SetSkill( SkillName.Tactics, 95.8, 102.6 );
			SetSkill( SkillName.Wrestling, 100.5, 111.4 );

			Fame = 5000;
			Karma = 0;

			VirtualArmor = 32;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 67.1;
		}

		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 22; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay | FoodType.FruitsAndVegies; } }
		public override HideType HideType{ get{ return HideType.Frozen; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 30 ); } }
		public override FurType FurType{ get{ return FurType.White; } }

		public Ramadon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}