using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinSetter : MonoBehaviour
{
    public Material[] defaultSkins;
    public Material[] skins;

    public Material getDefaultSkin(int index){
        return defaultSkins[index];
    }

    public Material getSkin(int index){
        return skins[index];
    }
}
