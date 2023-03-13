using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class GargoylesPickaxe : BaseAxe, IUsesRemaining
	{
		public override int LabelNumber{ get{ return 1041281; } } // a gargoyle's pickaxe
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
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.MagicProtection; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.StunningStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.MeleeProtection; } }

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
		public GargoylesPickaxe() : this( Utility.RandomMinMax( 101, 125 ))
		{
		}

		[Constructable]
		public GargoylesPickaxe( int uses ) : base( 0xE85 + Utility.Random( 2 ))
		{
			Weight = 11.0;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			list.Add( "Say 'I wish to start mining' near cave/mountain to mine automatically." ); 
		}

		public GargoylesPickaxe( Serial serial ) : base( serial )
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
			
			if ( Hue == 0x973 )
				Hue = 0x0;
		}
	}
}