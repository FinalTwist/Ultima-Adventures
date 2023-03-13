
using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class RandomAlacrity : Item
    {
        [Constructable]
        public RandomAlacrity() : base(  )
        {
            Name = "random alacrity scroll";
			ItemID = 0x14EF;
			Hue = 0x4AB;
            Weight = 1.0;
        }

        public RandomAlacrity( Serial serial ) : base( serial )
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

				from.AddToBackpack ( new ScrollofAlacrity ( skill ) );
				
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