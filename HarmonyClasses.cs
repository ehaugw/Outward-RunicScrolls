using HarmonyLib;
using InstanceIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunicScrolls
{

    [HarmonyPatch(typeof(QuiverDisplay), "Update")]
    public class QuiverDisplay_Update
    {
        [HarmonyPrefix]
        public static void Prefix(QuiverDisplay __instance)
        {
            var image = __instance.gameObject.GetComponentInChildren<UnityEngine.UI.Image>();
            if ((__instance.LocalCharacter.Inventory?.Equipment?.GetEquippedAmmunition()?.ItemID ?? -1) == InstanceIDs.IDs.runicScrollID)
            {
                if (image.sprite != RunicScrolls.scrollSprite)
                {
                    image.sprite = RunicScrolls.scrollSprite;
                }
            } else
            {
                if (image.sprite == RunicScrolls.scrollSprite)
                {
                    image.sprite = RunicScrolls.arrowSprite;
                }
            }
        }
    }

    [HarmonyPatch(typeof(AttackSkill), "OwnerHasAllRequiredItems")]
    public class AttackSkill_OwnerHasAllRequiredItems
    {
        [HarmonyPrefix]
        public static bool Prefix(AttackSkill __instance, ref bool __result)
        {
            if (RunicScrolls.IsRune(__instance))
            {
                for (int j = 0; j < __instance.RequiredTags.Length; j++)
                {
                    var quiverItem = __instance.OwnerCharacter.Inventory.Equipment.GetEquippedItem(EquipmentSlot.EquipmentSlotIDs.Quiver);
                    if (quiverItem != null && quiverItem.HasTag(__instance.RequiredTags[j].Tag))
                    {
                        __result = true;
                        return false;
                    }
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(CharacterEquipment), "HasWeaponTypeEquipped")]
    public class CharacterEquipment_HasWeaponTypeEquipped
    {
        [HarmonyPostfix]
        public static void Postfix(CharacterEquipment __instance, ref bool __result, ref EquipmentSlot.EquipmentSlotIDs _slotID, ref Weapon.WeaponType _weaponType)
        {
            if (__result && _weaponType == Weapon.WeaponType.Arrow)
            {
                Weapon weapon = __instance.GetEquippedItem(_slotID) as Weapon;
                __result = !weapon.HasTag(TagSourceManager.Instance.GetTag(IDs.lexiconTagID.ToString()));
            }
        }
    }

    [HarmonyPatch(typeof(Skill), "ConsumeRequiredItems")]
    public class Skill_ConsumeRequiredItems
    {
        [HarmonyPrefix]
        public static void Prefix(Skill __instance)
        {
            var runeSpell = __instance as AttackSkill;
            if (runeSpell != null && RunicScrolls.IsRune(__instance))
            {
                if (runeSpell.RequiredTags != null && runeSpell.RequiredTags.Length != 0)
                {
                    bool hasLexicon = false;
                    if (runeSpell.OwnerCharacter.Inventory.SkillKnowledge.IsItemLearned(IDs.internalLexiconID))
                    {
                        hasLexicon = true;
                    }
                    for (int j = 0; j < runeSpell.RequiredTags.Length; j++)
                    {
                        if ((runeSpell.OwnerCharacter.CurrentWeapon != null && runeSpell.OwnerCharacter.CurrentWeapon.HasTag(runeSpell.RequiredTags[j].Tag)) ||
                            (runeSpell.OwnerCharacter.LeftHandWeapon != null && runeSpell.OwnerCharacter.LeftHandWeapon.HasTag(runeSpell.RequiredTags[j].Tag)) ||
                            (runeSpell.OwnerCharacter.LeftHandEquipment != null && runeSpell.OwnerCharacter.LeftHandEquipment.HasTag(runeSpell.RequiredTags[j].Tag)))
                        {
                            hasLexicon = true;
                        }
                    }

                    if (!hasLexicon)
                    {
                        var quiverItem = runeSpell.OwnerCharacter.Inventory.Equipment.GetEquippedItem(EquipmentSlot.EquipmentSlotIDs.Quiver);
                        if (quiverItem != null && quiverItem.HasTag(TagSourceManager.Instance.GetTag(IDs.lexiconTagID.ToString())))
                        {
                            quiverItem.RemoveQuantity(1);
                        }
                    }
                }
            }
        }
    }
}