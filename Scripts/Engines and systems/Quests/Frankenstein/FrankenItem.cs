using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class FrankenItem : Item
	{
		[Constructable]
		public FrankenItem() : base( 0x1FDD )
		{
			Name = "body part";
			Weight = 5.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( from.Skills[SkillName.Forensics].Value >= 30 )
			{
				from.SendMessage( "What journal do you want to commit this to?" );
				t = new BodyTarget( this );
				from.Target = t;
			}
			else
			{
				from.SendMessage( "This is pretty gross." );
			}
		}

		private class BodyTarget : Target
		{
			private FrankenItem m_Part;

			public BodyTarget( FrankenItem part ) : base( 1, false, TargetFlags.None )
			{
				m_Part = part;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				Item iJournal = targeted as Item;

				if ( from.Backpack.FindItemByType( typeof ( SewingKit ) ) == null )
				{
					from.SendMessage( "You need a sewing kit to add this body part." );
				}
				else if ( iJournal is FrankenJournal )
				{
					if ( !iJournal.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "The journal needs to be in your pack." );
					}
					else if ( iJournal.IsChildOf( from.Backpack ) )
					{
						FrankenJournal xJournal = (FrankenJournal)iJournal;

						if ( from != null && from == xJournal.JournalOwner )
						{
							if ( m_Part is FrankenLegLeft )
							{
								if ( xJournal.HasLegLeft > 0 ){ from.SendMessage( "You already have a left leg." ); }
								else { from.SendSound( 0x48 ); from.SendMessage( "You now have a left leg for this experiment." ); m_Part.Delete(); xJournal.HasLegLeft = 1; from.Backpack.FindItemByType( typeof ( SewingKit ) ).Delete(); }
							}
							else if ( m_Part is FrankenLegRight )
							{
								if ( xJournal.HasLegRight > 0 ){ from.SendMessage( "You already have a right leg." ); }
								else { from.SendSound( 0x48 ); from.SendMessage( "You now have a right leg for this experiment." ); m_Part.Delete(); xJournal.HasLegRight = 1; from.Backpack.FindItemByType( typeof ( SewingKit ) ).Delete(); }
							}
							else if ( m_Part is FrankenArmLeft )
							{
								if ( xJournal.HasArmLeft > 0 ){ from.SendMessage( "You already have a left arm." ); }
								else { from.SendSound( 0x48 ); from.SendMessage( "You now have a left arm for this experiment." ); m_Part.Delete(); xJournal.HasArmLeft = 1; from.Backpack.FindItemByType( typeof ( SewingKit ) ).Delete(); }
							}
							else if ( m_Part is FrankenArmRight )
							{
								if ( xJournal.HasArmRight > 0 ){ from.SendMessage( "You already have a right arm." ); }
								else { from.SendSound( 0x48 ); from.SendMessage( "You now have a right arm for this experiment." ); m_Part.Delete(); xJournal.HasArmRight = 1; from.Backpack.FindItemByType( typeof ( SewingKit ) ).Delete(); }
							}
							else if ( m_Part is FrankenHead )
							{
								if ( xJournal.HasHead > 0 ){ from.SendMessage( "You already have a head." ); }
								else { from.SendSound( 0x48 ); from.SendMessage( "You now have a head for this experiment." ); m_Part.Delete(); xJournal.HasHead = 1; from.Backpack.FindItemByType( typeof ( SewingKit ) ).Delete(); }
							}
							else if ( m_Part is FrankenTorso )
							{
								if ( xJournal.HasTorso > 0 ){ from.SendMessage( "You already have a torso." ); }
								else { from.SendSound( 0x48 ); from.SendMessage( "You now have a torso for this experiment." ); m_Part.Delete(); xJournal.HasTorso = 1; from.Backpack.FindItemByType( typeof ( SewingKit ) ).Delete(); }
							}
							else if ( m_Part is FrankenBrain )
							{
								FrankenBrain brain = (FrankenBrain)m_Part;

								if ( brain.BrainLevel > from.Skills[SkillName.Forensics].Value )
								{
									from.SendMessage( "Your forensics skill isn't good enough to work with this brain." );
								}
								else
								{
									if ( xJournal.HasBrain > 0 )
									{
										from.SendMessage( "The current brain is beginning to rot, so you throw it out." );
									}
									from.SendMessage( "You now have a fresh brain for this experiment." );
									from.SendSound( 0x48 ); 
									xJournal.HasBrain = brain.BrainLevel;
									xJournal.BrainFrom = brain.BrainSource;
									from.Backpack.FindItemByType( typeof ( SewingKit ) ).Delete(); 
									m_Part.Delete();
								}
							}
						}
						else
						{
							from.SendMessage( "That doesn't seem like a good idea." );
						}
					}
					else
					{
						from.SendMessage( "That doesn't seem like a good idea." );
					}
				}
				else
				{
					from.SendMessage( "That isn't Frankenstein's journal." );
				}
			}
		}

		public FrankenItem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}