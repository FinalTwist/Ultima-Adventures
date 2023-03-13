// Shipwright function
//
// Keywords:
// job, ship, boat, vessel, sail
//
//
// 
// - cwm


#Fragment Britannia, Job, Britannia_Shipwright 
{

#Sophistication High 
{
	#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" 
	{
		#Attitude Wicked 
		{
			"I'm a shipwright.",
			"I build ships."
		}
		#Attitude Neutral 
		{
			"I'm a shipwright.",
			"I design seagoing vessels and supervise their construction."
		}
		#Attitude Goodhearted 
		{
		"I'm a shipwright.",
		"I build ships to explore the oceans of Britannia."
		}
	}
	#KEY "*ship*" "*vessel*" "*sail*" 
	{
		#Attitude Wicked 
		{
			"I build several kinds of ships, and I'm very busy, so please leave.",
			"People don't seem to get it... don't put my ships in rivers. They're OCEAN-GOING ships. An' don't
set 'em in the water near rocks, either. Sure way to lose a new boat.",
			"I can build any sort of ship for those that can afford it."
		}
		#Attitude Neutral 
		{
			"I've worked on ships that have gone everywhere in the known world.",
			"I should tell thee... Don't launch a ship near rocks or in a river.  that would be bad.",
			"Ship making does keep one busy, so if thou will excuse me...[Leave]"
		}
		#Attitude Goodhearted 
		{
			"I can build any kind of ship found on the sea.",
			"It would behoove thee to avoid rivers and rocky areas when thou dost launch a new vessel. Keep that
in mind.",
			"Most of my ships are made for the kingdom or a rich noble."
		}
	}
	#KEY "*Sextant*" 
	{
		"Thou can find a sextant at most provisioner's shops.",
		"Provisioners tend to carry sextants, I believe.",
		"Look to a provisioner for a sextant."
	}
	#KEY "*Boat*" 
	{
		#Attitude Wicked 
		{
			"Dost thou not know the difference between a ship and a mere boat, fool?",
			"Boat? BAH! [Leave]."
		}
		#Attitude Neutral 
		{
			"I don't build boats, I build ships.",
			"If thou wantest a boat, talk to a fisherman."
		}
		#Attitude Goodhearted 
		{
			"I build ships, friend, not boats.",
			"A boat is carried by a ship. A ship is made to cross the ocean ."
		}
	}
}
#Sophistication Medium 
{
	#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" 
	{
		#Attitude Wicked 
		{
			"I'm a shipwright.",
			"I build ships."
		}
		#Attitude Neutral 
		{
			"I'm a shipwright.",
			"I design seagoing vessels and supervise their construction."
		}
		#Attitude Goodhearted 
		{
			"I'm a shipwright.",
			"I build ships to sail the oceans of Britannia."
		}
	}
	#KEY "*ship*" "*vessel*" "*sail*" 
	{
		#Attitude Wicked 
		{
			"I build many kinds of ships, and I'm very busy, so go away.",
			"I tell folks over and over... don't launch a ship near rocks or in rivers. They get stuck that
way.",
			"I can build any sort of ship for those that can afford it."
		}
		#Attitude Neutral 
		{
			"I've worked on ships that have gone everywhere in the known world.",
			"I should warn thee, in case thou dost want to buy thy own ship... It would be best to launch it off
a dock. It could get stuck in rocks or in rivers.",
			"Ship making does keep one busy, so if thou wilt excuse me...[Leave] "
		}
		#Attitude Goodhearted 
		{
			"I can build any kind of ship found on the sea.",
			"Try to remember to launch only where thou art certain it's safe. And NEVER launch in a river!",
			"Most of my ships are made for the kingdom or a rich noble."
		}
	}
	#KEY "*Sextant*" 
	{
		"Look for a sextant in a provisioner's shop. Not here.",
		"Sextants are sold by the provisioner.",
		"Go to the provisioner for a sextant. Thou'lt find one there, more than likely."
	}
	#KEY "*Boat*" 
	{
		#Attitude Wicked 
		{
			"Dost thou not know the difference between a ship and a boat, fool?",
			"Boat? BAH! [Leave]."
		}
		#Attitude Neutral 
		{
			"I don't build boats, I build ships.",
			"If thou wantest a boat, talk to a fisherman."
		}
		#Attitude Goodhearted 
		{
			"I build ships, friend, not boats.",
			"A boat is carried by a ship. A ship is made to cross the ocean ."
		}
	}
}
#Sophistication Low 
{
	#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" 
	{
		#Attitude Wicked 
		{
			"I'm a shipwright.",
			"I build ships."
		}
		#Attitude Neutral 
		{
			"I'm a shipwright.",
			"I make seagoin' vessels and make sure they're built right."
		}
		#Attitude Goodhearted 
		{
			"I'm a shipwright.",
			"I build ships to go cross the oceans."
		}
	}
	#KEY "*ship*" "*vessel*" "*sail*" 
	{
		#Attitude Wicked 
		{
			"I build all kinds o' ships, and I'm real busy, so go away.",
			"Never launch a boat in a river. Dumb thing to do!",
			"I can build any sort of ship for them that can afford it. Just so's they don't launch it in a
river. Easy way to get a ship stuck."
		}
		#Attitude Neutral 
		{
			"I've worked on ships that have gone everywhere in the known world.",
			"Watch where thou'rt launchin' a ship for the first time. Off the docks is good, but a river's RIGHT
OUT!",
			"Ship makin' keeps me busy, so if thou'lt pardon me...[Leave]"
		}
		#Attitude Goodhearted 
		{
			"I can build any kind of ship found on the sea.",
			"Watch where thou launches a boat. Avoid the rivers an' the rocky shores. It'll get stuck.",
			"Most of my ships are made for the kingdom - or a rich noble."
		}
	}
	#KEY "*sextant*" 
	{
		"The provisioner's got a sextant. I think.",
		"Uh... I think a provisioner I saw once got them sextants.",
		"Go over an' try the provisioner's shop. Sometimes there'll be sextants there."
	}
	#KEY "*Boat*" 
	{
		#Attitude Wicked 
		{
			"Does thou not know the difference 'tween a ship and a boat, fool?",
			"Boat? BAH! [Leave]."
		}
		#Attitude Neutral 
		{
			"I don't build boats, I build ships.",
			"If thou wants a boat, talk to a fisherman."
		}
		#Attitude Goodhearted 
		{
			"I build ships, friend, not boats.",
			"A boat's carried by a ship. A ship's made to cross the ocean ."
		}
	}
}
}

