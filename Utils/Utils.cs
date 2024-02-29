namespace HotbarRD.Utils;

internal static class Utils
{
    internal static bool TrySetSlotFrames(CustomFrames frameType, int frameVariant)
    {
        if (HUDManager.Instance is null)
            return false;

        if (HUDManager.Instance.itemSlotIconFrames.Length < 1)
            return false;

        var redesignedFrames = AssetsManager.Singleton.SearchAssets(frameType);

        if (redesignedFrames.Length < 1)
            return false;

        var selectedFrame = redesignedFrames[frameVariant] ?? redesignedFrames[0];

        foreach (var frame in HUDManager.Instance.itemSlotIconFrames)
        {
            frame.sprite = Sprite.Create(selectedFrame, frame.sprite.rect, frame.sprite.pivot);
        }

        return true;
    }

    internal static byte[] ReadAllBytes(this Stream stream)
    {
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length - 1);
        return bytes;
    }
}
