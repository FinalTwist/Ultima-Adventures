using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
using Server.Engines.PartySystem;

namespace Server.Mobiles
{
	[CorpseName( "an evil essence" )]
	public class Shadowlord : BaseCreature
	{
		[Constructable]
		public Shadowlord() : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Title = "the shadowlord";

			Body = 0x190;
			Hue = 0x4001;
			BaseSoundID = 0x47D;
			NameHue = 0x22;
			EmoteHue = 123;

			Robe robe = new Robe();
				robe.ItemID = 0x2687;
			  	robe.Hue = 0x541;
				robe.LootType = LootType.Blessed;
				robe.Name = "shadowlord robe";
			  	AddItem( robe );

			SetStr( 986, 1185 );
			SetDex( 177, 255 );
			SetInt( 500, 700 );

			SetHits( 1500, 1950 );

			SetDamage( 22, 40 );

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
			SetSkill( SkillName.Magery, 105.5, 120.0 );
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
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.Rich );
		}

		public override bool OnBeforeDeath()
		{
			int CanDie = 0;
			Mobile winner = this;

			foreach ( Mobile m in this.GetMobilesInRange( 30 ) )
			{
				if ( m is PlayerMobile && !m.Blessed )
				{
					if ( this.Name == "Astaroth" )
					{
						Item flame = m.Backpack.FindItemByType( typeof ( CandleOfLove ) );
						if ( flame != null )
						{
							CanDie = 1;
							winner = m;
							m.SendMessage( "The Candle of Love has vanished after dispatching the Shadowlord." );
							Server.Items.QuestSouvenir.GiveReward( m, flame.Name, flame.Hue, flame.ItemID );
						}
					}
					else if ( this.Name == "Faulinei" )
					{
						Item flame = m.Backpack.FindItemByType( typeof ( BookOfTruth ) );
						if ( flame != null )
						{
							CanDie = 1;
							winner = m;
							m.SendMessage( "The Book of Truth has vanished after dispatching the Shadowlord." );
							Server.Items.QuestSouvenir.GiveReward( m, flame.Name, flame.Hue, flame.ItemID );
						}
					}
					else
					{
						Item flame = m.Backpack.FindItemByType( typeof ( BellOfCourage ) );
						if ( flame != null )
						{
							CanDie = 1;
							winner = m;
							m.SendMessage( "The Bell of Courage has vanished after dispatching the Shadowlord." );
							Server.Items.QuestSouvenir.GiveReward( m, flame.Name, flame.Hue, flame.ItemID );
						}
					}
				}
			}

			if ( CanDie == 0 )
			{
				Say("Foolish mortal! You cannot defeat me!");
				this.Hits = this.HitsMax;
				this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
				this.PlaySound( 0x202 );
				return false;
			}
			else
			{
				this.Body = 13;
				this.Hue = 0x497;

				string Iam = this.Name + " the Shadowlord";
				Server.Misc.LoggingFunctions.LogSlayingLord( this.LastKiller, Iam );

				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				if ( this.Name == "Astaroth" && ( item is CandleOfLove ) )
				{
					targets.Add( item );
				}
				else if ( this.Name == "Faulinei" && ( item is BookOfTruth ) )
				{
					targets.Add( item );
				}
				else if ( this.Name == "Nosfentor" && ( item is BellOfCourage ) )
				{
					targets.Add( item );
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];
					item.Delete();
				}

				if ( this.Name == "Astaroth" && winner is PlayerMobile )
				{
					winner.AddToBackpack( new ShardOfHatred() );
					winner.SendMessage( "You have obtained the Shard of Hatred!" );
					LoggingFunctions.LogGenericQuest( winner, "has obtained the shard of hatred" );
				}
				else if ( this.Name == "Faulinei" && winner is PlayerMobile )
				{
					winner.AddToBackpack( new ShardOfFalsehood() );
					winner.SendMessage( "You have obtained the Shard of Falsehood!" );
					LoggingFunctions.LogGenericQuest( winner, "has obtained the shard of falsehood" );
				}
				else if ( this.Name == "Nosfentor" && winner is PlayerMobile )
				{
					winner.AddToBackpack( new ShardOfCowardice() );
					winner.SendMessage( "You have obtained the Shard of Cowardice!" );
					LoggingFunctions.LogGenericQuest( winner, "has obtained the shard of cowardice" );
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
								if ( pmi.Mobile is PlayerMobile && pmi.Mobile.InRange(this.Location, 20) && pmi.Mobile.Map == this.Map && !pmi.Mobile.Blessed && !Server.Items.CharacterDatabase.GetSpecialsKilled( pmi.Mobile, this.Name ) )
								{
									Server.Items.CharacterDatabase.SetSpecialsKilled( pmi.Mobile, this.Name, true );
									ManualOfItems book = new ManualOfItems();
										book.Hue = 0x541;
										book.Name = "Tome of Shadowlord Relics";
										book.m_Charges = 1;
										book.m_Skill_1 = 99;
										book.m_Skill_2 = 32;
										book.m_Skill_3 = 0;
										book.m_Skill_4 = 0;
										book.m_Skill_5 = 0;
										book.m_Value_1 = 10.0;
										book.m_Value_2 = 10.0;
										book.m_Value_3 = 0.0;
										book.m_Value_4 = 0.0;
										book.m_Value_5 = 0.0;
										book.m_Slayer_1 = 5;
										book.m_Slayer_2 = 0;
										book.m_Owner = pmi.Mobile;
										book.m_Extra = "of the Shadows";
										book.m_FromWho = "Spawned from the Shadowlords";
										book.m_HowGiven = "Acquired by";
										book.m_Points = 200;
										book.m_Hue = 0x541;
										pmi.Mobile.AddToBackpack( book );

									pmi.Mobile.SendMessage("An item has appeared in your backpack!");
								}
							}
						}
						else if ( !Server.Items.CharacterDatabase.GetSpecialsKilled( winner, this.Name ) )
						{
							Server.Items.CharacterDatabase.SetSpecialsKilled( winner, this.Name, true );
							ManualOfItems book = new ManualOfItems();
								book.Hue = 0x541;
								book.Name = "Tome of Shadowlord Relics";
								book.m_Charges = 1;
								book.m_Skill_1 = 99;
								book.m_Skill_2 = 32;
								book.m_Skill_3 = 0;
								book.m_Skill_4 = 0;
								book.m_Skill_5 = 0;
								book.m_Value_1 = 10.0;
								book.m_Value_2 = 10.0;
								book.m_Value_3 = 0.0;
								book.m_Value_4 = 0.0;
								book.m_Value_5 = 0.0;
								book.m_Slayer_1 = 5;
								book.m_Slayer_2 = 0;
								book.m_Owner = winner;
								book.m_Extra = "of the Shadows";
								book.m_FromWho = "Spawned from the Shadowlords";
								book.m_HowGiven = "Acquired by";
								book.m_Points = 200;
								book.m_Hue = 0x541;
								winner.AddToBackpack( book );

							winner.SendMessage("An item has appeared in your backpack!");
						}
					}
				}
			}
			return base.OnBeforeDeath();
		}

        public override void OnAfterSpawn()
        {
			base.OnAfterSpawn();

			if ( this.Home.X == 6124 && this.Home.Y == 2639 ){ this.Name = "Nosfentor"; }
			else if ( this.Home.X == 6159 && this.Home.Y == 2845 ){ this.Name = "Faulinei"; }
			else if ( this.Home.X == 6537 && this.Home.Y == 2616 ){ this.Name = "Astaroth"; }

			Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
			this.PlaySound( 0x1FE );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 6; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public Shadowlord( Serial serial ) : base( serial )
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