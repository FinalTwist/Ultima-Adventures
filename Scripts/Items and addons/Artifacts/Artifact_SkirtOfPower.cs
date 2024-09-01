using System;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class SkirtOfPower : Skirt
	{

		[Constructable]
		public SkirtOfPower()
		{
          Name = "Skirt Of POWER";
	      Attributes.LowerManaCost = 25;
		  Attributes.SpellDamage = 50;
		  Attributes.CastSpeed = 1;
		  Attributes.BonusInt = 10;
		  Hue = 0x816;
		  //give power to resist curse
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public SkirtOfPower( Serial serial ) : base( serial )
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

		public static bool TryBlockCurse(Mobile m)
		{
            if (m is PlayerMobile && ((PlayerMobile)m).Sorcerer())
            {
                Item pants = ((PlayerMobile)m).FindItemOnLayer(Layer.OuterLegs);
                if (pants != null && pants is SkirtOfPower)
                {
                    m.PrivateOverheadMessage(MessageType.Regular, 19, false, "The weave of your skirt protects you from negative forces.", m.NetState);
                    return true;
                }
            }

            return false;
		}
	}
}
