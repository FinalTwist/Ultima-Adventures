using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles 
{ 
	public class BasePerson : BaseConvo
	{
		[Constructable] 
		public BasePerson() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = "person";
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			if ( Female = Utility.RandomBool() ) 
			{ 
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else 
			{ 
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public virtual bool IsInvulnerable { get { return false; } }
		public override bool Unprovokable { get { return true; } }
		public override bool Uncalmable{ get{ return true; } }

		public BasePerson( Serial serial ) : base( serial ) 
		{ 
		} 

		public override bool IsEnemy( Mobile m )
		{

			if (this is TimeLord)
				return false;
				
			if (this is TownHerald && ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "DarkMoor" || Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Temple of Praetoria") && m.Karma <= 0 && !m.Criminal)
				return false;

			if (this is TownHerald && m is PlayerMobile && ((PlayerMobile)m).Kills <=1 )
				return false;

			if (this is TownHerald && this.Map == Map.Trammel && this.X < 997 && this.X > 971 && this.Y >1583 && this.Y < 1603 && !m.Criminal)
				return false;

			if ( IntelligentAction.GetMyEnemies( m, this, true ) == false )
				return false;

			if ( m.Region != this.Region && !(m is PlayerMobile) )
				return false;

			//m.Criminal = true;
			return true;
		}

		public override bool OnBeforeDeath()
		{
			Server.Misc.MorphingTime.TurnToSomethingOnDeath( this );

			Mobile killer = this.LastKiller;

			if (killer is BaseCreature)
			{
				BaseCreature bc_killer = (BaseCreature)killer;
				if(bc_killer.Summoned)
				{
					if(bc_killer.SummonMaster != null)
						killer = bc_killer.SummonMaster;
				}
				else if(bc_killer.Controlled)
				{
					if(bc_killer.ControlMaster != null)
						killer=bc_killer.ControlMaster;
				}
				else if(bc_killer.BardProvoked)
				{
					if(bc_killer.BardMaster != null)
						killer=bc_killer.BardMaster;
				}
			}

			if ( killer is PlayerMobile )
			{
				killer.Criminal = true;
				killer.Kills = killer.Kills + 1;
			}

			string bSay = "Help!";

				switch ( Utility.Random( 5 ))		   
				{
					case 0: bSay = "Guards!"; break;
					case 1: bSay = "There will be no place for you to hide!"; break;
					case 2: bSay = "Noooo!"; break;
					case 3: bSay = "Vile rogue!"; break;
					case 4: bSay = "Aarrgh!"; break;
				};

			this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( bSay ) );

			if ( !base.OnBeforeDeath() )
				return false;

			return true;
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.MorphingTime.CheckMorph( this );
		}

		protected override void OnMapChange( Map oldMap )
		{
			base.OnMapChange( oldMap );
			Server.Misc.MorphingTime.CheckMorph( this );
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

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Cargo )
			{
				Server.Items.Cargo.GiveCargo( (Cargo)dropped, this, from );
			}
			return base.OnDragDrop( from, dropped );
		}
	}
}   