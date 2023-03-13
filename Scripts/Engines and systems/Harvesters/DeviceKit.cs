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
	public class DeviceKit : Item
	{

		public int HaveWood;
		[CommandProperty( AccessLevel.GameMaster )]
		private int g_HaveWood { get{ return HaveWood; } set{ HaveWood = value; } }

		public int HaveMetals;
		[CommandProperty( AccessLevel.GameMaster )]
		private int g_HaveMetals { get{ return HaveMetals; } set{ HaveMetals = value; } }

		public int HaveLeather;
		[CommandProperty( AccessLevel.GameMaster )]
		private int g_HaveLeather { get{ return HaveLeather; } set{ HaveLeather = value; } }

		public int HaveGears;
		[CommandProperty( AccessLevel.GameMaster )]
		private int g_HaveGears { get{ return HaveGears; } set{ HaveGears = value; } }

		public int HaveMats;

		public int Points;

		private bool itemone;
		private bool itemtwo;
		private bool itemthree;
		private bool itemfour;
		private bool itemfive;
		private bool itemsix;
		private bool itemseven;
		private bool food;
		private bool drink;

		private int typee;

		[Constructable]
		public DeviceKit() : base( 0x4F86 ) // 0x4BD1 
		{

			Name = "Build your own harvester kit(TM)";
			//ItemID = Utility.RandomList( 0x14F1, 0x14F2 );
			g_HaveWood = 0;
			g_HaveLeather = 0;
			g_HaveMetals = 0;
			g_HaveGears = 0;
			HaveMats = 0;
			Points = 0;
			typee = 0;

			itemone = Utility.RandomBool(); // AxelGears or ClockParts
			itemtwo = Utility.RandomBool();
			itemthree = Utility.RandomBool();
			itemfour = Utility.RandomBool();
			itemfive = Utility.RandomBool();
			itemsix = Utility.RandomBool();
			itemseven = Utility.RandomBool();
			food = Utility.RandomBool();
			drink = Utility.RandomBool();

			Weight = 1 + (HaveMats /10) + (HaveGears /10);
			
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			int needMats = 1000 - HaveMats;
				if ( needMats < 0 ){ needMats = 0; }
			int needGears = 500 - HaveGears;
				if ( needGears < 0 ){ needGears = 0; }

			int charged = needMats + needGears;

			if ( charged != 0 )
			{
				list.Add( 1070722, "Drop 1000 boards/ingots/cut leather and 500 gears in this kit to charge it." );
				list.Add( 1070722, "Deposit ingots for a mining harvester, boards for a lumber harvester, cut leather for a hides harvester." );
				list.Add( 1070722, "The quality of the materials deposited will determine the quality of the device." );
				list.Add( 1049644, "Need " + needMats.ToString() + " Materials, and " + needGears.ToString() + " Gears. ");
			}
			else 
			{
				list.Add( 1070722, "Device Charged! Double Click To Build." );
			}
        }

		public override void OnDoubleClick( Mobile from )
		{

			if (from == null || !(from is PlayerMobile) || from.Backpack == null)
				return;

			int needMats = 1000 - HaveMats;
			int needGears = 500 - HaveGears;
			int charged = needMats + needGears;

			if ( charged != 0 )
			{
				from.SendMessage( "The device isn't charged yet and needs more material." );
			}
			else
			{
				if (DoYouHaveEverything( from ) )
				{
					if (g_HaveWood > g_HaveMetals && g_HaveWood > g_HaveLeather) // for a lumber harvester
						typee = 1; // for lumber
					else if (g_HaveMetals > g_HaveWood && g_HaveMetals > g_HaveLeather)
						typee = 2; // metal
					else if (g_HaveLeather > g_HaveMetals && g_HaveLeather > g_HaveWood)
						typee = 3; // leather
					else
						typee = Utility.RandomMinMax(1,3);

					int quality = 0;

					double rando = Utility.RandomDouble() * 10000;
					if (Utility.RandomBool())
						Points += (int)rando; // randomize a bit
					else
						Points -= (int)rando; // randomize a bit

					if (Points >= 65000)
						quality = 3;
					else if (Points >= 25000)
						quality = 2;
					else if (Points < 25000)
						quality = 1;	

					Item harvy = null;

					if (typee ==1)
					{
						if (quality == 1)
							harvy = new PoorLumberHarvester();
						else if (quality == 2)
							harvy = new StandardLumberHarvester();
						else if (quality == 3)
							harvy = new PerfectLumberHarvester();
					}
					else if (typee == 2)
					{
						if (quality == 1)
							harvy = new PoorMiningHarvester();
						else if (quality == 2)
							harvy = new StandardMiningHarvester();
						else if (quality == 3)
							harvy = new PerfectMiningHarvester();
					}
					else if (typee == 3)
					{
						if (quality == 1)
							harvy = new PoorHideHarvester();
						else if (quality == 2)
							harvy = new StandardHideHarvester();
						else if (quality == 3)
							harvy = new PerfectHideHarvester();
					}

					if (harvy != null)
					{
						from.SendMessage( "You build yourself a harvester." );
						from.PlaySound( 0x23D );
						from.AddToBackpack ( harvy );
						this.Delete();
					}
				}
			}
		}



		public override bool OnDragDrop( Mobile from, Item dropped )
		{          	
			if (dropped == null)
				return false;

			Container pack = from.Backpack;
			int amount = dropped.Amount;
			string sEnd = ".";

			int needMats = 1000 - (HaveWood + HaveMetals + HaveLeather);
			int needGears = 500 - HaveGears;

			if ( from != null && from.Backpack != null)
			{
				if (dropped is Gears)
				{
					if (needGears <= 0 )
					{
						from.SendMessage( "This device has enough gears." );
						return false;
					}

					if (dropped.Amount > needGears)
					{
						dropped.Amount -= needGears;
						if ( needGears> 1 ){ sEnd = "s."; }
						from.SendMessage( "You added " + needGears.ToString() + " gear" + sEnd );
						g_HaveGears += needGears;
						Weight = 1 + (HaveMats /10) + (HaveGears /10);
						this.InvalidateProperties();
						return false;
					}
					else
					{
						g_HaveGears += dropped.Amount;
						if ( amount > 1 ){ sEnd = "s."; }
						from.SendMessage( "You added " + amount.ToString() + " gear" + sEnd );
						dropped.Delete();
						Weight = 1 + (HaveMats /10) + (HaveGears /10);
						this.InvalidateProperties();
						return true;
					}
				}
				else if ( dropped is BaseIngot && needMats > 0 )
				{
					BaseIngot thing = (BaseIngot)dropped;
					int pts = 0;

					if ( thing.Resource == CraftResource.Iron )
						pts = 1;
					else if ( thing.Resource == CraftResource.DullCopper )
						pts = 2;
					else if ( thing.Resource == CraftResource.ShadowIron )
						pts = 3;
					else if ( thing.Resource == CraftResource.Steel )
						pts = 4;
					else if ( thing.Resource == CraftResource.Copper )
						pts = 5;
					else if ( thing.Resource == CraftResource.Bronze )
						pts = 8;
					else if ( thing.Resource == CraftResource.Brass )
						pts = 10;
					else if ( thing.Resource == CraftResource.Gold )
						pts = 12;
					else if ( thing.Resource == CraftResource.Agapite )
						pts = 27;
					else if ( thing.Resource == CraftResource.Verite )
						pts = 40;
					else if ( thing.Resource == CraftResource.Valorite )
						pts = 60;
					else if ( thing.Resource == CraftResource.Nepturite )
						pts = 90;
					else if ( thing.Resource == CraftResource.Mithril )
						pts = 120;
					else if ( thing.Resource == CraftResource.Obsidian )
						pts = 140;
					else if ( thing.Resource == CraftResource.Dwarven )
						pts = 190;
					else if ( thing.Resource == CraftResource.Xormite )
						pts = 200;

					if (amount > needMats)
					{
						dropped.Amount -= needMats;
						if ( needMats> 1 ){ sEnd = "s."; }
						from.SendMessage( "You added " + needMats.ToString() + " ingot" + sEnd );
						g_HaveMetals += needMats;
						HaveMats += needMats;
						Points += (needMats*pts);
						Weight = 1 + (HaveMats /10) + (HaveGears /10);
						this.InvalidateProperties();
						return false;
					}
					else
					{
						g_HaveMetals += amount;
						HaveMats += amount;
						Points += (amount*pts);
						if ( amount > 1 ){ sEnd = "s."; }
						from.SendMessage( "You added " + amount.ToString() + " ingot" + sEnd );
						dropped.Delete();
						Weight = 1 + (HaveMats /10) + (HaveGears /10);
						this.InvalidateProperties();
						return true;
					}

				}
				else if ( ( dropped is BaseWoodBoard ) && needMats > 0 )
				{
					BaseWoodBoard thing = (BaseWoodBoard)dropped;
					int pts = 0;

					if ( thing.Resource == CraftResource.RegularWood )
						pts = 1;
					else if ( thing.Resource == CraftResource.AshTree )
						pts = 2;
					else if ( thing.Resource == CraftResource.CherryTree )
						pts = 3;
					else if ( thing.Resource == CraftResource.EbonyTree )
						pts = 5;
					else if ( thing.Resource == CraftResource.GoldenOakTree )
						pts = 8;
					else if ( thing.Resource == CraftResource.HickoryTree )
						pts = 12;
					else if ( thing.Resource == CraftResource.MahoganyTree )
						pts = 27;
					else if ( thing.Resource == CraftResource.OakTree )
						pts = 40;
					else if ( thing.Resource == CraftResource.PineTree )
						pts = 60;
					else if ( thing.Resource == CraftResource.GhostTree )
						pts = 75;
					else if ( thing.Resource == CraftResource.RosewoodTree )
						pts = 91;
					else if ( thing.Resource == CraftResource.WalnutTree )
						pts = 120;
					else if ( thing.Resource == CraftResource.PetrifiedTree )
						pts = 140;
					else if ( thing.Resource == CraftResource.DriftwoodTree )
						pts = 170;
					else if ( thing.Resource == CraftResource.ElvenTree )
						pts = 210;

					if (amount > needMats)
					{
						dropped.Amount -= needMats;
						if ( needMats> 1 ){ sEnd = "s."; }
						from.SendMessage( "You added " + needMats.ToString() + " board" + sEnd );
						g_HaveWood += needMats;
						HaveMats += needMats;
						Points += (needMats*pts);
						Weight = 1 + (HaveMats /10) + (HaveGears /10);
						this.InvalidateProperties();
						return false;
					}
					else
					{
						g_HaveWood += amount;
						HaveMats += amount;
						Points += (amount*pts);
						if ( amount > 1 ){ sEnd = "s."; }
						from.SendMessage( "You added " + amount.ToString() + " board" + sEnd );
						dropped.Delete();
						Weight = 1 + (HaveMats /10) + (HaveGears /10);
						this.InvalidateProperties();
						return true;
					}
				}
				else if ( ( dropped is BaseLeather ) && needMats > 0 )
				{
					BaseLeather thing = (BaseLeather)dropped;
					int pts = 0;

					if ( thing.Resource == CraftResource.RegularLeather )
						pts = 1;
					else if ( thing.Resource == CraftResource.SpinedLeather )
						pts = 10;
					else if ( thing.Resource == CraftResource.HornedLeather )
						pts = 20;
					else if ( thing.Resource == CraftResource.BarbedLeather )
						pts = 30;
					else if ( thing.Resource == CraftResource.NecroticLeather )
						pts = 40;
					else if ( thing.Resource == CraftResource.VolcanicLeather )
						pts = 50;
					else if ( thing.Resource == CraftResource.FrozenLeather )
						pts = 60;
					else if ( thing.Resource == CraftResource.GoliathLeather )
						pts = 70;
					else if ( thing.Resource == CraftResource.DraconicLeather )
						pts = 80;
					else if ( thing.Resource == CraftResource.HellishLeather )
						pts = 90;
					else if ( thing.Resource == CraftResource.DinosaurLeather )
						pts = 100;
					else if ( thing.Resource == CraftResource.AlienLeather )
						pts = 200;

					if (amount > needMats)
					{
						dropped.Amount -= needMats;
						if ( needMats> 1 ){ sEnd = "s."; }
						from.SendMessage( "You added " + needMats.ToString() + " leather" + sEnd );
						g_HaveLeather += needMats;
						HaveMats += needMats;
						Points += (needMats*pts);
						Weight = 1 + (HaveMats /10) + (HaveGears /10);
						this.InvalidateProperties();
						return false;
					}
					else
					{
						g_HaveLeather += amount;
						HaveMats += amount;
						Points += (amount*pts);
						if ( amount > 1 ){ sEnd = "s."; }
						from.SendMessage( "You added " + amount.ToString() + " leather" + sEnd );
						dropped.Delete();
						Weight = 1 + (HaveMats /10) + (HaveGears /10);
						this.InvalidateProperties();
						return true;
					}
				}
				else
					from.SendMessage( "Looks like that won't go in that." );
			}
			return false;
		}

		public bool DoYouHaveEverything( Mobile from )
		{
			if (from == null || from.Backpack == null)
				return false;

			Item first = null;

			if (itemone)
				first = from.Backpack.FindItemByType( typeof ( AxleGears ) );
			else
				first = from.Backpack.FindItemByType( typeof ( ClockParts ) );

			if ( first == null )
			{
				if (itemone)
					from.SendMessage( "You need Axle Gears in your pack to build this." );
				else
					from.SendMessage( "You need Clock Parts in your pack to build this." );
				return false;
			}

			Item second = null;

			if (itemtwo)
				second = from.Backpack.FindItemByType( typeof ( StarSapphire ) );
			else
				second = from.Backpack.FindItemByType( typeof ( Tourmaline ) );

			if ( second == null )
			{
				if (itemtwo)
					from.SendMessage( "You need a Star Sapphire gem in your pack to build this." );
				else
					from.SendMessage( "You need a Tourmaline gem in your pack to build this." );
				return false;
			}

			Item third = null;

			if (itemthree)
				third = from.Backpack.FindItemByType( typeof ( Spyglass ) );
			else
				third = from.Backpack.FindItemByType( typeof ( Springs ) );

			if ( third == null )
			{
				if (itemthree)
					from.SendMessage( "You need a Telescope in your pack to build this." );
				else
					from.SendMessage( "You need Springs in your pack to build this." );
				return false;
			}

			Item fourth = null;

			if (itemfour)
				fourth = from.Backpack.FindItemByType( typeof ( DovetailSaw ) );
			else
				fourth = from.Backpack.FindItemByType( typeof ( DrawKnife ) );

			if ( fourth == null )
			{
				if (itemfour)
					from.SendMessage( "You need a Dovetail Saw in your pack to build this." );
				else
					from.SendMessage( "You need a Draw Knife in your pack to build this." );
				return false;
			}

			Item fifth = null;

			if (itemfive)
				fifth = from.Backpack.FindItemByType( typeof ( Froe ) );
			else
				fifth = from.Backpack.FindItemByType( typeof ( Hammer ) );

			if ( fifth == null )
			{
				if (itemfive)
					from.SendMessage( "You need a Froe in your pack to build this." );
				else
					from.SendMessage( "You need a Hammer in your pack to build this." );
				return false;
			}

			Item sixth = null;

			if (itemsix)
				sixth = from.Backpack.FindItemByType( typeof ( MortarPestle ) );
			else
				sixth = from.Backpack.FindItemByType( typeof ( MouldingPlane ) );

			if ( sixth == null )
			{
				if (itemsix)
					from.SendMessage( "You need a Mortar and Pestle in your pack to build this." );
				else
					from.SendMessage( "You need a Moulding Plane in your pack to build this." );
				return false;
			}

			Item seventh = null;

			if (itemseven)
				seventh = from.Backpack.FindItemByType( typeof ( ArcaneGem ) );
			else
				seventh = from.Backpack.FindItemByType( typeof ( Diamond ) );

			if ( seventh == null )
			{
				if (itemseven)
					from.SendMessage( "You need an Arcane Gem in your pack to build this." );
				else
					from.SendMessage( "You need a Diamond in your pack to build this." );
				return false;
			}

			Mobile tinker = null;

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m is Tinker && from.InLOS( m ) )
					tinker = m;
			}

			if ( tinker == null )
			{
				from.SendMessage( "You need to be near a tinker to build that!" );
				return false;
			}

			Item foods = null;

			if ( food )
				foods = from.Backpack.FindItemByType( typeof ( Bacon ) );
			else
				foods = from.Backpack.FindItemByType( typeof ( CheeseWheel ) );

			if ( foods == null )
			{
				if (food)
				{
					tinker.Say("Yes I'll work on that for you, but first, I'm dying of hunger.  Have some bacon in your pack there by any chance m'Lord?");
					from.SendMessage( "Looks like " + tinker.Name + " won't work unless you also have some bacon for him." );
				}
				else
				{
					tinker.Say("Yes I'll work on that for you, but first, I'm dying of hunger.  Have a Cheese Wheel in your pack there by any chance m'Lord?");
					from.SendMessage( "Looks like " + tinker.Name + " won't work unless you also have a Cheese Wheel" );
				}
				return false;
			}

			Item drinks = null;

			if ( drink )
				drinks = from.Backpack.FindItemByType( typeof ( RefreshPotion ) );
			else
				drinks = from.Backpack.FindItemByType( typeof ( TotalRefreshPotion ) );

			if ( drinks == null )
			{
				if (drink)
				{
					tinker.Say("Perfect, but I'll need a nice refresh potion to to wash that down.");
					from.SendMessage( "Looks like " + tinker.Name + " won't work unless you have a refresh potion for him." );
				}
				else
				{
					tinker.Say("Perfect, but I'll need a nice total refresh potion to to wash that down.");
					from.SendMessage( "Looks like " + tinker.Name + " won't work unless you have a total refresh potion" );
				}

				return false;
			}

			tinker.Say("Great! Lets get to work then shall we.... I can complete the device for you.");
			if (first.Amount > 1)
				first.Amount -= 1;
			else
				first.Delete();
			if (second.Amount > 1)
				second.Amount -= 1;
			else
				second.Delete();
			if (third.Amount > 1)
				third.Amount -= 1;
			else
				third.Delete();
			if (fourth.Amount > 1)
				fourth.Amount -= 1;
			else
				fourth.Delete();
			if (fifth.Amount > 1)
				fifth.Amount -= 1;
			else
				fifth.Delete();
			if (sixth.Amount > 1)
				sixth.Amount -= 1;
			else
				sixth.Delete();
			if (seventh.Amount > 1)
				seventh.Amount -= 1;
			else
				seventh.Delete();
			if (foods.Amount > 1)
				foods.Amount -= 1;
			else
				foods.Delete();
			if (drinks.Amount > 1)
				drinks.Amount -= 1;
			else
				drinks.Delete();
			return true; // LOL

		}

		public DeviceKit( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version

			writer.Write( (int)HaveMats );
			writer.Write( (int)HaveGears );
			writer.Write( (int)g_HaveLeather );
			writer.Write( (int)g_HaveMetals );
			writer.Write( (int)g_HaveWood );
			writer.Write( (int)typee );
			writer.Write( (int)Points );
			writer.Write( (bool)itemone );
			writer.Write( (bool)itemtwo );
			writer.Write( (bool)itemthree );
			writer.Write( (bool)itemfour );
			writer.Write( (bool)itemfive );
			writer.Write( (bool)itemsix );
			writer.Write( (bool)itemseven );
			writer.Write( (bool)food );
			writer.Write( (bool)drink );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			HaveMats = reader.ReadInt();
			HaveGears = reader.ReadInt();
			g_HaveLeather = reader.ReadInt();
			g_HaveMetals = reader.ReadInt();
			g_HaveWood = reader.ReadInt();
			typee = reader.ReadInt();
			Points = reader.ReadInt();
			itemone = reader.ReadBool();
			itemtwo= reader.ReadBool();
			itemthree = reader.ReadBool();
			itemfour = reader.ReadBool();
			itemfive = reader.ReadBool();
			itemsix = reader.ReadBool();
			itemseven = reader.ReadBool();
			food = reader.ReadBool();
			drink = reader.ReadBool();

			Weight = 1 + (HaveMats /10) + (HaveGears /10);
		}


	}
}