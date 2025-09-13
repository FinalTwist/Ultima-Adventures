using System;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.Quests;
using Server.Misc;

namespace Server.Mobiles
{
    public class SBStudyBookbinder : SBInfo
    {
        private readonly List<GenericBuyInfo> m_BuyInfo;
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBStudyBookbinder(Mobile m)
        {
            if (m != null)
            {
                m_BuyInfo = new InternalBuyInfo(m);
            }
        }

        public override IShopSellInfo SellInfo
        {
            get
            {
                return m_SellInfo;
            }
        }
        public override List<GenericBuyInfo> BuyInfo
        {
            get
            {
                return m_BuyInfo;
            }
        }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo(Mobile m)
            {
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardArmsLoreStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardBeggingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardCampingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardCartographyStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardForensicsStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardItemIDStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardTasteIDStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardAnatomyStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardArcheryStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardFencingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardFocusStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardHealingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardMacingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardParryStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardSwordsStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardTacticsStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardThrowingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardWrestlingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardAlchemyStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardBlacksmithStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardFletchingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardCarpentryStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardCookingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardInscribeStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardLumberjackingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardMiningStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardTailoringStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardTinkeringStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardBushidoStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardChivalryStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardEvalIntStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				//if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardImbuingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardMageryStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardMeditationStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				//if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardMysticismStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardNecromancyStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardNinjitsuStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardMagicResistStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				//if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardSpellweavingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardSpiritSpeakStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardAnimalLoreStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardAnimalTamingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardFishingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardHerdingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardTrackingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardVeterinaryStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardDetectHiddenStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardHidingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardLockpickingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardPoisoningStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardRemoveTrapStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardSnoopingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardStealingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardStealthStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardDiscordanceStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardMusicianshipStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellChance() ) {Add(new GenericBuyInfo(typeof(StandardPeacemakingStudyBook), 7500, 10, 0x225A, 0x1BA)); }
				if ( MyServerSettings.SellRareChance() ) {Add(new GenericBuyInfo(typeof(StandardProvocationStudyBook), 7500, 10, 0x225A, 0x1BA)); }
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
            }
        }
    }
}