using System; 
using System.Collections.Generic; 
using System.Collections;
using Server.Items; 
using Server.Misc;

namespace Server.Mobiles
{ 
    public class SBWonderous : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();


		public SBWonderous() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{
             private double baseprice = 12500;
             private double multiple = 1;

             public InternalBuyInfo() 
			{
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Alchemy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Anatomy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_AnimalLore), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_AnimalTaming), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Archery), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_ArmsLore), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Blacksmith), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Fletching), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Bushido), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Carpentry), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Cartography), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Chivalry), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Cooking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_DetectHidden), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Discordance), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_EvalInt), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Fencing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Fishing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Focus), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Healing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Inscribe), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Lockpicking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Hiding), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Lumberjacking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Macing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Magery), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_MagicResist), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Meditation), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Mining), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Musicianship), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Necromancy), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Ninjitsu), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Parry), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Peacemaking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Poisoning), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Provocation), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_RemoveTrap), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Snooping), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_SpiritSpeak), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Stealing), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Stealth), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Swords), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Tactics), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Tailoring), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellVeryRareChance() ){multiple = 3; Add(new GenericBuyInfo(typeof(DJ_SW_Tinkering), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if (MyServerSettings.SellChance()) { Add(new GenericBuyInfo(typeof(DJ_SW_Tracking), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Veterinary), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
            if ( MyServerSettings.SellRareChance() ){multiple = 2; Add(new GenericBuyInfo(typeof(DJ_SW_Wrestling), (int)(baseprice * multiple), 1, 0x14F0, 0x481)); }
        } 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Alchemy ), 1000 ) ;}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Anatomy ), 2000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_AnimalLore ), 2000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_AnimalTaming ), 5000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Archery ), 5000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_ArmsLore ), 2000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Blacksmith ), 1000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Fletching ), 1000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Bushido ), 1000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Carpentry ), 1000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Cartography ), 2000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SW_Chivalry ), 5000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Cooking ), 1000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SW_DetectHidden ), 1000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SW_Discordance ), 5000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_EvalInt ), 8000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Fencing ), 8000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Fishing ), 2000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Focus ), 2000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Healing ), 8000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Inscribe ), 2000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SW_Lockpicking ), 5000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Lumberjacking ), 2000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Macing ), 8000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Magery ), 10000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_MagicResist ), 5000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Meditation ), 2000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SW_Mining ), 5000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Musicianship ), 5000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Necromancy ), 2000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Hiding ), 5000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Ninjitsu ), 5000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Parry ), 8000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SW_Peacemaking ), 800 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SW_Poisoning ), 5000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Provocation ), 2000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_RemoveTrap ), 1000 );}
				if ( MyServerSettings.BuyChance() ){Add( typeof( DJ_SW_Snooping ), 1000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_SpiritSpeak ), 1000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Stealing ), 2000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Stealth ), 5000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Swords ), 1000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Tactics ), 8000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Tailoring ), 2000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Tinkering ), 1000 );}
				if ( MyServerSettings.BuyRareChance() ){Add( typeof( DJ_SW_Tracking ), 800 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Veterinary ), 2000 );}
				if ( MyServerSettings.BuyChance() ) {Add( typeof( DJ_SW_Wrestling ), 2000 );}
			} 
		} 
	} 
}
