namespace HotbarRD.Patches;

[HarmonyPatch(typeof(HUDManager))]
internal class HUDManager_Patches
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(HUDManager), "Start")]
    private static void Start(HUDManager __instance)
    {
        Plugin.logger.LogDebug($"HUDManager started - Trying to set slot frames [{PluginConfig.SelectedFrameType?.Value}:{PluginConfig.SelectedFrameVariant?.Value}]");
        HotbarUtils.TrySetSlotFrames(PluginConfig.SelectedFrameType.Value, PluginConfig.SelectedFrameVariant.Value);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(HUDManager), "OnEnable")]
    private static void OnEnable(HUDManager __instance)
    {
        Plugin.logger.LogDebug($"HUDManager enabled - Trying to set slot frames [{PluginConfig.SelectedFrameType?.Value}:{PluginConfig.SelectedFrameVariant?.Value}]");
        HotbarUtils.TrySetSlotFrames(PluginConfig.SelectedFrameType.Value, PluginConfig.SelectedFrameVariant.Value);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(HUDManager), "DisplayNewScrapFound")]
    private static void DisplayNewScrapFound(HUDManager __instance)
    {
        Plugin.logger.LogDebug($"HUDManager display new scrap found - Trying to set slot frames [{PluginConfig.SelectedFrameType?.Value}:{PluginConfig.SelectedFrameVariant?.Value}]");
        HotbarUtils.TrySetSlotFrames(PluginConfig.SelectedFrameType.Value, PluginConfig.SelectedFrameVariant.Value);
    }
}
