using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelSelector : MonoBehaviour
{
    public SerializableDictionary<string, GameObject> Models;
    public SerializableDictionary<string, Material> Materials;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectModel(string codename)
    {
        bool modelSet = false;
        //foreach (KeyValuePair<string, GameObject> modelEntry in Models)
        //{
        //    if (modelSet == false) { modelSet = (codename == modelEntry.Key); }
        //    modelEntry.Value.SetActive(codename == modelEntry.Key);
        //}
        //if(modelSet == false) { print("¸ðµ¨ ¾øÀ½."); }
        GameObject modelObject = Models[codename];
        if(modelObject)
        {
            modelObject.SetActive(true);
        }
        else
        {
            print("¸ðµ¨ ¾øÀ½.");
        }

        Material material;
        if (Materials.TryGetValue(codename, out material))
        {
            modelObject.GetComponent<SkinnedMeshRenderer>().material = material;
        }
    }
}
