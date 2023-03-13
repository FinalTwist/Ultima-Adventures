using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseManaRefreshPotion : BasePotion
	{
		public abstract int MinMana { get; }
		public abstract int MaxMana { get; }
		public abstract double Delay { get; }

		public BaseManaRefreshPotion( PotionEffect effect ) : base( 0x180F, effect )
		{
		}

		public BaseManaRefreshPotion( Serial serial ) : base( serial )
		{
		}

		public void DoMana( Mobile from )
		{
			int min = Scale( from, Server.Misc.MyServerSettings.PlayerLevelMod( MinMana, from ) );
			int max = Scale( from, Server.Misc.MyServerSettings.PlayerLevelMod( MaxMana, from ) );

			from.Mana = from.Mana + ( Utility.RandomMinMax( min, max ) );
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

		public override void Drink( Mobile from )
		{
			if ( from.Mana < from.ManaMax )
			{
				if ( from.BeginAction( typeof( BaseManaRefreshPotion ) ) )
				{
					DoMana( from );

					BasePotion.PlayDrinkEffect( from );

					this.Consume();

					Timer.DelayCall( TimeSpan.FromSeconds( Delay ), new TimerStateCallback( ReleaseManaLock ), from );
				}
				else
					from.LocalOverheadMessage( MessageType.Regular, 0x22, true, "You must wait 10 seconds before using another mana potion." );

			}
			else
				from.SendMessage( "You decide against drinking this potion, as you are already at full mana." );
		}

		private static void ReleaseManaLock( object state )
		{
			((Mobile)state).EndAction( typeof( BaseManaRefreshPotion ) );
		}
	}
}
