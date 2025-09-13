// Created by Malice_Molaka
// For Script support contact Malice at Malice_Molaka@hotmail.com
using System;using Server;namespace Server.Items
{
	public class DantesInks : Item
	{
		public override int LabelNumber{ get{ return 1062926; } } // I used Petal of the Rose of Trinsic as the base

		[Constructable]
		public DantesInks() : this( 1 )
		{
		}

		[Constructable]
		public DantesInks( int amount ) : base( 0x15FA ) // this is the itemid for the brain
		{
			Name = "Dante's Inks"; // This is the item name
			Stackable = true; // This makes the item either stackable or not
			Amount = amount;
                        Hue = Utility.RandomList( 1157, 1175, 1172, 1171, 1170, 1169, 1168, 1167, 1166, 1165 );
			Weight = 3.0; // This makes the item weigh what you want it to
		}

		public virtual bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You can not dye that.
			return false;
		}

		

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042038 ); // This gives the player the message "You must have the object in your backpack to use it."
			}
			else if ( from.GetStatMod( "Brain" ) != null )
			{
				from.SendLocalizedMessage( 1062927 ); // This gives the player the message "You have eaten one of these recently and eating another would provide no benefit."
			}
			else
			{
				from.PlaySound( 0x1EE );
				from.AddStatMod( new StatMod( StatType.Int, "DantesInks", 5, TimeSpan.FromMinutes( 15.0 ) ) ); // this makes DantesInks give you 5 INT for 15 mins when eaten

				Consume();
			}
		}

		public DantesInks( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
