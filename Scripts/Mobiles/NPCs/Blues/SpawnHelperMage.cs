using System; 
using Server;
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 
using Server.Targeting;

namespace Server.Mobiles 
{ 
	public class SpawnHelperMage : BaseBlue
	{ 
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
					if ( !m.Hidden && this.Combatant == null && DateTime.UtcNow >= m_NextTalk ) // check if its time to talk
					{
						m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
						switch (Utility.Random(19))
						{
							case 0: Emote("Hello " + m.Name + " have you come to play?"); break;
							case 1: Emote("" + m.Name + "?"); break;
							case 2: Emote("" + m.Name + " where do you think your going?"); break;
							case 3: Emote("Hey " + m.Name + " want to play tag?"); break;
							case 4: Emote(" Hey " + m.Name + " , did you hear about Moonglow?"); break;
							case 5: Emote("" + m.Name + " How are ya?"); break;
							case 6: Emote("To adventure, " + m.Name + "."); break;
							case 7: Emote("" + m.Name + "!!"); break;
							case 8: Emote("Hi " + m.Name + "!"); break;
							case 9: Emote("Oh Hi " + m.Name + "!"); break;
							case 10: Emote("Nice Weapon there " + m.Name + ""); break;
							case 11: Emote("Can't wait for the next Patch"); break;
							case 12: Emote("You looking at me, " + m.Name + "?"); break;
							case 13: Emote("Nice name " + m.Name + ""); break;
							case 14: Emote("I killed 10 succubus at once yesterday " + m.Name + ""); break;
							case 15: Emote("Hey, I didn't know you still played " + m.Name + ""); break;
							case 16: Emote("I got this."); break;
							case 17: Emote("I hate arties, t2a was the best, don't you think so " + m.Name + "?"); break;
							case 18: Emote("Darn, another one "); break;
						}
					}
				}
			}
			#endregion
		}
		[Constructable] 
		public SpawnHelperMage() : base( AIType.AI_Mage, FightMode.Closest, 25, 1, 0.4, 0.3 ) 
		{ 
            Title = "[BEC-Mage]";
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
			//Name.Hue = 2002;
			//Title.Hue = 2002;

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

            Fame = 10000;
            Karma = 5000;

            VirtualArmor = 40;


                        Spellbook book = new Spellbook();
						book.Hue = 2063;
                      	book.LootType = LootType.Newbied;
						book.Attributes.SpellChanneling = 1;
                      	book.Movable = false;
						AddItem( book );




                           	 Item Robe =  new Robe( );			
                        	Robe.Name = "Britania Electric Co.";
                        	Robe.Movable = false; 
				Robe.Hue = 1109;
				AddItem( Robe );

                           	 Item FireFieldMagicStaff =  new FireFieldMagicStaff( );			
                        	FireFieldMagicStaff.Name = "Rigtheous Flame";
                        	FireFieldMagicStaff.Movable = false; 
				FireFieldMagicStaff.Hue = 1109;
				AddItem( FireFieldMagicStaff );

                           	 switch (Utility.Random(3))
                           	 {
                           	 	case 0: AddItem( new LongPants(1050)); break;
                           	 	case 1: Item LegsOfBane = new LeggingsOfBane();
						LegsOfBane.Hue = 1269;
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
						PlateHelm.Hue = 2063;
                      				PlateHelm.LootType = LootType.Newbied;
                      				PlateHelm.Movable = false;
						AddItem( PlateHelm );

						Item LeatherGloves = new LeatherGloves();
						LeatherGloves.Hue = 2063;
                      				LeatherGloves.LootType = LootType.Newbied;
                      				LeatherGloves.Movable = false;
						AddItem( LeatherGloves );

						Item LeatherGorget = new LeatherGorget();
						LeatherGorget.Hue = 2063;
                      				LeatherGorget.LootType = LootType.Newbied;
                      				LeatherGorget.Movable = false;
						AddItem( LeatherGorget );

			AddItem( new Shirt(743) );

			

			
			Utility.AssignRandomHair( this );

			for (int i = 0; i < 10; i++)
			{
				PackItem( new GreaterCurePotion() );
				PackItem( new GreaterHealPotion() );
				PackItem( new TotalRefreshPotion() );
			}

          		  PackItem(new Bandage(Utility.RandomMinMax(5, 20)));

//			Horse ns = new Horse();
//			ns.Controlled = true;
//			ns.Hue = 2063;
//			ns.ControlMaster = this;
//			ns.ControlOrder = OrderType.Stay;
//			ns.Rider = this; 
		{
			AddLoot( LootPack.Average );
		}
}

		public override bool CanRummageCorpses{ get{ return true; } }

		public override bool CheckResurrect( Mobile m )
		{
			if ( m.Criminal )
			{
				Say("You did something wrong, wait a bit"); // Thou art a criminal.  I shall not resurrect thee.
				return false;
			}
			else if ( m.Kills >= 5 )
			{
				Say("I don't help reds"); // Thou'rt not a decent and good person. I shall not resurrect thee.
				return false;
			}
			else if ( m.Karma < 0 )
			{
				Say("You have bad Karma, but OK."); // Thou hast strayed from the path of virtue, but thou still deservest a second chance.
			}
			Say("An Corp");
			return true;
		}

		public override void OnThink()
		{
		    base.OnThink();

		    // Dismount if in no-mount area
		    if (this.Mounted && Server.Mobiles.AnimalTrainer.IsNoMountRegion(this.Region) && MyServerSettings.NoMountsInCertainRegions())
		    {
			Server.Mobiles.AnimalTrainer.DismountPlayer(this);
		    }

		}

		public override void OnAfterSpawn()
		{
		    base.OnAfterSpawn();

		    if (!(Server.Mobiles.AnimalTrainer.IsNoMountRegion(this.Region) && MyServerSettings.NoMountsInCertainRegions()))
		    {
			new EtherealLlama().Rider = this;
		    }
		}

		public SpawnHelperMage( Serial serial ) : base( serial ) 
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
