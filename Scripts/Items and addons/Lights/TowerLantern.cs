using System;
using Server.Network;
using Server.Gumps;
using Server.Multis;
using System.Collections.Generic;
using Server.ContextMenus; 

namespace Server.Items
{
	public class TowerLantern : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x24BF; } }
		
		public override int UnlitItemID{ get { return 0x24C0; } }

		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public TowerLantern() : base( 0x24C0 )
		{
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Weight = 10;
			Light = LightType.Circle300;
			Burning = false;
			Name = "tower lantern";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public TowerLantern( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
}