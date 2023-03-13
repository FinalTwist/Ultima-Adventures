//Britannia Armourer Fragment				    
//Notes:  This is used for general information pertaining to all Britannian Armourers, Armourers, and Blacksmits
//	Eventually, armourers and armourers will be put into separate frags.
//Current Keyword List:	
//Additional Keywords:
//Revision Date:  3/14/96
//Author:  Andrew Morris


#Fragment Britannia, Job, Britannia_Armourer 
{
	#Sophistication High 
	{
		#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" 
		{
			#Attitude Wicked 
			{
				"I'm an armourer.",
				"I pound steel into submission.",
				"I create, buy, and sell armor."
			}
			#Attitude Neutral 
			{
				"I've been an armourer most of my life.",
				"I'm an armourer.",
				"I make and sell armor."
			}
			#Attitude Goodhearted 
			{
				"I make armor. I enjoy what I do! 'Tis good to hammer out a beautiful breastplate!",
				"Working metal is what I was born to do!."
			}
		}
		#KEY "*Breastplate*" 
		{
			#Attitude Wicked 
			{
				"Good for protection, breastplates are.  Thou wouldst probably be in need of much protection, from the looks of it.",
				"A good breastplate could keep a sword from between thy ribs.",
				"Some skill and metal are all that goes in to the making of armor.  And the right tools, of course."
			}
			#Attitude Neutral 
			{
				"I sell breastplates at a reasonable price.  I do need to make a profit.",
				"I tend to have a few breastplates in my inventory.  Enough to fit most sizes of people, in fact.",
				"Thou can learn to make thine own. It takes metal, the tools of a smith, and skill."
			}
			#Attitude Goodhearted 
			{
				"I sell breastplates at a competitive price $milord/milady$. Knives too.",
				"I always try to carry a supply of breastplates and shields, $milord/milady$.",
				"Someday thou might be able to make thine own armor."
			}
		}
		#KEY "*plate*" "*padded*" "*chain*" "*ring*" "*gorget*" "*shield*" "*buckler*" "*heater*" "*helm*" "*gauntlet*" "*inventory*"  
		{
			#Attitude Wicked 
			{
				"Dost thou want armor?  I have plenty of that!",
				"I carry many different types of armor for many different types of people.",
				"Buy what thou dost need. Or sell what thou dost have extra.",
				"All my armor is guaranteed to reduce damage to the person wearing it."
			}
			#Attitude Neutral 
			{
				"In need of armor, my friend?  Thou'rt come to the right place!",
				"I have armor to fit everyone!",
				"I'll buy from thee what extra armor thou art carrying around.",
				"All of my armor is guaranteed to reduce the risk of injuries."
			}
			#Attitude Goodhearted 
			{
				"Dost thou need armor, $milord/milady$?  Thou hast come to the right place!",
				"I have armor for everyone!  Especially an adventuresome sort like thee, $milord/milady$",
				"I can offer thee fair price for what armor thou dost carry with thee.",
				"All of my armor is guaranteed to reduce the risk of injuries."
			}
		}
		#KEY "*Smith*", "*Armourer*", "*Armorer*"  
		{
			#Attitude Wicked 
			{
				"I've been a smith for a good number of years.",
				"Smithing is my life.",
				"I learned how to work metal before I could stand upright.",
				"I have armoring people all of my adult life."
			}
			#Attitude Neutral 
			{
				"I've been a smith for a good number of years.",
				"Smithing is my life.",
				"I learned how to work metal before I could stand upright.",
				"I have been armoring people all of my adult life."
			}
			#Attitude Goodhearted 
			{
				"I've been a smith for a good number of years.",
				"Smithing is my life.",
				"I learned how to work metal before I could stand upright.",
				"I have been an armourer all of my adult life."
			}
		}
		#KEY "*armor*", "*Armour*", "*protection*" 
		{
			#Attitude Wicked 
			{
				"I carry a bunch of armor in my inventory.",
				"I shall offer to buy pre-owned armor if thou didst have some to sell.",
				"I can purchase thine armor, if thou dost wish it.",
				"I craft the finest armor.",
				"Chainmail, shields, gauntlets... thou dost name it, I probably have it."
			}
			#Attitude Neutral 
			{
				"I carry fine armor in my inventory.",
				"I shall buy thy pre-owned armor if thou wouldst be willing to sell.",
				"I sell excellent armor at reasonable prices.",
				"I craft some of the finest armor.",
				"Chainmail, shields, gauntlets... thou dost name it, I probably have it."
			}
			#Attitude Goodhearted 
			{
				"I carry only the best armor.",
				"If thou art willing to sell, I would buy thy unwanted armor from thee.",
				"I sell the best armor at fair prices.",
				"I craft some of the sturdiest armor.",
				"Chainmail, shields, gauntlets... thou dost name it, $milord/milady$, I probably have it."
			}
		}
		#KEY "*shield*" 
		{ 
			#Attitude Wicked 
			{
				"I have many different shields for sale.",
				"I carry kite-shields, heaters... thou dost name the shield, I should have it.",
				"I'll buy and sell what armor thou have or need."
			}
			#Attitude Neutral 
			{
				"I have several different shields in stock.",
				"I carry kite-shields, heaters, bucklers, and many more.",
				"I would be happy to buy and sell what pieces of armor thou dost have or need."
			}
			#Attitude Goodhearted 
			{
				"I have many shields in stock, $milord/milady$.",
				"I carry kite-shields, heaters, bucklers, and many more, $milord/milady$.",
				"I'll be willing to buy and sell what armor thou dost have or need, friend."
			}
		}
		#KEY "*skill*" 
		{
			#Attitude Wicked 
			{
				"Oh, don't tell me thou dost want to learn armor-making. I'd be hard-pressed to train someone like thee.",
				"What, dost thou want practice in making armor?  Well I might be able to teach thee a thing or two.",
				"Well, for a little compensation, I could show thee some things to practice to improve thy skill in metal work."
			}
			#Attitude Neutral 
			{
				"I've been known to teach some how to improve their skills, for a few coins.",
				"If thou dost need some training in armor-making, just say so.",
				"I can give thee some practice in armor-construction, all thou dost need do is to ask.  And give me a few coins for my time."
			}
			#Attitude Goodhearted 
			{
				"I'd be happy to help train thee in some armoring techniques. I would ask for a few coins, though, to help cover my time.",
				"I'd be honored to teach thee some of what I know.  'Twould be up to thyself, though, to practice it.",
				"I can teach thee only if thou art willing to learn. And pay me for the time I would invest. A few coins would suffice."
			}
		}
	}
	#Sophistication Medium 
	{
		#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" 
		{
			#Attitude Wicked 
			{
				"I'm an armourer.",
				"I pound steel into submission.",
				"I create, buy, and sell armor."
			}
			#Attitude Neutral 
			{
				"I've been an armourer most of my life.",
				"I'm an armourer.",
				"I make and sell armor."
			}
			#Attitude Goodhearted 
			{
				"I make armor. I enjoy what I do! 'Tis good to hammer out a beautiful breastplate!",
				"Working metal is what I was born to do!."
			}
		}
		#KEY "*Breastplate*" 
		{
			#Attitude Wicked 
			{
				"Good for protection, breastplates are. Thou wouldst probably be in need of much protection, from the looks of it.",
				"A good breastplate could keep a sword from between thy ribs.",
				"Some skill and metal are all that goes in to the making of armor.  And the right tools, of course."
			}
			#Attitude Neutral 
			{
				"I sell breastplates at a reasonable price. I do need to make a profit.",
				"I tend to have a few breastplates in my inventory. Enough to fit most sizes of people, in fact.",
				"Thou can learn to make thine own. It takes metal, the tools of a smith, and skill."
			}
			#Attitude Goodhearted 
			{
				"I sell breastplates at a competitive price $milord/milady$. Knives too.",
				"I always try to carry a supply of breastplates and shields, $milord/milady$.",
				"Someday thou might be able to make thine own armor."
			}
		}
		#KEY "*plate*" "*padded*" "*chain*" "*ring*" "*gorget*" "*shield*" "*buckler*" "*heater*" "*helm*" "*gauntlet*" "*inventory*"  
		{ 
			#Attitude Wicked 
			{
				"Dost thou want armor? I carry plenty of that!",
				"I carry many different types of armor for many different types of people.",
				"Buy what thou dost need. Or sell what thou dost have extra.",
				"All my armor is guaranteed to reduce damage to the person wearing it."
			}
			#Attitude Neutral 
			{
				"In need of armor, my friend?  Thou'rt come to the right place!",
				"I have armor to fit everyone!",
				"I'll buy from thee what extra armor thou art carrying around.",
				"All of my armor is guaranteed to reduce the risk of injuries."
			}
			#Attitude Goodhearted 
			{
				"Dost thou need armor, $milord/milady$? Thou hast come to the right place!",
				"I have armor for everyone! Especially an adventuresome sort like thee, $milord/milady$",
				"I can offer thee fair price for what armor thou dost carry with thee.",
				"All of my armor is guaranteed to reduce the risk of injuries."
			}
		}
		#KEY "*Smith*"  "*Armourer*"  "*Armourer*"  
		{
			#Attitude Wicked 
			{
				"I've been a smith for a good number of years.",
				"Smithing is my life.",
				"I learned how to work metal before I could stand upright.",
				"I have armoring people all of my adult life."
			}
			#Attitude Neutral 
			{
				"I've been a smith for a good number of years.",
				"Smithing is my life.",
				"I learned how to work metal before I could stand upright.",
				"I have been armoring people all of my adult life."
			}
			#Attitude Goodhearted 
			{
				"I've been a smith for a good number of years.",
				"Smithing is my life.",
				"I learned how to work metal before I could stand upright.",
				"I have been an armourer all of my adult life."
			}
		}
		#KEY "*armor*"  "*Armour*" "*protection*" 
		{ 
			#Attitude Wicked 
			{
				"I carry a bunch of armor in my inventory.",
				"I shall offer to buy pre-owned armor if thou didst have some to sell.",
				"I can purchase thine armor, if thou dost wish it.",
				"I craft the finest armor.",
				"Chainmail, shields, gauntlets... thou dost name it, I probably have it."
			}
			#Attitude Neutral 
			{
				"I carry fine armor in my inventory.",
				"I shall buy thy pre-owned armor if thou wouldst be willing to sell.",
				"I sell excellent armor at reasonable prices.",
				"I craft some of the finest armor.",
				"Chainmail, shields, gauntlets... thou dost name it, I probably have it."
			}
			#Attitude Goodhearted 
			{
				"I carry only the best armor.",
				"If thou art willing to sell, I would buy thy unwanted armor from thee.",
				"I sell the best armor at fair prices.",
				"I craft some of the sturdiest armor.",
				"Chainmail, shields, gauntlets... thou dost name it, $milord/milady$, I probably have it."
			}
		}
		#KEY "*shield*" 
		{ 
			#Attitude Wicked 
			{
				"I have many different shields for sale.",
				"I carry kite-shields, heaters... thou dost name the shield, I should have it.",
				"I'll buy and sell what armor thou have or need."
			}
			#Attitude Neutral 
			{
				"I have several different shields in stock.",
				"I carry kite-shields, heaters, bucklers, and many more.",
				"I would be happy to buy and sell what pieces of armor thou dost have or need."
			}
			#Attitude Goodhearted 
			{
				"I have many shields in stock, $milord/milady$.",
				"I carry kite-shields, heaters, bucklers, and many more, $milord/milady$.",
				"I'll be willing to buy and sell what armor thou dost have or need, friend."
			}
		}
		#KEY "*skill*" 
		{   
			#Attitude Wicked 
			{
				"Oh, don't tell me thou dost want to learn armor-making. I'd be hard-pressed to train someone like thee.",
				"What, dost thou want practice in making armor? Well I might be able to teach thee a thing or two.",
				"Well, for a little compensation, I could show thee some things to practice to improve thy skill in metal work."
			}
			#Attitude Neutral 
			{
				"I've been known to teach some how to improve their skills, for a few coins.",
				"If thou dost need some training in armor-making, just say so.",
				"I can give thee some practice in armor-construction, all thou dost need do is to ask.  And give me a few coins for my time."
			}
			#Attitude Goodhearted 
			{
				"I'd be happy to help train thee in some armoring techniques. I'd ask for a few coins, though, to help cover my time.",
				"I'd be honored to teach thee some of what I know. 'Twould be up to thyself, though, to practice it.",
				"I can teach thee only if thou art willing to learn. And pay me for the time I would invest. A few coins would suffice."
			}
		}
	}
	#Sophistication Low 
	{
		#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" 
		{
			#Attitude Wicked 
			{
				"I'm an armourer.",
				"I pound steel into armor.",
				"I make, buy, and sell armor."
			}
			#Attitude Neutral 
			{
				"Been an armorer most of my life.",
				"I'm an armorer.",
				"I make and sell armor."
			}
			#Attitude Goodhearted 
			{
			"I make armor. I love it! 'Tis good to hammer out a strong breastplate!",
			"Workin' metal is what I was born to do!."
			}
		}
		#KEY "*Breastplate*" 
		{
			#Attitude Wicked 
			{
				"Good for protection, breastplates are. Thou would probably be needin' a lot of protection, from the looks of it.",
				"A good breastplate can keep a sword from between thy ribs.",
				"Some skill and metal are all that goes into the making of armor. And the right tools, of course."
			}
			#Attitude Neutral 
			{
				"I sell breastplates at a good price. I gotta make a profit.",
				"I tend to have a few breastplates in the store. Enough to fit most sizes of people, in fact.",
				"Learn to make thine own! It takes metal, the tools of a smith, and skill."
			}
			#Attitude Goodhearted 
			{
				"I sell breastplates at a good price $milord/milady$. Knives too.",
				"I try to carry a supply of breastplates and shields, $milord/milady$.",
				"Someday thou might be able to make thine own armor."
			}
		}
		#KEY "*plate*" "*padded*" "*chain*" "*ring*" "*gorget*" "*shield*" "*buckler*" "*heater*" "*helm*" "*gauntlet*"  "*inventory*" 
		{
			#Attitude Wicked 
			{
				"Want armor? I got plenty of that!",
				"I carry a lot of different types of armor for lots of different types of people.",
				"Buy what thou need. Or sell what thou got extra.",
				"All my armor is guaranteed to limit damage to the person wearin' it."
			}
			#Attitude Neutral 
			{
				"Need armor? Thou'rt in the right place!",
				"I got armor for everyone!",
				"I'll buy what extra armor thou'rt carryin' 'round.",
				"All my armor is guaranteed to limit the risk of injuries."
			}
			#Attitude Goodhearted 
			{
				"Need armor, $milord/milady$? Thou'rt in the right place!",
				"I got armor enough for everyone! 'Specially a wanderin' sort like thee, $milord/milady$",
				"I can offer a fair price for what armor thou'rt carryin'.",
				"All my armor is guaranteed to limit the risk of injuries."
			}
		}
		#KEY "*Smith*"  "*Armourer*" "*Armourer*"  
		{
			#Attitude Wicked 
			{
				"I been a smith for years.",
				"Smithin's my life.",
				"I learned to work metal 'fore I could stand upright.",
				"I been armorin' people all of my adult life."
			}
			#Attitude Neutral 
			{
				"I been a smith for years.",
				"Smithin's my life.",
				"I learned to work metal 'fore I could stand upright.",
				"I been armorin' people all of my adult life."
			}
			#Attitude Goodhearted 
			{
				"I've been a smith for years.",
				"Smithin's my life.",
				"I learned to work metal 'fore I could stand upright.",
				"I been an armorer all my life."
			}
		}
		#KEY "*armor*"  "*Armour*" "*protection*" 
		{
			#Attitude Wicked 
			{
				"I carry a bunch of armor in my inventory.",
				"I'll offer to buy pre-owned armor if thou had some to sell.",
				"I can take thine armor off thy hands, if thou wish it.",
				"I make the finest armor.",
				"Chainmail, shields, gauntlets... thou names it, I probably have it."
			}
			#Attitude Neutral 
			{
				"I carry good armor in my inventory.",
				"I'll buy thy armor if thou'ld be willin' to sell.",
				"I sell great armor at good prices.",
				"I make some of the finest armor.",
				"Chainmail, shields, gauntlets... thou names it, I probably got it."
			}
			#Attitude Goodhearted 
			{
				"I got only the best armor.",
				"If thou'rt willin' to sell, I'd buy thy unwanted armor from thee.",
				"I sell the best armor at fair prices.",
				"I make some of the sturdiest armor.",
				"Chainmail, shields, gauntlets... thou names it, $milord/milady$, I probably got it."
			}
		}
		#KEY "*shield*" 
		{ 
			#Attitude Wicked 
			{
				"I got lotsa different shields for sale.",
				"I carry kite-shields, heaters... name the shield, I should have it.",
				"I'll buy and sell what armor thou got or need."
			}
			#Attitude Neutral 
			{
				"I got several different shields in stock.",
				"I carry kite-shields, heaters, bucklers, and many more.",
				"I'd be happy to buy and sell what armor thou got or need."
			}
			#Attitude Goodhearted 
			{
				"I have lotsa shields in stock, $milord/milady$.",
				"I got kite-shields, heaters, bucklers, and lots more, $milord/milady$.",
				"I'm willin' to buy and sell what armor thou have or need, friend."
			}
		}
		#KEY "*skill*" 
		{
			#Attitude Wicked 
			{
				"I don't got to teach thee nothin'. But, if thou leave me a few coins, I guess thou could practice a bit in my shop.",
				"What, dost thou want practice in making armor?  Well, I might be able to teach thee a thing or two. Shouldn't be hard.",
				"Well, for a little money, I could show thee some things thou could practice."
			}
			#Attitude Neutral 
			{
				"I've been known to teach some people, for a few coins.",
				"If thou'rt needin' to be trained in armor-making, just say so.",
				"I can let thee practice in armor-construction here, all thou need to do is ask. And give me a few coins for my time."
			}
			#Attitude Goodhearted 
			{
				"Ain't that cute? I'd be happy to help thee practice some armorin'. I'd ask for a some money, though, to help cover my time.",
				"I'd be honored to teach thee some of what I know.'Twould be up to thyself, though, to practice it.",
				"I'll teach thee only if thou'rt wantin' to learn. And pay me for the time I'd put in. A few coins would be good."
			}
		}
	}
}


