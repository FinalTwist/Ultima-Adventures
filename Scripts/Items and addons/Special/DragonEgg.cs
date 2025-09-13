using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;

namespace Server.Items
{
	public class DragonEgg : Item
	{
		[Constructable]
		public DragonEgg() : base( 0x278C )
		{
			Weight = 4.0;
			Name = "Dragon Egg";
			Light = LightType.Circle225;

			if ( Weight > 3.0 )
			{
				Weight = 3.0;

				HavePotionA = 0;
				HavePotionB = 0;
				HavePotionC = 0;
				HavePotionD = 0;
				HaveGold = 0;

				AnimalTrainerLocation = Server.Items.AlienEgg.GetRandomVet();

				PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
				PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Weight > 2.0 && from.Map == Map.Felucca && from.X >= 5296 && from.Y >= 664 && from.X <= 5318 && from.Y <= 686 )
			{
				Weight = 1.0;
			}

			if ( Weight < 1.5 )
			{
				from.CloseGump( typeof( DragonEggGump ) );
				from.SendGump( new DragonEggGump( from, this ) );
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
			int iAmount = 0;
			string sEnd = ".";

			if ( from != null && Weight < 1.5 )
			{
				if ( dropped is Gold && NeedGold > HaveGold )
				{
					int WhatIsDropped = dropped.Amount;
					int WhatIsNeeded = NeedGold - HaveGold;
					int WhatIsExtra = WhatIsDropped - WhatIsNeeded; if ( WhatIsExtra < 1 ){ WhatIsExtra = 0; }
					int WhatIsTaken = WhatIsDropped - WhatIsExtra;

					if ( WhatIsExtra > 0 ){ from.AddToBackpack( new Gold( WhatIsExtra ) ); }
					iAmount = WhatIsTaken;

					if ( iAmount > 1 ){ sEnd = "s."; }

					HaveGold = HaveGold + iAmount;
					from.SendMessage( "You added " + iAmount.ToString() + " gold coin" + sEnd );
					dropped.Delete();
					return true;
				}
			}

			return false;
		}

		public DragonEgg( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version

			writer.Write( HavePotionA );
			writer.Write( HavePotionB );
			writer.Write( HavePotionC );
			writer.Write( HavePotionD );
			writer.Write( HaveGold );
			writer.Write( NeedGold );
			writer.Write( AnimalTrainerLocation );
			writer.Write( PieceLocation );
			writer.Write( PieceRumor );
			writer.Write( DragonType );
			writer.Write( DragonBody );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			HavePotionA = reader.ReadInt();
			HavePotionB = reader.ReadInt();
			HavePotionC = reader.ReadInt();
			HavePotionD = reader.ReadInt();
			HaveGold = reader.ReadInt();
			NeedGold = reader.ReadInt();
			AnimalTrainerLocation = reader.ReadString();
			PieceLocation = reader.ReadString();
			PieceRumor = reader.ReadString();
			DragonType = reader.ReadInt();
			DragonBody = reader.ReadInt();
		}

		public static bool ProcessDragonEgg( Mobile m, Mobile vet, Item dropped )
		{
			DragonEgg egg = (DragonEgg)dropped;

			if ( Server.Misc.Worlds.GetRegionName( vet.Map, vet.Location ) != egg.AnimalTrainerLocation ){ return false; }

			int vetSkill = (int)(m.Skills[SkillName.Veterinary].Value);
				if ( vetSkill > 100 ){ vetSkill = 100; }

				if (m.Skills[SkillName.AnimalTaming].Value < 80) 
			{ vet.Say("Looks like you don't have enough skill to hatch this egg."); return false;}

			int GoldReturn = 0;
				if ( vetSkill > 0 ){ GoldReturn = (int)( egg.NeedGold * ( vetSkill * 0.005 ) ); }

			int HaveIngredients = 0;

			if ( egg.HavePotionB >= 0 ){ HaveIngredients++; }
			if ( egg.HavePotionC >= 0 ){ HaveIngredients++; }
			if ( egg.HavePotionD >= 0 ){ HaveIngredients++; }
			if ( egg.HaveGold >= egg.NeedGold ){ HaveIngredients++; }
			if ( egg.HavePotionA >= 0 ){ HaveIngredients++; }

			if ( HaveIngredients < 5 ){ return false; }

			int followers = 3;
			if ( (dropped.Name).Contains(" dragon") ){ followers = 2; }

			if ( (m.Followers + followers) > m.FollowersMax )
			{
				vet.Say( "You have too many followers with you to hatch this egg." );
				return false;
			}

			if ( GoldReturn > 0 ){ m.AddToBackpack( new Gold( GoldReturn ) ); vet.Say( "Here is " + GoldReturn.ToString() + " gold back for all of your help." ); }

			BaseCreature dragon = new Dragons( egg.DragonBody, egg.DragonType );
			dragon.Tamable = true;
			dragon.SetControlMaster( m );
			dragon.IsBonded = true;
			dragon.MoveToWorld( m.Location, m.Map );
			dragon.OnAfterSpawn();

						if (dragon.ControlSlots >1)
				dragon.ControlSlots -= 1;
			dragon.ControlTarget = m;
			
			dragon.MinTameSkill = 29.1;
			dragon.ControlOrder = OrderType.Follow;

			string style = "dragon";
			if ( followers == 3 ){ style = "wyrm"; dragon.Name = (dragon.Name).Replace(" dragon", " wyrm"); }

			LoggingFunctions.LogGenericQuest( m, "has hatched a " + style + "" );
			m.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Your " + style + " has hatched.", m.NetState);

			m.PlaySound( 0x041 );

			dropped.Delete();

			return true;
		}

		public class DragonEggGump : Gump
		{
			public DragonEggGump( Mobile from, DragonEgg egg ): base( 25, 25 )
			{
				string sDragon = "dragon";
					if ( egg.DragonBody == 59 ){ sDragon = "wyrm"; }

				string sText = "This egg contains the embryo of a " + sDragon + ". Dwarves would take these eggs and brew the potions of the four elements to pour over the shell. The elixir of the flame, the potion of the earth, the mixture of the sea, and the oil of the winds are the four alchemical potions used in this process. Once these liquids are poured onto the shell, it could be broken by the young " + sDragon + " and the power of all the elements combined would mature the " + sDragon + " to almost be fully grown. These alchemical skills died off with the dwarven race, but you did hear rumors of these potions being seen in various places. If you could get them, and bring the egg to an animal expert, they may be able to help you hatch it. The animal expert will require some gold (placed onto the egg) as you will need the help of a particular animal expert and they will require payment for their services. This animal expert is at the location shown below. If you have any veterinary skill, they may refund some of the gold for the help you may provide in the birth. When hatched and almost fully grown, these " + sDragon + "s will become your bonded pet. You will have to feed it and stable it when required. You can also perform some animal lore on it without having any proficiency in the skill. This will help you with information about them, like what they want to eat.";

				string sRumor = egg.PieceRumor + " " + egg.PieceLocation;

				if ( egg.HavePotionA == 0 ){ sRumor = "The elixir of the flame " + sRumor; }
				else if ( egg.HavePotionB == 0 ){ sRumor = "The potion of the earth " + sRumor; }
				else if ( egg.HavePotionC == 0 ){ sRumor = "The mixture of the sea " + sRumor; }
				else if ( egg.HavePotionD == 0 ){ sRumor = "The oil of the winds " + sRumor; }
				else if ( egg.HaveGold < egg.NeedGold ){ sRumor = "You have obtained everything except the gold."; }
				else { sRumor = "You have obtained everything you need."; }

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 154);
				AddImage(300, 0, 154);
				AddImage(600, 0, 154);
				AddImage(300, 300, 154);
				AddImage(600, 300, 154);
				AddImage(0, 300, 154);
				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(300, 298, 129);
				AddImage(598, 298, 129);
				AddImage(7, 9, 150);
				AddImage(696, 9, 146);
				AddImage(711, 21, 156);
				AddImage(410, 37, 132);
				AddImage(156, 23, 156);
				AddImage(180, 25, 156);
				AddImage(195, 37, 132);
				AddImage(697, 34, 143);
				AddImage(175, 34, 159);

				AddItem(708, 130, 11665, egg.Hue);

				AddItem(191, 59, 10124, egg.Hue);
				AddHtml( 240, 66, 454, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + (egg.Name).ToUpper() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(77, 127, 3823);
				AddHtml( 122, 130, 362, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + egg.HaveGold.ToString() + "/" + egg.NeedGold.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 87, 171, 593, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + sRumor + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(93, 210, 13042);
				AddItem(273, 210, 13042);
				AddItem(453, 210, 13042);
				AddItem(633, 210, 13042);

				if ( egg.HavePotionA > 0 ){ AddItem(105, 210, 10279, 0xB54); }
				if ( egg.HavePotionB > 0 ){ AddItem(285, 210, 10279, 0xB27); }
				if ( egg.HavePotionC > 0 ){ AddItem(465, 210, 10279, 0xB46); }
				if ( egg.HavePotionD > 0 ){ AddItem(645, 210, 10279, 0xB49); }

				AddHtml( 50, 303, 796, 235, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(3, 553, 3000);
				AddHtml( 47, 559, 624, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Bring Gathered Materials to the Animal Expert in " + egg.AnimalTrainerLocation + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public string AnimalTrainerLocation;
		[CommandProperty( AccessLevel.GameMaster )]
		public string g_AnimalTrainerLocation { get{ return AnimalTrainerLocation; } set{ AnimalTrainerLocation = value; } }

		public string PieceLocation;
		[CommandProperty( AccessLevel.GameMaster )]
		public string g_PieceLocation { get{ return PieceLocation; } set{ PieceLocation = value; } }

		public string PieceRumor;
		[CommandProperty( AccessLevel.GameMaster )]
		public string g_PieceRumor { get{ return PieceRumor; } set{ PieceRumor = value; } }

		public int DragonType;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_DragonType { get{ return DragonType; } set{ DragonType = value; } }

		public int DragonBody;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_DragonBody { get{ return DragonBody; } set{ DragonBody = value; } }

		// ----------------------------------------------------------------------------------------

		public int NeedGold;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_NeedGold { get{ return NeedGold; } set{ NeedGold = value; } }

		// ----------------------------------------------------------------------------------------

		public int HavePotionA;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HavePotionA { get{ return HavePotionA; } set{ HavePotionA = value; } }

		public int HaveGold;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveGold { get{ return HaveGold; } set{ HaveGold = value; } }

		public int HavePotionC;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HavePotionC { get{ return HavePotionC; } set{ HavePotionC = value; } }

		public int HavePotionB;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HavePotionB { get{ return HavePotionB; } set{ HavePotionB = value; } }

		public int HavePotionD;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HavePotionD { get{ return HavePotionD; } set{ HavePotionD = value; } }
	}
}