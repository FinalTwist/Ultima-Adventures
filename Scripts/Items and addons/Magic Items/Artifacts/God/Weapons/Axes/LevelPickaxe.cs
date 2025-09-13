using System;
using Server.Items;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
	[FlipableAttribute( 0xE86, 0xE85 )]
    public class LevelPickaxe : BaseLevelAxe
	{
		//public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override HarvestSystem HarvestSystem
		{ get
			{
				if (this.Map == Map.Midland || this.Map == Map.Underground) 
					return DeepMine.DeepMining.GetSystem(this);
				else if (this.RootParentEntity is Mobile)
				{
					Mobile m = (Mobile)this.RootParentEntity;
					if (m.Map == Map.Midland || m.Map == Map.Underground) 
						return DeepMine.DeepMining.GetSystem(this);
				}	 
				return Mining.System;

			} 
		}

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.MagicProtection2; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.ZapDexStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.ShadowStrike; } }

		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 35; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public LevelPickaxe() : base( 0xE86 )
		{
			Weight = 11.0;
			UsesRemaining = 50;
			ShowUsesRemaining = true;
		}

		public LevelPickaxe( Serial serial ) : base( serial )
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
			ShowUsesRemaining = true;
		}
	}
}