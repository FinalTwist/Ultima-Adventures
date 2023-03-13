using System;
using Server;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Misc
{
    class BuildQuestItems
    {
		public static void CreateQuestItems()
		{
			// FIRST DELETE THE SHADOWLORDS ROAMING AROUND ////////////////////////////////////////
			// THEN DELETE SERPENT ISLE CHARACTERS ////////////////////////////////////////////////
			// THEN DELETE MANGAR IF HE IS STILL ROAMING AROUND ///////////////////////////////////
			// THEN DELETE TIME LORD CHAMPIONS IF ROAMING AROUND //////////////////////////////////
			// THE SPAWNERS APPEARS IN THE DECORATION FILES ///////////////////////////////////////
			ArrayList npcs = new ArrayList();
			foreach ( Mobile evil in World.Mobiles.Values )
			if (	evil is Mangar || 
					evil is CaddelliteDragon || 
					evil is Xurtzar || 
					evil is Arachnar || 
					evil is Surtaz || 
					evil is Vulcrum || 
					evil is Shadowlord || 
					evil is BaneOfAnarchy || 
					evil is BaneOfWantoness || 
					evil is BaneOfInsanity || 
					evil is SerpentOfOrder || 
					evil is SerpentOfChaos )
			{
				npcs.Add( evil );
			}
			for ( int i = 0; i < npcs.Count; ++i )
			{
				Mobile dude = ( Mobile )npcs[ i ];
				dude.Delete();
			}

			ArrayList QStargets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is QuestTeleporter )
			{
				QStargets.Add( item );
			}
			for ( int i = 0; i < QStargets.Count; ++i )
			{
				Item item = ( Item )QStargets[ i ];
				item.Delete();
			}

			QuestTeleporter qTeleporter = new QuestTeleporter();

			////////// MANGAR SEWER GATE
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 0xEC;
				qTeleporter.TeleporterItem = 0x685;
				qTeleporter.TeleporterMessage = "You step through the gate.";
				qTeleporter.TeleporterFail = "The gate doesn't seem to budge, but there is a keyhole with a symbol of a demon on it.";
				qTeleporter.TeleporterQuest = "BardsTaleEbonyKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6295, 1883, 25);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Hue = 0xB9A;
				qTeleporter.Name = "rusty gate";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6527, 1538, 10), Map.Felucca);
			////////// MANGAR SEWER GATE
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 0xEC;
				qTeleporter.TeleporterItem = 0x687;
				qTeleporter.TeleporterMessage = "You step through the gate.";
				qTeleporter.TeleporterFail = "The gate doesn't seem to budge, but there is a keyhole with a symbol of a demon on it.";
				qTeleporter.TeleporterQuest = "BardsTaleEbonyKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6295, 1883, 25);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Hue = 0xB9A;
				qTeleporter.Name = "rusty gate";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6528, 1538, 10), Map.Felucca);

			////////// MANGAR CRYSTAL BALL TO SKARA BRAE
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 0x1E1;
				qTeleporter.TeleporterItem = 0xE30;
				qTeleporter.TeleporterMessage = "You are magically pulled into another realm.";
				qTeleporter.TeleporterFail = "";
				qTeleporter.TeleporterQuest = "blank";
				qTeleporter.TeleporterLock = "BardsTaleKilledMangar";
				qTeleporter.TeleporterLockMsg = "The crystal ball has an eerie glow to it.";
				qTeleporter.TeleporterPointDest = new Point3D(6927, 220, 0);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "a mysterious crystal ball";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(2830, 1875, 102), Map.Trammel);

			////////// CATACOMB DOORS
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1741;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a keyhole.";
				qTeleporter.TeleporterQuest = "BardsTaleCatacombKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6580, 2021, 75);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6944, 181, 0), Map.Felucca);
			////////// CATACOMB DOORS
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1743;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a keyhole.";
				qTeleporter.TeleporterQuest = "BardsTaleCatacombKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6580, 2021, 75);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6944, 180, 0), Map.Felucca);

			////////// KYLEARAN TOWER
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1679;
				qTeleporter.TeleporterMessage = "You step through the gate.";
				qTeleporter.TeleporterFail = "The gate doesn't seem to budge, but there is a keyhole with a symbol of a unicorn on it.";
				qTeleporter.TeleporterQuest = "BardsTaleKylearanKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5772, 2412, -8);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal gate";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6883, 174, 5), Map.Felucca);
			////////// KYLEARAN TOWER
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1677;
				qTeleporter.TeleporterMessage = "You step through the gate.";
				qTeleporter.TeleporterFail = "The gate doesn't seem to budge, but there is a keyhole with a symbol of a unicorn on it.";
				qTeleporter.TeleporterQuest = "BardsTaleKylearanKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5772, 2412, -8);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal gate";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6883, 175, 5), Map.Felucca);

			////////// HARKYN'S CASTLE
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 234;
				qTeleporter.TeleporterItem = 1773;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a keyhole with a symbol of a dragon on it.";
				qTeleporter.TeleporterQuest = "BardsTaleHarkynKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5839, 1118, 30);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "wooden door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6889, 273, 1), Map.Felucca);
			////////// HARKYN'S CASTLE
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 234;
				qTeleporter.TeleporterItem = 1775;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a keyhole with a symbol of a dragon on it.";
				qTeleporter.TeleporterQuest = "BardsTaleHarkynKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5839, 1118, 30);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "wooden door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6889, 272, 1), Map.Felucca);

			////////// HARKYN'S CASTLE INNER
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 234;
				qTeleporter.TeleporterItem = 1773;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge.";
				qTeleporter.TeleporterQuest = "BardsTaleHarkynKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5835, 1118, 30);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "wooden door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(5836, 1119, 30), Map.Felucca);
			////////// HARKYN'S CASTLE INNER
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 234;
				qTeleporter.TeleporterItem = 1775;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge.";
				qTeleporter.TeleporterQuest = "BardsTaleHarkynKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5835, 1118, 30);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "wooden door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(5836, 1118, 30), Map.Felucca);

			////////// HARKYN'S CASTLE EXIT
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 234;
				qTeleporter.TeleporterItem = 1773;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a keyhole with a symbol of a dragon on it.";
				qTeleporter.TeleporterQuest = "BardsTaleHarkynKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6890, 273, 0);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "wooden door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(5841, 1119, 30), Map.Felucca);
			////////// HARKYN'S CASTLE EXIT
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 234;
				qTeleporter.TeleporterItem = 1775;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a keyhole with a symbol of a dragon on it.";
				qTeleporter.TeleporterQuest = "BardsTaleHarkynKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6890, 273, 0);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "wooden door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(5841, 1118, 30), Map.Felucca);

			////////// HARKYN'S CASTLE GRAY DRAGON
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 234;
				qTeleporter.TeleporterItem = 1773;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a rusty keyhole.";
				qTeleporter.TeleporterQuest = "BardsTaleDragonKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5804, 1123, 30);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "wooden door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(5805, 1124, 30), Map.Felucca);
			////////// HARKYN'S CASTLE GRAY DRAGON
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 234;
				qTeleporter.TeleporterItem = 1775;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a rusty keyhole.";
				qTeleporter.TeleporterQuest = "BardsTaleDragonKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5804, 1123, 30);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "wooden door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(5805, 1123, 30), Map.Felucca);

			////////// KYLEARAN'S TOWER
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 234;
				qTeleporter.TeleporterItem = 1765;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a keyhole with a symbol of a tree on it.";
				qTeleporter.TeleporterQuest = "BardsTaleBedroomKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5766, 2248, 0);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "wooden door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(5766, 2247, 0), Map.Felucca);

			////////// MANGAR'S ROOM ENTER
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1653;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a lock made of silver on it.";
				qTeleporter.TeleporterQuest = "BardsTaleMangarKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6426, 1542, 0);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6592, 1399, -100), Map.Felucca);
			////////// MANGAR'S ROOM ENTER
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1655;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a lock made of silver on it.";
				qTeleporter.TeleporterQuest = "BardsTaleMangarKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6426, 1542, 0);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6593, 1399, -100), Map.Felucca);

			////////// MANGAR'S ROOM EXIT
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1653;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a lock made of silver on it.";
				qTeleporter.TeleporterQuest = "BardsTaleMangarKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6592, 1400, -100);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6426, 1543, 0), Map.Felucca);
			////////// MANGAR'S ROOM EXIT
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1655;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but there is a lock made of silver on it.";
				qTeleporter.TeleporterQuest = "BardsTaleMangarKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6592, 1400, -100);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6427, 1543, 0), Map.Felucca);

			////////// UNDERMOUNTAIN DOOR
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1733;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but the door looks to be made of dwarven steel and it has a keyhole.";
				qTeleporter.TeleporterQuest = "UndermountainKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5409, 508, 0);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(5409, 509, 0), Map.Felucca);
			////////// UNDERMOUNTAIN DOOR
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1735;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but the door looks to be made of dwarven steel and it has a keyhole.";
				qTeleporter.TeleporterQuest = "UndermountainKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(5410, 508, 0);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(5410, 509, 0), Map.Felucca);

			////////// BOTTLE TO THE BOTTLE CITY
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 0x1E1;
				qTeleporter.TeleporterItem = 0x0FE5;
				qTeleporter.Light = LightType.Circle225;
				qTeleporter.TeleporterMessage = "You are magically pulled into the bottle.";
				qTeleporter.TeleporterFail = "";
				qTeleporter.TeleporterQuest = "blank";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6603, 1082, 2);
				qTeleporter.TeleporterMapDest = Map.Trammel;
				qTeleporter.Name = "a city in a bottle";
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.Hue = 0;
				qTeleporter.MoveToWorld (new Point3D(6376, 302, 15), Map.Felucca);

			////////// BLACK KNIGHT VAULT DOOR
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1733;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but the door seems to be made of blackened steel and it has a keyhole with a symbol of a sword above it.";
				qTeleporter.TeleporterQuest = "BlackKnightKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6248, 306, 20);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.Hue = 0x497;
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(1581, 199, 0), Map.Malas);
			////////// BLACK KNIGHT VAULT DOOR
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1735;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but the door seems to be made of blackened steel and it has a keyhole with a symbol of a sword above it.";
				qTeleporter.TeleporterQuest = "BlackKnightKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(6249, 306, 20);
				qTeleporter.TeleporterMapDest = Map.Felucca;
				qTeleporter.Name = "metal door";
				qTeleporter.Hue = 0x497;
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(1582, 199, 0), Map.Malas);
			////////// BLACK KNIGHT VAULT DOOR
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1733;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but the door seems to be made of blackened steel and it has a keyhole with a symbol of a sword above it.";
				qTeleporter.TeleporterQuest = "BlackKnightKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(1581, 200, 0);
				qTeleporter.TeleporterMapDest = Map.Malas;
				qTeleporter.Name = "metal door";
				qTeleporter.Hue = 0x497;
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6248, 307, 20), Map.Felucca);
			////////// BLACK KNIGHT VAULT DOOR
				qTeleporter = new QuestTeleporter();
				qTeleporter.TeleporterOpen = 0;
				qTeleporter.TeleporterSound = 236;
				qTeleporter.TeleporterItem = 1735;
				qTeleporter.TeleporterMessage = "You step through the doorway.";
				qTeleporter.TeleporterFail = "The door doesn't seem to budge, but the door seems to be made of blackened steel and it has a keyhole with a symbol of a sword above it.";
				qTeleporter.TeleporterQuest = "BlackKnightKey";
				qTeleporter.TeleporterLock = "";
				qTeleporter.TeleporterLockMsg = "";
				qTeleporter.TeleporterPointDest = new Point3D(1582, 200, 0);
				qTeleporter.TeleporterMapDest = Map.Malas;
				qTeleporter.Name = "metal door";
				qTeleporter.Hue = 0x497;
				qTeleporter.ItemID = qTeleporter.TeleporterItem;
				qTeleporter.MoveToWorld (new Point3D(6249, 307, 20), Map.Felucca);
		}
	}
}