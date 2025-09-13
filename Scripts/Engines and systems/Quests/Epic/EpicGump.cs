using System;
using Server;
using Server.Misc;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Items;
using System.Text;
using Server.Mobiles;
using System.Collections;
using Server.Commands.Generic;

namespace Server.Gumps
{
	public class EpicGump : Gump
	{
        public EpicGump( Mobile talker, Mobile listener, bool allowed, string alignment ) : base( 25, 25 )
        {
			string sTitle = "";
			string sText = "";

			string myName = talker.Name;
			string yourName = listener.Name;

			string sInfo = "<br><br>These items can be customized to fit your adventuring style. When you obtain one of these items tribute, single click on the item and select the 'Status' option. A menu will appear that will allow you to spend the points given on whatever attributes you choose. Be careful, as you cannot change an attribute once you select it. Once the points have been used up, the item will remain as it is.";

			string sBare = "<br><br>" + myName + " will offer you an item of tribute if you retrieve a rare item...<br><br>" + Server.Mobiles.EpicCharacter.GetSpecialItemRequirement( listener ) + "<br><br>...and have achieved a fame of at least 7,000 points. If you accept their tribute, your fame will decrease by 7,000 points and you will have to rebuild it again. If you have achieved this amount, single click on " + myName + " and select Tribute to choose the type of item you want. " + myName + " will also need at least 5,000 gold in order to construct the item for you.";

			if ( alignment == "good" )
			{
				sInfo = "<br><br>These items can be customized to fit your adventuring style. When you obtain one of these items tribute, single click on the item and select the 'Status' option. A menu will appear that will allow you to spend the points given on whatever attributes you choose. Be careful, as you cannot change an attribute once you select it. Once the points have been used up, the item will remain as it is.";

				sBare = "<br><br>" + myName + " will offer you an item of tribute if you retrieve a rare item...<br><br>" + Server.Mobiles.EpicCharacter.GetSpecialItemRequirement( listener ) + "<br><br>...and have achieved a fame of at least 4,000 points and a karma of at least 4,000 points. If you accept their tribute, your fame and karma will decrease by 4,000 points and you will have to rebuild them again. If you have achieved these amounts, single click on " + myName + " and select Tribute to choose the type of item you want. " + myName + " will also need at least 5,000 gold in order to construct the item for you.";
			}
			else if ( alignment == "evil" )
			{
				sInfo = "<br><br>These items can be customized to fit your adventuring style. When you obtain one of these items tribute, single click on the item and select the 'Status' option. A menu will appear that will allow you to spend the points given on whatever attributes you choose. Be careful, as you cannot change an attribute once you select it. Once the points have been used up, the item will remain as it is.";

				sBare = "<br><br>" + myName + " will offer you an item of tribute if you retrieve a rare item...<br><br>" + Server.Mobiles.EpicCharacter.GetSpecialItemRequirement( listener ) + "<br><br>...and have achieved a fame of at least 4,000 points and a karma of at least -4,000 points or lower. If you accept their tribute, your fame will decrease by 4,000 points and your karma will increase by 4,000 points. You will have to rebuild them again. If you have achieved these amounts, single click on " + myName + " and select Tribute to choose the type of item you want. " + myName + " will also need at least 5,000 gold in order to construct the item for you.";
			}

			if ( myName == "Lord Draxinusom" )
			{
				sTitle = "The Gargoyle Race";
				sText = "Greetings, " + yourName + " and welcome to our great city. Your deeds are known throughout the land and we would like to pay tribute to you. We do have weapons that could aid in the fight against the ophidians or elements of earth, but many of our items help those that craft with ores and metals." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Greetings, " + yourName + " and welcome to our great city. I would offer you tribute but you are quite unknown throughout the land. Perhaps if you adventured more would your stories start to be told by others." + sBare;
				}
			}
			else if ( myName == "the Great Earth Serpent" )
			{
				sTitle = "Seeking the Balance";
				sText = "Greetings, " + yourName + ". Your deeds have shown me that you could perhaps provide balance to the land, and thus I would like to pay tribute to you. I could summon weapons that would help you with the ophidians who attempt to destroy the balance, or aid you in dealing with my brothers or order and chaos." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Greetings, " + yourName + ". I would offer you tribute to help with the order of balance, but you are quite unknown throughout the land. Perhaps you need to prove yourself, and show me that you can indeed provide balance to the land." + sBare;
				}
			}
			else if ( myName == "Morphius" )
			{
				sTitle = "The Necrotic Hand";
				sText = "Well, well, well. I see that " + yourName + " has come to seek power in the realm of the dead. You have been quite busy, aiding to my plans like a worthy puppet. I suppose a tribute to your efforts are in order. I can offer items that would help those that explore death and molest the graves of others." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Well, well, well. I see that " + yourName + " has come to seek power in the realm of the dead. But alas you have failed to further my efforts against the living. Go forth, and return when you have seen to my will." + sBare;
				}
			}
			else if ( myName == "Mondain" )
			{
				sTitle = "The Shards of Time";
				sText = "Greetings, " + yourName + ". I see you managed to find me here. I know what you are thinking. The Stranger had slain me years ago. Although that may be the tale told within the shadows of taverns, it is far from true. The gem of immortality saved me from the Stranger's attack, as the shattered gem unleashed a power that returned me to life shortly thereafter. My efforts have been furthered by some of the exploits you undertook. For that, I would like to give you tribute to help you further in my pursuits. These items add great power to even the most novice of wizards." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Greetings, " + yourName + ". I see you managed to find me here. I know what you are thinking. The Stranger had slain me years ago. Although that may be true, it isn't who they slain but when. They may one day slay me again but the ripples of time have helped me escape to this place. If you can prove that you are worthy of my tribute, then go forth and bring havoc to the land. If not, perhaps you should not return." + sBare;
				}
			}
			else if ( myName == "Tyball" )
			{
				sTitle = "The Demon Unbound";
				sText = "Hail, " + yourName + ". It is not often I find someone that could brave the halls of this dungeon. I am currently practicing the art of alchemic reactions toward demon control. So far, I have been successful as you see with my little friend over there. I do find myself leaving from time to time, where I have to acquire items to concoct these mixtures. Although some try to stop me, you have done many things to help me avoid these inconveniences. For that, I can offer you an item that would help you brew potions as good as I." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Hail, " + yourName + ". It is not often I find someone that could brave the halls of this dungeon. I am currently practicing the art of alchemic reactions toward demon control. So far, I have been successful as you see with my little friend over there. I do find myself leaving from time to time, where I have to acquire items to concoct these mixtures. Many try to stop me, and you have done nothing to reduce these inconveniences I often face. Maybe the next time we meet, you will have tales of those that once opposed me." + sBare;
				}
			}
			else if ( myName == "Arcadion" )
			{
				sTitle = "The Fires of Purgatory";
				sText = "Well, well, well. If it isn't the soul of " + yourName + ", perhaps surrending to my chains? No matter. You are more useful to me out there, as you have delivered many souls to my lair. I want you to continue your efforts, but I do feel you need an item that aids in your wretched ways." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Well, well, well. If it isn't the soul of " + yourName + ", perhaps surrending to my chains? No matter. You may be more useful to me out there, as you can deliver many souls to my lair. Spread havoc throughout the land and return to me. There are matters we can further discuss." + sBare;
				}
			}
			else if ( myName == "Samhayne" )
			{
				sTitle = "The Glory of Poseidon";
				sText = "Greetings, " + yourName + ". I have heard much about you and your deeds throughout the many lands. If you have interest in sailing the high seas, I am willing to offer you items that will help you traverse the waves and face the creatures of the deep." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Greetings, " + yourName + ". Do you plan on sailing the high seas? If so, there is much glory to gain. Return to me if you have tales of bravery and courage. I would like to offer something to such a person." + sBare;
				}
			}
			else if ( myName == "Seggallion" )
			{
				sTitle = "The Life of a Pirate";
				sText = "Hail, " + yourName + ". I have heard many stories of your travels. You take what you want and let no one stand in your way. I like that. If you have interest in sailing the high seas, I am willing to offer you items that will help you traverse the waves and face the creatures of the deep. There are many ships out there that could be easily loosed of their cargo." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Hail, " + yourName + ". I have heard very little of your travels. You should go out there and take what you want and let no one stand in your way. Maybe if you have what it takes to draw from the riches of others, then we could talk a bit further. I may be able to help you." + sBare;
				}
			}
			else if ( myName == "Minax" )
			{
				sTitle = "The Mother of Evil";
				sText = "It appears that " + yourName + " has found my little lair wrapped in time. You may have heard that the Stranger had defeated me years ago. Although they did foil my plans, they unknowingly created a time paradox and allowed another version of me to exist onward. This brings me to your arrival. It seems that you have done some things throughout the land that have hindered my adversaries. For this, I offer you something for your actions. These items add great power to even the most novice of sorcerers." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "It appears that " + yourName + " has found my little lair wrapped in time. You may have heard that the Stranger had defeated me years ago. Although they did foil my plans, my prodigy Exodus has returned me to Sosaria to further my efforts. This brings me to question your arrival. If you have come here to seek tribute, you best look elsewhere as you have not done anything to cause my enemies to fall." + sBare;
				}
			}
			else if ( myName == "Nystal" )
			{
				sTitle = "The Watchful Eye";
				sText = "Greetings, " + yourName + " and welcome to the castle of Lord British. Forgive my room but I have not the time to clean as I have been very busy with my studies. I am always looking to help others explore the properties of magic, and from what I hear you are the trustworthy sort. Would you perhaps like a tribute for your good deeds? I have items that would help a wizard in their practice of magic." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Greetings, " + yourName + " and welcome to the castle of Lord British. Forgive my room but I have not the time to clean as I have been very busy with my studies. I am always looking to help others explore the properties of magic, but I am not sure you are a trustworthy sort. Maybe come back later when you have helped your fellow citizens more." + sBare;
				}
			}
			else if ( myName == "Lord British" )
			{
				sTitle = "Knights of Sosaria";
				sText = "Greetings, " + yourName + ". Your recent actions have been noted by my court and I would like to offer you an item that would show my personal gratitude. These items embrace the qualities of my chivalry and I feel that you may one day be one of them." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Greetings, " + yourName + ". I must admit that I have not heard much about you in these lands. Perhaps if you venture into the world and vanquish evil, then my knights my take notice. Return to me if your valor outweighs your greed and I will greatly reward you." + sBare;
				}
			}
			else if ( myName == "Lord Blackthorne" )
			{
				sTitle = "Price to Pay";
				sText = "Well if it isn't " + yourName + ". I have been trapped in this bottle for years and there are many that I want to thank personally, but I cannot do much within this glass prison. I have many agents seeking items for me, and I have some enemies that need to be dispatched. You seem to have taken care of some of these issues without even knowing it. I would like to give you something that an assassin would appreciate, if your trade moves in that direction." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Well if it isn't " + yourName + ". I have been trapped in this bottle for years and there are many that I want to thank personally, but I cannot do much within this glass prison. I have many agents seeking items for me, and I have some enemies that need to be dispatched. For those that further my cause, I do tend to take notice and reward appropriately." + sBare;
				}
			}
			else if ( myName == "Geoffrey" )
			{
				sTitle = "The Art of War";
				sText = "Greetings, " + yourName + ". I have been living in the castle for many years and served Lord British even longer than that. The word in the tavern is that you have done some really good deeds in your travels. This is good to hear as we need more champions in the realm. I would like to offer you something for your service. Something that could make you a better fighter." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Greetings, " + yourName + ". I have been living in the castle for many years and served Lord British even longer than that. The word in the tavern is that you have stayed to yourself and have failed to choose a side against good or evil. If your heart ever leads you to do what is right for the realm, come back and see me. I may have something to share with you." + sBare;
				}
			}
			else if ( myName == "Shimazu" )
			{
				sTitle = "The Way of Shogun";
				sText = "Hail, " + yourName + ". You have found my dojo. The does not surprise me since I have heard about your travels. If you seek to become master of ninjitsu, or simply one of the samurai, then I could maybe help you with some special items. What say you?" + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Hail, " + yourName + ". You have found my dojo, although I am uncertain how. This dojo is for those that faced danger and lived to tell the tale, not those that sit around drinking ale all day. Leave this place, before the stink of cowardice attracts others." + sBare;
				}
			}
			else if ( myName == "Gorn" )
			{
				sTitle = "Strength and Steel";
				sText = "Hail, " + yourName + ". I have ruled these islands for many years, but never had I heard such tales of bravery and courage than I have about you. If you are a true barbarian, I can make you something that would help you live off the land. Then you could travel even further, spreading your tales of victory to others." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Hail, " + yourName + ". I have ruled these islands for many years, but I have never heard of you. If you want to be a true barbarian, then you need to travel to the harshest regions of the world and face the mightiest of foes. Maybe next time I see you, you will be such a soul." + sBare;
				}
			}
			else if ( myName == "Jaana" )
			{
				sTitle = "The Healing Hand";
				sText = "Hail, " + yourName + ". I am one of the many healers of Sosaria and decided to live here since it is central to the land. I have not needed to heal you, which is suprising since I have heard of the many deeds you have done in the land. If you need to be better equipped to heal thyself, perhaps I can help." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Hail, " + yourName + ". I am one of the many healers of Sosaria and decided to live here since it is central to the land. I have not needed to heal you, but that is probably because you do not risk your life for a cause. If you take up a true life of adventure, come visit again if you need to be better equipped to heal thyself." + sBare;
				}
			}
			else if ( myName == "Dupre" )
			{
				sTitle = "Knights Against Evil";
				sText = "Greetings, " + yourName + ". I am Dupre and I have been traveling the many lands, vanquishing evil in all of its forms. I have heard of your valor in battle and your purity of spirit. You should perhaps embrace the life of chivalry, and join the battle against the undead. If you have the goal to bring light to the dark, just say so and another champion will join our ranks." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Greetings, " + yourName + ". I am Dupre and I have been traveling the many lands, vanquishing evil in all of its forms. You should perhaps embrace the life of chivalry, and join the battle against the undead. Nevertheless, I will keep my ears listening about you. If you can show me your mettle against the wicked dead of the world, I would be happy to call you friend." + sBare;
				}
			}
			else if ( myName == "Gwenno" )
			{
				sTitle = "Music to My Ears";
				sText = "Well met, " + yourName + ". I enjoy my music and tales of adventure, and I had sung many songs about you. If you perhaps seek to help others with the gift of music, I could perhaps help thee." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Well met, " + yourName + ". I enjoy my music and tales of adventure, but sadly I have no songs about your life. Perhaps you should return when you have stories of glory and I could then sing about your valor to others." + sBare;
				}
			}
			else if ( myName == "Iolo" )
			{
				sTitle = "The True Shot";
				sText = "Well met, " + yourName + ". I am a bard by night but a good archer by day. I make many bows for Lord British as he appreciates the quality, but that is all I seem to have time for in fletching. He has told me many stories about your conquests of valor and justice though. I am willing to help you as I can give you many items that would greatly help an archer." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Well met, " + yourName + ". I am a bard by night but a good archer by day. I make many bows for Lord British as he appreciates the quality, but that is all I seem to have time for in fletching. If you ever make a name for yourself, come back and visit. I may be able to make you a better archer." + sBare;
				}
			}
			else if ( myName == "Shamino" )
			{
				sTitle = "The Wonderful Woodlands";
				sText = "Well met, " + yourName + ". I am a simple woodsman but I have heard many tales of valor about you. I wish I could help in your pursuits, but all I could offer are items that would help you in the carpentry trade. Don't get me wrong, they can do much more than that. They are just items that would help you travel the forests in relative safety to be sure. If that interests you, let me know." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Well met, " + yourName + ". I am a simple woodsman and I have been living in Montor for many years. I usually like to help prospective woodsmen, but I usually aid those that have been seasoned in the ways of valor and kindness. Well, take care then." + sBare;
				}
			}
			else if ( myName == "Stefano" )
			{
				sTitle = "What is Yours is Mine";
				sText = "Well, well, well. You must be " + yourName + ". While others drink their ale I hear them tell stories of some pretty harsh things you have done in the realm. I must say I am quite impressed. If you could part with some gold, I could make an item for you that would help you acquire things easier than simply buying them. Are you interested?" + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "So you say you are called " + yourName + "? Never heard of ya. Maybe go out and steal something that makes the entire land whisper stories of the theft. If you can do that, then maybe we can talk. Otherwise, get lost!" + sBare;
				}
			}
			else if ( myName == "Katrina" )
			{
				sTitle = "Woodland Friends";
				sText = "Hello, " + yourName + ". I have been taming animals here for many years, but I have not achieved the glory your name has brought. If you are willing to learn the ways of animal taming, I could perhaps make something to help you with that goal. It can be a long journey but may be worth it later on if you don't want to travel alone." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Hello, " + yourName + ". I have been taming animals here for many years, but I have not heard of you. Come back and visit if you ever have stories to tell of great battles or evil vanquished. I would love to hear them." + sBare;
				}
			}
			else if ( myName == "the Guardian" )
			{
				sTitle = "The Black Gate";
				sText = "So you must be " + yourName + ". I have been watching your journey for quite some time. I am stuck here in Sosaria, looking for a way to get to the world of Pagan. Dupre and Lord British have been a thorn in my side, but I have many eager disciples to seek what I need or slay who I want elminated. You have done much in securing my success in this matter, as I am getting closer to having my black gate lead me to were I seek. Let me enhance your efforts, as I can conjure many items that could be of assistance to you." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "So you say you are called " + yourName + "? I have not heard of you, but I am stuck here in Sosaria looking for a way to get to the world of Pagan. Dupre and Lord British have been a thorn in my side, but I have many eager disciples to seek what I need or slay who I want elminated. Maybe you can venture forth and assist those that worship me, or simply create havoc to keep Dupre and Lord British busy. Return to me when you have shown me you deserve my attention." + sBare;
				}
			}
			else if ( myName == "Garamon" )
			{
				sTitle = "A Brother No More";
				sText = "Greetings, " + yourName + ". I am quite happy to see you as I have heard many stories of your exploits. Whether you know it or not, your efforts have slowed my brother Tyball down considerably. His attempt to enslave demons have only caused more to appear in the land and I have been trying to create elixirs to aid others in the fight against such evil. I would like to offer you tribute, as I have many items that would help you perpare potions as I do." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Greetings, " + yourName + ". I am happy to meet you as I feel you could perhaps help me against the insane practices my brother Tyball performs. His attempt to enslave demons have only caused more to appear in the land and I have been trying to create elixirs to aid others in the fight against such evil. There are other items and agents he seeks to further his cause. Return to me when you have reached glory throughout the land. I feel that I could part with something for you." + sBare;
				}
			}
			else if ( myName == "Mors Gotha" )
			{
				sTitle = "Death to the Righteous";
				sText = "Well met, " + yourName + ". The light of this world will eventually be vanquished due to your recent actions. People like you are quite rare. Rare enough you shouldn't have wreak havoc with what you got. How about I give you something to help you...spread the word of death? Alas I only have items that could really aid a death knight, but death knights are rare indeed." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "What do you want, " + yourName + "? We don't need the likes of you roaming around Umbra! Perhaps you should make your way to the surface and show us you are worthy of the darkness!" + sBare;
				}
			}
			else if ( myName == "Lethe" )
			{
				sTitle = "The Shallow Grave";
				sText = "Well, well, well. I see that " + yourName + " has come to pursue knowledge in the resting places of the dead. You have been quite busy, aiding to my plans like a worthy servant. I suppose a tribute to your efforts are in order. I can offer items that would help those that explore death and molest the graves of others." + sInfo + sBare;

				if ( allowed == false )
				{
					sText = "Well, well, well. I see that " + yourName + " has come to pursue knowledge in the resting places of the dead. But alas you have failed to further my efforts against the living. Go forth, and return when you have dispatched those that oppose me." + sBare;
				}
			}

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(500, 300, 155);
			AddImage(500, 0, 155);
			AddImage(0, 0, 155);
			AddImage(300, 0, 155);
			AddImage(0, 300, 155);
			AddImage(300, 300, 155);
			AddImage(2, 2, 129);
			AddImage(300, 2, 129);
			AddImage(2, 298, 129);
			AddImage(300, 298, 129);
			AddImage(7, 8, 145);
			AddImage(167, 20, 132);
			AddImage(94, 535, 130);
			AddImage(498, 2, 129);
			AddImage(466, 20, 132);
			AddHtml( 175, 47, 544, 26, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + sTitle + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddImage(498, 298, 129);
			AddImage(388, 364, 136);
			AddImage(758, 17, 143);
			AddImage(81, 518, 141);
			AddImage(6, 345, 148);
			AddHtml( 175, 89, 545, 329, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			AddImage(353, 524, 156);
			AddImage(346, 527, 156);
			AddImage(349, 529, 159);
        }
    }
}