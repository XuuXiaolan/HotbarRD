namespace HotbarRD.Patches;

[HarmonyPatch(typeof(HUDManager))]
internal class HUDManager_Patches
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(HUDManager), "Start")]
    private static void Start(HUDManager __instance)
    {
        Utils.Utils.TrySetSlotFrames(PluginConfig.SelectedFrameType.Value, PluginConfig.SelectedFrameVariant.Value);
    }
}
