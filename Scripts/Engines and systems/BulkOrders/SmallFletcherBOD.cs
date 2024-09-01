using System;
using System.Collections.Generic;
using Server;
using Server.Engines.Craft;
using Server.Items;
using Mat = Server.Engines.BulkOrders.BulkMaterialType;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.SmallFletcherBOD" )]
	public class SmallFletcherBOD : SmallBOD
	{
		public override int ComputeFame()
		{
			return FletcherRewardCalculator.Instance.ComputeFame( this );
		}

		public override int ComputeGold()
		{
			return FletcherRewardCalculator.Instance.ComputeGold( this );
		}

		public override List<Item> ComputeRewards( bool full )
		{
			List<Item> list = new List<Item>();

			RewardGroup rewardGroup = FletcherRewardCalculator.Instance.LookupRewards( FletcherRewardCalculator.Instance.ComputePoints( this ) );

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

		public static SmallFletcherBOD CreateRandomFor( Mobile m, bool defaultMaterialOnly = true )
		{
			SmallBulkEntry[] entries;
			bool useMaterials;

			if ( useMaterials = Utility.RandomBool() )
				entries = SmallBulkEntry.FletcherWeapons;
			else
				entries = SmallBulkEntry.FletcherWeapons;

            if (defaultMaterialOnly) useMaterials = false;

			if ( entries.Length > 0 )
			{
				double theirSkill = m.Skills[SkillName.Fletching].Base;
				int amountMax;

				if ( theirSkill >= 70.1 )
					amountMax = Utility.RandomList( 10, 15, 20, 20 );
				else if ( theirSkill >= 50.1 )
					amountMax = Utility.RandomList( 10, 15, 15, 20 );
				else
					amountMax = Utility.RandomList( 10, 10, 15, 20 );

				BulkMaterialType material = BulkMaterialType.None;

				if ( useMaterials && theirSkill >= 70.1 )
				{
					for ( int i = 0; i < 20; ++i )
					{
						BulkMaterialType check = GetRandomMaterial( BulkMaterialType.Ash, BulkMaterialType.Elven );
						double skillReq = 0.0;

						switch ( check )
						{
                            case BulkMaterialType.Ash: skillReq = 65.0; break;
                            case BulkMaterialType.Cherry: skillReq = 70.0; break;
                            case BulkMaterialType.Ebony: skillReq = 75.0; break;
                            case BulkMaterialType.GoldenOak: skillReq = 80.0; break;
                            case BulkMaterialType.Hickory: skillReq = 85.0; break;
                            case BulkMaterialType.Mahogany: skillReq = 90.0; break;
                            case BulkMaterialType.Oak: skillReq = 95.0; break;
                            case BulkMaterialType.Pine: skillReq = 100.0; break;
                            case BulkMaterialType.Rosewood: skillReq = 100.0; break;
                            case BulkMaterialType.Walnut: skillReq = 100.0; break;
                            case BulkMaterialType.Driftwood: skillReq = 105.0; break;
                            case BulkMaterialType.Ghost: skillReq = 110.0; break;
                            case BulkMaterialType.Petrified: skillReq = 115.0; break;
                            case BulkMaterialType.Elven: skillReq = 120; break;
                        }

						if ( theirSkill >= skillReq )
						{
							material = check;
							break;
						}
					}
				}

				double excChance = 0.0;

				if ( theirSkill >= 70.1 )
					excChance = (theirSkill + 80.0) / 200.0;

				bool reqExceptional = ( excChance > Utility.RandomDouble() );

				SmallBulkEntry entry = null;

				CraftSystem system = DefBowFletching.CraftSystem;

				for ( int i = 0; i < 150; ++i )
				{
					SmallBulkEntry check = entries[Utility.Random( entries.Length )];

					CraftItem item = system.CraftItems.SearchFor( check.Type );

					if ( item != null )
					{
						bool allRequiredSkills = true;
						double chance = item.GetSuccessChance( m, null, system, false, ref allRequiredSkills );

						if ( allRequiredSkills && chance >= 0.0 )
						{
							if ( reqExceptional )
								chance = item.GetExceptionalChance( system, chance, m );

							if ( chance > 0.0 )
							{
								entry = check;
								break;
							}
						}
					}
				}

				if ( entry != null )
					return new SmallFletcherBOD( entry, material, amountMax, reqExceptional );
			}

			return null;
		}

		private SmallFletcherBOD( SmallBulkEntry entry, BulkMaterialType material, int amountMax, bool reqExceptional )
		{
			this.Hue = 0x58;
			this.AmountMax = amountMax;
			this.Type = entry.Type;
			this.Number = entry.Number;
			this.Graphic = entry.Graphic;
			this.RequireExceptional = reqExceptional;
			this.Material = material;
		}

		[Constructable]
		public SmallFletcherBOD()
		{
			SmallBulkEntry[] entries;
			bool useMaterials;

			if ( useMaterials = Utility.RandomBool() )
				entries = SmallBulkEntry.FletcherWeapons;
			else
				entries = SmallBulkEntry.FletcherWeapons;

			if ( entries.Length > 0 )
			{
				int hue = 0x58;
				int amountMax = Utility.RandomList( 10, 15, 20 );

				BulkMaterialType material;

				if ( useMaterials )
					material = GetRandomMaterial( BulkMaterialType.Ash, BulkMaterialType.Elven );
				else
					material = BulkMaterialType.None;

				bool reqExceptional = Utility.RandomBool() || (material == BulkMaterialType.None);

				SmallBulkEntry entry = entries[Utility.Random( entries.Length )];

				this.Hue = hue;
				this.AmountMax = amountMax;
				this.Type = entry.Type;
				this.Number = entry.Number;
				this.Graphic = entry.Graphic;
				this.RequireExceptional = reqExceptional;
				this.Material = material;
			}
		}

		public SmallFletcherBOD( int amountCur, int amountMax, Type type, int number, int graphic, bool reqExceptional, BulkMaterialType mat )
		{
			this.Hue = 0x58;
			this.AmountMax = amountMax;
			this.AmountCur = amountCur;
			this.Type = type;
			this.Number = number;
			this.Graphic = graphic;
			this.RequireExceptional = reqExceptional;
			this.Material = mat;
		}

		public SmallFletcherBOD( Serial serial ) : base( serial )
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
            switch (version)
            {
                case 1:
                    break;

                case 0:
                    if (Material != BulkMaterialType.None)
                    {
                        Material += 7; // Number of Metals added ahead of it in the Enum
                        Material += 8; // Number of Leathers added ahead of it in the Enum
                    }
                    break;
            }
        }
	}
}
