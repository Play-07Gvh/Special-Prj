using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum warnDirection
{
    Front = 0,
    Right,
    Back,
    Left,
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private TMP_Text distanceText;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private Transform Body;
    [SerializeField] private Transform Head;

    [SerializeField] private float subTextDuration = 2;
    private float sbtxtDur = 0;

    // For the warning symbols
    [SerializeField] private RawImage img_nwarn;
    [SerializeField] private RawImage img_ewarn;
    [SerializeField] private RawImage img_swarn;
    [SerializeField] private RawImage img_wwarn;

    [SerializeField] private GameObject interactTxt;

    [SerializeField] private GameObject headCrosshair;
    [SerializeField] private GameObject bodyCrosshair;

    [SerializeField] private GameObject[] watcherWarningDisplay = new GameObject[2];
    [SerializeField] private TMP_Text headHPTxt;

    private void Start()
    {
        if (!healthText) Debug.LogError("No health text!");
        if (!subtitleText) Debug.LogError("No subtitle text!");
        if (!distanceText) Debug.LogError("No distance text!");
        if (!winPanel) Debug.LogError("No win panel!");
        if (!losePanel) Debug.LogError("No lose panel!");

        if (!Body) Debug.LogError("No body!");
        if (!Head) Debug.LogError("No head!");

        if (!headCrosshair) Debug.LogWarning("Head Crosshair missing");
        if (!bodyCrosshair) Debug.LogWarning("Body Crosshair missing");
    }

    public void HideOrShowInteract(bool HoS)
    {
        interactTxt.SetActive(HoS);   
    }

    public void HideShowCrosshair(bool BoH, bool isShow)
    {
        if (!BoH)
            bodyCrosshair.SetActive(isShow);
        else
            headCrosshair.SetActive(isShow);
    }

    public void UpdateHealthText(int hp, bool BoH)
    {
        if (!BoH)
            healthText.text = "HP: " + hp;
        else
            headHPTxt.text = "HP: " + hp;
    }

    public void SetSubtitleText(string text)
    {
        if (subtitleText.text == "" && text == "")
            return;
        if (subtitleText.text == text)
        {
            subtitleText.text = text + " again.";
        }
        else
            subtitleText.text = text;
        sbtxtDur = subTextDuration;
    }

    public void SetProximityDistance(int distance)
    {
        if (distance <= 1)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
        distanceText.text = "Distance from Head: " + distance;
    }

    public void lose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    // Show warning to body that there is a threat somewhere in the general cardinal direction
    public void warningDisplay(warnDirection dir, float opacity)
    {
        switch(dir)
        {
            case warnDirection.Front:
                //img_nwarn.SetActive(true);
                //if (opacity > img_nwarn.GetComponent<RawImage>().canvasRenderer.GetAlpha())
                if (opacity > img_nwarn.canvasRenderer.GetAlpha())
                    img_nwarn.GetComponent<RawImage>().canvasRenderer.SetAlpha(opacity);
                break;
            case warnDirection.Right:
                //img_ewarn.SetActive(true);
                //if (opacity > img_ewarn.GetComponent<RawImage>().canvasRenderer.GetAlpha())
                if (opacity > img_ewarn.canvasRenderer.GetAlpha())
                    img_ewarn.GetComponent<RawImage>().canvasRenderer.SetAlpha(opacity);
                break;
            case warnDirection.Back:
                //img_swarn.SetActive(true);
                //if (opacity > img_swarn.GetComponent<RawImage>().canvasRenderer.GetAlpha())
                if (opacity > img_swarn.canvasRenderer.GetAlpha())
                    img_swarn.GetComponent<RawImage>().canvasRenderer.SetAlpha(opacity);
                break;
            case warnDirection.Left:
                //img_wwarn.SetActive(true);
                //if (opacity > img_wwarn.GetComponent<RawImage>().canvasRenderer.GetAlpha())
                if (opacity > img_wwarn.canvasRenderer.GetAlpha())
                    img_wwarn.GetComponent<RawImage>().canvasRenderer.SetAlpha(opacity);
                break;
        }
    }


    public void WatcherWarningDisplay(int warningState)
    {
       switch(warningState)
        {
            case -1: // when sleeping
                watcherWarningDisplay[0].SetActive(false);
                watcherWarningDisplay[1].SetActive(false);
                break;
            case 0: // when waking up
                watcherWarningDisplay[0].SetActive(true);
                break;
            case 1: // when awake
                watcherWarningDisplay[0].SetActive(false);
                watcherWarningDisplay[1].SetActive(true);
                break;
        }
    }

    public void warningRemove()
    {
        img_nwarn.canvasRenderer.SetAlpha(0);
        img_ewarn.canvasRenderer.SetAlpha(0);
        img_swarn.canvasRenderer.SetAlpha(0);
        img_wwarn.canvasRenderer.SetAlpha(0);
    }

    private void FixedUpdate()
    {
        SetProximityDistance(((int)Vector3.Distance(Body.position, Head.position)));
        if (sbtxtDur > 0)
            sbtxtDur -= Time.deltaTime;
        else
            SetSubtitleText("");
    }
}
