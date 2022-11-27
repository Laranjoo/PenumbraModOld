using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace PenumbraMod.Content.Items
{
	public class NightBeam : ModProjectile
	{
		public override void SetStaticDefaults()
		{
		 DisplayName.SetDefault("Night Beam"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.

		}

		public override void SetDefaults()
		{
			Projectile.damage = 30;
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = 173;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 4;
			Projectile.timeLeft= 600;
			Projectile.light = 0.50f;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;	
		}
        public override bool PreDrawExtras()
        {
            Projectile.type = ModContent.ProjectileType<NightBeam>();
            return base.PreDrawExtras();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.type = ModContent.ProjectileType<NightBeam>();

            if (Projectile.ai[0] == 1f)
            {
                Texture2D projectileTexture = TextureAssets.Projectile[Projectile.type].Value;
                Vector2 drawPosition = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Vector2 drawOrigin = new Vector2(projectileTexture.Width, projectileTexture.Height) / 2f;
                Color drawColor = Projectile.GetAlpha(lightColor);
                drawColor.A = 127;
                drawColor *= 0.5f;
                int launchTimer = (int)Projectile.ai[1];
                if (launchTimer > 3)
                {
                    launchTimer = 3;
                }

                SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                for (float transparancy = 1f; transparancy >= 0f; transparancy -= 0.125f)
                {
                    float opacity = 1f - transparancy;
                    Vector2 drawAdjustment = Projectile.velocity * -launchTimer * transparancy;
                    Main.EntitySpriteDraw(projectileTexture, drawPosition + drawAdjustment, null, drawColor * opacity, Projectile.rotation, drawOrigin, Projectile.scale * 1.15f * MathHelper.Lerp(0.9f, 1.2f, opacity), spriteEffects, 0);
                }
            }

            return base.PreDraw(ref lightColor);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // If collide with tile, reduce the penetrate.
            // So the projectile can reflect at most 5 times
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

                // If the projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }

                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }

            return false;
        }
        public override void AI()
		{
			int dust2 = Dust.NewDust(Projectile.Center, 5, 5, DustID.CursedTorch, 2f, 5f, 0);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.9f;
            Main.dust[dust2].scale = (float)Main.rand.Next(100, 200) * 0.018f;
            Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 1.2f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 300);
        }
    }
}