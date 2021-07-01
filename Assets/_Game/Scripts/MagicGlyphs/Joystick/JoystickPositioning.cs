using UnityEngine;
using TouchScript;



public class JoystickPositioning : MonoBehaviour
{
    private GameObject joystick;
    private GameObject container;

    [SerializeField] private bool turnOffJoystickWhenReleaseTouch;

    private Vector2 point;

    private void Start()
    {
        joystick = GameObject.FindWithTag("Joystick");
        container = joystick.transform.GetChild(0).gameObject;
       // container?.SetActive(false);
    }

    private void OnEnable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.PointersPressed += ScreenTouch;
            TouchManager.Instance.PointersReleased += ScreenRelease;
        }
    }

    private void OnDisable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.PointersPressed -= ScreenTouch;
            TouchManager.Instance.PointersReleased -= ScreenRelease;
        }
    }

    void ScreenTouch(object sender, PointerEventArgs e)
    {
        
        //container?.SetActive(true);
        container.transform.position = e.Pointers[0].Position;

    }

    void ScreenRelease(object sender, PointerEventArgs e)
    {
        if(turnOffJoystickWhenReleaseTouch)
            container?.SetActive(false);
    }
}