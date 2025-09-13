using System;
using Server;

namespace Server.Items
{
	public enum ComponentType : byte
	{
		Power = 0,
		Regular = 1,
		Channeling = 2,
		Luck = 3
	}
	public class PhylacteryComponent : Item
	{
		private string m_BoundEssence; 

		public string BoundEssence
		{
			get
			{
				return m_BoundEssence;
			}
			set
			{
				if ( m_BoundEssence != value )
				{
					m_BoundEssence = value;
					InvalidateProperties();
				}
			}
		}


		private ComponentType m_ComponentType;

		public ComponentType ComponentType
		{
			get
			{
				return m_ComponentType;
			}
			set
			{
				if ( m_ComponentType != value )
				{
					m_ComponentType = value;
					InvalidateProperties();
				}
			}
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public PhylacteryComponent() : this( 1 )
		{
		}

		[Constructable]
		public PhylacteryComponent( int amount ) : base( 0x0F7E )
		{
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Amount = amount;
			Light = LightType.Circle150;
		}

		public PhylacteryComponent( Serial serial ) : base( serial )
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
			Light = LightType.Circle150;
		}
	}
}