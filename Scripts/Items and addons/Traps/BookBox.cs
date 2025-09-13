using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class BookBox : LockableContainer
	{
		private static int[] m_ItemIDs = new int[]
		{
			0x9A8, 0xE80
		};

		[Constructable]
		public BookBox() : base( Utility.RandomList( m_ItemIDs ) )
		{
			Name = "magical box";
			Locked = true;
			LockLevel = 1000;
			MaxLockLevel = 1000;
			RequiredSkill = 1000;
			Weight = 40.0;
			Hue = 0x495;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Cursed Box with Books of Power");
			list.Add( 1049644, "Give to a Wizard or Knight to Remove the Curse"); // PARENTHESIS
        }

		public BookBox( Serial serial ) : base( serial )
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