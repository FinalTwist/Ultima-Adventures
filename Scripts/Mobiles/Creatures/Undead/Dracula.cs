using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles 
{ 
	[CorpseName( "Dracula's corpse" )] 
	public class Dracula : BaseCreature 
	{ 
		private bool m_TrueForm;

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable] 
		public Dracula() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = "Dracula";
			Title = "the master vampire";
			Body = 311;
			BaseSoundID = 0x47D;

			SetStr( 1096, 1185 );
			SetDex( 86, 175 );
			SetInt( 686, 775 );

			SetHits( 850, 900 );

			SetDamage( 29, 65 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.EvalInt, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.Meditation, 52.5, 75.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );
			SetSkill( SkillName.Necromancy, 90.1, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 90.1, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 70;

			if ( 1 == Utility.RandomMinMax( 0, 2 ) )
			{
				LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
				MyChest.Name = "Dracula's Chest";
				MyChest.Hue = 0x485;
				PackItem( MyChest );
			}

			if ( 1 == Utility.RandomMinMax( 0, 3 ) )
			{
				PackItem( new DraculaSword() );
			}
			else
			{
				RoyalSword sword = new RoyalSword();
				sword.Hue = 0x497;
				PackItem( sword );
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.UltraRich, 2 );
			AddLoot( LootPack.HighScrolls, 2 );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return Core.AOS ? 4 : 0; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
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
			if ( m_TrueForm )
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

			Body = 191;
			Hue = 0x497;
			BaseSoundID = 372;

			SetHits( 350, 400 );

			Say("Arrrrrgh!"); 
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 5 ) == 1 && !Server.Items.CharacterDatabase.GetSpecialsKilled( killer, "Dracula" ) )
					{
						Server.Items.CharacterDatabase.SetSpecialsKilled( killer, "Dracula", true );
						ManualOfItems book = new ManualOfItems();
							book.Hue = 0x497;
							book.Name = "Tome of Dracula's Relics";
							book.m_Charges = 1;
							book.m_Skill_1 = 99;
							book.m_Skill_2 = 0;
							book.m_Skill_3 = 0;
							book.m_Skill_4 = 0;
							book.m_Skill_5 = 0;
							book.m_Value_1 = 15.0;
							book.m_Value_2 = 0.0;
							book.m_Value_3 = 0.0;
							book.m_Value_4 = 0.0;
							book.m_Value_5 = 0.0;
							book.m_Slayer_1 = 24;
							book.m_Slayer_2 = 0;
							book.m_Owner = killer;
							book.m_Extra = "of the Vampire";
							book.m_FromWho = "from Dracula";
							book.m_HowGiven = "Acquired by";
							book.m_Points = 300;
							book.m_Hue = 0x497;
							killer.AddToBackpack( book );
							killer.PrivateOverheadMessage(MessageType.Regular, 1153, false, "You found a book and put it in your pack.", killer.NetState);
					}

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Server.Misc.IntelligentAction.FameBasedEvent( this ) )
					{
						LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
						Server.Misc.ContainerFunctions.MakeTomb( MyChest, this, 0 );
						c.DropItem( MyChest );
					}
				}
			}
		}

		public Dracula( Serial serial ) : base( serial ) 
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