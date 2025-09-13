//Britannia Vet Fragment				    
//Notes:  This is used for general information pertaining to all Britannian Healers
//Current Keyword List:
//   job
//   heal, cure, poison, resurrect, resurrection, healing, toxin, venom, raise dead, 
//   	raise the dead	
//   help
//   buy, potions, healing scrolls, potion
//   
//Additional Keywords:
//Revision Date:  7/17/96
//Author:  Andrew Morris

//!! Change heal, cure, resurrect
//!! Add animal, beast, creature, etc. services, animal handler

#Fragment Britannia, Job, Britannia_Vet {
        #Sophistication High {
                #Key "*job*", "*what*do*do*" "*profession*" "*occupation*" {
                        "I am one of the many animal healers of Britannia, $milord/milady$.",
			"Why I am a veterinarian, $milord/milady$.",
                        "Tending to ill animals is what I do, $milord/milady$, for I am a veterinarian."
                }
		#Key "*vet*" {
                        "Yes, I am a veterinarian, $milord/milady$.",
                        "Yes, my desire to help all creatures led me to be a veterinarian."
                }

                #Key "*heal*" {
                        "Aye, I would be willing to help thee with a sick or hurt beast.",
                        "$Milord/Milady$, I can help, if thou dost have an injured animal",
                        "I can heal thy injured pets or beasts of burden."
                }
                #Key "*help*",  "*pet*", "*hurt*", "*injured*", "*cat*",  "*dog*", "*bird*", "*bear*", "*dragon*",  "*mule*", "*llama*", "*horse*",  "*gorilla*"  {
                        "$Milord/Milady$, I can help thee with thy ill livestock and pets.",
                        "Art thou in need of mine assistance? I am happy to help.",
                        "Please, bring in thy animals, $milord/milady."
		}
                #Key "*cure*", "*poison*", "*toxin*", "*venom*" {
                        "Aye, I can remove toxins that befoul thy creatures.",
                     	"I make no promises of success, $milord/milady$, but I am willing to attempt to cure thy poisoned animals.",
                        "Be it natural or magical, I have remedies for both types of poison."
                }
                #Key "*resurrect*", "*resurrection*", "*raise*dead*" {
                        "Indeed, the power to restore life is not to be treated lightly. Unfortunately, the spirits of the creatures of nature -- those who do not have the power of speech -- travel from this realm more 
quickly than they do for thyself or me. Alas, I cannot raise dead animals.",
                        "I am saddened to say that resurrection is not a power that can be performed on animals.",
			"'Tis quite disappointing, I am afraid, but the spirit of animals who have passed on cannot be returned to their bodies."
                }
                #Key "*buy*" {
                        "Unless thou'rt in my profession, I have little that thou would be likely to need, $milord/milady$. Perhaps an animal handler would likely have 
provisions and equipment for thee to purchase.",
                        "I merely sell my services, $milord/milady$, and a few srcolls of healing.",
		        "Perhaps thou'rt asking about the services I offer."
                }
                #Key "*potions*", "*potion*" {
                        "Healing potions?  Aye, those may be used to aid animals as people. The same is true for potions of curing.",
                        "I know of potions of healing and curing, $milord/milady$, all quite capable of providing aid to a wounded animal.",
                        "If thou'rt interested in potions of curing and healing, I cannot help thee."
                }
                #Key "*healing scrolls*" {
                        "Healing scrolls can be used to help heal wounds. I can sell thee some.",
                        "My healing scrolls are made from strong cloth fibers and silk. I still have some if thou dost want to buy them.",
                        "Why, I sell healing scrolls $milord/milady$. They can aid in the healing of many types of wounds."
                }
        }

        #Sophistication Medium {
                #Key "*job*", "*what*do*do*" "*occupation*" "*profession*" {
                        "I heal animals that are sick.",
                        "I am a veterinarian.",
                        "I heal the wounds of injured and sick animals."
                }
	#Key "*vet*" {
                        "Yes, I am a veterinarian, $milord/milady$.",
                        "Yes, my desire to help all creatures led me to be a veterinarian."
                }

                #Key "*heal*", "*healing*" {
                        "Aye, healing nature's creatures is what I do.",
                        "I can speed up the healing process.",
                        "There are many ways to help wounds to heal."
                }
                #Key "*help*",  "*pet*", "*hurt*", "*injured*", "*cat*",  "*dog*", "*bird*", "*bear*", "*dragon*",  "*mule*", "*llama*", "*horse*",  "*gorilla*"  {
                        "What is wrong, $milord/milady$?",
                        "Dost thou have an ailing beast?  I can, perheps, assist thee if that is the case.",
                        "With what dost thou need help?"
                }
                #Key "*cure*", "*poison*" {
                        "If thy beast has been poisoned, I can help thee.",
                        "Perhaps I may be able to help thee with thy troubles, $milord/milady$.",
                        "Is thine animal poisoned? Perhaps thou wilt permit me to aid it."
                }
                #Key "*resurrect*", "*resurrection*", "*raise*dead*" {
                        "Begging thy pardon, $milord/milady$, but I cannot restor life to a pet that has passed on.",
                        "If thou dost wish me to raise thy dead beast, I am afraid thou'rt art likely to be disappointed.",
                        "For reasons too long to explain at the moment, resurrection is a powerful manipulation of magic that does not work on beasts. I am sorry, $milord/milady$."
                }
                #Key "*buy*" {
                        "I sell only my healing service, my $good sir/dear woman$, as well as a few healing scrolls.",
                        "Dost thou wish to purchase my services?",
                        "I have little to sell, save my skill in tending to ill animals. Perhaps I also have a few scrolls of healing, $milord/milady$."
                }
                #Key "*potions*", "*potion*" {
		   	"Healing potions? Aye, those may be used to aid animals as people. The same is true for potions of curing.",
                        "I know of potions of healing and curing, $milord/milady$, all quite capable of providing aid to a wounded animal.",
                        "If thou'rt interested in potions of curing and healing, I cannot help thee."
                }
                #Key "*healing scrolls*" {
                        "These healing scrolls will soothe thy wounds.",
                        "I have healing scrolls made from the strongest cloth.",
                        "I have lots of healing scrolls."
                }
        }

        #Sophistication Low {
                #Key "*job*", "*what*do*do*" "*occupation*" "*profession*" {
                        "I heal animals.",
                        "I tend to wounded beasts, $milord/milady$",
                        "I heal animals, $milord/milady$."
                }
	#Key "*vet*" {
                        "Yes, I'm a veterinarian, $milord/milady$.",
                        "I liker animals, so I'm a veterinarian."
                }

                #Key "*heal*", "*healing*" {
                        "Aye, $milord/milady$, I can help animals.",
                        "Thou'rt needin my aid, $milord/milady$?",
                        "What does thy need, $milord/milady$?."
                }
                #Key "*help*",  "*pet*", "*hurt*", "*injured*", "*cat*",  "*dog*", "*bird*", "*bear*", "*dragon*",  "*mule*", "*llama*", "*horse*",  "*gorilla*"  {
                        "What's wrong?",
                        "Does thou got a hurt animal? I might could help thee.",
                        "Maybe I can help thee, $milord/milady$."
                 }
                #Key "*cure*", "*poison*" {
                        "Is thy animal is poisoned?",
                        "Maybe I can heal thy animal.",
                        "Is thy beast in need of help? I can maybe help."
                }
                #Key "*resurrect*", "*resurrection*", "*raise*dead*" {
                        "I can't raise dead beasts.",
                        "Resurrection ain't possible for animals, $milord/milady$.",
                        "That can be done only for dead people, $milord/milady$."
                }
                #Key "*buy*" {
                        "I got little to sell.",
                        "Does thou need my aid, $milord/milady$?",
                        "I only buy supplies, $milord/milady$, and sell my services."
                }
                #Key "*potions*", "*potion*" {
                        "I don't know much about healing potions.",
                        "Thou'rt interested in healing potions?.",
                        "I got no potions, $milord/milady$."
                }
                #Key "*healing scrolls*" {
                        "I sell healing scrolls.",
                        "Thou'rt interested in healing scrolls?.",
                        "I got healing scrolls."
                }
        }
}
