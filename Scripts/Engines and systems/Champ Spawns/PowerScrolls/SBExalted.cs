using System; 
using System.Collections.Generic; 
using System.Collections;
using Server.Items; 
using Server.Misc;

namespace Server.Mobiles 
{ 
	public class SBExalted: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBExalted() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			
                private double baseprice = 90000;
                private double multiple = 1;
            public InternalBuyInfo()
            { 
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Alchemy), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Anatomy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_AnimalLore), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_AnimalTaming), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Archery), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_ArmsLore), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Blacksmith), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Fletching), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Bushido), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Carpentry), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Cartography), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Chivalry), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Cooking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_DetectHidden), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Discordance), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_EvalInt), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Fencing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Fishing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Focus), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Healing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Inscribe), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Lockpicking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Hiding), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Lumberjacking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Macing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Magery), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_MagicResist), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Meditation), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Mining), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Musicianship), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Necromancy), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Ninjitsu), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Parry), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Peacemaking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Poisoning), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Provocation), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_RemoveTrap), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Snooping), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_SpiritSpeak), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Stealing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Stealth), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Swords), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Tactics), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Tailoring), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SE_Tinkering), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SE_Tracking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Veterinary), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SE_Wrestling), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Alchemy ), 2000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Anatomy ), 4000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_AnimalLore ), 4000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_AnimalTaming ), 10000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Archery ), 10000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_ArmsLore ), 4000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Blacksmith ), 2000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Fletching ), 2000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Bushido ), 2000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Carpentry ), 2000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Cartography ), 4000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SE_Chivalry ), 10000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Cooking ), 2000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SE_DetectHidden ), 2000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SE_Discordance ), 10000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_EvalInt ), 16000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Fencing ), 16000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Fishing ), 4000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Focus ), 4000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Healing ), 16000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Inscribe ), 4000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SE_Lockpicking ), 10000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SE_Hiding ), 10000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Lumberjacking ), 4000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Macing ), 16000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Magery ), 25000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_MagicResist ), 10000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Meditation ), 4000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SE_Mining ), 10000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Musicianship ), 10000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Necromancy ), 4000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Ninjitsu ), 10000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Parry ), 16000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SE_Peacemaking ), 1600 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SE_Poisoning ), 10000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Provocation ), 4000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_RemoveTrap ), 2000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SE_Snooping ), 2000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_SpiritSpeak ), 2000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Stealing ), 4000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Stealth ), 10000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Swords ), 2000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Tactics ), 16000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Tailoring ), 4000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Tinkering ), 2000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SE_Tracking ), 1600 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Veterinary ), 4000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SE_Wrestling ), 4000 );}
			} 
		} 
	} 
}
