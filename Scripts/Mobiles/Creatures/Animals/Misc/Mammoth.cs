using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a mammoth corpse" )]
	public class Mammoth : Elephant
	{
		[Constructable]
		public Mammoth()
		{
			if (Utility.RandomDouble() > 0.85)
				{
			Name = "a greater mammoth";
			Hue = 0xB5B;

			SetStr( 326, 455 );
			SetDex( 81, 205 );
			SetInt( 16, 40 );

			SetHits( 276, 393 );

			SetDamage( 14, 29 );

			Fame = 3500;

			VirtualArmor = 35;

			ControlSlots = 2;
			MinTameSkill = 89.1;

		}
		else
		{
			Name = "a mammoth";
			Hue = 0xB5B;

			SetStr( 326, 355 );
			SetDex( 81, 105 );
			SetInt( 16, 40 );

			SetHits( 276, 293 );

			SetDamage( 14, 20 );

			Fame = 3000;

			VirtualArmor = 35;

			ControlSlots = 2;
			MinTameSkill = 82.1;

		}
		}

		public override FurType FurType{ get{ return FurType.White; } }
		public override HideType HideType{ get{ return HideType.Frozen; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 16 ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay | FoodType.FruitsAndVegies; } }
		public override bool CanAngerOnTame { get {
			if (Utility.RandomDouble() > 0.90 )
				return true;
			return false; } }

		public override void OnAfterSpawn()
		{
			Region reg = Region.Find( this.Location, this.Map );

			if ( reg.IsPartOf( "the Glacial Scar" ) )
			{
				AI = AIType.AI_Melee;
				FightMode = FightMode.Closest;
				Tamable = false;
				NameHue = 0x22;
			}

			base.OnAfterSpawn();
		}

		public Mammoth( Serial serial ) : base( serial )
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