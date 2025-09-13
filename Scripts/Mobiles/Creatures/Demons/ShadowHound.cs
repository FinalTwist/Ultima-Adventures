using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a shadow hound corpse" )]
	public class ShadowHound : BaseCreature
	{
		public override bool CanChew { get{return true;}}
		[Constructable]
		public ShadowHound() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a shadow hound";
			Body = 277;
			BaseSoundID = 0xE5;
			Hue = 0x4001;

			SetStr( 196, 220 );
			SetDex( 76, 95 );
			SetInt( 91, 115 );

			SetHits( 164, 398 );
			SetMana( 0 );

			SetDamage( 9, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 20, 25 );
			SetResistance( ResistanceType.Poison, 10, 15 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 20.1, 35.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 2000;
			Karma = -2000;

			VirtualArmor = 32;
		}

		public ShadowHound( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}