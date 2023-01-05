namespace RunicScrolls
{
    using InstanceIDs;
    using SideLoader;
    using HarmonyLib;
    using BepInEx;
    using UnityEngine;
    using System.Linq;
    using System.Collections.Generic;

    [BepInDependency("com.sinai.SideLoader", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(GUID, NAME, VERSION)]
    public class RunicScrolls : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.runicscrolls";
        public const string VERSION = "4.0.1";
        public const string NAME = "Runic Scrolls";

        public static Sprite scrollSprite;
        public static Sprite arrowSprite;

        public Item runicScrollInstance;
        public Item runicScrollRecipeInstance;
        public const string sideloaderFolder = "RunicScrolls";

        internal void Awake()
        {
            SL.OnPacksLoaded += OnPackLoaded;
            SL.OnSceneLoaded += OnSceneLoaded;

            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        private void OnSceneLoaded()
        {
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.name == "UNPC_LaineAberforthA" || x.name == "HumanSNPC_CounterAlchemist"))
            {
                if (obj.GetComponentsInChildren<GuaranteedDrop>()?.FirstOrDefault(table => table.ItemGenatorName == "Recipes") is GuaranteedDrop recipes)
                {
                    if (At.GetField<GuaranteedDrop>(recipes, "m_itemDrops") is List<BasicItemDrop> drops)
                    {
                        foreach (Item item in new Item[] { runicScrollRecipeInstance })
                        {
                            //Used to say DroppedItem = item
                            drops.Add(new BasicItemDrop() { ItemRef= item, MaxDropCount = 1, MinDropCount = 1 });
                        }
                    }
                }
            }
        }

        private void OnPackLoaded()
        {
            runicScrollInstance = RunicScroll.MakeItem();
            runicScrollRecipeInstance = RunicScroll.MakeRecipes();
            scrollSprite = CustomTextures.CreateSprite(SL.GetSLPack("RunicScrolls").Texture2D["quiverDisplayRunicScroll"]);
            arrowSprite = CustomTextures.CreateSprite(SL.GetSLPack("RunicScrolls").Texture2D["tex_men_arrowQuiverIndicator"]);
        }

        public static bool IsRune(Item item)
        {
            return item.ItemID == IDs.dezID || item.ItemID == IDs.shimID || item.ItemID == IDs.egothID || item.ItemID == IDs.falID;
        }
    }
}