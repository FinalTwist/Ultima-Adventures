using System;
using Server.Items;
using Server;
using Server.Misc;
//using Server.AllHues;


namespace Server.Mobiles
{
	public class Wench : BaseCreature
	{
        private static bool m_Talked; // flag to prevent spam 

        string[] kfcsay = new string[] 
	  {
			"Come here to me. Let me relax you.  Just give me some gold.",
			"If you have some gold for me, we can have some fun.",
            "I will give you a nice massage if you give me some coins.",
			"The more you give me the more I can do for you..."
		};
        string[] kfcsay2 = new string[] 
	  {
			"Hey, I am waiting for customers!",
			"Go away!",
            "This is my territory!",
		};
		[Constructable]
		public Wench()
			: base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			InitStats( 31, 41, 51 );

            Title = "the wench";
            Body = 0x191; // female
            Name = NameList.RandomName(" female");

            switch (Utility.Random(3))
            {
                case 0: AddItem(new GoldNecklace()); break;
                case 1: AddItem(new GoldRing()); break;
                case 2: AddItem(new GoldBracelet()); break;
            }

            AddItem(new FemalePlateChest());

			Container pack = new Backpack();
			pack.DropItem( new Gold( 150, 300 ) );
			pack.Movable = false;
			AddItem( pack );
		}

		public override bool ClickTitle { get { return false; } }

        #region gold dropped

        public override bool OnGoldGiven(Mobile from, Gold dropped)
        {
				bool stolen = false;
                Direction = GetDirectionTo(from);
                if (!from.Female)
                {

                    if (dropped.Amount <= 9)

                        SayTo(from, "Thank you, but for that not even a goat would be at your service!");

                    if (dropped.Amount >= 10)
                    {
                        if (dropped.Amount <= 99 && dropped.Amount <= from.HitsMax - from.Hits)
                        {
                            from.Hits = dropped.Amount;
                        SayTo(from, "Let me give you a relaxing massage... on your shoulders.");
                        from.SendMessage("Your back feels relaxed.");
                        }

                        else if (dropped.Amount <= 99 && dropped.Amount >= from.HitsMax - from.Hits)
                        {
                            from.Hits += from.HitsMax;
                        SayTo(from, "Let me use my hands... I can do more for more gold... *wink*");
                        from.SendMessage("Her hands are very soft.");
                        }

                        else if (dropped.Amount <= 500)
                        {
                            from.Hits = from.HitsMax;
                            from.Mana = from.ManaMax;
                            from.Stam = from.StamMax;
                        SayTo(from, "Hmm... let me go on my knees...");
                        from.SendMessage("Amazing!! You would marry this girl!");
                        }
						
                        else if (dropped.Amount > 500)
                        {
							int statchange = Utility.RandomMinMax(1, 7);
							if (statchange == 2)
								from.RawStr += 1;
							else if (statchange == 4)
								from.RawDex += 1;
							else if (statchange == 5)
								from.RawDex += 1;
							else if (statchange == 7)
								from.RawInt -= 1;
							SayTo(from, "Wow big boy... I'm all yours for the taking... ");
                            from.SendMessage("You gave it your all, and REALLY enjoy yourself...");

                        }
						
						
                        switch (Utility.Random(6))
                        {
                            case 0:
                                {
                                    from.PlaySound(from.Female ? 811 : 1085);
                                    from.Say("oooh");
                                    break;
                                }
                            case 1:
                                {
                                    PlaySound(800);
                                    from.PlaySound(from.Female ? 800 : 1072);
                                    from.Emote("*kiss*");
                                    break;
                                }
                            case 2:
                                {
                                    from.PlaySound(from.Female ? 816 : 1090);
                                    from.Emote("*sighs*");
                                    break;
                                }
                            case 3:
                                {
                                    from.PlaySound(from.Female ? 823 : 1097);
                                    from.Say("yeah!");
                                    break;
                                }
                            case 4:
                                {
                                    from.PlaySound(from.Female ? 795 : 1067);
                                    from.Emote("*groans*");
                                    break;
                                }
                            case 5:
                                {
                                    from.PlaySound(from.Female ? 778 : 1049);
                                    from.Say("It's so big!");
                                    break;
                                }
                        }
						
						
						if (Utility.RandomDouble() > 0.92 && from.Backpack != null)
						{
						Item gold = from.Backpack.FindItemByType( typeof ( Gold ) );
						while (gold != null)
						{
							gold.Delete();
							gold = from.Backpack.FindItemByType( typeof ( Gold ) );
						}
						from.PrivateOverheadMessage(Network.MessageType.Regular, 0x3B2, false, "You notice your backpack is quite a bit lighter... wait, where did she go?", from.NetState);
						this.Delete();
						stolen = true;
						return true;
						}						
						
						
						LoggingFunctions.LogWench( from, dropped.Amount, stolen );
                    }
                    dropped.Delete();	
                    return true;
                }
                else
                {
                    SayTo(from, "Hey, leave me alone!");
                    from.SendMessage("She takes your money and turns around.");
                    from.SendMessage("She's sexy when she plays hard to get.");
                    return true;
                }
        
        }
#endregion

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false)
            {

                if (m.InRange(this, 2) )
                {
                    if (!m.Female)
                    {
                        m_Talked = true;
                        SayRandom(kfcsay, this);
                        this.Move(GetDirectionTo(m.Location));
                        // Start timer to prevent spam 
                        SpamTimer t = new SpamTimer();
                        t.Start();
                    }
                    if (m.Female)
                    {
                        m_Talked = true;
                        SayRandom(kfcsay2, this);
                        //this.Move(GetDirectionTo(m.Location));
                        // Start timer to prevent spam 
                        SpamTimer t = new SpamTimer();
                        t.Start();
                    }
                }
            }
        }

        private class SpamTimer : Timer
        {
            public SpamTimer()
                : base(TimeSpan.FromSeconds(8))
            {
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Talked = false;
            }
        }

        private static void SayRandom(string[] say, Mobile m)
        {
            m.Say(say[Utility.Random(say.Length)]);
        }

		public Wench( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
