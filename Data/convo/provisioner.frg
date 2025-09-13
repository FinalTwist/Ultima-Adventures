// Provisioner function
//
// Keywords:
// job, provision, suppl*, ration, equip, arrow, bolt, pouch, bag, pack, candle, torch, lantern, food, bread, meat, mutton, chicken, bird, ale, wine, liquor, cider, beer, drink, fruit, pear, apple
//
//
// 
// - cwm


#Fragment Britannia, Job, Britannia_Provisioner {
	#Sophistication High {
		#KEY "*job", "*job*", "*what*do*do*", "*occupation*", "*profession*"  {
			#Attitude Wicked {
				"I provide provisions.",
				"I run the general store."
			}
			#Attitude Neutral {
				"I sell general provisions.",
				"I run the general store."
			}
			#Attitude Goodhearted {
				"I supply the necessities of the discriminating traveler.",
				"I run the general store."
			}
		}
		#KEY "*provision*" "*suppl*" "*ration*" "*equip*" {
			#Attitude Wicked {
				"I carry supplies and provisions for travellers.",
				"I stock food and other supplies."
			}
			#Attitude Neutral {
				"I carry supplies and provisions for travellers.",
				"I stock food and other supplies."
			}
			#Attitude Goodhearted {
				"I stock the finest supplies and provisions.",
				"I supply excellent food and other necessities for travelers."
			}
		}
		#KEY "*arrow*" "*bolt*" {
			#Attitude Wicked {
				"I have arrows and crossbow bolts for sale."
			}
			#Attitude Neutral {
				"I sell arrows and crossbow bolts."
			}
			#Attitude Goodhearted {
				"I can sell thee fine arrows and crossbow bolts."
			}
		}
		#KEY "*pouch*", "*bag*", "*pack*" {
			#Attitude Wicked {		
				"I have pouches, bags and back packs for sale."
			}
			#Attitude Neutral {
				"I have sturdy pouches, bags and backpacks for sale."
			}
			#Attitude Goodhearted {
				"I have pouches, bags and back packs of the finest leather for sale."
			}
		}
		#KEY "*lantern*" "*candle*" "*torch*" {
			#Attitude Wicked {
				"I can sell thee candles, torches or a lantern."
			}
			#Attitude Neutral {
				"I can sell thee candles, torches or a lantern."
			}
			#Attitude Goodhearted {
				"I can sell thee candles, torches or a lantern to light thy way."
			}
		}
#KEY "*food*"{
#Attitude Wicked {
"I have rations available.",
		"Thou canst buy bread, meat and fruit here."
		}
#Attitude Neutral {
		"I have excellent trail rations.",
		"Thou can buy bread, meat and fruit here."
		}
#Attitude Goodhearted {
"I have trail rations, delicious and sustaining.",
		"I can supply thee with bread, meat and fruit."
		}
		}

#KEY "*bread*" {
#Attitude Wicked {
		"I have bread."
		}
#Attitude Neutral {
		"I have good dark bread, fresh baked and guaranteed to last."
		}
#Attitude Goodhearted {
		"I have delicious bread, fresh baked just this morning."
		}
		}
#KEY "*meat*", "*mutton*", "*bird*", "*chicken*" {
#Attitude Wicked {
		"We have roasted and smoked meats.",
		"I can sell thee roast mutton, smoked chicken or smoked game birds."
		}
#Attitude Neutral {
		"We have fine roasted and smoked meats.",
		"Our roast mutton, smoked chicken or smoked game birds are fine supplies for a journey."
		}
#Attitude Goodhearted {
		"We have fine roasted and smoked meats.",
		"Our roast mutton, smoked chicken or smoked game birds are fit for the table of Lord British himself!"
		}
		}
#KEY "*ale*", "*drink*", "*beer*", "*liquor*", "*wine*", "*cider*" {
#Attitude Wicked {
		"I sell drink by the bottle or flask.",
		"I have ale, wine, liquor or cider."
		}
#Attitude Neutral {
		"I sell strong drink of all types.",
		"My ale, wine, liquor and cider are all of the highest quality."
		}
#Attitude Goodhearted {
		"I sell strong drink of all types.",
		"My ale, wine, liquor and cider are all fit for a lord."
		}
		}
#KEY "*fruit*", "*pear*", "*apple*" {
#Attitude Wicked {		
		"I can sell thee fruit.", 
		"I have pears and apples."
		}
#Attitude Neutral {
		"I can sell thee fresh fruit.",
		"I have large, sweet pears and apples."
		}
#Attitude Goodhearted {
		"I sell only the freshest fruit.",
		"I have pears and apples the like of which thou hast never seen."
		}
}
}
#Sophistication Medium {
#KEY "*job","*job*", "*what*do*do*", "*profession*", "*occupation*" {

 #Attitude Wicked {
"I provide provisions.",
"I run the general store."
			}
#Attitude Neutral {
	"I sell general provisions.",
	"I run the general store."
		}
#Attitude Goodhearted {
	"I supply the necessities of the discriminating traveler.",
	"I run the general store."
		}
}

#KEY "*provision*" "*suppl*" "*ration*" "*equip*" {

#Attitude Wicked {
	"I carry supplies and provisions for travellers.",
	"I stock food and other supplies."
		}
#Attitude Neutral {
"I carry supplies and provisions for travellers.",
"I stock food and other supplies."
		}
#Attitude Goodhearted {
"I stock the finest supplies and provisions.",
"I supply excellent food and other necessities for travelers."
		}
		}
#KEY "*arrow*" "*bolt*" {
#Attitude Wicked {
	"I have arrows and crossbow bolts for sale."
		}
#Attitude Neutral {
		"I sell arrows and crossbow bolts."
		}
#Attitude Goodhearted {
		"I can sell thee fine arrows and crossbow bolts."
		}
		}
#KEY "*pouch*", "*bag*", "*pack*" {
#Attitude Wicked {		
"I have pouches, bags and back packs for sale."
		}
#Attitude Neutral {
		"I have sturdy pouches, bags and backpacks for sale."
		}
#Attitude Goodhearted {
		"I have pouches, bags and back packs of the finest leather for sale."
		}
		}
#KEY "*lantern*" "*candle*" "*torch*" {
#Attitude Wicked {
		"I can sell thee candles, torches or a lantern."
		}
#Attitude Neutral {
		"I can sell thee candles, torches or a lantern."
		}
#Attitude Goodhearted {
		"I can sell thee candles, torches or a lantern to light thy way."
		}
		}

#KEY "*food*"{
#Attitude Wicked {
"I have rations available.",
		"Thou canst buy bread, meat and fruit here."
		}
#Attitude Neutral {
		"I have excellent trail rations.",
		"Thou can buy bread, meat and fruit here."
		}
#Attitude Goodhearted {
"I have trail rations, delicious and sustaining.",
		"I can supply thee with bread, meat and fruit."
		}
		}

#KEY "*bread*" {
#Attitude Wicked {
		"I have bread."
		}
#Attitude Neutral {
		"I have good dark bread, fresh baked and guaranteed to last."
		}
#Attitude Goodhearted {
		"I have delicious bread, fresh baked just this morning."
		}
		}
#KEY "*meat*", "*mutton*", "*bird*", "*chicken*" {
#Attitude Wicked {
		"We have roasted and smoked meats.",
		"I can sell thee roast mutton, smoked chicken or smoked game birds."
		}
#Attitude Neutral {
		"We have fine roasted and smoked meats.",
		"Our roast mutton, smoked chicken or smoked game birds are fine supplies for a journey."
		}
#Attitude Goodhearted {
		"We have fine roasted and smoked meats.",
		"Our roast mutton, smoked chicken or smoked game birds are fit for the table of Lord British himself!"
		}
		}
#KEY "*ale*", "*drink*", "*beer*", "*liquor*", "*wine*", "*cider*" {
#Attitude Wicked {
		"I sell drink by the bottle or flask.",
		"I have ale, wine, liquor or cider."
		}
#Attitude Neutral {
		"I sell strong drink of all types.",
		"My ale, wine, liquor and cider are all of the highest quality."
		}
#Attitude Goodhearted {
		"I sell strong drink of all types.",
		"My ale, wine, liquor and cider are all fit for a lord."
		}
		}
#KEY "*fruit*", "*pear*", "*apple*" {
#Attitude Wicked {		
		"I can sell thee fruit.", 
		"I have pears and apples."
		}
#Attitude Neutral {
		"I can sell thee fresh fruit.",
		"I have large, sweet pears and apples."
		}
#Attitude Goodhearted {
		"I sell only the freshest fruit.",
		"I have pears and apples the like of which thou hast never seen."
		}

}
}
#Sophistication Low {
#KEY "*job","*job*", "*what*do*do*", "*occupation*", "*profession*" {

 #Attitude Wicked {
"I sell provisions.",
"I run the general store."
			}
#Attitude Neutral {
	"I sell general provisions.",
	"I run the general store."
		}
#Attitude Goodhearted {
	"I supply the stuff that travelers might be needin'.",
	"I run the general store."
		}
}

#KEY "*provision*" "*suppl*" "*ration*" "*equip*" {

#Attitude Wicked {
	"I carry supplies and stuff for travellers.",
	"I got food and other supplies."
		}
#Attitude Neutral {
"I carry supplies and things for travellers.",
"I got some food and other supplies."
		}
#Attitude Goodhearted {
"I stock the best supplies and stuff I can get my hands on.",
"I got real good food and other things for travelers."
		}
		}
#KEY "*arrow*" "*bolt*" {
#Attitude Wicked {
	"I got arrows and crossbow bolts for sale."
		}
#Attitude Neutral {
		"I sell arrows and crossbow bolts."
		}
#Attitude Goodhearted {
		"I can sell thee some arrows and crossbow bolts."
		}
		}
#KEY "*pouch*", "*bag*", "*pack*" {
#Attitude Wicked {		
"I got pouches, bags and back packs for sale."
		}
#Attitude Neutral {
		"I got some sturdy pouches, bags and backpacks for sale."
		}
#Attitude Goodhearted {
		"I got pouches, bags and back packs of the best leather for sale."
		}
		}
#KEY "*lantern*" "*candle*" "*torch*" {
#Attitude Wicked {
		"I can sell candles, torches or a lantern."
		}
#Attitude Neutral {
		"I can sell candles, torches or a lantern."
		}
#Attitude Goodhearted {
		"I can sell candles, torches or a lantern to light thy way."
		}
		}

#KEY "*food*"{
#Attitude Wicked {
"I have rations available.",
		"Thou can buy bread, meat and fruit here."
		}
#Attitude Neutral {
		"I carry some real good trail rations.",
		"Thou can buy bread, meat and fruit here."
		}
#Attitude Goodhearted {
"I got trail rations. They're pretty good, and they'll fill thy stomach well enough.",
		"I can get thee some bread, meat and fruit."
		}
		}

#KEY "*bread*" {
#Attitude Wicked {
		"I got bread."
		}
#Attitude Neutral {
		"I got some good dark bread, fresh baked and guaranteed to last a while."
		}
#Attitude Goodhearted {
		"I got some really good bread, fresh baked just this morning."
		}
		}
#KEY "*meat*", "*mutton*", "*bird*", "*chicken*" {
#Attitude Wicked {
		"We have roasted and smoked meats.",
		"I can sell thee roast mutton, smoked chicken or smoked game birds."
		}
#Attitude Neutral {
		"We got some fine roasted and smoked meats.",
		"Our roast mutton, smoked chicken or smoked game birds are real nice for a journey."
		}
#Attitude Goodhearted {
		"We got some really fine roasted and smoked meats.",
		"Our roast mutton, smoked chicken or smoked game birds are fit for Lord British himself!"
		}
		}
#KEY "*ale*", "*drink*", "*beer*", "*liquor*", "*wine*", "*cider*" {
#Attitude Wicked {
		"I sell drink by the bottle or flask.",
		"I got ale, wine, liquor or cider."
		}
#Attitude Neutral {
		"I sell strong drink of all types.",
		"My ale, wine, liquor and cider are all of the best quality."
		}
#Attitude Goodhearted {
		"I sell strong drink of all types.",
		"My ale, wine, liquor and cider are all fit for a lord."
		}
		}
#KEY "*fruit*", "*pear*", "*apple*" {
#Attitude Wicked {		
		"I can sell thee fruit.", 
		"I got some pears and apples."
		}
#Attitude Neutral {
		"I can sell thee fresh fruit.",
		"I got large, sweet pears and apples."
		}
#Attitude Goodhearted {
		"I sell only the freshest fruit.",
		"I got pears and apples the like of which thou ain't never seen."
		}
}
}
		}
