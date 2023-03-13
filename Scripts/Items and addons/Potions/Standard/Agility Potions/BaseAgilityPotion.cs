// coded by ßåяя¥ 2.0
using System;
using Server;

namespace Server.Items
{
	public abstract class BaseAgilityPotion : BasePotion
	{
		public abstract int DexOffset{ get; }
		public abstract TimeSpan Duration{ get; }

		//public override int Hue{ get { return 0; } }

		public BaseAgilityPotion( PotionEffect effect ) : base( 0xF08, effect )
		{
		}

		public BaseAgilityPotion( Serial serial ) : base( serial )
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

		public bool DoAgility( Mobile from )
		{
			int scale = Scale(from, DexOffset);
            		if (Spells.SpellHelper.AddStatOffset(from, StatType.Dex, scale, Duration))
			// TODO: Verify scaled; is it offset, duration, or both?
			//if ( Spells.SpellHelper.AddStatOffset( from, StatType.Dex, Scale( from, Server.Misc.MyServerSettings.PlayerLevelMod( DexOffset, from ) ), Duration ) )
			{
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				
				BuffInfo.AddBuff(from, new BuffInfo(BuffIcon.Agility, 1075841, Duration, from, scale.ToString()));
				
				return true;
			}

			from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.
			return false;
		}

		public override void Drink( Mobile from )
		{
			if ( DoAgility( from ) )
			{
				BasePotion.PlayDrinkEffect( from );

				this.Consume();
			}
		}
	}
}
