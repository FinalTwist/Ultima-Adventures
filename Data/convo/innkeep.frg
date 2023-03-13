// Innkeeper Fragment for Britannia
//
// Innkeepers should also have the Tavernkeeper keyword responses,
// unless their inn sells no food or drink.
//
// Keyword list:
// room, sleep, rent, stay the night, bed
//
// To add:
// knowledge about travel conveyances, stables, etc
//
// Last revised March 11th
//
// - Raph
//

#Fragment Britannia, Job, Britannia_Innkeeper {
    #Sophistication High {
	#Key "*job*", "*work*", "*what*do*" {
		"I am an innkeeper, $milord/milady$.",
		"I keep this place running and full of guests, $milord/milady$.",
		"I work here at the inn, keeping it running as smoothly as I can."
                }
	#Key "*Inn*" {
		"Yes, I am the one who runs this inn.",
		"I try to see to it that all of my guest's needs are met, but I'm afraid I can't keep up sometimes.",
		"I mint take care of this inn."
                }

        #Key "room", "rent", "stay the night", "sleep", "for the night", "bed" {
            #Attitude Wicked {
                #Notoriety Infamous {
                    "I suppose that thou desirest a room. And I likewise suppose 
                        that I might find it possible to grant thee thy wish. If
                        thou agreest not to sully the sheets.",
                    "Ah, so foul $ruffians/harpies$ like thyself actually sleep 
                        by night? And here I was thinking that such people spent 
                        their nights under children's beds.",
                    "I suppose I shall have to place a sign 'pon my inn, declaring
                        that the Great and Vile, Killer of Infants and Slayer of
                        Guards, the Monstrous... erm... whatever your name is--slept
                        here once."
                }
                #Notoriety Outlaw {
                    "I can offer thee a room, although it goeth against the 
                        grain to harbor one such as thee.",
                    "Dost thou promise not to harm anyone? In that case, I
                        can offer thee the use of a room to spend the night
                        here, if thou needest it.",
                    "I offer beds and rooms for those seeking a place to sleep
                        the night. Aye, even rooms fit for such as thee."
                }
                #Notoriety Anonymous {
                    "Despite the difficult times my life now experiences, I can
                        still offer thee a room--even if thou dost look like a 
                        commoner off the street.",
                    "Rooms, beds, places to sleep the night. Of course I offer
                        them, am I not an _Job_? Or art thou so weary from the road
                        that thou hast no sense of the obvious?",
                    "Ah, witness me, if thou wouldst: a #man/woman# worn to the
                        bone with the petty demands of life, drowning my sorrows
                        in self-pity, and barely able to muster the energy to
                        tell thee that I do indeed offer rooms for rent."
                }
                #Notoriety Known, Famous {
                    "Ah, so one such as thou seeks, perhaps, a room in which to
                        lay thy weary head, while those such as I, whose lives
                        are at their worst, must needs slave away...",
                    "If 'twere not beneath thy lofty notice, I might mention
                        that I rent rooms for those seeking a night's sleep--
                        but of course nothing I say is worth thy attention.",
                    "As miserable as I am, I still have enough sense to note
                        when one such as thou says '%0.' I can offer you a bed
                        despite my low spirits."
                }
            }
            #Attitude Belligerent {
                #Notoriety Infamous {
                    "I offer rooms and beds for rent--notorious villains are
                        welcome, although slayings upon the premises are, I 
                        fear, extra.",
                    "Thou couldst slay me where I stand for mentioning it, but
                        I offer a place to sleep here in mine inn. And by the
                        way, I have never seen a better wicked glint in the eye
                        than thine--dost thou practice it or doth it come
                        naturally?",
                    "[getBuilding(getLocation(Actor()))] offers rooms, if one
                        such as thee deigns to sleep in such places."

                }
                #Notoriety Outlaw {
                    "I offer beds to wayfarers, although folk such as thee
                        had best remain on best behavior.",
                    "Art thou tired of terrorizing innocent folk? In that case,
                        I can offer thee a bed for the night."
                }
                #Notoriety Anonymous {
                    "I suppose that thou dost not care, but I do have rooms
                        here at the inn.",
                    "Beds and rooms are for rent here, despite the miserable
                        circumstances of my life."
                }
                #Notoriety Known, Famous {
                    "Ah, if 'twere possible for thee to stay the night in one
                        of mine humble rooms, then perhaps my lot in life would
                        improve.",
                    "Given the sorry state of my life, thy custom as one of
                        the guests in mine inn would indeed be a blessing."
                }
	    }	

            #Attitude Neutral, Kindly {
                    "I am the _Job_ here at [getBuilding(getLocation(Actor()))],
                        and 'twould be a pleasure to offer thee a room.",
                    "I have beds to slumber in and rooms to relax in.",
                    "No better rooms may be found than those I rent for
                        travellers."
            }
            #Attitude Goodhearted {
                #Notoriety Infamous, Outlaw {
                    "I can indeed offer thee a room to sleep the night. But
                        thou must promise me that thou wilt attempt to reform
                        thy wicked ways.",
                    "I find myself indeed deeply uncertain as to whether a
                        $man/woman$ of thy known lack of probity should stay
                        the night in mine establishment.",
                    "If thou makest a solemn promise not to harm anyone while
                        thou stayest at [getBuilding(getLocation(Actor()))],
                        then I can offer thee a room."
                }
                #Notoriety Anonymous, Known {
                    "Indeed, I can offer thee a goodly room here at mine inn.",
                    "'Tis a pleasure to offer thee a bed and a room.",
                    "If thou dost require a room in which to sleep, I can
                        provide thee with one."
                }
                #Notoriety Famous {
                    "'Twould be a great honor to have a citizen of thy caliber
                        sleep the night in one of [getBuilding(getLocation(Actor()))]'s
                        beds.",
                    "I have comfortable beds and rooms for guests who seek a soft
                        pillow for their heads. And a guest of thy caliber would be most
                        welcome.",
                    "If thou wouldst pay me the compliment of sleeping this night
                        under my roof, I could offer thee a most pleasurable
                        night's sleep."
                }
            } 
        }	    
    #Sophistication Medium {
#Key "*job*", "*work*", "*what*do*" {
		"I am an innkeeper, $milord/milady$.",
		"I keep this place running and full of guests, $milord/milady$.",
		"I work here at the inn, keeping it running as smoothly as I can."
                }
	#Key "*Inn*" {
		"Yes, I am the one who runs this inn.",
		"I try to see to it that all of my guest's needs are met, but I'm afraid I can't keep up sometimes.",
		"I mint take care of this inn."
                }
        #Key "room", "rent", "stay the night", "sleep", "for the night", "bed" {
            #Attitude Wicked {
                #Notoriety Infamous {
                    "If thou dost not make a mess, thou canst stay at mine inn.",
                    "Thou'rt an ugly $fellow/harridan$, but I have a room fit for
                        thee.",
                    "I have rooms for those that need them. Unless thou wouldst prefer
                        to sleep under a rock, as is thy custom."
                }
                #Notoriety Outlaw {
                    "Thou'rt not the most desirable of people, but I can offer thee
                        a room.",
                    "If thou dost promise not to kill anyone, thou canst stay here.",
                    "My life is miserable enough that thy presence in one of my
                        rooms will not make much difference."
                }
                #Notoriety Anonymous {
                    "Thou lookest like a dense type, so I shall say it slow: I. Have.
                        Rooms. To. Rent.",
                    "Well, this is an inn. I am a _Job._ Seems obvious to me.",
                    "Were my life not a disaster, I might mention that I rent rooms
                        to travellers."
                }
                #Notoriety Known, Famous {
                    "Ah, so thou, the high and mighty $lord/lady$, wanteth a room?
                        Well, I offer them.",
                    "Well, to one such as thee, I am a bug. But even bugs have rooms
                        for rent.",
                    "If thou wanted to cure some of the ills thou hast created by thy
                        messing up the country, thou couldst rent a room from me."
                }
            }
            #Attitude Belligerent {
                #Notoriety Infamous {
                    "Thou canst stay here, but kill no one, I prithee.",
                    "Do not kill me for saying it, but I offer beds to sleep
                        in.",
                    "Thou'rt a big and powerful outlaw, but thou couldst still
                        stay at mine inn. After all, life could hardly get worse."
                }
                #Notoriety Outlaw {
                    "Thou canst rent a room here, but no touching the silverware.",
                    "I can offer thee a bed for the night. But no stealing from
                        th'other guests."
                }
                #Notoriety Anonymous {
                    "I suppose that thou dost not care, but I do have rooms
                        here at the inn.",
                    "Beds and rooms are for rent here, even though my life is
                        miserable."
                }
                #Notoriety Known, Famous {
                    "If thou didst stay here the night, perhaps I would get more
                        customers.",
                    "Thy presence here could only make my bad life better. Thou canst
                        rent a room from me."
                }
            }
            #Attitude Neutral, Kindly {
                    "I am the _Job_ here at [getBuilding(getLocation(Actor()))],
                        and 'twould be a pleasure to offer thee a room.",
                    "I have beds and rooms for rent.",
                    "This is the best inn in _Town_."
            }
            #Attitude Goodhearted {
                #Notoriety Infamous, Outlaw {
                    "Thou'rt on a wicked path. Thou shouldst reform thy ways and
                        discover virtue. And thou couldst stay at mine inn while
                           thou didst it!",
                    "I don't know if I should let someone like thee rent here.",
                    "Swear not to hurt anyone while at [getBuilding(getLocation(Actor()))],
                        and I can offer thee a room."
                }
                #Notoriety Anonymous, Known {
                    "Thou wouldst be welcome at mine inn.",
                    "'Tis a pleasure to offer thee a bed and a room.",
                    "Any who needeth sleep are welcome here."
                }
                #Notoriety Famous {
                    "'Twould be wonderful to have thee stay at
                        [getBuilding(getLocation(Actor()))].",
                    "'Tis an honor to offer thee a room, good $sir/lady$.",
                    "My humble inn is hardly worthy of thee, $milord/milady$,
                        but I can offer thee a room."
                }
            } 
        }	    
    }
    #Sophistication Low {
#Key "*job*", "*work*", "*what*do*" {
		"I am an innkeeper, $milord/milady$.",
		"I keep this place running and full of guests, $milord/milady$.",
		"I work here at the inn, keeping it running as smoothly as I can."
                }
	#Key "*Inn*" {
		"Yes, I am the one who runs this inn.",
		"I try to see to it that all of my guest's needs are met, but I'm afraid I can't keep up sometimes.",
		"I mint take care of this inn."
                }
        #Key "room", "rent", "stay the night", "sleep", "for the night", "bed" {
            #Attitude Wicked {
                #Notoriety Infamous {
                    "Well, ye know, I got rooms.",
                    "Ye're ugly, but I kin give ye a room.",
                    "Ifn ye like, ye kin sleep here."
                }
                #Notoriety Outlaw {
                    "I rent rooms, ye know.",
                    "Ye kin sleep here 'slong as ye don't hurt nobody.",
                    "Ugh, be nobbut but trouble, but ye kin rent a room from me."
                }
                #Notoriety Anonymous {
                    "Ain't this an inn? Ye kin rent here.",
                    "Ye're an ugly sort, but I kin rent ye a room.",
                    "Me life is ripe as a rotten tomato. So's yer clothes. But
                        I's not picky about smells, so ye's kin rent a room here."
                }
                #Notoriety Known, Famous {
                    "Ye mean bigshots like ye might mebbe stay at me inn?",
                    "Ifn 'tisn't too nasty for ye, ye kin rent a room from me.",
                    "Folk like ye have ruined me life."
                }
            }
            #Attitude Belligerent {
                #Notoriety Infamous {
                    "Ye kin stay here, but don' kill me.",
                    "I got beds to sleep in, ifn ye don't kill nobody.",
                    "Ye's big and could beat me to pulp. So I's tellin' truth
                        when I say I got rooms for rent."
                }
                #Notoriety Outlaw {
                    "Ye kin rent a room here, but no stealin' nothin'.",
                    "T'other guests might be scared of ye, but ye can sleep
                        here tonight."
                }
                #Notoriety Anonymous {
                    "Ye probably don't care, but I have room for rent.",
                    "I rent rooms even though my life is awful."
                }
                #Notoriety Known, Famous {
                    "Ye should stay here. Maybe then others would too.",
                    "Please please rent a room here. Maybe then life would
                        get better."
                }
            }
            #Attitude Neutral, Kindly {
                    "I's the _Job_, an' I'd be glad to offer ye a room.",
                    "I got beds and rooms for rent.",
                    "Sleep here. 'Tis the best inn in _Town_."
            }
            #Attitude Goodhearted {
                #Notoriety Infamous, Outlaw {
                    "Ye's a bad 'un, bad indeed. Ye ought to try changing
                        yer life summat. Ye kin stay at me inn whiles ye
                        do.",
                    "I dunno but that I should bar the door to ye.",
                    "Ye shouldn't hurt anyone while ye stay under me roof."
                }
                #Notoriety Anonymous, Known {
                    "Ye'd be welcome at th' inn.",
                    "I kin offer ye a bed and a room.",
                    "Ifn ye need sleep yer welcome here."
                }
                #Notoriety Famous {
                    "Ifn ye stayed here at [getBuilding(getLocation(Actor()))],
                        'twould me me famous! That'ud be wunnerful!",
                    "Please stay here, good $sir/lady$! I's got rooms for rent!",
                    "Me inn is pretty bad for th elikes of ye, $milord/milady$,
                        but ye kin have an 'umble room if ye want."
                }
            } 
        }	    
    }
    
    // End of fragment
