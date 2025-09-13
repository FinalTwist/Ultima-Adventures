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
	public class TradesmanLogger : Citizens
	{
		[Constructable]
		public TradesmanLogger()
		{
			CitizenType = 5;
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
				foreach ( Item tree in this.GetItemsInRange( 1 ) )
				{
					if ( tree is TreeHit )
					{
						if ( this.FindItemOnLayer( Layer.FirstValid ) != null && !(this.FindItemOnLayer( Layer.FirstValid ) is Hatchet) ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.TwoHanded ) != null && !(this.FindItemOnLayer( Layer.TwoHanded ) is Hatchet) ) { this.Delete(); }
						TreeHit log = (TreeHit)tree;
						log.OnDoubleClick( this );
						m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 2, 5 ) ));
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
			Item axe = new Hatchet();
			axe.Name = "axe";
			axe.ItemID = Utility.RandomList( 0xF45, 0xF47, 0xF49, 0xF4B, 0x13FA, 0x1442 );
			axe.Movable = false;
			AddItem( axe );
		}

		public TradesmanLogger( Serial serial ) : base( serial )
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
	public class TreeHit : Item
	{
		[Constructable]
		public TreeHit() : base( 0x13 )
		{
			Name = "tree";
			Visible = false;
			Movable = false;
		}

		public TreeHit( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.FindItemOnLayer( Layer.TwoHanded ) is BaseWeapon && from is Citizens )
			{
				BaseWeapon weapon = ( BaseWeapon )( from.FindItemOnLayer( Layer.TwoHanded ) );
				from.Direction = from.GetDirectionTo( GetWorldLocation() );
				weapon.PlaySwingAnimation( from );
				from.PlaySound( 0x13E );
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
		}
	}
}

namespace Server.Items
{
	public class CrateOfWood : Item
	{
		public int CrateQty;
		[CommandProperty(AccessLevel.Owner)]
		public int Crate_Qty { get { return CrateQty; } set { CrateQty = value; InvalidateProperties(); } }

		public string CrateItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Crate_Item { get { return CrateItem; } set { CrateItem = value; InvalidateProperties(); } }

		[Constructable]
		public CrateOfWood() : base( 0x5095 )
		{
			Name = "crate of boards";
			Weight = 10;
		}

		public CrateOfWood( Serial serial ) : base( serial )
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

				if ( CrateItem == "ash" ){ from.AddToBackpack ( new AshBoard( CrateQty ) ); }
				else if ( CrateItem == "cherry" ){ from.AddToBackpack ( new CherryBoard( CrateQty ) ); }
				else if ( CrateItem == "ebony" ){ from.AddToBackpack ( new EbonyBoard( CrateQty ) ); }
				else if ( CrateItem == "golden oak" ){ from.AddToBackpack ( new GoldenOakBoard( CrateQty ) ); }
				else if ( CrateItem == "hickory" ){ from.AddToBackpack ( new HickoryBoard( CrateQty ) ); }
				else if ( CrateItem == "mahogany" ){ from.AddToBackpack ( new MahoganyBoard( CrateQty ) ); }
				else if ( CrateItem == "oak" ){ from.AddToBackpack ( new OakBoard( CrateQty ) ); }
				else if ( CrateItem == "pine" ){ from.AddToBackpack ( new PineBoard( CrateQty ) ); }
				else if ( CrateItem == "ghostwood" ){ from.AddToBackpack ( new GhostBoard( CrateQty ) ); }
				else if ( CrateItem == "rosewood" ){ from.AddToBackpack ( new RosewoodBoard( CrateQty ) ); }
				else if ( CrateItem == "walnut" ){ from.AddToBackpack ( new WalnutBoard( CrateQty ) ); }
				else if ( CrateItem == "petrified" ){ from.AddToBackpack ( new PetrifiedBoard( CrateQty ) ); }
				else if ( CrateItem == "driftwood" ){ from.AddToBackpack ( new DriftwoodBoard( CrateQty ) ); }
				else if ( CrateItem == "elven" ){ from.AddToBackpack ( new ElvenBoard( CrateQty ) ); }
				else { from.AddToBackpack ( new Board( CrateQty ) ); }

				from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You separate the boards into your backpack", from.NetState);
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Contains " + CrateQty + " " + CrateItem + " Boards");
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
	public class CrateOfLogs : Item
	{
		public int CrateQty;
		[CommandProperty(AccessLevel.Owner)]
		public int Crate_Qty { get { return CrateQty; } set { CrateQty = value; InvalidateProperties(); } }

		public string CrateItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Crate_Item { get { return CrateItem; } set { CrateItem = value; InvalidateProperties(); } }

		[Constructable]
		public CrateOfLogs() : base( 0x5095 )
		{
			Name = "crate of logs";
			Weight = 10;
		}

		public CrateOfLogs( Serial serial ) : base( serial )
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

				if ( CrateItem == "ash" ){ from.AddToBackpack ( new AshLog( CrateQty ) ); }
				else if ( CrateItem == "cherry" ){ from.AddToBackpack ( new CherryLog( CrateQty ) ); }
				else if ( CrateItem == "ebony" ){ from.AddToBackpack ( new EbonyLog( CrateQty ) ); }
				else if ( CrateItem == "golden oak" ){ from.AddToBackpack ( new GoldenOakLog( CrateQty ) ); }
				else if ( CrateItem == "hickory" ){ from.AddToBackpack ( new HickoryLog( CrateQty ) ); }
				else if ( CrateItem == "mahogany" ){ from.AddToBackpack ( new MahoganyLog( CrateQty ) ); }
				else if ( CrateItem == "oak" ){ from.AddToBackpack ( new OakLog( CrateQty ) ); }
				else if ( CrateItem == "pine" ){ from.AddToBackpack ( new PineLog( CrateQty ) ); }
				else if ( CrateItem == "ghostwood" ){ from.AddToBackpack ( new GhostLog( CrateQty ) ); }
				else if ( CrateItem == "rosewood" ){ from.AddToBackpack ( new RosewoodLog( CrateQty ) ); }
				else if ( CrateItem == "walnut" ){ from.AddToBackpack ( new WalnutLog( CrateQty ) ); }
				else if ( CrateItem == "petrified" ){ from.AddToBackpack ( new PetrifiedLog( CrateQty ) ); }
				else if ( CrateItem == "driftwood" ){ from.AddToBackpack ( new DriftwoodLog( CrateQty ) ); }
				else if ( CrateItem == "elven" ){ from.AddToBackpack ( new ElvenLog( CrateQty ) ); }
				else { from.AddToBackpack ( new Log( CrateQty ) ); }

				from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You separate the logs into your backpack", from.NetState);
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Contains " + CrateQty + " " + CrateItem + " Logs");
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