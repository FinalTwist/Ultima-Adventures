using System; 
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	public class Psionicist : BaseCreature 
	{ 
		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 50; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathDamageScalar{ get{ return 0.35; } }
		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSound{ get{ return 0x20A; } }
		public override int BreathEffectItemID{ get{ return 0x3818; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 0 ); }

		[Constructable] 
		public Psionicist() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
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
				case 0: Title = "the psychic of the bomb"; break;
				case 1: Title = "the psychic of the atom"; break;
				case 2: Title = "the irradiated psychic"; break;
				case 3: Title = "of the psychic glow"; break;
				case 4: Title = "the glowing psychic"; break;
				case 5: Title = "of the psychic light"; break;
				case 6: Title = "the enlightened psychic"; break;
			}

			SetStr( 350, 400 );
			SetDex( 177, 255 );
			SetInt( 350, 400 );

			SetHits( 302, 331 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 95.5, 100.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );

			SetSkill( SkillName.Anatomy, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Macing, 90.1, 100.0 );
			SetSkill( SkillName.Fencing, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );
			SetSkill( SkillName.Swords, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );

			Fame = 12000;
			Karma = -12000;

			VirtualArmor = 90;

			AddItem( new LightSource() );

			AddItem( new Robe( 0xBAD ) );
			AddItem( new ClothHood( 0xBAD ) );
			AddItem( new Boots() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
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

		public Psionicist( Serial serial ) : base( serial ) 
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