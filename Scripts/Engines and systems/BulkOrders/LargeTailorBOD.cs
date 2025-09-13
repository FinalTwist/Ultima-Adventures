using System;
using System.Collections;
using Server;
using Server.Items;
using Mat = Server.Engines.BulkOrders.BulkMaterialType;
using System.Collections.Generic;

namespace Server.Engines.BulkOrders
{
	public class LargeTailorBOD : LargeBOD
	{
		public override int ComputeFame()
		{
			return TailorRewardCalculator.Instance.ComputeFame( this );
		}

		public override int ComputeGold()
		{
			return TailorRewardCalculator.Instance.ComputeGold( this );
		}

        [Constructable]
        public LargeTailorBOD() : this(false)
        {
        }

        [Constructable]
        public LargeTailorBOD(bool useMaterials = false)
		{
			LargeBulkEntry[] entries;

			if (!useMaterials)
            {
                switch (Utility.Random(14))
                {
                    default:
                    case 0: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.Farmer); break;
                    case 1: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.FemaleLeatherSet); break;
                    case 2: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.FisherGirl); break;
                    case 3: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.Gypsy); break;
                    case 4: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.HatSet); break;
                    case 5: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.Jester); break;
                    case 6: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.Lady); break;
                    case 7: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.MaleLeatherSet); break;
                    case 8: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.Pirate); break;
                    case 9: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.ShoeSet); break;
                    case 10: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.StuddedSet); break;
                    case 11: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.TownCrier); break;
                    case 12: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.Wizard); break;
                    case 13: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.BoneSet); break;
                }
			}
			else
			{
                switch (Utility.Random(5))
                {
					default:
                    case 0: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.FemaleLeatherSet); break;
                    case 1: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.MaleLeatherSet); break;
                    case 2: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.ShoeSet); break;
                    case 3: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.StuddedSet); break;
                    case 4: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.BoneSet); break;
                }
            }

			int hue = 0x483;
			int amountMax = Utility.RandomList( 10, 15, 20, 20 );
			bool reqExceptional = ( 0.825 > Utility.RandomDouble() );

			BulkMaterialType material;

			if ( useMaterials )
				material = GetRandomMaterial( BulkMaterialType.Horned, BulkMaterialType.Alien );
			else
				material = BulkMaterialType.None;

			this.Hue = hue;
			this.AmountMax = amountMax;
			this.Entries = entries;
			this.RequireExceptional = reqExceptional;
			this.Material = material;
		}

		public LargeTailorBOD( int amountMax, bool reqExceptional, BulkMaterialType mat, LargeBulkEntry[] entries )
		{
			this.Hue = 0x483;
			this.AmountMax = amountMax;
			this.Entries = entries;
			this.RequireExceptional = reqExceptional;
			this.Material = mat;
		}

		public override List<Item> ComputeRewards( bool full )
		{
			List<Item> list = new List<Item>();

			RewardGroup rewardGroup = TailorRewardCalculator.Instance.LookupRewards( TailorRewardCalculator.Instance.ComputePoints( this ) );

			if ( rewardGroup != null )
			{
				if ( full )
				{
					for ( int i = 0; i < rewardGroup.Items.Length; ++i )
					{
						Item item = rewardGroup.Items[i].Construct();

						if ( item != null )
							list.Add( item );
					}
				}
				else
				{
					RewardItem rewardItem = rewardGroup.AcquireItem();

					if ( rewardItem != null )
					{
						Item item = rewardItem.Construct();

						if ( item != null )
							list.Add( item );
					}
				}
			}

			return list;
		}

		public LargeTailorBOD( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			switch(version)
			{
				case 1:
					break;

				case 0:
					if (Material != BulkMaterialType.None)
					{
                        Material += 7; // The number of Metals added ahead of it in the Enum
                    }
                    break;
			}
		}
	}
}