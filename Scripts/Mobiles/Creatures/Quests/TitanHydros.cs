using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
using Server.Engines.PartySystem;

namespace Server.Mobiles
{
	[CorpseName( "a titan corpse" )]
	public class TitanHydros : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 50; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return (0xB75-1); } }
		public override int BreathEffectItemID{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 43 ); }

		[Constructable]
		public TitanHydros () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; 
			Name = "Hydros";
			Title = "the titan of water";
			Body = 436;
			BaseSoundID = 0x4B0;
			NameHue = 0x22;

			SetStr( 986, 1185 );
			SetDex( 177, 255 );
			SetInt( 580, 750 );

			SetHits( 2592, 3711 );

			SetDamage( 32, 59 );

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
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Potions, 3 );
		}

		public override bool OnBeforeDeath()
		{
			int CanDie = 0;
			int CanKillIt = 0;
			Mobile winner = this;
			int RewardColor = 0xB46;

			foreach ( Mobile m in this.GetMobilesInRange( 30 ) )
			{
				if ( m is PlayerMobile && m.Map == this.Map && !m.Blessed )
				{
					Item obelisk = m.Backpack.FindItemByType( typeof ( ObeliskTip ) );
					if ( obelisk != null )
					{
						ObeliskTip tip = (ObeliskTip)obelisk;
						if ( tip.ObeliskOwner == m && tip.HasWater > 0 && tip.WonWater < 1 )
						{
							CanDie = 1;
							winner = m;
							tip.WonWater = 1;
							m.SendMessage( "You absord the Titan's power into the Tear of the Seas." );
							m.PlaySound( 0x65A );
							m.FixedParticles( 0x375A, 1, 30, 9966, 33, 2, EffectLayer.Head );
						}
					}
				}
			}
			if ( CanDie == 0 )
			{
				foreach ( Mobile m in this.GetMobilesInRange( 30 ) )
				{
					if ( m is PlayerMobile && m.Map == this.Map && !m.Blessed && m.StatCap > 275 ) // TITANS OF ETHER CAN KILL IT
					{
						CanKillIt = 1;
					}
					if ( m is PlayerMobile && m.Map == this.Map && !m.Blessed ) // ANYONE WITH THE BLACKROCK CAN KILL IT
					{
						Item obelisk = m.Backpack.FindItemByType( typeof ( ObeliskTip ) );
						if ( obelisk != null )
						{
							ObeliskTip tip = (ObeliskTip)obelisk;
							if ( tip.ObeliskOwner == m && tip.HasWater > 0 && tip.WonWater > 0 )
							{
								CanKillIt = 1;
							}
						}
					}
				}
			}

			if ( CanDie == 0 && CanKillIt == 0 )
			{
				Say("Fool! I will take your corpse to the depths!");
				this.Hits = this.HitsMax;
				this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
				this.PlaySound( 0x202 );
				return false;
			}
			else if ( CanKillIt == 0 )
			{
				string Iam = "the Titan of Water";
				Server.Misc.LoggingFunctions.LogSlayingLord( this.LastKiller, Iam );
				if ( winner is PlayerMobile )
				{
					LoggingFunctions.LogGenericQuest( winner, "has obtained the power of the water titan" );
				}

				if ( winner != null )
				{
					if ( winner is BaseCreature )
						winner = ((BaseCreature)winner).GetMaster();

					if ( winner is PlayerMobile && !winner.Blessed )
					{
						Party p = Engines.PartySystem.Party.Get( winner );
						if ( p != null )
						{
							foreach ( PartyMemberInfo pmi in p.Members )
							{
								if ( pmi.Mobile is PlayerMobile && pmi.Mobile.InRange(this.Location, 20) && pmi.Mobile.Map == this.Map && !pmi.Mobile.Blessed && pmi.Mobile.StatCap < 275 && !Server.Items.CharacterDatabase.GetSpecialsKilled( pmi.Mobile, "TitanHydros" ) )
								{
									Server.Items.CharacterDatabase.SetSpecialsKilled( pmi.Mobile, "TitanHydros", true );
									ManualOfItems book = new ManualOfItems();
										book.Hue = RewardColor;
										book.ItemID = 0x1AA3;
										book.Name = "Tome of Water Titan Relics";
										book.m_Charges = 1;
										book.m_Skill_1 = 0;
										book.m_Skill_2 = 0;
										book.m_Skill_3 = 0;
										book.m_Skill_4 = 0;
										book.m_Skill_5 = 0;
										book.m_Value_1 = 0.0;
										book.m_Value_2 = 0.0;
										book.m_Value_3 = 0.0;
										book.m_Value_4 = 0.0;
										book.m_Value_5 = 0.0;
										book.m_Slayer_1 = 5;
										book.m_Slayer_2 = 0;
										book.m_Owner = pmi.Mobile;
										book.m_Extra = "of the Sea";
										book.m_FromWho = "Taken from Hydros";
										book.m_HowGiven = "Acquired by";
										book.m_Points = 300;
										book.m_Hue = RewardColor;
										pmi.Mobile.AddToBackpack( book );

									pmi.Mobile.SendMessage("An item has appeared in your backpack!");
								}
							}
						}
						else if ( winner.StatCap < 275 && !Server.Items.CharacterDatabase.GetSpecialsKilled( winner, "TitanHydros" ) )
						{
							Server.Items.CharacterDatabase.SetSpecialsKilled( winner, "TitanHydros", true );
							ManualOfItems book = new ManualOfItems();
								book.Hue = RewardColor;
								book.ItemID = 0x1AA3;
								book.Name = "Tome of Water Titan Relics";
								book.m_Charges = 1;
								book.m_Skill_1 = 0;
								book.m_Skill_2 = 0;
								book.m_Skill_3 = 0;
								book.m_Skill_4 = 0;
								book.m_Skill_5 = 0;
								book.m_Value_1 = 0.0;
								book.m_Value_2 = 0.0;
								book.m_Value_3 = 0.0;
								book.m_Value_4 = 0.0;
								book.m_Value_5 = 0.0;
								book.m_Slayer_1 = 5;
								book.m_Slayer_2 = 0;
								book.m_Owner = winner;
								book.m_Extra = "of the Sea";
								book.m_FromWho = "Taken from Hydros";
								book.m_HowGiven = "Acquired by";
								book.m_Points = 300;
								book.m_Hue = RewardColor;
								winner.AddToBackpack( book );

							winner.SendMessage("An item has appeared in your backpack!");
						}
					}
				}
			}
			return base.OnBeforeDeath();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 4 ) == 1 )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "deep water" ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "deep water", 0x555, 0 );
				}
			}
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 6; } }
		public override bool BardImmune { get { return true; } }

		public TitanHydros( Serial serial ) : base( serial )
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
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; 
		}
	}
}