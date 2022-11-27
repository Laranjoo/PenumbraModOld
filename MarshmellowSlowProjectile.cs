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
	public class MarshmellowSlowProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Heavy Marshmellow"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Main.projFrames[Projectile.type] = 4;
        }

		public override void SetDefaults()
		{
			Projectile.damage = 10;
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft= 600;
			Projectile.light = 0.50f;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.scale = 1.4f;
		}
        public override void AI()
        {
            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }


            int dust2 = Dust.NewDust(Projectile.Center, 0, 4, DustID.WhiteTorch, 0f, 0f, 0);
            Main.dust[dust2].noGravity = false;
            Main.dust[dust2].velocity *= 0.4f;
            Main.dust[dust2].scale = (float)Main.rand.Next(50, 70) * 0.014f;

        }
        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Slow, 300);
        }
    }
}