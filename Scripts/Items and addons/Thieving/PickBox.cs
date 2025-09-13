using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	[Flipable( 0x1C0E, 0x1C0F )]
    public class PickBoxEasy : LockableContainer
    {
        [Constructable]
        public PickBoxEasy(): base( 0x1C0E )
        {
			Name = "practice lockbox (easy)";
			Hue = 0xB61;
            Locked = true;
            LockLevel = 1;
            MaxLockLevel = 25;
            RequiredSkill = 1;
            Weight = 4.0;
			Movable = false;
        }

        public override void LockPick(Mobile from)
        {
            this.Locked = true;
            from.SendMessage("The container relocks itself.");
        }

        public PickBoxEasy(Serial serial) : base(serial)
        {
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
	/////////////////////////////////////////////////////////////////////////////////////////
	[Flipable( 0x1C0E, 0x1C0F )]
    public class PickBoxNormal : LockableContainer
    {
        [Constructable]
        public PickBoxNormal(): base( 0x1C0E )
        {
			Name = "practice lockbox (normal)";
			Hue = 0xB61;
            Locked = true;
            LockLevel = 20;
            MaxLockLevel = 35;
            RequiredSkill = 20;
            Weight = 4.0;
			Movable = false;
        }

        public override void LockPick(Mobile from)
        {
            this.Locked = true;
            from.SendMessage("The container relocks itself.");
        }

        public PickBoxNormal(Serial serial) : base(serial)
        {
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
	/////////////////////////////////////////////////////////////////////////////////////////
	[Flipable( 0x1C0E, 0x1C0F )]
    public class PickBoxDifficult : LockableContainer
    {
        [Constructable]
        public PickBoxDifficult(): base( 0x1C0E )
        {
			Name = "practice lockbox (difficult)";
			Hue = 0xB61;
            Locked = true;
            LockLevel = 30;
            MaxLockLevel = 45;
            RequiredSkill = 30;
            Weight = 4.0;
			Movable = false;
        }

        public override void LockPick(Mobile from)
        {
            this.Locked = true;
            from.SendMessage("The container relocks itself.");
        }

        public PickBoxDifficult(Serial serial) : base(serial)
        {
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
	/////////////////////////////////////////////////////////////////////////////////////////
	[Flipable( 0x1C0E, 0x1C0F )]
    public class PickBoxChallenging : LockableContainer
    {
        [Constructable]
        public PickBoxChallenging(): base( 0x1C0E )
        {
			Name = "practice lockbox (challenging)";
			Hue = 0xB61;
            Locked = true;
            LockLevel = 40;
            MaxLockLevel = 55;
            RequiredSkill = 40;
            Weight = 4.0;
			Movable = false;
        }

        public override void LockPick(Mobile from)
        {
            this.Locked = true;
            from.SendMessage("The container relocks itself.");
        }

        public PickBoxChallenging(Serial serial) : base(serial)
        {
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
	/////////////////////////////////////////////////////////////////////////////////////////
	[Flipable( 0x1C0E, 0x1C0F )]
    public class PickBoxHard : LockableContainer
    {
        [Constructable]
        public PickBoxHard(): base( 0x1C0E )
        {
			Name = "practice lockbox (hard)";
			Hue = 0xB61;
            Locked = true;
            LockLevel = 50;
            MaxLockLevel = 65;
            RequiredSkill = 50;
            Weight = 4.0;
			Movable = false;
        }

        public override void LockPick(Mobile from)
        {
            this.Locked = true;
            from.SendMessage("The container relocks itself.");
        }

        public PickBoxHard(Serial serial) : base(serial)
        {
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