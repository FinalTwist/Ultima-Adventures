using System;
using Server.Network;
using Server.Gumps;
using Server.Spells;

namespace Server.Items
{
	public class HolyManSpellbook : Spellbook
	{
		public Mobile owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		public override SpellbookType SpellbookType{ get{ return SpellbookType.HolyMan; } }
		public override int BookOffset{ get{ return 770; } }
		public override int BookCount{ get{ return 15; } }

		[Constructable]
		public HolyManSpellbook( ulong content, Mobile gifted ) : base( content, 0x22C5 )
		{
			Hue = 0x430;
			Layer = Layer.Invalid;

			owner = gifted;

			string sGood = "Holy";
			switch ( Utility.RandomMinMax( 0, 7 ) ) 
			{
				case 0: sGood = "Holy";			break;
				case 1: sGood = "Divine";		break;
				case 2: sGood = "Spiritual";	break;
				case 3: sGood = "Faithful";		break;
				case 4: sGood = "Angelic";		break;
				case 5: sGood = "Virtuous";		break;
				case 6: sGood = "Blessed";		break;
				case 7: sGood = "Devout";		break;
			}

			switch ( Utility.RandomMinMax( 1, 2 ) ) 
			{
				case 1: this.Name = "Book of " + sGood + " Prayers";	break;
				case 2: this.Name = "Tome of " + sGood + " Prayers";	break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			Container pack = from.Backpack;

			if ( owner != from )
			{
				from.SendMessage( "These pages appears as scribbles to you." );
			}
			else if ( Parent == from || ( pack != null && Parent == pack ) )
			{
				from.SendSound( 0x55 );
				from.CloseGump( typeof( HolyManSpellbookGump ) );
				from.SendGump( new HolyManSpellbookGump( from, this, 1 ) );
			}
			else from.SendLocalizedMessage(500207); // The spellbook must be in your backpack (and not in a container within) to open.
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( owner != null ){ list.Add( 1070722, "For " + owner.Name + "" ); }
        }

		public HolyManSpellbook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (Mobile)owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
		}
	}
}
