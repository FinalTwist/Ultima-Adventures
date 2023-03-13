// rehello.frg is for the repeat hello's. I *know* players will do this to
// make our sad, awful "AI" look dumb.
//
// Edmond
//

#Fragment Britannia, Default, ReHello {
    #Sophistication High {
        #Key "Hello" {
            #Attitude Wicked, Belligerent {
                "I greeted thee. Now, GET ON WITH IT!",
                "Oh, go soak thy head. I heard thee.",
                "HELLO! HELLO! I hear ye, foul vermin.",
                "Hello, a dozen times; Hello! Gads."
            }
            #Attitude Neutral, Kindly, Goodhearted {
                #Notoriety Anonymous {
                    "And hello again. How may I help thee, _Name_?",
                    "Thou said that.",
                    "Is it me, or art thou repetitive?",
                    "And hello to thee, again."
                }
                #Notoriety Infamous, Outlaw, Known, Famous {
                    "How did thou become so notorious repeating thyself?",
                    "Thou art fearsome and repetitive, _Name_.",
                    "_Name_, repeating thyself gets thee nowhere.",
                    "And to thee, hello and hello again."
                }
            }
        }
    }
    #Sophistication Medium {
        #Key "hello" {
            #Attitude Wicked, Belligerent {
                "Was that thee, or a parrot, _Name_?",
                "Get to thy point, $man/lady$!",
                "Lose thy greetings and acquire some meaning."
            }
            #Attitude Neutral, Kindly, Goodhearted {
                #Notoriety Anonymous {
                    "Hello again. My, what a friendly $fellow/lady$."
                    "Hee-hee. Hello. Thou art a rascal.",
                    "What is it thou wishest?",
                    "Yes, goodtidings to thee as well."
                }
                #Notoriety Known, Famous {
                    "I'd think a renown $rogue/lady$'d too busy for repeatin' thyself.",
                    "Yes, I heard thou.",
                    "So famous, yet so addled. Hello, _Name_.",
                    "I am listening to thee, _Name_. But hello anyway."
                }
                #Notoriety Infamous, Outlaw {
                    "I greet thee a thousand times.",
                    "Yes, $Sire/Lady$, hello. I heard thee."
                    "Thou'rt a repetitive $man/woman$ to talk to.",
                    "Uh, right. Art thou having me on?"
                }
            }
        }
    }
    #Sophistication Low {
        #Key "*" {
            #Attitude Wicked, Belligerent {
                "Uh what? Hello. Hm. Could thou repeat that?",
                "Er... Did thee say, 'hello,' _Name_?",
                "Uh. What?"
            }
            #Attitude Neutral, Kindly, Goodhearted {
                #Notoriety Anonymous {
                    "Canst thou say that again?",
                    "I hear thee. Do I say 'hello' now?",
                    "Thou art broing."
                }
                #Notoriety Known, Famous {
                    "Hello, _Name_. Again, that is.",
                    "What? I'm confused now. Hello, I guess.",
                    "I'm not smart. _Name_ is smart. Hello _Name_."
                }
                #Notoriety Infamous, Outlaw {
                    "Say 'hello' again and I pour slop on thy armor.",
                    "WHAT?!?!",
                    "I'm not worthy, but hello anyways.",
                    "Are ye talkin' to me? Ye must be, I see no others afoot."
                }
            }
        }
    }
} 
