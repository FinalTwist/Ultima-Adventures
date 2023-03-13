//Britannia Miller Fragment				    
//Notes:  This is used for general information pertaining to all Britannian millers
//Current Keyword List:
//   job
//   
//Additional Keywords:
//Revision Date:  2/13/97 - dab
//Author:  Andrew Morris

//!!Directions to the mill would be nice as part of the response to that keyword

#Fragment Britannia, Job, Britannia_Miller {
        #Sophistication High {
                #Key "*job*", "*work*", "*profession*", "*occupation*", "*what*do*do*", "*miller*" {
                        "I am a miller, $milord/milady$.",
			"Why, I work at the flour mill, $milord/milady$.",
                        "I take wheat and grind it into flour, $milord/milady$."
                }

                #Key "*flour*" {
                        "Aye, the very stuff a baker uses for his bread and pastries. I can sell thee some, if thou wish't.",
                        "'Tis an essential ingredient in baking, $milord/milady$.",
		        "Only the best flour can be used to make the best breads, $milord/milady$."
	      	}

		#Key "*mill *", "*mill.*", "*mill?*", "*mill!*" {
		 	"Aye, 'tis where I grind the grain.",
			"The flour mill is where wheat grains are made ready for baking."
		}

		#Key "*wheat*", "*oat*" {
		 	"'Tis one of many grains, $milord/milady$.",
			"Aye, $milord/milady$, it makes for an excellent tasting bread.",
			"'Tis one of my favorite grains, $milord/milady$."
		}

		#Key "*grain*" {
			"Wheat or oat, I grind and sell both.",
			"'Tis actually the seed from wheat and oat, $milord/milady$. I can sell thee some, if thou wouldst like.",
			"A finer grain makes for a lighter bread, while larger grains
				make a very filling bread." 
		}
        }

        #Sophistication Medium {
               #Key "*job*", "*work*", "*profession*", "*occupation*", "*what*do*do*", "*miller*" {
                        "I am a miller, $milord/milady$.",
			"Why, I work at the flour mill, $milord/milady$.",
                        "I take wheat and grind it into flour, $milord/milady$."
                }

                #Key "*flour*" {
                        "Aye, the very stuff a baker uses for his bread and pastries. I have some that thou couldst purchase.",
                        "'Tis an essential ingredient in baking, $milord/milady$.",
		        "Only the best flour can be used to make the best breads, $milord/milady$."
	      	}

		#Key "*mill *", "*mill.*", "*mill?*", "*mill!*"  {
		 	"Aye, 'tis where I grind the grain.",
			"The flour mill is where wheat grains are made ready for baking."
		}

		#Key "*wheat*", "*oat*" {
		 	"'Tis one of many grains, $milord/milady$.",
			"Aye, $milord/milady$, it makes for an excellent tasting bread.",
			"'Tis one of my favorite grains, $milord/milady$."
		}

		#Key "*grain*" {
			"Wheat or oat, I grind and sell both.",
			"'Tis actually the seed from wheat and oat, $milord/milady$. ",
			"A finer grain makes for a lighter bread, while larger grains
				make a very filling bread." 
		}
        }

        #Sophistication Low {
                #Key "*job*", "*work*", "*profession*", "*occupation*", "*what*do*do*", "*miller*" {
                        "I'm a miller, $milord/milady$.",
			"I work at the flour mill.",
                        "I grind wheat into flour."
                }

                #Key "*flour*" {
                        "Any baker needs it. Cooks, too.",
                        "'Tis important for baking.",
		        "The better the flour, the better the bread."
	      	}

		#Key "*mill *", "*mill.*", "*mill?*", "*mill!*" {
		 	"'At's where I grind the grain.",
			"We makes flour and grain there."
		}

		#Key "*wheat*", "*oat*" {
		 	"'Tis one of many grains.",
			"Makes for a good-tastin' bread, it does!",
			"'Tis one of my favorite grains!"
		}

		#Key "*grain*" {
			"Wheat or oat, I grind both.",
			"'Tis actually the seed from wheat and oat.",
			"Breads need grain. Don't they? Hmmm." 
		}
        }
}
