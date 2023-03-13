using System;
using Server;
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles 
{
	public class BombWorshipper : BaseCreature 
	{
		public override int BreathPhysicalDamage{ get{ return 30; } }
		public override int BreathFireDamage{ get{ return 30; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 40; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathDamageScalar{ get{ return 0.35; } }
		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSound{ get{ return 0x54A; } }
		public override int BreathEffectItemID{ get{ return 0x28EF; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 0 ); }

		[Constructable] 
		public BombWorshipper() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Hue = 0xB79;

			HairItemID = 0;
			FacialHairItemID = 0;

			if ( Female = Utility.RandomBool() ) 
			{
				Body = 0x191; 
				Name = NameList.RandomName( "dark_elf_prefix_male" ) + NameList.RandomName( "dark_elf_suffix_female" );
			} 
			else 
			{ 
				Body = 0x190; 
				Name = NameList.RandomName( "dark_elf_prefix_female" ) + NameList.RandomName( "dark_elf_suffix_male" );
			}

			switch ( Utility.RandomMinMax( 0, 6 ) )
			{
				case 0: Title = "of the bomb"; break;
				case 1: Title = "of the atom"; break;
				case 2: Title = "the irradiated"; break;
				case 3: Title = "of the glow"; break;
				case 4: Title = "the glowing"; break;
				case 5: Title = "of the light"; break;
				case 6: Title = "the enlightened"; break;
			}

			SetStr( 336, 385 );
			SetDex( 96, 115 );
			SetInt( 281, 305 );

			SetHits( 302, 331 );
			SetMana( 0 );

			SetDamage( 7, 23 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.Anatomy, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Macing, 90.1, 100.0 );
			SetSkill( SkillName.Fencing, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );
			SetSkill( SkillName.Swords, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 50;

			AddItem( new LightSource() );

			AddItem( new Robe( 0xBAD ) );
			AddItem( new ClothHood( 0xBAD ) );
			AddItem( new Boots() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
		}

		public override void OnAfterSpawn()
		{
			if ( Utility.RandomBool() )
			{
				IntelligentAction.GiveBasicWepShld( this );
				if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { MorphingTime.MakeSpaceAceItem( this.FindItemOnLayer( Layer.OneHanded ), this ); }
				if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { MorphingTime.MakeSpaceAceItem( this.FindItemOnLayer( Layer.TwoHanded ), this ); }
			}
			base.OnAfterSpawn();
		}

		public override void OnDeath( Container c )
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 )
			{
				SpaceJunk junk = new SpaceJunkA(); junk.Delete();

				switch ( Utility.RandomMinMax( 0, 12 ) )
				{
					case 0: junk = new SpaceJunkA(); break;
					case 1: junk = new SpaceJunkB(); break;
					case 2: junk = new SpaceJunkC(); break;
					case 3: junk = new SpaceJunkD(); break;
					case 4: junk = new SpaceJunkE(); break;
					case 5: junk = new SpaceJunkF(); break;
					case 6: junk = new SpaceJunkG(); break;
					case 7: junk = new SpaceJunkH(); break;
					case 8: junk = new SpaceJunkI(); break;
					case 9: junk = new SpaceJunkJ(); break;
					case 10: junk = new SpaceJunkK(); break;
					case 11: junk = new SpaceJunkL(); break;
					case 12: junk = new SpaceJunkM(); break;
				}

				junk.ItemID = Utility.RandomList( 0x3F65, 0x3F8F );
				junk.Name = Server.Items.SpaceJunk.RandomCondition() + " bowcaster";
				c.DropItem( junk );
			}

			base.OnDeath( c );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.CryOut( this );
		}

		public BombWorshipper( Serial serial ) : base( serial ) 
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