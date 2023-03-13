// coded by ßåяя¥ 2.0
using System;
using Server;

namespace Server.Items
{
	public abstract class BaseStrengthPotion : BasePotion
	{
		public abstract int StrOffset{ get; }
		public abstract TimeSpan Duration{ get; }

		//public override int Hue{ get { return 0; } }

		public BaseStrengthPotion( PotionEffect effect ) : base( 0xF09, effect )
		{
		}

		public BaseStrengthPotion( Serial serial ) : base( serial )
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

		public bool DoStrength( Mobile from )
		{
			int scale = Scale(from, StrOffset);
            		if (Spells.SpellHelper.AddStatOffset(from, StatType.Str, scale, Duration))
			// TODO: Verify scaled; is it offset, duration, or both?
			//if ( Spells.SpellHelper.AddStatOffset( from, StatType.Str, Scale( from, Server.Misc.MyServerSettings.PlayerLevelMod( StrOffset, from ) ), Duration ) )
			{
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				
				BuffInfo.AddBuff(from, new BuffInfo(BuffIcon.Strength, 1075845, Duration, from, scale.ToString()));
				
				return true;
			}

			from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.
			return false;
		}

		public override void Drink( Mobile from )
		{
			if ( DoStrength( from ) )
			{
				BasePotion.PlayDrinkEffect( from );

				this.Consume();
			}
		}
	}
}
