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
	public class CorpseSailor : LockableContainer
	{
		[Constructable]
		public CorpseSailor() : this( 0 )
		{
		}
		
		[Constructable]
		public CorpseSailor( int level ) : base( 0xe40 )
		{
			if ( level < 1 ){ level = Utility.RandomMinMax( 1, 4 ); }
			
			string sCorpse = ContainerFunctions.GetOwner( "BodySailor" );
			Name = sCorpse;
			Movable = true;
			Weight = 11.0 + (double)level;
			GumpID = 0x2A73;
			DropSound = 0x48;
			ItemID = 3786 + Utility.Random( 8 );

			TrapType = TrapType.None;
			TrapPower = 0;
			TrapLevel = 0;
			Locked = false;
            LockLevel = 0;
			MaxLockLevel = 0;
			RequiredSkill = 0;
			
			Hue = Utility.RandomList( 0xB97, 0xB98, 0xB99, 0xB9A, 0xB88 );

			if ( Weight > 10 ){ Movable = false; } // DON'T WANT THEM TO MOVE IT UNTIL THEY OPEN IT FIRST
		}

		public override void Open( Mobile from )
		{
			if ( this.Weight > 10 )
			{
				Movable = true;
				int FillMeUpLevel = (int)(this.Weight - 11);
				this.Weight = 5.0;

				if ( GetPlayerInfo.LuckyPlayer( from.Luck, from ) )
				{
					FillMeUpLevel = FillMeUpLevel + Utility.RandomMinMax( 1, 2 );
				}

				ContainerFunctions.FillTheContainer( FillMeUpLevel, this, from );
			}

			base.Open( from );
		}

		public CorpseSailor( Serial serial ) : base( serial )
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