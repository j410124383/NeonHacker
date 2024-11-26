using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalOpacityChanger : MonoBehaviour
{
    public float opacity = 0.5f; // the desired opacity value between 0 and 1

    private DecalProjector decalProjector;
    private Material decalMaterial;

    void Start()
    {
        decalProjector = GetComponent<DecalProjector>();
        decalMaterial = decalProjector.material;
    }

    void Update()
    {
        //Color decalColor = decalMaterial.color;
        //decalColor.a = opacity;
        //decalMaterial.color = decalColor;
    }
}