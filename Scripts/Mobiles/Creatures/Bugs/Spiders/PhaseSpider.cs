using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a spider corpse" )]
	public class PhaseSpider : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 50; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override int BreathEffectSound{ get{ return 0x62A; } }
		public override int BreathEffectItemID{ get{ return 0x10D4; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 6 ); }

		[Constructable]
		public PhaseSpider () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a phase spider";
			Body = 180;
			BaseSoundID = 1170;

			SetStr( 286, 310 );
			SetDex( 126, 145 );
			SetInt( 196, 220 );

			SetHits( 266, 286 );

			SetDamage( 8, 20 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 80 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 65.1, 80.0 );
			SetSkill( SkillName.Magery, 65.1, 80.0 );
			SetSkill( SkillName.Meditation, 65.1, 80.0 );
			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 75.0 );
			SetSkill( SkillName.DetectHidden, 125.0 );
			SetSkill( SkillName.Hiding, 125.0 );
			SetSkill( SkillName.Stealth, 125.0 );

			Fame = 7000;
			Karma = -7000;

			VirtualArmor = 42;

			PackItem( new SpidersSilk( 10 ) );

			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.HideFromOthers( this );
		}

		public override void RevealingAction()
		{
			Spells.Sixth.InvisibilitySpell.RemoveTimer( this );
			this.CantWalk = false;
			base.RevealingAction();
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m is PlayerMobile && this.Hits > 30 && Utility.RandomMinMax( 1, 4 ) == 1 && this.Hidden == true )
			{
				RevealingAction();
			}

			base.OnMovement( m, oldLocation );
		}

		public PhaseSpider( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 1170;
		}
	}
}