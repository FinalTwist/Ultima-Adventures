using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{
	public class ElfMonks : BaseCreature 
	{
		[Constructable] 
		public ElfMonks() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Hue = 1316;
			Race = Race.Elf;

			if ( this.Female = Utility.RandomBool() ) 
			{
				Body = 606; 
				Name = NameList.RandomName( "dark_elf_prefix_female" ) + NameList.RandomName( "dark_elf_suffix_female" );
			} 
			else 
			{ 
				Body = 605; 
				Name = NameList.RandomName( "dark_elf_prefix_male" ) + NameList.RandomName( "dark_elf_suffix_male" );
			}

			SetStr( Utility.RandomMinMax( 100, 120 ) );
			SetDex( Utility.RandomMinMax( 90, 100 ) );
			SetInt( Utility.RandomMinMax( 40, 60 ) );

			SetHits( RawStr );

			SetDamage( 8, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 0 );
			SetResistance( ResistanceType.Poison, 0 );
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
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Average );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			Server.Misc.IntelligentAction.LeapToAttacker( this, from );
			base.OnDamage( amount, from, willKill );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.PunchStun( defender );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.CryOut( this );
		}

		public override void OnAfterSpawn()
		{
			Server.Misc.IntelligentAction.ChooseMonk( this, "drow " );
			base.OnAfterSpawn();
		}

		public ElfMonks( Serial serial ) : base( serial ) 
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