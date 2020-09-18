﻿using OriMod.Abilities;
using Terraria.ID;

namespace OriMod.Items.Abilities {
  /// <summary>
  /// Medallion that grants <see cref="Dash"/> Level 3: <see cref="ChargeDash"/>.
  /// </summary>
  /// <remarks>
  /// Although <see cref="ChargeDash"/> is a different ability, its unlock condition is that <see cref="Dash"/> is at least Level 3.
  /// </remarks>
  public class DashLevel3 : AbilityMedallionBase {
    public override byte ID => AbilityID.Dash;
    public override byte Level => 3;

    public override void AddRecipes() {
      var recipe = GetAbilityRecipe<DashLevel2>();
      recipe.AddIngredient(ItemID.ShroomiteBar, 16);
      recipe.AddIngredient(ItemID.SoulofLight, 35);
      recipe.AddIngredient(ItemID.Tabi);
      recipe.AddRecipe();
    }
  }
}