namespace HotbarRD.Utils;

internal class AssetsManager
{
    internal static AssetsManager? Singleton { get; private set; }
    private readonly Dictionary<string, UnityEngine.Object> Assets;
    private AssetBundle assetBundle;

    internal AssetsManager()
    {
        Singleton = this;
        Assets = [];
        LoadAllAssets();
    }

    internal void LoadAllAssets()
    {
        var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        var bundleName = names.FirstOrDefault((n) => n.EndsWith("hotbarrd.assets"));
        var bundleStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bundleName);

        assetBundle = AssetBundle.LoadFromStream(bundleStream);
        bundleStream.Close();

        var assets = assetBundle.LoadAllAssets();

        foreach (var asset in assets)
        {
            var resname = asset.GetType().Name + "." + asset.name;
            Assets.TryAdd(resname, asset);
            Plugin.logger.LogDebug($"Loaded and cached asset '{resname}' ({asset.name}) of type {asset.GetType().FullName}.");
        }
    }

    internal bool TryGetAsset<T>(string name, out T result) where T : UnityEngine.Object
    {
        var wasAssetFound = Assets.TryGetValue($"{typeof(T).Name}.{name}", out var asset);
        result = (T)asset;
        return wasAssetFound;
    }

    internal UnityEngine.Object[] SearchAssets(string searchArgument)
    {
        Plugin.logger.LogDebug($"Searching resource {searchArgument}");
        var regex = new Regex($"{searchArgument}", RegexOptions.IgnoreCase);
        List<UnityEngine.Object> Results = [];
        foreach (var asset in Assets)
        {
            if (!regex.IsMatch(asset.Key))
                continue;

            Results.Add(asset.Value);
        }
        Plugin.logger.LogDebug($"Resources found: {Results.Count}/{Assets.Count}");
        return Results.ToArray();
    }

    internal T[] SearchAssets<T>(string searchArgument) where T : UnityEngine.Object
    {
        var preResults = SearchAssets($"{searchArgument}").Where((o) => o.GetType() == typeof(T)).ToArray();
        List<T> reslist = [];
        foreach(var asset in preResults)
        {
            reslist.Add((T)asset);
        }
        var results = reslist.ToArray();
        Plugin.logger.LogDebug($"Resources filtered: {results.Length}/{Assets.Count}");
        if (results.Length < 1 || results is null)
            return [];
        return results;
    }

    internal Texture2D[] SearchAssets(CustomFrames frameType)
    {
        return SearchAssets<Texture2D>(frameType.ToString());
    }
}
