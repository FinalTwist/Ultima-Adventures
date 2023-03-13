using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "Peg Leg's Corpse" )]
	public class PegLeg : BaseCreature
	{
		[Constructable]
		public PegLeg() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Peg Leg";
			Body = 159;

			SetStr( 60, 110 );
			SetDex( 60, 100 );
			SetInt( 60, 100 );

			SetHits( 200 );

			SetDamage( 5, 25 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 60 );
			SetDamageType( ResistanceType.Energy, 10 );

			SetResistance( ResistanceType.Physical, 30, 80 );
			SetResistance( ResistanceType.Fire, 20, 100 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 20 );
			SetResistance( ResistanceType.Energy, 0, 20 );

			SetSkill( SkillName.Anatomy, 120 );
			SetSkill( SkillName.EvalInt, 90, 95.0 );
			SetSkill( SkillName.Magery, 50.5, 100.0 );
			SetSkill( SkillName.Meditation, 60 );
			SetSkill( SkillName.MagicResist, 10.1, 50.0 );
			SetSkill( SkillName.Tactics, 115, 125 );
			SetSkill( SkillName.Swords, 115, 125 );

			Fame = 8000;
			Karma = -8000;

			Kills = 10;

			Hue = 1271;

			VirtualArmor = 50;

			LeatherArms LeatherArms = new LeatherArms();
			LeatherArms.Hue = 1175;
			AddItem( LeatherArms );
			
			LeatherCap LeatherCap = new LeatherCap();
			LeatherCap.Hue = 1157;
			AddItem( LeatherCap );
			
			LeatherGloves LeatherGloves = new LeatherGloves();
			LeatherGloves.Hue = 1175;
			AddItem( LeatherGloves );

			LeatherLegs LeatherLegs = new LeatherLegs();
			LeatherLegs.Hue = 1157;
			AddItem( LeatherLegs );
			
			LeatherChest LeatherChest = new LeatherChest();
			LeatherChest.Hue = 1175;
			AddItem( LeatherChest );

			LeatherGorget LeatherGorget = new LeatherGorget();
			LeatherGorget.Hue = 1157;
			AddItem( LeatherGorget );

			//AddItem( new SparrowBlade() );
 			PackItem( new PegLeghook() );
			PackItem( new PegLegHead() );


		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public PegLeg( Serial serial ) : base( serial )
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