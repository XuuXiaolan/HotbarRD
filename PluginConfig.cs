using System;
using System.Collections.Generic;
using System.Text;
using HotbarRD.Utils;

namespace HotbarRD;

public class PluginConfig
{

    public static ConfigEntry<CustomFrames>   SelectedFrameType;
    public static ConfigEntry<int>            SelectedFrameVariant;

    public PluginConfig(ConfigFile cfg)
    {
        SelectedFrameType = cfg.Bind(
            "General",
            "Texture pack",
            CustomFrames.Default,
            "Choose among the hotbar texture packs."
        );

        SelectedFrameVariant = cfg.Bind(
            "General",
            "Texture variant",
            0,
            "Select among the variants of the selected hotbar design. If the variant is not found, the first existing will be used instead."
        );


        cfg.SettingChanged += OnChanged;
    }

    internal static void OnChanged(object sender, SettingChangedEventArgs args)
    {
        Plugin.logger.LogDebug($"Plugin config saved - Trying to set slot frames [{SelectedFrameType?.Value}:{SelectedFrameVariant?.Value}]");
        HotbarUtils.TrySetSlotFrames(SelectedFrameType.Value, SelectedFrameVariant.Value);
    }
}
