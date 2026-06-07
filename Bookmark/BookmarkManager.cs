using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Markup;

public static class BookmarkManager
{
    private static string GetPath() => Path.Combine(AppContext.BaseDirectory, "bookmarks.json");
    private static Dictionary<string, string> GetBookmarks()
    {
        Dictionary<string, string>? dict = new Dictionary<string, string>();
        string path = GetPath();
        if(!File.Exists(path)) return dict;
        string json = File.ReadAllText(GetPath());
        dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        if(dict == null) return new Dictionary<string, string>();
        return dict;
    }

    private static void WriteToBookmarks(Dictionary<string, string> bookmarks)
    {
        var json = JsonSerializer.Serialize(bookmarks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(GetPath(), json);
    }

    public static string Get(string key)
    {
        Dictionary<string, string> bookmarks = GetBookmarks();
        if(!bookmarks.ContainsKey(key)) return "";
        return bookmarks[key];
    }

    public static void Add(string key, string value)
    {
        Dictionary<string, string> bookmarks = GetBookmarks();
        if(bookmarks.ContainsKey(key)) bookmarks[key] = value;
        else bookmarks.Add(key, value);
        WriteToBookmarks(bookmarks);
    }

    public static Dictionary<string,string> List()
    {
        return GetBookmarks();
    }

    public static void Remove(string key)
    {
        Dictionary<string, string> bookmarks = GetBookmarks();
        bookmarks.Remove(key);
        WriteToBookmarks(bookmarks);
    }
}