using System;
using System.Collections;
using Server;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using System.Reflection;
using System.Text;
using Server.Misc;
using Server.Regions;

namespace Server.Items
{
	public class DungeonChest : LockableContainer
	{
		public int ContainerID;
		public int ContainerGump;
		public int ContainerHue;
		public int ContainerFlip;
		public double ContainerWeight;
		public string ContainerName;
		public int ContainerLevel;
		public int ContainerTouched;
		public int ContainerNoSpawn;
		public int ContainerLockable;

		[CommandProperty(AccessLevel.Owner)]
		public int Container_ID { get { return ContainerID; } set { ContainerID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Gump { get { return ContainerGump; } set { ContainerGump = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Hue { get { return ContainerHue; } set { ContainerHue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Flip { get { return ContainerFlip; } set { ContainerFlip = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public double Container_Weight { get { return ContainerWeight; } set { ContainerWeight = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Container_Name { get { return ContainerName; } set { ContainerName = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Level { get { return ContainerLevel; } set { ContainerLevel = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Touched { get { return ContainerTouched; } set { ContainerTouched = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_NoSpawn { get { return ContainerNoSpawn; } set { ContainerNoSpawn = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Lockable { get { return ContainerLockable; } set { ContainerLockable = value; InvalidateProperties(); } }

		[Constructable]
		public DungeonChest() : this( 99 )
		{
		}

		[Constructable]
		public DungeonChest( int level ) : base( 0xe43 )
		{
			if ( level == 99 ){ level = Utility.RandomMinMax( 1, 3 ); ContainerNoSpawn = 1; }
			if ( level > 10 ){ level = 10; }
			if ( level < 0 ){ level = 0; }
			ContainerLevel = level;
			if ( ContainerTouched != 0 ){ ContainerTouched = 0; }

			Weight = 5.0;
			Name = "treasure chest";
			Movable = false;
			LiftOverride = true;
		}

        public override void OnAfterSpawn()
        {
			SetupChest( this );
		}

		public static void SetupChest( Item box )
		{
			Region reg = Region.Find( box.Location, box.Map );

			int design = 0;
			DungeonChest chest = (DungeonChest)box;

			if ( Server.Misc.Worlds.IsIceDungeon( box.Location, box.Map ) ){ design = 1; }
			else if ( reg.IsPartOf( "the Daemon's Crag" ) || reg.IsPartOf( "Dungeon Covetous" ) ){ design = 2; } // CRATES, BARRELS, BODIES
			else if ( reg.IsPartOf( "the Ancient Sky Ship" ) && box.X > 879 && box.Y > 3613 ){ design = 14; } // METAL CRATES
			else if ( reg.IsPartOf( "the Ancient Crash Site" ) || reg.IsPartOf( "the Ancient Sky Ship" ) ){ design = 13; } // METAL CRATES AND ALIEN BODIES
			else if ( reg.IsPartOf( "the Temple of Osirus" ) ){ design = 4; } // BODIES
			else if ( reg.IsPartOf( "the Lodoria Sewers" ) || reg.IsPartOf( "the Montor Sewers" ) || reg.IsPartOf( "the Sewers" ) || reg.IsPartOf( "the Kuldara Sewers" ) ){ design = 5; } // CRATES, BARRELS
			else if ( Server.Misc.Worlds.IsFireDungeon( box.Location, box.Map ) ){ design = 6; } // METAL CHESTS
			else if ( reg.IsPartOf( "Dungeon Hythloth" ) || reg.IsPartOf( "the Ancient Pyramid" ) || reg.IsPartOf( "the Tomb of the Fallen Wizard" ) ){ design = 7; } // METAL CHESTS/BOXES, BODIES, URNS
			else if ( reg.IsPartOf( "the Mines of Morinia" ) )
			{
				if (
				( box.X >= 5859 && box.Y >= 1384 && box.X <= 5959 && box.Y <= 1477 ) || 
				( box.X >= 5589 && box.Y >= 1445 && box.X <= 5711 && box.Y <= 1473 ) || 
				( box.X >= 5611 && box.Y >= 1473 && box.X <= 5715 && box.Y <= 1530 ) || 
				( box.X >= 5652 && box.Y >= 1525 && box.X <= 5717 && box.Y <= 1552 ) 
				){ design = 0; }
				else
				{
					design = 4; // BODIES
				}
			}
			else if ( reg.IsPartOf( typeof( NecromancerRegion ) ) )
			{
				design = 9; // CASKETS
			}
			else if ( Server.Misc.Worlds.IsCrypt( box.Location, box.Map ) )
			{
				design = 8; // METAL CHESTS/BOXES, CASKETS, URNS
			}
			else if ( Server.Misc.Worlds.IsSeaDungeon( box.Location, box.Map ) || reg.IsPartOf( "Argentrock Castle" ) )
			{
				design = 11; // WOOD CHESTS, METAL CHESTS, URNS, STONE CHESTS
			}
			else if ( reg.IsPartOf( "the Hall of the Mountain King" ) )
			{
				design = 12; // METAL/WOOD CHESTS, LEATHER BAGS, BODIES
			}
			else if ( reg.IsPartOf( "the Dragon's Maw" ) || reg.IsPartOf( "Dungeon Destard" ) )
			{
				design = 15; // METAL/WOOD CHESTS, STONE CHESTS, BODIES
			}

			int nContainerLockable = ContainerFunctions.BuildContainer( ((LockableContainer)box), 0, 0, 0, design );

			int LockWatch = ContainerFunctions.LockTheContainer( chest.ContainerLevel, ((LockableContainer)box), nContainerLockable );

			// THE CONTAINER FILLS WHEN IT IS OPENED
			// THIS KEEPS THE WORLD ITEM COUNT DOWN
			// AND ALSO ALLOWS FOR CHARACTER LUCK TO
			// INFLUENCE WHAT IS INSIDE THE CONTAINER

			chest.ContainerLockable = LockWatch;
			chest.ContainerID = box.ItemID;
			chest.ContainerGump = ((Container)box).GumpID;
			chest.ContainerHue = box.Hue;
			chest.ContainerName = box.Name;
			chest.ContainerWeight = box.Weight;
			if ( box.ItemID == 0xE3F ){ chest.ContainerFlip = 0xE3E; }
			else if ( box.ItemID == 0xE3E ){ chest.ContainerFlip = 0xE3F; }
			else if ( box.ItemID == 0xE3D ){ chest.ContainerFlip = 0xE3C; }
			else if ( box.ItemID == 0xE75 ){ chest.ContainerFlip = 0x53D5; }
			else if ( box.ItemID == 0x9A8 ){ chest.ContainerFlip = 0xE80; }
			else if ( box.ItemID == 0x9AA ){ chest.ContainerFlip = 0xE7D; }
			else if ( box.ItemID == 0x2813 ){ chest.ContainerFlip = 0x2814; }
			else if ( box.ItemID == 0x2811 ){ chest.ContainerFlip = 0x2812; }
			else if ( box.ItemID == 0xe40 ){ chest.ContainerFlip = 0xe41; }
			else if ( box.ItemID == 0xe42 ){ chest.ContainerFlip = 0xe43; }
			else if ( box.ItemID == 0xE3C ){ chest.ContainerFlip = 0xE3D; }
			else if ( box.ItemID == 0x53D5 ){ chest.ContainerFlip = 0xE75; }
			else if ( box.ItemID == 0xE80 ){ chest.ContainerFlip = 0x9A8; }
			else if ( box.ItemID == 0xE7D ){ chest.ContainerFlip = 0x9AA; }
			else if ( box.ItemID == 0x2814 ){ chest.ContainerFlip = 0x2813; }
			else if ( box.ItemID == 0x2812 ){ chest.ContainerFlip = 0x2811; }
			else if ( box.ItemID == 0xe41 ){ chest.ContainerFlip = 0xe40; }
			else if ( box.ItemID == 0xe43 ){ chest.ContainerFlip = 0xe42; }
			else if ( box.ItemID == 0xE76 ){ chest.ContainerFlip = 0xE76; }
			else if ( box.ItemID == 0x281D ){ chest.ContainerFlip = 0x281E; }
			else if ( box.ItemID == 0x281F ){ chest.ContainerFlip = 0x2820; }
			else if ( box.ItemID == 0x2821 ){ chest.ContainerFlip = 0x2822; }
			else if ( box.ItemID == 0x2825 ){ chest.ContainerFlip = 0x2826; }
			else if ( box.ItemID == 0x2823 ){ chest.ContainerFlip = 0x2824; }
			else if ( box.ItemID == 0x3330 ){ chest.ContainerFlip = 0x3331; }
			else if ( box.ItemID == 0x3332 ){ chest.ContainerFlip = 0x3333; }
			else if ( box.ItemID == 0x3334 ){ chest.ContainerFlip = 0x3335; }
			else if ( box.ItemID == 0x3336 ){ chest.ContainerFlip = 0x3337; }
			else if ( box.ItemID == 0x10EA ){ chest.ContainerFlip = 0x10EB; }
			else if ( box.ItemID == 0x10EC ){ chest.ContainerFlip = 0x10ED; }
			else if ( box.ItemID == 0x281E ){ chest.ContainerFlip = 0x281D; }
			else if ( box.ItemID == 0x2820 ){ chest.ContainerFlip = 0x281F; }
			else if ( box.ItemID == 0x2822 ){ chest.ContainerFlip = 0x2821; }
			else if ( box.ItemID == 0x2826 ){ chest.ContainerFlip = 0x2825; }
			else if ( box.ItemID == 0x2824 ){ chest.ContainerFlip = 0x2823; }
			else if ( box.ItemID == 0x3331 ){ chest.ContainerFlip = 0x3330; }
			else if ( box.ItemID == 0x3333 ){ chest.ContainerFlip = 0x3332; }
			else if ( box.ItemID == 0x3335 ){ chest.ContainerFlip = 0x3334; }
			else if ( box.ItemID == 0x3337 ){ chest.ContainerFlip = 0x3336; }
			else if ( box.ItemID == 0x10EB ){ chest.ContainerFlip = 0x10EA; }
			else if ( box.ItemID == 0x10ED ){ chest.ContainerFlip = 0x10EC; }
			else if ( box.ItemID == 0x3866 ){ chest.ContainerFlip = 0x3867; }
			else if ( box.ItemID == 0x3867 ){ chest.ContainerFlip = 0x3866; }
			else { chest.ContainerFlip = box.ItemID; }
		}

		public void RemoveDungeonChest()
		{
			LoggingFunctions.LogServer( "Start - Remove Dungeon Chest Spawner" );
				
			if ( ContainerNoSpawn != 1 )
			{
				Item spawnBox = new DungeonChestSpawner( ContainerLevel, (double)(Utility.RandomMinMax( 45, 105 )) );
				spawnBox.MoveToWorld (new Point3D(this.X, this.Y, this.Z), this.Map);
			}

			LoggingFunctions.LogServer( "Done - Remove Dungeon Chest Spawner" );
			
			this.Delete();
		}

		public virtual void RemoveBox()
		{
			if( Deleted )
				return;
			if( ContainerTouched > 0 )
				Timer.DelayCall( TimeSpan.FromMinutes( 15.0 ), new TimerCallback( RemoveDungeonChest ) );
		}

		public override void Open( Mobile from )
		{
			if ( from.Blessed )
			{
				from.SendMessage( "You cannot open that while in this state." );
				return;
			}
			else if ( from.Hidden && from is PlayerMobile && from.Skills[SkillName.Hiding].Value < Utility.RandomMinMax( 1, 125 ) )
			{
				from.RevealingAction();
			}

			if ( CheckLocked( from ) )
				return;

			if ( /* from.AccessLevel == AccessLevel.Player && */ ContainerTouched != 1 && !from.Blessed )
			{
				OpenCoffin( from, this.ItemID, ContainerLevel );

				int FillMeUpLevel = ContainerLevel;

				if ( GetPlayerInfo.LuckyPlayer( from.Luck, from ) )
				{
					if (Utility.RandomDouble() < 0.10)
					{
						Item standardbook = new StandardRandomStudyBook();
						this.DropItem( standardbook );
					}
					else if (Utility.RandomDouble() < 0.02)
					{
						Item advancebook = new AdvancedRandomStudyBook();
						this.DropItem( advancebook );
					}

					FillMeUpLevel = FillMeUpLevel + Utility.RandomMinMax( 1, 2 );
				}

				string sWorld = Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y );
				ContainerFunctions.FillTheContainerByWorld( FillMeUpLevel, this, sWorld, from );
				ContainerFunctions.FillTheContainer( ContainerLevel, this, from );

				if (Utility.RandomDouble() > 0.66)
					LoggingFunctions.LogLoot( from, this.Name, "box" );
				StandardQuestFunctions.CheckTarget( from, null, this );
				ContainerTouched = 1;
				RemoveBox();
				Server.Items.CharacterDatabase.LootContainer( from, this );
			}

			base.Open( from );
		}

		public static void OpenCoffin( Mobile from, int item, int level )
		{
			if ( Utility.RandomMinMax( 1, 10 ) == 1 ) // 10% CHANCE FOR RAISING DEAD IN COFFINS
			{
				if ( item == 0x2800 || item == 0x2801 || item == 0x27E9 || item == 0x27EA || item == 0x27E0 || item == 0x280A || item == 0x2802 || item == 0x2803 )
				{
					int seance = (int)(from.Skills[SkillName.SpiritSpeak].Value);

					if ( !Server.Misc.GetPlayerInfo.LuckyPlayer( from.Luck, from ) && Utility.RandomMinMax( 1,100 ) > seance )
					{
						if ( level > 6 ){ level = 6; }
						level = level * 3;

						from.RevealingAction();

						BaseCreature spawned = new Zombie();

						switch ( Utility.Random( level ))
						{
							case 0: spawned = new Skeleton(); break;
							case 1: spawned = new Zombie(); break;
							case 2: spawned = new Ghoul(); break;
							case 3: spawned = new Shade(); break;
							case 4: spawned = new Spectre(); break;
							case 5: spawned = new Wraith(); break;
							case 6: spawned = new Phantom(); break;
							case 7: spawned = new SkeletalWizard(); break;
							case 8: spawned = new BoneKnight(); break; 
							case 9: spawned = new BoneMagi(); break;
							case 10: spawned = new SkeletalKnight(); break;
							case 11: spawned = new SkeletalMage(); break;
							case 12: spawned = new Mummy(); break;
							case 13: spawned = new Vampire(); break;
							case 14: spawned = new Ghostly(); break;
							case 15: spawned = new Lich(); break;
							case 16: spawned = new LichLord(); break;
							case 17: spawned = new RottingCorpse(); break;
						}

						string sSaying = "";
						switch ( Utility.Random( 9 ))
						{
							case 0: sSaying = "Who has disturbed me!"; break;
							case 1: sSaying = "You dare steal from my grave?"; break;
							case 2: sSaying = "Those that take from me will join me!"; break;
							case 3: sSaying = "Your soul is now mine for the taking!"; break;
							case 4: sSaying = "Who dares waken me?"; break;
							case 5: sSaying = "Your life will be extinguished!"; break;
							case 6: sSaying = "Do you have no respect for the dead?"; break;
							case 7: sSaying = "I have been waiting to feast off the living!"; break;
							case 8: sSaying = "Soon you will join my legion of the dead!"; break;
						}

						spawned.OnBeforeSpawn( from.Location, from.Map );
						spawned.Home = from.Location;
						spawned.RangeHome = 5;
						spawned.Title += " [Awakened]";
						spawned.MoveToWorld( from.Location, from.Map );
						spawned.Say(sSaying);
						spawned.ControlSlots = 666; // WIZARD ADDED FOR MONSTER CLEANUP
						spawned.Combatant = from;
					}
				}
			}
		}

		public override bool DisplaysContent{ get{ return false; } }
		public override bool DisplayWeight{ get{ return false; } }

		public DungeonChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( ContainerID );
            writer.Write( ContainerGump );
            writer.Write( ContainerHue );
            writer.Write( ContainerFlip );
            writer.Write( ContainerWeight );
            writer.Write( ContainerName );
            writer.Write( ContainerLevel );
            writer.Write( ContainerTouched );
            writer.Write( ContainerNoSpawn );
            writer.Write( ContainerLockable );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            ContainerID = reader.ReadInt();
            ContainerGump = reader.ReadInt();
            ContainerHue = reader.ReadInt();
            ContainerFlip = reader.ReadInt();
            ContainerWeight = reader.ReadDouble();
            ContainerName = reader.ReadString();
            ContainerLevel = reader.ReadInt();
            ContainerTouched = reader.ReadInt();
			ContainerNoSpawn = reader.ReadInt();
			ContainerLockable = reader.ReadInt();
				if (ContainerTouched > 0){ RemoveBox(); }
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DungeonChestSpawner : Item
	{
		public int SpawnerLevel;

		[CommandProperty(AccessLevel.Owner)]
		public int Spawner_Level { get { return SpawnerLevel; } set { SpawnerLevel = value; InvalidateProperties(); } }

		[Constructable]
		public DungeonChestSpawner() : this( Utility.RandomMinMax( 1, 3 ), 1.0 )
		{
		}

		[Constructable]
		public DungeonChestSpawner( int level, double respawn ) : base( 0x51e )
		{
			SpawnerLevel = level;
			Name = "chest spawner";
			Movable = false;
			Visible = false;
			RemoveBox( respawn );
		}

		public void RemoveDungeonChest()
		{
			LoggingFunctions.LogServer( "Start - Remove Dungeon Chest" );
				
			Item spawnBox = new DungeonChest( SpawnerLevel );
			spawnBox.MoveToWorld (new Point3D(this.X, this.Y, this.Z), this.Map);
			Server.Items.DungeonChest.SetupChest( spawnBox );

			LoggingFunctions.LogServer( "Done - Remove Dungeon Chest" );
				
			this.Delete();
		}

		public virtual void RemoveBox( double respawn )
		{
			if( Deleted )
				return;

			Timer.DelayCall( TimeSpan.FromMinutes( respawn ), new TimerCallback( RemoveDungeonChest ) );
		}

		public DungeonChestSpawner( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( SpawnerLevel );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            SpawnerLevel = reader.ReadInt();
			RemoveBox( 0.1 );
		}
	}
}