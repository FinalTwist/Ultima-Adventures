using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{
	[CorpseName( "a pirate corpse" )] 

	public class ElfPirateCaptain : BaseCreature 
	{
		[Constructable] 
		public ElfPirateCaptain() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			((BaseCreature)this).midrace = 4;
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Hue = 1316;
			Race = Race.Elf;

            if (this.Female = Utility.RandomBool())
            {
                Body = 606;
                Name = NameList.RandomName("elf_female");
            }
            else
            {
                Body = 605;
                Name = NameList.RandomName("elf_male");
            }

            Title = "the drow pirate captain";

			AddItem( new Scimitar() );

			PirateChest MyChest = new PirateChest(8,null);
			MyChest.ContainerOwner = "Treasure Chest of " + Name + " the drow";
			MyChest.Hue = Utility.RandomList( 0x961, 0x962, 0x963, 0x964, 0x965, 0x966, 0x967, 0x968, 0x969, 0x96A, 0x96B, 0x96C );
			PackItem( MyChest );

            Utility.AssignRandomHair( this );
            HairHue = 1150;

            AddItem( new ElvenBoots( 0x6F8 ) );
            Item armor = new LeatherChest(); armor.Hue = 0x6F8; AddItem( armor );
			AddItem( new FancyShirt( 0 ) );	
			AddItem( new TricorneHat ( 0 ) );

            switch ( Utility.Random( 2 ))
			{
				case 0: AddItem( new LongPants ( 0xBB4 ) ); break;
				case 1: AddItem( new ShortPants ( 0xBB4 ) ); break;
			}

			AddItem(new Scimitar());

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );

			SetHits( 300, 400 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.Anatomy, 125.0 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 83.5, 92.5 );
			SetSkill( SkillName.Swords, 125.0 );
			SetSkill( SkillName.Tactics, 125.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 30;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return Utility.RandomMinMax( 1, 6 ); } }

		public ElfPirateCaptain( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	} 
}