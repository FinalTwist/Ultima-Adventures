// Tanner function
//
// Keywords:
// 
//
//
// 
// - dab


#Fragment Britannia, Job, Britannia_Tanner 
{
	#Sophistication High 
	{
		#KEY "*job*", "*what*do*do*", "*occupation*", "*profession*"  
		{
			#Attitude Wicked 
			{
				"I'm the tanner here.",
				"I sell leather goods.",
				"If thou dost need to purchase gloves, or a pouch or a backpack, I should have 'em here in stock."
			}
			#Attitude Neutral 
			{
				"I am a tanner in this fine town.",
				"I can sell thee all kinds of useful leather goods.",
				"I can turn any animal hide into something useful."
			}
			#Attitude Goodhearted 
			{
				"I am the maker and seller of leather goods.  A tanner, if it please thee.",
				"'I can sell thee any leather item that thou might need. If it is in stock, of course.",
				"Gloves, backpacks, clothing..., If thou dost need it, I probably have it. If it is made of leather, anyway."
			}
		}
		#KEY "*gloves*" "*backpack*" "*pouch*" 
		{
			#Attitude Wicked 
			{
				"Either buy something or leave. I've little time for thee.",
				"What, dost thou think! That I would give thee something for free?  Pah!",
				"I have made leather clothing, gloves, pouches... many things."
			}
			#Attitude Neutral 
			{
				"I work hard to produce quality work.  My things should last.",
				"Gloves to protect thy hands and a pack to protect thy possessions.",
				"Thou can't expect to get very far with nothing to hold thy possessions."
			}
			#Attitude Goodhearted 
			{
				"My goods are well-made.  They should last for quite a while.",
				"I usually carry several packs and pouches in my inventory. Oh, and gloves, also.",
				"Gloves to protect thy hands and a pack to protect thy possessions.",
				"Thou can't expect to get very far with nothing to hold thy possessions."
			}
		}
		#KEY  "*hide*", "*fur*"  
		{
			#Attitude Wicked 
			{
				"Yes, I've been known to buy some.",
				"I could be convinced to buy some cut hides or fur."
			}
			#Attitude Neutral 
			{
				"I could possibly be convinced to buy some from thee.",
				"If thou dost have some fur or hide for sale, I might buy it from thee."
			}
			#Attitude Goodhearted 
			{
				"Ah, thou wouldst save me some trouble if thou did sell me hides or fur!",
				"'Twould possibly be worth my time to buy hides from thee."
			}
		}
	}	
	#Sophistication Medium 
	{
		#KEY "*job*", "*what*do*do*", "*occupation*", "*profession*" 
		{
			#Attitude Wicked 
			{
				"I'm the tanner here.",
				"I sell leather goods.",
				"If thou dost need to purchase gloves, or a pouch or a backpack, I should have 'em here in stock."
			}
			#Attitude Neutral 
			{
				"I am a tanner in this fine town.",
				"I can sell thee all kinds of useful leather goods.",
				"I can turn any animal hide into something useful."
			}
			#Attitude Goodhearted 
			{
				"I am the maker and seller of anything leather.  A tanner, if it please thee.",
				"'I can sell to thee any leather item that thou might need. If it is in stock, of course.",
				"Gloves, backpacks, clothing..., If thou dost need it, I probably have it. If it is made of leather, anyway."
			}
		}
		#KEY "*gloves*" "*backpack*" "*pouch*" 
		{
			#Attitude Wicked 
			{
				"Either buy something or leave. I've little time for thee.",
				"What, dost thou think that I would give thee something for free?  Pah!",
				"I have made leather clothing, gloves, pouches... many things."
			}
			#Attitude Neutral 
			{
				"I work hard to produce quality work.  My things should last awhile.",
				"Gloves to protect thy hands and a pack to protect thy possessions.",
				"Thou can't expect to get very far with nothing to hold thy possessions."
			}
			#Attitude Goodhearted 
			{
				"My goods are well-made.  They should last for quite a while.",
				"I usually carry several packs and pouches in my inventory. Oh, and gloves, also.",
				"Gloves to protect thy hands and a pack to protect thy possessions.",
				"Thou can't expect to get very far with nothing to hold thy possessions."
			}
		}
		#KEY  "*hide*", "*fur*"  
		{
			#Attitude Wicked 
			{
				"Yes, I've been known to buy some.",
				"I could be convinced to buy some cut hides or fur."
			}
			#Attitude Neutral 
			{
				"I could possibly be convinced to buy some from thee.",
				"If thou dost have some fur or hide for sale, I might buy it from thee."
			}
			#Attitude Goodhearted 
			{
				"Ah, thou wouldst save me some trouble if thou did sell me hides or fur!",
				"'Twould possibly be worth my time to buy hides from thee."		
			}
		}
	}
	#Sophistication Low 
	{
		#KEY "*job*", "*what*do*do*", "*profession*", "*occupation*" 
		{
			#Attitude Wicked 
			{
				"I'm the tanner here.",
				"I sell leather stuff.",
				"If thou needs to buy gloves, or a pouch or a backpack, I should have 'em here in stock."
			}
			#Attitude Neutral 
			{
				"I'm a tanner here in this town.",
				"I can sell thee all kinds of leather stuff.",
				"I can turn animal hide into somethin'."
			}
			#Attitude Goodhearted 
			{
				"I'm the maker and seller of leather stuff.  A tanner, 'an it please thee.",
				"'I can sell to thee any leather item that thou might need. If I got it, of course.",
				"Gloves, backpacks, clothing..., Thou needs it, I should have it. If it's made of leather, anyway."
			}
		}
		#KEY "*gloves*" "*backpack*" "*pouch*" 
		{
			#Attitude Wicked 
			{
				"Either buy somethin' or leave. I got no time for thee.",
				"What, thou thinks I'd give thee somethin' for free?  Pah!",
				"I've made all sorts of leather things."
			}
			#Attitude Neutral 
			{
				"I work hard to make these things! They should last.",
				"Gloves'll protect thy hands and a pack'll protect thy possessions.",
				"Thou can't expect to get real far without much to hold thy things."
			}
			#Attitude Goodhearted 
			{
				"My stuff is made sturdy!  They should last for a long time, if thou cares for 'em.",
				"I usually got a few packs and pouches around. Gloves, too.",
				"Gloves'll protect thy hands and a pack'll protect thy possessions.",
				"Thou can't expect to get real far without much to hold thy things."
			}
		}
		#KEY  "*hide*", "*fur*"  
		{
			#Attitude Wicked 
			{
				"Yeah, I've been known to buy some.",
				"I could buy some cut hides or fur."
			}
			#Attitude Neutral 
			{
				"I might be convinced to buy some from thee.",
				"If thou got some fur or hide for sale, I might buy it from thee."
			}
			#Attitude Goodhearted 
			{
				"Ah, thou would save me some work if thou sold me hides or fur!",
				"Might be worth my time to buy hides from thee."
			}
		}
	}
}
