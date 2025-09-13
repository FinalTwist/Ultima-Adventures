// Tinker function
//
// Keywords:
// job, tinker, clock, sextant, machine, device, part, gear, spring, sticks
//
//
// 
// - cwm


#Fragment Britannia, Job, Britannia_Tinker 
{
	#Sophistication High 
	{
		#KEY "*job*" "*what*do*do*" "*profession*" "*occupation*"  
		{
			#Attitude Wicked 
			{
				"I'm a tinker.",
				"I'm a clockmaker."
			}
			#Attitude Neutral 
			{
				"I'm a tinker.",
				"I make clocks, sextants and other precision devices."
			}
			#Attitude Goodhearted 
			{
				"I craft precision machinery.",
				"I build sextants, clocks and similar instruments."
			}
		}
		#KEY "*tinker*"  
		{
			#Attitude Wicked 
			{
				"I can fix most machines.",
				"I make clockwork and other intricate mechnical things of such nature."
			}
			#Attitude Neutral 
			{
				"I build and repair clocks and similar machinery.",
				"I've always been fascinated by gears, springs and other small parts."
			}
			#Attitude Goodhearted 
			{
				"Tinker is perhaps the best word, but I find it trivializes what I do.",
				"I build precision machinery."
			}
		}
		#KEY "*clock*" "*sextant*" "*machine*" "*device*" 
		{
			#Attitude Wicked 
			{
				"I make clocks and sextants, and buy and sell used ones.",
				"Clocks, sextants... I can make or fix all kinds of things like that."
			}
			#Attitude Neutral 
			{
				"I do my best business in clocks and sextants.",
				"I make clocks and sextants, and also buy and repair used ones."
			}
			#Attitude Goodhearted 
			{
				"My clocks and sextants are completely reliable. Thou wilt always know where thou art, and what the time is.",
				"My clocks and sextants represent the very cutting edge of technology."
			}
		}
		#KEY "*part*", "*gear*", "*spring*" 
		{
			#Attitude Wicked 
			{
				"I can sell thee gears, springs and other parts, if thou wish."
			}
			#Attitude Neutral 
			{
				"I have some extra gears, springs and such that I could, perhaps, part with."
			}
			#Attitude Goodhearted 
			{
				"The gears, springs and other parts of my devices are meticulously handcrafted."
			}
		}
		#KEY "*sticks*" "*wood*" "*lumber*" 
		{
			#Attitude Wicked 
			{
				"I could use some extra wood, to make frames out of."
			}
			#Attitude Neutral 
			{
				"If thou has some lumber for sale, I could perhaps use some."
			}
			#Attitude Goodhearted 
			{
				"I use wood in some of my devices, and I don't have time to gather my own."
			}
		}
		#Key "*skill*"
		{
			"I already have an apprentice, but I could take a few minutes to teach thee some tricks of the trade.",
			"It takes years of practice to reach my level of skill, but thou could learn something in a short time.",
			"It will take a steady hand and a sure eye to be a tinker. If thou art willing to learn, then I can help thee.",
			"It would be a pleasure to teach thee, if thou art ready to learn."
		}
		#Key "*trade*"
		{
			"My trade is in the making clocks and sextants, and buy and sell used ones.",
			"Trade is always good for a clockmaker, such as I."
		}
	}
	#Sophistication Medium 
	{
		#KEY "*job*" "*what*do*do*"  "*profession*" "*occupation*"
		{
			#Attitude Wicked 
			{
				"I'm a tinker.",
				"I'm a clockmaker."
			}
			#Attitude Neutral 
			{
				"I'm a tinker.",
				"I make clocks, sextants and other precision devices."
			}
			#Attitude Goodhearted 
			{
				"I craft precision machinery.",
				"I build sextants, clocks and similar instruments."
			}
		}
		#KEY "*tinker*" 
		{
			#Attitude Wicked 
			{
				"I can fix most machines.",
				"I make clockwork and suchlike."
			}
			#Attitude Neutral 
			{
				"I build and repair clocks and similar machinery.",
				"I've always been fascinated by gears, springs and other small parts."
			}
			#Attitude Goodhearted 
			{
				"Tinker is such a common word. I find it trivializes what I do.",
				"I build precision machinery. What does a tinker do? Tink? Tinkle?."
			}
		}
		#KEY "*clock*" "*sextant*" "*machine*" "*device*" 
		{
			#Attitude Wicked 
			{
				" I make clocks and sextants, and buy and sell used ones.",
				"Clocks, sextants, I can make or fix all kinds of things like that."
			}
			#Attitude Neutral 
			{
				"I do my best business in clocks and sextants.",
				"I make clocks and sextants, and also buy and repair used ones."
			}
			#Attitude Goodhearted 
			{
				"My clocks and sextants are completely reliable. Thou wilt always know where thou art, and what the time is.",
				"My clocks and sextants represent the very cutting edge of technology."
			}
		}
		#KEY "*part*", "*gear*", "*spring*" 
		{
			#Attitude Wicked 
			{
				"I can sell thee gears, springs and other parts, if thou wish."
			}
			#Attitude Neutral 
			{
				"I have some extra gears, springs and such that I could, perhaps, part with."
			}
			#Attitude Goodhearted 
			{
				"The gears, springs and other parts of my devices are meticulously handcrafted."
			}
		}
		#KEY "*sticks*" "*wood*" "*lumber*" 
		{
			#Attitude Wicked 
			{
				"I could use some extra wood, to make frames out of."
			}
			#Attitude Neutral 
			{
				"If thou has some lumber for sale, I could perhaps use some."
			}
			#Attitude Goodhearted 
			{
				"I use wood in some of my devices, and I don't have time to gather my own."
			}
			}
			#Key "*skill*"
			{
				"I could take a few minutes to teach thee some tricks of the trade.",
				"It takes years of practice to reach my level of skill, but perhaps something could be learned in a short time.",
				"If thou art willing to learn, then I can help thee.",
				"It would be a pleasure to teach thee, if thou art ready to learn."
			}
			#Key "*trade*"
			{
				"My trade is in the making clocks and sextants, and buy and sell used ones.",
				"Trade is always good for a clockmaker, such as I."
			}
	}
	#Sophistication Low 
	{
		#KEY "*job*" "*what*do*do*"  "*profession*" "*occupation*"
		{
			#Attitude Wicked 
			{
				"I'm a tinker.",
				"I'm a clockmaker."
			}
			#Attitude Neutral 
			{
				"I'm a tinker.",
				"I make clocks, sextants and other stuff like that."
			}
			#Attitude Goodhearted 
			{
				"I'm a tinker.",
				"I build sextants, clocks and things."
			}
		}
		#KEY "*tinker*" 
		{
			#Attitude Wicked 
			{
				"I can fix most machines.",
				"I make clockwork and suchlike."
			}
			#Attitude Neutral 
			{
				"I build and repair clocks and similar such things.",
				"I've always been amazed by gears, springs and other small parts."
			}
			#Attitude Goodhearted 
			{
				"Tinker is such a wierd word. I don't think it describes what I do.",
				"I build precision things. What does a tinker do? Tink? Tinkle?."
			}
		}
		#KEY "*clock*" "*sextant*" "*machine*" "*device*" {
			#Attitude Wicked 
			{
				"I make clocks and sextants, and buy and sell used ones.",
				"Clocks, sextants, I can make or fix all kinds of things like that."
			}
			#Attitude Neutral 
			{
				"I do my best business in clocks and sextants.",
				"I make clocks and sextants, and also buy and repair used ones."
			}
			#Attitude Goodhearted 
			{
				"My clocks and sextants are really reliable. Thou wilt always know where thou art, and what the time is.",
				"My clocks and sextants are the very cutting edge of technology."
			}
		}
		#KEY "*part*", "*gear*", "*spring*" 
		{
			#Attitude Wicked 
			{
				"I can sell thee gears, springs and other parts, if thou wish't."
			}
			#Attitude Neutral 
			{
				"I got some extra gears, springs and such that I could part with."
			}
			#Attitude Goodhearted 
			{
				"The gears, springs and other parts of my things are real hard to make."
			}
		}
		#KEY "*sticks*" "*wood*" "*lumber*" 
		{
			#Attitude Wicked 
			{
				"I could use some extra wood, to make frames out of."
			}
			#Attitude Neutral 
			{
				"If thou has some lumber for sale, I could perhaps use some."
			}
			#Attitude Goodhearted 
			{
				"I use wood in some of my devices, and I don't have lotsa time to gather my own."
			}
			}
			#Key "*skill*"
			{
				"It'll be a bother, but I can help thee learn.",
				"Maybe I could teach thee somethin' in a short time.",
				"If thou'rt willing to learn, then I can help thee.",
				"If thou'rt ready to learn, I can help."
			}
			#Key "*trade*"
			{
				"I trade in the making of clocks and sextants, and I buy and sell used ones.",
				"Trade's always good for a clockmaker, like me."
			}
		}
	}
