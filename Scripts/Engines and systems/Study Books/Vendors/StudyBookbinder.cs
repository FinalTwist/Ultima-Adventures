using System;
using System.Collections.Generic;
using Server.Engines.BulkOrders;
using Server.Items;

namespace Server.Mobiles
{
    public class StudyBookbinder : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        [Constructable]
        public StudyBookbinder()
            : base("the training manual bookbinder")
        {
            this.SetSkill(SkillName.Inscribe, 90.0, 100.0);
        }

        public StudyBookbinder(Serial serial)
            : base(serial)
        {
        }

        public override NpcGuild NpcGuild
        {
            get
            {
                return NpcGuild.MagesGuild;
            }
        }
        public override VendorShoeType ShoeType
        {
            get
            {
                return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;
            }
        }
        protected override List<SBInfo> SBInfos
        {
            get
            {
                return this.m_SBInfos;
            }
        }
        public override void InitSBInfo()
        {
            this.m_SBInfos.Add(new SBStudyBookbinder(this));
        }
		
		public override bool OnDragDrop(Mobile from, Item dropped)
        {
            PlayerMobile player = from as PlayerMobile;

            if (player != null && dropped is StudyBook)
            {
                StudyBook book = dropped as StudyBook;
				int rewardAmount = 1;
				if (book.MaxSkillTrained >= 1200)
					rewardAmount = 10000;//This is the amount that will be awarded for giving the vendor a 120-skill study book.
				else if (book.MaxSkillTrained >= 1000)
					rewardAmount = 2500;//This is the amount that will be awarded for giving the vendor a 100-skill study book.
				else
					rewardAmount = 500;//If you want the vendor to offer a reward for lower-skill books, change this amount from 0.
				
				if (rewardAmount > 0)
				{
					player.AddToBackpack(new Gold(rewardAmount));//if you wish to change the type of reward, do that here.
					this.Say("Thank you for returning this book to me.");
					player.SendMessage(0, "The vendor has rewarded you with {0} gold.", rewardAmount);//Be sure to change this message if you change the type of reward.
					book.Delete();
					return false;
				}
			}

            return base.OnDragDrop(from, dropped);
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            this.AddItem(new Server.Items.Robe(Utility.RandomNeutralHue()));
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