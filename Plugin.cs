using BepInEx.Logging;

namespace HotbarRD;

[BepInPlugin(PluginInfo.PluginGUID, PluginInfo.PluginName, PluginInfo.PluginVers)]
internal class Plugin : BaseUnityPlugin
{
    private static readonly Harmony harmony = new(PluginInfo.PluginGUID);
    internal static ManualLogSource logger;
    internal static new PluginConfig Config { get; private set; }

    private void Awake()
    {
        logger = Logger;
        Config = new(base.Config);
    }

    private void Start()
    {
        logger.LogInfo("Hotbar Redesign - Arts by Xu Xiaolan / Mod programmed by VELD-Dev.");
    }
}
