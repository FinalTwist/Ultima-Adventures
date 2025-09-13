using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "an orcish corpse" )] 
	public class OrkRogue : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ if ( YellHue == 2 ){ return 50; } else { return 100; } } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ if ( YellHue == 2 ){ return 50; } else { return 0; } } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override int BreathEffectSound{ get{ return 0x238; } }
		public override int BreathEffectItemID{ get{ if ( YellHue == 1 ){ return 0x27AC; } else if ( YellHue == 2 ){ return 0x406C; } else { return 0xF51; } } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 3 ); }
		public override double BreathDamageScalar{ get{ return 0.4; } }
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Orc; } }

		[Constructable]
		public OrkRogue() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			((BaseCreature)this).midrace = 5;
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "ork_female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "ork_male" );
			}

			int myBonus = 10;
			int myMinDmg = 4;
			int myMaxDmg = 12;
			int myResist = 5;

			switch ( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: myBonus = 10; myMinDmg = 4; myMaxDmg = 12; myResist = 5;  break;
				case 1: myBonus = 20; myMinDmg = 5; myMaxDmg = 13; myResist = 10; break;
				case 2: myBonus = 30; myMinDmg = 6; myMaxDmg = 14; myResist = 15; break;
				case 3: myBonus = 40; myMinDmg = 7; myMaxDmg = 15; myResist = 20; break;
				case 4: myBonus = 50; myMinDmg = 8; myMaxDmg = 16; myResist = 25; break;
				case 5: myBonus = 60; myMinDmg = 9; myMaxDmg = 17; myResist = 30; break;
			}

			SetStr( ( Utility.RandomMinMax( 86, 100 ) + myBonus ) );
			SetDex( ( Utility.RandomMinMax( 81, 95 ) + myBonus ) );
			SetInt( ( Utility.RandomMinMax( 61, 75 ) + myBonus ) );

			SetHits( RawStr );

			SetDamage( myMinDmg, myMaxDmg );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, ( 10 + myResist ) );
			SetResistance( ResistanceType.Fire, myResist );
			SetResistance( ResistanceType.Cold, myResist );
			SetResistance( ResistanceType.Poison, myResist );
			SetResistance( ResistanceType.Energy, myResist );

			SetSkill( SkillName.DetectHidden, ( 20.0 + myBonus ) );
			SetSkill( SkillName.Anatomy, ( 50.0 + myBonus ) );
			SetSkill( SkillName.MagicResist, ( 20.0 + myBonus ) );
			SetSkill( SkillName.Macing, ( 50.0 + myBonus ) );
			SetSkill( SkillName.Fencing, ( 50.0 + myBonus ) );
			SetSkill( SkillName.Wrestling, ( 50.0 + myBonus ) );
			SetSkill( SkillName.Swords, ( 50.0 + myBonus ) );
			SetSkill( SkillName.Tactics, ( 50.0 + myBonus ) );
			SetSkill( SkillName.Snooping, ( 50.0 + myBonus ) );
			SetSkill( SkillName.Stealing, ( 50.0 + myBonus ) );
			SetSkill( SkillName.Hiding, ( 50.0 + myBonus ) );
			SetSkill( SkillName.Stealth, ( 50.0 + myBonus ) );

			Fame = myBonus * 50;
			Karma = myBonus * -50;

			VirtualArmor = myResist;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

		public override void OnAfterSpawn()
		{
			Server.Misc.IntelligentAction.DressUpRogues( this, "ork ", true, 0, "" );
			base.OnAfterSpawn();
			Server.Misc.MorphingTime.CheckMorph( this );
		}

		public override bool OnBeforeDeath()
		{
			Server.Misc.IntelligentAction.MakeAssassinNote( this );
			Server.Misc.MorphingTime.TurnToSomethingOnDeath( this );
			return base.OnBeforeDeath();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.CryOut( this );
			Server.Misc.IntelligentAction.HideStealMove( attacker, this );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.PoisonVictim( defender, this );
		}

		public override void RevealingAction()
		{
			Spells.Sixth.InvisibilitySpell.RemoveTimer( this );
			this.CantWalk = false;
			base.RevealingAction();
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m is PlayerMobile && this.Hits > 30 && Utility.RandomMinMax( 1, 5 ) == 1 && this.Hidden == true )
			{
				RevealingAction();
			}

			base.OnMovement( m, oldLocation );
		}

		public OrkRogue( Serial serial ) : base( serial )
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