using System;
using Server;
using System.Collections.Generic;
using System.Linq;
using Server.Items;
using Server.Network;
using System.Collections;
using Server.Spells;

namespace Server.Mobiles
{
    public class CustomAbilityList
    {

        [CommandProperty(AccessLevel.GameMaster)]
        public CustomAbility[] CustomAbilities { get; private set; }

	public CustomAbilityList()
	{
	}


	public void Add(CustomAbility ability)
	{
	    if(CustomAbilities == null)
	    {
		CustomAbilities = new CustomAbility[] { ability };
	    }
	    else if(!CustomAbilities.Any( a=> a == ability))
	    {
		var temp = CustomAbilities;

		CustomAbilities = new CustomAbility[temp.Length + 1];

		for(int i = 0; i < temp.Length; i++)
			CustomAbilities[i] = temp[i];

		CustomAbilities[temp.Length] = ability;
	    }

	}

	public void Remove(CustomAbility ability)
	{
           if (CustomAbilities == null || !CustomAbilities.Any(a => a == ability))
                return;

            var list = CustomAbilities.ToList();

            list.Remove(ability);

            CustomAbilities = list.ToArray();

            //ColUtility.Free(list);
			//list.Clear();
			list.Clear();
			list.TrimExcess();
	}

        public IEnumerable<CustomAbility> EnumerateCustomAbilities()
        {
            if (CustomAbilities == null)
            {
                yield break;
            }

            foreach (var ability in CustomAbilities)
            {
                yield return ability;
            }
        }

        public CustomAbility[] GetCustomAbilities()
        {
            return EnumerateCustomAbilities().ToArray();
        }


	public void CheckTrigger(BaseCreature creature)
	{

	    if (!(creature.Combatant is Mobile))
		return;

	    Mobile combatant = creature.Combatant as Mobile;

	    var defender = combatant;

	    if(defender == null)
		return;

	    if(defender is Mobile)
	    {
		CustomAbility ability = null;
		CustomAbility[] abilties = EnumerateCustomAbilities().Where(m => !m.IsInCooldown(creature)).ToArray();

		if (abilties != null && abilties.Length > 0)
		{
                        ability = abilties[Utility.Random(abilties.Length)];
		}

		if (ability != null )
		{
                        
                        ability.Trigger(creature, defender, 0, 0);
		}
	    }
	}

	public CustomAbilityList(GenericReader reader)
	{
            int version = reader.ReadInt();

            int count = reader.ReadInt();
            CustomAbilities = new CustomAbility[count];

            for (int i = 0; i < count; i++)
            {
                CustomAbilities[i] = CustomAbility.Abilities[reader.ReadInt()];
            }

	}

	public virtual void Serialize(GenericWriter writer)
	{
	    writer.Write(1);

	    writer.Write(CustomAbilities != null ? CustomAbilities.Length : 0);

	    if(CustomAbilities != null)
	    {
		foreach(var abil in CustomAbilities)
		{
		    writer.Write(Array.IndexOf(CustomAbility.Abilities, abil));
		}
	    }
	}
    }
}