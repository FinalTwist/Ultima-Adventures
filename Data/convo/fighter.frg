// Fighter function
//
// Keywords:
//
//job, what...do, trouble, idiot, stupid, moron
//provisions, weapons
// 
// - Dab


#Fragment Britannia, Job, Britannia_Fighter {
#Sophistication High {
#KEY "*Provisions*"{
#Attitude Wicked {
"Thou canst buy equipment from a provisioner.",
"Get thy provisions from a provisioner, fool.",
"'Tis not my job to hold thy hand, $little boy/little girl$."
}
#Attitude Neutral {
"Thou canst find provisions at a provisioner's shop.",
"I would think to look to a provisioner for that.",
"Most provisioner's shops would probably carry provisions.  Don't hold me to it, though.  'Tis just a guess."
}
#Attitude Goodhearted {
"Thou canst find provisions at a provisioner's shop, $milord/milady$.",
"I would think, $milord/milady$, that a provisioner could help thee with that.",
"Most provisioner's shops would probably carry what thou dost need."
}
}
#KEY "*weapon*" "*armor*" "*bardiche*" "*scimitar*" "*axe*" "* mace*" "*bardiche*" "*fork*" "*spear*" "*staff*" "*sword*" "*maul*" "*meat cleaver*" "*halberd*" "*kryss*" "*kitana*" "*cutlass*" "*crossbow*" "*club*" "*bullwhip*" "*bow*" "*plate*" "*padded*
" "*chain*" "*ring*" "*gorget*" "*bucket helm*" "*norse helm*" "*open faced helm*" "*shield*" "*buckler*" "*heater*" "*helm*" "*gauntlet*" {
#Attitude Wicked {
"Thou canst buy weapons from a weaponsmith and armor from an armourer!  Art thou thick of the head?",
"I would think that a weaponsmith would sell weapons.  Wouldn't thou?  Or possibly even a blacksmith.  An armourer would carry armor.",
"I do so hate to point out the obvious.  Try a weaponsmith.  And possibly a blacksmith."
}
#Attitude Neutral {
"Weapons can be gained from a weaponsmith or a blacksmith.  Armor from an armourer or a blacksmith.",
"I would think that a weaponsmith would sell weapons and an armorer armor.  Wouldn't thou?",
"I've taken my weapons and armor from the hands of enemies I have slain!"
}
#Attitude Goodhearted {
"Weapons can be gained from a weaponsmith or a blacksmith, $milord/milady$.  Armor from an armourer.",
"Look to a blacksmith or a weaponsmith for thy needs.",
"Find the blacksmith's shop and thou shouldst find weapons and armor."
}
}
#KEY "*thou*idiot*" "*you*idiot*"  "*thou*stupid*" "*you*stupid*" "*thou*moron*" "*you*moron*" {
#Attitude Wicked {
"What didst thou call me?",
"Thou didst dare to call me what?"
}
#Attitude Neutral {
"What didst thou call me?",
"Thou didst dare to call me what?"
}
#Attitude Goodhearted {
"Thou didst call me what?",
"Excuse me?",
"Thou didst dare to call me what?"
}
}
#KEY "*idiot*" "*stupid*" "*moron*" {
#Attitude Wicked {
"Thou dost say such things to me?  Have at thee! [Attack]",
"I shall annihilate thee![Attack]",
"Cursed dog![Attack]"
}
#Attitude Neutral {
"Thou shouldst be more careful of what thou dost say to me!",
"Watch thy manners, foolish one!"
}
#Attitude Goodhearted {
"Thy words do not match thy appearance, $milord/milady$!",
"Careful, $milord/milady$, lest my anger is aroused!"
}
}
#KEY "*job*" {
#Attitude Wicked {
"Huh!  I should tell THEE anything!",
"Slaying thee would be preferable to listening to thy blatherings!",
"Remove thy stinking carcass from my presence, lest I destroy thee utterly!"
}
#Attitude Neutral {
"I fight!",
"I kill!",
"I maim!",
"'Tis not for thee to know what I do, $little man/little lady$.",
"I do think that a man's affairs are his own business!"
}
#Attitude Goodhearted {
"I am paid to fight!",
"I kill!  For the right cause, that is.  And usually not humans.",
"I maim!  But only when I'm short of money.  Otherwise I'm a gardener.",
"'Tis not for thee to know what I do, $milord/milady$.",
"I do what I must to survive!",
"I fight to survive!  And then I fight some more!"
}
}
#KEY "*skill*" "*fight*" "*kill*" "*maim*" {    
	#Attitude Wicked {
	"Dost thou need to improve thy parry? Sure, I can help even a $scruff/girlie$ like thee.",
"Thou dost need some skill to live in these dangerous times. I might be able to teach thee a trick or two, but I can't promise that it would keep thee alive.",
"Well, for a little compensation, I could show thee how to improve thy skills and keep from getting killed."
			}
	#Attitude Neutral {
	"I've been known to teach a select few how to improve their fighting, for a few coins.",
"If thou dost need some help with thy fighting, just say the word. I shall try to help thee.",
"I can give thee some fighting tips, thou just need ask.  And leave me a few coins."
			}
	#Attitude Goodhearted {
	"I'd be happy to help train thy fighting abilities. I need a few coins, though. Can't sell my abilities for free, thou knowest.",
"Sure, I could be talked into teaching thee some of what I know.  'Twould be up to thyself, though, to practice it.",
"I can teach thee only if thou art willing to learn. Thou also must pay me for my time. A few coins, no more."
			}
		}
#KEY "*what*do*do*", "*profession*", "*occupation*"  {
#Attitude Wicked {
	"I fight!",
"I kill!",
"I maim!"
		}
#Attitude Neutral {
	"I travel.",
	"I fight.  A lot.",
	"I am sometimes paid to fight."
		}
#Attitude Goodhearted {
	"I travel.  With weapons.  And use them.",
	"I fight.  A lot.",
	"I am paid to fight battles."

		}
}
#KEY "*trouble*"  {
#Attitude Wicked {
	"Trouble.  'Tis the word I live for!",
	"Trouble?  Thou art causing trouble!  Cease, or I shall be obliged to smite thee into oblivion!"

		}
#Attitude Neutral {
	"Trouble?  Where?  I'll give them some trouble!",
	"There will be no trouble while I am around!  Unless, of course, I am the cause!"
}
#Attitude Goodhearted {
	"No one would dare cause trouble while I am about!",
	"I care not for thy trouble!"
		}
}
}

#Sophistication Medium {
#KEY "*Provisions*"{
#Attitude Wicked {
"Thou canst buy equipment from a provisioner.",
"Get thy provisions from a provisioner, fool.",
"'Tis not my job to hold thy hand, $little boy/little girl$."
}
#Attitude Neutral {
"Thou canst find provisions at a provisioner's shop.",
"I would think to look to a provisioner for that.",
"Most provisioner's shops would probably carry provisions.  Don't hold me to it, though.  'Tis just a guess."
}
#Attitude Goodhearted {
"Thou canst find provisions at a provisioner's shop, $milord/milady$.",
"I would think, $milord/milady$, that a provisioner could help thee with that.",
"Most provisioner's shops would probably carry what thou dost need."
}
}
#KEY "*weapon*" "*armor*" "*bardiche*" "*scimitar*" "*axe*" "* mace*" "*bardiche*" "*fork*" "*spear*" "*staff*" "*sword*" "*maul*" "*meat cleaver*" "*halberd*" "*kryss*" "*kitana*" "*cutlass*" "*crossbow*" "*club*" "*bullwhip*" "*bow*" "*plate*" "*padded*
" "*chain*" "*ring*" "*gorget*" "*bucket helm*" "*norse helm*" "*open faced helm*" "*shield*" "*buckler*" "*heater*" "*helm*" "*gauntlet*" {
#Attitude Wicked {
"Thou canst buy weapons from a weaponsmith and armor from an armourer!  Art thou thick of the head?",
"I would think that a weaponsmith would sell weapons.  Wouldn't thou?  Or possibly even a blacksmith.  An armourer would carry armor.",
"I do so hate to point out the obvious.  Try a weaponsmith.  And possibly a blacksmith."
}
#Attitude Neutral {
"Weapons can be gained from a weaponsmith or a blacksmith.  Armor from an armourer or a blacksmith.",
"I would think that a weaponsmith would sell weapons and an armorer armor.  Wouldn't thou?",
"I've taken my weapons and armor from the hands of enemies I have slain!"
}
#Attitude Goodhearted { 
"Weapons can be gained from a weaponsmith or a blacksmith, $milord/milady$.  Armor from an armourer.",
"Look to a blacksmith or a weaponsmith for thy needs.",
"Find the blacksmith's shop and thou shouldst find weapons and armor."
}
}
#KEY "*thou*idiot*" "*you*idiot*"  "*thou*stupid*" "*you*stupid*" "*thou*moron*" "*you*moron*" {
#Attitude Wicked {
"What didst thou call me?",
"Thou didst dare to call me what?"
}
#Attitude Neutral {
"What didst thou call me?",
"Thou didst dare to call me what?"
}
#Attitude Goodhearted {
"Thou didst call me what?",
"Excuse me?",
"Thou didst dare to call me what?"
}
}
#KEY "*idiot*" "*stupid*" "*moron*" {
#Attitude Wicked {
"Thou dost say such things to me?  Have at thee! [Attack]",
"I shall annihilate thee![Attack]",
"Cursed dog![Attack]"
}
#Attitude Neutral {
"Thou shouldst be more careful of what thou dost say to me!",
"Watch thy manners, foolish one!"
}
#Attitude Goodhearted {
"Thy words do not match thy appearance, $milord/milady$!",
"Careful, $milord/milady$, lest my anger is aroused!"
}
}
#KEY "*job*" {
#Attitude Wicked {
"Huh!  I should tell THEE anything!",
"Slaying thee would be preferable to listening to thy blatherings!",
"Remove thy stinking carcass from my presence, lest I destroy thee utterly!"
}
#Attitude Neutral {
"I fight!",
"I kill!",
"I maim!",
"'Tis not for thee to know what I do, $little man/little lady$.",
"I do think that a man's affairs are his own business!"
}
#Attitude Goodhearted {
"I am paid to fight!",
"I kill!  For the right cause, that is.  And usually not humans.",
"I maim!  But only when I'm short of money.  Otherwise I'm a gardener.",
"'Tis not for thee to know what I do, $milord/milady$.",
"I do what I must to survive!",
"I fight to survive!  And then I fight some more!"
}
}
#KEY "*skill*" "*fight*" "*kill*" "*maim*" {
	#Attitude Wicked {
	"Dost thou need to improve thy parry? Sure, I can help even a $scruff/girlie$ like thee.",
"Thou dost need some skill to live in these dangerous times. I might be able to teach thee a trick or two, but I can't promise that it would keep thee alive.",
"Well, for a little compensation, I could show thee how to improve thy skills and keep from getting killed."
			}
	#Attitude Neutral {
	"I've been known to teach a select few how to improve their fighting, for a few coins.",
"If thou dost need some help with thy fighting, just say the word. I shall try to help thee.",
"I can give thee some fighting tips, thou just need ask.  And leave me a few coins."
			}
	#Attitude Goodhearted {
	"I'd be happy to help train thy fighting abilities. I need a few coins, though. Can't sell my abilities for free, thou knowest.",
"Sure, I could be talked into teaching thee some of what I know.  'Twould be up to thyself, though, to practice it.",
"I can teach thee only if thou art willing to learn. Thou also must pay me for my time. A few coins, no more."
			}
		}
#KEY "*what*do*do*", "*profession*", "*occupation*"  {
#Attitude Wicked {
	"I fight!",
"I kill!",
"I maim!"
		}
#Attitude Neutral {
	"I travel.",
	"I fight.  A lot.",
	"I am sometimes paid to fight."
		}
#Attitude Goodhearted {
	"I travel.  With weapons.  And use them.",
	"I fight.  A lot.",
	"I am paid to fight battles."

		}
}
#KEY "*trouble*"  {
#Attitude Wicked {
	"Trouble.  'Tis the word I live for!",
	"Trouble?  Thou art causing trouble!  Cease, or I shall be obliged to smite thee into oblivion!"

		}
#Attitude Neutral {
	"Trouble?  Where?  I'll give them some trouble!",
	"There will be no trouble while I am around!  Unless, of course, I am the cause!"
}
#Attitude Goodhearted {
	"No one would dare cause trouble while I am about!",
	"I care not for thy trouble!"
		}
} 
}
#Sophistication Low {
#KEY "*Provisions*"{
#Attitude Wicked {
"Thou can buy stuff from a provisioner.",
"Get provisions from a provisioner, fool.",
"Ain't my job to hold thy hand, $little boy/little girl$."
}
#Attitude Neutral {
"Thou can find some stuff at a provisioner's shop.",
"I'd think to look for a provisioner for that.",
"Most provisioner's shops would probably carry provisions. Don't be holdin' me to it, though. Just a guess."
}
#Attitude Goodhearted {
"Thou can find provisions at a provisioner's shop, $milord/milady$.",
"I'd think, $milord/milady$, that a provisioner could help with that.",
"Most provisioner's shops would probably carry what thou need."
}
}
#KEY "*weapon*" "*armor*" "*bardiche*" "*scimitar*" "*axe*" "* mace*" "*bardiche*" "*fork*" "*spear*" "*staff*" "*sword*" "*maul*" "*meat cleaver*" "*halberd*" "*kryss*" "*kitana*" "*cutlass*" "*crossbow*" "*club*" "*bullwhip*" "*bow*" "*plate*" "*padded*" "*chain*" "*ring*" "*gorget*" "*bucket helm*" "*norse helm*" "*open faced helm*" "*shield*" "*buckler*" "*heater*" "*helm*" "*gauntlet*" {
#Attitude Wicked {
"Thou can buy weapons from a weaponsmith and armor from an armourer!  Art thou thick o' the head?",
"I'd think that a weaponsmith would sell weapons.  Wouldn't thou?  Or a blacksmith.  An armourer would carry armor.",
"Try a weaponsmith, fool.  And possibly a blacksmith."
}
#Attitude Neutral {
"Weapons can be gotten from a weaponsmith or a blacksmith.  Armor from an armourer or a blacksmith.",
"I'd think a weaponsmith would sell weapons and an armorer, armor.  Wouldn't thou?",
"I got my weapons and armor from enemies I've killed!"
}
#Attitude Goodhearted {
"Weapons are gotten from a weaponsmith or a blacksmith, $milord/milady$.  Armor from an armourer.",
"Look for a blacksmith or a weaponsmith.",
"Find the blacksmith's shop and thou'rt gonna find weapons and armor."
}
}
#KEY "*thou*idiot*" "*you*idiot*"  "*thou*stupid*" "*you*stupid*" "*thou*moron*" "*you*moron*" {
#Attitude Wicked {
"What did thou call me?",
"Thou called me what?"
}
#Attitude Neutral {
"What did thou call me?",
"Thou called me what?"
}
#Attitude Goodhearted {
"Thou called me what?",
"'Scuse me?",
"Thou called me a what?"
}
}
#KEY "*idiot*" "*stupid*" "*moron*" {
#Attitude Wicked {
"Thou say them things to me?  Have at thee! [Attack]",
"I'll destroy thee completely![Attack]",
"Cursed dog![Attack]"
}
#Attitude Neutral {
"Thou should be more careful of what thou says to me!",
"Watch thy manners, idiot!"
}
#Attitude Goodhearted {
"Thy words don't match thy appearance, $milord/milady$!",
"Careful, $milord/milady$, or I'll get mad!"
}
}
#KEY "*job*" {
#Attitude Wicked {
"Huh! I should tell THEE anything!",
"Slaying thee would be better than listening to thy blabber!",
"Remove thy stinking hide from here, before I destroy thee utterly!"
}
#Attitude Neutral {
"I fight!",
"I kill!",
"I maim!",
"None o' thy business what I do, $little man/little lady$.",
"I think a my affairs are my own business!"
}
#Attitude Goodhearted {
"I'm paid to fight!",
"I kill!  For the right cause, that is.  And usually not humans.",
"I maim!  But only when I'm short o' money.  Otherwise I'm a gardener.",
"It ain't for thee to know what I do, $milord/milady$.",
"I do what I can to survive!",
"I fight to survive!  And then I fight for fun!"
}
}
#KEY "*skill*" "*fight*" "*kill*" "*maim*" {    
	#Attitude Wicked {
	"Sure, I can help thee. Heh. My granny could teach thee.",
	"I could teach thee some things. For money.",
	"I can show thee how to stay alive, if that's what thou need."
			}
	#Attitude Neutral {
	"I taught a few how to fight, for a little money.",
"Thou needs help with thy fighting? I'll try to help.",
"I'll give some tips, just ask.  And give me a few coins."
			}
	#Attitude Goodhearted {
	"Yeah, thou could learn some things from me. Gimme money, though. Can't learn for free.",
"Sure, I could show thee what I know. 'Course, thou would need to practice.",
"I can teach thee if thou'rt willin' to learn. Also, pay me."
			}
		}
#KEY "*what*do*do*", "*profession*", "*occupation*"    {
#Attitude Wicked {
	"I fight!",
	"I kill!",
	"I maim!"
		}
#Attitude Neutral {
	"I travel.",
	"I fight.  A lot.",
	"I'm paid to fight. Sometimes."
		}
#Attitude Goodhearted {
	"I travel.  With my weapons.  And use them.",
	"I fight.  A lot.",
	"I'm paid to fight battles."

		}
}
#KEY "*trouble*"  {
#Attitude Wicked {
	"Trouble. It's what I live for!",
	"Trouble? Thou'rt causin' trouble! Quit, or I'll have to destroy thee!"

		}
#Attitude Neutral {
	"Trouble?  Where?  I'll give 'em some... trouble!",
	"There'll be no trouble while I'm around! 'Less, o' course, I'm the cause!"
}
#Attitude Goodhearted {
	"No one's stupid enough to cause trouble while I'm here!",
	"I don't care about thy troubles!"
		}
}
}
}




