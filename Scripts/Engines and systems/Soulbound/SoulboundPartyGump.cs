using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.PartySystem;

namespace Server.Gumps
{
	public class SoulboundPartyGump : Gump
	{
		private Phylactery m_Phylactery;
		
		public SoulboundPartyGump( Mobile from, Phylactery phylactery, int page ) : base( 0, 0 )
		{
			if(from != null) {
				from.CloseGump( typeof( SoulboundPartyGump ) );
			}
			from.SendSound( 0x55 );
			if(phylactery != null)
			{				
				m_Phylactery = phylactery;
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;	

				AddPage(0);
				AddImage(40, 20, 152);
				AddImage(325, 20, 152);
				AddImage(20, 82, 152);
				AddImage(305, 82, 152);
				AddImage(303, 22, 129);
				AddImage(303, 80, 129);
				AddImage(22, 80, 129);
				AddImage(22, 22, 129);
				AddImage(27, 27, 145);
				AddImage(187, 27, 140);
				AddImage(263, 27, 140);
				AddImage(563, 29, 143);
				AddImage(67, 314, 130);
				AddImage(194, 143, 136);
				AddImage(39, 309, 159);
				AddHtml( 196, 53, 344, 28, @"<BODY><BASEFONT Color=#FBFBFB><BIG>RECEIVE A SOULBOUND PARTY BUFF</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				this.AddHtml( 246, 80, 320, 30, @"<BODY><BASEFONT Color=#FCFF00><BIG>Give me a protector role buff</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				this.AddButton(196, 75, 2151, 2153, 1, GumpButtonType.Reply, 0);
	
				this.AddHtml( 246, 110, 320, 30, @"<BODY><BASEFONT Color=#FCFF00><BIG>Give me a spell damage role buff</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(196, 105, 2151, 2153, 2, GumpButtonType.Reply, 0);
		
				this.AddHtml( 246, 140, 320, 30, @"<BODY><BASEFONT Color=#FCFF00><BIG>Give me a supporting role buff</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(196, 135, 2151, 2153, 3, GumpButtonType.Reply, 0);
			
				this.AddHtml( 246, 170, 320, 30, @"<BODY><BASEFONT Color=#FCFF00><BIG>Give me a physical damage role buff</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(196, 165, 2151, 2153, 4, GumpButtonType.Reply, 0);				       
			}
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			PlayerMobile player = (PlayerMobile)state.Mobile;
			if(player != null && m_Phylactery != null )
			{	
				List<PhylacteryMod> mods = new List<PhylacteryMod>();
				int baseOffset = 0;
				switch (info.ButtonID) {
					case 1: // resistance buff
						mods = new List<PhylacteryMod>() {
							 new PhylacteryMod("EarthEssence", baseOffset),
							 new PhylacteryMod("PlantEssence", baseOffset),
							 new PhylacteryMod("BearEssence", baseOffset),
							 new PhylacteryMod("ThornEssence", baseOffset)	 
						};						
					break;
					case 2: // spellcasting buff
						mods = new List<PhylacteryMod>() {
							 new PhylacteryMod("DemonicEssence", baseOffset),
							 new PhylacteryMod("PixieEssence", baseOffset),
							 new PhylacteryMod("CelestialEssence", baseOffset),
							 new PhylacteryMod("PlanarEssence", baseOffset),
							 new PhylacteryMod("GazerEssence", baseOffset),
							 new PhylacteryMod("EnergyEssence", baseOffset)	 	 
						};
					break;
					case 3: // weapon weilding buff
						mods = new List<PhylacteryMod>() {
							 new PhylacteryMod("WaterEssence", baseOffset),
							 new PhylacteryMod("ImpEssence", baseOffset),
							 new PhylacteryMod("ColdEssence", baseOffset),	 
							 new PhylacteryMod("SageEssence", baseOffset)	 
						};
					break;
					case 4: // regeneration buff
						mods = new List<PhylacteryMod>() {
							 new PhylacteryMod("SnakeEssence", baseOffset),
							 new PhylacteryMod("EagleEssence", baseOffset),
							 new PhylacteryMod("TitanEssence", baseOffset),
							 new PhylacteryMod("FireEssence", baseOffset)	 
						};	
					break;
				}
				m_Phylactery.PhylacteryMods = mods;
				Party party = (Party)player.Party;
				if (party != null) {
					party.UpdateSoulboundBuffs();
				}
			} 
		}
	}
}
