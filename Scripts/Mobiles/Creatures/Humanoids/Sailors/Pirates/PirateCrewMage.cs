using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a pirate corpse" )] 
	public class PirateCrewMage : BaseCreature 
	{ 
		[Constructable] 
		public PirateCrewMage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			if ( this.Female = Utility.RandomBool() ) 
			{ 
				Body = 0x191; 
				Name = NameList.RandomName( "evil witch" );
				Title = "the pirate mage";
			} 
			else 
			{ 
				Body = 0x190; 
				Name = NameList.RandomName( "evil mage" );
				Title = "the pirate mage";
			}

			Title = "the pirate";
            ((BaseCreature)this).midrace = 4;
            Hue = Server.Misc.RandomThings.GetRandomSkinColor();
			Utility.AssignRandomHair( this );
			HairHue = Utility.RandomHairHue();

			if ( Utility.RandomBool() )
			{
				AddItem( new PirateCoat() );
				switch ( Utility.Random( 2 ))
				{
					case 0: AddItem( new PirateHat () ); break;
					case 1: AddItem( new SkullCap () ); break;
				}

				switch ( Utility.Random( 2 ))
				{
					case 0: AddItem( new LongPants ( 0xBB4 ) ); break;
					case 1: AddItem( new ShortPants ( 0xBB4 ) ); break;
				}
			}
			else
			{
				AddItem( new ElvenBoots( 0x83A ) );
				Item armor = new LeatherChest(); armor.Hue = 0x83A; AddItem( armor );
				AddItem( new FancyShirt( 0 ) );
				AddItem( new Cloak( 0x83A ) );

				switch ( Utility.Random( 2 ))
				{
					case 0: AddItem( new LongPants ( 0xBB4 ) ); break;
					case 1: AddItem( new ShortPants ( 0xBB4 ) ); break;
				}				

				switch ( Utility.Random( 2 ))
				{
					case 0: AddItem( new Bandana ( 0x846 ) ); break;
					case 1: AddItem( new SkullCap ( 0x846 ) ); break;
				}
			}

			SetStr( 81, 105 );
			SetDex( 191, 215 );
			SetInt( 126, 150 );

			SetHits( 49, 63 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 80.2, 100.0 );
			SetSkill( SkillName.Magery, 95.1, 100.0 );
			SetSkill( SkillName.Meditation, 27.5, 50.0 );
			SetSkill( SkillName.MagicResist, 77.5, 100.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.3, 80.0 );

			Fame = 10500;
			Karma = -10500;

			VirtualArmor = 16;
			PackReg( 23 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public PirateCrewMage( Serial serial ) : base( serial ) 
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