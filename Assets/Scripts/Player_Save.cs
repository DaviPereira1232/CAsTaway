    using System.Collections.Generic;
    using UnityEngine;

    public class Player_Save : MonoBehaviour
    {
        public static Player_Save Instance;

    public List<FishInfo> FishCaught = new List<FishInfo>();


    private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

    public void AddFish(FishInfo peixe)
    {
        FishCaught.Add(peixe);
    }
}