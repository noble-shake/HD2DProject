using TMPro;
using UnityEngine;

public class TextGazeCamera : MonoBehaviour
{
    private TMP_Text TextName;

    private void Start()
    {
        TextName = GetComponent<TMP_Text>();
    }

    private void LateUpdate()
    {
        TextName.transform.LookAt(Camera.main.transform);
    }
}
