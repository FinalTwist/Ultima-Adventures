using Server;
using System;
using Server.Misc;

namespace Server.Items
{
	public class RobotSheetMetal : Item
	{
		[Constructable]
		public RobotSheetMetal() : this( 1 )
		{
		}

		[Constructable]
		public RobotSheetMetal( int amount ) : base( 0x3544 )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Name = "robot metal";
		}

		public override void OnDoubleClick( Mobile from )
		{
			bool anvil, forge;
			Server.Engines.Craft.DefBlacksmithy.CheckAnvilAndForge( from, 2, out anvil, out forge );

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( !forge )
			{
				from.SendMessage( "You need to be near a forge to smelt that." );
			}
			else if ( from.Skills[SkillName.Blacksmith].Value >= 50 )
			{
				Item ingot = new IronIngot();
				ingot.Amount = this.Amount*3;
				from.AddToBackpack( ingot );
				from.PlaySound( 0x208 );
				from.SendMessage( "You smelt this into usable iron ingots." );
				this.Delete();
			}
			else
			{
				from.SendMessage( "Only an apprentice blacksmith can smelt that." );
			}
		}

		public RobotSheetMetal( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}