namespace HotbarRD.Patches;

[HarmonyPatch(typeof(HUDManager))]
internal class HUDManager_Patches
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(HUDManager), "Start")]
    private void Start(HUDManager __instance)
    {
        foreach(var itemSlotFrame in __instance.itemSlotIconFrames)
        {
            itemSlotFrame.sprite = Sprite.Create(new());
        }
    }
}
