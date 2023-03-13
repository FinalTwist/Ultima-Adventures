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
	[CorpseName( "Tarjan's corpse" )]
	public class Tarjan : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 125.0; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public Tarjan () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Tarjan";
			Title = "the mad god";
			Body = 9;
			Hue = 0x5B7;
			BaseSoundID = 357;
			EmoteHue = 123;

			SetStr( 476, 505 );
			SetDex( 76, 95 );
			SetInt( 301, 325 );

			SetHits( 286, 303 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 70.1, 80.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.MagicResist, 85.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 58;

			TarjanTimer thisTimer = new TarjanTimer( this ); 
			thisTimer.Start(); 
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }
		public override bool BardImmune { get { return true; } }

		public override bool OnBeforeDeath()
		{
			TarjanStatue MyStatue = new TarjanStatue();
			TarjanBox MyChest = new TarjanBox();

			Map map = this.Map;

			bool validLocation = false;
			Point3D loc = this.Location;
			Point3D sloc = new Point3D( 5559, 1121, 45 );

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
			MyStatue.MoveToWorld( sloc, map );

			QuestGlow MyGlow = new QuestGlow();
			MyGlow.MoveToWorld( loc, map );

			return base.OnBeforeDeath();
		}

		public class TarjanTimer : Timer 
		{ 
			private Mobile m_tarjan; 
			public TarjanTimer( Mobile from ) : base( TimeSpan.FromSeconds( 120.0 ) )
			{ 
				Priority = TimerPriority.OneSecond; 
				m_tarjan = from; 
			} 

			protected override void OnTick() 
			{ 
				if ( m_tarjan != null )
				{
					if ( m_tarjan.Warmode == false )
					{
						TarjanStatue MyStatue = new TarjanStatue();
						Map map = m_tarjan.Map;
						Point3D sloc = new Point3D( 5559, 1121, 45 );
						MyStatue.MoveToWorld( sloc, map );

						Effects.SendLocationParticles( EffectItem.Create( MyStatue.Location, MyStatue.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						m_tarjan.PlaySound( 0x1FE );
						Effects.SendLocationParticles( EffectItem.Create( m_tarjan.Location, m_tarjan.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );
						m_tarjan.Delete();
					}
					else
					{
						TarjanTimer thisTimer = new TarjanTimer( m_tarjan ); 
						thisTimer.Start();
					}
				}
			} 
		} 

		public Tarjan( Serial serial ) : base( serial )
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
			TarjanTimer thisTimer = new TarjanTimer( this ); 
			thisTimer.Start(); 
		}
	}
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Server.Items
{
	public class TarjanBox : Item
	{
		[Constructable]
		public TarjanBox() : base( 0x9AB )
		{
			Name = "Tarjan's box";
			Movable = false;
			Hue = 0x489;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		}

		public TarjanBox( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleKylearanKey" ) && CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSpectreEye" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You find nothing of interest.", from.NetState);
				}
				else
				{
					CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleKylearanKey", true );
					CharacterDatabase.SetBardsTaleQuest( from, "BardsTaleSpectreEye", true );
					from.SendSound( 0x3D );
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You found a key with a symbol of a unicorn on it.", from.NetState);
					from.CloseGump( typeof(Server.Gumps.ClueGump) );
					from.SendGump(new Server.Gumps.ClueGump( "You found a key with a symbol of a unicorn on it, along with a magical book of items.", "Tarjan's Death" ) );

					if ( !Server.Items.CharacterDatabase.GetSpecialsKilled( from, "Tarjan" ) )
					{
						Server.Items.CharacterDatabase.SetSpecialsKilled( from, "Tarjan", true );
						ManualOfItems book = new ManualOfItems();
							book.Hue = 0x5B7;
							book.Name = "Tome of Tarjan Relics";
							book.m_Charges = 1;
							book.m_Skill_1 = 99;
							book.m_Skill_2 = 32;
							book.m_Skill_3 = 0;
							book.m_Skill_4 = 0;
							book.m_Skill_5 = 0;
							book.m_Value_1 = 10.0;
							book.m_Value_2 = 10.0;
							book.m_Value_3 = 0.0;
							book.m_Value_4 = 0.0;
							book.m_Value_5 = 0.0;
							book.m_Slayer_1 = 11;
							book.m_Slayer_2 = 0;
							book.m_Owner = from;
							book.m_Extra = "of the Mad God";
							book.m_FromWho = "Taken from Tarjan";
							book.m_HowGiven = "Acquired by";
							book.m_Points = 100;
							book.m_Hue = 0x5B7;
							from.AddToBackpack( book );
							from.SendMessage( "A book has been added to your pack!" );
					}
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
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
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Server.Items
{
	public class TarjanStatue : Item
	{
		[Constructable]
		public TarjanStatue() : base( 0x2104 )
		{
			Name = "a statue of Tarjan";
			Movable = false;
			Hue = 0x5B7;
			Weight = -2;
		}

		public TarjanStatue( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				PlayerMobile pm = (PlayerMobile)from;

				if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleKylearanKey" ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "This statue still has the eye you placed in it.", from.NetState);
				}
				else if ( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleSpectreEye" ) && !( CharacterDatabase.GetBardsTaleQuest( from, "BardsTaleKylearanKey" ) ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You place the mysterious eye into the statue.", from.NetState);
					SpawnTarjan( from );
					this.Delete();
				}
				else
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "This statue seems to be missing an eye.", from.NetState);
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public void SpawnTarjan( Mobile target )
		{
			Map map = this.Map;
			int nTarjan = 0;

			if ( map == null )
				return;

			ArrayList tarjans = new ArrayList();
			foreach ( Mobile tarj in World.Mobiles.Values )
			if ( tarj is Tarjan )
			{
				nTarjan = 1;
			}

			if ( nTarjan == 0 )
			{
				BaseCreature monster = new Tarjan();
				Point3D loc = this.Location;
				monster.PlaySound( 0x216 );
				monster.MoveToWorld( loc, map );
				monster.Combatant = target;
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
		}
	}
}