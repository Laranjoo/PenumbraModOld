using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PenumbraMod.Content.Items
{
	public class FinnBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Finn Blade"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("[c/cbecf8:'This blade has a long history...']"
				+ "\n[c/76b6cb:''A long time ago this sword was used by a hero, Called ''Finn''] "
                + "\n[c/76b6cb:He sacrified his alternate reality by transforming himself into this legendary blade...'']"
				+ "\n[c/6198aa:Take care of it, its still watching you...]");
			
		}

		public override void SetDefaults()
		{
			Item.damage = 76;
			Item.DamageType = DamageClass.Melee;
			Item.width = 82;
			Item.height = 82;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 1000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			
		}

	
	}
}