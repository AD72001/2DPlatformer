using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int currentPosition;

    // Audio
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip selectSound;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Choose options with arroww
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);
        
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);
        
        // Select Option
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            SelectOption();
    }

    private void ChangePosition(int _change)
    {
        if (_change == 0) return;

        SoundManager.instance.PlaySound(changeSound);

        currentPosition += _change;

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, rect.position.z);
    }

    private void SelectOption()
    {
        SoundManager.instance.PlaySound(selectSound);

        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
