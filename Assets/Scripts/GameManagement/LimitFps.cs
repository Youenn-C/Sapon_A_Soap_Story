using TMPro;
using UnityEngine;

public class LimitFps : MonoBehaviour
{
    [SerializeField] private int _maxFrameRate;
    private float _frameRate;
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GameObject.FindGameObjectWithTag("FrameRate").GetComponent<TMP_Text>();
        
        _text.text = "Frame Rate : ";
        Application.targetFrameRate = _maxFrameRate;
    }

    void Update()
    {
        _frameRate = 1f / Time.deltaTime;
        _text.text = "Frame Rate : " + Mathf.RoundToInt(_frameRate);
    }
}
