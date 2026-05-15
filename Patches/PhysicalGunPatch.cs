using System;
using HarmonyLib;
using LowAmmoClicks.Components;

namespace LowAmmoClicks.Patches;

[HarmonyPatch(typeof(PhysicalGun))]
public class PhysicalGunPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(PhysicalGun.Shoot))]
    private static void PlayClicks(PhysicalGun __instance, PlayerMain playerShooter)
    {
        var myPlayer = PlayersController.instance.MyPlayer();
        if (myPlayer == null || myPlayer != playerShooter) return;

        var inventory = playerShooter.inventory;
        if (inventory == null) return;

        var index = Array.IndexOf(inventory.spawnedEquipment, __instance.prop);
        if (index == -1) return;

        var item = inventory.equippedItem[index];
        if (item == null || __instance.DbReference == null) return;

        var maxAmmo = __instance.DbReference.maxAmmo;
        if (maxAmmo <= 0) return;

        var ammoPercent = (float)item.ammo / maxAmmo;
        if (ammoPercent <= 0.3f)
        {
            AudioLoader.PlayClick();
        }
    }
}
