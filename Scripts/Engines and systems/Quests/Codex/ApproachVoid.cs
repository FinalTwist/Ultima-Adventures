using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;
using System.Collections;

namespace Server.Items
{
	public class ApproachVoid : Item
	{
		[Constructable]
		public ApproachVoid() : base(0x2161)
		{
			Movable = false;
			Visible = false;
			Name = "floor";
		}

		public ApproachVoid(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				if ( m.Backpack.FindItemByType( typeof ( VortexCube ) ) != null )
				{
					Item square = m.Backpack.FindItemByType( typeof ( VortexCube ) );

					if ( square is VortexCube )
					{
						VortexCube cube = (VortexCube)square;
						int quest = cube.HasConvexLense + cube.HasConcaveLense + cube.HasKeyLaw + cube.HasKeyChaos + cube.HasKeyBalance + cube.HasCrystalRed + cube.HasCrystalBlue + cube.HasCrystalGreen + cube.HasCrystalYellow + cube.HasCrystalWhite + cube.HasCrystalPurple;

						if ( cube.CubeOwner == m && quest > 10 )
						{
							square.Delete();

							ArrayList targets = new ArrayList();
							foreach ( Item item in World.Items.Values )
							if ( item is VortexCube )
							{
								if ( ((VortexCube)item).CubeOwner == m )
									targets.Add( item );
							}
							for ( int i = 0; i < targets.Count; ++i )
							{
								Item item = ( Item )targets[ i ];
								item.Delete();
							}

							CodexWisdom book = new CodexWisdom();
							book.CodexOwner = m;
							book.HasConvexLense = 1;
							book.HasConcaveLense = 1;
							m.AddToBackpack( book );
							m.SendMessage( "You draw the Codex out from the Void!" );
							m.LocalOverheadMessage(MessageType.Emote, 1150, true, "You draw the Codex out from the Void");
							m.SendSound( 0x203 );
							LoggingFunctions.LogGeneric( m, "has obtained the Codex of Ultimate Wisdom." );
						}
					}
				}
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