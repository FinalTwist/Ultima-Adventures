using System;
using Server;

namespace Server.Items
{
	public class FrankenBrain : FrankenItem
	{
		public int BrainLevel;
		[CommandProperty(AccessLevel.Owner)]
		public int Brain_Level { get { return BrainLevel; } set { BrainLevel = value; InvalidateProperties(); } }

		public string BrainSource;
		[CommandProperty(AccessLevel.Owner)]
		public string Brain_Source { get { return BrainSource; } set { BrainSource = value; InvalidateProperties(); } }

		[Constructable]
		public FrankenBrain()
		{
			Name = "fresh brain";
			Weight = 2.0;
			ItemID = 0x25E2;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Level: " + BrainLevel + "" );
            list.Add( 1049644, "From " + BrainSource + "" );
        } 

		public FrankenBrain( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( BrainLevel );
            writer.Write( BrainSource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			BrainLevel = reader.ReadInt();
			BrainSource = reader.ReadString();
		}
	}
}