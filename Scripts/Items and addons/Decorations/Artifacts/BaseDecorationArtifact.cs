using System;
using Server;

namespace Server.Items
{
	public abstract class BaseDecorationArtifact : Item
	{
		public override bool ForceShowProperties{ get{ return true; } }
		public virtual int ArtifactRarity
		{
			get{ return 0; }
		}

		public BaseDecorationArtifact( int itemID ) : base( itemID )
		{
			Weight = 10.0;
		}

		public BaseDecorationArtifact( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public abstract class BaseDecorationContainerArtifact : BaseContainer
	{
		public override bool ForceShowProperties{ get{ return true; } }
		public virtual int ArtifactRarity
		{
			get{ return 0; }
		}

		public BaseDecorationContainerArtifact( int itemID ) : base( itemID )
		{
			Weight = 10.0;
		}

		public BaseDecorationContainerArtifact( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
