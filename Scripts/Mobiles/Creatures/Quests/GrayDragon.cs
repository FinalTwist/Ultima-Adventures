using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class GrayDragon : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public GrayDragon () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a gray dragon";
			Body = 12;
			BaseSoundID = 362;
			Hue = 0x392;
			EmoteHue = 123;

			SetStr( 401, 430 );
			SetDex( 133, 152 );
			SetInt( 101, 140 );

			SetHits( 241, 258 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Fire, 20 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 65.1, 90.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );

			Fame = 5500;
			Karma = -5500;

			VirtualArmor = 46;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.White ); } }

		public override bool OnBeforeDeath()
		{
			GrayDragonBox MyChest = new GrayDragonBox();

			Map map = this.Map;

			bool validLocation = false;
			Point3D loc = this.Location;

			for ( int j = 0; !validLocation && j < 10; ++j )
			{
				int x = X + Utility.Random( 3 ) - 1;
				int y = Y + Utility.Random( 3 ) - 1;
				int z = map.GetAverageZ( x, y );

				if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
					loc = new Point3D( x, y, Z );
				else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
					loc = new Point3D( x, y, z );
			}

			MyChest.MoveToWorld( loc, map );

			QuestGlow MyGlow = new QuestGlow();
			MyGlow.MoveToWorld( loc, map );

			return base.OnBeforeDeath();
		}

		public GrayDragon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Server.Items
{
	public class GrayDragonBox : Item
	{
		[Constructable]
		public GrayDragonBox() : base( 0xE80 )
		{
			Name = "the dragon's box";
			Movable = false;
			Hue = 0x1BA;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		}

		public GrayDragonBox( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile pm = (PlayerMobile)from;

			if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleDragonKey" ) && CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleHarkynKey" ) )
			{
				from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You find nothing of interest.", from.NetState);
			}
			else
			{
				CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleDragonKey", true );
				CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleHarkynKey", true );
				from.SendSound( 0x3D );
				from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You found the rusty key that was around the gray dragon's neck.", from.NetState);
				from.CloseGump( typeof(Server.Gumps.ClueGump) );
				from.SendGump(new Server.Gumps.ClueGump( "You found the rusty key that was around the gray dragon's neck. Perhaps it can be used on that nearby door.", "Rusty Key" ) );
			}
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
			this.Delete(); // none when the world starts 
		}

		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 
			public ItemRemovalTimer( Item item ) : base( TimeSpan.FromMinutes( 10.0 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = item; 
			} 

			protected override void OnTick() 
			{ 
				if (( i_item != null ) && ( !i_item.Deleted ))
				{
					i_item.Delete();
				}
			} 
		} 
	}
}