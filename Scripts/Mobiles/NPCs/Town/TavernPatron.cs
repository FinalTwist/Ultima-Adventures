using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class TavernPatronEast : Citizens
	{
		[Constructable]
		public TavernPatronEast() : base( )
		{
			Direction = Direction.East;
		}

		public TavernPatronEast( Serial serial ) : base( serial )
		{
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.TavernPatrons.RemoveSomeGear( this, true );
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
	public class TavernPatronWest : Citizens
	{
		[Constructable]
		public TavernPatronWest() : base( )
		{
			Direction = Direction.West;
		}

		public TavernPatronWest( Serial serial ) : base( serial )
		{
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.TavernPatrons.RemoveSomeGear( this, true );
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
	public class TavernPatronSouth : Citizens
	{
		[Constructable]
		public TavernPatronSouth() : base( )
		{
			Direction = Direction.South;
		}

		public TavernPatronSouth( Serial serial ) : base( serial )
		{
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.TavernPatrons.RemoveSomeGear( this, true );
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
	public class TavernPatronNorth : Citizens
	{
		[Constructable]
		public TavernPatronNorth() : base( )
		{
			Direction = Direction.North;
		}

		public TavernPatronNorth( Serial serial ) : base( serial )
		{
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.TavernPatrons.RemoveSomeGear( this, true );
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