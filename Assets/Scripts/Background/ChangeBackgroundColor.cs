using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger  : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public void ChangeBackgroundColor()
    {
        Color newColor = GetRandomColor();
        _camera.backgroundColor = newColor;
    }

    private Color GetRandomColor()
    {
        Color color;
        do
        {
            color = new Color(Random.value, Random.value, Random.value);
        } while (color == Color.black || color == Color.white);
        return color;
    }
}
