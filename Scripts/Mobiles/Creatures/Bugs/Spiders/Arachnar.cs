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
	[CorpseName( "Arachnar's corpse" )]
	public class Arachnar : BaseCreature
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
		public Arachnar() : base( AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = "Arachnar";
			Title = "the deep crawler";
			Body = 173;
			BaseSoundID = 0x388;

			SetStr( 800, 900 );
			SetDex( 650, 750 );
			SetInt( 151, 250 );

			SetHits( 1500, 1900 );

			SetDamage( 30, 35 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 60, 80 );
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

			PackItem( new SpidersSilk( 100 ) );

			Item Venom1 = new VenomSack();
				Venom1.Name = "lethal venom sack";
				AddItem( Venom1 );

			Item Venom2 = new VenomSack();
				Venom2.Name = "lethal venom sack";
				AddItem( Venom2 );

			Item Venom3 = new VenomSack();
				Venom3.Name = "lethal venom sack";
				AddItem( Venom3 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is StaffPartVenom )
			{
				targets.Add( item );
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];
				item.Delete();
			}

   			c.DropItem( new StaffPartVenom() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.FilthyRich, 2 );
		}

		public override PackInstinct PackInstinct{ get{ return PackInstinct.Arachnid; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override bool Unprovokable{ get{ return true; } }

		public override int GetAttackSound(){ return 0x601; }	// A
		public override int GetDeathSound(){ return 0x602; }	// D
		public override int GetHurtSound(){ return 0x603; }		// H

		public override bool OnBeforeDeath()
		{
			if (Utility.RandomDouble() > 0.66)
			{
				ArachnarChest MyChest = new ArachnarChest();
				MyChest.MoveToWorld( Location, Map );

				QuestGlow MyGlow = new QuestGlow();
				MyGlow.MoveToWorld( Location, Map );
			}

			return base.OnBeforeDeath();
		}

		public Arachnar( Serial serial ) : base( serial )
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
	public class ArachnarChest : Item
	{
		[Constructable]
		public ArachnarChest() : base( 0xE40 )
		{
			Name = "Arachnar's Vault";
			Movable = false;
			Hue = 0x4F6;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		}

		public ArachnarChest( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				from.SendSound( 0x3D );
				from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You have pulled Arachnar's Vault toward you.", from.NetState);

				LootChest MyChest = new LootChest( 6 );
				MyChest.Name = "Arachnar's Vault";
				MyChest.Hue = 0x4F6;

				if ( from is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( from.Luck, from ) )
					{
						Item arty = ArtifactBuilder.CreateArtifact( "random" );
						MyChest.DropItem( arty );
					}
					if ( GetPlayerInfo.LuckyKiller( from.Luck, from ) && !Server.Items.CharacterDatabase.GetSpecialsKilled( from, "Arachnar" ) )
					{
						Server.Items.CharacterDatabase.SetSpecialsKilled( from, "Arachnar", true );
						ManualOfItems lexicon = new ManualOfItems();
							lexicon.Hue = 0x4F6;
							lexicon.Name = "Tome of Arachnar Relics";
							lexicon.m_Charges = 1;
							lexicon.m_Skill_1 = 40;
							lexicon.m_Skill_2 = 0;
							lexicon.m_Skill_3 = 0;
							lexicon.m_Skill_4 = 0;
							lexicon.m_Skill_5 = 0;
							lexicon.m_Value_1 = 10.0;
							lexicon.m_Value_2 = 0.0;
							lexicon.m_Value_3 = 0.0;
							lexicon.m_Value_4 = 0.0;
							lexicon.m_Value_5 = 0.0;
							lexicon.m_Slayer_1 = 18;
							lexicon.m_Slayer_2 = 0;
							lexicon.m_Owner = from;
							lexicon.m_Extra = "of Arachnar the Deep Crawler";
							lexicon.m_FromWho = "Taken from Arachnar";
							lexicon.m_HowGiven = "Acquired by";
							lexicon.m_Points = 200;
							lexicon.m_Hue = 0x4F6;
							MyChest.DropItem( lexicon );
					}
				}

				MyChest.MoveToWorld( from.Location, from.Map );

				LoggingFunctions.LogGenericQuest( from, "defeated Arachnar the Deep Crawler" );
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