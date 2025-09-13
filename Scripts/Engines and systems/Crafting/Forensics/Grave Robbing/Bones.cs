using System;
using Server.Items;

namespace Server.Items
{
	public class Bones : Item
	{
		public static int[] m_Bone = new int[]
		{
			6921, 6922, 6923, 6924, 6925, 6926, 6927, 6928, 6929, 6930, 6931, 6932, 
			6933, 6934, 6935, 6936, 6937, 6938, 6939, 6940, 6880, 6881, 6882, 6883, 
			6884
		};

		[Constructable]
		public Bones() : base( 6921 )
		{
			Name = "bones";
			ItemID = m_Bone[Utility.Random(m_Bone.Length)];
			Weight = 0.1;
		}

		public Bones( Serial serial ) : base( serial )
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