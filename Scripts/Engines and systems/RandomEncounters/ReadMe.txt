Random Encounter Engine V 2.0RC1
=======================

COPYRIGHT JOE KRASKA aka 'Courageous', November, 2005

A limited use license to this material is contained in the
attached file COPYING.TXT.

DESCRIPTION
===========

This is a D&D style "random encounter" engine for RunUO.

Note that encounters do not have to be hostile. For example,
this is a good way to make your world "come alive". You can
set it up so that wilderness creatures frequently appear near
the player when the player is in the wilderness. Since these
creatures are only spawned at or near where an active player
can see them, no CPU is wasted. It also adds an element of
unpredictability to the shard.

The system is fully configurable, by facet, by region type,
and by named regions (if desired). This allows the administrator
to control what kind of encounters appear where.

The system runs off a configuration file; the system rescans
this file periodically, if it notices that the file has been
written. So your shard does not have to be bounced for you to
make configuration changes.

INSTALLATION
============

Extract the zip file into your Scripts/Misc/Custom directory. Nothing else
is required. Note that the configuration file "RandomEncounters.xml" is
expected to reside in $RUNUO_INSTALL_DIR/Scripts/Misc/Custom/RandomEncounters.
If you want to put it somewhere else or name it something else, find the variable
"m_EncountersFile" in the RandomEncounterEngine class and fiddle with it.

HOW TO
======

The Random Encounter Engine is entirely controlled by its configuration file,
so understanding its format is essential. Before we talk aboutthat, though, a few
preliminaries:

The system is controlled on a common clock that periodically checks to see
if players (player mobiles with access level == Player, not staff members!)
qualify for a random encounter. To see if they qualify, first the system
decides which picker is in use. There are two.

The first picker is the "sqrt" (square root) picker. Its the default. When
the sqrt picker is in use, the system finds the square root of the number of
players online. The system then picks this many players to see if they further
qualify for a random encounter. No more than one encounter per player.

The second picker is the "all" picker. Using the all picker, the system
considers all players online for a random encounter.

Note switching between pickers is a dramatic event, which will GREATLY
impact the number of random encounters your player see; you should decide
early which picker you're going to use.

A player qualifies for a random encounter when:

1. The facet name, region type, and region name the player is in has
encounters described for it (example, the player is on Facet the
facet named "Felucca", in a "Dungeon" region named "Covetous").

2. Failing #1 above, the the system finds a region named "default" that
otherwise qualfies by facet name and region type (example, the
player is in a Felucca Dungeon, and is in Covetous, but the administrator
did not define a named region for Covetous. If there is a default
region for Felucca Dungeons, this will be used instead).

3. The system draws a random number 0.0-1.0, and searches for the LOWEST
probability encounter it can match.

If all of the above fail, the player has no encounter.

Now on to the details. Consider the following example:

[CODE]
<!--mycomment-->
<RandomEncounters picker="sqrt" delay="15.0" interval="15.0" cleanup="300.0" debug="true">
	<!--mycomment-->
	<Facet name="Felucca">
		<Region type="Wilderness" name="default">
			<Encounter p="1.0" distance="7">
				<Mobile pick="OrcCaptain,OrcishLord">
					<Mobile n="1:3" pick="Orc"/>
					<Mobile p=".25" pick="OrcishMage"/>
				</Mobile>
				<Item pick="WoodenTreasureChest">
					<Item pick="OrcHelm"/>
					<Item pick="ChainChest,BattleAxe"/>
				</Item>
			</Encounter>
			<Encounter p=".05" distance="7">
				<Mobile pick="EvilMage"/>
				<Mobile n="2:3" pick="Brigand"/>
			</Encounter>
		</Region>
	</Facet>
</RandomEncounters>
[/CODE]


This is a standard nested tag formatted xml file. It supports the following tags:

"RandomEncounters" tag. This supports the following attributes and defaults:

picker = the picking method, defaults to "sqrt"
delay = amount of secs before encounters begin after server start, defaults to "60"
interval = frequency in secs encounters are checked, defaults to "1800"
cleanup = how long to wait before shutting down spawned mobiles, defaults to "300"
debug ="false" (print out extra debugging information)

"Facet" tag.

name =[None] (mandatory tag naming the facet)

"Region" tag.

type =[None] (mandatory tag specifying region type; can be "Guarded", "Dungeon", and "Wilderness")
name =[None] (mandatory tag naming the region; use "default" to pick up generic)

"Encounter" tag.

p = probability of encounter, default of "1.0", which means 100%
distance = the preferred distance from the player for the encounter, supports a range (e.g, "3:7"), or a constant distance (e.g.,"8")
water = water mobiles can't spawn without this; land mobiles can't spawn with it, default is "false"

"Mobile" tag.

p = probability of the mobile being included in a picked encounter, default is "1.0"
pick =[None] (a list of mobiles to pick from (NO SPACES!); one is picked randomly)
n = a number or a range (example "1" or "0:2" or "1:3"), default is "1"

"Item Tag"

Same as Mobile tag.

Note how Mobiles and Items can appear embedded in one another. To know the rules, use
some common sense. An Item can be in a Mobile (e.g., "orc has a Sword"), and an Item can
be in an Item (e.g., "sword in a chest"), but a Mobile cannot be in an Item! A Mobile,
however, might belong to another Mobile's team...

Now a little word on picking. Consider the following (modified) fragment:

[CODE]
<Mobile n="2" pick="OrcCaptain,OrcishLord">
	<Mobile n="1:3" pick="Orc"/>
	<Mobile p=".25" pick="OrcishMage"/>
</Mobile>
[/CODE]

The above says "pick 2, each randomly from OrcCaptain and OrcishLord. Then
for each leader, attach 1-3 Orcs, and likewise for each leader, offer a
.25 chance to have an OrcishMage. This calculates to a maximum of 10
Mobiles for the whole encounter.

Sub items spawn IN their parent items. Sub mobiles spawn NEAR their parent
mobiles.

(note that if a valid spawn point can't be found, a mobile or item might not
be spawned, and if this happens, the remaining part of a whole encounter might
be skipped).

FUTURE PLANS
==========

1. I'd like to figure out a way to "scale" certain encounters to the player or player party.

2. The current mobile system expresses teams, but only uses this information to place
   the mobiles. I'd like to complete the team AI, and place certain monster elements in
   coordinated AI teams.

STATUS, LIMATATIONS AND KNOWN BUGS
==================================

1. I've noticed a client crash that replicated several times when I added a Bardiche to
every orc twice. I don't know why, and did not attempt to further diagnose. Perhaps adding
multiple weapons to a mobile may cause a client crash if the server communicates this
information to the Mondain client?

2. You cannot spawn Gold (yet).

3. There's an issue with the despawner that I'm currently working on. Right now, when
   the cleanup is called (after 5 minutes, typically) it deletes any outstanding mobiles.
   For hostile encoutners this is okay, but for tamed creatures, this is not okay.
   I anticipate that this will be a significant annoyance, and will update this shortly.

TEST STATUS
===========

This engine has only been tested on a very small private shard. I do regard it as well
tested, but certain things like scalability really couldn't have been tested.

END NOTES
=========

I'll happily accept patches, stack traces, bugs, bug fixes, and any recommendations
for improvement.

