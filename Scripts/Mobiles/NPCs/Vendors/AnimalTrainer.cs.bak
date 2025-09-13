using System;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Misc;
using Server.Regions;
using Server.Spells.Syth;
using Server.Spells.Mystic;
using System.Collections;

namespace Server.Mobiles
{
	public class AnimalTrainer : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.DruidsGuild; } }

		[Constructable]
		public AnimalTrainer() : base( "the animal trainer" )
		{
			Job = JobFragment.animal;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.AnimalLore, 64.0, 100.0 );
			SetSkill( SkillName.AnimalTaming, 90.0, 100.0 );
			SetSkill( SkillName.Veterinary, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Serpent Island" )
			{
				m_SBInfos.Add( new SBGargoyleAnimalTrainer() );
			}
			else if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Land of Lodoria" )
			{
				m_SBInfos.Add( new SBElfAnimalTrainer() );
			}
			else if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Isles of Dread" )
			{
				m_SBInfos.Add( new SBBarbarianAnimalTrainer() );
			}
			else if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Savaged Empire" )
			{
				m_SBInfos.Add( new SBOrkAnimalTrainer() );
			}
			else
			{
				m_SBInfos.Add( new SBHumanAnimalTrainer() );
			}
			m_SBInfos.Add( new SBAnimalTrainer() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override VendorShoeType ShoeType
		{
			get{ return Female ? VendorShoeType.ThighBoots : VendorShoeType.Boots; }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( Utility.RandomBool() ? (Item)new QuarterStaff() : (Item)new ShepherdsCrook() );
		}

		private class StableEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public StableEntry( AnimalTrainer trainer, Mobile from ) : base( 6126, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.BeginStable( m_From );
			}
		}

		///////////////////////////////////////////////////////////////////////////
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new ClaimingGumpEntry( from, this ) ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
			list.Add( new RidingGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "Animal Companions", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Pets" ) ));
					}
				}
            }
        }

		public class RidingGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public RidingGumpEntry( Mobile from, Mobile giver ) : base( 6098, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( Server.Mobiles.Veterinarian.RidingGump ) ) )
					{
						mobile.SendGump(new Server.Mobiles.Veterinarian.RidingGump());
					}
				}
            }
        }

		public class ClaimingGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private AnimalTrainer m_Giver;
			
			public ClaimingGumpEntry( Mobile from, AnimalTrainer giver ) : base( 6165, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					m_Giver.BeginClaimList( m_Mobile );
				}
            }
        }

		///////////////////////////////////////////////////////////////////////////

		private class ClaimListGump : Gump
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;
			private List<BaseCreature> m_List;

			public ClaimListGump( AnimalTrainer trainer, Mobile from, List<BaseCreature> list ) : base( 25, 25 )
			{
				m_Trainer = trainer;
				m_From = from;
				m_List = list;

				from.CloseGump( typeof( ClaimListGump ) );

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 298, 129);
				AddImage(298, 298, 129);
				AddImage(7, 8, 133);
				AddImage(218, 47, 132);
				AddImage(380, 8, 134);
				AddImage(164, 551, 140);
				AddImage(8, 517, 139);
				AddImage(269, 342, 147);
				AddHtml( 174, 68, 200, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>PETS IN THE STABLE</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				int y = 95;

				for ( int i = 0; i < list.Count; ++i )
				{
					BaseCreature pet = list[i];

					if ( pet == null || pet.Deleted )
						continue;

					y = y + 35;

					AddHtml( 145, y, 425, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + pet.Name + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(105, y, 4005, 4005, (i + 1), GumpButtonType.Reply, 0);
				}
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				int index = info.ButtonID - 1;

				if ( index >= 0 && index < m_List.Count )
					m_Trainer.EndClaimList( m_From, m_List[index] );
			}
		}

		private class ClaimAllEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public ClaimAllEntry( AnimalTrainer trainer, Mobile from ) : base( 6127, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.Claim( m_From );
			}
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive )
			{
				list.Add( new StableEntry( this, from ) );

				if ( from.Stabled.Count > 0 )
					list.Add( new ClaimAllEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

		public static int GetMaxStabled( Mobile from )
		{
			double taming = from.Skills[SkillName.AnimalTaming].Value;
			double anlore = from.Skills[SkillName.AnimalLore].Value;
			double vetern = from.Skills[SkillName.Veterinary].Value;
			double herd = from.Skills[SkillName.Herding].Value;
			double sklsum = taming + anlore + vetern + herd;

			int max;

			if ( sklsum >= 400.0 )
				max = 15;
			else if ( sklsum >= 300.0 )
				max = 10;
			else if ( sklsum >= 240.0 )
				max = 7;
			else if ( sklsum >= 200.0 )
				max = 5;
			else if ( sklsum >= 160.0 )
				max = 4;
			else
				max = 2;

			if ( taming >= 100.0 )
				max += (int)((taming - 90.0) / 10);

			if ( anlore >= 100.0 )
				max += (int)((anlore - 90.0) / 10);

			if ( vetern >= 100.0 )
				max += (int)((vetern - 90.0) / 10);

			if ( herd >= 100.0 )
				max += (int)((herd - 90.0) / 10);

			return max;
		}

		private class StableTarget : Target
		{
			private AnimalTrainer m_Trainer;

			public StableTarget( AnimalTrainer trainer ) : base( 12, false, TargetFlags.None )
			{
				m_Trainer = trainer;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BaseCreature )
					m_Trainer.EndStable( from, (BaseCreature)targeted );
				else if ( targeted == from )
					m_Trainer.SayTo( from, 502672 ); // HA HA HA! Sorry, I am not an inn.
				else
					m_Trainer.SayTo( from, 1048053 ); // You can't stable that!
			}
		}
		
		private void CloseClaimList( Mobile from )
		{
			from.CloseGump( typeof( ClaimListGump ) );
		}

		public static void DismountPlayer( Mobile m )
		{
			CleanClaimList( m );

			if ( m.Mount is EtherealMount )
			{
				IMount mount = m.Mount;
				EtherealMount ethy = (EtherealMount)mount;
				Server.Mobiles.EtherealMount.Dismount( m );
				m.SendMessage( "Your mount has moved to your pack." );
			}
			else if ( m.Mount is BaseMount )
			{
				BaseCreature pet = (BaseCreature)(m.Mount);
				Server.Mobiles.BaseMount.Dismount( m );

				pet.ControlTarget = null;
				//pet.ControlOrder = OrderType.Stay;
				pet.Internalize();
				pet.SetControlMaster( null );
				pet.SummonMaster = null;
				pet.IsStabled = true;
				//pet.Loyalty = BaseCreature.MaxLoyalty;
				pet.Language = "mount";

				m.Stabled.Add( pet );

				m.SendMessage( "Your mount is safely waiting for you elsewhere." );
			}
		}

		public static bool IsBeingFast( Mobile from )
		{
			if ( from.Mounted )
				return true;

			Item shoes = from.FindItemOnLayer( Layer.Shoes );
			if ( shoes is BootsofHermes && shoes.Weight < 5.0 )
				return true;

			if ( from is PlayerMobile)
			{
				if ( Spells.Syth.SythSpeed.UnderEffect( (PlayerMobile)from ) )
					return true;

				if ( Spells.Mystic.WindRunner.UnderEffect( (PlayerMobile)from ) )
					return true;
			}

			return false;
		}

		public static void CleanClaimList( Mobile from )
		{
			List<BaseCreature> list = new List<BaseCreature>();

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}
				else { pet.Language = null; }
			}
		}

		public static void GetLastMounted( Mobile from )
		{
			bool hasMount = false;

			List<BaseCreature> list = new List<BaseCreature>();

			BaseCreature bc = null;
			bool claimed = false;
			int stabled = 0;

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}
				else if ( pet.Language == "mount" )
				{
					bc = pet;
				}
			}

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				++stabled;

				if ( CanGetLastMounted( from, pet ) && pet is BaseMount && pet == bc )
				{
					pet.SetControlMaster( from );
					pet.ControlTarget = from;
					pet.MoveToWorld( from.Location, from.Map );
					pet.IsStabled = false;
					//pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy

					from.Stabled.RemoveAt( i );
					--i;

					claimed = true;
					((Mobile)pet).Language = null;
					Server.Mobiles.BaseMount.Ride( ((BaseMount)pet), from );
					hasMount = true;
				}
				else if ( pet == bc )
				{
					((Mobile)pet).Language = null;
				}
			}

			Server.Mobiles.AnimalTrainer.CleanClaimList( from );

			if ( !hasMount )
			{
				ArrayList ethy = new ArrayList();
				foreach ( Item item in World.Items.Values )
				{
					if ( item is EtherealMount )
					{
						if ( ((EtherealMount)item).Owner == from )
						{
							((EtherealMount)item).Rider = from;
							((EtherealMount)item).Owner = from;
						}
					}
				}
			}
		}

		public static bool CanGetLastMounted( Mobile from, BaseCreature pet )
		{
			return ((from.Followers + pet.ControlSlots) <= from.FollowersMax);
		}

		public static bool IsNoMountRegion( Region reg )
		{
			if ( reg.IsPartOf( "the Hidden Valley" ) )
				return true;

			if ( reg.IsPartOf( "the Corrupt Pass" ) )
				return true;

			if ( reg.IsPartOf( "the Hedge Maze" ) )
				return true;

			if ( reg.IsPartOf( "the Valley of Dark Druids" ) )
				return true;

			if ( reg.IsPartOf( "the Altar of the Dragon King" ) )
				return true;

			if ( reg.IsPartOf( "the Altar of the Blood God" ) )
				return true;

			if ( !reg.IsPartOf( "the Port" ) && ( reg is ProtectedRegion || reg is CaveRegion || reg is BardDungeonRegion || reg is MoonCore || reg is UmbraRegion || reg is DungeonRegion || reg is PublicRegion ) )

					return true;

			if ( reg is ProtectedRegion || reg is CaveRegion || reg is BardDungeonRegion || reg is MoonCore || reg is UmbraRegion || reg is DungeonRegion || reg is PublicRegion )
					return true;

			return false;
		}

		public static bool AllowMagicSpeed( Mobile m, Region reg )
		{
			if ( reg.IsPartOf( "the Port" ) || reg is ProtectedRegion || reg is PublicRegion )
					return true;

			return false;
		}

		public void BeginClaimList( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			List<BaseCreature> list = new List<BaseCreature>();

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

				list.Add( pet );
			}

			if ( list.Count > 0 )
				from.SendGump( new ClaimListGump( this, from, list ) );
			else
				SayTo( from, 502671 ); // But I have no animals stabled with me at the moment!
		}

		public void EndClaimList( Mobile from, BaseCreature pet )
		{
			if ( pet == null || pet.Deleted || from.Map != this.Map || !from.Stabled.Contains( pet ) || !from.CheckAlive() )
				return;
			
			if ( !from.InRange( this, 14 ) )
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
				return;
			}

			if ( CanClaim( from, pet ) )
			{
				DoClaim( from, pet );

				from.Stabled.Remove( pet );
			}
			else
			{
				SayTo( from, 1049612, pet.Name ); // ~1_NAME~ remained in the stables because you have too many followers.
			}
		}

		public void BeginStable( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			Container bank = from.FindBankNoCreate();
			
			if ( ( from.Backpack == null || from.Backpack.GetAmount( typeof( Gold ) ) < 30 ) && ( bank == null || bank.GetAmount( typeof( Gold ) ) < 30 ) )
			{
				SayTo( from, 1042556 ); // Thou dost not have enough gold, not even in thy bank account.
			}
			else
			{
				/* I charge 30 gold per pet for a real week's stable time.
				 * I will withdraw it from thy bank account.
				 * Which animal wouldst thou like to stable here?
				 */
				from.SendLocalizedMessage(1042558);

				from.Target = new StableTarget( this );
			}
		}

		public void EndStable( Mobile from, BaseCreature pet )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			if ( pet.Body.IsHuman )
			{
				SayTo( from, 502672 ); // HA HA HA! Sorry, I am not an inn.
			}
			else if ( !pet.Controlled )
			{
				SayTo( from, 1048053 ); // You can't stable that!
			}
			else if ( pet.ControlMaster != from )
			{
				SayTo( from, 1042562 ); // You do not own that pet!
			}
			else if ( pet.IsDeadPet )
			{
				SayTo( from, 1049668 ); // Living pets only, please.
			}
			else if ( pet.Summoned )
			{
				SayTo( from, 502673 ); // I can not stable summoned creatures.
			}
			else if ( (pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) )
			{
				SayTo( from, 1042563 ); // You need to unload your pet.
			}
			else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map )
			{
				SayTo( from, 1042564 ); // I'm sorry.  Your pet seems to be busy.
			}
			else if ( from.Stabled.Count >= GetMaxStabled( from ) )
			{
				SayTo( from, 1042565 ); // You have too many pets in the stables!
			}
			else
			{
				Container bank = from.FindBankNoCreate();

				if ( ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( Gold ), 30 ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), 30 ) ) )
				{
					pet.Language = null;
					pet.ControlTarget = null;
					pet.ControlOrder = OrderType.Stay;
					pet.Internalize();

					pet.SetControlMaster( null );
					pet.SummonMaster = null;

					pet.IsStabled = true;

					if ( Core.SE )	
						pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully happy

					from.Stabled.Add( pet );

					SayTo( from, Core.AOS ? 1049677 : 502679 ); // [AOS: Your pet has been stabled.] Very well, thy pet is stabled. Thou mayst recover it by saying 'claim' to me. In one real world week, I shall sell it off if it is not claimed!
				}
				else
				{
					SayTo( from, 502677 ); // But thou hast not the funds in thy bank account!
				}
			}
		}

		public void Claim( Mobile from )
		{
			Claim( from, null );
		}

		public void Claim( Mobile from, string petName )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			bool claimed = false;
			int stabled = 0;
			
			bool claimByName = ( petName != null );

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

				++stabled;

				if ( claimByName && !Insensitive.Equals( pet.Name, petName ) )
					continue;

				if ( CanClaim( from, pet ) )
				{
					DoClaim( from, pet );

					from.Stabled.RemoveAt( i );
					--i;

					claimed = true;
				}
				else
				{
					SayTo( from, 1049612, pet.Name ); // ~1_NAME~ remained in the stables because you have too many followers.
				}
			}

			if ( claimed )
				SayTo( from, 1042559 ); // Here you go... and good day to you!
			else if ( stabled == 0 )
				SayTo( from, 502671 ); // But I have no animals stabled with me at the moment!
			else if ( claimByName )
				BeginClaimList( from );
		}

		public bool CanClaim( Mobile from, BaseCreature pet )
		{
			return ((from.Followers + pet.ControlSlots) <= from.FollowersMax);
		}

		private void DoClaim( Mobile from, BaseCreature pet )
		{
			pet.SetControlMaster( from );

			if ( pet.Summoned )
				pet.SummonMaster = from;

			pet.Language = null;
			pet.ControlTarget = from;
			pet.ControlOrder = OrderType.Follow;

			pet.MoveToWorld( from.Location, from.Map );

			pet.IsStabled = false;

			if ( Core.SE )
				pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			return true;
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled && e.HasKeyword( 0x0008 ) ) // *stable*
			{
				e.Handled = true;
				
				CloseClaimList( e.Mobile );				
				BeginStable( e.Mobile );
			}
			else if ( !e.Handled && e.HasKeyword( 0x0009 ) ) // *claim*
			{
				e.Handled = true;
				
				CloseClaimList( e.Mobile );
				
				int index = e.Speech.IndexOf( ' ' );

				if ( index != -1 )
					Claim( e.Mobile, e.Speech.Substring( index ).Trim() );
				else
					Claim( e.Mobile );
			}
			else
			{
				base.OnSpeech( e );
			}
		}

		public AnimalTrainer( Serial serial ) : base( serial )
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
