using System;

namespace Server.Items
{
	public class HalloweenGift : Item
	{
		[Constructable]
		public HalloweenGift(): base()
		{
			Weight = 1.0;
			int Gift = Utility.RandomMinMax( 0, 15 );
			Movable = true;

			if (Gift == 2){ ItemID = 16382; Name = "Stuffed Nightmare"; Hue = 0x386; }
			else if (Gift == 3){ ItemID = 2852; Name = "Gothic Lamp Post"; Light = LightType.Circle225; }
			else if (Gift == 4){ ItemID = 9777; Name = "Bat"; }
			else if (Gift == 5){ ItemID = 9737; Name = "Chaos Demon";}
			else if (Gift == 6){ ItemID = 16142; Name = "Coffin";}
			else if (Gift == 7){ ItemID = 15117; Name = "Witch's Toad";}
			else if (Gift == 8){ ItemID = 10936; Name = "Altar";}
			else if (Gift == 9){ ItemID = 10897; Name = "Bone Throne";}
			else if (Gift == 10){ ItemID = 10898; Name = "Torture Table";}
			else if (Gift == 11){ ItemID = 8700; Name = "Skull Spikes";}

			else // BOOK
			{
				switch ( Utility.RandomMinMax( 0, 13 ) ) 
				{
					case 0: ItemID = 3643; break;
					case 1: ItemID = 3834; break;
					case 2: ItemID = 4029; break;
					case 3: ItemID = 4030; break;
					case 4: ItemID = 7185; break;
					case 5: ItemID = 7185; break;
					case 6: ItemID = 7187; break;
					case 7: ItemID = 7187; break;
					case 8: ItemID = 8787; break;
					case 9: ItemID = 8787; break;
					case 10: ItemID = 8788; break;
					case 11: ItemID = 8788; break;
					case 12: ItemID = 8901; break;
					case 13: ItemID = 8901; break;
				}

				switch ( Utility.RandomMinMax( 0, 4 ) ) 
				{
					case 0: Hue = Utility.RandomNeutralHue(); break;
					case 1: Hue = Utility.RandomRedHue(); break;
					case 2: Hue = Utility.RandomBlueHue(); break;
					case 3: Hue = Utility.RandomGreenHue(); break;
					case 4: Hue = Utility.RandomYellowHue(); break;
				}

				switch ( Utility.RandomMinMax( 0, 23 ) )
				{
					case 0: Name="Salem's Lot"; break;
					case 1: Name="The Haunting Hill House"; break;
					case 2: Name="I Am Legend"; break;
					case 3: Name="The Mummy"; break;
					case 4: Name="Dracula"; break;
					case 5: Name="The War of the World"; break;
					case 6: Name="Frankenstein"; break;
					case 7: Name="The Exorcist"; break;
					case 8: Name="It"; break;
					case 9: Name="Stories of Edgar Allen Poe"; break;
					case 10: Name="Tales from the Crypt"; break;
					case 11: Name="Better Gnomes and Goblins"; break;
					case 12: Name="How To Kill Vampires"; break;
					case 13: Name="Lycanthropy Cures"; break;
					case 14: Name="How To Unwrap Mummies"; break;
					case 15: Name="Interview with a Vampire"; break;
					case 16: Name="How To Carve Pumpkins"; break;
					case 17: Name="How To Cook People"; break;
					case 18: Name="How To A Witch Wet"; break;
					case 19: Name="Fright Night"; break;
					case 20: Name="The Legend Of The Werewolf"; break;
					case 21: Name="Famous Murderers"; break;
					case 22: Name="The Story of Elizabeth Bathory"; break;
					case 23: Name="The Tale of Vlad the Impaler"; break;
				}
			}
		}

		public HalloweenGift( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Happy Halloween" );
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
}