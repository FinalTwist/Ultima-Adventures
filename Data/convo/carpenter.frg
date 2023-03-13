// Carpenter function
//
// Keywords:
// job, carpent*, wood, tool, hammer, nail, saw, lumber, plane, chisel


//
//
// 
// - cwm


#Fragment Britannia, Job, Britannia_Carpenter {
#Sophistication High {
#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" {
#Attitude Wicked {
"I'm a carpenter.",
"I'm a carpenter and woodcarver."
		}
#Attitude Neutral {
"I'm the carpenter here.",
"I'm do fine carpentry and woodworking."
		}
#Attitude Goodhearted {
"I'm a carpenter by trade.",
"Carpentry and woodcarving are my art and my livelihood."
		}
		}
#KEY "*skill*" {    
	#Attitude Wicked {
	"What, dost thou want to learn some wood working? I'm not sure that thou art trainable.",
"Thou dost need some skill to craft anything out of wood. I might be able to teach thee a thing or two, but I can't promise anything.",
"Well, for a little compensation, I could show thee some things to practice to improve thy skill in wood working."
			}
	#Attitude Neutral {
	"I've been known to teach some how to improve their skills, for a few coins.",
"If thou dost need some training in carpentry, just say the word.",
"I can give thee some practice in carpentry, all thou dost need do is to ask.  And leave me a few coins."
			}
	#Attitude Goodhearted {
	"I'd be happy to help train thee in some wood working techniques. I'd ask for a few coins, though, to help cover my time.",
"I'd be honored to teach thee some of what I know.  'Twould be up to thyself, though, to practice it.",
"I can teach thee only if thou art willing to learn. And pay me for the time I would invest. A few coins would suffice."
			}
		}
#KEY "*carpent*" "*woodcarving*"  "*joining*" {
#Attitude Wicked { 
"I make furniture and do woodworking for houses.",
"I do carpentry of all kinds."
		}
#Attitude Neutral {
"I both make furniture and do woodwork for houses.",
"If it's done with wood, I can do it."
		}
#Attitude Goodhearted {
"The wonderful thing about carpentry is watching thy thoughts take solid shape in thy hand.",
"I have mastered all manifestations of the arts of woodcarving and joining."
		}
		}
#KEY "*wood*", "*lumber*" {
#Attitude Wicked {
	"I buy and sell loose lumber."
		}
#Attitude Neutral {
	"I buy and sell loose lumber."
		}
#Attitude Goodhearted {
	"I buy and sell loose lumber."
		}
		}
#KEY "*tool*" "*hammer*" "*nail*" "*saw*" "*chisel*" "*plane*" {
#Attitude Wicked {
"I have some extra tools I could sell. Tools, skill and wood are all that it takes to be thine own carpenter.",
"If it's woodworking tools thou dost need, I might have some available."
		}
#Attitude Neutral {
	"I wouldn't mind selling some of my old tools. Add to them the right skill, and thou couldst do thine own woodworking ",
"I have a fine set of tools I'd like to sell."
		}
#Attitude Goodhearted {
"I'm willing to part with some of my extra tools. With the right skill, thou couldst do thine own woodworking.",
"I can provide thee with the tools of my trade."
		}
		}		
}
#Sophistication Medium {
#KEY "*job*", "*what*do*do*", "*occupation*", "*profession*" {
#Attitude Wicked {
"I'm a carpenter.",
"I'm a carpenter and woodcarver."
		}
#Attitude Neutral {
"I'm the carpenter here.",
"I'm do fine carpentry and woodworking."
		}
#Attitude Goodhearted {
"I'm a carpenter by trade.",
"Carpentry and woodcarving are my art and my livelihood."
		}
		}
#KEY "*skill*" {    
	#Attitude Wicked {
	"What, dost thou want to learn some wood working? I'm not sure that thou art trainable.",
"Thou dost need some skill to craft anything out of wood. I might be able to teach thee a thing or two, but I can't promise anything.",
"Well, for a little compensation, I could show thee some things to practice to improve thy skill in wood working."
			}
	#Attitude Neutral {
	"I've been known to teach some how to improve their skills, for a few coins.",
"If thou dost need some training in carpentry, just say the word.",
"I can give thee some practice in carpentry, all thou dost need do is to ask.  And leave me a few coins."
			}
	#Attitude Goodhearted {
	"I'd be happy to help train thee in some wood working techniques. I'd ask for a few coins, though, to help cover my time.",
"I'd be honored to teach thee some of what I know.  'Twould be up to thyself, though, to practice it.",
"I can teach thee only if thou art willing to learn. And pay me for the time I would invest. A few coins would suffice."
			}
		}
#KEY "*carpent*" "*woodcarving*"  "*joining*" {
#Attitude Wicked { 
"I make furniture and do woodworking for houses.",
"I do carpentry of all kinds."
		}
#Attitude Neutral {
"I both make furniture and do woodwork for houses.",
"If it's done with wood, I can do it."
		}
#Attitude Goodhearted {
"The wonderful thing about carpentry is watching thy thoughts take solid shape in thy hand.",
"I have mastered all manifestations of the arts of woodcarving and joining."
		}
		}
#KEY "*wood*", "*lumber*" {
#Attitude Wicked {
	"I buy and sell loose lumber."
		}
#Attitude Neutral {
	"I buy and sell loose lumber."
		}
#Attitude Goodhearted {
	"I buy and sell loose lumber."
		}
		}
#KEY "*tool*" "*hammer*" "*nail*" "*saw*" "*chisel*" "*plane*" {
#Attitude Wicked {
"I have some extra tools I could sell.",
"If it's woodworking tools thou need, I might have some available. Thou couldst improve thy own skills with them."
		}
#Attitude Neutral {
	"I wouldn't mind selling some of my old tools.",
"I have a fine set of tools I'd like to sell. Become thine own carpenter.  Al it takes is some skill.  Thou couldst practice and improve that."
		}
#Attitude Goodhearted {
"I'm willing to part with some of my extra tools.",
"I can provide thee with the tools of my trade. The skill comes with practice."
		}
		}		
}
#Sophistication Low {
#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" {
#Attitude Wicked {
"I'm a carpenter.",
"I'm a carpenter and woodcarver."
		}
#Attitude Neutral {
"I'm the carpenter here.",
"I'm do fine carpentry and woodworking."
		}
#Attitude Goodhearted {
"I'm a carpenter by trade.",
"Carpentry and woodcarving are my livelihood."
		}
		}
#KEY "*skill*" {    
	#Attitude Wicked {
	"What, dost thou want to learn to work wood? I'm not sure that thou art able.",
"Thou dost need skill to make anything out of wood. I might be able to teach thee some, but I ain't promisin' nothin'.",
"Well, for a little money, I could show thee some things to practice. But the more thou dost practice, the better thou'lt be."
			}
	#Attitude Neutral {
	"I've been known to teach some how to improve their skills, for a few coins.",
"If thou dost need some leanin' in carpentry, just say so.",
"I can give thee some practice in carpentry, just ask.  And gimme some coins."
			}
	#Attitude Goodhearted {
	"I could help train thee in some wood working techniques. I'd need some money, though.",
"I'd like to show thee what I can do.  'Tis thy own problem, though, to practice it.",
"Sure, I can teach. Thou needs to pay me for my time. A few coins would be fine."
			}
		}
#KEY "*carpent*" "*woodcarving*"  "*joining*" {
#Attitude Wicked { 
"I make furniture and do woodworking for houses.",
"I do carpentry of all kinds."
		}
#Attitude Neutral {
"I both make furniture and do woodwork for houses.",
"If it's done with wood, I can do it."
		}
#Attitude Goodhearted {
"The wonderful thing about carpentry is watching thy thoughts take solid shape in thy hand.",
"I have mastered all manifestations of the arts of woodcarving and joining."
		}
		}
#KEY "*wood*", "*lumber*" {
#Attitude Wicked {
	"I buy and sell loose lumber."
		}
#Attitude Neutral {
	"I buy and sell loose lumber."
		}
#Attitude Goodhearted {
	"I buy and sell loose lumber."
		}
		}
#KEY "*tool*" "*hammer*" "*nail*" "*saw*" "*chisel*" "*plane*" {
#Attitude Wicked {
"I may have some extra tools I could sell. Mayhaps thou couldst buy the skill to use 'em.  Heh heh.",
"If it's woodworking tools thou need, I might have some available."
		}
#Attitude Neutral {
	"I wouldn't mind selling some of my old tools.",
"I have a fine set of tools I'd like to sell. The skill to use 'em is thy own problem."
		}
#Attitude Goodhearted {
"I'm willing to part with some of my extra tools. Of course some skill is needed to use 'em.",
"I can provide thee with the tools of my trade."
		}
		}		
}
		}
