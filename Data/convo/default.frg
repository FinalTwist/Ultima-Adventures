// The absolute last default, there only for fallback.
//
// This should hopefully never be triggered.
//
// Keywords: failed match, name
//
// Last revised: March 13th
//
// Author: Raph Koster
//

#Fragment Global, Default, Global_Default {
	#Sophistication High {
        #Key "No key" {
			"Pardon me?",
			"Excuse me?",
			"I do not understand."
		}
        #Key "name" {
            "My name is _MyName_."
        }
	}
	#Sophistication Medium {
        #Key "No key" {
			"Sorry?",
			"What?",
			"Say again?"
		}
        #Key "name" {
            "I am _MyName_."
        }
	}
	#Sophistication Low {
        #Key "No key" {
			"Enh?",
			"Huh?",
			"Arglebargle glop-glyf?"
		}
        #Key "name" {
            "I'm _MyName_."
        }
	}
}

// End of fragment

