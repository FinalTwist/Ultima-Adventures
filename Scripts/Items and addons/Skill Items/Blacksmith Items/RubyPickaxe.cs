using System;
using Server.Items;
using Server.Network;
using Server.Regions;
using Server.Mobiles;

namespace Server.Items
{
	public enum ZornEffect
	{
		Charges
	}

	[FlipableAttribute( 0xE86, 0xE85 )]
	public class RubyPickaxe : BaseSword
	{
		private ZornEffect m_ZornEffect;
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public ZornEffect Effect
		{
			get{ return m_ZornEffect; }
			set{ m_ZornEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
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

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public RubyPickaxe() : base( 0xE86 )
		{
			Name = "adamantium pickaxe";
			Weight = 11.0;
			Charges = 20;
			Hue = 0xADA;
			Attributes.BonusStr = 5;
			DamageLevel = WeaponDamageLevel.Vanq;
			SkillBonuses.SetValues( 0, SkillName.Swords, 10 );
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060741, m_Charges.ToString() );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From Zorn the Blacksmith");
            list.Add( 1049644, "Magically Dig Caddellite");
        }

		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			if ( Charges == 0 )
			{
				from.SendLocalizedMessage( 1019073 ); // This item is out of charges.
			}
		}

        public override void OnDoubleClick(Mobile from)
		{
			if ( Parent == from )
			{
				int hydra = 0;

				foreach ( Mobile m in this.GetMobilesInRange( 100 ) )
				{
					if ( m is EnergyHydra )
						hydra = 1;
				}

				if ( from.Skills[SkillName.Mining].Value < 90 )
				{
					from.SendMessage("You must be a master miner to use this pickaxe!");
				}
				else if ( hydra > 0 )
				{
					from.SendMessage("You cannot dig here while the hydra is nearby!");
				}
				else if ( from.Mounted )
				{
					from.SendMessage("You cannot dig while riding.");
				}
				else if ( from.IsBodyMod && !from.Body.IsHuman )
				{
					from.SendMessage("You cannot dig while polymorphed.");
				}
				else if ( Charges > 0 && from.Region.IsPartOf( "the Caddellite Crater" ) )
				{
					ConsumeCharge( from );
					from.PlaySound( 0x125 );
					from.Animate( 11, 5, 1, true, false, 0 );
					from.SendMessage("You dig up a chunk of caddellite ore from the meteor!");
					from.AddToBackpack( new CaddelliteOre() );
					this.HitPoints = this.HitPoints - 5;
					this.MaxHitPoints = this.MaxHitPoints - 5;

					if ( this.HitPoints < 1 ){ this.HitPoints = 1; }
					if ( this.MaxHitPoints < 1 ){ this.MaxHitPoints = 1; }
				}
				else if ( Charges < 1 )
				{
					from.SendMessage("This pickaxe is too worn to dig caddellite!");
				}
				else
				{
					from.SendMessage("There is no caddellite meteor nearby to dig!");
				}
			}
			else
			{
				from.SendLocalizedMessage( 502641 ); // You must equip this item to use it.
			}
        }

		public RubyPickaxe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_ZornEffect );
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_ZornEffect = (ZornEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();

					break;
				}
			}

			Name = "adamantium pickaxe";
			Hue = 0xADA;
	    }
	}
}