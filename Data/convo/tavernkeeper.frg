  // Tavernkeeper Fragment for Britannia
//
// This is the base fragment for tavernkeepers, and should also be
// attached to all innkeepers. Innkeepers have an additional set of
// keyword responses.
//
// Keyword list (completed):
// ale, beer
// barmaid, wench, waitress   
// food, breakfast, lunch, dinner
//
// To add:
// brewing knowledge
// bar
// macros for barmaid name substitution
// 
// Last revised March 11th
//
// - Raph
//

#Fragment Britannia, Job, Britannia_Tavernkeeper {
    #Sophistication High {
#Key "*job*",  "*what*do*",    {
	"I run this tavern.",
	"I run this place.", 
	"I'm the tavernkeeper here.", 
			}

#Key "*tavern*"  {
	"I keep this tavern clean and in business.",
	"I am a tavernkeeper, yes.", 
	"I work hard to keep my customers happy.", 
			}

        #Key "*ale*", "*beer*" {
            #Attitude Wicked, Belligerent {
                #Notoriety Infamous {
                    "Aye, I sell %0. I'm supposing that thou wilt wish to take them
			from me without pay.",
                    "I brew my own %0, and 'tis excellent, strong and suitable for
			one such as thee.",
                    "If thou starest at my %0 for too long, it might sour. But I'll
			sell thee a draught of it."
                }
                #Notoriety Outlaw {
                    "I could sell thee a drink, if thou doth promise me not to wreck
			the place.",
                    "Life is hard enough without getting one such as thee drunk.",
                    "Thou'rt a scruffy $fellow/lass$. Aye, I sell drink fit for such
			as thee."
                }
                #Notoriety Anonymous {
                    "I sell the finest %0, all of mine own brewing. 'Tis the only thing
			to give me comfort in these trying times.",
                    "Indeed, 'tis mine own brew, with a delicate bouquet and a subtle
			flavor. Oft have I drowned my own sorrows in it, in recent
			times.",
                    "My %0 is well-aged in wood casks, the best to be had in these
			parts. Not that any appreciate the craftsmanship."
                }
                #Notoriety Known, Famous {
                    "'Twould be an honor to serve thee from my casks, if thou dost
			deign to drink the brew of one as ill-fortuned as I.",
                    "I fear my rough brew may not be sufficient for $milord's/milady's$  
			subtle palate. After all, naught I do these days serves well 
			enough.",
                    "Aye, the finest %0 only for $milord/milady$. If it please thee, carry 
			word of my brewing with thee. I could use the customers, these 
			difficult days."
                }
            }
            #Attitude Neutral {
                #Notoriety Infamous, Outlaw {
                    "Aye, %0 is for sale here, even to such a $villain/harridan$ as thee.",
                    "Indeed, thou canst purchase a draught of %0 here. I am not choosy as 
			to my custom.",
                    "I sell %0, yes."
                }
                #Notoriety Anonymous {
                    "Certainly I sell %0, and at a reasonable price too.",
                    "I can sell thee %0.",
                    "The best %0 anywhere is sold right here, brewed by mine own hands."
                }
                #Notoriety Known, Famous {
                    "'Twould be an honor to sell thee some %0.",
                    "Only the best %0 for $milord/milady$.",
                    "Oh, my brewing would surely not be good enough for one such as 
			thee... but wouldst thou like to try it?"
                }
            }
            #Attitude Kindly {
                #Notoriety Infamous {
                    "If I give thee %0 wilt thou leave my establishment alone?",
                    "If thou gettest drunk off of my %0, thou wilt do something rash... I 
			beg thee, restrain thyself.",
                    "Many an infamous character hath drunk at my bar--a draught of %0 and 
			thou couldst be added to the list."
                }
                #Notoriety Outlaw {
                    "I dislike serving %0 to common ruffians.",
                    "Thou'rt like to smash furniture if I give thee too much %0.",
                    "Thou lookest somewhat unsavory, but I suppose I could sell thee %0 if 
			thou wert on thy best behavior."
                }
                #Notoriety Anonymous {
                    "I sell %0, of course. 'Tis a tavern, is it not?",
                    "What better than %0 to slake the thirst of travellers?",
                    "Certainly I sell %0, friend."
                }
                #Notoriety Known, Famous {
                    "An honor indeed to serve thee %0!",
                    "Gladly will I serve thee %0.",
                    "'Tis a pleasure indeed to have one such as thee drinking of my $0."
                }
            }
            #Attitude Goodhearted {
                #Notoriety Infamous, Outlaw {
                    "Canst thou give me one good reason why one such as I should slake the 
            thirst of a $scoundrel/witch$ such as thee?",
                    "I have a good life, a good tavern, and the courage to tell thee to 
			thy face that thou'rt scum, whether thou wishest no more than %0 or 
			no.",
                    "Aye, I sell %0, and suppose that even a $scoundrel/wench$ such as thee must 
			drink once in a while.",
                    "Aye, drink some of my %0 to relieve thy troubles and perhaps get thee 
			back onto the path of the righteous."
                }
                #Notoriety Anonymous {
                    "Gladly will I serve thee %0, friend.",
                    "I sell most excellent %0 and would be pleased if thou wouldst try a 
			draught of it.",
                    "Brewed by mine own hands! 'Tis excellent %0 that I sell--thou 
			shouldst try it."
                }
                #Notoriety Known {
                    "It would be a pleasure indeed to serve thee of my %0.",
                    "An honor to have thee here! We sell %0, of course, and I can also 
			offer thee food.",
                    "Aye, i can offer thee %0. Might I also tempt thee to stay for a 
			meal?"
                }
                #Notoriety Famous {
                    "Anything I can do to help thee, I would count an honor. If thou 
			desirest %0, I can supply it.",
                    "Such an honor to have one of thy great renown here, inquiring about 
			%0! Certainly, I sell it.",
                    "The very highest quality %0, brewed by myself, is available
			here--I shall serve thee from my private cask."
                }
            }
        }
        #Key "*barmaid*", "*wench*", "*waitress*" {
            #Notoriety Infamous, Outlaw {
                #Attitude Wicked {
                    "No shortage of the wenches about, $though I doubt any would spend 
            time with thee./so 'tis not likely that I would offer thee a job.$",
                    "Aye, barmaids, wenches, waitresses. Who can afford to pay them these 
            days? $Plannest thou to steal one away or somesuch?/Seek thee a sister in thy villainy?$",
                    "Don't touch her, she may bite."
                }
                #Attitude Belligerent, Neutral, Kindly, Goodhearted {
                    "If thou harmest or $even lookest sideways at/attempt to recruit away$ my barmaids, I'll call 
			the guards.",
                    "$Do not get any ideas about the %0./What, seek ye to take the %0's place?$",
                    "She is her own woman, $though I suspect she would sneer at the likes 
            of thee./not one to succumb to the life thou dost live.$"
                }
            }
            #Notoriety Anonymous {
                #Attitude Wicked, Belligerent {
                    "$Thou'rt a nobody, why dost thou think she would spend the time of day 
            with thee?/Art thou a friend of hers?$",
                    "Do not pester the help."
                }
                #Attitude Neutral, Kindly, Goodhearted {
                    "Thou come here to ask about a %0? Thou shouldst be drinking instead.",
                    "What, $art thou desperate for company/is she a cousin or sister$?",
                    "What of her? She works hard."
                }
            }
            #Notoriety Known, Famous {
                #Attitude Wicked, Belligerent {
                    "Thou dost seek the %0? I should have known that thy presence here was not 
			because of me.",
                    "Fine, speak thou with the %0, since thou art so high and mighty.",
                    "What of her? She is hired help."
                }
                #Attitude Neutral, Kindly, Goodhearted {
                    "The %0 would be pleased to meet thee, no doubt.",
                    "What of her? She works hard.",
                    "The barmaids get quite a lot of unwanted attention, this being an 
			establishment where men drink."
                }
            }
        }
        #Key "*food*", "*breakfast*", "*lunch*", "*dinner*" {
            #Attitude Wicked {
                "Aye, %0 is for sale. With the state of the world these days, I begin to 
			suspect all is for sale, even the well-being of poor folk like 
			me.",
                "Indeed, I will sell thee %0. If 'twould improve my lot in life, I'd sell 
			thee the business!",
            }
            #Attitude Belligerent, Neutral {
                "I can sell thee %0.",
                "%0? I can sell thee that."
            }
            #Attitude Kindly, Goodhearted {
                "A stout meal can cure many ills. I can supply %0 if thou needest it.",
                "Certainly we can serve thee %0 if thou dost desire it."
            }           
      	}
   }
    #Sophistication Medium {
#Key "*job*",  "*what*do*",    {
	"I run this tavern.",
	"I run this place.", 
	"I'm the tavernkeeper here.", 
			}
#Key "*tavern*"  {
	"I keep this tavern clean and in business.",
	"I am a tavernkeeper, yes.", 
	"I work hard to keep my customers happy.", 
			}
#Key "*job*", "*what*do*", "*tavernkeeper*"  {
	"I keep this tavern clean and running.",
	"I am a tavernkeeper, yes."
			}

        #Key "*ale*", "*beer*" {
            #Attitude Wicked, Belligerent {
                #Notoriety Infamous, Outlaw {
                    "What dost thou care? Aye, I sell %0.", 
                    "If thou didst go away, perhaps I might offer some %0 as a parting gift.",
                    "I'll take thy coin for some %0, but don't expect me to wait on thee hand and foot.",
                    "Thou'rt a bigshot criminal, but if thou wreckest the place I'll set the hounds on thee.",
                    "Life is hard enough without getting one such as thee drunk.",
                    "Thou'rt a scruffy $fellow/lass$. Aye, I sell drink fit for such
			as thee."
                }
                #Notoriety Anonymous {
                    "I sell fine %0 which I have brought in. In these hard times, 'tis often difficult.",
                    "My life is very hard, but I know enough to purchase the best brews to be had.",
                    "'Tis expensive to bring in the %0, and with the sorry state of my life I can ill-afford it. But I have it."
                }
                #Notoriety Known, Famous {
                    "If thou dost not think 'twill bring thee bad luck, thou canst have my %0. I say bad luck because I am indeed plagued with it.",
                    "I can't do anything right these days, but at least i can sell thee %0.",
                    "Try my %0. Tell thy friends how good it is. More customers would brighten up my awful life."
                }
            }
            #Attitude Neutral {
                #Notoriety Infamous, Outlaw {
                    "Even people like thee can buy %0 here.",
                    "I cannot afford to turn away even scruffy folk, so thou mayst buy %0 here.",
                    "I sell %0, yes."
                }
                #Notoriety Anonymous {
                    "I sell %0 fairly cheap.",
                    "I can sell thee %0.",
                    "I sell only the best %0."
                }
                #Notoriety Known, Famous {
                    "'Twould gladden my heart to sell thee some %0.",
                    "Only the best %0 for $milord/milady$.",
                    "My %0 is not good enough for the likes of thee, but I can sell thee some."
                }
            }
            #Attitude Kindly {
                #Notoriety Infamous {
                    "If I give thee %0, wouldst thou leave my bar?",
                    "Don't break the furniture once thou art drunk on my %0.",
                    "Many famous $villains/villainesses$ like thee have drunk %0 here."
                }
                #Notoriety Outlaw {
                    "I hate selling good %0 to bad people.",
                    "No doubt thou wouldst $spew/spit$ my %0 on the floor.",
                    "Dost thou promise not to act poorly? If so, I shall sell thee %0."
                }
                #Notoriety Anonymous {
                    "I sell %0, of course. 'Tis a tavern, is it not?",
                    "My %0 is good for quenching thirst.",
                    "I sell %0, friend."
                }
                #Notoriety Known, Famous {
                    "An honor indeed to serve thee %0!",
                    "Gladly will I serve thee %0.",
                    "'Tis a pleasure indeed to have one such as thee drinking of my $0."
                }
            }
            #Attitude Goodhearted {
                #Notoriety Infamous, Outlaw {
                    "I should not give drink to scum like thee.",
                    "'Tis like to lose me my head, but I shouldn't serve ruffians like thee.",
                    "Even murderers must get thirsty for %0 I suppose.",
                    "Thou shouldst reform. Not drinking %0 would be a good start. Except I need the money."
                }
                #Notoriety Anonymous {
                    "I'll serve thee %0, if thou likest.",
                    "I sell very good %0. Want some?",
                    "Thou shouldst try my %0. The best in these parts!"
                }
                #Notoriety Known {
                    "Please, have some of my %0. 'Twould be an honor.",
                    "Gladly will I sell thee %0. Wouldst thou like food as well?",
                    "I sell not just %0 but food too."
                }
                #Notoriety Famous {
                    "If thou were to drink %0 here, I could advertise it!",
                    "Art thou really who I think thou art? Goodness, I'd gladly sell thee %0.",
                    "For thee I'll open my private secret cask of the GOOD stuff. Wait 'til thou tastest this %0!"
                }
            }
        }
        #Key "*barmaid*", "*wench*", "*waitress*" {
            #Notoriety Infamous, Outlaw {
                #Attitude Wicked {
                    "Thou art not her type.",
                    "Ah, kidnap the wench. No doubt she has a hand in the till.",
                    "Don't touch her, she may $bite/scratch thy eyes out$."
                }
                #Attitude Belligerent, Neutral, Kindly, Goodhearted {
                    "Please do not make me call the guards.",
                    "Stay away from the %0.",
                    "$She's too good for thee./As my name's _MyName_, she's a good woman.$"
                }
            }
            #Notoriety Anonymous {
                #Attitude Wicked, Belligerent {
                    "$Don't waste thy time with %0. Thou'rt nobody./She looks more a lady than thou!$",
                    "Do not pester the help."
                }
                #Attitude Neutral, Kindly, Goodhearted {
                    "Drink up, and leave the %0 alone.",
                    "The %0 is a good worker.",
                    "What of her? She works hard."
                }
            }
            #Notoriety Known, Famous {
                #Attitude Wicked, Belligerent {
                    "Thou meanest thou art not here to talk to ME?",
                    "Ah, well, if thou must snub me, very well. Talk with the %0 then.",
                    "What of her? She's just the %0."
                }
                #Attitude Neutral, Kindly, Goodhearted {
                    "Oh, the %0 would love to meet thee!",
                    "The %0 works hard. A kind word from thee would make her day.",
                    "Poor %0, the drunks never leave her alone."
                }
            }
        }
        #Key "*food*", "*breakfast*", "*lunch*", "*dinner*" {
            #Attitude Wicked {
                "The world's a mess, and thou wantest %0?",
                "With life so rotten, no doubt any %0 would be too. But I'll sell it to thee anyway.",
            }
            #Attitude Belligerent, Neutral {
                "I sell %0.",
                "Thou wantest %0? Just order it."
            }
            #Attitude Kindly, Goodhearted {
                "A full stomach makes the world go 'round! What %0 didst thou have in mind?",
                "Dost thou want %0? I can sell it to thee."
            }           
      	}
   }
#Sophistication Low {
#Key "*job*",  "*what*do*",    {
	"I run this tavern.",
	"I run this place.", 
	"I'm the tavernkeeper here.", 
			}
#Key "*tavern*"  {
	"I keep this tavern clean and in business.",
	"I am a tavernkeeper, yes.", 
	"I work hard to keep my customers happy.", 
			}
#Key "*job*", "*what*do*", "*tavernkeeper*"  {
	"I keep this tavern clean and running.",
	"I am a tavernkeeper, yes."
			}

        #Key "*ale*", "*beer*" {
            #Attitude Wicked, Belligerent {
                #Notoriety Infamous {
                    "Don't steal me %0! Ye kin buy it like everyone else.",
                    "Heh, 'tis good swill for folk like thee.",
                    "Thee's ugly. Ye want any %0?"
                }
                #Notoriety Outlaw {
                    "Don' break no benches.",
                    "Don' break no faces.",
                    "I sell bad %0. Ye'd like it."
                }
                #Notoriety Anonymous {
                    "%0. Aye, I sell it. Lookee here, life is a pig's arse and me, I sell %0. Ain't no justice, I tell ye.",
                    "Sell %0? My life is so rough I DRINK it. All day. Night too.",
                    "Ifn ye want %0 I got some left in the cracked cask there."
                }
                #Notoriety Known, Famous {
                    "I'll bring ye beer, ifn ye tell me how ye noble types are gonna make me life better!",
                    "Ye big shots ruin me life, then ye want me beer?! Well, have at it, I hope it drowns ye.",
                    "Ye want %0? Sure, an' lemme just slip some rusty nails in it for the flavor. Mayhap then ye'll know how life is for us folk less lucky than thou."
                }
            }
            #Attitude Neutral {
                #Notoriety Infamous, Outlaw {
                    "Aye, %0, 'tis great, ye want summat to drink?",
                    "Ye're my kinda $scoundrel/wench$--ye wanna buy a mug of %0?",
                    "I sells the %0 to rich an' poor an' th'occasional dog."
                }
                #Notoriety Anonymous {
                    "I sells cheap %0, th' best kind.",
                    "I got %0.",
                    "%0's for sale here."
                }
                #Notoriety Known, Famous {
                    "Oh, ye... ye want some... some %0? Lemme get ye %0!",
                    "'Tis good %0, honest, $Sir Noble Milord/Ma'am Noble Lady$.",
                    "Me %0 tastes like water, but ye kin have some."
                }
            }
            #Attitude Kindly {
                #Notoriety Infamous {
                    "Take yer %0 an' go 'way.",
                    "Ye gonna get drunk on me %0? Hee, hee.",
                    "I once had an ettin walk in an' ask for %0. Twice. Heh, heh."
                }
                #Notoriety Outlaw {
                    "Ye's a bad 'un, but I s'pose ye kin have yer %0.",
                    "Don' smash any chairs after I give ye %0.",
                    "I'd heard that ye did bad things. I s'pose it might just be the %0 workin' in ye. I once saw a drunk blacksmith armwrestle a cow."
                }
                #Notoriety Anonymous {
                    "Well, doh, tavern, %0, don't take much to make the connection.",
                    "%0's good ifn ye're thirsty.",
                    "I sells %0. Ye want some?"
                }
                #Notoriety Known, Famous {
                    "Oh, please lemme serve ye some %0!",
                    "Me friends will nivver believe I served %0 to ye!",
                    "I beg ye, drink me %0! Please? Yer such a grand $fellow/lady$ an' I's such a foolish #man/woman#... 'twould be so wonderful to watch ye drink of me %0..."
                }
            }
            #Attitude Goodhearted {
                #Notoriety Infamous, Outlaw {
                    "Don' hit me, please. Ye kin have me %0.",
                    "Yer... yer a scoundrel, an'... an' a murderer an' just nasty! But I'll get ye %0 ifn ye want it, I will, 'cause I's better than ye. An' so there!",
                    "Ye mean murderers like ye drink %0?",
                    "Aye, have ye some %0, and think about yer life gone wrong."
                }
                #Notoriety Anonymous {
                    "Aye, ye kin have me %0.",
                    "I'll sell ye %0.",
                    "Ye ought to try me %0."
                }
                #Notoriety Known, Famous {
                    "I's be glad to sell ye %0. I's got food too.",
                    "Ifn ye want %0, ye kin have it from me.",
                    "%0, sure. Ye want food too?",
                    "I'll give ye %0 from me private stash. Hee, hee."
                }
            }
        }
        #Key "*barmaid*", "*wench*", "*waitress*" {
            #Notoriety Infamous, Outlaw {
                #Attitude Wicked {
                    "Yeh, ain't the %0 ugly? $Heh./Hmm, as ugly as ye be!$",
                    "She's a %0. What 'bout it?",
                    "The %0 bites. $Got me finger once./Do ye?$"
                }
                #Attitude Belligerent, Neutral, Kindly, Goodhearted {
                    "Leave the %0 alone or I'll slap ye silly.",
                    "$'Ey, no hands wanderin' towards the %0, right?/Don't try distractin' the custom from 'er... the men buy more beer ifn she brings it.$",
                    "Ye's ugly, $an' the %0 likes 'em nicer./'an the custom prefers a pretty %0.$"
                }
            }
            #Notoriety Anonymous {
                #Attitude Wicked, Belligerent {
                    "The %0? $An' ye? HA HA HA HA HA HA!/Ye want 'er job?$",
                    "Leave me %0 alone."
                }
                #Attitude Neutral, Kindly, Goodhearted {
                    "Ye got a %0, ye got beer. $Take the beer./Trust me, dearie, ye don't want her life.$",
                    "$Ye lonely or what? Leave/Missie, leave$ the %0 alone.",
                    "Her? Works hard for a %0."
                }
            }
            #Notoriety Known, Famous {
                #Attitude Wicked, Belligerent {
                    "Ye'd rather talk to the %0 than to me?",
                    "Right, the big $noble/lady$ 'ud rather talk to a %0 than to me.",
                    "The %0 is an emplo... implo... servant of mine. Ye should talk to me instead."
                }
                #Attitude Neutral, Kindly, Goodhearted {
                    "The %0 would like to fall over and die of joy ifn ye spoke to her.",
                    "She works too hard, methinks.",
                    "Ifn any more drunks paw her, I'm like to spike their ale with purgative."
                }
            }
        }
        #Key "*food*", "*breakfast*", "*lunch*", "*dinner*" {
            #Attitude Wicked {
                "Ye want %0. What ifn I made ye listen to how me life is as rotten as yesterday's mutton? No? Well, figure that.",
                "I'd ready to sell ye %0, but don't be expectin' no smiles with it.",
            }
            #Attitude Belligerent, Neutral {
                "I can sell ye %0.",
                "%0? I can sell ye that."
            }
            #Attitude Kindly, Goodhearted {
                "Aye, fill yer belly with %0, bring a smile to yer face.",
                "I can whip up some %0 for ye if ye want it."
            }           
      	}
   }
}

// End of fragment
