namespace HotbarRD.Utils;

internal static class HotbarUtils
{
    internal static bool TrySetSlotFrames(CustomFrames frameType, int frameVariant)
    {
        if (HUDManager.Instance is null)
            return false;

        if (HUDManager.Instance.itemSlotIconFrames.Length < 1)
            return false;

        if (frameType == CustomFrames.Default)
        {
            foreach(var frame in HUDManager.Instance.itemSlotIconFrames)
            {
                frame.overrideSprite = frame.sprite;
                frame.material = frame.defaultMaterial;
            }

            return true;
        }

        var redesignedFrames = AssetsManager.Singleton.SearchAssets(frameType);

        Plugin.logger.LogDebug($"Redesigned frames: {redesignedFrames.Length} variants.");

        if (redesignedFrames.Length < 1)
            return false;

        var selectedFrame = redesignedFrames[Math.Clamp(frameVariant, 0, redesignedFrames.Length-1)];

        foreach (var frame in HUDManager.Instance.itemSlotIconFrames)
        {
            Plugin.logger.LogDebug($"Updating slot: {frame.name} with {selectedFrame.width}x{selectedFrame.height} frame. (Col:{frame.color})");
            var sprite = Sprite.Create(selectedFrame, new Rect(0, 0, selectedFrame.width, selectedFrame.height), Vector2.zero);
            frame.overrideSprite = sprite;
            frame.color = new Vector4(1, 1, 1, 1);
        }

        return true;
    }

    internal static byte[] ReadAllBytes(this Stream stream)
    {
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, (int)stream.Length);
        return bytes;
    }
}
