// Britannian Bard fragment
//
// Notes: This whole fragment will likely need rewritten once we actually
// have something for the bards to talk about. For now, it's very very
// blah.
//
// Keywords:
// music(ian), minstrel, troubad(or/our)
// bardic conservatory
// lore, history, rumo(u)r, knowledge, information, story, background,
//      tales
//
// -Raph Koster
//

//!!Nuke easter eggs!

#Fragment Britannia, Job, Britannia_Bard 
{
	#Sophistication High 
	{
		#Key "*job*", "*what*do*do*", "*profession*", "*occupation*"  
		{
			"I am a bard, $milord/milady$. A minstrel. A thoughtful troubadour.",
			"I do claim to play music and tell tales, $milord/milady$.",
			"I am a teller of tales. A player of song. In short, I am a bard."
                }
		#Key "*hint*", "*news*", "*rumor*", "*rumour*", "* info*", "*magic*", "*artifact*", "*interesting*", "*cool*", "*to do*" 
		{
			"[getHint]",
               		"Let me think... [getHint]",
			"Just a moment, allow me to think... [getHint]",
			"[getHint] I know not if that is the sort of thing thou art seeking.",
			"[getHint] Inns andf taverns seem to be good sources of news, however."
		}
		#KEY "*relvinian*"  
		{
			"I've heard Relvinian's maze contains naught but mages now.",
			"Rumor has it that Relvinian's maze contains the power to heal those wounded in battle.",
			"It is said that somewhere in Relvinian's maze is the power to summon daemons, but I would not bet my life on it."
		}
		#Key "*empath*", "*abbey*", "*where*monks*"  
		{
                        "Ah! Thou art speaking of Empath Abbey, $milord/milady$. I believe that it can be found just outside of Yew.",
                        "To find the Abbey, head northeast from Yew.",
                        "The monks of Empath Abbey are to be found near Yew, if my memory serves me."
                }
                #Key "*music*", "*minstrel*", "*troubad*", "*play*", "*song*"  
		{
                        "I am indeed a bard, but I fear that for now I cannot play for thee.",
                        "What, thou dost desire music? Well, thou shalt have to wait.",
                        "I'm sorry, but I don't have the time to play for thee now. Maybe another time."
                }
		#Key "*tale*" 
		{
			"Ah the tales I could tell. Or, more importantly for some, the news I gather... interesting rumors I hear sometimes.",
                        "Art thou intersested in tales? Or possibly hearing some bit of gossip from other parts? I can often tell thee things that may be of interest to thee.",
                        "I sometimes know news that only a barkeep, or maybe an innkeeper would have heard."
                }
                #Key "*conservatory*", "*bard*" 
		{
                        "The Bardic Conservatory of Britain is a haven for all of a musical bent, and 'tis the ideal place for storytellers and gatherers of lore.",
                        "Our grand Conservatory in Britain is a place of training, of retreat, a place joyful with music and tales.",
                        "I am a bard, and I must say that the best place for bards in all of Britannia must be the Conservatory - 'tis a place for lovers of the arts."
                }
        }
        #Sophistication Medium 
	{
                #Key "*job*", "*what*do*do*", "*profession*", "*occupation*"  
		{
			"I am a bard, $milord/milady$. A minstrel. A thoughtful troubadour.",
			"I do claim to play music and tell tales, $milord/milady$.",
			"I am a teller of tales. A player of song. In short, I am a bard."
                }
		#Key "*hint*", "*news*", "*rumor*", "*rumour*", "* info*", "*magic*", "*artifact*", "*interesting*", "*cool*", "*to do*" 
		{
			"[getHint]",
			"Let me think... [getHint]",
			"Hmm. [getHint]",
			"[getHint] If thou dost stay at the inn, thou mayst hear more.",
			"[getHint] That wast what thou were inquiring after?"
                }
		#Key "*empath*", "*abbey*", "*where*monks*"  
		{
                        "Ah! Thou art speaking of Empath Abbey, $milord/milady$. I believe that it can be found just outside of Yew.",
                        "Head northeast from Yew to find Empath Abbey.",
                        "The monks of Empath Abbey are to be found near Yew, if memory serves."
                }
                #Key "*music*", "*minstrel*", "*troubad*", "*play*",  "*song*" 
		{
                        "I am indeed a bard, but I fear that for now I cannot play for thee.",
                        "What, thou dost desire music? Well, thou shalt have to wait.",
                        "'Tis not within my power to play music for thee yet, e'en though my profession is that of minstrel."
                }
		#KEY "*relvinian*"  
		{
			"I've heard Relvinian's maze contains naught but mages now.",
			"Rumor has it that Relvinian's maze contains the power to heal those wounded in battle.",
			"It is said that somewhere in Relvinian's maze is the power to summon daemons, but I would not bet my life on it."
		}
		#Key "*tale*" 
		{
                        "Ah the tales I could tell. Or, more importantly for some, the news I gather... interesting rumors I hear sometimes.",
                        "Art thou intersested in tales? Or possibly hearing some bit of gossip from other parts? I can often tell thee things that may be of interest to thee.",
                        "I sometimes know news that only a barkeep, or maybe an innkeeper would have heard."
                }
                #Key "*conservatory*", "*bard*" 
		{
                        "The Bardic Conservatory of Britain is a haven for all of a musical bent, and 'tis the ideal place for storytellers and gatherers of lore.",
                        "Our grand Conservatory in Britain is a place of training, of retreat, and a place joyful with music and tales.",
                        "I am a bard, and I must say that the best place for bards in all of Britannia must be the Conservatory - 'tis a wondrous place for lovers of the arts."
                }
        }
        #Sophistication Low 
	{
                #Key "*job*", "*what*do*do*", "*profession*", "*occupation*"  
		{
                        "I'm a bard, $m'lord/m'lady$.",
                        "I play music and tell some tales.",
                        "I'm a bard."
                }
		#Key "*hint*", "*news*", "*rumor*", "*rumour*", "* info*", "*magic*", "*artifact*", "*interesting*", "*cool*", "*to do*" 
		{
			"[getHint]",
			"Umm... [getHint]",
			"Lemme think. [getHint]",
        		"[getHint] Inns an' taverns are good for rumors, though."       
		}
		#Key "*empath*", "*abbey*", "*where*monks*"  
		{
                        "I've heard that the Abbey is up north, somewhere. But I ain't never been, myself.",
                        "I think the Abbey is up north somewhere.",
                        "The monks that make the good wine are north of here. I think."
                }
		#KEY "*relvinian*"  
		{
			"I heard Relvinian's maze contains nothin' but mages now.",
			"Rumor has it that Relvinian's maze has the power to heal people wounded in battle.",
			"It's said that somewhere in Relvinian's maze is the power to call daemons, but I wouldn't bet my life on it."
		}
                #Key "*music*", "*minstrel*", "*troubad*", "*play*",  "*song*"   
		{
                        "I'm a bard, or at least I wisht to be, but my fingers fumble bad.",
                        "Hey! Can I play thee a nursery rhyme? Goes like this, it does ... umm, LA LA LA LA. Ahem. 'Twas once a young lass from Jhelomward ... no, that ain't it ...",
                        "I's just in trainin', and ain't yet a real bard, I s'pose."
                }
		#Key "*tale*" 
		{
                        "Ah the news I hear. Or, more interestin' for some, the rumors I gather... useful rumors I hear sometimes.",
                        "Intersested in tales? Or hearin' some gossip from other parts? I might tell thee sometimes what I heard.  And then, maybe I won't.",
                        "I heard some rumors in my day, and I ain't even a tavernkeeper."
                }
                #Key "*conservatory*", "*bard*" 
		{
                        "I's thinkin' that the Conservatory's the biggest building in Britain, 'cept for the castle!",
                        "There's always music at the Conservatory in Britain.",
                        "All us bards like the Conservatory."
                }
        }
}

// End of fragment
