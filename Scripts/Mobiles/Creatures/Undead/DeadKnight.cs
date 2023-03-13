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
	[CorpseName( "a corpse" )] 
	public class DeadKnight : BaseCreature 
	{
		[Constructable] 
		public DeadKnight() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			switch ( Utility.Random( 3 ) )
			{
				case 0: Hue = 1150; 	BaseSoundID = 412;		AddItem( new LightSource() );	break;	// GHOST
				case 1: Hue = 0x430;	BaseSoundID = 0x4FB;									break;	// SKELETON
				case 2: Hue = 0xB97;	BaseSoundID = 471;										break;	// ZOMBIE
			}

			if ( this.Female = Utility.RandomBool() ) 
			{
				this.Body = 0x191; 
				this.Name = NameList.RandomName( "female" ); 
			} 
			else 
			{ 
				this.Body = 0x190; 
				this.Name = NameList.RandomName( "male" );
			}

			SetStr( Utility.RandomMinMax( 150, 170 ) );
			SetDex( Utility.RandomMinMax( 70, 90 ) );
			SetInt( Utility.RandomMinMax( 40, 60 ) );

			SetHits( RawStr );

			SetDamage( 8, 18 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 0 );
			SetResistance( ResistanceType.Poison, 10 );
			SetResistance( ResistanceType.Energy, 0 );

			SetSkill( SkillName.DetectHidden, 20.0 );
			SetSkill( SkillName.Anatomy, 50.0 );
			SetSkill( SkillName.MagicResist, 20.0 );
			SetSkill( SkillName.Macing, 50.0 );
			SetSkill( SkillName.Fencing, 50.0 );
			SetSkill( SkillName.Wrestling, 50.0 );
			SetSkill( SkillName.Swords, 50.0 );
			SetSkill( SkillName.Tactics, 50.0 );

			Fame = 100;
			Karma = -100;

			VirtualArmor = 0;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool BleedImmune{ get{ return true; } }

		public override void OnAfterSpawn()
		{
			Server.Misc.IntelligentAction.ChooseFighter( this, "undead " );

			if ( this.Hue == 1150 ){ MorphingTime.BlessMyClothes( this ); MorphingTime.ColorMyClothes( this, 1150 ); }
			else if ( this.Hue == 0xB97 ){ MorphingTime.ColorMyClothes( this, 0xB9A ); }
			else
			{
				Item helm = new WornHumanDeco();
					helm.Name = "skull";
					helm.ItemID = 0x1451;
					helm.Hue = this.Hue;
					helm.Layer = Layer.Helm;
					AddItem( helm );
			}

			base.OnAfterSpawn();
		}

		public override bool OnBeforeDeath()
		{
			if ( this.Hue == 1150 ){ this.Body = 13; }
			else if ( this.Hue == 0xB97 ){ this.Body = 155; }
			else { this.Body = 50; }

			return base.OnBeforeDeath();
		}

		public DeadKnight( Serial serial ) : base( serial ) 
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