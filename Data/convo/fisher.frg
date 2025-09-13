// Fisher function
//
// Keywords:
//fish, fisherman, nets, fishermen, job
//
//catch, seas, food
// 
// - Dab


#Fragment Britannia, Job, Britannia_Fisher {
#Sophistication High {

#KEY "*job*"  "*what*do*do*" "*occupation*" "*profession*"   {
#Attitude Wicked {
	"I am a fisher of the seas.",
"I am a fisher.",
"I catch fish."
}
#Attitude Neutral {
	"I am a fisherman.",
"I've been a fisher all my life.",
"I catch fish."
}
#Attitude Goodhearted {
	"I am a fisherman, $milord/milady$.",
"I've spent my years fishing the seas of Britannia.",
"I catch fish, $milord/milady$."
}
}
#KEY "*food*" {
#Attitude Wicked {
	"I can sell thee fish. That is all.",
"If thou dost want fish, then I could help thee. Otherwise go away.",
"I sell fish."
}
#Attitude Neutral {
	"I have some fresh fish for thee, if thou art interested.",
"I've been selling fish to people just like thee for years. But if it's not fish that thou dost want... I can't help thee.",
"I sell fresh fish."
}
#Attitude Goodhearted {
	"I am a fisherman, $milord/milady$. I can certainly sell thee good fish to eat.",
"I've spent my years fishing the seas of Britannia. If I can't supply thee with fresh fish to eat, no one can, $milord/milady$.",
"I can sell thee a variety of fish to eat, $milord/milady$."
}
}
#KEY "*catch*" "*sea*" "*river*" {
#Attitude Wicked {
	"I fish the seas. Not enough fish in the rivers to make a living.",
"The open seas are all I need to make a living. Could never do anything else, really.",
"I catch fish on the open seas of Britannia."
}
#Attitude Neutral {
	"I never fish in the rivers.  Not enough there for my effort.",
"I've only fished in the seas. The best catches are usually there.",
"I catch fish in the seas, not in the rivers."
}
#Attitude Goodhearted {
	"I wake up before dawn to fish the seas. A wonderful life I have, I know it.",
"I spend my days catching the fish offshore and my nights relaxing. I'm a happy man.",
"I enjoy the time I spend riding the waves offshore, waiting to haul in the newest catch."
}
}

#KEY "*skill*" {    
	#Attitude Wicked {
	"I could certainly show thee how to fish. It doesn't look like thou dost have the patience for it, though.",
"Catching fish is all a matter of knowing when and where to cast the nets. I might be able to show thee such things, but then thou wouldst have to remember it. ",
"I guess I could teach thee a few things that I know about casting nets. I'd expect payment for my time, of course."
			}
	#Attitude Neutral {
	"I could teach thee how best to cast thy nets or line, if thou art interested. That would help with thy catching of fish.",
"If thou dost need some training in the fishing, just say the word. I shall try to help thee.",
"I can give thee some fishing tips, all thou dost need do is to ask.  And I do expect to be paid for my time."
			}
	#Attitude Goodhearted {
	"I'd be happy to help teach thee to fish. I need a few coins, though, to pay for my time.",
"Sure, I could be convinced to teach thee some of what I know.  'Twould be up to thyself, though, to work at it.",
"I can teach thee what I know. I need thee to pay me for the time I would invest."
			}
		}
#KEY "*fish *" "*fish?*" "*fish.*" "*fish!*" {
#Attitude Wicked {
	"Fish - what?  Where?  I need to catch 'em.  [Leave]",
"Fish.  Oooohh what a smell!"
		}
#Attitude Neutral {
	"Fish?  Suddenly my appetite's gone away.",
	"Fish?  Buy it at the tavern."
		}
#Attitude Goodhearted {
	"Ahh, fish.  The food of the gods!",
	"The best food in the world!"
		}
}

#KEY "*Fisherman*"  "*Fishermen*" "*Fisher*" {
#Attitude Wicked {
	"I've spent my years fishing the seas of Britannia.",
"I would be one those.  A fisherman, I mean."
		}
#Attitude Neutral {
	"And don't forget the fisherwomen!",
	"Yes, I am a fisherman."
		}
#Attitude Goodhearted {
	"I would be one of those.  A fisherman, I mean.",
"I'm a fisher and I'm bloody happy about it.",
"'Course, I'm a fisherman.  Canst thou not smell me?  Heh heh."
		}
}

#KEY "*nets*"  {
#Attitude Wicked {
	"Oh, that reminds me... I need to repair my nets.[Leave]"
}
#Attitude Neutral {
	"Oh, that reminds me... I need to find someone to repair my nets.",
"My nets are in poor repair."
		}
#Attitude Goodhearted {
	"Oh, that reminds me... I need to buy new nets."
		}
}
}

#Sophistication Medium {

#KEY "*job*"  "*what*do*do*" "*occupation*" "*profession*"   {
#Attitude Wicked {
	"I am a fisher of the seas.",
"I am a fisher#man/woman#.",
"I catch fish."
}
#Attitude Neutral {
	"I am a fisher#man/woman#.",
"I've been a fisher#man/woman# all my life.",
"I catch fish."
}
#Attitude Goodhearted {
	"I am a fisher#man/woman#, $milord/milady$.",
"I've spent my years fishing the seas of Britannia.",
"I catch fish, $milord/milady$."
}
}

#KEY "*fish *" "*fish?*" "*fish.*" "*fish!*" {
#Attitude Wicked {
	"Fish - what?  Where?  I need to catch 'em.  [Leave]",
"Fish.  Oooohh what a smell!"
		}
#Attitude Neutral {
	"Fish?  Suddenly my appetite's gone away.",
	"Fish?  Buy it at the tavern."
		}
#Attitude Goodhearted {
	"Ahh, fish.  The food of the gods!",
	"The best food in the world!"
		}
}
#KEY "*food*" {
#Attitude Wicked {
	"I can sell thee fish. That is all.",
"If thou dost want fish, then I could help thee. Otherwise go away.",
"I sell fish."
}
#Attitude Neutral {
	"I have some fresh fish for thee, if thou art interested.",
"I've been selling fish to people just like thee for years. But if it's not fish that thou dost want... I can't help thee.",
"I sell fresh fish."
}
#Attitude Goodhearted {
	"I am a fisherman, $milord/milady$. I can certainly sell thee good fish to eat.",
"I've spent my years fishing the seas of Britannia. If I can't supply thee with fresh fish to eat, no one can, $milord/milady$.",
"I can sell thee a variety of fish to eat, $milord/milady$."
}
}
#KEY "*catch*" "*sea*" "*river*" {
#Attitude Wicked {
	"I fish the seas. Not enough fish in the rivers to make a living.",
"The open seas are all I need to make a living. Could never do anything else, really.",
"I catch fish on the open seas of Britannia."
}
#Attitude Neutral {
	"I never fish in the rivers.  Not enough there for my effort.",
"I've only fished in the seas. The best catches are usually there.",
"I catch fish in the seas, not in the rivers."
}
#Attitude Goodhearted {
	"I wake up before dawn to fish the seas. A wonderful life I have, I know it.",
"I spend my days catching the fish offshore and my nights relaxing. I'm a happy man.",
"I enjoy the time I spend riding the waves offshore, waiting to haul in the newest catch."
}
}
#KEY "*skill*" {    
	#Attitude Wicked {
	"I could certainly show thee how to fish. It doesn't look like thou dost have the patience for it, though.",
"Catching fish is all a matter of knowing when and where to cast the nets. I might be able to show thee such things, but then thou wouldst have to remember it. ",
"I guess I could teach thee a few things that I know about casting nets. I'd expect payment for my time, of course."
			}
	#Attitude Neutral {
	"I could teach thee how best to cast thy nets or line, if thou art interested. That would help with thy catching of fish.",
"If thou dost need some training in the fishing, just say the word. I shall try to help thee.",
"I can give thee some fishing tips, all thou dost need do is to ask.  And I do expect to be paid for my time."
			}
	#Attitude Goodhearted {
	"I'd be happy to help teach thee to fish. I need a few coins, though, to pay for my time.",
"Sure, I could be convinced to teach thee some of what I know.  'Twould be up to thyself, though, to work at it.",
"I can teach thee what I know. I need thee to pay me for the time I would invest."
			}
		}
#KEY "*Fisherman*"  "*Fishermen*" "*Fisher*" {
#Attitude Wicked {
	"I've spent my years fishing the seas of Britannia.",
"I would be one those.  A fisher#man/woman#, I mean."
		}
#Attitude Neutral {
	"And don't forget the fisherwomen!",
	"Yes, I am a fisher#man/woman#."
		}
#Attitude Goodhearted {
	"I would be one of those.  A fisher#man/woman#, I mean.",
"I'm a fisher and I'm bloody happy about it.",
"'Course, I'm a fisherman#man/woman#.  Canst thou not smell me?  Heh heh."
		}
}

#KEY "*nets*"  {
#Attitude Wicked {
	"Oh, that reminds me... I need to repair my nets.[Leave]"
}
#Attitude Neutral {
	"Oh, that reminds me... I need to find someone to repair my nets.",
"My nets are in poor repair."
		}
#Attitude Goodhearted {
	"Oh, that reminds me... I need to buy new nets."
		}
}
}

#Sophistication Low {

#KEY "*job*"  "*what*do*do*" "*occupation*" "*profession*"   {
#Attitude Wicked {
	"I'm a fisher of the seas.",
"I'm a fisher#man/woman#.",
"I catch fish."
}
#Attitude Neutral {
	"I'm a fisher#man/woman#.",
"I been a fisher#man/woman# all my life.",
"I catch fish."
}
#Attitude Goodhearted {
	"I'm a fisher#man/woman#, $milord/milady$.",
"I spent my years fishin' the seas of Britannia.",
"I catch fish, $milord/milady$."
}
}

#KEY "*fish *" "*fish?*" "*fish.*" "*fish!*" {
#Attitude Wicked {
	"Fish - what?  Where?  I need to catch 'em.  [Leave]",
"Fish.  Oooohh what a smell!"
		}
#Attitude Neutral {
	"Fish?  Hmmm...Now my appetite's gone away.",
	"Fish?  Buy it at the tavern."
		}
#Attitude Goodhearted {
	"Ahh, fish.  Food o' the gods!",
	"Best food in the world, fish is!"
		}
}
#KEY "*food*" {
#Attitude Wicked {
	"I can sell thee fish. That's all.",
"If thou wants fish, I can help. Otherwise go away.",
"I sell fish."
}
#Attitude Neutral {
	"I got some fresh fish for thee, if thou'rt interested.",
"I been sellin' fish for years. But if thou ain't wantin' fish... I can't help.",
"I sell fresh fish."
}
#Attitude Goodhearted {
	"Been a fisherman for years. I can probably sell some good fish to eat.",
"I spent my years fishin'. If I ain't able to supply thee with fresh fish, ain't no one can.",
"I can sell thee all kinds o' fish to eat."
}
}
#KEY "*catch*" "*sea*" "*river*" {
#Attitude Wicked {
	"I fish the seas. Ain't enough fish in the rivers to make a living.",
"The open seas are all I need to make a living. Couldn't never do nothin' else, really.",
"I catch fish out on the seas of Britannia."
}
#Attitude Neutral {
	"I ain't never fished in the rivers.  Not enough there.",
"I only fish in the seas. The biggest fish are there.",
"I catch in the seas, not the rivers."
}
#Attitude Goodhearted {
	"I wake up 'fore dawn to fish. Wonderful life I got, I know.",
"I spend the days catchin' fish and the nights relaxin'. I'm a happy man.",
"I like ridin' the waves offshore, waitin' to haul in the newest catch."
}
}
#KEY "*skill*" {    
	#Attitude Wicked {
	"Want to learn to fish? Don't look like thou got the patience for it.",
"Fishin's a simple thing. I could teach thee some tricks, though.",
"I could teach thee to catch fish better."
			}
	#Attitude Neutral {
	"I could teach thee how to cast thy nets or line, if thou'rt interested. That'd help with thy fishing.",
"If thou needs to be taught to fish, jus' say the word.",
"I can give fishin' lessons, just ask. And gimme some coins."
			}
	#Attitude Goodhearted {
	"I'd be happy to help teach thee to fish. I'd need a few coins, though.",
"Sure, I could teach thee what I know. 'Twould be thy own fault, though, if thou didn't work at it.",
"I can teach thee to fish. I need thee to pay me for my time."
			}
		}
#KEY "*Fisherman*"  "*Fishermen*" "*Fisher*" {
#Attitude Wicked {
	"I spent my years fishin' the seas o' Britannia.",
"I'd be one o' those. A fisher#man/woman#, I mean."
		}
#Attitude Neutral {
	"And don't forget the fisherwomen!",
	"Yes, I'm a fisher#man/woman#."
		}
#Attitude Goodhearted {
	"I'd be one o' those. A fisher#man/woman#, I mean.",
"I'm a fisher#man/woman# and I'm bloody happy about it.",
"'Course, I'm a fisher#man/woman#. Can thou smell me?  He he."
		}
}

#KEY "*nets*"  {
#Attitude Wicked {
	"Oh, that reminds me... I gotta repair my nets.[Leave]"
}
#Attitude Neutral {
	"Oh, that reminds me... I gotta find someone to repair my nets.",
"My nets are in poor repair."
		}
#Attitude Goodhearted {
	"Oh, that reminds me... I gotta buy new nets."
		}
}
}
}



