using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PenumbraMod.Content.Items
{
	public class ShadowBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("ShadowBlaster"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Fires a spread of 3 Shadow Hell Bullets,inflicting Shadow Flame to enemies."
				+ "\n''This is why kids you shouldn't play with guns!''"
				+ "\n[c/00FF00:-Developer Item-]");
			
		}

		public override void SetDefaults()
		{
			Item.damage = 270;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 42;
			Item.height = 42;
            Item.useAnimation = 12;
            Item.useTime = 4; // one third of useAnimation
            Item.reuseDelay = 14;
            Item.useStyle = 5;
			Item.knockBack = 6;
			Item.value = 4450;
			Item.rare = 6;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<ShadowHellBullets>();
			Item.shootSpeed = 12f;
			Item.noMelee = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddIngredient(ModContent.ItemType<PenumbraMod.Content.Items.MirrorFragment>(), 10);
            recipe.AddIngredient(ItemID.FragmentVortex, 12);
			recipe.AddIngredient(ItemID.CursedFlame, 7);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
		}

	


	}
}