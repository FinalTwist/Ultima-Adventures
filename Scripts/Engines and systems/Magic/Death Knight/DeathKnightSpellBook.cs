using System;
using Server.Network;
using Server.Gumps;
using Server.Spells;

namespace Server.Items
{
	public class DeathKnightSpellbook : Spellbook
	{
		public Mobile owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		public override SpellbookType SpellbookType{ get{ return SpellbookType.DeathKnight; } }
		public override int BookOffset{ get{ return 750; } }
		public override int BookCount{ get{ return 15; } }

		[Constructable]
		public DeathKnightSpellbook( ulong content, Mobile gifted ) : base( content, 0x27B6 )
		{
			Hue = 0x969;

			owner = gifted;

			string sEvil = "Evil";
			switch ( Utility.RandomMinMax( 0, 7 ) ) 
			{
				case 0: sEvil = "Evil";			break;
				case 1: sEvil = "Vile";			break;
				case 2: sEvil = "Sinister";		break;
				case 3: sEvil = "Wicked";		break;
				case 4: sEvil = "Corrupt";		break;
				case 5: sEvil = "Hateful";		break;
				case 6: sEvil = "Malevolent";	break;
				case 7: sEvil = "Nefarious";	break;
			}

			switch ( Utility.RandomMinMax( 1, 2 ) ) 
			{
				case 1: this.Name = "Kas' Book of " + sEvil + " Knights";	break;
				case 2: this.Name = "Kas' Tome of " + sEvil + " Knights";	break;
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
				from.CloseGump( typeof( DeathKnightSpellbookGump ) );
				from.SendGump( new DeathKnightSpellbookGump( from, this, 1 ) );
			}
			else from.SendLocalizedMessage(500207); // The spellbook must be in your backpack (and not in a container within) to open.
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( owner != null ){ list.Add( 1070722, "For " + owner.Name + "" ); }
        }

		public DeathKnightSpellbook( Serial serial ) : base( serial )
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
