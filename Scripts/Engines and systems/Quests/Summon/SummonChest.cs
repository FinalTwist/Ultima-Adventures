using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using Server.Misc;

namespace Server.Items
{
	public class SummonChest : MetalGoldenChest
	{
		public string Prisoner;
		[CommandProperty( AccessLevel.GameMaster )]
		public string p_Prisoner { get{ return Prisoner; } set{ Prisoner = value; } }

		[Constructable]
		public SummonChest( Mobile killer ) : base()
		{
			Name = "Metal Chest";
			ItemID = Utility.RandomList( 0x9AB, 0xE40, 0xE41, 0xE7C );
			Hue = Utility.RandomList( 0x961, 0x962, 0x963, 0x964, 0x965, 0x966, 0x967, 0x968, 0x969, 0x96A, 0x96B, 0x96C, 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E, 0x4AA );
			if ( killer is PlayerMobile ){ ContainerFunctions.FillTheContainer( 12, this, killer ); }
			TrapType = TrapType.None;
			Locked = false;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From Slaying " + Prisoner);
            list.Add( 1049644, "Released From Their Magic Prison");
        }

		public SummonChest( Serial serial ) : base( serial )
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