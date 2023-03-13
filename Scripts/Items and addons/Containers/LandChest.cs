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
	public class LandChest : LockableContainer, IChopable
	{
		[Constructable]
		public LandChest() : base( 19290 )
		{
			Name = "Corpse";
			ContainerFunctions.BuildContainer( this, 0, 0, 0, 4 );
			Movable = false;
			Weight = 25.0;
			LiftOverride = true;
			GumpID = 0x2A74;

			if ( Utility.RandomMinMax(1,4) == 1 )
			{
				ItemID = Utility.RandomList( 0x531C, 0x531D, 0x555C, 0x555D );
				Hue = 0;
				Name = "Wagon";
				GumpID = 0x2A77;
			}
		}

		public override void Open( Mobile from )
		{
			if ( this.Weight > 20 )
			{
				int FillMeUpLevel = Utility.RandomList( 5, 4, 4, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1 );

				if ( GetPlayerInfo.LuckyPlayer( from.Luck, from ) )
				{
					FillMeUpLevel = FillMeUpLevel + Utility.RandomMinMax( 1, 2 );
				}

				ContainerFunctions.FillTheContainer( FillMeUpLevel, this, from );

				this.Weight = 5.0;
				if ( isBody ( this.ItemID ) ){ LoggingFunctions.LogLoot( from, this.Name, "corpse" ); }
				else { ContainerFunctions.FillTheContainer( FillMeUpLevel, this, from ); LoggingFunctions.LogLoot( from, this.Name, "wagon" ); }
			}

			base.Open( from );

			Server.Items.CharacterDatabase.LootContainer( from, this );
		}

		public static bool isBody ( int item )
		{
			if ( item == 0x531C || item == 0x531D || item == 0x5534 || item == 0x5535 )
				return false;

			return true;
		}

		public virtual void OnChop( Mobile from )
		{
			if ( this.ItemID >= 0x4B5A && this.ItemID <= 0x4BAB && isBody ( this.ItemID ) )
			{
				this.ItemID = Utility.RandomList( 0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0, 0xED1, 0xED2 );
				this.Hue = 0;

				from.RevealingAction(); // REVEALING ONLY WHEN FAILED +++
				from.CriminalAction( true );
				Misc.Titles.AwardKarma( from, -50, true );

				this.DropItem( new BodyPart( 0x1D9F ) );
				this.DropItem( new BodyPart( 0x1DA4 ) );
				this.DropItem( new BodyPart( 0x1DA2 ) );
				this.DropItem( new BodyPart( 0x1DA3 ) );
				this.DropItem( new BodyPart( 0x1DA1 ) );
				this.DropItem( new BodyPart( 0x1DA0 ) );

				from.SendMessage( "You hack up the body into bloody pieces." );
			}
		}

		public override bool DisplaysContent{ get{ return false; } }
		public override bool DisplayWeight{ get{ return false; } }

        public override void OnAfterSpawn()
        {
			base.OnAfterSpawn();
			this.Location = Worlds.GetRandomLocation( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ), "land" );
		}

		public LandChest( Serial serial ) : base( serial )
		{
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