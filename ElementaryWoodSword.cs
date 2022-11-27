using PenumbraMod.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using PenumbraMod.Content.Buffs;
using System;

namespace PenumbraMod.Content.Items
{
	public class ElementaryWoodSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			 DisplayName.SetDefault("Elementary Wood Sword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("An old elementary sword"
			+ "\nFires an [c/00ffff:Enchanted Shot]"
				+ "\n[c/00ff00:By right clicking], the blade will fire 3 Differents Elemental Shots! [c/cf8484:(With a 15 sec cooldown)]"
				+ "\n[c/cc00ff:Corruption]: Shoots 2 Eater of souls like projectile that slowly homes into enemies, dealing normal damage"
				+ "\n[c/1e90ff:Ice]: Shoots an ice crystal that deals high damage"
				+ "\n[c/adff2f:Jungle]: Shoots 2 Snacther like Projectile that quickly homes into enemies, dealing lower damage");

        }

		public override void SetDefaults()
		{
			Item.damage = 33;
			Item.DamageType = DamageClass.Melee;
			Item.width = 42;
			Item.height = 42;
			Item.useTime = 36;
			Item.useAnimation = 28;
			Item.useStyle = 1;
			Item.knockBack = 4;
			Item.value = 1000;
			Item.rare = 3;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<EnchantedShot>();
			Item.shootSpeed = 10f;
			Item.crit = 9;
		    
			
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(1))
            {
                
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Enchanted_Pink);
				
            }
        }
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			

			if (player.altFunctionUse == 2)
			{
				Item.useStyle = 1;
				Item.useTime = 36;
				Item.useAnimation = 28;
				Item.shoot = ModContent.ProjectileType<ProjectileCorruption>();
                Item.shootSpeed = 10f;
				
				
            }
			else
			{
                Item.useStyle = 1;
                Item.useTime = 36;
                Item.useAnimation = 28;               
                Item.shoot = ModContent.ProjectileType<EnchantedShot>();
                Item.shootSpeed = 10f;

            }
			return (!player.HasBuff(ModContent.BuffType<ElementalCooldown>()));
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				player.AddBuff(ModContent.BuffType<ElementalCooldown>(), 960);
				
				const int NumProjectiles = 2;

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
					type = ModContent.ProjectileType<ProjectileSnacther>();
					damage = 23;

					Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
					type = ModContent.ProjectileType<ProjectileIce>();
					damage = 60;
					
				}

				return true;
			}
			return true;
		}

		


		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			
			recipe.AddIngredient(ItemID.WoodenSword, 1);
			recipe.AddIngredient(ItemID.BorealWoodSword, 1);
			recipe.AddIngredient(ItemID.RichMahoganySword, 1);
			recipe.AddIngredient(ItemID.PalmWoodSword, 1);
			recipe.AddIngredient(ItemID.ShadewoodSword, 1);
			recipe.AddIngredient(ItemID.EnchantedSword, 1);
			recipe.AddIngredient(ModContent.ItemType<RustyCopperSword>(), 1);
			recipe.AddIngredient(ItemID.Diamond, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

            Recipe recipe2 = CreateRecipe();

            recipe2.AddIngredient(ItemID.WoodenSword, 1);
            recipe2.AddIngredient(ItemID.BorealWoodSword, 1);
            recipe2.AddIngredient(ItemID.RichMahoganySword, 1);
            recipe2.AddIngredient(ItemID.PalmWoodSword, 1);
            recipe2.AddIngredient(ItemID.EbonwoodSword, 1);
            recipe2.AddIngredient(ItemID.EnchantedSword, 1);
            recipe2.AddIngredient(ModContent.ItemType<RustyCopperSword>(), 1);
            recipe2.AddIngredient(ItemID.Diamond, 5);
            recipe2.AddTile(TileID.Anvils);
			recipe2.Register();
		}
	}
}