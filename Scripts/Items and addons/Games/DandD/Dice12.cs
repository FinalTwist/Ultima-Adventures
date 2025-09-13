using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class Dice12 : Item, ITelekinesisable
	{
		[Constructable]
		public Dice12() : base( 0x301D )
		{
			Name = "dice";
			Weight = 1.0;
		}

		public Dice12( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "twelve sided");
            list.Add( 1049644, "Dungeons & Dragons");
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 2 ) )
				return;

			Roll( from );
		}

		public void OnTelekinesis( Mobile from )
		{
			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5022 );
			Effects.PlaySound( Location, Map, 0x1F5 );

			Roll( from );
		}

		public void Roll( Mobile from )
		{
			this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} rolls {1} on 1d12*", from.Name, Utility.Random( 1, 12 ) ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}