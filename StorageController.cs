﻿using System;
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

        /// <summary>
        /// Represents a class for managing application settings.
        /// </summary>
        public class Settings
        {
            /// <summary>
            /// Dictionary to store expandable settings.
            /// </summary>
            public Dictionary<string, object> ExpandableSettings { get; set; } = new Dictionary<string, object>();

            /// <summary>
            /// Initializes the default settings.
            /// </summary>
            public void InitializeDefaultSettings()
            {
                ExpandableSettings["Theme"] = "Light";
                ExpandableSettings["DateLock"] = false;
            }

            /// <summary>
            /// Gets a setting value by key.
            /// </summary>
            /// <typeparam name="T">Type of the setting value.</typeparam>
            /// <param name="key">Key of the setting.</param>
            /// <returns>The value of the setting.</returns>
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

            /// <summary>
            /// Sets a setting value by key.
            /// </summary>
            /// <typeparam name="T">Type of the setting value.</typeparam>
            /// <param name="key">Key of the setting.</param>
            /// <param name="value">Value to set.</param>
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

        /// <summary>
        /// Represents a class for managing notes.
        /// </summary>
        public class Notes
        {
            public string Name { get; set; }
            public string Data { get; set; }
            public DateTime Date { get; set; }
            public int Id { get; set; }

            /// <summary>
            /// Creates a new note with specified details.
            /// </summary>
            /// <param name="name">Name of the note.</param>
            /// <param name="data">Data of the note.</param>
            /// <param name="date">Optional creation date of the note.</param>
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

            /// <summary>
            /// Loads notes from the JSON file.
            /// </summary>
            /// <returns>List of notes.</returns>
            internal static List<Notes> LoadNotes()
            {
                if (File.Exists(NotesFilePath))
                {
                    var json = File.ReadAllText(NotesFilePath);
                    return JsonConvert.DeserializeObject<List<Notes>>(json) ?? new List<Notes>();
                }
                return new List<Notes>();
            }

            /// <summary>
            /// Saves notes to the JSON file.
            /// </summary>
            /// <param name="notes">List of notes to save.</param>
            private static void SaveNotes(List<Notes> notes)
            {
                var json = JsonConvert.SerializeObject(notes);
                File.WriteAllText(NotesFilePath, json);
            }
        }

        public static List<Notes> AllNotes { get; } = LoadAllNotes();

        /// <summary>
        /// Loads all notes from the JSON file.
        /// </summary>
        /// <returns>List of notes.</returns>
        private static List<Notes> LoadAllNotes()
        {
            return Notes.LoadNotes();
        }

        /// <summary>
        /// Loads application settings from the JSON file.
        /// </summary>
        /// <returns>Settings object.</returns>
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

        /// <summary>
        /// Saves application settings to the JSON file.
        /// </summary>
        /// <param name="settings">Settings object to save.</param>
        public static void SaveSettings(Settings settings)
        {
            var existingSettings = LoadSettings();
            if (!settingsSafetyCheck(existingSettings, settings))
            {
                var json = JsonConvert.SerializeObject(settings);
                File.WriteAllText(SettingsFilePath, json);
            }
        }

        /// <summary>
        /// Checks if all settings are present in the new settings object before saving.
        /// </summary>
        /// <param name="settings1">Existing settings object.</param>
        /// <param name="settings2">New settings object.</param>
        /// <returns>True if all settings are present; otherwise, false.</returns>
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
