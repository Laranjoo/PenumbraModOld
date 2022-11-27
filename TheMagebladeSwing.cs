﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace PenumbraMod.Content.Items
{
	public class TheMagebladeSwing : ModProjectile
	{
        protected virtual float HoldoutRangeMin => 0f;
        protected virtual float HoldoutRangeMax => 0.1f;
        public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Mageblade");
            Main.projFrames[Projectile.type] = 10;
        }

		public override void SetDefaults() {
            Projectile.aiStyle = ProjectileID.Spear;
            Projectile.damage = 100;
			Projectile.knockBack = 6f;
			Projectile.width = 38;
			Projectile.height = 38;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true; // Prevents hits through tiles. Most melee weapons that use projectiles have this
            Projectile.hide = true; // Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
            Projectile.timeLeft = 420;
        }
        public override bool PreDrawExtras()
        {
            Projectile.type = ModContent.ProjectileType<TheMagebladeSwing>();
            return base.PreDrawExtras();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.type = ModContent.ProjectileType<TheMagebladeSwing>();

            if (Projectile.ai[0] == 1f)
            {
                Texture2D projectileTexture = TextureAssets.Projectile[Projectile.type].Value;
                Vector2 drawPosition = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Vector2 drawOrigin = new Vector2(projectileTexture.Width, projectileTexture.Height) / 2f;
                Color drawColor = Projectile.GetAlpha(lightColor);
                drawColor.A = 127;
                drawColor *= 0.5f;
 
                SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                for (float transparancy = 1f; transparancy >= 0f; transparancy -= 0.125f)
                {
                    float opacity = 1f - transparancy;
                    Vector2 drawAdjustment = Projectile.velocity * transparancy;
                    Main.EntitySpriteDraw(projectileTexture, drawPosition + drawAdjustment, null, drawColor * opacity, Projectile.rotation, drawOrigin, Projectile.scale * 1.15f * MathHelper.Lerp(1.2f, 1.8f, opacity), spriteEffects, 0);
                }
            }

            return base.PreDraw(ref lightColor);
        }
       

        public override bool PreAI() {
			Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
			int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

			player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

			// Reset projectile time left if necessary
			if (Projectile.timeLeft > duration) {
				Projectile.timeLeft = duration;
			}

			Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

			float halfDuration = duration * 0.7f;
			float progress;

			// Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
			if (Projectile.timeLeft < halfDuration) {
				progress = Projectile.timeLeft / halfDuration;
			}
			else {
				progress = (duration - Projectile.timeLeft) / halfDuration;
			}

            
            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
           
            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }
            Lighting.AddLight(Projectile.Center, Color.LightBlue.ToVector3() * 1.7f);
            if (!Main.dedServ)
            {
                // These dusts are added later, for the 'ExampleMod' effect
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, Projectile.velocity.X * 12f, Projectile.velocity.Y * 12f, Alpha: 128, Scale: 1.3f);
                }

                if (Main.rand.NextBool(4))
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.UltraBrightTorch, Alpha: 128, Scale: 1.0f);
                }
            }

            return false; // Don't execute vanilla AI.

		}
       
    }
}
