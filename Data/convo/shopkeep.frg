// Generic shopkeeper fragment for Britannia
//
// Keywords:
// buy, supplies
// sell
//
// Notes: This should probably be attached to every shopkeeper in
// Britannia even if they have a job-specific pool. It's very skimpy.
//
// -Raph Koster
//

#Fragment Britannia, Job, Britannia_Shopkeeper {
    #Sophistication High {

#Key "*what*thou*sell*", "*what*thou*sale*", "*what*you*sell*", "*what*you*sale*", "*can*you*sell*", "*what*you*sale*" {
"Why, I sell the same things that any other _Job_ would sell.",
"If thou dost look around my shop, I'm sure it will become obvious to thee.",
"I am a _Job_.  What wouldst thou expect me to sell?",
"I sell fairy dust.  What dost thou think a _Job_ would sell?"
}
#Key "*sell*", "*thou*purchase*", "*you*purchase*", "*you*buy*", "*I*sale*" {
#Notoriety Infamous, Outlaw {
                "Doubtless aught thou hast to sell would be stolen goods.",
                "I trust not aught that ye sell, given thy reputation.",
                "Thou wouldst doubtless try to cheat me, wert thou trying to sell me aught."
            }
            #Notoriety Anonymous, Known, Famous {
                "If thou hast aught to sell, mayhap I am interested--give it to me and I shall value it for thee. Otherwise thou art welcome to buy my goods.",
                "Depending on what thou hast to sell, I may be interested in purchasing it to add to my stock. Hand it to me and I shall decide its worth.",
                "If thou art interested in selling an item to me, then thou must give it to me and I'll decide if I need it in my stock. Otherwise, thou can buy anything that I sell here.",
"I might be willing to purchase some supplies from thee. Thou wouldst need to hand them to me for appraisal. But if thou wouldst like to buy something, just say so.",
"Let me see what thou art offering to sell me, if indeed that is thy intent. Or thou couldst buy something of mine.",
"I am a _Job_. I could purchase those things that I supply. ",
"If thou dost wish to sell something to me, hand it over and I shall determine its value. Otherwise thou art welcome to buy some of my items."
            }
}


        #Key "*buy*", "*supplies*" {
            #Notoriety Infamous, Outlaw {
                #Attitude Wicked, Belligerent {
                    "If thou needest to buy something, I can supply things from my wares. No stealing needed.",
                    "Thou'rt more like to steal than to buy supplies, but nonetheless I can offer thee what I sell.",
                    "Life is bad enough that I have no qualms about selling my stock to a known $scoundrel/hussy$."
               				 }
                #Attitude Neutral, Kindly, Goodhearted {
                    "Thou must not steal from me, mind! I can sell thee supplies.",
                    "If thou needest supplies, why art thou not looking to steal them? I can sell them to thee if thou dost behave.",
                    "I trust thee not, but will offer to sell thee supplies."
                					}
            					}
            #Notoriety Anonymous, Known, Famous {
                #Attitude Neutral, Kindly, Goodhearted {
                    "I sell many wares and thou'rt welcome to buy from me!",
                    "If thou seekest supplies, thou canst buy from me.",
                    "If thou seekest to buy, I can sell to thee."
                					}
                #Attitude Wicked, Belligerent {
                    "Thou canst buy supplies from me if thou art willing to waste thy time with one my mine humble station.",
                    "If thou'rt willing to interact with one as miserable as I, thou canst buy supplies from me."
                    "I can sell thee supplies. 'Tis hardly a hardship--not at all like the rest of my hopeless life."
               					 }
            					}
        }
}
        
    #Sophistication Medium {
        #Key "*buy*", "*supplies*" {
            #Notoriety Infamous, Outlaw {
                #Attitude Wicked, Belligerent {
                    "'Twould be just my ill-luck if thou were to steal from me.",
                    "Prithee, steal not! I can sell thee supplies.",
                    "Well, I could hardly get in worse trouble... even if I do help a criminal such as thee. I can sell thee supplies."
                }
                #Attitude Neutral, Kindly, Goodhearted {
                    "Prithee, harm me not! I am but a merchant of supplies!",
                    "Thou'rt speaking of buying, yes? Not of stealing?",
                    "Thou'rt unsavory, but I am willing to sell to thee."
                }
            }
            #Notoriety Anonymous, Known, Famous {
                #Attitude Neutral, Kindly, Goodhearted {
                    "Thou mayst purchase supplies from me.",
                    "I would gladly sell to thee of my many wares.",
                    "My shop sells many things."
                }
                #Attitude Wicked, Belligerent {
                    "Thou meanest one such as thee dost not have everything thou needest already?",
                    "If thou wilt buy from one such as me, I have much to sell."
                    "Making money may be th'only thing to cheer my dismal life."
                }
            }
        }
       
#Key "*sell*", "*thou*purchase*", "*you*purchase*", "*you*buy*", "*I*sale*" {
#Notoriety Infamous, Outlaw {
                "I am not a fence, so sell me not thy stolen goods.",
                "Thou'rt untrustworthy--anything thou sellest must be bad.",
                "Thou wishest to sell aught? I might buy, though I suspect thou'rt a cheat."
            }            
#Notoriety Anonymous, Known, Famous {
                "I may be interested in what thou have-let me see it. Otherwise thou art welcome to buy my goods.",
                "I could be interested in purchasing what thou has to add to my stock. Hand it to me and I shall decide its worth.",
                "If thou art interested in selling an item to me, then thou must give it to me and I'll decide if I need it in my stock. Or thou can buy anything that I sell here.",
"I may be willing to purchase some supplies from thee. Hand them to me for appraisal. And if thou wouldst like to buy something from me, just say so.",
"Let me see what thou art offering to sell me, if indeed that is what thou dost mean. Or thou couldst buy something of mine.",
"I am a _Job_. I could purchase those things that I supply. ",
"If thou dost wish to sell something to me, hand it over and I shall determine its value. Otherwise thou art welcome to buy some of my items."
            }
       }
}
    #Sophistication Low {
        #Key "*buy*", "*supplies*" {
            #Notoriety Infamous, Outlaw {
                #Attitude Wicked, Belligerent {
                    "Aww, no stealin', by thy $lord/lady$ship's leave!",
                    "Sure, I can sell thee stuff even if thou'rt a bit whiffy.",
                    "I can sell thee things if thou ain't hurtin' me."
                }
                #Attitude Neutral, Kindly, Goodhearted {
                    "Don' hurt me! I can sell thee stuff.",
                    "Please, don' steal nothin'.",
                    "If thou don' beat me up, I can sell to thee."
                }
            }
            #Notoriety Anonymous, Known, Famous {
                #Attitude Neutral, Kindly, Goodhearted {
                    "I'm here to sell thee supplies.",
                    "I got lots to sell!",
                    "Thou surely wants all the things that I can sell thee."
                }
                #Attitude Wicked, Belligerent {
                    "Thou wishes to buy from me? Thee? A big $lord/lady$ type?",
                    "I'm a little worm to thee, but I can make thee spend money in my shop!",
                    "I could use anythin' to cheer me up. Want to buy from me?"
                }
            }
        }
#Key "*sell*", "*thou*purchase*", "*you*purchase*", "*you*buy*", "*I*sale*" {
#Notoriety Infamous, Outlaw {
                "Thou'rt wantin' to sell to me? I can't afford the price thou wants... the price thou'rt willin' to
break me head to get...",
                "Thou wants to sell somethin'? 'Tis probably stolen.",
                "Thou'rt a cheat, but I might buy from thee anyway."
            }            
#Notoriety Anonymous, Known, Famous {
	"What's thou got to sell? Give it here. Or buy somethin'. Or go 'way.",
                "If I sell it, I buys it too. Let me see it. Oh, and if thou'rt wantin' to buy somethin', jus' say so.",
                "Thee got summat to sell? Give it here.",
                "If thou'rt wantin' to but somethin', then say the word.  Buy... that's the word. Or give me what thou'rt wantin' to sell me.",
                "Thou wants to sell me somethin'? Hand it over and I'll see if it's worth nothin'.",
"I could take some supplies off thy hands. Give 'em to me for a once-over. And look around abit. Just say thou wants to buy somethin' and I'll see if I got it.",
"I could purchase those things that I supply. "
            }
       }
    }
}


            
