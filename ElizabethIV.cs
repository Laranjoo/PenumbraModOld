using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PenumbraMod.Content.Items
{
	public class ElizabethIV : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elizabeth IV"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("[c/e3aaff:Elizabeth IV is the legendary talking hilt of the heroes]"
				+ "\n[c/e3aaff:''A masked kid probably lost it''...]");
			
		}

		public override void SetDefaults()
		{
			Item.damage = 87;
			Item.DamageType = DamageClass.Melee;
			Item.width = 82;
			Item.height = 82;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 3240;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.crit = 22;
			
		}

	
	}
}