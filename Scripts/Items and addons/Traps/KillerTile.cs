using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class KillerTile : Item
	{
		[Constructable]
		public KillerTile() : base(0x4228)
		{
			Movable = false;
			Visible = false;
			Name = "killer";
		}

		public KillerTile(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile && Spells.Research.ResearchAirWalk.UnderEffect( m ) )
			{
				Point3D air = new Point3D( ( m.X+1 ), ( m.Y+1 ), ( m.Z+5 ) );
				Effects.SendLocationParticles(EffectItem.Create(air, m.Map, EffectItem.DefaultDuration), 0x2007, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( m, 0 ), 0, 5022, 0);
				m.PlaySound( 0x014 );
			}
			else if ( m is PlayerMobile && m.Blessed == false && m.Alive && m.AccessLevel == AccessLevel.Player && Server.Misc.SeeIfGemInBag.GemInPocket( m ) == false && Server.Misc.SeeIfJewelInBag.JewelInPocket( m ) == false )
			{
				m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You made a fatal mistake!");
				m.Damage( 10000, m );
				LoggingFunctions.LogKillTile( m, this.Name );
			}
			return true;
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