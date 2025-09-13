using System;

namespace Server.Items
{
	public class ThrowingWeapon : Item
	{
		public string ammo;

		[CommandProperty(AccessLevel.Owner)]
		public string Ammo { get { return ammo; } set { ammo = value; InvalidateProperties(); } }

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public ThrowingWeapon() : this( 1 )
		{
		}

		[Constructable]
		public ThrowingWeapon( int amount ) : base( 0x10B2 )
		{
			Name = "throwing star";
			if ( ammo == "" || ammo == null )
			{
				switch ( Utility.RandomMinMax( 0, 4 ) ) 
				{
					case 0: ammo = "Throwing Axes"; ItemID = 0x10B3; Name = "throwing axe"; break;
					case 1: ammo = "Throwing Daggers"; ItemID = 0x10B7; Name = "throwing dagger"; break;
					case 2: ammo = "Throwing Darts"; ItemID = 0x10B5; Name = "throwing dart"; break;
					case 3: ammo = "Throwing Stars"; ItemID = 0x10B2; Name = "throwing star"; break;
					case 4: ammo = "Throwing Stones"; ItemID = 0x10B6; Name = "throwing stone"; break;
				}
				this.InvalidateProperties();
			}
			Stackable = true;
			Amount = amount;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1049644, "Double click to change ammo from " + ammo );
			list.Add( 1070722, "Can Be Used With Throwing Gloves" );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to change the ammo type." );
				return;
			}
			else
			{
				if ( ammo == "Throwing Stones" ){ ammo = "Throwing Axes"; ItemID = 0x10B3; Name = "throwing axe"; }
				else if ( ammo == "Throwing Axes" ){ ammo = "Throwing Daggers"; ItemID = 0x10B7; Name = "throwing dagger"; }
				else if ( ammo == "Throwing Daggers" ){ ammo = "Throwing Darts"; ItemID = 0x10B5; Name = "throwing dart"; }
				else if ( ammo == "Throwing Darts" ){ ammo = "Throwing Stars"; ItemID = 0x10B2; Name = "throwing star"; }
				else if ( ammo == "Throwing Stars" && Server.Misc.GetPlayerInfo.isJester( from ) ){ ammo = "Throwing Cards"; ItemID = 0x4C29; Name = "throwing card"; }
				else if ( ammo == "Throwing Cards" && Server.Misc.GetPlayerInfo.isJester( from ) ){ ammo = "Throwing Tomatoes"; ItemID = 0x4C28; Name = "throwing tomato"; }
				else { ammo = "Throwing Stones"; ItemID = 0x10B6; Name = "throwing stone"; }
				from.SendMessage(68, "You have changed the ammo to " + ammo + ".");
				this.InvalidateProperties();
			}
		}

		public ThrowingWeapon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( ammo );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            ammo = reader.ReadString();
			Hue = 0;
		}
	}
}