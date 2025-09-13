// convinit.frg is for the "I am talking to you" automatic responses
// when conversation is initiated.
//
// Raph
//

#Fragment Britannia, General, Britannia_Init {
    #Sophistication High {
        #Key "@InternalConvinit" {
            #Attitude Wicked, Belligerent {
                "Prithee, be quiet.",
                "Thou mayst wish to talk to me, but I do not wish to talk to thee.",
                "What, thou dost wish to waste my time?"
            }
            #Attitude Neutral, Kindly, Goodhearted {
                #Notoriety Anonymous {
                    "Thou wishest to speak with me?",
                    "Thou hast mine attention.",
                    "What is it thou wishest?",
                    "I am listening to thee.",
                }
                #Notoriety Infamous, Outlaw, Known, Famous {
                    "Thou wishest to speak with me, _Name_?",
                    "Thou hast mine attention, _Name_.",
                    "What is it thou wishest, _Name_?",
                    "I am listening to thee, _Name_."
                }
            }
        }
    }
    #Sophistication Medium {
        #Key "@InternalConvinit" {
            #Attitude Wicked, Belligerent {
                "Be quiet, mine head hurts.",
                "What if I do not wish to speak with thee?",
                "Waste not my time."
            }
            #Attitude Neutral, Kindly, Goodhearted {
                #Notoriety Anonymous {
                    "Thou wishest to speak with me?",
                    "Thou hast mine attention.",
                    "What is it thou wishest?",
                    "I am listening to thee.",
                }
                #Notoriety Known, Famous {
                    "Thou wishest to speak with me, _Name_?",
                    "Thou hast mine attention, _Name_.",
                    "What is it thou wishest, _Name_?",
                    "I am listening to thee, _Name_."
                }
                #Notoriety Infamous, Outlaw {
                    "Prithee, do not hurt me.",
                    "Hurt me not, and I will talk with thee.",
                    "Thou'rt a dangerous $man/woman$ to talk to.",
                    "Thou wishest to speak to me? Please, harm me not..."
                }
            }
        }
    }
    #Sophistication Low {
        #Key "@InternalConvinit" {
            #Attitude Wicked, Belligerent {
                "Go 'way.",
                "I ain't wantin' to talk to thee.",
                "Thou'rt rude."
            }
            #Attitude Neutral, Kindly, Goodhearted {
                #Notoriety Anonymous {
                    "Hmm?",
                    "Aye?",
                    "What's thee wantin'?",
                    "I'm listenin'.",
                }
                #Notoriety Known, Famous {
                    "Yes, _Name_?",
                    "Hmm? Oh! Tis thee, _Name_!",
                    "Can I help thee?",
                    "_Name_! Nice to see thee."
                }
                #Notoriety Infamous, Outlaw {
                    "Don't hurt me.",
                    "What do thou want? I can't help.",
                    "Thou wants to talk to me? Umm...",
                    "Thou'rt talkin' to me?"
                }
            }
        }
    }
}    


