using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a farmers chicken corpse" )]
	public class FarmersChicken : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public FarmersChicken() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a farmers chicken";
			Body = 0xD0;
			BaseSoundID = 0x6E;
            Hue = 1153;
            
			SetStr( 450 );
			SetDex( 350 );
			SetInt( 5 );

			SetHits( 1350 );
			SetMana( 0 );

			SetDamage( 50 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 1, 5 );

			SetSkill( SkillName.MagicResist, 140.0 );
			SetSkill( SkillName.Tactics, 85.0 );
			SetSkill( SkillName.Wrestling, 125.0 );

			Fame = 150;
			Karma = 0;

			VirtualArmor = 54;
			ControlSlots = 2;

			PackItem( new PureWhiteFeather( 1 ) );
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }

		public override int Feathers{ get{ return 25; } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public FarmersChicken( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
