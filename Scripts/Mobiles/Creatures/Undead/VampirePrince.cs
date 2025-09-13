using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;

namespace Server.Mobiles 
{ 
	[CorpseName( "a vampire corpse" )] 
	public class VampirePrince : BaseCreature 
	{
		private bool m_TrueForm;

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable] 
		public VampirePrince() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = NameList.RandomName( "vampire" );
			Title = "the vampire baron";
			Body = Utility.RandomList( 125, 126 );

			BaseSoundID = 0x47D;

			SetStr( 211, 245 );
			SetDex( 191, 215 );
			SetInt( 246, 270 );

			SetHits( 180, 200 );

			SetDamage( 10, 15 );

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

			Fame = 10500;
			Karma = -10500;

			VirtualArmor = 36;
			PackReg( Utility.RandomMinMax( 10, 30 ) );
			PackReg( Utility.RandomMinMax( 10, 30 ) );
			PackReg( Utility.RandomMinMax( 10, 30 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.HighScrolls );
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
				Server.Misc.MorphingTime.VampireDressUp( this, 0 );
				if ( this.Body == 605 ){ this.Title = "the vampire prince"; } else { this.Title = "the vampire princess"; }
			}
			else if ( this.Fame >= 12600 )
			{
				Server.Misc.MorphingTime.VampireDressUp( this, 0 );
				if ( this.Body == 605 ){ this.Title = "the vampire duke"; } else { this.Title = "the vampire duchess"; }
			}
			else if ( this.Fame >= 11550 )
			{
				Server.Misc.MorphingTime.VampireDressUp( this, 0 );
				if ( this.Body == 605 ){ this.Title = "the vampire earl"; } else { this.Title = "the vampire countess"; }
			}
			else
			{
				Server.Misc.MorphingTime.VampireDressUp( this, 0 );
				if ( this.Body == 605 ){ this.Title = "the vampire baron"; } else { this.Title = "the vampire baroness"; }
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

			Body = 38;
			BaseSoundID = 357;

			SetHits( 180, 200 );

			Say("Arrrrrgh!"); 
		}

		public VampirePrince( Serial serial ) : base( serial ) 
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