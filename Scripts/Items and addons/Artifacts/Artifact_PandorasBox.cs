using System;
using Server;
using Server.Mobiles;


namespace Server.Items
{
	public enum PandoraEffect
	{
		Charges
	}


    public class PandorasBox : Item
	{
		private PandoraEffect m_PandoraEffect;
		private int m_Charges;


		[CommandProperty( AccessLevel.GameMaster )]
		public PandoraEffect Effect
		{
			get{ return m_PandoraEffect; }
			set{ m_PandoraEffect = value; InvalidateProperties(); }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}


        [Constructable]
        public PandorasBox() : base(0x2824)
		{
            Name = "Pandora's Box";
			Hue = 0xA3E;
			Charges = 200;
			Weight = 5.0;
		}


		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060741, m_Charges.ToString() );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
            list.Add( 1049644, "Magically Access Your Bank Box");
        }


		public void ConsumeCharge( Mobile from )
		{
			--Charges;


			if ( Charges == 0 )
			{
				from.SendLocalizedMessage( 1019073 ); // This item is out of charges.
				Item Mybox = new MetalBox();
			  	Mybox.Hue = 0x492;
			  	from.AddToBackpack ( Mybox );
				this.Delete();
			}
		}


        public override void OnDoubleClick(Mobile from)
		{
			if ( Charges > 0 )
			{
				ConsumeCharge( from );
				BankBox box = from.BankBox;
				if (box != null)
				{
					//box.GumpID = BaseContainer.BankGump( from, box );
					box.Open();
				}
			}
        }


        public PandorasBox( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_PandoraEffect );
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
					m_PandoraEffect = (PandoraEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();


					break;
				}
			}
	    }
    }
}