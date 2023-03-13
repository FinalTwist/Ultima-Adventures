using System;
using System.Collections;
using Server.Items;
using Server.Misc;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a rotting corpse" )]
	public class Zombie : BaseCreature
	{
		[Constructable]
		public Zombie() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = Utility.RandomList( 3, 728 );

			switch( Utility.RandomMinMax( 0, 9 ) )
			{
				case 0: Name = "a zombie";			break;
				case 1: Name = "a walking dead";	break;
				case 2: Name = "a corpse";			break;
				case 3: Name = "a rotten corpse";	break;
				case 4: Name = "an undead corpse";	break;
				case 5: Name = "a rotting zombie";	break;
				case 6: Name = "a zombie";			break;
				case 7: Name = "a decaying zombie";	break;
				case 8: Name = "a decaying corpse";	break;
				case 9: Name = "a walking corpse";	break;
			}

			Hue = 0xB97;
			switch( Utility.RandomMinMax( 0, 12 ) )
			{
				case 0: Hue = 0x83B;	break;
				case 1: Hue = 0x89F;	break;
				case 2: Hue = 0x8A0;	break;
				case 3: Hue = 0x8A1;	break;
				case 4: Hue = 0x8A2;	break;
				case 5: Hue = 0x8A3;	break;
				case 6: Hue = 0x8A4;	break;
			}

			int[] list = new int[]
				{
					0x1CF0, 0x1CEF, 0x1CEE, 0x1CED, 0x1CE9, 0x1DA0, 0x1DAE, // pieces
					0x1CEC, 0x1CE5, 0x1CE2, 0x1CDD, 0x1AE4, 0x1DA1, 0x1DA2, 0x1DA4, 0x1DAF, 0x1DB0, 0x1DB1, 0x1DB2, // limbs
					0x1CE8, 0x1CE0, 0x1D9F, 0x1DAD // torsos
				};

			PackItem( new BodyPart( Utility.RandomList( list ) ) );

			BaseSoundID = 471;

			SetStr( 46, 70 );
			SetDex( 31, 50 );
			SetInt( 26, 40 );

			SetHits( 28, 42 );

			SetDamage( 3, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 5, 10 );

			SetSkill( SkillName.MagicResist, 15.1, 40.0 );
			SetSkill( SkillName.Tactics, 35.1, 50.0 );
			SetSkill( SkillName.Wrestling, 35.1, 50.0 );

			Fame = 600;
			Karma = -600;

			VirtualArmor = 18;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 3;
			return base.OnBeforeDeath();
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison HitPoison{ get{ return Poison.Lesser; } }
		public override int Hides{ get{ return 3; } }
		public override HideType HideType{ get{ return HideType.Necrotic; } }

		public static int GetDeadHue()
		{
			int Hue = 0;
			switch( Utility.RandomMinMax( 0, 10 ) )
			{
				case 0: Hue = 0xB97;	break;
				case 1: Hue = 0xB98;	break;
				case 2: Hue = 0xB99;	break;
				case 3: Hue = 0xB9A;	break;
				case 4: Hue = 0xB88;	break;
				case 5: Hue = 0x83B;	break;
				case 6: Hue = 0x83C;	break;
				case 7: Hue = 0x83D;	break;
				case 8: Hue = 0x83E;	break;
				case 9: Hue = 0x83F;	break;
				case 10: Hue = 0x840;	break;
			}
			return Hue;
		}

		public Zombie( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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