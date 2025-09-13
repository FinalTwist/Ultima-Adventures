using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;

namespace Server.Spells.Third
{
    public class TelekinesisSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Telekinesis", "Ort Por Ylem",
            203,
            9031,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot);

        public TelekinesisSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Third;
            }
        }

        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(ITelekinesisable obj)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, obj);

                obj.OnTelekinesis(this.Caster);
            }

            this.FinishSequence();
        }

        public void Target(Container item)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, item);

                object root = item.RootParent;

                if (!item.IsAccessibleTo(this.Caster))
                {
                    item.OnDoubleClickNotAccessible(this.Caster);
                }
                else if (!item.CheckItemUse(this.Caster, item))
                {
                }
                else if (root != null && root is Mobile && root != this.Caster)
                {
                    item.OnSnoop(this.Caster);
                }
                else if (item is Corpse && !((Corpse)item).CheckLoot(this.Caster, null))
                {
                }
                else if (this.Caster.Region.OnDoubleClick(this.Caster, item))
                {
                    Effects.SendLocationParticles(EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, 5022, 0);
                    Effects.PlaySound(item.Location, item.Map, 0x1F5);

                    item.OnItemUsed(this.Caster, item);
                }
            }

            this.FinishSequence();
        }

#region Grab
        public void Target(Item item)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, item);
                object root = item.RootParent;

				if (item.Movable == false){ Caster.SendMessage( "That item does not seem to move." ); }
				else if (item.Amount > 1){ Caster.SendMessage( "There are too many items stacked here to move." ); }
				else if (item.Weight > (Caster.Int / 20)){ Caster.SendMessage( "That is to heavy to move." ); }
				else if (item.RootParentEntity != null){ Caster.SendMessage( "You can not move objects that are inside of other objects or being worn." ); }
				else
				{
					Effects.SendLocationParticles(EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, 5022, 0);
					Effects.PlaySound(item.Location, item.Map, 0x1F5);
					Caster.AddToBackpack( item );
					Caster.SendMessage( "You move the object to within your grasp and place it in your backpack."); 
				}
			}
            this.FinishSequence();
        }
#endregion

        public class InternalTarget : Target
        {
            private readonly TelekinesisSpell m_Owner;
            public InternalTarget(TelekinesisSpell owner) : base(Core.ML ? 10 : 12, false, TargetFlags.None)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is ITelekinesisable)
                    this.m_Owner.Target((ITelekinesisable)o);
                else if (o is Container)
                    this.m_Owner.Target((Container)o);
                    else if (o is Item)
                    this.m_Owner.Target((Item)o);
                else
                    from.SendLocalizedMessage(501857); // This spell won't work on that!
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}

namespace Server
{
    public interface ITelekinesisable : IPoint3D
    {
        void OnTelekinesis(Mobile from);
    }
}