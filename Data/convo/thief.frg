//Britannia Thief Fragment
//Notes: This is used for general information pertaining to all Britannian Thieves
//Current Keyword List: thief(1/3), authorities, guards, soldiers, Lord British's fork,
//                      (gold, silver, copper(2/3))
//Additional Keywords:  stole, ruffian, guard, adventurer, riches, treasure, lock,
//				    steal, fork, Guild
//Revision Date: 3/15/96
//Author: Kristen Koster

#Fragment Britannia, Job, Britannia_Thief 
{
	#Sophistication High 
	{
		#Key "*gold*", "*silver*", "*copper*" 
		{
			#Attitude Wicked 
			{
				"A fine pile of %0 would suit me well, and some of thine would be fine.",
        			"I used to have a chest filled with %0,.Ahh...how quickly it goes.",
        			"How about giving me some of thine, in the spirit of generosity."
			}
			#Attitude Belligerent 
			{
        			"As if I would share my acquisitions with the likes of thee.",
				"I doth make frequent visits to the bank, but methinks they frown upon my taking as more than is mine.",
				"I would be much happier if I had more coin."
			}
			#Attitude Neutral 
			{
				"I would not object if thou gavest me the sum of thy %0.",
				"Gold, silver and copper are always plentiful if thou knowest where to look. An oft overlooked receptacle of these is, surprisingly, the backpacks of companions."
			}
			#Attitude Kindly 
			{
				"What a marvelous invention--the coin. Thou canst take thy %0 with thee, and the sparkle when the light doth fall upon a mound of them is most beautiful.",
 				"Collecting %0 coins hath been a pastime of mine for quite a while. Dost thou know that one may discover them in the most unlikely places, such as the pockets of supposed beggars?"
			}
			#Attitude Goodhearted 
			{
				"Gold, silver, copper. It all feels delectable when it slips through thy fingers back into thy treasury.",
				"What a pleasant chink %0 makes against %0 when first dropped in thy pocket. Especially someone else's %0.",
        			"Sooner or later all %0 finds its way across my palm."
			}
		}
		#KEY "*skill*" "*lock*" "*trap*" "*hide*"  
		{
			#Attitude Wicked 
			{
				"Get thee away from me. I have no reason to teach thee anything, though I know quite a bit",
				"I don't need to show thee anything!",
				"Oh yes, I know many things! Things that could help thee in thy journeys. Things I am loath to show thee."
			}
			#Attitude Neutral 
			{
				"If thou dost want to learn, I might be persuaded to teach thee about such things that would help thee in the cities. I would expect payment, thou knowest.",
				"Thou shouldst know something about the people that thou art likely to spend time with. Could be of use to thee. I could help thee some, I think.",
				"If thou dost need help with thy people skills, just ask me to teach thee and if I can, I will. For a small price.",
				"I can teach thee how to observe things around thee better. Thou could learn things about the people thou dost see every day."
			}
			#Attitude Goodhearted 
			{
				"I'd be happy for thee to learn what I know about surviving in towns. Just give me a few coins.",
				"Thou can always learn new things, my friend. I could show thee some things, for instance, about the observation of people that could help thee. For some small amount of money, of course.",
				"I can teach thee things that could help thee when thou art locked out. I would need thee to pay me for my time, however."
			}
		}	
		#Key "*fork*Lord British*", "*Lord British*fork*" 
		{
 			#Attitude Wicked, Belligerent 
			{
				"'Twas only a bloody fork! Ahem. Must thou remind me of that most unfortunate evening?",
				"Ha! A most generous lord. Indeed.", 
				"I think Lord British is far to enamoured with the idea of MINE."
			}
			#Attitude Neutral, Kindly, Goodhearted 
			{
				"Supping with his Lordship one night, I pocketed his fork, quite by accident."
			}
		}
		#Key "*authorities*" "*guard*" "*soldier*" 
		{
			#Notoriety Infamous, Outlaw 
			{
				"Mayhaps thou shouldst seek the help of thy Guild as I have heard that the %0 are seeking thee, _Name_. That is to say, if thy Guild still claims thee.",
				"I couldst give thee advice on evading the _Town_ %0, but thou appearest quite experienced in avoidance of authorities.",
        			"The _Town_ %0 be no more than brutish louts. They retain not an inkling of sense about them. Thou shouldst have no difficulty evading them."
			}
			#Notoriety Anonymous, Known, Famous 
			{
				"Call the %0 if thou must. My day hath been ruined already.",
        			"Fools! The %0 hast plagued me for the past fortnight.",
        			"What makes thee certain the %0 will come when thou callest? Pets of thine, are they?"
			}
		}
		#Key "*thief*" 
		{
			#Notoriety Infamous, Outlaw 
			{
				"Aye. I have been called thief, rogue and rapscallion and some things far worse, as I am sure thou hast been. Let us not say that again for others might hear and recognize us."
			}
			#Notoriety Anonymous 
			{
				#Attitude Wicked, Belligerent 
				{
					"Aye, a complete unknown like thyself makes an excellent thief.",
          				"Why wouldst thou be interested in thieves? I cannot recommend it as a good profession.",
          				"I have not the time to answer the accusations of some traveller."
				}
				#Attitude Neutral, Kindly 
				{
					"Aye, a fair number of thieves are in _Town_. Thou shouldst be more cautious, $milord/milady$. Thou canst never identify them 'til too late.",
          				"I've been called worse. Beware friend, one cannot be too careful when it comes to one's gold.",
				}
				#Attitude Goodhearted 
				{
					"Where?! I have not seen many ruffians about except for the adventuring types.",
					"$Lord/Lady$, hast thou been robbed of thy worldly possessions?	A most terrible occurence. Hast thou notified the proper authorities?"
				}
			}
			#Notoriety Known, Famous 
			{
				"Thieves? In Britain? Surely not! The guards and heroes such as thee, are far too efficient.",
        			"There is no theiving in Britain, for all are afraid of	meeting thee--thou'rt well-known as an honorable $man/woman$."
      			}
    		}
	}
	#Sophistication Medium 
	{
		#Key "*gold*", "*silver*", "*copper*" 
		{
			#Attitude Wicked 
			{
        			"A heap of %0 would suit me fine.",
				"Everyone doth seem to have more %0 than me lately.",
        			"Couldst thou spare some %0 for a poor, unfortunate old #man/woman#?"
			}
			#Attitude Belligerent 
			{
				"And why should I share my hard earned %0 with the likes of thee?",
        			"Hast thou visited the bank? They have a plentiful supply.",
        			"I would be much happier if mine pockets contained more %0."
			}
			#Attitude Neutral 
			{
				"Free %0 is always preferred to a lack of %0.",
				"Hast thou visited the bank? They dost have a pentiful supply.",
				"Dost thou wish to give up thy %0 or share it 'round?",
				"Many places in _Town_ doth provide a near unending supply of %0. I always doth look first in the strongboxes."
			}
			#Attitude Kindly 
			{
				"Hast thou asked the mint if they dost have an excess?",
				"I have a good store of %0. But I wouldn't mind a share of thine.",
				"All donations of %0 are accepted or taken gladly."
			}
			#Attitude Goodhearted 
			{
				"I could sit counting %0 all day, into me own pockets, that is.",
				"It certainly doth ease the way into all sorts of interesting places, %0 does.",
				"Sooner or later a goodly bit of %0 finds its way into me strongbox."
			}
		}
		#KEY "*skill*" "*lock*" "*trap*" "*hide*"  
		{
			#Attitude Wicked 
			{
				"Get thee away from me. I have no reason to teach thee anything, though I know quite a bit",
				"I don't need to show thee anything!",
				"Oh yes, I know many things! Things that could help thee in thy journeys. Things I am loath to show thee."
			}
			#Attitude Neutral 
			{
				"If thou dost want to learn, I might be persuaded to teach thee about such things that would help thee in the cities. I would expect payment, thou knowest.",
				"Thou shouldst know something about the people that thou art likely to spend time with. Could be of use to thee. I could help thee some, I think.",
				"If thou dost need help with thy people skills, just ask me to teach thee and if I can, I will. For a small price.",
				"I can teach thee how to observe things around thee better. Thou could learn things about the people thou dost see every day."
			}
			#Attitude Goodhearted 
			{
				"I'd be happy for thee to learn what I know about surviving in towns. Just give me a few coins.",
				"Thou can always learn new things, my friend. I could show thee some things, for instance, about the observation of people that could help thee. For some small amount of money, of course.",
				"I can teach thee things that could help thee when thou art locked out. I would need thee to pay me for my time, however."
			}
		}
    		#Key "*fork*Lord British*", "*Lord British*fork*" 
		{
			"'Twas just a fork! It's not like he doesn't have a drawer full of them.",
			"I had heard he was a generous lord. Thou wouldst think he could spare just one.",
			"Wouldn't think he would miss it that much, truly.",
      			"I was imprisoned for nearly a year for borrowing one of his Lordship's forks a bit too long once."
		}
		#Key "*authorities*" "*guards*" "*soldiers*" 
		{
			#Notoriety Infamous, Outlaw 
			{
				"The %0 here in _Town_ are dedicated to their jobs. I have heard them mention thy name while eavesdropping 'pon their briefing meetings.",
				"The %0 be not bright, but they seem to keep the most notorious outlaws like thee out of _Town_.",
        			"Methinks the %0 doth take their jobs too seriously some days. Especially the days when they're looking for me."
			}
			#Notoriety Anonymous, Known, Famous 
			{
				#Attitude Wicked, Belligerent 
				{
					"Don't call the %0! They will have my head for sure.",
					"Fool! The %0 have plagued me for the past fortnight.",
					"The way my luck runneth lately, the %0 wilst have caught up with me by sundown.",	
				}
				#Attitude Neutral, Kindly, Goodhearted 
				{
					"The %0 have not caught me yet!",
         				 "Why doth everyone speak of the %0 as if they were necessary?",
					"The %0 have not caught me yet!"
				}
			}
		}
  	}
	#Sophistication Low 
	{
		#Key "*fork*Lord British*", "*Lord British*fork*" 
		{
			"Who woulda knowed he was so interested in keeping a fork?",
			"A fork? What would he want with another fork?",
			"I nicked one once while I was courtin' one of the kitchen maids. His Lordship nearly had me killed for't."
		}
		#KEY "*skill*" "*lock*" "*trap*" "*hide*"  
		{
			#Attitude Wicked 
			{
				"Get away. I got no reason to teach thee nothin'",
				"I don't got to show thee nothin'!",
				"Yeah, I know some things! Things I ain't wantin' to show thee."
			}
			#Attitude Neutral 
			{
				"If thou wants to learn, I could teach about things that'd help in the city. I'd expect to be paid, o' course.",
				"Thou should know more about people. I could teach thee some things, I think.",
				"If thou needs help with some skills, just ask me, and if I can, I'll teach. For a small price.",
				"I can teach about locks. I, um, used to know a tinker."
			}
			#Attitude Goodhearted 
			{
				"Thou could learn what I know about towns. Just give me a few coins.",
				"Thou can always learn some people skills that could help thee. I may be willin' to show thee.",
				"I can teach thee things for when thou'rt locked out. I'd need payment, though."
			}
		}
		#Key "*gold*", "*silver*", "*copper*" 
		{
			"I LIKES %0.",
      			"Thou knows, %0 just calls to me... like it wants to visit me pockets...",
      			"What, thou got some to give me?"
    		}
    		#Key "*authorities*", "*guards*", "*soldiers*" 
		{
			"Where? Where?",
      			"Guards? Where? Oh no, they'll catch me for sure!",
      			"Guards, faugh!"
    		}
	}
}
//
// End of Fragment
//

