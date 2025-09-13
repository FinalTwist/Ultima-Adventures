using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Gumps
{
	public class TamingBODBookGump : Gump
	{
		private Mobile m;
		private TamingBODBook b;
		
		public TamingBODBookGump( Mobile from, TamingBODBook book ) : base( 0, 0 )
		{
			from.CloseGump( typeof( TamingBODBookGump ) );
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			
			m = from;
			b = book;
			
			this.AddPage(0);
						
			this.AddBackground(8, 10, 700, 80+(book.Entries.Count*30), 9200);
			
			this.AddAlphaRegion(142, 21, 201, 20);
			this.AddLabel(158, 22, 50, @"MONSTER TAMING BOOK");
			
			this.AddAlphaRegion(28, 52, 150, 15);
			this.AddAlphaRegion(180, 52, 70, 15);
			this.AddAlphaRegion(252, 52, 70, 15);
			this.AddAlphaRegion(324, 52, 100, 15);
			this.AddLabel(30, 51, 50, @"");
			this.AddLabel(182, 51, 50, @"Tamed");
			this.AddLabel(254, 51, 50, @"To Tame");
			this.AddLabel(326, 51, 50, @"Reward");
			
			for( int i = 0; i < book.Entries.Count; ++i)
			{
				TamingBODEntry MCE = book.Entries[i] as TamingBODEntry;
				this.AddAlphaRegion(28, 71+(i*30), 150, 15);
				this.AddAlphaRegion(180, 71+(i*30), 70, 15);
				this.AddAlphaRegion(252, 71+(i*30), 70, 15);
				this.AddAlphaRegion(324, 71+(i*30), 100, 15);
				this.AddLabel(29, 70+(i*30), 50, "");
				this.AddLabel(182, 70+(i*30), 50, ""+MCE.AmountTamed);
				this.AddLabel(254, 70+(i*30), 50, ""+MCE.AmountToTame);
				this.AddLabel(326, 70+(i*30), 50, ""+MCE.Reward);
				this.AddButton(429, 73+(i*30), 2362, 2362, 200+i, GumpButtonType.Reply, 0);
				this.AddButton(444, 73+(i*30), 2360, 2360, 100+i, GumpButtonType.Reply, 0);
			}
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( info.ButtonID > 0 )
			{
				if( info.ButtonID >= 200 )// Add Corpse
				{
					TamingBODEntry MCE = b.Entries[ info.ButtonID % 100 ] as TamingBODEntry;
					if (MCE.AmountToTame >= MCE.AmountTamed)
					{
							m.SendMessage("You can't add another pet to this contract.");
							return;
					}
					m.SendMessage("Choose the Tamable to add.");
					m.Target = new TameCreatureBookTarget( b,info.ButtonID % 100 );
				}
				else if ( info.ButtonID >= 100 ) // One removes the deed book.
				{
					TamingBODEntry MCE = b.Entries[ info.ButtonID % 100 ] as TamingBODEntry;
					TamingBOD MC = new TamingBOD( MCE.AmountTamed, MCE.AmountToTame, MCE.Reward );
					m.AddToBackpack( MC );
					b.Entries.RemoveAt( info.ButtonID % 100 );
				}
				
				m.SendGump( new TamingBODBookGump( (PlayerMobile) m, b ) );//#01
			}
		}
	}
	public class TameCreatureBookTarget : Target
	{
		private TamingBODEntry MCE;
		private TamingBODBook b;
		
		public TameCreatureBookTarget( TamingBODBook book, int i ) : base( -1, true, TargetFlags.None )
		{
			MCE =  book.Entries[i] as TamingBODEntry;
			b = book;
		}
		
		protected override void OnTarget( Mobile from, object o )
		{
			if ( o is BaseCreature )
			{
				BaseCreature pet = (BaseCreature)o;

				if ( !pet.Controlled || pet.ControlMaster != from ) 
					from.SendLocalizedMessage( 1042562 ); // You do not own that pet! 
				else if ( pet.IsDeadPet ) 
					from.SendLocalizedMessage( 1049668 ); // Living pets only, please. 
				else if ( pet.Summoned ) 
					from.SendMessage( "This creature was summoned." ); // I can not PetSale summoned creatures. 
				else if ( pet is Squire ) 
					from.SendMessage( "This creature is too big to fit in this small deed." ); // I can not PetSale summoned creatures.
				else if ( pet.Body.IsHuman ) 
					from.SendMessage( "This won't work on humans." ); // HA HA HA! Sorry, I am not an inn. 
				else if ( (pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) ) 
					from.SendLocalizedMessage( 1042563 ); // You need to unload your pet. 
				else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map ) 
					from.SendLocalizedMessage( 1042564 ); // I'm sorry.  Your pet seems to be busy. 
				else if (pet.Tamable)
					{
						MCE.Reward += (int)((double)Server.Mobiles.AnimalTrainerLord.ValuatePet( pet, from ) * 0.75);
						MCE.AmountTamed += 1;
						pet.ControlTarget = null; 
						pet.ControlOrder = OrderType.None; 
						pet.Internalize(); 
						pet.SetControlMaster( null ); 
						pet.SummonMaster = null;
						pet.Delete();	
						from.CloseGump( typeof( TamingBODBookGump ) );
						from.SendGump( new TamingBODBookGump( (PlayerMobile)from, b ) );
					}
				else
					from.SendMessage("This pet won't work.");
			}
			else
				from.SendMessage("This is not a tamable pet.");
		}
		
	}
}
