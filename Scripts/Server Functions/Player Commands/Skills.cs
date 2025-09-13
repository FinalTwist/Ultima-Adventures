using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class NewSkillsGump : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
			CommandSystem.Register( "skill", AccessLevel.Player, new CommandEventHandler( NewSkillsGump_OnCommand ) );
		}

		[Usage( "skill" )]
		[Description( "Shows the player the definition of the skills in the game." )]
		public static void NewSkillsGump_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new NewSkillsGump( 0 ) );
		}

        public NewSkillsGump( int origin ) : base( 25, 25 )
        {
			m_Origin = origin;

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(300, 300, 153);
			AddImage(0, 300, 153);
			AddImage(0, 0, 153);
			AddImage(300, 0, 153);
			AddImage(600, 0, 153);
			AddImage(600, 300, 153);
			AddImage(2, 2, 129);
			AddImage(302, 2, 129);
			AddImage(2, 298, 129);
			AddImage(598, 2, 129);
			AddImage(598, 298, 129);
			AddImage(301, 298, 129);
			AddImage(8, 7, 133);
			AddImage(28, 367, 128);
			AddImage(238, 46, 132);
			AddImage(373, 46, 132);
			AddImage(678, 7, 134);
			AddImage(432, 538, 130);
			AddImage(836, 338, 131);
			AddImage(537, 538, 130);

			AddHtml( 109, 80, 716, 349, @"<BODY><BASEFONT Color=#FCFF00><BIG>
Alchemy - This will need a mortar, pestle, empty bottle, and some reagents. A potion keg can hold many bottles of potions. Double clicking the mortar and pestle will start you on your way.
				<br>
Anatomy - This will increase as you simply fight and heal with bandages. It allows for better damage and extra healing.
				<br>
Animal Lore - This helps you control tamed animals and how many pets you can stable. Use this skill directly on tamed creatures to improve it. This skill also allows you to use druidic herbalism potions.
				<br>
Animal Taming - This allows you to tame most creatures. The more you tame, the better you get at it. This skill also helps one be more effective with druidic herbalism potions.
				<br>
Archery - This will improve the more you use the appropriate weapons, increasing damage and possibilities to hit with the weapon. This isn't just for bows and crossbows, but also wizard staves, some magic wands, and throwing gloves. You have the ability to throw daggers, stones, darts, axes, or shurikens. These types of weapons use archery skill but will display marksmanship as its primary attack skill.
				<br>
Arms Lore - This allows some crafters to make better weapons and armor. You may also find armor or weapons that are unidentifed. Using this skill on them may identify them. You can identify weapons and armor to practice this skill, but they can only each be identified once. If you find some decorative armor or weapons, you can sometimes tell what you can get for it, and from whom. Again, these are decorative items and not something you would be able to use. If you determine that something like this does have value, give the item to the merchant you determined and they will give you some gold for it. Just hand it to them or keep it to decorate your home. If you master your item identification skill, you will get double the price for rare items like these. If you want to have a chance for better weapon damage, this skill can help that as well.
				<br>
Begging - This may get you a couple of gold if you use this skill on one of the many townsfolk. You can also target yourself for begging which will change your demeanor to 'begging'. While using this demeanor, townsfolk may do some services cheaper for you such as repairs, identification, and magic item charges. They will also be willing to buy your items and relics at a higher price. If you are really skilled at begging, you could convince opponents to stopping their attacks on you. Every time you are successful with this skill, you will lose a bit of karma. You will also lose fame if you cowardly convince one from attacking you.
				<br>
Blacksmithing - This allows you to make weapons and armor with various types of metals. You will need ingots of metal, a blacksmith hammer, a forge, and an anvil to get started.
				<br>
Bowcraft & Fletching - This allows you to make bows, crossbows, arrows, and bolts. You need a fletching kit to get started, along with wood and possibly feathers. Those that are proficient in this skill also gain an advantage when using bows or crossbows.
				<br>
Bushido - This is the main skill of the Samurai, and embodies the very essence of honorable combat. With it and the Book of Bushido (purchased from a monk), the Samurai can perform a variety of special abilities that mostly are defensive in nature...but can be used to quickly and honorably defeat the toughest of opponents. This skill also gives you an advantage when using pole arms, axes, and other slashing weapons.
				<br>
Camping - This is used to logout safely when out in the wilderness with a bedroll. You can use a bladed weapon on a tree branch to get kindling, allowing you to start a fire and cook food. Using a small tent, you can quickly build a secure area to rest for a bit...even with nearby enemies. With a camping tent, one good at camping may setup a tent and rest in complete safety. Those that become grandmasters in this skill, are able to use a hitching post to stable pets at their home. The better you are at this skill, the more resilient you are with hunger and thirst. This gives you the ability to travel longer without food and drink.
				<br>
Carpentry - This allows you to make furniture, staves, wooden shields, crates, etc. You need a carpentry tool (like a saw) to get started, along with wood.
				<br>
Cartography - This skill allows you to map the man lands you explore. Decoding treasure maps is also done with cartography. In order to make maps, you will need a map makers pen and blank scrolls. Maps can help you navigate this strange world, and sea charts can help you travel the high seas much easier. Most cartographers will tell you that the most treasure gained is from a well found treasure chest.
				<br>
Chivalry - This is the primary talent of any character calling themself a Knight. It allows a fighter to utilize a limited set of spells that would not be feasible with Magery. These spells include healing, curing of poison, improved strength, and holy magic damage. Chivalry skills require a Book of Chivalry, available from any Keeper of Chivalry, and consumes mana as well tithing points. Their rate of success is determined by the Chivalry skill, while the power and duration of their effects is based on karma. Thithing points are gained by donating gold at many ankh shrines, by single clicking the ankh shrine...you can choose to donate your gold. Legends tell of Death Knights following the path of Chivalry, but in a much different way forgotten by most.
				<br>
Cooking - If you want to be able to cook various types of food, this is the trade to take. You will need something like a frying pan, and a stove or fire, to get started. You can also practice the art of druidic herbalism. This is the craft of taking various plants and mixing the right ingredients to make unique potions of varying ability. Having a good alchemy skill will aid in such concoctions. You will need to be proficient in animal lore to use the potions, however. When one finds an unknown reagent, the amount is shown. Those good at cooking have a chance to increase that amount when identifying them with tasting identification. Those proficient in this skill, also have a chance to identify some potions of a better quality. So one that would have normally identified a healing potion, as an example, would instead have identified a greater healing potion. Elixirs and mixtures have a greater effect when one is good at this skill.
				<br>
Detect Hiding - To find stealthy or invisible beings, this skill helps with that. One may also avoid dungeon floor and wall traps with this skill, but only when actively searching. If a trap is nearby, it will show you the location to avoid, but only briefly. You may accidentally find a lost or hidden treasure chest in a dungeon. You may stumble upon it, or actively search for it with this skill. Secret doors will be revealed as well.
				<br>
Discordance - With a musical instrument, a bard is able to play a song that will cause an opponent to become less of a threat in battle by lowering their defenses and abilties. In order to be good with this skill, you must also master Musicianship.
				<br>
Evaluate Intelligence - Mages learn to evaluate another's intelligence to increase the power of their damaging spells against them.
				<br>
Fencing - This is a weapon skill that focuses on items such as daggers and spears. The more you use such weapons, the better you get at hitting opponents with them.
				<br>
Fishing - To catch you next meal with a fishing pole is not the only use of this skill. One may take to the high seas and fish up special treasure as well. Only good fisherman may fish up trophy fish, which you use with a taxidermy kit to mount in your home. Also, any shipwrecks under the waves can only have one skilled in this...bring them to the surface. There are also rare fish to be caught, which fetch a high price. Those skilled in fishing also have a better chance in using harpoons than other ranged weaponos. To learn more about fishing and the high seas, seek out the book titled 'Swords and Shackles'.
				<br>
Focus - This helps regenerate mana and stamina more quickly. Stamina can help a warrior swing their weapon quicker, while mana helps spellcasters cast more spells. Mana is also used in some combat maneuvers.
				<br>
Forensic Evaulation - To investigate who unlocked a chest, this skill can help you investigate that. The major use of this skill is the fascination with corpses and the anatomy of them. To determine who killed a discovered body, you would use this skill. If you train this skill up to at least 5, you will begin to get more resources off of creatures when carved (meaning more meat, feathers, wool, etc)...where the better your skill the more you may get. You can also get a surgeons knife and do a more intricate carving on the corpses of various monsters. Most monsters have something of value that can be expertly removed with the surgeons knife and sold to alchemists for a pretty good profit. You will need empty bottles to collect these items. The anatomy skill may provide better results when skinning creatures or using the surgeons knife on monsters. If you do not sell these bottles of various 'things', you can explore the craft of necrotic alchemy. This allows one to make horrid potions that necromancers can use to further their dark causes. A good alchemy skill helps when making such mixtures. If you have a morbid need for treasure, get a grave shovel and dig up any grave you can find. Double click the shovel and target a tombstone to see what you can dig up. It may be a chest, relic, potion, or the obvious body part. Be weary of the rising dead...but spirit speaking may keep them at bay. It is illegal to dig up graves, so don't get spotted doing so as it will get you reported as a criminal.
				<br>
Healing - With the use of bandages, one may heal themselves...even during combat. One that is very good at this skill, along with anatomy, are able to even cure poisons. You can cut cloth with scissors to make your own bandages. If you have a 60 in both healing and anatomy, you can start to cure poisons. If you have an 80 in both healing and anatomy, you can resurrect others.
				<br>
Herding - This skill is often used by shepherds and their wooden crooks, but those good at this skill is able to have even more tamed pets accompanying them. It also helps to master this skill as it provides more stable space for any tamed pets.
				<br>
Hiding - This allows you to become virtually invisible to others.
				<br>
Inscription - This skill uses a pen, blank scroll, and some reagents. You can make copies of spells from spellbooks you may have. You also get a bonus to your item identification when trying to identify a scroll. If you find a coded message during your journey, these can often be decoded by one intelligent enough. Having a good skill in inscription could also reveal the message.
				<br>
Item Identification - If you want to make more money selling items to vendors in town, this skill will allow you to persuade them. You will also come across artifacts, wands, and scrolls that can only be identified with this skill. If you have some unusual item, you can sometimes tell what you can get for it, and from whom. These are strange items that are usually decorative in nature, and not something you would be able to use. It could be artwork, banners, books, cloth, carpets, coins, srinks, furs, gems, gravestones, instruments, jewels, leather, orbs, paintings, reagents, rugs, scrolls, statues, tablets, or vases. If you determine that it does have value, give the item to the merchant you determined and they will give you some gold for it. Just hand it to them or keep it to decorate your home. If you master this skill, you will get double the price for rare items like these.
				<br>
Lockpicking - If you have a box or chest that is locked, this skill will probably help you get in. You must either make some lockpicks or find a thief that sells them. Others may sell them as well.
				<br>
Lumberjacking - Cutting trees in the forest will allow you to gather more wood. This wood can be used to make furniture, arrows, weapons, or armor if you possess carpentry skills. Double click an axe and then a tree to cut the wood from it. The better your lumberjacking skill, the better you can fight with axes.
				<br>
Mace Fighting - This is a weapon skill that focuses on items such as hammers and maces. The more you use such weapons, the better you get at hitting opponents with them.
				<br>
Magery - A difficult skill to master as those who seek the power have a long road of knowledge they must traverse. Get a spellbook and search the land for hidden spells you may collect and one day become a powerful wizard. Be on the lookout for reagents, as you will need them to summon these magics. Those skilled in magery also gain a combat benefit with using staves, sceptres, non-spell-imbued wands, and scepters.
				<br>
Marksmanship - This is not a skill as much as a sub-skill of archery. Weapons like staves, sceptres, or throwing gloves will have this displayed as its primary attack skill. They are simply based on your archery skill and your archery skill improves when you use such weapons. See Archery for additional information.
				<br>
Mining - THis is a basic, and fairly essential skill for an aspiring blacksmith or, to a slightly lesser degree, tinker. To begin mining you will need tools, these can be either picks or shovels and can be used from the backpack, they do not need to be equipped. The better your skill, the better the chance to dig up rare ore. Some ores can only be mined in certain lands and regions. The better your mining skill, the better you can fight with maces, clubs, and hammers.
				<br>
Necromancy - This is the study of dark magic. Using the power of spiritual speaking, one may amplify this power to its maximum potential. Like wizards, necromancers need specific reagents to use such magic. You will also need to find a necromancer spellbook, along with the spells to put in it. Those skilled in necromancy also gain a combat benefit with using staves, sceptres, non-spell-imbued wands, and scepters.
				<br>
Ninjitsu - Some of the stealithiest assassins have come from those proficient with this skill. One needs to seek out a monk and get a Book of Ninjitsu to begin down this secret path of attacking from the shadows. Ninjas are adept at combat so this skill enhances their ability to use any weapon or their bare hands.
				<br>
Parrying - With a shield, one can get better at blocking blows entirely. Those who follow the path of the samurai will use this skill with a sword instead of a shield. You should find someone to train you a little bit in this skill, so you will know what you are doing.
				<br>
Peacemaking - Bards are able to create music with instruments that will take the most violent opponent and make them stop their attacks to listen to the ballads. In order to be good with this skill, you must also master Musicianship.
				<br>
Poisoning - With a steady hand, and a bottle of poison, one can make certain weapons deal sickening blows. One also becomes better resistant to poison with this skill, and some poison-type spells benefit from this proficiency. Poison can be found or made by alchemists, or you may find venom sacks on some creatures. If you have an empty bottle, and you use the venom sack, you may be able to extract it out. Some weapons have a infectious strike ability that allows others to hit enemies with poisoned weapons. Although infectious strikes lets you better strategize your poisoned weapon, those that are good with this skill do not need infectious strikes to use poison weapons. Simply poison your weapon and it will test your poisoning skill when you attack an opponent that can be poisoned. You will not waste poison on those that are immune, or those that are currently poisoned.
				<br>
Provocation - Bards are able to play musical instruments and cause havoc among others to fight each other. In order to be good with this skill, you must also master Musicianship.
				<br>
Remove Trap - Some containers are trapped, and this skill will allow you to disarm them. With this skill, you are often able to walk near dungeon traps without setting them off. There are hidden traps as well, but being good with this skill can disable them when you walk near them. This skill is also passively active when you open trapped containers, along with walking near dungeon traps.
				<br>
Resisting Spells - This not only helps you resist spells cast upon you, but also enhances many of your other defenses. Some spells cannot be avoided however, but this skill will at least minimize the effects.
				<br>
Snooping - In order to begin Stealing from others, you need to be able to look in their packs for goods.
				<br>
Spirit Speak - This skill helps necromancers with extra power for their spells. You can also summon your own spiritual energy to heal your wounds and restore some stamina. Those with low karma can even channel the energy from corpses for a greater effect within themselves. Those that hone their bodies, with skills in wrestling, will have an added benefit with bodily restoration.
				<br>
Stealing - Things that don't belong to you can be acquired with this skill. Stealing gold from others, or stealing an artifact from an ancient dungeon, thieves make a living doing such things. To steal from other adventurers, one must find and join a Thieves Guild.
				<br>
Stealth - Hiding is one thing, but walking around without being detected is what this skill can do. Why fight your way home when you can walk past your enemies? You will need a skill of 30 in Hiding and then your choice of armor must be light.
				<br>
Swordsmanship - This is a weapon skill that focuses on items such as swords and axes. The more you use such weapons, the better you get at hitting opponents with them.
				<br>
Tactics - Fighters fight, but those who want to be the best train their Tactics skill. This will allow you to do more damage with weapons.
				<br>
Tailoring - Using a sewing kit, one may make clothes and leather armor. You may also need cloth or leather to make things from.
				<br>
Taste Identification - Used once by royalty to determine if food was poisoned, many adventurers use this skill to identify potions they may find. There are also many reagents that will be unidentified unless you taste them first. Old dirty water, stale bread, and dried beef found in dungeons may be hazardous to your health, but this skill will avoid such illnesses while still nourishing you. You may even get a better benefit from food and drink as well. Elixirs and mixtures have a greater effect when one is good at this skill.
				<br>
Tinkering - This skill allows one to make many different types of tools and intricate items. If one wants to make jewelry, then this skill can accomplish that. Tinker tools are needed...along with metal ingots or wood.
				<br>
Tracking - Hunters are proficient with this skill as it allows them to track their prey. With a good tracking skill, one may even track hidden or invisible creatures.
				<br>
Veterinary - If one decides to become an Animal Tamer, this skill will allow you to use bandages to heal your pets...and even resurrect them.
				<br>
Wrestling - For those that decide to not use weapons, and instead enhance the power of their bodies, this skill will allow one to land deadly punches. Find a pair of nice pugilist gloves and you may be able to best a sword fighter in combat. Mages often learn this combat skill as it leaves their hands free to cast spells. Those good with this skill are those that choose to hone their bodies and can gain a restoration benefit when spirit speaking.
			</BIG></BASEFONT></BODY>", (bool)false, (bool)true);

			AddHtml( 507, 471, 200, 29, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SKILL DESCRIPTIONS</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddItem(448, 458, 4893);
        }

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			if ( m_Origin > 0 ){ from.SendSound( 0x4A ); from.SendGump( new Server.Engines.Help.HelpGump( from, 1 ) ); }
		}
    }
}