using System;
using Server;
using Server.Spells.Fourth;

namespace Server.Items
{
	public class WhiteShard : SoulShard
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061206; } } // white shard	

		[Constructable]
		public WhiteShard() : this( 1 )
		{
		}

		[Constructable]
		public WhiteShard( int amount ) : base( amount )
		{
			
			SuccessfulCast = false;
			MaxCharges = 10;
			Stackable = false;
			Hue = 2904;
			Amount = amount;
			Light = LightType.Circle150;
		}

		public WhiteShard( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060658, "{0}\t{1}", "Healing charges", Charges + "/" + MaxCharges );  // ~1_val~: ~2_val~
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ))
			{
				if (HasCharges(from)) {
					GreaterHealSpell spell = new GreaterHealSpell( from, this ); 
					if (spell.Cast() && SuccessfulCast) {
						RemoveCharge();
						SuccessfulCast = false;
					}	
				}
				return;
			}
			else {
				from.SendLocalizedMessage( 1045158 ); //  You must have the item in your backpack to target it.
			}
		}
	}
}