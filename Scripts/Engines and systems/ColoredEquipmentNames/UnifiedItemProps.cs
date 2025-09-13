using System;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace ItemNameHue
{
    public class UnifiedItemProps
    {
	public static string GetArmorItemValue(Item item)
	{
	    int rarityValue = 0, rarityProps = 0;
	    ItemTier.GetItemTier(item, out rarityValue, out rarityProps);

	    if (rarityProps >= 7)
		return "<BASEFONT COLOR=#FF944D>";
	    else if (rarityProps >= 5)
		return "<BASEFONT COLOR=#B48FFF>";
	    else if (rarityProps >= 3)
		return "<BASEFONT COLOR=#8DBAE8>";
	    else if (rarityProps >= 1)
		return "<BASEFONT COLOR=#A1E68A>";

	    return "<BASEFONT COLOR=#D6D6D6>";
	}

	public static string RarityNameMod(Item item, string orig)
	{
	    return (string)(GetArmorItemValue(item) + orig + "<BASEFONT COLOR=#FFFFFF>");
	}
    }
}
