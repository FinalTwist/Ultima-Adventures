using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Mobiles.Data;
using Server.OneTime;

namespace Server.Mobiles
{
    public class PlayDirectorArrival: BaseCreature, IOneTime // name the director to play[whatever] here and everywhere else
    {
		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

        private Mobile Actor1;

        private Mobile Actor2;

        private int PlayStep; // scene of the play

        private bool ActivePlay; // is the play running?

        private int PlayCheck; // 0 is inactive, 1 is no response (play only), 2+ is response needed

        private int CheckCount; 

        private Mobile lastplayed;

        [Constructable]
        public PlayDirectorArrival()
            : this(Utility.RandomBool(), null, null)
        {
        }

        [Constructable]
        public PlayDirectorArrival( bool sex)
            : this(sex, null, null)
        {
        }

        [Constructable]
        public PlayDirectorArrival(bool sex, string name)
            : this(sex, name, null)
        {
        }

        [Constructable]
        public PlayDirectorArrival(bool sex, string name, string title) //no need to change anything here
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
            SpeechHue = Utility.RandomDyedHue();
            Hidden = true;

            PlayStep = 0;
            PlayCheck = 0;
            CheckCount = 0;
            ActivePlay = false;

            Actor1 = null;
            Actor2 = null;
            lastplayed = null;

            InitOutfit();
            InitPlay();

        }


        public virtual void InitOutfit() // this just dresses the director
        {
            int hueRange;

            switch ( Utility.Random(9)  )
            {
                case 1: break; // barefoot poor
                case 2: AddItem(new Shoes(GetShoeHue())); break; // poor, normal
                case 3: AddItem(new Sandals(GetShoeHue())); break; // poor, rich
                default:
                case 4: AddItem(new Shoes(GetShoeHue())); break; // normal
                case 6: AddItem(new Boots(GetShoeHue())); break; // normal, rich
                case 9: AddItem(new ThighBoots(GetShoeHue())); break; // rich
            }

            if (Female)
            {
                hueRange = Utility.Random(5); //get a color scheme


                            switch (Utility.Random(4))
                            {
                                case 0:
                                    {
                                        AddItem(new PlainDress(GetRandomHueRange(hueRange)));
                                        break;
                                    }
                                case 1:
                                    {
                                        AddItem(new Skirt(GetRandomHueRange(hueRange)));
                                        AddItem(new Shirt(GetRandomHueRange(hueRange)));
                                        break;
                                    }
                                case 2:
                                    {
                                        AddItem(new LongPants(GetRandomHueRange(hueRange)));
                                        DoShirt(hueRange);
                                        break;
                                    }
                                case 3:
                                    {
                                        AddItem(new ShortPants(GetRandomHueRange(hueRange)));
                                        DoShirt(hueRange);
                                        break;
                                    }
                            }

                            switch (Utility.Random(5))
                            {
                                default: break;
                                case 0: AddItem(new Bonnet(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new FloppyHat(GetRandomHueRange(hueRange))); break;
                                case 2: AddItem(new Cap(GetRandomHueRange(hueRange))); break;
                            }

                            if (Utility.RandomDouble() < .08)
                                AddItem(new FullApron(Utility.RandomNeutralHue()));

                            if (Utility.RandomBool())
                                AddItem(new GoldRing());


                
            }

            else // Male 
            {
                hueRange = Utility.Random(3);

                            switch (Utility.Random(3))
                            {
                                case 0: AddItem(new FancyShirt(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new Doublet(GetRandomHueRange(hueRange))); break;
                                case 2: AddItem(new Shirt(GetRandomHueRange(hueRange))); break;
                            }

                            switch (Utility.Random(2))
                            {
                                case 0: AddItem(new LongPants(GetRandomHueRange(hueRange))); break;
                                case 1: AddItem(new ShortPants(GetRandomHueRange(hueRange))); break;
                            }

                            switch (Utility.Random(5))
                            {
                                default: break;
                                case 0: AddItem(new FloppyHat(Utility.RandomNeutralHue())); break;
                                case 1: AddItem(new FeatheredHat(GetRandomHueRange(hueRange))); break;
                            }

                            if (Utility.RandomDouble() < .16)
                                AddItem(new FullApron(Utility.RandomNeutralHue()));

                            if (Utility.RandomBool())
                                AddItem(new GoldRing());

                
            }
        }


        public void InitPlay() // this is what resets/sets up the play.  
        {
            if (ActivePlay)
            {
                ActivePlay = false;
                PlayStep = 0;
            }

            if (Actor1 == null) // this is where you set up Actor 1 (clothes, name etc)
            {
                Actor1 = new PlayActor( true );

                //assign clothing for actor 1 here
                Server.Misc.IntelligentAction.DressUpFighters( Actor1, "", false, 0 );
                Actor1.Name = "Itchy";
                Actor1.Title = "the Quick";

                Actor1.SpeechHue = 233;

                Actor1.MoveToWorld( new Point3D( 2981, 1040, 25), Map.Trammel ); //where do you want him before play starts?
            }

            if (Actor2 == null) // this is where you set up Actor 2 (clothes, name etc)
            {
                Actor2 = new PlayActor( true );

                //assign clothing for actor 1 here
                Server.Misc.IntelligentAction.DressUpFighters( Actor2, "", false, 0 );
                Actor2.Name = "Scratchy";
                Actor2.Title = "the Dull";

                Actor2.SpeechHue = 64;

                Actor2.MoveToWorld( new Point3D(2981,1041,25), Map.Trammel ); // where do you want him before play starts?
            }

                       if (Actor1.X != 2981 || Actor1.Y != 1040) // checking locations for the actors, where do you want them before play starts?
                            Actor1.MoveToWorld( new Point3D( 2981, 1040, 25), Map.Trammel );
                        if (Actor2.X != 2981 || Actor2.Y != 1041)
                            Actor2.MoveToWorld( new Point3D(2981,1041,25), Map.Trammel );
            Actor1.Direction = Direction.East; // this is how an actor looks towards something.  Actor2.Location is a Point3D, see how i did it here throughout
            Actor2.Direction = Direction.East; // this is how an actor looks towards something.  Actor2.Location is a Point3D, see how i did it here throughout

            PlayCheck = 0;

        }

        public void StartPlay() // this is what is called when the play starts (here the trigger is in onthink)
        {


                ActivePlay = true;
                PlayCheck = 1;
                PlayStep = 0;

        }
        public void OneTimeTick() // this is the timer for everything.  works in 1 second increments.  every second, everything here will run
        {
            //check to make sure everything is as it should be
            if (Actor1 == null || Actor2 == null )
                InitPlay(); // if any actors are missing, reset play

            if (ActivePlay)
            {

                if (PlayCheck == 1) // play is going to run playcheck is the play "scene"  here 1 means scene 1
                {
                    if (PlayStep == 0) // playstep is the "action" or step in the scene.  e.g. a player moves to a location or says something is a step
                    {
                        // play just started, check actors are in place
                      if (Actor1.X != 2981 || Actor1.Y != 1040) // checking locations for the actors, where do you want them before play starts?
                            Actor1.MoveToWorld( new Point3D( 2981, 1040, 25), Map.Trammel );
                        if (Actor2.X != 2981 || Actor2.Y != 1041)
                            Actor2.MoveToWorld( new Point3D(2981,1041,25), Map.Trammel );    
                    Actor1.Direction = Direction.East; // this is how an actor looks towards something.  Actor2.Location is a Point3D, see how i did it here throughout
                    Actor2.Direction = Direction.East; // this is how an actor looks towards something.  Actor2.Location is a Point3D, see how i did it here throughout
       
                    }
                    if (PlayStep == 2) // every "playstep" is one second.  thats how you time actions.  so from playstep 1 to playstep 5 is 4 seconds
                    {
                        Actor1.Say("Look, Bud....");
                    }
                    if (PlayStep == 3)
                    {
                        Actor2.Say("Yeah I see... lol");
                    }
                    if (PlayStep == 6)
                    {
                        Actor1.Say("Its a new adventurer.");
                    }
                    if (PlayStep == 9)
                    {
                        Actor2.Say("Called " + lastplayed.Name);
                    }
                    if (PlayStep == 12)
                    {
                        Actor1.Say("Strange name.");
                    }
                    if (PlayStep == 14)
                    {
                        Actor2.Say("How long you wager until Mangar finds him?");
                    }
                    if (PlayStep == 19)
                    {
                        Actor1.Say("Hmmmm... I'll give him 5 days.");
                    }
                    if (PlayStep == 24)
                    {
                        Actor2.Say("I'll take that bet.");
                    }
                    if (PlayStep == 27)
                    {
                        Actor1.Say("You're on!");
                    }
                    if (PlayStep > 27)   
                    {          
                        InitPlay();
                        ActivePlay = false;  
                    }
                    PlayStep++;
                }
  
            }
		}

        public void MoveActor( Mobile actor, Point3D go) // use this to move an actor somewhere
        {
            WayPoint GoHere = new WayPoint();
			GoHere.Map = actor.Map;
			GoHere.Location = go;
            ((PlayActor)actor).CurrentWayPoint = GoHere;
        }

        public override void OnAfterDelete() // if you delete the director, what happens
        {
            if (Actor1 != null)
                Actor1.Delete();
            if (Actor2 != null)
                Actor2.Delete();
        }

        public virtual int GetShoeHue()
        {

            return Utility.RandomNeutralHue();
        }

        public virtual int GetRandomHueRange(int range)
        {
            // Used to create color coordinated outfits.
            // Passing 0-4 will return a random hue in a set range
            // 5-9 will return a Netural hue, above 9 is modded to 0-9
            switch (range % 10)
            {
                default:
                case 0: return Utility.RandomNeutralHue();
                case 1: return Utility.RandomBlueHue();
                case 2: return Utility.RandomGreenHue();
                case 3: return Utility.RandomRedHue();
                case 4: return Utility.RandomYellowHue();
            }
        }

        public virtual void DoShirt(int hues)
        {
            switch (Utility.Random(2))
            {
                case 0: AddItem(new Doublet(GetRandomHueRange(hues))); break;
                case 1: AddItem(new Shirt(GetRandomHueRange(hues))); break;
            }
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
               InitPlay();
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {

        }

        public override bool HandlesOnSpeech(Mobile from)
        {

            return true;

        }

        public override void OnThink() // this is constantly running - here when he detects a newwplayer play starts.  this can be used for trigger
        {
            if (Actor1 == null || Actor2 == null )
                InitPlay();

            if (!Hidden) // keep me hidden!
                Hidden = true;



            if (!ActivePlay ) // look for new players
            {
                Mobile spect = null;
               foreach ( Mobile mob in this.GetMobilesInRange( 10 ) )
                {
                    if (mob is PlayerMobile && mob.Player && InLOS( mob ) && mob.Z == this.Z ) 
                    {
                       spect = mob;
                    }
                }
                if (spect != null && lastplayed != spect)
                {
                    lastplayed = spect;
                    StartPlay();
                }
            }

            base.OnThink(); 
        }

        public override void OnSpeech(SpeechEventArgs e) // if the director hears anything, wha happens?
        {

			if ( !(e.Mobile is PlayerMobile) )
				return;

            Mobile m = e.Mobile;
			if (  m.AccessLevel > AccessLevel.Player && Insensitive.Contains( e.Speech, "start" ) )
			{
                StartPlay();
            } 

			if (  m.Player && Insensitive.Contains( e.Speech, "mangar" ) )
			{
                Actor1.Say("I'm not saying a thing buddy.");
            } 
                
            base.OnSpeech(e);

        }


        public PlayDirectorArrival(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
            writer.Write((Mobile)Actor1);
            writer.Write((Mobile)Actor2);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {

                case 1:
                    {
                        Actor1 = reader.ReadMobile();
                        Actor2 = reader.ReadMobile();
                        goto case 0;
                    }
                case 0:
                    {
                        // obsolete version
                        break;
                    }
            }

            m_OneTimeType = 3;

        }



    }
}
