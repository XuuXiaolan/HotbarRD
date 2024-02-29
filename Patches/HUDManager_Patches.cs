namespace HotbarRD.Patches;

[HarmonyPatch(typeof(HUDManager))]
internal class HUDManager_Patches
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(HUDManager), "Start")]
    private void Start(HUDManager __instance)
    {
        HUDUtils.TrySetSlotFrames(PluginConfig.SelectedFrameType.Value, PluginConfig.SelectedFrameVariant.Value);
    }
}
