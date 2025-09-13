using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Multis;

namespace Server.Mobiles 
{
	public class BasePirate : BaseCreature 
	{
        public BaseBoat ship;
        public bool boatspawn;
        public bool crewspawn;
		public string healme;
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }

		[Constructable] 
		public BasePirate() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			healme = "Heal me mateys!";
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.FilthyRich );
		}

		public override int TreasureMapLevel{ get{ return Utility.RandomMinMax( 1, 6 ); } }

		public override void OnThink()
		{
  			if( !boatspawn )
  			{
				if ( this is SailorOrkGuards || this is SailorElfGuards || this is SailorGuards ){ TitleGuards( this ); }

				Map map = Map;
				
  				if ( map == null )
  					return;

				Server.Multis.BaseBoat.BuildShip( ship, this );
				boatspawn = true;
				if ( Server.Multis.BaseBoat.IsNearOtherShip( this ) ){ this.Delete(); }
				else if ( Worlds.TestShore( Map, X, Y, 15 ) ){ this.Delete(); }
			}
		
			base.OnThink();

        	if (ship == null)
			{
				this.Delete();
			}

			base.OnThink();

  			if ( !crewspawn ) 
  			{
				crewspawn = true;
				int crew = Utility.RandomMinMax( 9, 12 );
				BaseCreature pirate = new Brigand(); pirate.Delete();
				bool evil = true;
				string toss = "stones";

				while ( crew > 0 )
				{
					if ( this is PirateCyclops || this is PirateDragonogre || this is PirateEttinMage || this is PirateTroll || this is PirateOgreLord || this is PirateMinotaur )
					{
						switch( Utility.RandomMinMax( 1, 13 ) )
						{
							case 1: pirate = new Orc();				toss = "stones";	break;
							case 2: pirate = new Bugbear();			toss = "axes";		break;
							case 3: pirate = new Gnoll();			toss = "daggers";	break;
							case 4: pirate = new Goblin();			toss = "darts";		break;
							case 5: pirate = new Morlock();			toss = "stones";	break;
							case 6: pirate = new Neanderthal();		toss = "stones";	break;
							case 7: pirate = new Ratman();			toss = "daggers";	pirate.Body = 42; 		break;
							case 8: pirate = new Minotaur();		toss = "axes";		pirate.Body = 241;		break;
							case 9: pirate = new Orc();				toss = "axes";		pirate.Body = 20;		break;
							case 10: pirate = new Orc();			toss = "daggers";	pirate.Body = 182;		break;
							case 11: pirate = new Orc();			toss = "arrows";	pirate.Body = 252;		break;
							case 12: pirate = new Kobold();			toss = "daggers";	pirate.Body = 245;		break;
							case 13: pirate = new Minotaur();		toss = "axes";		pirate.Body = 78;		break;
						}
					}
					else if ( this is PirateDaemon || this is PirateDemon || this is PirateDemoness || this is PirateDevil || this is PirateSuccubus )
					{
						switch( Utility.RandomMinMax( 1, 5 ) )
						{
							case 1: pirate = new Demon(); 			toss = "fire";		pirate.Hue = 0;		pirate.Body = 112;		break;
							case 2: pirate = new Gargoyle(); 		toss = "fire";		pirate.Hue = 0;		pirate.Body = 112;		break;
							case 3: pirate = new Succubus(); 		toss = "energy";	pirate.Hue = 0;		pirate.Body = 149;		break;
							case 4: pirate = new Demon(); 			toss = "daggers";	pirate.Hue = 0;		pirate.Body = 128;		break;
							case 5: pirate = new Demon(); 			toss = "poison";	pirate.Hue = 0;		pirate.Body = 136;		break;
						}
					}
					else if ( this is PirateGargoyle )
					{
						switch( Utility.RandomMinMax( 1, 5 ) )
						{
							case 1: pirate = new Gargoyle(); 		pirate.Body = 112;		break;
							case 2: pirate = new Gargoyle(); 		pirate.Body = 126;		break;
							case 3: pirate = new Gargoyle(); 		pirate.Body = 113;		break;
							case 4: pirate = new Gargoyle(); 		pirate.Body = 158;		break;
							case 5: pirate = new Gargoyle(); 								break;
						}
						switch( Utility.RandomMinMax( 1, 5 ) )
						{
							case 1: toss = "fire";		break;
							case 2: toss = "cold";		break;
							case 3: toss = "energy";	break;
							case 4: toss = "poison";	break;
							case 5: toss = "bolt";		break;
						}
					}
					else if ( this is PirateGrathek || this is PirateSakleth )
					{
						switch( Utility.RandomMinMax( 1, 4 ) )
						{
							case 1: pirate = new LizardmanArcher();		toss = "spear";		break;
							case 2: pirate = new Lizardman();			toss = "daggers";	pirate.Body = 33;		break;
							case 3: pirate = new Lizardman();			toss = "rocks";		pirate.Body = 326;		break;
							case 4: pirate = new Lizardman();			toss = "darts";		pirate.Body = 375;		break;
						}
					}
					else if ( this is PirateTitan )
					{
						switch( Utility.RandomMinMax( 1, 4 ) )
						{
							case 1: pirate = new Tritun();		toss = "daggers";		break;
							case 2: pirate = new Tritun();		toss = "spear";			pirate.Body = 678;		break;
							case 3: pirate = new Tritun();		toss = "poison";		pirate.Body = 676;		break;
							case 4: pirate = new Tritun();		toss = "bolt";			pirate.Body = 677;		break;
						}
					}
					else if ( this is PirateOphidian ){ 	pirate = new OphidianWarrior();		toss = "dagger";	pirate.Body = 87;	}
					else if ( this is PirateSnakeMan )
					{
						evil = false;
						switch( Utility.RandomMinMax( 1, 2 ) )
						{
							case 1: pirate = new OphidianWarrior();		toss = "poison";		pirate.Body = 704;		break;
							case 2: pirate = new OphidianWarrior();		toss = "daggers";		pirate.Body = 143;		break;
						}
					}
					else if ( this is PirateUndead )
					{
						switch( Utility.RandomMinMax( 1, 5 ) )
						{
							case 1: pirate = new Zombie(); 			toss = "bones";									break;
							case 2: pirate = new Zombie(); 			toss = "bones";			pirate.Body = 304;		break;
							case 3: pirate = new Ghoul(); 			toss = "bones";									break;
							case 4: pirate = new AquaticGhoul(); 	toss = "bones";									break;
							case 5: pirate = new Wight(); 			toss = "bones";									break;
						}
					}
					else if ( this is PirateGhost )
					{
						switch( Utility.RandomMinMax( 1, 5 ) )
						{
							case 1: pirate = new Spectre(); 			toss = "fire";			break;
							case 2: pirate = new Spectre(); 			toss = "cold";			break;
							case 3: pirate = new Spectre(); 			toss = "energy";		break;
							case 4: pirate = new Spectre(); 			toss = "bolt";			break;
							case 5: pirate = new Spectre(); 			toss = "poison";		break;
						}
					}
					else if ( this is PirateDarkLord || this is PirateLich || this is PirateLichLord || this is PirateSkeleton )
					{
						switch( Utility.RandomMinMax( 1, 14 ) )
						{
							case 1: pirate = new Zombie(); 			toss = "bones";									break;
							case 2: pirate = new Zombie(); 			toss = "bones";			pirate.Body = 304;		break;
							case 3: pirate = new Spectre(); 		toss = "fire";									break;
							case 4: pirate = new Ghoul(); 			toss = "bones";									break;
							case 5: pirate = new AquaticGhoul(); 	toss = "bones";									break;
							case 6: pirate = new Skeleton(); 		toss = "bones";			pirate.Body = 50;		break;
							case 7: pirate = new SkeletonArcher(); 	toss = "arrows";								break;
							case 8: pirate = new Wight(); 			toss = "bones";									break;
							case 9: pirate = new Skeleton(); 		toss = "bandages";		pirate.Body = 154;		BaseSoundID = 471;	break;
							case 10: pirate = new BoneMagi(); 		toss = "fire";									break;
							case 11: pirate = new BoneMagi(); 		toss = "cold";									break;
							case 12: pirate = new BoneMagi(); 		toss = "energy";								break;
							case 13: pirate = new BoneMagi(); 		toss = "poison";								break;
							case 14: pirate = new BoneMagi(); 		toss = "bolt";									break;
						}
					}


					else if ( this is PirateDrow ){ 		pirate = new ElfBerserker();		toss = "crossbow";	}
					else if ( this is PirateMen ){ 			pirate = new Berserker();			toss = "crossbow";	}
					else if ( this is PirateNatives ){ 		pirate = new Berserker();			toss = "harpoon";	}
					else if ( this is PirateCult )
					{
						switch( Utility.RandomMinMax( 1, 12 ) )
						{
							case 1: pirate = new Brigand(); 	toss = "daggers";	break;
							case 2: pirate = new Brigand(); 	toss = "stones";	break;
							case 3: pirate = new Brigand(); 	toss = "stars";		break;
							case 4: pirate = new Brigand(); 	toss = "darts";		break;
							case 5: pirate = new Brigand(); 	toss = "axes";		break;
							case 6: pirate = new Brigand(); 	toss = "bones";		break;
							case 7: pirate = new Brigand(); 	toss = "arrows";	break;
							case 8: pirate = new Brigand(); 	toss = "fire";		break;
							case 9: pirate = new Brigand(); 	toss = "cold";		break;
							case 10: pirate = new Brigand(); 	toss = "energy";	break;
							case 11: pirate = new Brigand(); 	toss = "poison";	break;
							case 12: pirate = new Brigand(); 	toss = "bolt";		break;
						}
					}
					else if ( this is SailorElfGuards )
					{
						pirate = new ElfBerserker();
						evil = false;
						switch( Utility.RandomMinMax( 1, 3 ) )
						{
							case 1: toss = "crossbow";		break;
							case 2: toss = "bow";			break;
							case 3: toss = "harpoon";		break;
						}
					}
					else if ( this is SailorGuards )
					{
						pirate = new Berserker();
						evil = false;
						switch( Utility.RandomMinMax( 1, 3 ) )
						{
							case 1: toss = "crossbow";		break;
							case 2: toss = "bow";			break;
							case 3: toss = "harpoon";		break;
						}
					}
					else if ( this is SailorOrkGuards )
					{
						pirate = new OrkWarrior();
						evil = false;
						switch( Utility.RandomMinMax( 1, 3 ) )
						{
							case 1: toss = "crossbow";		break;
							case 2: toss = "bow";			break;
							case 3: toss = "harpoon";		break;
						}
					}
					else if ( this is SailorElf )
					{
						pirate = new ElfBerserker();
						evil = false;
						switch( Utility.RandomMinMax( 1, 7 ) )
						{
							case 1: toss = "daggers";	break;
							case 2: toss = "harpoon";	break;
							case 3: toss = "stars";		break;
							case 4: toss = "darts";		break;
							case 5: toss = "axes";		break;
							case 6: toss = "arrows";	break;
							case 7: toss = "crossbow";	break;
						}
					}
					else if ( this is SailorMerchant )
					{
						pirate = new Berserker();
						evil = false;
						switch( Utility.RandomMinMax( 1, 7 ) )
						{
							case 1: toss = "daggers";	break;
							case 2: toss = "harpoon";	break;
							case 3: toss = "stars";		break;
							case 4: toss = "darts";		break;
							case 5: toss = "axes";		break;
							case 6: toss = "arrows";	break;
							case 7: toss = "crossbow";		break;
						}
					}
					else if ( this is SailorAngel || this is SailorAngelLord )
					{
						evil = false;
						switch( Utility.RandomMinMax( 1, 5 ) )
						{
							case 1: pirate = new Pixie();		toss = "fire";			pirate.Body = Utility.RandomList( 356, 128 ); break;
							case 2: pirate = new Fairy();		toss = "cold";			pirate.Body = 363; break;
							case 3: pirate = new Centaur();		toss = "arrows";		break;
							case 4: pirate = new Satyr();		toss = "daggers";		break;
							case 5: pirate = new Wisp();		toss = "energy";		break;
						}
					}

					SizeUpCrewMember( pirate, evil, EmoteHue, this, toss );
					crew--;
				}
			}
		}

		public override void OnDelete()
		{
			Server.Multis.BaseBoat.SinkShip( ship, this );
			base.OnDelete();
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			GrappleEnemy( from, this );
			base.OnDamage( amount, from, willKill );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			GrappleEnemy( attacker, this );
			base.OnGotMeleeAttack( attacker );
		}

		public void GrappleEnemy( Mobile enemy, Mobile me )
		{
			if ( me != null && me.Hits > 0 && Utility.RandomBool() && me.Z < 0 )
			{
				Effects.SendLocationParticles( EffectItem.Create( enemy.Location, enemy.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( enemy, enemy.Map, 0x201 );
				enemy.Location = me.Location;
				me.Combatant = enemy;
				me.Warmode = true;
				Effects.SendLocationParticles( EffectItem.Create( enemy.Location, enemy.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( enemy, enemy.Map, 0x201 );
			}
		}

		public void DressUpCrewMember( BaseCreature bc, Mobile captain )
		{
			if ( captain is PirateDrow )
			{
				bc.AddItem( new ElvenBoots( 0x6F8 ) );
				Item armor = new LeatherChest(); armor.Hue = 0x6F8; bc.AddItem( armor );
				bc.AddItem( new FancyShirt( 0 ) );	
				switch ( Utility.Random( 2 ))
				{
					case 0: bc.AddItem( new LongPants ( 0xBB4 ) ); break;
					case 1: bc.AddItem( new ShortPants ( 0xBB4 ) ); break;
				}
				switch ( Utility.Random( 2 ))
				{
					case 0: bc.AddItem( new Bandana ( 0x846 ) ); break;
					case 1: bc.AddItem( new SkullCap ( 0x846 ) ); break;
				}
			}
			else if ( captain is PirateMen )
			{
				bc.AddItem( new ElvenBoots( 0x83A ) );
				Item armor = new LeatherChest(); armor.Hue = 0x83A; bc.AddItem( armor );
				bc.AddItem( new FancyShirt( 0 ) );	
				switch ( Utility.Random( 2 ))
				{
					case 0: bc.AddItem( new LongPants ( 0xBB4 ) ); break;
					case 1: bc.AddItem( new ShortPants ( 0xBB4 ) ); break;
				}				

				switch ( Utility.Random( 2 ))
				{
					case 0: bc.AddItem( new Bandana ( 0x846 ) ); break;
					case 1: bc.AddItem( new SkullCap ( 0x846 ) ); break;
				}
			}
			else if ( captain is PirateNatives )
			{
				bc.Hue = 743;
				bc.HairHue = 0x96C;

				if ( bc.Female )
				{
					Item cloth9 = new FemaleLeatherChest();
						cloth9.Hue = 773;
						cloth9.Name = "Native Tunic";
						bc.AddItem( cloth9 );
				}
				Item cloth1 = new SavageArms();
					cloth1.Hue = 773;
					cloth1.Name = "Native Guantlets";
					bc.AddItem( cloth1 );
				Item cloth2 = new SavageLegs();
					cloth2.Hue = 773;
					cloth2.Name = "Native Leggings";
					bc.AddItem( cloth2 );
				Item cloth3 = new TribalMask();
					cloth3.Hue = 773;
					cloth3.Name = "Native Tribal Mask";
					bc.AddItem( cloth3 );
				Item cloth4 = new LeatherSkirt();
					cloth4.Hue = 773;
					cloth4.Name = "Native Skirt";
					cloth4.Layer = Layer.Waist;
					bc.AddItem( cloth4 );
			}
			else if ( captain is PirateCult )
			{
				bc.AddItem( new Robe( 0 ) );	
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: bc.AddItem( new ClothCowl() ); break;
					case 1: bc.AddItem( new ClothHood() ); break;
					case 2: bc.AddItem( new FancyHood() ); break;
				}
				MorphingTime.ColorMyClothes( bc, captain.SpeechHue );
				bc.AddItem( new ElvenBoots( 0x83A ) );

				if ( bc.FindItemOnLayer( Layer.OneHanded ) != null ) { Item hand = bc.FindItemOnLayer( Layer.OneHanded ); hand.ItemID = 0x13C6; hand.Name = "gloves"; hand.MoveToWorld( captain.Location, captain.Map ); bc.AddItem( hand ); }
				if ( bc.FindItemOnLayer( Layer.TwoHanded ) != null ) { Item hand = bc.FindItemOnLayer( Layer.TwoHanded ); hand.ItemID = 0x13C6; hand.Name = "gloves"; hand.MoveToWorld( captain.Location, captain.Map ); bc.AddItem( hand );  }
			}
			else if ( captain is SailorGuards || captain is SailorElfGuards || captain is SailorOrkGuards )
			{
				DressGuards( bc, captain );
				if ( captain is SailorElfGuards ){ bc.HairHue = Utility.RandomHairHue(); bc.Hue = Server.Misc.RandomThings.GetRandomSkinColor(); }
			}
			else if ( captain is SailorMerchant || captain is SailorElf )
			{
				DressSailor( bc );
				if ( captain is SailorElf ){ bc.HairHue = Utility.RandomHairHue(); bc.Hue = Server.Misc.RandomThings.GetRandomSkinColor(); }
				if ( bc.FindItemOnLayer( Layer.OneHanded ) != null )
				{
					Item hand = bc.FindItemOnLayer( Layer.OneHanded );
					if ( hand is MonsterGloves )
					{
						hand.ItemID = 0x1087;
						hand.Hue = 0;
						hand.Name = "earrings";
						hand.MoveToWorld( captain.Location, captain.Map );
						bc.AddItem( hand );
					}
				}
			}
		}

		public void TitleGuards( Mobile captain )
		{
			int title = 1;
			if ( Server.Misc.Worlds.GetMyWorld( captain.Map, captain.Location, captain.X, captain.Y ) == "the Land of Lodoria" ){ title = Utility.RandomMinMax( 1, 10 ); }
			else if ( Server.Misc.Worlds.GetMyWorld( captain.Map, captain.Location, captain.X, captain.Y ) == "the Land of Sosaria" ){ title = Utility.RandomMinMax( 11, 18 ); }
			else if ( Server.Misc.Worlds.GetMyWorld( captain.Map, captain.Location, captain.X, captain.Y ) == "the Savaged Empire" ){ title = Utility.RandomMinMax( 22, 23 ); }
			else if ( Server.Misc.Worlds.GetMyWorld( captain.Map, captain.Location, captain.X, captain.Y ) == "the Isles of Dread" ){ title = 21; }
			else if ( Server.Misc.Worlds.GetMyWorld( captain.Map, captain.Location, captain.X, captain.Y ) == "the Island of Umber Veil" ){ title = 19; }
			else if ( Server.Misc.Worlds.GetMyWorld( captain.Map, captain.Location, captain.X, captain.Y ) == "the Bottle World of Kuldar" ){ title = 20; }

			switch( title )
			{
				case 1: captain.Title = "of the Whisper Guard";		break;
				case 2: captain.Title = "of the Glacial Guard";		break;
				case 3: captain.Title = "of the Springvale Guard";	break;
				case 4: captain.Title = "of the Elidor Guard";		break;
				case 5: captain.Title = "of the Islegem Guard";		break;
				case 6: captain.Title = "of the Greensky Guard";	break;
				case 7: captain.Title = "of the Dusk Guard";		break;
				case 8: captain.Title = "of the Starguide Guard";	break;
				case 9: captain.Title = "of the Portshine Guard";	break;
				case 10: captain.Title = "of the Lodoria Guard";	break;

				case 11: captain.Title = "of the Devil Guard";		break;
				case 12: captain.Title = "of the Moon Guard";		break;
				case 13: captain.Title = "of the Grey Guard";		break;
				case 14: captain.Title = "of the Montor Guard";		break;
				case 15: captain.Title = "of the Fawn Guard";		break;
				case 16: captain.Title = "of the Yew Guard";		break;
				case 17: captain.Title = "of the Iceclad Guard";	break;
				case 18: captain.Title = "of the Britain Guard";	break;

				case 19: captain.Title = "of the Renika Guard";		break;

				case 20: captain.Title = "of the Kuldara Guard";	break;

				case 21: captain.Title = "of the Cimmeran Guard";	break;

				case 22: captain.Title = "of the Barako Guard";		break;
				case 23: captain.Title = "of the Kurak Guard";		break;
			}
			DressGuards( (BaseCreature)captain, captain );
		}

		public void DressGuards( BaseCreature bc, Mobile captain )
		{
			int clothColor = 0;
			int helmType = 0;
			int cloakColor = 0;

			if ( captain.Title == "of the Whisper Guard" )
			{
				clothColor = 0x96D;		helmType = 0x140E;		cloakColor = 0x972;
			}
			else if ( captain.Title == "of the Glacial Guard" )
			{
				clothColor = 0xB70;		helmType = 0x1412;		cloakColor = 0xB7A;
			}
			else if ( captain.Title == "of the Springvale Guard" )
			{
				clothColor = 0x595;		helmType = 0x140E;		cloakColor = 0x593;
			}
			else if ( captain.Title == "of the Elidor Guard" )
			{
				clothColor = 0x665;		helmType = 0x1412;		cloakColor = 0x664;
			}
			else if ( captain.Title == "of the Islegem Guard" )
			{
				clothColor = 0x7D1;		helmType = 0x140E;		cloakColor = 0x7D6;
			}
			else if ( captain.Title == "of the Greensky Guard" )
			{
				clothColor = 0x7D7;		helmType = 0x1412;		cloakColor = 0x7DA;
			}
			else if ( captain.Title == "of the Dusk Guard" )
			{
				clothColor = 0x601;		helmType = 0x140E;		cloakColor = 0x600;
			}
			else if ( captain.Title == "of the Starguide Guard" )
			{
				clothColor = 0x751;		helmType = 0x1412;		cloakColor = 0x758;
			}
			else if ( captain.Title == "of the Portshine Guard" )
			{
				clothColor = 0x847;		helmType = 0x140E;		cloakColor = 0x851;
			}
			else if ( captain.Title == "of the Lodoria Guard" )
			{
				clothColor = 0x6E4;		helmType = 0x1412;		cloakColor = 0x6E7;
			}
			else if ( captain.Title == "of the Devil Guard" )
			{
				clothColor = 0x430;		helmType = 0x140E;		cloakColor = 0;
			}
			else if ( captain.Title == "of the Moon Guard" )
			{
				clothColor = 0x8AF;		helmType = 0x1412;		cloakColor = 0x972;
			}
			else if ( captain.Title == "of the Grey Guard" )
			{
				clothColor = 0;			helmType = 0x140E;		cloakColor = 0x763;
			}
			else if ( captain.Title == "of the Montor Guard" )
			{
				clothColor = 0x96F;		helmType = 0x1412;		cloakColor = 0x529;
			}
			else if ( captain.Title == "of the Fawn Guard" )
			{
				clothColor = 0x59D;		helmType = 0x140E;		cloakColor = 0x59C;
			}
			else if ( captain.Title == "of the Yew Guard" )
			{
				clothColor = 0x83C;		helmType = 0x1412;		cloakColor = 0x850;
			}
			else if ( captain.Title == "of the Iceclad Guard" )
			{
				clothColor = 0x482;		helmType = 0x140E;		cloakColor = 0x47E;
			}
			else if ( captain.Title == "of the Britain Guard" )
			{
				clothColor = 0x9C4;		helmType = 0x140E;		cloakColor = 0x845;
			}
			else if ( captain.Title == "of the Renika Guard" )
			{
				clothColor = 0xA5D;		helmType = 0x140E;		cloakColor = 0x96D;
			}
			else if ( captain.Title == "of the Kuldara Guard" )
			{
				clothColor = 0x965;		helmType = 0x140E;		cloakColor = 0x845;
			}
			else if ( captain.Title == "of the Cimmeran Guard" )
			{
				clothColor = 0x978;		helmType = 0;			cloakColor = 0x973;
			}
			else if ( captain.Title == "of the Barako Guard" )
			{
				clothColor = 0x515;		helmType = 0x2645;		cloakColor = 0x58D;
			}
			else if ( captain.Title == "of the Kurak Guard" )
			{
				clothColor = 0x515;		helmType = 0x140E;		cloakColor = 0x59D;
			}


			Item arms = new PlateArms();
			Item tunic = new PlateChest();
			Item legs = new PlateLegs();
			Item neck = new PlateGorget();
			Item hand = new PlateGloves();
			Item foot = new Boots( );


			if ( captain.Title == "of the Cimmeran Guard" )
			{
				tunic.ItemID = 0x5652;	tunic.Name = "tunic";
				if ( bc.Female )
				{
					tunic.ItemID = 0x563E;
					Utility.AssignRandomHair( bc );
				}
				else
				{
					Utility.AssignRandomHair( bc );
					FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
				}


				bc.HairHue = 0x455;
				bc.FacialHairHue = 0x455;


				arms.ItemID = 22093;	arms.Name = "sleeves";
				legs.ItemID = 7176;		legs.Name = "skirt";
				neck.ItemID = 0x5650;	neck.Name = "amulet";
				hand.ItemID = 0x564E;	hand.Name = "gloves";
				foot.ItemID = 5901;		foot.Name = "sandals";
			}


			bc.AddItem( tunic );
			bc.AddItem( arms );
			bc.AddItem( legs );
			bc.AddItem( neck );
			bc.AddItem( hand );
			bc.AddItem( foot );


			if ( helmType > 0 )
			{
				PlateHelm helm = new PlateHelm();
					helm.ItemID = helmType;
					helm.Name = "helm";
					bc.AddItem( helm );
			}

			MorphingTime.ColorMyClothes( bc, clothColor );

			if ( bc is BasePirate )
			{
				Cloak cloak = new Cloak();
					cloak.Hue = cloakColor;
					bc.AddItem( cloak );
			}
		}

		public void DressSailor( BaseCreature bc )
		{
			switch ( Utility.Random( 3 ) )
			{
				case 0: bc.AddItem( new FancyShirt() ); break;
				case 1: bc.AddItem( new Doublet() ); break;
				case 2: bc.AddItem( new Shirt() ); break;
			}

			switch ( Utility.Random( 4 ) )
			{
				case 0: bc.AddItem( new Shoes() ); break;
				case 1: bc.AddItem( new Boots() ); break;
				case 2: bc.AddItem( new ElvenBoots() ); break;
				case 3: bc.AddItem( new ThighBoots() ); break;
			}

			if ( bc.Female )
			{
				switch ( Utility.Random( 6 ) )
				{
					case 0: bc.AddItem( new ShortPants() ); break;
					case 1:
					case 2: bc.AddItem( new Kilt() ); break;
					case 3:
					case 4:
					case 5: bc.AddItem( new Skirt() ); break;
				}
			}
			else
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: bc.AddItem( new LongPants() ); break;
					case 1: bc.AddItem( new ShortPants() ); break;
				}
			}

			if ( bc is SailorElf || bc is SailorMerchant ){ bc.AddItem( new TricorneHat() ); }
			else if ( Utility.RandomBool() ){ bc.AddItem( new SkullCap() ); bc.HairItemID = 0; }
			else { bc.AddItem( new Bandana() ); }
			MorphingTime.SailorMyClothes( bc );
		}

		public void SizeUpCrewMember( BaseCreature bc, bool evil, int link, Mobile captain, string toss )
		{
			MorphingTime.RemoveMyClothes( (Mobile)bc );

			if ( bc.Backpack != null )
			{
				List<Item> belongings = new List<Item>();
				foreach( Item i in bc.Backpack.Items )
				{
					belongings.Add(i);
				}
				foreach ( Item stuff in belongings )
				{
					stuff.Delete();
				}
			}

			bc.AI = AIType.AI_Archer;
			bc.FightMode = FightMode.Evil;
			bc.Karma = 1500;
			bc.Name = "a sailor";

			if ( evil )
			{
				bc.FightMode = FightMode.Closest;
				bc.Karma = -1500;
				bc.Name = "a pirate";
					if ( captain is PirateCult ){ bc.Name = "a follower"; }
			}

			bc.SetStr( 96, 120 );
			bc.SetDex( 81, 105 );
			bc.SetInt( 36, 60 );

			bc.SetHits( 58, 72 );

			SetDamage( 10, 23 );

			bc.SetDamageType( ResistanceType.Physical, 100 );

			bc.SetResistance( ResistanceType.Physical, 35, 50 );
			bc.SetResistance( ResistanceType.Fire, 10, 30 );
			bc.SetResistance( ResistanceType.Cold, 10, 30 );
			bc.SetResistance( ResistanceType.Poison, 10, 30 );
			bc.SetResistance( ResistanceType.Energy, 10, 30 );

			bc.SetSkill( SkillName.Archery, 66.0, 97.5 );
			bc.SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			bc.SetSkill( SkillName.Tactics, 65.0, 87.5 );
			bc.SetSkill( SkillName.Wrestling, 66.0, 97.5 );

			bc.Fame = 1500;

			bc.VirtualArmor = 32;

			bc.Title = null;
			bc.EmoteHue = link;

			if ( toss == "stones" ){ MonsterGloves gloves = new MonsterGloves(); gloves.ThrowType = "Stones"; bc.AddItem( gloves ); }
			else if ( toss == "stars" ){ MonsterGloves gloves = new MonsterGloves(); gloves.ThrowType = "Stars"; bc.AddItem( gloves ); }
			else if ( toss == "axes" ){ MonsterGloves gloves = new MonsterGloves(); gloves.ThrowType = "Axes"; bc.AddItem( gloves ); }
			else if ( toss == "daggers" ){ MonsterGloves gloves = new MonsterGloves(); gloves.ThrowType = "Daggers"; bc.AddItem( gloves ); }
			else if ( toss == "darts" ){ MonsterGloves gloves = new MonsterGloves(); gloves.ThrowType = "Darts"; bc.AddItem( gloves ); }
			else if ( toss == "spear" ){ MonsterGloves gloves = new MonsterGloves(); gloves.ThrowType = "Spear"; bc.AddItem( gloves ); }
			else if ( toss == "boulder" ){ MonsterGloves gloves = new MonsterGloves(); gloves.ThrowType = "Boulder"; bc.AddItem( gloves ); }
			else if ( toss == "bones" ){ MonsterGloves gloves = new MonsterGloves(); gloves.ThrowType = "Bones"; bc.AddItem( gloves ); }
			else if ( toss == "arrows" ){ bc.AddItem( new Bow() ); }
			else if ( toss == "crossbow" ){ bc.AddItem( new Crossbow() ); }
			else if ( toss == "harpoon" ){ bc.AddItem( new Harpoon() ); }

			else if ( toss == "fire" ){ WizardStaff staff = new WizardStaff(); staff.AosElementDamages.Fire = 75; staff.damageType = 1; bc.AddItem( staff ); }
			else if ( toss == "cold" ){ WizardStaff staff = new WizardStaff(); staff.AosElementDamages.Cold = 75; staff.damageType = 2; bc.AddItem( staff ); }
			else if ( toss == "energy" ){ WizardStaff staff = new WizardStaff(); staff.AosElementDamages.Energy = 75; staff.damageType = 3; bc.AddItem( staff ); }
			else if ( toss == "poison" ){ WizardStaff staff = new WizardStaff(); staff.AosElementDamages.Poison = 75; staff.damageType = 4; bc.AddItem( staff ); }
			else if ( toss == "bolt" ){ WizardStaff staff = new WizardStaff(); staff.damageType = 0; bc.AddItem( staff ); }

			DressUpCrewMember( bc, captain );
			bc.NameColor();
			bc.MoveToWorld( captain.Location, captain.Map );
		}

		public override bool OnBeforeDeath()
		{
			if ( CaptainCanDie() )
			{
				Server.Multis.BaseBoat.SinkShip( ship, this );
				Point3D wreck = new Point3D((this.X+3), (this.Y+3), 0);
				SunkenShip ShipWreck = Server.Multis.BaseBoat.CreateSunkenShip( this, this.LastKiller );
				ShipWreck.MoveToWorld( wreck, Map );
			}
			else 
			{
				Say( healme );
				this.Hits = this.HitsMax;
				this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
				this.PlaySound( 0x202 );
				return false;
			}
			return base.OnBeforeDeath();   
		}

		public bool CaptainCanDie()
		{
			foreach ( Mobile m in GetMobilesInRange( 10 ) )
			{
				if ( m is BaseCreature && m.EmoteHue == this.EmoteHue && m != this && m.Alive )
				{
					return false;
				}
			}
			return true;
		}

		public static int ShipColor( string color )
		{
			if ( color == "reptile" ){ return Utility.RandomList( 0xB51, 0xB19, 0xACF, 0x91E, 0xB79, 0xB27, 0xACE ); }
			else if ( color == "demon" ){ return Utility.RandomList( 0xB02, 0xB7F, 0x7BB, 0x7B7 ); }
			else if ( color == "pixie" ){ return Utility.RandomList( 0xAEA, 0xAB5, 0x9BE, 0x926, 0x929, 0x7BC ); }
			else if ( color == "titan" ){ return Utility.RandomList( 0x94B, 0x8D1, 0xB2C ); }
			else if ( color == "undead" ){ return Utility.RandomList( 0xB31, 0xB38, 0xB5E, 0xB5F, 0xAB3 ); }
			return Utility.RandomList( 0xABE, 0xAC0, 0xABE, 0xABF, 0xAC1, 0xB2F );
		}

		public static BaseBoat HauntedShip()
		{
			switch( Utility.RandomMinMax( 1, 7 ) )
			{
				case 1: return new GalleonWreckedRoyal();		break;
				case 2: return new GalleonWreckedExotic();		break;
				case 3: return new GalleonWreckedLarge();		break;
				case 4: return new GalleonRuinedBarbarian();	break;
				case 5: return new GalleonRuinedRoyal();		break;
				case 6: return new GalleonRuinedExotic();		break;
			}

			return new GalleonWreckedBarbarian();
		}

		public static BaseBoat AverageShip()
		{
			switch( Utility.RandomMinMax( 1, 3 ) )
			{
				case 1: return new GalleonRoyal();		break;
				case 2: return new GalleonExotic();		break;
			}

			return new GalleonLarge();
		}

		public static bool IsSailor( Mobile m )
		{
			if ( m is BaseCreature )
			{
				if ( m is BasePirate || 
					m is BaseSailor || 
					m is PirateCaptain || 
					m is PirateCrew || 
					m is PirateCrewBow || 
					m is PirateCrewMage || 
					m is PirateLand || 
					m is BoatSailorArcher || 
					m is BoatSailorBard || 
					m is BoatSailorMage || 
					( m.Name == "a sailor" && m.EmoteHue > 0 ) || 
					( m.Name == "a follower" && m.EmoteHue > 0 ) || 
					( m.Name == "a pirate" && m.EmoteHue > 0 ) )
				{
					return true;
				}
			}

			return false;
		}

		public BasePirate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (Item)ship );
			writer.Write( (bool)boatspawn );
			writer.Write( (bool)crewspawn );
			writer.Write( healme );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            ship = reader.ReadItem() as BaseBoat;
            boatspawn = reader.ReadBool();
            crewspawn = reader.ReadBool();
            healme = reader.ReadString();
		}
	}
}