// Brigand function
//
// Keywords:
// job, steal, rob, thief, bandit, brigand, crime, villain
//
//
// 
// - cwm


#Fragment Britannia, Job, Britannia_Brigand {

        #Sophistication High {
                #KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" {
                        #Attitude Wicked {
                                "None of thy business.",
                                "I do THIS! [Attack]"
                        }
                        #Attitude Neutral {
                                "Thou couldst say that I'm a traveling man.",
                                "I'm sort of like a porter. I lighten the burdens of rich travelers."
                        }
                        #Attitude Goodhearted {
                                "I do what I must to get by, just as we all do.",
                                "Oh, I do this and that."
                        }
                }

                #KEY "*thief*" "*bandit*" "*brigand*" {
                        #Attitude Wicked {
                                "Think thou'rt real smart, dost thou? [Attack]",
                                "I don't know anything about bandits."
                        }
                        #Attitude Neutral {
                                "Don't go throwing around accusations thou can't prove.",
                                "Now do I really look like a bandit?"
                        }
                        #Attitude Goodhearted {
                                "Ah, thieves and bandits are overwhelmingly common of late. Honest citizens should take care.",
                                "Bandits everywhere these days. 'Tis shocking, really."
                        }
                }

                #KEY "*steal*" "*rob*" "*crime*" {
                        #Attitude Wicked {
                                "Thou canst prove nothing!",
                                "Thou dost speak of things thou art ignorant of."
                        }
                        #Attitude Neutral {
                                "I have just as much respect for the law as the next man.",
                                "I'm as honest as the day is long."
                        }
                        #Attitude Goodhearted {
                                "Crime is horrible, is it not? Thou wouldst be inclined to think the authorities would do something.",
                                "Being poor is the only crime I'm guilty of."
                        }
                }

                #KEY "*villain*" {
                        #Attitude Wicked {
                                "Thou dost resemble a villain thyself!",
                                "Draw thy sword! [Attack]"
                        }
                        #Attitude Neutral {
                                "I'll not be accused by the likes of thee. [Attack]",
                                "I'll not be accused by the likes of thee. [Leave]."
                        }
                        #Attitude Goodhearted {
                                "Here here, is that any way to talk?",
                                "If that's the way of it, I'll be saying good day. We may be meeting again sooner than thou dost think. [Leave]"
                        }
                }
        }

        #Sophistication Medium {
                #KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" {
                        #Attitude Wicked {
                                "None of thy business.",
                                "I do THIS! [Attack]"
                        }
                        #Attitude Neutral {
                                "Thou couldst say that I'm a traveling man.",
                                "I am sort of like a porter. I lighten the burdens of rich travelers."
                        }
                        #Attitude Goodhearted {
                                "I do what I must to get by, just as we all do.",
                                "Oh, I do this and that."
                        }
                }

                #KEY "*thief*" "*bandit*" "*brigand*" {
                        #Attitude Wicked {
                                "Think thou'rt real smart, huh? [Attack]",
                                "I don't know anything about bandits."
                        }
                        #Attitude Neutral {
                                "Don't go throwing around accusations thou can't prove.",
                                "Now do I really look like a bandit?"
                        }
                        #Attitude Goodhearted {
                                "Ah, thieves and bandits are quite thick around these parts. Folk should take care.",
                                "Bandits everywhere these days. Shocking, really."
                        }
                }

                #KEY "*steal*" "*rob*" "*crime*" {
                        #Attitude Wicked {
                                "Thou can prove nothing!",
                                "Thou dost speak of things thou'rt ignorant of."
                        }
                        #Attitude Neutral {
                                "I have just as much respect for the law as the next man.",
                                "I'm as honest as the day is long."
	                        }
                        #Attitude Goodhearted {
                                "Crime is horrible, isn't it? Thou wouldst think the authorities would do something.",
                                "Being poor's the only crime I'm guilty of."
                        }
                }

                #KEY "*villain*" {
                        #Attitude Wicked {
                                "Thou'rt a villain thine own self, I'd wager!",
                                "Draw thy sword. [Attack]"
                        }
                        #Attitude Neutral {
                                "I'll not be accused by the likes of thee. [Attack]",
                                "I'll not be accused by the likes of thee. [Leave]."
                        }
                        #Attitude Goodhearted {
                                "Here, is that any way to talk?",
                                "If that's the way of it, I'll be saying good day. We may be meeting again sooner than thou dost think. [Leave]"
                        }
                }
        }

        #Sophistication Low {
                #KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" {
                        #Attitude Wicked {
                                "None of thy business.",
                                "I do THIS! [Attack]"
                        }
                        #Attitude Neutral {
                                "Ye could say that I'm a traveling man.",
                                "I'm sort of like a porter. I lighten the burdens of rich travelers."
                        }
                        #Attitude Goodhearted {
                                "I do what I must to get by, just as we all do.",
                                "Oh, I do this and that."
                        }
                }

                #KEY "*thief*" "*bandit*" "*brigand*" {
                        #Attitude Wicked {
                                "Think yer real smart, do ye? [Attack]",
                                "I don't know nothin' about no bandits."
                        }
                        #Attitude Neutral {
                                "Don't go throwin' around accusations thou can't prove.",
                                "Now do I really look like a bandit?"
                        }
                        #Attitude Goodhearted {
                                "Ah, thieves and bandits are powerful thick around these parts. Folk should take care.",
                                "Bandits everywhere these days. Shocking, really."
                        }
                }

                #KEY "*steal*" "*rob*" "*crime*" {
                        #Attitude Wicked {
                                "Ye can't prove nothin'!",
                                "Ye speak of things yer ignorant of."
                        }
                        #Attitude Neutral {
                                "I have just as much respect for the law as the next man.",
                                "I'm as honest as the day is long."
                        }
                        #Attitude Goodhearted {
                                "Crime is horrible, ain't it. Ye'd think the authorities would do something.",
                                "Bein' poor's the only crime I'm guilty of."
                        }
                }

                #KEY "*villain*" {
                        #Attitude Wicked {
                                "Yer a villain yerself!",
                                "Draw yer sword. [Attack]"
                        }
                        #Attitude Neutral {
                                "I'll not be accused by the likes of thee. [Attack]",
                                "I'll not be accused by the likes of thee. [Leave]."
                        }
                        #Attitude Goodhearted {
                                "Here, is that any way to talk?",
                                "If that's the way of it, I'll be sayin' good day. We may be meeting again sooner than thou dost think. [Leave]"
                        }
                }
         }
}


