using System;
using Server.Network;
using Server.Prompts;
using Server.Mobiles;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Targeting;
using Server.Targets;

namespace Server.Items
{
	public class LevelUpScroll : Item
	{
        private int m_Value;
        private bool m_BlacksmithValidated;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Value
        {
            get
            {
                return m_Value;
            }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool BlacksmithValidated
        {
            get { return m_BlacksmithValidated; }
            set { m_BlacksmithValidated = value; InvalidateProperties(); }
        }

		[Constructable]
        public LevelUpScroll( int value ): base(0x573C)
		{
			Weight = 1.0;
			Name = "Enhancement Rune";
            m_Value = value;
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (m_Value == 5.0)
                list.Add("a wonderous rune of enhancing (+{0} max levels)", m_Value);
            else if (m_Value == 10.0)
                list.Add("an exalted rune of enhancing (+{0} max levels)", m_Value);
            else if (m_Value == 15.0)
                list.Add("a mythical rune of enhancing (+{0} max levels)", m_Value);
            else if (m_Value == 20.0)
                list.Add("a legendary rune of enhancing (+{0} max levels)", m_Value);
            else
                list.Add("a rune of enhancing (+{0} max levels)", m_Value);
        }

        public override void OnSingleClick(Mobile from)
        {
            if (m_Value == 5.0)
                base.LabelTo(from, "a wonderous rune of enhancing (+{0} max levels)", m_Value);
            else if (m_Value == 10.0)
                base.LabelTo(from, "an exalted rune of enhancing (+{0} max levels)", m_Value);
            else if (m_Value == 15.0)
                base.LabelTo(from, "a mythical rune of enhancing (+{0} max levels)", m_Value);
            else if (m_Value == 20.0)
                base.LabelTo(from, "a legendary rune of enhancing (+{0} max levels)", m_Value);
            else
                base.LabelTo(from, "a rune of enhancing (+{0} max levels)", m_Value);
        }

		public override void AddNameProperties( ObjectPropertyList list )
		{
            string BlacksmithMsg;
            bool IsBlacksmithOnly;

			base.AddNameProperties( list );

            IsBlacksmithOnly = LevelItems.BlacksmithOnly;

            if (IsBlacksmithOnly)
            {
                if (!m_BlacksmithValidated)
                    BlacksmithMsg = "(Must be validated by player with " + LevelItems.BlacksmithSkillRequired + "+ Blacksmithy skill)";
                else
                    BlacksmithMsg = "(Blacksmith Validated)";
            }
            else
                BlacksmithMsg = "";


            list.Add(1060847, "Legendary Artifacts Only\t {0}", BlacksmithMsg);
		}

		public LevelUpScroll( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

            //Version 0
            writer.Write(m_BlacksmithValidated);
            writer.Write((int)m_Value);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_BlacksmithValidated = reader.ReadBool();
                        m_Value = reader.ReadInt();
                        break;
                    }
            }
		}

		public override void OnDoubleClick( Mobile from )
		{
            bool IsBlacksmithOnly;

            IsBlacksmithOnly = LevelItems.BlacksmithOnly;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}
			else
			{
                if (IsBlacksmithOnly)
                {
                    if (m_BlacksmithValidated || (from.Skills[SkillName.Blacksmith].Value >= LevelItems.BlacksmithSkillRequired))
                    {
                        from.SendMessage("Which legendary artifact item would you like to enhance?");
                        from.Target = new LevelItemTarget(this); // Call our target
                    }
                    else
                    {
                        from.SendMessage("Please target one with a base Blacksmith skill of " + LevelItems.BlacksmithSkillRequired + " or higher.");
                        from.Target = new BlacksmithTarget(this); // Call our target
                    }
                }
                else
                {
				    from.SendMessage( "Which legendary artifact would you like to enhance?" );
				    from.Target = new LevelItemTarget( this ); // Call our target
                }
			}
		}

		public class LevelItemTarget : Target
		{
			private LevelUpScroll m_Scroll;

            public LevelItemTarget(LevelUpScroll scroll): base(-1, false, TargetFlags.None)
			{
				this.m_Scroll = scroll;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is Mobile )
				{
					from.SendMessage( "This rune cannot enhance that!" );
				}
                else if (target is Item)
                {
                    Item item = (Item)target;

                    if (item.RootParent != from || !item.IsChildOf(from.Backpack)) // Make sure its in their pack or they are wearing it
                    {
                        from.SendMessage("The legendary artifact must be in your pack to enhance.");
                    }
                    else
                    {
                        if (target is ILevelable)
                        {
                            ILevelable b = (ILevelable)target;

                            if ((b.MaxLevel + m_Scroll.Value) > LevelItems.MaxLevelsCap)
                            {
                                from.SendMessage("The level on this legendary artifact is already too high to use this rune!");
                            }
                            else
                            {
                                b.MaxLevel += m_Scroll.Value;
                                from.SendMessage("Your legendary artifact has been enhanced by " + m_Scroll.Value + " levels.");
                                m_Scroll.Delete();

                            }
                        }
                        else
                        {
                            from.SendMessage("This rune cannot enhance that!");
                        }
                    }
                }
                else
                {
                    from.SendMessage("This rune cannot enhance that!");
                }
			}
		}

        private class BlacksmithTarget : Target
        {
            private LevelUpScroll m_Scroll;

            public BlacksmithTarget(LevelUpScroll scroll) : base(-1, false, TargetFlags.None)
            {
                this.m_Scroll = scroll;
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (target is PlayerMobile)
                {
                    //check if bs skill is high enough
                    Mobile smith = (Mobile)target;
                    if (smith.Skills[SkillName.Blacksmith].Value < LevelItems.BlacksmithSkillRequired)
                    {
                        from.SendMessage("This one's blacksmith skill is not high enough to enhance legendary artifacts.");
                    }
                    else
                    {
                        from.SendMessage("This one is a skilled blacksmith.");
                        from.SendGump(new AwaitingSmithApprovalGump(m_Scroll, from));
                        smith.SendGump(new LevelUpAcceptGump(m_Scroll, from));	
                    }
                }
                else
                {
                    from.SendMessage("This one is not a skilled blacksmith!");
                }
            }
        }
	}
}