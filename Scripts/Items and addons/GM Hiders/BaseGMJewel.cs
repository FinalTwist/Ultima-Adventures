namespace Server.Items
{
    public abstract class BaseGMJewel : Item
	{
		private AccessLevel m_AccessLevel;

		[CommandProperty( AccessLevel.GameMaster )]
		public AccessLevel AccessLevel{ get{ return m_AccessLevel; } set{ m_AccessLevel = value; } }

		public BaseGMJewel( AccessLevel level, int hue, int itemID ) : base( itemID )
		{
			Stackable = false;
			Hue = hue;
			Weight = 1.0;
			Movable = false;
			LootType = LootType.Newbied;
	     				
			m_AccessLevel = level;
		}

		public BaseGMJewel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_AccessLevel );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_AccessLevel = (AccessLevel)reader.ReadInt();
					break;
				}
			}
		}

		public bool Validate()
		{
			object root = RootParent;

			if ( root is Mobile && ((Mobile)root).AccessLevel < m_AccessLevel )
			{
				Delete();
				return false;
			}

			return true;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( Validate() )
				base.OnSingleClick( from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel < AccessLevel.GameMaster )
				from.SendMessage( "You may not use this. Please Page reporting that you found this item" );

			//return ( from.AccessLevel >= m_AccessLevel );
		
		}

		public override bool VerifyMove( Mobile from )
		{
			return ( from.AccessLevel >= m_AccessLevel );
		}

		
	}
}
