using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class UnidentifiedItem : LockableContainer
	{
		public string SkillRequired;

		[CommandProperty(AccessLevel.Owner)]
		public string Skill_Required { get { return SkillRequired; } set { SkillRequired = value; InvalidateProperties(); } }

		public string VendorCanID;

		[CommandProperty(AccessLevel.Owner)]
		public string Vendor_CanID { get { return VendorCanID; } set { VendorCanID = value; InvalidateProperties(); } }

		public int IDAttempt;

		[CommandProperty(AccessLevel.Owner)]
		public int ID_Attempt { get { return IDAttempt; } set { IDAttempt = value; InvalidateProperties(); } }

		[Constructable]
		public UnidentifiedItem() : base( 0x9A8 )
		{
			Name = "unknown item";
			Locked = true;
			LockLevel = 1000;
			MaxLockLevel = 1000;
			RequiredSkill = 1000;
			Weight = 0.1;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
			string sSkillRequired = "";
			string sVendorCanID = "";

			if ( VendorCanID == "Blacksmith" ){ sVendorCanID = "Blacksmith, Weaponsmith, or Armorer Can Identify"; }
			else if ( VendorCanID == "Jeweler" ){ sVendorCanID = "Jeweler Can Identify"; }
			else if ( VendorCanID == "Tailor" ){ sVendorCanID = "Tailor or Weaver Can Identify"; }
			else if ( VendorCanID == "Leatherworker" ){ sVendorCanID = "Leatherworker or Tanner Can Identify"; }
			else if ( VendorCanID == "Bowyer" ){ sVendorCanID = "Bowyer Can Identify"; }
			else if ( VendorCanID == "Sage" ){ sVendorCanID = "Sage Can Identify"; }
			else if ( VendorCanID == "Bard" ){ sVendorCanID = "Bard Can Identify"; }
			else if ( VendorCanID == "Carpenter" ){ sVendorCanID = "Carpenter Can Identify"; }
			else { VendorCanID = "Sage"; sVendorCanID = "Sage Can Identify"; SkillRequired = "ItemID"; }

			if ( SkillRequired == "ItemID" ){ sSkillRequired = "Use Item Identification To Determine What It Is"; }
			else if ( SkillRequired == "ArmsLore" ){ sSkillRequired = "Use Arms Lore To Determine What It Is"; }

            base.AddNameProperties(list);
			list.Add( 1070722, sVendorCanID);
			list.Add( 1049644, sSkillRequired); // PARENTHESIS
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
			{
				from.SendMessage( "That cannot move so you cannot identify it." );
				return;
			}
			else if ( !IsChildOf( from.Backpack ) && Server.Misc.MyServerSettings.IdentifyItemsOnlyInPack() ) 
			{
				from.SendMessage( "This must be in your backpack to identify." );
				return;
			}
			else if ( !from.InRange( this.GetWorldLocation(), 3 ) )
			{
				from.SendMessage( "You will need to get closer to identify that." );
				return;
			}
			else
			{
				if ( SkillRequired == "ArmsLore" )
					Server.SkillHandlers.ArmsLore.IDItem( from, this, this, false );

				else
					Server.Items.ItemIdentification.IDItem( from, this, this, false );
			}
		}

		public UnidentifiedItem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( SkillRequired );
            writer.Write( VendorCanID );
            writer.Write( IDAttempt );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            SkillRequired = reader.ReadString();
            VendorCanID = reader.ReadString();
            IDAttempt = reader.ReadInt();
		}
	}
}