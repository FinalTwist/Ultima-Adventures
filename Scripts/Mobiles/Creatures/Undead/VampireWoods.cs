using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;
using Server.Regions;
using Server.Mobiles;

namespace Server.Mobiles 
{ 
	[CorpseName( "a vampire corpse" )] 
	public class VampireWoods : BaseCreature 
	{ 
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable] 
		public VampireWoods() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = "a vampire";
			Body = 124;

			BaseSoundID = 0x47D;

			SetStr( 91, 125 );
			SetDex( 101, 135 );
			SetInt( 106, 140 );

			SetHits( 90, 150 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.EvalInt, 75.1, 100.0 );
			SetSkill( SkillName.Magery, 75.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 26;
			PackReg( 6 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return Core.AOS ? 1 : 0; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int Hides{ get{ return 3; } }
		public override HideType HideType{ get{ return HideType.Necrotic; } }

		public void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				if ( !Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
				{
					DoHarmful( m );

					m.PlaySound( 0x133 );
					m.FixedParticles( 0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist );

					m.SendMessage( "You feel the blood draining from you!" );

					int toDrain = Utility.RandomMinMax( 5, 8 );

					Hits += toDrain;
					m.Damage( toDrain, this );
				}
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 13;
			this.BaseSoundID = 655;
			this.Hue = 0xB85;

			return base.OnBeforeDeath();
		}

		public VampireWoods( Serial serial ) : base( serial )
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