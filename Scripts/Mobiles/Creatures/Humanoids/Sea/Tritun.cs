using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a tritun corpse" )]
	public class Tritun : BaseCreature
	{
		[Constructable]
		public Tritun() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a tritun";
			Body = 690;
			BaseSoundID = 0x553;

			SetStr( 116, 135 );
			SetDex( 106, 125 );
			SetInt( 71, 85 );

			SetDamage( 23, 27 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.Fencing, 60.0, 82.5 );
			SetSkill( SkillName.Macing, 60.0, 82.5 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 57.5, 80.0 );
			SetSkill( SkillName.Swords, 60.0, 82.5 );
			SetSkill( SkillName.Tactics, 60.0, 82.5 );

			Fame = 1100;
			Karma = -1100;
			VirtualArmor = 20;

			Pitchfork wep = new Pitchfork();
			  	wep.Hue = 0x499;
				wep.Name = "poseidon trident";
				wep.MinDamage = wep.MinDamage + 3;
				wep.MaxDamage = wep.MaxDamage + 5;
			  	PackItem( wep );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int Scales{ get{ return 1; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Blue ); } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }

		public Tritun( Serial serial ) : base( serial )
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