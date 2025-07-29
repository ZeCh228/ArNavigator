using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonDelayActivator : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private Button actionButton;
    [SerializeField] private float delay = 5f;
    [SerializeField] private Color inactiveColor = new Color32(126, 126, 126, 255);
    private Color originalColor;

    private Image buttonImage;

    void Start()
    {
        if (actionButton != null)
        {
            buttonImage = actionButton.GetComponent<Image>();
            if (buttonImage != null)
                originalColor = buttonImage.color;

            actionButton.interactable = false;

            if (buttonImage != null)
                buttonImage.color = inactiveColor;

            Invoke(nameof(EnableButton), delay);

            actionButton.onClick.AddListener(LoadNextScene);
        }
    }

    void EnableButton()
    {
        actionButton.interactable = true;

        if (buttonImage != null)
            buttonImage.color = originalColor;
    }

    void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
