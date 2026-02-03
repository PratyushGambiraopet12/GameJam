using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialType tutorialType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        TutorialManager.Instance.ShowTutorial(tutorialType);
        gameObject.SetActive(false); // trigger only once
    }
}
