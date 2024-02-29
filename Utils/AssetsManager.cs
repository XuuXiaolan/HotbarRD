namespace HotbarRD.Utils;

// This class can be overhauled to include any type of file, but mostly because it's not necessary,
// I'm making it only compatible with PNGs. JPG and BMP compatibility can be made very easily.
internal class AssetsManager
{
    internal static AssetsManager? Singleton { get; private set; }
    private readonly Dictionary<string, Texture2D> Assets;

    internal AssetsManager()
    {
        Singleton = this;
        Assets = [];
        LoadAllAssets();
    }

    internal void LoadAllAssets()
    {
        var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        foreach (var name in names)
        {
            if (!name.EndsWith(".png"))
                continue;
            var res = new Texture2D(2, 2);
            res.LoadImage(Assembly.GetExecutingAssembly().GetManifestResourceStream(name).ReadAllBytes());
            var resname = name.Split('.')[^2];
            Assets.Add(resname, res);
            Plugin.logger.LogDebug($"Loaded resource {resname} ({name}) {res.width}x{res.height}");
        }
    }

    internal bool TryGetAsset(string name, out Texture2D res)
    {
        return Assets.TryGetValue(name, out res);
    }

    internal Texture2D[] SearchAssets(string searchArgument)
    {
        Plugin.logger.LogDebug($"Searching resource {searchArgument}");
        var regex = new Regex($"{searchArgument}", RegexOptions.IgnoreCase);
        List<Texture2D> Results = [];
        foreach (var asset in Assets)
        {
            if (!regex.IsMatch(asset.Key))
                continue;

            Results.Add(asset.Value);
        }
        return Results.ToArray();
    }

    internal Texture2D[] SearchAssets(CustomFrames frameType)
    {
        return SearchAssets(frameType.ToString());
    }
}
