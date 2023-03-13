// Common laborer fragment for Britannia
//
// Keywords:
// work, labor, job
//
// Notes: obviously, this is a very very skimpy pool. It is also largely
// temporary as eventually it will cover several different jobs. For now,
// though, all those jobs have been conflated, which means that the speeches
// are necessarily damn generic. :P
//
// -Raph Koster
//

#Fragment Britannia, Job, Britannia_Laborer {
    #Sophistication High {
        #Key "*work*", "*labor*", "*job*", "*what*do*do*"  {
            #Attitude Wicked {
                "Thou askest after my work? Well, I tell thee frankly: I do makework, I labor under the sun that roasts my bones, and I break my back for petty rewards and much disrespect!",
                "My job is that of laborer, and a paltry lot 'tis! Gladly would I set it aside, for my life is miserable.",
                "My job? Dost THOU wish it? Thou canst have it! Being a laborer such as I am hath ruined my life and left me hopeless and in despair."
            }
            #Attitude Belligerent {
                "'Tis not the best life in the world to be a laborer. My life has oft been better than 'tis now.",
                "Thou askest after my labor? Well, I slave away for little reward. 'Tis not as bad as it could be, I suppose, but 'tis not a wonderful life either.",
                "To be a laborer is an unhappy life indeed--at least, 'tis so for me."
            }
            #Attitude Neutral {
                "I am a laborer, and working is how I spend my days.",
                "I wish I could tell thee wonderful details of my work, but I am just a laborer.",
                "I am just a laborer, and my life hath nothing special about it. I live out my days much as anyone
else."
            }
            #Attitude Kindly, Goodhearted {
                "I enjoy my work, even if 'tis only that of a common laborer.",
                "I believe that even we laborers have much to offer, and my life is happy enough that my belief has not been questioned.",
                "I am but a laborer, yet my life is content. I rejoice in the sunshine and the song of birds, I marvel at the sky full of stars, and I welcome those whom I love into my home. Canst thou say the same?"
            }    
        }
    }
    #Sophistication Medium {
        #Key "*work*", "*labor*", "*job*", "*what*do*do*"  {
            #Attitude Wicked {
                "None treat a laborer with common decency!",
                "I'm a laborer and 'tis miserable.",
                "My job? Thou canst have it!."
            }
            #Attitude Belligerent {
                "The life of a laborer is not the best.",
                "I do a lot and get little back. Life could be worse, but...",
                "I don't like being a laborer much."
            }
            #Attitude Neutral {
                "I am a laborer.",
                "There is nothing much to know of me, for I am just a laborer.",
                "I'm just a laborer, so there is not much to tell thee."
            }
            #Attitude Kindly, Goodhearted {
                "I like being a laborer.",
                "I am a laborer, and 'tis a good life.",
                "I am happy as a laborer, for the world is good to me."
            }
        }
    }
    #Sophistication Low {
#KEY "*" {
#Attitude Wicked {
	"Where's me welk? WHERE'S ME DAMN WELK!?",
	"'Ere, smell this. Think it's gone over?",
	"Casey? Is't thee Casey? I know what thou did. I KNOW WHAT THOU DID.",
	"vrgrumbfr PLOTTING sgarfifmfl BUGGEREM nrgfrafmalms LORD BRITISH ifinimirik G'ORF, THEE!",
	"Don't turn around, but there's a garfink followin' thee. DON'T TURN AROUND!"
	}
#Attitude Neutral {
	"Millennium hand and shrimp!",
	"Gorra copper frien'? Sparra copper ferra vetrin?",
	"I AIN'T drunk! I'm an IDIOT! So there!",
	"Get orf, thee. This town already got an idiot, an it don't needa nother.",
	"OODIE VOODIE BOODIE DOODIE!"
	}
#Attitude Goodhearted {
	"Oooh. Colors.",
	"Hmmph? Hmpph.",
	"I don't think... Hmmm.  I don't, do I?"
	}
	}

        #Key "*work*", "*labor*", "*job*", "*what*do*do*"  {
            #Attitude Wicked {
                "I hate bein' a laborer!",
                "I'm a laborer and 'tis awful.",
                "If thou wants me job, thou kin have it!"
            }
            #Attitude Belligerent {
                "Well, I's just a dumb laborer.",
                "I don' get paid 'nough for bein' a laborer.",
                "I don' like bein' a laborer much."
            }
            #Attitude Neutral {
                "I's a laborer.",
                "Nothin' to tell, I's just a laborer.",
                "What, thou wants to know about bein' a laborer?."
            }
            #Attitude Kindly, Goodhearted {
                "I likes bein' a laborer.",
                "I's happy to be a laborer. 'Tis challenging work.",
                "I's best suited to be a laborer, so I is one."
            }
        }
    }
}

// End of fragment
