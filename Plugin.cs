namespace HotbarRD;

[BepInPlugin(PluginInfo.PluginGUID, PluginInfo.PluginName, PluginInfo.PluginVers)]
internal class Plugin : BaseUnityPlugin
{
    internal static new PluginConfig Config { get; private set; }

    private static readonly Harmony harmony = new(PluginInfo.PluginGUID);
    internal static ManualLogSource logger;

    private void Awake()
    {
        logger = Logger;
        Config = new(base.Config);

        // I don't need to store it, it has a singleton field.
        new AssetsManager();
    }

    private void Start()
    {
        logger.LogInfo("Hotbar Redesign - Arts by Xu Xiaolan / Mod programmed by VELD-Dev.");
        harmony.PatchAll(typeof(HUDManager_Patches));
        logger.LogDebug("Applied HUDManager patches.");
    }
}
