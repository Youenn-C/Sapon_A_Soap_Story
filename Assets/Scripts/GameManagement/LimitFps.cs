using TMPro;
using UnityEngine;

public class LimitFps : MonoBehaviour
{
    [SerializeField] private int _maxFrameRate;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = _maxFrameRate;
    }
}
