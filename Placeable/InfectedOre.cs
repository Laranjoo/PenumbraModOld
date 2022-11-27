using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using PenumbraMod.Content.Items.Placeable;

namespace PenumbraMod.Content.Tiles
{
	public class InfectedOre : ModTile
	{
		public override void SetStaticDefaults() {
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			Main.tileOreFinderPriority[Type] = 410; // Metal Detector value, see https://terraria.gamepedia.com/Metal_Detector
			Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Infected Ore");
			AddMapEntry(new Color(183, 82, 221), name);

			DustType = 84;
			ItemDrop = ModContent.ItemType<PenumbraMod.Content.Items.Placeable.InfectedOre>();
			HitSound = SoundID.Tink;
			MineResist = 4f;
			MinPick = 200;
		}
	}

	public class InfectedOreSystem : ModSystem
	{
		public override void ModifyHardmodeTasks(List<GenPass> tasks) {
			
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

			if (ShiniesIndex != -1) {
				
				tasks.Insert(ShiniesIndex + 1, new InfectedOrePass("Penumbra Mod Ores", 237.4298f));
			}
          
			Main.NewText("Looks like something appeared in the evil lands...", 116, 195, 225);
			
			for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 8E-04); k++) {
				
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);

				int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);

				Tile tile = Framing.GetTileSafely(x, y);

				if (tile.HasTile && tile.TileType == TileID.Ebonstone)
				{
		       		WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(3, 8), ModContent.TileType<InfectedOre>());
				}
				
			}
		}


	   
	}
	public class InfectedOrePass : GenPass
	{
		public InfectedOrePass(string name, float loadWeight) : base(name, loadWeight) {
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
			
			progress.Message = "Infecting more the world...";

			
			
		}
	}
}
