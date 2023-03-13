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
	public class WaterChest : LockableContainer, IChopable
	{
		[Constructable]
		public WaterChest() : base( 0x2299 )
		{
			Name = "Boat";
			ContainerFunctions.BuildContainer( this, 0, 0, 0, 10 );
			Movable = false;
			Weight = 100.0;
			LiftOverride = true;
		}

		public override void Open( Mobile from )
		{
			if ( this.Weight > 50 )
			{
				int FillMeUpLevel = Utility.RandomList( 5, 4, 4, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1 );

				if ( GetPlayerInfo.LuckyPlayer( from.Luck, from ) )
				{
					FillMeUpLevel = FillMeUpLevel + Utility.RandomMinMax( 1, 2 );
				}

				if ( Utility.RandomBool() )
				{
					int[] list = new int[]
						{
							0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0,
							0xED1, 0xED2, 0x1B09, 0x1B0A, 0x1B0B, 0x1B0C,
							0x1B0D, 0x1B0E, 0x1B0F, 0x1B10,
						};

					Item bones = new BodyPart( Utility.RandomList( list ) );
					bones.Name = ContainerFunctions.GetOwner( "BodySailor" );
					this.DropItem( bones );
					//BaseContainer.DropItemFix( bones, from, ItemID, GumpID );
				}

				ContainerFunctions.FillTheContainer( FillMeUpLevel, this, from );

				this.Weight = 5.0;
				LoggingFunctions.LogLoot( from, this.Name, "boat" );
			}

			base.Open( from );

			Server.Items.CharacterDatabase.LootContainer( from, this );
		}

		public virtual void OnChop( Mobile from )
		{
			int fishSkill = (int)(from.Skills[SkillName.Fishing].Value/10);
				if ( fishSkill > 13 ){ fishSkill = 13; }
			int woodSkill = (int)(from.Skills[SkillName.Carpentry].Value/2);
				if ( woodSkill < 5 ){ woodSkill = 5; }

			switch ( Utility.Random( fishSkill ) )
			{
				case 0: from.AddToBackpack( new Board( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 1: from.AddToBackpack( new AshBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 2: from.AddToBackpack( new CherryBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 3: from.AddToBackpack( new EbonyBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 4: from.AddToBackpack( new GoldenOakBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 5: from.AddToBackpack( new HickoryBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 6: from.AddToBackpack( new MahoganyBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 7: from.AddToBackpack( new OakBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 8: from.AddToBackpack( new PineBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 9: from.AddToBackpack( new RosewoodBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 10: from.AddToBackpack( new WalnutBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 11: from.AddToBackpack( new DriftwoodBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
				case 12: from.AddToBackpack( new PetrifiedBoard( Utility.RandomMinMax( 5, woodSkill ) ) ); break;
			}

			from.PlaySound( 0x13E );
			from.PlaySound( 0x026 );
			Effects.SendLocationEffect( this.Location, this.Map, 0x352D, 16, 4 );
			from.SendMessage( "You salvage some usable wood from the boat." );
			this.Delete();
		}

		public override bool DisplaysContent{ get{ return false; } }
		public override bool DisplayWeight{ get{ return false; } }

        public override void OnAfterSpawn()
        {
			base.OnAfterSpawn();
			this.Location = Worlds.GetRandomLocation( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ), "sea" );
		}

		public WaterChest( Serial serial ) : base( serial )
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