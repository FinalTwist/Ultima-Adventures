using System;
using Server;
using Server.Items;

using Server.Misc;

namespace Server.Mobiles
{
	public class CodexGargoyleA : BaseCreature
	{
		[Constructable]
		public CodexGargoyleA () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Naxatilor";
			Title = "the gargoyle";
			Body = 257;
			BaseSoundID = 372;

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
		}

		public override bool OnBeforeDeath()
		{
			GargoyleCodexCorpse MyBody = new GargoyleCodexCorpse();
			MyBody.Name = "Naxatilor's corpse";
			MyBody.MoveToWorld( Location, Map );

			return base.OnBeforeDeath();
		}

        public override void OnAfterSpawn()
        {
			base.OnAfterSpawn();
			Worlds.MoveToRandomDungeon( this );
			Server.Misc.IntelligentAction.BurnAway( this );
		}

		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override int GetAttackSound(){ return 0x5F8; }	// A
		public override int GetDeathSound(){ return 0x5F9; }	// D
		public override int GetHurtSound(){ return 0x5FA; }		// H
		public override bool BardImmune { get { return true; } }

		public CodexGargoyleA( Serial serial ) : base( serial )
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

	public class CodexGargoyleB : BaseCreature
	{
		[Constructable]
		public CodexGargoyleB () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Lor-wis-lem";
			Title = "the gargoyle";
			Body = 257;
			BaseSoundID = 372;

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
		}

		public override bool OnBeforeDeath()
		{
			GargoyleCodexCorpse MyBody = new GargoyleCodexCorpse();
			MyBody.Name = "Lor-wis-lem's corpse";
			MyBody.MoveToWorld( Location, Map );

			return base.OnBeforeDeath();
		}

        public override void OnAfterSpawn()
        {
			base.OnAfterSpawn();
			Worlds.MoveToRandomDungeon( this );
			Server.Misc.IntelligentAction.BurnAway( this );
		}

		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override int GetAttackSound(){ return 0x5F8; }	// A
		public override int GetDeathSound(){ return 0x5F9; }	// D
		public override int GetHurtSound(){ return 0x5FA; }		// H
		public override bool BardImmune { get { return true; } }

		public CodexGargoyleB( Serial serial ) : base( serial )
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

namespace Server.Items
{
	public class GargoyleCodexCorpse : Item
	{
		[Constructable]
		public GargoyleCodexCorpse() : base( 0x4AA2 )
		{
			Name = "a gargoyle's corpse";
			Movable = false;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start();
			ItemID = Utility.RandomList( 0x4AA2, 0x4AA3 );
		}

		public GargoyleCodexCorpse( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			int remove = 0;

			if ( from.InRange( this.GetWorldLocation(), 5 ) )
			{
				string say = "You find nothing useful on the corpse";

				if ( from.Backpack.FindItemByType( typeof ( VortexCube ) ) != null )
				{
					Item square = from.Backpack.FindItemByType( typeof ( VortexCube ) );

					if ( square is VortexCube )
					{
						VortexCube cube = (VortexCube)square;
						if ( this.Name == "Lor-wis-lem's corpse" )
						{
							if ( cube.HasConvexLense == 0 )
							{
								cube.HasConvexLense = 1;
								say = "You take possession of the Convex Lense!";
								from.SendSound( 0x3D );
								LoggingFunctions.LogGeneric( from, "has found the Convex Lense." );
								remove = 1;
							}
						}
						else
						{
							if ( cube.HasConcaveLense == 0 )
							{
								cube.HasConcaveLense = 1;
								say = "You take possession of the Concave Lense!";
								from.SendSound( 0x3D );
								LoggingFunctions.LogGeneric( from, "has found the Concave Lense." );
								remove = 1;
							}
						}
					}
				}
				else if ( from.Backpack.FindItemByType( typeof ( CodexWisdom ) ) != null )
				{
					Item codex = from.Backpack.FindItemByType( typeof ( CodexWisdom ) );

					if ( codex is CodexWisdom )
					{
						CodexWisdom book = (CodexWisdom)codex;
						if ( this.Name == "Lor-wis-lem's corpse" )
						{
							if ( book.HasConvexLense == 0 )
							{
								book.HasConvexLense = 1;
								say = "You take possession of the Convex Lense!";
								from.SendSound( 0x3D );
								remove = 1;
							}
						}
						else
						{
							if ( book.HasConcaveLense == 0 )
							{
								book.HasConcaveLense = 1;
								say = "You take possession of the Concave Lense!";
								from.SendSound( 0x3D );
								remove = 1;
							}
						}
					}
				}
				else if ( from.FindItemOnLayer( Layer.Talisman ) != null )
				{
					Item codex = from.FindItemOnLayer( Layer.Talisman );

					if ( codex is CodexWisdom )
					{
						CodexWisdom book = (CodexWisdom)codex;
						if ( this.Name == "Lor-wis-lem's corpse" )
						{
							if ( book.HasConvexLense == 0 )
							{
								book.HasConvexLense = 1;
								say = "You take possession of the Convex Lense!";
								from.SendSound( 0x3D );
								remove = 1;
							}
						}
						else
						{
							if ( book.HasConcaveLense == 0 )
							{
								book.HasConcaveLense = 1;
								say = "You take possession of the Concave Lense!";
								from.SendSound( 0x3D );
								remove = 1;
							}
						}
					}
				}
				from.SendMessage( say );
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}

			if ( remove > 0 ){ this.Delete(); }
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