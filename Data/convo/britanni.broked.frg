//General Britannia General Response Fragment				    
//Notes:  This is used for general information pertaining to all of Britannia
//Current Keyword List:	Britannia, Dark Unknown, Danger and Despair, Feudal Lords,
//	Buccaneer's Den, Britain, Cove, Empath Abbey, Jhelom, Magincia, Minoc, Moonglow, 
//	Serpent's Hold, Skara Brae, Trinsic, Vesper, Yew, Lord British, ruler,
//	Weather, concerns, troubles, UDIC, Ultima Dragons, Shamino, Dupre, Iolo, other lands, 
//	other realms, many realms, many lands, other realm, one realm of many?, New Magincia
//	colony, Britanny Bay, knights, Order of the Silver Serpent, Robere, Lord Robere
//
// ["who are you" and "what is your job" keyword variants added, and "bye"]
// [ - Raph                                                    ]
//
//	Virtue, virtues, shrines, moongates, avatar, truth, love, courage, moonstones, orb of the moons, 
//	spirituality, valor, honor, justic, humility, compassion, sacrifice, honesty
//Expletives:  ????? 
//Additional Keywords:
//Revision Date:  3/14/96
//Author:  Andrew Morris


//!!Weather response needs to change once we have weather.
//!!Nuke Ultima Dragons and UDIC after the test
//!!The keywords of virtue must be addressed by Alpha

#Fragment Britannia, General, Britannia_General {
	#Sophistication High {
	    #Key "*how are*", "What's up*", "*are you well*" {
	     	"I would be much happier if I had more [desiresHack(actor())].",
		"I do not have enough of what I really need  -- [desiresHack(actor())].",
		"If only I were not lacking so much [desiresHack(actor())]."
	    }

            #Key "*thanks*", "*thank you*", "*thank thee*", "*thank ye*" {
                "Thou'rt welcome.",
                "'Twas nothing.",
                "Certainly, thou art welcome.",
                "Not a problem."
            }
            #Key "*bye*" {
	            "Goodbye.",
                "Farewell."
	        }
	        #Key "*who are you*", "*what's your name*", "*what is your name*",     
			"*who art thou*", "*who are ye*", "*who you are*",
			"*what are you called*", "*what art thou called*",
	             	"*what are ye called*", "*know your name*", "*know thy name*",      
			"*know yer name*", "*what is thy name*" {
		             "My name is _MyName_.",
		             "I am _MyName_, $milord/milady$."
		             "Thou mayest call me _MyName_."
 		}
	        #Key "*what do you do*", "*what is your job*", "*what is thy job*", 
			"*what's your job*", "*what's thy job*" {
		             "I am a _Job_.",
		             "My profession is that of _Job_.",
		             "I work as a _Job_."
        	}
	        #Key "name" {
        		"If thou'rt asking for my name?, $milord/milady$, please be more specific.",
        		"Please be more specific, if thou'rt asking for my name?, $milord/milady$."
        	}
        	#Key "job" {
        		"If thou'rt asking for my occupation?, $milord/milady$, please be more specific.",
        		"Please be more specific, if thou'rt asking for my job?, $milord/milady$."
        	}
        	#Key "*Britannia*" {
			#Notoriety Infamous, Outlaw {
				"What wouldst thou have me say about Britannia, save that $rogues/foul wenches$
					such as thee do much to bespoil its name.",
				"Britannia? I ask thee not to speak of this land while in my company, $varlet/witch$!",
				"$Scoundrel/Foul wench$! Thou carest not for Britannia! Thus, I ask thee 
					to remove the name of Lord British's realm from thy mind." 
			}
			#Notoriety Anonymous {
				"Ah, fair Britannia. Those who travel claim it is but one realm of many.",
				"I cannot speak but fondly on my homeland. Yet I would not forgo an opportunity
					to see other lands.",
				"I have lived in Britannia all my life. I would be lost were I to visit any other
					lands."
			}
			#Notoriety Famous, Known {
				"Ah, thou dost speak of fair Britannia. Lord British, himself, wouldst be proud of 
					thine efforts to keep it peaceful. Yet, those who travel claim it is but 
					one realm of many.",
				"I cannot speak but fondly on my homeland, the very land thou dost seek to keep safe. 
					Yet I would not forgo an opportunity to see other lands.",
				"I have lived in Britannia all my life, and so I thank thee for thine efforts to improve
					the lives of all who live here, for I would be lost were I forced to live in any other
					realm."
			}
		}
	
		#Key "*Dark Unknown*" {
			"Ah, yes, the continent south of Britannia. I must confess
				I know not a wit about it.",
			"If thou dost refer to the lands south of Lord British's realm,
 					I must tell thee I know little about them.",
			"I am afraid I cannot help thee with such details. Perhaps a cartographer might
				better address thy needs."
		}
	
		#Key "*Danger and Despair*" {
			"I have heard of such a place. Rumor has it that Lord British sometimes receives a guest 
				from the Land of Danger and Despair.  I believe his name is 
				Lord Shamino.",
			"Danger and Despair? Surely thou dost refer to the homeland of good Lord
				Shamino.",
			"The Lands of Danger and Despair do, indeed, sound familiar.  However, I am
				afraid I cannot say positively from where I recognize the name."
		}
	
		#Key "*Feudal Lords*" {
			"Feudal Lords? Ah, yes, thou'rt refering to the lands east of Britannia. There
				the rulers still rely on the vestiges of inherited land to imply their 
				right to govern.",
			"If thou dost mean the barbaric nations just off the coast of Verity Isle, then I 
				am afraid there is little I can tell thee. I do know that kingdoms there are 
				ruled by virtue-less nobles whose only claim to their thrones are the amount
				of property they own.",
	 		"Feudalism is dead here on Britannia, good $sir/lady$. If other lands care to practice
				such an archaic form of government, it is their affair. Yet I am thankful Lord
				British doth not hold such foolish notions in his own mind."  			 
		}
		
		#Key "*Buccaneer's Den*" {
			"Buccaneer's Den? Why, 'tis nothing more than a hiding place for pirates!",
			"'Tis where the scum of the realm go to lick their wounds.",
			"I would not visit there, $milord/milady$, for Buccaneer's Den is home to the
				most vile men and women to sail the seas."
		}
		#Key "*Britain*", "*capital*" {
			"Britain is the capital of all Britannia, land of Lord British.",
			"Lord British, sovereign of all Britannia, resides in Britain.",
			"Britain is home to Lord British, himself. 'Tis the capital of Britannia."
		}
		#Key "*Cove*" {
			"Cove? Thou shouldst find it just east of Britain.",
			"Cove is not much of a city. 'Tis more of a village, to be truthful.",
			"Just east of Britain is Cove."
		}
		#Key "*Empath Abbey*" {
			"Ah, Empath Abbey, where the best wine is prepared and aged.",
			"Empath Abbey is near the community of Yew.",
			"As the implies, $milord/milady$, Empath Abbey is a place where one may go to 
				heal both body and mind." 
		}
		#Key "*Jhelom*" {
			"Jhelom is an outpost for mercenaries and sellswords.",
			"Jhelom? 'Tis a town of sword arms looking for work. Thou canst
				find it off the southeastern coast of mainland Britannia.",
			"Some say Jhelom is as rough as Buccaneer's Den, with half the 		   
				scoundrels and twice the bruises."
		}
		#Key "*Magincia*" {
			"Quite a lovely place, $milord/milady$. And shouldst thou doubt my word, just ask
				someone who resides there.",
			"Magincia? 'Tis a city of arrogance and pride, I think.",
			"Magincia is a city, well, nearly an entire island, outside of Britanny Bay."
		}
		#Key "*Minoc*" {
			"Minoc is a mining town now.",
			"Minoc? Why it was once home to the best artisans in the realm,  
				but now that rich deposits of precious metals have been 
				discovered in the mountains, 'tis full of miners.",
		      	"Minoc lies on the northern coast of the mainland, $milord/milady$."		
		}
		#Key "*Moonglow*" {
			"Magic -- that's what one can find in Moonglow.",
			"Moonglow is nothing more than one large school for mages.",
			"If thou dost wish to study magic, thou shouldst cross the sea to 
				Moonglow."
		}
		#Key "*Serpent's Hold*" {
			"Serpent's Hold is where the best knights are trained. 'Tis located in the 
				Cape of Heroes, off the southeastern edge of the mainland.",
			"Serpent's Hold is the fortification Lord British granted to his loyal 
				knights, those of the Order of the Silver Serpent.",
			"Serpent's Hold? Art thou interested in becoming a member of Lord British's
				royal order of Knights -- the Order of the Silver Serpent? If such is true,
				then thou shouldst go to Serpent's Hold."
		}
		#Key "*Skara Brae*" {
			"Skara Brae is an island. Thou canst find it off the eastern coast of Britannia.",
			"The most skilled trackers in the land learn their craft on the island of Skara Brae.",
			"Many talented shipwrights live on Skara Brae, though the island is actually known
				for the many trackers who teach their skills there."
		}
		#Key "*Trinsic*" {
			"Though Serpent's Hold may be home to valiant knights, and Jhelom home to
				skilled mercenaries, Trinsic is where one shouldst go to find the most
				honorable warriors of the realm.",
			"Though known as a haven for men and women of honor who wish to hone their martial skills, 
				Trinsic also supports a large guild of architects and engineers.",					 
       			"Trinsic? Why, thou couldst find thy way there by merely following the south road
				from Britain."
		}
		#Key "*Vesper*" {
			"Vesper? 'Tis a city in northeastern Britannia. Ore from the mines of Minoc 
				are sent down river to be unloaded in Vesper.",
			"Much of the crafts forged in Minoc find their way to Vesper by way of the 
				river, $milord/milady$.",
			"Some claim Vesper is merely an extension of Minoc, calling it nothing more
				a large marketplace for artisans to sell their wares."
		}
		#Key "*Yew*" {
			"Yew is a small, peaceful community of farmers in northwester Britannia.",
			"The High Court of Britannia is in Yew. 'Tis there that important cases that
				concern all of Britannia are decided, many of which are determined by 
				Lord British, himself.",
			"They say farmers and criminals are all who go to Yew, $milord/milady, save for 
				those hoping to visit one of the two."
		}
	
		#Key "*Britanny Bay*" {
			"'Tis the bay that touches the city of Britain.",
			"Britanny Bay is the body of water on the edge of Britannia's capital.",
			"Britanny Bay? Why, 'tis the waters that border the ports of Britain."
		}
	
		#Key "*Order of the Silver Serpent*", "*Knights*" {
			"'Tis the order of knights who served Lord British in the battle against
				Lord Robere.",
			"When Lord Robere challenged Lord British for his share of the realm, Lord
				British's faithful knights defended the kingdom.",
			"Only the bravest knights belong to the Order of the Silver Serpent. 'Twas 
				them who defeated the forces of Lord Robere."
		}
	
		#Key "*Robere*"," Lord Robere*" {
		 	"History claims that Lord Robere was once an honorable man. But he was
				overcome with greed and sought to take the entire realm by force.",
			"Years ago, Lord Robere made claims to the lands under Lord British's domain. 
				Were it not for the stout knights in the Order of the Silver Serpent, 
				Lord Robere could very well have conquered the entire realm.", 
			"Though a kind and generous man in his youth, Lord Robere thirsted for power
				 in his later years, so legends say. Had not the Order of the Silver Serpent been ready 
				 to fight for Lord British, this very land could have belonged to the ambitious
				 conqueror."
		}
	
		#Key "*Lord British*", "*ruler*", "*king*" {
	    		#Attitude Neutral {
	    			"I have always find Lord British to be fair and just in his 
	    				rule.",
	    			"From my liege I ask no more than a strong but gently, guiding
	    				hand. Thus far, Lord British has shown just that.",
	    			"Lord British governs as any wise ruler would -- with an even hand and 
	    				a thoughtful eye."	
	    		}
	    		#Attitude Wicked, Belligerent {
	    	       		#Notoriety Infamous, Outlaw {
	    				"Love him like thine own brother, they say.  Fine, say I, but must
						I be so unnkind to mine own flesh and blood?",
					"Yes, it seems Lord British doth possess a rare gift for leadership...
						and I wish he would give it back.",
					"Wert thou not so masterful, $Milord/Milady$, I would compare THEE
						to our illustrious ruler... but only a fool would confuse a 
						fine $man/soul$ such as thee with Britannia'a royalty."
				}				
		       		#Notoriety Anonymous {
					"Hmmm...  yes, Lord British. That's a difficult one....",
					"I care not for his empty promises or hollow beliefs, for they
						brought me nothing vain hope.",
 						"Please accept mine apology, o' noble $lord/lady$, but I care little
						about sharing mine opinions on Lord Britsh with thee."
			       	}
			       	#Notoriety Famous, Known {
			       		"Lord British governs as any intelligent ruler would -- from afar,
			       			safe in his castle.",
			       		"I do not think thou wouldst agree with mine opinion.  Let us remain civil, 
	 		       			shall we?",
			       		"My dear, departed mother taught me never to say ill words about 
			       			another person. So I shan't."
			       	}
	  		}
	    		#Attitude Goodhearted, Kindly {
				#Notoriety Infamous, Outlaw {
					"Though thou might never know such, Lord British has done much
						to make our land one of prosperity.",
					"Fear not, troubled soul. The worries that thou might have about our land
						will soon be made to vanish by Lord British.",
					"It may not be apparent to thee, $Milord/Milady$, but Lord British's guidance has 
						made Britannia great."
				}
				#Notoriety Anonymous {
					"Lord British is a kind and generous ruler.",
					"I do not know of another who would rule as fairly in Lord British's stead.",
					"In truth, Lord British does his best to address any concerns we seem to have."
				}
				#Notoriety Famous, Known {
					"As thou must surely know, $Milord/Milady$ _Name_, Lord British is the reason our land
						has prospered so.",
					"I have little fear that all will be well in Britannia.",
					"As must be plainly apparent to thee, $Milord/Milday$, Lord British's 
						guidance has made Britannia great."
				}
			}
		}
		
	
		#Key "*Weather*" {
			"Ah, the weather... 'Tis an interesting thing, really. 
				No matter what the season, no matter what enchantments
				are cast, our land is always blessed with clear and
				beautiful blue skies."
		}
	
		#Key "*concerns*", "*troubles*" {
			"Various issues surface from time to time, such as taxation, invasion, 
				protection form creatures of the wild.",
			"Any land experiences difficult times, but it takes a wise ruler to lead his 
				people through them.",
			"Surely thou dost understand -- life cannot always be free of trouble."
		}
	
	
		#Key "*UDIC*" {
			"How darest thee call me such... Wait!  Didst thou say U-D-I-C? Oh, I thought
				thou didst call me a... well, never thou mindest. Yes, I know
				about UDIC, the faithful followers of the exploits of Lord British's
				companions!"
		}
	
	
		#Key "*Ultima Dragons*" {
			"Well, of course the dragons are Ultima Dragons. What other kind of dragons 
				wouldst thou expect to find here?!?!"				
		}
	
		#Key "*Shamino*" {
			"Lord Shamino? Ah, yes, Lord British's friend and ally, I believe.",
			"The name is familiar to me... ah, yes -- I believe he is the infrequent 
				guest of Lord British.",
			"Not a name bandied about often, to be sure, kind $sir/lady$. I suspect 
				thou'rt refering to the oft-time companion of Lord British, himself."	  
		}
	
		#Key "*Iolo*" {
			"Iolo? Ah, yes, Lord British's friend and ally, I believe.",
			"The name is familiar to me... ah, yes -- I believe he is the infrequent 
				guest of Lord British.",
			"Not a name bandied about often, to be sure, kind $sir/lady$. I suspect 
				thou'rt refering to the oft-time companion of Lord British, himself."	  
		}
		
		#Key "*Dupre*" {
			"Dupre? Ah, yes, Lord British's friend and ally, I believe. If thou wert to 
				search for him, I wouldst most recommend a tavern.",
			"The name is familiar to me... ah, yes -- I believe he is the infrequent 
				guest of Lord British. Mayhaps thou wilt find him sampling the local 
				spirits.",
			"Not a name bandied about often, to be sure, kind $sir/lady$. I suspect 
				thou'rt refering to the oft-time fighting companion of Lord British, himself."	  
		}
	
		#Key "*other lands*", "*other realms*", "*many realms*", "*many lands*", "*other realm*", "*one realm of many?*"  {
			"So, thou hast heard the stories as well. Travelers speak of several continents. I believe
				I have heard of holdings elsewhere. Perhaps thou hast heard of the Lands of Danger and
				Despair. Or the Lands of the Feudal Lords, or of the Dark Unknown?", 
			"Experienced travelers speak of lands across the seas -- the Lands of the Dark Unknown, Danger and
				Despair, and the Lands of the Feudal Lords.",
			"I have heard tell, $milord/milady$, of the Lands of Danger and Despair, and of others, such as the
		 	Lands of the Dark Unknown, as well as a land of Feudal Lords." 
		}
	
		#Key "*New Magincia*" {
			"New Magincia? Is there something wrong with the original?",
			"Forgive me, $milord/milady$, but I think thou dost suffer the madness 
				of the drink.",
			"I have not heard of such a place, $milord/milady$. I suppose it could
				be a colony of Britannians who have settled in a new territory."
		}
	
		#Key "*colony*" {
			"I know not of any, $milord/milady$.",
			"Rumors abound that Lord British wishes to send adventurous and resourceful
				individuals to settle unexplored areas.",
			"Aye, $milord/milady$, a colony of Britannians would expand Lord British's
				influence throughout the realm."
		}
	
		#Key "*Virtue*", "*virtues*", "*shrines*", "*moongates*", "*avatar*","*truth*", "*love*", 
			"*courage*", "*moonstones*", "*orb of the moons*", "*spirituality*", "*valor*, "*honor*", 
			"*justice*", "*sacrifice*", "*honesty*", "*humility*", "*compassion*" { 
				"I cannot speak of these things now, but fear not -- all will be addressed in the future."
		}

	}

	#Sophistication Medium {
	    #Key "*how are* ", "What's up*", "*are you well*" {
	     	"I would be much happier if I had more [desiresHack(actor())].",
		"I do not have enough of what I really need  -- [desiresHack(actor())].",
		"If only I were not lacking so much [desiresHack(actor())]."
	    }
	    #Key "*bye*" {
	            "Goodbye.",
		    "Farewell."
	    }
            #Key "*thanks*", "*thank you*", "*thank thee*", "*thank ye*" {
                "Thou'rt welcome.",
                "'Twas nothing.",
                "Certainly, thou art welcome.",
                "Not a problem."
            }
		#Key "*who are you*", "*what's your name*", "*what is your name*",     
			"*who art thou*", "*who are ye*", "*who you are*",
			"*what are you called*", "*what art thou called*",
	             	"*what are ye called*", "*know your name*", "*know thy name*",      
			"*know yer name*" {
		             "My name is _Name_.",
		             "I am _Name_, $milord/milady$."
		             "Thou mayest call me _Name_."
 		}
	        #Key "*what do you do*", "*what is your job*", "*what is thy job*", 
			"*what's your job*", "*what's thy job*" {
		             "I am a _Job_.",
		             "My occupation is that of _Job_.",
		             "I work as a _Job_."
        	}
	        #Key "* name", "* job" {
        		"Couldst thou please be more specific, $milord/milady$?"
        	}

		#Key "*Britannia*" {
			#Notoriety Infamous, Outlaw {
				"Please $sir/lady$, I will speak not of my feelings of my beloved homeland.",
				"Please forgive me, $sir/lady$, but I will tell thee nothing of Britannia, 
					for I fear what thou wilt do further."'
				"Hast thou not performed enough cruelty to Lord British's lands?"
			}
			#Notoriety Anonymous {
				"Britannia? Truly the lands of Lord British.",
				"Good times or ill, I will always love my homeland,.",
				"'Tis called Britannia in honor of our liege, Lord British."
			}
			#Notoriety Famous, Known {
				"Ah, $milord/milady$, surely thou dost know as much about the land as
					I. Why else wouldst thou do such good deeds for its people?",
				"What canst I tell one who has done so much? Nay, 'tis I who should be asking
					thee for advice.",
				"Why, all of Lord British's realm is called Britannia. But fear not, $good sir/fair lady$,
					with deeds such as thine, thou wilt surely be honored by Lord British, himself, 
						soon enough."
		       }
		}
	
		#Key "*Dark Unknown*" {
			"My apologies, $milord/milady$, I know nothing about these lands 
				of which thou dost speak.",
			"Dark Unknown? I know nothing about such a place. I would assume 
				it is a terribly unhappy place, good $sir/lady$.",
			"I do not understand thee. Mayhaps someone with a map would be of
				better service to thee."
		}
	
		#Key "*Danger and Despair*" {
			"How dreadful sounding! Nay, good $Lord/Lady, I know not
				of what you speak.",
			"Couldst thou be refering to the realm that supposedly lies south and east 
				of Britannia? Aye, I believe that it what it is called.",				
			"Perhaps thou shouldst consult a cartographer about this, $milord/milady$."
		}
	
		#Key "*Feudal Lords*" {
			"Such words sound familiar, $sir/fair lady$, but mean nothing to me. Wouldst not
				someone better acquainted with travel help thee more than I?",
	    		"Nay, $milord/milady$, I know nothing of feudal lords.",
			"Were that I a well-travelled #man/woman#, I could, perhaps, offer mine
				assistance. Alas, I but recognize the name, and barely that."
		}
		
		#Key "*Buccaneer's Den*" {
			"Buccaneer's Den? Why, 'tis nothing more than a hiding place for pirates!",
			"'Tis where the scum of the realm go to lick their wounds.",
			"I would not visit there, $milord/milady$, for Buccaneer's Den is home to the
				most vile men and women to sail the seas."
		}
		#Key "*Britain*", "*capital*" {
			"Britain is the capital of all Britannia, land of Lord British.",
			"Lord British, sovereign of all Britannia, resides in Britain.",
			"Britain is home to Lord British, himself. 'Tis the capital of Britannia."
		}
		#Key "*Cove*" {
			"Cove? Thou shouldst find it just east of Britain.",
			"Cove is not much of a city. 'Tis more of a village, to be truthful.",
			"Just east of Britain is Cove."
		}
		#Key "*Empath Abbey*" {
			"Ah, Empath Abbey, where the best wine is prepared and aged.",
			"Empath Abbey is near the community of Yew.",
			"As the implies, $milord/milady$, Empath Abbey is a place where one may go to 
				heal both body and mind." 
		}
		#Key "*Jhelom*" {
			"Jhelom is an outpost for mercenaries and sellswords.",
			"Jhelom? 'Tis a town of sword arms looking for work. Thou canst
				find it off the southeastern coast of mainland Britannia.",
			"Some say Jhelom is as rough as Buccaneer's Den, with half the 		   
				scoundrels and twice the bruises."
		}
		#Key "*Magincia*" {
			"Quite a lovely place, $milord/milady$. And shouldst thou doubt my word, just ask
				someone who resides there.",
			"Magincia? 'Tis a city of arrogance and pride, I think.",
			"Magincia is a city, well, nearly an entire island, outside of Britanny Bay."
		}
		#Key "*Minoc*" {
			"Minoc is a mining town now.",
			"Minoc? Why it was once home to the best artisans in the realm,  
				but now that rich deposits of precious metals have been 
				discovered in the mountains, 'tis full of miners.",
		      	"Minoc lies on the northern coast of the mainland, $milord/milady$."		
		}
		#Key "*Moonglow*" {
			"Magic -- that's what one can find in Moonglow.",
			"Moonglow is nothing more than one large school for mages.",
			"If thou dost wish to study magic, thou shouldst cross the sea to 
				Moonglow."
		}
		#Key "*Serpent's Hold*" {
			"Serpent's Hold is where the best knights are trained. 'Tis located in the 
				Cape of Heroes, off the southeastern edge of the mainland.",
			"Serpent's Hold is the fortification Lord British granted to his loyal 
				knights, those of the Order of the Silver Serpent.",
			"Serpent's Hold? Art thou interested in becoming a member of Lord British's
				royal order of Knights -- the Order of the Silver Serpent? If such is true,
				then thou shouldst go to Serpent's Hold."
		}
		#Key "*Skara Brae*" {
			"Skara Brae is an island. Thou canst find it off the eastern coast of Britannia.",
			"The most skilled trackers in the land learn their craft on the island of Skara Brae.",
			"Many talented shipwrights live on Skara Brae, though the island is actually known
				for the many trackers who teach their skills there."
		}
		#Key "*Trinsic*" {
			"Though Serpent's Hold may be home to valiant knights, and Jhelom home to
				skilled mercenaries, Trinsic is where one shouldst go to find the most
				honorable warriors of the realm.",
			"Though known as a haven for men and women of honor who wish to hone their martial skills, 
				Trinsic also supports a large guild of architects and engineers.",					 
	       		"Trinsic? Why, thou couldst find thy way there by merely following the south road
				from Britain."
		}
		#Key "*Vesper*" {
			"Vesper? 'Tis a city in northeastern Britannia. Ore from the mines of Minoc 
				are sent down river to be unloaded in Vesper.",
			"Much of the crafts forged in Minoc find their way to Vesper by way of the 
				river, $milord/milady$.",
			"Some claim Vesper is merely an extension of Minoc, calling it nothing more
				a large marketplace for artisans to sell their wares."
		}
		#Key "*Yew*" {
			"Yew is a small, peaceful community of farmers in northwester Britannia.",
			"The High Court of Britannia is in Yew. 'Tis there that important cases that
				concern all of Britannia are decided, many of which are determined by 
				Lord British, himself.",
			"They say farmers and criminals are all who go to Yew, $milord/milady, save for 
				those hoping to visit one of the two."
		}
	
		#Key "*Britanny Bay*" {
			"'Tis the bay that touches the city of Britain.",
			"Britanny Bay is the body of water on the edge of Britannia's capital.",
			"Britanny Bay? Why, 'tis the waters that border the ports of Britain."
		}
	
		#Key "*Order of the Silver Serpent*", "*Knights*" {
			"'Tis the order of knights who served Lord British in the battle against
				Lord Robere.",
			"When Lord Robere challenged Lord British for his share of the realm, Lord
				British's faithful knights defended the kingdom.",
			"Only the bravest knights belong to the Order of the Silver Serpent. 'Twas 
				them who defeated the forces of Lord Robere."
		}
	
		#Key "*Robere*"," Lord Robere*" {
		 	"History claims that Lord Robere was once an honorable man. But he was
				overcome with greed and sought to take the entire realm by force.",
			"Years ago, Lord Robere made claims to the lands under Lord British's domain. 
				Were it not for the stout knights in the Order of the Silver Serpent, 
				Lord Robere could very well have conquered the entire realm.", 
			"Though a kind and generous man in his youth, Lord Robere thirsted for power
				 in his later years, so legends say. Had not the Order of the Silver Serpent been ready 
				 to fight for Lord British, this very land could have belonged to the ambitious
				 conqueror."
		}
	
		#Key "*Lord British*", "*ruler*", "*king*" {
			#Attitude Neutral {
				"Lord British has always a fair and just ruler over
					this land.",
				"I have no qualms about the way Lord British governs us.",
				"Lord Britsh has done nothing to hurt my work as a _Job_."
			}
	 		#Attitude Wicked , Belligerent {
	    			"Aye, Lord British does the commanding in this realm. And we puppets
					are to jump when he so bids it.",
				"Dost thou wish for me merely to repeat what surely other
					sheep have said before me? Well and good then -- Lord 
					British is the finest leige I have ever been privileged
					to grovel before.",
				"Now is not the time to talk about ruler, Good $Sir/Lady$." 					 
			}
	    		#Attitude Goodhearted, Kindly {
				"A wiser, kinder ruler I have not heard of.",
				"I think Lord British makes for a fine ruler.",
				"We have a fine ruler in Lord British."
			}
		}
		
	
		#Key "*Weather*" {
			"Ah, the weather... 'Tis an interesting thing, really. 
				No matter what the season, no matter what enchantments
				are cast, our land is always blessed with clear and
				beautiful blue skies."
		}
	
		#Key "*concerns*", "*troubles*" {
			"Various issues surface from time to time, such as taxation, invasion, 
				protection form creatures of the wild.",
			"Any land experiences difficult times, but it takes a wise ruler to lead his 
				people through them.",
			"Surely thou dost understand -- life cannot always be free of trouble."
		}
	
	
		#Key "*UDIC*" {
			"How darest thee call me such... Wait!  Didst thou say U-D-I-C? Oh, I thought
				thou didst call me a... well, never thou mindest. Yes, I know
				about UDIC, the faithful followers of the exploits of Lord British's
				companions!"
		}
	
	
		#Key "*Ultima Dragons*" {
			"Well, of course the dragons are Ultima Dragons. What other kind of dragons 
				wouldst thou expect to find here?!?!"				
		}
	
		#Key "*Shamino*" {
			"I am sorry, $milord/milady$, I do not recognize the name.",
			"Shamino? I beg thee pardon, $milord/milady$, but the words mean norhing 
				to me.",
			"Shamino? Nay, $kind sir/fair lady$, I cannot help thee?"
		}
	
		#Key "*Iolo*" {
			"I am sorry, $milord/milady$, I do not recognize the name.",
			"Iolo? I beg thee pardon, $milord/milady$, but the words mean norhing 
				to me.",
			"Iolo? Nay, $kind sir/fair lady$, I cannot help thee?"
		}
		
		#Key "*Dupre*" {
			"I am sorry, $milord/milady$, I do not recognize the name.",
			"Dupre? I beg thee pardon, $milord/milady$, but the words mean norhing 
				to me.",
			"Dupre? Nay, $kind sir/fair lady$, I cannot help thee?"
		}
	
		#Key "*other lands*", "*other realms*", "*many realms*", "*many lands*", "*other realm*", "*one realm of many?*"  {
			"My apologies, $milord/milady$, but I know of no other than Britannia.",
			"$Milord/Milady$, I do not understand. Realms other than Brittania? Surely 
				thou dost make a jest.",
			"If thou'rt certain, $milord/milady$, I will not question thee. But I have
				heard of no lands other than our fair Britannia."  
		}
	
		#Key "*New Magincia*" {
			"New Magincia? Is there something wrong with the original?",
			"Forgive me, $milord/milady$, but I think thou dost suffer the madness 
				of the drink.",
			"I have not heard of such a place, $milord/milady$. I suppose it could
				be a colony of Britannians who have settled in a new territory."
		}
	
		#Key "*colony*" {
			"I know not of any, $milord/milady$.",
			"Rumors abound that Lord British wishes to send adventurous and resourceful
				individuals to settle unexplored areas.",
			"Aye, $milord/milady$, a colony of Britannians would expand Lord British's
				influence throughout the realm."
		}
	
		#Key "*Virtue*", "*virtues*", "*shrines*", "*moongates*", "*avatar*","*truth*", "*love*", 
			"*courage*", "*moonstones*", "*orb of the moons*", "*spirituality*", "*valor*, "*honor*", 
			"*justice*", "*sacrifice*", "*honesty*", "*humility*", "*compassion*" { 
				"I cannot speak of these things now, but fear not -- all will be addressed in the future."
		}

	}
	#Sophistication Low {
	    #Key "*how are* ", "What's up*", "*are you well*" {
	     	"I would be much happier if I had more [desiresHack(actor())].",
		"I do not have enough of what I really need  -- [desiresHack(actor())].",
		"If only I were not lacking so much [desiresHack(actor())]."
	    }
	    #Key "*bye*" {
	            "Goodbye.",
		    "Farewell."
	    }
            #Key "*thanks*", "*thank you*", "*thank thee*", "*thank ye*" {
                "Ye're welcome.",
                "'Twas nothing.",
                "Sure, yer welcome.",
                "No problem."
            }        
		#Key "*who are you*", "*what's your name*", "*what is your name*",     
			"*who art thou*", "*who are ye*", "*who you are*",
			"*what are you called*", "*what art thou called*",
	             	"*what are ye called*", "*know your name*", "*know thy name*",      
			"*know yer name*" {
		             "_Name_.",
		             "I am _Name_, $milord/milady$."
		             "Thou canst call me _Name_."
 		}
	        #Key "*what do you do*", "*what is your job*", "*what is thy job*", 
			"*what's your job*", "*what's thy job*" {
		             "I am a _Job_.",
		             "_Job_.",
		             "I work as a _Job_."
        	}
	        #Key "* name" {
        		"My name, $milard/milady?"
        	}
        	#Key "* job" {
        		"My job, $milord/milady$?"
        	}

    		#Key "*Britannia*" {
			#Notoriety Infamous, Outlaw {
				#Attitude Wicked, Belligerent {
					"Britannia? 'Tis the very land thou dost pillage, blackguard!",
					"Surely thou knowest the land thou dost work to destroy!",
					"And what wouldst thou like to know about Britannia? How thou canst
						do more to make our lives wretched?"  
				}
				#Attitude Kindly, Goodhearted {
					"Please, $sir/lady$, do not bespoil our land further. I beg thee!",
					"I cannot help but worry that any news I give thee will only aid thy
						plans for wanton destruction.",
					"I would ask thee, $sir/milady$, to pillage our land no further."
				}
				#Attitude Neutral {
					"Britannia is the very land thou'rt upon.",
					"Britannia? Why, 'tis all around thee.",
					"This very land is Britannia, as thou must surely know."
				}
			}
			#Notoriety Anonymous {
				"Britannia?  Why, 'tis the very name Lord British has claimed for the lands
					of hinself and his subjects", 
				"This very land is Britannia, as thou must surely know.",
				"Britannia? Why, 'tis all around thee."
			}
			#Notoriety Famous, Known {
				"Britannia is the very land thou hast helped make better.",
				"Britannia? Why, 'tis the very name that thanks thee daily for thy
					kindness.",
				"How couldst thou not know of the very lands made more prosperous by thine
					own deeds?"
			}
		}
	
		#Key "*Dark Unknown*" {
			"Dark Unknown? Wny, those lands are but a fairy tale told to wee
				children.",
			"I've not heard of such a land $milord/milady$.",
			"Thou hast been drinking a wee bit, eh, $milord/milady$?"
		}
	
		#Key "*Danger and Despair*" {
			"Danger and Despair? Why wouldst I know anything with such a fearsome name? 
				Nay, heard of it I have not, $milord/mmilady$.",
			"I know nothing of that $milord/milady$. It must be an horrible dungeon 
				filled with all sorts of nasty beasts!",
			"Mayhaps thou shouldst speak to someone who knows more, $milord/milady$. I've
				never heard of something so awful sounding." 
		}
	
		#Key "*Feudal Lords*" {
			"What's that, $milord/milady$? Feuding lords? Why, I had no idea Lord British is
				having trouble with other lords. I thank thee for the news.",
			"I know not of these feudal lords, $milord/milady$. Mayhaps another couldst tell
				the more.",
			"Begging thy pardon, $milord/milady$, but I cannot help thee. If thou hast angered 
				a lord, prithee leave me be."
		}
		
		#Key "*Buccaneer's Den*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Britain*", "*capital*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Cove*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Empath Abbey*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Jhelom*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Magincia*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Minoc*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Moonglow*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Serpent's Hold*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Skara Brae*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Trinsic*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Vesper*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
		#Key "*Yew*" {
			"Never heard of the place, $milord/milady$.",
			"Begging thy pardon, $milord/milady$, but I know of naught but where I live.",
			"Must be somewhere else, $milord/milady$."
		}
	
		#Key "*Lord British*", "*ruler*", "*king*" {
			#Attitude Neutral {
				"Aye, now thou'rt speaking of our king.",
				"Lord British? I bear him no ill will.",
				"They Lord British is fair and just. I've got no argument with that."
			}
    		     	#Attitude Wicked , Belligerent {
    		     		#Notoriety Infamous, Outlaw {
		     			"I'd like the see the bloody fool's head on a spit, I would.",
		     			"Dost thou want my opinion on our great and wise Lord British? I find 
		     				him a lout, I do. And I'd say it to him if I could.",
		    	       		"Thou wouldst not find me crying over his spilled blood."	 
		     		}
    		     		#Notoriety Anonymous {
					"Lord British? I suppose the taxes he takes to fill his coffers 
						leave us enough to live on... almost.",
					"I hope Lord British is pleased with all he's done for Britannia. 
						I must admit, I'm not.",
					"I don't know what makes him feel so lordly. Unless, 'tis his gift
						for collecting gold to fill his coffers."
				}
    				#Notoriety Famous, Known {
					"Lord British? Best we not speak of him, $milord/milady$.",
					"Thou wouldst not care for mine opinion, $milord/milady$.",
					"Nay, $milord/milady$, I will not tell thee my feelings about 
						our liege. Best to let such words stay unspoken."
				}
			}
    			#Attitude Goodhearted, Kindly {
				"Aye, Lord British works hard to make sure we're all happy.",
				"Lord British is our king. He is a good man, I say.",
				"Lord British? He's done right by Britannia, he has!"
			}
		}
		
		#Key "*Weather*" {
			"Ah, the weather... 'Tis an interesting thing, really. 
				No matter what the season, no matter what enchantments
				are cast, our land is always blessed with clear and
				beautiful blue skies."
		}
	
		#Key "*concerns*", "*troubles*" {
			"Various issues surface from time to time, such as taxation, invasion, 
				protection form creatures of the wild.",
			"Any land experiences difficult times, but it takes a wise ruler to lead his 
				people through them.",
			"Surely thou dost understand -- life cannot always be free of trouble."
		}
	
	
		#Key "*UDIC*" {
			"How darest thee call me such... Wait!  Didst thou say U-D-I-C? Oh, I thought
				thou didst call me a... well, never thou mindest. Yes, I know
				about UDIC, the faithful followers of the exploits of Lord British's
				companions!"
		}
	
	
		#Key "*Ultima Dragons*" {
			"Well, of course the dragons are Ultima Dragons. What other kind of dragons 
				wouldst thou expect to find here?!?!"				
		}
	
		#Key "*Shamino*", "*Iolo*", "*Dupre*" {
			"Who?",
			"Where?",
			"What?"
		}
	
		#Key "*other lands*", "*other realms*", "*many realms*", "*many lands*", "*other realm*", "*one realm of many?*"  {
			"What dost thou mean? There is only one land, and it's called Britannia.",
			"What other?",
			"There's no other!"
		}
	
		#Key "*New Magincia*" {
			"New Magincia? Is there something wrong with the original?",
			"Forgive me, $milord/milady$, but I think thou dost suffer the madness 
				of the drink.",
			"I have not heard of such a place, $milord/milady$. I suppose it could
				be a colony of Britannians who have settled in a new territory."
		}
	
		#Key "*colony*" {
			"I know not of any, $milord/milady$.",
			"Rumors abound that Lord British wishes to send adventurous and resourceful
				individuals to settle unexplored areas.",
			"Aye, $milord/milady$, a colony of Britannians would expand Lord British's
				influence throughout the realm."
		}
	
		#Key "*Virtue*", "*virtues*", "*shrines*", "*moongates*", "*avatar*","*truth*", "*love*", 
			"*courage*", "*moonstones*", "*orb of the moons*", "*spirituality*", "*valor*, "*honor*", 
			"*justice*", "*sacrifice*", "*honesty*", "*humility*", "*compassion*" { 
				"I cannot speak of these things now, but fear not -- all will be addressed in the future."
		}
	}
}
