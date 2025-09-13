using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class MagicHammer : Item
	{
		[Constructable]
		public MagicHammer() : base( 0xFB5 )
		{
			Weight = 1.0;
			ItemID = Utility.RandomList( 0xFB4, 0xFB5 );
			Hue = 0xAFA;
			Name = "magical forging hammer";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Changes the Name and Appearance of Metal Items");
            list.Add( 1049644, "Helms, Shields, Armor, Staves, and Sceptres");
        } 

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.SendMessage( "What do you want to use the hammer on?" );
				t = new WearTarget( this );
				from.Target = t;
			}
		}

		private class WearTarget : Target
		{
			private MagicHammer m_Wear;

			public WearTarget( MagicHammer hammers ) : base( 1, false, TargetFlags.None )
			{
				m_Wear = hammers;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{

				if (from == null || targeted == null || !(targeted is Item) )
					return;

				bool DoEffects = false;
				Item iWear = targeted as Item;

				string NewName = null;

				if ( !iWear.IsChildOf( from.Backpack ) )
				{
					from.SendMessage( "You can only use this hammer on items in your pack." );
				}
				else if ( iWear is WizardStick || iWear is GiftSceptre || iWear is LevelSceptre )
				{
					if ( iWear.ItemID == 0x269D || iWear.ItemID == 0x269E ){ iWear.ItemID = 0x26BC; }
					else if ( iWear.ItemID == 0x26BC || iWear.ItemID == 0x26C6 ){ iWear.ItemID = 0x0DF2; }
					else if ( iWear.ItemID == 0x0DF2 ){ iWear.ItemID = 0x0DF3; }
					else if ( iWear.ItemID == 0x0DF3 ){ iWear.ItemID = 0x0DF4; }
					else if ( iWear.ItemID == 0x0DF4 ){ iWear.ItemID = 0x0DF5; }
					else if ( iWear.ItemID == 0x0DF5 ){ iWear.ItemID = 0x269D; }

					from.PlaySound( 0x2A );
					from.SendMessage( "The hammer magical transforms the sceptre." );
				}
				else if ( iWear is WizardStaff || iWear is GiftStave || iWear is LevelStave )
				{
					if ( iWear.ItemID == 0x0908 || iWear.ItemID == 0x0909 ){ iWear.ItemID = 0xE81; }
					else if ( iWear.ItemID == 0xE81 || iWear.ItemID == 0xE82 ){ iWear.ItemID = 0x13F8; }
					else if ( iWear.ItemID == 0x13F8 || iWear.ItemID == 0x13F9 ){ iWear.ItemID = 0xDF1; }
					else if ( iWear.ItemID == 0xDF1 || iWear.ItemID == 0xDF0 ){ iWear.ItemID = 0x2D25; }
					else if ( iWear.ItemID == 0x2D25 || iWear.ItemID == 0x2D31 ){ iWear.ItemID = 0xE89; }
					else if ( iWear.ItemID == 0xE89 || iWear.ItemID == 0xE8A ){ iWear.ItemID = 0x0908; }

					from.PlaySound( 0x2A );
					from.SendMessage( "The hammer magical transforms the stave." );
				}
				else if ( iWear is BaseArmor && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( iWear ) )
				{
					if ( iWear.ItemID == 0x2778 || iWear.ItemID == 0x27C3 ){ DoEffects = true; iWear.ItemID = 0x2645; NewName = "dragon helm"; }
					else if ( iWear.ItemID == 0x2645 || iWear.ItemID == 0x2646 ){ DoEffects = true; iWear.ItemID = 0x2FBB; NewName = "dread helm"; }
					else if ( iWear.ItemID == 0x2FBB ){ DoEffects = true; iWear.ItemID = 0x2B10; NewName = "royal helm"; }
					else if ( iWear.ItemID == 0x2B10 || iWear.ItemID == 0x2B11 ){ DoEffects = true; iWear.ItemID = 0x1412; NewName = "platemail helm"; }
					else if ( iWear.ItemID == 0x1412 || iWear.ItemID == 0x1419 ){ DoEffects = true; iWear.ItemID = 0x1F0B; NewName = "horned helm"; }
					else if ( iWear.ItemID == 0x1F0B || iWear.ItemID == 0x1F0C ){ DoEffects = true; iWear.ItemID = 0x140E; NewName = "norse helm"; }
					else if ( iWear.ItemID == 0x140E || iWear.ItemID == 0x140F ){ DoEffects = true; iWear.ItemID = 0x140A; NewName = "helmet"; }
					else if ( iWear.ItemID == 0x140A || iWear.ItemID == 0x140B ){ DoEffects = true; iWear.ItemID = 0x1451; NewName = "bone helm"; }
					else if ( iWear.ItemID == 0x1451 || iWear.ItemID == 0x1456 ){ DoEffects = true; iWear.ItemID = 0x13BB; NewName = "chainmail coif"; }
					else if ( iWear.ItemID == 0x13BB || iWear.ItemID == 0x13C0 ){ DoEffects = true; iWear.ItemID = 0x1408; NewName = "close helm"; }
					else if ( iWear.ItemID == 0x1408 || iWear.ItemID == 0x1409 ){ DoEffects = true; iWear.ItemID = 0x140C; NewName = "bascinet"; }
					else if ( iWear.ItemID == 0x140C || iWear.ItemID == 0x140D ){ DoEffects = true; iWear.ItemID = 0x2774; NewName = "chainmail hatsuburi"; }
					else if ( iWear.ItemID == 0x2774 ){ DoEffects = true; iWear.ItemID = 0x2781; NewName = "platemail jingasa"; }
					else if ( iWear.ItemID == 0x2781 ){ DoEffects = true; iWear.ItemID = 0x2777; NewName = "platemail jingasa"; }
					else if ( iWear.ItemID == 0x2777 ){ DoEffects = true; iWear.ItemID = 0x2775; NewName = "platemail hatsuburi"; }
					else if ( iWear.ItemID == 0x2775 || iWear.ItemID == 0x27C0 ){ DoEffects = true; iWear.ItemID = 0x2789; NewName = "platemail kabuto"; }
					else if ( iWear.ItemID == 0x2789 || iWear.ItemID == 0x27D4 ){ DoEffects = true; iWear.ItemID = 0x2785; NewName = "platemail kabuto"; }
					else if ( iWear.ItemID == 0x2785 || iWear.ItemID == 0x27D0 ){ DoEffects = true; iWear.ItemID = 0x2784; NewName = "platemail jingasa"; }
					else if ( iWear.ItemID == 0x2784 ){ DoEffects = true; iWear.ItemID = 0x2778; NewName = "platemail kabuto"; }

					else if ( iWear.ItemID == 0x2FCB || iWear.ItemID == 0x3181 ){ DoEffects = true; iWear.ItemID = 0x1B72; NewName = "round shield"; }
					else if ( iWear.ItemID == 0x1B72 ){ DoEffects = true; iWear.ItemID = 0x1B74; NewName = "kite shield"; }
					else if ( iWear.ItemID == 0x1B74 || iWear.ItemID == 0x1B75 ){ DoEffects = true; iWear.ItemID = 0x1B76; NewName = "heater shield"; }
					else if ( iWear.ItemID == 0x1B76 || iWear.ItemID == 0x1B77 ){ DoEffects = true; iWear.ItemID = 0x1B7B; NewName = "metal shield"; }
					else if ( iWear.ItemID == 0x1B7B ){ DoEffects = true; iWear.ItemID = 0x1BC3; NewName = "chaos shield"; }
					else if ( iWear.ItemID == 0x1BC3 ){ DoEffects = true; iWear.ItemID = 0x1BC4; NewName = "order shield"; }
					else if ( iWear.ItemID == 0x1BC4 || iWear.ItemID == 0x1BC5 ){ DoEffects = true; iWear.ItemID = 0x2B01; NewName = "royal shield"; }
					else if ( iWear.ItemID == 0x2B01 ){ DoEffects = true; iWear.ItemID = 0x2B74; NewName = "champion shield"; }
					else if ( iWear.ItemID == 0x2B74 || iWear.ItemID == 0x316B ){ DoEffects = true; iWear.ItemID = 0x2B75; NewName = "jeweled shield"; }
					else if ( iWear.ItemID == 0x2B75 || iWear.ItemID == 0x316C ){ DoEffects = true; iWear.ItemID = 0x2FC8; NewName = "dark shield"; }
					else if ( iWear.ItemID == 0x2FC8 || iWear.ItemID == 0x317E ){ DoEffects = true; iWear.ItemID = 0x2FC9; NewName = "crested shield"; }
					else if ( iWear.ItemID == 0x2FC9 || iWear.ItemID == 0x317F ){ DoEffects = true; iWear.ItemID = 0x2FCA; NewName = "elven shield"; }
					else if ( iWear.ItemID == 0x2FCA || iWear.ItemID == 0x3180 ){ DoEffects = true; iWear.ItemID = 0x2FCB; NewName = "guardsman shield"; }

					else if ( iWear.ItemID == 0x2643 || iWear.ItemID == 0x2644 ){ DoEffects = true; iWear.ItemID = 0x1450; NewName = "bone gloves"; }
					else if ( iWear.ItemID == 0x1450 || iWear.ItemID == 0x1455 ){ DoEffects = true; iWear.ItemID = 0x1414; NewName = "platemail gloves"; }
					else if ( iWear.ItemID == 0x1414 || iWear.ItemID == 0x1418 ){ DoEffects = true; iWear.ItemID = 0x2B0C; NewName = "royal bracers"; }
					else if ( iWear.ItemID == 0x2B0C || iWear.ItemID == 0x2B0D ){ DoEffects = true; iWear.ItemID = 0x13eb; NewName = "ringmail gloves"; }
					else if ( iWear.ItemID == 0x13eb || iWear.ItemID == 0x13f2 ){ DoEffects = true; iWear.ItemID = 0x2643; NewName = "scalemail gloves"; }

					else if ( iWear.ItemID == 0x2641 || iWear.ItemID == 0x2642 ){ DoEffects = true; iWear.ItemID = 0x13BF; NewName = "chainmail tunic"; }
					else if ( iWear.ItemID == 0x13BF || iWear.ItemID == 0x13C4 ){ DoEffects = true; iWear.ItemID = 0x13EC; NewName = "ringmail tunic"; }
					else if ( iWear.ItemID == 0x13EC || iWear.ItemID == 0x13ED ){ DoEffects = true; iWear.ItemID = 0x1415; NewName = "platemail tunic"; }
					else if ( iWear.ItemID == 0x1415 || iWear.ItemID == 0x1416 ){ DoEffects = true; iWear.ItemID = 0x2B08; NewName = "royal tunic"; }
					else if ( iWear.ItemID == 0x2B08 || iWear.ItemID == 0x2B09 ){ DoEffects = true; iWear.ItemID = 0x277D; NewName = "platemail do"; }
					else if ( iWear.ItemID == 0x277D || iWear.ItemID == 0x27C8 ){ DoEffects = true; iWear.ItemID = 0x2641; NewName = "scalemail tunic"; }

					else if ( iWear.ItemID == 0x2B06 || iWear.ItemID == 0x2B07 ){ DoEffects = true; iWear.ItemID = 0x2647; NewName = "scalemail leggings"; }
					else if ( iWear.ItemID == 0x2647 || iWear.ItemID == 0x2648 ){ DoEffects = true; iWear.ItemID = 0x2788; NewName = "platemail suneate"; }
					else if ( iWear.ItemID == 0x2788 || iWear.ItemID == 0x27D3 ){ DoEffects = true; iWear.ItemID = 0x278D; NewName = "platemail haidate"; }
					else if ( iWear.ItemID == 0x278D || iWear.ItemID == 0x27D8 ){ DoEffects = true; iWear.ItemID = 0x13BE; NewName = "chainmail leggings"; }
					else if ( iWear.ItemID == 0x13BE || iWear.ItemID == 0x13C3 ){ DoEffects = true; iWear.ItemID = 0x13F0; NewName = "ringmail leggings"; }
					else if ( iWear.ItemID == 0x13F0 || iWear.ItemID == 0x13F1 ){ DoEffects = true; iWear.ItemID = 0x1411; NewName = "platemail leggings"; }
					else if ( iWear.ItemID == 0x1411 || iWear.ItemID == 0x141A ){ DoEffects = true; iWear.ItemID = 0x2B06; NewName = "royal leggings"; }

					else if ( iWear.ItemID == 0x2657 || iWear.ItemID == 0x2658 ){ DoEffects = true; iWear.ItemID = 0x13EE; NewName = "ringmail arms"; }
					else if ( iWear.ItemID == 0x13EE || iWear.ItemID == 0x13EF ){ DoEffects = true; iWear.ItemID = 0x1410; NewName = "platemail arms"; }
					else if ( iWear.ItemID == 0x1410 || iWear.ItemID == 0x1417 ){ DoEffects = true; iWear.ItemID = 0x2B0A; NewName = "royal arms"; }
					else if ( iWear.ItemID == 0x2B0A || iWear.ItemID == 0x2B0B ){ DoEffects = true; iWear.ItemID = 0x2780; NewName = "platemail kote"; }
					else if ( iWear.ItemID == 0x2780 || iWear.ItemID == 0x27CB ){ DoEffects = true; iWear.ItemID = 0x2657; NewName = "scalemail arms"; }

					else if ( iWear.ItemID == 0x2779 || iWear.ItemID == 0x27C4 ){ DoEffects = true; iWear.ItemID = 0x1413; NewName = "platemail gorget"; }
					else if ( iWear.ItemID == 0x1413 || iWear.ItemID == 0x264B || iWear.ItemID == 0x264C ){ DoEffects = true; iWear.ItemID = 0x2B0E; NewName = "royal gorget"; }
					else if ( iWear.ItemID == 0x2B0E || iWear.ItemID == 0x2B0F ){ DoEffects = true; iWear.ItemID = 0x2779; NewName = "platemail mempo"; }

					if ( DoEffects )
					{
						iWear.Name = Server.Misc.MaterialInfo.GetSpecialMaterialName( iWear ) + NewName;
						from.PlaySound( 0x2A );
						from.SendMessage( "The hammer magical transforms the armor." );
					}
					else
					{
						from.SendMessage( "This hammer is not really meant to do that." );
					}
				}
				else
				{
					from.SendMessage( "This hammer is not really meant to do that." );
				}
			}
		}

		public MagicHammer( Serial serial ) : base( serial )
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