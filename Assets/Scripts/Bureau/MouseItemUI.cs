using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using ScriptableObjects;

public class MouseItemUI : MonoBehaviour
{
    public Image image;
    public ScriptableImageMap Images;
    public Slider Progress;

    public void Update()
    {
        var mousePos = Input.mousePosition;

        transform.position = mousePos;
    }

    public void SetMouse(string id, bool isSliderActive)
    {
        image.sprite = Images.Get(id);
        Progress.gameObject.SetActive(isSliderActive);
    }

    public void SetSlider(float value)
    {
        Progress.value = value;
    }

    private void OnDisable()
    {
        transform.position = new Vector3(5000, 5000);
    }
}