using System; 
using System.Collections.Generic; 
using System.Collections;
using Server.Items; 
using Server.Misc;

namespace Server.Mobiles 
{ 
	public class SBLegendary: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBLegendary() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			
            private double baseprice = 1500000;
            private double multiple = 1;
            public InternalBuyInfo()
            { 
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Alchemy), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Anatomy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_AnimalLore), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_AnimalTaming), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Archery), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_ArmsLore), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Blacksmith), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Fletching), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Bushido), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Carpentry), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Cartography), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Chivalry), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Cooking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_DetectHidden), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Discordance), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_EvalInt), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Fencing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Fishing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Focus), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Healing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Inscribe), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Lockpicking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Hiding), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Lumberjacking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Macing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Magery), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_MagicResist), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Meditation), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Mining), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Musicianship), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Necromancy), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Ninjitsu), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Parry), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Peacemaking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Poisoning), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Provocation), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_RemoveTrap), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Snooping), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_SpiritSpeak), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Stealing), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Stealth), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Swords), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Tactics), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Tailoring), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SL_Tinkering), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SL_Tracking), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Veterinary), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SL_Wrestling), (int)(baseprice* multiple), 1, 0x14F0, 0x481)); }
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Alchemy ), 50000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Anatomy ), 70000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_AnimalLore ), 50000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_AnimalTaming ), 70000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Archery ), 70000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_ArmsLore ), 50000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Blacksmith ), 50000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Fletching ), 50000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Bushido ), 50000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Carpentry ), 50000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Cartography ), 60000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SL_Chivalry ), 70000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Cooking ), 60000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SL_DetectHidden ), 60000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SL_Discordance ), 70000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_EvalInt ), 150000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Fencing ), 150000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Fishing ), 50000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Focus ), 65000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Healing ), 80000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Inscribe ), 50000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SL_Lockpicking ), 80000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SL_Hiding ), 80000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Lumberjacking ), 50000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Macing ), 100000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Magery ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_MagicResist ), 80000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Meditation ), 80000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SL_Mining ), 80000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Musicianship ), 80000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Necromancy ), 50000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Ninjitsu ), 80000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Parry ), 25000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SL_Peacemaking ), 10000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SL_Poisoning ), 60000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Provocation ), 50000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_RemoveTrap ), 25000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SL_Snooping ), 25000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_SpiritSpeak ), 25000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Stealing ), 50000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Stealth ), 80000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Swords ), 25000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Tactics ), 50000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Tailoring ), 50000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Tinkering ), 25000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SL_Tracking ), 15000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Veterinary ), 50000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SL_Wrestling ), 50000  );}
			} 
		} 
	} 
}
