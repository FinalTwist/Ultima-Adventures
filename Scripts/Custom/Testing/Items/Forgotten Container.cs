//Created by Ashlar, beloved of Morrigan

using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using System.Collections.Generic;


namespace Server.Items
{
    [FlipableAttribute( 0x9B0, 0x1769, 0x176A, 0x176B, 0xE79, 0xE76 )]
    public class ForgottenContainer : LockableContainer
    {
        private bool m_HasFilled;
        private Mobile m_Owner;

        [CommandProperty( AccessLevel.GameMaster )]
        public virtual bool HasFilled { get { return m_HasFilled; } set { m_HasFilled = value; } }

        private static int[] m_ItemIDs = new int[]
		{
			0x9B0, 0x1769, 0x176A, 0x176B, 0xE79, 0xE76,
		};

        private static int[] m_Gumps = new int[]
		{
            9, 11, 64, 71, 76, 82, 104, 278, 1226, 1228, 2001, 2160, 2161, 2162, 2163
        };
        private static int[] m_Sounds = new int[]
		{
            0x1E0, 0x31E, 0x320, 0x324, 0x32F, 0x158, 0x42D, 0x542, 
            0x551, 0x554, 0x543, 0x556, 0x562
        };

        public override int DefaultGumpID { get { return Utility.RandomList( m_Gumps ); } }
        public override int DefaultDropSound { get { return Utility.RandomList( m_Sounds ); } }

        public override Rectangle2D Bounds
        {
            get { return new Rectangle2D( 46, 74, 150, 110 ); }
        }


        [Constructable]
        public ForgottenContainer() : base( Utility.RandomList(m_ItemIDs) )
        {
            Name = "a forgotten container";
            Hue = Utility.RandomNondyedHue();
        }

        public override void OnDoubleClick( Mobile from )
        {
            if ( !this.IsChildOf( from.Backpack ) && m_Owner == null )//you could set ownership while in a playervendor's backpack without this.
            {
                from.SendMessage( "That needs to be in your backpack" + from.Name.ToString() + "." );
            }
            else if ( m_Owner == null )//if no-one has opened it yet
            {
                m_Owner = from;
                this.Name = m_Owner.Name.ToString() + "'s loot.";
                DoFill( from );
                from.PlaySound( 0x548 );
                base.OnDoubleClick( from );
                from.SendMessage( "NOO! Release me!  I was happy when i was forgotten!" );
            }
            else if ( m_Owner != from )//if it has an owner other than the person trying to open it
            {
                from.SendMessage( "It is trying to get away!!" );
                from.PlaySound( 0x549 );
                this.ItemID = Utility.RandomList( m_ItemIDs );
                return;
            }
            else if ( m_Owner == from )//if it is yours and you want to open it
            {
                if ( !LootPack.CheckLuck( (from.Luck * 5) / 2 ) )
                {
                    this.ItemID = Utility.RandomList( m_ItemIDs );
                    from.PlaySound( Utility.RandomList( m_Sounds ) );
                    switch ( Utility.Random( 20 )) 
                    {
                        case 0: from.SendAsciiMessage( 0x53, m_Owner.Name.ToString()+ " you touched me! How dare you! " ); break;
                        case 1: from.SendAsciiMessage( 0x52, "Your not very lucky " +m_Owner.Name.ToString()+"!" ); break;
                        case 2: from.SendAsciiMessage( 0x51, "Release me before you get gobbled by a monster " + m_Owner.Name.ToString() + "!" ); break;
                        case 3: from.SendAsciiMessage( 0x50, "I was happier when everyone forgot about me." ); break;
                        case 4: from.SendAsciiMessage( 0x4F, "Release me " + m_Owner.Name.ToString() + "!" ); break;
                        case 5: from.SendAsciiMessage( 0x4E, "I wish i was home, the dust was just right there." ); break;
                        case 6: from.SendAsciiMessage( 0x4D, "When in doubt, pout and SHOUT!" ); break;
                        case 7: from.SendAsciiMessage( 0x4C, "It isn't me!  Look, I'm different now!" ); break;
                        case 8: from.SendAsciiMessage( 0x4B, "Quit grabbing me there " + m_Owner.Name.ToString() + "!" ); break;
                        case 9: from.SendAsciiMessage( 0x4A, "Wouldn't you be cranky if you had to carry all this around " + m_Owner.Name.ToString() + "?" ); break;
                        case 10: from.SendAsciiMessage( 0x49, "Hey!  I let you in before didn't I?" ); break;
                        case 11: from.SendAsciiMessage( 0x48, "Dang your ugly " + m_Owner.Name.ToString() + "!" ); break;
                        case 12: from.SendAsciiMessage( 0x47, "My mother was a container and my dad was a shapeshifter, so..." ); break;
                        case 13: from.SendAsciiMessage( 0x46, "Zzz Zzz Zz-wha?  Let me rest! I plan to run away in the morning." ); break;
                        case 14: from.SendAsciiMessage( 0x45, "Dat's noice." ); break;
                        case 15: from.SendAsciiMessage( 0x44, "I do too know how to keep my trap shut!  You aren't getting in, are you?" ); break;
                        case 16: from.SendAsciiMessage( 0x43, m_Owner.Name.ToString() + ", you can always send me away you know..." ); break;
                        case 17: from.SendAsciiMessage( 0x42, "Out of my head at the moment, be back later." ); break;
                        case 18: from.SendAsciiMessage( 0x41, "Do you mind " + m_Owner.Name.ToString() + "?  I have a date in here!  Or is it a plum?" ); break;
                        case 19: from.SendAsciiMessage( 0x40, "I wonder what that release thingy does...hmm?" ); break;
                    }

                }
                else
                {
                    from.PlaySound( Utility.RandomList( m_Sounds ) );
                    switch ( Utility.Random( 7 )) 
                    {
                        case 0: from.SendAsciiMessage( 0x3F, "I reject your reality and substitute my own." ); break;
                        case 1: from.SendAsciiMessage( 0x3E, "Like the new look?  I changed it just to bug you " + m_Owner.Name.ToString() + "." ); break;
                        case 2: from.SendAsciiMessage( 0x3D, "Hey!  You opened me when i wasn't looking!" ); break;
                        case 3: from.SendAsciiMessage( 0x3C, "You sure know how to get inside a person don't you " + m_Owner.Name.ToString() + "?" ); break;
                        case 4: from.SendAsciiMessage( 0x3B, "Hey! Close me!  I'm not a flasher!" ); break;
                        case 5: from.SendAsciiMessage( 0x3A, "Quit poking me " + m_Owner.Name.ToString() + "!" ); break;
                        case 6: from.SendAsciiMessage( 0x39, "This isn't a peep show you, you ...Peeper!" ); break;
                    }
                    base.OnDoubleClick( from );
                }
            }
        }
        #region ContextMenu
        private class ReleaseEntry : ContextMenuEntry
        {
            private ForgottenContainer m_Item;
            private Mobile m_Mobile;

            public ReleaseEntry( Mobile from, Item item ) : base( 6118 ) // uses "MRelease" entry
            {
                m_Item = ( ForgottenContainer )item;
                m_Mobile = from;
            }

            public override void OnClick()
            {
                m_Item.Delete();
                m_Mobile.SendMessage( "You have released it to happily be forgotten again." );
                m_Mobile.SendMessage( "It seems to have left something behind..." );
                m_Mobile.AddToBackpack( new PowderOfTranslocation( 2 ) );
            }
        }
        public static void GetContextMenuEntries( Mobile from, Item item, List<ContextMenuEntry> list  )
        {
            list.Add( new ReleaseEntry( from, item ) );
        }
        public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list  )
        {
            if ( m_Owner == null )
            {
                return;
            }
            else
            {
                if ( m_Owner != from )
                {
                    from.SendMessage( "Only my owner can release me.  They forgot to. I'm forgetable you see." );
                    return;
                }
                else
                {
                    base.GetContextMenuEntries( from, list );
                    ForgottenContainer.GetContextMenuEntries( from, this, list );
                }
            }
        }
        #endregion

        private int level;

        public void DoFill( Mobile from )
        {
            level = 0;
            if ( from.Luck >= 0 )
                level = 1;
            if ( from.Luck >= 200 )
                level++;
            if ( from.Luck >= 600 )
                level++;
            if ( from.Luck >= 1400 )
                level++;
            if ( from.Luck >= 3000 )
                level++;
            else
            {
                level = 5;
            }
            Fill( level );
            HasFilled = true;
        }

        private static void GetRandomAOSStats( out int attributeCount, out int min, out int max )
        {
            int rnd = Utility.Random(15);

            if (rnd < 1)
            {
                attributeCount = Utility.RandomMinMax( 2, 6 );
                min = 20; max = 70;
            }
            else if (rnd < 3)
            {
                attributeCount = Utility.RandomMinMax( 2, 4 );
                min = 20; max = 50;
            }
            else if (rnd < 6)
            {
                attributeCount = Utility.RandomMinMax( 2, 3 );
                min = 20; max = 40;
            }
            else if (rnd < 10)
            {
                attributeCount = Utility.RandomMinMax( 1, 2 );
                min = 10; max = 30;
            }
            else
            {
                attributeCount = 1;
                min = 10; max = 20;
            }
        }

        private void Fill(int level)
        {
            switch (level)
            {
                case 1: RequiredSkill = 36; break;
                case 2: RequiredSkill = 76; break;
                case 3: RequiredSkill = 84; break;
                case 4: RequiredSkill = 92; break;
                case 5: RequiredSkill = 100; break;
            }

            DropItem(new Gold(level * 6));

            for (int i = 0; i < level * 2; ++i)
            {
                Item item;

                if (Core.AOS)
                    item = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing();
                else
                    item = Loot.RandomArmorOrShieldOrWeapon();

                if (item is BaseWeapon)
                {
                    BaseWeapon weapon = (BaseWeapon)item;

                    if (Core.AOS)
                    {
                        int attributeCount;
                        int min, max;

                        GetRandomAOSStats(out attributeCount, out min, out max);

                        BaseRunicTool.ApplyAttributesTo(weapon, attributeCount, min, max);
                    }
                    else
                    {
                        weapon.DamageLevel = (WeaponDamageLevel)Utility.Random(6);
                        weapon.AccuracyLevel = (WeaponAccuracyLevel)Utility.Random(6);
                        weapon.DurabilityLevel = (WeaponDurabilityLevel)Utility.Random(6);
                    }

                    DropItem(item);
                }
                else if (item is BaseArmor)
                {
                    BaseArmor armor = (BaseArmor)item;

                    if (Core.AOS)
                    {
                        int attributeCount;
                        int min, max;

                        GetRandomAOSStats(out attributeCount, out min, out max);

                        BaseRunicTool.ApplyAttributesTo(armor, attributeCount, min, max);
                    }
                    else
                    {
                        armor.ProtectionLevel = (ArmorProtectionLevel)Utility.Random(6);
                        armor.Durability = (ArmorDurabilityLevel)Utility.Random(6);
                    }

                    DropItem(item);
                }
                else if (item is BaseJewel)
                {
                    int attributeCount;
                    int min, max;

                    GetRandomAOSStats(out attributeCount, out min, out max);

                    BaseRunicTool.ApplyAttributesTo((BaseJewel)item, attributeCount, min, max);

                    DropItem(item);
                }
            }

            for (int i = 0; i < level; i++)
            {
                Item item = Loot.RandomPossibleReagent();
                item.Amount = Utility.RandomMinMax(1, 3);
                DropItem(item);
            }

            for (int i = 0; i < level; i++)
            {
                Item item = Loot.RandomGem();
                DropItem(item);
            }
        }

        public ForgottenContainer( Serial serial ) : base( serial )
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write( ( int )0 ); // version
            writer.Write( ( bool )m_HasFilled );
            writer.Write( m_Owner );
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_HasFilled = reader.ReadBool();
            m_Owner = reader.ReadMobile();
        }
    }
}