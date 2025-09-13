// Ranger function
//
// Keywords:
// job, what*do*do, ranger, hunt*, track*, animal, alligator, bird, bear, deer, gorilla, llama, eagle, mountain cat,
// rabbit, wolf, dolphin, seal, cow, chicken, pig, sheep, cat, dog, horse, monster, elemental, corpser, daemon,
// dragon,.ettin, gargoyle, gazer, ghost, giant, harpy, headless, liche, lizard-men, lizard men, mong bat, ogre, orc,
// reaper, rat men, rat-men, serpent, skeleton, slime, troll, wisp, zombie, spirituality
//
//
// 
// - cwm


#Fragment Britannia, Job, Britannia_Ranger {

#Sophistication High {
#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*"  {
#Attitude Wicked {
	"I am a ranger.",
	"I am a ranger. I run through the forests hunting animals. Rain or shine.",
	"I am a ranger. Some say this profession is a noble one, but to me 'tis the glorified art of animal slaughter."
	}
#Attitude Neutral {
	"I am a ranger.",
	"I am a ranger. I spend most of my days in the woods, observing and tracking animals. I hunt that I may eat, and try to live in harmony with the land.",
	"I am a ranger. There are some who accuse us of preying on innocent creatures, but truly, I am a predator like the wolf or eagle - I hunt because I must eat to live. I take no more than I need and return what I can."
	}
#Attitude Goodhearted {
	"I am a ranger.",
	"I am a ranger. I wander the forest, the world and all the creatures in it my study. Thou wouldst not believe the marvels that exist around thee. Hadst thou but an eye to them, thou wouldst see.",
	"I am a ranger. I seek to understand the wild creatures of Britannia - not hating them because they lack humanity, nor seeking to dominate them because I crave power."
	}
	}
#KEY "*ranger*" {
#Attitude Wicked {
	"Some will tell thee that a ranger is steward to Nature. 'Tis all a fabrication - honey spooned out to those who haven't the stomach for their trade. Rangers are hunters. Killers. No more, no less."
	}
#Attitude Neutral {
	"Rangers live off of the land. The animals and plants of Britannia are their study, with the skills of the forest they make their living."
	}
#Attitude Goodhearted {
	"Rangers walk the earth with a soft step. Animals are their study - the habits of the deer, the wolf, the bear and the rabbit. They need little, and are pleased by nearly all."
}
}
#KEY "*hide*", "*pelt*" {
	"If thou art able to collect a pelt or hide, thou can sell it to a tanner.",
	"I am certain that thou could sell any hides or pelts that thou might procure to a tanner for a reasonable price."
}
#KEY "*meat*", "*fowl*", "*fish*"   {
	"Most meats thou can sell to a butcher, but thou would probably want to take fish and fowl to a cook.",
	"I am sure a butcher would offer thee fair price for most meats thou could obtain. If thou dost catch fish or fowl, though, look to a cook."
}

#KEY "*hunt*", "*skill*" {
#Attitude Wicked {
	"A good hunter never misses.",
	"A successful hunter knows his prey, knows the land nearby and is a skilled archer. If thou would be a hunter, I give thee only this advice - study the three things I just mentioned, and leave me be!"
	}
#Attitude Neutral {
	"A good hunter will never go hungry. I could possibly teach thee some skills that may help thee",
	"Hunting requires some skill in tracking."
	}
#Attitude Goodhearted {
	"A good hunter kills with the first arrow. 'Tis a terrible thing to wound an animal.",
	"Naturally, hunting requires skill in tracking and in using the bow. However, it also requires patience and sound judgment. I could show thee a few things I've picked up over the years that might help thee."
}
}
#KEY "*track*", "*skill*" {
#Attitude Wicked {
	"One cannot be a good tracker without knowing the habits of the animal one is tracking. If thou hast not this knowledge, I suggest thou seek it.",
"I can probably help improve thy tracking skills some.",
	}
#Attitude Neutral {
	"Tracking is more of an art than a science, but it begins with a thorough knowledge of the traits and habits of the creature being tracked. I can help thee with that, I think."
	}
#Attitude Goodhearted {
	"If thou wouldst learn the fine art of tracking, study the animal thou wouldst track, then follow it around. Let it evade thee, and then try to find it again. Eventually this will become second nature to thee."
}
}
#KEY "*animal*", "*alligator*", "*bird*", "*bear*", "*deer*", "*gorilla*", "*llama*", "*eagle*", "*mountain cat*", "*rabbit*", "*wolf*", "*dolphin*", "*seal*", "*skill*" {
#Attitude Wicked {
	"There is naught I can tell thee about any animal, that thou couldst not learn from a good book or thy own observation. I've no patience for laggards."
	}
#Attitude Neutral {
	"Naught that I tell thee can replace thy own observation. If thou wouldst learn about animals, study them, not me."
	}
#Attitude Goodhearted {
	"I cannot offer thee any knowledge equal to what thou wouldst learn by direct observation. Walk through the forest, $man/lady$, and take note of what thou seest. What there is to know lies all about thee."
}
}
#KEY "*cow*", "*chicken*", "*goat*", "*pig*", "*sheep*"{
#Attitude Wicked {
	"I am a ranger, not a farmer. I've no knowledge of domesticated animals, nor do I need any."
	}
#Attitude Neutral {
	"If thou hast questions about domesticated animals, 'twere best thou ask a farmer. I have little understanding of them myself."
	}
#Attitude Goodhearted {
	"I fear I know naught about any farm animal. They lie somewhat outside my line of work - 'twere best thou pose thy question to a farmer."
}
}
#KEY "*cat*", "*dog*", "*horse*"{
#Attitude Wicked {
	"I am a ranger, not a animal trainer. I suggest thou find one of them to answer thy questions."
	}
#Attitude Neutral {
	"I really don't consider myself an expert on these things. I am a ranger, not an animal trainer."
	}
#Attitude Goodhearted {
	"I fear I cannot help thee. Thou wert better asking an animal trainer."
}
}
#KEY "*monster*", "*elemental*", "*corpser*", "*daemon*", "*dragon*", "*ettin*", "*gargoyle*",
 "*gazer*", "*ghost*", "*giant*", "*harpy*", "*headless*", "*liche*", "*lizard-men*", "*lizard men*",
 "*mong bat*", "*ogre*", "*orc*", "*reaper*", "*rat-men*", "*rat men*", 
"*serpent*", "*skeleton*", "*slime*", "*troll*", "*wisp*", "*zombie*"{
#Attitude Wicked {
	"I occasionally meet such creatures in the course of my wandering, but I try to avoid them so far as I am able. I prithee, do likewise.",
	"Wise people do not make such creatures their study. Encountering them is sometimes unavoidable, but I do my best."
	}
#Attitude Neutral {
	"I try to avoid such creatures, unless they insist on aggression towards me.",
	"If thou art truly interested in such creatures, I believe there was a hunter by name of Craddock who had more than his fair share of encounters with them. He wrote a treatise on monsters, as I recall."
	}
#Attitude Goodhearted {
	"Such creatures are indeed fascinating, but still I try to avoid them unless they insist on attacking me.",
	"If thou art truly interested in such creatures, I believe there was a hunter by name of Craddock who had more than his fair share of encounters with them. He wrote a treatise on monsters, as I recall."
}
}
#KEY "*spirituality*"{
#Attitude Wicked{
	"There be those who insist the life of the ranger is a spiritual one. I say they are none but pathetic dreamers with naught to do all day but moon about under the trees. I bet thee gold they've never gone hungry."
}
#Attitude Neutral{
	"Aye, some say the life of a ranger is a spiritual one. I suppose there may be truth in that - we are out here under the sun and the trees away from the distraction of town."
}
#Attitude Goodhearted{
	"I can see why one may find the life of a ranger spiritual. We spend our time in the quiet wood, pondering the cycles of life and death, waxing and waning. It certainly gives time and cause for reflection."
}
}
#KEY "*skill*" {
	"Ususually these skills are honed by many hours out in nature, but perhaps I can teach thee a tip or two.",
	"Perhaps we can trade a bit of coin for a bit of time practicing with me.",
	"If you have the time to learn and a bit of coin, I can help thee with thy desire."
}
}
#Sophistication Medium {
#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*"  {
#Attitude Wicked {
	"I am a ranger.",
	"I am a ranger. I run through the forests hunting animals. Rain or shine.",
	"I am a ranger. Some say this profession is a noble one, but to me 'tis the glorified art of animal slaughter."
	}
#Attitude Neutral {
	"I am a ranger.",
	"I am a ranger. I spend most of my days in the woods, observing and tracking animals. I hunt that I may eat, and try to live in harmony with the land.",
	"I am a ranger. There are some who accuse us of preying on innocent creatures, but truly, I am a predator like the wolf or eagle - I hunt because I must eat to live. I take no more than I need and return what I can."
	}
#Attitude Goodhearted {
	"I am a ranger.",
	"I am a ranger. I wander the forest, the world and all the creatures in it my study. Thou wouldst not believe the marvels that exist around thee. Hadst thou but an eye to them, thou wouldst see.",
	"I am a ranger. I seek to understand the wild creatures of Britannia - not hating them because they lack humanity, nor seeking to dominate them because I crave power."
	}
	}
#KEY "*ranger*" {
#Attitude Wicked {
	"Some will tell thee that a ranger is steward to Nature. 'Tis all a fabrication - honey spooned out to those who haven't the stomach for their trade. Rangers are hunters. Killers. No more, no less."
	}
#Attitude Neutral {
	"Rangers live off of the land. The animals and plants of Britannia are their study, with the skills of the forest they make their living."
	}
#Attitude Goodhearted {
	"Rangers walk the earth with a soft step. Animals are their study - the habits of the deer, the wolf, the bear and the rabbit. They need little, and are pleased by nearly all."
}
}
#KEY "*hide*", "*pelt*" {
	"If thou art able to collect a pelt or hide, thou can sell it to a tanner.",
	"I am certain that thou could sell any hides or pelts that thou might procure to a tanner for a reasonable price."
}
#KEY "*meat*", "*fowl*", "*fish*"   {
	"Most meats thou can sell to a butcher, but thou would probably want to take fish and fowl to a cook.",
	"I am sure a butcher would offer thee fair price for most meats thou could obtain. If thou dost catch fish or fowl, though, look to a cook."
}
#KEY "*hunt*", "*skill*" {
#Attitude Wicked {
	"A good hunter never misses.",
	"A successful hunter knows his prey, knows the land nearby and is a skilled archer. If thou would be a hunter, I give thee only this advice - study the three things I just mentioned, and leave me be!"
	}
#Attitude Neutral {
	"A good hunter will never go hungry. I could teach thee some skills, if it would help thee. It won't cost thee much.",
	"Hunting requires skill in tracking and in using the bow."
	}
#Attitude Goodhearted {
	"A good hunter kills with the first arrow. 'Tis a terrible thing to wound an animal.",
	"Naturally, hunting requires skill in tracking and in using the bow. However, it also requires patience and sound judgment. I could maybe teach thee some of that, if thou dost have the time and money."
}
}
#KEY "*track*", "*skill*" {
#Attitude Wicked {
	"One cannot be a good tracker without knowing the habits of the animal one is tracking. If thou hast not this knowledge, I suggest thou seek it."
	}
#Attitude Neutral {
	"Tracking is more of an art than a science, but it begins with a thorough knowledge of the traits and habits of the creature being tracked. Perhaps I could help thy understanding of those habits, for a few coins."
	}
#Attitude Goodhearted {
	"If thou wouldst learn the fine art of tracking, study the animal thou wouldst track, then follow it around. Let it evade thee, and then try to find it again. Eventually this will become second nature to thee."
}
}
#KEY "*animal*", "*alligator*", "*bird*", "*bear*", "*deer*", "*gorilla*", "*llama*", "*eagle*", "*mountain cat*", "*rabbit*", "*wolf*", "*dolphin*", "*seal*", "*skill*" {
#Attitude Wicked {
	"There is naught I can tell thee about any animal, that thou couldst not learn from a good book or thy own observation. I've no patience for laggards."
	}
#Attitude Neutral {
	"Naught that I tell thee can replace thy own observation. If thou wouldst learn about animals, study them, not me. I could, of course teach thee some, for a price."
	}
#Attitude Goodhearted {
	"I cannot offer thee any knowledge equal to what thou wouldst learn by direct observation. Walk through the forest, $man/lady$, and take note of what thou seest. What there is to know lies all about thee.",
"I can, if thou dost wish, help thee to be more skilled at some aspect of my craft. I do charge for my time, though."
}
}
#KEY "*cow*", "*chicken*", "*goat*", "*pig*", "*sheep*"{
#Attitude Wicked {
	"I am a ranger, not a farmer. I've no knowledge of domesticated animals, nor do I need any."
	}
#Attitude Neutral {
	"If thou hast have questions about domesticated animals, 'twere best thou ask a farmer. I have little understanding of them myself."
	}
#Attitude Goodhearted {
	"I fear I know naught about any farm animal. They lie somewhat outside my line of work - 'twere best thou pose thy question to a farmer."
}
}
#KEY "*cat*", "*dog*", "*horse*"{
#Attitude Wicked {
	"I am a ranger, not a animal trainer. I suggest thou find one of them to answer thy questions."
	}
#Attitude Neutral {
	"I really don't consider myself an expert on these things. I am a ranger, not an animal trainer."
	}
#Attitude Goodhearted {
	"I fear I cannot help thee. Thou wert better asking an animal trainer."
}
}
#KEY "*monster*", "*elemental*", "*corpser*", "*daemon*", "*dragon*", "*ettin*", "*gargoyle*",
 "*gazer*", "*ghost*", "*giant*", "*harpy*", "*headless*", "*liche*", "*lizard-men*", "*lizard men*",
 "*mong bat*", "*ogre*", "*orc*", "*reaper*", "*rat-men*", "*rat men*", "*serpent*", "*skeleton*",
 "*slime*", "*troll*", "*wisp*", "*zombie*"{
#Attitude Wicked {
	"I occasionally meet such creatures in the course of my wandering, but I try to avoid them so far as I am able. I prithee, do likewise.",
	"Wise people do not make such creatures their study. Encountering them is sometimes unavoidable, but I do my best."
	}
#Attitude Neutral {
	"I try to avoid such creatures, unless they insist on aggression towards me.",
	"If thou art truly interested in such creatures, I believe there was a hunter by name of Craddock who had more than his fair share of encounters with them. He wrote a treatise on monsters, as I recall."
	}
#Attitude Goodhearted {
	"Such creatures are indeed fascinating, but even so, I try to avoid them unless they insist on attacking me.",
	"If thou art truly interested in such creatures, I believe there was a hunter by name of Craddock who had more than his fair share of encounters with them. He wrote a treatise on monsters, as I recall."
}
}
#KEY "*spirituality*"{
#Attitude Wicked{
	"There be those who insist the life of the ranger is a spiritual one. I say they are none but pathetic dreamers with naught to do all day but moon about under the trees. I bet thee gold they've never gone hungry."
}
#Attitude Neutral{
	"Aye, some say the life of a ranger is a spiritual one. I suppose there may be truth in that - we are out here under the sun and the trees away from the distraction of town."
}
#Attitude Goodhearted{
	"I can see why one may find the life of a ranger spiritual. We spend our time in the quiet wood, pondering the cycles of life and death, waxing and waning. It certainly gives time and cause for reflection."
}
}
#KEY "*skill*" {
	"Ususually these skills are honed by many hours out in nature, but perhaps I can teach thee a tip or two.",
	"Perhaps we can trade a bit of coin for a bit of time practicing with me.",
	"If you have the time to learn and a bit of coin, I can help thee with thy desire."
}
}
#Sophistication Low {
#KEY "*job*", "*what*do*do*", "*profession*" "*occupation*"  {
#Attitude Wicked {
	"I'm a ranger, $milord/milady$.",
	"I'm a ranger, $milord/milady$. I hunt animals, come rain or shine.",
	"I'm a ranger, $milord/milady$. Some say 'tis a noble trade, but I can't see as it's more than killing animals."
	}
#Attitude Neutral {
	"I'm a ranger, $milord/milady$.",
	"I'm a ranger, $milord/milady$. I spend my time in the woods, watchin' and trackin' animals. Huntin', when I'm hungry, that sort of thing.",
	"I'm a ranger, $milord/milady$. I study animals and hunt 'em for my food, takin' no more than I need to live."
	}
#Attitude Goodhearted {
	"I'm a ranger, $milord/milady$.",
	"I'm a ranger, $milord/milady$. I hike through the woods and study the animals. I tell thee, there are marvels about thee. Thou need but to see 'em.",
	"I'm a ranger, $milord/milady$. I try to understand the wild creatures of Britannia."
	}
	}
#KEY "*ranger*" {
#Attitude Wicked {
	"Some say a ranger is a kind of keper of Nature. Not sure I'd say that, but some do."
	}
#Attitude Neutral {
	"We rangers live off of the land. We study animals and plants, and make our living in the forest."
	}
#Attitude Goodhearted {
	"We rangers are a decent folk. We get our schooling from the animals, studyin' the deer, the
wolf, the bear and the rabbit. We don't need much."
}
}
#KEY "*hide*", "*pelt*" {
	"If thou'rt able to get a pelt or hide, thou can sell it to a tanner.",
	"I think that thou  can sell most hides an' pelts to a tanner for a good price."
}
#KEY "*meat*", "*fowl*", "*fish*"   {
	"A butcher will buy most meats from thee, but thou wouldd probably want to take fish and fowl to a cook.",
	"I'm sure a butcher would buy most meats thou might get from thee. If thou catch a fish or a bird, though, find thyself a cook."
}
#KEY "*hunt*", "*skill*" {
#Attitude Wicked {
	"A good hunter don't miss.",
	"A good hunter knows his prey and the land nearby. Bein' a good archer helps, too."
	}
#Attitude Neutral {
	"A good hunter will never go hungry.",
	"To hunt, thou needs skill at trackin' and in using the bow. I can maybe help."
	}
#Attitude Goodhearted {
	"A good hunter kills with the first arrow. 'Tis terrible to just wound an animal.",
	"To hunt, thou needs skill at trackin' and in using the bow. Thou needs to be patient and have sound judgment, too. I can help with thy skills, if thou needs it."
}
}
#KEY "*track*", "*skill*" {
#Attitude Wicked {
	"Thou can't be a good tracker, if thou don't know the habits of the animal thou tracks."
	}
#Attitude Neutral {
	"Tracking is more art than skill. Thou must know the creature and know what 'tis gonna to do. I might could help, if thou'rt interested in learnin'."
	}
#Attitude Goodhearted {
	"If thou would learn to track, study an animal, then follow it around. Lose it, then find it again. Soon 'twill be easy. I might be able to teach thee some, for a few coins."
}
}
#KEY "*animal*", "*alligator*", "*bird*", "*bear*", "*deer*", "*gorilla*", "*llama*", "*eagle*", "*mountain cat*", "*rabbit*", "*wolf*", "*dolphin*", "*seal*", "*skill*" {
#Attitude Wicked {
	"There ain't much I can tell thee, $milord/milady$, that thou couldn't learn from thy own study. I got no patience for laggards."
	}
#Attitude Neutral {
	"Nothin' I can say will replace thy own study, $milord/milady$. If thou would learn about animals,
then study 'em. Maybe I can teach thee a little, though."
	}
#Attitude Goodhearted {
	"I can't tell thee what thou can learn by watchin'. Walk through the forest, $milord/milady$, and remember what thou sees. I could demonstrate for a few coins."
}
}
#KEY "*cow*", "*chicken*", "*goat*", "*pig*", "*sheep*"{
#Attitude Wicked {
	"I'm a ranger, not a farmer, $milord/milady$. I know nothin' about farmyard animals."
	}
#Attitude Neutral {
	"If thou got questions about farmyard animals, best ask a farmer. I don't got much understandin' of 'em myself, $milord/milady$."
	}
#Attitude Goodhearted {
	"I don't know much about farm animals, $milord/milady$. Ain't somethin' I studied. Better ask
a farmer."
}
}
#KEY "*cat*", "*dog*", "*horse*"{
#Attitude Wicked {
	"I'm a ranger, not a animal trainer, $milord/milady$. Ask one of them thy questions."
	}
#Attitude Neutral {
	"I don't know much about these things, $milord/milady$. Better ask an animal trainer."
	}
#Attitude Goodhearted {
	"I can't help much, $milord/milady$. Thou wert better askin' an animal trainer."
}
}
#KEY "*monster*", "*elemental*", "*corpser*", "*daemon*", "*dragon*", "*ettin*", "*gargoyle*",
 "*gazer*", "*ghost*", "*giant*", "*harpy*", "*headless*", "*liche*", "*lizard-men*", "*lizard men*",
 "*mong bat*", "*ogre*", "*orc*", "*reaper*", "*rat-men*", "*rat men*", "*serpent*", "*skeleton*",
 "*slime*", "*troll*", "*wisp*", "*zombie*" {
#Attitude Wicked {
	"I meet such creatures when I'm out in the woods, but I try to avoid them. I'd say that thou should do the same.",
	"Wise people don't study them creatures. I try an' avoid 'em."
	}
#Attitude Neutral {
	"I try to avoid them creatures, unless they attack me.",
	"If thou'rt interested in them such creatures, there's a man by name of Craddock wrote a book on
monsters, as I recollect."
	}
#Attitude Goodhearted {
	"Such creatures are indeed strange. Still, I try to avoid them unless they attack me.",
	"If thou'rt interested in such creatures, there's a man by name of Craddock wrote a book on monsters, as
I recollect."
}
}
#KEY "*spirituality*"{
#Attitude Wicked{
	"There be those who say the life of the ranger is a spiritual one. I say they're dreamers who don't do much  but moon about under the trees. I bet thee gold they ain't never gone hungry."
}
#Attitude Neutral{
	"Aye, some say the life of a ranger is a spiritual one. I guess there may be truth in that - we work out here among the trees, away from the bustling of town."
}
#Attitude Goodhearted{
	"I can see why many think the life of a ranger is spiritual. We spend our time in the woods, watchin' all day. Gives a body a chance to think, it does."
}
}
#KEY "*skill*" {
	"My skills are honed by lotsa hours out in nature, but maybe I can teach thee a tip or two.",
	"I guess we can trade a bit of coin for a bit of time practicin' with me.",
	"If thou gots the time to learn and a bit of coin, I can help thee a bit."
}
}
}

