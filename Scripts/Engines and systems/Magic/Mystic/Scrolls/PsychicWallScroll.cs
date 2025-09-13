using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PsychicWallScroll : SpellScroll
	{
		public Mobile owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		[Constructable]
		public PsychicWallScroll() : base( 256, 0x2DD )
		{
			Name = "psychic wall";
        }

		public PsychicWallScroll( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from != owner )
			{
				from.SendMessage( "The parchement crumbles in your hand." );
				this.Delete();
			}
			else
			{
				from.SendMessage( "These writings need to be added to a monk's tome." );
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( owner != null ){ list.Add( 1070722, "Written by " + owner.Name + "" ); }
        }

		public override bool OnDragLift( Mobile from )
		{
			if ( from != owner )
			{
				from.SendMessage( "The parchement crumbles in your hand." );
				this.Delete();
			}

			return true;
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