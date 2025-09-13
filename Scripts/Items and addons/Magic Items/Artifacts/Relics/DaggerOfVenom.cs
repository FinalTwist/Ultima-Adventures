using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class DaggerOfVenom : Dagger, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }

		[Constructable]
		public DaggerOfVenom()
		{
			Name = "Dagger of Venom";
			Hue = 0x4F6;
			AosElementDamages.Physical = 50;
			AosElementDamages.Poison = 50;
		}

		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			base.OnHit( attacker, defender, damageBonus );

			if ( Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				switch ( Utility.RandomMinMax( 0, 4 ) ) 
				{
					case 0: defender.ApplyPoison( attacker, Poison.Lesser ); Misc.Titles.AwardKarma( attacker, -300, true ); break;
					case 1: defender.ApplyPoison( attacker, Poison.Regular ); Misc.Titles.AwardKarma( attacker, -400, true ); break;
					case 2: defender.ApplyPoison( attacker, Poison.Greater ); Misc.Titles.AwardKarma( attacker, -500, true ); break;
					case 3: defender.ApplyPoison( attacker, Poison.Deadly ); Misc.Titles.AwardKarma( attacker, -600, true ); break;
					case 4: defender.ApplyPoison( attacker, Poison.Deadly ); Misc.Titles.AwardKarma( attacker, -700, true ); break;
				}
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
			list.Add( 1049644, "Dripping With Venom" );
        }

		public DaggerOfVenom( Serial serial ) : base( serial )
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
	}
}
