using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MenuLoading : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image loadingFill;
    [SerializeField] private TMP_Text loadingText;

    [Header("Animation Settings")]
    [SerializeField] private float fillDuration = 3f;
    [SerializeField] private float waveSpeed = 3f;
    [SerializeField] private float waveHeight = 3f;

    private float fillAmount = 0f;
    private float timer = 0f;
    private bool done = false;

    private void OnEnable()
    {
        fillAmount = 0f;
        timer = 0f;
        done = false;

        loadingFill.fillAmount = 0f;
        loadingText.text = "";
        StartCoroutine(AnimateLoading());
    }

    private IEnumerator AnimateLoading()
    {
        while (fillAmount < 1f)
        {
            // Animation đầy thanh
            fillAmount += Time.deltaTime / fillDuration;
            loadingFill.fillAmount = Mathf.Clamp01(fillAmount);

            // Animation chữ “Loading...”
            AnimateDots();

            yield return null;
        }

        // Khi đầy hoàn toàn
        loadingFill.fillAmount = 1f;
        done = true;
        StartCoroutine(ShowConnected());
    }

    private void AnimateDots()
    {
        timer += Time.deltaTime * waveSpeed;

        // "Loading" + ba chấm có hiệu ứng sóng
        string baseText = "Loading";
        string animated = baseText + " ";

        for (int i = 0; i < 3; i++)
        {
            // tạo hiệu ứng nhảy theo sin
            float offset = Mathf.Sin(timer + i * 0.8f) * waveHeight;
            animated += $"<voffset={offset}px>.</voffset>";
        }

        loadingText.text = animated;
    }

    private IEnumerator ShowConnected()
    {
        loadingText.text = "Connected!";
        yield return new WaitForSeconds(0.5f);

        // Gọi sang Menu chính (ví dụ Menu.cs)
        var menu = FindObjectOfType<Menu>();
        if (menu != null)
        {
            menu.OnConnectedToMaster();
        }
    }

    // Nếu muốn cập nhật tiến trình thật
    public void SetProgress(float progress)
    {
        fillAmount = Mathf.Clamp01(progress);
    }
}