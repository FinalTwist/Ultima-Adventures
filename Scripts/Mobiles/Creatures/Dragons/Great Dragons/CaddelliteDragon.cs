using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;

namespace Server.Mobiles
{
	[CorpseName( "Vordinax's corpse" )]
	public class CaddelliteDragon : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3818; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override bool CanChew { get{return true;}}
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 1 ); }

		[Constructable]
		public CaddelliteDragon () : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = "Vordinax";
			Title = "the caddellite dragon";
			Hue = 0x5B6;
			Body = 106;
			BaseSoundID = 362;
			EmoteHue = 123;

			SetStr( 986, 1185 );
			SetDex( 177, 255 );
			SetInt( 700, 900 );

			SetHits( 1500, 1900 );

			SetDamage( 22, 30 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 95.5, 100.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 90;
			
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "caddellite scales" );
   			c.DropItem(scale);

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is StaffPartCaddellite )
			{
				targets.Add( item );
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];
				item.Delete();
			}

   			c.DropItem( new StaffPartCaddellite() );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", "Caddellite", this.Name + " " + this.Title, c, 10, 0 );
				}
			}
		}

		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }

		public override bool OnBeforeDeath()
		{
			if (Utility.RandomDouble() > 0.66)
			{
				VordinaxChest MyChest = new VordinaxChest();
				MyChest.MoveToWorld( Location, Map );

				QuestGlow MyGlow = new QuestGlow();
				MyGlow.MoveToWorld( Location, Map );
			}

			return base.OnBeforeDeath();
		}

        public override int GetAngerSound()
        {
            return 0x63E;
        }

        public override int GetDeathSound()
        {
            return 0x63F;
        }

        public override int GetHurtSound()
        {
            return 0x640;
        }

        public override int GetIdleSound()
        {
            return 0x641;
        }

		public CaddelliteDragon( Serial serial ) : base( serial )
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
			
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}
	}
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Items
{
	public class VordinaxChest : Item
	{
		[Constructable]
		public VordinaxChest() : base( 0xE40 )
		{
			Name = "Vordinax's Vault";
			Movable = false;
			Hue = 0x5B6;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		}

		public VordinaxChest( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				from.SendSound( 0x3D );
				from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You have pulled Vordinax's Vault toward you.", from.NetState);

				LootChest MyChest = new LootChest( 6 );
				MyChest.Name = "Vordinax's Vault";
				MyChest.Hue = 0x5B6;

				if ( from is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( from.Luck, from ) )
					{
						Item arty = ArtifactBuilder.CreateArtifact( "random" );
						MyChest.DropItem( arty );
					}
					if ( GetPlayerInfo.LuckyKiller( from.Luck, from ) && !Server.Items.CharacterDatabase.GetSpecialsKilled( from, "CaddelliteDragon" ) )
					{
						Server.Items.CharacterDatabase.SetSpecialsKilled( from, "CaddelliteDragon", true );
						ManualOfItems lexicon = new ManualOfItems();
							lexicon.Hue = 0x5B6;
							lexicon.Name = "Tome of Vordinax Relics";
							lexicon.m_Charges = 1;
							lexicon.m_Skill_1 = 0;
							lexicon.m_Skill_2 = 0;
							lexicon.m_Skill_3 = 0;
							lexicon.m_Skill_4 = 0;
							lexicon.m_Skill_5 = 0;
							lexicon.m_Value_1 = 0.0;
							lexicon.m_Value_2 = 0.0;
							lexicon.m_Value_3 = 0.0;
							lexicon.m_Value_4 = 0.0;
							lexicon.m_Value_5 = 0.0;
							lexicon.m_Slayer_1 = 6;
							lexicon.m_Slayer_2 = 0;
							lexicon.m_Owner = from;
							lexicon.m_Extra = "of Vordinax the Caddellite Dragon";
							lexicon.m_FromWho = "Taken from Vordinax";
							lexicon.m_HowGiven = "Acquired by";
							lexicon.m_Points = 200;
							lexicon.m_Hue = 0x5B6;
							MyChest.DropItem( lexicon );
					}
				}

				MyChest.MoveToWorld( from.Location, from.Map );

				LoggingFunctions.LogGenericQuest( from, "defeated Vordinax the Caddellite Dragon" );
				this.Delete();
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
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
			this.Delete(); // none when the world starts 
		}

		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 
			public ItemRemovalTimer( Item item ) : base( TimeSpan.FromMinutes( 10.0 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = item; 
			} 

			protected override void OnTick() 
			{ 
				if (( i_item != null ) && ( !i_item.Deleted ))
				{
					i_item.Delete();
				}
			} 
		} 
	}
}