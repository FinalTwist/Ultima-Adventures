using System;
using Server;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Mobiles
{
	[CorpseName( "Mangar's corpse" )]
	public class Mangar : BaseCreature
	{
		private Point3D m_MoonDest;
		private int m_MoonTime;
		private InternalTimer m_MoonTimer;
		private int m_MoonHue;
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int MoonHue
		{
			get {return m_MoonHue;}
			set {m_MoonHue = value;}
		}
		
		[CommandProperty(AccessLevel.GameMaster)]
		public Point3D MoonDest
		{
			get {return m_MoonDest;}
			set {m_MoonDest = value;}
		}
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int MoonTime
		{
			get {return m_MoonTime;}
			set {m_MoonTime = value;}
		}

		[Constructable]
		public Mangar () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Mangar";
			Title = "the Dark";

			Body = 400; 
			Hue = 0x83EA;
			FacialHairItemID = 0x204C; // BEARD
			FacialHairHue = 0x455;
			HairItemID = 0x203C; // LONG HAIR
			HairHue = 0x455;
			EmoteHue = 123;
			NameHue = 0x22;

			AddItem( new Boots() );
			Item cloth1 = new Robe();
				cloth1.Hue = 0x497;
				AddItem( cloth1 );
			Item cloth2 = new WizardsHat();
				cloth2.Hue = 0x497;
				AddItem( cloth2 );

			SetStr( 476, 505 );
			SetDex( 76, 95 );
			SetInt( 400, 500 );

			SetHits( 325, 450 );

			SetDamage( 20, 35 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 70.1, 80.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 85.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 58;
		}

		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool BardImmune { get { return true; } }

		public void SpawnCreature( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			int monsters = 0;

			foreach ( Mobile m in this.GetMobilesInRange( 4 ) )
			{
				if ( m is BoneKnight || m is BoneMagi || m is Ghoul || m is Mummy || m is Shade || m is SkeletalKnight || m is SkeletalMage || m is Skeleton || m is Spectre || m is Wraith || m is Phantom || m is Zombie )
					++monsters;
			}

			if ( monsters < 6 )
			{
				PlaySound( 0x216 );

				int newmonsters = Utility.RandomMinMax( 1, 3 );

				for ( int i = 0; i < newmonsters; ++i )
				{
					BaseCreature monster;

					switch ( Utility.Random( 13 ) )
					{
						default:
						case 0: monster = new BoneKnight(); break;
						case 1: monster = new BoneMagi(); break;
						case 2: monster = new Ghoul(); break;
						case 3: monster = new Ghostly(); break;
						case 4: monster = new Mummy(); break;
						case 5: monster = new Shade(); break;
						case 6: monster = new SkeletalKnight(); break;
						case 7: monster = new SkeletalMage(); break;
						case 8: monster = new Skeleton(); break;
						case 9: monster = new Spectre(); break;
						case 10: monster = new Wraith(); break;
						case 11: monster = new Phantom(); break;
						case 12: monster = new Zombie(); break;
					}

					monster.Team = this.Team;

					bool validLocation = false;
					Point3D loc = this.Location;

					for ( int j = 0; !validLocation && j < 10; ++j )
					{
						int x = X + Utility.Random( 3 ) - 1;
						int y = Y + Utility.Random( 3 ) - 1;
						int z = map.GetAverageZ( x, y );

						if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
							loc = new Point3D( x, y, Z );
						else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
							loc = new Point3D( x, y, z );
					}

					monster.ControlSlots = 666;
					monster.MoveToWorld( loc, map );
					monster.Combatant = target;
				}
			}
		}

		public void DoSpecialAbility( Mobile target )
		{
			if ( target == null || target.Deleted ) //sanity
				return;

			if ( 0.25 >= Utility.RandomDouble() ) // 25% chance
				SpawnCreature( target );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			DoSpecialAbility( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			DoSpecialAbility( defender );
		}

		public Mangar( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
			this.PlaySound( 0x1FE );

			string bSay = "I will return!";
			switch ( Utility.Random( 5 ))		   
			{
				case 0: bSay = "You will never defeat me!"; break;
				case 1: bSay = "I will be back!"; break;
				case 2: bSay = "You can never stop me!"; break;
				case 3: bSay = "I will return!"; break;
				case 4: bSay = "You could never vanquish me!"; break;
			};

			BardMangarPack MyChest = new BardMangarPack();
			MyChest.MoveToWorld( Location, Map );

			QuestGlow MyGlow = new QuestGlow();
			MyGlow.MoveToWorld( Location, Map );

			MyChest.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( bSay ) );

			m_MoonTimer = new InternalTimer (this);
			m_MoonTimer.Start ();
			return base.OnBeforeDeath();
		}
		
		public override void OnAfterDelete()
		{
			m_MoonTimer = null;
			base.OnAfterDelete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			/*Moongate destination*/
			writer.Write((int)m_MoonDest.X);
			writer.Write((int)m_MoonDest.Y);
			writer.Write((int)m_MoonDest.Z);
			/*--------------------*/
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			/*Moongate destination*/
			int new_X = reader.ReadInt();
			int new_Y = reader.ReadInt();
			int new_Z = reader.ReadInt();
			m_MoonDest = new Point3D(2830, 1874, 95);
			/*--------------------*/			
		}
		
		private class InternalTimer : Timer
		{
			private Moongate m_MoonGate;
			
			public InternalTimer (Mangar owner) : base (TimeSpan.FromSeconds(0))
			{
				Delay = TimeSpan.FromSeconds(1800);
				Priority = TimerPriority.OneSecond;
				m_MoonGate = new Moongate ();
				m_MoonGate.MoveToWorld (new Point3D(6426, 1498, 0), Map.Felucca);
				m_MoonGate.Name = "Mangar's Gate";
				m_MoonGate.Target = new Point3D(2830, 1874, 95);
				m_MoonGate.TargetMap = Map.Trammel;
				m_MoonGate.ItemID = 0x1FD4;
			}
			
			protected override void OnTick ()
			{
				((Item)m_MoonGate).Delete ();
				Stop();
			}
		}
	}
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Server.Items
{
	public class BardMangarPack : Item
	{
		[Constructable]
		public BardMangarPack() : base( 0xE40 )
		{
			Name = "Mangar's Vault";
			Movable = false;
			Hue = 0x497;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		}

		public BardMangarPack( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleWin" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You find nothing of interest.", from.NetState);
					from.SendMessage("A gate is open nearby. You better hurry or you will remain trapped here.");
				}
				else
				{
					CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleWin", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You have pulled Mangar's Vault toward you.", from.NetState);
					from.SendMessage("A gate is open nearby. You better hurry or you will remain trapped here.");
					from.CloseGump( typeof(Server.Gumps.ClueGump) );
					from.SendGump(new Server.Gumps.ClueGump( "You notice a magical gate is open nearby. You better hurry or you will remain trapped here.", "Final Escape!" ) );

					LootChest MyChest = new LootChest( 6 );
					MyChest.Name = "Mangar's Vault";
					MyChest.Hue = 0x497;

					Item arty = ArtifactBuilder.CreateArtifact( "random" );
					MyChest.DropItem( arty );

					DDRelicPainting painting = new DDRelicPainting();
					painting.RelicGoldValue = Utility.RandomMinMax( 100, 200 ) * 50;
					painting.Name = "Painting of Mangar the Dark";
					painting.Hue = 0;
					painting.ItemID = 0x52FE;
					MyChest.DropItem( painting );

					int IamNecro = 0;
					int IamMage = 0;
					int IamBard = 0;

					if ( from.Skills[SkillName.Necromancy].Base > 0 ){ IamNecro = (int)from.Skills[SkillName.Necromancy].Base; }
					if ( from.Skills[SkillName.Magery].Base > 0 ){ IamMage = (int)from.Skills[SkillName.Magery].Base; }
					if ( from.Skills[SkillName.Musicianship].Base > 0 ){ IamBard = (int)from.Skills[SkillName.Musicianship].Base; }

					if ( !Server.Items.CharacterDatabase.GetSpecialsKilled( from, "Mangar" ) )
					{
						Server.Items.CharacterDatabase.SetSpecialsKilled( from, "Mangar", true );
						ManualOfItems lexicon = new ManualOfItems();
							lexicon.Hue = 0x497;
							lexicon.Name = "Tome of Mangar Relics";
							lexicon.m_Charges = 1;
							lexicon.m_Skill_1 = 17;
							lexicon.m_Skill_2 = 31;
							lexicon.m_Skill_3 = 32;
							lexicon.m_Skill_4 = 33;
							lexicon.m_Skill_5 = 36;
							lexicon.m_Value_1 = 5.0;
							lexicon.m_Value_2 = 5.0;
							lexicon.m_Value_3 = 5.0;
							lexicon.m_Value_4 = 5.0;
							lexicon.m_Value_5 = 5.0;
							lexicon.m_Slayer_1 = 0;
							lexicon.m_Slayer_2 = 0;
							lexicon.m_Owner = from;
							lexicon.m_Extra = "of Mangar the Dark";
							lexicon.m_FromWho = "Taken from Mangar";
							lexicon.m_HowGiven = "Acquired by";
							lexicon.m_Points = 250;
							lexicon.m_Hue = 0x497;
							MyChest.DropItem( lexicon );
					}

					if ( IamBard > IamMage && IamBard > IamNecro && IamBard > 0 )
					{
						MyChest.DropItem( new BardicFeatheredHat() );
						MySongbook newBook = new MySongbook();
						newBook.Name = "Songs of Skara Brae";
						newBook.Content = 0xFFFF;
						MyChest.DropItem( newBook );
					}
					else if ( IamMage > IamBard && IamMage > IamNecro && IamMage > 0 )
					{
						MyChest.DropItem( new MangarsRobe() );
						MySpellbook newBook = new MySpellbook();
						newBook.Hue = 0x497;
						string book = newBook.Name;
						string[] eachWord = book.Split('\'');
						int nLine = 1; foreach (string eachWords in eachWord){ if ( nLine != 1 ){ newBook.Name = "Mangar'" + eachWords; } else { nLine = 2; } }
						MyChest.DropItem( newBook );
					}
					else if ( IamNecro > IamBard && IamNecro > IamMage && IamNecro > 0 )
					{
						MyChest.DropItem( new MangarsNecroRobe() );
						MyNecromancerSpellbook newBook = new MyNecromancerSpellbook();
						newBook.Hue = 0x497;
						string book = newBook.Name;
						string[] eachWord = book.Split('\'');
						int nLine = 1; foreach (string eachWords in eachWord){ if ( nLine != 1 ){ newBook.Name = "Mangar'" + eachWords; } else { nLine = 2; } }
						MyChest.DropItem( newBook );
					}
					else if ( IamMage > 0 )
					{
						MyChest.DropItem( new MangarsRobe() );
						MySpellbook newBook = new MySpellbook();
						newBook.Hue = 0x497;
						string book = newBook.Name;
						string[] eachWord = book.Split('\'');
						int nLine = 1; foreach (string eachWords in eachWord){ if ( nLine != 1 ){ newBook.Name = "Mangar'" + eachWords; } else { nLine = 2; } }
						MyChest.DropItem( newBook );
					}
					else if ( IamNecro > 0 )
					{
						MyChest.DropItem( new MangarsNecroRobe() );
						MyNecromancerSpellbook newBook = new MyNecromancerSpellbook();
						newBook.Hue = 0x497;
						string book = newBook.Name;
						string[] eachWord = book.Split('\'');
						int nLine = 1; foreach (string eachWords in eachWord){ if ( nLine != 1 ){ newBook.Name = "Mangar'" + eachWords; } else { nLine = 2; } }
						MyChest.DropItem( newBook );
					}
					else if ( IamBard > 0 )
					{
						MyChest.DropItem( new BardicFeatheredHat() );
						MySongbook newBook = new MySongbook();
						newBook.Name = "Songs of Skara Brae";
						newBook.Content = 0xFFFF;
						MyChest.DropItem( newBook );
					}

					MyChest.MoveToWorld( from.Location, from.Map );

					LoggingFunctions.LogGenericQuest( from, "defeated Mangar and escaped Skara Brae" );
				}
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