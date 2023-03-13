using System; 
using System.Collections.Generic; 
using System.Collections;
using Server.Items; 
using Server.Misc;

namespace Server.Mobiles 
{ 
	public class SBMythical: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBMythical() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{
            private double baseprice = 350000;
            private double multiple = 1;

            public InternalBuyInfo() 
			{
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Alchemy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Anatomy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_AnimalLore), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_AnimalTaming), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Archery), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_ArmsLore), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Blacksmith), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Fletching), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Bushido), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Carpentry), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Cartography), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Chivalry), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Cooking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_DetectHidden), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Discordance), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_EvalInt), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Fencing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Fishing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Focus), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Healing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Inscribe), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Lockpicking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Hiding), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Lumberjacking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Macing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Magery), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_MagicResist), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Meditation), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Mining), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Musicianship), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Necromancy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Ninjitsu), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Parry), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Peacemaking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Poisoning), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Provocation), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_RemoveTrap), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Snooping), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_SpiritSpeak), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Stealing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Stealth), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Swords), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Tactics), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Tailoring), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SM_Tinkering), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SM_Tracking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Veterinary), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SM_Wrestling), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            } 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Alchemy ), 20000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Anatomy ), 40000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_AnimalLore ), 40000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_AnimalTaming ), 100000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Archery ), 100000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_ArmsLore ), 40000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Blacksmith ), 20000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Fletching ), 20000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Bushido ), 20000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Carpentry ), 20000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Cartography ), 40000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SM_Chivalry ), 100000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Cooking ), 20000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SM_DetectHidden ), 20000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SM_Discordance ), 100000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_EvalInt ), 100000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Fencing ), 100000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Fishing ), 40000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Focus ), 40000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Healing ), 100000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Inscribe ), 40000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SM_Lockpicking ), 80000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SM_Hiding ), 80000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Lumberjacking ), 40000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Macing ), 100000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Magery ), 150000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_MagicResist ), 80000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Meditation ), 40000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SM_Mining ), 80000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Musicianship ), 80000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Necromancy ), 40000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Ninjitsu ), 80000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Parry ), 100000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SM_Peacemaking ), 25000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SM_Poisoning ), 60000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Provocation ), 40000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_RemoveTrap ), 20000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SM_Snooping ), 20000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_SpiritSpeak ), 20000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Stealing ), 40000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Stealth ), 80000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Swords ), 20000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Tactics ), 100000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Tailoring ), 40000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Tinkering ), 20000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SM_Tracking ), 16000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Veterinary ), 40000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SM_Wrestling ), 40000  );}
			} 
		} 
	} 
}
