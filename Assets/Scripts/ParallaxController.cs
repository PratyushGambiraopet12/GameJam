using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] materials;
    float[] backSpeed;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed = 0.02f;

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int count = transform.childCount;
        backgrounds = new GameObject[count];
        materials = new Material[count];
        backSpeed = new float[count];

        for (int i = 0; i < count; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        CalculateSpeed();
    }

    void CalculateSpeed()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float depth = Mathf.Abs(backgrounds[i].transform.position.z - cam.position.z);
            if (depth > farthestBack) farthestBack = depth;
        }

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float depth = Mathf.Abs(backgrounds[i].transform.position.z - cam.position.z);
            backSpeed[i] = 1 - (depth / farthestBack);
        }
    }

    void LateUpdate()
    {
        // How far camera moved horizontally
        distance = cam.position.x - camStartPos.x;

        // ⭐ FIX: Background now follows camera X AND Y
        transform.position = new Vector3(
            cam.position.x,
            cam.position.y,
            transform.position.z
        );

        // Scroll texture only horizontally
        for (int i = 0; i < materials.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            float offsetX = distance * speed;

            materials[i].SetTextureOffset("_MainTex", new Vector2(offsetX, 0));
        }
    }
}
