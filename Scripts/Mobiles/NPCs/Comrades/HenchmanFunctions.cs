using System;
using Server;
using System.Collections;
using Server.Misc;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using Server.Targeting;
using System.Collections.Generic;
using Server.Items;

namespace Server.Misc
{
    class HenchmanFunctions
    {

		public static bool IsInRestRegion(Mobile from)
		{
			bool house = false;
			if ( from.Region is HouseRegion )
			    if (((HouseRegion)from.Region).House.IsOwner(from))
					house = true;
			
			if ( from.Region.GetLogoutDelay( from ) == TimeSpan.Zero || house ) 
				return true;

			return false;
		}

		public static void ForceSlow( Mobile m )
		{
			if ( m is HenchmanMonster || m is HenchmanArcher || m is HenchmanFighter || m is HenchmanWizard )
			{
				BaseCreature bc = (BaseCreature)m;
				IMount mount = m.Mount;
				if ( mount != null )
				{
					mount.Rider = null;
					BaseCreature horse = (BaseCreature)mount;
					horse.Delete();
				}
				bc.ActiveSpeed = 0.2;
				bc.PassiveSpeed = 0.4;
			}
		}


        public static void ReportStatus( Mobile henchman )
        {
            string time = ((int)(henchman.Fame/5)).ToString();
            string bandages = henchman.Hunger.ToString();
            if (henchman is HenchmanMonster)
                henchman.Say("Have " + bandages + " bandages. Will stay for " + time + " minutes.");
            else
                henchman.Say("I have " + bandages + " bandages. I will travel with thee for " + time + " minutes.");
        }

		public static void DismountHenchman( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				PlayerMobile master = (PlayerMobile)from;
				List<Mobile> pets = master.AllFollowers;

				if ( pets.Count > 0 )
				{
					for ( int i = 0; i < pets.Count; ++i )
					{
						Mobile m = (Mobile)pets[i];

						if ( m is HenchmanMonster || m is HenchmanArcher || m is HenchmanFighter || m is HenchmanWizard || MyServerSettings.FastFriends( m ) )
						{
							BaseCreature bc = (BaseCreature)m;
							IMount mount = m.Mount;
							if ( mount != null )
							{
								mount.Rider = null;
								BaseCreature horse = (BaseCreature)mount;
								horse.Delete();
							}
							bc.ActiveSpeed = 0.2;
							bc.PassiveSpeed = 0.4;
						}
					}
				}
			}
		}

		public static void MountHenchman( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				PlayerMobile master = (PlayerMobile)from;
				List<Mobile> pets = master.AllFollowers;

				if ( pets.Count > 0 )
				{
					for ( int i = 0; i < pets.Count; ++i )
					{
						Mobile m = (Mobile)pets[i];

						if ( m is HenchmanMonster || m is HenchmanArcher || m is HenchmanFighter || m is HenchmanWizard || MyServerSettings.FastFriends( m ) )
						{
							BaseCreature bc = (BaseCreature)m;
							IMount mount = m.Mount;
							if ( mount == null && ( m is HenchmanArcher || m is HenchmanFighter || m is HenchmanWizard ) )
							{
								new HenchHorse().Rider = m;
							}
							bc.ActiveSpeed = 0.1;
							bc.PassiveSpeed = 0.2;
						}
					}
				}
			}
		}

		public static bool OnMoving( Mobile m, Point3D oldLocation, Mobile from, DateTime m_NextMorale )
		{
			bool GoAway = false;
			bool monster = false;

			if ( DateTime.UtcNow >= m_NextMorale && from.InRange( m, 20 ) )
			{

				if ( HenchmanFunctions.IsInRestRegion( from ) == true ){} else
				{
					from.Fame = from.Fame - 5;
					if ( from.Fame < 0 )
					{
						from.Fame = 0;
						ArrayList targets = new ArrayList();
						foreach ( Item item in World.Items.Values )
						if ( item is HenchmanItem )
						{
							HenchmanItem henchItem = (HenchmanItem)item;
							if ( henchItem.HenchSerial == from.Serial )
							{
								targets.Add( item );
							}
						}
						for ( int i = 0; i < targets.Count; ++i )
						{
							Item item = ( Item )targets[ i ];
							HenchmanItem henchThing = (HenchmanItem)item;
							henchThing.HenchTimer = from.Fame;
							henchThing.HenchBandages = from.Hunger;
							henchThing.LootType = LootType.Regular;

							if ( item is HenchmanArcherItem ){ henchThing.Name = "archer henchman"; }
							else if ( item is HenchmanFighterItem ){ henchThing.Name = "fighter henchman"; }
							else if ( item is HenchmanMonsterItem ){ henchThing.Name = "creature henchman"; monster = true; }
							else { henchThing.Name = "wizard henchman"; }

							henchThing.Visible = true;
						}

						if ( monster )
						{
							switch ( Utility.Random( 2 ) )		   
							{
								case 0: from.Say("There is not enough reward in this for me."); break;
								case 1: from.Say("If you hear stories of riches, come and get me."); break;
							}
						}
						else
						{
							switch ( Utility.Random( 5 ) )		   
							{
								case 0: from.Say("Sorry, but there is not enough reward on this journey for me."); break;
								case 1: from.Say("I think I will head back to town and get a drink."); break;
								case 2: from.Say("The risk is not worth the little reward I am getting."); break;
								case 3: from.Say("Come and find me later when you have a quest for riches."); break;
								case 4: from.Say("If you hear of any rumors of gold, come and get me."); break;
							}
						}
						GoAway = true;
					}
					else if ( from.Fame < 26 )
					{
						if ( monster )
						{
							switch ( Utility.Random( 2 ) )		   
							{
								case 0: from.Say("I will leave soon if we don't find treasure."); break;
								case 1: from.Say("You said there were riches, but I don't see it."); break;
							}
						}
						else
						{
							switch ( Utility.Random( 5 ))		   
							{
								case 0: from.Say("I will have to leave soon if we don't find some treasure."); break;
								case 1: from.Say("I feel this quest is a dead end and may leave soon."); break;
								case 2: from.Say("This lack of treasure is not what I came along for."); break;
								case 3: from.Say("You promised riches, but I fear there is none."); break;
								case 4: from.Say("What are we looking for? It is obviously not treasure."); break;
							}
						}
					}
				}

				((BaseCreature)from).Loyalty = 100;
			}

			return GoAway;
		}


		public static void OnGaveAttack( Mobile from )
		{
			if ( !(from is HenchmanMonster) )
			{
				switch ( Utility.Random( 28 ))		   
				{
					case 0: from.Say("Time to die!"); break;
					case 1: from.Say("I will send you to hell!"); break;
					case 2: from.Say("Your life ends here!"); break;
					case 3: from.Say("You are no match for me!"); break;
					case 4: from.Say("Prepare to die fool!"); break;
					case 5: from.Say("Taste my wrath and my blade!"); break;
					case 6: from.Say("Yield to me!"); break;
					case 7: from.Say("I sentence you to death!"); break;
					case 8: from.PlaySound( from.Female ? 793 : 1065 ); from.Say( "*gasp*" ); break;
					case 9: from.PlaySound( from.Female ? 0x338 : 0x44A ); from.Say( "*growls*" ); break;
					case 10: from.PlaySound( from.Female ? 797 : 1069 ); from.Say( "Hey!" ); break;
					case 11: from.PlaySound( from.Female ? 821 : 1095 ); from.Say( "*whistles*" ); break;
					case 12: from.PlaySound( from.Female ? 783 : 1054 ); from.Say( "Woohoo!" ); break;
					case 13: from.PlaySound( from.Female ? 823 : 1097 ); from.Say( "Yea!" ); break;
					case 14: from.PlaySound( from.Female ? 0x31C : 0x42C ); from.Say( "*yells*" ); break;
					case 15: new ESound( from, Utility.Random( 1, 42 ) ); break;
				}
			}
			((BaseCreature)from).Loyalty = 100;

		}

        public static void OnSpellAttack( Mobile from )
        {
			if ( !(from is HenchmanMonster) )
			{
				switch ( Utility.Random( 28 ))		   
				{
					case 0: from.Say("Time to die!"); break;
					case 1: from.Say("I will send you to hell!"); break;
					case 2: from.Say("Your life ends here!"); break;
					case 3: from.Say("You are no match for me!"); break;
					case 4: from.Say("Prepare to die fool!"); break;
					case 5: from.Say("Taste my wrath and my blade!"); break;
					case 6: from.Say("Yield to me!"); break;
					case 7: from.Say("I sentence you to death!"); break;
					case 8: from.PlaySound( from.Female ? 793 : 1065 ); from.Say( "*gasp*" ); break;
					case 9: from.PlaySound( from.Female ? 0x338 : 0x44A ); from.Say( "*growls*" ); break;
					case 10: from.PlaySound( from.Female ? 797 : 1069 ); from.Say( "Hey!" ); break;
					case 11: from.PlaySound( from.Female ? 821 : 1095 ); from.Say( "*whistles*" ); break;
					case 12: from.PlaySound( from.Female ? 783 : 1054 ); from.Say( "Woohoo!" ); break;
					case 13: from.PlaySound( from.Female ? 823 : 1097 ); from.Say( "Yea!" ); break;
					case 14: from.PlaySound( from.Female ? 0x31C : 0x42C ); from.Say( "*yells*" ); break;
					case 15: new ESound( from, Utility.Random( 1, 42 ) ); break;
				}
			}
			((BaseCreature)from).Loyalty = 100;

        }

		public static void OnGotAttack( Mobile from )
		{
			if ( !(from is HenchmanMonster) )
			{
				Server.Misc.IntelligentAction.CryOut( from );

				switch ( Utility.Random( 22 ))		   
				{
					case 0: from.Say("Is that all you got?"); break;
					case 1: from.Say("Tis but a scratch!"); break;
					case 2: from.Say("I've had worse!"); break;
					case 3: from.Say("You will have to do better than that!"); break;
					case 4: from.Say("You'll pay for that!"); break;
					case 5: from.Say("No one does that and lives!"); break;
					case 6: from.Say("It is your turn!"); break;
					case 7: from.Say("Not enough to bring me down!"); break;
					case 8: from.PlaySound( from.Female ? 793 : 1065 ); from.Say( "*gasp*" ); break;
					case 9: from.PlaySound( from.Female ? 0x338 : 0x44A ); from.Say( "*growls*" ); break;
					case 10: from.PlaySound( from.Female ? 797 : 1069 ); from.Say( "Hey!" ); break;
					case 11: from.PlaySound( from.Female ? 0x31C : 0x42C ); from.Say( "*yells*" ); break;
					case 12: new ESound( from, Utility.Random( 1, 42 ) ); break;
				}
			}
			((BaseCreature)from).Loyalty = 100;

		}

		public static void OnDead( Mobile from )
		{
			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is HenchmanItem )
			{
				HenchmanItem henchItem = (HenchmanItem)item;
				if ( henchItem.HenchSerial == from.Serial )
				{
					targets.Add( item );
				}
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];
				HenchmanItem henchThing = (HenchmanItem)item;
				henchThing.HenchDead = ( from.RawStr + from.RawInt + from.RawDex ) * 2;
				henchThing.HenchTimer = from.Fame;
				henchThing.HenchBandages = from.Hunger;
				henchThing.LootType = LootType.Regular;

				if ( item is HenchmanArcherItem ){ henchThing.Name = "dead archer henchman"; }
				else if ( item is HenchmanFighterItem ){ henchThing.Name = "dead fighter henchman"; }
				else if ( item is HenchmanMonsterItem ){ henchThing.Name = "dead creature henchman"; }
				else { henchThing.Name = "dead wizard henchman"; }

				henchThing.Visible = true;
			}

			IMount mount = from.Mount;
			if ( mount != null )
			{
				BaseCreature Horsey = (BaseCreature)mount;
				Horsey.Delete();
			}

			Mobile killer = from.FindMostRecentDamager(true);

			if ( ( killer is PlayerMobile ) && ( ((BaseCreature)from).ControlMaster != killer ) )
			{
				killer.Criminal = true;
				killer.Kills = killer.Kills + 1;
			}
		}

		public static void OnGive( Mobile from, Item dropped, Mobile henchman )
		{
			if ( dropped is Bandage )
			{
				if (henchman.Hunger + dropped.Amount > 200)
				{
				    if ( henchman is HenchmanMonster ){ henchman.Say("No, " + from.Name + ", too many bandages."); }
				    else { henchman.Say("Sorry, " + from.Name + ", but that's more bandages than I can carry at once."); }
				}
				else
				{
				    henchman.Hunger = henchman.Hunger + dropped.Amount;
				    if ( henchman is HenchmanMonster ){ henchman.Say("I could use these bandages. I have " + henchman.Hunger.ToString() + " of them now."); }
				    else { henchman.Say("Ahhh...bandages can be of great use. I have " + henchman.Hunger.ToString() + " of them now."); }
				    dropped.Delete();
				}
			}
			else if ( dropped is LesserCurePotion || dropped is CurePotion || dropped is GreaterCurePotion )
			{
				if ( henchman is HenchmanMonster ){ henchman.Say("Good, " + from.Name + "."); }
				else { henchman.Say("Thank you, " + from.Name + "."); }

				henchman.CurePoison( henchman );
				henchman.RevealingAction();
				henchman.PlaySound( 0x2D6 );
				from.AddToBackpack( new Bottle() );
				if ( henchman.Body.IsHuman && !henchman.Mounted )
					henchman.Animate( 34, 5, 1, true, false, 0 );
				dropped.Delete();
			}
			else if ( dropped is RefreshPotion || dropped is TotalRefreshPotion )
			{
				if ( henchman is HenchmanMonster ){ henchman.Say("Good, " + from.Name + "."); }
				else { henchman.Say("Thank you, " + from.Name + "."); }

				henchman.Stam = henchman.StamMax;
				henchman.RevealingAction();
				henchman.PlaySound( 0x2D6 );
				from.AddToBackpack( new Bottle() );
				if ( henchman.Body.IsHuman && !henchman.Mounted )
					henchman.Animate( 34, 5, 1, true, false, 0 );
				dropped.Delete();
			}
			else if ( dropped is LesserHealPotion || dropped is HealPotion || dropped is GreaterHealPotion )
			{
				if ( henchman is HenchmanMonster ){ henchman.Say("Good, " + from.Name + "."); }
				else { henchman.Say("Thank you, " + from.Name + "."); }

				henchman.Hits = henchman.HitsMax;
				henchman.RevealingAction();
				henchman.PlaySound( 0x2D6 );
				from.AddToBackpack( new Bottle() );
				if ( henchman.Body.IsHuman && !henchman.Mounted )
					henchman.Animate( 34, 5, 1, true, false, 0 );
				dropped.Delete();
			}
			else if ( dropped is LesserRejuvenatePotion || dropped is RejuvenatePotion || dropped is GreaterRejuvenatePotion )
			{
				if ( henchman is HenchmanMonster ){ henchman.Say("Good, " + from.Name + "."); }
				else { henchman.Say("Thank you, " + from.Name + "."); }

				henchman.Hits = henchman.HitsMax;
				henchman.Stam = henchman.StamMax;
				henchman.Mana = henchman.ManaMax;
				henchman.RevealingAction();
				henchman.PlaySound( 0x2D6 );
				from.AddToBackpack( new Bottle() );
				if ( henchman.Body.IsHuman && !henchman.Mounted )
					henchman.Animate( 34, 5, 1, true, false, 0 );
				dropped.Delete();
			}
			else if ( dropped is LesserManaPotion || dropped is ManaPotion || dropped is GreaterManaPotion )
			{
				if ( henchman is HenchmanMonster ){ henchman.Say("Good, " + from.Name + "."); }
				else { henchman.Say("Thank you, " + from.Name + "."); }

				henchman.Mana = henchman.ManaMax;
				henchman.RevealingAction();
				henchman.PlaySound( 0x2D6 );
				from.AddToBackpack( new Bottle() );
				if ( henchman.Body.IsHuman && !henchman.Mounted )
					henchman.Animate( 34, 5, 1, true, false, 0 );
				dropped.Delete();
			}
			else 
			{
				int nAmount = dropped.Amount;
				int nGold = 0;

				if ( Server.Misc.RelicItems.RelicValue( dropped, henchman ) > 0 )
				{
					nGold = Server.Misc.RelicItems.RelicValue( dropped, henchman );
				}

				else if ( dropped is DDSilver ){ double dGold = (nAmount / 5); nGold = (int)(Math.Floor(dGold)); }
				else if ( dropped is DDCopper ){ double dGold = (nAmount / 10); nGold = (int)(Math.Floor(dGold)); }
				else if ( dropped is DDJewels ){ nGold = nAmount * 2; }
				else if ( dropped is DDXormite ){ nGold = nAmount * 3; }
				else if ( dropped is Crystals ){ nGold = nAmount * 5; }
				else if ( dropped is Gold ){ nGold = nAmount; }
				else if ( dropped is DDGemstones ){ nGold = nAmount * 2; }
				else if ( dropped is DDGoldNuggets ){ nGold = nAmount; }

				else if ( dropped is MagicJewelryRing ){ nGold = Utility.Random( 50,300 ); }
				else if ( dropped is MagicJewelryNecklace ){ nGold = Utility.Random( 50,300 ); }
				else if ( dropped is MagicJewelryEarrings ){ nGold = Utility.Random( 50,300 ); }
				else if ( dropped is MagicJewelryBracelet ){ nGold = Utility.Random( 50,300 ); }
				else if ( dropped is MagicJewelryCirclet ){ nGold = Utility.Random( 50,300 ); }

				else if ( dropped is Amber ){ nGold = nAmount*12; }
				else if ( dropped is Amethyst ){ nGold = nAmount*25; }
				else if ( dropped is Citrine ){ nGold = nAmount*12; }
				else if ( dropped is Diamond ){ nGold = nAmount*50; }
				else if ( dropped is Emerald ){ nGold = nAmount*25; }
				else if ( dropped is Ruby ){ nGold = nAmount*19; }
				else if ( dropped is Sapphire ){ nGold = nAmount*25; }
				else if ( dropped is StarSapphire ){ nGold = nAmount*31; }
				else if ( dropped is Tourmaline ){ nGold = nAmount*23; }

				if ( nGold > 0 )
				{
					if ( henchman.Fame >= 1800 )
					{
						if ( henchman is HenchmanMonster ){ henchman.Say("Sorry, " + from.Name + "...but my bag is full."); }
						else { henchman.Say("Thank you, " + from.Name + "...but my treasure bag is full."); }
					}
					else
					{
						if ( henchman is HenchmanMonster ){ henchman.Say("Good, more treasure for me."); }
						else { henchman.Say("Ahhh...a cut of the treasure. This journey is worth the risk."); }

						if ( (henchman.Fame + nGold) > 1800 ){ henchman.Fame = 1800; }
						else { henchman.Fame = henchman.Fame + nGold; }
						int nTime = (int)(henchman.Fame/5);
						from.SendMessage("" + henchman.Name + " will probably adventure with you for another " + nTime.ToString() + " minutes.");
						dropped.Delete();
					}
				}
				else
				{
					if ( henchman is HenchmanMonster ){ henchman.Say("No, " + from.Name + "...but that is useless to me."); }
					else { henchman.Say("Sorry, " + from.Name + "...but I can't see much value in that."); }
				}
			}

			((BaseCreature)henchman).Loyalty = 100;
		}

		public static void NormalizeArmor( BaseCreature friend )
		{
			if ( friend.ColdResistance > 70 ){ friend.ColdResistSeed = friend.ColdResistSeed - (friend.ColdResistance - friend.ColdResistSeed); }
			if ( friend.FireResistance > 70 ){ friend.FireResistSeed = friend.FireResistSeed - (friend.FireResistance - friend.FireResistSeed); }
			if ( friend.PoisonResistance > 70 ){ friend.PoisonResistSeed = friend.PoisonResistSeed - (friend.PoisonResistance - friend.PoisonResistSeed); }
			if ( friend.EnergyResistance > 70 ){ friend.EnergyResistSeed = friend.EnergyResistSeed - (friend.EnergyResistance - friend.EnergyResistSeed); }
			if ( friend.PhysicalResistance > 70 ){ friend.PhysicalResistanceSeed = friend.PhysicalResistanceSeed - (friend.PhysicalResistance - friend.PhysicalResistanceSeed); }
		}

		public static void DressUp( HenchmanItem henchman, BaseCreature friend, Mobile from )
		{
			bool isOriental = Server.Misc.GetPlayerInfo.OrientalPlay( from );
			bool isEvil = Server.Misc.GetPlayerInfo.EvilPlay( from );

			if ( henchman is HenchmanWizardItem )
			{
				if ( isEvil == true && henchman.HenchGearColor != 0x485 && henchman.HenchGearColor != 0x497 && henchman.HenchGearColor != 0x4E9 )
				{
					henchman.HenchTitle = HenchmanFunctions.GetEvilTitle();
					friend.Title = henchman.HenchTitle;
					henchman.HenchGearColor = Utility.RandomList( 0x485, 0x497, 0x4E9 );
					henchman.Hue = henchman.HenchGearColor;
					henchman.HenchCloakColor = Utility.RandomList( 0x485, 0x497, 0x4E9 );
					if ( Utility.Random( 2 ) == 1 ){ henchman.HenchHatColor = henchman.HenchGearColor; } else { henchman.HenchHatColor = henchman.HenchCloakColor; }
				}

				if ( ( henchman.HenchBody == 401 ) && ( henchman.HenchRobe == 1 ) )
				{
					Item Armor4 = new GildedDress();
						Armor4.Hue = henchman.HenchGearColor;
						Armor4.Movable = false;
						BaseClothing Barmor4 = (BaseClothing)Armor4; Barmor4.StrRequirement = 1;
						Armor4.Name = "Robe";
						Armor4.LootType = LootType.Blessed;
							friend.AddItem( Armor4 );
				}
				else 
				{
					Item Armor4 = new Robe();
						Armor4.Hue = henchman.HenchGearColor;
						Armor4.Movable = false;
						BaseClothing Barmor4 = (BaseClothing)Armor4; Barmor4.StrRequirement = 1;
						Armor4.Name = "Robe";
						Armor4.LootType = LootType.Blessed;
							friend.AddItem( Armor4 );
				}

				Item Gear1 = new WizardsHat();
					Gear1.ItemID = henchman.HenchHelmID; if ( isOriental == true ){ Gear1.ItemID = 0x2798; }
					Gear1.Hue = henchman.HenchHatColor;
					Gear1.Movable = false;
					BaseClothing BarmorH = (BaseClothing)Gear1; BarmorH.StrRequirement = 1;
					Gear1.LootType = LootType.Blessed;
					Gear1.Name = "Hat";
						friend.AddItem( Gear1 );

				Item Gear3 = new WizardStaff();
					Gear3.ItemID = henchman.HenchWeaponID;
					Gear3.Movable = false;
					BaseWeapon Bwep = (BaseWeapon)Gear3; Bwep.StrRequirement = 1;
					Gear3.LootType = LootType.Blessed;
					Gear3.Name = "Weapon";
						friend.AddItem( Gear3 );

				if ( henchman.HenchCloak == 1 )
				{
					Item Capes = new Cloak();
						Capes.Hue = henchman.HenchCloakColor;
						Capes.Movable = false;
						BaseClothing Caper = (BaseClothing)Capes; Caper.StrRequirement = 1;
						Capes.LootType = LootType.Blessed;
							friend.AddItem( Capes );
				}

				Item Bootsy = new Boots();
					Bootsy.Hue = 0x967;
					Bootsy.Movable = false;
					BaseClothing Booty = (BaseClothing)Bootsy; Booty.StrRequirement = 1;
					Bootsy.LootType = LootType.Blessed;
						friend.AddItem( Bootsy );

				if ( henchman.HenchGloves > 1 )
				{
					Item Gloves = new LeatherGloves();
						Gloves.Hue = henchman.HenchCloakColor;
						Gloves.Movable = false;
						BaseArmor Glove = (BaseArmor)Gloves; Glove.StrRequirement = 1;
						Gloves.LootType = LootType.Blessed;
						Gloves.Name = "Gloves";
							friend.AddItem( Gloves );
				}
			}
			else if ( henchman is HenchmanFighterItem )
			{
				if ( isEvil == true && henchman.HenchGearColor != 0x485 && henchman.HenchGearColor != 0x497 && henchman.HenchGearColor != 0x4E9 )
				{
					henchman.HenchTitle = HenchmanFunctions.GetEvilTitle();
					friend.Title = henchman.HenchTitle;
					henchman.HenchGearColor = Utility.RandomList( 0x485, 0x497, 0x4E9 );
					henchman.Hue = henchman.HenchGearColor;
					henchman.HenchCloakColor = Utility.RandomList( 0x485, 0x497, 0x4E9 );
					if ( henchman.HenchHelmID > 0 ){ henchman.HenchHelmID = 0x2FBB; }
					switch ( Utility.Random( 2 ))		   
					{
						case 0: henchman.HenchShieldID = 0x2FC8; break;
						case 1: henchman.HenchShieldID = 0x1BC3; break;
					}
				}

				if ( henchman.HenchArmorType != 1 )
				{
					Item Armor0 = new PlateArms(); if ( isOriental == true ){ Armor0.ItemID = 0x2780; }
						Armor0.Hue = henchman.HenchGearColor;
						Armor0.Movable = false;
						BaseArmor Barmor0 = (BaseArmor)Armor0; Barmor0.StrRequirement = 1;
						Armor0.Name = "Armor";
						Armor0.LootType = LootType.Blessed;
							friend.AddItem( Armor0 );

					Item Armor1 = new PlateLegs(); if ( isOriental == true ){ Armor1.ItemID = 0x2788; }
						Armor1.Hue = henchman.HenchGearColor;
						Armor1.Movable = false;
						BaseArmor Barmor1 = (BaseArmor)Armor1; Barmor1.StrRequirement = 1;
						Armor1.Name = "Armor";
						Armor1.LootType = LootType.Blessed;
							friend.AddItem( Armor1 );

						if ( isOriental == true )
						{ 
							Item Bootsy = new Boots();
								Bootsy.Hue = 0x967;
								Bootsy.ItemID = 0x2796;
								Bootsy.Movable = false;
								BaseClothing Booty = (BaseClothing)Bootsy; Booty.StrRequirement = 1;
								Bootsy.LootType = LootType.Blessed;
									friend.AddItem( Bootsy );
						}

					Item Armor2 = new PlateGloves();
						Armor2.Hue = henchman.HenchGearColor;
						Armor2.Movable = false;
						BaseArmor Barmor2 = (BaseArmor)Armor2; Barmor2.StrRequirement = 1;
						Armor2.Name = "Armor";
						Armor2.LootType = LootType.Blessed;
							friend.AddItem( Armor2 );

					Item Armor3 = new PlateGorget(); if ( isOriental == true ){ Armor3.ItemID = 0x2779; }
						Armor3.Hue = henchman.HenchGearColor;
						Armor3.Movable = false;
						BaseArmor Barmor3 = (BaseArmor)Armor3; Barmor3.StrRequirement = 1;
						Armor3.Name = "Armor";
						Armor3.LootType = LootType.Blessed;
							friend.AddItem( Armor3 );

					if ( henchman.HenchBody == 401 )
					{
						Item Armor4 = new FemalePlateChest(); if ( isOriental == true ){ Armor4.ItemID = 0x277D; }
							Armor4.Hue = henchman.HenchGearColor;
							Armor4.Movable = false;
							BaseArmor Barmor4 = (BaseArmor)Armor4; Barmor4.StrRequirement = 1;
							Armor4.Name = "Armor";
							Armor4.LootType = LootType.Blessed;
								friend.AddItem( Armor4 );
					}
					else 
					{
						Item Armor4 = new PlateChest(); if ( isOriental == true ){ Armor4.ItemID = 0x277D; }
							Armor4.Hue = henchman.HenchGearColor;
							Armor4.Movable = false;
							BaseArmor Barmor4 = (BaseArmor)Armor4; Barmor4.StrRequirement = 1;
							Armor4.Name = "Armor";
							Armor4.LootType = LootType.Blessed;
								friend.AddItem( Armor4 );
					}
				}
				else
				{
					Item Armor0 = new RingmailArms(); if ( isOriental == true ){ Armor0.ItemID = 0x277F; }
						Armor0.Hue = henchman.HenchGearColor;
						Armor0.Movable = false;
						BaseArmor Barmor0 = (BaseArmor)Armor0; Barmor0.StrRequirement = 1;
						Armor0.Name = "Armor";
						Armor0.LootType = LootType.Blessed;
							friend.AddItem( Armor0 );

					Item Armor1 = new RingmailLegs(); if ( isOriental == true ){ Armor1.ItemID = 0x278D; }
						Armor1.Hue = henchman.HenchGearColor;
						Armor1.Movable = false;
						BaseArmor Barmor1 = (BaseArmor)Armor1; Barmor1.StrRequirement = 1;
						Armor1.Name = "Armor";
						Armor1.LootType = LootType.Blessed;
							friend.AddItem( Armor1 );

					Item Armor2 = new RingmailGloves();
						Armor2.Hue = henchman.HenchGearColor;
						Armor2.Movable = false;
						BaseArmor Barmor2 = (BaseArmor)Armor2; Barmor2.StrRequirement = 1;
						Armor2.Name = "Armor";
						Armor2.LootType = LootType.Blessed;
							friend.AddItem( Armor2 );

					Item Armor3 = new PlateGorget(); if ( isOriental == true ){ Armor3.ItemID = 0x2779; }
						Armor3.Hue = henchman.HenchGearColor;
						Armor3.Movable = false;
						BaseArmor Barmor3 = (BaseArmor)Armor3; Barmor3.StrRequirement = 1;
						Armor3.Name = "Armor";
						Armor3.LootType = LootType.Blessed;
							friend.AddItem( Armor3 );

					Item Armor4 = new ChainChest(); if ( isOriental == true ){ Armor4.ItemID = 0x277D; }
						Armor4.Hue = henchman.HenchGearColor;
						Armor4.Movable = false;
						BaseArmor Barmor4 = (BaseArmor)Armor4; Barmor1.StrRequirement = 1;
						Armor4.Name = "Armor";
						Armor4.LootType = LootType.Blessed;
							friend.AddItem( Armor4 );

					Item Bootsy = new Boots();
						Bootsy.Hue = 0x967;
						if ( isOriental == true ){ Bootsy.ItemID = 0x2796; }
						Bootsy.Movable = false;
						BaseClothing Booty = (BaseClothing)Bootsy; Booty.StrRequirement = 1;
						Bootsy.LootType = LootType.Blessed;
							friend.AddItem( Bootsy );
				}

				if ( henchman.HenchHelmID > 0 )
				{
					Item Gear1 = new PlateHelm();
						Gear1.ItemID = henchman.HenchHelmID; if ( isOriental == true ){ Gear1.ItemID = 0x2785; }
						Gear1.Hue = henchman.HenchGearColor;
						Gear1.Movable = false;
						BaseArmor BarmorH = (BaseArmor)Gear1; BarmorH.StrRequirement = 1;
						Gear1.LootType = LootType.Blessed;
						Gear1.Name = "Helm";
							friend.AddItem( Gear1 );
				}

				Item Gear2 = new BronzeShield();
					Gear2.ItemID = henchman.HenchShieldID;
					Gear2.Movable = false;
					BaseArmor BarmorS = (BaseArmor)Gear2; BarmorS.StrRequirement = 1;
					Gear2.LootType = LootType.Blessed;
					Gear2.Name = "Shield";
						friend.AddItem( Gear2 );

				if ( henchman.HenchWeaponType != 1 )
				{
					Item Gear3 = new Longsword();
						Gear3.ItemID = henchman.HenchWeaponID;
						Gear3.Movable = false;
						BaseWeapon Bwep = (BaseWeapon)Gear3; Bwep.StrRequirement = 1;
						Gear3.LootType = LootType.Blessed;
						Gear3.Name = "Weapon";
							friend.AddItem( Gear3 );
				}
				else
				{
					Item Gear3 = new Mace();
						Gear3.ItemID = henchman.HenchWeaponID;
						Gear3.Movable = false;
						BaseWeapon Bwep = (BaseWeapon)Gear3; Bwep.StrRequirement = 1;
						Gear3.LootType = LootType.Blessed;
						Gear3.Name = "Weapon";
							friend.AddItem( Gear3 );
				}

				if ( henchman.HenchCloak == 1 )
				{
					Item Capes = new Cloak();
						Capes.Hue = henchman.HenchCloakColor;
						Capes.Movable = false;
						BaseClothing Caper = (BaseClothing)Capes; Caper.StrRequirement = 1;
						Capes.LootType = LootType.Blessed;
							friend.AddItem( Capes );
				}
			}
			else
			{
				if ( isEvil == true && henchman.HenchGearColor != 0x485 && henchman.HenchGearColor != 0x497 && henchman.HenchGearColor != 0x4E9 )
				{
					henchman.HenchTitle = HenchmanFunctions.GetEvilTitle();
					friend.Title = henchman.HenchTitle;
					henchman.HenchGearColor = Utility.RandomList( 0x485, 0x497, 0x4E9 );
					henchman.Hue = henchman.HenchGearColor;
					henchman.HenchCloakColor = Utility.RandomList( 0x485, 0x497, 0x4E9 );
					if ( henchman.HenchHelmID > 0 && Utility.RandomMinMax( 1, 2 ) == 1 ){ henchman.HenchHelmID = 0x278E; }
				}

				if ( henchman.HenchArmorType != 1 )
				{
					Item Armor0 = new LeatherArms(); if ( isOriental == true ){ Armor0.ItemID = 0x277E; }
						Armor0.Hue = henchman.HenchGearColor;
						Armor0.Movable = false;
						BaseArmor Barmor0 = (BaseArmor)Armor0; Barmor0.StrRequirement = 1;
						Armor0.Name = "Armor";
						Armor0.LootType = LootType.Blessed;
							friend.AddItem( Armor0 );

					Item Armor1 = new LeatherLegs(); if ( isOriental == true ){ Armor1.ItemID = 0x2791; }
						Armor1.Hue = henchman.HenchGearColor;
						Armor1.Movable = false;
						BaseArmor Barmor1 = (BaseArmor)Armor1; Barmor1.StrRequirement = 1;
						Armor1.Name = "Armor";
						Armor1.LootType = LootType.Blessed;
							friend.AddItem( Armor1 );

					Item Armor2 = new LeatherGloves();
						Armor2.Hue = henchman.HenchGearColor;
						Armor2.Movable = false;
						BaseArmor Barmor2 = (BaseArmor)Armor2; Barmor2.StrRequirement = 1;
						Armor2.Name = "Armor";
						Armor2.LootType = LootType.Blessed;
							friend.AddItem( Armor2 );

					Item Armor3 = new LeatherGorget(); if ( isOriental == true ){ Armor3.ItemID = 0x277A; }
						Armor3.Hue = henchman.HenchGearColor;
						Armor3.Movable = false;
						BaseArmor Barmor3 = (BaseArmor)Armor3; Barmor3.StrRequirement = 1;
						Armor3.Name = "Armor";
						Armor3.LootType = LootType.Blessed;
							friend.AddItem( Armor3 );

					if ( henchman.HenchBody == 401 )
					{
						Item Armor4 = new FemaleLeatherChest(); if ( isOriental == true ){ Armor4.ItemID = 0x2793; }
							Armor4.Hue = henchman.HenchGearColor;
							Armor4.Movable = false;
							BaseArmor Barmor4 = (BaseArmor)Armor4; Barmor4.StrRequirement = 1;
							Armor4.Name = "Armor";
							Armor4.LootType = LootType.Blessed;
								friend.AddItem( Armor4 );
					}
					else 
					{
						Item Armor4 = new LeatherChest(); if ( isOriental == true ){ Armor4.ItemID = 0x2793; }
							Armor4.Hue = henchman.HenchGearColor;
							Armor4.Movable = false;
							BaseArmor Barmor4 = (BaseArmor)Armor4; Barmor4.StrRequirement = 1;
							Armor4.Name = "Armor";
							Armor4.LootType = LootType.Blessed;
								friend.AddItem( Armor4 );
					}
				}
				else
				{
					Item Armor0 = new StuddedArms(); if ( isOriental == true ){ Armor0.ItemID = 0x277F; }
						Armor0.Hue = henchman.HenchGearColor;
						Armor0.Movable = false;
						BaseArmor Barmor0 = (BaseArmor)Armor0; Barmor0.StrRequirement = 1;
						Armor0.Name = "Armor";
						Armor0.LootType = LootType.Blessed;
							friend.AddItem( Armor0 );

					Item Armor1 = new StuddedLegs(); if ( isOriental == true ){ Armor1.ItemID = 0x2791; }
						Armor1.Hue = henchman.HenchGearColor;
						Armor1.Movable = false;
						BaseArmor Barmor1 = (BaseArmor)Armor1; Barmor1.StrRequirement = 1;
						Armor1.Name = "Armor";
						Armor1.LootType = LootType.Blessed;
							friend.AddItem( Armor1 );

					Item Armor2 = new StuddedGloves();
						Armor2.Hue = henchman.HenchGearColor;
						Armor2.Movable = false;
						BaseArmor Barmor2 = (BaseArmor)Armor2; Barmor2.StrRequirement = 1;
						Armor2.Name = "Armor";
						Armor2.LootType = LootType.Blessed;
							friend.AddItem( Armor2 );

					Item Armor3 = new StuddedGorget(); if ( isOriental == true ){ Armor3.ItemID = 0x277A; }
						Armor3.Hue = henchman.HenchGearColor;
						Armor3.Movable = false;
						BaseArmor Barmor3 = (BaseArmor)Armor3; Barmor3.StrRequirement = 1;
						Armor3.Name = "Armor";
						Armor3.LootType = LootType.Blessed;
							friend.AddItem( Armor3 );

					if ( henchman.HenchBody == 401 )
					{
						Item Armor4 = new FemaleStuddedChest(); if ( isOriental == true ){ Armor4.ItemID = 0x2793; }
							Armor4.Hue = henchman.HenchGearColor;
							Armor4.Movable = false;
							BaseArmor Barmor4 = (BaseArmor)Armor4; Barmor4.StrRequirement = 1;
							Armor4.Name = "Armor";
							Armor4.LootType = LootType.Blessed;
								friend.AddItem( Armor4 );
					}
					else 
					{
						Item Armor4 = new StuddedChest(); if ( isOriental == true ){ Armor4.ItemID = 0x2793; }
							Armor4.Hue = henchman.HenchGearColor;
							Armor4.Movable = false;
							BaseArmor Barmor4 = (BaseArmor)Armor4; Barmor4.StrRequirement = 1;
							Armor4.Name = "Armor";
							Armor4.LootType = LootType.Blessed;
								friend.AddItem( Armor4 );
					}
				}

				Item Gear1 = new LeatherCap();
					Gear1.ItemID = henchman.HenchHelmID; if ( isOriental == true ){ Gear1.ItemID = 0x2798; }
					Gear1.Hue = henchman.HenchGearColor;
					Gear1.Movable = false;
					BaseArmor BarmorH = (BaseArmor)Gear1; BarmorH.StrRequirement = 1;
					Gear1.LootType = LootType.Blessed;
					Gear1.Name = "Helm";
						friend.AddItem( Gear1 );

				if ( henchman.HenchWeaponType != 1 )
				{
					Item Gear3 = new Bow();
						Gear3.ItemID = henchman.HenchWeaponID; if ( isOriental == true ){ Gear3.ItemID = 0x27A5; }
						Gear3.Movable = false;
						BaseWeapon Bwep = (BaseWeapon)Gear3; Bwep.StrRequirement = 1;
						Gear3.LootType = LootType.Blessed;
						Gear3.Name = "Weapon";
							friend.AddItem( Gear3 );
				}
				else
				{
					Item Gear3 = new Crossbow();
						Gear3.ItemID = henchman.HenchWeaponID;
						Gear3.Movable = false;
						BaseWeapon Bwep = (BaseWeapon)Gear3; Bwep.StrRequirement = 1;
						Gear3.LootType = LootType.Blessed;
						Gear3.Name = "Weapon";
							friend.AddItem( Gear3 );
				}

				if ( henchman.HenchCloak == 1 )
				{
					Item Capes = new Cloak();
						Capes.Hue = henchman.HenchCloakColor;
						Capes.Movable = false;
						BaseClothing Caper = (BaseClothing)Capes; Caper.StrRequirement = 1;
						Capes.LootType = LootType.Blessed;
							friend.AddItem( Capes );
				}

				Item Bootsy = new Boots();
					Bootsy.Hue = 0x967;
					if ( isOriental == true ){ Bootsy.ItemID = 0x2796; }
					Bootsy.Movable = false;
					BaseClothing Booty = (BaseClothing)Bootsy; Booty.StrRequirement = 1;
					Bootsy.LootType = LootType.Blessed;
						friend.AddItem( Bootsy );
			}
		}

		public static string GetTitle()
		{
			string sTitle = "";
			string myTitle = "";

			int otitle = Utility.RandomMinMax( 1, 33 );
			if (otitle == 1){sTitle = "of the North";}
			else if (otitle == 2){sTitle = "of the South";}
			else if (otitle == 3){sTitle = "of the East";}
			else if (otitle == 4){sTitle = "of the West";}
			else if (otitle == 5){sTitle = "of the City";}
			else if (otitle == 6){sTitle = "of the Hills";}
			else if (otitle == 7){sTitle = "of the Mountains";}
			else if (otitle == 8){sTitle = "of the Plains";}
			else if (otitle == 9){sTitle = "of the Woods";}
			else if (otitle == 10){sTitle = "of the Light";}
			else if (otitle == 11){sTitle = "of the Dark";}
			else if (otitle == 12){sTitle = "of the Night";}
			else if (otitle == 13){sTitle = "of the Sea";}
			else if (otitle == 14){sTitle = "of the Desert";}
			else if (otitle == 15){sTitle = "of the Order";}
			else if (otitle == 16){sTitle = "of the Forest";}
			else if (otitle == 17){sTitle = "of the Snow";}
			else if (otitle == 18){sTitle = "of the Coast";}
			else if (otitle == 19){sTitle = "of the Arid Wastes";}
			else if (otitle == 20){sTitle = "of the Beetling Brow";}
			else if (otitle == 21){sTitle = "of the Cyclopean City";}
			else if (otitle == 22){sTitle = "of the Dread Wilds";}
			else if (otitle == 23){sTitle = "of the Eerie Eyes";}
			else if (otitle == 24){sTitle = "of the Foetid Swamp";}
			else if (otitle == 25){sTitle = "of the Forgotten City";}
			else if (otitle == 26){sTitle = "of the Haunted Heath";}
			else if (otitle == 27){sTitle = "of the Hidden Valley";}
			else if (otitle == 28){sTitle = "of the Howling Hills";}
			else if (otitle == 29){sTitle = "of the Jagged Peaks";}
			else if (otitle == 30){sTitle = "of the Menacing Mien";}
			else if (otitle == 31){sTitle = "of the Savage Isle";}
			else if (otitle == 32){sTitle = "of the Tangled Woods";}
			else {sTitle = "of the Watchful Eyes";}

			string sColor = "Red";
			switch( Utility.RandomMinMax( 0, 9 ) )
			{
				case 0: sColor = "Black"; break;
				case 1: sColor = "Blue"; break;
				case 2: sColor = "Gray"; break;
				case 3: sColor = "Green"; break;
				case 4: sColor = "Red"; break;
				case 5: sColor = "Brown"; break;
				case 6: sColor = "Orange"; break;
				case 7: sColor = "Yellow"; break;
				case 8: sColor = "Purple"; break;
				case 9: sColor = "White"; break;
			}

			string gColor = "Gold";
			switch( Utility.RandomMinMax( 0, 11 ) )
			{
				case 0: gColor = "Gold"; break;
				case 1: gColor = "Silver"; break;
				case 2: gColor = "Arcane"; break;
				case 3: gColor = "Iron"; break;
				case 4: gColor = "Steel"; break;
				case 5: gColor = "Emerald"; break;
				case 6: gColor = "Ruby"; break;
				case 7: gColor = "Bronze"; break;
				case 8: gColor = "Jade"; break;
				case 9: gColor = "Sapphire"; break;
				case 10: gColor = "Copper"; break;
				case 11: gColor = "Royal"; break;
			}

			switch ( Utility.RandomMinMax( 0, 105 ) )
			{
				case 0: myTitle = "from Above"; break;
				case 1: myTitle = "from Afar"; break;
				case 2: myTitle = "from Below"; break;
				case 3: myTitle = "of the " + sColor + " Cloak"; break;
				case 4: myTitle = "of the " + sColor + " Robe"; break;
				case 5: myTitle = "of the " + sColor + " Order"; break;
				case 6: myTitle = "of the " + gColor + " Shield"; break;
				case 7: myTitle = "of the " + gColor + " Sword"; break;
				case 8: myTitle = "of the " + gColor + " Helm"; break;
				case 9: myTitle = sTitle; break;
				case 10: myTitle = sTitle; break;
				case 11: myTitle = sTitle; break;
				case 12: myTitle = sTitle; break;
				case 13: myTitle = sTitle; break;
				case 14: myTitle = sTitle; break;
				case 15: myTitle = sTitle; break;
				case 16: myTitle = sTitle; break;
				case 17: myTitle = sTitle; break;
				case 18: myTitle = sTitle; break;
				case 19: myTitle = sTitle; break;
				case 20: myTitle = sTitle; break;
				case 21: myTitle = sTitle; break;
				case 22: myTitle = "the " + sColor; break;
				case 23: myTitle = "the Adept"; break;
				case 24: myTitle = "the Nomad"; break;
				case 25: myTitle = "the Antiquarian"; break;
				case 26: myTitle = "the Arcane"; break;
				case 27: myTitle = "the Archaic"; break;
				case 28: myTitle = "the Barbarian"; break;
				case 29: myTitle = "the Batrachian"; break;
				case 30: myTitle = "the Battler"; break;
				case 31: myTitle = "the Bilious"; break;
				case 32: myTitle = "the Bold"; break;
				case 33: myTitle = "the Fearless"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Brave";} break;
				case 34: myTitle = "the Savage"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Civilized";} break;
				case 35: myTitle = "the Collector"; break;
				case 36: myTitle = "the Cryptic"; break;
				case 37: myTitle = "the Curious"; break;
				case 38: myTitle = "the Dandy"; break;
				case 39: myTitle = "the Daring"; break;
				case 40: myTitle = "the Decadent"; break;
				case 41: myTitle = "the Delver"; break;
				case 42: myTitle = "the Distant"; break;
				case 43: myTitle = "the Eldritch"; break;
				case 44: myTitle = "the Exotic"; break;
				case 45: myTitle = "the Explorer"; break;
				case 46: myTitle = "the Fair"; break;
				case 47: myTitle = "the Strong"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Weak";} break;
				case 48: myTitle = "the Fickle"; break;
				case 49:
						int iDice = Utility.RandomMinMax( 1, 10 );
						if (iDice == 1){myTitle = "the First";}
						else if (iDice == 2){myTitle = "the Second";}
						else if (iDice == 3){myTitle = "the Third";}
						else if (iDice == 4){myTitle = "the Fourth";}
						else if (iDice == 5){myTitle = "the Fifth";}
						else if (iDice == 6){myTitle = "the Sixth";}
						else if (iDice == 7){myTitle = "the Seventh";}
						else if (iDice == 8){myTitle = "the Eighth";}
						else if (iDice == 9){myTitle = "the Ninth";}
						else {myTitle = "the Tenth";}
						break;
				case 50: myTitle = "the Foul"; break;
				case 51: myTitle = "the Furtive"; break;
				case 52: myTitle = "the Gambler"; break;
				case 53: myTitle = "the Ghastly"; break;
				case 54: myTitle = "the Gibbous"; break;
				case 55: myTitle = "the Great"; break;
				case 56: myTitle = "the Grizzled"; break;
				case 57: myTitle = "the Gruff"; break;
				case 58: myTitle = "the Spiritual"; break;
				case 59: myTitle = "the Haunted"; break;
				case 60: myTitle = "the Calm"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Frantic";} break;
				case 61:
						int iDice2 = Utility.RandomMinMax( 1, 4 );
						if (iDice2 == 1){myTitle = "the Hooded";}
						else if (iDice2 == 2){myTitle = "the Cloaked";}
						else if (iDice2 == 3){myTitle = "the Cowled";}
						else {myTitle = "the Robed";}
						break;
				case 62: myTitle = "the Hunter"; break;
				case 63: myTitle = "the Imposing"; break;
				case 64: myTitle = "the Irreverent"; break;
				case 65: myTitle = "the Loathsome"; break;
				case 66:
						int iDice3 = Utility.RandomMinMax( 1, 3 );
						if (iDice3 == 1){myTitle = "the Quiet";}
						else if (iDice3 == 2){myTitle = "the Silent";}
						else {myTitle = "the Loud";}
						break;
				case 67: myTitle = "the Lovely"; break;
				case 68: myTitle = "the Mantled"; break;
				case 69: myTitle = "the Masked"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Veiled";} break;
				case 70: myTitle = "the Merciful"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Merciless";} break;
				case 71: myTitle = "the Mercurial"; break;
				case 72: myTitle = "the Mighty"; break;
				case 73: myTitle = "the Morose"; break;
				case 74: myTitle = "the Mutable"; break;
				case 75: myTitle = "the Mysterious"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Unknown";} break;
				case 76: myTitle = "the Obscure"; break;
				case 77: myTitle = "the Old"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Young";} break;
				case 78: myTitle = "the Ominous"; break;
				case 79: myTitle = "the Peculiar"; break;
				case 80: myTitle = "the Perceptive"; break;
				case 81: myTitle = "the Pious"; break;
				case 82: myTitle = "the Quick"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Slow";} break;
				case 83: myTitle = "the Ragged"; break;
				case 84: myTitle = "the Ready"; break;
				case 85: myTitle = "the Rough"; break;
				case 86: myTitle = "the Rugose"; break;
				case 87: myTitle = "the Scarred"; break;
				case 88: myTitle = "the Searcher"; break;
				case 89: myTitle = "the Shadowy"; break;
				case 90: myTitle = "the Short"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Tall";} break;
				case 91: myTitle = "the Steady"; break;
				case 92: myTitle = "the Uncanny"; break;
				case 93: myTitle = "the Unexpected"; break;
				case 94: myTitle = "the Unknowable"; break;
				case 95: myTitle = "the Verbose"; break;
				case 96: myTitle = "the Vigorous"; break;
				case 97: myTitle = "the Traveler"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Wanderer";} break;
				case 98: myTitle = "the Wary"; break;
				case 99: myTitle = "the Weird"; break;
				case 100: myTitle = "the Steady"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Unready";} break;
				case 101: myTitle = "the Gentle"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Cruel";} break;
				case 102: myTitle = "the Lost"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Exiled";} break;
				case 103: myTitle = "the Careless"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Clumsy";} break;
				case 104: myTitle = "the Hopeful"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Trustful";} break;
				case 105: myTitle = "the Angry"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Timid";} break;
			}
			return myTitle;
		}

		public static string GetEvilTitle()
		{
			string myTitle = "";

			switch ( Utility.RandomMinMax( 0, 54 ) )
			{
				case 0: myTitle = "the haunted"; 		break;
				case 1: myTitle = "of the undead"; 		break;
				case 2: myTitle = "of the void"; 		break;
				case 3: myTitle = "the vile"; 			break;
				case 4: myTitle = "of the dead";		break;
				case 5: myTitle = "of the cult"; 		break;
				case 6: myTitle = "of the grave"; 		break;
				case 7: myTitle = "the bloody"; 		break;
				case 8: myTitle = "of the " + Server.Misc.RandomThings.GetRandomColorName(0) + " Ghost"; 		break;
				case 9: myTitle = "of the tomb"; 		break;
				case 10: myTitle = "of the crypt"; 		break;
				case 11: myTitle = "the hated";			break;
				case 12: myTitle = "the villain";		break;
				case 13: myTitle = "the murderer";		break;
				case 14: myTitle = "the killer";		break;
				case 15: myTitle = "of the ghost";		break;
				case 16: myTitle = "the cultist";		break;
				case 17: myTitle = "the diabolist";		break;
				case 18: myTitle = "the butcher";		break;
				case 19: myTitle = "the slayer";		break;
				case 20: myTitle = "the executioner";	break;
				case 21: myTitle = "of the demon";		break;
				case 22: myTitle = "of the phantom";	break;
				case 23: myTitle = "the shadow";		break;
				case 24: myTitle = "of the spectre";	break;
				case 25: myTitle = "of the devil";		break;
				case 26: myTitle = "the shade";			break;
				case 27: myTitle = "of the wraith";		break;
				case 28: myTitle = "of the vampire";	break;
				case 29: myTitle = "of the banshee";	break;
				case 30: myTitle = "the dark";			break;
				case 31: myTitle = "the black";			break;
				case 32: myTitle = "of the fiend";		break;
				case 33: myTitle = "of the daemon";		break;
				case 34: myTitle = "the corrupt";		break;
				case 35: myTitle = "the hateful";		break;
				case 36: myTitle = "the heinous";		break;
				case 37: myTitle = "the hideous";		break;
				case 38: myTitle = "the malevolent";	break;
				case 39: myTitle = "the malicious";		break;
				case 40: myTitle = "the nefarious";		break;
				case 41: myTitle = "the vicious";		break;
				case 42: myTitle = "the wicked";		break;
				case 43: myTitle = "the foul";			break;
				case 44: myTitle = "the baneful";		break;
				case 45: myTitle = "the depraved";		break;
				case 46: myTitle = "the loathsome";		break;
				case 47: myTitle = "the wrathful";		break;
				case 48: myTitle = "the woeful";		break;
				case 49: myTitle = "the grim";			break;
				case 50: myTitle = "the dismal";		break;
				case 51: myTitle = "the lifeless";		break;
				case 52: myTitle = "of the deceased";	break;
				case 53: myTitle = "the bloodless";		break;
				case 54: myTitle = "the mortified";		break;
			}
			return myTitle;
		}

		public static int GetHue( int nValue )
		{
			int Hue = 0;
			switch( nValue )
			{
				case 0: Hue = Utility.RandomNeutralHue(); break;
				case 1: Hue = Utility.RandomRedHue(); break;
				case 2: Hue = Utility.RandomBlueHue(); break;
				case 3: Hue = Utility.RandomGreenHue(); break;
				case 4: Hue = Utility.RandomYellowHue(); break;
				case 5: Hue = Utility.RandomSnakeHue(); break;
				case 6: Hue = Utility.RandomMetalHue(); break;
				case 7: Hue = Utility.RandomAnimalHue(); break;
				case 8: Hue = Utility.RandomSlimeHue(); break;
				case 9: Hue = Utility.RandomOrangeHue(); break;
				case 10: Hue = Utility.RandomPinkHue(); break;
				case 11: Hue = Utility.RandomDyedHue(); break;
				case 12: Hue = Utility.RandomList( 0x467E, 0x481, 0x482, 0x47F ); break;
				case 13: Hue = Utility.RandomList( 0x54B, 0x54C, 0x54D, 0x54E, 0x54F, 0x550, 0x4E7, 0x4E8, 0x4E9, 0x4EA, 0x4EB, 0x4EC ); break;
				case 14: Hue = Utility.RandomList( 0x551, 0x552, 0x553, 0x554, 0x555, 0x556, 0x4ED, 0x4EE, 0x4EF, 0x4F0, 0x4F1, 0x4F2 ); break;
				case 15: Hue = Utility.RandomList( 0x557, 0x558, 0x559, 0x55A, 0x55B, 0x55C, 0x4F3, 0x4F4, 0x4F5, 0x4F6, 0x4F7, 0x4F8 ); break;
				case 16: Hue = Utility.RandomList( 0x55D, 0x55E, 0x55F, 0x560, 0x561, 0x562, 0x4F9, 0x4FA, 0x4FB, 0x4FC, 0x4FD, 0x4FE ); break;
				case 17: Hue = Utility.RandomList( 0xB93, 0xB94, 0xB95, 0xB96, 0xB83 ); break;
				case 18: Hue = Utility.RandomList( 0x1, 0x497, 0x965, 0x966, 0x96B, 0x96C ); break;
			}
			return Hue;
		}

		public static void ChangeSpeed( BaseCreature henchman )
		{
		    henchman.AIFullSpeedActive = henchman.AIFullSpeedPassive = !henchman.AIFullSpeedActive;
		    AnnounceSpeed(henchman, henchman.AIFullSpeedActive);
		}
		public static void ChangeSpeed( BaseCreature henchman, bool speed )
		{
		    henchman.AIFullSpeedActive = henchman.AIFullSpeedPassive = speed;
		    AnnounceSpeed(henchman, speed);
		}
		public static void AnnounceSpeed( BaseCreature henchman, bool speed )
		{
		    if (speed)
		    {
			if (henchman is HenchmanMonster)
			    henchman.Say("Me run now.");
			else
			    henchman.Say("I will run along with thee now.");
		    }
		    else
		    {
			if (henchman is HenchmanMonster)
			    henchman.Say("Me walk now.");
			else
			    henchman.Say("I will walk with thee now.");
		    }
		}

		// Using bandages
		private class BandageTimer : Timer 
		{ 
		    private Mobile pk;

		    public BandageTimer( Mobile o ) : base( TimeSpan.FromSeconds( 5 ) ) 
		    { 
			pk = o;
			Priority = TimerPriority.OneSecond; 
		    } 

		    protected override void OnTick() 
		    { 
			if (pk is HenchmanFighter)
			    ((HenchmanFighter)pk).Bandaging = false; 
			else if (pk is HenchmanArcher)
			    ((HenchmanArcher)pk).Bandaging = false;
			else if (pk is HenchmanWizard)
			    ((HenchmanWizard)pk).Bandaging = false;
			else if (pk is HenchmanMonster)
			    ((HenchmanMonster)pk).Bandaging = false;
		    } 
		}

		public static void UseBandages( Mobile m )
		{
		    bool Bandaging = false;

		    if (m is HenchmanFighter)
			Bandaging = ((HenchmanFighter)m).Bandaging;
		    else if (m is HenchmanArcher)
			Bandaging = ((HenchmanArcher)m).Bandaging;
		    else if (m is HenchmanWizard)
			Bandaging = ((HenchmanWizard)m).Bandaging;
		    else if (m is HenchmanMonster)
			Bandaging = ((HenchmanMonster)m).Bandaging;

		    if ( (m.Hunger > 0) && (m.Hits < m.HitsMax) && Bandaging == false )
		    {

			int nHealing = (int)m.Skills[SkillName.Healing].Value; // FINAL was magic resist before... WHY??? 
			int damaged = m.HitsMax - m.Hits; // FINAL
			double minhealed = damaged * 0.6;
			int mininthealed = Convert.ToInt32(minhealed);
			int amounthealed = Utility.RandomMinMax( mininthealed, damaged ); // FINAL randomizing amount healed

			if ( nHealing >= Utility.Random( 100 ) )
			{	
			    if ( m.Poisoned )
			    { if ( nHealing>=60 && ( Utility.Random( 100 ) <= nHealing ) )
				{ m.CurePoison( m ); } 
			    }
			    else 
			    { 
				m.Hits += amounthealed;
				//m.Hits = m.HitsMax; FINAL:  they were too OP
			    }
			    m.Hunger = m.Hunger - 1;
			    m.RevealingAction();
			    m.PlaySound( 0x57 );

			    if (m is HenchmanFighter)
				((HenchmanFighter)m).Bandaging = true; 
			    else if (m is HenchmanArcher)
				((HenchmanArcher)m).Bandaging = true;
			    else if (m is HenchmanWizard)
				((HenchmanWizard)m).Bandaging = true;
			    else if (m is HenchmanMonster)
				((HenchmanMonster)m).Bandaging = true;

			    BandageTimer bt = new BandageTimer( m );
			    bt.Start();					

			}
			if ( m.Stam < m.StamMax ){ m.Stam = m.Stam + (int)(m.StamMax / 40); }
			if ( m.Mana < m.ManaMax ){ m.Mana = m.Mana + (int)(m.StamMax / 20); }				
		    }
		}
	}
}
