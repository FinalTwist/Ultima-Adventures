using System;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class PolishBoneBrush : Item
	{
		[Constructable]
		public PolishBoneBrush() : base( 0x1371 )
		{
			Name = "bone polishing brush";
			Weight = 2.0;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Polish Bones For Crafting");
        } 

		public PolishBoneBrush( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "Which bones do you want to polish?" );
				from.Target = new PickBones( this );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		private class PickBones : Target
		{
			private PolishBoneBrush m_PolishBoneBrush;

			public PickBones( PolishBoneBrush brush ) : base( 1, false, TargetFlags.None )
			{
				m_PolishBoneBrush = brush;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item bone = targeted as Item;

					int boneCount = 0;
					int skullCount = 0;

					if ( !bone.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only polish bones that are in your pack." );
					}
					else if ( bone is Container )
					{
						from.SendMessage( "You cannot polish containers." );
					}
					else if ( bone.ItemID == 0xECA ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0xECB ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0xECC ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0xECD ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0xECE ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0xECF ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0xED0 ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0xED1 ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0xED2 ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0xF7E ){ boneCount = bone.Amount; skullCount = 0; }
					else if ( bone.ItemID == 0xF80 ){ boneCount = 1; skullCount = 0; }
					else if ( bone.ItemID == 0x1B09 ){ boneCount = 2; skullCount = 1; }
					else if ( bone.ItemID == 0x1B0A ){ boneCount = 3; skullCount = 0; }
					else if ( bone.ItemID == 0x1B0B ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0x1B0C ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0x1B0D ){ boneCount = 2; skullCount = 1; }
					else if ( bone.ItemID == 0x1B0E ){ boneCount = 3; skullCount = 0; }
					else if ( bone.ItemID == 0x1B0F ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0x1B10 ){ boneCount = 3; skullCount = 1; }
					else if ( bone.ItemID == 0x1B11 ){ boneCount = 1; skullCount = 0; }
					else if ( bone.ItemID == 0x1B12 ){ boneCount = 1; skullCount = 0; }
					else if ( bone.ItemID == 0x1B13 ){ boneCount = 1; skullCount = 0; }
					else if ( bone.ItemID == 0x1B14 ){ boneCount = 1; skullCount = 0; }
					else if ( bone.ItemID == 0x1B16 ){ boneCount = 1; skullCount = 0; }
					else if ( bone.ItemID == 0x1B19 ){ boneCount = 2; skullCount = 0; }
					else if ( bone.ItemID == 0x2C99 ){ boneCount = 1; skullCount = 0; }
					else if ( bone.ItemID == 0x1B17 ){ boneCount = 2; skullCount = 0; }
					else if ( bone.ItemID == 0x1B18 ){ boneCount = 2; skullCount = 0; }
					else if ( bone.ItemID == 0x1B19 ){ boneCount = 2; skullCount = 0; }
					else if ( bone.ItemID == 0x1B1A ){ boneCount = 2; skullCount = 0; }
					else if ( bone.ItemID == 0x1B1B ){ boneCount = 1; skullCount = 0; }
					else if ( bone.ItemID == 0x1B1C ){ boneCount = 1; skullCount = 0; }
					else if ( bone.ItemID == 0x1AE0 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1AE1 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1AE2 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1AE3 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1AE4 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x224 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x224 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1853 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1854 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1855 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1856 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1857 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1858 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1859 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x185A ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1AEE ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x1AEF ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x2203 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x2204 ){ boneCount = 0; skullCount = 10; }
					else if ( bone.ItemID == 0x224E ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x224F ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x2250 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x2251 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x2C95 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x3DCC ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x3DCD ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x3DE0 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x3DE1 ){ boneCount = 0; skullCount = 1; }
					else if ( bone.ItemID == 0x42B5 ){ boneCount = 0; skullCount = 3; }
					else
					{
						from.SendMessage( "You cannot polish that." );
					}

					if ( boneCount > 0 || skullCount > 0 )
					{
						if ( boneCount > 0 )
							from.AddToBackpack( new PolishedBone(boneCount) );

						if ( skullCount > 0 )
							from.AddToBackpack( new PolishedSkull(skullCount) );

						from.SendMessage( "You polish the bones so they can be used for crafting." );
						from.RevealingAction();
						from.PlaySound( 0x04F );
						bone.Delete();
					}
				}
				else
				{
					from.SendMessage( "You cannot polish that." );
				}
			}
		}
	}
}