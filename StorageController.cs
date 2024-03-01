using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace NotABook
{
    internal class StorageController
    {
        private const string SettingsFilePath = "./data/Settings.json";
        private const string NotesFilePath = "./data/Notes.json";

        public class Settings
        {
            public Dictionary<string, object> ExpandableSettings { get; set; } = new Dictionary<string, object>();

            public void InitializeDefaultSettings()
            {
                ExpandableSettings["Theme"] = "Light";
                ExpandableSettings["DateLock"] = false;
            }

            public T GetSetting<T>(string key)
            {
                if (ExpandableSettings.ContainsKey(key))
                {
                    return (T)ExpandableSettings[key];
                }
                else
                {
                    throw new KeyNotFoundException($"Setting with key '{key}' does not exist.");
                }
            }

            public void SetSetting<T>(string key, T value)
            {
                if (ExpandableSettings.ContainsKey(key))
                {
                    ExpandableSettings[key] = value;
                }
                else
                {
                    ExpandableSettings.Add(key, value);
                }
            }
        }

        public class Notes
        {
            public string Name { get; set; }
            public string Data { get; set; }
            public DateTime Date { get; set; }
            public int Id { get; set; }

            public static void Create(string name, string data, DateTime? date = null)
            {
                var notes = LoadNotes();
                var newNote = new Notes
                {
                    Name = name,
                    Data = data,
                    Date = date ?? DateTime.Now,
                    Id = notes.Count > 0 ? notes.Max(n => n.Id) + 1 : 1
                };
                notes.Add(newNote);
                notes = notes.OrderBy(n => n.Date).ToList();
                SaveNotes(notes);
            }

            internal static List<Notes> LoadNotes()
            {
                if (File.Exists(NotesFilePath))
                {
                    var json = File.ReadAllText(NotesFilePath);
                    return JsonConvert.DeserializeObject<List<Notes>>(json) ?? new List<Notes>();
                }
                return new List<Notes>();
            }

            private static void SaveNotes(List<Notes> notes)
            {
                var json = JsonConvert.SerializeObject(notes);
                File.WriteAllText(NotesFilePath, json);
            }
        }

        public static List<Notes> AllNotes { get; } = LoadAllNotes();

        public static Settings LoadSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                var json = File.ReadAllText(SettingsFilePath);
                return JsonConvert.DeserializeObject<Settings>(json);
            }
            var settings = new Settings();
            settings.InitializeDefaultSettings();
            return settings;
        }

        public static void SaveSettings(Settings settings)
        {
            var existingSettings = LoadSettings();
            if (!settingsSafetyCheck(existingSettings, settings))
            {
                var json = JsonConvert.SerializeObject(settings);
                File.WriteAllText(SettingsFilePath, json);
            }
        }

        private static List<Notes> LoadAllNotes()
        {
            return Notes.LoadNotes();
        }

        private static bool settingsSafetyCheck(Settings settings1, Settings settings2)
        {
            foreach (var setting in settings1.ExpandableSettings)
            {
                if (!settings2.ExpandableSettings.ContainsKey(setting.Key))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
