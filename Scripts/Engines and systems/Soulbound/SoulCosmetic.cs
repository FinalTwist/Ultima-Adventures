using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class SoulCosmetic : Item
	{
		private Body m_Body;
		private int m_BaseSoundID;

		public Body Body {
			get{return m_Body;}
			set{m_Body = value;}
		}

		public int BaseSoundID {
			get{return m_BaseSoundID;}
			set{m_BaseSoundID = value;}
		}

		[Constructable]
		public SoulCosmetic() : this( 1 )
		{
		}

		[Constructable]
		public SoulCosmetic( int amount )
		{
			
			Amount = amount;
			ItemID =  0x20E0;
			Light = LightType.Circle150;
			Stackable = false;
		}

		public SoulCosmetic( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_Body );
			writer.Write( (int) m_BaseSoundID );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Body = reader.ReadInt();
			m_BaseSoundID = reader.ReadInt();
			Light = LightType.Circle150;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( 1060658, "{0}\t{1}", "Soul", "(Cosmetic)" );  // ~1_val~: ~2_val~
		}

		public override void OnDoubleClick( Mobile mobile )
		{
			if (mobile is PlayerMobile && ((PlayerMobile)mobile).SoulBound) {
				if (mobile.Body == this.Body) {
					mobile.Body = ((PlayerMobile)mobile).OriginalBody;
				} else if (Body != null && BaseSoundID > 0) {
					mobile.Body = this.Body;
					mobile.SendSound(this.BaseSoundID);
				}
			} else {
				mobile.SendMessage("You are not elligible for this effect.");
			}
		}
	}
}