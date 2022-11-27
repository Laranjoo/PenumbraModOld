using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using PenumbraMod.Common.DropConditions;
using System.Linq;
using PenumbraMod.Content.Items.Consumables;

namespace PenumbraMod.Common.GlobalNPCs
{
	// This file shows numerous examples of what you can do with the extensive NPC Loot lootable system.
	// You can find more info on the wiki: https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4
	// Despite this file being GlobalNPC, everything here can be used with a ModNPC as well! See examples of this in the Content/NPCs folder.
	public class MarshmellowDrop : GlobalNPC
	{
		
		public override void ModifyGlobalLoot(GlobalLoot globalLoot) {
			
			globalLoot.Add(ItemDropRule.ByCondition(new MarshmellowCondition(), ModContent.ItemType<Marshmellow>(), 2, 1, 1, 7));
		}
	}
}
