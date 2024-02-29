using System;
using System.Collections.Generic;
using System.Text;

namespace HotbarRD;

internal static class HUDUtils
{
    internal static bool TrySetSlotFrames(CustomFrames frameType, int frameVariant)
    {
        if (HUDManager.Instance is null)
            return false;

        if (HUDManager.Instance.itemSlotIconFrames.Length < 1)
            return false;

        foreach(var frame in HUDManager.Instance.itemSlotIconFrames)
        {
            frame.sprite = Sprite.Create()
        }

        return true;
    }
}
