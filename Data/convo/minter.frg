//Britannia Minter Fragment				    
//Notes:  This is used for general information pertaining to all Britannian minters
//Current Keyword List:
//   job
//   
//Additional Keywords:
//Revision Date:  9/3/96
//Author:  Andrew Morris

//!!Directions to mint
//!!Talk about gold as a resource

#Fragment Britannia, Job, Britannia_Minter {
        #Sophistication High {
                #Key "*job*", "*work*", "*profession*", "*occupation*", "*what*do*do*" {
                        	"I am a minter, $milord/milady$.",
		"Why, I mint coins, $milord/milady$.",
                        	"I work for the royal mint."
                }
	#Key "*minter*" {
		"Yes, I am a minter of coins.",
		"That is my chosen profession - minting I mean.",
		"I mint coins for a living."
                }
                #Key "*mint *", "*mint.*", "*mint?*", "*mint!*" {
                        "Aye, $milord/milady$, the mint is where gold ingots are turned into coins.",
                        "Of course, the coins are not stored at the mint, $milord/milady$, merely stamped there.",
		        "'Tis where I, as the minter, mint gold into currency, $milord/milady$."
	      	}

		#Key "*coin*", "*money*", "*currency*" {
		 	"All of Britannia's coins are made of gold.",
			"Trading with currency is far simpler -- and more fair -- than bartering for poorly-defined goods and services.",
			"Using coins for trade is a step above the barter system, I believe, $milord/milady$."
		}

		#Key "*press*", "*plates*", "* die*" {
		 	"Some people say using Lord British's image on our coins is a bit garrish. I am not certain I agree.",
			"Of course, the dies must be cared for in order to get a clean impression.",
			"Every few years we change the face and back of our coins to make record keeping simpler."
		}

		#Key "*gold*", "*silver*", "*copper*" {
			"Gold is the only metal we use when minting our coins.",
			"Gold is the basis for nearly all of our exchanges.",
		}
	}

        #Sophistication Medium {
                  #Key "*job*", "*work*", "*profession*", "*occupation*", "*what*do*do*" {
                        	"I am a minter, $milord/milady$.",
		"Why, I mint coins, $milord/milady$.",
                        	"I work for the royal mint."
                }
	#Key "*minter*" {
		"Yes, I am a minter of coins.",
		"That is my chosen profession - minting I mean.",
		"I mint coins for a living."
                }
                #Key "*mint *", "*mint!*", "*mint?*", "*mint.*" {
                        "Aye, $milord/milady$, the mint is where gold ingots are turned into coins.",
                        "Of course, the coins are not stored at the mint, merely stamped there.",
		        "'Tis where I, as the minter, mint gold into currency, $milord/milady$."
	      	}

		#Key "*coin*", "*money*", "*currency*" {
		 	"All of Britannia's coins are made of gold.",
			"Trading with currency is far simpler -- and more fair -- than bartering for poorly-defined goods and services.",
			"Using coins for trade is a step above the barter system, I believe, $milord/milady$."
		}

		#Key "*press*", "*plates*", "* die*"  {
		 	"Some people say using Lord British's image on our coins is a bit garrish. I am not certain I agree.",
			"Of course, the dies must be cared for in order to get a clean impression.",
			"Every few years we change the face and back of our coins to make record-keeping simpler."
		}

		#Key "*gold*", "*silver*", "*copper*" {
			"Gold is the only metal we use when minting our coins.",
			"Gold is the basis for nearly all of our exchanges.",
		}
        }

        #Sophistication Low {
                 #Key "*job*", "*work*", "*profession*", "*occupation*", "*what*do*do*" {
                        	"I'm a minter.",
		"I mint coins.",
                        	"I work for the royal mint."
                }
	#Key "*minter*" {
		"I'm a minter of the realm's coins.",
		"That's my chosen profession - minting I mean.",
		"I mint coins for a living."
                }
                #Key "*mint *", "*mint?*", "*mint!*", "*mint.*" {
                        "The mint is where we stamp coins.",
                        "The coins aren't stored at the mint, just stamped there.",
		        "'Tis where I, the minter, mint gold into currency."
	      	}

		#Key "*coin*", "*money*", "*currency*" {
		 	"All of Britannia's coins are gold.",
			"Trading with coins is better than bartering for things like chickens or goats.",
			"Using coins for trade is better than arguing about what a pig's worth."
		}

		#Key "*press*", "*plates*", "* die*"  {
		 	"Some say using Lord British's image is a bit tacky. I don't think so.",
			"The die's gotta be cared for to get a clean stamp on 'em.",
			"We change the dies sometimes. It means we can keep up with what year the coins were stamped in."
		}

		#Key "*gold*", "*silver*", "*copper*" {
			"We only use gold for our coins.",
			"We don't use silver or copper.",
		}
        }
}
