using System;
using Server;
using Server.Items;


namespace Server.Mobiles
{
	public class AdventurerEast : Citizens
	{
		[Constructable]
		public AdventurerEast() : base( )
		{
			Direction = Direction.East;
		}


		public AdventurerEast( Serial serial ) : base( serial )
		{
		}


		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.FindItemOnLayer( Layer.OneHanded ).Delete(); }
			if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
			if ( this.FindItemOnLayer( Layer.Helm ) != null ) { if ( this.FindItemOnLayer( Layer.Helm ) is BaseArmor ){ this.FindItemOnLayer( Layer.Helm ).Delete(); } }
			Server.Misc.MorphingTime.CheckNecromancer( this );
			Server.Items.EssenceBase.ColorCitizen( this );
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
	public class AdventurerWest : Citizens
	{
		[Constructable]
		public AdventurerWest() : base( )
		{
			Direction = Direction.West;
		}


		public AdventurerWest( Serial serial ) : base( serial )
		{
		}


		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.FindItemOnLayer( Layer.OneHanded ).Delete(); }
			if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
			if ( this.FindItemOnLayer( Layer.Helm ) != null ) { if ( this.FindItemOnLayer( Layer.Helm ) is BaseArmor ){ this.FindItemOnLayer( Layer.Helm ).Delete(); } }
			Server.Misc.MorphingTime.CheckNecromancer( this );
			Server.Items.EssenceBase.ColorCitizen( this );
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
	public class AdventurerSouth : Citizens
	{
		[Constructable]
		public AdventurerSouth() : base( )
		{
			Direction = Direction.South;
		}


		public AdventurerSouth( Serial serial ) : base( serial )
		{
		}


		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.FindItemOnLayer( Layer.OneHanded ).Delete(); }
			if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
			if ( this.FindItemOnLayer( Layer.Helm ) != null ) { if ( this.FindItemOnLayer( Layer.Helm ) is BaseArmor ){ this.FindItemOnLayer( Layer.Helm ).Delete(); } }
			Server.Misc.MorphingTime.CheckNecromancer( this );
			Server.Items.EssenceBase.ColorCitizen( this );
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
	public class AdventurerNorth : Citizens
	{
		[Constructable]
		public AdventurerNorth() : base( )
		{
			Direction = Direction.North;
		}


		public AdventurerNorth( Serial serial ) : base( serial )
		{
		}


		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.FindItemOnLayer( Layer.OneHanded ).Delete(); }
			if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
			if ( this.FindItemOnLayer( Layer.Helm ) != null ) { if ( this.FindItemOnLayer( Layer.Helm ) is BaseArmor ){ this.FindItemOnLayer( Layer.Helm ).Delete(); } }
			Server.Misc.MorphingTime.CheckNecromancer( this );
			Server.Items.EssenceBase.ColorCitizen( this );
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