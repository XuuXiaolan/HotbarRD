namespace HotbarRD.Utils;

internal static class HotbarUtils
{
    private static RuntimeAnimatorController? _defaultHUDRuntimeAnimatorController;
    internal static bool TrySetSlotFrames(CustomFrames frameType, int frameVariant)
    {
        if (HUDManager.Instance is null)
            return false;

        if (HUDManager.Instance.itemSlotIconFrames.Length < 1)
            return false;

        if(_defaultHUDRuntimeAnimatorController is null)
        {
            var animator = HUDManager.Instance.itemSlotIconFrames[0].gameObject.GetComponent<Animator>() ?? throw new NullReferenceException("The animator was not found and then cannot be processed!");
            _defaultHUDRuntimeAnimatorController = animator!.runtimeAnimatorController;
            Plugin.logger.LogDebug($"Default HUD Rntime Animator Controller was null. Filled it with {animator.runtimeAnimatorController.name}");
        }

        if (frameType == CustomFrames.Default)
        {
            foreach(var frame in HUDManager.Instance.itemSlotIconFrames)
            {
                frame.overrideSprite = frame.sprite;
                frame.material = frame.defaultMaterial;

                var animator = frame.gameObject.GetComponent<Animator>() ?? throw new NullReferenceException("The animator was not found and then cannot be processed!");

                var prevname = animator.runtimeAnimatorController.name;
                animator.runtimeAnimatorController = _defaultHUDRuntimeAnimatorController;
                Plugin.logger.LogDebug($"Swapped Runtime Animator Controller {prevname} with {_defaultHUDRuntimeAnimatorController.name} (back to default)");
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

            var animator = frame.gameObject.GetComponent<Animator>() ?? throw new NullReferenceException("The animator was not found and then cannot be processed!");

            if(!AssetsManager.Singleton.TryGetAsset<AnimatorOverrideController>("SlotsAnimatorOverride", out var res))
            {
                Plugin.logger.LogError("The slot animator override was not found. Consider making a report for this error.");
                continue;
            }
            animator.runtimeAnimatorController = res;
            Plugin.logger.LogDebug($"Swapped Runtime Animator Controller {_defaultHUDRuntimeAnimatorController.name} with {res.name}.");
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
