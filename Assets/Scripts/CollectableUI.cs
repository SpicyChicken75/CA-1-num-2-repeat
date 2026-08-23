using UnityEngine;
using TMPro;

public class CollectableUI : MonoBehaviour
{
    public TextMeshProUGUI collectableText;
    public int totalCollectables = 5;
    public GameObject winScreen;

    void Update()
    {
        collectableText.text = "Collectables: "
            + Collectable.collected
            + "/"
            + totalCollectables;

        if (Collectable.collected >= totalCollectables)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}