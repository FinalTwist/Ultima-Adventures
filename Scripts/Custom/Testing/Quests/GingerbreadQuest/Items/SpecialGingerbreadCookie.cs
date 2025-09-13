using System;

namespace Server.Items
{
	public abstract class BaseSpecialGingerbreadCookie : Item
	{
		public virtual int Bonus{ get{ return 0; } }
		public virtual StatType Type{ get{ return StatType.Str; } }

        public BaseSpecialGingerbreadCookie(int hue)
            : base(0x2BE2)
		{
			Weight = 1.0;
            Hue = 0;

		


		}

        public BaseSpecialGingerbreadCookie(Serial serial)
            : base(serial)
		{
		}
		
		public virtual bool Apply( Mobile from )
		{
			bool applied = Spells.SpellHelper.AddStatOffset( from, Type, Bonus, TimeSpan.FromMinutes( 30.0 ) );
			
			if ( !applied )
				from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.
				
			return applied;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( Apply( from ) )
			{
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				Delete();
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
	
	public class SpecialGingerbreadCookie1 : BaseSpecialGingerbreadCookie
	{
		public override int Bonus{ get{ return 10; } }
		public override StatType Type{ get{ return StatType.Str; } }
		
		//public override int LabelNumber{ get{ return 1041073; } } // prized fish
		
		[Constructable]
		public SpecialGingerbreadCookie1() : base( 151 )
		{
			this.Name = "Special Gingerbread Cookie 1";
		}
		
		public SpecialGingerbreadCookie1( Serial serial ) : base( serial )
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
	public class SpecialGingerbreadCookie2 : BaseSpecialGingerbreadCookie
	{
		public override int Bonus{ get{ return 10; } }
		public override StatType Type{ get{ return StatType.Int; } }
		
		//public override int LabelNumber{ get{ return 1041073; } } // prized fish
		
		[Constructable]
		public SpecialGingerbreadCookie2() : base( 151 )
		{
			this.Name = "Special Gingerbread Cookie 2";
		}
		
		public SpecialGingerbreadCookie2( Serial serial ) : base( serial )
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

    public class SpecialGingerbreadCookie3 : BaseSpecialGingerbreadCookie
	{
		public override int Bonus{ get{ return 10; } }
		public override StatType Type{ get{ return StatType.Dex; } }
		
		//public override int LabelNumber{ get{ return 1041073; } } // prized fish
		
		[Constructable]
		public SpecialGingerbreadCookie3() : base( 151 )
		{
            this.Name = "Special Gingerbread Cookie 3";
		}
		
		public SpecialGingerbreadCookie3( Serial serial ) : base( serial )
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
