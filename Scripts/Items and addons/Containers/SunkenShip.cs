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
	public class SunkenShip : LockableContainer, IChopable
	{
		[Constructable]
		public SunkenShip( int level ) : base( 0x5186 )
		{
			Name = "sunken ship";
			ItemID = Utility.RandomList( 0x5186, 0x5199 );
			Weight = 51.0 + (double)level;
			Movable = false;
			LiftOverride = true;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start();
		}

		public override void Open( Mobile from )
		{
			if ( this.Weight > 50 )
			{
				int FillMeUpLevel = (int)(this.Weight - 51);
				this.Weight = 5.0;

				if ( GetPlayerInfo.LuckyPlayer( from.Luck, from ) )
				{
					FillMeUpLevel = FillMeUpLevel + Utility.RandomMinMax( 1, 2 );
				}

				ContainerFunctions.FillTheContainer( FillMeUpLevel, this, from );

				//BaseContainer.OrderContainer( this, from );
			}

			base.Open( from );

			Server.Items.CharacterDatabase.LootContainer( from, this );
		}

		public override bool DisplaysContent{ get{ return false; } }
		public override bool DisplayWeight{ get{ return false; } }

		public virtual void OnChop( Mobile from )
		{
			int wood = Utility.RandomMinMax( 10, 50 );
				if ( this.Name == "sunken boat" ){ wood = Utility.RandomMinMax( 5, 25 ); }
			int fishSkill = (int)(from.Skills[SkillName.Fishing].Value/10);
				if ( fishSkill > 13 ){ fishSkill = 13; }
			int woodSkill = (int)(from.Skills[SkillName.Carpentry].Value/2);
				if ( woodSkill < 5 ){ woodSkill = 5; }
				woodSkill = woodSkill + wood;

			switch ( Utility.Random( fishSkill ) )
			{
				case 0: from.AddToBackpack( new Board( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 1: from.AddToBackpack( new AshBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 2: from.AddToBackpack( new CherryBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 3: from.AddToBackpack( new EbonyBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 4: from.AddToBackpack( new GoldenOakBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 5: from.AddToBackpack( new HickoryBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 6: from.AddToBackpack( new MahoganyBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 7: from.AddToBackpack( new OakBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 8: from.AddToBackpack( new PineBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 9: from.AddToBackpack( new RosewoodBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 10: from.AddToBackpack( new WalnutBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 11: from.AddToBackpack( new DriftwoodBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
				case 12: from.AddToBackpack( new PetrifiedBoard( Utility.RandomMinMax( wood, woodSkill ) ) ); break;
			}

			from.PlaySound( 0x13E );
			from.PlaySound( 0x026 );
			Effects.SendLocationEffect( this.Location, this.Map, 0x352D, 16, 4 );
			from.SendMessage( "You salvage some usable wood from the ship." );
			this.Delete();
		}

		public SunkenShip( Serial serial ) : base( serial )
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
			this.Delete(); // none when the world starts 
		}

		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 
			public ItemRemovalTimer( Item item ) : base( TimeSpan.FromMinutes( 10.0 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = item; 
			} 

			protected override void OnTick() 
			{ 
				if (( i_item != null ) && ( !i_item.Deleted ))
				{
					i_item.Delete();
				}
			} 
		} 
	}
}