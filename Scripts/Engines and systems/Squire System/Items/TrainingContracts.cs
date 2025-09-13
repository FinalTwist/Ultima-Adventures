using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;
using Server.Multis;

namespace Server.Items
{
	public class STrainingContract : Item
	{
		[Constructable]
		public STrainingContract() : this( 1 )
		{
		}
		
		[Constructable]
		public STrainingContract( int amount ) : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "A Squire Training Contract";
		}
		
		public STrainingContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
	
	public class SWrestlingContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SWrestlingContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "A Wrestling Training Contract";
			skill = SkillName.Wrestling;
		}

		public SWrestlingContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class STacticsContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public STacticsContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Tactics Training Contract";
			skill = SkillName.Tactics;
		}

		public STacticsContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SMagicResistContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SMagicResistContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Magic Resistance Training Contract";
			skill = SkillName.MagicResist;
		}

		public SMagicResistContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( "Magic Resistance" + ": 30" );
		}
	}
	
	public class SAnatomyContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SAnatomyContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Anatomy Training Contract";
			skill = SkillName.Anatomy;
		}

		public SAnatomyContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SSwordsContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SSwordsContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Swordsmanship Training Contract";
			skill = SkillName.Swords;
		}

		public SSwordsContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( "Swordsmanship" + ": 30" );
		}
	}
	
	public class SMacingContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SMacingContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Macefighting Training Contract";
			skill = SkillName.Macing;
		}

		public SMacingContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( "Macefighting" + ": 30" );
		}
	}
	
	public class SFencingContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SFencingContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Fencing Training Contract";
			skill = SkillName.Fencing;
		}

		public SFencingContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SArcheryContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SArcheryContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Archery Training Contract";
			skill = SkillName.Archery;
		}

		public SArcheryContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SParryContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SParryContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Parry Training Contract";
			skill = SkillName.Parry;
		}

		public SParryContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SHealingContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SHealingContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Healing Training Contract";
			skill = SkillName.Healing;
		}

		public SHealingContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SVeterinaryContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SVeterinaryContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Veterinary Training Contract";
			skill = SkillName.Veterinary;
		}

		public SVeterinaryContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.Target = new STrainingTarget( from, skill, this );
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SEvalIntContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SEvalIntContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Evaluate Intelligence Training Contract";
			skill = SkillName.EvalInt;
		}

		public SEvalIntContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( "Evaluate Intelligence" + ": 30" );
		}
	}
	
	public class SMeditationContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SMeditationContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Meditation Training Contract";
			skill = SkillName.Meditation;
		}

		public SMeditationContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SAnimalLoreContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SAnimalLoreContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Animal Lore Training Contract";
			skill = SkillName.AnimalLore;
		}

		public SAnimalLoreContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( "Animal Lore" + ": 30" );
		}
	}
	
	public class SFocusContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SFocusContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Focus Training Contract";
			skill = SkillName.Focus;
		}

		public SFocusContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SMusicianshipContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SMusicianshipContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Musicianship Training Contract";
			skill = SkillName.Musicianship;
		}

		public SMusicianshipContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SPeacemakingContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SPeacemakingContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Peacemaking Training Contract";
			skill = SkillName.Peacemaking;
		}

		public SPeacemakingContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SDiscordanceContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SDiscordanceContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Discordance Training Contract";
			skill = SkillName.Discordance;
		}

		public SDiscordanceContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SProvocationContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SProvocationContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Provocation Training Contract";
			skill = SkillName.Provocation;
		}

		public SProvocationContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SHidingContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SHidingContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Hiding Training Contract";
			skill = SkillName.Hiding;
		}

		public SHidingContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SStealingContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SStealingContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "A Stealing Training Contract";
			skill = SkillName.Stealing;
		}

		public SStealingContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SLockpickingContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SLockpickingContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "A Lockpicking Training Contract";
			skill = SkillName.Lockpicking;
		}

		public SLockpickingContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SSpiritSpeakContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SSpiritSpeakContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "A Spirit Speak Training Contract";
			skill = SkillName.SpiritSpeak;
		}

		public SSpiritSpeakContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire )
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( "Spirit Speak" + ": 30" );
		}
	}
	
	public class SPoisoningContract : STrainingContract
	{
		private SkillName skill;
	
		[Constructable]
		public SPoisoningContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Poisoning Training Contract";
			skill = SkillName.Poisoning;
		}

		public SPoisoningContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SChivalryContract : STrainingContract // Added 1.9.6
	{
		private SkillName skill;
	
		[Constructable]
		public SChivalryContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Chivalry Training Contract";
			skill = SkillName.Chivalry;
		}

		public SChivalryContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	public class SBushidoContract : STrainingContract // Added Rafa
	{
		private SkillName skill;
	
		[Constructable]
		public SBushidoContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 401;
			Name = "An Bushido Training Contract";
			skill = SkillName.Bushido;
		}

		public SBushidoContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)1 ); //version
			
			writer.Write( (int) skill );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1: skill = (SkillName)reader.ReadInt(); break;
				case 0: break;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from is Squire ) 
			{
				if( ((Squire)from).Skills[skill].Lock == SkillLock.Up )
				{
					if( ((Squire)from).Skills[skill].Base < 30.0 )
					{
						if( ((Squire)from).m_SquireBeQuiet == false )
						{
							SquireDialog.DoSquireDialog( from, ((Squire)from), SquireDialogTree.LearnsFromContract, null, null );
						}
						((Squire)from).Skills[skill].Base = 30.0;
						this.Delete();
					}
					else
					{
						if (((Squire)from).Skills[skill].Base < 50)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 15;
						else if (((Squire)from).Skills[skill].Base < 75)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 5;
						else if (((Squire)from).Skills[skill].Base < 90)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 2;		
						else if (((Squire)from).Skills[skill].Base <= 100)
							((Squire)from).Skills[skill].Base = ((Squire)from).Skills[skill].Base + 1;						
						((Squire)from).ControlMaster.SendMessage( "Your Squire Learnt Something" );
					}
				}
				else
				{
					((Squire)from).ControlMaster.SendMessage( "This squire is not prepared to learn more of this skill." );
				}
			}
			else
			{
				from.Target = new STrainingTarget( from, skill, this );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
			
			list.Add( skill + ": 30" );
		}
	}
	
	
	internal class STrainingTarget : Target
	{
		private SkillName m_Skill;
		private Item m_Contract;

		public STrainingTarget( Mobile from, SkillName skill, Item contract ) : base( 1, false, TargetFlags.None )
		{
			m_Skill = skill;
			m_Contract = contract;
			from.SendMessage( "Choose a squire to teach " + skill + "." );
		}

		protected override void OnTarget( Mobile from, object obj )
		{
			DoOnTarget( from, obj, m_Skill, m_Contract );
		}

		public static void DoOnTarget( Mobile from, object o, SkillName skill, Item contract )
		{
			if( o is Mobile )
			{
				if( o is Squire )
				{
					if( ((Squire)o).Controlled == true && ((Squire)o).ControlMaster == from )
					{
						if( ((Squire)o).Skills[skill].Lock == SkillLock.Up )
						{
							if( ((Squire)o).Skills[skill].Base < 30.0 )
							{
								if( ((Squire)o).m_SquireBeQuiet == false )
								{
									SquireDialog.DoSquireDialog( from, ((Squire)o), SquireDialogTree.LearnsFromContract, null, null );
								}
								((Squire)o).Skills[skill].Base = 30.0;
								contract.Delete();
							}
							else
							{
						if (((Squire)o).Skills[skill].Base < 50)
							((Squire)o).Skills[skill].Base = ((Squire)o).Skills[skill].Base + 15;
						else if (((Squire)o).Skills[skill].Base < 75)
							((Squire)o).Skills[skill].Base = ((Squire)o).Skills[skill].Base + 5;
						else if (((Squire)o).Skills[skill].Base < 90)
							((Squire)o).Skills[skill].Base = ((Squire)o).Skills[skill].Base + 2;		
						else if (((Squire)o).Skills[skill].Base <= 100)
							((Squire)o).Skills[skill].Base = ((Squire)o).Skills[skill].Base + 1;						
						((Squire)o).ControlMaster.SendMessage( "Your Squire Learnt Something" );
							}
						}
						else
						{
							from.SendMessage( "This squire is not prepared to learn more of this skill." );
						}
					}
					else
					{
						from.SendMessage( "This squire does not belong to you." );
					}
				}
				else
				{
					from.SendMessage( "This would be a waste of a training contract." );
				}
			}
			else
			{
				from.SendMessage( "That cannot learn from a training contract." );
			}
		}
	}
}