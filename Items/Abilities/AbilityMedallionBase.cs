using OriMod.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OriMod.Items.Abilities {
  /// <summary>
  /// Base class for items that unlocks or upgrades an <see cref="Ability"/>.
  /// </summary>
  public abstract class AbilityMedallionBase : ModItem {
    /// <summary>
    /// <see cref="AbilityID"/> of the <see cref="Ability"/> to unlock.
    /// </summary>
    public abstract byte ID { get; }

    /// <summary>
    /// Level that the <see cref="Ability"/> with <see cref="ID"/> will be set to when this item is used.
    /// </summary>
    public virtual byte Level => 1;

    public override void SetDefaults() {
      item.useStyle = ItemUseStyleID.HoldingUp;
      item.useTime = 45;
      item.useAnimation = 45;
    }

    /// <summary>
    /// Returns <see langword="true"/> if the player does not have the <see cref="Ability"/> this Item represents upgraded to the <see cref="Ability.Level"/> this Item upgrades to.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> using the item.</param>
    /// <returns></returns>
    public override bool CanUseItem(Player player) {
      // Can only use the item if the ability to be unlocked has not been unlocked
      var oPlayer = player.GetModPlayer<OriPlayer>();
      return oPlayer.abilities[ID].Level < Level;
    }

    /// <summary>
    /// Increases the level of <paramref name="player"/>'s <see cref="Ability"/> this Item represents to by 1.
    /// <para>By increasing by 1, the player can level it multiple times if they skip one, rather than having their level skip.</para>
    /// </summary>
    /// <param name="player">The player using the item.</param>
    /// <returns><see langword="true"/> if the ability can be leveled. If this returns <see langword="false"/>, this <see cref="AbilityMedallionBase"/> or the <see cref="Ability"/> must be fixed.</returns>
    public override bool UseItem(Player player) {
      var oPlayer = player.GetModPlayer<OriPlayer>();
      var ability = oPlayer.abilities[ID];
      if (ability is ILevelable levelable) {
        levelable.Level = Level;
        return true;
      }
      else {
        Main.NewText($"OriMod dev bug: Ability {ability.GetType().Name} cannot be leveled.");
        return false;
      }
    }

    public abstract override void AddRecipes();

    /// <summary>
    /// Gets a <see cref="ModRecipe"/> that uses the ingredient <see cref="AbilityMedallionEmpty"/>, crafting station <see cref="Tiles.SpiritSapling"/>, and sets the result.
    /// <para>These are standard recipes to all <see cref="AbilityMedallionBase"/> types.</para>
    /// </summary>
    /// <returns>A <see cref="ModRecipe"/> set with ingredients and tiles common across all <see cref="AbilityMedallionBase"/> items.</returns>
    protected ModRecipe GetAbilityRecipe() {
      var recipe = new ModRecipe(mod);
      recipe.AddIngredient(ModContent.ItemType<AbilityMedallionEmpty>());
      recipe.AddTile(ModContent.TileType<Tiles.SpiritSapling>());
      recipe.SetResult(this);
      return recipe;
    }
  }
}
