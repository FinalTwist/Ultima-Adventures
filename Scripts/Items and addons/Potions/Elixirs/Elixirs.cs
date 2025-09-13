using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class ElixirAlchemy : BaseElixir
	{
		[Constructable]
		public ElixirAlchemy() : base( PotionEffect.ElixirAlchemy )
		{
			Name = "elixir of alchemy";
		}

		public ElixirAlchemy( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirAlchemy = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirAlchemy[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirAlchemy[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirAlchemy.Remove( m );
			m.EndAction( typeof( ElixirAlchemy ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirAlchemy ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Alchemy;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirAlchemy[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirAlchemy ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirAlchemy.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirAnatomy : BaseElixir
	{
		[Constructable]
		public ElixirAnatomy() : base( PotionEffect.ElixirAnatomy )
		{
			Name = "elixir of anatomy";
		}

		public ElixirAnatomy( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirAnatomy = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirAnatomy[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirAnatomy[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirAnatomy.Remove( m );
			m.EndAction( typeof( ElixirAnatomy ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirAnatomy ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Anatomy;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirAnatomy[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirAnatomy ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirAnatomy.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirAnimalLore : BaseElixir
	{
		[Constructable]
		public ElixirAnimalLore() : base( PotionEffect.ElixirAnimalLore )
		{
			Name = "elixir of animal lore";
		}

		public ElixirAnimalLore( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirAnimalLore = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirAnimalLore[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirAnimalLore[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirAnimalLore.Remove( m );
			m.EndAction( typeof( ElixirAnimalLore ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirAnimalLore ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.AnimalLore;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirAnimalLore[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirAnimalLore ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirAnimalLore.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirAnimalTaming : BaseElixir
	{
		[Constructable]
		public ElixirAnimalTaming() : base( PotionEffect.ElixirAnimalTaming )
		{
			Name = "elixir of animal taming";
		}

		public ElixirAnimalTaming( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirAnimalTaming = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirAnimalTaming[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirAnimalTaming[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirAnimalTaming.Remove( m );
			m.EndAction( typeof( ElixirAnimalTaming ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirAnimalTaming ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.AnimalTaming;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirAnimalTaming[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirAnimalTaming ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirAnimalTaming.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirArchery : BaseElixir
	{
		[Constructable]
		public ElixirArchery() : base( PotionEffect.ElixirArchery )
		{
			Name = "elixir of archery";
		}

		public ElixirArchery( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirArchery = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirArchery[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirArchery[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirArchery.Remove( m );
			m.EndAction( typeof( ElixirArchery ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirArchery ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Archery;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirArchery[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirArchery ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirArchery.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirArmsLore : BaseElixir
	{
		[Constructable]
		public ElixirArmsLore() : base( PotionEffect.ElixirArmsLore )
		{
			Name = "elixir of arms lore";
		}

		public ElixirArmsLore( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirArmsLore = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirArmsLore[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirArmsLore[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirArmsLore.Remove( m );
			m.EndAction( typeof( ElixirArmsLore ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirArmsLore ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.ArmsLore;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirArmsLore[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirArmsLore ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirArmsLore.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirBegging : BaseElixir
	{
		[Constructable]
		public ElixirBegging() : base( PotionEffect.ElixirBegging )
		{
			Name = "elixir of begging";
		}

		public ElixirBegging( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirBegging = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirBegging[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirBegging[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirBegging.Remove( m );
			m.EndAction( typeof( ElixirBegging ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirBegging ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Begging;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirBegging[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirBegging ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirBegging.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirBlacksmith : BaseElixir
	{
		[Constructable]
		public ElixirBlacksmith() : base( PotionEffect.ElixirBlacksmith )
		{
			Name = "elixir of blacksmithing";
		}

		public ElixirBlacksmith( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirBlacksmith = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirBlacksmith[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirBlacksmith[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirBlacksmith.Remove( m );
			m.EndAction( typeof( ElixirBlacksmith ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirBlacksmith ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Blacksmith;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirBlacksmith[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirBlacksmith ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirBlacksmith.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirCamping : BaseElixir
	{
		[Constructable]
		public ElixirCamping() : base( PotionEffect.ElixirCamping )
		{
			Name = "elixir of camping";
		}

		public ElixirCamping( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirCamping = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirCamping[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirCamping[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirCamping.Remove( m );
			m.EndAction( typeof( ElixirCamping ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirCamping ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Camping;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirCamping[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirCamping ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirCamping.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirCarpentry : BaseElixir
	{
		[Constructable]
		public ElixirCarpentry() : base( PotionEffect.ElixirCarpentry )
		{
			Name = "elixir of carpentry";
		}

		public ElixirCarpentry( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirCarpentry = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirCarpentry[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirCarpentry[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirCarpentry.Remove( m );
			m.EndAction( typeof( ElixirCarpentry ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirCarpentry ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Carpentry;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirCarpentry[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirCarpentry ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirCarpentry.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirCartography : BaseElixir
	{
		[Constructable]
		public ElixirCartography() : base( PotionEffect.ElixirCartography )
		{
			Name = "elixir of cartography";
		}

		public ElixirCartography( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirCartography = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirCartography[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirCartography[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirCartography.Remove( m );
			m.EndAction( typeof( ElixirCartography ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirCartography ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Cartography;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirCartography[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirCartography ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirCartography.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirCooking : BaseElixir
	{
		[Constructable]
		public ElixirCooking() : base( PotionEffect.ElixirCooking )
		{
			Name = "elixir of cooking";
		}

		public ElixirCooking( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirCooking = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirCooking[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirCooking[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirCooking.Remove( m );
			m.EndAction( typeof( ElixirCooking ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirCooking ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Cooking;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirCooking[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirCooking ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirCooking.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirDetectHidden : BaseElixir
	{
		[Constructable]
		public ElixirDetectHidden() : base( PotionEffect.ElixirDetectHidden )
		{
			Name = "elixir of detection";
		}

		public ElixirDetectHidden( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirDetectHidden = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirDetectHidden[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirDetectHidden[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirDetectHidden.Remove( m );
			m.EndAction( typeof( ElixirDetectHidden ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirDetectHidden ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.DetectHidden;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirDetectHidden[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirDetectHidden ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirDetectHidden.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirDiscordance : BaseElixir
	{
		[Constructable]
		public ElixirDiscordance() : base( PotionEffect.ElixirDiscordance )
		{
			Name = "elixir of discordance";
		}

		public ElixirDiscordance( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirDiscordance = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirDiscordance[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirDiscordance[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirDiscordance.Remove( m );
			m.EndAction( typeof( ElixirDiscordance ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirDiscordance ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Discordance;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirDiscordance[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirDiscordance ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirDiscordance.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirEvalInt : BaseElixir
	{
		[Constructable]
		public ElixirEvalInt() : base( PotionEffect.ElixirEvalInt )
		{
			Name = "elixir of intelligence evaluation";
		}

		public ElixirEvalInt( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirEvalInt = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirEvalInt[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirEvalInt[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirEvalInt.Remove( m );
			m.EndAction( typeof( ElixirEvalInt ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirEvalInt ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.EvalInt;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirEvalInt[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirEvalInt ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirEvalInt.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirFencing : BaseElixir
	{
		[Constructable]
		public ElixirFencing() : base( PotionEffect.ElixirFencing )
		{
			Name = "elixir of fencing";
		}

		public ElixirFencing( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirFencing = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirFencing[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirFencing[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirFencing.Remove( m );
			m.EndAction( typeof( ElixirFencing ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirFencing ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Fencing;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirFencing[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirFencing ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirFencing.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirFishing : BaseElixir
	{
		[Constructable]
		public ElixirFishing() : base( PotionEffect.ElixirFishing )
		{
			Name = "elixir of fishing";
		}

		public ElixirFishing( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirFishing = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirFishing[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirFishing[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirFishing.Remove( m );
			m.EndAction( typeof( ElixirFishing ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirFishing ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Fishing;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirFishing[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirFishing ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirFishing.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirFletching : BaseElixir
	{
		[Constructable]
		public ElixirFletching() : base( PotionEffect.ElixirFletching )
		{
			Name = "elixir of fletching";
		}

		public ElixirFletching( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirFletching = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirFletching[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirFletching[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirFletching.Remove( m );
			m.EndAction( typeof( ElixirFletching ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirFletching ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Fletching;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirFletching[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirFletching ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirFletching.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirFocus : BaseElixir
	{
		[Constructable]
		public ElixirFocus() : base( PotionEffect.ElixirFocus )
		{
			Name = "elixir of focus";
		}

		public ElixirFocus( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirFocus = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirFocus[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirFocus[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirFocus.Remove( m );
			m.EndAction( typeof( ElixirFocus ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirFocus ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Focus;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirFocus[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirFocus ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirFocus.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirForensics : BaseElixir
	{
		[Constructable]
		public ElixirForensics() : base( PotionEffect.ElixirForensics )
		{
			Name = "elixir of forensics";
		}

		public ElixirForensics( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirForensics = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirForensics[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirForensics[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirForensics.Remove( m );
			m.EndAction( typeof( ElixirForensics ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirForensics ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Forensics;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirForensics[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirForensics ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirForensics.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirHealing : BaseElixir
	{
		[Constructable]
		public ElixirHealing() : base( PotionEffect.ElixirHealing )
		{
			Name = "elixir of the healer";
		}

		public ElixirHealing( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirHealing = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirHealing[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirHealing[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirHealing.Remove( m );
			m.EndAction( typeof( ElixirHealing ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirHealing ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Healing;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirHealing[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirHealing ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirHealing.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirHerding : BaseElixir
	{
		[Constructable]
		public ElixirHerding() : base( PotionEffect.ElixirHerding )
		{
			Name = "elixir of herding";
		}

		public ElixirHerding( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirHerding = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirHerding[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirHerding[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirHerding.Remove( m );
			m.EndAction( typeof( ElixirHerding ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirHerding ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Herding;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirHerding[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirHerding ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirHerding.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirHiding : BaseElixir
	{
		[Constructable]
		public ElixirHiding() : base( PotionEffect.ElixirHiding )
		{
			Name = "elixir of hiding";
		}

		public ElixirHiding( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirHiding = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirHiding[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirHiding[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirHiding.Remove( m );
			m.EndAction( typeof( ElixirHiding ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirHiding ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Hiding;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirHiding[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirHiding ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirHiding.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirInscribe : BaseElixir
	{
		[Constructable]
		public ElixirInscribe() : base( PotionEffect.ElixirInscribe )
		{
			Name = "elixir of inscription";
		}

		public ElixirInscribe( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirInscribe = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirInscribe[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirInscribe[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirInscribe.Remove( m );
			m.EndAction( typeof( ElixirInscribe ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirInscribe ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Inscribe;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirInscribe[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirInscribe ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirInscribe.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirItemID : BaseElixir
	{
		[Constructable]
		public ElixirItemID() : base( PotionEffect.ElixirItemID )
		{
			Name = "elixir of item identifying";
		}

		public ElixirItemID( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirItemID = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirItemID[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirItemID[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirItemID.Remove( m );
			m.EndAction( typeof( ElixirItemID ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirItemID ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.ItemID;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirItemID[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirItemID ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirItemID.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirLockpicking : BaseElixir
	{
		[Constructable]
		public ElixirLockpicking() : base( PotionEffect.ElixirLockpicking )
		{
			Name = "elixir of lockpicking";
		}

		public ElixirLockpicking( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirLockpicking = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirLockpicking[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirLockpicking[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirLockpicking.Remove( m );
			m.EndAction( typeof( ElixirLockpicking ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirLockpicking ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Lockpicking;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirLockpicking[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirLockpicking ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirLockpicking.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirLumberjacking : BaseElixir
	{
		[Constructable]
		public ElixirLumberjacking() : base( PotionEffect.ElixirLumberjacking )
		{
			Name = "elixir of lumberjacking";
		}

		public ElixirLumberjacking( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirLumberjacking = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirLumberjacking[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirLumberjacking[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirLumberjacking.Remove( m );
			m.EndAction( typeof( ElixirLumberjacking ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirLumberjacking ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Lumberjacking;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirLumberjacking[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirLumberjacking ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirLumberjacking.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirMacing : BaseElixir
	{
		[Constructable]
		public ElixirMacing() : base( PotionEffect.ElixirMacing )
		{
			Name = "elixir of mace fighting";
		}

		public ElixirMacing( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirMacing = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirMacing[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirMacing[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirMacing.Remove( m );
			m.EndAction( typeof( ElixirMacing ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirMacing ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Macing;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirMacing[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirMacing ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirMacing.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirMagicResist : BaseElixir
	{
		[Constructable]
		public ElixirMagicResist() : base( PotionEffect.ElixirMagicResist )
		{
			Name = "elixir of magic resistance";
		}

		public ElixirMagicResist( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirMagicResist = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirMagicResist[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirMagicResist[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirMagicResist.Remove( m );
			m.EndAction( typeof( ElixirMagicResist ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirMagicResist ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.MagicResist;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirMagicResist[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirMagicResist ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirMagicResist.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirMeditation : BaseElixir
	{
		[Constructable]
		public ElixirMeditation() : base( PotionEffect.ElixirMeditation )
		{
			Name = "elixir of meditating";
		}

		public ElixirMeditation( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirMeditation = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirMeditation[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirMeditation[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirMeditation.Remove( m );
			m.EndAction( typeof( ElixirMeditation ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirMeditation ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Meditation;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirMeditation[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirMeditation ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirMeditation.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirMining : BaseElixir
	{
		[Constructable]
		public ElixirMining() : base( PotionEffect.ElixirMining )
		{
			Name = "elixir of mining";
		}

		public ElixirMining( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirMining = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirMining[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirMining[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirMining.Remove( m );
			m.EndAction( typeof( ElixirMining ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirMining ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Mining;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirMining[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirMining ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirMining.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirMusicianship : BaseElixir
	{
		[Constructable]
		public ElixirMusicianship() : base( PotionEffect.ElixirMusicianship )
		{
			Name = "elixir of musicianship";
		}

		public ElixirMusicianship( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirMusicianship = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirMusicianship[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirMusicianship[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirMusicianship.Remove( m );
			m.EndAction( typeof( ElixirMusicianship ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirMusicianship ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Musicianship;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirMusicianship[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirMusicianship ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirMusicianship.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirParry : BaseElixir
	{
		[Constructable]
		public ElixirParry() : base( PotionEffect.ElixirParry )
		{
			Name = "elixir of parrying";
		}

		public ElixirParry( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirParry = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirParry[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirParry[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirParry.Remove( m );
			m.EndAction( typeof( ElixirParry ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirParry ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Parry;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirParry[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirParry ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirParry.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirPeacemaking : BaseElixir
	{
		[Constructable]
		public ElixirPeacemaking() : base( PotionEffect.ElixirPeacemaking )
		{
			Name = "elixir of peacemaking";
		}

		public ElixirPeacemaking( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirPeacemaking = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirPeacemaking[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirPeacemaking[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirPeacemaking.Remove( m );
			m.EndAction( typeof( ElixirPeacemaking ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirPeacemaking ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Peacemaking;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirPeacemaking[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirPeacemaking ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirPeacemaking.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirPoisoning : BaseElixir
	{
		[Constructable]
		public ElixirPoisoning() : base( PotionEffect.ElixirPoisoning )
		{
			Name = "elixir of poisoning";
		}

		public ElixirPoisoning( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirPoisoning = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirPoisoning[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirPoisoning[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirPoisoning.Remove( m );
			m.EndAction( typeof( ElixirPoisoning ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirPoisoning ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Poisoning;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirPoisoning[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirPoisoning ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirPoisoning.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirProvocation : BaseElixir
	{
		[Constructable]
		public ElixirProvocation() : base( PotionEffect.ElixirProvocation )
		{
			Name = "elixir of provocation";
		}

		public ElixirProvocation( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirProvocation = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirProvocation[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirProvocation[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirProvocation.Remove( m );
			m.EndAction( typeof( ElixirProvocation ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirProvocation ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Provocation;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirProvocation[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirProvocation ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirProvocation.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirRemoveTrap : BaseElixir
	{
		[Constructable]
		public ElixirRemoveTrap() : base( PotionEffect.ElixirRemoveTrap )
		{
			Name = "elixir of removing trap";
		}

		public ElixirRemoveTrap( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirRemoveTrap = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirRemoveTrap[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirRemoveTrap[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirRemoveTrap.Remove( m );
			m.EndAction( typeof( ElixirRemoveTrap ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirRemoveTrap ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.RemoveTrap;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirRemoveTrap[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirRemoveTrap ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirRemoveTrap.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirSnooping : BaseElixir
	{
		[Constructable]
		public ElixirSnooping() : base( PotionEffect.ElixirSnooping )
		{
			Name = "elixir of snooping";
		}

		public ElixirSnooping( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirSnooping = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirSnooping[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirSnooping[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirSnooping.Remove( m );
			m.EndAction( typeof( ElixirSnooping ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirSnooping ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Snooping;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirSnooping[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirSnooping ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirSnooping.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirSpiritSpeak : BaseElixir
	{
		[Constructable]
		public ElixirSpiritSpeak() : base( PotionEffect.ElixirSpiritSpeak )
		{
			Name = "elixir of spirit speaking";
		}

		public ElixirSpiritSpeak( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirSpiritSpeak = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirSpiritSpeak[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirSpiritSpeak[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirSpiritSpeak.Remove( m );
			m.EndAction( typeof( ElixirSpiritSpeak ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirSpiritSpeak ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.SpiritSpeak;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirSpiritSpeak[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirSpiritSpeak ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirSpiritSpeak.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirStealing : BaseElixir
	{
		[Constructable]
		public ElixirStealing() : base( PotionEffect.ElixirStealing )
		{
			Name = "elixir of stealing";
		}

		public ElixirStealing( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirStealing = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirStealing[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirStealing[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirStealing.Remove( m );
			m.EndAction( typeof( ElixirStealing ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirStealing ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Stealing;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirStealing[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirStealing ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirStealing.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirStealth : BaseElixir
	{
		[Constructable]
		public ElixirStealth() : base( PotionEffect.ElixirStealth )
		{
			Name = "elixir of stealth";
		}

		public ElixirStealth( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirStealth = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirStealth[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirStealth[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirStealth.Remove( m );
			m.EndAction( typeof( ElixirStealth ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirStealth ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Stealth;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirStealth[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirStealth ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirStealth.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirSwords : BaseElixir
	{
		[Constructable]
		public ElixirSwords() : base( PotionEffect.ElixirSwords )
		{
			Name = "elixir of sword fighting";
		}

		public ElixirSwords( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirSwords = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirSwords[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirSwords[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirSwords.Remove( m );
			m.EndAction( typeof( ElixirSwords ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirSwords ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Swords;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirSwords[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirSwords ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirSwords.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirTactics : BaseElixir
	{
		[Constructable]
		public ElixirTactics() : base( PotionEffect.ElixirTactics )
		{
			Name = "elixir of tactics";
		}

		public ElixirTactics( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirTactics = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirTactics[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirTactics[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirTactics.Remove( m );
			m.EndAction( typeof( ElixirTactics ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirTactics ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Tactics;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirTactics[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirTactics ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirTactics.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirTailoring : BaseElixir
	{
		[Constructable]
		public ElixirTailoring() : base( PotionEffect.ElixirTailoring )
		{
			Name = "elixir of tailoring";
		}

		public ElixirTailoring( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirTailoring = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirTailoring[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirTailoring[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirTailoring.Remove( m );
			m.EndAction( typeof( ElixirTailoring ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirTailoring ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Tailoring;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirTailoring[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirTailoring ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirTailoring.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirTasteID : BaseElixir
	{
		[Constructable]
		public ElixirTasteID() : base( PotionEffect.ElixirTasteID )
		{
			Name = "elixir of taste identification";
		}

		public ElixirTasteID( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirTasteID = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirTasteID[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirTasteID[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirTasteID.Remove( m );
			m.EndAction( typeof( ElixirTasteID ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirTasteID ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.TasteID;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirTasteID[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirTasteID ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirTasteID.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirTinkering : BaseElixir
	{
		[Constructable]
		public ElixirTinkering() : base( PotionEffect.ElixirTinkering )
		{
			Name = "elixir of tinkering";
		}

		public ElixirTinkering( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirTinkering = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirTinkering[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirTinkering[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirTinkering.Remove( m );
			m.EndAction( typeof( ElixirTinkering ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirTinkering ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Tinkering;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirTinkering[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirTinkering ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirTinkering.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirTracking : BaseElixir
	{
		[Constructable]
		public ElixirTracking() : base( PotionEffect.ElixirTracking )
		{
			Name = "elixir of tracking";
		}

		public ElixirTracking( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirTracking = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirTracking[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirTracking[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirTracking.Remove( m );
			m.EndAction( typeof( ElixirTracking ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirTracking ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Tracking;
				int level = 0;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirTracking[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirTracking ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirTracking.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirVeterinary : BaseElixir
	{
		[Constructable]
		public ElixirVeterinary() : base( PotionEffect.ElixirVeterinary )
		{
			Name = "elixir of veterinary";
		}

		public ElixirVeterinary( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirVeterinary = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirVeterinary[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirVeterinary[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirVeterinary.Remove( m );
			m.EndAction( typeof( ElixirVeterinary ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirVeterinary ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Veterinary;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirVeterinary[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirVeterinary ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirVeterinary.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}

//// ------------------------------------------------------------------------------------------------

	public class ElixirWrestling : BaseElixir
	{
		[Constructable]
		public ElixirWrestling() : base( PotionEffect.ElixirWrestling )
		{
			Name = "elixir of wrestling";
		}

		public ElixirWrestling( Serial serial ) : base( serial )
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

		public static Hashtable TableElixirWrestling = new Hashtable();

		public static bool HasEffect( Mobile m )
		{
			return ( TableElixirWrestling[m] != null );
		}

		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])TableElixirWrestling[m];

			if ( mods != null )
			{
				m.RemoveSkillMod( (SkillMod)mods[0] );
			}

			TableElixirWrestling.Remove( m );
			m.EndAction( typeof( ElixirWrestling ) );
			m.Hidden = false;
		}

		public override void Drink( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( ElixirWrestling ) ) || !DrankTooMuch( m ) )
			{
				m.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You cannot drink another one of these elixirs yet", m.NetState);
			}
			else
			{
				SkillName skill = SkillName.Wrestling;
				int level = 1;

				int TotalTime = Buff( m, "time", level, skill );
				int MySkill = Buff( m, "strength", level, skill );

				object[] mods = new object[]
				{
					new DefaultSkillMod( skill, true, MySkill ),
				};

				TableElixirWrestling[m] = mods;

				m.AddSkillMod( (SkillMod)mods[0] );

				new InternalTimer( m, TimeSpan.FromMinutes( TotalTime ) ).Start();

				BasePotion.PlayDrinkEffect( m );

				m.BeginAction( typeof( ElixirWrestling ) );

				this.Amount--;
					if (this.Amount <= 0)
					this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = m;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					ElixirWrestling.RemoveEffect( m_m );
					Stop();
				}
			}
		}
	}
}