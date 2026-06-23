using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame(int day, float sanity, float fuel, float hunger)
    {
        PlayerPrefs.SetInt("Day", day);
        PlayerPrefs.SetFloat("Sanity", sanity);
        PlayerPrefs.SetFloat("Fuel", fuel);
        PlayerPrefs.SetFloat("Hunger", hunger);

        PlayerPrefs.Save();
    }

    public static int LoadDay()
    {
        return PlayerPrefs.GetInt("Day", 1);
    }

    public static float LoadSanity()
    {
        return PlayerPrefs.GetFloat("Sanity", 2f);
    }

    public static float LoadFuel()
    {
        return PlayerPrefs.GetFloat("Fuel", 10f);
    }

    public static float LoadHunger()
    {
        return PlayerPrefs.GetFloat("Hunger", 2f);
    }
}