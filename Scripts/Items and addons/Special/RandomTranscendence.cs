

using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class RandomTranscendence : Item
    {
        [Constructable]
        public RandomTranscendence() : base(  )
        {
            Name = "random transcendence scroll";
			ItemID = 0x14EF;
			Hue = 0x490;
            Weight = 1.0;
        }

        public RandomTranscendence( Serial serial ) : base( serial )
        {
        }

        public override void OnDoubleClick( Mobile from )
        {
            if ( !IsChildOf( from.Backpack ) )
            {
                from.SendMessage( "This must be in your backpack to read." );
                return;
            }
            else
            {
                from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You read the scroll and find out its true power.", from.NetState);

				SkillName skill = (SkillName)Utility.Random( SkillInfo.Table.Length );

				double random = Utility.RandomDouble();

				if (random <= 0.50)
					from.AddToBackpack ( new ScrollofTranscendence ( skill, 0.1 ) );
				else if (random <= 0.80)
					from.AddToBackpack ( new ScrollofTranscendence ( skill, 0.2 ) );
				else
					from.AddToBackpack ( new ScrollofTranscendence ( skill, 0.3 ) );
				
                this.Delete();
            }
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add( 1049644, "Read To Determine The Type");
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