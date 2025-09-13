using System;
using Server.Network;
using Server.Gumps;
using Server.Multis;
using System.Collections.Generic;
using Server.ContextMenus; 

namespace Server.Items
{
	public class CarvedPumpkin : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x4691; } }
		
		public override int UnlitItemID{ get { return 0x4694; } }

		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin() : base( 0x4694 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
		}
	public class CarvedPumpkin2 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x4695; } }
		public override int UnlitItemID{ get { return 0x4698; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin2() : base( 0x4698 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin2( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin3 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5458; } }
		public override int UnlitItemID{ get { return 0x5457; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin3() : base( 0x5457 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin3( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin4 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x545C; } }
		public override int UnlitItemID{ get { return 0x545B; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin4() : base( 0x545B )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin4( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin5 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5460; } }
		public override int UnlitItemID{ get { return 0x545F; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin5() : base( 0x545F )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin5( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin6 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5465; } }
		public override int UnlitItemID{ get { return 0x5464; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin6() : base( 0x5464 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin6( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin7 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5469; } }
		public override int UnlitItemID{ get { return 0x5468; } }
		private SecureLevel m_Level;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin7() : base( 0x5468 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
	{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}
		
		public CarvedPumpkin7( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin8 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x546D; } }
		public override int UnlitItemID{ get { return 0x546C; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin8() : base( 0x546C )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin8( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin9 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5471; } }
		public override int UnlitItemID{ get { return 0x5470; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin9() : base( 0x5470 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin9( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin10 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5475; } }
		public override int UnlitItemID{ get { return 0x5474; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin10() : base( 0x5474 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin10( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin11 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5479; } }
		public override int UnlitItemID{ get { return 0x5478; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin11() : base( 0x5478 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin11( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin12 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x547D; } }
		public override int UnlitItemID{ get { return 0x547C; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin12() : base( 0x547C )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin12( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin13 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5481; } }
		public override int UnlitItemID{ get { return 0x5480; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin13() : base( 0x5480 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin13( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin14 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5540; } }
		public override int UnlitItemID{ get { return 0x555B; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin14() : base( 0x555B )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin14( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin15 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5548; } }
		public override int UnlitItemID{ get { return 0x555B; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin15() : base( 0x555B )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin15( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin16 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x554A; } }
		public override int UnlitItemID{ get { return 0x5549; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin16() : base( 0x5549 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin16( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin17 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x554E; } }
		public override int UnlitItemID{ get { return 0x554D; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin17() : base( 0x554D )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin17( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin18 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5552; } }
		public override int UnlitItemID{ get { return 0x5551; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin18() : base( 0x5551 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin18( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin19 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5556; } }
		public override int UnlitItemID{ get { return 0x5555; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin19() : base( 0x5555 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin19( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
	public class CarvedPumpkin20 : BaseLight, ISecurable
	{
		public override int LitItemID{ get { return 0x5452; } }
		public override int UnlitItemID{ get { return 0x5451; } }
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public CarvedPumpkin20() : base( 0x5451 )
		{
			Name = "Jack-O-Lantern";
			Duration = TimeSpan.Zero; // Never burnt out
			Weight = 10;
			Light = LightType.Circle150;
			Burning = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				base.OnDoubleClick( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public CarvedPumpkin20( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_Level );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel) reader.ReadEncodedInt();
		}
	}
}