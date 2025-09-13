using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "Bad little Boy" )]
	public class AEvilElf : BaseCreature
	{
		[Constructable]
		public AEvilElf() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "A Evil Elf";
			Body = 400;
			BaseSoundID = 357;

			SetStr( 80, 110 );
			SetDex( 80, 90 );
			SetInt( 80, 90 );

			SetHits( 200 );

			SetDamage( 5, 20 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 40, 40 );
			SetResistance( ResistanceType.Fire, 40 );
			SetResistance( ResistanceType.Cold, 40, 40 );
			SetResistance( ResistanceType.Poison, 34 );
			SetResistance( ResistanceType.Energy, 40, 40 );

			SetSkill( SkillName.Anatomy, 90 );
			SetSkill( SkillName.EvalInt, 50.1, 70.0 );
			SetSkill( SkillName.Magery, 50.5, 70.0 );
			SetSkill( SkillName.Meditation, 60 );
			SetSkill( SkillName.MagicResist, 40.5, 50.0 );
			SetSkill( SkillName.Tactics, 60.1, 60.0 );
			SetSkill( SkillName.Wrestling, 60.1, 60.0 );

			Fame = 24000;
			Karma = -24000;

			Kills = 10;

			Hue = 0;

			VirtualArmor = 12;

			Item Boots = new Boots();
			Boots.Hue = 1369;
      	    Boots.Name = "Elf Boots";
			Boots.Movable = false;
			AddItem( Boots ); 

			Item Doublet = new Doublet();
			Doublet.Hue = 1369;
      	    Doublet.Name = "Elf's Doublet";
			Doublet.Movable = false;
			AddItem( Doublet ); 

			Item FancyShirt = new FancyShirt();
			FancyShirt.Hue = 1369;
      	    FancyShirt.Name = "Elf's Shirt";
			FancyShirt.Movable = false;
			AddItem( FancyShirt ); 

			Item LongPants = new LongPants();
			LongPants.Hue = 1369;
      	    LongPants.Name = "Elf's Pants";
			LongPants.Movable = false;
			AddItem( LongPants ); 


 			PackItem( new YellowSnow() );


		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public AEvilElf( Serial serial ) : base( serial )
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