using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Custom
{
	public class HitchingPost : Item
	{
		public Hashtable StabledTable;
		private bool m_solid;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool solid
        {
            get{ return m_solid; }
            set{ m_solid = value; }
        }

		[Constructable]
		public HitchingPost() : base()
		{
			ItemID = 0x14E7;
			Name = "Hitching Post";
			Hue = 0x33;
			StabledTable = new Hashtable();
			solid = false;
		}

		public HitchingPost(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			//if (from.InRange(this, 20))
			//{
				if (StabledTable != null && StabledTable.Count > 0)
				{
					if (StabledTable[from] != null && StabledTable[from] is BaseCreature)
					{
						BaseCreature bc = StabledTable[from] as BaseCreature;

						if (bc.IsHitchStabled && bc.ControlMaster == null)
						{
							if (from.Followers + bc.ControlSlots <= from.FollowersMax)
							{
								bc.IsHitchStabled = false;
								bc.Blessed = false;
								bc.ControlMaster = from;
								bc.ControlOrder = OrderType.Follow;
								bc.CantWalk = false;
								StabledTable.Remove(from);
								from.SendMessage("Your pet was released.");
								if ( !solid )
									this.Movable = true;
							}
							else
								from.SendMessage( bc.Name + " remained on the post because you have too many followers." );
						}
					}
					else
						from.SendMessage("Sorry! This hitching post is already busy! Try to find another.");
				}
				else
				{
					from.Target = new StableTarget(this);
					from.SendMessage("What pet do you want to hitch?");
				}
			//}
			//else
			//	from.SendMessage("You are too far away!");
		}
		
		public void removeowner (Mobile from)
		{
			StabledTable.Remove(from);
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)2); // version

			List<Mobile> listOwners = new List<Mobile>(StabledTable.Keys.Cast<Mobile>());
			List<Mobile> listPets = new List<Mobile>(StabledTable.Values.Cast<Mobile>());

			writer.Write( (bool) solid );
			writer.WriteMobileList(listOwners);
			writer.WriteMobileList(listPets);
			
			
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch(version)
			{
				case 2:
				{
					solid = reader.ReadBool();
					goto case 1;
				}
				case 1:
					{
						StabledTable = new Hashtable();

						List<Mobile> listOwners = reader.ReadStrongMobileList();
						List<Mobile> listPets = reader.ReadStrongMobileList();

						for (int i = 0; i < listOwners.Count; i++)
						{
							if (listOwners[i] != null && listPets[i] != null)
								StabledTable.Add(listOwners[i], listPets[i]);
						}
						
						goto case 0;
					}
				case 0:
					{
						if (StabledTable == null)
							StabledTable = new Hashtable();
					}
					break;
			}


		}

		private class StableTarget : Target
		{
			HitchingPost m_Post;
			public StableTarget(HitchingPost post) : base(2, false, TargetFlags.None)
			{
				m_Post = post;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Post == null || m_Post.Deleted)
					return;
				else if (m_Post.StabledTable != null && m_Post.StabledTable.Count > 0)
				{
					from.SendMessage("Sorry! This hitching post is already busy! Try to find another.");
					return;
				}

				if (targeted is GolemPorter )
				{
					from.SendMessage("You can't see a way to hitch that, try the controller.");
					return;
				}
				if (targeted is HenchmanArcher || targeted is HenchmanFighter || targeted is HenchmanWizard || targeted is Squire)
				{
					from.SendMessage("You can't see a way to hitch that, try some bondage gear.");
					return;
				}
				if (targeted is BaseFamiliar )
				{
					from.SendMessage("Just dismiss the familiar if they are getting on your nerves.");
					return;
				}	
				if (targeted is BaseCreature)
				{
					BaseCreature bc = targeted as BaseCreature;

					if (!bc.InRange(m_Post, 10))
					{
						from.SendMessage("You must be near with hitching post for stable pet.");
						return;
					}

					if (bc.Controlled)
					{
						if (bc.ControlMaster == from)
						{
							if (bc.IsDeadPet)
								from.SendMessage("Creature must be alive!");
							else if (bc.IsHitchStabled)
								from.SendMessage("Your pet is already stabled");
							else
							{
								bc.IsHitchStabled = true;
								bc.Blessed = true;								
								bc.ControlOrder = OrderType.Stay;
								bc.CantWalk = true;
								bc.ControlMaster = null;
								m_Post.StabledTable.Add(from, bc);
								from.SendMessage("Your pet has been stabled. You can use hitching post again when you will need claim your pet.");
								if (m_Post.Movable && !m_Post.IsLockedDown)
								{
									m_Post.Movable = false;
									m_Post.solid = false;
								}
								else if (!m_Post.Movable || m_Post.IsLockedDown)
									m_Post.solid = true;
							}
						}
						else
							from.SendMessage("You can stable only own pets!");
					}
					else
					from.SendMessage("You must be the one controlling the pet!");
				}
			}
		}
	}
}
