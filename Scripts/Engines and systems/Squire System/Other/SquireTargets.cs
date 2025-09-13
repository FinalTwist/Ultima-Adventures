using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Misc;
using Server.SkillHandlers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Server.Targeting;
using Server.ContextMenus;
using Server.HuePickers;
using Server.Prompts;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Spells.Necromancy;
using Server.Spells;
using Server.Spells.Ninjitsu;
using Server.Spells.Chivalry; // Added 1.9.6
using Server.Multis; // Added 1.9.6

namespace Server.Mobiles
{
    internal class MountTarget : Target
    {
	private Squire m_Squire;

	public MountTarget( Mobile from, Squire squire ) : base( 1, false, TargetFlags.None )
	{
	    m_Squire = squire;
	    from.SendMessage( "Choose a mount for " + m_Squire.Name + " to ride." );
	}

	protected override void OnTarget( Mobile from, object obj )
	{
	    DoOnTarget( from, obj, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    EtherealMount ethy = o as EtherealMount;
	    if ( null != ethy )
	    {
		if ( null != ethy.Rider )
		    from.SendMessage( "This ethereal mount is already in use by someone else." );

		else if ( !ethy.IsChildOf( from.Backpack ) )
		    from.SendMessage( "The ethereal mount must be in your pack for you to use it." );
		else
		    ethy.Rider = squire;
	    }
	    else
	    {
		BaseMount mount = o as BaseMount;

		if ( null == mount )
		    from.SendMessage( "That is not a mount." );

		else if ( null != mount.Rider )
		    from.SendMessage( "This mount is already in use by someone else." );

		else if ( mount.ControlMaster != from )
		    from.SendMessage( "You do not own this mount." );
		else
		    mount.Rider = squire;
	    }
	}
    }

    internal class GrabItemTarget : Target
    {
	private Squire m_Squire;

	public GrabItemTarget( Mobile from, Squire squire ) : base( 20, false, TargetFlags.None )
	{
	    m_Squire = squire;

	    from.SendMessage( "What would you like " + squire.Name + " to grab?" );
	}

	protected override void OnTarget( Mobile from, object obj )
	{
	    DoOnTarget( from, obj, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    ArrayList items = new ArrayList();
	    bool rejected = false;
	    bool lootAdded = false;
	    if ( o is Item )
	    {
		Item item = ( Item )o;

		if ( squire.InRange( item, 2 ) )
		{
		    if ( item.Movable )
		    {
			items.Add( item );
			if( squire.m_SquireBeQuiet == false)//Added 1.8
			    squire.Emote( "*Picks the item off of the ground.*" );
		    }
		    else if ( item is Corpse )
		    {
			if( squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantLiftCorpse, null, null );//New SquireDialog 1.8
			}
		    }
		    foreach ( Item i in items )
		    {
			if ( !squire.Backpack.CheckHold( squire, i, false, true ) )
			    rejected = true;
			else
			{
			    bool isRejected;
			    LRReason reason;

			    squire.NextActionTime = Core.TickCount + 5000;
			    squire.Lift( i, i.Amount, out isRejected, out reason );

			    if ( !isRejected )
			    {
				squire.Drop( squire, Point3D.Zero );
				lootAdded = true;
			    }
			    else
			    {
				rejected = true;
			    }
			}
		    }
		    if ( lootAdded )
		    {
			squire.PlaySound( 0x2E6 ); //drop gold sound
		    }
		    if ( rejected )
		    {
			if( squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantLiftItem, null, null );//New SquireDialog 1.8
			}
		    }
		}
		else
		{
		    if( squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantReach, null, null );//New SquireDialog 1.8
		    }
		}
	    }
	    else
	    {
		if( squire.m_SquireBeQuiet == false )//Added 1.8
		{
		    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantLiftNotItem, null, null );//New SquireDialog 1.8
		}
	    }
	}
    }

    internal class LootCorpseTarget : Target
    {
	private Squire m_Squire;

	public LootCorpseTarget( Mobile from, Squire squire ) : base( 20, false, TargetFlags.None )
	{
	    m_Squire = squire;

	    from.SendMessage( "What body would you like " + squire.Name + " to loot?" );
	}

	protected override void OnTarget( Mobile from, object obj )
	{
	    DoOnTarget( from, obj, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    ArrayList items = new ArrayList();
	    ArrayList myitems = new ArrayList(); // Added 1.9.1
	    bool rejected = false;
	    bool lootAdded = false;
	    if ( o is Item )
	    {
		Item item = ( Item )o;

		if ( squire.InRange( item, 2 ) )
		{
		    if ( item.Movable )
		    {
			if( squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.ItemIsNotCorpse, null, null );//New SquireDialog 1.8
			}
		    }
		    else if ( item is Corpse )
		    {
			if( ((Corpse)item).Owner != null && ((Corpse)item).Owner.Player ) // Added check for non-null to avoid crashing. 1.9.1
			{
			    if( squire.m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.RefusesToLootPlayers, null, null );
			    }
			}
			else if ( ((Corpse)item).Owner != null && ((Corpse)item).Owner == squire ) // Added 1.9.1
			{
			    if( squire.m_SquireBeQuiet == false )
			    {
				squire.Emote( "*Rummages through items in a corpse.*" );
			    }
			    foreach ( Item corpseItem in ((Corpse)item).Items )
				myitems.Add( corpseItem );
			}
			else
			{
			    if( squire.m_SquireBeQuiet == false )
			    {
				squire.Emote( "*Rummages through items in a corpse.*" );
			    }

			    foreach ( Item corpseItem in ((Corpse)item).Items )
			    {
				items.Add( corpseItem );
			    }
			}
		    }
		    foreach ( Item i in items )
		    {
			if ( !squire.Backpack.CheckHold( squire, i, false, true ) )
			    rejected = true;
			else
			{
			    bool isRejected;
			    LRReason reason;

			    squire.NextActionTime = Core.TickCount + 5000;;
			    squire.Lift( i, i.Amount, out isRejected, out reason );

			    if ( !isRejected )
			    {
				if ( squire.AutoEquipLoot == false )
				{
				    squire.Drop( squire.Backpack, new Point3D( Utility.Random( 29, 108 ), Utility.Random( 34, 94 ), 0 ) ); //Randomly set them in their pack.
				}
				else
				{
				    squire.Drop( squire, Point3D.Zero );
				}
				lootAdded = true;
			    }
			    else
			    {
				rejected = true;
			    }
			}
		    }
		    foreach ( Item myitem in myitems ) // Added 1.9.1
		    {
			if ( !squire.Backpack.CheckHold( squire, myitem, false, true ) )
			    rejected = true;
			else
			{
			    bool isRejected;
			    LRReason reason;

			    squire.NextActionTime = Core.TickCount + 5000;
			    squire.Lift( myitem, myitem.Amount, out isRejected, out reason );

			    if ( !isRejected )
			    {
				squire.Drop( squire, Point3D.Zero );
				lootAdded = true;
			    }
			    else
			    {
				rejected = true;
			    }
			}
		    }
		    if ( lootAdded )
		    {
			squire.PlaySound( 0x2E6 ); //drop gold sound
		    }
		    if ( rejected )
		    {
			if( squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantLootAllItems, null, null );//New SquireDialog 1.8
			}
		    }
		}
		else
		{
		    if( squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantReach, null, null );//New SquireDialog 1.8
		    }
		}
	    }
	    else
	    {
		if( squire.m_SquireBeQuiet == false )//Added 1.8
		{
		    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantLootNotCorpse, null, null );//New SquireDialog 1.8
		}
	    }
	}
    }

    internal class HealTarget : Target
    {
	private Squire m_Squire;

	public HealTarget( Mobile from, Squire squire ) : base( 20, false, TargetFlags.None )
	{
	    m_Squire = squire;

	    from.SendMessage( "Who would you like " + squire.Name + " to heal?" );
	}

	protected override void OnTarget( Mobile from, object obj )
	{
	    DoOnTarget( from, obj, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    if ( DateTime.UtcNow > squire.m_Delay )
	    {
		if ( o is BaseCreature )
		{
		    BaseCreature wounded = ((BaseCreature)o);

		    if ( wounded.Hidden == false )
		    {
			if ( squire.InRange( wounded, Bandage.Range ) ) //Changed 1.8, from a range of 2 to call upon Bandage's Range for friendliness with PreAOS servers.
			{
			    if ( wounded.Alive == true && wounded.Poisoned )
			    {
				Item item = squire.Backpack.FindItemByType( typeof( Bandage ) );

				if ( wounded.Body == 0x191 || wounded.Body == 0x190 || wounded.Body == 0x25D || wounded.Body == 0x25E )
				{
				    if ( squire.Skills.Healing.Value >= 60.0 && squire.Skills.Anatomy.Value >= 60.0 && null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				    {
					item.Consume( 1 );
					squire.RevealingAction();
					if( squire.m_SquireBeQuiet == false )//Added 1.8
					{
					    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCuresHumanoid, wounded, null );//New SquireDialog 1.8
					}

					squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				    }
				}
				else
				{
				    if ( squire.Skills.Veterinary.Value >= 60.0 && squire.Skills.AnimalLore.Value >= 60.0 && null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				    {
					item.Consume( 1 );
					squire.RevealingAction();
					if( squire.m_SquireBeQuiet == false )//Added 1.8
					{
					    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCuresAnimal, wounded, null );//New SquireDialog 1.8
					}

					squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				    }
				}
			    }
			    else if ( wounded.Alive == true && wounded.Hits < wounded.HitsMax - 5 )
			    {
				Item item = squire.Backpack.FindItemByType( typeof( Bandage ) );

				if ( null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				{
				    item.Consume( 1 );
				    squire.RevealingAction();
				    if( squire.m_SquireBeQuiet == false )//Added 1.8
				    {
					SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireHealsWounded, wounded, null );//New SquireDialog 1.8
				    }

				    squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				}
			    }
			    else if ( wounded.Alive == false )
			    {
				Item item = squire.Backpack.FindItemByType( typeof( Bandage ) );

				if ( wounded.Body == 0x191 || wounded.Body == 0x190 || wounded.Body == 0x25D || wounded.Body == 0x25E )
				{
				    if ( squire.Skills.Healing.Value >= 80.0 && squire.Skills.Anatomy.Value >= 80.0 && null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				    {
					item.Consume( 1 );
					squire.RevealingAction();
					if( squire.m_SquireBeQuiet == false )//Added 1.8
					{
					    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireRezsHumanoid, wounded, null );//New SquireDialog 1.8
					}

					squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 9 );
				    }
				}
				else
				{
				    if ( squire.Skills.Veterinary.Value >= 80.0 && squire.Skills.AnimalLore.Value >= 80.0 && null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				    {
					item.Consume( 1 );
					squire.RevealingAction();
					if( squire.m_SquireBeQuiet == false )//Added 1.8
					{
					    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireRezsAnimal, wounded, null );//New SquireDialog 1.8
					}

					squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 9 );
				    }
				}
			    }
			    else if ( wounded.Alive == false && squire.Skills.Healing.Value >= 79.9 && squire.Skills.Anatomy.Value <= 79.9 )
			    {
				if( squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantRez, wounded, null );//New SquireDialog 1.8
				}
			    }
			    else
			    {
				if( squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.WoundedIsNotHurtEnough, wounded, null );//New SquireDialog 1.8
				}
			    }
			}
			else
			{
			    if( squire.m_SquireBeQuiet == false )//Added 1.8
			    {
				SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.WoundedOutOfRange, wounded, null );//New SquireDialog 1.8
			    }
			}
		    }
		    else
		    {
			if( squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.WoundedInvisible, null, null );//New SquireDialog 1.8
			}
		    }
		}
		else if ( o is PlayerMobile )
		{
		    PlayerMobile wounded = ((PlayerMobile)o);

		    if( wounded == squire.ControlMaster && squire.Controlled == true && squire.ControlMaster != null )
		    {
			if ( wounded.Hidden == false )
			{
			    if ( squire.InRange( wounded, Bandage.Range ) ) //Changed 1.8, from a range of 2 to call upon Bandage's Range for friendliness with PreAOS servers.
			    {
				if ( wounded.Alive == true && wounded.Poisoned )
				{
				    Item item = squire.Backpack.FindItemByType( typeof( Bandage ) );

				    if ( squire.Skills.Healing.Value >= 60.0 && squire.	Skills.Anatomy.Value >= 60.0 && null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				    {
					item.Consume( 1 );
					squire.RevealingAction();
					if( squire.m_SquireBeQuiet == false )//Added 1.8
					{
					    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCuresMaster, null, null );//New SquireDialog 1.8
					}

					squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				    }
				}
				else if ( wounded.Alive == true && wounded.Hits < wounded.HitsMax - 5 )
				{
				    Item item = squire.Backpack.FindItemByType( typeof( Bandage ) );

				    if ( null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				    {
					item.Consume( 1 );
					squire.RevealingAction();
					if( squire.m_SquireBeQuiet == false )//Added 1.8
					{
					    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireHealsMaster, null, null );//New SquireDialog 1.8
					}

					squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				    }
				}
				else if ( wounded.Alive == false )
				{
				    Item item = squire.Backpack.FindItemByType( typeof( Bandage ) );

				    if ( squire.Skills.Healing.Value >= 80.0 && squire.Skills.Anatomy.Value >= 80.0 && null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				    {
					item.Consume( 1 );
					squire.RevealingAction();
					if( squire.m_SquireBeQuiet == false )//Added 1.8
					{
					    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireRezsMaster, null, null );//New SquireDialog 1.8
					}

					squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 9 );
				    }
				}
				else if ( wounded.Alive == false && squire.Skills.Healing.Value >= 79.9 && squire.Skills.Anatomy.Value <= 79.9 )
				{
				    if( squire.m_SquireBeQuiet == false )//Added 1.8
				    {
					SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantRezPlayer, null, wounded );//New SquireDialog 1.8
				    }
				}
				else
				{
				    if( squire.m_SquireBeQuiet == false )//Added 1.8
				    {
					SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.WoundedPlayerIsNotHurtEnough, null, wounded );//New SquireDialog 1.8
				    }
				}
			    }
			    else
			    {
				if( squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.WoundedPlayerOutOfRange, null, wounded );//New SquireDialog 1.8
				}
			    }
			}
			else
			{
			    if( squire.m_SquireBeQuiet == false )//Added 1.8
			    {
				SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.WoundedInvisible, null, null );//New SquireDialog 1.8
			    }
			}
		    }
		    else if ( wounded.Hidden == false )
		    {
			if ( squire.InRange( wounded, Bandage.Range ) ) //Changed 1.8, from a range of 2 to call upon Bandage's Range for friendliness with PreAOS servers.
			{
			    if ( wounded.Alive == true && wounded.Poisoned )
			    {
				Item item = squire.Backpack.FindItemByType( typeof( Bandage ) );

				if ( squire.Skills.Healing.Value >= 60.0 && squire.	Skills.Anatomy.Value >= 60.0 && null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				{
				    item.Consume( 1 );
				    squire.RevealingAction();
				    if( squire.m_SquireBeQuiet == false )//Added 1.8
				    {
					SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCuresPlayer, null, wounded );//New SquireDialog 1.8
				    }

				    squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				}
			    }
			    else if ( wounded.Alive == true && wounded.Hits < wounded.HitsMax - 5 )
			    {
				Item item = squire.Backpack.FindItemByType( typeof( Bandage ) );

				if ( null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				{
				    item.Consume( 1 );
				    squire.RevealingAction();
				    if( squire.m_SquireBeQuiet == false )//Added 1.8
				    {
					SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireHealsPlayer, null, wounded );//New SquireDialog 1.8
				    }

				    squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 4 );
				}
			    }
			    else if ( wounded.Alive == false )
			    {
				Item item = squire.Backpack.FindItemByType( typeof( Bandage ) );

				if ( squire.Skills.Healing.Value >= 80.0 && squire.Skills.Anatomy.Value >= 80.0 && null != item && null != BandageContext.BeginHeal( squire, wounded ) )
				{
				    item.Consume( 1 );
				    squire.RevealingAction();
				    if( squire.m_SquireBeQuiet == false )//Added 1.8
				    {
					SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireRezsPlayer, null, wounded );//New SquireDialog 1.8
				    }

				    squire.m_Delay = DateTime.UtcNow + TimeSpan.FromSeconds( 9 );
				}
			    }
			    else if ( wounded.Alive == false && squire.Skills.Healing.Value >= 79.9 && squire.Skills.Anatomy.Value <= 79.9 )
			    {
				if( squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.SquireCantRezPlayer, null, wounded );//New SquireDialog 1.8
				}
			    }
			    else
			    {
				if( squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.WoundedPlayerIsNotHurtEnough, null, wounded );//New SquireDialog 1.8
				}
			    }
			}
			else
			{
			    if( squire.m_SquireBeQuiet == false )//Added 1.8
			    {
				SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.WoundedPlayerOutOfRange, null, wounded );//New SquireDialog 1.8
			    }
			}
		    }
		    else
		    {
			if( squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.WoundedInvisible, null, wounded );//New SquireDialog 1.8
			}
		    }
		}
		else
		{
		    if( squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.HealingTargetNotCreature, null, null );//New SquireDialog 1.8
		    }
		}
	    }
	    else
	    {
		if( squire.m_SquireBeQuiet == false )//Added 1.8
		{
		    squire.Say( "Sorry, I'm still finishing healing my current target..." );
		}
	    }
	}
    }

    internal class ThrowTarget : Target
    {
	private Squire m_Squire;

	public ThrowTarget( Mobile from, Squire squire ) : base( 20, false, TargetFlags.None )
	{
	    m_Squire = squire;

	    from.SendMessage( "Who would you like " + squire.Name + " to hit with a snowball?" );
	}

	protected override void OnTarget( Mobile from, object obj )
	{
	    DoOnTarget( from, obj, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    if ( squire.Mounted )
	    {
		from.SendMessage( "Your squire cannot do this while mounted." );
	    }
	    else
	    {
		if ( o is Mobile )
		{
		    Mobile target = (Mobile)o;
		    Container pack = target.Backpack;

		    if ( pack != null && pack.FindItemByType( new Type[]{ typeof( SnowPile ), typeof( PileOfGlacialSnow ) } ) != null )
		    {
			from.PlaySound( 0x145 );
			squire.Animate( 9, 1, 1, true, false, 0 );

			from.SendMessage( squire.Name + " throws a snowball and hits the target!" );
			Effects.SendMovingEffect( squire, target, 0x36E4, 7, 0, false, true, 0x480, 0 );
			squire.m_ThrowDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 1 );
		    }
		    else
		    {
			from.SendMessage( "Your squire cannot throw a snowball at something that cannot throw one back." );
		    }
		}
		else
		{
		    from.SendMessage( "Your squire cannot throw a snowball at something that cannot throw one back." );
		}
	    }
	}
    }

    internal class ProvokeTargetOne : Target
    {
	private BaseInstrument m_Instrument;
	private Squire m_Squire;

	public ProvokeTargetOne( Mobile from, Squire squire, BaseInstrument instrument ) : base( BaseInstrument.GetBardRange( squire, SkillName.Provocation ), false, TargetFlags.None )
	{
	    m_Instrument = instrument;
	    m_Squire = squire;
	    if( squire.m_SquireBeQuiet == false )//Added 1.8
	    {
		SquireDialog.DoSquireDialog( from, squire, SquireDialogTree.BeginProvoking, null, null );//New SquireDialog 1.8
	    }
	}

	protected override void OnTarget( Mobile from, object provokeone )
	{
	    m_Squire.RevealingAction();

	    if ( provokeone is BaseCreature && m_Squire.CanBeHarmful( (Mobile)provokeone, true ) )
	    {
		BaseCreature creature = (BaseCreature)provokeone;

		if ( !m_Instrument.IsChildOf( m_Squire.Backpack ) )
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.WheredMyInstrumentGo, null, null );//New SquireDialog 1.8
		    }
		}
		else if ( creature.Controlled )
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.LoyalToTheirMaster, null, null );//New SquireDialog 1.8
		    }
		}
		else if ( creature.IsParagon && BaseInstrument.GetBaseDifficulty( creature ) >= 160.0 )
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.NoChanceToProvoke, null, null );//New SquireDialog 1.8
		    }
		}
		else
		{
		    m_Squire.RevealingAction();
		    m_Instrument.PlayInstrumentWell( m_Squire );
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CantProvokeOne, null, null );//New SquireDialog 1.8
		    }
		    from.Target = new ProvokeTargetTwo( from, m_Squire, m_Instrument, creature );
		}
	    }
	    else
	    {
		if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		{
		    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CantInciteAnger, null, null );//New SquireDialog 1.8
		}
	    }
	}
    }

    internal class ProvokeTargetTwo : Target
    {
	private BaseCreature m_Creature;
	private BaseInstrument m_Instrument;
	private Squire m_Squire;

	public ProvokeTargetTwo( Mobile from, Squire squire, BaseInstrument instrument, BaseCreature creature ) : base( BaseInstrument.GetBardRange( squire, SkillName.Provocation ), false, TargetFlags.None )
	{
	    m_Instrument = instrument;
	    m_Squire = squire;
	    m_Creature = creature;
	}

	protected override void OnTarget( Mobile from, object provoketwo )
	{
	    m_Squire.RevealingAction();

	    if ( provoketwo is BaseCreature )
	    {
		BaseCreature creature = (BaseCreature)provoketwo;

		if ( !m_Instrument.IsChildOf( m_Squire.Backpack ) )
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.WheredMyInstrumentGo, null, null );//New SquireDialog 1.8
		    }
		}
		else if ( m_Creature.Unprovokable )
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.NoChanceToProvoke, null, null );//New SquireDialog 1.8
		    }
		}
		// else if ( creature.Unprovokable && !( creature is DemonKnight ) ) // original
		else if ( creature.Unprovokable )	
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.NoChanceToProvoke, null, null );//New SquireDialog 1.8
		    }
		}
		else if ( m_Creature.Map != creature.Map || !m_Creature.InRange( creature, BaseInstrument.GetBardRange( m_Squire, SkillName.Provocation ) ) )
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.TooFarApartToProvoke, null, null );//New SquireDialog 1.8
		    }
		}
		else if ( m_Creature != creature )
		{
		    m_Squire.NextSkillTime = Core.TickCount + (10 * 1000);
		    double diff = ((m_Instrument.GetDifficultyFor(from, m_Creature ) + m_Instrument.GetDifficultyFor(from, creature )) * 0.5) - 5.0;
		    double music = m_Squire.Skills[SkillName.Musicianship].Value;

		    if ( music > 100.0 )
			diff -= (music - 100.0) * 0.5;

		    if ( m_Squire.CanBeHarmful( m_Creature, true ) && m_Squire.CanBeHarmful( creature, true ) )
		    {
			if ( !BaseInstrument.CheckMusicianship( m_Squire ) )
			{
			    m_Squire.NextSkillTime = Core.TickCount + (5 * 1000);
			    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			    {
				SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.BadPerformance, null, null );//New SquireDialog 1.8
			    }
			    m_Instrument.PlayInstrumentBadly( m_Squire );
			    m_Instrument.ConsumeUse( m_Squire );
			}
			else
			{
			    if ( !m_Squire.CheckTargetSkill( SkillName.Provocation, creature, diff-25.0, diff+25.0 ) )
			    {
				m_Squire.NextSkillTime = Core.TickCount + (5 * 1000);
				if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.FailedPerformanceProvoke, null, null );//New SquireDialog 1.8
				}
				m_Instrument.PlayInstrumentBadly( m_Squire );
				m_Instrument.ConsumeUse( m_Squire );
			    }
			    else
			    {
				if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.GoodPerformanceProvoke, null, null );//New SquireDialog 1.8
				}
				m_Instrument.PlayInstrumentWell( m_Squire );
				m_Instrument.ConsumeUse( m_Squire );
				m_Creature.Provoke( m_Squire, creature, true );
			    }
			}
		    }
		}
		else
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.ProvokeOnThemselves, null, null );//New SquireDialog 1.8
		    }
		}
	    }
	    else
	    {
		if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		{
		    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CantInciteAnger, null, null );//New SquireDialog 1.8
		}
	    }
	}
    }

    internal class SquireDiscordanceTarget : Target
    {
	private BaseInstrument m_Instrument;
	private Squire m_Squire;

	public SquireDiscordanceTarget( Mobile from, Squire squire, BaseInstrument inst ) : base( BaseInstrument.GetBardRange( squire, SkillName.Discordance ), false, TargetFlags.None )
	{
	    m_Instrument = inst;
	    m_Squire = squire;
	}
	// protected //
	protected override void OnTarget( Mobile from, object target )
	{
	    m_Squire.RevealingAction();
	    m_Squire.NextSkillTime = Core.TickCount + 1000;

	    if ( !m_Instrument.IsChildOf( m_Squire.Backpack ) )
	    {
		if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		{
		    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.WheredMyInstrumentGo, null, null );//New SquireDialog 1.8
		}
	    }
	    else if ( target is Mobile )
	    {
		Mobile targ = (Mobile)target;

		if ( targ == m_Squire || targ == from || (targ is BaseCreature && ( ((BaseCreature)targ).BardImmune || !m_Squire.CanBeHarmful( targ, false ) ) && ((BaseCreature)targ).ControlMaster != from ) )
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CantDiscord, null, null );//New SquireDialog 1.8
		    }
		}
		else if ( Discordance.m_Table.Contains( targ ) )
		{
		    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.AlreadyDiscord, null, null );//New SquireDialog 1.8
		    }
		}
		else if ( !targ.Player )
		{
		    TimeSpan len = TimeSpan.FromSeconds( from.Skills[SkillName.Discordance].Value * 2 );
		    double diff = m_Instrument.GetDifficultyFor(from, targ ) - 10.0;
		    double music = m_Squire.Skills[SkillName.Musicianship].Value;

		    if ( music > 100.0 )
			diff -= (music - 100.0) * 0.5;

		    if ( !BaseInstrument.CheckMusicianship( m_Squire ) )
		    {
			if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.BadPerformance, null, null );//New SquireDialog 1.8
			}
			m_Instrument.PlayInstrumentBadly( m_Squire );
			m_Instrument.ConsumeUse( m_Squire );
		    }
		    else if ( m_Squire.CheckTargetSkill( SkillName.Discordance, target, diff-25.0, diff+25.0 ) )
		    {
			if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.GoodPerformanceDiscord, null, null );//New SquireDialog 1.8
			}
			m_Instrument.PlayInstrumentWell( m_Squire );
			m_Instrument.ConsumeUse( m_Squire );

			ArrayList mods = new ArrayList();
			double scalar;

			if ( Core.AOS )
			{
				mods.Add( new ResistanceMod( ResistanceType.Physical, Discordance.ReduceValue(m_Squire, targ, targ.GetResistance(ResistanceType.Physical)) ) );
				mods.Add( new ResistanceMod( ResistanceType.Fire, Discordance.ReduceValue(m_Squire, targ, targ.GetResistance(ResistanceType.Fire)) ) );
				mods.Add( new ResistanceMod( ResistanceType.Cold, Discordance.ReduceValue(m_Squire, targ, targ.GetResistance(ResistanceType.Cold)) ) );
				mods.Add( new ResistanceMod( ResistanceType.Poison, Discordance.ReduceValue(m_Squire, targ, targ.GetResistance(ResistanceType.Poison)) ) );
				mods.Add( new ResistanceMod( ResistanceType.Energy, Discordance.ReduceValue(m_Squire, targ, targ.GetResistance(ResistanceType.Energy)) ) );

			    for ( int i = 0; i < targ.Skills.Length; ++i )
			    {
				if ( targ.Skills[i].Value > 0 )
				    mods.Add( new DefaultSkillMod( (SkillName)i, true, targ.Skills[i].Value * Discordance.Scalar(Discordance.Effect(from, targ)) ) );
			    }
			}
			else
			{
				mods.Add( new StatMod( StatType.Str, "DiscordanceStr", Discordance.ReduceValue(m_Squire, targ, targ.RawStr), TimeSpan.Zero ) );
				mods.Add( new StatMod( StatType.Int, "DiscordanceInt", Discordance.ReduceValue(m_Squire, targ, targ.RawInt), TimeSpan.Zero ) );
				mods.Add( new StatMod( StatType.Dex, "DiscordanceDex", Discordance.ReduceValue(m_Squire, targ, targ.RawDex), TimeSpan.Zero ) );

			    for ( int i = 0; i < targ.Skills.Length; ++i )
			    {
				if ( targ.Skills[i].Value > 0 )
				    mods.Add( new DefaultSkillMod( (SkillName)i, true, targ.Skills[i].Value * Discordance.Scalar(Discordance.Effect(from, targ)) ) );
			    }
			}

			//+++ Discordance.DiscordanceInfo info = new Discordance.DiscordanceInfo( m_Squire, targ, len, Math.Abs( effect ), mods );
			Discordance.DiscordanceInfo info = new Discordance.DiscordanceInfo( m_Squire, targ, Math.Abs( Discordance.Effect(m_Squire, targ) ), mods );
			info.m_Timer = Timer.DelayCall<Discordance.DiscordanceInfo>( TimeSpan.Zero, TimeSpan.FromSeconds( 1.25 ), new TimerStateCallback<Discordance.DiscordanceInfo>( Discordance.ProcessDiscordance ), info );

			Discordance.m_Table[targ] = info;
		    }


		    else
		    {
			if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.FailedPerformanceDiscord, null, null );//New SquireDialog 1.8
			}
			m_Instrument.PlayInstrumentBadly( m_Squire );
			m_Instrument.ConsumeUse( m_Squire );
		    }

		    m_Squire.NextSkillTime = Core.TickCount + (12 * 1000);
		}
		else
		{
		    m_Instrument.PlayInstrumentBadly( m_Squire );
		}
	    }
	    else
	    {
		if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
		{
		    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CantDiscord, null, null );//New SquireDialog 1.8
		}
	    }
	}
    }

    internal class SquirePeacemakingTarget : Target
    {
	private BaseInstrument m_Instrument;
	private bool m_SetSkillTime = true;
	private Squire m_Squire;

	public SquirePeacemakingTarget( Mobile from, Squire squire, BaseInstrument instrument ) :  base( BaseInstrument.GetBardRange( squire, SkillName.Peacemaking ), false, TargetFlags.None )
	{
	    m_Instrument = instrument;
	    m_Squire = squire;
	}

	protected override void OnTargetFinish( Mobile from )
	{
	    if ( m_SetSkillTime )
		from.NextSkillTime = Core.TickCount; //Updated NEXTVERSION for Compatibility
	}

	protected override void OnTarget( Mobile from, object targeted )
	{
	    from.RevealingAction();

	    if ( !(targeted is Mobile) )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CantCalm, null, null );//New SquireDialog 1.8
		}
	    } //Removed ConPVP Check NEXTVERSION for Compatibility
	    else if ( !m_Instrument.IsChildOf( m_Squire.Backpack ) )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.WheredMyInstrumentGo, null, null );//New SquireDialog 1.8
		}
	    }
	    else
	    {
		m_SetSkillTime = false;
		m_Squire.NextSkillTime = Core.TickCount + (10 * 1000);

		if ( targeted == from || targeted == m_Squire )
		{
		    if ( !BaseInstrument.CheckMusicianship( m_Squire ) )
		    {
			if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.BadPerformance, null, null );//New SquireDialog 1.8
			}
			m_Instrument.PlayInstrumentBadly( m_Squire );
			m_Instrument.ConsumeUse( m_Squire );
		    }
		    else if ( !m_Squire.CheckSkill( SkillName.Peacemaking, 0.0, 120.0 ) )
		    {
			if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.BadPerformance, null, null );//New SquireDialog 1.8
			}
			m_Instrument.PlayInstrumentBadly( m_Squire );
			m_Instrument.ConsumeUse( m_Squire );
		    }
		    else
		    {
			m_Squire.NextSkillTime = Core.TickCount + (5 * 1000);
			m_Instrument.PlayInstrumentWell( m_Squire );
			m_Instrument.ConsumeUse( m_Squire );

			Map map = m_Squire.Map;

			if ( map != null )
			{
			    int range = BaseInstrument.GetBardRange( m_Squire, SkillName.Peacemaking );

			    bool calmed = false;

			    foreach ( Mobile m in m_Squire.GetMobilesInRange( range ) )
			    {
				if ((m is BaseCreature && ((BaseCreature)m).Uncalmable) || (m is BaseCreature && ((BaseCreature)m).AreaPeaceImmune) || m == from || m == m_Squire || !m_Squire.CanBeHarmful ( m, false ))
				    continue;

				calmed = true;

				m.SendLocalizedMessage( 500616 ); // You hear lovely music, and forget to continue battling!
				m.Combatant = null;
				m.Warmode = false;

				if ( m is BaseCreature && !((BaseCreature)m).BardPacified )
				    ((BaseCreature)m).Pacify( m_Squire, DateTime.UtcNow + TimeSpan.FromSeconds( 1.0 ) );
			    }

			    if ( !calmed )
				if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.PeaceNobody, null, null );//New SquireDialog 1.8
				}
				else
				    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
				    {
					SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.GoodPerformancePeace, null, null );//New SquireDialog 1.8
				    }
			}
		    }
		}
		else
		{
		    Mobile targ = (Mobile)targeted;

		    if ( !m_Squire.CanBeHarmful( targ, false ) )
		    {
			if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CantCalm, null, null );//New SquireDialog 1.8
			}
			m_SetSkillTime = true;
		    }
		    else if ( targ is BaseCreature && ((BaseCreature)targ).Uncalmable )
		    {
			if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.NoChanceToCalm, null, null );//New SquireDialog 1.8
			}
			m_SetSkillTime = true;
		    }
		    else if ( targ is BaseCreature && ((BaseCreature)targ).BardPacified )
		    {
			if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.AlreadyCalmed, null, null );//New SquireDialog 1.8
			}
			m_SetSkillTime = true;
		    }
		    else if ( !BaseInstrument.CheckMusicianship( m_Squire ) )
		    {
			if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.BadPerformance, null, null );//New SquireDialog 1.8
			}
			m_Squire.NextSkillTime = Core.TickCount + (5 * 1000);
			m_Instrument.PlayInstrumentBadly( m_Squire );
			m_Instrument.ConsumeUse( m_Squire );
		    }
		    else
		    {
			double diff = m_Instrument.GetDifficultyFor(from, targ ) - 10.0;
			double music = m_Squire.Skills[SkillName.Musicianship].Value;

			if ( music > 100.0 )
			    diff -= (music - 100.0) * 0.5;

			if ( !m_Squire.CheckTargetSkill( SkillName.Peacemaking, targ, diff - 25.0, diff + 25.0 ) )
			{
			    if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
			    {
				SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.BadPerformance, null, null );//New SquireDialog 1.8
			    }
			    m_Instrument.PlayInstrumentBadly( m_Squire );
			    m_Instrument.ConsumeUse( m_Squire );
			}
			else
			{
			    m_Instrument.PlayInstrumentWell( m_Squire );
			    m_Instrument.ConsumeUse( m_Squire );

			    m_Squire.NextSkillTime = Core.TickCount + (5 * 1000);
			    if ( targ is BaseCreature )
			    {
				BaseCreature bc = (BaseCreature)targ;

				if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.GoodPerformancePeace, null, null );//New SquireDialog 1.8
				}

				targ.Combatant = null;
				targ.Warmode = false;

				double seconds = 100 - (diff / 1.5);

				if ( seconds > 120 )
				    seconds = 120;
				else if ( seconds < 10 )
				    seconds = 10;

				bc.Pacify( m_Squire, DateTime.UtcNow + TimeSpan.FromSeconds( seconds ) );
			    }
			    else
			    {
				if( m_Squire.m_SquireBeQuiet == false )//Added 1.8
				{
				    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.GoodPerformancePeace, null, null );//New SquireDialog 1.8
				}

				targ.SendLocalizedMessage( 500616 ); // You hear lovely music, and forget to continue battling!
				targ.Combatant = null;
				targ.Warmode = false;
			    }
			}
		    }
		}
	    }
	}
    }
    internal class SquireStealingTarget : Target
    {
	private Mobile m_From;
	private Squire m_Squire;

	public SquireStealingTarget( Mobile from, Squire squire ) : base ( 20, false, TargetFlags.None ) //Changed NEXTVERSION from base( 1, false, to base( 20, false to make it so you don't need to be next to the target
	{
	    m_From = from;
	    m_Squire = squire;

	    AllowNonlocal = true;
	}

	private Item TryStealItem( Item toSteal, ref bool caught )
	{
	    Item stolen = null;

	    object root = toSteal.RootParent;

	    StealableArtifactsSpawner.StealableInstance si = null;
	    if ( toSteal.Parent == null || !toSteal.Movable )
		si = StealableArtifactsSpawner.GetStealableInstance( toSteal );

	    if ( !Stealing.IsEmptyHanded( m_Squire ) )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.HandsAreFull, null, null );
		}
	    } //Removed ConPVP Zone Check NEXTVERSION for Compatibility
	    else if ( root is Mobile && ((Mobile)root).Player && !Stealing.IsInGuild( m_From ) )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.NotAPartOfThievesGuild, null, null );
		}
	    }
	    else if ( Stealing.SuspendOnMurder && root is Mobile && ((Mobile)root).Player && Stealing.IsInGuild( m_From ) && m_From.Kills > 0 )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.SuspendedFromThievesGuild, null, null );
		}
	    }
	    else if ( root is BaseVendor && ((BaseVendor)root).IsInvulnerable )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotStealFromVendors, null, null );
		}
	    }
	    else if ( root is PlayerVendor )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotStealFromVendors, null, null );
		}
	    }
	    else if ( !m_Squire.CanSee( toSteal ) )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotSeeStealingTarget, null, null );
		}
	    }
	    else if ( m_Squire.Backpack == null || !m_Squire.Backpack.CheckHold( m_Squire, toSteal, false, true ) )
	    {
			if( m_Squire.m_SquireBeQuiet == false )
			{
				SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.FullBackpackStealing, null, null );
			}
	    }

	    else if ( si == null && ( toSteal.Parent == null || !toSteal.Movable ) )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotStealThat, null, null );
		}
	    }
	    else if ( toSteal.LootType == LootType.Newbied || toSteal.CheckBlessed( root ) )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotStealThat, null, null );
		}
	    }
	    else if ( Core.AOS && si == null && toSteal is Container )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotStealThat, null, null );
		}
	    }
	    else if ( !m_Squire.InRange( toSteal.GetWorldLocation(), 1 ) )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.NeedToBeCloserToSteal, null, null );
		}
	    }
	    else if ( si != null && m_Squire.Skills[SkillName.Stealing].Value < 100.0 )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.NotSkilledEnoughToStealItem, null, null );
		}
	    }
	    else if ( toSteal.Parent is Mobile )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotStealFromTheirHands, null, null );
		}
	    }
	    else if ( root == m_Squire )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.StealFromSelf, null, null );
		}
	    }
	    else if ( root is Mobile && ((Mobile)root).AccessLevel > AccessLevel.Player )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotStealThat, null, null );
		}
	    }
	    else if ( root is Mobile && !m_Squire.CanBeHarmful( (Mobile)root ) )
	    {
	    }
	    else if ( root is Corpse )
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotStealThat, null, null );
		}
	    }
	    else
	    {
		double w = toSteal.Weight + toSteal.TotalWeight;

		if ( w > 10 )
		{
		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.TooHeavyToSteal, null, null );
		    }
		}
		else
		{
		    if ( toSteal.Stackable && toSteal.Amount > 1 )
		    {
			int maxAmount = (int)((m_Squire.Skills[SkillName.Stealing].Value / 10.0) / toSteal.Weight);

			if ( maxAmount < 1 )
			    maxAmount = 1;
			else if ( maxAmount > toSteal.Amount )
			    maxAmount = toSteal.Amount;

			int amount = Utility.RandomMinMax( 1, maxAmount );

			if ( amount >= toSteal.Amount )
			{
			    int pileWeight = (int)Math.Ceiling( toSteal.Weight * toSteal.Amount );
			    pileWeight *= 10;

			    if ( m_Squire.CheckTargetSkill( SkillName.Stealing, toSteal, pileWeight - 22.5, pileWeight + 27.5 ) )
				stolen = toSteal;
			}
			else
			{
			    int pileWeight = (int)Math.Ceiling( toSteal.Weight * amount );
			    pileWeight *= 10;

			    if ( m_Squire.CheckTargetSkill( SkillName.Stealing, toSteal, pileWeight - 22.5, pileWeight + 27.5 ) )
			    {
				stolen = Mobile.LiftItemDupe( toSteal, toSteal.Amount - amount );

				if ( stolen == null )
				    stolen = toSteal;
			    }
			}
		    }
		    else
		    {
			int iw = (int)Math.Ceiling( w );
			iw *= 10;

			if ( m_Squire.CheckTargetSkill( SkillName.Stealing, toSteal, iw - 22.5, iw + 27.5 ) )
			    stolen = toSteal;
		    }

		    if ( stolen != null )
		    {
			if( m_Squire.m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.SuccessfulSteal, null, null );
			}

			if ( si != null )
			{
			    toSteal.Movable = true;
			    si.Item = null;
			}
		    }
		    else
		    {
			if( m_Squire.m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.UnsuccessfulSteal, null, null );
			}
		    }

		    caught = ( m_Squire.Skills[SkillName.Stealing].Value < Utility.Random( 150 ) );
		}
	    }

	    m_Squire.m_StealingDelay = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
	    return stolen;
	}

	protected override void OnTarget( Mobile m_From, object target )
	{
	    m_Squire.RevealingAction();

	    Item stolen = null;
	    object root = null;
	    bool caught = false;

	    if ( target is Item )
	    {
		root = ((Item)target).RootParent;
		stolen = TryStealItem( (Item)target, ref caught );
	    } 
	    else if ( target is Mobile )
	    {
		Container pack = ((Mobile)target).Backpack;

		if ( pack != null && pack.Items.Count > 0 )
		{
		    int randomIndex = Utility.Random( pack.Items.Count );

		    root = target;
		    stolen = TryStealItem( pack.Items[randomIndex], ref caught );
		}
	    } 
	    else 
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.CannotStealThat, null, null );
		}
	    }

	    if ( stolen != null )
	    {
		m_Squire.AddToBackpack( stolen );

		if ( !( stolen is Container || stolen.Stackable ) ) 
		{
		    StolenItem.Add( stolen, m_Squire, root as Mobile );
		}
	    }

	    if ( caught )
	    {
		if ( root == null )
		{
		    m_Squire.CriminalAction( false );
		}
		else if ( root is Corpse && ((Corpse)root).IsCriminalAction( m_Squire ) )
		{
		    m_Squire.CriminalAction( false );
		}
		else if ( root is Mobile )
		{
		    Mobile mobRoot = (Mobile)root;

		    if ( !Stealing.IsInGuild( mobRoot ) && Stealing.IsInnocentTo( m_Squire, mobRoot ) )
			m_Squire.CriminalAction( false );

		    string message = String.Format( "You notice {0} trying to steal from {1}.", m_Squire.Name, mobRoot.Name );

		    foreach ( NetState ns in m_Squire.GetClientsInRange( 8 ) )
		    {
			if ( ns.Mobile != m_Squire )
			    ns.Mobile.SendMessage( message );
		    }
		}
	    }
	    else if ( root is Corpse && ((Corpse)root).IsCriminalAction( m_Squire ) )
	    {
		m_Squire.CriminalAction( false );
	    }

	    if ( root is Mobile && ((Mobile)root).Player && m_From is PlayerMobile && Stealing.IsInnocentTo( m_From, (Mobile)root ) && !Stealing.IsInGuild( (Mobile)root ) )
	    {
		PlayerMobile pm = (PlayerMobile)m_From;

		pm.PermaFlags.Add( (Mobile)root );
		pm.Delta( MobileDelta.Noto );
	    }
	}
    }
    internal class SquireLockpickTarget : Target
    {
	private Lockpick m_Item;
	private Squire m_Squire;

	public SquireLockpickTarget( Squire squire, Lockpick item ) : base( 20, false, TargetFlags.None ) //Changed NEXTVERSION from base( 1, false, to base( 20, false to make it so you don't need to be next to the target
	{
	    m_Item = item;
	    m_Squire = squire;
	}

	protected override void OnTarget( Mobile from, object targeted )
	{
	    if ( m_Item.Deleted )
		return;

	    if ( targeted is ILockpickable )
	    {
		Item item = (Item)targeted;
		m_Squire.Direction = m_Squire.GetDirectionTo( item );

		if ( !m_Squire.InRange( item.GetWorldLocation(), 1 ) )
		{
		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.LockpickTooFar, null, null );
		    }
		}
		else if ( ((ILockpickable)targeted).Locked )
		{
		    m_Squire.PlaySound( 0x241 );

		    new InternalTimer( from, m_Squire, (ILockpickable)targeted, m_Item ).Start();
		}
		else
		{
		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.NotLocked, null, null );
		    }
		}
	    }
	    else
	    {
		if( m_Squire.m_SquireBeQuiet == false )
		{
		    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CannotUnlock, null, null );
		}
	    }
	}

	private class InternalTimer : Timer
	{
	    private Mobile m_From;
	    private ILockpickable m_Item;
	    private Lockpick m_Lockpick;
	    private Squire m_Squire;

	    public InternalTimer( Mobile from, Squire squire, ILockpickable item, Lockpick lockpick ) : base( TimeSpan.FromSeconds( 3.0 ) )
	    {
		m_From = from;
		m_Squire = squire;
		m_Item = item;
		m_Lockpick = lockpick;
		Priority = TimerPriority.TwoFiftyMS;
	    }

	    protected void BrokeLockPickTest()
	    {
		if ( Utility.Random( 4 ) == 0 )
		{
		    Item item = (Item)m_Item;

		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.BrokenLockpick, null, null );
		    }

		    m_Squire.PlaySound( 0x3A4 );
		    m_Lockpick.Consume();
		}
	    }

	    protected override void OnTick()
	    {
		Item item = (Item)m_Item;

		if ( !m_Squire.InRange( item.GetWorldLocation(), 1 ) )
		    return;

		if ( m_Item.LockLevel == 0 || m_Item.LockLevel == -255 )
		{
		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.AbnormalLock, null, null );
		    }
		    return;
		}

		if ( m_Squire.Skills[SkillName.Lockpicking].Value < m_Item.RequiredSkill )
		{
		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.HardLock, null, null );
		    }
		    return;
		}

		if ( m_Squire.CheckTargetSkill( SkillName.Lockpicking, m_Item, m_Item.LockLevel, m_Item.MaxLockLevel ) )
		{
		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.SuccessfulLockpick, null, null );
		    }
		    m_Squire.PlaySound( 0x4A );
		    m_Item.LockPick( m_Squire );
		}
		else
		{
		    BrokeLockPickTest();
		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.UnsuccessfulLockpick, null, null );
		    }
		}
	    }
	}
    }
    internal class SquirePoisonTarget : Target
    {
	private Squire m_Squire;
	private Mobile m_From;

	public SquirePoisonTarget( Mobile from, Squire squire ) :  base ( 20, false, TargetFlags.None )
	{
	    m_Squire = squire;
	    m_From = from;
	}

	protected override void OnTarget( Mobile from, object targeted )
	{
	    if ( targeted is Item )
	    {
		if ( ( ( ( Item )targeted ).IsChildOf( from.Backpack ) && m_Squire.InRange( m_Squire.ControlMaster, 2 ) ) || ( ( Item )targeted ).IsChildOf( m_Squire.Backpack ) || m_Squire.InRange( ( ( Item )targeted ), 2 ) )
		{
		    if ( targeted is BasePoisonPotion )
		    {
			if( m_Squire.m_SquireBeQuiet == false )
			{
			    if( m_Squire.m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.ApplyPoisonTo, null, null );
			    }
			}
			from.Target = new SquirePoisoningTarget( (BasePoisonPotion)targeted, m_From, m_Squire );
		    }
		    else // Not a Poison Potion
		    {
			if( m_Squire.m_SquireBeQuiet == false )
			{
			    if( m_Squire.m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.NotAPoisonPotion, null, null );
			    }
			}
		    }
		}
		else if ( ( ( Item )targeted ).IsChildOf( from.Backpack ) && !m_Squire.InRange( m_Squire.ControlMaster, 2 ) )
		{
		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			if( m_Squire.m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.TooFarToPoison, null, null );
			}
		    }
		}
	    }
	}

	private class SquirePoisoningTarget : Target
	{
	    private BasePoisonPotion m_Potion;
	    private Mobile m_From;
	    private Squire m_Squire;

	    public SquirePoisoningTarget( BasePoisonPotion potion, Mobile from, Squire squire ) :  base ( 20, false, TargetFlags.None )
	    {
		m_Potion = potion;
		m_From = from;
		m_Squire = squire;
	    }

	    protected override void OnTarget( Mobile from, object targeted )
	    {
		if ( m_Potion.Deleted )
		    return;

		bool startTimer = false;

		if ( ( ( ( Item )targeted ).IsChildOf( from.Backpack ) && m_Squire.InRange( m_Squire.ControlMaster, 2 ) ) || ( ( Item )targeted ).IsChildOf( m_Squire.Backpack ) || m_Squire.InRange( ( ( Item )targeted ), 2 ) )
		{
		    if ( targeted is Food || targeted is FukiyaDarts || targeted is Shuriken )
		    {
			startTimer = true;
		    }
		    else if ( targeted is BaseWeapon )
		    {
			BaseWeapon weapon = (BaseWeapon)targeted;

			if ( Core.AOS )
			{
			    startTimer = ( weapon.PrimaryAbility == WeaponAbility.InfectiousStrike || weapon.SecondaryAbility == WeaponAbility.InfectiousStrike );
			}
			else if ( weapon.Layer == Layer.OneHanded )
			{
			    startTimer = ( weapon.Type == WeaponType.Slashing || weapon.Type == WeaponType.Piercing );
			}
		    }

		    if ( startTimer )
		    {
			new SquirePoisonTimer( m_Squire, m_From, (Item)targeted, m_Potion ).Start();
			m_Squire.PlaySound( 0x4F );
			m_Potion.Consume();
			m_Squire.AddToBackpack( new Bottle() );
		    }
		    else // Target can't be poisoned
		    {
			if ( Core.AOS )
			    if( m_Squire.m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CannotPoisonNotInfectious, null, null );
			    }
			    else
				if( m_Squire.m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.CannotPoisonNotBPFoD, null, null );
				}
		    }
		}
		else if ( ( ( Item )targeted ).IsChildOf( from.Backpack ) && !m_Squire.InRange( m_Squire.ControlMaster, 2 ) )
		{
		    if( m_Squire.m_SquireBeQuiet == false )
		    {
			if( m_Squire.m_SquireBeQuiet == false )
			{
			    SquireDialog.DoSquireDialog( from, m_Squire, SquireDialogTree.TooFarToPoison, null, null );
			}
		    }
		}
	    }

	    private class SquirePoisonTimer : Timer
	    {
		private Squire m_Squire;
		private Mobile m_From;
		private Item m_Target;
		private Poison m_Poison;
		private double m_MinSkill, m_MaxSkill;

		public SquirePoisonTimer( Squire squire, Mobile from, Item target, BasePoisonPotion potion ) : base( TimeSpan.FromSeconds( 2.0 ) )
		{
		    m_Squire = squire;
		    m_From = from;
		    m_Target = target;
		    m_Poison = potion.Poison;
		    m_MinSkill = potion.MinPoisoningSkill;
		    m_MaxSkill = potion.MaxPoisoningSkill;
		    Priority = TimerPriority.TwoFiftyMS;
		}

		protected override void OnTick()
		{
		    if ( m_Squire.CheckTargetSkill( SkillName.Poisoning, m_Target, m_MinSkill, m_MaxSkill ) )
		    {
			if ( m_Target is Food )
			{
			    ((Food)m_Target).Poison = m_Poison;
			}
			else if ( m_Target is BaseWeapon )
			{
			    ((BaseWeapon)m_Target).Poison = m_Poison;
			    ((BaseWeapon)m_Target).PoisonCharges = 18 - (m_Poison.Level * 2);
			}
			else if ( m_Target is FukiyaDarts )
			{
			    ((FukiyaDarts)m_Target).Poison = m_Poison;
			    ((FukiyaDarts)m_Target).PoisonCharges = Math.Min( 18 - (m_Poison.Level * 2), ((FukiyaDarts)m_Target).UsesRemaining );
			}
			else if ( m_Target is Shuriken )
			{
			    ((Shuriken)m_Target).Poison = m_Poison;
			    ((Shuriken)m_Target).PoisonCharges = Math.Min( 18 - (m_Poison.Level * 2), ((Shuriken)m_Target).UsesRemaining );
			}

			if( m_Squire.m_SquireBeQuiet == false )
			{
			    if( m_Squire.m_SquireBeQuiet == false )
			    {
				SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.PoisoningSuccess, null, null );
			    }
			}
			Misc.Titles.AwardKarma( m_Squire, -20, true );
		    }
		    else // Failed
		    {
			// 5% of chance of getting poisoned if failed
			if ( m_Squire.Skills[SkillName.Poisoning].Base < 80.0 && Utility.Random( 20 ) == 0 )
			{
			    if( m_Squire.m_SquireBeQuiet == false )
			    {
				if( m_Squire.m_SquireBeQuiet == false )
				{
				    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.TerribleMistake, null, null );
				}
			    }
			    m_Squire.ApplyPoison( m_Squire, m_Poison );
			}
			else
			{
			    if ( m_Target is BaseWeapon )
			    {
				BaseWeapon weapon = (BaseWeapon)m_Target;

				if ( weapon.Type == WeaponType.Slashing )
				    if( m_Squire.m_SquireBeQuiet == false )
				    {
					if( m_Squire.m_SquireBeQuiet == false )
					{
					    SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.PoisoningFailure, null, null );
					}
				    }
				    else
					if( m_Squire.m_SquireBeQuiet == false )
					{
					    if( m_Squire.m_SquireBeQuiet == false )
					    {
						SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.PoisoningFailure, null, null );
					    }
					}
			    }
			    else
			    {
				if( m_Squire.m_SquireBeQuiet == false )
				{
				    if( m_Squire.m_SquireBeQuiet == false )
				    {
					SquireDialog.DoSquireDialog( m_From, m_Squire, SquireDialogTree.PoisoningFailure, null, null );
				    }
				}
			    }
			}
		    }
		}
	    }
	}
    }
    internal class SquireCleanseDelayTimer : Timer
    {
	private Squire m_Squire;
	private Mobile m_From;
	private TimeSpan m_DelayTime;

	public SquireCleanseDelayTimer( Squire squire, Mobile from, TimeSpan delay ) : base( delay )
	{
	    m_Squire = squire;
	    m_From = from;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    m_From.Target = new SquirePassCleanseTarget( m_From, m_Squire );
	}
    }
    internal class SquireCloseWoundsDelayTimer : Timer
    {
	private Squire m_Squire;
	private Mobile m_From;
	private TimeSpan m_DelayTime;

	public SquireCloseWoundsDelayTimer( Squire squire, Mobile from, TimeSpan delay ) : base( delay )
	{
	    m_Squire = squire;
	    m_From = from;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    m_From.Target = new SquirePassCloseWoundsTarget( m_From, m_Squire );
	}
    }
    internal class SquireRemoveCurseDelayTimer : Timer
    {
	private Squire m_Squire;
	private Mobile m_From;
	private TimeSpan m_DelayTime;

	public SquireRemoveCurseDelayTimer( Squire squire, Mobile from, TimeSpan delay ) : base( delay )
	{
	    m_Squire = squire;
	    m_From = from;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    m_From.Target = new SquirePassRemoveCurseTarget( m_From, m_Squire );
	}
    }
    internal class SquireSacredJourneyDelayTimer : Timer
    {
	private Squire m_Squire;
	private Mobile m_From;
	private TimeSpan m_DelayTime;

	public SquireSacredJourneyDelayTimer( Squire squire, Mobile from, TimeSpan delay ) : base( delay )
	{
	    m_Squire = squire;
	    m_From = from;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    m_From.Target = new SquirePassSacredJourneyTarget( m_From, m_Squire );
	}
    }
    internal class SquirePassCleanseTarget : Target
    {
	private Mobile m_From;
	private Squire m_Squire;

	public SquirePassCleanseTarget( Mobile from, Squire squire ) : base( 20, false, TargetFlags.Beneficial )
	{
	    m_From = from;
	    m_Squire = squire;
	}

	protected override void OnTarget( Mobile from, object o )
	{
	    DoOnTarget( from, o, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    if( o is Mobile )
	    {
		if( !((Mobile)o).Poisoned )
		{
		    from.SendLocalizedMessage( 1060176 );
		}
		else
		{
		    if( squire.Target != null ) // Added 1.9.6.3
		    {
			squire.Target.Invoke( squire, o );
		    }
		}
	    }
	    else
	    {
		from.SendMessage( "This cannot be cleansed by fire." );
	    }
	}
    }
    internal class SquirePassCloseWoundsTarget : Target
    {
	private Mobile m_From;
	private Squire m_Squire;

	public SquirePassCloseWoundsTarget( Mobile from, Squire squire ) : base( 20, false, TargetFlags.Beneficial )
	{
	    m_From = from;
	    m_Squire = squire;
	}

	protected override void OnTarget( Mobile from, object o )
	{
	    DoOnTarget( from, o, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    if( o is Mobile )
	    {
		if ( !squire.InRange( ((Mobile)o), 2 ) )
		{
		    from.SendLocalizedMessage( 1060178 ); // You are too far away to perform that action!
		}
		else if ( ((Mobile)o) is BaseCreature && ((BaseCreature)o).IsAnimatedDead )
		{
		    from.SendLocalizedMessage( 1061654 ); // You cannot heal that which is not alive.
		}
		else if ( ((Mobile)o) is BaseCreature && ((BaseCreature)o).IsDeadBondedPet )
		{
		    from.SendLocalizedMessage( 1060177 ); // You cannot heal a creature that is already dead!
		}
		else if ( ((Mobile)o).Hits >= ((Mobile)o).HitsMax )
		{
		    from.SendLocalizedMessage( 500955 ); // That being is not damaged!
		}
		else if ( ((Mobile)o).Poisoned || Server.Items.MortalStrike.IsWounded( ((Mobile)o) ) )
		{
		    from.LocalOverheadMessage( MessageType.Regular, 0x3B2, (from == ((Mobile)o)) ? 1005000 : 1010398 );
		}
		else
		{
		    if( squire.Target != null ) // Added 1.9.6.3
		    {
			squire.Target.Invoke( squire, o );
		    }
		}
	    }
	    else
	    {
		from.SendMessage( "This cannot have its wounds closed." );
	    }
	}
    }
    internal class SquirePassRemoveCurseTarget : Target
    {
	private Mobile m_From;
	private Squire m_Squire;

	public SquirePassRemoveCurseTarget( Mobile from, Squire squire ) : base( 20, false, TargetFlags.Beneficial )
	{
	    m_From = from;
	    m_Squire = squire;
	}

	protected override void OnTarget( Mobile from, object o )
	{
	    DoOnTarget( from, o, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    if( squire.Target != null ) // Added 1.9.6.3
	    {
		squire.Target.Invoke( squire, o );
	    }
	}
    }
    internal class SquirePassSacredJourneyTarget : Target
    {
	private Mobile m_From;
	private Squire m_Squire;

	public SquirePassSacredJourneyTarget( Mobile from, Squire squire ) : base( 20, false, TargetFlags.Beneficial )
	{
	    m_From = from;
	    m_Squire = squire;
	}

	protected override void OnTarget( Mobile from, object o )
	{
	    DoOnTarget( from, o, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    if( o is RecallRune || o is Runebook || ( o is Key && ((Key)o).KeyValue != 0 && ((Key)o).Link is BaseBoat ) || ( o is HouseRaffleDeed && ((HouseRaffleDeed)o).ValidLocation() ) )
	    {
		if( squire.Target != null )
		{
		    squire.Target.Invoke( squire, o );
		}
	    }
	    else
	    {
		from.SendMessage( "They cannot travel from this." );
	    }
	}
    }
    internal class SquireSelfCloseWoundsDelayTimer : Timer
    {
	private Squire m_Squire;
	private TimeSpan m_DelayTime;

	public SquireSelfCloseWoundsDelayTimer( Squire squire, TimeSpan delay ) : base( delay )
	{
	    m_Squire = squire;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    if( m_Squire.Target != null ) // Added 1.9.6.3
	    {
		m_Squire.Target.Invoke( m_Squire, m_Squire );
	    }
	}
    }
    internal class SquireSelfCleanseByFireDelayTimer : Timer
    {
	private Squire m_Squire;
	private TimeSpan m_DelayTime;

	public SquireSelfCleanseByFireDelayTimer( Squire squire, TimeSpan delay ) : base( delay )
	{
	    m_Squire = squire;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    if( m_Squire.Target != null ) // Added 1.9.6.3
	    {
		m_Squire.Target.Invoke( m_Squire, m_Squire );
	    }
	}
    }
    internal class SquirePassExplosionPotionTarget : Target
    {
	private Mobile m_From;
	private Squire m_Squire;

	public SquirePassExplosionPotionTarget( Mobile from, Squire squire ) : base( 20, false, TargetFlags.Beneficial )
	{
	    m_From = from;
	    m_Squire = squire;
	}

	protected override void OnTarget( Mobile from, object o )
	{
	    DoOnTarget( from, o, m_Squire );
	}

	public static void DoOnTarget( Mobile from, object o, Squire squire )
	{
	    if( squire.Target != null ) // Added 1.9.6.3
	    {
		squire.Target.Invoke( squire, o );
	    }
	}
    }
    internal class SquireTargetCloseWoundsDelayTimer : Timer // Added 1.9.7
    {
	private Squire m_Squire;
	private Mobile m_Target;
	private TimeSpan m_DelayTime;

	public SquireTargetCloseWoundsDelayTimer( Squire squire, Mobile target, TimeSpan delay ) : base( delay )
	{
	    m_Squire = squire;
	    m_Target = target;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    if( m_Squire.Target != null )
	    {
		m_Squire.Target.Invoke( m_Squire, m_Target );
	    }
	}
    }
    internal class SquireTargetCleanseByFireDelayTimer : Timer // Added 1.9.7
    {
	private Squire m_Squire;
	private Mobile m_Target;
	private TimeSpan m_DelayTime;

	public SquireTargetCleanseByFireDelayTimer( Squire squire, Mobile target, TimeSpan delay ) : base( delay )
	{
	    m_Squire = squire;
	    m_Target = target;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    if( m_Squire.Target != null )
	    {
		m_Squire.Target.Invoke( m_Squire, m_Target );
	    }
	}
    }


    internal class SquirePainSpikeDelayTimer : Timer
    {
	private Squire m_Squire;
	private Mobile m_From;
	private TimeSpan m_DelayTime;

	public SquirePainSpikeDelayTimer(Squire squire, Mobile from, TimeSpan delay) : base(delay)
	{
	    m_Squire = squire;
	    m_From = from;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    m_From.Target = new SquirePassPainSpikeTarget(m_From, m_Squire);
	}
    }

    internal class SquirePassPainSpikeTarget : Target
    {
	private Mobile m_From;
	private Squire m_Squire;

	public SquirePassPainSpikeTarget(Mobile from, Squire squire) : base(20, false, TargetFlags.None)
	{
	    m_From = from;
	    m_Squire = squire;
	}

	protected override void OnTarget(Mobile from, object o)
	{
	    DoOnTarget(from, o, m_Squire);
	}

	public static void DoOnTarget(Mobile from, object o, Squire squire)
	{
	    if (o is Mobile)
	    {
		if (squire.Target != null) // Added 1.9.6.3
		{
		    squire.Target.Invoke(squire, o);
		}
	    }
	    else
	    {
		from.SendMessage("This cannot be attacked.");
	    }
	}
    }


    internal class SquirePoisonStrikeDelayTimer : Timer
    {
	private Squire m_Squire;
	private Mobile m_From;
	private TimeSpan m_DelayTime;

	public SquirePoisonStrikeDelayTimer(Squire squire, Mobile from, TimeSpan delay) : base(delay)
	{
	    m_Squire = squire;
	    m_From = from;
	    m_DelayTime = delay;

	    Priority = TimerPriority.TwentyFiveMS;
	}

	protected override void OnTick()
	{
	    m_From.Target = new SquirePassPoisonStrikeTarget(m_From, m_Squire);
	}
    }

    internal class SquirePassPoisonStrikeTarget : Target
    {
	private Mobile m_From;
	private Squire m_Squire;

	public SquirePassPoisonStrikeTarget(Mobile from, Squire squire) : base(20, false, TargetFlags.None)
	{
	    m_From = from;
	    m_Squire = squire;
	}

	protected override void OnTarget(Mobile from, object o)
	{
	    DoOnTarget(from, o, m_Squire);
	}

	public static void DoOnTarget(Mobile from, object o, Squire squire)
	{
	    if (o is Mobile)
	    {
		if (squire.Target != null) // Added 1.9.6.3
		{
		    squire.Target.Invoke(squire, o);
		}
	    }
	    else
	    {
		from.SendMessage("This cannot be attacked.");
	    }
	}
    }


}
