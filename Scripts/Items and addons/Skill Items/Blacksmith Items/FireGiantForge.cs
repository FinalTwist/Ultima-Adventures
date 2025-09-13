using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public enum DrainCauldron
	{
		Charges
	}

    public class FireGiantForge : Item
	{
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

        [Constructable]
        public FireGiantForge() : base( 0x1AF0 )
		{
            Name = "smoldering cauldron";
			Charges = 50;
			Weight = 20.0;
			Light = LightType.Circle225;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
			string uses = m_Charges.ToString() + " Uses Remaining";
				if ( m_Charges == 1 ){ uses = m_Charges.ToString() + " Use Remaining"; }
				if ( m_Charges < 1 ){ uses = "Useless"; }
            base.AddNameProperties(list);
			list.Add( 1070722, "Fire Giant Forge");
            list.Add( 1049644, uses );
        }

		public static void ConsumeCharge( FireGiantForge kettle )
		{
			--kettle.Charges;

			if ( kettle.Charges < 1 )
			{
				kettle.Light = LightType.Empty;
				kettle.ItemID = 0x09ED;
				kettle.Name = "cold cauldron";
			}
		}

        public FireGiantForge( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Charges = (int)reader.ReadInt();
					break;
				}
			}
	    }
    }
}