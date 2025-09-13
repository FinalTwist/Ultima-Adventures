#region About This Script - Do Not Remove This Header

#endregion About This Script - Do Not Remove This Header

using System;
using System.Threading;
using System.Collections;
using Server;
using Server.Network;
using Server.Scripts;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items.Crops
{
    [FlipableAttribute(0xE28, 0xF00)]
    public class SmokeweedBong : DrugSystem_Effect
    {
        [Constructable]
        public SmokeweedBong(): base(0xE28)
        {
            Name = "a water bong";
            this.Weight = 0.4;
            this.Hue = 1289;
        }

        public SmokeweedBong(Serial serial): base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Stam > from.StamMax / 2)
            {
            Container pack = from.Backpack;

            if (pack != null && pack.ConsumeTotal(typeof(SmokeweedLeaves), 10) )
 
            {              
                if (from.Body.IsHuman && !from.Mounted)
                {
                    from.Animate(34, 5, 1, true, false, 0);

                }
                from.PlaySound(Utility.Random(0x20, 2));
                from.SendMessage("You Pack A Bowl And Spark It Up!");
                from.Meditating = true;
                from.SendMessage("You Begin To Feel The Darkness Throughout Your Body!");
                from.PlaySound(from.Female ? 798 : 1070);
                from.Say("*hiccup!*");
                Highness = 120;

                if (from is PlayerMobile && !((PlayerMobile)from).High )
                    new DrugSystem_StonedTimer(from, Highness).Start();

                if (from is PlayerMobile)
                {
                    ((PlayerMobile)from).THC += 25;
                    if (((PlayerMobile)from).THC > 60)
                        ((PlayerMobile)from).THC = 60;
                }
            }

            else
            {
                from.SendMessage("You need 10 leaves to pack this bowl!");
                return;
            }
            }
        }

		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			list.Add( "Seeking the sweet focus, smoking the weed" ); 
            list.Add( "gives your task a higher chance to succeed" ); 
            
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
