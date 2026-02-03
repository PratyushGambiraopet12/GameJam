using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI")]
    public GameObject panel;
    public Image tutorialImage;

    [Header("Tutorial Images")]
    public Sprite jumpTutorial;
    public Sprite lightMaskTutorial;
    public Sprite stoneMaskTutorial;
    public Sprite magnetMaskTutorial;

    private bool isOpen;
    public bool IsTutorialOpen => isOpen;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        panel.SetActive(false);
    }

    public void ShowTutorial(TutorialType type)
    {
        panel.SetActive(true);
        isOpen = true;

        switch (type)
        {
            case TutorialType.Jump:
                tutorialImage.sprite = jumpTutorial;
                break;

            case TutorialType.LightMask:
                tutorialImage.sprite = lightMaskTutorial;
                break;

            case TutorialType.StoneMask:
                tutorialImage.sprite = stoneMaskTutorial;
                break;

            case TutorialType.MagnetMask:
                tutorialImage.sprite = magnetMaskTutorial;
                break;
        }
    }

    private void Update()
    {
        if (!isOpen) return;

        // Controller X button OR Keyboard X
        if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.X))
        {
            CloseTutorial();
        }
    }

    public void CloseTutorial()
    {
        isOpen = false;
        panel.SetActive(false);
    }
}
