using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Gumps
{
	public class PhylacteryGump : Gump
	{
		private Phylactery m_Phylactery;

		private BloodLich m_BloodLich;
		
		public PhylacteryGump( Mobile from, Phylactery phylactery, BloodLich bloodLich, int page ) : base( 0, 0 )
		{
			if(from != null) {
				from.CloseGump( typeof( PhylacteryGump ) );
			}
			from.SendSound( 0x55 );
			m_BloodLich = bloodLich;
			if(phylactery != null)
			{	
				if (!phylactery.Temporary) 
				{
					m_Phylactery = phylactery;
					from.CantWalk=true;
					this.Closable=true;
					this.Disposable=true;
					this.Dragable=true;
					this.Resizable=false;	

					this.AddPage(0);
					this.AddImage(40,36,1100);

					if (page == 3)
					{

						this.AddButton(108, 50, 1057, 1057, 1003, GumpButtonType.Reply, 0);
						this.AddLabel(360, 60, 0, "Total essence power: " + phylactery.TotalPower().ToString() + "/" + phylactery.GetRawPoints(from).ToString());
						this.AddLabel(360, 90, 0, "Soul Items:");
						this.AddLabel(400, 125, 0, "White Shard: 7500 SF");
						this.AddButton(360, 120, 11320, 11320, 27, GumpButtonType.Reply, 0);
						this.AddLabel(400, 155, 0, "Prismatic Shard: 7500 SF");
						this.AddButton(360, 150, 11320, 11320, 28, GumpButtonType.Reply, 0);
						this.AddLabel(400, 185, 0, "Blood Rune: 2500 SF");
						this.AddButton(360, 180, 11320, 11320, 29, GumpButtonType.Reply, 0);
						this.AddLabel(400, 215, 0, "Royal Blood Urn: 35000 SF");
						this.AddButton(360, 210, 11320, 11320, 30, GumpButtonType.Reply, 0);
						this.AddLabel(360, 245, 0, "Cosmetic Items:");
						this.AddLabel(400, 275, 0, "Orc Soul: 25000 SF");
						this.AddButton(360, 270, 11320, 11320, 31, GumpButtonType.Reply, 0);

					}
					else if (page == 2) 
					{
						this.AddButton(108, 50, 1057, 1057, 1000, GumpButtonType.Reply, 0);
						this.AddButton(525, 50, 1058, 1058, 1002, GumpButtonType.Reply, 0);
						this.AddLabel(165, 60, 0, "Owl Essence: " + phylactery.OwlEssence.ToString() + "/" + phylactery.OwlEssenceMax.ToString());
						if (phylactery.OwlEssence < phylactery.OwlEssenceMax) {
							this.AddButton(290, 55, 2151, 2153, 15, GumpButtonType.Reply, 0);
						}
						this.AddLabel(140, 90, 0, "Pixie Essence: " + phylactery.PixieEssence.ToString() + "/" + phylactery.PixieEssenceMax.ToString());
						if (phylactery.PixieEssence < phylactery.PixieEssenceMax) {
							this.AddButton(290, 85, 2151, 2153, 16, GumpButtonType.Reply, 0);
						}
						this.AddLabel(140, 120, 0, "Planar Essence: " + phylactery.PlanarEssence.ToString() + "/" + phylactery.PlanarEssenceMax.ToString());
						if (phylactery.PlanarEssence < phylactery.PlanarEssenceMax) {
							this.AddButton(290, 115, 2151, 2153, 17, GumpButtonType.Reply, 0);
						}
						this.AddLabel(140, 150, 0, "Plant Essence: " + phylactery.PlantEssence.ToString() + "/" + phylactery.PlantEssenceMax.ToString());
						if (phylactery.PlantEssence < phylactery.PlantEssenceMax) {
							this.AddButton(290, 145, 2151, 2153, 18, GumpButtonType.Reply, 0);
						}
						this.AddLabel(140, 180, 0, "Sage Essence: " + phylactery.SageEssence.ToString() + "/" + phylactery.SageEssenceMax.ToString());
						if (phylactery.SageEssence < phylactery.SageEssenceMax) {
							this.AddButton(290, 175, 2151, 2153, 19, GumpButtonType.Reply, 0);
						}
						this.AddLabel(140, 210, 0, "Snake Essence: " + phylactery.SnakeEssence.ToString() + "/" + phylactery.SnakeEssenceMax.ToString());
						if (phylactery.SnakeEssence < phylactery.SnakeEssenceMax) {
							this.AddButton(290, 205, 2151, 2153, 20, GumpButtonType.Reply, 0);
						}
						this.AddLabel(140, 240, 0, "Thorn Essence: " + phylactery.ThornEssence.ToString() + "/" + phylactery.ThornEssenceMax.ToString());
						if (phylactery.ThornEssence < phylactery.ThornEssenceMax) {
							this.AddButton(290, 235, 2151, 2153, 21, GumpButtonType.Reply, 0);
						}
						this.AddLabel(140, 270, 0, "Titan Essence: " + phylactery.TitanEssence.ToString() + "/" + phylactery.TitanEssenceMax.ToString());
						if (phylactery.TitanEssence < phylactery.TitanEssenceMax) {
							this.AddButton(290, 265, 2151, 2153, 22, GumpButtonType.Reply, 0);
						}
						this.AddLabel(140, 300, 0, "Water Essence: " + phylactery.WaterEssence.ToString() + "/" + phylactery.WaterEssenceMax.ToString());
						if (phylactery.WaterEssence < phylactery.WaterEssenceMax) {
							this.AddButton(290, 295, 2151, 2153, 23, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 60, 0, "Vampire Essence: " + phylactery.VampireEssence.ToString() + "/" + phylactery.VampireEssenceMax.ToString());
						if (phylactery.VampireEssence < phylactery.VampireEssenceMax) {
							this.AddButton(495, 55, 2151, 2153, 24, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 90, 0, "Spring Essence: " + phylactery.SpringEssence.ToString() + "/" + phylactery.SpringEssenceMax.ToString());
						if (phylactery.SpringEssence < phylactery.SpringEssenceMax) {
							this.AddButton(530, 85, 2151, 2153, 25, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 120, 0, "Sacred Essence: " + phylactery.SacredEssence.ToString() + "/" + phylactery.SacredEssenceMax.ToString());
						if (phylactery.SacredEssence < phylactery.SacredEssenceMax) {
							this.AddButton(530, 115, 2151, 2153, 26, GumpButtonType.Reply, 0);
						}
					} 
					else 
					{
						this.AddButton(525, 50, 1058, 1058, 1001, GumpButtonType.Reply, 0);
						this.AddLabel(140, 60, 0, "Available Soul Force: " + ((PlayerMobile)from).SoulForce.ToString() );
						this.AddLabel(140, 80, 0, "Soul Force Costs:"); 
						this.AddLabel(140, 100, 0, "Gain Level:  " + phylactery.SoulForceRequiredForGain(from, "BaseSoulForcePowerCost").ToString() );
						this.AddLabel(140, 120, 0, "Gain Essence: " + phylactery.SoulForceRequiredForGain(from, "BaseSoulForceEssenceCost").ToString() );
						this.AddLabel(140, 140, 0, "Gain Luck:  " + phylactery.SoulForceRequiredForGain(from, "BaseSoulForceLuckCost").ToString() );
						this.AddLabel(140, 170, 0, "Vault Level: " + phylactery.PowerLevel.ToString() + "/" + phylactery.PowerLevelMax.ToString());
						if (phylactery.PowerLevel < phylactery.PowerLevelMax) {
							this.AddButton(290, 165, 2151, 2153, 1, GumpButtonType.Reply, 0);
							// this.AddLabel(300, 161, 0, "+");
						}
						this.AddLabel(140, 200, 0, "Cold Essence: " + phylactery.ColdEssence.ToString() + "/" + phylactery.ColdEssenceMax.ToString());
						if (phylactery.ColdEssence < phylactery.ColdEssenceMax) {
							this.AddButton(290, 195, 2151, 2153, 2, GumpButtonType.Reply, 0);
							// this.AddLabel(165, 218, 0, "Add cold");
						}
						this.AddLabel(140, 230, 0, "Earth Essence: " + phylactery.EarthEssence.ToString() + "/" + phylactery.EarthEssenceMax.ToString());
						if (phylactery.EarthEssence < phylactery.EarthEssenceMax) {
							this.AddButton(290, 225, 2151, 2153, 3, GumpButtonType.Reply, 0);
							// this.AddLabel(165, 258, 0, "Add earth");
						}
						this.AddLabel(140, 260, 0, "Energy Essence: " + phylactery.EnergyEssence.ToString() + "/" + phylactery.EnergyEssenceMax.ToString());
						if (phylactery.EnergyEssence < phylactery.EnergyEssenceMax) {
							this.AddButton(290, 255, 2151, 2153, 4, GumpButtonType.Reply, 0);
							// this.AddLabel(104, 228, 0, "Add point");
						}			
						this.AddLabel(140, 290, 0, "Fire Essence: " + phylactery.FireEssence.ToString() + "/" + phylactery.FireEssenceMax.ToString());
						if (phylactery.FireEssence < phylactery.FireEssenceMax) {
							this.AddButton(290, 285, 2151, 2153, 5, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 60, 0, "Bear Essence: " + phylactery.BearEssence.ToString() + "/" + phylactery.BearEssenceMax.ToString());
						if (phylactery.BearEssence < phylactery.BearEssenceMax) {
							this.AddButton(495, 55, 2151, 2153, 6, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 90, 0, "Scorpion Essence: " + phylactery.ScorpionEssence.ToString() + "/" + phylactery.ScorpionEssenceMax.ToString());
						if (phylactery.ScorpionEssence < phylactery.ScorpionEssenceMax) {
							this.AddButton(530, 85, 2151, 2153, 7, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 120, 0, "Celestial Essence: " + phylactery.CelestialEssence.ToString() + "/" + phylactery.CelestialEssenceMax.ToString());
						if (phylactery.CelestialEssence < phylactery.CelestialEssenceMax) {
							this.AddButton(530, 115, 2151, 2153, 8, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 150, 0, "Demonic Essence: " + phylactery.DemonicEssence.ToString() + "/" + phylactery.DemonicEssenceMax.ToString());
						if (phylactery.DemonicEssence < phylactery.DemonicEssenceMax) {
							this.AddButton(530, 145, 2151, 2153, 9, GumpButtonType.Reply, 0);
						}
						
						this.AddLabel(360, 180, 0, "Eagle Essence: " + phylactery.EagleEssence.ToString() + "/" + phylactery.EagleEssenceMax.ToString());
						if (phylactery.EagleEssence < phylactery.EagleEssenceMax) {
							this.AddButton(530, 175, 2151, 2153, 10, GumpButtonType.Reply, 0);
						}
						this.AddLabel(360, 210, 0, "Gazer Essence: " + phylactery.GazerEssence.ToString() + "/" + phylactery.GazerEssenceMax.ToString());
						if (phylactery.GazerEssence < phylactery.GazerEssenceMax) {
							this.AddButton(530, 205, 2151, 2153, 11, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 240, 0, "Horse Essence: " + phylactery.HorseEssence.ToString() + "/" + phylactery.HorseEssenceMax.ToString());
						if (phylactery.HorseEssence < phylactery.HorseEssenceMax) {
							this.AddButton(530, 235, 2151, 2153, 12, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 270, 0, "Imp Essence: " + phylactery.ImpEssence.ToString() + "/" + phylactery.ImpEssenceMax.ToString());
						if (phylactery.ImpEssence < phylactery.ImpEssenceMax) {
							this.AddButton(530, 265, 2151, 2153, 13, GumpButtonType.Reply, 0);
						}

						this.AddLabel(360, 300, 0, "Lucky Essence: " + phylactery.LuckyEssence.ToString() + "/" + phylactery.LuckyEssenceMax.ToString());
						if (phylactery.LuckyEssence < phylactery.LuckyEssenceMax) {
							this.AddButton(530, 295, 2151, 2153, 14, GumpButtonType.Reply, 0);
						}
					}
				} 
				else {
					m_BloodLich.SayTo(from, "This phylactery is too weak to peform a soul crafting ritual");
				}
				
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			PlayerMobile player = (PlayerMobile)state.Mobile;
			
			bool okay = false;
			foreach ( Mobile lich in player.GetMobilesInRange( 4 ) )
			{
				if ( lich is BloodLich )
					okay = true;
			}


			if(okay && player != null && m_Phylactery != null )
			{	
				if (info.ButtonID > 0) {
					int page = 1;
					if (info.ButtonID >= 27)
						page = 3;
					else if (info.ButtonID >= 15)
						page = 2;
					//(info.ButtonID >= 15) ? 2 : 1; 
					if (info.ButtonID >= 1000) {
						if (info.ButtonID == 1000)
							page = 1;
						if (info.ButtonID == 1001)
							page = 2;
						if (info.ButtonID == 1002)
							page = 3;
						if (info.ButtonID == 1003)
							page = 2;
						//page = (info.ButtonID == 1000) ? 1 : 2; 
						player.SendGump(new PhylacteryGump(player, m_Phylactery, m_BloodLich, page));
						return;
					} 
					int requiredSoulForce = 0; 
					// is it a soul item purchase?
					if (info.ButtonID > 26) {
						this.ProcessPurchase(info.ButtonID, player);		
					} else { // its an imbue
						this.ProcessImbue(info.ButtonID, player);
					}
					
					player.SendGump(new PhylacteryGump(player, m_Phylactery, m_BloodLich, page));
					player.SendSound( 0x19D );		
				} else {
					state.Mobile.CantWalk=false;
				}		
			} 
			else
			{
				player.SendMessage( "There is no Blood Lich nearby." ); 
				state.Mobile.CantWalk=false;
			}
		}

		public void ProcessImbue(int buttonID, PlayerMobile player) {
			string soulForcePropertyName = "BaseSoulForceEssenceCost";
			if (buttonID == 14) { // luck
				soulForcePropertyName = "BaseSoulForceLuckCost";
			} else if (buttonID == 1)  {// power
				soulForcePropertyName = "BaseSoulForcePowerCost";
			}
			int requiredSoulForce = m_Phylactery.SoulForceRequiredForGain(player, soulForcePropertyName);
			// soul items overwrite the above
			if (player.SoulForce >= requiredSoulForce) { 
				bool speak = (m_BloodLich.NextMockery < DateTime.UtcNow);
				// remove old stats first!
				// m_Phylactery.ResetOwnerStats(player);
				string message = "Here, have some of my blood";
				switch (buttonID) {
					//Power
					case 1:
					
						++m_Phylactery.PowerLevel;
						if (m_Phylactery.PowerLevel == 10)
							player.sbmaster = true;
					
					break;
					//ColdEssence
					case 2:
						if (m_Phylactery.ColdEssence >= m_Phylactery.ColdEssenceMax)
							return;
						++m_Phylactery.ColdEssence;
						message = "Frosty cold.";
					break;
					case 3:
						if (m_Phylactery.EarthEssence >= m_Phylactery.EarthEssenceMax)
							return;
						++m_Phylactery.EarthEssence;
						message = "Do you have any idea how many earth elementals I had to obliterate to find this essence.";
					break;
					//EnergyEssence
					case 4:
						if (m_Phylactery.EnergyEssence >= m_Phylactery.EnergyEssenceMax)
							return;
						++m_Phylactery.EnergyEssence;
						message = "I love the blood of a mage, don't you?";
					break;
					//FireEssence
					case 5:
						if (m_Phylactery.FireEssence >= m_Phylactery.FireEssenceMax)
							return;
						++m_Phylactery.FireEssence;
						message = "Toasty warm.";
					break;
					//BearEssence
					case 6:
						if (m_Phylactery.BearEssence >= m_Phylactery.BearEssenceMax)
							return;
						++m_Phylactery.BearEssence;
						message = "I went on a bear hunt";
					break;
					//ScorpionEssence
					case 7:
						if (m_Phylactery.ScorpionEssence >= m_Phylactery.ScorpionEssenceMax)
							return;
						++m_Phylactery.ScorpionEssence;
						message = "I hate scorpions.";
					break;
					//CelestialEssence
					case 8:
						if (m_Phylactery.CelestialEssence >= m_Phylactery.CelestialEssenceMax)
							return;
						++m_Phylactery.CelestialEssence;
						message = "Ack! Its too holy, away with it!";
					break;
					//DemonicEssence
					case 9:
						if (m_Phylactery.DemonicEssence >= m_Phylactery.DemonicEssenceMax)
							return;
						++m_Phylactery.DemonicEssence;	
						message = "The blood of my most recent summon, it didn't end well for " + NameList.RandomName( "demonic" ) + ".";
					break;
					//EagleEssence
					case 10:
						if (m_Phylactery.EagleEssence >= m_Phylactery.EagleEssenceMax)
							return;
						++m_Phylactery.EagleEssence;
						message = "It definitely saw you coming!";
					break;
					//GazerEssence
					case 11:
						if (m_Phylactery.GazerEssence >= m_Phylactery.GazerEssenceMax)
							return;
						++m_Phylactery.GazerEssence;
						message = "Careful, you don't want Mad Gazer Disease";
					break;
					//HorseEssence
					case 12:
						if (m_Phylactery.HorseEssence >= m_Phylactery.HorseEssenceMax)
							return;
						++m_Phylactery.HorseEssence;
						message = "Good with a side of skull worms";
					break;
					//ImpEssence
					case 13:
						if (m_Phylactery.ImpEssence >= m_Phylactery.ImpEssenceMax)
							return;
						++m_Phylactery.ImpEssence;
						message = "Poor " + NameList.RandomName( "imp" ) + ", it didn't listen to me.";
					break;						
					//LuckyEssence
					case 14:
						if (m_Phylactery.LuckyEssence >= m_Phylactery.LuckyEssenceMax)
							return;
						++m_Phylactery.LuckyEssence;
						message = "You take it, I don't need luck.";
					break;
					//OwlEssence
					case 15:
						if (m_Phylactery.OwlEssence >= m_Phylactery.OwlEssenceMax)
							return;
						++m_Phylactery.OwlEssence;
						message = "Don't start hooting, people will think you're crazy.";
					break;
					//PixieEssence
					case 16:
						if (m_Phylactery.PixieEssence >= m_Phylactery.PixieEssenceMax)
							return;
						++m_Phylactery.PixieEssence;
						message = "I love a gloomy Tinkerbell.";
					break;
					//PlanarEssence
					case 17:
						if (m_Phylactery.PlanarEssence >= m_Phylactery.PlanarEssenceMax)
							return;
						++m_Phylactery.PlanarEssence;
						message = "Falling into our plane.... now.";
					break;
					//PlantEssence
					case 18:
						if (m_Phylactery.PlantEssence >= m_Phylactery.PlantEssenceMax)
							return;
						++m_Phylactery.PlantEssence;
						message = "Cold off the back of a hairy Ent named " + NameList.RandomName( "trees" ) + ".";
					break;
					//SageEssence
					case 19:
						if (m_Phylactery.SageEssence >= m_Phylactery.SageEssenceMax)
							return;
						++m_Phylactery.SageEssence;
						message = "Sage blood, better than the herb.";
					break;
					//SnakeEssence
					case 20:
						if (m_Phylactery.SnakeEssence >= m_Phylactery.SnakeEssenceMax)
							return;
						++m_Phylactery.SnakeEssence;
						message = "I loot this snake oil from the white witches, the black ones are ok.";
					break;
					//ThornEssence
					case 21:
						if (m_Phylactery.ThornEssence >= m_Phylactery.ThornEssenceMax)
							return;
						++m_Phylactery.ThornEssence;
						message = "Ouch, you spiked me!";
					break;
					//TitanEssence
					case 22:
						if (m_Phylactery.TitanEssence >= m_Phylactery.TitanEssenceMax)
							return;
						++m_Phylactery.TitanEssence;
						message = "Titan flesh, a bit tough, but edible.";
					break;
					//WaterEssence
					case 23:
						if (m_Phylactery.WaterEssence >= m_Phylactery.WaterEssenceMax)
							return;
						++m_Phylactery.WaterEssence;
						message = "The tears of a monk. So salty. So delicious.";
					break;
					case 24:
						if (m_Phylactery.VampireEssence >= m_Phylactery.VampireEssenceMax)
							return;
						++m_Phylactery.VampireEssence;
						message = "Ah yes, the grinded tooth of a vampire.  Crunchy.";
					break;
					case 25:
						if (m_Phylactery.SpringEssence >= m_Phylactery.SpringEssenceMax)
							return;
						++m_Phylactery.SpringEssence;
						message = "Reguvenating water from the purest spring.";
					break;
					case 26:
						if (m_Phylactery.SacredEssence >= m_Phylactery.SacredEssenceMax)
							return;
						++m_Phylactery.SacredEssence;
						message = "A little piece of a sacred altar.  Swallow it whole.";
					break;
				}
				if (speak) {
					m_BloodLich.NextMockery = DateTime.UtcNow.AddSeconds(7);
					m_BloodLich.SayTo( player, message );  
				}
				player.SoulForce -= requiredSoulForce;
				m_Phylactery.UpdateOwnerSoul(player);
				m_Phylactery.SoulForceSpent += requiredSoulForce;
				m_Phylactery.UpdateSoulForceCost(soulForcePropertyName, m_Phylactery.SoulForceFraction(player, soulForcePropertyName), true);
				// Server.Items.CharacterDatabase.GetMySpellHue( player, 0 )
				player.FixedParticles( 0x374A, 10, 30, 5013, 18, 2, EffectLayer.Waist );
				player.PlaySound( 0x0FC );
			} else {
				m_BloodLich.SayTo( player, BloodLich.BadForceReply );
			}
		}

		public void ProcessPurchase(int buttonID, PlayerMobile player) {
			string message = "";
			int requiredSoulForce = 0;
			if (buttonID == 27 || buttonID == 28) {
				// white shard or prismatic shard
				requiredSoulForce = 7500;
			} else if (buttonID == 29) {
				// blood rune
				requiredSoulForce = 2500;
			} else if (buttonID == 30) {
				// royal blood urn
				requiredSoulForce = 35000;
			} else if (buttonID == 31 ) {
				// orc soul
				requiredSoulForce = 25000;
			}
			if (player.SoulForce >= requiredSoulForce && player.Backpack != null) { 
				bool speak = (m_BloodLich.NextMockery < DateTime.UtcNow);
				switch (buttonID) {
					case 27:
						// white shard
					 	WhiteShard playerWhiteShard = (WhiteShard)player.Backpack.FindItemByType(typeof(WhiteShard)); 
					 	if (playerWhiteShard != null) {
					 		m_BloodLich.SayTo(player, BloodLich.AlreadyHaveOne);
					 		return;
					 	}
						WhiteShard whiteShard = new WhiteShard();
						player.Backpack.DropItem(whiteShard);
						message = "And you thought healing wands were good!";
					break;
					case 28:
						// prismatic shard
						PrismaticShard playerPrismaticShard = (PrismaticShard)player.Backpack.FindItemByType(typeof(PrismaticShard)); 
					 	if (playerPrismaticShard != null) {
					 		m_BloodLich.SayTo(player, BloodLich.AlreadyHaveOne);
					 		return;
					 	}
						PrismaticShard prismaticShard = new PrismaticShard();
						player.Backpack.DropItem(prismaticShard);
						message = "Now you know why us liches never run out of mana!";
					break;
					case 29:
						// blood rune
					 	BloodRune playerBloodRune = (BloodRune)player.Backpack.FindItemByType(typeof(BloodRune)); 
					 	if (playerBloodRune != null) {
					 		m_BloodLich.SayTo(player, BloodLich.AlreadyHaveOne);
					 		return;
					 	}
						BloodRune bloodRune = new BloodRune();
						player.Backpack.DropItem(bloodRune);
						message = "I guess I will be seeing you more quickly next time?";
					break;
					case 30:
						// royal blood urn; 
						RoyalBloodUrn urn = new RoyalBloodUrn();
						player.Backpack.DropItem(urn);
						message = "You are brave, mortal, make sure to party and get prepared before using this deadly item.";
					break;
					case 31:
						// orc soul 
						OrcSoul playerOrcSoul = (OrcSoul)player.Backpack.FindItemByType(typeof(OrcSoul)); 
					 	if (playerOrcSoul != null) {
					 		m_BloodLich.SayTo(player, BloodLich.AlreadyHaveOne);
					 		return;
					 	}
						OrcSoul soul = new OrcSoul();
						player.Backpack.DropItem(soul);
						message = "Enjoy dressing up as an orc, not sure why you would want to, but there here you are.";
					break;
				}
				if (speak) {
					m_BloodLich.NextMockery = DateTime.UtcNow.AddSeconds(7);
					m_BloodLich.SayTo( player, message );  
				}
				player.SoulForce -= requiredSoulForce;
			} else {
				m_BloodLich.SayTo( player, BloodLich.BadForceReply );
			}
		}
		

	}
}
