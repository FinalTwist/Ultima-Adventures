using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Targeting;
using Server.Misc;
using Server.Accounting;
using System.Xml;
using Server.Mobiles; 

namespace Server.Items
{
	public class PetBondDeed : Item
	{
		[Constructable]
		public PetBondDeed() : base( 0x14F0 )
		{
			base.Weight = 0;
			//base.LootType = LootType.Blessed;
			base.Name = "a pet bond deed";
		}		

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.Target = new InternalTarget(from, this);
			}
			else
			{
				from.SendMessage("This needs to be in your backpack, silly.");
			}
		}
		
		public PetBondDeed( Serial serial ) : base( serial )
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
	
		public class InternalTarget : Target
		{
			private Mobile m_From;
			private PetBondDeed m_Deed;
			
			public InternalTarget( Mobile from, PetBondDeed deed ) :  base ( 3, false, TargetFlags.None )
			{
				m_Deed = deed;
				m_From = from;
				from.SendMessage("What animal do you want to bond with?");
		
				
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				
				if (m_Deed.IsChildOf( m_From.Backpack ) )
				{					
					if ( targeted is Mobile )
					{
						if ( targeted is BaseCreature )
						{
							BaseCreature creature = (BaseCreature)targeted;
							if( !creature.Tamable ){
								from.SendMessage("This animal isn't tamable!");
							}
							else if(  !creature.Controlled || creature.ControlMaster != from ){
								from.SendMessage("It's not your pet!");
							}
							else if( creature.IsDeadPet ){
								from.SendMessage("This pet is dead... ");
							}
							else if ( creature.Summoned ){
								from.SendMessage("This pet is summoned");
							}
							else if ( creature.Body.IsHuman ){
								from.SendMessage("You want to bond with a humanoid??  Hmm... try a room.");
							}
							else{	
								
								if( creature.IsBonded == true ){
									from.SendMessage("Trying to bond the pet....");
								}
								else{
									
									if( from.Skills[SkillName.AnimalTaming].Base  < creature.MinTameSkill ){
										from.SendMessage("Your skill is too low to control this pet!");
									}
									else if( from.Skills[SkillName.AnimalLore].Base  < creature.MinTameSkill ){
											from.SendMessage("Your skill is too low to control this pet!");
										}
									else{
										try{
											creature.IsBonded = true;
											from.SendMessage("{0} is now bonded with you!",creature.Name);
											m_Deed.Delete();
										}
										catch{
											from.SendMessage("There was a problem bonding this animal..");
										}
											
									}
								}
							}							
						}
						else{
							from.SendMessage("Você pode bondar somente animais");
						}
					}
					else{
							from.SendMessage("Você pode bondar somente animais");
						}
				}
				else{
					from.SendMessage("This needs to be in your backpack, silly.");
				}			
		}
	}
}
