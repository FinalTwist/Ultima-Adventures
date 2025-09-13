/* Created by Hammerhand */

using System;
using Server;

namespace Server.Items
{
	public class CharredHead : Item
	{

		[Constructable]
		public CharredHead(): base( 0x1CE9 )
		{
            Hue = 2949;
            Name = "The charred head of a FireLord";
            Weight = 1;
		}

        public CharredHead(Serial serial): base( serial )
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
