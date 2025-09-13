using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Mobiles.Data;
using Server.OneTime;

namespace Server.Mobiles
{
    /*
    #region Some Objects
    public struct SpeechResponse
    {
        public string Response;
        public Mobile Speaker;
        public int Animation;
        public int Reaction;
        public string Reward;
        public string DelObject;

        public SpeechResponse(string response, Mobile speaker, int animationID, int reactionID, string rewardObject, string QuestObject2Delete)
        {
            Response = response;
            Speaker = speaker;
            Animation = animationID;
            Reaction = reactionID;
            Reward = rewardObject;
            DelObject = QuestObject2Delete;
        }
    }

    public class ReactionCallBackState
    {
        private Mobile m_Mobile;
        private int m_Reaction;

        public Mobile Mobile { get { return m_Mobile; } }
        public int Reaction { get { return m_Reaction; } }

        public ReactionCallBackState(Mobile speaker, int reactNum)
        {
            m_Mobile = speaker;
            m_Reaction = reactNum;
        }
    }
    #endregion*/

    public class PlayActor : BaseCreature, IOneTime
    {

		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

        private Direction oldDirection;
        public override bool CanOpenDoors { get { return true; } }
        public override bool Unprovokable { get { return true; } }
		public override bool InitialInnocent{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

        // Does the Attacker become Criminal?
        private bool m_criminalAction = false; //default

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AttackIsCriminal
        {
            get { return m_criminalAction; }
            set { m_criminalAction = value; }
        }

        # region Constructors
        [Constructable]
        public PlayActor()
            : this(Utility.RandomBool(), null, null)
        {
        }

        public PlayActor( bool sex)
            : this(sex, null, null)
        {
        }

        [Constructable]
        public PlayActor(bool sex, string name)
            : this(sex, name, null)
        {
        }

        [Constructable]
        public PlayActor(bool sex, string name, string title)
            : base(AIType.AI_PlayActor, FightMode.None, 10, 1, 0.8, 1.6)
        {

            m_OneTimeType = 3;


            Name = name;
            Title = title;

            SetStr(45, 90);
            SetDex(35, 70);
            SetInt(35, 70);

            SetHits(35, 120);
            SetStam(60, 80);
            SetMana(25, 50);

            SetDamage(7, 25);
            SetDamageType(ResistanceType.Physical, 100);
            SetResistance(ResistanceType.Physical, 10, 20);

            VirtualArmor = 30;

            if (!sex)
            {
                Body = 0x191;
                if (Name == null)
                    Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                if (Name == null)
                    Name = NameList.RandomName("male");

                // add beard
                switch (Utility.Random(7))
                {
                    default: FacialHairItemID = 0x00; break; //None
                    case 0: FacialHairItemID = 0x2041; break; //Mustache
                    case 1: FacialHairItemID = 0x203F; break; //ShortBeard
                    case 2: FacialHairItemID = 0x204D; break; //Vandyke
                    case 3:
                        {
                            if (Utility.RandomBool())
                                FacialHairItemID = 0x203E; //LongBeard
                            else 
                                FacialHairItemID = 0x2040; //Goatee
                            break;
                        }
                }
            }

            switch (Utility.Random(7))
            {
                case 0: HairItemID = 0x2047; break; //Afro
                case 1: HairItemID = 0x2045; break; //PageboyHair
                case 2: HairItemID = 0x203D; break; //PonyTail
                case 3: HairItemID = 0x203B; break; //ShortHair
                case 4: HairItemID = 0x2049; break; //TwoPigTails
                case 5: HairItemID = 0x203C; break; //LongHair
                case 6:
                    {
                        if (Female)
                            HairItemID = 0x2046; //BunsHair
                        else
                            HairItemID = 0x2048; //ReceedingHair
                        break;
                    }
            }

            Hue = Utility.RandomSkinHue();
            HairHue = Utility.RandomHairHue();
            FacialHairHue = HairHue;
            SpeechHue = 0;

            //AIFullSpeedActive = AIFullSpeedPassive = true;
            CurrentSpeed = 0.1;
            PassiveSpeed = 0.1;
            ActiveSpeed = 0.1;

        }
        # endregion


        public void OneTimeTick()
        {

		}


        #region Utility

        public override void OnAfterDelete()
        {

        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            //base.AggressiveAction(aggressor, m_criminalAction);


        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            //base.OnDamage(amount, from, willKill);

        }

		public override void OnDoubleClick( Mobile from )
		{
            if (from.AccessLevel > AccessLevel.Player)
                Strip( this );
        }


        public static void Strip(Mobile from)
        {
            DeleteByLayer(from, Layer.OneHanded);
            DeleteByLayer(from, Layer.TwoHanded);
            DeleteByLayer(from, Layer.Shoes);
            DeleteByLayer(from, Layer.Pants);
            DeleteByLayer(from, Layer.Shirt);
            DeleteByLayer(from, Layer.Helm);
            DeleteByLayer(from, Layer.Gloves);
            DeleteByLayer(from, Layer.Ring);
            DeleteByLayer(from, Layer.Neck);
            DeleteByLayer(from, Layer.Talisman);
            DeleteByLayer(from, Layer.Waist);
            DeleteByLayer(from, Layer.InnerTorso);
            DeleteByLayer(from, Layer.Bracelet);
            DeleteByLayer(from, Layer.MiddleTorso);
            DeleteByLayer(from, Layer.Earrings);
            DeleteByLayer(from, Layer.Arms);
            DeleteByLayer(from, Layer.Cloak);
            DeleteByLayer(from, Layer.OuterTorso);
            DeleteByLayer(from, Layer.OuterLegs);
            DeleteByLayer(from, Layer.InnerLegs);
        }

        private static void DeleteByLayer(Mobile from, Layer layer)
        {
            Item item = from.FindItemOnLayer(layer);

            if (item != null && item.Movable)
                item.Delete();
        }



        #endregion

        #region SpeechHandlers
        public override void OnMovement(Mobile m, Point3D oldLocation)
        {

        }

        public override bool HandlesOnSpeech(Mobile from)
        {

                return true;

        }


        public override void OnSpeech(SpeechEventArgs e)
        {
            base.OnSpeech(e);

        }


        #endregion

        # region Serialize
        public PlayActor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
            writer.Write((bool)m_criminalAction);
            writer.Write((int)SpeechHue);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {

                case 1:
                    {
                        m_criminalAction = reader.ReadBool();
                        SpeechHue = reader.ReadInt();
                        goto case 0;
                    }
                case 0:
                    {
                        // obsolete version
                        break;
                    }
            }

            CurrentSpeed = 0.1;
            PassiveSpeed = 0.1;
            ActiveSpeed = 0.1;

            m_OneTimeType = 3;

        }
        # endregion

        # region Overrides
        public override void OnThink()
        {


            //base.OnThink(); 
        }
        # endregion

    }
}
