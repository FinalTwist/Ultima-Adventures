using System;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseMagicFish : Item
	{
		public virtual int Bonus{ get{ return 0; } }
		public virtual StatType Type{ get{ return StatType.Str; } }

		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		public BaseMagicFish( int hue ) : base( 0xDD6 )
		{
			Hue = hue;
		}

		public BaseMagicFish( Serial serial ) : base( serial )
		{
		}

		public virtual bool Apply( Mobile from )
		{
			bool applied = Spells.SpellHelper.AddStatOffset( from, Type, Bonus, TimeSpan.FromMinutes( 1.0 ) );

			if ( !applied )
				from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.

			return applied;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( Apply( from ) )
			{
				if ( from.Hunger < 20 )
				{
					from.Hunger += 3;
					// Send message to character about their current Hunger value

					if ( from.Body.IsHuman && !from.Mounted )
						from.Animate( 34, 5, 1, true, false, 0 );

					int iHeal = (int)from.Skills[SkillName.TasteID].Value;
					int iHurt = from.HitsMax - from.Hits;

					if ( iHurt > 0 ) // WIZARD DID THIS FOR TASTE ID
					{
						if ( iHeal > iHurt )
						{
							iHeal = iHurt;
						}

						from.Hits = from.Hits + iHeal;

						if ( from.Poisoned )
						{
							if ( (int)from.Skills[SkillName.TasteID].Value >= Utility.RandomMinMax( 1, 100 ) )
							{
								from.CurePoison( from );
								from.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
							}
						}
					}
				}

				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 501774 ); // You swallow the fish whole!
				Delete();
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

	public class PrizedFish : BaseMagicFish
	{
		public override int Bonus{ get{ return 5; } }
		public override StatType Type{ get{ return StatType.Int; } }

		public override int LabelNumber{ get{ return 1041073; } } // prized fish

		[Constructable]
		public PrizedFish() : base( 51 )
		{
		}

		public PrizedFish( Serial serial ) : base( serial )
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

			if ( Hue == 151 )
				Hue = 51;
		}
	}

	public class WondrousFish : BaseMagicFish
	{
		public override int Bonus{ get{ return 5; } }
		public override StatType Type{ get{ return StatType.Dex; } }

		public override int LabelNumber{ get{ return 1041074; } } // wondrous fish

		[Constructable]
		public WondrousFish() : base( 86 )
		{
		}

		public WondrousFish( Serial serial ) : base( serial )
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

			if ( Hue == 286 )
				Hue = 86;
		}
	}

	public class TrulyRareFish : BaseMagicFish
	{
		public override int Bonus{ get{ return 5; } }
		public override StatType Type{ get{ return StatType.Str; } }

		public override int LabelNumber{ get{ return 1041075; } } // truly rare fish

		[Constructable]
		public TrulyRareFish() : base( 76 )
		{
		}

		public TrulyRareFish( Serial serial ) : base( serial )
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

			if ( Hue == 376 )
				Hue = 76;
		}
	}

	public class PeculiarFish : BaseMagicFish
	{
		public override int LabelNumber{ get{ return 1041076; } } // highly peculiar fish

		[Constructable]
		public PeculiarFish() : base( 66 )
		{
		}

		public PeculiarFish( Serial serial ) : base( serial )
		{
		}

		public override bool Apply( Mobile from )
		{
			from.Stam += 10;
			return true;
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

			if ( Hue == 266 )
				Hue = 66;
		}
	}
}