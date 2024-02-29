using System;
using System.Collections.Generic;
using System.Text;

namespace HotbarRD;

public class PluginConfig
{

    public static ConfigEntry<CustomFrames>   SelectedFrameType;
    public static ConfigEntry<int>            SelectedFrameVariant;

    public PluginConfig(ConfigFile cfg)
    {
        cfg.SettingChanged += OnChanged;

        SelectedFrameType = cfg.Bind(
            "Hotbar Redesign",
            "Hotbar design",
            CustomFrames.Default,
            "Choose among the hotbar redesigns."
        );

        SelectedFrameVariant = cfg.Bind(
            "Hotbar Redesign",
            "Hotbar design variant",
            1,
            "Select among the variants of the selected hotbar design. If the variant is not found, the first existing will be used instead."
        );
    }

    internal static void OnChanged(object sender, SettingChangedEventArgs args)
    {
        
    }
}
