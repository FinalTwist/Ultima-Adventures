LIARS DICE for Ultima Online
Copyright: Bobby Kramer 2011, http://www.panthar.net
Released under GPL V3. 

Includes disconnect protection, turn timers, and a monetary punishment to any player who leaves on his turn, 
or 1 player before his turn. This will make sense once you understand how the game works.

What this script does, is it allows the server admin (and maybe GM) to create an item called a "LiarsDice".

INSTALL:
Extract the zip file into /scripts/ inside your RunUO directory.

ONCE YOU HAVE LAIRS DICE INSTALLED/WORKING ON SERVER:

To add this item, log into your RunUO server, type [add LiarsDice into UO client.

Once you have created a LiarsDice Constructable item, that item and any copys of that object you make will all link  players to the same LIARS DICE GAME.

TO PLAY THE GAME, SIMPLY DOUBLE CLICK THE DICE TO JOIN THAT GAME. YOU MUST HAVE AT LEAST 2 PLAYERS TO PLAY.

Basically, if you look at LiarsDice.cs,  it will become quickly apperant how to customize the settings for the game.

IF you want MORE THAN ONE GAME with different SETTINGS on EACH game:

Copy LiarsDice.cs to a new file, rename the file/constructor/class to the new name of the Item, and set the settings for 
that game using LiarsDice.cs as an example. Now when you create/double click your new item, it will display a DIFFERENT Liars Dice Game window.

