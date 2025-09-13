// Occlo Runner function
//
// Keywords:
// runner, job, message, cashual
//
//
// 
// - Dab


#Fragment Britannia, Job, Britannia_O_Runner {
#Sophistication High {
#KEY "*Runner*", "*job*", "*what*do*do*", "*occupation*", "*profession*" {

#Attitude Wicked {
	"I carry messages to and from places.",
"I carry messages.  Pardon me, I need to be off. [Leave]"
			}
#Attitude Neutral {
	"I carry messages to and from places in order to further communication.",
"I am a message carrier.  Pardon me, I need to be running. [Leave]"
			}
#Attitude Goodhearted {
	"I carry messages back and forth in order to keep people informed of events and information.",
"I am a message carrier.  Pardon me, I'm running behind schedule. [Leave]"
			}
}

#KEY "*message*" {

#Attitude Wicked {
	"Only the recipient can know the message.",
"I cannot tell thee the contents of the messages I carry."
			}
#Attitude Neutral {
	"I am not allowed to relay the messages to anyone but the proper recipient.",
"I cannot share with thee any messages unless thou art the recipient.  And I know that thou art not."
			}
#Attitude Goodhearted {
	"I am not allowed to relay the messages to anyone but the proper recipient."
		}
}

#KEY "*cashual*" "*Cashual*" {

#Attitude Wicked {
	"Yes, I carry for the members of the Cashual.  If thou wouldst excuse me, I must be going.[Leave]"
			}
#Attitude Neutral {
	"I sometimes carry for the members of the Cashual.  I must run, now.  Farewell. [Leave]"
			}

#Attitude Goodhearted {
"I often carry messages for the members of the Cashual.  They are very important people who bring water for the crops."
	
			}
}
}

#Sophistication Medium {
#KEY "*Runner*", "*job*", "*what*do*do*", "*occupation*", "*profession*" {

#Attitude Wicked {
	"I carry messages to and from places.",
"I carry messages.  Pardon me, I need to be off. [Leave]"
			}
#Attitude Neutral {
	"I carry messages to and from places in order to further communication.",
"I am a message carrier.  Pardon me, I need to be running. [Leave]"
			}
#Attitude Goodhearted {
	"I carry messages back and forth in order to keep people informed of events and information.",
"I am a message carrier.  Pardon me, I'm running behind schedule. [Leave]"
			}
}

#KEY "*message*" {

#Attitude Wicked {
	"Only the recipient can know the message.",
"I cannot tell thee the contents of the messages I carry."
			}
#Attitude Neutral {
	"I am not allowed to relay the messages to anyone but the proper recipient.",
"I cannot share with thee any messages unless thou art the recipient.  And I know that thou art not."
			}
#Attitude Goodhearted {
	"I am not allowed to relay the messages to anyone but the proper recipient."
		}
}

#KEY "*Cashual*" "*cashual*"  {

#Attitude Wicked {
	"Yes, I carry for the members of the Cashual.  If thou wouldst excuse me, I must be going.[Leave]"
			}
#Attitude Neutral {
	"I sometimes carry for the members of the Cashual.  I must run, now.  Farewell. [Leave]"
			}

#Attitude Goodhearted {
"I often carry messages for the members of the Cashual.  They are very important people who bring water for the crops."
	
			}
}
}

#Sophistication Low {
#KEY "*Runner*", "*job*", "*what*do*do*", "*occupation*", "*profession*" {

#Attitude Wicked {
	"I carry messages around.",
"I carry messages.  Pardon me, I got to go. [Leave]"
			}
#Attitude Neutral {
	"I carry messages all around for people.",
"I am a message carrier.  Pardon me, I gotta be off. [Leave]"
			}
#Attitude Goodhearted {
	"I carry messages back and forth to keep people better informed.",
"I am a message carrier.  Pardon me, I'm running behind. [Leave]"
			}
}

#KEY "*message*" {

#Attitude Wicked {
	"Only the person I take the message to can know what it is.",
"I can't tell what the message is."
			}
#Attitude Neutral {
	"I ain't allowed to give the messages to anyone but the proper person.",
"I can't share with thee any messages unless thou'rt the person the message is for. And I know that thou aren't."
			}
#Attitude Goodhearted {
	"I'm not allowed to give the messages to anyone but the proper people."
		}
}

#KEY "*cashual*" "*Cashual*" {

#Attitude Wicked {
	"Yeah, I carry for the members of the Cashual. If thou would excuse me, I gotta go.[Leave]"
			}
#Attitude Neutral {
	"I sometimes carry for the members of the Cashual. I must run, now. Farewell. [Leave]"
			}

#Attitude Goodhearted {
"I tend to carry messages for the members of the Cashual. They're really important people who bring water for the
crops."
	
			}
}
}
}

