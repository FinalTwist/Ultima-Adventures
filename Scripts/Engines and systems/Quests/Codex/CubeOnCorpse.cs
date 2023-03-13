using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class CubeOnCorpse : Item
	{
		[Constructable]
		public CubeOnCorpse() : base( 0x05D5 )
		{
			Weight = 1.0;
			Name = "vortex cube";
			Movable = false;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Double Click To Take It" );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				bool HasCube = false;
				bool HasBook = false;

				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				{
					if ( item is VortexCube )
					{
						if ( ((VortexCube)item).CubeOwner == from )
						{
							targets.Add( item );
							HasCube = true;
						}
					}
					else if ( item is CodexWisdom )
					{
						if ( ((CodexWisdom)item).CodexOwner == from )
						{
							targets.Add( item );
							HasBook = true;
							HasCube = true;
						}
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];

					if ( item is CodexWisdom )
					{
						from.AddToBackpack( item );
						from.SendMessage( "You don't need the Vortex Cube since the Codex is already in your pack." );
					}
					else if ( item is VortexCube )
					{
						if ( HasBook )
						{
							item.Delete();
						}
						else
						{
							from.AddToBackpack( item );
							from.SendMessage( "Your Vortex Cube is already in your pack." );
						}
					}
				}

				if ( !HasCube )
				{
					SetupCube( from );
					from.SendMessage( "You take possession of the Vortex Cube!" );
					from.SendSound( 0x3D );
					LoggingFunctions.LogGeneric( from, "has found the Vortex Cube." );
					this.Delete();
				}
			}
		}

		public void SetupCube( Mobile from )
		{
			VortexCube cube = new VortexCube();

			cube.CubeOwner = from;

			cube.HasConvexLense = 0;
			cube.HasConcaveLense = 0;

			cube.HasKeyLaw = 0;
			cube.TextKeyLaw = GetRumor();
			cube.LocationKeyLaw = PickDungeon();

			cube.HasKeyChaos = 0;
			cube.TextKeyChaos = GetRumor();
			cube.LocationKeyChaos = PickDungeon();

			cube.HasKeyBalance = 0;
			cube.TextKeyBalance = GetRumor();
			cube.LocationKeyBalance = PickDungeon();

			cube.HasCrystalRed = 0;
			cube.HasCrystalBlue = 0;
			cube.HasCrystalGreen = 0;
			cube.HasCrystalYellow = 0;
			cube.HasCrystalWhite = 0;
			cube.HasCrystalPurple = 0;

			cube.TextCrystal = GetRumor();
			cube.LocationCrystal = PickDungeon();

			from.AddToBackpack( cube );
		}

		public static string GetRumor()
		{
			string rumor = "is said to be in";

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:	rumor = "is said to be in";					break;
				case 1:	rumor = "is rumored to be in";				break;
				case 2:	rumor = "has legends tell of it being in";	break;
				case 3:	rumor = "was heard to be in";				break;
			}

			return rumor;
		}

		public static string PickDungeon()
		{
			string dungeon = "the Dungeon of Abandon";

			int aCount = 0;
			ArrayList targets = new ArrayList();
			foreach ( Item target in World.Items.Values )
			if ( target is SearchBase )
			{
				targets.Add( target );
				aCount++;
			}

			aCount = Utility.RandomMinMax( 1, aCount );

			int xCount = 0;
			for ( int i = 0; i < targets.Count; ++i )
			{
				xCount++;

				if ( xCount == aCount )
				{
					Item finding = ( Item )targets[ i ];
					dungeon = Server.Misc.Worlds.GetRegionName( finding.Map, finding.Location );
				}
			}

			return dungeon;
		}

		public CubeOnCorpse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}