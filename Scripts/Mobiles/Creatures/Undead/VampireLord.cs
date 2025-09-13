using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;

namespace Server.Mobiles 
{ 
	[CorpseName( "a vampire corpse" )] 
	public class VampireLord : BaseCreature 
	{
		private bool m_TrueForm;

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable] 
		public VampireLord() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = NameList.RandomName( "vampire" );
			Title = "the elder vampire";
			Body = 125;

			BaseSoundID = 0x47D;

			SetStr( 111, 145 );
			SetDex( 191, 215 );
			SetInt( 146, 170 );

			SetHits( 300, 750 );

			SetDamage( 10, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 80.2, 100.0 );
			SetSkill( SkillName.Magery, 95.1, 100.0 );
			SetSkill( SkillName.Meditation, 27.5, 50.0 );
			SetSkill( SkillName.MagicResist, 77.5, 100.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.3, 80.0 );
			SetSkill( SkillName.Necromancy, 90.1, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 90.1, 100.0 );

			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; // Force full speed

			Fame = 8500;
			Karma = -8500;

			VirtualArmor = 36;
			PackReg( 23 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return Core.AOS ? 2 : 0; } }
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
			else if ( this.Fame >= 13650 )
			{
				this.Title = "the ancient vampire";
				Server.Misc.MorphingTime.VampireDressUp( this, 0 );
			}
			else if ( this.Fame >= 12600 )
			{
				this.Title = "the grand vampire";
				Server.Misc.MorphingTime.VampireDressUp( this, 0 );
			}
			else if ( this.Fame >= 11550 )
			{
				this.Title = "the great vampire";
				Server.Misc.MorphingTime.VampireDressUp( this, 0 );
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

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null && this.Title == "the pharaoh" )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Server.Misc.IntelligentAction.FameBasedEvent( this ) )
					{
						LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
						Server.Misc.ContainerFunctions.MakeTomb( MyChest, this, 0 );
						c.DropItem( MyChest );
					}
				}
			}
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

			Body = 4;
			Hue = 0x497;
			BaseSoundID = 372;

			SetHits( 100, 150 );

			Say("Arrrrrgh!"); 
		}

		public VampireLord( Serial serial ) : base( serial ) 
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

			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; // Force full speed
		} 
	} 
}