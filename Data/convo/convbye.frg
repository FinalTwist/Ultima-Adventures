// convbye.frg for when players end a conversation with an NPC.
//
// Raph
//

#Fragment ?, ?, ? {
    #Sophistication High {
        #Key "*bye*", "*farewell*", "*see ya*", "*hasta l*",
                "*see you *" {
            #Attitude Belligerent, Wicked {
                #Notoriety Infamous, Outlaw {
                    "Good riddance to thee. [Leave]",
                    "I thank thee for leaving me my miserable life. [Leave]",
                    "Thou didst not kill me! I thank thee! [Leave]"
                }
                #Notoriety Anonymous, Known, Famous {
                    "Finally! Be on thy way.[Leave]",
                    "About time, for I have other tasks to attend to. [Leave]",
                    "Ah, decided that thou hast wasted my time long enough? [Leave]"
                }
            }
            #Attitude Neutral, Kindly, Goodhearted {
                #Notoriety Infamous, Outlaw {
                    "Good riddance to thee, scum. [Leave]",
                    "I have survived an encounter with _Name_! [Leave]",
                    "I thank thee for not slaying me, _Name_. [Leave]"
                }
                #Notoriety Anonymous {
                    "Fare thee well. [Leave]",
                    "Goodbye, stranger. [Leave]",
                    "May the rest of thy day be pleasant. [Leave]"
                }
                #Notoriety Known, Famous {
                    "'Twas a pleasure to speak with thee, _Name_. [Leave]",
                    "Fare thee well, _Name_. [Leave]",
                    "Goodbye, _Name_. [Leave]"
                }
            }
        }
        #Key "*go away*", "*leave me alone*", "*fuck off*", "*take a hike*",
                "*go fly a kite*" {
            #Attitude Belligerent, Wicked {
                "Fine. Have a nice day, scum. [Leave]",
                "Same to thee. [Leave]",
                "Oh, don't be so immature about it. [Leave]"
            }
            #Attitude Kindly, Goodhearted, Neutral {
                "Fine. Have a nice day. [Leave]",
                "Same to thee. [Leave]",
                "There is no need to be so rude about it. [Leave]",
                "Very well, if thou dost not wish to speak to me, kindly do not interrupt me again. [Leave]"
            }
        }
    }
    #Sophistication Medium {
        #Key "*bye*", "*farewell*", "*see ya*", "*hasta l*",
                "*see you *" {
            #Attitude Belligerent, Wicked {
                #Notoriety Infamous, Outlaw {
                    "Good riddance. [Leave]",
                    "Look, everyone, _Name_ did not kill me! [Leave]",
                    "Thou didst not kill me?! [Leave]"
                }
                #Notoriety Anonymous, Known, Famous {
                    "Finally! Go away. [Leave]",
                    "Thank thee for ending thy interruption. [Leave]",
                    "Goodbye. [Leave]"
                }
            }
            #Attitude Neutral, Kindly, Goodhearted {
                #Notoriety Infamous, Outlaw {
                    "Good riddance! [Leave]",
                    "I spoke with _Name_ and I lived...! [Leave]",
                    "Goodbye. [Leave]"
                }
                #Notoriety Anonymous {
                    "Farewell. [Leave]",
                    "Goodbye. [Leave]",
                    "Good day, then. [Leave]"
                }
                #Notoriety Known, Famous {
                    "An honor to speak with thee, _Name_. [Leave]",
                    "Farewell, _Name_. [Leave]",
                    "Goodbye, _Name_. [Leave]"
                }
            }
        }
        #Key "*go away*", "*leave me alone*", "*fuck off*", "*take a hike*",
                "*go fly a kite*" {
            #Attitude Belligerent, Wicked {
                "Fine. Have a nice day. [Leave]",
                "Same to thee. [Leave]",
                "Thou'rt very rude. [Leave]"
            }
            #Attitude Kindly, Goodhearted, Neutral {
                "Fine. Have a nice day. [Leave]",
                "Same to thee. [Leave]",
                "There is no need to be so rude about it. [Leave]",
            }
        }
    }
    #Sophistication Low {
        #Key "*bye*", "*farewell*", "*see ya*", "*hasta l*",
                "*see you *" {
            #Attitude Belligerent, Wicked {
                #Notoriety Infamous, Outlaw {
                    "Bye. [Leave]",
                    " [Leave]",
                    "Thou didst not kill me?! [Leave]"
                }
                #Notoriety Anonymous, Known, Famous {
                    "Finally! Go away. [Leave]",
                    "Thank thee for ending thy interruption. [Leave]",
                    "Goodbye. [Leave]"
                }
            }
            #Attitude Neutral, Kindly, Goodhearted {
                #Notoriety Infamous, Outlaw {
                    "Good riddance! [Leave]",
                    "I spoke with _Name_ and I lived...! [Leave]",
                    "Goodbye. [Leave]"
                }
                #Notoriety Anonymous {
                    "Farewell. [Leave]",
                    "Goodbye. [Leave]",
                    "Good day, then. [Leave]"
                }
                #Notoriety Known, Famous {
                    "An honor to speak with ye, _Name_. [Leave]",
                    "Farewell, _Name_. [Leave]",
                    "Bye, _Name_. [Leave]"
                }
            }
        }
        #Key "*go away*", "*leave me alone*", "*fuck off*", "*take a hike*",
                "*go fly a kite*" {
            #Attitude Belligerent, Wicked {
                "Fine. [Leave]",
                "Same to ye. [Leave]",
                "Ye's rude. [Leave]"
            }
            #Attitude Kindly, Goodhearted, Neutral {
                "Fine. [Leave]",
                "Same to ye. [Leave]",
                "Aye, whatever. [Leave]",
            }
        }
    }
}

