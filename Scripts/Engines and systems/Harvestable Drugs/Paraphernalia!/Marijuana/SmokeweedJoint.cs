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
    [FlipableAttribute(0x1AA2, 0xF00)]
    public class SmokeweekJoint : DrugSystem_Effect
    {
        [Constructable]
        public SmokeweekJoint(): base(0x1420)
        {
            Name = "A Fat Hand-Rolled Blunt";
            this.Weight = 0.3;
            this.Hue = 1153;
        }

        public SmokeweekJoint(Serial serial): base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Stam > from.StamMax / 2)
            {
            Container pack = from.Backpack;

            if (pack != null && pack.ConsumeTotal(typeof(SmokeweekJoint), 1) )

            {
                if (from.Body.IsHuman && !from.Mounted)
                {
                    from.Animate(34, 5, 1, true, false, 0);
                }

                from.SendMessage("You Ignite A Spark On A Nearby Rock And Light Up A Doobie.");
                from.PlaySound(0x226);
                Highness = 25;

                if (Core.AOS)
                    from.FixedParticles(0x3735, 1, 30, 9503, EffectLayer.Waist);

                else
                    from.FixedEffect(0x3735, 6, 30);

                if (from is PlayerMobile && !((PlayerMobile)from).High )
                    new DrugSystem_StonedTimer(from, Highness).Start();

                if (from is PlayerMobile)
                {
                    ((PlayerMobile)from).THC += 15;
                    if (((PlayerMobile)from).THC > 60)
                        ((PlayerMobile)from).THC = 60;
                }

            }
            else
            {
                from.SendMessage("Your Must Have The Doobie In Your Pack To Toke It!");
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
