// Nobility function
//
// Keywords:
// noble, nobility, wealthy, blue blood, blue-blood, job
// 
//
//  Detail for 3 attitude and 3 notoriety levels included here. 
// - Dab


#Fragment Britannia, Job, Britannia_Noble {
#Sophistication High {

#KEY "*gold*", "*diamond*", "*wealth*", "*money*", "*importan*" {
       #Notoriety Infamous, Outlaw {
          "'Tis probably not prudent of me to speak of mine wealth and importance in front of thee.",
          "'Twould not be wise to speak of such matters in front of thy kind, tho polite society as foreign as it might be to thee recommends it nonetheless.",
          "'Tis probably not prudent of me to speak of mine wealth and importance before thee. Thou might get ideas."
       }
       #Notoriety Anonymous {
          "Thou wouldst be most impressed by mine family's wealth and influence.",
          "If only thou did know what it is like to have all that thou dost wish...",
          "Yes, I do have many things to be envious of. I lead such a charmed life."
       }
       #Notoriety Known, Famous {
          "I only wear the finest jemstones. Those second-rate stones just won't do.",
          "Ahh, money. The bane of my existance. I desrve so much more than I have. ",
          "Thou wouldst know not what it's like to be as wealthy and important as me. 'Tis a strain. But I hold up well."
       }
    }
    
    #Key "*how are you*", "*how art thee*", "*how art thou*", "What's up*", "*are you well*", "*art thou well*"{
        "Quite well, wouldst thou like to view mine new purchases? My clothes were just tailored this morning. And thy own?",
        "A most glorious accomplishment for mine house. Mine newest frock hath been made by the finest tailors around. The silk was imported. Hast thou been able to purchase any silk lately?",
        "Quite well, what hast thou purchased recently? Wouldst thou like to view mine new clothes that were just tailored this morning?"
    }


#KEY "* Job*" "*What*do*" "what do you do" "*occupation*" "*what is your job*" "*what is thy job*" "*profession*" {
#Attitude Wicked {
	"What nonsense dost thou speaketh of? Imagine me working with mine own hands. $Sirrah/Mistress$ that is what the servants are for -- To manage those sloppy, disgusting mundane people who are forced to do work.",
	"Job?  Oh!  Thou dost jest!  Ahah-hah-hah![Leave]",
	"My bloodline IS my job!"
		}
#Attitude Neutral {
	"Mine servants doth oversee the workers. How couldst thou imagine that I wouldst dirty mine own hands so.",
	"No no no, a job is for those who lack money!  Nobility doesn't take jobs."
		}
#Attitude Goodhearted {
	"What dost thou take me for? Perhaps thou art not used to having servants at thy beck and call to manage those who do thy work for thee. I pity thee.",
	"I come from a noble family, $sir/madam$.  I fear that my taking on work would demean them.",
	"The job of the nobility is to further the arts and set a good example for the common folk."
}
}

#KEY "*Noble*" "*Nobility*" "*wealthy*"  "*blue blood*" "*blue-blood*" {
#Attitude Wicked {
	"Really!  That the Nobility of this fair city must suffer the likes of thee![Leave]",
	"I have much better people to talk with than thee.  Farewell.  Or as well as thou art able![Leave]"
		}
#Attitude Neutral {
	"Look here, 'tis improper to go barking about one's lineage or wealth!",
	"Thou art too lowly to dicuss these things with!  I hope thou dost understand."
		}

#Attitude Goodhearted {
	"Yes, well, my family does go back a ways, I assure thee!"
		}
		}
		}

#Sophistication Medium {

#KEY "*gold*", "*diamond*", "*wealth*", "*money*", "*importan*" {
       #Notoriety Infamous, Outlaw {
          "'Tis probably not prudent of me to speak of mine wealth and importance in front of thee.",
          "'Twould not be wise to speak of such matters in front of thy kind, tho polite society as foreign as it might be to thee recommends it nonetheless.",
          "'Tis probably not prudent of me to speak of mine wealth and importance before thee. Thou might get ideas."
       }
       #Notoriety Anonymous {
          "Thou wouldst be most impressed by mine family's wealth and influence.",
          "Thou wouldst be most impressed by my family's wealth and influence.",
          "Thou art jealous of my family? Well, 'tis only right and proper. Thou were not born noble, after all."
       }
       #Notoriety Known, Famous {
           "I only wear the finest jemstones. Those measly second-rate stones just won't do.",
          "Ahh, money. 'Tis the bane of my existance. I desrve so much more than I currently have. ",
          "Thou wouldst know not what it's like to be as wealthy and important as me. 'Tis a strain. But I hold up well. Shopping helps."
       }
    }
    
    #Key "*how are you*", "*how art thee*", "*how art thou*", "What's up*", "*are you well*", "*art thou well*"{
        "Quite well, wouldst thou like to view mine new purchases? My clothes were just tailored this morning. And thy own?",
        "A most glorious accomplishment for mine house. Mine newest frock hath been made by the finest tailors around. The silk was imported. Hast thou been able to purchase any silk lately?",
        "Quite well, what hast thou purchased recently? Wouldst thou like to view mine new clothes that were just tailored this morning?"
    }


#KEY "* Job*" "*What*do*" "what do you do" "*occupation*" "*what is your job*" "*what is thy job*" "*profession*" {
#Attitude Wicked {
	"What nonsense dost thou speaketh of? Imagine me working with mine own hands. $Sirrah/Mistress$ that is what the servants are for -- To manage those sloppy, disgusting mundane people who are forced to do work.",
	"Job?  Oh!  Thou dost jest!  Ahah-hah-hah![Leave]",
	"My bloodline IS my job!"
		}
#Attitude Neutral {
	"Mine servants doth oversee the workers. How couldst thou imagine that I wouldst dirty mine own hands so.",
	"No no no, a job is for those who lack money!  Nobility doesn't take jobs."
		}
#Attitude Goodhearted {
	"What dost thou take me for? Perhaps thou art not used to having servants at thy beck and call to manage those who do thy work for thee. I pity thee.",
	"I come from a noble family, $sir/madam$.  I fear that my taking on work would demean them.",
	"The job of the nobility is to further the arts and set a good example for the common folk."
}
}

#KEY "*Noble*" "*Nobility*" "*wealthy*"  "*blue blood*" "*blue-blood*" {
#Attitude Wicked {
	"Really!  That the Nobility of this fair city must suffer the likes of thee![Leave]",
	"I have much better people to talk with than thee.  Farewell.  Or as well as thou art able![Leave]"
		}
#Attitude Neutral {
	"Look here, 'tis improper to go barking about one's lineage or wealth!",
	"Thou art too lowly to dicuss these things with!  I hope thou dost understand."
		}

#Attitude Goodhearted {
	"Yes, well, my family does go back a ways, I assure thee!"
		}
		}
		}

#Sophistication Low {

#KEY "*gold*", "*diamond*", "*wealth*", "*money*", "*importan*" {
       #Notoriety Infamous, Outlaw {
          "'Tis rude to speak of my wealth and importance in front of thee.",
          "'Twouldn't be nice to speak of such things in front of thee.",
          "'Tis probably not good of me to speak of my money in front of thee. Thou might get ideas."
       }
       #Notoriety Anonymous {
          "Thou would be impressed by my family's wealth and influence.",
          "Yes, I really must wear only the best jewels. Nothing else flatters me quite as well.",
          "Ahh, 'tis nice to have what eveyone else only dreams of. I deserve it, thou knowest."
       }
       #Notoriety Known, Famous {
          "I only wear the finest jemstones. Those second-rate stones just won't do.",
          "Ahh, money. The bane of my existence. I desrve so much more than I have. ",
          "Thou wouldn't know what it's like to be as wealthy and important as me. 'Tis a strain. But I hold up
well."
       }
    }
    
    #Key "*how are you*", "*how art thee*", "*how art thou*", "What's up*", "*are you well*", "*art thou well*"{
        "I'm doing good, wouldst thou like to view mine new purchases? My clothes were just tailored this morning.
And thy own?",
        "A most glorious accomplishment for me. My newest frock hath been made by the finest tailors around. The
silk was imported. Hast thou been able to purchase any silk lately?",
        "Quite well, what hast thou purchased recently? Wouldst thou like to view mine new clothes that were just
tailored this morning?"
    }


#KEY "* Job*" "*What*do*" "what do you do" "*occupation*" "*what is your job*" "*what is thy job*" "*profession*" {
#Attitude Wicked {
	"What nonsense dost thou speak of? Imagine me working with my own hands. $Sirrah/Mistress$ that is what
the servants are for -- To manage those sloppy, disgusting mundane people who are forced to do work.",
	"Job?  Oh!  Thou dost jest!  Ahah-hah-hah![Leave]",
	"My bloodline IS my job!"
		}
#Attitude Neutral {
	"My servants oversee the workers. How could thou imagine that I would dirty mine own hands so.",
	"No no no, a job is for those who have little money!  Nobility doesn't take jobs."
		}
#Attitude Goodhearted {
	"What do thou take me for? Perhaps thou'rt not used to having servants at thy beck and call to manage those
who do thy work for thee. I pity thee.",
	"I come from a noble family, $sir/madam$.  I think that taking on work would demean them.",
	"The job of the nobility is to further the arts and set a good example for the common folk."
}
}

#KEY "*Noble*" "*Nobility*" "*wealthy*"  "*blue blood*" "*blue-blood*" {
#Attitude Wicked {
	"Really!  That the Nobility of this fair city must suffer the likes of thee![Leave]",
	"I got better people to talk with than thee.  Farewell.  Or as well as thou art able![Leave]"
		}
#Attitude Neutral {
	"Look here, 'tis wrong to go barking about one's lineage or wealth!",
	"Thou art too lowly to dicuss these things with!  I hope thou understands."
		}

#Attitude Goodhearted {
	"Yes, well, my family does go back a ways, I assure thee!"
		}
		}
		}
		}

