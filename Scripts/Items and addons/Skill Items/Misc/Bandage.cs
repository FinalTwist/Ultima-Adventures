using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Misc;

namespace Server.Items
{
	public class Bandage : Item, IDyable
	{
		public static int Range = ( Server.Misc.MyServerSettings.FriendsAvoidHeels() ? 5 : 2 ); 

		public override int Hue{ get { return 0; } }

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Bandage() : this( 1 )
		{
		}

		[Constructable]
		public Bandage( int amount ) : base( 0xE21 )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0;
		}

		public Bandage( Serial serial ) : base( serial )
		{
		}

		public virtual bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
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
			Hue = 0;
		}

		// This method added for [bandself command to call.
		public static void BandSelfCommandCall( Mobile from, Item m_Bandage )
		{
			if ( from is PlayerMobile && from.FindItemOnLayer( Layer.Ring ) != null && from.FindItemOnLayer( Layer.Ring ) is OneRing)
			{
				from.SendMessage( "The ring convinces you not to do that, and you listen to it... " );
				return;
			}

			if ( from.Blessed)
			{
				from.SendMessage( "You cannot use bandages while in this state." );
				return;
			}

			from.RevealingAction();

			if ( BandageContext.BeginHeal( from, from ) != null )
				m_Bandage.Consume();
				Server.Gumps.QuickBar.RefreshQuickBar( from );
		}

		// This method added for [bandother command to call.
		public static void BandOtherCommandCall( Mobile from, Item m_Bandage )
		{
			if ( from is PlayerMobile && from.FindItemOnLayer( Layer.Ring ) != null && from.FindItemOnLayer( Layer.Ring ) is OneRing)
			{
				from.SendMessage( "The ring convinces you not to do that, and you listen to it... " );
				return;
			}
			if ( from.Blessed )
			{
				from.SendMessage( "You cannot use bandages while in this state." );
				return;
			}

			from.RevealingAction();
			Bandage band = (Bandage)m_Bandage;
			from.SendLocalizedMessage( 500948 ); // Who will you use the bandages on?
			from.Target = new InternalTarget( band );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is PlayerMobile && from.FindItemOnLayer( Layer.Ring ) != null && from.FindItemOnLayer( Layer.Ring ) is OneRing)
			{
				from.SendMessage( "The ring convinces you not to do that, and you listen to it... " );
				return;
			}
			if ( from.Blessed )
			{
				from.SendMessage( "You cannot use bandages while in this state." );
				return;
			}

			if ( from.InRange( GetWorldLocation(), Range ) )
			{
				from.RevealingAction();

				from.SendLocalizedMessage( 500948 ); // Who will you use the bandages on?

				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
			}
		}

		private class InternalTarget : Target
		{
			private Bandage m_Bandage;

			public InternalTarget( Bandage bandage ) : base( Bandage.Range, false, TargetFlags.Beneficial )
			{
				m_Bandage = bandage;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Bandage.Deleted )
					return;

				if ( targeted is Mobile )
				{
					if ( from.InRange( m_Bandage.GetWorldLocation(), Bandage.Range ) )
					{
						if ( BandageContext.BeginHeal( from, (Mobile)targeted ) != null )
						{
							m_Bandage.Consume();
							Server.Gumps.QuickBar.RefreshQuickBar( from );
						}
					}
					else
					{
						from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
					}
				}
				else if ( targeted is HenchmanFighterItem && from.Skills[SkillName.Anatomy].Value >= 80 && from.Skills[SkillName.Healing].Value >= 80 )
				{
					HenchmanFighterItem friend = (HenchmanFighterItem)targeted;

					if ( friend.HenchDead > 0 )
					{
						friend.Name = "fighter henchman";
						friend.HenchDead = 0;
						friend.InvalidateProperties();
						m_Bandage.Consume();
					}
					else
					{
						from.SendMessage("They are not dead.");
					}
				}
				else if ( targeted is HenchmanWizardItem && from.Skills[SkillName.Anatomy].Value >= 80 && from.Skills[SkillName.Healing].Value >= 80 )
				{
					HenchmanWizardItem friend = (HenchmanWizardItem)targeted;

					if ( friend.HenchDead > 0 )
					{
						friend.Name = "wizard henchman";
						friend.HenchDead = 0;
						friend.InvalidateProperties();
						m_Bandage.Consume();
					}
					else
					{
						from.SendMessage("They are not dead.");
					}
				}
				else if ( targeted is HenchmanArcherItem && from.Skills[SkillName.Anatomy].Value >= 80 && from.Skills[SkillName.Healing].Value >= 80 )
				{
					HenchmanArcherItem friend = (HenchmanArcherItem)targeted;

					if ( friend.HenchDead > 0 )
					{
						friend.Name = "archer henchman";
						friend.HenchDead = 0;
						friend.InvalidateProperties();
						m_Bandage.Consume();
					}
					else
					{
						from.SendMessage("They are not dead.");
					}
				}
				else if (targeted is HenchmanMonsterItem && from.Skills[SkillName.Anatomy].Value >= 80 && from.Skills[SkillName.Healing].Value >= 80 )
				{
					HenchmanMonsterItem friend = (HenchmanMonsterItem)targeted;

					if ( friend.HenchDead > 0 )
					{
						friend.Name = "creature henchman";
						friend.HenchDead = 0;
						friend.InvalidateProperties();
						m_Bandage.Consume();
					}
					else
					{
						from.SendMessage("They are not dead.");
					}
				}
				else
				{
					from.SendLocalizedMessage( 500970 ); // Bandages can not be used on that.
				}
			}

			protected override void OnNonlocalTarget( Mobile from, object targeted )
			{
				base.OnNonlocalTarget( from, targeted );
			}
		}
	}

	public class BandageContext
	{
		private Mobile m_Healer;
		private Mobile m_Patient;
		private int m_Slips;
		private Timer m_Timer;

		public Mobile Healer{ get{ return m_Healer; } }
		public Mobile Patient{ get{ return m_Patient; } }
		public int Slips{ get{ return m_Slips; } set{ m_Slips = value; } }
		public Timer Timer{ get{ return m_Timer; } }

		public void Slip()
		{
			m_Healer.SendLocalizedMessage( 500961 ); // Your fingers slip!
			m_Healer.LocalOverheadMessage( MessageType.Regular, 1150, 500961 ); // WIZARD ADDED FOR OVERHEAD MESSAGE

			++m_Slips;
		}

		public BandageContext( Mobile healer, Mobile patient, TimeSpan delay )
		{
			m_Healer = healer;
			m_Patient = patient;

			m_Timer = new InternalTimer( this, delay );
			m_Timer.Start();
		}

		public void StopHeal()
		{
			m_Table.Remove( m_Healer );

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}

		private static Dictionary<Mobile, BandageContext> m_Table = new Dictionary<Mobile, BandageContext>();

		public static BandageContext GetContext( Mobile healer )
		{
			BandageContext bc = null;
			m_Table.TryGetValue( healer, out bc );
			return bc;
		}

		public static SkillName GetPrimarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.Veterinary;
			else
				return SkillName.Healing;
		}

		public static SkillName GetSecondarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.AnimalLore;
			else
				return SkillName.Anatomy;
		}

		public void EndHeal()
		{
			StopHeal();

			int healerNumber = -1, patientNumber = -1;
			bool playSound = true;
			bool checkSkills = false;

			SkillName primarySkill = GetPrimarySkill( m_Patient );
			SkillName secondarySkill = GetSecondarySkill( m_Patient );

			BaseCreature petPatient = m_Patient as BaseCreature;

			if ( !m_Healer.Alive )
			{
				healerNumber = 500962; // You were unable to finish your work before you died.
				patientNumber = -1;
				playSound = false;
			}
			else if ( !m_Healer.InRange( m_Patient, Bandage.Range ) )
			{
				healerNumber = 500963; // You did not stay close enough to heal your target.
				patientNumber = -1;
				playSound = false;
			}
			else if ( !m_Patient.Alive || (petPatient != null && petPatient.IsDeadPet) )
			{
				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing - 68.0) / 50.0) - (m_Slips * 0.02);

				if (( (checkSkills = (healing >= 80.0 && anatomy >= 80.0)) && chance > Utility.RandomDouble() )
				       )	//TODO: Dbl check doesn't check for faction of the horse here?
				{
					if ( m_Patient.Map == null || !m_Patient.Map.CanFit( m_Patient.Location, 16, false, false ) )
					{
						healerNumber = 501042; // Target can not be resurrected at that location.
						patientNumber = 502391; // Thou can not be resurrected there!
					}
					else if ( m_Patient.Region != null && m_Patient.Region.IsPartOf( "Khaldun" ) )
					{
						healerNumber = 1010395; // The veil of death in this area is too strong and resists thy efforts to restore life.
						patientNumber = -1;
					}
					else
					{
						healerNumber = 500965; // You are able to resurrect your patient.
						patientNumber = -1;

						m_Patient.PlaySound( 0x214 );
						m_Patient.FixedEffect( 0x376A, 10, 16 );

						if ( petPatient != null && petPatient.IsDeadPet )
						{
							Mobile master = petPatient.ControlMaster;

							if( master != null && m_Healer == master )
							{
								petPatient.ResurrectPet();

								for ( int i = 0; i < petPatient.Skills.Length; ++i )
								{
									petPatient.Skills[i].Base -= 0.1;
								}
							}
							else if ( master != null && master.InRange( petPatient, 3 ) )
							{
								healerNumber = 503255; // You are able to resurrect the creature.

								master.CloseGump( typeof( PetResurrectGump ) );
								master.SendGump( new PetResurrectGump( m_Healer, petPatient ) );
							}
							else
							{
								bool found = false;

								List<Mobile> friends = petPatient.Friends;

								for ( int i = 0; friends != null && i < friends.Count; ++i )
								{
									Mobile friend = friends[i];

									if ( friend.InRange( petPatient, 3 ) )
									{
										healerNumber = 503255; // You are able to resurrect the creature.

										friend.CloseGump( typeof( PetResurrectGump ) );
										friend.SendGump( new PetResurrectGump( m_Healer, petPatient ) );

										found = true;
										break;
									}
								}

								if ( !found )
									healerNumber = 1049670; // The pet's owner must be nearby to attempt resurrection.
							}
						}
						else
						{
							m_Patient.CloseGump( typeof( ResurrectGump ) );
							m_Patient.SendGump( new ResurrectGump( m_Patient, m_Healer ) );
						}
					}
				}
				else
				{
					if ( petPatient != null && petPatient.IsDeadPet )
						healerNumber = 503256; // You fail to resurrect the creature.
					else
						healerNumber = 500966; // You are unable to resurrect your patient.

					patientNumber = -1;
				}
			}
			else if ( m_Patient.Poisoned )
			{
				m_Healer.SendLocalizedMessage( 500969 ); // You finish applying the bandages.
				m_Healer.LocalOverheadMessage( MessageType.Regular, 1150, 500969 ); // WIZARD ADDED FOR OVERHEAD MESSAGE

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing - 30.0) / 50.0) - (m_Patient.Poison.Level * 0.1) - (m_Slips * 0.02);

				if ( (checkSkills = (healing >= 60.0 && anatomy >= 60.0)) && chance > Utility.RandomDouble() )
				{
					if ( m_Patient.CurePoison( m_Healer ) )
					{
						healerNumber = (m_Healer == m_Patient) ? -1 : 1010058; // You have cured the target of all poisons.
						patientNumber = 1010059; // You have been cured of all poisons.
					}
					else
					{
						healerNumber = -1;
						patientNumber = -1;
					}
				}
				else
				{
					healerNumber = 1010060; // You have failed to cure your target!
					patientNumber = -1;
				}
			}
			else if ( BleedAttack.IsBleeding( m_Patient ) )
			{
				healerNumber = 1060088; // You bind the wound and stop the bleeding
				patientNumber = 1060167; // The bleeding wounds have healed, you are no longer bleeding!

				BleedAttack.EndBleed( m_Patient, false );
			}
			else if ( MortalStrike.IsWounded( m_Patient ) )
			{
				healerNumber = ( m_Healer == m_Patient ? 1005000 : 1010398 );
				patientNumber = -1;
				playSound = false;
			}
			else if ( m_Patient.Hits == m_Patient.HitsMax )
			{
				healerNumber = 500967; // You heal what little damage your patient had.
				patientNumber = -1;
			}
			else
			{
				checkSkills = true;
				patientNumber = -1;

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing + 10.0) / 100.0) - (m_Slips * 0.02);

				if ( chance > Utility.RandomDouble() )
				{
					healerNumber = 500969; // You finish applying the bandages.

					double min, max;

					min = (anatomy / 2) + (healing / 2) + 25.0; 
					max = (anatomy / 2) + (healing / 2) + 100.0;

					double toHeal = min + (Utility.RandomDouble() * (max - min));

					if ( m_Patient.Body.IsMonster || m_Patient.Body.IsAnimal )
						toHeal += m_Patient.HitsMax / 100;

					if ( Core.AOS )
						toHeal -= toHeal * m_Slips * 0.35; // TODO: Verify algorithm
					else
						toHeal -= m_Slips * 4;

					if ( toHeal < 1 )
					{
						toHeal = 1;
						healerNumber = 500968; // You apply the bandages, but they barely help.
					}

					m_Patient.Heal( (int) toHeal, m_Healer, false );
				}
				else
				{
					healerNumber = 500968; // You apply the bandages, but they barely help.
					playSound = false;
				}
			}

			if ( healerNumber != -1 ){
				m_Healer.SendLocalizedMessage( healerNumber );
				m_Healer.LocalOverheadMessage( MessageType.Regular, 1150, healerNumber );} // WIZARD ADDED FOR OVERHEAD MESSAGE

			if ( patientNumber != -1 ){
				m_Patient.SendLocalizedMessage( patientNumber );
				m_Healer.LocalOverheadMessage( MessageType.Regular, 1150, patientNumber );} // WIZARD ADDED FOR OVERHEAD MESSAGE

			if ( playSound )
				m_Patient.PlaySound( 0x57 );

			if ( checkSkills ) 
			{
				//final, increase gains when someone is playing legit Veterinary edition
				if (primarySkill == SkillName.Veterinary && m_Patient is BaseCreature )
				{
					BaseCreature bc = m_Patient as BaseCreature;
					if (!bc.Summoned && bc.ControlMaster != null && bc.ControlMaster == m_Healer && bc.Combatant != null && bc.Combatant is BaseCreature)
					{
						BaseCreature fighting = bc.Combatant as BaseCreature;
						int ratio = (int)(fighting.HitsMax / ( bc.HitsMax *2));

						if (ratio > 10)
							ratio = 10;

						if (ratio >= 2)
						{
							while (ratio > 1 )
							{
								if (Utility.RandomBool())
								{
									m_Healer.CheckSkill( secondarySkill, 0.0, 120.0 );
									m_Healer.CheckSkill( primarySkill, 0.0, 120.0 );
								}
								ratio -= 1;
							}
						}
					}
				}
/*
				//healing edition... to be continued
				if (primarySkill == SkillName.Healing && m_Healer is PlayerMobile && ((PlayerMobile).m_Healer).Combatant != null && ((PlayerMobile).m_Healer).Combatant is BaseCreature)
				{

				}
*/
				m_Healer.CheckSkill( secondarySkill, 0.0, 120.0 );
				m_Healer.CheckSkill( primarySkill, 0.0, 120.0 );
			}
		}

		private class InternalTimer : Timer
		{
			private BandageContext m_Context;

			public InternalTimer( BandageContext context, TimeSpan delay ) : base( delay )
			{
				m_Context = context;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				m_Context.EndHeal();
			}
		}

		public static BandageContext BeginHeal( Mobile healer, Mobile patient )
		{
			bool isDeadPet = ( patient is BaseCreature && ((BaseCreature)patient).IsDeadPet );

			if ( patient.Hunger < 5 && patient is PlayerMobile && patient.Alive )
			{
				healer.SendMessage( "You cannot heal those that are extremely hungry." );
			}
			else if ( patient is Golem )
			{
				healer.SendLocalizedMessage( 500970 ); // Bandages cannot be used on that.
			}
			else if ( patient is BaseCreature && ((BaseCreature)patient).IsAnimatedDead )
			{
				healer.SendLocalizedMessage( 500951 ); // You cannot heal that.
			}
			else if ( !patient.Poisoned && patient.Hits == patient.HitsMax && !BleedAttack.IsBleeding( patient ) && !isDeadPet )
			{
				healer.SendLocalizedMessage( 500955 ); // That being is not damaged!
			}
			else if ( !patient.Alive && (patient.Map == null || !patient.Map.CanFit( patient.Location, 16, false, false )) )
			{
				healer.SendLocalizedMessage( 501042 ); // Target cannot be resurrected at that location.
			}
			else if ( healer.CanBeBeneficial( patient, true, true ) )
			{
				double bandageSpeedMin = MyServerSettings.BandageSpeedMin();
				
				healer.DoBeneficial( patient );

				bool onSelf = ( healer == patient );
				int dex = healer.Dex;

				if (dex > 300)
					dex = 300;

				double seconds;
				double resDelay = ( patient.Alive ? 0.0 : bandageSpeedMin );

				double dexseconds = 4 * (1- ((double)dex/300));// ( 300 - dex ) /60;

				if (AdventuresFunctions.IsPuritain((object)healer) && healer is PlayerMobile)
				{
					bandageSpeedMin = MyServerSettings.BandageSpeedMin();
					dexseconds = (( 150 - (double)dex ) /25) * (1-((PlayerMobile)healer).Agility());
					//midland healing: 3 seconds and high agility negates dexseconds
				}
				else if (healer is PlayerMobile && ((PlayerMobile)healer).SoulBound)
					dexseconds = 4 * (1- (dex/150));		

				if (dexseconds < 0 ) // FINAL dex over 300 is possible
					dexseconds = 0;

				if ( onSelf )
				{

					seconds = bandageSpeedMin + dexseconds; //FINAL changed to account for dex over 120

				}
				else
				{
					if ( Core.AOS && GetPrimarySkill( patient ) == SkillName.Veterinary )
					{
						if (dexseconds >= 2)
							seconds = dexseconds + 1; // FINAL why should vet skill take 2 seconds regardless of dex?
						else
							seconds = 3;
					}
					else if ( Core.AOS )
					{
						if (dex < 204)
						{		
							seconds = 3.2-(Math.Sin((double)dex/130)*2.5) + resDelay;
						}
						else
						{
							seconds = 0.7 + resDelay;
						}
					}
					else
					{
						if ( dex >= 100 )
							seconds = 3.0 + resDelay;
						else if ( dex >= 40 )
							seconds = 4.0 + resDelay;
						else
							seconds = bandageSpeedMin + resDelay;
					}
				}

				BandageContext context = GetContext( healer );

				if ( context != null )
					context.StopHeal();

				seconds *= 1000;
				
				context = new BandageContext( healer, patient, TimeSpan.FromMilliseconds( seconds ) );

				m_Table[healer] = context;

				if ( !onSelf )
					patient.SendLocalizedMessage( 1008078, false, healer.Name ); //  : Attempting to heal you.

				
				healer.SendLocalizedMessage( 500956 ); // You begin applying the bandages.
				healer.LocalOverheadMessage( MessageType.Regular, 1150, 500956 ); // WIZARD ADDED FOR OVERHEAD MESSAGE

				return context;
			}

			return null;
		}

	}
}