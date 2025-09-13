//Britannia Cook/Baker Fragment				    
//Notes:  This is used for general information pertaining to all Britannian Cooks and Bakers
//	Eventually, cooks and bakers will be put into separate frags.
//Current Keyword List: job, profession, occupation, craft, cooking, baking, bread, pastry, cake,meals, meal
//	flour, meat, vegetables, what do you cook?, sweets, oven, ingredients	
//Additional Keywords:
//Revision Date:  3/14/96
//Author:  Andrew Morris

//!!Cooking and Baking needs will need to be addressed later.

//!!Note!!  All three levels of Sophistication are nearly identical (after the first 3-4 keywords)!!  And, no detail for
//attitude or notoriety has been added!!

#Fragment Britannia, Job, Britannia_Cook {
	#Sophistication High {
		#Key "*job*", "*profession*", "*occupation*", "*craft*", "*what*do*do*"  {
			"I am a creator of fine cuisine, $milord/milady$.",
			"'Tis my lot to prepare food for the consumption of others, $milord/milady$.",
			"My art is that of preparing fine meals for others to enjoy."
		}
#Key "*consumption*" {
			"Nonono... I'm referring to eating, $milord/milady$. As in the consumption of FOOD."
		}

		#Key "*cook*" {
			"Cooking is an art, $milord/milady$, one that requires years of practice to perfect thy skill.",
		 	"With time and practice, $milord/milady$, nary a soul couldst fail to become a cook. But it takes skill to be a true culinary artist.",
			"Cooking is very important to me, $milord/milady, which is why I spend so much time perfecting my craft."
		}
		#Key "*baking*" {
			"Even more than cooking, $milord/milady$, baking is a skill that takes time to master.",
			"I love baking, $milord/milady$. 'Tis quite peaceful to spend one's day preparing sweets and pastries.",
			"Baking is quite a bit more than merely mixing flour and water together, $milord/milady$. 'Tis a skill like any other."
		}
		#Key "*bread*" {
			"Aye, I enjoy baking bread, though it is one of the more simple aspects of my craft.",
			"Why, 'tis nothing more than flour, yeast, and water left to rise in an oven.",
			"A most excellent meal when served with jam."
		}
		#Key "*pastr*", "*cake*", "*sweets*" {
			"My pastries are enjoyed by all.  From the haughtiest noble to the most lowly peasant.",
			"My pastries are quite popular, $milord/milady$.  I hope that they please thee also.",
			"One day I hope to present my pastries to Lord British himself."
		}
		#Key "*meals*", "*meal*", "*what*you*cook*", "*food*" {
			"I prepare a variety foods, $milord/milady$, for many occasions.",
			"My creations have been deemed suitable for Lord British, himself.",
			"I have nothing prepared to serve thee, but if thou art hungry, perhaps some meat and bread will fill thy empty stomach."
		}
		#Key "*flour*" {
			"Flour is a key ingredient in baking bread.",
			"Without flour, I couldst not bake bread or cake.",
			"I get my flour from the mill."
		}
		#Key "*meat*", "*beef*",  "*ham*",  "*pork*", "*bacon*", "*fish*", "*chicken*", "*roast*"   {
			"Much to choose from, $milord/milady$. Perhaps thou wouldst be satisfied with a bit of ham?  Or maybe beef?  Or even fish?",
			"I have some fresh beef,  pork, ham, and many other meats for sale $milord/milady$.",
			"I have many meats for sale.  Bacon, chicken, beef... just about anything that thou couldst want." 
		}
		#Key "*vegetables*" {
			"Fresh vegetables, every day.",
			"Vegetables from all over Britannia.",
      			"I carry anything that thou couldst want, $milord/milady$."
		}
		#Key "*oven*" {
			"I use an oven to bake nearly everything I prepare, whether it be a fine roast or fruit-filled tart.",
			"I couldst not bake bread without an oven in which to let it rise.",
			"My oven has seen many a masterpiece."
		}
		#Key "*ingredient*" {
			"Special ingredients are required to make my meals perfect.",
			"I use only the finest of ingredients in my food, $milord/miilady$. I must choose my sources with care.",
			"Ingredients are the essence of the culinary arts, $milord/milady$. Well, ingredients and years of experience."
		}
#KEY "*skill*" {    
	#Attitude Wicked {
	"What, dost thou find need to increase thy skills with the culinary arts? I'm not sure that thou dost have the nose for cooking.",
"Thou dost need some skill to cook a dinner that a $man/woman$ can live through. I might be able to teach thee a trick or two, but I can't promise anything.",
"Well, for a little compensation, I could show thee how to improve thy skills and keep from killing someone, but I doubt thou wouldst ever be a master chef."
			}
	#Attitude Neutral {
	"I've been known to teach a select few how to improve their cooking, for a few coins.",
"If thou dost need some training in the art of the cooking, just say the word. I shall try to help thee.",
"I can give thee some cooking tips, all thou dost need do is to ask.  And leave me a few coins."
			}
	#Attitude Goodhearted {
	"I'd be happy to help train thee in cooking techniques. I'd ask for a few coins, though, to help pay for my time.",
"Sure, I could be convinced to teach thee some of what I know.  'Twould be up to thyself, though, to practice it.",
"I can teach thee only if thou art willing to listen and learn. And pay me for the time I would invest. A few coins would suffice."
			}
		}

	}
	
	#Sophistication Medium {
		#Key "*job*", "*profession*", "*occupation*", "*craft*",  "*what*do*do*"  {
			"I am a cook and baker, $milord/milady$.",
			"I cook and bake, $milord/milady$, preparing food for others to eat.",
			"I work as a cook, $milord/milady$."
		}
		#Key "*cook*" {
			"Cooking is a chore I suffer gladly.",
		 	"Cooking is not for everyone, $milord/milady$, but I have no qualms with a hard day's toil.",
			"I find the pleasure others get from enjoying a simple meal quite satisfying, $milord/milady."
		}
#Key "*consumption*" {
			"Nonono... I'm referring to eating, $milord/milady$. As in the consumption of FOOD."
		}

			#Key "*baking*" {
			"Even more than cooking, $milord/milady$, baking is a skill that takes time to master.",
			"I love baking, $milord/milady$. 'Tis quite peaceful to spend one's day preparing sweets and pastries.",
			"Baking is quite a bit more than merely mixing flour and water together, $milord/milady$. 'Tis a skill like any other."
		}
		#Key "*bread*" {
			"Aye, I enjoy baking bread, though it is one of the more simple aspects of my craft.",
			"Why, 'tis nothing more than flour, yeast, and water left to rise in an oven.",
			"A most excellent meal when served with jam."
		}
		#Key "*pastr*", "*cake*", "*sweets*" {
			"My pastries are enjoyed by all. From the haughtiest noble to the most lowly peasant.",
			"My pastries are quite popular, $milord/milady$. I hope that they please thee also.",
			"One day I hope to present my pastries to Lord British himself."
		}
		#Key "*meals*", "*meal*", "*what*you*cook*", "*food*" {
			"I prepare a variety foods, $milord/milady$, for many occasions.",
			"My creations have been deemed suitable for Lord British, himself.",
			"I have nothing prepared to serve thee, but if thou art hungry, perhaps some meat and bread will fill thy empty stomach."
		}
		#Key "*flour*" {
			"Flour is a key ingredient in baking bread.",
			"Without flour, I couldst not bake bread or cake.",
			"I get my flour from the mill."
		}
		#Key "*meat*", "*beef*",  "*ham*",  "*pork*", "*bacon*", "*fish*", "*chicken*", "*roast*"   {
			"Much to choose from, $milord/milady$. Perhaps thou wouldst be satisfied with a bit of ham?  Or maybe beef?  Or even fish?",
			"I have some fresh beef,  pork, ham, and many other meats for sale $milord/milady$.",
			"I have many meats for sale.  Bacon, chicken, beef... just about anything that thou couldst want." 
		}
		#Key "*vegetables*" {
			"Fresh vegetables, every day.",
			"Vegetables from all over Britannia.",
      			"I carry anything that thou couldst want, $milord/milady$."
		}
		#Key "*oven*" {
			"I use an oven to bake nearly everything I prepare, whether it be a fine roast or fruit-filled tart.",
			"I couldst not bake bread without an oven in which to let it rise.",
			"My oven has seen many a masterpiece."
		}
		#Key "*ingredient*" {
			"Special ingredients are required to make my meals perfect.",
			"I use only the finest of ingredients in my food, $milord/miilady$. I must choose my sources with care.",
			"Ingredients are the essence of the culinary arts, $milord/milady$. Well, ingredients and years of experience."
		}
#KEY "*skill*" {    
	#Attitude Wicked {
	"What, dost thou find need to increase thy skills with the culinary arts? I'm not sure that thou dost have the nose for cooking.",
"Thou dost need some skill to cook a dinner that a $man/woman$ can live through. I might be able to teach thee a trick or two, but I can't promise anything.",
"Well, for a little compensation, I could show thee how to improve thy skills and keep from killing someone, but I doubt thou wouldst ever be a master chef."
			}
	#Attitude Neutral {
	"I've been known to teach a select few how to improve their cooking, for a few coins.",
"If thou dost need some training in the art of the cooking, just say the word. I shall try to help thee.",
"I can give thee some cooking tips, all thou dost need do is to ask.  And leave me a few coins."
			}
	#Attitude Goodhearted {
	"I'd be happy to help train thee in cooking techniques. I'd ask for a few coins, though, to help pay for my time.",
"Sure, I could be convinced to teach thee some of what I know.  'Twould be up to thyself, though, to practice it.",
"I can teach thee only if thou art willing to listen and learn. And pay me for the time I would invest. A few coins would suffice."
			}
		}
	}
	
	#Sophistication Low {
		#Key "*job*", "*profession*", "*occupation*", "*craft*", "*what*do*do*"  {
			"I'm a cook an' a baker, $milord/milady$.",
			"I cook and bake, $milord/milady$, preparin' food to eat.",
			"I make meals for people to eat."
		}
		#Key "*cook*" {
			"Cookin' keeps me out o' the hot sun.",
			"'Tis a job, $milord/milady$, nothin' more.",
			"Cooking, baking -- 'tis all the same to me, $milord/milady$."
		}
		#Key "*consumption*" {
			"Nonono... I meant eating. Like in the consumption of FOOD."
		}
		#Key "*baking*" {
			"Even more than cooking, $milord/milady$, bakin's a skill that takes time to get good at.",
			"I love bakin'. 'Tis quite peaceful to spend my day preparin' sweets an' stuff.",
			"Bakin' is more than mixin' flour and water together, $milord/milady$. 'Tis a skill like any other."
		}
		#Key "*bread*" {
			"Aye, I like bakin' bread, though it's one of the simpler parts of a cook's job.",
			"Why, 'tis nothing more than flour, yeast, and water that rises in a hot oven.",
			"Makes a great meal when served with jam."
		}
		#Key "*pastr*", "*cake*", "*sweets*" {
			"My pastries are good.",
			"My pastries are very good. I hope that thou likes 'em too.",
			"One day I want to present my pastries to Lord British 'imself."
		}
		#Key "*meals*", "*meal*", "*what*you*cook*", "*food*" {
			"I make a lot of different foods, $milord/milady$, for lots of occasions.",
			"My foods are good enough for Lord British, 'imself.",
			"I got nothin' prepared to serve thee, but if thou'rt hungry, maybe some meat and bread will fill thy empty stomach."
		}
		#Key "*flour*" {
			"Flour is a key ingredient in baking bread.",
			"Without flour, I couldst not bake bread or cake.",
			"I get my flour from the mill."
		}
		#Key "*meat*", "*beef*",  "*ham*",  "*pork*", "*bacon*", "*fish*", "*chicken*", "*roast*"  {
			"A lot to choose from, $milord/milady$. Would thou be satisfied with a bit o' ham? Or maybe beef?  Or even fish?",
			"I got some fresh beef, pork, ham, and tons of other meats for sale $milord/milady$.",
			"I got many meats for sale.  Bacon, chicken, beef... just about anything that thou could want." 
		}
		#Key "*vegetables*" {
			"Fresh vegetables, every day.",
			"Vegetables from all over Britannia.",
      			"I got anything that thou could want, $milord/milady$."
		}
		#Key "*oven*" {
			"I use an oven to bake nearly everything I make, whether it's a roast or fruit-filled tart.",
			"I couldn't bake bread without an oven to let it rise.",
			"My oven has seen lotsa good food."
		}
		#Key "*ingredient*" {
			"Special ingredients and skill are all that's required to make my meals good.",
			"I use the finest ingredients in my food, $milord/miilady$. I must choose my sources with care.",
			"Ingredients are the secret of cookin', $milord/milady$. Well, ingredients and skill."
		}
#KEY "*skill*" {    
	#Attitude Wicked {
	"What, thou wants to cook better? I ain't sure that thou got the nose for it.",
"Thou needs some skill to cook a dinner that a $man/woman$ can live through. I might teach thee a trick or two, but I ain't promisin' nothin'.",
"Well, for a little money, I could show thee how to cook better, but I doubt thou would ever be a master."
			}
	#Attitude Neutral {
	"I've been known to teach some how to improve their cookin', for a few coins.",
"If thou needs some training in cookin', just say the word. I'll try to help.",
"I can give some cookin' tips, just ask.  And give me a few coins."
			}
	#Attitude Goodhearted {
	"I'd be happy to help thee with cookin' techniques. I'd ask for a few coins, though, to pay for my time.",
"Sure, I could be convinced to teach some of what I know.  'Twould be up to thee, though, to practice it.",
"I can teach thee only if thou'rt willin' to listen and learn. And pay me for my time. Just a few coins."
			}
		}
	}
}


