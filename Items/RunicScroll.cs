namespace RunicScrolls
{
    using InstanceIDs;
    using System.Collections.Generic;
    using SideLoader;
    class RunicScroll
    {
        public const string SubfolderName = "RunicScroll";
        public const string ItemName = "Runic Scroll";
        public static Item MakeItem()
        {
            var myitem = new SL_Weapon()
            {
                Name = ItemName,
                Target_ItemID = IDs.arrowID,
                New_ItemID = IDs.runicScrollID,
                Description = "A magic scroll inscribed with runes.\n\nA Runic Scroll can be used to cast a Rune spell without a lexicon, but it will be consumed in the process.",
                EffectBehaviour = EditBehaviours.Override,

                Tags = new string[] { "Lexicon", "Item", "Consummable", "Misc" },

                EquipSlot = EquipmentSlot.EquipmentSlotIDs.Quiver,
                TwoHandType = Equipment.TwoHandedType.None,
                WeaponType = Weapon.WeaponType.Arrow,

                ItemVisuals = new SL_ItemVisual() { Prefab_Name = "scrollquiver_Prefab", Prefab_AssetBundle = "scrollquiver", Prefab_SLPack = RunicScrolls.sideloaderFolder },
                SpecialItemVisuals = new SL_ItemVisual() { Prefab_Name = "scrollquiverequipped_Prefab", Prefab_AssetBundle = "scrollquiver", Prefab_SLPack = RunicScrolls.sideloaderFolder },

                SLPackName = RunicScrolls.sideloaderFolder,
                SubfolderName = SubfolderName
            };
            myitem.ApplyTemplate();
            return ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);

            //var ammo = item.gameObject.GetComponentInChildren<Ammunition>();
            //ammo.SpecialVisualPrefabDefault = null;
        }


        public static Item MakeRecipes()
        {
            string newUID = RunicScrolls.GUID + "." + SubfolderName.ToLower() + "recipe";

            new SL_Recipe()
            {
                StationType = Recipe.CraftingType.Alchemy,
                Results = new List<SL_Recipe.ItemQty>() {
                    new SL_Recipe.ItemQty() {Quantity = 1, ItemID = IDs.runicScrollID},
                },
                Ingredients = new List<SL_Recipe.Ingredient>() {
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.manaStoneID},
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.linenClothID},
                },
                UID = newUID
            }.ApplyTemplate();

            var myitem = new SL_RecipeItem()
            {
                Name = "Alchemy: " + ItemName,
                Target_ItemID = IDs.arbitraryAlchemyRecipeID,
                New_ItemID = IDs.runicScrollRecipeID,
                EffectBehaviour = EditBehaviours.Override,
                RecipeUID = newUID
            };
            myitem.ApplyTemplate();
            return ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);
        }
    }
}
