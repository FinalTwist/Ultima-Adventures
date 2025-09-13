//Britannia Farmer Fragment				    
//Notes:  This is used for general information pertaining to all Britannian Farmers
//Current Keyword List: job, profession, occupation, crops, vermin, rodents, hoe, tools, plow, 
//	plowshare, plowshares	
//Additional Keywords:
//Revision Date:  3/14/96
//Author:  Andrew Morris

//!!Note!!  No detail for attitude or notoriety has been added!!

#Fragment Britannia, Job, Britannia_Farmer {
	#Sophistication High {
		#Key "*job*", "*profession*", "*occupation*", "*what*do*do*"  {
			"I am a farmer, $milord/milady$. I raise crops.",
			"I till the land to make sure the people have food, $milord/milady$.",
			"I farm, $milord/milady$, merely seeking to reap the benefits of fertile soil."
		}
		#Key "*crops*", "*farm*"  {
			"I raise many different crops, $milord/milady$.",
			"I work long days to see to it that my crops survive.",
			"My crops feed the people of britannia."
		}

		#Key "*vermin*", "*rodents*" {
			"Prithee, $milord/milady$, ask not of my nemesis.",
			"I wouldst rather not discuss my trials with thee, good $sir/lady$.",
			"I cannot but decry the creatures that have ravaged my crops. Please, gentle $sir/lady$, speak not of the vermin."  
		}

		#Key "*hoe*", "*tools*", "*plow*", "*plowshare*", "*plowshares*" {
			"With hoe in hand I till my fields.",
			"Pray forgive me, $milord/miilady$, for I am but a simple #man/woman#. Mine hoe is mine only tool.",
	      		"A good hoe is nearly as valuable as fertile land, for without one, the other goes to waste."
	}
	}
	#Sophistication Medium {
		#Key "*job*", "*profession*", "*occupation*", "*what*do*do*"  {
			"I am a simple farmer, $milord/milady$. I raise crops.",
			"I till the land to make sure the people have food, $milord/milady$.",
			"I farm, $milord/milady$."
		}
		#Key "*crops*", "*farm*"  {
			"I raise many different crops, $milord/milady$.",
			"My crops help to feed the people of this land.",
			"I work days and nights to produse enough crops to make a living."
		}

		#Key "*vermin*", "*rodents*" {
			"Curse them! I have not the patience for insect or rat!",
			"'Tis a veritable plague, I tell thee! Rats and insects ravage my tomatoes and potatoes, while rabbits devour my cabbage and carrots. What will I have for sale, save grain for the baker?",
		  	"I cannot but decry the creatures that have ravaged my crops. Please, gentle $sir/lady$, speak not of the vermin lest thou suffer my outcries."  
		}

		#Key "*hoe*", "*tools*", "*plow*", "*plowshare*", "*plowshares*" {
			"With hoe in hand, I till my fields.",
			"Pray forgive me, $milord/miilady$, for I am but a simple #man/woman#. Mine hoe is mine only tool.",
	      		"A good hoe is nearly as valuable as fertile land, for without one, the other goes to waste."
		}
		}
	#Sophistication Low {
		#Key "*job*", "*profession*", "*occupation*", "*what*do*do*"  {
			"I'm a farmer, $milord/milady$.",
			"I raise crops, $milord/milady$.",
			"I farm, $milord/milady$."
		}
		#Key "*crops*", "*farm*"  {
			"Aye, $milord/milady$, I raise what I can.",
			"Thanks to the derned rodents, I don't got much left!",
			"My crops'll survive, $milord/milady$.  Even if I gotta stay up nights to do it!"
		}

		#Key "*vermin*", "*rodents*" {
			"Curse 'em!",
			"A pox on rats, rabbits, and insects!",
		  	"Vile spawns of evil they are!"  
		}

		#Key "*hoe*", "*tools*", "*plow*", "*plowshare*", "*plowshares*" {
			"I use what I can get, $milord/milady$.",
			"To be sure, $milord/milady$, no farmer does without a hoe.",
	      		"I hoe, I plow, I farm."
		}
	}
}

