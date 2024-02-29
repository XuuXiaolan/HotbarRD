namespace HotbarRD.Utils;

internal static class HUDUtils
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
        var frametexture = selectedFrame.ToTexture();

        foreach (var frame in HUDManager.Instance.itemSlotIconFrames)
        {
            frame.sprite = Sprite.Create(frametexture, frame.sprite.rect, frame.sprite.pivot);
        }

        return true;
    }

    internal static Texture2D ToTexture(this System.Drawing.Image image)
    {
        var imgbmp = new Bitmap(image);
        var target = new Texture2D(image.Width, image.Height);
        for (var y = 0; y < image.Height; y++)
        {
            for (var x = 0; x < image.Width; x++)
            {
                target.SetPixel(x, y, imgbmp.GetPixel(x, y).ToUnityColor());
            }
        }
        target.Apply();
        return target;
    }

    internal static UnityEngine.Color ToUnityColor(this System.Drawing.Color color) => new(color.R, color.G, color.B, color.A);
}
