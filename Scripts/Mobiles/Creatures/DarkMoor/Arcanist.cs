using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using System.Collections.Generic;

namespace Server.Mobiles
{
   [CorpseName( "a glowing corpse" )]
    public class Arcanist : BaseCreature
	{
		



		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ConcussionBlow;
		}

		[Constructable]
		public Arcanist() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
		    Race = Race.Human;

			if ( Female = Utility.RandomBool() )
			{
				Body = 401;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 400;
				Name = NameList.RandomName( "male" );
			}
			
			Title = "the Arcanist";
            Hue = Race.RandomSkinHue();

			SetStr( 800, 1199 );
			SetDex( 200, 250 );
			SetInt( 322, 396 );

			SetHits( 1491, 1598 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 60, 93 );
			SetResistance( ResistanceType.Fire, 60, 100 );
			SetResistance( ResistanceType.Cold, 40, 70 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 42, 55 );

			SetSkill( SkillName.MagicResist, 110.1, 132.6 );
			SetSkill( SkillName.Tactics, 86.9, 95.5 );
			SetSkill( SkillName.Wrestling, 42.2, 98.8 );
			SetSkill( SkillName.Magery, 97.1, 100.8 );
			SetSkill( SkillName.EvalInt, 91.1, 91.8 );
			SetSkill( SkillName.Meditation, 45.4, 94.1 );
			SetSkill( SkillName.Anatomy, 45.4, 74.1 );
			SetSkill( SkillName.Macing, 65.3, 79.3 );

			Fame = 7000;
			Karma = 10000;

			VirtualArmor = 65;
			
				
				//switch ( Utility.Random( 45 ) )
			//{
				//case 0: PackItem( new RuneEtchedRing() ); break;
			//}
			
			AddItem( new Sandals( 0x1 ) );
			AddItem( new Robe( 0x66D ) );
			AddItem( new BlackStaff() );
			AddItem( new WizardsHat( 0x1 ) );

			HairItemID = 0x203C;
			HairHue = 0x482;

		}

		public override void GenerateLoot()
		{
	      AddLoot(LootPack.UltraRich, 3);
		}
		
		public override bool AlwaysMurderer{get {return true; }}
		public override bool CanRummageCorpses{get {return true; }}
		
		public Arcanist( Serial serial ) : base( serial )
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
