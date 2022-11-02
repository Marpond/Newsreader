using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using Newtonsoft.Json;

namespace Newsreader;

public static class JsonHandler
{
    private const string PATH = $"../data.json";

    public static List<User>? GetUsers()
    {
        if (!File.Exists(PATH)) return null;
        var json = File.ReadAllText(PATH);
        return json.Split("\n").ToList().Select(JsonConvert.DeserializeObject<User>).ToList();
    }

    public static string GetRememberedUsername()
    {
        try
        {
            return GetUsers()!.First(user => user.Saved).Username;
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }

    public static void RememberUsername(string username)
    {
        var users = GetUsers();
        users!.ForEach(user => user.Saved = user.Username == username);
        File.WriteAllText(PATH,string.Join("\n", users.Select(JsonConvert.SerializeObject)));
    }
    
    public static void ForgetUsername()
    {
        var users = GetUsers();
        users!.ForEach(user => user.Saved = false);
        File.WriteAllText(PATH, string.Join("\n", users.Select(JsonConvert.SerializeObject)));
    }

    public static ObservableCollection<string> GetFavoriteGroups(string username)
    {
        var user = GetUsers()!.First(user => user.Username == username);
        return new ObservableCollection<string>(user.FavoriteGroups);
    }
    
    public static void AddFavoriteGroup(string group, string username)
    {
        var users = GetUsers();
        var user = users!.First(user => user.Username == username);
        if (user.FavoriteGroups.Contains(group)) return;
        user.FavoriteGroups.Add(group);
        File.WriteAllText(PATH, string.Join("\n", users!.Select(JsonConvert.SerializeObject)));
    }
    
    public static void RemoveFavoriteGroup(string group, string username)
    {
        var users = GetUsers();
        var user = users!.First(user => user.Username == username);
        if (!user.FavoriteGroups.Contains(group)) return;
        user.FavoriteGroups.Remove(group);
        File.WriteAllText(PATH, string.Join("\n", users!.Select(JsonConvert.SerializeObject)));
    }
    
    public static void AddNewUser(string username)
    {
        User user = new()
        {
            Username = username,
            Saved = false,
            FavoriteGroups = new()
        };
        var json = JsonConvert.SerializeObject(user);
        File.AppendAllText(PATH,json);
    }

    public class User
    {
        public string Username { get; init; }
        public bool Saved { get; set; }
        public List<string> FavoriteGroups { get; init; }
    }
}