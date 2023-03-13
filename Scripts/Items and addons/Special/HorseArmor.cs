using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class HorseArmor : Item
	{
		public string ArmorMaterial;

		[CommandProperty(AccessLevel.Owner)]
		public string Armor_Material { get { return ArmorMaterial; } set { ArmorMaterial = value; InvalidateProperties(); } }

		[Constructable]
		public HorseArmor() : base( 0x040A )
		{
			Weight = 25.0;
			Name = "horse barding";
			Hue = MaterialInfo.GetMaterialColor( "silver", "classic", 0 );

			int chance = 0;
			double chancetest = Utility.RandomDouble();
			
            if (chancetest < 0.50 )
                chance = 3;
            else if (chancetest < 0.70)
                chance = 7;
            else if (chancetest < 0.85)
                chance = 9;
            else if (chancetest < 0.95)
                chance = 11;
            else if (chancetest >= 0.95)
                chance = 14;
            
            switch ( Utility.Random( chance ) )
            {
                case 0: ArmorMaterial = "Iron"; break;
                case 1: ArmorMaterial = "Dull Copper"; break;
                case 2: ArmorMaterial = "Shadow Iron"; break;
                case 3: ArmorMaterial = "Copper"; break;
                case 4: ArmorMaterial = "Bronze"; break;
                case 5: ArmorMaterial = "Gold"; break;
                case 6: ArmorMaterial = "Agapite"; break;
                case 7: ArmorMaterial = "Verite"; break;
                case 8: ArmorMaterial = "Valorite"; break;
                case 9: ArmorMaterial = "Nepturite"; break;
                case 10: ArmorMaterial = "Obsidian"; break;
                case 11: ArmorMaterial = "Steel"; break;
                case 12: ArmorMaterial = "Brass"; break;
                case 13: ArmorMaterial = "Mithril"; break;   
            }

			if ( ArmorMaterial == "Dull Copper" ){ 		Hue = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); }
			else if ( ArmorMaterial == "Shadow Iron" ){ Hue = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); }
			else if ( ArmorMaterial == "Copper" ){ 		Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); }
			else if ( ArmorMaterial == "Bronze" ){ 		Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); }
			else if ( ArmorMaterial == "Gold" ){ 		Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); }
			else if ( ArmorMaterial == "Agapite" ){ 	Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); }
			else if ( ArmorMaterial == "Verite" ){ 		Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); }
			else if ( ArmorMaterial == "Valorite" ){ 	Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); }
			else if ( ArmorMaterial == "Nepturite" ){ 	Hue = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); }
			else if ( ArmorMaterial == "Obsidian" ){ 	Hue = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); }
			else if ( ArmorMaterial == "Steel" ){ 		Hue = MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); }
			else if ( ArmorMaterial == "Brass" ){ 		Hue = MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); }
			else if ( ArmorMaterial == "Mithril" ){ 	Hue = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ); }
			else if ( ArmorMaterial == "Xormite" ){ 	Hue = MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ); }
			else if ( ArmorMaterial == "Dwarven" ){ 	Hue = MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ); }
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, ArmorMaterial );
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
				from.SendMessage( "Which horse do you want to use this on?" );
				t = new HorseTarget( this, ArmorMaterial );
				from.Target = t;
			}
		}

		private class HorseTarget : Target
		{
			private HorseArmor m_Horse;
			private string m_ArmorMaterial;

			public HorseTarget( HorseArmor armor, string metal ) : base( 8, false, TargetFlags.None )
			{
				m_Horse = armor;
				m_ArmorMaterial = metal;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					Mobile iArmor = targeted as Mobile;

					if ( iArmor is BaseCreature )
					{
						BaseCreature xArmor = (BaseCreature)iArmor;

						if ( ( xArmor is Horse || xArmor is ZebraRiding || xArmor is Zebra || xArmor is FireSteed || xArmor is Nightmare || xArmor is AncientNightmareRiding) && xArmor.ControlMaster == from && xArmor is BaseMount )
						{
							BaseMount mArmor = (BaseMount)xArmor;

							if ( MyServerSettings.ClientVersion() )
							{
								mArmor.Body = 587;
								mArmor.ItemID = 587;
							}
							else
							{
								mArmor.Body = 0xE2;
								mArmor.ItemID = 0x3EA0;
							}

							int mod = 5;

							if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ) ){ 			mod=mod-1; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ) ){ 	mod=mod-2; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "copper", "classic", 0 ) ){ 			mod=mod-3; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ) ){ 			mod=mod-4; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "gold", "classic", 0 ) ){ 			mod=mod-5; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ) ){ 		mod=mod-6; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "verite", "classic", 0 ) ){ 			mod=mod-7; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ) ){ 		mod=mod-8; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ) ){ 		mod=mod-9; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ) ){ 		mod=mod-10; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "steel", "classic", 0 ) ){ 			mod=mod-11; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "brass", "classic", 0 ) ){ 			mod=mod-12; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ) ){ 		mod=mod-13; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ) ){ 		mod=mod-14; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ) ){ 		mod=mod-15; }
							else if ( xArmor.Hue == MaterialInfo.GetMaterialColor( "silver", "classic", 0 ) ){ 			mod=mod-0; }

							if ( m_ArmorMaterial == "Dull Copper" ){ 		mod=mod+1; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Shadow Iron" ){ 	mod=mod+2; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Copper" ){ 		mod=mod+3; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Bronze" ){ 		mod=mod+4; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Gold" ){ 			mod=mod+5; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Agapite" ){ 		mod=mod+6; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Verite" ){ 		mod=mod+7; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Valorite" ){ 		mod=mod+8; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Nepturite" ){ 	mod=mod+9; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Obsidian" ){ 		mod=mod+10; 	xArmor.Hue = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Steel" ){ 		mod=mod+11; 	xArmor.Hue = MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Brass" ){ 		mod=mod+12; 	xArmor.Hue = MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Mithril" ){ 		mod=mod+13; 	xArmor.Hue = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Xormite" ){ 		mod=mod+14; 	xArmor.Hue = MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ); }
							else if ( m_ArmorMaterial == "Dwarven" ){ 		mod=mod+15; 	xArmor.Hue = MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ); }
							else { 											mod=mod+0; 		xArmor.Hue = MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); }

							xArmor.SetStr( xArmor.RawStr+mod );
							xArmor.SetDex( xArmor.RawDex+mod );
							xArmor.SetInt( xArmor.RawInt+mod );

							xArmor.SetHits( xArmor.HitsMax+mod );

							xArmor.SetDamage( xArmor.DamageMin+mod, xArmor.DamageMax+mod );

							xArmor.SetResistance( ResistanceType.Physical, xArmor.PhysicalResistance+mod );

							xArmor.SetSkill( SkillName.MagicResist, xArmor.Skills[SkillName.MagicResist].Base+mod );
							xArmor.SetSkill( SkillName.Tactics, xArmor.Skills[SkillName.Tactics].Base+mod );
							xArmor.SetSkill( SkillName.Wrestling, xArmor.Skills[SkillName.Wrestling].Base+mod );

							from.RevealingAction();
							from.PlaySound( 0x0AA );

							m_Horse.Consume();
						}
						else
						{
							from.SendMessage( "This armor is only for horses you own." );
						}
					}
					else
					{
						from.SendMessage( "This armor is only for horses you own." );
					}
				}
			}
		}

		public static void DropArmor( BaseCreature bc )
		{
			HorseArmor armor = new HorseArmor();
			armor.Hue = 0;
			armor.ArmorMaterial = null;
			if ( bc.Hue == MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ) ){ 			armor.Hue = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); 	armor.ArmorMaterial = "Dull Copper"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ) ){ 	armor.Hue = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); 	armor.ArmorMaterial = "Shadow Iron"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "copper", "classic", 0 ) ){ 			armor.Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); 		armor.ArmorMaterial = "Copper"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ) ){ 			armor.Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); 		armor.ArmorMaterial = "Bronze"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "gold", "classic", 0 ) ){ 			armor.Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); 			armor.ArmorMaterial = "Gold"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ) ){ 		armor.Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); 		armor.ArmorMaterial = "Agapite"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "verite", "classic", 0 ) ){ 			armor.Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); 		armor.ArmorMaterial = "Verite"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ) ){ 		armor.Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); 		armor.ArmorMaterial = "Valorite"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ) ){ 		armor.Hue = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); 	armor.ArmorMaterial = "Nepturite"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ) ){ 		armor.Hue = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); 		armor.ArmorMaterial = "Obsidian"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "steel", "classic", 0 ) ){ 			armor.Hue = MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); 		armor.ArmorMaterial = "Steel"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "brass", "classic", 0 ) ){ 			armor.Hue = MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); 		armor.ArmorMaterial = "Brass"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ) ){ 		armor.Hue = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ); 		armor.ArmorMaterial = "Mithril"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ) ){ 		armor.Hue = MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ); 		armor.ArmorMaterial = "Xormite"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ) ){ 		armor.Hue = MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ); 		armor.ArmorMaterial = "Dwarven"; }
			else if ( bc.Hue == MaterialInfo.GetMaterialColor( "silver", "classic", 0 ) ){ 			armor.Hue = MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); 		armor.ArmorMaterial = "Silver"; }

			if ( armor.ArmorMaterial != null ){ bc.AddItem( armor ); }
			else { armor.Delete(); }
		}

		public HorseArmor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
            writer.Write( ArmorMaterial );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            ArmorMaterial = reader.ReadString();
		}
	}
}