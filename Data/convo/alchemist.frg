// Alchemist function
//
// Keywords:
// job, alchem*, magic, mortar, pestle, vial, potion, reagent
//
//
// 
// - cwm


#Fragment Britannia, Job, Britannia_Alchemist 
{
	#Sophistication High 
	{
                #KEY "*job*", "*what*do*do*", "*profession*",  "*occupation*"   
		{
                        #Attitude Wicked 
			{
                                "I'm an alchemist.",
                                "I am a student of the alchemical arts.",
				"I practice the alchemical arts."
                        }
                        #Attitude Neutral 
			{
                                "I am an alchemist.",
                                "I study the ancient arts of alchemy."
                        }
                        #Attitude Goodhearted 
			{
                                "I am a practitioner of alchemy.",
                                "I have devoted my life to the alchemical arts."
                        }
                }
                #KEY "*alchem*" 
		{
                        #Attitude Wicked 
			{
                                "The secrets of alchemy are secret because we do not discuss them with outsiders. Good day.",
                                "I sell potions; I don't sell answers to idle questions.",
				"Years I've spent experimenting and learning the things I practice. I could not tell thee my secrets. 'Tis not to be learned in a day."
                        }
                        #Attitude Neutral 
			{
                                "I'm afraid that I cannot discuss the details of my art with the uninitiated.",
                                "Yes, I'm an alchemist. No, I cannot turn lead into gold.",
				"I've spent far too many years gaining the knowledge that is so important to an alchemist... I shall not divulge it to thee here and now."
                        }
                        #Attitude Goodhearted 
			{
                                "The art of alchemy encompasses all the secrets of both life and death.",
                                "The first and most important alchemical text is read in thine own heart."
                        }
                }
		#KEY "*secret*" 
		{
                        #Attitude Wicked 
			{
                                "Get thee away, and do not ask me again. I shall reveal nothing to thee.",
                                "I will tell thee nothing about my art, good $sir/woman$.",
				"Years I've spent experimenting and learning the things I practice. I could not tell thee my secrets. 'Tis not to be learned by one such as thee!"
                        }
                        #Attitude Neutral 
			{
                                "I cannot discuss the secrets of alchemy!  Please, do not ask again!",
                                "Yes, turning lead to gold would be the highest achievement of my art. Sadly, I haven't gotten it quite right yet",
				"I can tell thee nothing of my art. 'Tis against all guild rules."
                        }
                        #Attitude Goodhearted 
			{
                                "I can tell thee nothing of our secrets.  Sorry.",
                                "The secrets are mine to keep.  Not to share."
                        }
                }
                #KEY "*magic*", "*mage*"   
		{
                        #Attitude Wicked 
			{
                                "I don't do magic, fool. I am an alchemist!",
				"Thou art thinking of a mage, idiot! Alchemy is a far more sophisticated art!"
                        }
                        #Attitude Neutral 
			{
                                "Eh, magic? 'Tis fine for some, but I prefer a higher art.",
				"Magic? I care not for such a base thing!"
                        }
                        #Attitude Goodhearted 
			{
                                "An alchemist and a mage are as different as water and mercury, though they do share certain properties."
                        }
                }
		#KEY "*skill*"  
		{    
			"Dost thou need to improve thy alchemy? Sure, I might be able to teach thee.",
			"Thou dost need some bit of skill to create potions. I may be able to teach thee some things.",
			"Well, I could show thee how to improve thy skills and make thyself some potions, but don't go and undercut my prices."
		}
                #KEY "*mortar*" "*pestle*" "*vial*" 
		{
                        #Attitude Wicked 
			{
                                "I could sell thee a mortar and pestle, or some unused vials.",
				"I could sell thee some things... maybe a vial?  A mortar and pestle?"
                        }
                        #Attitude Neutral 
			{
                                "I have an unused mortar and pestle I could part with, and also some empty vials I could sell.",
				"I can sell thee some of the tools of my craft. Dost thou need a vial?"
                        }
                        #Attitude Goodhearted 
			{
                                "I don't normally sell apparatus, but I do have some empty vials, and maybe a mortar and pestle I could let thee have."
                        }
                }
                #KEY "*potion*" 
		{
                        #Attitude Wicked 
			{
                                "Ah, so it's potions thou art needing? Why didn't thou say so? Sit. Look. Buy.",
				"The blue potions help to make a person quite agile.",
				"Don't drink the black potion. That's to give to one's enemies.",
				"The yellow potions can heal thy wounds.",
				"The red potions are quite refreshing.",
				"Drink some of the white potions to temporarily increase thy strength.",
				"Remember: green is poison.",
				"Insomniacs will like my orange potions.",
				"Stand clear of the purple potion if thou art wanting to use it.",
                                "Of course I have potions. All the potions thou couldst ever want. For a price, of course."
                        }
                        #Attitude Neutral 
			{
                                "I brew potions of all known types, potency unconditionally guaranteed.",
				"The blue potions help to make a person quite agile.",
				"Don't drink the black potion. That's to give to one's enemies.",
				"The yellow potions can heal thy wounds.",
				"The red potions are quite refreshing.",
				"Drink some of the white potions to temporarily increase thy strength.",
				"Remember: green is poison.",
				"Insomniacs will like my orange potions.",
				"Stand clear of the purple potion if thou art wanting to use it.",
                                "My customers regard my potions most highly. How can I serve thee?"
                        }
                        #Attitude Goodhearted 
			{
                                "Potions, yes. A minor facet of mine art, but one must keep one's hand in.",
				"The blue potions help to make a person quite agile.",
				"Don't drink the black potion. That's to give to one's enemies.",
				"The yellow potions can heal thy wounds.",
				"The red potions are quite refreshing.",
				"Drink some of the white potions to temporarily increase thy strength.",
				"Remember: green is poison.",
				"Insomniacs will like my orange potions.",
				"Stand clear of the purple potion if thou art wanting to use it.",
                                "Base commerce is beneath my dignity, but I do brew up the odd potion. If thou dost need something, I should be glad to help, if thou canst offer a minimal reimbursement for my time and supplies."
                        }
                }
		#KEY "*black pearl*", "*red potion*", "*refresh*"
		{
			"To make a refreshing red-colored potion, all thou dost need are some black pearls.",
			"The red potion will help restore thy energy, if thou art fatigued.",
			"My red potions are made from black pearls."
		}
		#KEY "*garlic*", "*black potion*", "*blind*"
		{
			"It takes garlic to create a good potion of blindness.", 
			"The black potion is easily made from garlic.", 
			"The black potion, the one that causes blindness, is made from garlic."
		}
		#KEY "*ginsing*", "*yellow potion*", "*heal*"
		{
			"Adequate use of ginsing will make a yellowish potion that will speed thy healing.",
			"I use ginsing for my yellow potion.",
			"If thou art injured, the yellow potion will help thee to heal. Ginsing is its main ingredient."
		}
		#KEY "*bloodmoss*", "*blue potion*", "*agility*"
		{
			"Bloodmoss yields the best potion for increasing thy agility. 'Tis easy to make.",
			"The blue potion is made from bloodmoss.",
			"Thou shalt move more quickly if thou dost partake of the blue potion."
		}
		#KEY "*nightshade*", "*green potion*", "*poison*"
		{
			"Avoid drinking the green potion. It could kill thee slowly.",
			"The deadly reagent nightshade is all that goes into the green potion.",
			"Thou can make thy own poison, if thou dost have the skill and the nightshade."
		}
		#KEY "*mandrake*", "*white potion*", "*strength*"
		{
			"Use some mandrake to make thy own strength-enhancing potions.",
			"The white potion is for the purity of strength.",
			"Use of mandrake results in a white concoction that temporarily increases thy strength."
		}
		#KEY "*sulphurous ash*", "*purple potion*", "*explode*", "*explosion*"
		{
			"The purple stuff is extremely volatile. Be careful with it.",
			"Careful use of sulphurous ash will produce a nice, purple explosive.",
			"Sulphurous ash, when made into a potion, proves a very volatile liquid."
		}
		#KEY "*spider silk*", "*orange potion*", "*sleep*"
		{
			"My orange potions will put people right to sleep, whether they wish to be or not.",
			"Spider silk is used in the orange potion.",
			"Thou can certainly make thy own sleeping potion. A little spider silk is all it takes."
		}
                #KEY "*reagents*" 
		{
                        #Attitude Wicked 
			{
                                "I'll buy any reagents thou hast. It's far better than gathering mine own.",
                                "I don't sell reagents, but I sometimes buy them."
                        }
                        #Attitude Neutral 
			{
                                "Reagents are essential to alchemy. Dost thou have any thou could, perhaps, part with?",
                                "If thou art selling reagents, perhaps we can make a deal."
                        }
                        #Attitude Goodhearted 
			{
                                "Ah, reagents, the bounty of nature. Wouldst thou have any to sell?",
                                "I usually gather mine own reagents, but if thou hast some thou couldst spare, perhaps I could make thee an offer."
                        }
                }
        }
        #Sophistication Medium 
	{
                #KEY "*job*", "*what*do*do*", "*profession*",  "*occupation*"    
		{
                        #Attitude Wicked 
			{
                                "I'm an alchemist.",
                                "I am a student of the alchemical arts."
                        }
                        #Attitude Neutral 
			{
                                "I am an alchemist.",
                                "I study the ancient arts of alchemy."
                        }
                        #Attitude Goodhearted 
			{
                                "I am a practitioner of alchemy.",
                                "I have devoted my life to the alchemical arts."
                        }
                }
                #KEY "*alchem**" 
		{
                        #Attitude Wicked 
			{
                                "The secrets of alchemy are secret because we do not discuss them with outsiders. Good day.",
                                "I sell potions; I don't sell answers to idle questions."
                        }
                        #Attitude Neutral 
			{
                                "I'm afraid that I cannot discuss the details of my art with the uninitiated.",
                                "Yes, I'm an alchemist. No, I can't turn lead into gold."
                        }
                        #Attitude Goodhearted 
			{
                                "The art of alchemy encompasses all the secrets of both life and death.",
                                "The first and most important alchemical text is read in thine own heart."
                        }
                }
		#KEY "*secret*" 
		{
                        #Attitude Wicked 
			{
                                "Get thee away, and do not ask me again. I shall reveal nothing to thee.",
                                "I will tell thee nothing about my art, good $sir/woman$.",
				"Years I've spent experimenting and learning the things I practice. I could not tell thee my secrets. 'Tis not to be learned by one such as thee!"
                        }
                        #Attitude Neutral 
			{
                                "I cannot discuss the secrets of alchemy!  Please, do not ask again!",
                                "Yes, turning lead to gold would be the highest achievement of my art. Sadly, I haven't gotten it quite right yet",
				"I can tell thee nothing of my art. 'Tis against all guild rules."
                        }
                        #Attitude Goodhearted 
			{
                                "I can tell thee nothing of our secrets.  Sorry.",
                                "The secrets are mine to keep.  Not to share."
                        }
                }
                #KEY "*magic*", "*mage*"   
		{
                        #Attitude Wicked 
			{
                                "I don't do magic, fool. I am an alchemist!",
				"Thou art thinking of a mage, idiot! Alchemy is a far more sophisticated art!"
                        }
                        #Attitude Neutral 
			{
                                "Eh, magic? 'Tis fine for some, but I prefer a higher art.",
				"Magic? I care not for such a base thing!"
                        }
                        #Attitude Goodhearted 
			{
                                "An alchemist and a mage are as different as water and mercury, though they do share certain properties."
                        }
                }
		#KEY "*skill*"  
		{    
			"Dost thou need to improve thy alchemy? Sure, I can help thee.",
			"Thou dost need some bit of skill to create potions. I can teach thee some things.",
			"Well, I could show thee how to improve thy skills and make thyself some potions, but don't go and undercut my prices."
		}
                #KEY "*mortar*" "*pestle*" "*vial*" 
		{
                        #Attitude Wicked 
			{
                                "I could sell thee a mortar and pestle, or some unused vials.",
				"I could sell thee some things... maybe a vial?  A mortar and pestle?"
                        }
                        #Attitude Neutral 
			{
                                "I have an unused mortar and pestle I could part with, and also some empty vials I could sell.",
				"I can sell thee some of the tools of my craft. Dost thou need a vial?"
                        }
                        #Attitude Goodhearted 
			{
                                "I don't normally sell apparatus, but I do have some empty vials, and maybe a mortar and pestle I could let thee have."
                        }
                }
                #KEY "*potion*" 
		{
                        #Attitude Wicked 
			{
                                "Ah, so it's potions thou art needing? Why didn't thou say so? Sit. Look. Buy.",
				"The blue potions help to make a person quite agile.",
				"Don't drink the black potion. That's to give to one's enemies.",
				"The yellow potions can heal thy wounds.",
				"The red potions are quite refreshing.",
				"Drink some of the white potions to temporarily increase thy strength.",
				"Remember: green is poison.",
				"Insomniacs will like my orange potions.",
				"Stand clear of the purple potion if thou art wanting to use it.",
                                "Of course I have potions. All the potions thou couldst ever want."
                        }
                        #Attitude Neutral 
			{
                                "I brew potions of all known types, potency unconditionally guaranteed.",
				"The blue potions help to make a person quite agile.",
				"Don't drink the black potion. That's to give to one's enemies.",
				"The yellow potions can heal thy wounds.",
				"The red potions are quite refreshing.",
				"Drink some of the white potions to temporarily increase thy strength.",
				"Remember: green is poison.",
				"Insomniacs will like my orange potions.",
				"Stand clear of the purple potion if thou art wanting to use it.",
                                "My customers regard my potions most highly. How can I serve thee?"
                        }
                        #Attitude Goodhearted 
			{
                                "Potions, yes. A minor facet of my art, but one must keep one's hand in.",
				"The blue potions help to make a person quite agile.",
				"Don't drink the black potion. That's to give to one's enemies.",
				"The yellow potions can heal thy wounds.",
				"The red potions are quite refreshing.",
				"Drink some of the white potions to temporarily increase thy strength.",
				"Remember: green is poison.",
				"Insomniacs will like my orange potions.",
				"Stand clear of the purple potion if thou art wanting to use it.",
                                "Base commerce is beneath my dignity, but I do brew up the odd potion. If thou dost need something, I should be glad to help, if thou canst offer a minimal reimbursement for my time and supplies."
                        }
                }
		#KEY "*black pearl*", "*red potion*", "*refresh*"
		{
			"To make a refreshing red-colored potion, all thou dost need are some black pearls.",
			"The red potion will help restore thy energy, if thou art fatigued.",
			"My red potions are made from black pearls."
		}
		#KEY "*garlic*", "*black potion*", "*blind*"
		{
			"It takes garlic to create a good potion of blindness.", 
			"The black potion is easily made from garlic.", 
			"The black potion, the one that causes blindness, is made from garlic."
		}
		#KEY "*ginsing*", "*yellow potion*", "*heal*"
		{
			"Adequate use of ginsing will make a yellowish potion that will speed thy healing.",
			"I use ginsing for my yellow potion.",
			"If thou art injured, the yellow potion will help thee to heal. Ginsing is its main ingredient."
		}
		#KEY "*bloodmoss*", "*blue potion*", "*agility*"
		{
			"Bloodmoss yields the best potion for increasing thy agility. 'Tis easy to make.",
			"The blue potion is made from bloodmoss.",
			"Thou shalt move more quickly if thou dost partake of the blue potion."
		}
		#KEY "*nightshade*", "*green potion*", "*poison*"
		{
			"Avoid drinking the green potion. It could kill thee slowly.",
			"The deadly reagent nightshade is all that goes into the green potion.",
			"Thou can make thy own poison, if thou dost have the skill and the nightshade."
		}
		#KEY "*mandrake*", "*white potion*", "*strength*"
		{
			"Use some mandrake to make thy own strength-enhancing potions.",
			"The white potion is for the purity of strength.",
			"Use of mandrake results in a white concoction that temporarily increases thy strength."
		}
		#KEY "*sulphurous ash*", "*purple potion*", "*explode*", "*explosion*"
		{
			"The purple stuff is extremely volatile. Be careful with it.",
			"Careful use of sulphurous ash will produce a nice, purple explosive.",
			"Sulphurous ash, when made into a potion, proves a very volatile liquid."
		}
		#KEY "*spider silk*", "*orange potion*", "*sleep*"
		{
			"My orange potions will put people right to sleep, whether they wish to be or not.",
			"Spider silk is used in the orange potion.",
			"Thou can certainly make thy own sleeping potion. A little spider silk is all it takes."
		}
                #KEY "*reagents*" 
		{
                        #Attitude Wicked 
			{
                                "I'll buy any reagents thou hast. It's far better than gathering mine own.",
                                "I don't sell reagents, but I sometimes buy them."
                        }
                        #Attitude Neutral 
			{
                                "Reagents are essential to alchemy. Dost thou have any thou could, perhaps, part with?",
                                "If thou art selling reagents, perhaps we can make a deal."
                        }
                        #Attitude Goodhearted 
			{
                                "Ah, reagents, the bounty of nature. Wouldst thou have any to sell?",
                                "I usually gather my own reagents, but if thou hast some thou couldst spare, perhaps I could make thee an offer."
                        }
                }
        }
        #Sophistication Low 
	{
                #KEY "*job*", "*what*do*do*", "*profession*",  "*occupation*"    
		{
                        #Attitude Wicked 
			{
                                "I'm an alchemist.",
                                "I am a student of the alchemical arts."
                        }
                        #Attitude Neutral 
			{
                                "Alchemy. I do alchemy.",
                                "I study alchemy."
                        }
                        #Attitude Goodhearted 
			{
                                "I do alchemy.",
                                "I practice alchemy."
                        }
                }
                #KEY "*alchem**" 
		{
                        #Attitude Wicked 
			{
                                "I can't talk about alchemy with outsiders. Good day.",
                                "I sell potions, not answers to questions."
                        }
                        #Attitude Neutral 
			{
                                "I'm afraid that I can't discuss my art.",
                                "Yes, I'm an alchemist. No, I can't turn lead into gold."
                        }
                        #Attitude Goodhearted 
			{
                                "The art of alchemy has the secrets of life and death ... somewhere in it.",
                                "The first and most important alchemy text is read inside thee."
                        }
                }
		#KEY "*secret*" 
		{
                        #Attitude Wicked 
			{
                                "Go 'way, and don't ask again!",
                                "I will say nothing 'bout what I do!",
				"Art thou serious? I won't share my secrets with thee!"
                        }
                        #Attitude Neutral 
			{
                                "I can't discuss the secrets of alchemy!  Don't ask me again!",
                                "I have no secrets to share.",
				"I can tell thee nothing of my art."
                        }
                        #Attitude Goodhearted 
			{
                                "I can tell thee nothing of our secrets. Sorry.",
                                "The secrets are mine. I can't share them."
                        }
                }
                #KEY "*magic*", "*mage*"  
		{
                        #Attitude Wicked 
			{
                                "I don't do magic. I'm an alchemist!"
                        }
                        #Attitude Neutral 
			{
                                "Eh, magic? 'Tis fine for some, but I prefer another art."
                        }
                        #Attitude Goodhearted 
			{
                                "An alchemist and a mage are as different as ... well, as two people can be!"
                        }
                }
		#KEY "*skill*"  
		{    
			"Thou needs to improve thy alchemy, huh? Sure, I can help thee.",
			"Thou needs some skill to make potions. I can teach thee some. For a price.",
			"Well, I could show thee how to improve thy skills and make thyself some potions, but don't go undercuttin' me. Business is bad enough as it is."
		}
                #KEY "*mortar*" "*pestle*" "*vial*" 
		{
                        #Attitude Wicked 
			{
                                "I could sell thee a mortar and pestle, or some unused vials."
                        }
                        #Attitude Neutral 
			{
                                "I have an unused mortar and pestle I could part with, and also some empty vials."
                        }
                        #Attitude Goodhearted 
			{
                                "I don't normally sell my things, but I do have some empty vials, and maybe a mortar and pestle I could let thee have."
                        }
                }
                #KEY "*potion*" 
		{
                        #Attitude Wicked 
			{
                                "Ah, so it's potions thou'rt after? Just say so! Sit. Look. Buy.",
				"The blue potions'll help thee move better.",
				"Don't drink that black potion. That'll make thee blind.",
				"The yellow potions are for healin'.",
				"The red potions are really refreshing.",
				"Drink some of that white potion to get stronger for a while.",
				"Remember: green is poison.",
				"Orange will put thee, or someone else, right to sleep.",
				"Stand clear of the purple potion if thou art wantin' to use it.",
                                "'Course I got potions. All the potions thou could want!"
                        }
                        #Attitude Neutral 
			{
                                "I brew all types of potions.",
				"The blue potions'll help thee move better.",
				"Don't drink that black potion. That'll make thee blind.",
				"The yellow potions are for healin'.",
				"The red potions are really refreshing.",
				"Drink some of that white potion to get stronger for a while.",
				"Remember: green is poison.",
				"Orange will put thee, or someone else, right to sleep.",
				"Stand clear of the purple potion if thou art wantin' to use it.",
                                "My customers like my potions. How can I serve thee?"
                        }
                        #Attitude Goodhearted 
			{
                                "Potions? Yeah, I make potions.",
				"The blue potions'll help thee move better.",
				"Don't drink that black potion. That'll make thee blind.",
				"The yellow potions are for healin'.",
				"The red potions are really refreshing.",
				"Drink some of that white potion to get stronger for a while.",
				"Remember: green is poison.",
				"Orange will put thee, or someone else, right to sleep.",
				"Stand clear of the purple potion if thou art wantin' to use it.",
                                "I don't like sellin' my things, but I do brew up some potions. If thou need something, I can help, if thou can offer a few coins for my time and supplies."
                        }
                }
		#KEY "*black pearl*", "*red potion*", "*refresh*"
		{
			"Some black pearls, some skill... That's all thou needs to make the red potion.",
			"The red potion will help get some energy back, if thou'rt tired.",
			"Them red potions are made from black pearls."
		}
		#KEY "*garlic*", "*black potion*", "*blind*"
		{
			"It takes some garlic to make a good potion of blindness.", 
			"The black potion is made from garlic. Real easy to make.", 
			"The black potion, the one that causes blindness, is made from garlic."
		}
		#KEY "*ginsing*", "*yellow potion*", "*heal*"
		{
			"Ginsing will make a yellowish potion that'll speed thy healing.",
			"I use ginsing for my yellow potion.",
			"If thou'rt injured, the yellow potion will help thee to heal. Ginsing is what it's made of."
		}
		#KEY "*bloodmoss*", "*blue potion*", "*agility*"
		{
			"Bloodmoss will give thee the best potion for agility. 'Tis easy to make.",
			"The blue potion is made with bloodmoss.",
			"Thou will move faster if thou drinks the blue stuff."
		}
		#KEY "*nightshade*", "*green potion*", "*poison*"
		{
			"Don't drink the green potion. It'll kill thee real slow.",
			"Nightshade is all that goes in the green potion.",
			"Thou can make thy own poison, if thou got the skill and the nightshade."
		}
		#KEY "*mandrake*", "*white potion*", "*strength*"
		{
			"Use some mandrake to make some strength-helpin' potions.",
			"The white potion makes thee a bit stronger.",
			"Thou can use the mandrake and get some stuff that'll make thee stronger."
		}
		#KEY "*sulphurous ash*", "*purple potion*", "*explode*", "*explosion*"
		{
			"The purple stuff will blow up. Be careful with it.",
			"Sulphurous ash will make a nice, purple explosive. If it don't kill thee makin' it.",
			"Sulphurous ash can be a real explosive liquid."
		}
		#KEY "*spider silk*", "*orange potion*", "*sleep*"
		{
			"My orange potions will put people right to sleep, whether they want to be or not.",
			"Spider silk is used to make the orange potion.",
			"Thou can certainly make some sleepin' potion. A little spider silk is all thou needs."
		}
                #KEY "*reagents*" 
		{
                        #Attitude Wicked 
			{
                                "I'll buy any reagents thou hast. It's better than getting my own.",
                                "I don't sell reagents, but I sometimes buy them."
                        }
                        #Attitude Neutral 
			{
                                "Reagents are important in alchemy. Dost thou have any thou could, perhaps, part with?",
                                "If thou art selling reagents, perhaps we can make a deal."
                        }
                        #Attitude Goodhearted 
			{
                                "Ah, reagents! Got any to sell?",
                                "I usually gather my own reagents, but if thou hast some, perhaps I could make thee an offer."
                        }
                }
        }
}

