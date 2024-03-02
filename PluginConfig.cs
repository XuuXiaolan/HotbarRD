using System;
using System.Collections.Generic;
using System.Text;
using HotbarRD.Utils;
using LethalConfig;
using LethalConfig.ConfigItems;

namespace HotbarRD;

public class PluginConfig
{

    public static ConfigEntry<CustomFrames>   SelectedFrameType;
    public static ConfigEntry<int>            SelectedFrameVariant;

    private static EnumDropDownConfigItem<CustomFrames> LCFrameSelector { get; set; }
    private static IntSliderConfigItem LCFrameVariantSelector { get; set; }

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
            "Select among the variants of the selected hotbar design.\n\nIf the variant is not found, the first existing variant will be used instead."
        );

        LCFrameSelector = new EnumDropDownConfigItem<CustomFrames>(SelectedFrameType, new EnumDropDownOptions
        {
            RequiresRestart = false
        });

        LCFrameVariantSelector = new IntSliderConfigItem(SelectedFrameVariant, new IntSliderOptions
        {
            Min = 0,
            Max = 2,
            RequiresRestart = false,
        });

        LethalConfigManager.AddConfigItem(LCFrameSelector);
        LethalConfigManager.AddConfigItem(LCFrameVariantSelector);
        LethalConfigManager.SetModDescription("HotbarRD adds new hotbar designs.\n\nArts by Xu Xiaolan\nMod by VELD-Dev");
        if(AssetsManager.Singleton != null)
        {
            var modicon = AssetsManager.Singleton.SearchAssets(SelectedFrameType.Value)[SelectedFrameVariant.Value] ?? AssetsManager.Singleton.SearchAssets(CustomFrames.Holy)[0];
            LethalConfigManager.SetModIcon(Sprite.Create(modicon, new(0, 0, modicon.width, modicon.height), new(0, 0)));
        }

        cfg.SettingChanged += OnChanged;
    }

    internal static void OnChanged(object sender, SettingChangedEventArgs args)
    {
        Plugin.logger.LogDebug($"Plugin config saved - Trying to set slot frames [{SelectedFrameType?.Value}:{SelectedFrameVariant?.Value}]");
        HotbarUtils.TrySetSlotFrames(SelectedFrameType.Value, SelectedFrameVariant.Value);
        var modicon = AssetsManager.Singleton.SearchAssets(SelectedFrameType.Value)[SelectedFrameVariant.Value] ?? AssetsManager.Singleton.SearchAssets(CustomFrames.Holy)[0];
        LethalConfigManager.SetModIcon(Sprite.Create(modicon, new(0, 0, modicon.width, modicon.height), new(0, 0)));
    }
}
