// Baker function
//
// Keywords:
// job, baker, bread, bake, dough, flour, pie, cake, muffin, pastr*, honey, egg, milk
//
//
// 
// - cwm

#Fragment Britannia, Job, Britannia_Baker 
{
        #Sophistication High 
	{
                #KEY "*job*", "*what*do*do*", "*occupation*", "*profession*" 
		{
                        #Attitude Wicked 
			{
                                "I'm a baker.",
                                "I bake bread and pastries. Dost thou want to buy some?"
                        }
                        #Attitude Neutral 
			{
                                "I'm a baker. May I help thee?",
                                "This is my bakery."
                        }
                        #Attitude Goodhearted 
			{
                                "I am the baker. How may I serve thee?",
                                "I'm a baker of delicious fresh loaves and wonderful pies!"
                        }
                }
                #KEY "*baker*", "*bake*" 
		{
                        #Attitude Wicked 
			{
                                "If thou art looking for bread, this is the place.",
                                "I'm the baker. What dost thou want?"
                        }
                        #Attitude Neutral 
			{
                                "I bake bread and fine pastries.",
                                "Yes, I'm the baker. How may I help thee?"
                        }
                        #Attitude Goodhearted 
			{
                                "I'm a baker and my father was a baker before me.",
                                "Baking! Ah, how I love it!"
                        }
                }
                #KEY "*bread*", "*loaves*", "*loaf*"  
		{
                        #Attitude Wicked 
			{
                                "Dost thou plan to buy some bread?",
                                "I can sell thee bread."
                        }
                        #Attitude Neutral 
			{
                                "I just baked some nice, fresh bread.",
                                "I'd be happy to sell thee some bread.",
                                "There's more to making a good loaf of bread than just heating up dough."
                        }
                        #Attitude Goodhearted 
			{		
                                "My bread is fresh and piping hot!",
                                "Wilt thou honor me by buying one of my humble loaves?",
                                "Flour to dough, dough to bread ... that's the magic of what I do."
                        }
                }
                #KEY "*pie*" 
		{
                        #Attitude Wicked 
			{
                                "Dost thou want to buy a pie?",
                                "I have meat pies, fruit pies and vegetable pies."
                        }
                        #Attitude Neutral 
			{
                                "Wouldst thou like a lovely fresh-baked pie?",
                                "I have meat pies, fruit pies and vegetable pies.",
                                "A good pie needs dough, fillings and love."
                        }
                        #Attitude Goodhearted 
			{
                                "I pride myself on the quality of my pies.",
                                "I have meat pies, fruit pies and vegetable pies.",
                                "Good dough and fresh fillings are the keys to a great pie."
                        }
                }
                #KEY "*dough*" 
		{
                        #Attitude Wicked 
			{
                                "I make my own dough.",
                                "Not much dough to be made in these parts."
                        }
                        #Attitude Neutral 
			{
                                "I make all my dough by hand.",
                                "It takes a certain touch to make good dough. It's not just flour and water."
                        }
                        #Attitude Goodhearted 
			{
                                "Only the finest flour and purest water go into my dough."
                                "A baker is only as good as his dough."
                        }
                }
                #KEY "*flour*", "*honey*", "*egg*", "*milk*" 
		{
                        #Attitude Wicked 
			{
                                "I could use a bit more flour, honey, eggs or milk."
                        }
                        #Attitude Neutral 
			{
                                "I could use a new supplier for flour, honey, eggs or milk."
                        }
                        #Attitude Goodhearted 
			{
                                "I'm always looking for a source of good flour, honey, eggs or milk."
                        }
                }
                #KEY "*cake*" 
		{
                        #Attitude Wicked 
			{
                                "I have cakes for sale."
                        }
                        #Attitude Neutral 
			{
                                "I have fine, freshly baked cakes."
                        }
                        #Attitude Goodhearted 
			{
                                "I have lovely sweet cakes for sale."
                        }
                }
                #KEY "*muffin*" 
		{
                        #Attitude Wicked 
			{
                                "I have muffins for sale."
                        }
                        #Attitude Neutral 
			{
                                "I have warm, fresh muffins for sale."
                        }
                        #Attitude Goodhearted 
			{
                                "My hot muffins are famous!"
                        }
                }
                #KEY "*pastr*" 
		{
                        #Attitude Wicked 
			{
                                "I sell bread, pies, cakes and muffins."
                        }
                        #Attitude Neutral 
			{
                                "I sell bread, pies, cakes and muffins."
                        }
                        #Attitude Goodhearted 
			{
                                "I sell only the finest bread, pies, cakes and muffins."
                        }
                }
        }
        #Sophistication Medium 
	{
                #KEY "*job*", "*what*do*do*", "*occupation*", "*profession*" 
		{
                        #Attitude Wicked 
			{
                                "I'm a baker.",
                                "I bake bread and pastries. Dost thou want to buy some?"
                        }
                        #Attitude Neutral 
			{
                                "I'm a baker. May I help thee?",
                                "This is my bakery."
                        }
                        #Attitude Goodhearted 
			{
                                "I am the baker. How may I serve thee?",
                                "I'm a baker of delicious fresh loaves and wonderful pies!"
                        }
                }
                #KEY "*baker*", "*bake*" 
		{
                        #Attitude Wicked 
			{
                                "If thou art looking for bread, this is the place.",
                                "I'm the baker. What dost thou want?"
                        }
                        #Attitude Neutral 
			{
                                "I bake bread and fine pastries.",
                                "Yes, I'm the baker. How might I help thee?"
                        }
                        #Attitude Goodhearted 
			{
                                "I'm a baker and my father was a baker before me.",
                                "Baking! Ah, how I love it!"
                        }
                }
                #KEY "*bread*", "*loaves*", "*loaf*"  
		{
                        #Attitude Wicked 
			{
                                "Dost thou plan to buy some bread?",
                                "I can sell thee bread."
                        }
                        #Attitude Neutral 
			{
                                "I just baked some nice, fresh bread.",
                                "I'd be happy to sell thee some bread.",
                                "There's more to making a good loaf of bread than just heating up dough."
                        }
                        #Attitude Goodhearted 
			{		
                                "My bread is fresh and piping hot!",
                                "Wilt thou honor me by buying one of my humble loaves?",
                                "Flour to dough, dough to bread ... that's the magic of what I do."
                        }
                }
                #KEY "*pie*" 
		{
                        #Attitude Wicked 
			{
                                "Dost thou want to buy a pie?",
                                "I have meat pies, fruit pies and vegetable pies."
                        }
                        #Attitude Neutral 
			{
                                "Wouldst thou like a lovely fresh-baked pie?",
                                "I have meat pies, fruit pies and vegetable pies.",
                                "A good pie needs dough, fillings and love."
                        }
                        #Attitude Goodhearted 
			{
                                "I pride myself on the quality of my pies.",
                                "I have meat pies, fruit pies and vegetable pies.",
                                "Good dough and fresh fillings are the keys to a great pie."
                        }
                }
                #KEY "*dough*" 
		{
                        #Attitude Wicked 
			{
                                "I make my own dough.",
                                "Not much dough to be made in these parts."
                        }
                        #Attitude Neutral 
			{
                                "I make all my dough by hand.",
                                "It takes a certain touch to make good dough. It's not just flour and water."
                        }
                        #Attitude Goodhearted 
			{
                                "Only the finest flour and purest water go into my dough."
                                "A baker is only as good as his dough."
                        }
                }
                #KEY "*flour*", "*honey*", "*egg*", "*milk*" 
		{
                        #Attitude Wicked 
			{
                                "I could use a bit more flour, honey, eggs or milk."
                        }
                        #Attitude Neutral 
			{
                                "I could use a new supplier for flour, honey, eggs or milk."
                        }
                        #Attitude Goodhearted 
			{
                                "I'm always looking for a source of good flour, honey, eggs or milk."
                        }
                }
                #KEY "*cake*" 
		{
                        #Attitude Wicked 
			{
                                "I have cakes for sale."
                        }
                        #Attitude Neutral 
			{
                                "I have fine, fresh-baked cakes."
                        }
                        #Attitude Goodhearted 
			{
                                "I have lovely sweet cakes for sale."
                        }
                }
                #KEY "*muffin*" 
		{
                        #Attitude Wicked 
			{
                                "I have muffins for sale."
                        }
                        #Attitude Neutral 
			{
                                "I have warm, fresh muffins for sale."
                        }
                        #Attitude Goodhearted 
			{
                                "My hot muffins are famous!"
                        }
                }
                #KEY "*pastr*" 
		{
                        #Attitude Wicked 
			{
                                "I sell bread, pies, cakes and muffins."
                        }
                        #Attitude Neutral 
			{
                                "I sell bread, pies, cakes and muffins."
                        }
                        #Attitude Goodhearted 
			{
                                "I sell only the finest bread, pies, cakes and muffins."
                        }
                }
	}
	#Sophistication Low 
	{
                #KEY "*job*", "*what*do*do*", "*occupation*", "*profession*" 
		{
                        #Attitude Wicked 
			{
                                "I'm a baker.",
                                "I bake bread and pastries. Want to buy some?"
                        }
                        #Attitude Neutral 
			{
                                "I'm a baker. Can I help?",
                                "This is my bakery."
                        }
                        #Attitude Goodhearted 
			{
                                "I'm the baker. What may I get thee?",
                                "I'm a baker of fresh bread and pies!"
                        }
                }
                #KEY "*baker*", "*bake*" 
		{
                        #Attitude Wicked 
			{
                                "If thou'rt lookin' for bread, this is the place.",
                                "I'm the baker. What's thou want?"
                        }
                        #Attitude Neutral 
			{
                                "I bake bread and things.",
                                "Yes, I'm the baker. How can I help thee?"
                        }
                        #Attitude Goodhearted 
			{
                                "I'm a baker and my father was a baker.",
                                "Baking! Ah, I love it!"
                        }
                }
                #KEY "*bread*", "*loaves*", "*loaf*"   
		{
                        #Attitude Wicked 
			{
                                "Plan to buy some bread? I'll sell some.",
                                "I can sell thee bread."
                        }
                        #Attitude Neutral 
			{
                                "I just baked some nice, fresh bread.",
                                "I'd be happy to sell thee some bread.",
                                "There's more to making a good loaf of bread than just heatin' up dough."
                        }
                        #Attitude Goodhearted 
			{		
                                "My bread is fresh and hot!",
                                "Will thou buy one of my loaves?",
                                "Flour to dough, dough to bread ... that's the magic of it."
                        }
                }
                #KEY "*pie*" 
		{
                        #Attitude Wicked 
			{
                                "Dost thou want to buy a pie?",
                                "I got meat pies, fruit pies and vegetable pies."
                        }
                        #Attitude Neutral 
			{
                                "Would thou like a lovely fresh-baked pie?",
                                "I got meat pies, fruit pies and vegetable pies.",
                                "A good pie needs dough, fillings and love."
                        }
                        #Attitude Goodhearted 
			{
                                "I got good quality pies.",
                                "I got meat pies, fruit pies and vegetable pies.",
                                "Good dough and fresh fillings will make a great pie."
                        }
                }
                #KEY "*dough*" 
		{
                        #Attitude Wicked 
			{
                                "I make my own dough.",
                                "Not much dough to be made in these parts."
                        }
                        #Attitude Neutral 
			{
                                "I make all my dough.",
                                "It takes a certain touch to make good dough. It's not just flour and water."
                        }
                        #Attitude Goodhearted 
			{
                                "Only the best flour and water go into my dough.",
                                "A baker is only as good as his dough."
                        }
                }
                #KEY "*flour*", "*honey*", "*egg*", "*milk*" 
		{
                        #Attitude Wicked 
			{
                                "I could use a bit more flour, honey, eggs or milk."
                        }
                        #Attitude Neutral 
			{
                                "I could use a new supplier for flour, honey, eggs or milk."
                        }
                        #Attitude Goodhearted 
			{
                                "I'm always lookin' for a source of good flour, honey, eggs or milk."
                        }
                }
                #KEY "*cake*" 
		{
                        #Attitude Wicked 
			{
                                "I got cakes for sale."
                        }
                        #Attitude Neutral 
			{
                                "I got fine, fresh-baked cakes."
                        }
                        #Attitude Goodhearted 
			{
                                "I got lovely sweet cakes for sale."
                        }
                }
                #KEY "*muffin*" 
		{
                        #Attitude Wicked 
			{
                                "I got muffins for sale."
                        }
                        #Attitude Neutral 
			{
                                "I got warm, fresh muffins for sale."
                        }
                        #Attitude Goodhearted 
			{
                                "My muffins are famous!"
                        }
                }
                #KEY "*pastr*" 
		{
                        #Attitude Wicked 
			{
                                "I sell bread, pies, cakes and muffins."
                        }
                        #Attitude Neutral 
			{
                                "I sell bread, pies, cakes and muffins."
                        }
                        #Attitude Goodhearted 
			{
                                "I sell only the best bread, pies, cakes and muffins."
                        }
                }
        }
}

