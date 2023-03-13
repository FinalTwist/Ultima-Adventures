using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Misc;

namespace Server.Items
{
	public class BoxOfAtonement : Item
	{
		[Constructable]
		public BoxOfAtonement() : base( 0x9A8 )
		{
			Name = "Box of Atonement";
			Hue = 0x497;
			Movable = false;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Gold )
			{
				int nPenalty = AssassinFunctions.QuestFailure( from );

				if ( dropped.Amount == nPenalty )
				{
					CharacterDatabase.ClearQuestInfo( from, "AssassinQuest" );
					from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Your failure in this task has been forgiven.", from.NetState);
					dropped.Delete();
				}
				else
				{
					from.AddToBackpack ( dropped );
				}
			}
			else
			{
				from.AddToBackpack ( dropped );
			}
			return true;
		}			

		public BoxOfAtonement( Serial serial ) : base( serial )
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