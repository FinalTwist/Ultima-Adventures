using System;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;
using Server.Misc;
using Server.Targeting;

namespace Server.Items
{
	[FlipableAttribute( 0x0DF2, 0x0DF3 )]
	public class WandOfColors : Item
	{
		[Constructable]
		public WandOfColors() : base( 0x0DF2 )
		{
			Name = "wand of colors";
			Weight = 1.0;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Flip Wand To Reverse Order");
            list.Add( 1049644, "Change Hue By A Value Up Or Down");
        }

		public override void OnDoubleClick( Mobile m )
		{
			m.Target = new InternalTarget( this );
			m.SendMessage( "What target do you want to change the color?" );
		}

		private class InternalTarget : Target
		{
			private WandOfColors m_WandOfColors;

			public InternalTarget( WandOfColors wand ) :  base ( 8, false, TargetFlags.None )
			{
				m_WandOfColors = wand;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					if ( m_WandOfColors.ItemID == 0x0DF2 ){ ((Mobile)targeted).Hue = ((Mobile)targeted).Hue + 1; }
					else { ((Mobile)targeted).Hue = ((Mobile)targeted).Hue - 1; }
					if ( ((Mobile)targeted).Hue < 1 ){ ((Mobile)targeted).Hue = 0; }
				}
				else if ( targeted is Item )
				{
					if ( m_WandOfColors.ItemID == 0x0DF2 ){ ((Item)targeted).Hue = ((Item)targeted).Hue + 1; }
					else { ((Item)targeted).Hue = ((Item)targeted).Hue - 1; }
					if ( ((Item)targeted).Hue < 1 ){ ((Item)targeted).Hue = 0; }
				}
			}
        }

		public WandOfColors(Serial serial) : base(serial)
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