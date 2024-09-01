using System;

namespace Server.Engines.BulkOrders
{
	public class BOBSmallEntry
	{
		private Type m_ItemType;
		private bool m_RequireExceptional;
		private BODType m_DeedType;
		private BulkMaterialType m_Material;
		private int m_AmountCur, m_AmountMax;
		private int m_Number;
		private int m_Graphic;
		private int m_Price;

		public Type ItemType{ get{ return m_ItemType; } }
		public bool RequireExceptional{ get{ return m_RequireExceptional; } }
		public BODType DeedType{ get{ return m_DeedType; } }
		public BulkMaterialType Material{ get{ return m_Material; } }
		public int AmountCur{ get{ return m_AmountCur; } }
		public int AmountMax{ get{ return m_AmountMax; } }
		public int Number{ get{ return m_Number; } }
		public int Graphic{ get{ return m_Graphic; } }
		public int Price{ get{ return m_Price; } set{ m_Price = value; } }

		public Item Reconstruct()
		{
			SmallBOD bod = null;

			if ( m_DeedType == BODType.Smith )
				bod = new SmallSmithBOD( m_AmountCur, m_AmountMax, m_ItemType, m_Number, m_Graphic, m_RequireExceptional, m_Material );
			else if ( m_DeedType == BODType.Tailor )
				bod = new SmallTailorBOD( m_AmountCur, m_AmountMax, m_ItemType, m_Number, m_Graphic, m_RequireExceptional, m_Material );
			else if ( m_DeedType == BODType.Carpenter )
				bod = new SmallCarpenterBOD( m_AmountCur, m_AmountMax, m_ItemType, m_Number, m_Graphic, m_RequireExceptional, m_Material );
			else if ( m_DeedType == BODType.Fletcher )
				bod = new SmallFletcherBOD( m_AmountCur, m_AmountMax, m_ItemType, m_Number, m_Graphic, m_RequireExceptional, m_Material );

			return bod;
		}

		public BOBSmallEntry( SmallBOD bod )
		{
			m_ItemType = bod.Type;
			m_RequireExceptional = bod.RequireExceptional;

			if ( bod is SmallTailorBOD )
				m_DeedType = BODType.Tailor;
			else if ( bod is SmallSmithBOD )
                m_DeedType = BODType.Smith;
            else if ( bod is SmallCarpenterBOD )
                m_DeedType = BODType.Carpenter;
            else if ( bod is SmallFletcherBOD )
                m_DeedType = BODType.Fletcher;

            m_Material = bod.Material;
			m_AmountCur = bod.AmountCur;
			m_AmountMax = bod.AmountMax;
			m_Number = bod.Number;
			m_Graphic = bod.Graphic;
		}

		public BOBSmallEntry( GenericReader reader )
		{
			int version = reader.ReadEncodedInt();
            string type = reader.ReadString();

            if (type != null)
                m_ItemType = ScriptCompiler.FindTypeByFullName(type);

            m_RequireExceptional = reader.ReadBool();

            m_DeedType = (BODType)reader.ReadEncodedInt();

            m_Material = (BulkMaterialType)reader.ReadEncodedInt();
            m_AmountCur = reader.ReadEncodedInt();
            m_AmountMax = reader.ReadEncodedInt();
            m_Number = reader.ReadEncodedInt();
            m_Graphic = reader.ReadEncodedInt();
            m_Price = reader.ReadEncodedInt();

            switch (version)
            {
                case 2:
					break;

                case 1:
					{
                        if (m_DeedType == BODType.Tailor && (int)m_Material == 7)
                        {
                            m_Material -= 7;
                        }
                        break;
                    }

                case 0:
                    {
                        switch (m_DeedType)
                        {
                            case BODType.Tailor:
                                if (m_Material > 0)
                                    m_Material += 7; // Number of Metals added ahead of it in the Enum
                                break;

                            case BODType.Carpenter:
                            case BODType.Fletcher:
                                m_Material += 7; // Number of Metals added ahead of it in the Enum
                                m_Material += 8; // Number of Leathers added ahead of it in the Enum
                                break;
                        }

                        break;
                    }
            }
        }

		public void Serialize( GenericWriter writer )
		{
			writer.WriteEncodedInt( 2 ); // version

			writer.Write( m_ItemType == null ? null : m_ItemType.FullName );

			writer.Write( (bool) m_RequireExceptional );

			writer.WriteEncodedInt( (int) m_DeedType );
			writer.WriteEncodedInt( (int) m_Material );
			writer.WriteEncodedInt( (int) m_AmountCur );
			writer.WriteEncodedInt( (int) m_AmountMax );
			writer.WriteEncodedInt( (int) m_Number );
			writer.WriteEncodedInt( (int) m_Graphic );
			writer.WriteEncodedInt( (int) m_Price );
		}
	}
}