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
    public class PlayDirectorMangarTower: BaseCreature, IOneTime // name the director to play[whatever] here and everywhere else
    {
		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

        private Mobile Actor1;

        private Mobile Actor2;
      
        private ArrayList m_WayPoints;
        private int PlayStep; // scene of the play

        private bool ActivePlay; // is the play running?

        private int PlayCheck; // 0 is inactive, 1 is no response (play only), 2+ is response needed

        private int CheckCount; 

        private Mobile lastplayed;

        [Constructable]
        public PlayDirectorMangarTower()
            : this(Utility.RandomBool(), null, null)
        {
        }

        [Constructable]
        public PlayDirectorMangarTower( bool sex)
            : this(sex, null, null)
        {
        }

        [Constructable]
        public PlayDirectorMangarTower(bool sex, string name)
            : this(sex, name, null)
        {
        }

        [Constructable]
        public PlayDirectorMangarTower(bool sex, string name, string title) //no need to change anything here
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
                Actor1.AddItem( new PlateChest() );
                Actor1.AddItem( new PlateLegs() );
                Actor1.AddItem( new PlateGorget() );
                Actor1.AddItem( new PlateGloves() );
                Actor1.AddItem( new PlateHelm() );
                Actor1.AddItem( new Boots( ) );
                Actor1.Name = "Nublet";
                Actor1.Title = "the Adventurer";

                Actor1.SpeechHue = 233;

                Actor1.MoveToWorld( new Point3D( 4502, 3385, 0), Map.Trammel ); //where do you want him before play starts?
            }

            if (Actor2 == null) // this is where you set up Actor 2 (clothes, name etc)
            {
                Actor2 = new PlayActor( false );

                //assign clothing for actor 1 here
                Actor2.AddItem( new Robe( ) );
                Actor2.AddItem(new WizardsHat());
                Actor2.Name = "Mangar";
                Actor2.Title = "the Black";
                Actor2.AddItem(new Boots());
                Actor2.SpeechHue = 64;

                Actor2.MoveToWorld( new Point3D(4502, 3385, 0), Map.Trammel ); // where do you want him before play starts?
            }

            if (m_WayPoints == null)
                m_WayPoints = new ArrayList();

            foreach( WayPoint wp in m_WayPoints )
            {
                if (wp != null)
                    wp.Delete();
            }

            m_WayPoints.Clear(); 

            PlayCheck = 0;

        }

        public void StartPlay( ) // this is what is called when the play starts (here the trigger is in onthink)
        {

            

            ActivePlay = true;
            PlayCheck = 1;
            PlayStep = 0;

        }
        public void OneTimeTick() // this is the timer for everything.  works in 1 second increments.  every second, everything here will run
        {
            //check to make sure everything is as it should be
            if (Actor1 == null || Actor2 == null)
                InitPlay(); // if any actors are missing, reset play

            if (ActivePlay)
            {

                if (PlayCheck == 1) // play is going to run playcheck is the play "scene"  here 1 means scene 1
                {
                    if (PlayStep == 0) // playstep is the "action" or step in the scene.  e.g. a player moves to a location or says something is a step
                    {
                        // play just started, check actors are in place
                   //    if (Actor1.X != 3675 || Actor1.Y != 3498) // checking locations for the actors, where do you want them before play starts?
                    //        MoveActor(Actor1, new Point3D( 3675, 3498, 3));
                     //   if (Actor2.X != 3675 || Actor2.Y != 3495)
                    //        MoveActor(Actor2, new Point3D(3675,3495,3));
                    }
                    if (PlayStep == 2) // every "playstep" is one second.  thats how you time actions.  so from playstep 1 to playstep 5 is 4 seconds
                    {
                        Actor1.MoveToWorld(new Point3D(2830, 1873, 95), Map.Trammel);
                        Effects.SendLocationParticles(EffectItem.Create(Actor1.Location, Actor1.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 0, 0, 2023, 0);
                        Actor1.PlaySound(0x1FE);
                        Actor1.Say("*Pants*");
                    }
                    if (PlayStep == 3)
                    {
                        Actor1.Say("...I killed him...I...Escaped...at last....");
                    }
                    if (PlayStep == 7)
                    {
                        Actor2.MoveToWorld(new Point3D(2830, 1877, 95), Map.Trammel);
                        Effects.SendLocationParticles(EffectItem.Create(Actor2.Location, Actor2.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 0, 0, 2023, 0);
                        Actor2.PlaySound(0x1FE);
                        ((PlayActor)Actor2).Direction = Actor2.GetDirectionTo(Actor1.Location);
                        
                    }
                    if (PlayStep == 10)
                    {
                        Actor2.Say("You thought you could kill me? Mwhahaha...You may have escaped my clutches this time, but you can't kill me! Not in my Realm!");
                    }
                    if (PlayStep == 20)
                    {
                    ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo(Actor2.Location);
                        Actor1.Say("*Trembles*");
                    }
                    if (PlayStep == 25)
                    {
                        Actor1.Say("Noooo, leave me be - I will not return to that place!");
                    }
                    if (PlayStep == 32)
                    {
                        Actor2.Say("Quite right, you caused enough mischief with my toys, besides it looks like a new play thing has arrived...");
                    }
                    if (PlayStep == 42)
                    {
                        ((PlayActor)Actor2).Direction = Actor2.GetDirectionTo(lastplayed.Location);
                        Actor2.Say("*Mangar turns his eyes towards you* You there, Come join me...come join my world...FOREVER! Mwhahahaha");
                       // ((PlayActor)Actor2).Direction = Actor2.GetDirectionTo(spect.Location); - **work out to determine spectator location
                    }
                    if (PlayStep == 54)
                    {
                        Actor2.Say("I'll be waiting for you...See you soon, if you dare. Now, I must leave, for I have preparations to make.");
                    }
                    if (PlayStep == 64)
                    {
                        Effects.SendLocationParticles(EffectItem.Create(Actor2.Location, Actor2.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 0, 0, 2023, 0);
                        Actor2.PlaySound(0x1FE);
                        Actor2.MoveToWorld(new Point3D(4502, 3385, 0), Map.Trammel);
                    }
                    if (PlayStep == 65)
                    {
                        ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo(lastplayed.Location);
                        // ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo(spect.Location); **work out to determine spectator location
                        Actor1.Say("He's gone! Back to his realm I suspect.");
                    }
                    if (PlayStep == 70)
                    {
                        // ((PlayActor)Actor1).Direction = Actor1.GetDirectionTo(spect.Location); **work out to determine spectator location
                        Actor1.Say("*Pants* I must admit, I never thought I would escape.");
                    }
                    if (PlayStep == 78)
                    {
                        Actor1.Say("I was trapped there for days, The Mad Wizard, Mangar The Black, has captured the entire town of Skara Brae in his own pocket dimension");
                    }
                    if (PlayStep == 92)
                    {
                        Actor1.Say("The denizens of that realm go about their business as if nothing has happened, perhaps driven mad by their eternal prison");
                    }
                    if (PlayStep == 105)
                    {
                        Actor1.Say("I might have been trapped there myself, if not for my cunning and guile");
                    }
                    if (PlayStep == 119)
                    {
                        Actor1.Say("The realm is labyrinthian! Endless dungeon after dungeon, each more fiendish than the last, each with secrets and riddles to be solved");
                    }
                    if (PlayStep == 134)
                    {
                        Actor1.Say("I warn you adventurer, if you are considering going after Mangar yourself ensure you are prepared, for once you enter you may not leave until you confront Mangar himself!");
                    }
                    if (PlayStep == 149)
                    {
                        Actor1.Say("Perhaps you will fare better than I, perhaps you will be able to end Mangar's madness once and for all and if not, you will leave all the stronger for it for there are treasures to be found within his realm");
                    }
                    if (PlayStep == 164)
                    {
                        Actor1.Say("If you wish to proceed, simply click on this orb...thats how I began this whole debacle...");
                    }
                    if (PlayStep == 176)
                    {
                        Actor1.Say("Alas, I am weary - I must go inform the King of what has happened here and then return to my family, so that I may rest, for it has been so long...far too long.");
                    }
                    if (PlayStep == 189)
                    {
                        Actor1.Say("Fare thee well, Adventurer - remember my warning...always be prepared!");
                    }
                    if (PlayStep == 196)
                    {
                        MoveActor(Actor1, new Point3D(2824, 1872, 95));
                    }
                    if (PlayStep == 198)
                    {
                        MoveActor(Actor1, new Point3D(2824, 1878, 75));
                    }
                    if (PlayStep == 202)
                    {
                        Actor1.MoveToWorld(new Point3D(4502, 3385, 0), Map.Trammel);
                    }
                    if (PlayStep > 205)   
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
            if (Actor1 == null || Actor2 == null)
                InitPlay();

            if (!Hidden) // keep me hidden!
                Hidden = true;


            if (!ActivePlay ) // look for new players
            {
                Mobile spect = null;
               foreach ( Mobile mob in this.GetMobilesInRange( 15 ) )
                {
                    if (mob is PlayerMobile && mob.Player && mob.Z == this.Z && !CharacterDatabase.GetBardsTaleQuest( mob, "BardsTaleWin" ) ) 
                    {
                       spect = mob;
                       
                    }
                }
                if (spect != null && lastplayed != spect)
                {
                       // ((PlayActor)Actor3).Direction = Actor3.GetDirectionTo( spect );
                       // this.Say("We have a spectator!  Sit down, grab a drink, the play will begin soon.");
                        ((PlayActor)Actor2).Direction = Actor2.GetDirectionTo(spect.Location);
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
                
            base.OnSpeech(e);

        }


        public PlayDirectorMangarTower(Serial serial)
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
