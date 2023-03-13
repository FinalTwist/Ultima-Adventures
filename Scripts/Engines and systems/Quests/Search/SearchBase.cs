using System;
using Server;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class SearchBase : BaseAddon
	{
		[Constructable]
		public SearchBase() : this( 0 )
		{
		}

		[ Constructable ]
		public SearchBase( int style )
		{
			int surface = Utility.RandomList( 0x40A3, 0x40A4, 0x40A5, 0x40A6, 0x40A7, 0x40A8, 0x40A9, 0x40AA, 0x40AB, 0x40AC, 0x40AD, 0x40AE, 0x40AF, 0x40B0, 0x40B1, 0x40B2, 0x40B3, 0x40B4, 0x40B5, 0x40B6, 0x40B7, 0x40B8, 0x40B9, 0x40BA, 0x40BB );

			int iPed = 0x71E;
			int hThing = 6;

			string sPed = "an ornately ";

			switch( Utility.RandomMinMax( 0, 10 ) )
			{
				case 0: sPed = "an ornately ";		break;
				case 1: sPed = "a beautifully ";	break;
				case 2: sPed = "an expertly ";		break;
				case 3: sPed = "an artistically ";	break;
				case 4: sPed = "an exquisitely ";	break;
				case 5: sPed = "a decoratively ";	break;
				case 6: sPed = "an ancient ";		break;
				case 7: sPed = "an old ";			break;
				case 8: sPed = "an unusually ";		break;
				case 9: sPed = "a curiously ";		break;
				case 10: sPed = "an oddly ";		break;
			}
			sPed = sPed + "crafted stone";

			int iColor = 0;
			int iThing = 0x9AA;
			string sArty = "a strange";
			switch( Utility.RandomMinMax( 0, 6 ) )
			{
				case 0: sArty = "an odd ";		break;
				case 1: sArty = "an unusual ";	break;
				case 2: sArty = "a bizarre ";	break;
				case 3: sArty = "a curious ";	break;
				case 4: sArty = "a peculiar ";	break;
				case 5: sArty = "a strange ";	break;
				case 6: sArty = "a weird ";		break;
			}

			string sThing = "metal chest";
			switch( Utility.RandomMinMax( 0, 1 ) )
			{
				case 0: iThing = 0x9AA; sThing = "metal chest"; break;
				case 1: iThing = 0xE7D; sThing = "metal chest"; break;
			}

			switch ( Utility.RandomMinMax( 0, 18 ) ) 
			{
				case 0: iColor = MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); sThing = "silver chest";		break;
				case 1: iColor = MaterialInfo.GetMaterialColor( "emerald", "classic", 0 ); sThing = "emerald chest";		break;
				case 2: iColor = MaterialInfo.GetMaterialColor( "jade", "classic", 0 ); sThing = "jade chest";			break;
				case 3: iColor = MaterialInfo.GetMaterialColor( "onyx", "classic", 0 ); sThing = "onyx chest";			break;
				case 4: iColor = MaterialInfo.GetMaterialColor( "ruby", "classic", 0 ); sThing = "ruby chest";			break;
				case 5: iColor = MaterialInfo.GetMaterialColor( "sapphire", "classic", 0 ); sThing = "sapphire chest";		break;
				case 6: iColor = 0x317; sThing = "iron chest";			break;
				case 7: iColor = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); sThing = "dull copper chest";	break;
				case 8: iColor = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); sThing = "shadow iron chest";	break;
				case 9: iColor = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); sThing = "copper chest";				break;
				case 10: iColor = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); sThing = "bronze chest";				break;
				case 11: iColor = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); sThing = "golden chest";				break;
				case 12: iColor = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); sThing = "agapite chest";			break;
				case 13: iColor = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); sThing = "verite chest";				break;
				case 14: iColor = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); sThing = "valorite chest";			break;
				case 15: iColor = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); sThing = "nepturite chest";		break;
				case 16: iColor = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); sThing = "obsidian chest";			break;
				case 17: iColor = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ); sThing = "mithril chest";			break;
				case 18: iColor = MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ); sThing = "dwarven chest";			break;
			}

			sThing = sArty + sThing;

			///// DO THE CARVINGS ON THE BAG OR BOX ///////////////////////////////////////////////////////////
			string sLanguage = "pixie";
			switch( Utility.RandomMinMax( 0, 25 ) )
			{
				case 0: sLanguage = "balron"; break;
				case 1: sLanguage = "pixie"; break;
				case 2: sLanguage = "centaur"; break;
				case 3: sLanguage = "demonic"; break;
				case 4: sLanguage = "dragon"; break;
				case 5: sLanguage = "dwarvish"; break;
				case 6: sLanguage = "elven"; break;
				case 7: sLanguage = "fey"; break;
				case 8: sLanguage = "gargoyle"; break;
				case 9: sLanguage = "cyclops"; break;
				case 10: sLanguage = "gnoll"; break;
				case 11: sLanguage = "goblin"; break;
				case 12: sLanguage = "gremlin"; break;
				case 13: sLanguage = "druidic"; break;
				case 14: sLanguage = "tritun"; break;
				case 15: sLanguage = "minotaur"; break;
				case 16: sLanguage = "naga"; break;
				case 17: sLanguage = "ogrish"; break;
				case 18: sLanguage = "orkish"; break;
				case 19: sLanguage = "sphinx"; break;
				case 20: sLanguage = "treekin"; break;
				case 21: sLanguage = "trollish"; break;
				case 22: sLanguage = "undead"; break;
				case 23: sLanguage = "vampire"; break;
				case 24: sLanguage = "dark elf"; break;
				case 25: sLanguage = "magic"; break;
			}

			string sPart = "a strange";
			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0:	sPart = "a strange";	break;
				case 1:	sPart = "an odd";		break;
				case 2:	sPart = "an ancient";	break;
				case 3:	sPart = "a weird";		break;
				case 4:	sPart = "a cryptic";	break;
				case 5:	sPart = "a mystical";	break;
			}

			string sPart2 = "symbols";
			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0:	sPart2 = "symbol";	break;
				case 1:	sPart2 = "rune";	break;
				case 2:	sPart2 = "glyph";	break;
				case 3:	sPart2 = "mark";	break;
				case 4:	sPart2 = "sign";	break;
				case 5:	sPart2 = "letter";	break;
			}

			string RuneCarving = sPart + " " + sLanguage + " " + sPart2;

			if ( style > 0 )
			{
				surface = 0x22C1;
				RuneCarving = "transparent aluminum";
				iThing = 0x3865;
				sThing = "cargo chest";
				iColor = 0;
				iPed = 0x22BD;
				sPed = "transparent aluminum";
				hThing = 4;
			}

			AddComplexComponent( (BaseAddon) this, iThing, 0, 0, hThing, iColor, -1, sThing, 1);	// CHEST
			AddComplexComponent( (BaseAddon) this, 0x1647, 0, 0, 0, 0, 29, sPed, 1);				// LIGHT
			AddComplexComponent( (BaseAddon) this, iPed, 0, 0, 0, 0, -1, sPed, 1);					// PED
			AddComplexComponent( (BaseAddon) this, surface, 0, 0, 5, 0, -1, RuneCarving, 1);		// TOP OF PED
		}

		public SearchBase( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public override void OnComponentUsed( AddonComponent ac, Mobile from )
		{
			int monsters = 0;
			foreach ( Mobile monster in this.GetMobilesInRange( 5 ) )
			{
				if ( monster is BaseCreature )
				{
					Mobile leader = ((BaseCreature)monster).GetMaster();
					if ( leader is PlayerMobile ){}
					else { ++monsters; }
				}
			}

			if ( from.Blessed )
			{
				from.SendMessage( "You cannot open that while in this state." );
			}
			else if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.SendMessage( "You will have to get closer to open that." );
			}
			else if ( monsters > 0 )
			{
				from.SendMessage( "You cannot open this with too many creatures around." );
			}
			else if (	from.Backpack.FindItemByType( typeof ( SearchPage ) ) != null || 
						from.Backpack.FindItemByType( typeof ( DDRelicTablet ) ) != null || 
						from.Backpack.FindItemByType( typeof ( VortexCube ) ) != null || 
						from.Backpack.FindItemByType( typeof ( AlienEgg ) ) != null ||
                        from.Backpack.FindItemByType(typeof  ( FeyEgg )) != null ||
                        from.Backpack.FindItemByType(typeof  (EarthyEgg)) != null ||
                        from.Backpack.FindItemByType(typeof  (CorruptedEgg)) != null ||
                        from.Backpack.FindItemByType(typeof  (PrehistoricEgg)) != null ||
                        from.Backpack.FindItemByType(typeof  (ReptilianEgg)) != null ||
                        from.Backpack.FindItemByType( typeof ( ResearchBag ) ) != null || 
						from.Backpack.FindItemByType( typeof ( DragonEgg ) ) != null || 
						from.Backpack.FindItemByType( typeof ( DracolichSkull ) ) != null || 
						from.Backpack.FindItemByType( typeof ( MuseumBook ) ) != null || 
						from.Backpack.FindItemByType( typeof ( QuestTome ) ) != null || 
						from.Backpack.FindItemByType( typeof ( DemonPrison ) ) != null || 
						from.Backpack.FindItemByType( typeof ( CourierMail ) ) != null )
			{
				int EmptyBox = 1;

				if ( from.Backpack.FindItemByType( typeof ( MuseumBook ) ) != null )
				{
					if ( MuseumBook.FoundItem( from, 2 ) )
					{
						EmptyBox = 0;
					}
				}

				if ( from.Backpack.FindItemByType( typeof ( QuestTome ) ) != null )
				{
					if ( QuestTome.FoundItem( from, 2, null ) )
					{
						EmptyBox = 0;
					}
				}

				if ( from.Backpack.FindItemByType( typeof ( CourierMail ) ) != null )
				{
					Item mail = from.Backpack.FindItemByType( typeof ( CourierMail ) );
					CourierMail envelope = (CourierMail)mail;

					if ( envelope.SearchDungeon == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) && envelope.owner == from && envelope.DungeonMap == from.Map && envelope.MsgComplete == 0 )
					{
						envelope.MsgComplete = 1;
						from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the " + envelope.SearchItem + ".");
						from.SendSound( 0x3D );
						EmptyBox = 0;
					}
				}

				if ( from.Backpack.FindItemByType( typeof ( ResearchBag ) ) != null )
				{
					ResearchBag bag = (ResearchBag)from.Backpack.FindItemByType( typeof ( ResearchBag ) );

					if ( bag.BagOwner == from )
					{
						if ( Server.Misc.Research.SearchResult( from, bag ) ){ EmptyBox = 0; }
					}
				}

				if ( from.Backpack.FindItemByType( typeof ( VortexCube ) ) != null )
				{
					Item cubes = from.Backpack.FindItemByType( typeof ( VortexCube ) );
					VortexCube cube = (VortexCube)cubes;

					if ( cube.CubeOwner == from )
					{
						if ( cube.LocationKeyLaw == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) && cube.HasKeyLaw == 0 )
						{
							cube.HasKeyLaw = 1;
							from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Key of Law!");
							from.SendSound( 0x3D );
							LoggingFunctions.LogGeneric( from, "has found the Key of Law." );
							EmptyBox = 0;
						}
						if ( cube.LocationKeyBalance == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) && cube.HasKeyBalance == 0 )
						{
							cube.HasKeyBalance = 1;
							from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Key of Balance!");
							from.SendSound( 0x3D );
							LoggingFunctions.LogGeneric( from, "has found the Key of Balance." );
							EmptyBox = 0;
						}
						if ( cube.LocationKeyChaos == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) && cube.HasKeyChaos == 0 )
						{
							cube.HasKeyChaos = 1;
							from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Key of Chaos!");
							from.SendSound( 0x3D );
							LoggingFunctions.LogGeneric( from, "has found the Key of Chaos." );
							EmptyBox = 0;
						}

						int crystals = cube.HasCrystalRed + cube.HasCrystalBlue + cube.HasCrystalGreen + cube.HasCrystalYellow + cube.HasCrystalWhite + cube.HasCrystalPurple;

						if ( crystals < 6 && cube.LocationCrystal == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) )
						{
							if ( cube.HasCrystalRed == 0 )
							{
								cube.HasCrystalRed = 1;
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the red Void Crystal!");
								from.SendSound( 0x3D );
								LoggingFunctions.LogGeneric( from, "has found the red Void Crystal." );
								EmptyBox = 0;
							}
							else if ( cube.HasCrystalBlue == 0 )
							{
								cube.HasCrystalBlue = 1;
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the blue Void Crystal!");
								from.SendSound( 0x3D );
								LoggingFunctions.LogGeneric( from, "has found the blue Void Crystal." );
								EmptyBox = 0;
							}
							else if ( cube.HasCrystalBlue == 0 )
							{
								cube.HasCrystalBlue = 1;
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the blue Void Crystal!");
								from.SendSound( 0x3D );
								LoggingFunctions.LogGeneric( from, "has found the blue Void Crystal." );
								EmptyBox = 0;
							}
							else if ( cube.HasCrystalGreen == 0 )
							{
								cube.HasCrystalGreen = 1;
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the green Void Crystal!");
								from.SendSound( 0x3D );
								LoggingFunctions.LogGeneric( from, "has found the green Void Crystal." );
								EmptyBox = 0;
							}
							else if ( cube.HasCrystalYellow == 0 )
							{
								cube.HasCrystalYellow = 1;
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the yellow Void Crystal!");
								from.SendSound( 0x3D );
								LoggingFunctions.LogGeneric( from, "has found the yellow Void Crystal." );
								EmptyBox = 0;
							}
							else if ( cube.HasCrystalWhite == 0 )
							{
								cube.HasCrystalWhite = 1;
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the white Void Crystal!");
								from.SendSound( 0x3D );
								LoggingFunctions.LogGeneric( from, "has found the white Void Crystal." );
								EmptyBox = 0;
							}
							else if ( cube.HasCrystalPurple == 0 )
							{
								cube.HasCrystalPurple = 1;
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the purple Void Crystal!");
								from.SendSound( 0x3D );
								LoggingFunctions.LogGeneric( from, "has found the purple Void Crystal." );
								EmptyBox = 0;
							}

							cube.TextCrystal = Server.Items.CubeOnCorpse.GetRumor();
							cube.LocationCrystal = Server.Items.CubeOnCorpse.PickDungeon();

							int crystal = cube.HasCrystalRed + cube.HasCrystalBlue + cube.HasCrystalGreen + cube.HasCrystalYellow + cube.HasCrystalWhite + cube.HasCrystalPurple;

							if ( crystal > 5 )
							{
								cube.TextCrystal = "";
								cube.LocationCrystal = "";
							}
						}
					}
				}

				if ( from.Backpack.FindItemByType( typeof ( AlienEgg ) ) != null )
				{
					Item eggs = from.Backpack.FindItemByType( typeof ( AlienEgg ) );
					AlienEgg egg = (AlienEgg)eggs;

					if ( egg.PieceLocation == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) )
					{
						bool pickNewEggSpot = false;
						if ( egg.HaveRod < 1 ){ pickNewEggSpot = true; egg.HaveRod = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the rod of amber!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( egg.HaveYellowCrystal < 1 ){ pickNewEggSpot = true; egg.HaveYellowCrystal = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the sun crystal!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( egg.HaveRedCrystal < 1 ){ pickNewEggSpot = true; egg.HaveRedCrystal = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the blood crystal!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( egg.HavePotion < 1 ){ pickNewEggSpot = true; egg.HavePotion = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the potion of growth!"); from.SendSound( 0x3D ); EmptyBox = 0; }

						if ( pickNewEggSpot )
						{
							egg.PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
							egg.PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
						}
					}
				}

                if (from.Backpack.FindItemByType(typeof(FeyEgg)) != null)
                {
                    Item eggs = from.Backpack.FindItemByType(typeof(FeyEgg));
                    FeyEgg egg = (FeyEgg)eggs;

                    if (egg.PieceLocation == Server.Misc.Worlds.GetRegionName(from.Map, from.Location))
                    {
                        bool pickNewEggSpot = false;
                        if (egg.HaveOrb < 1) { pickNewEggSpot = true; egg.HaveOrb = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the The Orb of the Seelie Queen!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveFairyDust < 1) { pickNewEggSpot = true; egg.HaveFairyDust = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found The Fairy Dust!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveSeelieFetish < 1) { pickNewEggSpot = true; egg.HaveSeelieFetish = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found A Seelie Fetish!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveShimmeringLeaf < 1) { pickNewEggSpot = true; egg.HaveShimmeringLeaf = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found The Shimmering Leaf"); from.SendSound(0x3D); EmptyBox = 0; }

                        if (pickNewEggSpot)
                        {
                            egg.PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
                            egg.PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
                        }
                    }
                }
                if ( from.Backpack.FindItemByType( typeof ( DragonEgg ) ) != null )
				{
					Item eggs = from.Backpack.FindItemByType( typeof ( DragonEgg ) );
					DragonEgg egg = (DragonEgg)eggs;

					if ( egg.PieceLocation == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) )
					{
						bool pickNewEggSpot = false;
						if ( egg.HavePotionA < 1 ){ pickNewEggSpot = true; egg.HavePotionA = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the elixir of the flame!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( egg.HavePotionB < 1 ){ pickNewEggSpot = true; egg.HavePotionB = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the potion of the earth!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( egg.HavePotionC < 1 ){ pickNewEggSpot = true; egg.HavePotionC = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the mixture of the sea!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( egg.HavePotionD < 1 ){ pickNewEggSpot = true; egg.HavePotionD = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the oil of the winds!"); from.SendSound( 0x3D ); EmptyBox = 0; }

						if ( pickNewEggSpot )
						{
							egg.PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
							egg.PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
						}
					}
				}
                if (from.Backpack.FindItemByType(typeof(CorruptedEgg)) != null)
                {
                    Item eggs = from.Backpack.FindItemByType(typeof(CorruptedEgg));
                    CorruptedEgg egg = (CorruptedEgg)eggs;

                    if (egg.PieceLocation == Server.Misc.Worlds.GetRegionName(from.Map, from.Location))
                    {
                        bool pickNewEggSpot = false;
                        if (egg.HaveSkull < 1) { pickNewEggSpot = true; egg.HaveSkull = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Skull of Hades!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveRune < 1) { pickNewEggSpot = true; egg.HaveRune = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found An Unholy Rune!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveSeal < 1) { pickNewEggSpot = true; egg.HaveSeal = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Serpent Seal!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HavePotion < 1) { pickNewEggSpot = true; egg.HavePotion = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Potion of Gathered Souls!"); from.SendSound(0x3D); EmptyBox = 0; }

                        if (pickNewEggSpot)
                        {
                            egg.PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
                            egg.PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
                        }
                    }
                }
                if (from.Backpack.FindItemByType(typeof(EarthyEgg)) != null)
                {
                    Item eggs = from.Backpack.FindItemByType(typeof(EarthyEgg));
                    EarthyEgg egg = (EarthyEgg)eggs;

                    if (egg.PieceLocation == Server.Misc.Worlds.GetRegionName(from.Map, from.Location))
                    {
                        bool pickNewEggSpot = false;
                        if (egg.HaveChisel < 1) { pickNewEggSpot = true; egg.HaveChisel = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Dwarven Chisel!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HavePowder < 1) { pickNewEggSpot = true; egg.HavePowder = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "Combustion Powder!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveRune < 1) { pickNewEggSpot = true; egg.HaveRune = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Earthen Rune!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HavePotion < 1) { pickNewEggSpot = true; egg.HavePotion = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Potion of Lava!"); from.SendSound(0x3D); EmptyBox = 0; }

                        if (pickNewEggSpot)
                        {
                            egg.PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
                            egg.PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
                        }
                    }
                }
                if (from.Backpack.FindItemByType(typeof(PrehistoricEgg)) != null)
                {
                    Item eggs = from.Backpack.FindItemByType(typeof(PrehistoricEgg));
                    PrehistoricEgg egg = (PrehistoricEgg)eggs;

                    if (egg.PieceLocation == Server.Misc.Worlds.GetRegionName(from.Map, from.Location))
                    {
                        bool pickNewEggSpot = false;
                        if (egg.HaveHammer < 1) { pickNewEggSpot = true; egg.HaveHammer = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Prehistoric Hammer!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveVine < 1) { pickNewEggSpot = true; egg.HaveVine = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "A Strangling Vine!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveFang < 1) { pickNewEggSpot = true; egg.HaveFang = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Raptor's Fang!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HavePotion < 1) { pickNewEggSpot = true; egg.HavePotion = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Potion of Primordial Ooze!"); from.SendSound(0x3D); EmptyBox = 0; }

                        if (pickNewEggSpot)
                        {
                            egg.PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
                            egg.PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
                        }
                    }
                }
                if (from.Backpack.FindItemByType(typeof(ReptilianEgg)) != null)
                {
                    Item eggs = from.Backpack.FindItemByType(typeof(ReptilianEgg));
                    ReptilianEgg egg = (ReptilianEgg)eggs;

                    if (egg.PieceLocation == Server.Misc.Worlds.GetRegionName(from.Map, from.Location))
                    {
                        bool pickNewEggSpot = false;
                        if (egg.HaveHeart < 1) { pickNewEggSpot = true; egg.HaveHeart = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Heart of Fire!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveRune < 1) { pickNewEggSpot = true; egg.HaveRune = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "A Combustion Rune!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HaveEmber < 1) { pickNewEggSpot = true; egg.HaveEmber = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You an Eternal Ember!"); from.SendSound(0x3D); EmptyBox = 0; }
                        else if (egg.HavePotion < 1) { pickNewEggSpot = true; egg.HavePotion = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the Potion of Flame!"); from.SendSound(0x3D); EmptyBox = 0; }

                        if (pickNewEggSpot)
                        {
                            egg.PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
                            egg.PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
                        }
                    }
                }

                if ( from.Backpack.FindItemByType( typeof ( DracolichSkull ) ) != null )
				{
					Item skulls = from.Backpack.FindItemByType( typeof ( DracolichSkull ) );
					DracolichSkull skull = (DracolichSkull)skulls;

					if ( skull.PieceLocation == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) )
					{
						bool pickNewSpot = false;
						if ( skull.HavePotionA < 1 ){ pickNewSpot = true; skull.HavePotionA = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the heart of the dead god!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( skull.HavePotionB < 1 ){ pickNewSpot = true; skull.HavePotionB = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the eye of the mad king!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( skull.HavePotionC < 1 ){ pickNewSpot = true; skull.HavePotionC = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the orb of the astral lich!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( skull.HavePotionD < 1 ){ pickNewSpot = true; skull.HavePotionD = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the mind of the planar ghost!"); from.SendSound( 0x3D ); EmptyBox = 0; }

						if ( pickNewSpot )
						{
							skull.PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
							skull.PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
						}
					}
				}

				if ( from.Backpack.FindItemByType( typeof ( DemonPrison ) ) != null )
				{
					Item prisons = from.Backpack.FindItemByType( typeof ( DemonPrison ) );
					DemonPrison prison = (DemonPrison)prisons;

					if ( prison.PieceLocation == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) )
					{
						bool pickNewprisonSpot = false;
						if ( prison.HaveShardA < 1 ){ pickNewprisonSpot = true; prison.HaveShardA = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the shard of hellfire!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( prison.HaveShardB < 1 ){ pickNewprisonSpot = true; prison.HaveShardB = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the shard of the abyss!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( prison.HaveShardC < 1 ){ pickNewprisonSpot = true; prison.HaveShardC = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the shard of souls!"); from.SendSound( 0x3D ); EmptyBox = 0; }
						else if ( prison.HaveShardD < 1 ){ pickNewprisonSpot = true; prison.HaveShardD = 1; from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the shard of the void!"); from.SendSound( 0x3D ); EmptyBox = 0; }

						if ( pickNewprisonSpot )
						{
							prison.PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
							prison.PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
						}
					}
				}

				if ( from.Backpack.FindItemByType( typeof ( DDRelicTablet ) ) != null )
				{
					Container pack = from.Backpack;

					List<DDRelicTablet> rock = pack.FindItemsByType<DDRelicTablet>();

					for ( int i = 0; i < rock.Count; ++i )
					{
						DDRelicTablet stone = rock[i];

						if ( stone.SearchDungeon == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) )
						{
							if ( stone.SearchReal >= Utility.RandomMinMax( 1, 100 ) )
							{
								Item item = null;
								string itemName = stone.SearchType;
								Type itemType = ScriptCompiler.FindTypeByName( itemName );

								if ( itemType != null )
								{
									item = (Item)Activator.CreateInstance(itemType);
									from.AddToBackpack ( item );
									LoggingFunctions.LogFoundItemQuest( from, stone.SearchItem );
									from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the " + stone.SearchItem + ".");
									from.SendSound( 0x3D );
								}
							}
							else if ( 1 == Utility.RandomMinMax( 1, 2 ) )
							{
								Item item = null;
								string itemName = stone.SearchType;
								Type itemType = ScriptCompiler.FindTypeByName( itemName );

								if ( itemType != null )
								{
									item = (Item)Activator.CreateInstance(itemType);
									Item fake = new BrokenGear();
									fake.ItemID = item.ItemID;
									fake.Hue = item.Hue;
									fake.Weight = item.Weight;
									fake.Name = "Fake " + stone.SearchItem;
									item.Delete();
									from.AddToBackpack ( fake );
								}
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "The " + stone.SearchItem + " appears to be a fake.");
								from.SendSound( 0x5B3 );
							}
							else
							{
								from.SendMessage( "" );
								from.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "The tablet for the " + stone.SearchItem + " seems to be false.");
								from.PlaySound( 0x5B3 );
							}

							from.SendMessage( "The tablet crumbles to dust!" );
							stone.Delete();
							EmptyBox = 0;
						}
					}
				}

				if ( from.Backpack.FindItemByType( typeof ( SearchPage ) ) != null )
				{
					Item scroll = from.Backpack.FindItemByType( typeof ( SearchPage ) );
					SearchPage page = (SearchPage)scroll;

					int LeadToAnotherSpot = 100 - page.LegendPercent;

					if ( page.SearchDungeon == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) && page.owner == from && page.DungeonMap == from.Map )
					{
						if ( LeadToAnotherSpot >= Utility.RandomMinMax( 1, 100 ) )
						{
							from.PlaySound(0x249);
							SearchPage.PickSearchLocation( page, page.SearchDungeon, from );
							from.SendMessage( "You didn't find it, but you did get another clue." );
							from.SendMessage( "so you update your notes for the new place to search." );
							EmptyBox = 0;
						}
						else
						{
							if ( page.LegendReal == 1 )
							{
								Item item = null;
								string itemName = page.SearchType;
								Type itemType = ScriptCompiler.FindTypeByName( itemName );

								if ( itemType != null )
								{
									item = (Item)Activator.CreateInstance(itemType);
									from.AddToBackpack ( item );
									LoggingFunctions.LogFoundItemQuest( from, page.SearchItem );
									from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You found the " + page.SearchItem + ".");
									from.SendSound( 0x3D );
								}
							}
							else if ( page.LegendPercent >= Utility.RandomMinMax( 1, 200 ) )
							{
								int nGold = page.LegendPercent * 100;
								string sGold = nGold.ToString();
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "The legend was false, but there was " + sGold + " gold in here.");
								from.SendSound( 0x2E6 );
								from.AddToBackpack ( new Gold( nGold ) );
							}
							else if ( 1 == Utility.RandomMinMax( 1, 2 ) )
							{
								Item item = null;
								string itemName = page.SearchType;
								Type itemType = ScriptCompiler.FindTypeByName( itemName );

								if ( itemType != null )
								{
									item = (Item)Activator.CreateInstance(itemType);
									Item fake = new BrokenGear();
									fake.ItemID = item.ItemID;
									fake.Hue = item.Hue;
									fake.Weight = item.Weight;
									fake.Name = "Fake " + page.SearchItem;
									item.Delete();
									from.AddToBackpack ( fake );
								}
								from.LocalOverheadMessage(MessageType.Emote, 1150, true, "The " + page.SearchItem + " appears to be a fake.");
								from.SendSound( 0x3D );
							}
							else
							{
								from.SendMessage( "" );
								from.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "The legends of the " + page.SearchItem + " seemed to be false.");
								from.PlaySound( 0x249 );
							}
							scroll.Delete();
							SearchPage.ArtifactQuestTimeAllowed( from );
							EmptyBox = 0;
						}
					}
				}

				if ( EmptyBox == 1 )
				{
					from.SendMessage( "The chest appears to be empty." );
				}
			}
			else
			{
				from.SendMessage( "The chest appears to be empty." );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}