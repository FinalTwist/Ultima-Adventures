using System;
using System.Collections;
using Server.Network;
using Server.Prompts;
using Server.Targeting;

namespace Server.Items
{
	public class Waterskin : Item
	{
		[Constructable]
		public Waterskin() : base( 0xA21 )
		{
			Name = "empty waterskin";
			Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			bool soaked;
			Server.Items.DrinkingFunctions.CheckWater( from, 3, out soaked );

			if ( this.Weight < 1.95 && soaked ) // FILL IT IF NEAR WATER AND FLASK IS NOT FULL
			{
				if ( !IsChildOf( from.Backpack ) ) 
				{
					from.SendMessage( "This must be in your backpack to fill." );
					return;
				}
				else
				{
					from.PlaySound( 0x240 );
					if ( this.ItemID == 0x48E4 || this.ItemID == 0x4971 )
					{
						this.ItemID = 0x48E4;
						this.Name = "canteen";
					}
					else
					{
						this.ItemID = 0x98F;
						this.Name = "waterskin";
					}
					this.Weight = 2.0;
					this.InvalidateProperties();
				}
			}
			else if ( this.ItemID == 0xA21 || this.ItemID == 0x4971 ) // TOTALLY EMPTY
			{
				from.SendMessage( "You can only fill this at a water trough, tub, or barrel!" ); 
			}
			else
			{
				if ( !IsChildOf( from.Backpack ) ) 
				{
					from.SendMessage( "This must be in your backpack to drink." );
					return;
				}
				else
				{
					// increase characters thirst value based on type of drink
					if ( from.Thirst < 20 )
					{
						from.Thirst += 5;
						// Send message to character about their current thirst value
						int iThirst = from.Thirst;
						if ( iThirst < 5 )
							from.SendMessage( "You drink the water but are still extremely thirsty" );
						else if ( iThirst < 10 )
							from.SendMessage( "You drink the water and feel less thirsty" );
						else if ( iThirst < 15 )
							from.SendMessage( "You drink the water and feel much less thirsty" ); 
						else
							from.SendMessage( "You drink the water and are no longer thirsty" );

						if ( from.Body.IsHuman && !from.Mounted )
							from.Animate( 34, 5, 1, true, false, 0 );

						from.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) );

						this.Weight = this.Weight - 0.1;

						if ( this.Weight <= 1.0 )
						{
							if ( this.ItemID == 0x48E4 || this.ItemID == 0x4971 )
							{
								this.ItemID = 0x4971;
								this.Name = "empty canteen";
							}
							else
							{
								this.ItemID = 0xA21;
								this.Name = "empty waterskin";
							}
							this.Weight = 1.0;
						}

						this.InvalidateProperties();

						Server.Items.DrinkingFunctions.DrinkBenefits( from );
					}
					else
					{
						from.SendMessage( "You are simply too quenched to drink any more!" );
						from.Thirst = 20;
					}
				}
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			string drinks = "empty";

			if ( this.Weight >= 2.0 ){ drinks = "10 Drinks Remaining"; }
			else if ( this.Weight >= 1.85 ){ drinks = "9 Drinks Remaining"; }
			else if ( this.Weight >= 1.75 ){ drinks = "8 Drinks Remaining"; }
			else if ( this.Weight >= 1.65 ){ drinks = "7 Drinks Remaining"; }
			else if ( this.Weight >= 1.55 ){ drinks = "6 Drinks Remaining"; }
			else if ( this.Weight >= 1.45 ){ drinks = "5 Drinks Remaining"; }
			else if ( this.Weight >= 1.35 ){ drinks = "4 Drinks Remaining"; }
			else if ( this.Weight >= 1.25 ){ drinks = "3 Drinks Remaining"; }
			else if ( this.Weight >= 1.15 ){ drinks = "2 Drinks Remaining"; }
			else if ( this.Weight > 1.0 ){ drinks = "1 Drink Remaining"; }

			if ( drinks != "empty" ){ list.Add( 1070722, drinks ); }
		}

		public Waterskin( Serial serial ) : base( serial )
		{
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
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DirtyWaterskin : Item
	{
		[Constructable]
		public DirtyWaterskin() : base( 0x98F )
		{
			Hue = 0xB97;
			Name = "old waterskin";
			Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			bool soaked;
			Server.Items.DrinkingFunctions.CheckWater( from, 3, out soaked );

			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else if ( soaked )
			{
				from.SendMessage( "You fill the container with fresh water." );
				from.PlaySound( 0x240 );
				this.Consume();
				Item flask = new Waterskin();
				flask.ItemID = this.ItemID;
				flask.Name = "waterskin";
					if ( this.ItemID == 0x48E4 ){ flask.Name = "canteen"; }
				flask.Weight = 2.0;
				from.AddToBackpack( flask );
			}
			else if ( from.Thirst < 20 )
			{
				from.Thirst += 5;
				// Send message to character about their current thirst value
				int iThirst = from.Thirst;
				if ( iThirst < 5 )
					from.SendMessage( "You drink the dirty water but are still extremely thirsty" );
				else if ( iThirst < 10 )
					from.SendMessage( "You drink the dirty water and feel less thirsty" );
				else if ( iThirst < 15 )
					from.SendMessage( "You drink the dirty water and feel much less thirsty" ); 
				else
					from.SendMessage( "You drink the dirty water and are no longer thirsty" );

				this.Consume();

				if ( from.Body.IsHuman && !from.Mounted )
					from.Animate( 34, 5, 1, true, false, 0 );

				from.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) );

				Item flask = new Waterskin();
				flask.Weight = 1.0;
				flask.ItemID = 0xA21;
				flask.Name = "empty waterskin";
					if ( this.ItemID == 0x48E4 ){ flask.ItemID = 0x48E4; flask.Name = "empty canteen"; }
				from.AddToBackpack( flask );

				Server.Items.DrinkingFunctions.DrinkBenefits( from );

				// CHECK FOR ANY DUNGEON FOOD ILLNESSES //////////////////////////////////////
				if ( from.CheckSkill( SkillName.TasteID, 0, 100 ) )
				{
				}
				else if ( Utility.RandomMinMax( 1, 100 ) > 70 )
				{
					int nPoison = Utility.RandomMinMax( 0, 10 );
					from.Say( "Poison!" );
					if ( nPoison > 9 ) { from.ApplyPoison( from, Poison.Deadly ); }
					else if ( nPoison > 7 ) { from.ApplyPoison( from, Poison.Greater ); }
					else if ( nPoison > 4 ) { from.ApplyPoison( from, Poison.Regular ); }
					else { from.ApplyPoison( from, Poison.Lesser ); }
					from.SendMessage( "Poison!");
				}
			}
			else
			{
				from.SendMessage( "You are simply too quenched to drink any more!" );
				from.Thirst = 20;
			}
		}

		public DirtyWaterskin( Serial serial ) : base( serial )
		{
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
		}
	}

    class DrinkingFunctions
    {
		public static void CheckWater( Mobile from, int range, out bool soaked )
		{
			soaked = false;

			Map map = from.Map;

			if ( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, range );

			foreach ( Item item in eable )
			{
				Type type = item.GetType();

				bool isWater = ( item.ItemID == 4090 || item.ItemID == 0x21F2 || item.ItemID == 0x1519 || item.ItemID == 0x1534 || 
					item.ItemID == 0x22A1 || item.ItemID == 0x22A2 || item.ItemID == 0x22A3 || 
					item.ItemID == 0x22A4 || item.ItemID == 0x22A5 || item.ItemID == 0x22A6 || 
					item.ItemID == 0x21F3 || item.ItemID == 0x21F4 || item.ItemID == 0x21F5 || 
					( item.ItemID >= 0x4CCF && item.ItemID <= 0x4CD9 ) || 
					item.ItemID == 0xFFA || item.ItemID == 0xB41 || item.ItemID == 0xB42 || item.ItemID == 0x0F33 || 
					item.ItemID == 0xB43 || item.ItemID == 0xB44 || item.ItemID == 0xE7B || item.ItemID == 0x154D || 
					item.ItemID == 3707 || item.ItemID == 5453 || item.ItemID == 2882 || item.ItemID == 2881 || 
					item.ItemID == 13422 || item.ItemID == 2883 || item.ItemID == 2884 );

				if ( isWater )
				{
					if ( (from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS( item ) )
						continue;

					soaked = soaked || isWater;

					if ( soaked )
						break;
				}
			}

			eable.Free();

			for ( int x = -range; (!soaked) && x <= range; ++x )
			{
				for ( int y = -range; (!soaked) && y <= range; ++y )
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles( from.X+x, from.Y+y, true );

					for ( int i = 0; (!soaked) && i < tiles.Length; ++i )
					{
						int id = tiles[i].ID;

						bool isWater = ( id== 4090 || id== 0x21F2 || id== 0x1519 || id== 0x1534 || 
							id== 0x22A1 || id== 0x22A2 || id== 0x22A3 || 
							id== 0x22A4 || id== 0x22A5 || id== 0x22A6 || 
							id== 0x21F3 || id== 0x21F4 || id== 0x21F5 || 
							( id >= 0x4CCF && id <= 0x4CD9 ) || 
							id== 0xFFA || id== 0xB41 || id== 0xB42 || id== 0x0F33 || 
							id== 0xB43 || id== 0xB44 || id== 0xE7B || id== 0x154D || 
							id== 3707 || id== 5453 || id== 2882 || id== 2881 || 
							id== 13422 || id== 2883 || id== 2884 );

						if ( isWater )
						{
							if ( (from.Z + 16) < tiles[i].Z || (tiles[i].Z + 16) < from.Z || !from.InLOS( new Point3D( from.X+x, from.Y+y, tiles[i].Z + (tiles[i].Height/2) + 1 ) ) )
								continue;

							soaked = soaked || isWater;
						}
					}
				}
			}
		}

		public static void OnDrink( Item drink, Mobile from )
		{
			if ( !drink.IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to drink." );
				return;
			}
			else
			{
				// increase characters thirst value based on type of drink
				if ( from.Thirst < 20 )
				{
					from.Thirst += 5;
					// Send message to character about their current thirst value
					int iThirst = from.Thirst;
					if ( iThirst < 5 )
						from.SendMessage( "You drink the water but are still extremely thirsty" );
					else if ( iThirst < 10 )
						from.SendMessage( "You drink the water and feel less thirsty" );
					else if ( iThirst < 15 )
						from.SendMessage( "You drink the water and feel much less thirsty" ); 
					else
						from.SendMessage( "You drink the water and are no longer thirsty" );

					drink.Consume();
					from.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) );

					Server.Items.DrinkingFunctions.DrinkBenefits( from );
				}
				else
				{
					from.SendMessage( "You are simply too quenched to drink anymore" );
					from.Thirst = 20;
				}
			}
		}

		public static void DrinkBenefits( Mobile from )
		{
			int iHeal = (int)from.Skills[SkillName.TasteID].Value;
			int iHurt = from.StamMax - from.Stam;

			if ( iHurt > 0 )
			{
				if ( iHeal > iHurt )
				{
					iHeal = iHurt;
				}

				from.Stam = from.Stam + iHeal;

				if ( from.Poisoned )
				{
					if ( (int)from.Skills[SkillName.TasteID].Value >= Utility.RandomMinMax( 1, 100 ) )
					{
						from.CurePoison( from );
						from.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
					}
				}
			}
		}
	}
}