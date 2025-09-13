using System; 
using Server;
using System.Collections; 
using System.Collections.Generic;
using Server.Targeting;
using Server.Misc; 
using Server.Items; 
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Mobiles; 
using Server.Targeting;

namespace Server.Mobiles 
{ 
	public class OrphanGuard : BaseBlue
	{ 
		private bool m_Bandaging;
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
		public DateTime m_NextTalk;
		private bool leetguard = false;

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			#region Tass23/Raist
			if ( !( m is PlayerMobile ) )
				return;
			
			if ((m is PlayerMobile) && (m.AccessLevel == AccessLevel.Player))
			{
				if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) )
				{
				}
			}
			if (m is OrphanGuard && this.Combatant != null)
            {
                if (m.Combatant == null)
                {
                    m.Combatant = Combatant;
					if (Utility.RandomBool())
						m.Say("You Scoundrel!");  // what does the guard getting called say
					if (Utility.RandomBool())
						Say("How could you do such a thing to innocent children!!"); // what does the guard doing the calling say
                }
            }
			#endregion
		}

		[Constructable] 
		public OrphanGuard() : base( AIType.AI_Melee, FightMode.Closest, 25, 1, 0.4, 0.3 ) 
		{
			Job = JobFragment.guard;
			Karma = Utility.RandomMinMax( 13, -45 );
			AIFullSpeedActive = true; // Force full speed
			AIFullSpeedPassive = false;

			//bool leetguard = false;
			if (Utility.RandomDouble() < 0.20)
			{
				leetguard = true;
			}
				
			if (leetguard)
			{
				SetStr(500, 800);
				SetDex(300, 500);
				SetInt(50, 100);
				ActiveSpeed = 0.1;
				PassiveSpeed = 0;

				SetHits(300, 600);

				SetDamage(55, 75);

				SetDamageType(ResistanceType.Physical, 100);

				SetResistance(ResistanceType.Physical, 50, 70);
				SetResistance(ResistanceType.Fire, 50, 70);
				SetResistance(ResistanceType.Cold, 50, 70);
				SetResistance(ResistanceType.Poison, 50, 70);
				SetResistance(ResistanceType.Energy, 50, 70);

				SetSkill(SkillName.Swords, 100.0, 120.0);
				SetSkill(SkillName.Fencing, 100.0, 120.0);
				SetSkill(SkillName.Macing, 100.0, 120.0);
				SetSkill(SkillName.Tactics, 100.0, 120.0);				
				SetSkill(SkillName.Tactics, 100.0, 120.0);
				SetSkill(SkillName.MagicResist, 100.0, 120.0);
				SetSkill(SkillName.Tactics, 100.0, 120.0);
				SetSkill(SkillName.Parry, 100.0, 120.0);
				SetSkill(SkillName.Anatomy, 100.0, 120.0);
				SetSkill(SkillName.Healing, 100.0, 120.0);
				SetSkill(SkillName.Magery, 100.0, 120.0);
				SetSkill(SkillName.EvalInt, 100.0, 120.0);
				SetSkill(SkillName.DetectHidden, 90.0, 120.0);
				Fame = 10000;
				Karma = 5000;
				VirtualArmor = 50;
			}
			else
			{
				SetStr(200, 400);
				SetDex(100, 200);
				SetInt(50, 100);
				ActiveSpeed = 0.2;
				PassiveSpeed = 0.1;

				SetHits(200, 300);

				SetDamage(45, 60);

				SetDamageType(ResistanceType.Physical, 100);

				SetResistance(ResistanceType.Physical, 50, 70);
				SetResistance(ResistanceType.Fire, 40, 50);
				SetResistance(ResistanceType.Cold, 40, 50);
				SetResistance(ResistanceType.Poison, 40, 50);
				SetResistance(ResistanceType.Energy, 40, 50);

				SetSkill(SkillName.Swords, 89.0, 100.0);
				SetSkill(SkillName.Fencing, 89.0, 100.0);
				SetSkill(SkillName.Macing, 89.0, 100.0);
				SetSkill(SkillName.Tactics, 89.0, 100.0);
				SetSkill(SkillName.MagicResist, 89.0, 100.0);
				SetSkill(SkillName.Tactics, 89.0, 100.0);
				SetSkill(SkillName.Parry, 89.0, 100.0);
				SetSkill(SkillName.Anatomy, 85.0, 100.0);
				SetSkill(SkillName.Healing, 85.0, 100.0);
				SetSkill(SkillName.Magery, 85.0, 100.0);
				SetSkill(SkillName.EvalInt, 85.0, 100.0);
				SetSkill(SkillName.DetectHidden, 50.0, 100.0);
				Fame = 5000;
				Karma = 1000;
				VirtualArmor = 50;
			}
			
			Utility.AssignRandomHair( this );

			for (int i = 0; i < 10; i++)
			{
				PackItem( new GreaterCurePotion() );
				PackItem( new GreaterHealPotion() );
				PackItem( new TotalRefreshPotion() );
			}

			PackItem(new Bandage(Utility.RandomMinMax(10, 40)));
			PackItem(new Bola(Utility.RandomMinMax(1, 3)));
			
		}

		public override bool IsEnemy( Mobile m )
		{
			if (m is PlayerMobile && InRange( m, 8 ) && InLOS( m ) )
			{
				foreach ( Mobile mm in m.GetMobilesInRange( 10 ) )
				{
					if ( mm is Orphan && ((Orphan)mm).captured && ((BaseCreature)mm).ControlMaster == m )
					{
						this.Hidden = false;
						Combatant = m;
						Say("You Scum!!  How could you dare harm innocent children!");
						return true;
					}
				}
			}

			else if (Combatant == m)
				return true;

			return false;
		}
		
		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();

			Region reg = Region.Find( this.Location, this.Map );

			string World = Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y );

			int clothColor = 0;
			int shieldType = 0;
			int helmType = 0;
			int cloakColor = 0;

			Item weapon = new VikingSword(); weapon.Delete();

			if ( this.Map == Map.Trammel )
			{
				Title = "[Slave Hunter]";
				if ( Female = Utility.RandomBool() ) 
				{ 
					Body = 401; //606 for elf
					Name = NameList.RandomName( "female" );	
				}
				else 
				{ 
					Body = 400; 	//605 for elf		
					Name = NameList.RandomName( "male" ); 
				}				
			}

			if (leetguard)
			{
				clothColor = 0x2F;		shieldType = 0x1B76;	helmType = 0x1412;		cloakColor = 0x3AE;		weapon = new Longsword();
			}
			else 
			{
				clothColor = 0x598;		shieldType = 0;			helmType = 0x140E;		cloakColor = 0x83F;		weapon = new Spear();
			}

			weapon.Movable = true;
			((BaseWeapon)weapon).MaxHitPoints = 250;
			((BaseWeapon)weapon).HitPoints = 100;
			((BaseWeapon)weapon).MinDamage = 10;
			((BaseWeapon)weapon).MaxDamage = 25;
			AddItem( weapon );

			AddItem( new PlateChest() );
			if ( World == "the Serpent Island" ){ AddItem( new RingmailArms() ); } else { AddItem( new PlateArms() ); } // FOR GARGOYLES
			AddItem( new PlateLegs() );
			AddItem( new PlateGorget() );
			AddItem( new PlateGloves() );
			AddItem( new Boots( ) );

			if ( helmType > 0 )
			{
				PlateHelm helm = new PlateHelm();
					helm.ItemID = helmType;
					helm.Name = "helm";
					AddItem( helm );
			}
			if ( shieldType > 0 )
			{
				ChaosShield shield = new ChaosShield();
					shield.ItemID = shieldType;
					shield.Name = "shield";
					AddItem( shield );
			}

			MorphingTime.ColorMyClothes( this, clothColor );

			if ( cloakColor > 0 )
			{
				Cloak cloak = new Cloak();
					cloak.Hue = cloakColor;
					AddItem( cloak );
			}

			Server.Misc.MorphingTime.CheckMorph( this );
		}		
		
		public override void OnGaveMeleeAttack( Mobile defender )
		{
			switch ( Utility.Random( 15 ))		   
			{
				case 0: Say("Die villain!"); break;
				case 1: Say("I will bring you justice!"); break;
				case 2: Say("So, " + defender.Name + "? Your evil ends here!"); break;
				case 3: Say("We have been told to watch for " + defender.Name + "!"); break;
				case 4: Say("I found a slaver, " + defender.Name + " is here!"); break;
				case 5: Say("We have ways of dealing with the likes of " + defender.Name + "!"); break;
				case 6: Say("Give up! We do not fear " + defender.Name + "!"); break;
				case 7: Say("So, " + defender.Name + "? I sentence you to death!"); break;
			};
			base.OnGaveMeleeAttack( defender );
		}		
		

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
        public override bool CanHeal { get { return true; } }

		public override void OnThink()
		{
			base.OnThink();

			if (Combatant == null)
				this.Hidden = true;
			
			if (this.Combatant != null && this.Combatant.Mounted)
			{
				Server.Ability.TossBola(this);
			}
			
		}
		
		public OrphanGuard( Serial serial ) : base( serial ) 
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
			AIFullSpeedActive = true; // Force full speed
			AIFullSpeedPassive = false;
		} 		
		
		
	}	
}   
