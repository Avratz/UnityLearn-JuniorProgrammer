using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    [SerializeField][Range(10.0f, 100.0f)] private float rotationSpeed = 10.0f;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField][Range(0f, 1.0f)] private float opacity = 0.4f;
    [SerializeField][Range(1f, 8.0f)] private float size = 1.0f;
    
    void Start()
    {
        startColor = GenerateRandomColor();
        endColor = GenerateRandomColor();
    }
    
    void Update()
    {
        transform.localScale = Vector3.one * size;
        transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, 0.0f);

        Material material = Renderer.material;
        startColor.a = opacity;
        endColor.a = opacity;
        material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
    }

    private Color GenerateRandomColor()
    {
        float red = Random.Range(0f, 1.0f);
        float green = Random.Range(0f, 1.0f);
        float blue = Random.Range(0f, 1.0f);
        float opacity = Random.Range(0f, 1.0f);

        return new Color(red, green, blue, opacity);
    }

}
