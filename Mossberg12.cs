using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PenumbraMod.Content;
using Terraria.Audio;
using Terraria.DataStructures;

namespace PenumbraMod.Content.Items
{
	public class Mossberg12 : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Mossberg-12"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Fires a lot of bullets dealing a lot of damage"
				+ "\n'This is for those who can't aim...'");
			
		}

		public override void SetDefaults()
		{
			Item.damage = 135;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.knockBack = 6;
			Item.value = 1000;
			Item.rare = ItemRarityID.Yellow;
            Item.UseSound = new SoundStyle("PenumbraMod/Assets/Sounds/Items/HeavyShotgun1")
            {
                Volume = 1.2f,
                PitchVariance = 0.2f,
                MaxInstances = 5,
            };
            Item.autoReuse = true;
			Item.shoot = ProjectileID.Bullet;
			Item.useAmmo = AmmoID.Bullet;
			Item.shootSpeed = 12f;
			Item.noMelee = true;
            Item.scale = 1.3f;
            Item.crit = 12;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3f, 1f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            const int NumProjectiles = 10;

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                float ceilingLimit = target.Y;
                if (ceilingLimit > player.Center.Y - 5f)
                {
                    ceilingLimit = player.Center.Y - 5f;
                }

                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(20));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.4f);

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                type = ProjectileID.Bullet;
                damage = 39;

            }
            return true;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddIngredient(ItemID.QuadBarrelShotgun, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }



    }
}