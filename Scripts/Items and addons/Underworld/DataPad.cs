using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using System.Collections;
using System.Globalization;

namespace Server.Items
{
	public class DataPad : Item
	{
		[Constructable]
		public DataPad( ) : base( 0x27FB )
		{
			Weight = 1.0;
			Light = LightType.Circle150;

			ItemID = Utility.RandomList( 0x27FB, 0x27FC );
			Hue = Utility.RandomList( 0x859, 0x85B, 0x85C, 0x85E, 0x85F, 0x860, 0x861, 0x862, 0x863, 0x864, 0x865, 0x866, 0x867, 0x86C, 0x86D, 0x871, 0x873 );
			if ( DataID < 1 ){ DataID = Utility.RandomMinMax( 1, 58 ); } // DO NOT USE 59

			SetupDataPad( this );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( DataSubject );
		}

		public class DataPadGump : Gump
		{
			public DataPadGump( Mobile from, DataPad Data ): base( 100, 100 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 1242);
				AddHtml( 47, 85, 308, 180, @"<BODY><BASEFONT Color=#00FF06>" + SetupDataPad( Data ) + "</BASEFONT></BODY>", (bool)false, (bool)true);
				AddHtml( 47, 31, 308, 20, @"<BODY><BASEFONT Color=#00FF06>" + Data.DataSubject + " - " + Data.DataTitle + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 47, 56, 308, 20, @"<BODY><BASEFONT Color=#00FF06>By " + Data.DataAuthor + "</BASEFONT></BODY>", (bool)false, (bool)false);
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile;
				from.SendSound( 0x54D );
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			e.CloseGump( typeof( DataPadGump ) );
			e.SendGump( new DataPadGump( e, this ) );
			e.SendSound( 0x54D );
		}

		public DataPad(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
			writer.Write( DataID );
			writer.Write( DataTitle );
			writer.Write( DataAuthor );
			writer.Write( DataSubject );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			DataID = reader.ReadInt();
			DataTitle = reader.ReadString();
			DataAuthor = reader.ReadString();
			DataSubject = reader.ReadString();
		}

		public int DataID;
		[CommandProperty(AccessLevel.Owner)]
		public int Data_ID { get { return DataID; } set { DataID = value; InvalidateProperties(); } }

		public string DataTitle;
		[CommandProperty(AccessLevel.Owner)]
		public string Data_Title { get { return DataTitle; } set { DataTitle = value; InvalidateProperties(); } }

		public string DataAuthor;
		[CommandProperty(AccessLevel.Owner)]
		public string Data_Author { get { return DataAuthor; } set { DataAuthor = value; InvalidateProperties(); } }

		public string DataSubject;
		[CommandProperty(AccessLevel.Owner)]
		public string Data_Subject { get { return DataSubject; } set { DataSubject = value; InvalidateProperties(); } }

		public static string SetupDataPad( DataPad pad )
		{
			string text = "";

			switch( pad.DataID )
			{
				case 1:	pad.DataTitle = "Eldritch Daemon";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Dr. Thomas Witman";
						pad.DataSubject = "Autopsy Record";
						text = "I have examined many life forms, from many different planets, but none like this. Major Parks brought me the body of what looks to be a demonic creature, which I was tasked to perform a full autopsy on. Normal scalpels wouldn't cut through the skin, so we had to use the plasma torch. As soon as we opened the chest cavity, the heart of the beast burst forth in a shower of light. Not knowing what had occurred, we sealed off the lab in order to contain what may have happened. I think it was this incident that is causing the odd things happening around here recently. Mark, my lab assistant, said that he was thinking about reading his data pad and it flew across the room to his hand. The computer systems around here have been performing normally for the most part, but sometimes odd glitches would occur that the engineers cannot make sense of. Just the other day, one of the repair droids killed a cafeteria worker. I had no choice but to open the lab and finish the autopsy. To my surprise, the creature's chest looked as though we didn't cut it open at all. I put on my radiation suit, only to find that it also made me invisible. This is getting stranger every day. I have to setup a meeting with the captain.";
					break;
				case 2:	pad.DataTitle = "The Cave Monster";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Major Jon Parks";
						pad.DataSubject = "Situation Report";
						text = "It has been two years since we crashed on this planet, and we still know very little of the ecology around us. Scouting parties have returned, providing information on primitive humanoid settlements. Omega Team even reported a sighting of a winged dinosaur, that was able to breathe fire. We decided to send a squadron to a nearby cave, that was marked as an area of interest to search. At night, we could make out a strange glow from within, and the radiation counter had very high readings. I wasn't going to take any chances, and I sent in a full compliment of soldiers. Where most were expecting rocks or fungus causing the strange glow, we were instead faced with a giant humanoid winged creature. It had a greenish-yellow color, which glowed in the dark. This was the source of the light, and the radiation. The battle was fierce as the beast killed five of my men, but we were able to finally kill it with a well thrown plasma grenade. Why the creature died, however, we don't know for sure. Although the explosion caused it to collapse, the skin of the beast was not damaged. Regardless, we hauled the corpse back to the lab so Dr. Witman can do a full examination of it. Maybe he can find some answers for us.";
					break;
				case 3:	pad.DataTitle = "Lower Deck Freezer";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Ensign Mauro Alegria";
						pad.DataSubject = "Situation Report";
						text = "I managed to corner that ogre that has been creating havoc in the lower deck. I came out of the bathroom when it was down the hall and it saw me. I didn't have my bowcaster so I had it chase me toward the cold storage area. I ran into the freezer and held the door open as I hid behind it. The ogre came barging in, looking for where I went. I quickly jumped out of the room and slammed the door, locking it behind me. So that noise you hear is that thing banging against the door. All personnel should stay out of the freezer until Dr. Witman can assess what he wants to do with the creature. He has been mumbling about some dragon gene splicing test he wants to do, but he is getting harder to understand as of late.";
					break;
				case 4:	pad.DataTitle = "Picking the Bones";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Corporal Esteban Middleton";
						pad.DataSubject = "Situation Report";
						text = "It seems that some of the crew members have somehow devolved into primal natures. They run around with spears, swords, and clubs. They wear what appears to be armor made of bones from some of the races that were a part of the ship's crew. They communicate with each other in grunts and hand gestures. I need to get out of here before they make something of my bones.";
					break;
				case 5:	pad.DataTitle = "The Xormite Dragon";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Colonel Luther Rohn";
						pad.DataSubject = "Situation Report";
						text = "Message for Captain Gadberry: Captain, I am not sure that Doc Witman has been doing in that lab, but he changed all of the security access codes to lock me and my men out of those rooms. If you remember that just a few days ago, we killed that mithril dragon we encountered a few miles from here. With our decreasing food stores, we thought that we could keep it in the freezer until we needed to carve it up for the cook to prepare for us. The Doc had his cronies bring the corpse to his lab, where he fused some xormite over the scales of the creature, and somehow reanimated the corpse. With the strange things going on with our gear, and the fact that most of the crew seems to have gone insane, we cannot have the Doc doing these things. They provide no value to our survival and only seem to cause chaos when one of his experiments gets loose and we have to clean up the mess. Something needs to be done with him, and soon.";
					break;
				case 6:	pad.DataTitle = "Stasis Chambers";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Nurse Parker Hefner";
						pad.DataSubject = "Medical Report";
						text = "We have had issues where some of the stasis chambers failing due to lack of power to their systems. When this happens, containment of bacta fluids cannot be maintained and it begins to leak from the cylinders. The chief engineer stated that there has been a series of coolant leaks in areas generating power, and that this may be the cause. With part of our crew in hibernation, we cannot afford to have these chambers fail like this. Unlike other members of the crew, those in stasis seem to have their vitals remaining normal and they may need to replace us and continue our work if we cannot get off this planet soon.";
					break;
				case 7:	pad.DataTitle = "Lower Deck Damage";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Engineer Weston Bozeman";
						pad.DataSubject = "Engineering Report";
						text = "Using the rest of the thruster fuel paid off as the damage to the upper deck was quite minor, but the lower deck wasn't so lucky. The center of the lower deck was ripped wide open when we scraped against that mountain on entry. About fifty crew members were lost in that section. The surrounding portions of the lower deck are battered, but still operational and even livable if we need to. Otherwise I suggest that the survivors of the lower deck gather their things and move to the upper deck as soon as they can. We don't know what type of radiation leaks there may be down there and we don't have time to analyze the situation right now.";
					break;
				case 8:	pad.DataTitle = "Mined Crystals";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Geologist Kali Lansing";
						pad.DataSubject = "Science Report";
						text = "Now that we explored this cavern system under our hull, we found some remarkable crystals that seem to have some energetic properties. We are able to power some of the onboard generators using these crystals, which is allowing us to then charge up the functioning droids. Oliphant stopped by and took the larger of the samples. He thinks we can perhaps power the main engines and perhaps thrust the upper deck back into orbit. If we can achieve orbit for even a few minutes, we could get a signal to command and give them our situation and coordinates. I wish he would try to get one of the shuttles to work instead, but he said that the shuttles rely on liquid fuel only. I will take some of these crystals to the laser lab and see if I can break off smaller pieces. Maybe we can liquefy those.";
					break;
				case 9:	pad.DataTitle = "Fungal Spores";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Doctor Artie Hutzler";
						pad.DataSubject = "Medical Report";
						text = "Captain Gadberry wanted us to determine if the various mushrooms down here are edible for us. Our food supplies are dwindling as we have been trapped below the surface for months now. When we crashed in the valley, the upper deck had access to the surface. We even established our Devil Guard Outpost near the lake so we could catch fish to supplement our food stores. Our crash must have made the nearby mountain fracture, as an avalanche of stone and earth swarmed down and buried the entire hull. Being far underground, and no way to get to the surface, we have to find a way to survive down here.<br><br>Unfortunately, our research has shown the fungus to be highly contaminated to the point that the very spores have infected some of the crew. We had a breach of the fungal containment system on the lower deck, which caused us to evacuate that area. Those that did not get out in time died instantly and their bodies eventually grew small mushrooms. Some turned to skeletal remains, while a few crew members turned into walking mushrooms. We need to find alternative food and soon. We are down to some cans of spam and soylent green, which nobody wants to know what that is made of.";
					break;
				case 10:pad.DataTitle = "Mutated Plants";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Botanist Fannie Renard";
						pad.DataSubject = "Science Report";
						text = "Out attempts to grow crops down here have failed. We moved our research lab to the lower deck because there was some actual soil in the area that was heavily damaged from the crash. When we tried to create a genetically engineered type of corn, it had some undesirable consequences. The few plants that did grow, grew to a height of two meters. Then they began growing into each other where we are now left with a pulsating blob of a mass, spewing out green liquid. We think the contamination for the toxic waste has caused this, so we sealed off the lab until further notice. We will continue our experiments in our other lab. Maybe we will have more success, being away from this toxic environment.";
					break;
				case 11:pad.DataTitle = "Shuttle Fuel";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Engineer Richard Brantley";
						pad.DataSubject = "Engineering Report";
						text = "Something happened to our shuttle fuel, to the point where we cannot get it to combust anymore. Oliphant thinks that we have mynocks on board, that are feeding on the fuel lines. They don't just drink the fuel, but filter out the parts they need and spit the rest back. I sent a sample to the lab to check for sure. I don't even know where we would have gotten mynocks from anyway. Maybe when we took on that Kilrathi cargo?<br><br>We have containers of gasoline and diesel fuel, but I can't seem to find a way to convert that to shuttle fuel. We only had that on board to power the smaller droids and the mining equipment. Maybe we can convert the entire shuttle fuel filtration system to work with it.";
					break;
				case 12:pad.DataTitle = "Power Flux";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Technician Archie Oliphant";
						pad.DataSubject = "Safety Report";
						text = "We hooked up that huge crystal, from Lansing's lab, to the main engines to see if we could get them back online. When we charged up the crystal, a pulse went through the ship, causing some slight injuries throughout the crew. These included nausea and vomiting, almost like we were quickly spun around. We didn't think anything was wrong, until we tried to contact the lower deck. When we checked the chronometer down there, it was over 1,000 years off from ours on the upper deck. The structure was rusted and bones from crew members were almost turned to dust. There is no doubt that the lower deck was sent to the past from the pulse. Until we figure this out, no one is authorized to enter the main engine room. If we are not careful, we might erase ourselves from existence.";
					break;
				case 13:pad.DataTitle = "Fuel Reserves";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Lieutenant Randee Giguere";
						pad.DataSubject = "Inventory Report";
						text = "I noticed that our fuel reserves are getting dangerously low. Our inventory records were not up to date and when that stranger came on board to get some fuel, we gave them too much. Now we are in a position to lose our orbit, all because this stranger wanted to impress some girl on the surface of the primitive planet. We need to set a heading for the nearest fuel station, but how do I tell the Captain that I messed up the fuel inventory? If we don't, the gravity of the planet will pull us down before we know it but I don't want to risk a demotion over this.";
					break;
				case 14:pad.DataTitle = "Phaser Batteries";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Lieutenant Douglas Hong";
						pad.DataSubject = "Security Report";
						text = "When we crashed on this planet, our phasers worked just fine. It wasn't until Major Parks brought that damn daemon aboard that we started having trouble. Why are those Kilrathi able to fire their bowcasters at us? How did they get out of detention anyway? The xormite in all of these battery systems are useless now. Rogers had an idea to turn the xormite into coins and try to use it for trade with the local tribes since they seem to like shiny things. I guess we could maybe trade it for an axe or something. We need to be able to defend ourselves. Maybe one of those Kilrathi crates has a supply of krystals and bowcasters in them. If they do, we'll confiscate those for sure.";
					break;
				case 15:pad.DataTitle = "Computer Virus";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Programmer Bobbie Unger";
						pad.DataSubject = "Systems Report";
						text = "I knew we should not have connected that Kilrathi data drive. What seemed like a wealth of strategic information turned out to be nothing but falsehoods wrapped in a computer virus. I am trying to clean out the entire network, but it already reprogrammed the firmware on many of the droids on the ship. Some simply powered down, while others have been going haywire and attacking the crew. We used up the rest of our EMP grenades and have nothing but plasma grenades left. The Captain said we shouldn't use them unless absolutely necessary, as not to damage the ship further. I have to see what other systems this virus has affected. This isn't what I needed today.";
					break;
				case 16:pad.DataTitle = "Infection Containment";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Doctor Craig Sheaffer";
						pad.DataSubject = "Medical Report";
						text = "Our research into the rage virus has reached a critical point. If we can manage to narrow down this vaccine, we can perhaps save the others on Beta Delta III. The monkeys are at full infection and we injected a few of them with the experimental cure. So far their behavior has remained unchanged, but scans show that something inside them is happening. We need to give it another day before we come to some conclusions. Nurse Harper noted that they seem to be showing behaviors of increased intelligence and problem solving. She said that she saw one of them actually unlock and open their cage, but closed it right away. I looked over the surveillance footage and she may be right, but it was too hard to tell. I am sure it is nothing.";
					break;
				case 17:pad.DataTitle = "Demon Behavior";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Biologist Dion Waymire";
						pad.DataSubject = "Science Report";
						text = "Nobody knows what happened with the transporter system, but when a demon stepped out from it, we had to act quickly. I had the engineer quickly transport it to the observation cell. For days we gathered on the other side of the glass, trying to communicate with it and it trying to do the same. It was attempting to use magic to get out, but the field in the cell seemed to absorb the energy as it was intended to do. We have translated much of its language, so we can start to understand what it is saying. We just cannot talk directly to it. So far it says things about eating our souls or that we are all to be damned. We threw some food in there but it didn't eat it. We don't know what it eats. We'll keep studying this creature, as it may provide some answers to what that eldritch deamon has done to this place.";
					break;
				case 18:pad.DataTitle = "Irradiated Daemon";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Biologist Dion Waymire";
						pad.DataSubject = "Science Report";
						text = "Where the transporter system was an efficient way to move about, it now has been nothing but trouble since we crashed here. One of the engineers tried to get the lower deck's transporter system running, but managed to bring forth more demons. Three of them were transported on board and the crew evacuated the area. We usually stay away from the lower decks, due to the radiation down there, but we did get a good look by using the surveillance system. The demons killed many, both crew members and mutated creatures. They just didn't seem to care which. They now gather near the toxic waste drums, torturing those that have yet to perish. Where are these beasts coming from? I am going to recommend to the Captain that we cease all transporter activity.";
					break;
				case 19:pad.DataTitle = "Shuttle Cargo";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Ensign Landon Petro";
						pad.DataSubject = "Status Report";
						text = "Message for Lieutenant Evers: Lieutenant, I finished unloading the cargo, but we had to do it manually because the robotic arms, that normally do it, were behaving erratically. Bobbie said that it was due to some computer virus in the system. We shut them down just to be safe. Do you want us to go unload that Kilrathi cargo? We can if you want but there is some odd yellow liquid leaking from most of the containers. One of crates did have some bowcasters though, which I think Lieutenant Hong would be interested in. We will be in the mess hall taking a break, so let us know what you want us to do next.";
					break;
				case 20:pad.DataTitle = "Transporter Accident";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Lieutenant Ben Bachman";
						pad.DataSubject = "Status Report";
						text = "The entire transporter subsystem has been destroyed, and with xormite becoming an inert substance, we have no way to repair it. It is just as well because Doctor Waymire has been bothering the Captain that we should stop anyway. The main coils were destroyed when we attempted another transport operation, and we replace a section of our room with a section of some dungeon room from who knows where. It wasn't just the room that came with it, but also a couple of sarcophagi and two humanoids dressed in metal armors and robes. They were fighting a dragon in that room when we accidentally brought it on board, but the security droid was able to kill it before it could do too much damage. The two humanoids died from their wounds and the transporter process. Doctor Santini wanted to take one of the sarcophagi to his lab to see what is inside. Seems too scary for me to go along.";
					break;
				case 21:pad.DataTitle = "Xurtzar Escaped";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Biologist Dion Waymire";
						pad.DataSubject = "Security Report";
						text = "Thanks to our new translation algorithm, we can understand what those demons are talking about in the lower deck. I was wondering where these creatures were coming from, but it is those cosmic gargoyles from Sector Delta X that are trying to invade this sector of space. It would normally take their ships years to reach our borders, but they are working with some type of new transporter technology to travel great distances quickly. It seems their technology needs to tether a signal to something like our transporter signal, so they cannot just appear in this sector without that end point. A few of the gargoyles did make it through the transporter incident the other day, but nobody noticed them. We can see them on the video feed, however, and they are definitely working with these demonic creatures. One of the demons calls himself Xurtzar. He seems to be the most powerful of the group, but before we could observe him further, he left the ship. He took one of the cosmic gargoyle's laser swords, and one of the lower deck's power cores, and disappeared. I think the rest are trying to figure out how to get up here. ";
					break;
				case 22:pad.DataTitle = "Excavated Tomb";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Archeologist Ken Santini";
						pad.DataSubject = "Situation Report";
						text = "That last transporter accident brought with it a tomb of some ancient king that use to rule these lands. We brought it to the lab for further study. When we removed the lid, the skeletal remains were indeed lifeless. We examined some of the belongings, like a metal rod with a crystal at the tip, and a book with cryptic writings we couldn't make out. That night, while most of the crew was asleep, the remains came to life and turned the lab into a fiery hell that we dare not enter. The security staff that were guarding the room are dead, or some think they became the mutant creatures guarding the skeletal being within. We are not sure what they are doing, but they created a pentagram on the floor and connected it to the secondary power systems. We need to see if we can cut the power to that room before it can finish its task.";
					break;
				case 23:pad.DataTitle = "Tainted Cargo";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Sergeant Ardis Rawley";
						pad.DataSubject = "Situation Report";
						text = "We were able to take on that shuttle after the stranger left to fight those Kilrathi Starfighters. I am not too sure about these guys. They look like a familiar alien species we met before, but their skin is green and they have a strange glow to them. They like to hold their hands up in a kind of salute and call me Brother Rawley. Strange. Their cargo is something I need to report to the Captain. It is barrels and barrels of this green glowing goo. The radiation readings are quite high, but the containers are keeping them at safe levels so there isn't an immediate danger. What are they going to do with this stuff? Are they weapon dealers? ";
					break;
				case 24:pad.DataTitle = "Containment Breach";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Doctor Damien Eisner";
						pad.DataSubject = "Situation Report";
						text = "One of the stasis units, keeping the alien eggs frozen, appears to have ruptured. The temperature raised in the chamber and the egg hatched. We have an alien loose on this ship somewhere. I am not sure why the unit failed. They had their maintenance cycle performed just the other day. I tried to see what happened on the archival footage, but for some reason the cameras were not working for about an hour during that time. Some believe it was sabotage, as we found a blunt carbon tube next to the glass case. There was also a piece of torn, yellow fabric laying within the glass shards. All security personnel are on high alert, and search teams have been formed to look for the creature. We better catch it before it reproduces.";
					break;
				case 25:pad.DataTitle = "Xenomorph Stowaway";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Sergeant Van Baccus";
						pad.DataSubject = "Situation Report";
						text = "It seems we have a stowaway from that derelict shuttle we brought into the docking bay. We thought it was abandoned so we unloaded the cargo. During the unloading process, we found two crew member bodies on board. One had a hole in its chest. We took that one down to medical to get an autopsy done on it. When we went to get the other one, her body started to involuntarily move and heave. Then a small xenomorph burst from the center of her chest plate. We grabbed some nearby diesel fuel, dumped it all over everything, and lit it on fire. We have to notify security that the other must be somewhere on board. The halon system isn't working so we can only contain the area and let it burn the shuttle out.";
					break;
				case 26:pad.DataTitle = "Gene Splicing";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Professor Jeffery Spillane";
						pad.DataSubject = "Biology Report";
						text = "The species on this planet are the most abundant I have ever got to study. My first analysis was with a bear they captured nearby. It was quite powerful and had to be heavily sedated. It was a pretty harmless animal, in the sense it had no poison fangs. I kept it in a cage in the lab. I fed it some of the dead crew members as it seems to like meat. I hope the Captain doesn't find out.<br><br>The next species was a troll, and this interested me as it seemed like an early evolution of a sentient species. It is able to use clubs and other tools. It just seems to be too smart for my needs. Ever since the crash, the lower deck and outer areas have had a large amount of radioactive waste spilled out from the engine coolant system. If I can genetically engineer that bear with this troll, I could create a creature we can set loose outside the ship to keep the other creatures away. This is crazy enough to work.";
					break;
				case 27:pad.DataTitle = "Interbreeding Spiders";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Professor Jeffery Spillane";
						pad.DataSubject = "Biology Report";
						text = "Never lock an alien with a giant spider. That is what I recently learned from my failed containment order. I wanted to see what would happen. Would the alien kill the giant spider? Would the spider kill the giant alien? Nope. They mated instead, and then the alien ate the spider. It laid about 10 eggs and then dropped dead afterwards. I kept the eggs until they hatched. This was remarkable. They possess both physical qualities of the aliens and the spiders. They aren't very big yet. I could maybe release then into the ventilation system. Maybe they will hunt down and kills the xenomorphs on the loose. Should I ask someone first?";
					break;
				case 28:pad.DataTitle = "Dragon Plasma";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Professor Jeffery Spillane";
						pad.DataSubject = "Biology Report";
						text = "We have had our share of dragons, and they all seem to be very different from each other, yet the same. I have seen prehistoric creatures on other worlds, but these have wings and often breath something I don't like them to. At least at me. One was of particular interest, as it was almost primeval in its nature. It killed quite a few mutants outside before some malfunctioning battle droid took it down. I had them bring it to the fuel processing room. We don't have fuel to process, but the pumps can be used to drain the plasma from it. If these beasts are at all like that eldritch deamon, then I wonder what I can do with this dragon's blood. Maybe make something to drinky drinky? I could use a drink. Where did I put that Romulan ale?";
					break;
				case 29:pad.DataTitle = "DNA Sequencing";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Professor Jeffery Spillane";
						pad.DataSubject = "Biology Report";
						text = "Eisner is so dumb. I went in there with my big happy face and asked if I could take that other dark elf to my lab. Like I was going to throw him a birthday party? He is dumb. Everyone is dumb. I hate dumb people. I wanted to see if the dark elf and giant spider would mate, but I decided to have fun instead. I dissected the elf and spider, resequenced their DNA, and now I have a bunch of baby elf spider running around my lab. They are so cute, with their little legs and their elf faces.<br><br>I just got a message that the Captain was coming by. I better let my babies out of here before he gets here. He’ll never understand science. No one is gooder at science than me.";
					break;
				case 30:pad.DataTitle = "Blood Orchids";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Botanist Fannie Renard";
						pad.DataSubject = "Situation Report";
						text = "Our continued pursuit of edible plants has hit a bit of a snag. The orchids we were growing were exposed to some sort of radiation through the ultraviolet lamp system. They seem to now be sentient and are able to move freely on their own, without the need for soil and water to sustain them. They seem to be attracted to blood, as we found a crop of them in the rage virus lab just the other day. They were feeding on the vomited blood of the infected. They are starting to come at us at times, but they are not hard to dispatch. Ken has started calling them blood orchids due to this behavior. We think we have them all contained now, but I issued a safety report for the crew to watch out for others. So now that this situation is over, we will try planting that corn again in the lab on the upper deck.";
					break;
				case 31:pad.DataTitle = "Prepare for War";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Captain Titus Gadberry";
						pad.DataSubject = "Orders";
						text = "To all crew members: Our scouts have reported a conflict between the dark elves and a group of people called the Zealans. Someone calling themselves the Guardian seems to be instigating the fight. We have no claim to make in this war that is brewing down here, but since our phasers are useless and we have a limited amount of bowcasters, we need to make some armor and weapons to defend ourselves if the war reaches our door.<br><br>Since our ship is buried under hundreds of meters of rubble, it is never going to see the stars again. So I am ordering everyone to gather as much raw material from the ship as you can. Take it to the processing factories where it will get melted down into bars of raw material. We can then have the production droids construct weapons and armor we need once we can program them to do so. We captured one of the dark elves and they won't tell us how they make their weapons and armor, but Doctor Eisner thinks he has a way to learn what we need. Also, take the hatch doors and some of the hull plating to use as shields. ";
					break;
				case 32:pad.DataTitle = "Alien Egg Analysis";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Dr. Thomas Witman";
						pad.DataSubject = "Resource Request";
						text = "Ben, I need the spectral radiographic lab as soon as possible. I have an alien egg that I need to see if the embryo inside is still alive. If I don't know this ahead of time, I will waste the few laser scalpels that we have left cutting open an egg with a dead alien inside. We have no way to produce more laser scalpels, as it uses the same technology as our phasers and they ceased to function when we crashed here. I only needed a few minutes of the imaging lights of the examination table, but your staff refused to let me in. You apparently have been studying the biology of that dead red wyrm Major Parks dragged back here from that cave. Again, I just need a few moments of the imager and I can continue on with my business.";
					break;
				case 33:pad.DataTitle = "Additional Resources";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Major Thomas Macomber";
						pad.DataSubject = "Status Report";
						text = "We have noticed that our supplies of metallic raw materials are going to be dwindling soon. Unless we plan to tear out the wall panels to our rooms, we will need to get materials from elsewhere. We can dig for mithril in the surrounding caves, and even xormite has now infused itself in the nearby rock, but our excavation droids have not been reformatted after the computer virus we had. I sent a crew out with some axes to cut the petrified trees down here. With that, along with the other types of wood we already have on this ship, we can create some wooden weapons and bows. The bows will come in handy because it is getting harder and harder to find krystals for the bowcasters.";
					break;
				case 34:pad.DataTitle = "Xormite Inert";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Professor Tristan McLelland";
						pad.DataSubject = "Status Report";
						text = "I have spent weeks studying this sample of inert xormite, and I think I have an illogical understanding of why the material has become inert. The crew blames the corpse of that eldritch deamon in the lab, and I think they are correct. The aura coming from the corpse is having a profound effect on our mundane items to be sure, while at the same time drawing the power from the xormite to keep the cycle moving. Our radiation counters couldn't pick up the energy from the corpse, because it is not radiation. It is magic. It is this magic that is causing our newly crafted weapons and armor to have unique properties we cannot explain. It is the reason that our simple safety goggles could make one more protected from fire or greater in strength. Things are changing, and I fear our minds are changing with it.<br><br>Xormite is still a useful substance, as the metallurgical properties are still apparent. Xormite can make great weapons and armor, perhaps better than mithril or even that steel we see the dwarves with. We even learned that the nearby dark elf tribes like trading for such shiny metals. We can make it look like the gold coins they trade with.";
					break;
				case 35:pad.DataTitle = "Factory Production";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Lieutenant Quinton Norfleet";
						pad.DataSubject = "Status Report";
						text = "We have collected a lot of metal from the ship, and wood from the crew quarters and surrounding petrified forests. Weapon and armor production are at full capacity, but I would like to use the holographic specifications for the plate armor and use the wood we gathered to make similar armor. After that encounter with the rusty robot creature the dark elves had, the hit from it would sometimes spread the effects of quick oxidation, causing our metal weapons and armor to rust instantly. If we had some of our men wearing some good wooden armor, they would be immune to this effect.";
					break;
				case 36:pad.DataTitle = "Chemical Reaction";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Botanist Fannie Renard";
						pad.DataSubject = "Situation Report";
						text = "Another lab has been categorized as quarantined after that chemical spill the other day. After failing to grow this strange corn on the lower deck, and having to run around the ship picking blood orchids, we tried to grow more corn in the lab on the upper level. We made a new type of fertilizer that was supposed to be given in small amounts, but the operating procedure was written with the wrong units of measure so the technician used too much. Now we have a sealed lab full of those gross, blobby masses of spewing waste in there. The plants have overgrown the entire room and are starting to creep through the containment areas. We may need to burn that part of the ship. I am sick of eating these cans of soylent green and spam. We need something else in our diets.";
					break;
				case 37:pad.DataTitle = "Pirate Cargo";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Ensign Landon Petro";
						pad.DataSubject = "Safety Report";
						text = "We got those black crates unloaded from that shuttle we confiscated from those pirates, but we didn't get them very far before a strange gold fluid began leaking from them. We thought that maybe it was dilithium resin, but scans indicate it is some strange contaminated fluid. We were told to leave them in the hall and to inform the crew to avoid the area until a cleanup can be performed. The fumes were starting to get a bit strong, as our eyes were burning. We have to report to sick bay to make sure we won't have any other affects.";
					break;
				case 38:pad.DataTitle = "Backup Batteries";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Engineer Weston Bozeman";
						pad.DataSubject = "Engineering Report";
						text = "Since the lack of fuel, and the xormite problem, our main ship batteries have been tasked to pick up much of the slack. Unfortunately, two of the battery casings have cracked and acid fluid is leaking all over the floor. We need to turn off the air filtration system as the scientists stated we don't need them anymore as the air has proven to be quite breathable. Some dispute this, that the air outside is contaminated to the point where brain functions have become inhibited, causing some of the crew to revert to primal natures. Doctor Spillane ensures us that it is a virus from the contaminated fluid from those black crates. I wish it didn't have to come to this, but our other systems need the power right now.";
					break;
				case 39:pad.DataTitle = "Search Party";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Lieutenant Kenny Fielding";
						pad.DataSubject = "Status Report";
						text = "Our latest outing has proved to be quite interesting. We have been out for three weeks and we returned with some strange objects from nearby ruins. We found ornately carved rods of various metals, and orbs of glass with various colors and light patterns. We found some ancient scrolls, but we can't make out the writing on them. I guess it isn't for us to figure out, as the brainiacs can look them over. I noticed on our way back that the nearby mountain seems to be shifting a bit. Some rocks have seemed to tumble down. That is probably caused by our crash landing. We left some of the crew on the surface, at a camp we setup that one of them colorfully named Devil Guard. This is due to the demons we have been seeing lately.";
					break;
				case 40:pad.DataTitle = "Holographic Blueprints";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Doctor Damien Eisner";
						pad.DataSubject = "Situation Report";
						text = "We have been ordered to start creating melee weapons and armor for our ongoing protection, but we are not skilled in anything so primitive. We are used to making laser weapons and blaster rifles, not a well balanced axe or finely crafted sword. Major Parks happened to capture a couple of dark elves from a nearby settlement, but they were hard to understand. After Doctor Waymire figured out the language, and programmed the translators, we were able to talk back and forth a bit better. They were angry to be sure and they were not going to help us because they think we are helping the Zealans with their war effort.<br><br>We figured out that one was a craftsman and the other was some type of wizard. It was good we had their hands tied or they may have unleashed some magic in the interrogation room. We took a drastic step and dissected his brain of the craftsman and connected it to the neural holographic network. The results were more than we hoped for. Accessing parts of his memories, we were able to create holographic blueprints of various weapons and armors they create for themselves. Now we can get the production factories programmed and running. Professor Spillane took the other prisoner to his lab, and I don't want to know why.";
					break;
				case 41:pad.DataTitle = "Abandon Ship";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Captain Titus Gadberry";
						pad.DataSubject = "Orders";
						text = "To anyone capable of reading these orders, you are to gather what you can and abandon the ship immediately. We have held it for as long as we can but we are getting outnumbered by mutants, insane crew members, defective droids, and other monstrosities. We found a way to the surface at 44d 21n and 125d 27e. If you can gather together, it would be safer to travel in groups. The natives on the surface are fair skinned humans, so it is important to blend in. So this is a warning to those alien crew members that have different colored skin. Try to get to the nearest medical bay and get some pigment alteration oil. You want to look like these natives as much as you can but do not risk your life to obtain one. If you can't, then just hope that the natives have seen stranger things on this planet than you. Find a peaceful settlement and live out your lives. Start a family, enjoy the sun, or whatever you want to do. Good luck. It has been an honor to serve with all of you.";
					break;
				case 42:pad.DataTitle = "Captain's Log";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Captain Titus Gadberry";
						pad.DataSubject = "Final Report";
						text = "We have been on this ship for the last 10 years, and I finally gave the order to abandon it. I state 10 years but for those on the lower deck, it has been well over 1,000 years. We violated prime directives, unbalanced the ecosystem, and committed crimes against species we had no right to. I am glad our ship was buried in that avalanche. Hopefully nobody will ever find it and witness the atrocities we have done here. Many of my friends have succumbed to some illness, that caused them to become primal again. They wander the halls, grunting and carrying spears. I sometimes see them wearing the bones of our comrades. I had to kill a few in recent weeks.<br><br>When we found the exit leading out from this underworld, we were able to launch a drone into the lower atmosphere. We didn't have much to power it so it mostly ran on some robotic batteries, but we did collect some good data on the surrounding areas. It even had a cloaking device so we could get information on forming settlements. One in particular interests me. They call it Britain and it has a few shops and a stables at the moment, but it is near a harbor so it looks like a good place for me to retire and spend my old age fishing. I grabbed enough supplies for my trip as I will be making it alone. I will leave the rest as I have no need for pictures and letters from loved ones. I just need to start over and hope that history will remain buried in this tomb.";
					break;
				case 43:pad.DataTitle = "Hibernation Chamber";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Ensign Tomas Condrey";
						pad.DataSubject = "Personal Log";
						text = "My hibernation chamber must have malfunctioned, because it woke me up about an hour ago. According the settings on the device, it should have been a few years from now. I managed to make my way back to my quarters so I can put this entry in the log. The ship has been abandoned. Some terrible accident must have happened because rooms are ransacked, I have seen blood on the walls in some areas, and there are hatched alien eggs rotting on the floor. I can only assume we are on a planet, because the gravity systems are not functioning and I am not floating around.<br><br>I seen some strange creatures in the halls, but they didn't see me. I hear strange noises from the ventilation system as well. Food is scarce, but I did find some soylent green so I will eat something before I move onward through the deck. I need to figure out what happened.";
					break;
				case 44:pad.DataTitle = "Chronometer Malfunction";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Ensign Tomas Condrey";
						pad.DataSubject = "Personal Log I";
						text = "I made my way to the main command center, and these systems seem to be malfunctioning as well. The chronometer in here shows that a few years have passed, but the chronometer on the lower deck shows that over 1,000 years have passed since I was put into hyper sleep. Even the logs I accessed only go on for a few years, where the Captain eventually ordered everyone to leave. I can only believe this is possible because the lower deck appears much older than this deck. The walls, doors, and pipes are all rusted. What happened here?";
					break;
				case 45:pad.DataTitle = "Mutants";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Ensign Tomas Condrey";
						pad.DataSubject = "Personal Log II";
						text = "I have been forced to sneak around and hide as I traverse the halls. There is a group of humanoids, wearing yellow robes and hoods, patrolling the area. They seem to have a leadership structure that mimics those of cultists. I managed to crawl through the ventilation system and see where they meet. It was one of our launch bays and they built an altar next to one of the nuclear missiles. They speak in religious tones, talking about the glow and the power of the bomb. These people are clearly insane. How long have they been here? Their skin is greenish in color, but it isn't a martian green. It has a strange glow to it, almost like radiation.";
					break;
				case 46:pad.DataTitle = "Radiation Chamber";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Ensign Tomas Condrey";
						pad.DataSubject = "Personal Log III";
						text = "Those bomb worshippers have captured a large humanoid creature, and put them in the radiation booth on the upper deck. They flooded the room with radiation and sat on the benches, chanting about the glow and the acceptance of it. That if the creature did not survive, that they were not worthy of the glow. The creature eventually died from the radiation, but I did manage to view the footage from the chamber's video surveillance system. This cult has been doing this for years, and horrifically more successful than not. They captured some of the crew and put them into the booth. Their skin turned glowing green like the cultists. When they emerged from the room, they donned their new hoods and robes and followed the group out. I need to get off this ship.";
					break;
				case 47:pad.DataTitle = "Hail the Bomb";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Brother Condrey";
						pad.DataSubject = "Personal Log IV";
						text = "I was afraid when they first found me hiding in the lower deck and placed me in the chamber of life. The glowing green gas flooded the room and I thought it was the end. It was merely the beginning. For weeks I waited in that chamber, as the high priest taught me the scriptures of the bomb. How the spark of radiation gives us life and we owe everything to its grand beauty. We have encountered man and beasts with their magic and weapons, but the glow of the bomb sustains us and we overcome those that invade our home. Those that are worthy of the light, will emerge from the chamber as I did and become one with the glow. Others will either perish or become one of our beastly slave guards to protect our home. The bomb is power. The bomb is life. We owe everything in this world to the bomb.";
					break;
				case 48:pad.DataTitle = "Temporal Wormhole";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Science Officer Ashmore";
						pad.DataSubject = "Safety Report";
						text = "In our search to return to our own time line, we attempted to create a black hole similar to the one that brought us to the past. We powered up the influx generators to see if we can bend the space in the center. We did create a tear in space-time, or so we thought. Some of the soldiers entered the void to investigate where it led. They never returned. We sealed the room for a few days, until we decided our next steps. It didn't take long, but creatures began to come out from the portal. They were demonic looking things, with armor and weapons. They appear to be a cosmic race of creatures that may have the ability to travel between star systems. The room was sealed, so there was nothing to fear, but then a large demon followed them out. It was as tall as three or four men, and shined like a sun. It forced the doors open and led the others toward the time crystal. They have been in that room ever since, while some of the smaller creatures roam the halls and attack us on sight. This isn't a portal of time, but a gate to Hell. Are they protecting the crystal, or trying to use it for something?";
					break;
				case 49:pad.DataTitle = "The Black Hole";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Captain Titus Gadberry";
						pad.DataSubject = "Situation Report";
						text = "When the Stranger left our station, they took almost all of our fuel reserves with them. We didn't realize we were low on fuel until the gravity of a nearby black hole started drawing us closer. We burned through most of what we had left, in an attempt to escape the forces, but it wasn't enough. I thought we were doomed but it pulled us into a fold of space-time that sent us 10,000 years in the past if our chronometers are correct. Without the means to maintain orbit, we crashed on this planet and destroyed most of the lower deck. I have my best minds trying to find a solution to our problem as I write this, but I fear that we will never be heard from again. Instead, I believe that one day there will be someone that finds our remains and picks through our ancient bones.";
					break;
				case 50:pad.DataTitle = "Genetic Engineering";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Professor Jeffery Spillane";
						pad.DataSubject = "Biology Report";
						text = "The creatures on this planet are some of the most evolved I have ever seen. The life here must have evolved over billions of years to the state we are seeing them in. Why they haven't achieved the means to travel to the start is a mystery, but they do have forces here that we can't readily explain with science. Ever since we brought that eldritch daemon on board, we have seen these supernatural properties on our own belongings. Some of the creatures here have this magic within them, so I thought I would see if I could increase this ability if I splice the genes of an ogre and a dragon. Preliminary experiments have failed as the creatures died during the maturation process. I did manage to have partial success, where I did create a beastly creature for sure. During continued experiments and observations, my lab assistant was careless and did not secure the lock on the main door. These creatures escaped during the night and surveillance footage showed two of them leaving the ship. That means the third is somewhere on board. Probably in the ventilation system. I have to find it before the Captain finds out.";
					break;
				case 51:pad.DataTitle = "Stasis Chamber Leak";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Corporal Esteban Middleton";
						pad.DataSubject = "Personal Log";
						text = "The safety system on my stasis chamber released me as there was a crack in the glass and the fluid leaked out. I awoke into a nightmare as the ship is just a ruined hulk of what I remember. I was in that chamber due to a few broken bones but that should have only lasted a few days. The timer on the chamber showed that I was in there for almost 4,200 years. What happened here? I can see strange humanoids wandering the halls but they have yet to notice me. I also hear strange noises through the ventilation system. My phaser doesn't seem to work for some reason. It is as if the xormite cells were totally drained. I need to find some food and weapons if I am going to try and maneuver the ship.";
					break;
				case 52:pad.DataTitle = "Time to Leave";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Corporal Esteban Middleton";
						pad.DataSubject = "Situation Report";
						text = "I was able to find a working Kilrathi bowcaster, which still seems to work even though the phasers don't. I only have a few krystals so I better make them count. There was also a broken laser sword I will take with me. Both are not traditional weapons of this world, so metal workers won't be able to fix it for me, but perhaps a skilled tinker can. I also found some food, a canteen of clean water, a sword made of durasteel, and some armor made of promethium. Why were the factories programmed to make such primitive weapons and armor? They used some of the metal from the ship's lower deck, melted it down, and made a bunch of these things. I guess it matters very little now. I am able to move about the ship by use of the ventilation system, but I have seen this ship infested with many different creatures. Most I have never seen before. Some I know all too well. I have seen Kilrathi throughout the ship, but I think they devolved a bit like the others. I think they may have formed primitive tribes on the ship, but they are still as ruthless when they were more civilized. I also have seen some of the devolved crew members hanging around near the radiation booth and missile launch bay. Either they are adapting to the radiation or they will be dead within a few months. I was able to access a computer from the main server room. It appears that Captain Gadberry ordered an evacuation centuries ago. He told the remaining survivors to find a nearby settlement and live out their lives. I will have to do the same. I studied a digital map of where the Captain went, a place called Britain. I will head there even though everyone I ever knew would have died of old age long ago. If it is still there, it will be as good of a place as any.";
					break;
				case 53:pad.DataTitle = "Kilrathi Pirates";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Chief of Security";
						pad.DataSubject = "Security Bulletin";
						text = "WARNING: The Kilrathi shuttle we brought on board was not a pilgrimage craft as the beacon identity stated. They are a band of pirates that have been hijacking cargo in this sector for a few years now. Ensign Petro reported that their latest cargo has been unloaded, but we have yet to identify what it is. For now, stay away from that cargo as some of the crates have started to leak. Everyone is to carry phasers with them as we have yet to locate where the Kilrathi are on the ship. We think they may have headed for the lower deck. Approach with extreme caution and apprehend them immediately.";
					break;
				case 54:pad.DataTitle = "Toxic Waste";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Commander Cara Sanders";
						pad.DataSubject = "Safety Bulletin";
						text = "WARNING: Since our crash on the surface, we noticed that there is a coolant leak on the lower deck that has been increasing in flow. The good news is that the spill is staying outside of the ship, but the bad news is that it is affecting the nearby ecology at an alarming rate. Surveillance footage has shown that some of the terrestrial humanoid creatures have been exposed to this toxic material and have begun to mutate at an alarming rate. Although we have made nicknames for these creatures, we learned from the dark elf prisoner that the creatures are known as minotaurs, gargoyles, and lizardmen. When you travel outside the ship, be careful to avoid these creatures and the glowing green waste that is spilling out from our hull. The doctor believes that breathing in the fumes can cause hallucinations, as one ensign reported seeing the liquid come alive and almost form a man-shaped figure.";
					break;
				case 55:pad.DataTitle = "Sentenced Execution";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Captain Titus Gadberry";
						pad.DataSubject = "Ship's Log";
						text = "It is my unfortunate duty to report that Professor Jeffery Spillane has been sentenced to death for the crimes of unauthorized genetic experiments on sentient creatures. His experiments started out for the sake of research, but they were limited toward animals we captured and wanted to learn more about them. He took these experiments too far when he took our dark elf prisoner and spliced its DNA with that of a giant shadow recluse spider. Not only did the good Professor manage to breed a few of these monstrosities, but they managed to get loose on the ship. A couple of them got away and we could not track them down. We may be stranded on this planet, but we will not forfeit our values as long as I am alive.";
					break;
				case 56:pad.DataTitle = "Sentenced Execution";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Professor Tamien Krangur";
						pad.DataSubject = "The Legend of the Syth";
						text = "The Syth, also referred to as the Syth Order was a sect of psychics who utilized their power to instill fear in the galaxy. The term 'Syth' originally referred to a species of aliens native to the planets Korriban and Ziost, who were later enslaved and ruled by an exiled Dark Lord. These Syth had once been members of the Mystics, a monastic psychic religion dedicated to peace by using their abilties for the greater good and to bring peace to the universe. The Syth, who refused to rely exclusively on the Mystic teachings, challenged the Mystics by giving in to the dark powers, which started the century of darkness. However, they had been defeated and subsequently exiled from known space, which led to their discovery of the Syth species. Following centuries of interbreeding and mixing of cultures between the aliens and the exiles, the Syth would no longer be identified by their race, but by their dedication to the ancient Syth philosophy. This religious order would survive in many different incarnations throughout galactic history. The rise of a new leader, or Dark Lord, would often cause drastic reorganizations in the cult, however the Syth would always be characterized by their lust for power and their desire to destroy the Mystics. The Syth were the most infamous of all dark psychic religions, and the members of the cult were often seen as the pinnacle of power within the dark psychics. Throughout their long history, the Syth commanded several empires and initiated many galactic wars. With such great influence, the Syth religion inspired many cults that were not technically part of the Syth Order, nor did they consist of actual Syth. Instead, they were founded and made up of Syth devotees and other psychic-sensitives dedicated to prolonging the teachings and the memory of the Syth. Such cults included the Naddists, the Disciples of Ragnos, and the Krath.";
					break;
				case 57:pad.DataTitle = "Syth Spies";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Chief of Security";
						pad.DataSubject = "Security Bulletin";
						text = "It was discovered that shortly before the crash, two Syth spies have disguised themselves as crew members and have begun accessing our most critical mission data from the central computer core. We are still trying to determine what information was compromised, but whatever they took must have been important enough as they were seen leaving the crash site. Major Parks sent a team after them but they were lost in the nearby dense forest. We may be alright, however, as they are stranded on this planet as much as we are. Hopefully they will not recruit the indigenous humoids into their cult, but they would have to find psychic-sensitive ones to be any sort of threat. We do have a Mystic on board, and we should see if they could pursue the Syth much better than our security team can.";
					break;
				case 58:pad.DataTitle = "Illegal Pets";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Commander Cara Sanders";
						pad.DataSubject = "Safety Bulletin";
						text = "NOTICE: Per regulation 134.2, no crew members shall bring any pets on board that fall on the 18B classification list. This includes the shaclaw that Cadet Brad Williams brought on board when we last visited Star Port 12. These regulations are in place for a reason, and this recent violation provides an example on why we cannot be lenient on such matters. Shaclaws may start out small and cute, but they live for centuries and grow up to 10 meters tall. Without the proper care, these creatures could grow to a beast that would ravage the ship. The shaclaw has been confiscated and taken to a containment unit in Professor Spillane's laboratory. If anyone else has any such pets under the 18B classification, you are to turn them into me immediately and there will be no questions asked.";
					break;
				case 59:pad.DataTitle = "Droid Construction";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Engineer Weston Bozeman";
						pad.DataSubject = "Forest Harvesters";
						text = "With many of the electronic systems either broken or malfunctioning, our team has been able to collect enough parts to make a few forest harvester droids. Because we needed to use some war mech parts, these droids have the programming for attacks that we can't seem to remove from the drives. The Captain thinks we should leave it as is, because then these droids can both harvest wood and perform sentry duties when needed. The issue we are running into is that these droids will not chop any wood, so we are still doing that manually with axes. We can, however, give the logs to the droid and they will cut them into the much needed boards. Our computer specialist is trying to solve these issues, but she isn't very confident since this programming came from Seti Alpha III.";
					break;
				case 60:pad.DataTitle = "Far From Home";
						pad.Name = pad.DataTitle;
						pad.DataAuthor = "Chief Medical Officer";
						pad.DataSubject = "Medical Shuttle Lost";
						text = "All I know of the patient is entered into their medical record. They are near death, but placing them in the stasis chamber seems to have begun the healing process. The scans show an incredible head trauma, so they may wake from their coma with no memories of what or who they were (you begin with no skills). With the station plummeting to the planet below, due to that stranger draining our fuel reserves, I have decided to place the stasis chamber our last medical shuttle. I will set it on auto-pilot and hope for the best. If it can land safely on the planet below, and they survive the descent, then perhaps they can continue their lives on this primitive world. They may have an advantage as they are from a more advanced race of beings so they have the ability to remember and learn more things (can grandmaster 40 different skills).<br><br>Because of our advanced knowledge of logic and science, however, some things we have learned about this world is that they have elements we cannot understand. Magical resurrection, and the concept of deities, are things we cannot comprehend (costs 3 times as much gold to resurrect at a shrine or healer). The system shock from any such resurrection would surely take its toll (paying full tribute still causes a 10% loss in fame and karma, and a 5% loss in skills and attributes) which could prove to be devastating (paying no tribute at all would cause a 20% loss in fame and karma, and a 10% loss in skills and attributes).<br><br>Although they will be able to learn some of the skills that are classified as magic or divine, they will surely justify it with science. Because of our lack of superstition, like the inhabitants of this world, we don’t believe in the concept of luck (you will never benefit from luck). We don't have any of the world's currency to barter with (you begin with no gold), and because we feel we are more advanced we will probably not get along with the guildmasters of the crude trades they practice (guild membership costs 4 times as much as normal).<br><br>If you are the patient in the stasis chamber, then simply step into the star gate and say 'beam me up'. You will be taken to your crashed shuttle where your adventure begins. Use the computer terminal nearby to change your skin and hair tones if you want an appearance that is slightly different than human due to your alien heritage<br><br>When you awake, you have no memory of who you were. You find yourself near the shuttle that crashed on top of the mountain. The computer system instructed you on how to setup a power source from the remaining fuel, and it appears an alien creature latched onto the shuttle but died in the crash. You have been using this as a source of food and survived a few days here from it. Now your supplies are running out, your canteen is empty, and all you have is a knife. You will have to venture out if you plan to survive.";
					break;
			}
			return text;
		}
	}
}