// Britain Local knowledge fragment
//
// Keywords:
// thief, steal
// smith
// blacksmith
// weaponsmith
// conservatory, bard, minstrel, musician, troubad(our/or)
// mage tower, mage guild
// stables
// vet
// theater, theatre
// carpenter, woodworker
// guild
// mining guild, miners
// baker, bread
// tanner, leather
// healer
// bowyer, fletcher
// butcher
// armour, armor
// inn
// wayfarer's inn, wayfarers inn
// blue boar
// sweet dreams
// alchemist
// weaponeer
// mechanician
// artisan
// provision
// jewel(er)
// bank
// fishmonger
// clothiers, clothes
// tavern
// cat's lair
// shipwright, oaken oar, boats, ships
// customs
// weapons trainer, train
// salty dog
// lighthouse
// Lord British
// LB
// castle
// old keep, fighter, warrior
// farms, farmers
// river
// narrows, narrows neck
// Brittany river
// gate
// poor gate
// main gate
// bridge gate
// mage gate
// city wall, wall
// bridge
// ankh, temple, resur(rection)
// paws
// brittany bay
// ocean
// where am I, I'm lost
// orcs camps encampments
// skeletons graveyard crypt mausoleum
//
// Notes: virtually everything here needs responses with the macros added
// later so that the NPC can give directions to the place dynamically. But
// for now these responses have been left out. Those responses will go in
// the High Sophistication layer.
//
// -Raph Koster
//

#Fragment Britannia, Local, Britannia_Britain {
    #Sophistication High {
        #Key "*where am i*", "*m lost*" {
            "Why, thou'rt in the city of Britain, of course.",
	"Thou art in Britain.",
	"Thou art in Britain, $milord/milady$."
        }
        #Key "*thief*", "*thiev*", "*steal*" {
            "I know naught of thieves!",
            "Indeed, there be thievery in Britain."
        }
	#Key "*dummy*", "*Training dummy*", "*dummies*", "*training dummies*" {
		"The Cavalry Guild in Northeast Britain has sword-training dummies, if that is what thou art looking for.",
		"Thou can work on thy swordplay at the old keep, south of Lord British's castle.",
		"I think that Blackthorn's castle has some training devices, but I'm not certain."
	}
        #Key "*smith*" {
            "Which sort of smith? Weaponsmith, blacksmith?",
            "There are many sorts of smith."
        }
        #Key "*blacksmith*" {
            "There is a blacksmith beside the castle moat on the northern edge of town."
        }
        #Key "*weaponsmith*" {
            "Thou mightest wish to check at the armourer's, or the castle.",
            "Thou canst buy weapons at the blacksmith's shop."
        }
        #Key "*conservatory*", "*bard*", "*minstrel*", "*musician*", "*troubad*" {
            "The center of the musical arts in Britain is the Bardic Conservatory on the north side of town."
        }
        #Key "*mage tower*", "*mage guild*", "*mage*"{
            "The Mage Tower is the building with all the arches, made of grey stone, on the north side of the town.",
            "Ah, the Mage Guild is housed within a tower of many arches, with pools of water beside it."
        }
	#Key "*mage's shop*", "*magic*", "*magic shop*" "*Mage shop*"
	{
		"There are three or four merchants of the arcane in Britain.",
		"The magic shop The Sorcerer's Delight is also known as the Mage Tower. Thou can purchase magical items there.",
		"Ethral Goods Magic Shop has magical items for sale, and it boasts the only alchemist in Britain.",
		"The magic shop Incantations and Enchantments is situated by the park in eastern Britain.",
		"Sage Advice Magic Shop can sell thee some arcane items, if that is thy desire."
	}
	#Key "*Sorcerer's Delight*", "*Mage tower*"
	{
		"Thou can purchase arcane goods at the Sorcerer's Delight - which some call the Mage Tower. It can be found in northern Britain, next to the Conservatory of Music."
	}
	#Key "*Ethral*", "*Ethral Goods*"
	{
		"Thou can find the Ethral Goods Magic Shop just south of the Main Gate. Next to the Premier Provisioner's shop."
	}
	#Key "*Incantations and Enchantments*", "*Incantations*"
	{
		"Incantations and Enchantments is in the middle of eastern Britain, on the south side of the park."
	}
	#Key "*Sage Advice*"
	{
		"Thou can get to the Sage Advice Magic Shop on the east side of the river."
	}
        #Key "*stables*" {
            "Aside from Lord British's stables, the only ones I can think of lie on the western bank of the river.",
            "Try the stables beside the Mage Tower."
        }
        #Key "*vet*" {
            "There is a doctor of animals whose shop lies beside The Bucking Horse Stables.",
            "The city's sole veterinarian lives by the river, by the Mage's Bridge."
        }
        #Key "*theater*", "*theatre*" {
            "Lord British supports the arts. There is a public theatre across from the Bardic Conservatory."
        }
        #Key "*carpenter*", "*woodworker*" {
            "A goodly carpenter lives not far from the market square before the castle gate.",
            "There is a woodworker near the city library."
        }
        #Key "*guild*" {
            "There are many guilds in this city. 'Tis the benefit of living in Lord British's capital."
        }
        #Key "*miners*", "*mining guild*" {
            "There is a Mining Guild beside the city library."
        }
        #Key "*baker*", "*bread*" {
            "There's a baker on the market square before the castle gate."
        }
        #Key "*tanner*", "*leather*" {
            "A tanner's shop exists by Poor Gate, 'gainst the old city wall.",
            "There is a leatherworker's shop facing the market square."
        }
        #Key "*healer*" {
            "There is a healer on the market square, near the Main Gate.",
            "If thou dost walk north through the main gates, thou will be facing the healer's shop."
        }
        #Key "*bowyer*", "*fletcher*" {
            "Try the bowyer just East of the theatre."
        }
        #Key "*butcher*" {
            "The butcher can be found between the docks, near the customs house.",
		"In Southern Britain on the waterfront there is a butcher. His shop is between the two docks."
        }
        #Key "*armour*", "*armor*" {
            "There is an armourer's shop beside the Mage Gate. Just South of the old city wall.",
	"Strength and Steel Armory is located in front of the Mage's Tower in Northern Britain."
        }
        #Key "*inn*" {
            "If there be one thing Britain is not lacking, it be inns!",
            "The Wayfarer's Inn is just to the East of the Mage's Bridge.",
            "An inn called Sweet Dreams lies next to the Main Gate, just East of the healer's shop.",
	"The North Side Inn is way up in North Britain, just past the Mage's Tower."
        }
        #Key "*Wayfarer's Inn*", "*Wayfarers Inn*" {
            "'Tis South of the Cavalry Guild by the Mage's Gate.",
            "The Wayfarer's Inn is beside the Mage's Gate, on the East side of the river."
        }
        #Key "*Sweet Dreams*" {
            "The Sweet Dreams Inn is quite upscale!",
            "Despite its name, the Sweet Dreams inn is quite good. It is near the Main Gate."
        }
	#Key "*Northside*" {
            "The Northside Inn is situated on the lake that surrounds Blackthorn's Castle.",
            "The Northside Inn is quite good. It is just North of the Music Conservatory."
        }
        #Key "*alchemist*" {
            "The alchemist can be found with the mages in the the magic shop in front of the Main Gate.",
		"Ethral Goods Magic Shop has an alchemist. Try there."
        }
        #Key "*weaponeer*" "*weaponsmith*" {
            "The blacksmith North of the Miner's Guild can probably sell thee anything thou might need."
        }
        #Key "*mechanician*" {
            "The guildhall of the Mechanicians, or the Tinkers as some call them, is right beside Poor Gate."
        }
        #Key "*artisan*" {
            "There be a guildhall for the artisans of the town outside the old walls, south of the Armorer's."
        }
        #Key "*provision*" {
            "One can purchase provisions at any of a number of shops.",
            "There is a provisioner's directly before the Main Gate.",
            "A quite well-appointed provisioner's exists East of the river, just North of the Lighthouse."
        }
        #Key "*jewel*" {
            "There is a jeweler's just to the South of the Artist's guild.",
		"About as far East as thou can go, there is a jeweler's shop sitting next to the park."
        }
        #Key "*bank*" {
            "The First Bank of Britain lies next to the jeweler's, beside the moat.",
		"The bank is outside the old walls, next to the moat.",
        }
        #Key "*clothes*", "*clothiers*" {
            "A goodly shop for clothing is next to the Blue Boar Tavern.",
            "Thou canst purchase clothes on the East side of the river, near the Cypress Bridge."
        }
        #Key "*tavern*" {
            "If thou seekest a place to drink, well, there are many choices!",
		"There's the Salty Dog, The Unicorn's Horn, The Cat's Lair, and The Blue Boar taverns, here in Britain.",
	}
       #Key "*tavern*", "*Salty Dog*" {
            "If thou seekest a place to drink, well, there are many choices!",
            "The Salty Dog is a tavern overlooking the park, right next to The Wayfarer's Inn."
	}
       #Key "*tavern*", "*Unicorn Horn*" {
            "If thou seekest a place to drink, well, there are many choices!", 
            "'Tis an expensive place, but the Unicorn Horn, overlooking the ocean on the east side, serves a decent ale."
        }
       #Key "*tavern*", "*Blue Boar*" {
            "If thou seekest a place to drink, well, there are many choices!", 
            "The Blue Boar is just North of the library, on the river."
        }
       #Key "*tavern*", "*Cat's Lair*", {
            "If thou seekest a place to drink, well, there are many choices!", 
            "The Cat's Lair is a bit disreputable, as it lies near the docks, but its beer is good."
        }
        #Key "*shipwright*", "*oaken oar*", "*boats*", "*ships*" {
            "The place to buy waterfaring vessels is the Oaken Oar.",
            "The Oaken Oar houses excellent shipwrights. It is on the waterfront by the mouth of the Narrow Neck."
        }
	#Key "*mapmaker*", "*cartographer*", "* map*" {
            "Cartographer's can usually be found sharing space with shipwrights.",
            "If there's not a mapmaker to be found at the Oaken Oar, then I can't help thee much."
        }
        #Key "*customs*", "*customs house*" {
            "The Customs office checks imported goods. 'Tis on the waterfront.",
            "Smugglers from Buccaneer's Den have taken to going to Vesper since the Customs House opened on the waterfront."
        }
        #Key "*train*", "*weapons trainer*" {
            "Thou canst be trained in weapons at the weapons trainer. But thou shouldst be warned that 'tis expensive. He caters to the nobility.",
            "The weapons trainer? Ah, that is on the north edge of town, on the eastern bank of the river. 'Tis called the Cavalry Guild."
        }
        #Key "*lighthouse*" {
            "'Tis being repaired, but it stands on the promontory o'erlooking Brittany Bay.",
            "The lighthouse is a most imposing sight, but 'twas badly damaged in a storm, and it is inoperable right now."
        }
        #Key "*Lord British*" {
            "He liveth here, in Britain, in his castle.",
            "Lord British doth reside in his castle, but the gates are always open."
        }
        #Key "* LB*" {
            "LB? Mayhap thou means Lord British ...."
        }
        #Key "*castle*" {
            "Lord British's castle is quite huge. A moat encircles it. Thou canst not miss it.",
            "Lord British's castle forms the western side of the city. Only farms lie beyond it.",
		"Blackthorn's castle is on an island in the middle of a lake North of Britain."
        }
	#Key "*blackthorn's castle*", "*blackthorn*"  {
            "Blackthorn stays out of sight mostly, sequestered in his castle up North of Britain.",
            "Blackthorn's castle is on an island in the middle of a lake North of Britain."
        }
        #Key "*old keep*", "*fighter*", "*warrior*" {
            "The Warrior's Guild is housed in the Old Keep.",
            "The Old Keep, where the Warrior's Guild is headquartered, was once the castle hereabouts, but now we have Lord British's castle.",
            "'Tis crumbling into the Narrows, but the home of the Warrior's Guild is the Old Keep."
        }
        #Key "*farms*", "*farmers*" {
            "The farms in this area are all in the countryside, to the west of Lord British's castle. Thou must cross the Narrows and follow the road there."
        }
        #Key "*river*" {
            "Britain is bounded by two rivers. The Narrows is on the west and Brittany River on the east.",
            "When most people say the river, they mean Brittany River, which runs through the middle of the city."
        }
        #Key "*narrows*", "*narrows neck*", "*neck*" {
            "Narrows Neck is the name for the river mouth on the southwestern side of Britain."
        }
        #Key "*Brittany River*" {
            "That's the river that runs down the middle of the city."
        }
        #Key "*gate*" {
            "There are two gates in the old city walls. The Poor Gate, and the Main Gate are the names."
        }
        #Key "*poor gate*" {
            "That is the name of the old gate beside the castle moat, for once the poor came in through there to go to market. It is quite narrow.",
            "Poor Gate is on the south wall, beside the Tinker's Guild."
        }
        #Key "*Main gate*" {
            "The Main Gate is quite majestic! The space before it is paved with grey and red stones, and there be guard towers on either side.",
            "The Main Gate divides the central city from the waterfront."
        }
        #Key "*city wall*", "*wall*" {
            "When Britain was but a young city, 'twas enclosed by a wall. But it has long since outgrown it.",
            "Thou canst see that the old city walls are of a different vintage stone than the castle proper. 'Twas built before the current castle buildings.",
            "The northern side of the old city wall hath disappeared completely. 'Tis said the Mage Tower was built from its fragments."
        }
        #Key "*bridge*" {
 		"There are many bridges across the Brittany. There's the Great Northern Bridge at the north-most end, the Mage's Bridge, then the one we call Virtue's Pass. South of that there's Cypress Bridge, and the River's Gate Bridge. And, of course, the
 Gung-Farmer's Bridge connects Britain with the farmlands to the West.",
 		"The bridge next to the Mage's tower is thus called the Mage's Bridge.",
		"The Great Northern Bridge is so called because it's the Northern-most bridge in Britain.",
		"Cypress Bridge is named for the trees which greet thee coming and going across it.",
		"Virtue's Pass connects to Virtue's Path, which will take thee into Lord British's Castle.",
		"There is the Gung-Farmer's Bridge, which leadeth across Narrows Neck."
        }
	#Key "*Northern bridge*", "*Great Northern*", "*Great bridge*"   {
		"The Great Northern Bridge is so called because it's the Northern-most bridge in Britain.",
		"The Great Northern Bridge is built fairly high above the water."
        }
	#Key "*Cypress Bridge*" {
		"Cypress Bridge is named for the trees which greet thee coming and going across it.",
     		"The Cypress Bridge is surrounded on one side by a tailor's shop and on the other by a magic shop."
        }
	#Key "*Virtue's Pass*" {
		"Virtue's Pass connects to Virtue's Path, which will take thee into Lord British's Castle.",
            	"There used to be a gate at Virtue's Pass, it was torn down some time ago."
        }
	#Key "*Mage's Bridge*" {
		"The bridge next to the Mage's tower is thus called the Mage's Bridge.",
		"Mage's Bridge was once a gate leading into the city, but the old wall has outlived its usefulness and most of it has been torn down.",
    		"The Mage's Bridge is so named for the fact that the Mage's Tower almost overlooks it."
        }
	#Key "*River's Gate*" {
		"The Southernmost bridge is called River's Gate. "
        }
	#Key "*Gung-Farmer*", "*Gung Farmer*"  {
		"The Gung-Farmer's Bridge connects all the farms with Britain proper. "
        }
        #Key "*ankh*", "*death*", "*temple*", "*resur*" {
            "Thou mayst find the temple, where thou canst be resurrected, directly by Mage Gate Bridge.",
            "The ankh in the temple is capable of restoring life to the dead. 'Tis in northern part of town, beside the river."
        }
        #Key "*paws*" {
            "Paws? Take a look at the feet of a dog!"
        }
        #Key "*guardhouse*", "*guard house*" {
            "There are two guardhouses. One near the Gung-Farmer's Bridge, and the other next to Virtue's Pass.",
            "One Guardhouse is right beside the Cat's Lair Tavern."
        }
        #Key "*waterfront*" {
            "The waterfront is what many call everything south of the old city wall. Properly speaking, it runneth along the river from River's Gate Bridge, down along the water to the Oaken Oar and the Narrows."
        }
        #Key "*moat*" {
            "Long ago the Narrows were dug up to meet the moat, and now 'tis fed from the water of Brittany Bay."
        }
        #Key "*brittany bay*" {
            "'Tis the bay on our southern coast."
        }
        #Key "*ocean*" {
            "The ocean is to the south of Britain."
        }
        #Key "*undead*", "*skeleton*", "*graves*", "*graveyard*", "*crypt*", "*cemetery*", "*mausoleum*" {
            "Ah, 'tis a terrible thing, but the graveyard far to the south has become infested with the living bones of the dead.",
            "Far to the south of here, across the Narrows, there is a crypt full of skeletons come to life--a frightening place.",
            "We have had to abandon our mausoleums, for the cemetery south of here has been touched by magic and is now filled with undead skeletons."
        }
        #Key "*orc*", "*camp*" {
            "There is a plague of orcs on the western side of the farms--if thou seekest their encampments, that is the place to look."
        }
    }
    #Sophistication Medium {
        #Key "*undead*", "*skeleton*", "*graves*", "*graveyard*", "*crypt*", "*cemetery*", "*mausoleum*" {
            "Ah, 'tis a terrible thing, but the graveyard far to the south has become infested with the living bones of the dead.",
            "Far to the south of here, across the Narrows, there is a crypt full of skeletons come to life--a frightening place.",
            "We have had to abandon our mausoleums, for the cemetery south of here has been touched by magic and is now filled with undead skeletons."
        }
	#Key "*where am i*", "*m lost*" {
            	"Why, thou'rt in the city of Britain, of course.",
		"Thou art in Britain, $milord/milady$."
        }
        #Key "*thief*", "*thiev*", "*steal*" {
            "I know naught of thieves!",
            "Indeed, there be thievery in Britain."
	}
	#Key "*dummy*", "*training dummy*", "*dummies*", "*training dummies*" {
		"Thy swordsmanship can be practiced at the old keep or at the Cavalry Guild.",
		"The Cavalry Guild has training dummies. They can be found just north of The Wayfarer's Inn.",
		"Just south of Castle Britain is the old keep. The Warrior's Guild now calls it home. Thou can find dummies there."
	}
        #Key "*smith*" {
            "Which sort of smith? Weaponsmith, blacksmith?",
            "There are many sorts of smith."
        }
        #Key "*blacksmith*" {
            "There is a blacksmith beside the castle moat on the northern edge of town."
        }
        #Key "*weaponsmith*" {
            "Thou mightest wish to check at the armourer's, or the castle.",
            "Thou canst buy weapons at the blacksmith's shop."
        }
        #Key "*conservatory*", "*bard*", "*minstrel*", "*musician*", "*troubad*" {
            "The center of the musical arts in Britain is the Bardic Conservatory on the north side of town."
        }
        #Key "*mage tower*", "*mage guild*", "*mage*" {
            "The Mage Tower is the building with all the arches, made of grey stone, on the north side of the town.",
            "Ah, the Mage Guild is housed within a tower of many arches, with pools of water beside it."
        }
	#Key "*mage's shop*", "*magic*", "*magic shop*" "*Mage shop*"
	{
		"I thinik there are four merchants of magic in Britain.",
		"The Sorcerer's Delight is also known as the Mage Tower. Thou can purchase magical items there.",
		"Ethral Goods Magic Shop has magical items for sale, and it boasts the only alchemist in Britain.",
		"The magic shop Incantations and Enchantments is situated by the park in eastern Britain.",
		"Sage Advice Magic Shop can sell thee some arcane items, if that is thy desire."
	}
	#Key "*Sorcerer's Delight*", "*Mage tower*"
	{
		"Thou can purchase magical wares at the Sorcerer's Delight - which some call the Mage Tower. It can be found in northern Britain, next to the Conservatory of Music."
	}
	#Key "*Ethral*", "*Ethral Goods*"
	{
		"Thou can find the Ethral Goods Magic Shop just south of the Main Gate. It's next to the Premier Provisioner's shop."
	}
	#Key "*Incantations and Enchantments*", "*Incantations*"
	{
		"The shop called Incantations and Enchantments is in the middle of eastern Britain, on the south side of the park."
	}
	#Key "*Sage Advice*"
	{
		"Thou can find the Sage Advice Magic Shop on the east side of the river."
	}

        #Key "*stables*" {
            "Aside from Lord British's stables, the only ones I can think of lie West of the Keep.",
            "Try the stables beside the Mage Tower."
        }
        #Key "*vet*" {
            "There is a doctor of animals whose shop lies beside the Mage Tower.",
            "The city's sole veterinarian liveth by the river, by the Mage's Bridge."
        }
        #Key "*theater*", "*theatre*" {
            "Lord British supports the arts. There is a public theatre across from the Bardic Conservatory."
        }
        #Key "*carpenter*", "*woodworker*" {
            "A goodly carpenter can be found not far from the market square before the castle gate.",
            "There is a woodworker near the public library."
        }
        #Key "*guild*" {
            "There are many guilds in this city. 'Tis the benefit of living in Lord British's capital."
        }
        #Key "*miners*", "*mining guild*" {
            "There is a Mining Guild beside the public library."
        }
        #Key "*baker*", "*bread*" {
            "There is a baker on the market square before the castle gate."
        }
        #Key "*tanner*", "*leather*" {
            "A tanner's shop exists by Poor Gate, in front of the castle gates.",
            "There is a leatherworker's shop facing the market square, just next to the bakery."
        }
        #Key "*healer*" {
            "There is a healer on the market square, near the Main Gate.",
            "If thou dost walk north through the main gates, thou will be facing the healer's shop."
        }
        #Key "*bowyer*", "*fletcher*" {
            "Try the bowyer just East of the theatre."
        }
        #Key "*butcher*" {
            "Britain's butcher has a shop on the waterfront, next to the custome house.",
		"The butcher can be found between the two docks, on the waterfront."
        }
        #Key "*armour*", "*armor*" {
           "There is an armourer's shop beside the Mage Gate. Just South of the old city wall.",
	"Strength and Steel Armory is located in front of the Mage's Tower in Northern Britain.",
	"There's an Armorer way out to the East. Just North of the Lighthouse."
        }
	#Key "*inn*" {
            "If there be one thing Britain is not lacking, it be inns!",
            "The Wayfarer's Inn is just to the East of the Mage's Bridge.",
            "An inn called Sweet Dreams lies next to the Main Gate, just East of the healer's shop.",
	"The North Side Inn is way up in North Britain, just past the Mage's Tower."
        }
        #Key "*Wayfarer's Inn*", "*Wayfarers Inn*" {
            "'Tis South of the Cavalry Guild by the Mage's Gate.",
            "The Wayfarer's Inn is beside the Mage's Gate, on the East side of the river."
        }
        #Key "*Sweet Dreams*" {
            "The Sweet Dreams Inn is quite upscale!",
            "Despite its name, the Sweet Dreams inn is quite good. It is near the Main Gate."
        }
	#Key "*Northside*" {
            "The Northside Inn is situated on the lake that surrounds Blackthorn's Castle.",
            "The Northside Inn is quite good. It is just North of the Music Conservatory."
        }
        #Key "*alchemist*" {
            "The alchemist shares space in the Ethral Goods Magic Shop.",
		"The only alchemist I know of is in the magic shop near the Main Gate."
        }
        #Key "*weaponeer*" "*weaponsmith*" {
            "The armorer beside the Tinker's Guild can probably sell thee anything thou mightest need. Or look to the blacksmith."
        }
        #Key "*mechanician*" {
            "The guildhall of the Mechanicians is right beside Poor Gate. Some do call them Tinkerers, if thou were wondering."
        }
        #Key "*artisan*" {
            "There be a guildhall for the artisans of the town outside the old walls, south of the armorer's."
        }
        #Key "*provision*" {
            "One can purchase provisions at any of a number of shops.",
            "There is a provisioner's directly before the Main Gate.",
            "A quite well-appointed provisioner's exists in the Eastern part of Britain."
        }
        #Key "*jewel*" {
            "There is a jeweler's by the Artist's Guild."
        }
        #Key "*bank*" {
            "The First Bank of Britain lies across the street from the Jeweler's, beside the moat."
        }
        #Key "*clothes*", "*clothiers*" {
            	"A goodly shop for clothing is next to Cypress Bridge, on the East side of the river.",
		"A good shop for clothing is next to the Blue Boar Tavern.",
            	"Thou can purchase clothes on the east side of the river."
        }
       	#Key "*tavern*" {
            	"If thou seekest a place to drink, well, there are many choices!",
		"There's the Salty Dog, The Unicorn's Horn, The Cat's Lair, and The Blue Boar taverns, here in Britain.",
	}
        #Key "*tavern*", "*Salty Dog*" {
            "If thou seekest a place to drink, well, there are many choices!",
            "The Salty Dog is a tavern overlooking the park, right next to The Wayfarer's Inn."
	}
       #Key "*tavern*", "*Unicorn Horn*" {
            "If thou seekest a place to drink, well, there are many choices!", 
            "'Tis usually an expensive place, but the Unicorn Horn, overlooking the ocean on the east side, serves a decent ale."
        }
       #Key "*tavern*", "*Blue Boar*" {
            "If thou seekest a place to drink, well, there are many choices!", 
            "The Blue Boar is just North of the library, on the river."
        }
       #Key "*tavern*", "*Cat's Lair*", {
            "If thou seekest a place to drink, well, there are many choices!", 
            "The Cat's Lair is a bit disreputable, as it lies near the docks, but its beer is good."
        }
        #Key "*shipwright*", "*oaken oar*", "*boats*", "*ships*" {
            "The place to buy waterfaring vessels is the Oaken Oar.",
            "The Oaken Oar houses excellent shipwrights. It is on the waterfront by the mouth of the Narrows Neck."
        }
	#Key "*mapmaker*", "*cartographer*", "* map*" {
            "Cartographer's can usually be found sharing space with shipwrights.",
            "If there's not a mapmaker to be found at the Oaken Oar, then I can't help thee much."
	}
        #Key "*customs*", "*customs house*" {
            "The Customs office checks imported goods. 'Tis on the waterfront.",
            "Smugglers from Buccaneer's Den have taken to going to Vesper since the Customs House opened on the waterfront."
        }
        #Key "*train*", "*weapons trainer*" {
            "Thou canst be trained in weapons at the weapons trainer. But thou shouldst be warned that 'tis expensive. He caters to the nobility.",
            "The weapons trainer? Ah, that is on the north edge of town, on the eastern bank of the river. 'Tis called the Cavalry Guild."
        }
        #Key "*lighthouse*" {
            "'Tis being repaired, but it stands on the promontory o'erlooking Brittany Bay.",
            "The lighthouse is a most imposing sight, but 'twas badly damaged in a storm, and 'tis inoperable."
        }
        #Key "*Lord British*" {
            "He liveth here, in Britain, in his castle.",
            "Lord British doth reside in his castle, but the gates are always open."
        }
        #Key "* LB*" {
            "LB? Mayhap thou means Lord British ...."
        }
	#Key "*blackthorn's castle*", "*blackthorn*"  {
            "Blackthorn stays out of sight mostly, sequestered in his castle up North of Britain.",
            "Blackthorn's castle is on an island in the middle of a lake North of Britain."
        }
        #Key "*castle*" {
            	"Lord British's castle is quite huge. A moat encircles it. Thou canst not miss it.",
            	"Lord British's castle forms the western side of the city. Only farms lie beyond it.",
		"Blackthorn's castle is on an island in the middle of a lake North of Britain."
        }
        #Key "*old keep*", "*fighter*", "*warrior*" {
            "The Warrior's Guild is housed in the Old Keep.",
            "The Old Keep, where the Warrior's Guild is headquartered, was once the castle hereabouts, but now we have Lord British's castle.",
            "'Tis crumbling into the Narrows, but the home of the Warrior's Guild is the Old Keep."
        }
        #Key "*farms*", "*farmers*" {
            "The farms in this area are all in the countryside, to the west of Lord British's castle. Thou must cross the Narrows and follow the road there."
        }
        #Key "*river*" {
            "Britain is bounded by two rivers. The Narrows is on the west and Brittany River on the east.",
            "When most people say the river, they mean Brittany River, which runs through the middle of the city."
        }
        #Key "*narrows*", "*narrows neck*" "*neck*" {
            "Narrows Neck is the name for the river mouth on the southwestern side of Britain."
        }
        #Key "*Brittany River*" {
            "That's the river that runs down the middle of the city."
        }
        #Key "*gate*" {
            "There are two gates in the old city walls. The Poor Gate and the Main Gate are their names."
        }
        #Key "*poor gate*" {
            "That is the name of the old gate beside the castle moat, for once the poor came in through there to go to market. It is quite narrow.",
            "Poor Gate is on the south wall, beside the Mechanicians' Guild."
        }
        #Key "*Main gate*" {
            "The Main Gate is quite majestic! The space before it is paved with grey and red stones, and there be guard towers on either side.",
            "The Main Gate divides the central city from the waterfront."
        }
        #Key "*city wall*", "*wall*" {
            "When Britain was but a young city, 'twas enclosed by a wall. But it has long since outgrown it.",
            "Thou canst see that the old city walls are of a different vintage stone than the castle proper. 'Twas built before the current castle buildings.",
            "The northern side of the old city wall hath disappeared completely. 'Tis said the Mage Tower was built from its fragments."
        }
	#Key "*bridge*" {
		"There are many bridges across the Brittany. There's the Great Northern Bridge at the north-most end, the Mage's Bridge, then the one we call Virtue's Pass. South of that there's Cypress Bridge, and the River's Gate Bridge. And, of course, the
 Gung-Farmer's Bridge connects Britain with the farmlands to the West.",
		"The bridge next to the Mage's tower is thus called the Mage's Bridge.",
		"The Great Northern Bridge is so called because it's the Northern-most bridge in Britain.",
		"Cypress Bridge is named for the trees which greet thee coming and going across it.",
		"Virtue's Pass connects to Virtue's Path, which will take thee into Lord British's Castle.",
            	"There is the Gung-Farmer's Bridge, which leadeth across Narrows Neck."
        }
	#Key "*Northern bridge*", "*Great Northern*", "*Great bridge*"   {
           	"The Great Northern Bridge is so called because it's the Northern-most bridge in Britain.",
            	"The Great Northern Bridge is built fairly high above the water."
        }
	#Key "*Cypress Bridge*" {
		"Cypress Bridge is named for the trees which greet thee coming and going across it.",
            	"The Cypress Bridge is surrounded on one side by a tailor's shop and on the other by a magic shop."
        }
	#Key "*Virtue's Pass*" {
		"Virtue's Pass connects to Virtue's Path, which will take thee into Lord British's Castle.",
            	"There used to be a gate at Virtue's Pass, it was torn down some time ago."
        }
	#Key "*Mage's Bridge*" {
		"The bridge next to the Mage's tower is thus called the Mage's Bridge.",
		"Mage's Bridge was once a gate leading into the city, but the old wall has outlived its usefulness and most of it has been torn down.",
		"The Mage's Bridge is so named for the fact that the Mage's Tower almost overlooks it."
        }
	#Key "*River's Gate*" {
		"The Southernmost bridge is called River's Gate."
        }
	#Key "*Gung-Farmer*", "*Gung Farmer*"  {
		"The Gung-Farmer's Bridge connects all the farms with Britain proper. "
        }
        #Key "*ankh*", "*death*", "*temple*", "*resur*" {
            "Thou mayst find the temple, where thou canst be resurrected, directly by Mage's Bridge.",
            "The ankh hath the power to restore life to the dead. 'Tis in the temple, by Mage's Bridge."
        }
        #Key "*paws*" {
            "Paws? Take a look at the feet of a dog."
        }
        #Key "*guardhouse*", "*guard house*" {
            "The two guardhouses are located near the Gung-Farmer's Bridge and next to the Sweet Dreams Inn.",
            "There is a Guardhouse right beside the Cat's Lair."
        }
        #Key "*waterfront*" {
            "The waterfront is what many call everything south of the old city wall. Properly speaking, it runneth along the river from River's Gate Bridge, down along the water to the Oaken Oar and the Narrows."
        }
        #Key "*moat*" {
            "Long ago the Narrows were dug up to meet the moat, and now 'tis fed from the water of Brittany Bay."
        }
        #Key "*brittany bay*" {
            "'Tis the bay on our southern coast."
        }
        #Key "*ocean*" {
            "The ocean is to the south of Britain."
        }
    }
    #Sophistication Low {
        #Key "*skeleton*", "*graves*", "*graveyard*", "*crypt*", "*undead*", "*cemetery*", "*mausoleum*" {
            "I don' like undead.",
            "There's a cemetery way to the south.",
            "Lots of skeletons have come to life in the cemetery."
        }
        #Key "*orc*", "*camp*" {
            "Orcs live in camps. They's dangerous, too."
        }
        #Key "*thief*", "*thiev*", "*steal*" {                                                                    
            "I dunno about thiefy types.",
            "I once had my pocket picked.",
            "Thou a thief? Don' steal nothin' from me!"
        }
	#Key "*dummy*", "*training dummy*", "*dummies*", "*training dummies*" {
		"Ummm... I think the keep's got some dummies.",
		"Up North, near to one o' the inns there's some sword-fightin' dummies."
	}
        #Key "*guild*" {
            "There's so many, I can't keep track of 'em."
        }
        #Key "*bridge*" {
            "Ah, there's lots of bridges!"
        }
        #Key "*where is*" {
            "'Fraid I can't help, I dunno where."
        }
        #Key "*tavern*" {
            "I #like/don't like# taverns. #Heh!/Bah! Always gettin' pawed there.#"
        }
        #Key "*inn*" {
             "Inns are fancy places for the rich to sleep."
        }
        #Key "*temple*", "*resur*", "*death*", "*ankh*" {
            "They say the ankh can bring folks back to life.",
            "The ankh be in the temple by the river."
        }
        #Key "*bay*", "*river*", "*ocean*", "*brittany*", "*narrows*", "*neck*", "*moat*" {
            "'Tis powerful wet, it is."
        }
        #Key "*where am i*", "*m lost*" {
            "Eh? Thou'rt right here."
        }
    }
}


            
