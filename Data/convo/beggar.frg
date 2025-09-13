// Beggar function
//
// Keywords:
//gold, money, donate, beggar, job
//
// help
// 
// - Dab


#Fragment Britannia, Job, Britannia_Beggar 
{
        #Sophistication High 
	{
                #KEY "*job*", "*what*do*do*", "*occupation*", "*profession*"  
		{
                        #Attitude Wicked 
			{
                                "I, um ... don't do well with employment."
                        }
                        #Attitude Neutral 
			{
                                "I'm between jobs at the moment. But if thou couldst find it in thine heart to help out a soul in need, I'm sure that my luck will change!"
                        }
                        #Attitude Goodhearted 
			{
                                "I  haven't found suitable work, yet. So I am forced to ask complete strangers, like thyself, $milord/milady$, for assistance in order to get a day's meal! 'Tis horrible! Wouldst thou help? Just a few coins?"
                        }
                }
		#Key "*hint*", "*news*", "*rumor*", "*rumour*", "* info*", "*magic*", "*artifact*", "*interesting*", "*cool*", "*to do*" 
		{
			"[getHint]",
               		"Give me just a moment to think... [getHint]",
			"Hold it, let me think... [getHint]",
			"[getHint] I know not if that is the sort of thing thou seekest.",
			"[getHint] Inns and taverns seem to be good sources of news, however."
		}
                #KEY "*Gold*", "*coin*"  
		{
                        #Attitude Wicked 
			{
                                "'Twould be nice to have some gold of mine own!"
                        }
                        #Attitude Neutral 
			{
                                "Gold? Gold? Didst thou say gold? Couldst thou give gold?!",
                                "Wouldst thou please give?"
                        }
                        #Attitude Goodhearted 
			{
                                "Ahh! That color! That marvelous yellow! Please allow me to partake of that which thou dost seem to have in abundance, $milord/milady$.",
                                "I would deeply appreciate some assistance, if thou art able."
                        }
                }
                #KEY "*beggar*" 
		{
                        #Attitude Wicked 
			{
                                "I just do what I can to get by. Wouldst thou like to help?"
                        }
                        #Attitude Neutral 
			{
                                "I don't beg! I simply ask. There's a difference."
                        }
                        #Attitude Goodhearted 
			{
                                "Beg, convince, cajole ... it's all in the way thou dost look at it."
                        }
                }
                #KEY "*donate*", "*money*", "*help*" 
		{
                        #Attitude Wicked 
			{
                                "Wouldst thou please give - to help me live?"
                        }
                        #Attitude Neutral 
			{
                                "Just a little money would help get me back on my feet."
                        }
                        #Attitude Goodhearted 
			{
                                "Please, $good Sir/good Lady$, have a heart and donate some needed help."
                        }
                }
        }
        #Sophistication Medium 
	{
                #KEY "*job*", "*what*do*do*", "*occupation*", "*profession*"  
		{
                        #Attitude Wicked 
			{
                                "I, um ... don't do well with employment."
                        }
                        #Attitude Neutral 
			{
                                "I'm between jobs at the moment. But if thou couldst find it in thine heart to help out a soul in need, I'm sure that my luck will change!"
                        }
                        #Attitude Goodhearted 
			{
                                "I haven't found suitable work, yet. So I am forced to ask complete strangers, like thyself, $milord/milady$, for assistance in order to get a day's meal! 'Tis horrible! Wouldst thou help? Just a few coins?"
                        }
                }
		#Key "*hint*", "*news*", "*rumor*", "*rumour*", "* info*", "*magic*", "*artifact*", "*interesting*", "*cool*", "*to do*" 
		{
			"[getHint]",
			"Let me think... [getHint]",
			"Hmm. [getHint]",
			"[getHint] If thou dost stay around the inn, thou mayst hear more.",
			"[getHint] 'Twas that thou wast inquiring after?"
                }
                #KEY "*Gold*", "*coin*"  
		{
                        #Attitude Wicked 
			{
                                "'Twould be nice to have some gold of my own!"
                        }
                        #Attitude Neutral 
			{
                                "Gold? Gold? Didst thou say gold? Couldst thou give gold?!",
                                "Wouldst thou please give?"
                        }
                        #Attitude Goodhearted 
			{
                                "Ahh! That color! That marvelous yellow! Please allow me to partake of that which thou dost seem to have in abundance, $milord/milady$.",
                                "I would deeply appreciate some assistance, if thou art able."
                        }
                }
                #KEY "*beggar*" 
		{
                        #Attitude Wicked 
			{
                                "I just do what I can to get by. Wouldst thou like to help?"
                        }
                        #Attitude Neutral 
			{
                                "I don't beg! I simply ask. There's a difference."
                        }
                        #Attitude Goodhearted 
			{
                                "Beg, convince, cajole ... it's all in the way thou dost look at it."
                        }
                }
                #KEY "*donate*", "*money*", "*help*" 
		{
                        #Attitude Wicked 
			{
                                "Wouldst thou please give - to help me live?"
                        }
                        #Attitude Neutral 
			{
                                "Just a little money would help get me back on my feet."
                        }
                        #Attitude Goodhearted 
			{
                                "Please, $good Sir/good Lady$, have a heart and donate some needed help."
                        }
                }
        }
        #Sophistication Low 
	{
                #KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" 
		{
                        #Attitude Wicked 
			{
                                "I, um ... don't do well with employment."
                        }
                        #Attitude Neutral 
			{
                                "I'm between jobs at the moment. But if thou couldst find it in thine heart to help out a soul in need, I'm sure that my luck will change!"
                        }
                        #Attitude Goodhearted 
			{
                                "I  haven't found suitable work, yet. So I am forced to ask complete strangers, like thyself, $milord/milady$, for assistance in order to get a day's meal! 'Tis horrible! Wouldst thou help? Just a few coins?"
                        }
                }
		#Key "*hint*", "*news*", "*rumor*", "*rumour*", "* info*", "*magic*", "*artifact*", "*interesting*", "*cool*", "*to do*" 
		{
			"[getHint]",
			"Umm... [getHint]",
			"Lemme think. [getHint]",
   			"[getHint] Inns are good for rumors, though."       
		}
                #KEY "*Gold*", "*coin*"  
		{
                        #Attitude Wicked 
			{
                                "'Twould be nice to have some gold of mine own!"
                        }
                        #Attitude Neutral 
			{
                                "Gold? Gold? Didst thou say gold? Couldst thou give gold?!",
                                "Wouldst thou please give?"
                        }
                        #Attitude Goodhearted 
			{
                                "Ahh! That color! That marvelous yellow! Please allow me to partake of that which thou dost seem to have in abundance, $milord/milady$.",
                                "I would deeply appreciate some assistance, if thou art able."
                        }
                }
                #KEY "*beggar*" 
		{
                        #Attitude Wicked 
			{
                                "I just do what I can to get by. Wouldst thou like to help?"
                        }
                        #Attitude Neutral 
			{
                                "I don't beg! I simply ask. There's a difference."
                        }
                        #Attitude Goodhearted 
			{
                                "Beg, convince, cajole ... it's all in the way thou dost look at it."
                        }
                }
                #KEY "*donate*", "*money*", "*help*" 
		{
                        #Attitude Wicked 
			{
                                "Wouldst thou please give - to help me live?"
                        }
                        #Attitude Neutral 
			{
                                "Just a little money would help get me back on my feet."
                        }
                        #Attitude Goodhearted 
			{
                                "Please, $good Sir/good Lady$, have a heart and donate some needed help."
                        }
                }
        }
}

