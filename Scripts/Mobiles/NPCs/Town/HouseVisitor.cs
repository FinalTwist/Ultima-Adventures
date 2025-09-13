using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	public class HouseVisitor : Citizens
	{
		private DateTime m_NextChat;
		public DateTime NextChat{ get{ return m_NextChat; } set{ m_NextChat = value; } }

		[Constructable]
		public HouseVisitor() : base()
		{
			Direction = Direction.East;
			Blessed = true;
			NameHue = 1150;
			CantWalk = true;
			AI = AIType.AI_Melee;
		}

		public HouseVisitor( Serial serial ) : base( serial )
		{
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.TavernPatrons.RemoveSomeGear( this, true );
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

		public override void OnThink()
		{
			if ( DateTime.UtcNow >= m_NextChat && Server.Items.TavernTable.CountPatrons( this ) > 1 )
			{
				if ( Utility.RandomBool() ){ TavernPatrons.GetChatter( this ); }
				m_NextChat = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 45 ) ));
			}
		}
	}
}