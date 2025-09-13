using System;
using Server; 
using Server.Network;
using System.Collections;
using System.Globalization;
using Server.Items;
using Server.Misc;
using Server.Gumps;

namespace Server.Items
{
	public class EssenceOrb : Item
	{
		[Constructable]
		public EssenceOrb() : base( 0x4FD6 )
		{
			Weight = 5.0;
			Name = "Essence";
			Light = LightType.Circle150;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( m_Owner != null ){ list.Add( 1049644, "Belongs to " + m_Owner.Name + "" ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
			bool CanUse = false;

			if ( m_Owner == null ){ CanUse = true; }
			else if ( m_Owner == from ){ CanUse = true; }

			if ( CanUse == true )
			{
				m_Owner = from;
				m_OriginalName = "Essence of " + m_Owner.Name;
				int colorChange = 0xB92; 

				if ( m_Status > 0 ) // TURN IT OFF
				{
					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
					from.Hue = DB.CharHue;
					from.HairHue = DB.CharHairHue;
					from.FacialHairHue = DB.CharHairHue;
					this.Name = m_MorphName;
					this.Hue = m_MorphHue;
					m_Status = 0;
				}
				else // TURN IT ON
				{
					TurnOtherOrbsOff( from, this );
					this.Name = m_OriginalName;
					this.Hue = 0xB92;
					from.Hue = m_MorphHue;
					from.HairHue = m_MorphHairHue;
					from.FacialHairHue = m_MorphHairHue;
					m_Status = 1;
				}

				from.PlaySound( 0x659 );
				from.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Waist );
				from.SendMessage( "Your flesh and hair magically change color." );
			}
			else
			{
				from.SendMessage( "You cannot use this magical orb as it belongs to another." );
			}
		}

		public static void TurnOtherOrbsOff( Mobile from, EssenceOrb orb )
		{
			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is EssenceOrb && item != orb )
			{
				if ( ((EssenceOrb)item).m_Owner == from )
				{
					item.Name = ((EssenceOrb)item).m_MorphName;
					item.Hue = ((EssenceOrb)item).m_MorphHue;
					((EssenceOrb)item).m_Status = 0;
				}
			}
		}

		public Mobile m_Owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile mOwner { get{ return m_Owner; } set{ m_Owner = value; InvalidateProperties(); } }

		public int m_MorphHue;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mMorphHue { get{ return m_MorphHue; } set{ m_MorphHue = value; InvalidateProperties(); } }

		public int m_MorphHairHue;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mMorphHairHue { get{ return m_MorphHairHue; } set{ m_MorphHairHue = value; InvalidateProperties(); } }

		public string m_MorphName;
		[CommandProperty( AccessLevel.GameMaster )]
		public string mMorphName { get{ return m_MorphName; } set{ m_MorphName = value; InvalidateProperties(); } }

		public string m_OriginalName;
		[CommandProperty( AccessLevel.GameMaster )]
		public string mOriginalName { get{ return m_OriginalName; } set{ m_OriginalName = value; InvalidateProperties(); } }

		public int m_Status;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mStatus { get{ return m_Status; } set{ m_Status = value; InvalidateProperties(); } }

		public string m_Type;
		[CommandProperty( AccessLevel.GameMaster )]
		public string mType { get{ return m_Type; } set{ m_Type = value; InvalidateProperties(); } }

		public EssenceOrb( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
            writer.Write(m_Owner);
            writer.Write(m_MorphHue);
            writer.Write(m_MorphHairHue);
            writer.Write(m_MorphName);
            writer.Write(m_OriginalName);
            writer.Write(m_Status);
            writer.Write(m_Type);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Owner = reader.ReadMobile();
            m_MorphHue = reader.ReadInt();
            m_MorphHairHue = reader.ReadInt();
            m_MorphName = reader.ReadString();
            m_OriginalName = reader.ReadString();
            m_Status = reader.ReadInt();
            m_Type = reader.ReadString();

			if ( ItemID == 0xE2E ){ ItemID = 0x4FD6; }
		}
	}
}