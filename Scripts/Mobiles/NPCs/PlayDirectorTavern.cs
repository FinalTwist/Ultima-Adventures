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
    public class PlayDirectorTavern: BaseCreature, IOneTime // name the director to play[whatever] here and everywhere else
    {
		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

        private ArrayList m_WayPoints;
        private Mobile Actor1;

        private Mobile Actor2;

        private Mobile Actor3;

        private Mobile Actor4;

        private int PlayStep; // scene of the play

        private bool ActivePlay; // is the play running?

        private int PlayCheck; // 0 is inactive, 1 is no response (play only), 2+ is response needed

        private int CheckCount; 

        [Constructable]
        public PlayDirectorTavern()
            : this(Utility.RandomBool(), null, null)
        {
        }

        [Constructable]
        public PlayDirectorTavern( bool sex)
            : this(sex, null, null)
        {
        }

        [Constructable]
        public PlayDirectorTavern(bool sex, string name)
            : this(sex, name, null)
        {
        }

        [Constructable]
        public PlayDirectorTavern(bool sex, string name, string title) //no need to change anything here
            : base(AIType.AI_PlayActor, FightMode.None, 10, 1, 0.8, 1.6)
        {
            m_WayPoints = new ArrayList();

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
            Actor3 = null;
            Actor4 = null;

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

            //delete old actors
            if (Actor1 != null)
            Actor1.Delete();
            if (Actor2 != null)
            Actor2.Delete();
            if (Actor3 != null)
            Actor3.Delete();
            if (Actor4 != null)
            Actor4.Delete();

            if (ActivePlay)
            {
                ActivePlay = false;
                PlayStep = 0;
            }

            if (m_WayPoints == null)
                m_WayPoints = new ArrayList();
            foreach( WayPoint wp in m_WayPoints )
			{
                if (wp != null)
				    wp.Delete();     
            }         

            m_WayPoints.Clear(); 

                Actor1 = new PlayActor( true );

                //assign clothing for actor 1 here
                Actor1.AddItem( new PlateChest() );
                Actor1.AddItem( new PlateLegs() );
                Actor1.AddItem( new PlateGorget() );
                Actor1.AddItem( new PlateGloves() );
                Actor1.AddItem( new PlateHelm() );
                Actor1.AddItem( new Boots( ) );
                Actor1.Name = "John Henry";
                Actor1.Title = "the King";

                Actor1.SpeechHue = 233;

                Actor1.MoveToWorld( new Point3D( 3675, 3495, 3), Map.Trammel ); //where do you want him before play starts?
            


                Actor2 = new PlayActor( false );

                //assign clothing for actor 1 here
                Actor2.AddItem( new FancyDress( ) );
                Actor2.Name = "Kate";
                Actor2.Title = "the Fair";

                Actor2.SpeechHue = 64;

                Actor2.MoveToWorld( new Point3D(3675,3495,3), Map.Trammel ); // where do you want him before play starts?
            

                Actor3 = new PlayActor( true );

                Actor3.Name = "Hepatitis";
                Actor3.Title = "the bard";

                Item doublet = new JesterHat();
                Actor3.AddItem( doublet );

                Item halby = new JesterSuit();
                Actor3.AddItem( halby );

                Item kilt = new Kilt();
                Actor3.AddItem( kilt);

                Item boots = new JesterShoes();
                Actor3.AddItem( boots );

                Actor3.SpeechHue = 233;

                Actor3.MoveToWorld( new Point3D( 3683, 3497, 2), Map.Trammel ); // location before play starts
            


                Actor4 = new PlayActor( false );

                //assign clothing for actor 1 here
                Actor4.AddItem( new GildedDress( ) );
                Actor4.Name = "Marie";
                Actor4.Title = "the Kind";

                Actor4.SpeechHue = 64;

                Actor4.MoveToWorld( new Point3D(3674,3495,3), Map.Trammel ); // where do you want him before play starts?
            

                       if (Actor1.X != 3675 || Actor1.Y != 3498) // checking locations for the actors, where do you want them before play starts?
                            MoveActor(Actor1, new Point3D( 3675, 3498, 3));
                        if (Actor2.X != 3675 || Actor2.Y != 3495)
                            MoveActor(Actor2, new Point3D(3675,3495,3));
                        if (Actor3.X != 3683 || Actor3.Y != 3497)
                            MoveActor( Actor3, new Point3D( 3683, 3497, 2) );
                        if (Actor4.X != 3675 || Actor4.Y != 3495)
                            MoveActor(Actor4, new Point3D(3674,3495,3));

            PlayCheck = 0;

        }

        public void StartPlay( ) // this is what is called when the play starts (here the trigger is in onthink)
        {
            Actor3.Direction = Direction.East; // this is how an actor looks towards something.  Actor2.Location is a Point3D, see how i did it here throughout
            ActivePlay = true;
            PlayCheck = 1;
            PlayStep = 0;
        }
        public void OneTimeTick() // this is the timer for everything.  works in 1 second increments.  every second, everything here will run
        {
            //check to make sure everything is as it should be
            if (Actor1 == null || Actor2 == null || Actor3 == null || Actor4 == null)
                InitPlay(); // if any actors are missing, reset play

            if (ActivePlay)
            {

                if (PlayCheck == 1) // play is going to run playcheck is the play "scene"  here 1 means scene 1
                {
                    if (PlayStep == 0) // playstep is the "action" or step in the scene.  e.g. a player moves to a location or says something is a step
                    {
                        // play just started, check actors are in place
                       if (Actor1.X != 3675 || Actor1.Y != 3498) // checking locations for the actors, where do you want them before play starts?
                            MoveActor(Actor1, new Point3D( 3675, 3498, 3));
                        if (Actor2.X != 3675 || Actor2.Y != 3495)
                            MoveActor(Actor2, new Point3D(3675,3495,3));
                        if (Actor3.X != 3683 || Actor3.Y != 3497)
                            MoveActor( Actor3, new Point3D( 3683, 3497, 2) );
                        if (Actor4.X != 3675 || Actor4.Y != 3495)
                            MoveActor(Actor4, new Point3D(3674,3495,3));              }
                    if (PlayStep == 5) // every "playstep" is one second.  thats how you time actions.  so from playstep 1 to playstep 5 is 4 seconds
                    {
                        //move actor 2 down in front of cell
                        Actor3.Say("Hear Ye, Hear Ye!");
                    }
                    if (PlayStep == 7)
                    {
                        Actor3.Say("Hepatitis and Company will now perform 'The Ballad of the Bachelor'");
                    }
                    if (PlayStep == 11)
                    {
                        Actor3.Say("Sit, enjoy and remember to tip!'");
                    }
                    if (PlayStep == 20)
                    {
                        Actor3.Say("Listen, ladies, while I sing The ballad of John Henry King.");
                        
                    }
                    if (PlayStep == 25)
                    {
                        Actor3.Say("The Scene opens: A Castle Courtyard");
                    }
                    if (PlayStep == 33)
                    {
                        Actor3.Say("John Henry was a bachelor, His age was thirty-three or four.");
                        MoveActor(Actor1, new Point3D(3679,3496,2) );
                    }
                    if (PlayStep == 41)
                    {
                        Actor3.Say("Two maids for his affection vied, And each desired to be his bride,");
                    }
                    if (PlayStep == 45)
                    {
                        Actor1.Say("Yeah, this is great!");
                    }
                    if (PlayStep == 49)
                    {
                        Actor3.Say("And bravely did they strive to bring Unto their feet John Henry King.");
                        Actor3.Say("");
                    }
                    if (PlayStep == 54)
                    {
                        Actor1.Say("They sure tried!");
                    }
                   if (PlayStep == 58)
                    {
                        Actor3.Say("John Henry liked them both so well, To save his life he could not tell");
                    }
                    if (PlayStep == 67)
                    {
                        Actor3.Say("Which he most wished to be his bride, Nor was he able to decide.");
                    }
                   if (PlayStep == 73)
                    {
                        Actor1.Say("You know, they're both so great...");
                    }
                    if (PlayStep == 75)
                    {
                        MoveActor(Actor2, new Point3D(3677,3498,2) );
                    }
                    if (PlayStep == 79)
                    {
                        Actor3.Say("Fair Kate was jolly, bright, and gay, And sunny as a summer day;");
                    }
                    if (PlayStep == 83)
                    {
                        Actor2.Say("*blush*");
                        ((PlayActor)Actor2).Direction = Actor2.GetDirectionTo( Actor1.Location );
                    }
                    if (PlayStep == 85)
                    {
                        MoveActor(Actor4, new Point3D(3677,3495,2) );
                    }
                    if (PlayStep == 89)
                    {
                        Actor3.Say("Marie was kind, sedate, and sweet, With gentle ways and manners neat.");
                    }
                    if (PlayStep == 93)
                    {
                        Actor4.Say("*chuckles*");
                        ((PlayActor)Actor4).Direction = Actor4.GetDirectionTo( Actor1.Location );
                    }
                    if (PlayStep == 98)
                    {
                        Actor3.Say("Each was so dear that John confessed He could not tell which he liked best.");
                    }
                    if (PlayStep == 103)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor2.Location );
                        Actor1.Say("Kate, I love your peaches.");
                    }
                    if (PlayStep == 106)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor4.Location );
                        Actor1.Say("Marie, I love your tree.");
                    }
                    if (PlayStep == 109)
                    {
                        Actor3.Say("He studied them for quite a year, And still found no solution near,");
                    }
                    if (PlayStep == 113)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor4.Location );
                        Actor1.Say("you!");
                    }
                    if (PlayStep == 116)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor2.Location );
                        Actor1.Say("No... You!");
                    }
                    if (PlayStep == 119)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor4.Location );
                        Actor1.Say("No... You!");
                    }
                    if (PlayStep == 124)
                    {
                        MoveActor(Actor1, new Point3D(3680,3499,2));
                        Actor3.Say("And might have studied two years more Had he not, walking on the shore,");
                    }
                    if (PlayStep == 127)
                    {
                        MoveActor(Actor1, new Point3D(3680,3496,2));
                    }
                    if (PlayStep == 132)
                    {
                        MoveActor(Actor1, new Point3D(3680,3499,2));
                        Actor3.Say("Conceived a very simple way Of ending his prolonged delay");
                    }
                    if (PlayStep == 139)
                    {
                        MoveActor(Actor1, new Point3D(3680,3496,2));
                        Actor3.Say("A way in which he might decide Which of the maids should be his bride.");
                    }
                    if (PlayStep == 144)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor3.Location );
                        Actor1.Say("Aha!  I know!");
                    }
                    if (PlayStep == 148)
                    {
                        Actor1.Say("Can I have both?");
                    }
                    if (PlayStep == 152)
                    {
                        Actor3.Say("No.");
                    }
                    if (PlayStep == 156)
                    {
                        Actor1.Say("Oh... What then?");
                    }
                    if (PlayStep == 160)
                    {
                        Actor3.Say("Shh... I'll tell you.");
                    }
                    if (PlayStep == 165)
                    {
                        Actor3.Say("He said, 'I'll toss into the air A coin, and I'll toss it fair;");
                    }
                    if (PlayStep == 174)
                    {
                        Actor3.Say("If heads come up, I'll wed Marie; If tails, fair Kate my bride shall be.'");
                    }
                    if (PlayStep == 179)
                    {
                        Actor1.Say("Great idea!");
                        MoveActor(Actor1, new Point3D(3679,3496,2) );
                    }
                    if (PlayStep == 183)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor2.Location );
                        Actor1.Say("See this coin?  It'll decide which if you will be my bride!");
                    }
                    if (PlayStep == 189)
                    {
                        Actor3.Say("Then from his leather pocket-book A coin bright and new he took;");
                    }
                    if (PlayStep == 193)
                    {
                        Actor1.Say("Here it is...");
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor4.Location );
                    }
                    if (PlayStep == 197)
                    {
                        Actor3.Say("He kissed one side for fair Marie,");
                    }
                    if (PlayStep == 201)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor2.Location );
                        Actor1.Say("I do hope its you...");
                    }
                    if (PlayStep == 205)
                    {
                        Actor3.Say("The other side for Kate kissed he.");
                    }
                    if (PlayStep == 209)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor4.Location );
                        Actor1.Say("I do hope its you...");
                    }
                    if (PlayStep == 215)
                    {
                        Actor3.Say("Then in a manner free and fair He tossed the coin in the air.");
                    }
                    if (PlayStep == 218)
                    {
                        Actor2.Say("*ghasp*");
                        Actor4.Say("*prays*");
                    }
                    if (PlayStep == 222)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor4.Location );
                        Actor1.Say("Ye fates,pray let this be A lucky throw indeed for me!");
                    }
                    if (PlayStep == 230)
                    {
                        Actor3.Say("The dollar rose, the dollar fell; He watched its whirling transit well,");
                    }
                    if (PlayStep == 232)
                    {
                        MoveActor(Actor1, new Point3D(3677,3496,2));
                        MoveActor(Actor4, new Point3D(3677,3494,2));
                        MoveActor(Actor2, new Point3D(3677,3499,2));
                    }                    
                    if (PlayStep == 239)
                    {
                        Actor3.Say("And off some twenty yards or more The dollar fell upon the shore.");
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo( Actor3.Location );
                        ((PlayActor)Actor2).Direction = Actor2.GetDirectionTo( Actor3.Location );
                        ((PlayActor)Actor4).Direction = Actor4.GetDirectionTo( Actor3.Location );
                    }
                    if (PlayStep == 247)
                    {
                        Actor3.Say("John Henry ran to where it struck To see which maiden was in luck.");
                    }
                    if (PlayStep == 255)
                    {              
                        MoveActor(Actor1, new Point3D(3679,3496,2));   
                        Actor2.Say("*ghasp*");
                        Actor4.Say("*prays*");
                    }   
                    if (PlayStep == 265)
                    {
                        Actor3.Say("But, oh, the irony of fate! Upon its edge the coin stood straight!");
                    }
                    if (PlayStep == 270)
                    {              
                        Actor1.Say("What???");
                    } 
                    if (PlayStep == 276)
                    {
                        Actor3.Say("And there, embedded in the sand, John Henry let the dollar stand!");
                    }
                    if (PlayStep == 280)
                    {              
                        Actor1.Say("Wait!  I can throw it again!");
                    } 
                    if (PlayStep == 286)
                    {
                        Actor3.Say("And he will tempt his fate no more, But live and die a bachelor.");
                    }
                    if (PlayStep == 289)
                    {              
                        Actor1.Say("NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!");
                    } 
                    if (PlayStep == 305)
                    {              
                       if (Actor1.X != 3675 || Actor1.Y != 3498) // checking locations for the actors, where do you want them before play starts?
                            MoveActor(Actor1, new Point3D( 3675, 3498, 3));
                        if (Actor2.X != 3675 || Actor2.Y != 3495)
                            MoveActor(Actor2, new Point3D(3675,3495,3));
                        if (Actor3.X != 3683 || Actor3.Y != 3497)
                            MoveActor( Actor3, new Point3D( 3683, 3497, 2) );
                        if (Actor4.X != 3675 || Actor4.Y != 3495)
                            MoveActor(Actor4, new Point3D(3674,3495,3));  
                    }
                    if (PlayStep == 315)
                    {
                        Actor3.Say("Thus, ladies, you have heard me sing The ballad of John Henry King.");
                    }     
                    if (PlayStep > 330)   
                    {          
                        ActivePlay = false; 
                        InitPlay();
                    }
                    PlayStep++;
                }
            }
		}

        public void MoveActor( Mobile actor, Point3D go) // use this to move an actor somewhere
        {
            if (actor == null || actor.Map == null)
                return;
            
            if (m_WayPoints == null)
                m_WayPoints = new ArrayList();

            WayPoint GoHere = new WayPoint();
			GoHere.Map = actor.Map;
			GoHere.Location = go;
            m_WayPoints.Add( GoHere );
            ((PlayActor)actor).CurrentWayPoint = GoHere;
        }

        public override void OnAfterDelete() // if you delete the director, what happens
        {
            if (Actor1 != null)
                Actor1.Delete();
            if (Actor2 != null)
                Actor2.Delete();
            if (Actor3 != null)
                Actor3.Delete();
            if (Actor4 != null)
                Actor4.Delete();
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
            if (Actor1 == null || Actor2 == null || Actor3 == null || Actor4 == null)
                InitPlay();

            if (!Hidden) // keep me hidden!
                Hidden = true;

            if (!ActivePlay ) // look for new players
            {
                Mobile spect = null;
               foreach ( Mobile mob in this.GetMobilesInRange( 10 ) )
                {
                    if (mob is PlayerMobile && mob.AccessLevel == AccessLevel.Player ) 
                    {
                       spect = mob;
                    }
                }
                if (spect != null)
                {
                        ((PlayActor)Actor3).Direction = Actor3.GetDirectionTo( spect );
                        Actor4.Say("We have a spectator!  Sit down, grab a drink, the play will begin soon.");
                        spect = null;
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
                
            base.OnSpeech(e);

        }


        public PlayDirectorTavern(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
            writer.Write((Mobile)Actor1);
            writer.Write((Mobile)Actor2);
            writer.Write((Mobile)Actor3);
            writer.Write((Mobile)Actor4);
            
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
                        Actor3 = reader.ReadMobile();
                        Actor4 = reader.ReadMobile();
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
