using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class HardScales : Item
	{
		[Constructable]
		public HardScales() : this( 1, "scales" )
		{
		}

		[Constructable]
		public HardScales( int amount, string name ) : base( 0x26B2 )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Name = name;

			string material = "";

			if ( Name == "dull copper scales" ){ material = "dull copper"; }
			else if ( Name == "shadow iron scales" ){ material = "shadow iron"; }
			else if ( Name == "copper scales" ){ material = "copper"; }
			else if ( Name == "bronze scales" ){ material = "bronze"; }
			else if ( Name == "gold scales" ){ material = "gold"; }
			else if ( Name == "agapite scales" ){ material = "agapite"; }
			else if ( Name == "verite scales" ){ material = "verite"; }
			else if ( Name == "valorite scales" ){ material = "valorite"; }
			else if ( Name == "caddellite scales" ){ material = "caddellite"; }
			else if ( Name == "onyx scales" ){ material = "onyx"; }
			else if ( Name == "quartz scales" ){ material = "quartz"; }
			else if ( Name == "ruby scales" ){ material = "ruby"; }
			else if ( Name == "sapphire scales" ){ material = "sapphire"; }
			else if ( Name == "spinel scales" ){ material = "spinel"; }
			else if ( Name == "topaz scales" ){ material = "topaz"; }
			else if ( Name == "amethyst scales" ){ material = "amethyst"; }
			else if ( Name == "emerald scales" ){ material = "emerald"; }
			else if ( Name == "garnet scales" ){ material = "garnet"; }
			else if ( Name == "silver scales" ){ material = "silver"; }
			else if ( Name == "star ruby scales" ){ material = "star ruby"; }
			else if ( Name == "marble scales" ){ material = "marble"; }
			else if ( Name == "jade scales" ){ material = "jade"; }
			else if ( Name == "ice scales" ){ material = "ice"; }
			else if ( Name == "obsidian scales" ){ material = "obsidian"; }
			else if ( Name == "nepturite scales" ){ material = "nepturite"; }
			else if ( Name == "steel scales" ){ material = "steel"; }	
			else if ( Name == "brass scales" ){ material = "brass"; }
			else if ( Name == "mithril scales" ){ material = "mithril"; }
			else if ( Name == "xormite scales" ){ material = "xormite"; }
			else { Name = "iron scales"; Hue = 0x579; }

			Hue = MaterialInfo.GetMaterialColor( material, "", Hue );
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Can Be Smelted Into Ingots" );
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
				if ( this.Name == "dull copper scales" )
				{
					Item ingot = new DullCopperIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "shadow iron scales" )
				{
					Item ingot = new ShadowIronIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "copper scales" )
				{
					Item ingot = new CopperIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "bronze scales" )
				{
					Item ingot = new BronzeIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "gold scales" )
				{
					Item ingot = new GoldIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "agapite scales" )
				{
					Item ingot = new AgapiteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "verite scales" )
				{
					Item ingot = new VeriteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "valorite scales" )
				{
					Item ingot = new ValoriteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "caddellite scales" )
				{
					Item ingot = new CaddelliteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "onyx scales" )
				{
					Item ingot = new OnyxIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "quartz scales" )
				{
					Item ingot = new QuartzIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "ruby scales" )
				{
					Item ingot = new RubyIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "sapphire scales" )
				{
					Item ingot = new SapphireIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "spinel scales" )
				{
					Item ingot = new SpinelIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "topaz scales" )
				{
					Item ingot = new TopazIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "amethyst scales" )
				{
					Item ingot = new AmethystIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "emerald scales" )
				{
					Item ingot = new EmeraldIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "garnet scales" )
				{
					Item ingot = new GarnetIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "silver scales" )
				{
					Item ingot = new ShinySilverIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "star ruby scales" )
				{
					Item ingot = new StarRubyIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "marble scales" )
				{
					Item ingot = new MarbleIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "jade scales" )
				{
					Item ingot = new JadeIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "ice scales" )
				{
					Item ingot = new IceIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "obsidian scales" )
				{
					Item ingot = new ObsidianIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "nepturite scales" )
				{
					Item ingot = new NepturiteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "steel scales" )
				{
					Item ingot = new SteelIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "brass scales" )
				{
					Item ingot = new BrassIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "mithril scales" )
				{
					Item ingot = new MithrilIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "xormite scales" )
				{
					Item ingot = new XormiteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else
				{
					Item ingot = new IronIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}

				from.PlaySound( 0x208 );
				from.SendMessage( "You smelt the scales and put the ingots in your backpack." );
				this.Delete();
			}
			else
			{
				from.SendMessage( "Only an apprentice blacksmith can smelt that." );
			}
		}

		public HardScales( Serial serial ) : base( serial )
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