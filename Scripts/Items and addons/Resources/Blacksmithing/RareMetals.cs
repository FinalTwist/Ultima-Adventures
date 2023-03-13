using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class RareMetals : Item
	{
		[Constructable]
		public RareMetals() : this( 1, "silver stones" )
		{
		}

		[Constructable]
		public RareMetals( int amount, string name ) : base( 0x19B9 )
		{
			Weight = 12.0;
			Stackable = true;
			Amount = amount;
			Name = name;

			if ( Name == "onyx stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 ); }
			else if ( Name == "quartz stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 ); }
			else if ( Name == "ruby stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 ); }
			else if ( Name == "sapphire stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 ); }
			else if ( Name == "spinel stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 ); }
			else if ( Name == "topaz stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 ); }
			else if ( Name == "amethyst stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 ); }
			else if ( Name == "emerald stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 ); }
			else if ( Name == "garnet stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 ); }
			else if ( Name == "star ruby stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 ); }
			else if ( Name == "gargish marble stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 ); }
			else if ( Name == "jade stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 ); }
			else if ( Name == "mystical ice stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 ); }
			else if ( Name == "silver stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 ); }
			else if ( Name == "copper stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "copper", "", 0 ); }
			else if ( Name == "verite stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "verite", "", 0 ); }
			else if ( Name == "valorite stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "valorite", "", 0 ); }
			else if ( Name == "agapite stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "agapite", "", 0 ); }
			else if ( Name == "bronze stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "bronze", "", 0 ); }
			else if ( Name == "dull copper stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); }
			else if ( Name == "gold stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "gold", "", 0 ); }
			else if ( Name == "shadow iron stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); }
			else if ( Name == "mithril stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "mithril", "", 0 ); }
			else if ( Name == "xormite stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "xormite", "", 0 ); }
			else if ( Name == "obsidian stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); }
			else if ( Name == "nepturite stones" ){ Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); }
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
				if ( this.Name == "onyx stones" )
				{
					Item ingot = new OnyxIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "quartz stones" )
				{
					Item ingot = new QuartzIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "ruby stones" )
				{
					Item ingot = new RubyIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "sapphire stones" )
				{
					Item ingot = new SapphireIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "spinel stones" )
				{
					Item ingot = new SpinelIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "topaz stones" )
				{
					Item ingot = new TopazIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "amethyst stones" )
				{
					Item ingot = new AmethystIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "emerald stones" )
				{
					Item ingot = new EmeraldIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( this.Name == "garnet stones" )
				{
					Item ingot = new GarnetIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "star ruby stones" )
				{
					Item ingot = new StarRubyIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "gargish marble stones" )
				{
					Item ingot = new MarbleIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "jade stones" )
				{
					Item ingot = new JadeIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "mystical ice stones" )
				{
					Item ingot = new IceIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "silver stones" )
				{
					Item ingot = new ShinySilverIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "copper stones" )
				{
					Item ingot = new CopperIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "verite stones" )
				{
					Item ingot = new VeriteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "valorite stones" )
				{
					Item ingot = new ValoriteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "agapite stones" )
				{
					Item ingot = new AgapiteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "bronze stones" )
				{
					Item ingot = new BronzeIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "dull copper stones" )
				{
					Item ingot = new DullCopperIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "gold stones" )
				{
					Item ingot = new GoldIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "shadow iron stones" )
				{
					Item ingot = new ShadowIronIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "mithril stones" )
				{
					Item ingot = new MithrilIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "xormite stones" )
				{
					Item ingot = new XormiteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "obsidian stones" )
				{
					Item ingot = new ObsidianIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}
				else if ( Name == "nepturite stones" )
				{
					Item ingot = new NepturiteIngot();
					ingot.Amount = this.Amount;
					from.AddToBackpack( ingot );
				}

				from.PlaySound( 0x208 );
				from.SendMessage( "You smelt the stones and put the ingots in your backpack." );
				this.Delete();
			}
			else
			{
				from.SendMessage( "Only an apprentice blacksmith can smelt that." );
			}
		}

		public RareMetals( Serial serial ) : base( serial )
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