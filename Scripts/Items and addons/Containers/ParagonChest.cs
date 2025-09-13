using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using Server.Misc;

namespace Server.Items
{
	[Flipable]
	public class ParagonChest : LockableContainer
	{
		private static int[] m_ItemIDs = new int[]
		{
			0x9AB, 0xE40, 0xE41, 0xE7C
		};

		private static int[] m_Hues = new int[]
		{
			0x0, 0x455, 0x47E, 0x89F, 0x8A5, 0x8AB, 0x966, 0x96D, 0x972, 0x973, 0x979
		};

		private string m_Name;

		[Constructable]
		public ParagonChest( string name, string title, int level, Mobile from ) : base( Utility.RandomList( m_ItemIDs ) )
		{
			int tMapLevel = level;
			level = level + 4;
				if ( level > 7 ){ level = 7; }

			m_Name = name;
				if ( title != "" && title != null ){ m_Name = name + " " + title; }

			Hue = Utility.RandomList( m_Hues );

			if ( level > 0 ){ ContainerFunctions.FillTheContainer( level, this, from ); }
			if ( level > 3 ){ ContainerFunctions.FillTheContainer( level, this, from ); }
			if ( level > 7 ){ ContainerFunctions.FillTheContainer( level, this, from ); }

			ContainerFunctions.LockTheContainer( level, this, 1 );

			int xTraCash = Utility.RandomMinMax( (level*600), (level*900) );
			ContainerFunctions.AddGoldToContainer( xTraCash, this, 0, from );

            if ( Utility.RandomMinMax( 0, 100 ) < ( level * 5 ) )
			{
				Item arty = ArtifactBuilder.CreateArtifact( "random" );
				DropItem( arty );
				//BaseContainer.DropItemFix( arty, from, ItemID, GumpID );
			}

			Map tMap = Map.Trammel;
			switch( Utility.RandomMinMax( 0, 4 ) )
			{
				case 0: tMap = Map.Trammel; break;
				case 1: tMap = Map.Felucca; break;
				case 2: tMap = Map.Malas; break;
				case 3: tMap = Map.Tokuno; break;
				case 4: tMap = Map.TerMur; break;
				case 5: tMap = Map.Ilshenar; break;
			}

			Point3D loc = new Point3D( 200, 200, 0 );
			Item map = new TreasureMap( tMapLevel, tMap, loc, 200, 200 );
			DropItem( map );
			//BaseContainer.DropItemFix( map, from, ItemID, GumpID );

			int giveRelics = level;
			Item relic = Loot.RandomRelic();
			while ( giveRelics > 0 )
			{
				relic = Loot.RandomRelic();
				ContainerFunctions.RelicValueIncrease( level, relic );
				//BaseContainer.DropItemFix( relic, from, ItemID, GumpID );
				DropItem( relic );
				giveRelics = giveRelics - 1;
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			LabelTo( from, 1063449, m_Name );
		}

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
            list.Add( "A Cursed Chest From " + m_Name );
        }
		
		public void Flip()
		{
			switch ( ItemID )
			{
				case 0x9AB : ItemID = 0xE7C; break;
				case 0xE7C : ItemID = 0x9AB; break;
				case 0xE40 : ItemID = 0xE41; break;
				case 0xE41 : ItemID = 0xE40; break;
			}
		}

		public ParagonChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Name );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Name = Utility.Intern( reader.ReadString() );
		}
	}
}
