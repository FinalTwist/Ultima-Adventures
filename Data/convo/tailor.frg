// tailor function
//
// Keywords:
// job, tailor, clothier, sew, cloth , cloth., cloth?, cloth!, thread, clothes, clothing, garment, shirt, pant, vest, dress, kilt, skirt, apron, shawl, robe, coat, cape
//
//
// 
// - cwm


#Fragment Britannia, Job, Britannia_Tailor 
{
	#Sophistication High 
	{
		#KEY "*job*", "*what*do*do*", "*occupation*", "*profession*" 
		{
			#Attitude Wicked 
			{
				"I'm a tailor.",
				"I make and sell clothing."
			}
			#Attitude Neutral 
			{
				"I'm a tailor.",
				"I make men's and women's clothing."
			}
			#Attitude Goodhearted 
			{
				"I'm a maker of fine garments.",
				"I am an artist of the needle and thread."
			}
		}
		#KEY "*tailor*" "*clothier*" 
		{
			#Attitude Wicked 
			{
				"I make clothes of all kinds.",
				"I make clothes for both men and women."
			}
			#Attitude Neutral 
			{
				"I make both sturdy traveling clothes and fancy dress.",
				"I make and sell a full line of garments."
			}
			#Attitude Goodhearted 
			{
				"Many fine ladies and gentlemen patronize me exclusively for their couture needs.",
				"I can fulfill all thy needs for plain and formal dress."
			}
		}
		#KEY "*sew*" 
		{
			#Attitude Wicked 
			{
				"I can sell thee a sewing kit so thou can mend thine own clothes."
			}
			#Attitude Neutral 
			{
				"Perhaps thou wouldst be interested in a sewing kit, to repair damaged clothing."
			}
			#Attitude Goodhearted 
			{
				"If thou knowest which end of a needle is which, thou wilt find my sewing kit quite handy."
			}
		}
		#KEY "*cloth *" "*cloth.*" "*cloth?*" "*cloth!*" "*thread*" 
		{
			#Attitude Wicked 
			{
				"I buy cloth and thread.",
				"If thou hast cloth or thread for sale, I would be happy to look it over."
			}
			#Attitude Neutral 
			{
				"I could use another source for cloth or thread.",
				"If thou art selling cloth or thread, I'm buying."
			}
			#Attitude Goodhearted 
			{
				"Decent cloth and thread is so hard to find these days.",
				"If thou dost have cloth or thread for sale, I could perhaps make thee an offer."
			}
		}
		#KEY "*clothes*" "*clothing*" "*garment*" "*shirt*" "*pant*" "*vest*" "*dress*" "*kilt*" "*skirt*" "*apron*" "*shawl*" "*robe*" "*coat*" "*cape*" {
			#Attitude Wicked 
			{
				"I can sell thee a garment to cover up whatever thou dost want covered.",
				"If thou dost need clothing, look over my stock and buy."
			}
			#Attitude Neutral 
			{
				"I have clothing of all sorts, for both men and ladies.",
				"Ankle to neck, I can meet all thy clothing needs."
			}
			#Attitude Goodhearted 
			{
				"I'm sure that thou can find any garment thou might wish in my stock.",
				"My stock of all types of garments is extensive."
			}
		}
		#Key "*skill*"
		{
			"I would be delighted to help thee learn more of my trade.",
			"Although it is a demanding skill, I can teach thee what thou needs to know.",
			"Of course thou must be willing to put in some time learning, but I will be glad to teach thee.",
			"I am sure I can break my busy schedule to lend thee some instruction, assuming thou hast some coin."
		}
	}
	#Sophistication Medium 
	{
		#KEY "*job*", "*what*do*do*", "*occupation*", "*profession*" 
		{
			#Attitude Wicked 
			{
				"I'm a tailor.",
				"I make and sell clothing."
			}
			#Attitude Neutral 
			{
				"I'm a tailor.",
				"I make men's and women's clothing."
			}
			#Attitude Goodhearted 
			{
				"I'm a maker of fine garments.",
				"I am an artist of needle and thread."
			}
		}
		#KEY "*tailor*" "*clothier*" 
		{
			#Attitude Wicked 
			{
				"I make clothes of all kinds.",
				"I make clothes for both men and women."
			}
			#Attitude Neutral 
			{
				"I make sturdy traveling clothes and fancy dress.",
				"I make and sell a full line of garments."
			}
			#Attitude Goodhearted 
			{
				"Many fine ladies and gentlemen patronize me exclusively for their couture needs.",
				"I can fulfill all thy needs for plain and formal dress."
			}
		}
		#KEY "*sew*" 
		{
			#Attitude Wicked 
			{
				"I can sell thee a sewing kit so thou can mend thine own clothes."
			}
			#Attitude Neutral 
			{
				"Perhaps thou would be interested in a sewing kit, to repair damaged clothing."
			}
			#Attitude Goodhearted 
			{
				"If thou knowest which end of a needle is which, thou wilt find my sewing kit quite handy."
			}
		}
		#KEY "*cloth *" "*cloth.*" "*cloth?*" "*cloth!*" "*thread*" 
		{
			#Attitude Wicked 
			{
				"I buy cloth and thread.",
				"If thou hast cloth or thread for sale, I'll look it over."
			}
			#Attitude Neutral 
			{
				"I could use another source for cloth or thread.",
				"If thou art selling cloth or thread, I'm buying."
			}
			#Attitude Goodhearted 
			{
				"Decent cloth and thread is so hard to find these days.",
				"If thou dost have cloth or thread for sale, I could perhaps make thee an offer."
			}
		}
		#KEY "*clothes*" "*clothing*" "*garment*" "*shirt*" "*pant*" "*vest*" "*dress*" "*kilt*" "*skirt*" "*apron*" "*shawl*" "*robe*" "*coat*" "*cape*" 
		{
			#Attitude Wicked 
			{
				"I can sell thee a garment to cover up whatever thou want covered.",
				"If thou dost need clothing, look my stock over and buy."
			}
			#Attitude Neutral 
			{
				"I have clothing of all sorts, for both men and ladies.",
				"Ankle to neck, I can meet all thy clothing needs."
			}
			#Attitude Goodhearted 
			{
				"I'm sure that thou can find any garment thou might wish among my stock.",
				"My stock of all types of garments is extensive."
			}
		}
		#Key "*skill*"
		{
			"I would be happy to help thee learn my trade.",
			"It is a demanding skill, but I can teach thee what thou needs to know.",
			"Thou must be willing to put in some time learning, but I will be glad to teach thee the basics.",
			"If thou hast some coin, I have the time to help thee practice."
		}
	}
	#Sophistication Low 
	{
		#KEY "*job*", "*what*do*do*", "*occupation*", "*profession*"  
		{
			#Attitude Wicked 
			{
				"I'm a tailor.",
				"I make and sell clothing."
			}
			#Attitude Neutral 
			{
				"I'm a tailor.",
				"I make men's and women's clothing."
			}
			#Attitude Goodhearted 
			{
				"I'm a maker of fine garments.",
				"I am an expert with needle and thread."
			}
		}
		#KEY "*tailor*" "*clothier*" 
		{
			#Attitude Wicked 
			{
				"I make clothes of all kinds.",
				"I make clothes for both men and women."
			}
			#Attitude Neutral 
			{
				"I make sturdy travelin' clothes and fancy dress.",
				"I make and sell a full line of clothing."
			}
			#Attitude Goodhearted 
			{
				"Lotsa fine ladies and gentlemen buy their clothes from me.",
				"I can take care of all thy needs for plain and formal dress."
			}
		}
		#KEY "*sew*" 
		{
			#Attitude Wicked 
			{
				"I can sell thee a sewing kit so thou can mend thine own clothes."
			}
			#Attitude Neutral 
			{
				"Maybe thou would be interested in a sewing kit, to repair damaged clothing."
			}
			#Attitude Goodhearted 
			{
				"If thou knows how to use a needle, thou might find my sewing kit handy."
			}
		}
		#KEY "*cloth *" "*cloth.*" "*cloth?*" "*cloth!*" "*thread*" 
		{
			#Attitude Wicked 
			{
				"I buy cloth and thread.",
				"If thou has cloth or thread for sale, I'll look at it."
			}
			#Attitude Neutral 
			{
				"I might could use another source for cloth or thread.",
				"If thou'rt sellin' cloth or thread, I might be buyin'."
			}
			#Attitude Goodhearted 
			{
				"Good cloth and thread is so hard to find these days.",
				"If thou has cloth or thread for sale, I could make thee an offer."
			}
		}
		#KEY "*clothes*" "*clothing*" "*garment*" "*shirt*" "*pant*" "*vest*" "*dress*" "*kilt*" "*skirt*" "*apron*" "*shawl*" "*robe*" "*coat*" "*cape*" 
		{
			#Attitude Wicked 
			{
				"I can sell thee any garment thou might need.",
				"If thou needs clothing, look over my stock and buy."
			}
			#Attitude Neutral 
			{
				"I got clothing for both men and ladies.",
				"Ankle to neck, I can sell thee what thou needs."
			}
			#Attitude Goodhearted 
			{
				"I'm sure that thou can find any garment thou might wish among my stock.",
				"My stock is large."
			}
		}
		#Key "*skill*"
		{
			"I'd be willin' to help thee learn the trade.",
			"It's a demanding skill, but I can teach thee.",
			"Thou must be willing to put in some time learning, but I'll be glad to teach thee the basics.",
			"If thou got some coin, I got the time to help thee practice."
		}
	}
}

