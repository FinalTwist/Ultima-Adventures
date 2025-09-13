using System;
using Server;
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles 
{
	[CorpseName( "a deathly corpse" )]
	public class SoulReaper : BaseCreature 
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable] 
		public SoulReaper() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = "a soul reaper";
			Hue = 0x47E;
			Body = 0x190; 
			BaseSoundID = 0x48D;

			Item hands = new WornHumanDeco();
				hands.Name = "bony fingers";
				hands.ItemID = 0x1450;
				hands.Hue = 0x47E;
				hands.Layer = Layer.Gloves;
				AddItem( hands );

			Item feet = new WornHumanDeco();
				feet.Name = "bony feet";
				feet.ItemID = 0x170D;
				feet.Hue = 0x47E;
				feet.Layer = Layer.Shoes;
				AddItem( feet );

			Robe robe = new Robe();
				robe.Name = "reaper robe";
				robe.ItemID = 0x2687;
				robe.Hue = 0x497;
				AddItem( robe );

			Scythe scythe = new Scythe();
				scythe.Name = "reaper scythe";
				scythe.Hue = 0x978;
				scythe.LootType = LootType.Blessed;
				scythe.WeaponAttributes.HitLeechHits = 100;
				scythe.WeaponAttributes.HitLeechStam = 100;
				scythe.WeaponAttributes.HitLeechMana = 100;
				AddItem( scythe );

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );

			SetHits( 150, 200 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.DetectHidden, 125.0 );
			SetSkill( SkillName.Anatomy, 125.0 );
			SetSkill( SkillName.MagicResist, 125.0 );
			SetSkill( SkillName.Swords, 125.0 );
			SetSkill( SkillName.Tactics, 125.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 10;
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 50;
			this.Hue = 0;
			return base.OnBeforeDeath();
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

		public SoulReaper( Serial serial ) : base( serial ) 
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