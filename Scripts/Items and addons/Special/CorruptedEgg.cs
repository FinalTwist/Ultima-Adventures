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
	public class CorruptedEgg : Item
	{
		[Constructable]
		public CorruptedEgg() : base(0x278C)
		{
			Weight = 4.0;
			Name = "A Corrupted Egg";
            

            if ( Weight > 3.0 )
			{
				ItemID = Utility.RandomList( 0x278C);
				Weight = 3.0;

				HaveSkull = 0;
				HaveRune = 0;
				HaveSeal = 0;
				HavePotion = 0;
				HaveGold = 0;

				Hue = 0xB73;

				NeedGold = 90000;
                

                AnimalTrainerLocation = Server.Items.CorruptedEgg.GetRandomVet();

				PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
				PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
			}
		}
        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add(1070722, "Take to the Void Orb Shrine in Darkmoor");
        }
        public override void OnDoubleClick(Mobile from)
        {
            if (Weight > 2.0 && from.Map == Map.Ilshenar && from.X >= 723 && from.Y >= 55 && from.X <= 741 && from.Y <= 67)
            {
				Weight = 1.0;
			}

			if ( Weight < 1.5 )
			{
				from.CloseGump( typeof( CorruptedEggGump ) );
				from.SendGump( new CorruptedEggGump( from, this ) );
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
					from.SendMessage( "You added " + iAmount.ToString() + " Gold coin" + sEnd );
					dropped.Delete();
					return true;
				}
			}

			return false;
		}

		public CorruptedEgg( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version

			writer.Write( HaveSkull );
			writer.Write( HaveRune );
			writer.Write( HaveSeal );
			writer.Write( HavePotion );
			writer.Write( HaveGold );
			writer.Write( NeedGold );
			writer.Write( AnimalTrainerLocation );
			writer.Write( PieceLocation );
			writer.Write( PieceRumor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			HaveSkull = reader.ReadInt();
			HaveRune = reader.ReadInt();
			HaveSeal = reader.ReadInt();
			HavePotion = reader.ReadInt();
			HaveGold = reader.ReadInt();
			NeedGold = reader.ReadInt();
			AnimalTrainerLocation = reader.ReadString();
			PieceLocation = reader.ReadString();
			PieceRumor = reader.ReadString();
		}

        public static bool ProcessCorruptedEgg(Mobile m, Mobile vet, Item dropped)
        {
            CorruptedEgg egg = (CorruptedEgg)dropped;

            if (Server.Misc.Worlds.GetRegionName(vet.Map, vet.Location) != egg.AnimalTrainerLocation) { return false; }

            int vetSkill = (int)(m.Skills[SkillName.Veterinary].Value);
            if (vetSkill > 100) { vetSkill = 100; }

			if (m.Skills[SkillName.AnimalTaming].Value < 80) 
			{ vet.Say("Looks like you don't have enough skill to hatch this egg."); return false;}

            int GoldReturn = 0;
            if (vetSkill > 0) { GoldReturn = (int)(egg.NeedGold * (vetSkill * 0.005)); }

            int HaveIngredients = 0;

            if (egg.HaveRune >= 0) { HaveIngredients++; }
            if (egg.HaveSeal >= 0) { HaveIngredients++; }
            if (egg.HavePotion >= 0) { HaveIngredients++; }
            if (egg.HaveGold >= egg.NeedGold) { HaveIngredients++; }
            if (egg.HaveSkull >= 0) { HaveIngredients++; }

            if (HaveIngredients < 5) { return false; }

            if ((m.Followers + 3) > m.FollowersMax)
            {
                vet.Say("You have too many followers with you to hatch this egg.");
                return false;
            }

            if (GoldReturn > 0) { m.AddToBackpack(new Gold(GoldReturn)); vet.Say("Here is " + GoldReturn.ToString() + " Gold back for all of your help."); }

            Type FeyMonR = null;
            
            double Dice = Utility.RandomDouble();
            {
                if (Dice <= 0.50)
                    FeyMonR = typeof (SoulWorm);
                else if
                 (Dice <= 0.60)
                    FeyMonR = typeof (SkeletonHorse);
                else if
                     (Dice <= 0.70)
                    FeyMonR = typeof (HellSteed);
                else if
                    (Dice <= 0.90)
                    FeyMonR = typeof (VampiricDragon);
                else if
                    (Dice <= 0.95)
                    FeyMonR = typeof(AbysmalDrake);
                else if
                 (Dice <= 1.0)
                    FeyMonR = typeof (Dracolich);

            }
            BaseCreature FeyMon = Activator.CreateInstance(FeyMonR) as BaseCreature;
			FeyMon.Tamable = true;
			FeyMon.SetControlMaster( m );
            FeyMon.IsBonded = true;
            FeyMon.MoveToWorld( m.Location, m.Map );
			FeyMon.OnAfterSpawn();

						if (FeyMon.ControlSlots >1)
				FeyMon.ControlSlots -= 1;
				
            FeyMon.ControlTarget = m;
            
            FeyMon.MinTameSkill = FeyMon.MinTameSkill / 1.5;
            FeyMon.ControlOrder = OrderType.Follow;
            

            LoggingFunctions.LogGenericQuest( m, "has hatched a Corrupted Egg" );

			m.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Your Corrupted Egg has hatched.", m.NetState);
			m.PlaySound( 0x041 );

			dropped.Delete();

			return true;
		}

		public class CorruptedEggGump : Gump
		{
			public CorruptedEggGump( Mobile from, CorruptedEgg egg ): base( 25, 25 )
			{
				string sText = "This egg contains the corrupted embryo of an unholy creature! Liches once used a dangerous ritual to animate Corrupted Eggs using ancient magics, because Corrupted eggs are warded by evil magics they are unlikely to hatch naturally. Since you are not a Lich and do not know their mystical ways, there are some other ways you think this egg can hatched. Hearing many rumors at the tavern, there is a Skull of Hades that can be powered by magical items. The darkness given from these assembled artifact should be able to hatch the egg. A corrupted creature would surely emerge, however, it would not be a worthy creature to help on your journey. Instead, you can try to find a very rare Potion of Gathered Souls. This should mature the hatchling into a full unholy creature. Along with these things, you will also need some gold as you will need the help of a particular animal expert and they will require payment for their services. This animal expert is at the location shown on this screen. If you have any veterinary skill, they may refund some of the gold for the help you may provide in the birth. When hatched and fully grown, these creatures will become your bonded pet. You will have to feed it and stable it when required. You can also perform some animal lore on it without having any proficiency in the skill. This will help you with information about them, like what they want to eat.";

				string sRumor = egg.PieceRumor + " " + egg.PieceLocation;

				if ( egg.HaveSkull == 0 ){ sRumor = "The Skull of Hades " + sRumor; }
				else if ( egg.HaveRune == 0 ){ sRumor = "An Unholy Rune " + sRumor; }
				else if ( egg.HaveSeal == 0 ){ sRumor = "An Serpent's Seal " + sRumor; }
				else if ( egg.HavePotion == 0 ){ sRumor = "The Potion of Gathered Souls " + sRumor; }
				else if ( egg.HaveGold < egg.NeedGold ){ sRumor = "You have obtained everything except the Gold."; }
				else { sRumor = "You have obtained everything you need."; }

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 30521);
				AddItem(574, 32, 20305); //last value controls graphic - top right flavor image

				AddHtml( 50, 38, 207, 20, @"<BODY><BASEFONT Color=#00FF06>Corrupted Egg</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(376, 36, 3823); //last value controls gold coins graphic add ,0xxxx to control hue
				AddHtml( 420, 38, 180, 20, @"<BODY><BASEFONT Color=#00FF06>" + egg.HaveGold.ToString() + "/" + egg.NeedGold.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 50, 70, 520, 60, @"<BODY><BASEFONT Color=#00FF06>" + sRumor + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(41, 237, 3000);
				AddHtml( 85, 242, 622, 20, @"<BODY><BASEFONT Color=#00FF06>Bring Gathered Materials to the Animal Expert in " + egg.AnimalTrainerLocation + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(85, 145, 8893);
				if ( egg.HaveSkull > 0 ){ AddItem(84, 156, 587); }

				AddItem(235, 145, 8893);
				if ( egg.HaveRune > 0 ){ AddItem(236, 144, 21266); }

				AddItem(385, 145, 8893);
				if ( egg.HaveSeal > 0 ){ AddItem(387, 143, 5396); }

				AddItem(535, 145, 8893);
				if ( egg.HavePotion > 0 ){ AddItem(546, 145, 6896); }

				AddHtml( 50, 289, 665, 319, @"<BODY><BASEFONT Color=#00FF06>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public static string GetRandomVet()
		{
			int aCount = 0;
			Region reg = null;
			string sRegion = "";

			ArrayList targets = new ArrayList();
			foreach ( Mobile target in World.Mobiles.Values )
			if ( target is BaseVendor )
			{
				reg = Region.Find( target.Location, target.Map );
				string tWorld = Worlds.GetMyWorld( target.Map, target.Location, target.X, target.Y );

				if (	tWorld == "the Land of Sosaria" || 
						tWorld == "the Land of Lodoria" || 
						tWorld == "the Serpent Island" || 
						tWorld == "the Isles of Dread" || 
						tWorld == "the Savaged Empire" || 
						tWorld == "the Island of Umber Veil" || 
						tWorld == "the Bottle World of Kuldar" )
				{
					if ( ( target is AnimalTrainer || target is Veterinarian ) && reg.IsPartOf( typeof( VillageRegion ) ))
					{
						targets.Add( target ); aCount++;
					}
				}
			}

			aCount = Utility.RandomMinMax( 1, aCount );

			int xCount = 0;
			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile vet = ( Mobile )targets[ i ];
				xCount++;

				if ( xCount == aCount )
				{
					sRegion = Server.Misc.Worlds.GetRegionName( vet.Map, vet.Location );
				}
			}

			return sRegion;
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

		// ----------------------------------------------------------------------------------------

		public int NeedGold;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_NeedGold { get{ return NeedGold; } set{ NeedGold = value; } }

		// ----------------------------------------------------------------------------------------

		public int HaveSkull;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveSkull { get{ return HaveSkull; } set{ HaveSkull = value; } }

		public int HaveGold;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveGold { get{ return HaveGold; } set{ HaveGold = value; } }

		public int HaveRune;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveRune { get{ return HaveRune; } set{ HaveRune = value; } }

		public int HaveSeal;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveSeal { get{ return HaveSeal; } set{ HaveSeal = value; } }

		public int HavePotion;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HavePotion { get{ return HavePotion; } set{ HavePotion = value; } }
	}
}