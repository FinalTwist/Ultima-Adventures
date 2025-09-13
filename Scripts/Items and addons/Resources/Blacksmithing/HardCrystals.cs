using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class HardCrystals : Item
	{
		[Constructable]
		public HardCrystals() : this( 1, "crystalline rock" )
		{
		}

		[Constructable]
		public HardCrystals( int amount, string name ) : base( 0x3003 )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Name = name;

			string material = "";

			if ( Name == "crystalline dull copper" ){ material = "dull copper"; }
			else if ( Name == "crystalline shadow iron" ){ material = "shadow iron"; }
			else if ( Name == "crystalline copper" ){ material = "copper"; }
			else if ( Name == "crystalline bronze" ){ material = "bronze"; }
			else if ( Name == "crystalline gold" ){ material = "gold"; }
			else if ( Name == "crystalline agapite" ){ material = "agapite"; }
			else if ( Name == "crystalline verite" ){ material = "verite"; }
			else if ( Name == "crystalline valorite" ){ material = "valorite"; }
			else if ( Name == "crystalline caddellite" ){ material = "caddellite"; }
			else if ( Name == "crystalline onyx" ){ material = "onyx"; }
			else if ( Name == "crystalline quartz" ){ material = "quartz"; }
			else if ( Name == "crystalline ruby" ){ material = "ruby"; }
			else if ( Name == "crystalline sapphire" ){ material = "sapphire"; }
			else if ( Name == "crystalline spinel" ){ material = "spinel"; }
			else if ( Name == "crystalline topaz" ){ material = "topaz"; }
			else if ( Name == "crystalline amethyst" ){ material = "amethyst"; }
			else if ( Name == "crystalline emerald" ){ material = "emerald"; }
			else if ( Name == "crystalline garnet" ){ material = "garnet"; }
			else if ( Name == "crystalline silver" ){ material = "silver"; }
			else if ( Name == "crystalline star ruby" ){ material = "star ruby"; }
			else if ( Name == "crystalline marble" ){ material = "marble"; }
			else if ( Name == "crystalline jade" ){ material = "jade"; }
			else if ( Name == "crystalline ice" ){ material = "ice"; }
			else if ( Name == "crystalline obsidian" ){ material = "obsidian"; }
			else if ( Name == "crystalline nepturite" ){ material = "nepturite"; }
			else if ( Name == "crystalline steel" ){ material = "steel"; }	
			else if ( Name == "crystalline brass" ){ material = "brass"; }
			else if ( Name == "crystalline mithril" ){ material = "mithril"; }
			else if ( Name == "crystalline xormite" ){ material = "xormite"; }
			else { Name = "iron"; Hue = 0x579; }

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
				if ( this.Name == "crystalline dull copper" )
				{
					Item ingot = new DullCopperIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline shadow iron" )
				{
					Item ingot = new ShadowIronIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline copper" )
				{
					Item ingot = new CopperIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline bronze" )
				{
					Item ingot = new BronzeIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline gold" )
				{
					Item ingot = new GoldIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline agapite" )
				{
					Item ingot = new AgapiteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline verite" )
				{
					Item ingot = new VeriteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline valorite" )
				{
					Item ingot = new ValoriteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline caddellite" )
				{
					Item ingot = new CaddelliteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline onyx" )
				{
					Item ingot = new OnyxIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline quartz" )
				{
					Item ingot = new QuartzIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline ruby" )
				{
					Item ingot = new RubyIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline sapphire" )
				{
					Item ingot = new SapphireIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline spinel" )
				{
					Item ingot = new SpinelIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline topaz" )
				{
					Item ingot = new TopazIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline amethyst" )
				{
					Item ingot = new AmethystIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline emerald" )
				{
					Item ingot = new EmeraldIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "crystalline garnet" )
				{
					Item ingot = new GarnetIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline silver" )
				{
					Item ingot = new ShinySilverIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline star ruby" )
				{
					Item ingot = new StarRubyIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline marble" )
				{
					Item ingot = new MarbleIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline jade" )
				{
					Item ingot = new JadeIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline ice" )
				{
					Item ingot = new IceIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline obsidian" )
				{
					Item ingot = new ObsidianIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline nepturite" )
				{
					Item ingot = new NepturiteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline steel" )
				{
					Item ingot = new SteelIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline brass" )
				{
					Item ingot = new BrassIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline mithril" )
				{
					Item ingot = new MithrilIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "crystalline xormite" )
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
				from.SendMessage( "You smelt the crystalline metal the and put the ingots in your backpack." );
				this.Delete();
			}
			else
			{
				from.SendMessage( "Only an apprentice blacksmith can smelt that." );
			}
		}

		public HardCrystals( Serial serial ) : base( serial )
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