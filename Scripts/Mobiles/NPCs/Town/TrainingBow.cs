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
	public class TrainingBow : Citizens
	{
		[Constructable]
		public TrainingBow()
		{
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
				foreach ( Item hay in this.GetItemsInRange( 6 ) )
				{
					if ( hay is ArcheryButte && ( hay.X == X || hay.Y == Y ) )
					{
						if ( this.FindItemOnLayer( Layer.FirstValid ) != null && !(this.FindItemOnLayer( Layer.FirstValid ) is BaseRanged) ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.TwoHanded ) != null && !(this.FindItemOnLayer( Layer.TwoHanded ) is BaseRanged) ) { this.Delete(); }
						hay.OnDoubleClick( this );
						if ( hay.X == X ){ hay.ItemID = 0x100B; } else { hay.ItemID = 0x100A; }
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
			Item bow = new Bow();
				bow.Delete();
				switch ( Utility.RandomMinMax( 1, 6 ) )
				{
					case 1: bow = new Bow();				break;
					case 2: bow = new Crossbow();			break;
					case 3: bow = new HeavyCrossbow();		break;
					case 4: bow = new CompositeBow();		break;
					case 5: bow = new Yumi();				break;
					case 6: bow = new RepeatingCrossbow();	break;
				}
			bow.Movable = false;
			AddItem( bow );
		}

		public TrainingBow( Serial serial ) : base( serial )
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