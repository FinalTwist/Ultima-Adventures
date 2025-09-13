//Britannia Mage Fragment				    
//Notes:  This is used for general information pertaining to all Britannian Mages
//Current Keyword List:	job, profession, craft, art, occupation, art, talent, spell, spells
//	spellcasting, casting, cast, reagent, component, guild, words of power, spell books, 
//	spellbooks, arcane, ingredients, ether, Moonglow, (all the spell names)
//Additional Keywords:  teleport, curse, gate
//Revision Date:  3/16/96 // MAy 2020
//Author:  Andrew Morris

//Eventually, this pool, like a lot of the other specialties (i.e., "classes") should check to see whether
//the player belongs to a particular guild. That way, the mages will give informative answers that apply beyond 
//the level of laymanship.

//Note, nothing here distinguishes between notoriety and attitude and the sophistication for High and Medium
//are identical

//!!Ultimately, the guild info. will have to be more informative, since it will be used over the local knowledge pool.

#Fragment Britannia, Job, Britannia_Mage 
{
	#Sophistication High 
	{
		#Key "*job*", "*profession*", "*craft*", "*occupation*", "* art*", "*talent*", "*what*do*do*" 
		{
			"I am a wizard, my dear $sir/lady$, a weaver of spells.",
			"I, my dear $sir/lady$, am a master of the arcane.",
			"_MyName_, mage extraordinaire, $milord/milady$."
		}
		#Key "*empath*", "*abbey*", "*where*monks*"  
		{
                        "Hmm. Empath Abbey can be found just outside of Yew. To the northeast of that fair city, to be more precise.",
                        "The Abbey is to be found northeast from Yew.",
                        "The monks of Empath Abbey are found near Yew. Northeast of the city proper."
                }
		#KEY "*relvinian*"  
		{
			"Relivinian was a mage driven mad by the Daemons he attempted to control.  He became so afraid that others were attempting to steal his research that 
he fled Britain and drew upon his powers to create a magical maze pulled from all areas of the world to protect himself from watchful eyes.",
			"Relvinian is an elf-spawn from the old days of Britannia before the coming of Exodus.  He grew in power soon after the destruction of Exodus, but vanished mysteriously ages ago.",
			"It is said that Relvinian's goal was to summon Daemons to serve as slaves to the people of Britannia.  As thou might suspect, his foolish venture went gravely wrong.",
			"I've heard Relvinian's maze contains naught but mages now.",
			"Rumor has it that Relvinian's maze contains the power to heal those wounded in battle.",
			"It is said that somewhere in Relvinian's maze is the power to summon daemons, but I would not bet my life on it."
		}
		#KEY "*skill*" "*ability*" "*abilities*" 
		{
			#Attitude Wicked 
			{
				"I may grudgingly help thee learn a few things. I would prefer that thou didst leave me alone, but I can teach thee a bit, if thou dost need it.",
				"I'd wish for thee to leave me be. But if thou dost need, I could teach thee some of what I know.",
				"Please, I ask thee to just leave me alone. I have little time for thee."
			}
			#Attitude Neutral 
			{
				"If thou dost want to learn, I might be persuaded to teach thee some of the arcane arts. Thou wouldst certainly need to pay me, of course.",
				"Thou shouldst know something about the magical arts. 'Twould help thee greatly. I could be of some assistance, I think.",
				"If thou dost need help with thy magic, just ask me to teach thee and I will. For a small price.",
				"I can teach thee to better weave thy spells. Could be of use to thee in the future."
			}
			#Attitude Goodhearted 
			{
				"I'd be happy for thee to learn what I know about the arcane arts. I could give thee lessons for a few coins.",
				"Thou can always learn new things, my friend. I could teach thee some things that could help thee cast thy spells. For some small amount of money, of course.",
				"I can teach thee things that could help thee perform thy magic better. I would need thee to pay me for my time, however."
			}
		}
		#Key "*spell*", "* spells*" 
		{
			"Well, $sir/dear lady$, I know many, many spells. I can tell thee about some basic spells if thou dost have questions.",
			"Spells are nothing more than the warping of supernatural forces by mortal beings... highly intelligent and learned beings, but mortal nonetheless. Dost thou have a question about a particular spell?",
			"Learning a spell takes a considerable amount of time, $my friend/my dear$, and even then only after one has spent many, many years learning to comprehend how little of the world thou dost truly understand. 
Mayhaps thou hast a particular spell in mind? I can explain the use of some of the simpler spells."
		}
		#Key "*casting*", "* cast*" 
		{
			"Casting spells takes more than the understanding of the magics involved. Certain ingredients, called reagents, must be mixed together to muster the supernatural forces of the ether.", 
			"To cast a spell, a mage must know the words of power required to harness the ether.",
			"Spellcasting is a delicate process, involving the use of reagents, spell books, and words of power."	
		}
		#Key "*reagent*", "*component*", "*ingredients*" 
		{
			"There are many different types of reagents. In fact, to some degree, nearly any material can be used to bring forth power. However, some ingredients are better than others, 
and still others possess such a minute modicum of power that it would take enough of that material to fill an entire house before the desired effect coulst be achieved.",
			"Reagents are the ingredients necessary to summon the ether to do one's bidding. Most spells cannot be cast until the required reagents have been mixed together. Of course, 
those who are more in tune with the ether are more inclined to succeed without mixing the reagents ahead of time.", 
			"Finding and choosing the best reagents for a particular spell can be difficult, which is a major reason for the importance of a spell book. 
In addition, choosing inferior reagents for a spell couldst increase the chance of the spell failing... or even working contrary to the wishes of the caster."
		}
		#Key "*guild*" 
		{
			"Aye, $sir/dear lady$, we do often find that working together and exchanging information about the arcane arts is quite beneficial to all parties involved. There is quite a lot to learn -- more than canst be gleaned during a single lifetime.",
			"Our... kind... tend to congregate in secluded or secure places where we know our quest for knowledge will not be disturbed. Of course, some travel to Moonglow to study in the largest libraries dedicated to the mystical arts.",
			"Mages are like any other artisan. Well, SOMEWHAT like any other artisan. By that I mean, of course, that we find benefits in sharing knowledge and resource, just like members of any of the mundane  crafts."
		}
		#Key "*words of power*" 
		{
			"Words of power are the syllables that are combined to express the name of a particular spell.",
			"The words of power are merely yet another way to facilitate the mage's attempt to harness the ether.",
			"By combining these words of power, a mage can manipulate and build upon previous knowledge to form new or stronger forms of magic."
		} 
		#Key "*spellbooks*", "*spell books*" 
		{
			"Spell books are much like books of recipes... save that the results of preparing a spell are far, far different than what a cook might serve.",
	   		"Each spell requires certain combinations for success. Each page within a spell book is a description of those elements, as well as an explanation of the results.",
			"Some extremists have speculated that spell books are not required components of casting a spell. 
However, I have yet to meet anyone who couldst demonstrate such a theory, short of reading the spell directly from a scroll."							 
		}
		#Key "*scroll*"
		{
			"Thou can read a spell directly from the scroll, or use the magic inherent in the parchment
to scripe the spell into thy sellbook.",
			"A scroll is useful for throwing a spell, but I would suggest using it to add the spell to
thy spellbook, if thou dost wish to advance thy magical powers.",
			"There are as many different scrolls as there are spells in Britannia. That is, in fact,
the only way to add to thy spellbook."
		}
		#Key "*arcane *" 
		{
			"Well, in the world of mundane information, arcane merely means mysterious. However, in MY world, the mystical world, arcane refers to the lesser known  magical arts.",
			"Aye, it means mysterious or unknown. Thus, when applied to magic, it refers to mastery of spells and spellcasting.",
			"The art of the arcane is one of mystical power, dear $sir/lady$, of magic.",	
			"We have seen some odd goings on in the ether... using it to travel is dangerous now..."			
		}
		#Key "*ether*" 
		{
			"I cannot explain it in simple terms, my friend. Suffice it to say that ether is merely an invisible manifestation of supernatural will.",
			"Wouldst that I could summarize such a complex concept. I suppose in its most basic form, ether is the energy of existence. No, not quite right. 'Tis the energy DERIVED from all existence.  No, that does not ring wholly true either...",
			"Ether? 'Tis an inexplicable force. Why, even the most sagacious and learned of wizards canst not hope to comprehend fully what either IS.",
			"There is an odd curse now... it plagues travel such as the use of teleport or gate... an ugly curse"
		}
		#Key "*Moonglow*" 
		{
			"Ah, yes, Lord British's haven for those wishing to master the arcane.",
			"Lord British set aside an entire island dedicated entirely towards the pursuit of knowledge and magic.",
			"Moonglow is a veritable plethora of knowledge, especially regarding magic and the mytical world."
		}
		#Key "*Wis Uus*" 
		{
			"That spell can be of use when thou art in need of a bird's-eye-view.",
			"Ah!  Use that spell to see landmarks from above.",
			"Those words can lift thine eyes above thy head, and see the land around thee."	
		}
		#Key "*Uus Jux*" 
		{
			"Ah, yes! Thou canst trip up thy opponents and make tem easier to hit!",
			"Use that spell to cause thy enemies to miss when they strike at thee or another.",
			"Ah! A spell to make one clumsier! It can be useful!" 
		}
		#Key "*In Mani Ylem*" 
		{
			"Why, 'tis a spell to create food to fill one's plate.",
			"A useful spell to feed the hungry.",
			"The ability to create food from the ether is one that sets a mage apart from the common people."
		}
		#Key "*Rel Wis*" 
		{
			"Why, 'tis the spell that can create morons!",
			"Aye, thou can certainly drain the intelligence straight out of a person with those words.",
			"The stupid-spell! With some skill, thy opponents won't know enough anymore to curse thee." 
		}
		#Key "*In Mani*" 
		{
			"Healer, heal thyself!", 
			"A useful spell for reducing the damage inflicted upon thee by this hostile world.",
			"Why, 'tis the magic that restores one's health.",
			"'Tis very useful on the battlefield to know how to heal.",
			"Yes, I know that one well. Any injured man or woman can tell thee how crucial that spell is!", 
			"Thou dost refer to the spell of healing."
		}
		#Key "*In Lor*" 
		{
			"Why, 'tis the spell that illuminates an area.",
			"Ah, the spell that creates a shining light to brighten one's evening.",
			"A useful spell for combating the dark of night.",
			"Why, 'tis a fine spell for creating a source of light.",
			"Ah, the very words that call forth a source of brilliant light.",
			"Thou dost speak of the magics involved in fabricating an object of luminescence."
		}
		#Key "*In Por Ylem*" 
		{
			"Strike at thine enemies from a distance!",
			"Yes, that spell can inflict some wounds while keeping thee out of the arc of the sword.",
			"A useful spell for combat!."	
		}
		#Key "*Des Mani*" 
		{
			"Yes, thou can use that spell to temporarily weaken thy opponent.",
			"Take the strength right out of a person!",
			"Makes the warrior as weak as a baby."
		}
		#Key "*teleport*" 
		{
			"Careful my friend, be wary of the curse when using this spell!",
			"There is a corruption that is tearing the ether appart... using it to travel is not recommended.",
			"Beware this spell my friend, there are repercussions!"
		}
		#Key "*curse*" 
		{
			"It is said that the curse affecting the ether came about when Malaka perished.",
			"The curse tears away at the very fabric of a traveller... ",
			"Alas, i do not know of a cure for this curse... ."
		}
		#Key "*gate*" 
		{
			"Careful my friend, be wary of the curse when using this spell!",
			"There is a corruption that is tearing the ether appart... using it to travel is not recommended.",
			"Beware this spell my friend, there are repercussions!"
		}
	}
	#Sophistication Medium 
	{
		#Key "*job*", "*profession*", "*craft*", "*occupation*", "* art*", "*talent*", "*what*do*do*" 
		{
			"I am a wizard, my dear $sir/lady$, a weaver of spells.",
			"I, my dear $sir/lady$, am a master of the arcane.",
			"_MyName_, mage extraordinaire, $milord/milady$."
		}
		#Key "*empath*", "*abbey*", "*where*monks*"  
		{
                        "Hmm. Empath Abbey can be found just outside of Yew. To the northeast of that city, to be specific.",
                        "The Abbey is northeast of Yew.",
                        "The monks of Empath Abbey are found near Yew. Northeast of the city proper."
                }
		#KEY "*relvinian*"  
		{
			"Relivinian was a mage driven mad by the Daemons he attempted to control. He became so afraid that others were attempting to steal 
his research that he fled Britain and drew upon his powers to create a magical maze pulled from all areas of the world to protect himself from watchful eyes.",
			"Relvinian is an elf-spawn from the old days of Britannia before the coming of Exodus.  He grew in power soon after the destruction of Exodus, but vanished mysteriously ages ago.",
			"It is said that Relvinian's goal was to summon Daemons to serve as slaves to the people of Britannia.  As thou might suspect, his foolish venture went gravely wrong.",
			"I've heard Relvinian's maze contains naught but mages now.",
			"Rumor has it that Relvinian's maze contains the power to heal those wounded in battle.",
			"It is said that somewhere in Relvinian's maze is the power to summon daemons, but I would not bet my life on it."
		}
		#KEY "*skill*" "*ability*" "*abilities*" 
		{
			#Attitude Wicked 
			{
				"I may grudgingly help thee learn a few things. I would prefer that thou didst leave me alone, but I can teach thee a bit, if thou dost need it.",
				"I'd wish for thee to leave me be. But if thou dost need, I could teach thee some of what I know.",
				"Please, I ask thee to just leave me alone. I have little time for thee."
			}
			#Attitude Neutral 
			{
				"If thou dost want to learn, I might be persuaded to teach thee some of the arcane arts. Thou wouldst certainly need to pay me, of course.",
				"Thou shouldst know something about the magical arts. 'Twould help thee greatly. I could be of some assistance, I think.",
				"If thou dost need help with thy magic, just ask me to teach thee and I will. For a small price.",
				"I can teach thee to better weave thy spells. Could be of use to thee in the future."
			}
			#Attitude Goodhearted 
			{
				"I'd be happy for thee to learn what I know about the arcane arts. I could give thee lessons for a few coins.",
				"Thou can always learn new things, my friend. I could teach thee some things that could help thee cast thy spells. For some small amount of money, of course.",
				"I can teach thee things that could help thee perform thy magic better. I would need thee to pay me for my time, however."
			}
		}
		#Key "*spell*", "* spells*" 
		{
			"Well, $sir/dear lady$, I know many, many spells. I can tell thee about some basic spells if thou dost have questions.",
			"Spells are nothing more than the warping of supernatural forces by mortal beings... highly intelligent and learned beings, but mortal nonetheless. Dost thou have a question about a particular spell?",
			"Learning a spell takes a considerable amount of time, $my friend/my dear$, and even then only after one has spent many, many years learning to comprehend how little of the world thou dost truly understand. 
Mayhaps thou hast a particular spell in mind? I can explain the use of some of the simpler spells."
		}
		#Key "*casting*", "* cast*" 
		{
			"Casting spells takes more than the understanding of the magics involved. Certain ingredients, called reagents, must be mixed together to muster the supernatural forces of the ether.", 
			"To cast a spell, a mage must know the words of power required to harness the ether.",
			"Spellcasting is a delicate process, involving the use of reagents, spell books, and words of power."	
		}
		#Key "*reagent*", "*component*", "*ingredients*" 
		{
			"There are many different types of reagents. In fact, to some degree, nearly any material can be used to bring forth power. However, some ingredients are better than others, 
and still others possess such a minute modicum of power that it would take enough of that material to fill an entire house before the desired effect coulst be achieved.",
			"Reagents are the ingredients necessary to summon the ether to do one's bidding. Most spells cannot be cast until the required reagents have been mixed together. 
Of course, those who are more in tune with the ether are more inclined to succeed without mixing the reagents ahead of time.", 
			"Finding and choosing the best reagents for a particular spell can be difficult, which is a major reason for the importance of a spell book. 
In addition, choosing inferior reagents for a spell couldst increase the chance of the spell failing... or even working contrary to the wishes of the caster."
		}
		#Key "*guild*" 
		{
			"Aye, $sir/dear lady$, we do often find that working together and exchanging information about the arcane arts is quite beneficial to all parties involved. There is quite a lot to learn -- more than canst be gleaned during a single lifetime.",
			"Our... kind... tend to congregate in secluded or secure places where we know our quest for knowledge will not be disturbed. Of course, some travel to Moonglow to study in the largest libraries dedicated to the mystical arts.",
			"Mages are like any other artisan. Well, SOMEWHAT like any other artisan. By that I mean, of course, that we find benefits in sharing knowledge and resource, just like members of any of the mundane  crafts."
		}
		#Key "*words of power*" 
		{
			"Words of power are the syllables that are combined to express the name of a particular spell.",
			"The words of power are merely yet another way to facilitate the mage's attempt to harness the ether.",
			"By combining these words of power, a mage can manipulate and build upon previous knowledge to form new or stronger forms of magic."
		} 
		#Key "*spellbooks*", "*spell books*" 
		{
			"Spell books are much like books of recipes... save that the results of preparing a spell are far, far different than what a cook might serve.",
	   		"Each spell requires certain combinations for success. Each page within a spell book is a description of those elements, as well as an explanation of the results.",
			"Some extremists have speculated that spell books are not required components of casting a spell. However, I have yet to meet anyone who couldst demonstrate such a theory."							 
		}
		#Key "*scroll*"
		{
			"If thy spellbook dost lack a spell, and thou art in possession of a scroll that it is
written upon, I would suggest using the scroll to inscribe the spell in thy book. 'Tis of much more value to thee.",
			"Thou can certainly cast a spell directly from a scroll, if thou dost wish."
		}
		#Key "*arcane *" 
		{
			"Well, in the world of mundane information, arcane merely means mysterious. However, in MY world, the mystical world, arcane refers to the lesser known  magical arts.",
			"Aye, it means mysterious or unknown. Thus, when applied to magic, it refers to mastery of spells and spellcasting.",
			"The art of the arcane is one of mystical power, dear $sir/lady$, of magic."	 
		}
		#Key "*ether*" 
		{
			"I cannot explain it in simple terms, my friend. Suffice it to say that ether is merely an invisible manifestation of supernatural will.",
			"Wouldst that I could summarize such a complex concept. I suppose in its most basic form, ether is the energy of existence. No, not quite right. 'Tis the energy DERIVED from all existence.  No, that does not ring wholly true either...",
			"Ether? 'Tis an inexplicable force. Why, even the most sagacious and learned of wizards canst not hope to comprehend fully what either IS."
		}
		#Key "*Moonglow*" 
		{
			"Ah, yes, Lord British's haven for those wishing to master the arcane.",
			"Lord British set aside an entire island dedicated entirely towards the pursuit of knowledge and magic.",
			"Moonglow is a veritable plethora of knowledge, especially regarding magic and the mytical world."
		}
		#Key "*Wis Uus*" 
		{
			"That spell can be of use when thou art in need of a bird's-eye-view.",
			"Ah! Use that spell to see landmarks from above.",
			"Those words can lift thine eyes above thy head, and see the land around thee."	
		}
		#Key "*Uus Jux*" 
		{
			"Ah, yes! Thou canst trip up thy opponents and make tem easier to hit!",
			"Use that spell to cause thy enemies to miss when they strike at thee or another.",
			"Ah! A spell to make one clumsier! It can be useful!" 
		}
		#Key "*In Mani Ylem*" 
		{
			"Why, 'tis a spell to create food to fill one's plate.",
			"A useful spell to feed the hungry.",
			"The ability to create food from the ether is one that sets a mage apart from the common people."
		}
		#Key "*Rel Wis*" 
		{
			"Why, 'tis the spell that can create morons!",
			"Aye, thou can certainly drain the intelligence straight out of a person with those words.",
			"The stupid-spell! With some skill, thy opponents won't know enough anymore to curse thee." 
		}
		#Key "*In Mani*" 
		{
			"Healer, heal thyself!", 
			"A useful spell for reducing the damage inflicted upon thee by this hostile world.",
			"Why, 'tis the magic that restores one's health.",
			"'Tis very useful on the battlefield to know how to heal.",
			"Yes, I know that one well. Any injured man or woman can tell thee how crucial that spell is!", 
			"Thou dost refer to the spell of healing."
		}
		#Key "*In Lor*" 
		{
			"Why, 'tis the spell that illuminates an area.",
			"Ah, the spell that creates a shining light to brighten one's evening.",
			"A useful spell for combating the dark of night.",
			"Why, 'tis a fine spell for creating a source of light.",
			"Ah, the very words that call forth a source of brilliant light.",
			"Thou dost speak of the magics involved in fabricating an object of luminescence."
		}
		#Key "*In Por Ylem*" 
		{
			"Strike at thine enemies from a distance!",
			"Yes, that spell can inflict some wounds while keeping thee out of the arc of the sword.",
			"A useful spell for combat!.",	
		}
		#Key "*Des Mani*" 
		{
			"Yes, thou can use that spell to temporarily weaken thy opponent.",
			"Take the strength right out of a person!",
			"Makes the warrior as weak as a baby."
		}
		#Key "*teleport*" 
		{
			"Careful my friend, be wary of the curse when using this spell!",
			"There is a corruption that is tearing the ether appart... using it to travel is not recommended.",
			"Beware this spell my friend, there are repercussions!"
		}
		#Key "*curse*" 
		{
			"It is said that the curse affecting the ether came about when Malaka perished.",
			"The curse tears away at the very fabric of a traveller... ",
			"Alas, i do not know of a cure for this curse... ."
		}
		#Key "*gate*" 
		{
			"Careful my friend, be wary of the curse when using this spell!",
			"There is a corruption that is tearing the ether appart... using it to travel is not recommended.",
			"Beware this spell my friend, there are repercussions!"
		}
	}
	#Sophistication Low 
	{
		#Key "*job*", "*profession*", "*craft*", "*occupation*", "* art*", "*talent*", "*what*do*do*" 
		{
			"I am a wizard, my dear $sir/lady$, a weaver of spells.",
			"I, my dear $sir/lady$, am a master of the arcane.",
			"_MyName_, mage extraordinaire, $milord/milady$."
		}
		#Key "*empath*", "*abbey*", "*where*monks*"  
		{
                        "Find Empath Abbey in the northwestern part of Britannia.",
                        "The Abbey is to be found in northwestern Britannia.",
                        "Empath Abbey is found in the northwest area of Britannia."
                }
		#KEY "*relvinian*"  
		{
			"Relivinian... Ah! He was a mage driven mad by the Daemons he tried to control.  He was so afraid that others were attempting to steal his research that he ran from Britain 
and used his powers to create a magical maze pulled from all areas of the world to protect himself from others.",
			"Relvinian is an elf-spawn from the old days of Britannia before the coming of Exodus.  He grew in power soon after the destruction of Exodus, but disappeared ages ago.",
			"It's said that Relvinian's goal was to summon Daemons to be slaves to the people of Britannia.  As thou might think, his venture went wrong.",
			"I've heard Relvinian's maze contains nothing but mages now.",
			"Rumor has it that Relvinian's maze has the power to heal those wounded in battle.",
			"It'ss said that somewhere in Relvinian's maze is the power to summon daemons, but I would not bet my life on it."
		}
		#KEY "*skill*" "*ability*" "*abilities*" 
		{
			#Attitude Wicked 
			{
				"I may be able to help thee learn a few things. I would prefer that thou left me alone, but I can teach thee some, if thou dost need it.",
				"I'd want thee to leave me be. But if thou dost need, I could teach thee some of what I know.",
				"Please, just leave me alone. I have little time for thee."
			}
			#Attitude Neutral 
			{
				"If thou dost want to learn, I might be persuaded to teach thee some of my skills. Thou wouldst  need to pay me, of course.",
				"Thou shouldst know something about magic. 'Twould help thee greatly. I could be of some help, I think.",
				"If thou dost need help with magic, just ask me to teach thee and I will. For a small price.",
				"I can teach thee to weave thy spells better. Could be of use to thee in the future."
			}
			#Attitude Goodhearted 
			{
				"I'd be happy for thee to learn what I know about magic. I could give thee lessons for a few coins.",
				"Thou can always learn new things, my friend. I could teach thee some things that could help thee cast thy spells. For some small amount of money, of course.",
				"I can teach thee things that could help thee perform thy magic. I would need thee to pay me for my time, however."
			}
		}
		#Key "*spell*", "* spells*" 
		{
			"Well, $sir/dear lady$, I know many, many spells. I can tell thee about some basic spells if thou dost have questions.",
			"Spells are nothing more than the warping of supernatural forces by mortal beings... highly intelligent and learned beings, but mortal nonetheless. Dost thou have a question about a particular spell?",
			"Learning a spell takes a considerable amount of time, $my friend/my dear$, and even then only after one has spent many, many years learning to comprehend how little of the world thou dost truly understand. 
Mayhaps thou has a particular spell in mind? I can explain the use of some of the simpler spells."
		}
		#Key "*casting*", "* cast*" 
		{
			"Casting spells takes more than the understanding of the magics involved. Certain ingredients, called reagents, must be mixed together to muster the supernatural forces of the ether.", 
			"To cast a spell, a mage must know the words of power required to harness the ether.",
			"Spellcasting is a delicate process, involving the use of reagents, spell books, and words of power."	
		}
		#Key "*reagent*", "*component*", "*ingredients*" 
		{
			"There are many different types of reagents. In fact, to some degree, nearly any material can be used to bring forth power. However, some ingredients are better than others, 
and still others possess such a minute modicum of power that it would take enough of that material to fill an entire house before the desired effect coulst be achieved.",
			"Reagents are the ingredients necessary to summon the ether to do one's bidding. Most spells cannot be cast until the required reagents have been mixed together. 
Of course, those who are more in tune with the ether are more inclined to succeed without mixing the reagents ahead of time.", 
			"Finding and choosing the best reagents for a particular spell can be difficult, which is a major reason for the importance of a spell book. 
In addition, choosing inferior reagents for a spell couldst increase the chance of the spell failing... or even working contrary to the wishes of the caster."
		}
		#Key "*guild*" 
		{
			"Aye, $sir/dear lady$, we do often find that working together and exchanging information about the arcane arts is quite beneficial to all parties involved. There is quite a lot to learn -- more than canst be gleaned during a single lifetime.",
			"Our... kind... tend to congregate in secluded or secure places where we know our quest for knowledge will not be disturbed. Of course, some travel to Moonglow to study in the largest libraries dedicated to the mystical arts.",
			"Mages are like any other artisan. Well, SOMEWHAT like any other artisan. By that I mean, of course, that we find benefits in sharing knowledge and resource, just like members of any of the mundane  crafts."
		}
		#Key "*words of power*" 
		{
			"Words of power are the syllables that are combined to express the name of a particular spell.",
			"The words of power are merely yet another way to facilitate the mage's attempt to harness the ether.",
			"By combining these words of power, a mage can manipulate and build upon previous knowledge to form new or stronger forms of magic."
		} 
		#Key "*scroll*"
		{
			"Scrolls can be used to add spells to a spellbook.",
			"Thou can either cast a spell directly from a scroll, or ad it to thy spellbook. Either way
it can be used once only."
		}
		#Key "*spellbooks*", "*spell books*" 
		{
			"Spell books are much like books of recipes... save that the results of preparing a spell are far, far different than what a cook might serve.",
	   		"Each spell requires certain combinations for success. Each page within a spell book is a description of those elements, as well as an explanation of the results.",
			"Some extremists have speculated that spell books are not required components of casting a spell. However, I have yet to meet anyone who couldst demonstrate such a theory."							 
		}
		#Key "*arcane *" 
		{
			"Well, in the world of mundane information, arcane merely means mysterious. However, in MY world, the mystical world, arcane refers to the lesser known  magical arts.",
			"Aye, it means mysterious or unknown. Thus, when applied to magic, it refers to mastery of spells and spellcasting.",
			"The art of the arcane is one of mystical power, dear $sir/lady$, of magic."	 
		}
		#Key "*ether*" 
		{
			"I cannot explain it in simple terms, my friend. Suffice it to say that ether is merely an invisible manifestation of supernatural will.",
			"Wouldst that I could summarize such a complex concept. I suppose in its most basic form, ether is the energy of existence. No, not quite right. 'Tis the energy DERIVED from all existence.  No, that does not ring wholly true either...",
			"Ether? 'Tis an inexplicable force. Why, even the most sagacious and learned of wizards canst not hope to comprehend fully what either IS."
		}
		#Key "*Moonglow*" 
		{
			"Ah, yes, Lord British's haven for those wishing to master the arcane.",
			"Lord British set aside an entire island dedicated entirely towards the pursuit of knowledge and magic.",
			"Moonglow is a veritable plethora of knowledge, especially regarding magic and the mytical world."
		}
		#Key "*Wis Uus*" 
		{
			"That spell can be of use when thou art in need of a bird's-eye-view.",
			"Ah!  Use that spell to see landmarks from above.",
			"Those words can lift thine eyes above thy head, and see the land around thee."	
		}
		#Key "*Uus Jux*" 
		{
			"Ah, yes!  Thou canst trip up thy opponents and make tem easier to hit!",
			"Use that spell to cause thy enemies to miss when they strike at thee or another.",
			"Ah!  A spell to make one clumsier!  It can be useful!" 
		}
		#Key "*In Mani Ylem*" 
		{
			"Why, 'tis a spell to create food to fill one's plate.",
			"A useful spell to feed the hungry.",
			"The ability to create food from the ether is one that sets a mage apart from the common people."
		}
		#Key "*Rel Wis*" 
		{
			"Why, 'tis the spell that can create morons!",
			"Aye, thou can certainly drain the intelligence straight out of a person with those words.",
			"The stupid-spell!  With some skill, thy opponents won't know enough anymore to curse thee." 
		}
		#Key "*In Mani*" 
		{
			"Healer, heal thyself!", 
			"A useful spell for reducing the damage inflicted upon thee by this hostile world.",
			"Why, 'tis the magic that restores one's health.",
			"'Tis very useful on the battlefield to know how to heal.",
			"Yes, I know that one well.  Any injured man or woman can tell thee how crucial that spell is!", 
			"Thou dost refer to the spell of healing."
		}
		#Key "*In Lor*" 
		{
			"Why, 'tis the spell that illuminates an area.",
			"Ah, the spell that creates a shining light to brighten one's evening.",
			"A useful spell for combating the dark of night.",
			"Why, 'tis a fine spell for creating a source of light.",
			"Ah, the very words that call forth a source of brilliant light.",
			"Thou dost speak of the magics involved in fabricating an object of luminescence."	

		}
		#Key "*In Por Ylem*" 
		{
			"Strike at thine enemies from a distance!",
			"Yes, that spell can inflict some wounds while keeping thee out of the arc of the sword.",
			"A useful spell for combat!."	
		}
		#Key "*Des Mani*" 
		{
			"Yes, thou can use that spell to temporarily weaken thy opponent.",
			"Take the strength right out of a person!",
			"Makes the warrior as weak as a baby."
		}
		#Key "*teleport*" 
		{
			"haha go ahead and use the ether... careful about the curse!",
			"teleporting is dangerous, go ahead though im sure youll be fine.",
			"Pfft! go and fry your brain if you wish!"
		}
		#Key "*curse*" 
		{
			"Damn Malaka and his curse...",
			"The curse has helped horse vendors.. it's not all that bad. ",
			"Alas, i do not know of a cure for this curse... ."
		}
		#Key "*gate*" 
		{
			"haha go ahead and use the ether... careful about the curse!",
			"teleporting is dangerous, go ahead though im sure youll be fine.",
			"Pfft! go and fry your brain if you wish!"
		}
	}
}

