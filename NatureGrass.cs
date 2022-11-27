using PenumbraMod.Content;
using PenumbraMod.Content.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PenumbraMod.Content.Items
{
	public class NatureGrass : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nature Grass"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("One of the many nature gifts was given to you...");
			
		}

		public override void SetDefaults()
		{
			Item.damage = 98;
			Item.DamageType = DamageClass.Melee;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 36;
			Item.useAnimation = 26;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 1000;
			Item.rare = ModContent.RarityType<NatureGrassRarity>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<NatureFlower>();
			Item.shootSpeed = 10f;
			
		}
       
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ChlorophyteBar, 22);
			recipe.AddIngredient(ItemID.BladeofGrass, 1);
			recipe.AddIngredient(ItemID.Vine, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 7);


            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}