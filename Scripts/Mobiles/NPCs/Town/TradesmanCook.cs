using System;
using Server;
using Server.ContextMenus;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class TradesmanCook : Citizens
	{
		[Constructable]
		public TradesmanCook()
		{
			CitizenType = 10;
			SetupCitizen();
			Blessed = true;
			CantWalk = true;
			AI = AIType.AI_Melee;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
		}

		public override void OnThink()
		{
			if ( DateTime.UtcNow >= m_NextTalk )
			{
				foreach ( Item fire in this.GetItemsInRange( 1 ) )
				{
					if ( fire is StoveHit )
					{
						if ( this.FindItemOnLayer( Layer.FirstValid ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.Delete(); }
						StoveHit stove = (StoveHit)fire;
						stove.OnDoubleClick( this );
						m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 6, 12 ) ));
					}
				}
			}
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.TavernPatrons.RemoveSomeGear( this, false );
			Server.Misc.MorphingTime.CheckNecromancer( this );
			Server.Items.EssenceBase.ColorCitizen( this );
		}

		public TradesmanCook( Serial serial ) : base( serial )
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

namespace Server.Items
{
	public class StoveHit : Item
	{
		[Constructable]
		public StoveHit() : base( 0x568B )
		{
			Name = "skillet";
			Movable = false;
			Weight = -2.0;
			ItemID = Utility.RandomList( 0x568B, 0x568C, 0x568D, 0x568E );
		}

		public StoveHit( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is Citizens )
			{
				from.Direction = from.GetDirectionTo( GetWorldLocation() );
				from.Animate( 230, 5, 1, true, false, 0 ); 
				if ( ItemID == 0x568B ){ ItemID = 0x568C; from.PlaySound( Utility.RandomList( 0x059, 0x057 ) ); }
				else if ( ItemID == 0x568C ){ ItemID = 0x568B; from.PlaySound( Utility.RandomList( 0x5CF, 0x5CA, 0x345 ) ); }
				else if ( ItemID == 0x568D ){ ItemID = 0x568E; from.PlaySound( Utility.RandomList( 0x5CF, 0x5CA, 0x345 ) ); }
				else if ( ItemID == 0x568E ){ ItemID = 0x568D; from.PlaySound( Utility.RandomList( 0x059, 0x057 ) ); }
			}
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
			Weight = -2.0;
		}
	}
}

namespace Server.Items
{
	public class CrateOfFood : Item
	{
		public int CrateQty;
		[CommandProperty(AccessLevel.Owner)]
		public int Crate_Qty { get { return CrateQty; } set { CrateQty = value; InvalidateProperties(); } }

		public string CrateItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Crate_Item { get { return CrateItem; } set { CrateItem = value; InvalidateProperties(); } }

		[Constructable]
		public CrateOfFood() : base( 0x5095 )
		{
			Name = "crate of food";
			Weight = 10;
		}

		public CrateOfFood( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to open." );
				return;
			}
			else
			{
				from.PlaySound( 0x02D );
				from.AddToBackpack ( new LargeCrate() );

				if ( ItemID == 0x508B ){ from.AddToBackpack ( new FishSteak( CrateQty ) ); }
				else if ( ItemID == 0x508C ){ from.AddToBackpack ( new LambLeg( CrateQty ) ); }
				else if ( ItemID == 0x508D ){ from.AddToBackpack ( new Ribs( CrateQty ) ); }
				else if ( ItemID == 0x50BA ){ from.AddToBackpack ( new BreadLoaf( CrateQty ) ); }

				from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You separate the leather into your backpack", from.NetState);
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Contains " + CrateQty + " " + CrateItem + "");
			list.Add( 1049644, "Open to Remove them from the Crate");
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( CrateQty );
            writer.Write( CrateItem );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            CrateQty = reader.ReadInt();
            CrateItem = reader.ReadString();
		}
	}
}