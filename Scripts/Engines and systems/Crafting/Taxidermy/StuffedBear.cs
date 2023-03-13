using System;
using Server.Network;
using Server.Items;
using Server;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	[Furniture]
	[Flipable(0x127D, 0x12B4)]
    public class StuffedBear : Item
	{
		public string AnimalKiller;
		public string AnimalWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Animal_Killer { get { return AnimalKiller; } set { AnimalKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Animal_Where { get { return AnimalWhere; } set { AnimalWhere = value; InvalidateProperties(); } }

        [Constructable]
        public StuffedBear() : base( 0x127D )
		{
            Name = "stuffed bear";
			Weight = 1.0;
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( AnimalWhere != "" && AnimalWhere != null ){ list.Add( 1070722, AnimalWhere ); }
			if ( AnimalKiller != "" && AnimalKiller != null ){ list.Add( 1049644, AnimalKiller ); }
        }

        public StuffedBear( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( AnimalKiller );
            writer.Write( AnimalWhere );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            AnimalKiller = reader.ReadString();
            AnimalWhere = reader.ReadString();
	    }
    }
}