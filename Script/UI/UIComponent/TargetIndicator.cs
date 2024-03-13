using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TargetIndicator : GameUnit
{
    [SerializeField] Image iconLevel;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI nameText;

    [SerializeField] Image iconHealth;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI fullHealth;

    [SerializeField] RectTransform rect;
    [SerializeField] RectTransform direct;
    [SerializeField] CanvasGroup canvasGroup;

    Transform target;
    Vector3 viewPoint;
    Vector2 viewPointX = new Vector2(0.025f, 0.975f);
    Vector2 viewPointY = new Vector2(0.02f, 0.95f);

    Vector2 viewPointInCameraX = new Vector2(0f, 1f);
    Vector2 viewPointInCameraY = new Vector2(0f, 1f);

    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;

    Camera Camera => CameraFollow.Ins.Camera;
    private bool IsInCamera => viewPoint.x > viewPointInCameraX.x && viewPoint.x < viewPointInCameraX.y && viewPoint.y > viewPointInCameraY.x && viewPoint.y < viewPointInCameraY.y;

    private void Update()
    {
        viewPoint = Camera.WorldToViewportPoint(target.position);
        direct.gameObject.SetActive(!IsInCamera);
        nameText.gameObject.SetActive(IsInCamera);
        iconHealth.gameObject.SetActive(IsInCamera);
        health.gameObject.SetActive(IsInCamera);
        fullHealth.gameObject.SetActive(IsInCamera);
        iconLevel.gameObject.SetActive(IsInCamera);
        viewPoint.x = Mathf.Clamp(viewPoint.x, viewPointX.x, viewPointX.y);
        viewPoint.y = Mathf.Clamp(viewPoint.y, viewPointY.x, viewPointY.y);

        Vector3 targetSPoint = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
        Vector3 playerSPoint = Camera.WorldToScreenPoint(LevelManager.Ins.player.TF.position) - screenHalf;

        rect.anchoredPosition = targetSPoint / (Screen.width / 1080f);

        direct.up = (targetSPoint - playerSPoint).normalized;
        SetAlpha(GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.GamePause) ? 1 : 0);
    }

    public void OnInit()
    {
        Color color = new Color(Random.value, Random.value, Random.value, 1);
        iconLevel.color = color;
        nameText.color = color;
        iconHealth.color = color;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        OnInit();
    }

    public void SetLevel(int level)
    {
        levelText.SetText(level.ToString());
    }

    public void SetName(string name)
    {
        nameText.SetText(name);
    }

    public void SetHealth(float current, float full)
    {
        this.health.SetText(current.ToString());
        this.fullHealth.SetText(full.ToString());
    }
    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
