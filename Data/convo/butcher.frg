//Britannia Butcher Fragment				    
//Notes:  This is used for general information pertaining to all Britannian butchers
//Current Keyword List:
//   job
//   
//Additional Keywords:
//Revision Date:  9/3/96
//Author:  Andrew Morris

#Fragment Britannia, Job, Britannia_Butcher {
        #Sophistication High {
                #Key "*job*", "*what*do*do*", "*butcher*", "*profession*", "*occupation*" {
                        "I am a butcher, $milord/milady$.",
			"Why, I cut fresh meat, $milord/milady$.",
                        "I see to it that people like thee have fresh meat."
                }

                #Key "*buy*" {
                        "Thou wouldst like to purchase fresh meat?",
                        "I have beef, ham, and poultry, $milord/milady$, if either is to thy liking.",
		        "Perhaps thou wouldst be interested in some cut meat."
	      	}

		#Key "*beef*" {
		 	"Aye, I take only the finest parts of the cow.",
			"A good steak is an excellent way to enjoy the day.",
			"Aye, $milord/milady$, after all, cows are for more than milk."
		}

		#Key "*poultry*" {
		 	"I hear many people prefer chicken to beef.",
			"Ah, yes, chicken makes for a fine meat for lunch.",
			"Aye, $milord/milady$, both light meat and dark."
		}

		#Key "*ham*" {
		 	"I suggest some for breakfast, myself.",
			"Whether roasted or baked, a good ham makes for a good meal.",
			"I must say, I have never met a pig I did not like."
		}

		#Key "*cut*"  {
		     	"Why, I sell only the best cuts of meat, $milord/milady$.",
			"It takes many years to learn the best parts of a beast.",
			"'Tis very important to me to cut away the gristle and tendons. Who wants to eat that?"
		}

		#Key "*fresh*" "*meat*" {
		 	"Aye, $milord/milady$, none of that salted meat for my patrons.",
			"Why, of course I sell only the freshest cuts.",
			"I do not sell anything killed more than three days past, $milord/milady$."
		}
        }

        #Sophistication Medium {
                #Key "*job*", "*what*do*do*", "*butcher*", "*profession*", "*occupation*" {   
                        "I am a butcher, $milord/milady$.",
			"Why, I cut fresh meat, $milord/milady$.",
                        "I see to it that people like thee have fresh meat."
                }

                #Key "*buy*" {
                        "Thou wouldst like to purchase fresh meat?",
                        "I have beef, ham, and poultry, $milord/milady$, if either is to thy liking.",
		        "Perhaps thou wouldst be interested in some cut meat."
	      	}

		#Key "*beef*" {
		 	"Aye, I take only the finest parts of the cow.",
			"A good steak is an excellent way to enjoy the day.",
			"Aye, $milord/milady$, after all, cows are for more than milk."
		}

		#Key "*poultry*" {
		 	"I hear many people prefer chicken to beef.",
			"Ah, yes, chicken makes for a fine meat for lunch.",
			"Aye, $milord/milady$, both light meat and dark."
		}

		#Key "*ham*" {
		 	"I suggest some for breakfast, myself.",
			"Whether roasted or baked, a good ham makes for a good meal.",
			"I must say, I have never met a pig I did not like."
		}

		#Key "*cut*"  {
		     	"Why, I sell only the best cuts of meat, $milord/milady$.",
			"It takes many years to learn the best parts of a beast.",
			"'Tis very important to me to cut away the gristle and tendons. Who wants to eat that?"
		}

		#Key "*fresh*" "*meat*"  {
		 	"Aye, $milord/milady$, none of that salted meat for my patrons.",
			"Why, of course I sell only the freshest cuts.",
			"I do not sell anything killed more than three days past, $milord/milady$."
		}
        }

        #Sophistication Low {
                #Key "*job*", "*what*do*do*", "*butcher*", "*profession*", "*occupation*" {   
                        "I'm a butcher.",
			"I cut fresh meat, $milord/milady$.",
                        "It's my job to cut up fresh meat."
                }

                #Key "*buy*" {
                        "I have several meats for sale.",
                        "I have beef, ham, and poultry, $milord/milady$, if that's what thou wants.",
		        "I can get thee some cut up meat."
	      	}

		#Key "*beef*" {
		 	"Aye, I use a well-fattened cow.",
			"A good steak is a great dinner.",
			"Aye, $milord/milady$, after all, cows are for more than milk."
		}

		#Key "*poultry*" {
		 	"Lotsa people like chicken better'n beef.",
			"Chicken's a good lunch meat.",
			"Aye, $milord/milady$, both light meat and dark."
		}

		#Key "*ham*" {
		 	"I like ham for breakfast.",
			"A good ham's a good meal.",
			"I ain't never met a pig I didn't like."
		}

		#Key "*cut*"  {
		     	"Why, I sell only the best cuts of meat, $milord/milady$.",
			"It takes a while to learn the best parts of an animal.",
			"'Tis very important to me to cut away the gristle and tendons. Who wants to eat that?"
		}

		#Key "*fresh*" "*meat*" {
		 	"Aye, $milord/milady$, none of that salted meat for my customers.",
			"Why, of course I sell only the best cuts.",
			"I don't sell anything killed more than three days past, usually."
		}
        }
}
