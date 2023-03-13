using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;

namespace Server.Mobiles 
{ 
	[CorpseName( "a vampire corpse" )] 
	public class Vampire : BaseCreature 
	{
		private bool m_TrueForm;

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable] 
		public Vampire() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = "a young vampire";
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

					int toDrain = Utility.RandomMinMax( 15, 30 );

					Hits += toDrain;
					m.Damage( toDrain, this );
				}
			}
		}

		public override void OnAfterSpawn()
		{
			Server.Misc.IntelligentAction.BeforeMyBirth( this );
			base.OnAfterSpawn();

			if ( this.EmoteHue == 123 )
			{
				// DON'T DO ANYTHING BECAUSE THEY ARE QUEST MONSTERS
			}
			else if ( this.Fame >= 4900 )
			{
				this.Title = "the grand vampire";
				Server.Misc.MorphingTime.VampireDressUp( this, 0 );
			}
			else if ( this.Fame >= 4550 )
			{
				this.Title = "the great vampire";
				Server.Misc.MorphingTime.VampireDressUp( this, 0 );
			}
			else if ( this.Fame >= 4200 )
			{
				this.Name = "an elder vampire";
				this.Body = 125;
			}
			else if ( this.Fame >= 3800 )
			{
				this.Name = "a vampire";
				this.Body = 124;
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, defender );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override bool OnBeforeDeath()
		{
			if ( m_TrueForm || Utility.RandomBool() )
			{
				Server.Misc.IntelligentAction.BeforeMyDeath( this );
				Server.Misc.IntelligentAction.DropItem( this, this.LastKiller );

				this.Body = 13;
				this.BaseSoundID = 655;
				this.Hue = 0xB85;

				return base.OnBeforeDeath();
			}
			else
			{
				Morph();
				return false;
			}
		}

		public void Morph()
		{
			if (m_TrueForm)
			return;

			m_TrueForm = true;

			Body = 317;
			BaseSoundID = 0x270;
			Hue = 0x497;

			SetHits( 90, 150 );

			Say("Squeak!"); 
		}

		public Vampire( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( m_TrueForm );	
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_TrueForm = reader.ReadBool();
					break;
				}
			}
		}
	}
}