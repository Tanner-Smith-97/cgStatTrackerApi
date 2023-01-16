namespace StatTracker.Services;

public class ImageUriCachingService
{
    private readonly IDictionary<string, Uri> _cache = new Dictionary<string, Uri>();

    public Uri? Get(string key)
    {
        return _cache.ContainsKey(key)
            ? _cache.Single(x => x.Key == key).Value
            : null;
    }

    public void Set(string key, Uri value)
    {
        _cache.Add(new KeyValuePair<string, Uri>(key, value));
    }
}