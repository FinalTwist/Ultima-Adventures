using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a wisp corpse" )]
	public class ShadowWisp : BaseCreature
	{
		[Constructable]
		public ShadowWisp() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = "a shadow wisp";
			Body = 58;
			Hue = 1109;
			BaseSoundID = 466;

			SetStr( 146, 175 );
			SetDex( 76, 95 );
			SetInt( 81, 105 );

			SetHits( 88, 105 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 15, 25 );

			SetSkill( SkillName.EvalInt, 70.1, 85.0 );
			SetSkill( SkillName.Magery, 70.1, 85.0 );
			SetSkill( SkillName.MagicResist, 70.1, 85.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 40.1, 80.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 32;

			AddItem( new LightSource() );

			switch ( Utility.Random( 10 ))
			{
				case 0: PackItem( new LeftArm() ); break;
				case 1: PackItem( new RightArm() ); break;
				case 2: PackItem( new Torso() ); break;
				case 3: PackItem( new Bone() ); break;
				case 4: PackItem( new RibCage() ); break;
				case 5: PackItem( new RibCage() ); break;
				case 6: PackItem( new BonePile() ); break;
				case 7: PackItem( new BonePile() ); break;
				case 8: PackItem( new BonePile() ); break;
				case 9: PackItem( new BonePile() ); break;
			}
		}

		public ShadowWisp( Serial serial ) : base( serial )
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