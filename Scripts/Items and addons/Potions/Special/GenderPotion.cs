using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class GenderPotion : Item
	{
        [Constructable]
        public GenderPotion() : base( 0x1FDC )
		{
            Name = "potion of gender change";
			Hue = 0xB46;
        }

        public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}

            from.Target = new InternalTarget( this );
        }

		public void Consume(Mobile from)
		{
            from.PlaySound(Utility.RandomList(0x30, 0x2D6));
            this.Delete();
            from.AddToBackpack(new Bottle());
        }

        public GenderPotion( Serial serial ) : base( serial )
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

        public class InternalTarget : Target
        {
            private GenderPotion _potion;

            public InternalTarget(GenderPotion potion) : base(5, false, TargetFlags.None)
            {
                _potion = potion;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is PlayerMobile && from == o)
                {
                    if (from.Body == 0x191)
                    {
                        int HairColor = from.HairHue;
                        from.Body = 0x190;
                        from.BodyValue = 0x190;
                        from.Female = false;
                        Utility.AssignRandomHair(from);
                        from.FacialHairItemID = Utility.RandomList(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269);
                        from.HairHue = HairColor;
                        from.FacialHairHue = HairColor;
                        from.SendMessage("Your body transforms into that of a man.");
                        _potion.Consume();
                    }
                    else if (from.Body == 0x190)
                    {
                        int HairColor = from.HairHue;
                        from.Body = 0x191;
                        from.BodyValue = 0x191;
                        from.Female = true;
                        Utility.AssignRandomHair(from);
                        from.FacialHairItemID = 0;
                        from.HairHue = HairColor;
                        from.FacialHairHue = HairColor;
                        from.SendMessage("Your body transforms into that of a woman.");
                        _potion.Consume();
                    }
                }
				else if (o is BaseCreature)
                {
					var creature = o as BaseCreature;

                    if (creature.Controlled
						&& creature.ControlMaster == from
						&& creature.Commandable
						&& !creature.Summoned
						&& !creature.IsAnimatedDead
						&& !creature.IsNecroFamiliar
						&& false == (
							creature is FrankenFighter
							|| creature is AerialServant
							|| creature is FrankenPorter
							|| creature is Robot
							|| creature is GolemFighter
							|| creature is GolemPorter
							|| creature is PackBeast
							|| creature is HenchmanMonster
							|| creature is HenchmanFighter
							|| creature is HenchmanWizard
							|| creature is HenchmanArcher
							|| creature is HenchmanFamiliar
							|| creature is BaseChild
							|| creature is JediMirage
							|| creature is SythProjection
							|| creature is Clown
							|| creature is Clone)
							)
					{
                        creature.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("*Looks at {0} in dismay*", from.Name));
                        creature.Female = !creature.Female;
                        creature.InvalidateProperties();
                        _potion.Consume();
                    }
                }
            }
        }
    }
}