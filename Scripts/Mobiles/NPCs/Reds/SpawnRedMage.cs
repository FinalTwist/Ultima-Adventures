using System; 
using Server;
using System.Collections.Generic; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 
using Server.Targeting;
using Server.Spells.Fifth;
using System.Collections; 

namespace Server.Mobiles 
{ 
	public class SpawnRedMage : BaseRed
	{ 
		private bool m_dispelling;
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
		public DateTime m_NextTalk;

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			#region Tass23/Raist
			if ( !( m is PlayerMobile ) )
				return;
			if ((m is PlayerMobile) && (m.AccessLevel == AccessLevel.Player))
			{
				if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) )
				{
					if ( DateTime.UtcNow >= m_NextTalk && m is PlayerMobile && (this.Combatant == null || this.Combatant == m) ) // check if its time to talk
					{
						if (m.Kills < 5)
						{
							m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
							switch (Utility.Random(9))
							{
						case 0: Say("Hah " + m.Name + " I'm gonna get you"); break;
						case 1: Say("" + m.Name + "!!!"); break;
						case 2: Say("" + m.Name + " where do you think your going?"); break;
						case 3: Say("Hey " + m.Name + ", I'll eat your entrails"); break;
						case 4: Say("Found a Blue!"); break;
						case 5: Say("" + m.Name + ", you better run"); break;
						case 6: Say("Die, " + m.Name + "."); break;
						case 7: Say("" + m.Name + "!!"); break;
						case 8: Say("hahahah a noob !"); break;
						case 9: Say("I'm 1337 D0od.  You're n0t."); break;
						case 10: Say("Pfft.... Carebear, go back to tram."); break;
						case 11: Say("Don't even try and run N00B."); break;
							}
						}
						else 
						{
							m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
							switch (Utility.Random(9))
							{
								case 0: Say("Hah " + m.Name + " You think you're one of us?"); break;
								case 1: Say("" + m.Name + "!!!"); break;
								case 2: Say("" + m.Name + ", your crossbow sucks ass"); break;
								case 3: Say("Hey " + m.Name + ", I could take you on anyday"); break;
								case 4: Say("Just wait till you get below 5 kills!"); break;
								case 5: Say("" + m.Name + ", you're nothing, i have more kills than you!"); break;
								case 6: Say("hey, " + m.Name + "."); break;
								case 7: Say("" + m.Name + " fuck the blues"); break;
								case 8: Say("you're still a noob"); break;
							}
						}
					}
				}
			}
			#endregion
		}
		[Constructable] 
public SpawnRedMage() : base( AIType.AI_Mage, FightMode.Closest, 25, 1, 0.4, 0.3 ) 
{ 
	



            		Title = "[KIL-Mage]";
			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
			
				
			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" ); 


			}
//			Name.Hue = 2002;
//			Title.Hue = 2002;

 
       	    SetStr(80, 100);
            SetDex(60, 100);
            SetInt(400, 500);
	    ActiveSpeed = 0.2;
	    PassiveSpeed = 0.1;

            SetHits(150, 200);

            //SetDamage(18, 22);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 40, 60);
            SetResistance(ResistanceType.Fire, 40, 60);
            SetResistance(ResistanceType.Cold, 40, 60);
            SetResistance(ResistanceType.Poison, 40, 60);
            SetResistance(ResistanceType.Energy, 40, 60);

            SetSkill(SkillName.Swords, 89.0, 120.0);
            SetSkill(SkillName.Tactics, 89.0, 120.0);
            SetSkill(SkillName.MagicResist, 89.0, 120.0);
            SetSkill(SkillName.Tactics, 89.0, 120.0);
            SetSkill(SkillName.Parry, 89.0, 120.0);
            SetSkill(SkillName.Anatomy, 85.0, 120.0);
            SetSkill(SkillName.Healing, 85.0, 120.0);
            SetSkill(SkillName.Magery, 95.0, 120.0);
            SetSkill(SkillName.EvalInt, 95.0, 120.0);

            Fame = 9500;
            Karma = -9500;
			Criminal = true;
			Kills = Utility.RandomMinMax(5, 20);

            VirtualArmor = 40;

                           	 switch (Utility.Random(4))
                           	 {
                           	 	case 0: BattleAxe weapona = new BattleAxe();
						weapona.Hue = 1633;
                      				weapona.LootType = LootType.Newbied;
						weapona.Attributes.SpellChanneling = 1;
						weapona.WeaponAttributes.HitLeechMana = 60;
                      				weapona.Movable = false;
						AddItem( weapona );
						break;
                           	 	case 1: Axe weaponb = new Axe();
						weaponb.Hue = 1633;
                      				weaponb.LootType = LootType.Newbied;
						weaponb.Attributes.SpellChanneling = 1;
						weaponb.WeaponAttributes.HitLeechMana = 60;
                      				weaponb.Movable = false;
						AddItem( weaponb );
						break;
                           	 	case 2: Bardiche weaponc = new Bardiche();
						weaponc.Hue = 1633;
                      				weaponc.LootType = LootType.Newbied;
						weaponc.Attributes.SpellChanneling = 1;
						weaponc.WeaponAttributes.HitLeechMana = 60;
                      				weaponc.Movable = false;
						AddItem( weaponc );
						break;
                           	 	case 3: Hatchet weapond = new Hatchet();
						weapond.Hue = 1633;
                      				weapond.LootType = LootType.Newbied;
						weapond.Attributes.SpellChanneling = 1;
						weapond.WeaponAttributes.HitLeechMana = 60;
                      				weapond.Movable = false;
						AddItem( weapond );
						break;
                            	} 



                           	 Item Robe =  new Robe( );			
                        	Robe.Name = "Dastardly Cowl";
                        	//Robe.Movable = false; 
				Robe.Hue = 1633;
				AddItem( Robe );



                           	 switch (Utility.Random(3))
                           	 {
                           	 	case 0: AddItem( new LongPants(1050)); break;
                           	 	case 1: Item LegsOfBane = new LeggingsOfBane();
						LegsOfBane.Hue = 1633;
                      				LegsOfBane.LootType = LootType.Newbied;
                      				LegsOfBane.Movable = false;
						AddItem( LegsOfBane );
						break;
                           	 	case 2: break;
                            	} 

                           	 switch (Utility.Random(3))
                           	 {
                           	 	case 0:	AddItem( new Boots() ); break;
                           	 	case 1: Item Sandals = new Sandals();
						Sandals.Hue = 1195;
                        			Sandals.LootType = LootType.Blessed;
                        			Sandals.Movable = false;
						AddItem( Sandals );
						break;
                           	 	case 2: break;
                            	} 


						Item PlateHelm = new PlateHelm();
						PlateHelm.Hue = 1633;
                      				PlateHelm.LootType = LootType.Newbied;
                      				PlateHelm.Movable = false;
						AddItem( PlateHelm );


                           	 switch (Utility.Random(3))
                           	 {
                           	 	case 0:	AddItem( new LeatherGloves() ); break;
                           	 	case 1: break;
                           	 	case 2: break;
                            	} 


			AddItem( new Shirt(743) );

			

			
			Utility.AssignRandomHair( this );

			for (int i = 0; i < 10; i++)
			{
				PackItem( new GreaterCurePotion() );
				PackItem( new GreaterHealPotion() );
				PackItem( new TotalRefreshPotion() );
			}

			PackItem(new Bandage(Utility.RandomMinMax(5, 20)));

			AddLoot( LootPack.Average );
			m_dispelling = false;
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool CanHeal { get { return true; } }

		public override void OnAfterSpawn()
		{
		    base.OnAfterSpawn();

		    if (!(Server.Mobiles.AnimalTrainer.IsNoMountRegion(this.Region) && MyServerSettings.NoMountsInCertainRegions()))
		    {
			new EtherealLlama().Rider = this;
		    }
		}

		public override void OnThink()
		{
			base.OnThink();

			// Dismount if in no-mount area
			if (this.Mounted && Server.Mobiles.AnimalTrainer.IsNoMountRegion(this.Region) && MyServerSettings.NoMountsInCertainRegions())
			{
				Server.Mobiles.AnimalTrainer.DismountPlayer(this);
			}

			foreach (Item item in GetItemsInRange(7))
			{
				if ( (item.ItemID == 0x3915 || item.ItemID == 0x3922) && !m_dispelling )
				{		
					m_dispelling = true;
					this.Say("Hah! a field!  pfft.");	
					Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerStateCallback ( Dispel ), new object[]{ item }  );
				}
			}

		}

		public void Dispel( object state )
		{
			
			if (this.Deleted || this == null)
				return;
				
			object[] states = (object[])state;

			Item field = (Item)states[0];
			Map map = this.Map;

			if ( map == null || map == Map.Internal || field == null )
				return;

			List<Item> targets = new List<Item>();

			if ( this.InLOS( field ) )
			{
				foreach (Item item in field.GetItemsInRange(5))
				{
					if ( (item.ItemID == 0x3915 || item.ItemID == 0x3922|| item.ItemID == 0x3967|| item.ItemID == 0x3979|| item.ItemID == 0x398C || item.ItemID == 0x3996) && item != field )
					{		
						targets.Add(item);
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item m = targets[i];
					m.Delete();
				}
				field.Delete();
				return;
			}
			this.m_dispelling = false;
		}

		public override void OnDeath(Container c)
		{
            base.OnDeath(c);
			if (Utility.RandomDouble() >= 0.95)
			{
				switch (Utility.Random(6))
					{
							case 0:
								c.DropItem(new PhoenixGloves());
								break;
							case 1:
								c.DropItem(new PhoenixGorget());
								break;
							case 2:
								c.DropItem(new PhoenixHelm());
								break;
							case 3:
								c.DropItem(new PhoenixLegs());
								break;
							case 4:
								c.DropItem(new PhoenixSleeves());
								break;
							case 5:
								c.DropItem(new PhoenixChest());
								break;
					}
			}
			if (Utility.RandomDouble() > 0.60)
					c.DropItem(new Gold(Utility.RandomMinMax(2500, 4000)));
		}


		public SpawnRedMage( Serial serial ) : base( serial ) 
		{ 
		} 



		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 
}   
