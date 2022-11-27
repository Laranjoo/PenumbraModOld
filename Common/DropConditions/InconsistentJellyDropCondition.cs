﻿using Terraria;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;

namespace PenumbraMod.Common.DropConditions
{
	public class InconsistentJellyDropCondition : IItemDropRuleCondition
	{
		public bool CanDrop(DropAttemptInfo info) {
			if (!info.IsInSimulation) {
				

				NPC npc = info.npc;
				if (npc.boss || NPCID.Sets.CannotDropSouls[npc.type]) {
					return false;
				}

				if (npc.lifeMax <= 9000 || npc.friendly /*|| npc.position.Y <= Main.rockLayer * 16.0*/ || npc.value < 1f) {
					return true;
				}

				return info.player.InZonePurity();
			}
			return false;
		}

		public bool CanShowItemDropInUI() {
			return true;
		}

		public string GetConditionDescription() {
			return "";
		}
	}
}