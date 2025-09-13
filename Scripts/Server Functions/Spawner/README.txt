	Premium Spawner project was born as a "mod" of "Ultimate Spawner"
script created by a brazilian scripter called Atomic, who had a SHARD
called AtomicShard. It was based on a script that reads the spawns
information from a bunch of "maps" files and then place it in the
world. The original script was modified by Nerun (myself) and when the
number of changes to the original script became great, i changed the
name of my version. The Premium is the successor of the Ultimate Spawner
v4.0 R5 (until then, the simple addition of "R", followed by a number
differentiated my version from original script designed by Atomic).
The fundamental differences between the default RunUO Spawner system are:

 - "Premium" has new properties:
	1. SpawnID - Spawners IDentity: used to unload or save spawns
	2. OverrideMap - automatically change all map files from the
		spawners entries bellow it
	3. OverrideID - works as OverrideMap, but for SpawnID
	4. OverrideMinTime - works as OverrideMap, but for MinDelay
	5. OverrideMaxTime - works as OverrideMap, but for MaxDelay

  - "Premium" has it own engine, not using the default "Spawner"
that comes with RunUO, instead it uses the "PremiumSpawner". You can
use both systems simultaneously.

  - "Premium" has map files (pre-spawned world).

  - "Premium" is user friendly.

	The basics of basics that you need to know is that this system is
useful to remain "safe" (in a ".map" file) the spawners that you created
with so much effort. Well, suppose you have to erase everything and start
the world from scratch. You will have to place more than ten thousand
spawns, by hand, again? NO! You just use a command prompt, everything
will be generated again, and without effort. All effort is done in the
process of map file creation (that you can do in-game).
	The current release is in version 5.2.x, and is considered very
mature (in terms of stability, reliability, features and ease of use)
and complete (in terms of world spawns).


INDEX:
	1. PREMIUM SPAWNER INSTALATION
	2. PART I - Main Menu
	3. PART II - Writing a Map File (Basics)
	4. PART III - Using Maps "In-Game" (Básics)
	5. PART IV - Writing a Map File (Advanced)
	6. PART V - Using Maps "In-Game" (Advanced)
	7. PART VI - Edition Options


>>>>>>>>>>>>>>>>>>>>>>>>>>>
PREMIUM SPAWNER INSTALATION
<<<<<<<<<<<<<<<<<<<<<<<<<<<

	Spawner creation system, Premium "Spawner" consists in a collection
of scripts. As I added many scripts, I will not list them here. Today this
system is distributed in a package called "Nerun's Distro". There were
several packages in the beginning, but for convenience I have grouped in
a single distribution. This package also includes other resources, such as
spawns maps for use with this system, as gumps (menus) easy to use to further
facilitate the settlement of your world. This distro can be found in the
RunUO forum at http://www.runuo.com/.

To install the distro:

1) Unrar "Distro SVN x.rar".

2) You will se two folders inside: "Data" and "Scripts". Plus some files
   (including this tutiorial, changes history, benchmark tests, SpawnsID of
    all maps etc). Read the files for more usefull information if you want.

3) Cut the folders "Data" and "Scripts".

4) Go to "c:\RunUO 2.1\" (or where you install it) and paste it there. Windows
   Explorer will say that those folders already exists, and will ask if you
   want to overwrite, click "yes to all".


>>>>>>>>>>>>>>>>>>
PART I - Main Menu
<<<<<<<<<<<<<<<<<<

	To access the Main Menu wrote "[spawner" (without quotes) in the
command prompt. There are a lot of options in the menu, in two pages. These
options are self-explained:


GUMP NAME                                    COMMAND PROMPT
===========================================================
WORLD CREATION OPTIONS:
	Create World Gump ------------------ [createworld
SPAWN OPTIONS:
	Spawn Trammel/Felucca -------------- [spawntrammel or [spawnfelucca
	Spawn Ilshenar --------------------- [spawnilshenar
	Spawn Malas ------------------------ [spawnmalas
	Spawn Tokuno ----------------------- [spawntokuno
	Spawn Ter Mur ---------------------- [spawntermur
UNLOAD SPAWNS
	Unload Trammel/Felucca spawns ------ [unloadtrammel or [unloadfelucca
	Unload Ilshenar spawns ------------- [unloadilshenar
	Unload Malas spawns ---------------- [unloadmalas
	Unload Tokuno spawns --------------- [unloadtokuno
	Unload Ter Mur Spawns -------------- [unloadtermur
SAVE OPTIONS:
	Save All spawns (spawns.map) ------- [spawngen save
	Save 'By Hand' spawns (byhand.map) - [spawngen savebyhand
	Save spawns inside region ---------- [spawngen save RegionName
	Save spawns by coordinates --------- [spawngen save x1 y1 x2 y2
REMOVE OPTIONS:
	Remove All spawners (all facets) --- [spawngen remove
	Remove All spawners (current map) -- [spawnrem
	Remove spawners by ID -------------- [spawngen unload SpawnID
	Remove spawners by Coordinates ----- [spawngen remove x1 y1 x2 y2
	Remove spawners inside Region ------ [spawngen remove RegionName
EDITION OPTIONS:
	Spawn Editor ----------------------- [editor
	Clear All Facets ------------------- [clearall
	Set my own body to GM Style -------- [gmbody
CONVERSION UTILITY:
	RunUO Spawns to PremiumSpawner ----- [rse


	As you can see, it centered on a single menu all the system commands,
you do not know how to write each line to use them, simply click, follow
the instructions and it is done. The following sections will describe how to
create a map file, and how to use the command line instead of the Menu.


>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
PART II - Writing a Map File (Basics)
<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

	Notepad can be used to create map files. You will see the following
basic information on a map:

## Britain Graveyard:
*|Spectre:Wraith|Skeleton|Zombie||||1369|1475|10|2|5|10|30|20|1|2|3|4|0|0|0

	This map above provides information from all spawns of the Britain
Graveyard. Let's analyze it:

 - 1st Line: Starts with "##", this double "sharp" marks the beginning of
   a comment. In other words, what comes after him will not be read by the
   script. It is usually used to provide information about the script:
   Dungeon map name, actual review and so on.

 - 2nd Line: The spawner itself. Each line is a spawner, but the advantage
   of PremiumSpawner is that it contains up to 6 FakeSpawners within
   themselves, which are nothing more than spawners with the same
   attributes of distance, time etc, but with different creatures and
   amounts:

	*|Spectre:Wraith|Skeleton|Zombie||||1369|1475|10|2|5|10|30|20|1|2|3|
	4|0|0|0

-  All Spawners starts with "*" followed by "|". This "|" separates the
   information into the line. The first 6 spaces are the names of the
   creatures, which in this case are:

	Spawner 1: Spectre OR Wraith (the ":" serves to add several
                 creatures in a random list)

	Spawner 2: Skeleton

	Spawner 3: Zombie

	Spawner 4: Empty (none)

	Spawner 5: Empty (none)

	Spawner 6: Empty (none)

   Each of them are called "Fake Spawner": are 6 spawners inside only
   one spawner.

-  The three numbers that come after the creatures lists define the place
   where the spawner will be created. Following the "XYZ" format (all
   details of Spawners are separated by a "|"). In this case, the spawner
   will appear at coordinates "1369 | 1475 | 10", in other words,
   X = 1369, Y = 1475 and Z = 10. If you type "[go 1369 1475 10" you will
   go to the place where this Spawner will appear.


-  The fourth number says in wich facet the Spawner will be placed. Note
   that this number is 2. The definition of the maps follow this pattern:

	0 = Felucca AND Trammel

	1 = Felucca

	2 = Trammel

	3 = Ilshenar

	4 = Malas

	5 = Tokuno

   So deduct that the spawner will be placed in Trammel, because the room
   number (the map) is the number 2.

-  Next 2 numbers after facet number: defines respectively the minimum
   and the maximum respawn time. That is, the creatures that spawn will
   respawn in a randomly chosen interval between the minimum and maximum
   time. In the example We have "5 | 10 |" (always a "|"). Time is in
   minutes, so creatures will respawn between 5 and 10 min after being
   killed by players.

-  Next 2 numbers after respawn time: defines WalkingRange and HomeRange.
   In this case, 30 WalkingRange and 20 HomeRange. The creatures will walk
   for up to 30 "boxes" (those who we see in a maximum zoom in game)
   away from the spawner. But they will "respawn" randomly within a
   radius of up to 20 "boxes" from the spawner. Note that the HomeRange
   is always less than or equal to WalkingRange, NEVER MORE.

-  Next number after Ranges: identifies the spawn, is a "SpawnID", it
   tell us to which "spawn group" it belongs. By default, it is always
   1. If you create any spawner in game using the "[add premiumspawner"
   command, the SpawnID will be the number 1. This identifies the
   spawners created "by hand". But the maps can have any number of
   SpawnID. It is advisable that all spawners of the same map, have the
   same SpawnID. We will see why bellow.

-  The last 6 numbers, also important, say how many monsters defined at
   the beginning of the spawner (the first 6 spaces) will be generated
   by that spawner. If the numbers are "2 | 3 | 4 | 0 | 0 | 0" will be
   generated 2 Specters OR Wraiths (or 1 of each), 3 skeletons and 4
   Zombies. The latest figures are 0: nothing is created in the past
   3 spawners, even if you define a value, nothing could be generated,
   because no creature was listed there (as we saw).

	As observation, note that most of spawner's properties, as
described above, can be defined without the need to "see" where the
spawner will appear, but the coordinates will need to "see". Because if
you choose coordinates randomly, risks creating a spawner in an
inaccessible place, for example, in the middle of the ocean! So you must
go to the place where you would like spawner appear and use the command
"[get location" or "[where" in that place. Then write down the details
that will appear on the screen.
	Made the map, just save it in the folder Data / Monsters (if there
isn't a folder Date/Monsters, it's time to create one. Click "Save As",
select the Save as Type "All Files" and then type a name for the map, not
forgetting to set ".map" at the end of the name. In the above example,
if we had made that map, we could give him the name "graveyard.map".

SUMMARIZING
	Default spawner format:

*|List1|List2|List3|List4|List5|List6|X|Y|Z|facet|MinTime|MaxTime|
WalkingRange|HomeRange|SpawnID|Count1|Count2|Count3|Count4|Count5|Count6



>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
PART III - Using Maps "In-Game" (Básics)
<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

	This is simple, are just a commands list to be used in game for
the PremiumSpawner's engine generates spawners from the maps created. On
the assumption that you already installed the required scripts in RunUO
folder, you can use the following commands:


 - [spawngen MapName.map
	Read maps and create spawners. In the example you should use
	"[spawngen graveyard.map" (no quotes).

 - [spawngen remove
	Dungerous command! It will DELETE all PremiumSpawners of ALL facets
	of UO, done "by hand" or "by map"!

 - [spawngen save
	Usefull command: saves in a file called "Spawns.map" ALL the
	PremiumSpawners in ALL UO facets, done "by hand" or "by map"!
	Usefull if tou did a lot of custom maps and use this distro too.
	After this, a simple "[spawngen spawns.map" will spawn everything
	again.

 - [spawnrem
	It will DELETE all PremiumSpawners of actual facet (that one
	where your character is standing). Other facets will remain
	spawned.



>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
PART IV - Writing a Map File (Advanced)
<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


Multiple Creatures and Randomness
---------------------------------

	You have learned to create maps using the class method, in other
words, you type the name of the creature and then the statistics (count,
delay, range etc). But what if you want more than one type of creature in
the same spawner?

	Use the method of two dots (":"). In this case, simply separate
the creatures that you want with a ":" as in the example below:

*|Spectre:Wraith||||||1369|1475|10|2|5|10|30|20|1|10|0|0|0|0|0

	As a result, the spawner randomly selecs within the amount
indicated, among the creatures on the list, separated by two dots.
Remembering that you can put as many creatures you like, all separated by
":". In the example, we could have 7 Wraith and 3 Specter, or 5 of each,
this number will vary, but the tendency is to remain in the ratio of
count / creatures. In the example: count = 10, creatures = 2. Ratio 5.
So the spawns will tend to 5 Wraith and 5 Specter. Now let's play with
statistics. And if we want to have a greater chance of appearing more
Spectres than Wraiths? So we can write in this way:

*|Spectre:Spectre:Wraith||||||1369|1475|10|2|5|10|30|20|1|10|0|0|0|0|0

	The ratio now is 3.3 for creature, so the chances will be now:
66.6% Spectres and 33.3% Wraiths.
	As an advise, never place inside the same Spawner (FakeSpawner),
"target" creatures with "non-target" creatures or items. What i want to
say is: don't place a Spectre, a Wraith and a dog or a bottle to spawn
together:

*|Spectre:Wraith:Dog||||||1369|1475|10|2|5|10|30|20|1|10|0|0|0|0|0

	If you do it, sometime the players will kill all monsters, but
will not kill the dogs, and all the 10 creatures of that spawner will
be just dogs! It happens because the Spawner spawn just the remaning
amount (amount not spawned). If it spawn 10 creatures: 5 Spectres,
3 Wraiths and 2 Dog, and if players kill all monsters except the dogs,
next time the spawner will spawn 8 creatures (cause Dogs are alive) and
it can spawn a few Spectres and Wraiths and a lot of Dogs again! And
so on... I dit it in past releases, already fixed.


Override Maps
-------------

	And if you want to do a map that works both in Trammel and Felucca?
Instead of edit the facet number in each line (spawner) of your map,
you can superscribe the facet number with a simple, only, command line.
In our example above, that generates spawns only in Trammel, to generates
spawns in Trammel AND Felucca, we adds an "overridemap":

overridemap 0
## Britain Graveyard:
*|Spectre:Wraith|Skeleton|Zombie||||1369|1475|10|2|5|10|30|20|1|2|3|4|0|0|0

	Here, the number "2" of that spawner (the facet), will be read as "0"
by the spawn generator engine. Facet numbers are the same as described in
"PART II - Writing a Map File (Basics)".


Override SpawnID
----------------

	Do the same as OverrideMap, but for SpawnIDs. SpawnID "1" will be read
as "14", as bellow:

overrideid 14
overridemap 0
## Britain Graveyard:
*|Spectre:Wraith|Skeleton|Zombie||||1369|1475|10|2|5|10|30|20|1|2|3|4|0|0|0


Override DelayTime
------------------

	Do the same as OverrideMap and OverrideID, but for delay time.

	overridemintime

	and or

	overridemaxtime

Exemple:

overridemintime 10
overridemaxtime 20
overrideid 14
overridemap 0
## Britain Graveyard:
*|Spectre:Wraith|Skeleton|Zombie||||1369|1475|10|2|5|10|30|20|1|2|3|4|0|0|0

	The delays (min 5 and max 10 minutes) will be read as 10 and 20
minutes.



>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
PART V - Using Maps "In-Game" (Advanced)
<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


Settlement In-Game
------------------

	Another way to settlement your world (place spawns) is to go "in-game"
and adds spawners by hand, with command [add premiumspawner CreatureName.
And "set" the attributes (x = number):

[set count x homerange x spawnrange x maxdelay x mindelay x


Commands to Save and to Remove Spawns
-------------------------------------

	After spawn the desired area, you need to save yours spawns either with
[spawngen save or with some advanced options. Just type [spawner and look under
SAVE/REMOVE OPTIONS. There is a GUMP for each command. But you can use the
command prompt instead:

[spawngen savebyhand
	To save spawns done "by hand" ([add premiumspawner... all by hand premium
	spawners has SpawnID = 1). Spawns will be saved in "byhand.map" file in
	Data/Monsters folder.

[spawngen save x1 y1 x2 y2
	To save all premium spawners in a spawns.map file. All premium spawners
	inside the rectangle area (x1 y1 x2 y2) will be saved.

	X and Y are coordinates:

	(x1,y1)------+      All premium spawners between coordinates (x1,y1)
	  |          |      and coordinates (x2,y2) will be saved.
	  |          |
	  |          |
	  +---------(x2,y2)

[spawngen save <region>
	Save premium spawners inside a region defined by RunUO to a spawns.map
	file, in Data/Monsters.
	Complete list of regions are in c:/RunUO/Data/Regions.xml.
	Use [where to see the region where you are.
	Open Regions.xml to understand regions:

	<region priority="50" name="Minoc">
		<rect x="2411" y="366" width="135" height="241" />
		<rect x="2548" y="495" width="72" height="55" />
		<rect x="2564" y="585" width="3" height="42" />
		<rect x="2567" y="585" width="61" height="61" />
		<rect x="2499" y="627" width="68" height="63" />
		<inn x="2457" y="397" width="40" height="8" />
		<inn x="2465" y="405" width="8" height="8" />
		<inn x="2481" y="405" width="8" height="8" />
		<go location="(2466,544,0)" />
		<music name="Minoc" />
	</region>

	As you see, a Region is a lot of rectangles.

NOTE: "[spawngen remove" can be used with the same options as for
"[spawngen save" above, but will remove spawns instead of save.

NOTE: Go to Data/Monsters and rename spawns.map or byhand.map to another name,
because each time you save with those options, the old file will be deleted and
a new one will be saved over it.

EXAMPLE:
 "[spawngen save minoc" (save spawns inside Minoc region)
 We can rename the spawns.map to Minoc.map.


Unloading Maps (recommended)
----------------------------

	The better way to remove spawns instead of using "[spawngen remove"
and other Spartan options is the "Unload" method:

[spawngen unload SpawnID

	If you define a SpawnID to each custom map you has you can unload
or remove the entire map easier. Example: Graveyards.map saw above. All the
premium spawners inside that map has SpawnID = 1. Lets see:

## Britain Graveyard:
*|Spectre:Wraith|Skeleton|Zombie||||1369|1475|10|2|5|10|30|20|1|2|3|4|0|0|0

	The problem is that 1 is the default number to "by hand" maps.
Lets change the ID of this map. In the example above we just need to change
one number (that between numbers 20 and 2). But if the map had 100, maybe
1000 premium spawners? hard work uh? Because of it there is the "overrideid"
option. It set all the SpawnIDs bellow it to the desired ID. So lets do it:

overrideid 14
## Britain Graveyard:
*|Spectre:Wraith|Skeleton|Zombie||||1369|1475|10|2|5|10|30|20|1|2|3|4|0|0|0

	Although each spawner still have 1 as ID on each line, the
"overrideid 14" will force the spawn generator engine to read that "1" as
if "14". Later in the game, if I want to remove this map is just type
"[spawngen unload 14" and ready. None of my other spawns will be changed
or removed.


>>>>>>>>>>>>>>>>>>>>>>>>>
PART VI - Edition Options
<<<<<<<<<<<<<<<<<<<<<<<<<

[editor
	Opens the spawn editor of course. This will list all the
	PremiumSpawners in the left side. In the right column you can see
	a bunch of options to select only the desired spawners, go to it,
	see it properties, and see it creatures.

[clearall
	Works as [Clearfacet but for ALL facets. Caution here.

[GMbody
	Will set some common attributes to GMs. Target yourself. A lot of
	items, skills, stats, robes, full spellbooks etc, will appear in
	your backpack. You always set your body to human body. You add
	titles to [GM], [Admin], etc.