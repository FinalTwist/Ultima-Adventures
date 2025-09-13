//Britannia Actor Fragment
//Notes: This is used for general information pertaining to all Britannian actors
//Keywords:
// job, actor, what*do, stage, theatre, theater, costume, sissy, wuss, priss, real job
// Note conflict between keywords "job" and "real job"
//
//Nobles, Lord British, 
//Revision Date: 9/4/96
//

#Fragment Britannia, Job, Britannia_Actor 
{
	#Sophistication High 
	{
                #Key "*job*", "*ac*tor*", "*what*do*do*", "*ac-tore*" 
		{
                        "I am an Ac-tore!",
                        "I have performed, to the best of my humble abilities, for Lord British and other Nobles of this fine land.",
                        "The stage, my ignoble friend, is my - as thou dost say - job.",
                        "I, $milord/milady$ am no mere actor. I am an ... ac-tore!"
                }
                #Key "*stage*", "*perform*" 
		{
                        "To paraphrase: All our land is a stage, acted upon by men and women who shall, in their lives, play many parts. I just happen to be one of the best of them.",
                        "To live is to act. And the world is our stage. And thou art the janitor, my friend.",
                        "The stage is that which I yearn for."
                }
		#Key "*Noble*", "*Lord British*", "* art*" 
		{
                        "The nobility of this land supports the fine arts. They seem to appreciate our humble efforts.",
                        "Lord British is well-known as one of the greatest supporters of the arts.",
                        "The arts prospers in Britannia, thanks to the generosity of Lord British and the great nobles of this land."
                }
                #Key "*theatre*", "*theater*" 
		{
                        "The theatre IS my life!",
                        "Ah! That grande analogy of life! The theatre!"
                }
                #Key "*costume*" 
		{
                        "What thou dost wear ... is not that thy costume? So is all that I doth put on, mine.",
                        "I doth wear clothes, $milord/milady$. Even that which I wear on the stage, is but clothing.",
                        "Clothes do make the man, do they not?"
                } 
		#Key "*clothe*"
		{
			"Buy thy clothes from a tailor, $milord/milady$.",
			"A tailor can supply thee with clothes."
		}
                #Key "*sissy*" "*wuss*" "*priss*" "*real job*" 
		{
                        "I shall brook no discourtesy from the likes of a bottom-dweller like thyself![Leave]",
                        "Art thou mocking me? Well thy ignorant words fall on deaf ears.",
                        "Thou art not of the intelligence to understand what it is that we suffer so diligently to accomplish, $milord/milady$. Compassion is not a virtue of thine!"
                }
        }
        #Sophistication Medium 
	{
                #Key "*job*", "*actor*", "*what*do*do*", "*ac-tore*" 
		{
                        "I am an Ac-tore!",
                        "I have performed, to the best of my humble abilities, for Lord British and other Nobles of this fine land.",
                        "The stage, my ignoble friend, is my - as thou dost say - job.",
			"I, $milord/milady$ am no mere actor. I am an ... ac-tore!"
                }
                #Key "*stage*", "*perform*"
		{
                        "To paraphrase: All our land is a stage, acted upon by men and women who shall, in their lives, play many parts. I just happen to be one of the best of them.",
                        "To live is to act. And the world is our stage. And thou art the janitor, my friend.",
                        "The stage is that which I yearn for."
                }
		#Key "*Noble*", "*Lord British*", "* art*" 
		{
                        "The nobility of this land supports the fine arts. They seem to appreciate our humble efforts.",
                        "Lord British is well-known as one of the greatest supporters of the arts.",
                        "The arts prospers in Britannia, thanks to the generosity of Lord British and the great nobles of this land."
                }
                #Key "*theatre*", "*theater*" 
		{
                        "The theatre IS my life!",
                        "Ah! That grand analogy of life! The theatre!"
                }
                #Key "*costume*" 
		{
                        "What thou dost wear ... is not that thy costume? So is all that I doth put on, mine.",
                        "I doth wear clothes, $milord/milady$. Even that which I wear on the stage, is but clothing.",
                        "Clothes do make the man, do they not?"
                } 
		#Key "*clothe*"
		{
			"A good tailor can sell thee clothes.",
			"If thou art needing clothes, look to a tailor."
		}
                #Key "*sissy*" "*wuss*" "*priss*" "*real job*" 
		{
                        "I shall brook no discourtesy from the likes of a bottom-dweller like thyself![Leave]", 
                        "Art thou mocking me?", 
                        "Thou art not of the intelligence to understand what it is that we suffer so diligently to accomplish, $milord/milady$. Compassion is not a virtue of thine!"
                }
        }
        #Sophistication Low 
	{
                #Key "*job*", "*actor*", "*what*do*do*", "*ac-tore*" 
		{
                        "I am an Ac-tore!",
                        "I have performed, to the best of my humble abilities, for Lord British and other Nobles of this fine land.",
                        "The stage, my ignoble friend, is my - as thou dost say - job.",
			"I, $milord/milady$ am no mere actor. I am an ... ac-tore!"
                }
                #Key "*stage*", "*perform*" 
		{
                        "To paraphrase: All our land is a stage, acted upon by men and women who shall, in their lives, play many parts. I just happen to be one of the best of them.",
                        "To live is to act. And the world is our stage. And thou art the janitor, my friend.",
                        "The stage is that which I yearn for."
                }
		#Key "*Noble*", "*Lord British*", "*art*"
		{
                        "The nobility of this land are our main supporters, I'm told.",
                        "Lord British enjoys the theater.  He has been known to help support many performers.",
                        "We don't tend to go hungry in Britannia, thanks to Lord British and some of the nobles of this land."
                }
                #Key "*theatre*", "*theater*" 
		{
                        "The theatre IS my life!",
                        "Ah! That grand analogy of life! The theatre!"
                }
                #Key "*costume*" 
		{
                        "What thou dost wear ... is not that thy costume? So is all that I doth put on, mine.",
                        "I doth wear clothes, $milord/milady$. Even that which I wear on the stage, is but clothing.",
                        "Clothes do make the man, do they not?"
                }
		#Key "*clothe*"
		{
			"A tailor will sell thee clothes.",
			"Find a tailor and thou'lt find clothes."
		}
                #Key "*sissy*" "*wuss*" "*priss*" "*real job*" 
		{
                        "I shall brook no discourtesy from the likes of a bottom-dweller like thyself![Leave]", 
                        "Art thou mocking me?",
                        "Thou art not of the intelligence to understand what it is that we suffer so diligently to accomplish, $milord/milady$. Compassion is not a virtue of thine!"
                }
        }
}
