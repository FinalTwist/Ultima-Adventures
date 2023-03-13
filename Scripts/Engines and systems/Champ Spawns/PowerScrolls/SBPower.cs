using System; 
using System.Collections.Generic; 
using System.Collections;
using Server.Items; 
using Server.Misc;

namespace Server.Mobiles 
{ 
	public class SBPower: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBPower() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{
            private double baseprice = 5000000;
            private double multiple = 1;

            public InternalBuyInfo() 
			{
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Alchemy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Anatomy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_AnimalLore), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_AnimalTaming), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Archery), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_ArmsLore), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Blacksmith), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Fletching), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Bushido), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Carpentry), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Cartography), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Chivalry), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Cooking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_DetectHidden), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Discordance), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_EvalInt), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Fencing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Fishing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Focus), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Healing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Inscribe), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Lockpicking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Hiding), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Lumberjacking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Macing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Magery), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_MagicResist), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Meditation), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Mining), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Musicianship), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Necromancy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Ninjitsu), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Parry), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Peacemaking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Poisoning), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Provocation), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_RemoveTrap), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Snooping), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_SpiritSpeak), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Stealing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Stealth), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Swords), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Tactics), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Tailoring), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellVeryRareChance()) {multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SP_Tinkering), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SP_Tracking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Veterinary), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
                if (MyServerSettings.SellRareChance()) {multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SP_Wrestling), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            } 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Alchemy ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Anatomy ), 370000 ) ;}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_AnimalLore ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_AnimalTaming ), 370000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Archery ), 370000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_ArmsLore ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Blacksmith ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Fletching ), 250000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Bushido ), 250000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Carpentry ), 250000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Cartography ), 260000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SP_Chivalry ), 370000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Cooking ), 260000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SP_DetectHidden ), 260000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SP_Discordance ), 370000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_EvalInt ), 550000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Fencing ), 450000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Fishing ), 250000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Focus ), 365000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Healing ), 480000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Inscribe ), 250000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SP_Lockpicking ), 480000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Lumberjacking ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Macing ), 500000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Magery ), 650000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_MagicResist ), 480000 ) ;}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Meditation ), 480000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SP_Mining ), 480000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Musicianship ), 480000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Necromancy ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Hiding ), 250000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Ninjitsu ), 380000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Parry ), 250000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SP_Peacemaking ), 110000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SP_Poisoning ), 260000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Provocation ), 350000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_RemoveTrap ), 125000  );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SP_Snooping ), 125000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_SpiritSpeak ), 125000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Stealing ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Stealth ), 380000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Swords ), 125000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Tactics ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Tailoring ), 250000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Tinkering ), 125000  );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SP_Tracking ), 115000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Veterinary ), 250000  );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SP_Wrestling ), 250000  );}
			} 
		} 
	} 
}
