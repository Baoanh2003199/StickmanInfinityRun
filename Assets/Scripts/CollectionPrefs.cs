using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectionPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void SetInts(string key, ICollection<int> collection)
    {
        int count = PlayerPrefs.GetInt(key + ".Count", 0);
        for(int i = 0; i < count;i++)
        {
            PlayerPrefs.DeleteKey(key + "[" + i + "]");
        }

        PlayerPrefs.SetInt(key + ".Count", collection.Count);
        for(int i = 0; i < collection.Count;i++)
        {
            PlayerPrefs.SetInt(key + "[" + i + "]", collection.ElementAt(i));
        }
    }

    public static ICollection<int>GetInts(string key)
    {
        int count = PlayerPrefs.GetInt(key + ".Count", 0);
        int[] array = new int[count];
        for(int i = 0; i < count; i ++)
        {
            array[i] = PlayerPrefs.GetInt(key + "[" + i + "]");
        }
        return array;
    }
     


    // Update is called once per frame
    void Update()
    {
        
    }
}
