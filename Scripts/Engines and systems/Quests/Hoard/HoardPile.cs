using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class HoardPiles : Item
	{
		private int m_Uses;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses { get{ return m_Uses; } set{ m_Uses = value; InvalidateProperties(); } }

		public string HoardName;
		
		[CommandProperty(AccessLevel.Owner)]
		public string Hoard_Name { get { return HoardName; } set { HoardName = value; InvalidateProperties(); } }

		private DateTime m_DecayTime;
		private Timer m_DecayTimer;

		public virtual TimeSpan DecayDelay{ get{ return TimeSpan.FromMinutes( 10.0 ); } } // HOW LONG UNTIL THE PILE DECAYS IN MINUTES

		[Constructable]
		public HoardPiles() : base( 0x0879 )
		{
			Movable = false;
			Name = "treasure hoard";
			Light = LightType.Circle225;
			ItemID = Utility.RandomList( 0x0879, 0x08AD );
		}

		public virtual void RefreshDecay( bool setDecayTime )
		{
			if( Deleted )
				return;
			if( m_DecayTimer != null )
				m_DecayTimer.Stop();
			if( setDecayTime )
				m_DecayTime = DateTime.UtcNow + DecayDelay;
			m_DecayTimer = Timer.DelayCall( DecayDelay, new TimerCallback( Delete ) );
		}

		public HoardPiles( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
			writer.WriteDeltaTime( m_DecayTime );
			writer.WriteEncodedInt( (int) m_Uses );
            writer.Write( HoardName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();
					RefreshDecay( false );
					break;
				}
			}
			m_Uses = reader.ReadEncodedInt();
			HoardName = reader.ReadString();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Blessed )
			{
				from.SendMessage( "You cannot look through that while in this state." );
			}
			else if ( !from.InRange( GetWorldLocation(), 3 ) )
			{
				from.SendMessage( "You will have to get closer to it!" );
			}
			else if ( m_Uses < 5 )
			{
				m_Uses++;
				if ( Server.Misc.GetPlayerInfo.LuckyPlayer( from.Luck, from ) && Utility.RandomBool() )
				{
					m_Uses--;
				}

				from.PlaySound( 0x2E5 );
				from.SendMessage( "You pull something from the treasure hoard!" );

				Item item = null;

				switch ( Utility.Random( 17 ) )
				{
					case 0:
						item = Loot.RandomArty();
						break;
					case 1:
						item = DungeonLoot.RandomSlayer();
						break;
					case 2:
						item = Loot.RandomSArty();
						break;
					case 3:
						if ( Server.Misc.GetPlayerInfo.EvilPlay( from ) == true && Utility.RandomMinMax( 0, 10 ) == 10 )
						{
							item = DungeonLoot.RandomEvil();
						}
						else
						{
							item = Loot.RandomRelic();
							if ( item is DDRelicWeapon && Server.Misc.GetPlayerInfo.OrientalPlay( from ) == true ){ Server.Items.DDRelicWeapon.MakeOriental( item ); }
							else if ( item is DDRelicStatue && Server.Misc.GetPlayerInfo.OrientalPlay( from ) == true ){ Server.Items.DDRelicStatue.MakeOriental( item ); }
							else if ( item is DDRelicBanner && item.ItemID != 0x2886 && item.ItemID != 0x2887 && Server.Misc.GetPlayerInfo.OrientalPlay( from ) == true ){ Server.Items.DDRelicBanner.MakeOriental( item ); }
						}
						break;
					case 4:
						item = DungeonLoot.RandomRare();
						if ( item.Stackable == true ){ item.Amount = Utility.RandomMinMax( 5, 40 ); }
						break;
					case 5:
						item = DungeonLoot.RandomLoreBooks();
						break;
					case 6:
						if ( Utility.Random( 4 ) != 1 ) { item = Loot.RandomScroll( 0, 7, SpellbookType.Regular ); } 
						else { item = Loot.RandomScroll( 0, 17, SpellbookType.Necromancer ); }
						break;
					case 7:
						int luckMod = from.Luck; if ( luckMod > 2000 ){ luckMod = 2000; }

						if (	(Region.Find( from.Location, from.Map )).IsPartOf( "the Ancient Crash Site" ) || 
								(Region.Find( from.Location, from.Map )).IsPartOf( "the Ancient Sky Ship" ) )
						{
							item = new DDXormite( ( luckMod + Utility.RandomMinMax( 333, 666 ) ) );
						}
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Mines of Morinia" )
						{
							item = new Crystals( ( luckMod + Utility.RandomMinMax( 200, 400 ) ) );
						}
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" )
						{
							item = new DDJewels( ( luckMod + Utility.RandomMinMax( 500, 1000 ) ) );
						}
						else
						{
							item = new Gold( ( luckMod + Utility.RandomMinMax( 1000, 2000 ) ) );
						}
						break;
					case 8: case 9: case 10: case 11:
						item = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing( Server.LootPackEntry.IsInTokuno( from ) );
						ContainerFunctions.LootMutate( from, Server.LootPack.GetRegularLuckChance( from ), item, from.Backpack, Utility.RandomMinMax( 5, 10 ) );
						break;
					case 12:
						item = Loot.RandomInstrument();

						int attributeCount;
						int min, max;
						Server.Misc.ContainerFunctions.GetRandomAOSStats( out attributeCount, out min, out max, 6 );

						BaseInstrument instr = (BaseInstrument)item;

						int cHue = 0;
						int cUse = 0;

						switch ( instr.Resource )
						{
							case CraftResource.AshTree: cHue = MaterialInfo.GetMaterialColor( "ash", "", 0 ); cUse = 20; break;
							case CraftResource.CherryTree: cHue = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); cUse = 40; break;
							case CraftResource.EbonyTree: cHue = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); cUse = 60; break;
							case CraftResource.GoldenOakTree: cHue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); cUse = 80; break;
							case CraftResource.HickoryTree: cHue = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); cUse = 100; break;
							case CraftResource.MahoganyTree: cHue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); cUse = 120; break;
							case CraftResource.DriftwoodTree: cHue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); cUse = 120; break;
							case CraftResource.OakTree: cHue = MaterialInfo.GetMaterialColor( "oak", "", 0 ); cUse = 140; break;
							case CraftResource.PineTree: cHue = MaterialInfo.GetMaterialColor( "pine", "", 0 ); cUse = 160; break;
							case CraftResource.RosewoodTree: cHue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); cUse = 180; break;
							case CraftResource.WalnutTree: cHue = MaterialInfo.GetMaterialColor( "walnute", "", 0 ); cUse = 200; break;
						}

						if ( !( Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) ) )
						{
							if ( cHue > 0 ){ item.Hue = cHue; }
							else if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ item.Hue = RandomThings.GetRandomColor(0); }
							Server.Misc.MorphingTime.MakeOrientalItem( item, from );
							item.Name = LootPackEntry.MagicItemName( item, from, Region.Find( from.Location, from.Map ) );
						}
						else 
						{
							string newName = "odd alien";
							switch( Utility.RandomMinMax( 0, 6 ) )
							{
								case 0: newName = "odd"; break;
								case 1: newName = "unusual"; break;
								case 2: newName = "bizarre"; break;
								case 3: newName = "curious"; break;
								case 4: newName = "peculiar"; break;
								case 5: newName = "strange"; break;
								case 6: newName = "weird"; break;
							}

							switch( Utility.RandomMinMax( 1, 4 ) )
							{
								case 1: item = new Pipes();		item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " pipes";		break;
								case 2: item = new Pipes();		item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " pan flute";	break;
								case 3: item = new Fiddle();	item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " violin";		break;
								case 4: item = new Fiddle();	item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " fiddle";		break;
							}

							BaseInstrument lute = (BaseInstrument)item;
								lute.Resource = CraftResource.None;

							item.Hue = Server.Misc.RandomThings.GetRandomColor(0);
						}

						instr.UsesRemaining = instr.UsesRemaining + cUse;

						BaseRunicTool.ApplyAttributesTo( (BaseInstrument)item, attributeCount, min, max );

						SlayerName slayer = BaseRunicTool.GetRandomSlayer();

						instr.Quality = InstrumentQuality.Regular;
						if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Quality = InstrumentQuality.Exceptional; }

						if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Slayer = slayer; }

						break;
					case 13:
						item = Loot.RandomGem();
						break;
					case 14:
						item = Loot.RandomPotion();
						break;
					case 15:
						item = Loot.RandomWand(); Server.Misc.MaterialInfo.ColorMetal( item, 0 ); string wandOwner = ""; if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ wandOwner = Server.LootPackEntry.MagicWandOwner() + " "; } item.Name = wandOwner + item.Name;
						break;
					case 16:
						m_Uses = 6; // STOP GIVING LOOT WHEN THEY GET A CONTAINER
						int chestLuck = Server.Misc.GetPlayerInfo.LuckyPlayerArtifacts( from.Luck, from );
						if ( chestLuck < 3 ){ chestLuck = 3; }
						if ( chestLuck > 8 ){ chestLuck = 8; }
						item = new LootChest( Utility.RandomMinMax( 3, chestLuck ) );
						item.ItemID = Utility.RandomList( 0x9AB, 0xE40, 0xE41, 0xE7C );
						item.Hue = Utility.RandomList( 0x961, 0x962, 0x963, 0x964, 0x965, 0x966, 0x967, 0x968, 0x969, 0x96A, 0x96B, 0x96C, 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E, 0x4AA );

						Region reg = Region.Find( from.Location, from.Map );

						string box = "hoard chest";
						switch ( Utility.RandomMinMax( 0, 7 ) )
						{
							case 0:	box = "hoard chest";		break;
							case 1:	box = "treasure chest";		break;
							case 2:	box = "secret chest";		break;
							case 3:	box = "fabled chest";		break;
							case 4: box = "legendary chest";	break;
							case 5:	box = "mythical chest";		break;
							case 6:	box = "lost chest";			break;
							case 7:	box = "stolen chest";		break;
						}

						if ( Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) )
						{
							Server.Misc.ContainerFunctions.MakeSpaceCrate( ((LockableContainer)item) );
							box = item.Name;
						}

						switch ( Utility.RandomMinMax( 0, 1 ) )
						{
							case 0:	item.Name = box + " from " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location );	break;
							case 1:	item.Name = box + " of " + HoardName;	break;
						}
						int xTraCash = Utility.RandomMinMax( 5000, 8000 );

						int zone = 0;
						if ( Worlds.IsOnSpaceship( from.Location, from.Map ) ){ zone = 1; }
						else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" ){ zone = 2; }

						ContainerFunctions.AddGoldToContainer( xTraCash, (LootChest)item, zone, from );
						int artychance = GetPlayerInfo.LuckyPlayerArtifacts( from.Luck, from ) + 10;
						if ( Utility.RandomMinMax( 0, 100 ) < artychance ){ Item artys = Loot.RandomArty(); ((LootChest)item).DropItem( artys ); //BaseContainer.DropItemFix( artys, from, ((LootChest)item).ItemID, ((LootChest)item).GumpID ); 
						}
						break;
				}

				if ( item != null )
				{ 
					if ( Worlds.IsOnSpaceship( from.Location, from.Map ) ){ Server.Misc.MorphingTime.MakeSpaceAceItem( item, from ); }

					if ( item is Container ){ item.MoveToWorld( from.Location, from.Map ); }
					else { from.AddToBackpack( item ); }
				}
				else
				{
					if ( Worlds.IsOnSpaceship( from.Location, from.Map ) )
					{
						item = new DDXormite( ( from.Luck + Utility.RandomMinMax( 333, 666 ) ) );
					}
					else if ( Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Underworld" )
					{
						item = new DDJewels( ( from.Luck + Utility.RandomMinMax( 500, 1000 ) ) );
					}
					else
					{
						item = new Gold( ( from.Luck + Utility.RandomMinMax( 1000, 2000 ) ) );
					}
					if ( item != null ){ from.AddToBackpack( item ); }
				}
			}
			else
			{
				from.SendMessage( "There is nothing else worth taking from this pile!" );
				this.Delete();
			}
		}
	}
}