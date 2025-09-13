//General Britannia General Response Fragment				    
//Notes:  This is used for general information pertaining to all of Britannia
//Current Keyword List:	Britannia,
//	Buccaneer's Den, Britain, Cove, Jhelom, Magincia, Minoc, Moonglow, 
//	Serpent's Hold, Skara Brae, Trinsic, Vesper, Yew, Lord British, ruler,
//	Weather, concerns, troubles, Shamino, Dupre, Iolo, other lands, 
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
//Revision Date:  2/6/97  dab   6/26-27/97  dab
//Author:  Andrew Morris


//!!Weather response needs to change once we have weather.
//!!Nuke Ultima Dragons and UDIC after the test - done
//!!The keywords of virtue must be addressed by Alpha - kinda done

#Fragment Britannia, General, Britannia_General {
	#Sophistication High 
	{
		#Key "@InternalRefuseItem" 
		{
            		#Attitude Wicked 
			{
                		#Notoriety Infamous 
				{
                    			"No thanks!",
	      				"Thanks, but no.",
	      				"I don't want that!"
				}
                		#Notoriety Anonymous 
				{
                   			"I don't want it.",
	     				"I can't use that!",
	     				"Don't give me that!"
				}
				#Notoriety Famous 
				{
					"I really don't need thy cast-offs!",
	     				"Thy generosity is duly noted. Thanks, but no thanks.",
	     				"I don't need that."
				}
			}
			#Attitude Neutral 
			{
                		#Notoriety Infamous 
				{
                    			"Thank thee. Don't need any.",
	      				"I don't want it. Sorry.",
	      				"I don't have a use for it. Thanks, though."
                		}
                		#Notoriety Anonymous 
				{
                    			"Tis very generous of thee, but I don't need it.",
                    			"I can't take it. Sorry.",
	      				"Thank thee, but I don't want it." 
				}
                		#Notoriety Famous 
				{
                     			"Thou art most kind $good sir/good lady$, but it isn't anything that I need.",
	     				"Please, keep it. I don't want it.",
	     				"I really don't need it."
                		}
            		}
            		#Attitude Kindly 
			{
                		#Notoriety Infamous 
				{
                    			"Keep it! I have no need for it.",
                    			"'Tis a shame I have no use for it. Keep it.",
	      				"If only I needed it! I don't."
                		}
				#Notoriety Anonymous 
				{
                    			"'Tis kind of thee, but I have no use for it.",
                    			"Thank thee, but no.",
	      				"Thank thee, kind $sir/lady$, but no."
                		}
                		#Notoriety Famous 
				{
                    			"I would like to accept thy gift, but I cannot. Thank thee.",
                    			"Thank thee _Name_! But keep it. I have no use for it.",
	      				"'Tis greatly appreciated, but unnecessary. I don't want it."
				}
			}
		}
		#Key "@InternalNeedResponse" 
		{
            		#Attitude Wicked 
			{
                		#Notoriety Infamous 
				{
                    			"I heard thee say [GetNeed]! I need some!",
	      				"Thou did say [GetNeed]! I could really use some!",
	      				"I want some [GetNeed]!"
				}
                		#Notoriety Anonymous 
				{
                   			"'Twould be helpful if I could get [GetNeed] myself.",
	     				"If thou dost have [GetNeed], I'd be happy to take some from thee.",
	     				"Could I have some? [GetNeed], I mean."
				}
				#Notoriety Famous 
				{
					"If thou couldst bring thy great person to get ME some[GetNeed], twould help!",
	     				"Not that I expect it, but I could really use help getting [GetNeed].",
	     				"If thou couldst find some [GetNeed] for me, 'twould make my life that much better. Not that a great one like thee should care..."
				}
			}
			#Attitude Neutral 
			{
                		#Notoriety Infamous 
				{
                    			"I could use some of thy stolen [GetNeed].",
	      				"If thou did... happen upon some [GetNeed], I could use some.",
	      				"I could use some [GetNeed]. However thou might obtain it."
                		}
               	 		#Notoriety Anonymous 
				{
                    			"If thou art given any [GetNeed], bring it here. I could use some.",
                    			"If thou dost have [GetNeed], I'd be willing to take it from thee.",
	      				"Just a little [GetNeed] is all I need!" 
				}
                		#Notoriety Famous 
				{
                     			"If thou couldst find it in thy heart to get ME some[GetNeed], twould be most wonderful!",
	     				"I would appreciate it greatly if thou couldst get me [GetNeed]. I could really use some.",
	     				"If thou couldst find some [GetNeed] for me, 'twould make my life that much better."
                		}
            		}
            		#Attitude Kindly 
			{
                		#Notoriety Infamous 
				{
                    			"Please, $sir/madam$, if thou art able, I could really make use of [GetNeed].",
                    			"I would be grateful if thou wouldst help me acquire some [GetNeed].",
	      				"If thou couldst bring me some [GetNeed], I would be in thy debt."
                		}
				#Notoriety Anonymous 
				{
                    			"I would love to have some [GetNeed]!",
                    			"'Twould be nice to get more [GetNeed]",
	      				"[GetNeed]? I could use some more."
                		}
                		#Notoriety Famous 
				{
                    			"Please, $good sir/good lady$, I could use some more [GetNeed].",
                    			"I could really do with some more [GetNeed].",
	      				"If thou dost find the time, more [GetNeed] would do wonders for me."
				}
			}
		}
		#Key "@InternalAcceptItem" 
		{
            		#Attitude Wicked 
			{
                		#Notoriety Infamous 
				{
                    			"Good! 'Tis what I need!",
	      				"Thanks.",
	      				"'Tis appreciated."
				}
                		#Notoriety Anonymous 
				{
                   			"I will accept it from thee. Thanks.",
	     				"I will make good use of it.",
	     				"I've been needing it."
				}
				#Notoriety Famous 
				{
					"I'm happy that thou chose to do without.",
	     				"This will certainly help me. Thanks.",
	     				"I appreciate it."
				}
			}
			#Attitude Neutral 
			{
                		#Notoriety Infamous 
				{
                    			"Thank thee.",
	      				"I accept.",
	      				"I will take it, then."
                		}
                		#Notoriety Anonymous 
				{
                    			"Tis very generous of thee.",
                    			"I shall accept with pleasure.",
	      				"Thank thee!" 
				}
                		#Notoriety Famous 
				{
                     			"Thou art most kind $good sir/good lady$!",
	     				"I shall be happy to take it!  Thank thee!",
	     				"May a light follow thee in the dark!"
                		}
            		}
            		#Attitude Kindly 
			{
                		#Notoriety Infamous 
				{
                    			"Why, I thank thee!",
                    			"'Tis good to see thee doing right.",
	      				"I knew that thy generosity would show through!"
                		}
				#Notoriety Anonymous 
				{
                    			"'Tis gracious of thee!",
                    			"I humbly accept. Thank thee.",
	      				"Thank thee, kind $sir/lady$!"
                		}
                		#Notoriety Famous 
				{
                    			"This from _Name_? Oh thank thee!",
                    			"Thank thee _Name_! Thank thee!",
	      				"'Tis greatly appreciated!"
				}
			}
		}
		#Key "@InternalPersonalSpace" 
		{
            		#Attitude Wicked 
			{
                		#Notoriety Infamous 
				{
                    			"Here, get off me!",
	      				"Back off, fool.",
	      				"Back away. Thy smell overwhelms me.",
	      				"I have no wish to look so closely on thine ugly face."
				}
                		#Notoriety Anonymous 
				{
                   			"Dost thou try to provoke me?",
	     				"Thy nearness offends me.",
	     				"Stand not so close by me.",
	     				"Stand back, I do not wish to be seen so near to thee."
				}
				#Notoriety Famous 
				{
                   			"Who would have thought that such a great personage would smell so common.",
	     				"I care not how important thou art, stand back from me.",
	     				"Step back, or thou shalt need more than a fine name to protect thee.",
	     				"Wherefore followest thou me so closely?"
				}
			}
			#Attitude Neutral 
			{
                		#Notoriety Infamous 
				{
                    			"Why standest thou so near to me?",
	      				"Prithee, step back.",
	      				"Wilt thou stand back a bit",
	      				"I seek no trouble with thee, but please step back."
                		}
                		#Notoriety Anonymous 
				{
                    			"Wherefore dost thou press me so closely?",
                    			"Thou art nearer to me than I like.",
	      				"Stand thou back.", 
      					"Please back away a bit, $milord/milady$.",
     	      				"I would ask thee to stand not so near to me."
				}
                		#Notoriety Famous 
				{
                    			"An it please thee, wilt thou step back a bit.",
	      				"Is it necessary to stand so near by?",
	      				"Forgive me for asking, but please step back.",
      					"Please back away a bit, $milord/milady$.",
	      				"$Sir/Madam$, thou standest too close."
                		}
            		}
            		#Attitude Kindly 
			{
                		#Notoriety Infamous 
				{
                    			"Please, wilt thou stand off a little ways?",
                    			"A bit more distance between thee and me, an it please thee.",
	      				"Excuse me, but pray step back.",
     	      				"May I ask thee to step back a bit."
                		}
				#Notoriety Anonymous 
				{
                    			"Friend, why standest thou so close?",
                    			"Take a step back, if it please thee.",
	      				"I would prefer if thou wouldst step back.",
	      				"A bit more space between thee and me, I pray thee."
                		}
                		#Notoriety Famous 
				{
                    			"Forgive me, I seem to be standing too close to thee.",
                    			"An it please thee, wilt thou step back a bit.",
	      				"Forgive me $sir/madam$, but wilt thou step back a pace or two.",
	      				"Thou art standing very close to me."
				}
			}
		}
		#Key "@InternalScavenger" 
		{
            		#Attitude Wicked 
			{
               			"Mine!",
				"I'll take that!",
				"Keep away from this! It's mine!",
				"Ah ha! It's mine!",
				"Come here, my pretty.",
				"Well here's something!"
			}
         		#Attitude Neutral 
			{
               			"Well, well, what have we here?",
				"Ah, this will be useful.",
				"Excellent. 'Tis my lucky day.",
				"I've been needing one of these.",
				"This will come in handy indeed.",
				"Well, look what I found!"
                	}
            		#Attitude Kindly 
			{
				"Why look at that!",
				"Ah, most interesting.",
				"This may be of some use.",
				"And what's this then?",
				"A most fortunate discovery!"
			}
		}
		#Key "*hello*" "hi" "*greetings*" "*good*see*thee*" 
		{
			#Attitude Wicked, Belligerent 
			{
				"What dost thou want?",
				"Uh huh?", 
				"Yeah, what?"
			}
			#Attitude Neutral 
			{
				"Hello.",
				"Hi.", 
				"Greetings."
			}
			#Attitude Goodhearted, Kindly 
			{
				"Hello, $milord/milady$.",
				"Hi.", 
				"Greetings, $milord/milady$."
			}
		}
		#Key "*how*you*" "*how*thou*" 
		{
			#Attitude Wicked, Belligerent 
			{
				"Horrible!",
				"None of thy business!", 
				"Get thee away from me!"
			}
			#Attitude Neutral 
			{
				"I'm doing relatively well.",
				"Just fine.", 
				"I am well."
			}
			#Attitude Goodhearted, Kindly 
			{
				"I am well, $milord/milady$. I hope thou art the same.",
				"Doing great!", 
				"As well as I can be, $milord/milady$."
			}
		}
		#Key "*where*thou*live*", "*where*you*live*",  "*thou*live*", "*you*live*", 
			"*what city are you from*", "*what town are you from*", "*where*you*from*",
			"*where*thou*from*"  
		{
			#Attitude Wicked, Belligerent 
			{
				"I live here!",
				"None of thy business!", 
				"I live in the bottom of the ocean!  I only come here during the day!"
			}
			#Attitude Neutral 
			{
				"I live here in _Town_.",
				"I live in _Town_.",
				"Here.  In _Town_.",
				"Right here.  In _Town_.", 
				"I live in the town that thou art standing in."
			}
			#Attitude Goodhearted, Kindly 
			{
				"I live here in _Town_.",
				"Here, $milord/milady$.",
				"Right here.  In _Town_, $milord/milady$.", 
				"I live in the town that thou art standing in, $milord/milady$."
			}
		}
		#Key "*where*am*I*", "*What town am I in*", "*what*town is this*" 
		{
			"Thou art in _Town_.",
			"Thou art a visitor of _Town_.",
			"If thou art lost, then know that thou art in _Town_."
		}
		#Key "*where*thou*work*" "*where*you*work*" 
		{
			#Attitude Wicked, Belligerent 
			{
				"I work here, moron!",
				"None of thy business!", 
				"I work out of a cave!  What dost thou think, imbecile!"
			}
			#Attitude Neutral 
			{
				"I work here in _Town_.",
				"Here.  In _Town_.", 
				"I work in the town that thou art in."
			}
			#Attitude Goodhearted, Kindly 
			{
				"I work here in _Town_.",
				"Here.  In _Town_, $milord/milady$.", 
				"I work in this town, $milord/milady$."
			}
		}
            	#Key "*thanks*", "*thank you*", "*thank thee*", "*thank ye*" "*appreciate*" 
		{
                	"Thou'rt welcome.",
                	"'Twas nothing.",
                	"Certainly, thou art welcome.",
                	"Not a problem."
            	}
            	#Key "*bye*", "*fare*well*", "*chow*", "*ciao*", "*see*ya*", "*see*you*"  
		{
	        	"Goodbye.[Leave]",
                	"Farewell.[Leave]" 
	        }
	        #Key "*who are you*", "*what's your name*", "*what is your name*",     
			"*who art thou*", "*who are ye*", "*who*you*",
			"*what are you called*", "*what art thou called*",
	             	"*what are ye called*", "*know your name*", "*know thy name*",      
			"*know yer name*", "*what is thy name*" "*what's thy name*" 
		{
		             "My name is _MyName_.",
		             "I am _MyName_, $milord/milady$."
		             "Thou mayest call me _MyName_."
 		}
		#Key "*Is*name*" "*What's thy name*" 
		{
		             "My name, $sir/madam$, is _MyName_.",
		             "I am _MyName_.",
		             "If thou art looking for _MyName_ then thou hast found me."
        	}
	        #Key "name" 
		{
        		"If thou'rt asking for my name, $milord/milady$, please be more specific.",
        		"Please be more specific, if thou'rt asking for my name, $milord/milady$."
        	}
		#Key "*make*money*", "*earn*money*", "*get*money*" 
		{
			#Attitude Wicked, Belligerent
			{
				"Oh, well there's stealing, begging, scavenging... many ways for one such as thee to find money.",
				"I'm sure that thou can find many varied ways to take money. Like stealing things and selling them back to their owners, killing people...",
				"Just lift money from people whho have it. That's what thy type would do anyway.",
				"Thou can always beg for thy gold."
			}
			#Attitude Neutral
			{
				"If thou dost want to earn money, practice a skill. Make things and sell them to the public. Most shopkeepers will let thee use the tools and materials that they aren't using.",
				"If thou art able to hunt the wild animals, sometimes thou can sell the meat that thou dost carve from them to a butcher.",
				"If thou can get an axe, perhaps thou could cut some trees into useful boards for woodworkers. They may be willing to pay thee for tham.",
				"If thou dost happen upon some ore, go to the blacksmith's shop and try to craft some armor or weapons. I have heard that it just takes practice.",
				"I have heard that the great dungeons sometimes have great treasures.",
				"Tanners will buy hides and pelts that thou dost take from the animals thou dost hunt.",
				"Thou can sell feathers from birds to a bowyer. And a cook may purchase the bird itself from thee."
			}
			#Attitude Goodhearted, Kindly
			{
				"There are many different ways to make money in Britannia. Thou can sell raw materials to merchants, make and sell goods, sell the meat that thou dost find hunting... there are many ways.",
				"The merchants will usually let people practice skills in their shops. All thou dost need is raw materials, and sometimes tools.",
				"Some will stoop to stealing or killing to get their money, so be careful where thou dost walk. Make use of thy skills. Make something and sell it to a merchant.",
				"Hunting seems to be a very profitable source of income. Most cooks will purchase fowl and fish from thee, butchers will pay thee for meat, and tanners for the pelts and hides."
			}
		}
		#Key "*camp*"
		{
			"It's essential to have good kindling and a bedroll if thou dost want to camp.",
			"I never leave the city without a bedroll on which to sleep and some kindling for a fire.",
			"Kindling for a fire and a bedroll for thy comfort is all thou dost need to make camp."
		}
		#Key "*how*quit*?*", "*log off*", "*logoff*"
		{
			"Making camp, if thou art in the wilds, and paying for a room in an Inn are the ways to give thyself a rest from this world.",
			"If thou art wanting a respite from the rigors of Britannia, then thou can make a camp or check in to an Inn, whichever is the most convenient."  
		}
		#Key "*bedroll*"
		{
			"Bedrolls can be purchased from a provisioner.",
			"If thou dost need a bedroll, I think a provisioner might help thee.",
			"I think thou can find a bedroll at a provisioner's shop, if that is what thou art looking for."
		}
		#Key "*kindling*"
		{
			"If thou dost use an axe on some wood, thou should get kindling from it. Otherwise, I think a provisioner will carry some for those wanting to camp in the wild.",
			"Thou shouldst be able to get kindling from any wood thou finds. If thou has an axe, of course.",
			"A provisioner can supply thee with kindling if thou dost lack the means to make thy own."
		}
		#Key "*cave*", "*dungeon*", "*destard*", "*despise*", "*shame*", "*deceit*", "*hythloth*", "*wrong*", "*covetous*"
		{
			"There are many as yet unmapped places beneath Britannia. 'Tis rumored to be riches and magic in them. And creatures that will rend the flesh from thy bones.",
			"The places under Britannia - some call them 'dungeons' - are spposed to be rich with treasures and gold. They are also guarded by horrible creatures that want nothing more than to dine on humans.", 
			"I know of seven dungeons in Britannia. Covetous, Despise, Deceit, and... a, uh, few others.",
			"Let's see... I remember the names of Shame, Wrong, Despise, and Hythloth. I've never owned a map of Britannia myself.",
			"I think the dungeons that people tend to forget are Destard, Covetous, and Hythloth. I think."
		}
		#Key "*gold*", "*treasure*"
		{
			"Thou can find a good deal of treasure under Britannia. Look to the caves and what thou call dungeons.",
			"Some of the more dangerous creatures will carry gold that they've scavenged.",
			"If thou art needing gold, either find it in the dungeons, or scavenge it from monsters thou have killed. Thou will find it either way, if thou dost survive."
		}
        	#Key "*Britannia*" 
		{
			#Notoriety Infamous, Outlaw 
			{
				"What wouldst thou have me say about Britannia, save that $rogues/foul wenches$ such as thee do much to bespoil its name.",
				"Britannia? I ask thee not to speak of this land while in my company, $varlet/witch$!",
				"$Scoundrel/Foul wench$! Thou carest not for Britannia! Thus, I ask thee to remove the name of Lord British's realm from thy mind." 
			}
			#Notoriety Anonymous 
			{
				"Ah, fair Britannia. Those who travel claim it is but one realm of many.",
				"I cannot speak but fondly on my homeland. Yet I would not forgo an opportunity to see other lands.",
				"I have lived in Britannia all my life. I do love it so."
			}
			#Notoriety Famous, Known 
			{
				"Ah, thou dost speak of fair Britannia. Lord British, himself, wouldst be proud of thine efforts to keep it peaceful. Yet, those who travel claim it is but one realm of many.",
				"I cannot speak but fondly on my homeland, the very land thou dost seek to keep safe. Yet I would not forgo an opportunity to see other lands.",
				"I have lived in Britannia all my life, and so I thank thee for thine efforts to improve the lives of all who live here."
			}
		}
		#Key "*Buccaneer's Den*" 
		{
			"Buccaneer's Den? Why, 'tis nothing more than a hiding place for pirates!",
			"'Tis where the scum of the realm go to lick their wounds.",
			"I would not live in Buccaneer's Den if I were thee, $milord/milady$, for it is home to the most vile men and women to sail the seas."
		}
		#Key "*Britain*", "*capital*" 
		{
			"Britain is the capital of all Britannia, land of Lord British.",
			"Lord British, sovereign of all Britannia, resides in Britain.",
			"Britain is home to Lord British, himself. 'Tis the capital of Britannia."
		}
		#Key "*Cove*" 
		{
			"Cove? Thou shouldst find it North and East of Britain.",
			"Cove is not much of a city. 'Tis more of a village, to be truthful.",
			"Just Northeast of Britain is Cove."
		}
		#Key "*Jhelom*" 
		{
			"Jhelom is an outpost for mercenaries and sellswords.",
			"Jhelom? 'Tis a town of sword arms looking for work. Thou canst find it off the southwestern coast of mainland Britannia.",
			"Some say Jhelom is as rough as Buccaneer's Den, with half the scoundrels and twice the bruises."
		}
		#Key "*Magincia*" 
		{
			"Quite a lovely place, $milord/milady$. And shouldst thou doubt my word, just ask someone who resides there.",
			"Magincia? 'Tis a city of arrogance and pride, I think.",
			"Magincia is a city spanning nearly an entire island, outside of Britanny Bay."
		}
		#Key "*Minoc*" 
		{
			"Minoc is a mining town now.",
			"Minoc? Why it was once home to the best artisans in the realm, but now that rich deposits of precious metals have been discovered in the mountains, 'tis full of miners.",
		      	"Minoc lies on the northern coast of the mainland, $milord/milady$."		
		}
		#Key "*Moonglow*" 
		{
			"Magic -- that's what one can find in Moonglow.",
			"Moonglow is nothing more than one large school for mages.",
			"If thou dost wish to study magic, thou shouldst visit the isle of  Moonglow."
		}
		#Key "*Serpent's Hold*" 
		{
			"Serpent's Hold is where the best knights are trained. 'Tis located in the Cape of Heroes, off the southeastern edge of the mainland.",
			"Serpent's Hold is the fortification Lord British granted to his loyal knights, those of the Order of the Silver Serpent.",
			"Serpent's Hold? Art thou interested in becoming a member of Lord British's royal order of Knights -- the Order of the Silver Serpent? If such is true, then thou shouldst go to Serpent's Hold."
		}
		#Key "*Skara Brae*" 
		{
			"Skara Brae is an island. Thou canst find it off the western coast of Britannia.",
			"The most skilled trackers in the land learn their craft on the island of Skara Brae.",
			"Many talented shipwrights live on Skara Brae, though the island is actually known for the many trackers who teach their skills there."
		}
		#Key "*Trinsic*" 
		{
			"Though Serpent's Hold may be home to valiant knights, and Jhelom home to skilled mercenaries, Trinsic is where one shouldst go to find the most honorable warriors of the realm.",
			"Though known as a haven for men and women of honor who wish to hone their martial skills, Trinsic also supports a large guild of architects and engineers.",
       			"Trinsic? Why, thou couldst find thy way there by merely following the south road from Britain."
		}
		#Key "*Vesper*" 
		{
			"Vesper? 'Tis a city in northeastern Britannia. Ore from the mines of Minoc are sent down river to be unloaded in Vesper.",
			"Much of the crafts forged in Minoc find their way to Vesper by way of the river, $milord/milady$.",
			"Some claim Vesper is merely an extension of Minoc, calling it nothing more than a large marketplace for artisans to sell their wares."
		}
		#Key "*Yew*" 
		{
			"Yew is a small, peaceful community of farmers in northwestern Britannia.",
			"The High Court of Britannia is in Yew. 'Tis there that important cases that concern all of Britannia are decided, many of which are determined by Lord British, himself.",
			"They say farmers and criminals are all who go to Yew, $milord/milady$, save for those hoping to visit one of the two."
		}
		#Key "*Britanny Bay*" 
		{
			"'Tis the bay that touches the city of Britain.",
			"Britanny Bay is the body of water on the edge of Britannia's capital.",
			"Britanny Bay? Why, 'tis the waters that border the ports of Britain."
		}
		#Key "*Order of the Silver Serpent*", "*Knights*" 
		{
			"'Tis the order of knights who served Lord British in the battle against Lord Robere.",
			"When Lord Robere challenged Lord British for his share of the realm, Lord British's faithful knights defended the kingdom.",
			"Only the bravest knights belong to the Order of the Silver Serpent. 'Twas they who defeated the forces of Lord Robere."
		}
		#Key "*Robere*"," Lord Robere*" 
		{
		 	"History claims that Lord Robere was once an honorable man. But he was overcome with greed and sought to take the entire realm by force.",
			"Years ago, Lord Robere made claims to the lands under Lord British's domain. Were it not for the stout knights in the Order of the Silver Serpent, Lord Robere could very well have conquered the entire realm.", 
			"Though a kind and generous man in his youth, Lord Robere thirsted for power in his later years, so legends say. 
Had not the Order of the Silver Serpent been ready to fight for Lord British, this very land could have belonged to the ambitious conqueror."  
		}
		#Key "*Lord British*", "*ruler*", "* king*" 
		{
	    		#Attitude Neutral 
			{
	    			"I have always find Lord British to be fair and just in his rule.",
	    			"From my liege I ask no more than a strong but gently, guiding hand. Thus far, Lord British has shown just that.",
	    			"Lord British governs as any wise ruler would -- with an even hand and a thoughtful eye."	
	    		}
	    		#Attitude Wicked, Belligerent 
			{
	    	       		#Notoriety Infamous, Outlaw 
				{
	    				"Love him like thine own brother, they say.  Fine, say I, but must I be so unnkind to mine own flesh and blood?",
					"Yes, it seems Lord British doth possess a rare gift for leadership...and I wish he would give it back.",
					"Wert thou not so masterful, $Milord/Milady$, I would compare THEE to our illustrious ruler... but only a fool would confuse a fine $man/soul$ such as thee with Britannia'a royalty."
				}				
		       		#Notoriety Anonymous 
				{
					"Hmmm...  yes, Lord British. That's a difficult one....",
					"I care not for his empty promises or hollow beliefs, for they brought me nothing vain hope.",
 						"Please accept mine apology, o' noble $lord/lady$, but I care little about sharing mine opinions on Lord Britsh with thee."
			       	}
			       	#Notoriety Famous, Known 
				{
			       		"Lord British governs as any intelligent ruler would -- from afar, safe in his castle.",
			       		"I do not think thou wouldst agree with mine opinion.  Let us remain civil, shall we?",
			       		"My dear, departed mother taught me never to say ill words about another person. So I shan't."
			       	}
	  		}
	    		#Attitude Goodhearted, Kindly 
			{
				#Notoriety Infamous, Outlaw 
				{
					"Though thou might never know such, Lord British has done much to make our land one of prosperity.",
					"Fear not, troubled soul. The worries that thou might have about our land will soon be made to vanish by Lord British.",
					"It may not be apparent to thee, $Milord/Milady$, but Lord British's guidance has made Britannia great."
				}
				#Notoriety Anonymous 
				{
					"Lord British is a kind and generous ruler.",
					"I do not know of another who would rule as fairly in Lord British's stead.",
					"In truth, Lord British does his best to address any concerns we seem to have."
				}
				#Notoriety Famous, Known 
				{
					"As thou must surely know, $Milord/Milady$ _Name_, Lord British is the reason our land has prospered so.",
					"I have little fear that all will be well in Britannia.",
					"As must be plainly apparent to thee, $Milord/Milday$, Lord British's guidance has made Britannia great."
				}
			}
		}
		#Key "*Weather*" 
		{
			"Ah, the weather... 'Tis an interesting thing, really. No matter what the season, no matter what enchantments are cast, our land is almost always blessed with clear and beautiful blue skies."
		}
		#Key "*concerns*", "*troubles*" 
		{
			"Various issues surface from time to time, such as taxation, invasion, protection form creatures of the wild.",
			"Any land experiences difficult times, but it takes a wise ruler to lead his people through them.",
			"Surely thou dost understand -- life cannot always be free of trouble."
		}
		#Key "*blackthorn*" 
		{
			"I've heard that Lord Blackthorn has written a couple of books describing his philosophies in-depth. If thou dost find these, I imagine thou wouldst be able to say better than I what Blackthorn is about.",
			"Lord Blackthorn and Lord British are still friends, or so I've heard. They just envision different futures for Britannia.",
			"Lord Blackthorn wishes the freedom of choice in ALL things extended to everyone. Some say that he'd even sees the Orcs and Lizardmen as equals to the humans in Britannia."
		}
		#Key "*Shamino*" 
		{
			"Lord Shamino? Ah, yes, Lord British's friend and ally, I believe.",
			"The name is familiar to me... ah, yes -- I believe he is the infrequent guest of Lord British.",
			"Not a name bandied about often, to be sure, kind $sir/lady$. I suspect thou'rt refering to the oft-time companion of Lord British, himself."	  
		}
		#Key "*Iolo*" 
		{
			"Iolo? Ah, yes, Lord British's friend and ally, I believe.",
			"The name is familiar to me... ah, yes -- I believe he is the infrequent guest of Lord British.",
			"Not a name bandied about often, to be sure, kind $sir/lady$. I suspect thou'rt refering to the oft-time companion of Lord British, himself."	  
		}
		#Key "*Dupre*" 
		{
			"Dupre? Ah, yes, Lord British's friend and ally, I believe. If thou wert to search for him, I wouldst most recommend a tavern.",
			"The name is familiar to me... ah, yes -- I believe he is the infrequent guest of Lord British. Mayhaps thou wilt find him sampling the local spirits.",
			"Not a name bandied about often, to be sure, kind $sir/lady$. I suspect thou'rt refering to the oft-time fighting companion of Lord British, himself."	  
		}
		#Key "*New Magincia*" 
		{
			"New Magincia? Is there something wrong with the original?",
			"Forgive me, $milord/milady$, but I think thou dost suffer the madness of the drink.",
			"I have not heard of such a place, $milord/milady$. I suppose it could be a colony of Britannians who have settled in a new territory."
		}
		#Key "*colony*" 
		{
			"I know not of any, $milord/milady$.",
			"Rumors abound that Lord British wishes to send adventurous and resourceful individuals to settle unexplored areas.",
			"I know not of which thou doth speak, $milord/milady$."
			
		}
		#Key "*Virtue*", "*virtues*", "*shrines*","*truth*", "*love*", 
			"*courage*", "*spirituality*", "*valor*", "*honor*", 
			"*justice*", "*sacrifice*", "*honesty*", "*humility*", "*compassion*" 
		{ 
			"Shrines to the virtues are spread around our land. I've heard that they can even resurrect the dead.",
			"Rest and health can be found at the shrines.",
			"The shrines are rumored to have the power to resurrect the dead."
		}
		#Key "*avatar*" 
		{
			"Who?",
			"I'm sorry, I know not of whom thou doth speak.",
			"I have never heard of this tar person.",
			"I cannot help thee."
		}
		#Key  "*moongates*" 
		{
			"The moongates? They are... doors to different parts of Britannia. Except the destinations of the doors change with the phases of the two moons.", 
			"Thou shouldst learn to use the moongates if thou dost plan to travel far. The key is in the phases of the moons.", 
			"If thou dost use the moongates to travel, then thou might not end up where thou had planned, unless thou hast learned how to use them correctly."
		}
		#Key "*moons*"
		{
			"Our moons are called Trammel and Felucca. They control the moongates."
		}
	}
	#Sophistication Medium 
	{
		#Key "@InternalRefuseItem" 
		{
        		#Attitude Wicked 
			{
            			#Notoriety Infamous 
				{
                			"No thanks!",
	      				"Thanks, but no.",
	      				"I don't want that!"
				}
                		#Notoriety Anonymous 
				{
                			"I don't want it.",
	     				"I can't use that!",
	     				"Don't give me that!"
				}
				#Notoriety Famous 
				{
					"I really don't need thy cast-offs!",
	     				"Thy generosity is duly noted. Thanks, but no thanks.",
	     				"I don't need that."
				}
			}
			#Attitude Neutral 
			{
            			#Notoriety Infamous 
				{
                			"Thank thee. Don't need any.",
	      				"I don't want it. Sorry.",
	      				"I don't have a use for it. Thanks, though."
        			}
            			#Notoriety Anonymous 
				{
                			"Tis very generous of thee, but I don't need it.",
                			"I can't take it. Sorry.",
	      				"Thank thee, but I don't want it." 
				}
            			#Notoriety Famous 
				{
                			"Thou art most kind $good sir/good lady$, but it isn't anything that I need.",
	     				"Please, keep it. I don't want it.",
	     				"I really don't need it."
        			}
        		}
        		#Attitude Kindly 
			{
            			#Notoriety Infamous 
				{
                			"Keep it! I have no need for it.",
                			"'Tis a shame I have no use for it. Keep it.",
	      				"If only I needed it! I don't."
        			}
	    			#Notoriety Anonymous 
				{
                			"'Tis kind of thee, but I have no use for it.",
                			"Thank thee, but no.",
	      				"Thank thee, kind $sir/lady$, but no."
        			}
           	 		#Notoriety Famous 
				{
                			"I would like to accept thy gift, but I cannot. Thank thee.",
                			"Thank thee _Name_! But keep it. I have no use for it.",
	      				"'Tis greatly appreciated, but unnecessary. I don't want it."
				}
			}
		}
		#Key "@InternalNeedResponse" 
		{
        		#Attitude Wicked 
			{
            			#Notoriety Infamous 
				{
                			"I heard thee say [GetNeed]! I need some!",
	      				"Thou did say [GetNeed]! I could really use some!",
	      				"I want some [GetNeed]!"
				}
            			#Notoriety Anonymous 
				{
               				"'Twould be helpful if I could get [GetNeed] myself.",
	     				"If thou dost have [GetNeed], I'd be happy to take some from thee.",
	     				"Could I have some? [GetNeed], I mean."
				}
	    			#Notoriety Famous 
				{
					"If thou couldst bring thy great person to get ME some[GetNeed], twould help!",
	     				"Not that I expect it, but I could really use help getting [GetNeed].",
	     				"If thou couldst find some [GetNeed] for me, 'twould make my life that much better. Not that a great one like thee should care..."
				}
			}
			#Attitude Neutral 
			{
            			#Notoriety Infamous 
				{
                			"I could use some of thy ill-gotten [GetNeed].",
	      				"If thou did... happen upon some [GetNeed], I could use some.",
	      				"I could use some [GetNeed]. However thou might obtain it."
        			}
            			#Notoriety Anonymous 
				{
                			"If thou art given any [GetNeed], bring it here. I could use some.",
                			"If thou have any [GetNeed], I'd be willing to take some.",
	     				"Just a little [GetNeed] is all I need!" 
				}
            			#Notoriety Famous 
				{
                			"If thou couldst get ME some[GetNeed], twould be most wonderful!",
	     				"I would appreciate it greatly if thou couldst get me [GetNeed]. I could really use some.",
	     				"If thou couldst find some [GetNeed] for me, 'twould be easier on me."
        			}
        		}
        		#Attitude Kindly 
			{
            			#Notoriety Infamous 
				{
                			"Please, $sir/madam$, I could really make use of [GetNeed], if thou art able.",
                			"I would be grateful if thou wouldst help me get some more [GetNeed].",
	      				"If thou couldst bring me some [GetNeed], I would be in thy debt."
       	 			}
	    			#Notoriety Anonymous 
				{
                			"I really need some [GetNeed]!",
                			"'Twould be nice to get more [GetNeed]",
	      				"[GetNeed]? I could always use more."
        			}
            			#Notoriety Famous 
				{
                			"I could really use some more [GetNeed], $milord/milady$."
                			"I could really do with some more [GetNeed]. I could reward thee... maybe.",
	      				"If thou dost find the time, more [GetNeed] would do wonders for me."
				}
			}
		}
		#Key "@InternalAcceptItem" 
		{
        		#Attitude Wicked 
			{
            			#Notoriety Infamous 
				{
                			"Good! 'I needed this!",
	      				"Thanks.",
	      				"I appreciate it."
				}
            			#Notoriety Anonymous 
				{
                			"I will take it from thee. Thanks.",
	     				"I will make use of it.",
	     				"I've been needing it."
				}
	    			#Notoriety Famous 
				{
					"I'm happy that thou chose to give it to me.",
	     				"This will certainly help. Thank thee.",
	     				"I appreciate it."
				}
			}
        		#Attitude Neutral 
			{
            			#Notoriety Infamous 
				{
                			"Thank thee.",
	      				"I accept.",
	      				"I will take it, then."
        			}
            			#Notoriety Anonymous 
				{
                			"Tis generous of thee.",
                			"I shall accept with pleasure!",
	      				"Thank thee!" 
				}
            			#Notoriety Famous 
				{
                			"Thou art a kind $man/lady$!",
	     				"I shall be happy to take it!  Thank thee!",
	     				"May thy paths be smooth!"
        			}
        		}
        		#Attitude Kindly 
			{
            			#Notoriety Infamous 
				{
                			"Why, I thank thee!",
                			"'Tis good to see thee doing aright.",
	      				"I knew that thy generosity would finally come through!"
        			}
	    			#Notoriety Anonymous 
				{
                			"'Tis gracious of thee!",
                			"I humbly accept. Thank thee, kind $sir/lady$.",
	      				"Thank thee, $milord/milady$!"
        			}
            			#Notoriety Famous 
				{
                			"This from _Name_? Thank thee!",
                			"Thank thee _Name_! Thank thee very much!",
	      				"'Tis greatly appreciated!"
				}
			}
		}
		#Key "@InternalPersonalSpace" 
		{
        		#Attitude Wicked 
			{
            			#Notoriety Infamous 
				{
                			"Here, get off me!",
	      				"Back off, fool.",
	      				"Back away. Thy smell overwhelms me.",
	      				"I have no wish to look so closely on thine ugly face."
				}
            			#Notoriety Anonymous 
				{
               				"Dost thou try to provoke me?",
	     				"Thy nearness offends me.",
	     				"Stand not so close by me.",
	     				"Stand back, I do not wish to be seen so near to thee."
				}
	    			#Notoriety Famous 
				{
                			"Who would have thought that such a great personage would smell so common.",
	     				"I care not how important thou art, stand back from me.",
	     				"Step back, or thou shalt need more than a fine name to protect thee.",
	     				"Wherefore followest thou me so closely?"
				}
			}
			#Attitude Neutral 
			{
            			#Notoriety Infamous 
				{
                			"Why standest thou so near to me?",
	      				"Prithee, step back.",
	      				"Wilt thou stand back a bit",
	      				"I seek no trouble with thee, but please step back."
        			}
            			#Notoriety Anonymous 
				{
                			"Wherefore dost thou press me so closely?",
                			"Thou art nearer to me than I like.",
	      				"Stand thou back.", 
      					"Please back away a bit, $milord/milady$.",
     	      				"I would ask thee to stand not so near to me."
				}
            			#Notoriety Famous 
				{
                			"An it please thee, wilt thou step back a bit.",
	      				"Is it necessary to stand so near by?",
	      				"Forgive me for asking, but please step back.",
      					"Please back away a bit, $milord/milady$.",
	      				"$Sir/Madam$, thou standest too close."
        			}
        		}
        		#Attitude Kindly 
			{
            			#Notoriety Infamous 
				{
                			"Please, wilt thou stand off a little ways?",
                			"A bit more distance between thee and me, an it please thee.",
	      				"Excuse me, but pray step back.",
     	      				"May I ask thee to step back a bit."
        			}
	    			#Notoriety Anonymous 
				{
                			"Friend, why standest thou so close?",
                			"Take a step back, if it please thee.",
	      				"I would prefer if thou wouldst step back.",
	      				"A bit more space between thee and me, I pray thee."
        			}
            			#Notoriety Famous 
				{
                			"Forgive me, I seem to be standing too close to thee.",
                			"An it please thee, wilt thou step back a bit.",
	      				"Forgive me $sir/madam$, but wilt thou step back a pace or two.",
	      				"Thou art standing very close to me."
				}
			}
		}
		#Key "@InternalScavenger" 
		{
            		#Attitude Wicked 
			{
               			"Mine!",
				"I'll take that!",
				"Keep away from that! It's mine!",
				"Ah ha! I've got it now.",
				"Come here, my pretty.",
				"Here's something!"
			}
            		#Attitude Neutral 
			{
               			"Well, well, what have we here?",
				"Ah, this will be useful.",
				"Excellent. 'Tis my lucky day.",
				"I've been needing one of these.",
				"This will come in handy indeed.",
				"Hmmm! Look what I found!"
        		}
            		#Attitude Kindly 
			{
				"Why look at that!",
				"Ah, most interesting.",
				"This may be of some use.",
				"And what's this then?",
				"A most fortunate discovery!"
			}
		}
		#Key "*hello*" "hi" "*greetings*" "*good*see*thee*" 
		{
			#Attitude Wicked, Belligerent 
			{
				"What dost thou want?",
				"Uh huh?", 
				"Yeah, what?"
			}
			#Attitude Neutral 
			{
				"Hello.",
				"Hi.", 
				"Greetings."
			}
			#Attitude Goodhearted, Kindly 
			{
				"Hello, $milord/milady$.",
				"Hi.", 
				"Greetings, $milord/milady$."
			}
		}
		#Key "*how*you*" "*how*thou*" 
		{
			#Attitude Wicked, Belligerent 
			{
				"Horrible!",
				"None of thy business!", 
				"Get thee away from me!"
			}
			#Attitude Neutral 
			{
				"I'm doing relatively well.",
				"Just fine.", 
				"I am well."
			}
			#Attitude Goodhearted, Kindly 
			{
				"I am well, $milord/milady$. I hope thou art the same.",
				"Doing great!", 
				"As well as I can be, $milord/milady$."
			}
		}
		#Key "*where*thou*live*", "*where*you*live*",  "*thou*live*", "*you*live*", 
			"*what city are you from*", "*what town are you from*", "*where*you*from*",
			"*where*thou*from*"  
		{
			#Attitude Wicked, Belligerent 
			{
				"I live here!",
				"None of thy business!", 
				"I live in the bottom of the ocean! I only come here during the day!"
			}
			#Attitude Neutral 
			{
				"I live here in _Town_.",
				"I live in _Town_.",
				"Here.  In _Town_.",
				"Right here.  In _Town_.", 
				"I live in the town that thou art standing in."
			}
			#Attitude Goodhearted, Kindly 
			{
				"I live here in _Town_.",
				"Here, $milord/milady$.",
				"Right here.  In _Town_, $milord/milady$.", 
				"I live in the town that thou art standing in, $milord/milady$."
			}
		}
		#Key "*where*am*i*", "*what town am I in*", "*what*town is this*" 
		{
			"Thou hast stumbled into _Town_.",
			"_Town_.",
			"_Town_ is where thou dost find thyself."
		}
		#Key "*where*thou*work*" "*where*you*work*" 
		{
			#Attitude Wicked, Belligerent 
			{
				"I work here, moron!",
				"None of thy business!", 
				"I work out of a cave! What dost thou think, imbecile!"
			}
			#Attitude Neutral 
			{
				"I work here in _Town_.",
				"Here. In _Town_.", 
				"I work in the town that thou art in."
			}
			#Attitude Goodhearted, Kindly 
			{
				"I work here in _Town_.",
				"Here.  In _Town_, $milord/milady$.", 
				"I work in this town, $milord/milady$."
			}
		}
	        #Key "*bye*", "*fare*well*", "*chow*", "*ciao*", "*see*ya*", "*see*you*"  
		{
	        	"Goodbye.[Leave]",
			"Farewell.[Leave]",
			"Fare thee well.[Leave]"
	        }
            	#Key "*thanks*", "*thank you*", "*thank thee*", "*thank ye*", "*appreciate*" 
		{
                	"Thou'rt welcome.",
                	"'Twas nothing.",
                	"Certainly, thou art welcome.",
                	"Not a problem."
            	}
		#Key "*who are you*", "*what's your name*", "*what is your name*",     
			"*who art thou*", "*who are ye*", "*who*you*",
			"*what are you called*", "*what art thou called*",
	             	"*what are ye called*", "*know your name*", "*know thy name*",      
			"*know yer name*", "*what's thy name*",  "*what is thy name*" 
		{
			"My name is _MyName_.",
			"I am _MyName_, $milord/milady$.",
			"Thou mayest call me _MyName_."
 		}
		#Key "*Is*name*" "*What's thy name*" 
		{
			"My name is _MyName_.",
			"I am _MyName_.",
			"If thou art looking for _MyName_ then thou hast found me."
        	}
		#Key "*make*money*", "*earn*money*", "*get*money*"
		{
			#Attitude Wicked, Belligerent
			{
				"If thou dost want money, I'd expect thee to just take it. That's what thy kind usually does.",
				"Money can be found where thou dost look for it. If thou art too lazy to work for it, then accept what thou art given.",
				"Thou can always beg. Of course I wouldn't beg in the wilderness. Beggars have a strange habit of disappearing out there."
			}
			#Attitude Neutral
			{
				"There are many ways to earn thy keep, here in Britannia. I would suggest working for thy money. 
Thou can craft weapons, armor, clothes, furniture... many things to do. Find a shop with the tools and set to work. The raw materials should be there as well.",
				"If thou dost look in some shops, thou might find the raw materials and tools necessary to make the things thou dost need. Or thou could seel those things thou ARE able to make and collect the money.",
				"Most people in Britannia earn money honestly, by using their skills to make things and sell them. 
If thou dost look in some of the shops, there may be the necessary raw materials and tools to make thy own items.",
				"If thou art serious about earning money, then look to the shops. Inside thou can find the raw materials and tools necessary to make thy own items.",
				"Using an axe on some trees can yield wood that a carpenter may buy from thee. Also thou can use a pickaxe to mine for ore in rocks.",
				"Hunting seems to be a profitable living to many. The hides, meat, feathers, and
pelts that thou dost obtain can all be sold to various merchants."
			}
			#Attitude Goodhearted, Kindly
			{
				"I would tell thee to look to the shopkeepers for thy money. If thou art able to craft the things that they sell, then they would more than likely buy them from thee.",
				"The shopkeepers are usually nice enough to let thee use their leftover materials and tools. Try selling what thou dost make to others.",
				"If thou dost find a shop with some materials and tools, then work on making thy own
items. Then thou can sell them for money.",
				"Thou could hunt for meat to sell to butchers, rid us of some of the detestable monsters that roam so freely, or even collect wood from trees to sell to carpenters. 
Of course thou would need an axe for that."
				"If thou art interested in earning money, thou can collect raw materials to sell or use. Many shopkeepers let people use their tools, and even sometimes their left-overs to craft their own items.",
				"Hunting seems to do wonders for many bank accounts. Try selling the hides to the tanners and the meat to the butchers.",
				"If thou catches a bird, Thou can sell it to a cook, and a bowyer may appreciate the feathers."
			}
 
		}
		#Key "*camp*"
		{
			"When I think I may have to sleep out of doors, I make sure that I have my bedroll and some kindling with me.",
			"I try to not be without a bedroll and some kindling when I think I may have to camp in the wilderness.",
			"Make sure to keep kindling with thee if thou art thinking of camping. Sometimes 'tis a difficult thing to find. Of course, a bedroll is quite necessary also."
		}
		#Key "*how*quit*?*", "*log off*", "*logoff*"
		{
			"If thou art in need of a rest from this world, make thyself a camp, or check in to an Inn.",
			"Thou can either camp or check in to an Inn. Either way thou art likely to find the respite thou art looking for."
		}
		#Key "*bedroll*"
		{
			"A bedroll can be purchased from a provisioner's shop.",
			"If thou art looking for camping equipment, look to a provisioner.",
			"A provisioner should be able to supply thee with bedrolls." 
		}
		#Key "*kindling*"
		{
			"All thou dost need is an axe and some wood to make kindling.",
			"Any provisioner should sell som ekindling to adventurers like thee.",
			"Thou can easily make thy own kindling with an axe and some wood. Or thou can purchase some from a provisioner."
		}
		#Key "*dungeon*", "*cave*", "*Destard*", "*Despise*", "*covetous*", "*hythloth*", "*wrong*", "*shame*", "*deceit*"
		{
			"The dugeons and caves under Britannia are rumored to have treasures and excitements in abundance. Though I don't know many who have returned unscathed from them.",
			"If thou art able to fight thy way through, the dungeons beneath Britannia can make thee wealthy.",
			"I seem to recall the names of a few of the dungeons... Despise, Deceit, Shame... I don't know any more.",
			"Umm... I can recall two more of the dungeons, I think. Hythloth is one, and Wrong is the other.",
			"The dungeons that people tend to forget about are Covetous and Destard. At least I usually do."
		}
		#Key "*gold*", "*treasure*"
		{
			"Gold can be found on the bodies of the more dangerous monsters of Britannia.",
			"Treasure abounds in the dungeons beneath Britannia. The creatures themselves sometimes have things on them, also.",
			"Look to the dungeons or the monsters of Britannia if thou dost want gold and treasure."
		}
		#Key "*Britannia*" 
		{
			#Notoriety Infamous, Outlaw 
			{
				"Please $sir/lady$, I will speak not of my feelings of my beloved homeland.",
				"Please forgive me, $sir/lady$, but I will tell thee nothing of Britannia, for I fear what thou wilt do further.",
				"Hast thou not performed enough cruelty to Lord British's lands?"
			}
			#Notoriety Anonymous 
			{
				"Britannia? Truly the lands of Lord British.",
				"Good times or ill, I will always love my homeland,.",
				"'Tis called Britannia in honor of our liege, Lord British."
			}
			#Notoriety Famous, Known 
			{
				"Ah, $milord/milady$, surely thou dost know as much about the land as I. Why else wouldst thou do such good deeds for its people?",
				"What canst I tell one who has done so much? Nay, 'tis I who should be asking thee for advice.",
				"Why, all of Lord British's realm is called Britannia. But fear not, $good sir/fair lady$, with deeds such as thine, thou wilt surely be honored by Lord British, himself, soon enough."
			}
		}
		#Key "*Buccaneer's Den*" 
		{
			"Buccaneer's Den? Why, 'tis nothing more than a hiding place for pirates!",
			"'Tis where the scum of the realm go to lick their wounds.",
			"I would not live in Buccaneer's Den if I were thee, $milord/milady$, for it is home to the most vile men and women to sail the seas."

		}
		#Key "*Britain*", "*capital*" 
		{
			"Britain is the capital of all Britannia, land of Lord British.",
			"Lord British, sovereign of all Britannia, resides in Britain.",
			"Britain is home to Lord British, himself. 'Tis the capital of Britannia."
		}
		#Key "*Cove*" 
		{
			"Cove? Thou shouldst find it North and East of Britain.",
			"Cove is not much of a city. 'Tis more of a village, to be truthful.",
			"Just Northeast of Britain is Cove."
		}
		#Key "*Jhelom*" 
		{
			"Jhelom is an outpost for mercenaries and sellswords.",
			"Jhelom? 'Tis a town of sword arms looking for work. Thou canst find it off the southwestern coast of mainland Britannia.",
			"Some say Jhelom is as rough as Buccaneer's Den, with half the scoundrels and twice the bruises."
		}
		#Key "*Magincia*" 
		{
			"Quite a lovely place, $milord/milady$. And shouldst thou doubt my word, just ask someone who resides there.",
			"Magincia? 'Tis a city of arrogance and pride, I think.",
			"Magincia is a city, well, nearly an entire island, outside of Britanny Bay."
		}
		#Key "*Minoc*" 
		{
			"Minoc is a mining town now.",
			"Minoc? Why it was once home to the best artisans in the realm, but now that rich deposits of precious metals have been discovered in the mountains, 'tis full of miners.",
		      	"Minoc lies on the northern coast of the mainland, $milord/milady$."		
		}
		#Key "*Moonglow*" 
		{
			"Magic -- that's what one can find in Moonglow.",
			"Moonglow is nothing more than one large school for mages.",
			"If thou dost wish to study magic, thou shouldst visit the isle of  Moonglow."
		}
		#Key "*Serpent's Hold*" 
		{
			"Serpent's Hold is where the best knights are trained. 'Tis located in the Cape of Heroes, off the southeastern edge of the mainland.",
			"Serpent's Hold is the fortification Lord British granted to his loyal knights, those of the Order of the Silver Serpent.",
			"Serpent's Hold? Art thou interested in becoming a member of Lord British's royal order of Knights -- the Order of the Silver Serpent? If such is true, then thou shouldst go to Serpent's Hold."
		}
		#Key "*Skara Brae*" 
		{
			"Skara Brae is an island. Thou canst find it off the western coast of Britannia.",
			"The most skilled trackers in the land learn their craft on the island of Skara Brae.",
			"Many talented shipwrights live on Skara Brae, though the island is actually known for the many trackers who teach their skills there."
		}
		#Key "*Trinsic*" 
		{
			"Though Serpent's Hold may be home to valiant knights, and Jhelom home to skilled mercenaries, Trinsic is where one shouldst go to find the most honorable warriors of the realm.",
			"Though known as a haven for men and women of honor who wish to hone their martial skills, Trinsic also supports a large guild of architects and engineers.",					 
	       		"Trinsic? Why, thou couldst find thy way there by merely following the south road from Britain."
		}
		#Key "*Vesper*" 
		{
			"Vesper? 'Tis a city in northeastern Britannia. Ore from the mines of Minoc are sent down river to be unloaded in Vesper.",
			"Much of the crafts forged in Minoc find their way to Vesper by way of the river, $milord/milady$.",
			"Some claim Vesper is merely an extension of Minoc, calling it nothing more than a large marketplace for artisans to sell their wares."
		}
		#Key "*Yew*" 
		{
			"Yew is a small, peaceful community of farmers in northwestern Britannia.",
			"The High Court of Britannia is in Yew. 'Tis there that important cases that concern all of Britannia are decided, many of which are determined by Lord British, himself.",
			"They say farmers and criminals are all who go to Yew, $milord/milady$, save for those hoping to visit one of the two."
		}
		#Key "*Britanny Bay*" 
		{
			"'Tis the bay that touches the city of Britain.",
			"Britanny Bay is the body of water on the edge of Britannia's capital.",
			"Britanny Bay? Why, 'tis the waters that border the ports of Britain."
		}
		#Key "*Order of the Silver Serpent*", "*Knights*" 
		{
			"'Tis the order of knights who served Lord British in the battle against Lord Robere.",
			"When Lord Robere challenged Lord British for his share of the realm, Lord British's faithful knights defended the kingdom.",
			"Only the bravest knights belong to the Order of the Silver Serpent. 'Twas them who defeated the forces of Lord Robere."
		}
		#Key "*Robere*"," Lord Robere*" 
		{
		 	"History claims that Lord Robere was once an honorable man. But he was overcome with greed and sought to take the entire realm by force.",
			"Years ago, Lord Robere made claims to the lands under Lord British's domain. Were it not for the stout knights in the Order of the Silver Serpent, Lord Robere could very well have conquered the entire realm.", 
			"Though a kind and generous man in his youth, Lord Robere thirsted for power in his later years, so legends say. 
Had not the Order of the Silver Serpent been ready to fight for Lord British, this very land could have belonged to the ambitious conqueror."  
		}
		#Key "*Lord British*", "*ruler*", "* king*" 
		{
			#Attitude Neutral 
			{
				"Lord British has always a fair and just ruler over this land.",
				"I have no qualms about the way Lord British governs us.",
				"Lord Britsh has done nothing to hurt my work as a _Job_."
			}
	 		#Attitude Wicked , Belligerent 
			{
	    			"Aye, Lord British does the commanding in this realm. And we puppets are to jump when he so bids it.",
				"Dost thou wish for me merely to repeat what surely other sheep have said before me? Well and good then -- Lord British is the finest leige I have ever been privileged to grovel before.",
				"Now is not the time to talk about ruler, Good $Sir/Lady$." 					 
			}
	    		#Attitude Goodhearted, Kindly 
			{
				"A wiser, kinder ruler I have not heard of.",
				"I think Lord British makes for a fine ruler.",
				"We have a fine ruler in Lord British."
			}
		}
		#Key "*Weather*" 
		{
			"Ah, the weather... 'Tis an interesting thing, really. No matter what the season, no matter what enchantments are cast, our land is almost always blessed with clear and beautiful blue skies."
		}
		#Key "*concerns*", "*troubles*" 
		{
			"Various issues surface from time to time, such as taxation, invasion, protection form creatures of the wild.",
			"Any land experiences difficult times, but it takes a wise ruler to lead his people through them.",
			"Surely thou dost understand -- life cannot always be free of trouble."
		}
		#Key "*Shamino*" 
		{
			"I am sorry, $milord/milady$, I do not recognize the name.",
			"Shamino? I beg thee pardon, $milord/milady$, but the words mean nothing to me.",
			"Shamino? Nay, $kind sir/fair lady$, I cannot help thee."
		}
		#Key "*blackthorn*" 
		{
			"Blackthorn's written a book or two about himself. I haven't read them, myself, but I bet they say alot about the man.",
			"I hear that Blackthorn and Lord British still play chess together. Kinda odd, considering their differences, I think.",
			"Blackthorn believes in the freedom of choices. Even the WRONG choices that could lead to disaster."
		}
		#Key "*Iolo*" {
			"I am sorry, $milord/milady$, I do not recognize the name.",
			"Iolo? I beg thee pardon, $milord/milady$, but the words mean norhing to me.",
			"Iolo? Nay, $kind sir/fair lady$, I cannot help thee?"
		}
		#Key "*Dupre*" 
		{
			"I am sorry, $milord/milady$, I do not recognize the name.",
			"Dupre? I beg thee pardon, $milord/milady$, but the words mean norhing to me.",
			"Dupre? Nay, $kind sir/fair lady$, I cannot help thee?"
		}
		#Key "*other lands*", "*other realms*", "*many realms*", "*many lands*", "*other realm*", "*one realm of many?*"  
		{
			"My apologies, $milord/milady$, but I know of no other than Britannia.",
			"$Milord/Milady$, I do not understand. Realms other than Brittania? Surely thou dost make a jest.",
			"If thou'rt certain, $milord/milady$, I will not question thee. But I have heard of no lands other than our fair Britannia."  
		}
		#Key "*New Magincia*" 
		{
			"New Magincia? Is there something wrong with the original?",
			"Forgive me, $milord/milady$, but I think thou dost suffer the madness of the drink.",
			"I have not heard of such a place, $milord/milady$. I suppose it could be a colony of Britannians who have settled in a new territory."
		}
		#Key "*colony*" 
		{
			"I know not of any, $milord/milady$.",
			"Rumors abound that Lord British wishes to send adventurous and resourceful individuals to settle unexplored areas.",
			"I know not of which thou doth speak, $milord/milady$."
		}
		#Key "*Virtue*", "*virtues*", "*shrines*","*truth*", "*love*", 
			"*courage*", "*spirituality*", "*valor*", "*honor*", 
			"*justice*", "*sacrifice*", "*honesty*", "*humility*", "*compassion*" 
		{ 
			"Shrines to the virtues are spread around our land. 'Tis rumored that they will resurrect thee, if thou dost manage to get thyself killed.",
			"Rest and health can be found at the shrines.",
			"The power of resurrection is supposedly contained within the shrines. I know not whether 'tis true or no."
		}
		#Key "*avatar*" 
		{
			"Who?",
			"I'm sorry, I know not of whom thou doth speak.",
			"I have never heard of this tar person.",
			"I cannot help thee."
		}
		#Key  "*moongates*" 
		{
			"The moongates? They are doors that will lead thee around Britannia. The destinations of the moongates change in accordance to the phases of the two moons.", 
			"Thou shouldst learn to use the moongates if thou dost plan to travel far. Study the moons. The phases of the moons control the destinations of the moongates.", 
			"If thou dost use the moongates, then thou might not end up where thou had planned, unless thou hast learned how to use them correctly. The phases of the two moons are the key, thou knowest."
		}
		#Key "*mooons*" 
		{
			"Britannia's moons are called Trammel and Felucca. They control the destinations of the moongates."
		}
	}
	#Sophistication Low 
	{
		#Key "@InternalRefuseItem" 
		{
            		#Attitude Wicked 
			{
                		#Notoriety Infamous 
				{
                    			"No thanks!",
	      				"Thanks, but no.",
	      				"I don't want that!"
				}
                		#Notoriety Anonymous 
				{
                  	 		"I don't want it.",
	     				"I can't use that!",
	     				"Don't give me that!"
				}
				#Notoriety Famous 
				{
					"I really don't need thy cast-offs!",
	     				"Thy generosity is duly noted. Thanks, but no thanks.",
	     				"I don't need that."
				}
			}
			#Attitude Neutral 
			{
                		#Notoriety Infamous 
				{
                    			"Thank thee. Don't need any.",
	      				"I don't want it. Sorry.",
	      				"I don't have a use for it. Thanks, though."
                		}
                		#Notoriety Anonymous 
				{
                    			"Tis very generous of thee, but I don't need it.",
                    			"I can't take it. Sorry.",
	      				"Thank thee, but I don't want it." 
				}
                		#Notoriety Famous 
				{
                     			"Thou art most kind $good sir/good lady$, but it isn't anything that I need.",
	     				"Please, keep it. I don't want it.",
	     				"I really don't need it."
                		}
            		}
            		#Attitude Kindly 
			{
                		#Notoriety Infamous 
				{
                    			"Keep it! I have no need for it.",
                    			"'Tis a shame I have no use for it. Keep it.",
	      				"If only I needed it! I don't."
                		}
				#Notoriety Anonymous 
				{
                    			"'Tis kind of thee, but I have no use for it.",
                    			"Thank thee, but no.",
	      				"Thank thee, kind $sir/lady$, but no."
                		}
                		#Notoriety Famous 
				{
                    			"I would like to accept thy gift, but I cannot. Thank thee.",
                    			"Thank thee _Name_! But keep it. I have no use for it.",
	      				"'Tis greatly appreciated, but unnecessary. I don't want it."
				}
			}
		}
		#Key "@InternalNeedResponse" 
		{
            		#Attitude Wicked 
			{
                		#Notoriety Infamous 
				{
                    			"[GetNeed]? I need some!",
	      				"Thou said [GetNeed]! Get me some!",
	      				"I want some [GetNeed]!"
				}
                		#Notoriety Anonymous 
				{
                   			"'Twould be nice to get [GetNeed] for me.",
	     				"If thou dost have [GetNeed], I'd like some.",
	     				"Could I have some? [GetNeed], I mean."
				}
				#Notoriety Famous 
				{
					"If thou couldst stoop to get ME some[GetNeed], twould help!",
	     				"I could really use help getting [GetNeed].",
	     				"If thou could find some [GetNeed], 'twould be great. But only if thou dost have time."
				}
			}
			#Attitude Neutral 
			{
                		#Notoriety Infamous 
				{
                    			"I could use some of thy ill-gotten [GetNeed].",
	      				"If thou did... find some [GetNeed], I could use it.",
	      				"I could use some [GetNeed]. However thou might get it."
                		}
                		#Notoriety Anonymous 
				{
                    			"If thou art givien any [GetNeed], bring it here. I could use some.",
                    			"If thou dost have [GetNeed], I'd be willing to take some from thee.",
	      				"I need just a little [GetNeed]!" 
				}
                		#Notoriety Famous 
				{
                     			"If thou couldst get ME some[GetNeed], twould be great!",
	     				"I'd really appreciate it if thou got me [GetNeed]. I could use some.",
	     				"If thou couldst find [GetNeed] for me, 'twould be helpful!"
                		}
            		}
            		#Attitude Kindly 
			{
                		#Notoriety Infamous 
				{
                    			"Please, $sir/madam$, if thou'rt able, I could make use of [GetNeed].",
                    			"I'd be grateful to get some more [GetNeed].",
	      				"If thou couldst bring me some [GetNeed], I'd be in thy debt."
                		}
				#Notoriety Anonymous 
				{
                    			"I would love to have some [GetNeed]!",
                    			"'Twould be nice to get more [GetNeed]",
	      				"[GetNeed]? I could use some more."
                		}
                		#Notoriety Famous 
				{
                    			"Please, $good sir/good lady$, I could use some more [GetNeed].",
                    			"I could really do with some more [GetNeed].",
	     	 			"If thou dost find the time, more [GetNeed] would do wonders for me."
				}
			}
		}
		#Key "@InternalAcceptItem" 
		{
            		#Attitude Wicked 
			{
                		#Notoriety Infamous 
				{
                    			"Good! 'Tis what I need!",
	      				"Thanks.",
	      				"'Tis a welcome thing."
				}
                		#Notoriety Anonymous 
				{
                   			"I'll be glad to take it from thee. Thanks.",
	     				"I'll make good use of it.",
	     				"I've been needing it."
				}
				#Notoriety Famous 
				{
					"I'm happy that thou chose to do this.",
	     				"It will certainly help me. Thanks.",
	     				"I appreciate it."
				}
			}
			#Attitude Neutral 
			{
                		#Notoriety Infamous 
				{
                    			"Thank thee.",
	      				"I accept.",
	      				"I will take it, then."
                		}
                		#Notoriety Anonymous 
				{
                    			"Tis very generous of thee.",
                    			"I shall accept with pleasure.",
	      				"Thank thee!" 
				}
                		#Notoriety Famous 
				{
                     			"Thou art kind!",
	     				"I shall take it gladly! Thanks!",
	     				"Thank thee! And be safe!"
                		}
            		}
            		#Attitude Kindly 
			{
                		#Notoriety Infamous 
				{
                    			"Why, I thank thee!",
                    			"About time thou did something good.",
	      				"I knew that thy goodness would show itself!"
                		}
				#Notoriety Anonymous 
				{
                    			"'Tis gracious of thee!",
                    			"I accept! Thank thee.",
	      				"Thank thee, kind $sir/lady$!"
                		}
                		#Notoriety Famous 
				{
                    			"This from thee? Oh thank thee!",
                    			"Thank thee _Name_! Thank thee!",
	      				"'Tis very welcome!"
				}
			}
		}
		#Key "@InternalPersonalSpace" 
		{
            		#Attitude Wicked 
			{
                		#Notoriety Infamous 
				{
                    			"Here, get off me!",
	      				"Back off.",
	      				"Back away!",
	      				"Thou'rt too close!"
				}
                		#Notoriety Anonymous 
				{
                   			"Back away from me!",
	    				"Please, get away.",
	     				"Stand away.",
	     				"Get away. Thou'rt too close."
				}
				#Notoriety Famous 
				{
                   			"Back up a bit!",
	     				"Stand back from me.",
	     				"Step back, or thy fame won't protect thee.",
	     				"Get away from me, oh exalted one!"
				}
			}
			#Attitude Neutral 
			{
                		#Notoriety Infamous 
				{
                    			"Don't stand so near.",
	      				"Hey, step back.",
	      				"Wilt thou stand back a bit?",
	      				"I don' want no trouble, but step back!"
                		}
                		#Notoriety Anonymous 
				{
                    			"Don't stand so close.",
	      				"Could thou back off a bit?",
                    			"Thou'rt nearer to me than I like.",
	      				"Stand back.", 
      					"Please back away a bit.",
     	      				"I'd ask thee to stand away some."
				}
                		#Notoriety Famous 
				{
                    			"An it please thee, wilt thou step back a bit.",
	      				"Is it necessary to stand so near by?",
	      				"Forgive me for askin', but please step back.",
      					"Please back away a bit, $milord/milady$.",
      					"Please give me some space, $milord/milady$.",
	      				"$Sir/Madam$, thou stands too close."
               	 		}
            		}
            		#Attitude Kindly 
			{
                		#Notoriety Infamous 
				{
                    			"Please, stand off a ways?",
                   			"A bit more distance 'tween us would be better.",
	      				"'Scuse me, but step back.",
     	      				"Step back a bit."
                		}
				#Notoriety Anonymous 
				{
                    			"Friend, why're thou so close?",
                    			"Take a step back, if thou would.",
	      				"I'd like thee to step back.",
	      				"A bit more space 'tween us, please."
                		}
                		#Notoriety Famous 
				{
                    			"Forgive me, I'm too close to thee.",
                    			"An it please thee, wilt thou step back a bit.",
	      				"Pleas step back a pace or two.",
	      				"Thou'rt standin' too close to me."
				}
			}
		}
		#Key "@InternalScavenger" 
		{
            		#Attitude Wicked 
			{
               			"Mine!",
				"I'll take that!",
				"Ah ha!",
				"Come here, my pretty.",
				"At last!"
			}
			#Attitude Neutral 
			{
               			"Well, well, what have we here?",
				"Ah, this will be useful.",
				"Excellent. This is mine, now.",
				"This'll help.",
				"This'll come in handy.",
				"Huh! Look what I found!"
                	}
            		#Attitude Kindly 
			{
				"Why look at that!",
				"Ah. Good.",
				"This'll be of use.",
				"And what's this then?",
				"This is useful!"
			}
		}
		#Key "*hello*" "hi" "*greetings*" "*good*see*thee*" 
		{
			#Attitude Wicked, Belligerent 
			{
				"What dost thou want?",
				"Uh huh?", 
				"Yeah, what?"
			}
			#Attitude Neutral 
			{
				"Hello.",
				"Hi.", 
				"Greetings."
			}
			#Attitude Goodhearted, Kindly 
			{
				"Hello, $milord/milady$.",
				"Hi.", 
				"Greetings, $m'lord/m'lady$."
			}
		}
		#Key "*how*you*" "*how*thou*" 
		{
			#Attitude Wicked, Belligerent 
			{
				"Horrible!",
				"None of thy business!", 
				"Get away from me!"
			}
			#Attitude Neutral 
			{
				"I'm alright.",
				"Fine.", 
				"I'm good."
			}
			#Attitude Goodhearted, Kindly 
			{
				"I'm good, $m'lord/m'lady$.  I hope thou'rt alright too.",
				"Doin' great!", 
				"Pretty good, $m'lord/m'lady$."
			}
		}
		#Key "*where*thou*live*", "*where*you*live*",  "*thou*live*", "*you*live*", 
			"*what city are you from*", "*what town are you from*", "*where*you*from*",
			"*where*thou*from*"  
		{
			#Attitude Wicked, Belligerent 
			{
				"I live here!",
				"None of thy business!", 
				"I live in the ocean! I only come here durin' the day!"
			}
			#Attitude Neutral 
			{
				"I live here in _Town_.",
				"I live in _Town_.",
				"Here.  In _Town_.",
				"Right here.  In _Town_.", 
				"I live in the town that thou'rt standin' in."
			}
			#Attitude Goodhearted, Kindly 
			{
				"I live here in _Town_.",
				"Here, $milord/milady$.",
				"Right here.  In _Town_, $m'lord/m'lady$.", 
				"I live in the town that thou'rt standin' in, $m'lord/m'lady$."
			}
		}
		#Key "*where*am*i*", "*what town am I in*", "*what*town is this*"  
		{
			"_Town_.",
			"Uhhh... _Town_, I think.",
			"Thou'rt in the town of _Town_."
		}
		#Key "*where*thou*work*" "*where*you*work*" 
		{
			#Attitude Wicked, Belligerent 
			{
				"I work here, moron!",
				"None of thy business!", 
				"I work in a cave!"
			}
			#Attitude Neutral 
			{
				"I work here in _Town_.",
				"Here. In _Town_.", 
				"I work in the town that thou'rt in."
			}
			#Attitude Goodhearted, Kindly 
			{
				"I work here in _Town_.",
				"Here.  In _Town_, $m'lord/m'lady$.", 
				"I work in this town, $m'lord/m'lady$."
			}
		}
		#Key "*bye*", "*fare*well*", "*chow*", "*ciao*", "*see*ya*", "*see*you*"  
		{
			"Goodbye.[Leave]",
			"Farewell.[Leave]",
			"Fare thee well.[Leave]"
	        }
		#Key "*thanks*", "*thank you*", "*thank thee*", "*thank ye*", "*appreciate*"  
		{
                	"Thou'rt welcome.",
                	"'Twas nothin'.",
                	"Sure, Thee's welcome.",
                	"No problem."
            	}        
		#Key "*who are you*", "*what's your name*", "*what is your name*",     
			"*who art thou*", "*who are ye*", "*who*you*",
			"*what are you called*", "*what art thou called*",
	             	"*what are ye called*", "*know your name*", "*know thy name*",      
			"*know yer name*", "*what's thy name*",  "*what is thy name*" 
		{
			"_MyName_.",
			"I am _MyName_, $m'lord/m'lady$.",
			"Call me _MyName_."
 		}
		#Key "*Is*name*" "*What's thy name*" 
		{
			"I'm _MyName_.",
			"My name's _MyName_.",
			"I'm _MyName_."
        	}
	        #Key "name" 
		{
        		"My name, $m'lord/m'lady$?"
        	}
		#Key "*make*money*", "*get*money*", "*earn*money*"
		{
			#Attitude Wicked, Belligerent
			{
				"If it's money thou'rt wantin', find thy own source!",
				"I ain't tellin' thee nothin'. Thou would elbow in on my victim - er - customers.",
				"If thou'rt able, go huntin'. What thou hunts is thy own business, though." 
			}
			#Attitude Neutral
			{
				"I can tell thee that in order to make some money thou would do best to practice thy skills in a shop somewhere. Some even have the tools and materials thou would need on hand.",
				"If thou'rt willing to work, thou could use the tools and stuff at some of the local shops and make some things that thou could sell.",
				"Thou could go chop up some trees for wood. Carpenters are usually lookin' for wood.",
				"Either work an honest living and make some things or don't. Ain't no matter to me.",
				"Thou can always hunt. Meat, hides, and feathers an' such can all be sold to shokeepers."
				"There's ore to be mined, wood to be cut, armor to be made, and lotsa other things to do. Take thy pick."
			}
			#Attitude Goodhearted, Kindly
			{
				"Thou could practice thy skills at a shop, and then sell what thou art able to make. The shops should have tools and materials.",
				"If thou kills some monsters, thou can usually find gold. They tend to keep any that they find.",
				"Thou want money? Hunt for meat an' hides. Lotsa folks do it.",
				"Get an axe and chop a tree, if thou want money from a carpenter. Or sell some furniture."
			}
		}
		#Key "*camp*"
		{
			"Take some kindling with thee if thou'rt wantin' to camp. Oh, and a bedroll. Yeah, that too.",	
			"If thou gots a bedroll and some kindling, thou'rt set to camp.",
			"I just take a bedroll when I'm goin' out in the woods. Kindlin' thou can get from the trees, usually."

		}
		#Key "*how*quit*?*", "*log off*", "*logoff*"
		{
			"Find an Inn. Or make a camp. Either one will do thee just fine.",
			"Thou can make camp, or check in to an Inn."
		}
		#Key "*bedroll*"
		{
			"Thou'rt needin' a bedroll? Find a provisioner.",
			"Provisioners got bedrolls.",
			"Find a provisioner. Then thou can buy a bedroll."
		}
		#Key "*kindling*"
		{
			"Chop up some wood. That'll get thee some kindling.",
			"Uh... the provisioner's got some for sale, if thou wants to buy it.",
			"Thou wants kindling? Go chop some wood. Or buy some from the provisioner. I don't care."
		}
		#Key "*dungeon*", "*cave*", "*despise*", "*deceit*", "*covetous*", "*wrong*", "*shame*", "*hythloth*", "*destard*"
		{
			"Dungeons got treasure. That and lotsa pain.",
			"I don't know none of the names of the dungeons. An' I KNOW I couldn't find my way there!",
			"I'd go explore some of them places - caves and such - but I like my skin attached to my body.",
			"Them dungeons got monsters. Dangerous places. I hear they can make thee rich, though."
		}
		#Key "*gold*", "*treasure*"
		{
			"Find thy own gold! I ain't lettin' my secrets out!",
			"I ain't tellin' thee where I gets my gold! 'Sides, thou couldn't survive the dungeons without knowin' how.",
			"Tell thee what... go kill a deamon. They got lotsa good stuff on 'em."
		}
    		#Key "*Britannia*" 
		{
			#Notoriety Infamous, Outlaw 
			{
				#Attitude Wicked, Belligerent 
				{
					"Britannia? 'Tis the land thou'rt ruinin', blackguard!",
					"Surely thou knows the land thou'rt destroyin'!",
					"And what would thou like to know about Britannia? How thou can do more to make our lives worse?"  
				}
				#Attitude Kindly, Goodhearted 
				{
					"Please, $sir/lady$, don't spoil our land no further. I beg thee!",
					"I can't help but worry that any news I give will only aid thy plans for destruction.",
					"I would ask thee, $sir/m'lady$, to pillage our land no further."
				}
				#Attitude Neutral 
				{
					"Britannia is the very land thou'rt standin' on.",
					"Britannia? Why, 'tis all around thee.",
					"This very land is Britannia, as thou should know."
				}
			}
			#Notoriety Anonymous 
			{
				"Britannia?  Why, 'tis the name Lord British has claimed for the lands of 'imself and 'is subjects", 
				"This very land is Britannia, as thou must know.",
				"Britannia? Why, 'tis all around thee."
			}
			#Notoriety Famous, Known 
			{
				"Britannia is this land thou helps make better.",
				"Britannia? Why, 'tis the name that thanks thee daily for thy kindness.",
				"How could thou not know of the lands made more prosperous by thy deeds?"
			}
		}
		#Key "*Buccaneer's Den*" 
		{
			"Find thy own way about.",
			"If thou don't know, $m'lord/m'lady$, I can't help.",
			"I ain't good with directions, $m'lord/m'lady$."
		}
		#Key "*Britain*", "*capital*" 
		{
			"Find thy own way around.",
			"If thou don't know, $m'lord/m'lady$, I can't help.",
			"I ain't so good with directions, $m'lord/m'lady$."
		}
		#Key "*Cove*" 
		{
			"Find thy own way about.",
			"If thou don't know, $m'lord/m'lady$, I can't help.",
			"I ain't good with directions, $m'lord/m'lady$."
		}
		#Key "*Jhelom*" 
		{
			"Find thy own way about.",
			"If thou don't know, $m'lord/m'lady$, I can't help thee.",
			"I just ain't good with directions, $m'lord/m'lady$."
		}
		#Key "*Magincia*" 
		{
			"Find thy own way about.",
			"If thou don't know, $m'lord/m'lady$, I can't help thee.",
			"I ain't good with directions, $m'lord/m'lady$."
		}
		#Key "*Minoc*" 
		{
			"Find thy own way about.",
			"If thou don't know, $m'lord/m'lady$, I can't help thee.",
			"I ain't good with directions, $m'lord/m'lady$."
		}
		#Key "*Moonglow*" 
		{
			"Find thy own way about.",
			"If thou don't know, $m'lord/m'lady$, I can't help.",
			"I just ain't good with directions, $m'lord/m'lady$."
		}
		#Key "*Serpent's Hold*" 
		{
			"Find thy own way about.",
			"If thou don't know, I can't help thee much.",
			"I ain't so good with directions, $m'lord/m'lady$."
		}
		#Key "*Skara Brae*" 
		{
			"Find thy own way about.",
			"If thou don't know, $m'lord/m'lady$, I can't help.",
			"I ain't that good with directions, $m'lord/m'lady$."
		}
		#Key "*Trinsic*" 
		{
			"Find thy own way about.",
			"If thou ain't knowin', $m'lord/m'lady$, I cain't help.",
			"I'm not real good with directions, $m'lord/m'lady$."
		}
		#Key "*Vesper*" 
		{
			"Find thy own way about.",
			"If thou don't know, I ain't gonna help thee much.",
			"I'm not so good with directions."
		}
		#Key "*Yew*" 
		{
			"Find thy own way about.",
			"If thou'rt lost, I can't help thee.",
			"I ain't so good with directions, $milord/milady$."
		}
		#Key "*Lord British*", "*ruler*", "* king*" 
		{
			#Attitude Neutral 
			{
				"Aye, now thou'rt speakin' of our king.",
				"Lord British? I like 'im.",
				"They say Lord British is fair and just. I got no argument with that."
			}
    		     	#Attitude Wicked , Belligerent 
			{
    		     		#Notoriety Infamous, Outlaw 
				{
		     			"I'd like to see the bloody fool's head on a spit, I would.",
		     			"Dost thou want my opinion on Lord British? I find him a lout, I do. And I'd say it to him if I could.",
		    	       		"Thou wouldn't find me cryin' over 'is spilled blood."	 
		     		}
    		     		#Notoriety Anonymous 
				{
					"Lord British? I suppose the taxes he takes to fill his coffers leave us enough to live on... almost.",
					"I hope Lord British is pleased with all he's done for Britannia. I'll say I ain't.",
					"I don't know what makes him feel so lordly. 'Less, 'tis his gift for collectin' gold to fill his coffers."
				}
    				#Notoriety Famous, Known 
				{
					"Lord British? Best we don't speak of him, $m'lord/m'lady$.",
					"Thou wouldn't care for my opinion, $m'lord/m'lady$.",
					"Nah, $m'lord/m'lady$, I ain't tellin' my feelings 'bout our liege. Best to be quiet 'bout them things."
				}
			}
    			#Attitude Goodhearted, Kindly 
			{
				"Aye, Lord British works hard to make sure we're all happy.",
				"Lord British is our king. He's a good man, I say.",
				"Lord British? He's done right by Britannia, he has!"
			}
		}
		#Key "*Weather*" 
		{
			"Ah, the weather... 'Tis an interestin' thing, really. No matter what the season, no matter what enchantments are cast, our land is just about always blessed with clear and beautiful blue skies."
		}
		#Key "*concerns*", "*troubles*" 
		{
			"Lotsa things annoy people, like taxation, invasion, and protection form creatures of the wild.",
			"Any land sees hard times, an' it takes a wise ruler to lead his people through 'em.",
			"Surely thou ain't understandin' -- life can't always be free of trouble."
		}
		#Key "*Shamino*", "*Iolo*", "*Dupre*" 
		{
			"Who?",
			"Where?",
			"What?"
		}
		#Key "blackthorn*" 
		{
			"I think Blackthorn wrote a book or somethin'. I, uh, ain't had the time to read it though.",
			"Blackthorn an' British are still pals, I hear. Huh. Strange pair, that.",
			"I don't think either one of 'em's better than the other. Blackthorn an' British, I mean."
		}
		#Key "*other lands*", "*other realms*", "*many realms*", "*many lands*", "*other realm*", "*one realm of many?*"  {
			"What's thou mean? There's only one land, and it's called Britannia.",
			"What other?",
			"There ain't no other!"
		}
		#Key "*New Magincia*" 
		{
			"New Magincia? Is there something wrong with the old one?",
			"Forgive me, $m'lord/m'lady$, but I think thou'rt drunk.",
			"I ain't heard o' such a place, $m'lord/m'lady$. I s'pose it could be a colony or somethin'."
		}
		#Key "*colony*" 
		{
			"I ain't knowin' of none, $m'lord/m'lady$.",
			"Rumor is that Lord British wants to send idiots to settle unexplored areas.",
			"I don't know what thou'rt speakin' 'bout, $m'lord/m'lady$."
		}
		#Key "*Virtue*", "*virtues*", "*shrines*","*truth*", "*love*", 
			"*courage*", "*spirituality*", "*valor*", "*honor*", 
			"*justice*", "*sacrifice*", "*honesty*", "*humility*", "*compassion*" 
		{ 
				"Shrines to the virtues are all 'round our land. They're mighty powerful, I've heard.",
				"Rest and health is found at the shrines. I heard they'll bring thee alive if thou'rt dead or some such non-sense."
		}
		#Key "*avatar*" 
		{
			"Who?",
			"Sorry, Never heard of 'im.",
			"I ain't never heard of this tar person.",
			"I can't help thee."
		}
		#Key  "*moongates*" 
		{
			"The moongates?  What are they for?", 
			"They help travellers. I think.", 
			"Them things are beyond me, $m'lord/m'lady$."
		}
		#Key "*moons*"
		{
			"The moons are called Trammel and Felucca. Everyone knows that."
		}
	}
}
