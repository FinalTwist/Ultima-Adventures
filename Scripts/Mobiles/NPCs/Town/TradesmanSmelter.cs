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
	public class TradesmanSmelter : Citizens
	{
		[Constructable]
		public TradesmanSmelter()
		{
			CitizenType = 8;
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
				foreach ( Item forge in this.GetItemsInRange( 1 ) )
				{
					if ( forge is ForgeHit )
					{
						if ( this.FindItemOnLayer( Layer.FirstValid ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.Delete(); }
						ForgeHit smith = (ForgeHit)forge;
						smith.OnDoubleClick( this );
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

		public TradesmanSmelter( Serial serial ) : base( serial )
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
	public class ForgeHit : Item
	{
		[Constructable]
		public ForgeHit() : base( 0x10DE )
		{
			Name = "forge";
			Movable = false;
			Light = LightType.Circle225;
		}

		public ForgeHit( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is Citizens )
			{
				from.Direction = from.GetDirectionTo( GetWorldLocation() );
				from.Animate( 230, 5, 1, true, false, 0 );   
				from.PlaySound( Utility.RandomList( 0x02B, 0x047, 0x208 ) );
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