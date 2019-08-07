using Terraria.ID;
using Terraria.ModLoader;

namespace OriMod.Items {
  public class SpiritSapling2 : ModItem {
    public override bool Autoload(ref string name) => false;
    public override void SetStaticDefaults() {}
    public override string Texture => "OriMod/Items/SpiritSapling"; // TODO: Add SpiritSapling2 sprite
    public override void SetDefaults() {
      item.width = 12;
      item.height = 30;
      item.maxStack = 1;
      item.useTurn = true;
      item.autoReuse = true;
      item.useAnimation = 15;
      item.useTime = 10;
      item.useStyle = 1;
      item.consumable = true;
      item.value = 150000;
      item.createTile = mod.TileType("SpiritSapling2");
    }
    public override void AddRecipes() {
      ModRecipe recipe = new ModRecipe(mod);
      recipe.AddIngredient(mod.ItemType("SpiritSapling"), 1);
      recipe.AddIngredient(ItemID.SoulofLight, 30);
    }
  }
}