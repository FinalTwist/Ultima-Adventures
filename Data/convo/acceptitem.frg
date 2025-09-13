// internal accept item
// written: April 30
// 
//always prefaced with ("thou art giving me [item]?")

#Fragment Global, Default, Britannia_AcceptItem { 

    #Sophistication High {
        #Key "@InternalAcceptItem" {
            #Attitude Wicked {
                #Notoriety Infamous {
                    "Good! 'Tis what I need!",
	      "Thanks.",
	      "'Tis appreciated."
}
                #Notoriety Anonymous {
                   "I will accept it from thee. Thanks.",
	     "I will make good use of it.",
	     "I've been needing it."
}
	#Notoriety Famous {
	"I'm happy that thou chose to do without.",
	     "This will certainly help me. Thanks.",
	     "I appreciate it."
}
}
#Attitude Neutral {
                #Notoriety Infamous {
                    "Thank thee.",
	      "I accept.",
	      "I will take it, then."
                }
                #Notoriety Anonymous {
                    "Tis very generous of thee.",
                    "I shall accept with pleasure.",
	      "Thank thee!" 
}
                #Notoriety Famous {
                     "Thou art most kind $good sir/good lady$!",
	     "I shall be happy to take it!  Thank thee!",
	     "May a light follow thee in the dark!"
                }
            }
            #Attitude Kindly {
                #Notoriety Infamous {
                    "Why, I thank thee!",
                    "'Tis good to see thee doing right.",
	      "I knew that thy generosity would show through!"
                }
	#Notoriety Anonymous {
                    "'Tis gracious of thee!",
                    "I humbly accept. Thank thee.",
	      "Thank thee, kind $sir/lady$!"
                }
                #Notoriety Famous {
                    "This from _Name_? Oh thank thee!"
                    "Thank thee _Name_! Thank thee!",
	      "'Tis greatly appreciated!"
}
}
}
}
#Sophistication Medium {
       #Key "@InternalAcceptItem" {
            #Attitude Wicked {
                #Notoriety Infamous {
                    "Good! 'I needed this!",
	      "Thanks.",
	      "I appreciate it."
}
                #Notoriety Anonymous {
                   "I will take it from thee. Thanks.",
	     "I will make use of it.",
	     "I've been needing it."
}
	#Notoriety Famous {
	"I'm happy that thou chose to give it to me.",
	     "This will certainly help. Thank thee.",
	     "I appreciate it."
}
}
#Attitude Neutral {
                #Notoriety Infamous {
                    "Thank thee.",
	      "I accept.",
	      "I will take it, then."
                }
                #Notoriety Anonymous {
                    "Tis generous of thee.",
                    "I shall accept with pleasure!",
	      "Thank thee!" 
}
                #Notoriety Famous {
                     "Thou art a kind $man/lady$!",
	     "I shall be happy to take it!  Thank thee!",
	     "May thy paths be smooth!"
                }
            }
            #Attitude Kindly {
                #Notoriety Infamous {
                    "Why, I thank thee!",
                    "'Tis good to see thee doing aright.",
	      "I knew that thy generosity would finally come through!"
                }
	#Notoriety Anonymous {
                    "'Tis gracious of thee!",
                    "I humbly accept. Thank thee, kind $sir/lady$.",
	      "Thank thee, $milord/milady$!"
                }
                #Notoriety Famous {
                    "This from _Name_? Thank thee!"
                    "Thank thee _Name_! Thank thee very much!",
	      "'Tis greatly appreciated!"
}
}
}
}
#Sophistication Low {
#Key "@InternalAcceptItem" {
            #Attitude Wicked {
                #Notoriety Infamous {
                    "Good! 'Tis what I need!",
	      "Thanks.",
	      "'Tis a welcome thing."
}
                #Notoriety Anonymous {
                   "I'll be glad to take it from thee. Thanks.",
	     "I'll make good use of it.",
	     "I've been needing it."
}
	#Notoriety Famous {
	"I'm happy that thou chose to do this.",
	     "It will certainly help me. Thanks.",
	     "I appreciate it."
}
}
#Attitude Neutral {
                #Notoriety Infamous {
                    "Thank thee.",
	      "I accept.",
	      "I will take it, then."
                }
                #Notoriety Anonymous {
                    "Tis very generous of thee.",
                    "I shall accept with pleasure.",
	      "Thank thee!" 
}
                #Notoriety Famous {
                     "Thou art kind!",
	     "I shall take it gladly!  Thanks!",
	     "Thank thee! And be safe!"
                }
            }
            #Attitude Kindly {
                #Notoriety Infamous {
                    "Why, I thank thee!",
                    "About time thou did something good.",
	      "I knew that thy goodness would show itself!"
                }
	#Notoriety Anonymous {
                    "'Tis gracious of thee!",
                    "I accept! Thank thee.",
	      "Thank thee, kind $sir/lady$!"
                }
                #Notoriety Famous {
                    "This from thee? Oh thank thee!"
                    "Thank thee _Name_! Thank thee!",
	      "'Tis very welcome!"
}
}
}
}
}

// End of Template Fragment

