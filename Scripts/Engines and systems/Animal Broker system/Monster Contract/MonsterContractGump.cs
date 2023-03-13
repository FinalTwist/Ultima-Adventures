
using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Gumps
{
	public class MonsterContractGump : Gump
	{
		private MonsterContract MCparent;
		
		public MonsterContractGump( Mobile from, MonsterContract parentMC ) : base( 0, 0 )
		{
			if(from != null)from.CloseGump( typeof( MonsterContractGump ) );
			
			if(parentMC != null)
			{
				
				MCparent = parentMC;
			
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				this.AddPage(0);
				this.AddBackground(0, 0, 300, 170, 5170);
				this.AddLabel(40, 40, 0, @"Contract For: " + parentMC.AmountToTame + " " + MonsterContractType.Get[parentMC.Monster].Name);
				this.AddLabel(40, 60, 0, @"Quantity Tamed: " + parentMC.AmountTamed);
				this.AddLabel(40, 80, 0, @"Reward: " + parentMC.Reward);
				if ( parentMC.AmountTamed < parentMC.AmountToTame )
				{
					this.AddButton(90, 110, 2061, 2062, 1, GumpButtonType.Reply, 0);
					this.AddLabel(104, 108, 0, @"Add Pet");
				}
				else
				{
					this.AddButton(90, 110, 2061, 2062, 2, GumpButtonType.Reply, 0);
					this.AddLabel(104, 108, 0, @"Reward");
				}
			}
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile m_from = state.Mobile;
			
			if(m_from != null && MCparent != null)
			{
				if ( info.ButtonID == 1 )
				{
					m_from.SendMessage("Choose the tamed animal to add.");
					m_from.Target = new MonsterCorpseTarget( MCparent );
				}
				if ( info.ButtonID == 2 )
				{
					if (MonsterContract.PayRewardTo(m_from, MCparent))
						MCparent.Delete();
				}
			}
		}
	}
	
	public class MonsterCorpseTarget : Target
	{
		private MonsterContract MCparent;
		
		public MonsterCorpseTarget( MonsterContract parentMC ) : base( -1, true, TargetFlags.None )
		{
			MCparent = parentMC;
		}
		
		protected override void OnTarget( Mobile from, object o )
		{
            if ( MCparent == null || from == null || o == null || MCparent.Monster == null)
            {
                Console.WriteLine( "MonsterContract: Sa bug !! Mais o�, on sait pas :p" );
                return;
            }
			
			
			if ( o is BaseCreature )
			{
				BaseCreature pet = (BaseCreature)o;

				if ( !pet.Controlled || pet.ControlMaster != from ) 
					from.SendLocalizedMessage( 1042562 ); // You do not own that pet! 
				else if ( pet.IsDeadPet ) 
					from.SendLocalizedMessage( 1049668 ); // Living pets only, please. 
				else if ( pet.Summoned ) 
					from.SendMessage( "This creature was summoned." ); // I can not PetSale summoned creatures. 
				else if ( pet.Body.IsHuman ) 
					from.SendMessage( "This won't work on humans." ); // HA HA HA! Sorry, I am not an inn. 
				else if ( (pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) ) 
					from.SendLocalizedMessage( 1042563 ); // You need to unload your pet. 
				else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map ) 
					from.SendLocalizedMessage( 1042564 ); // I'm sorry.  Your pet seems to be busy. 
				else if ( pet.GetType() == MonsterContractType.Get[MCparent.Monster].Type )
					{
						MCparent.Reward += (int)((double)Server.Mobiles.AnimalTrainerLord.ValuatePet( pet, from ) * (1+(Utility.RandomMinMax(15,35)/100)));
						MCparent.AmountTamed += 1;
						MCparent.InvalidateProperties();
						pet.ControlTarget = null; 
						pet.ControlOrder = OrderType.None; 
						pet.Internalize(); 
						pet.SetControlMaster( null ); 
						pet.SummonMaster = null;
						pet.Delete();	
					}
				else
					from.SendMessage("This pet won't work.");
			}
			else
				from.SendMessage("This is not a tamable pet.");
		}

	}
}
