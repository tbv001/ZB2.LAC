using System;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LowAmmoClicks.Components;

namespace LowAmmoClicks;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class LowAmmoClicks : BaseUnityPlugin
{
    internal new static ManualLogSource Logger;
    public const string PluginGuid = "com.theblackvoid.lowammoclicks";
    public const string PluginName = "Low Ammo Clicks";
    public const string PluginVersion = "1.0.0";
    private readonly Harmony _harmony = new(PluginGuid);

    private void Awake()
    {
        Logger = base.Logger;
        try
        {
            gameObject.AddComponent<AudioLoader>();
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo("Successfully loaded!");
        }
        catch (Exception e)
        {
            Logger.LogError($"Failed to load: {e}");
        }
    }
}
