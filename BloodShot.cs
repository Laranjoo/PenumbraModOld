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
	public class BloodShot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
		 DisplayName.SetDefault("Blood Shot"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.

		}

		public override void SetDefaults()
		{
			Projectile.damage = 39;
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 2;
			Projectile.timeLeft= 600;
			Projectile.light = 0.20f;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;	
		}

		public override void AI()
		{
			int dust2 = Dust.NewDust(Projectile.Center, 2, 2, DustID.RedMoss, 0f, 0f, 0, Color.DarkRed, 1f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.6f;
            Main.dust[dust2].scale = (float)Main.rand.Next(100, 135) * 0.012f;

            Projectile.rotation += 360;

            Lighting.AddLight(Projectile.Center, Color.DarkRed.ToVector3() * 0.80f);
        }
        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 300);
        }
    }
}