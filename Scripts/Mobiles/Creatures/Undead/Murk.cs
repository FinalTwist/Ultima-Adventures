using System;
using System.Collections;
using Server.Items;
using Server.Misc;
using Server.Targeting;
using Server.Regions;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly essence" )]
	public class Murk : BaseCreature
	{
		[Constructable]
		public Murk() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Murk";
			Title = "the Slayer of the Coast";
			BaseSoundID = 412;
			Hue = 0x430;
			Body = 0x190;

			FacialHairItemID = 0x204C; // BEARD
			HairItemID = 0x203C; // LONG HAIR
			FacialHairHue = 0x430;
			HairHue = 0x430;

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );

			SetHits( 200, 300 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.DetectHidden, 100.0 );
			SetSkill( SkillName.Anatomy, 125.0 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 83.5, 92.5 );
			SetSkill( SkillName.Swords, 125.0 );
			SetSkill( SkillName.Tactics, 125.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 20;

			PirateChest MyChest = new PirateChest(12,null);
			MyChest.ContainerOwner = "Murk's Lost Treasure Chest";
			PackItem( MyChest );

			AddItem( new StuddedChest() );
			AddItem( new StuddedArms() );
			AddItem( new StuddedLegs() );
			AddItem( new StuddedGorget() );
			AddItem( new StuddedGloves() );
			AddItem( new TricorneHat() );
			AddItem( new Scimitar() );
			AddItem( new Buckler() );
			AddItem( new Boots() );

			MorphingTime.BlessMyClothes( this );
			MorphingTime.ColorMyClothes( this, 0x430 );

			AddItem( new LightSource() );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						switch( Utility.RandomMinMax( 0, 9 ) )
						{
							case 0: Item loot1 = new StuddedChest(); MorphingItem.MorphMyItem( loot1, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("armors") ); c.DropItem( loot1 ); break;
							case 1: Item loot2 = new StuddedArms(); MorphingItem.MorphMyItem( loot2, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("armors") ); c.DropItem( loot2 ); break;
							case 2: Item loot3 = new StuddedLegs(); MorphingItem.MorphMyItem( loot3, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("armors") ); c.DropItem( loot3 ); break;
							case 3: Item loot4 = new StuddedGorget(); MorphingItem.MorphMyItem( loot4, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("armors") ); c.DropItem( loot4 ); break;
							case 4: Item loot5 = new StuddedGloves(); MorphingItem.MorphMyItem( loot5, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("armors") ); c.DropItem( loot5 ); break;
							case 5: Item loot6 = new MagicHat(); loot6.ItemID = 5915; loot6.Name = "hat"; MorphingItem.MorphMyItem( loot6, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("misc") ); c.DropItem( loot6 ); break;
							case 6: Item loot7 = new Scimitar(); MorphingItem.MorphMyItem( loot7, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("weapons") ); c.DropItem( loot7 ); break;
							case 7: Item loot8 = new Buckler(); MorphingItem.MorphMyItem( loot8, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("armors") ); c.DropItem( loot8 ); break;
							case 8: Item loot9 = new MagicBoots(); loot9.Name = "boots"; loot9.ItemID = 0x170b; MorphingItem.MorphMyItem( loot9, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("misc") ); c.DropItem( loot9 ); break;
							case 9: Item loot10 = Loot.RandomJewelry(); MorphingItem.MorphMyItem( loot10, "IGNORED", "Murk's Pirate", "IGNORED", MorphingTemplates.TemplateMurk("misc") ); c.DropItem( loot10 ); break;
						}
					}
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Rich );
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 13;
			return base.OnBeforeDeath();
		}

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public Murk( Serial serial ) : base( serial )
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