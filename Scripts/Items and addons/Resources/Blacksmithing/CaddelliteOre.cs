using System;
using Server;
using Server.Regions;

namespace Server.Items
{
	public class CaddelliteOre : Item
	{
		[Constructable]
		public CaddelliteOre() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 7.0; }
		}

		[Constructable]
		public CaddelliteOre( int amount ) : base( 0x19B9 )
		{
			Name = "caddellite ore";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else if ( from.Skills[SkillName.Blacksmith].Base < 90 )
			{
				from.SendMessage("You must be a master blacksmith to smelt caddellite ore!");
			}
			else if ( from.Region.IsPartOf( "the Great Dwarven Forge" ) )
			{
				from.PlaySound( 0x208 );
				from.Animate( 11, 5, 1, true, false, 0 );
				from.SendMessage("The heat is able to form the caddellite into something useable!");
				from.AddToBackpack( new CaddelliteIngot( this.Amount ) );
				this.Delete();
			}
			else
			{
				from.SendMessage("Only the fires of the dwarven forge could melt caddellite!");
			}
		}

		public CaddelliteOre( Serial serial ) : base( serial )
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
}