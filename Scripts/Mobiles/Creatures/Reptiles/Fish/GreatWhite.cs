using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a shark corpse" )]
	public class GreatWhite : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public GreatWhite() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a great white shark";
			Body = 394;
			BaseSoundID = 660;

			Hue = Utility.Random( 0x8A0, 5 );

			SetStr( 251, 425 );
			SetDex( 87, 135 );
			SetInt( 87, 155 );

			SetHits( 251, 355 );

			SetDamage( 6, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 59;
			CanSwim = true;
			CantWalk = true;
		}

		public override void OnThink()
		{
			if ( VirtualArmor < 60 )
			{
				if ( Worlds.TestShore( Map, X, Y, 10 ) ){ this.Delete(); }
				VirtualArmor = 60;
			}
			base.OnThink();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int Meat{ get{ return 10; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Scales{ get{ return 8; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Blue; } }
		public override bool BleedImmune{ get{ return true; } }

		public GreatWhite( Serial serial ) : base( serial )
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