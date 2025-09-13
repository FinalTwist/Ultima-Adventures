using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Regions;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a dark hound corpse" )]
	public class DarkHound : BaseCreature
	{
		[Constructable]
		public DarkHound() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater dark hound";
			Body = 225;
			BaseSoundID = 0xE5;
			Hue = 0x497;

			SetStr( 96, 220 );
			SetDex( 81, 185 );
			SetInt( 36, 60 );

			SetHits( 58, 142 );
			SetMana( 0 );

			SetDamage( 11, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 57.6, 75.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 3000;
			Karma = -2500;

			VirtualArmor = 22;
		}
		else
		{
			Name = "a dark hound";
			Body = 225;
			BaseSoundID = 0xE5;
			Hue = 0x497;

			SetStr( 96, 120 );
			SetDex( 81, 105 );
			SetInt( 36, 60 );

			SetHits( 58, 72 );
			SetMana( 0 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 57.6, 75.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 22;
		}
	}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 7; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public DarkHound(Serial serial) : base(serial)
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