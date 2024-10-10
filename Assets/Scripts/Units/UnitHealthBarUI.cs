using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthBarUI : MonoBehaviour
{
    [SerializeField] private GameObject statusEffectIconPrefab;
    [SerializeField] private int activeStatusEffectsCount;
    [SerializeField] private float spaceBetweenIcons;
    public Slider playerHealthBar;
    public TMPro.TextMeshProUGUI healthPointCounter;
    public Transform statusEffectIconsContainer; // Контейнер для иконок эффектов

    public void UpdateUI(float value)
    {
        playerHealthBar.value = value;
        healthPointCounter.text = value.ToString();
    }
    public void UpdateStatusEffects(List<StatusEffect> activeStatusEffects)
    {
        // Очистка старых иконок
        foreach (Transform child in statusEffectIconsContainer)
        {
            Destroy(child.gameObject);
        }
        activeStatusEffectsCount = 0;

        // Добавление новых иконок
        foreach (StatusEffect effect in activeStatusEffects)
        {
            GameObject icon = Instantiate(statusEffectIconPrefab, statusEffectIconPrefab.transform.position, Quaternion.identity, statusEffectIconsContainer);
            Image iconImage = icon.GetComponent<Image>();
            iconImage.color = effect.Color;
            iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 255f);
            // Установить здесь изображение для иконки эффекта

            TMPro.TextMeshProUGUI counterText = icon.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            counterText.text = effect.Duration.ToString();

            RectTransform iconRectTransform = icon.GetComponent<RectTransform>();
            float iconXPositionDiscplacement = ((icon.GetComponent<RectTransform>().rect.width * 2) + spaceBetweenIcons) * activeStatusEffectsCount;
            //Vector3 newIconPosition = new Vector3(iconXPositionDiscplacement, icon.transform.position.y, icon.transform.position.z);
            //icon.transform.position = icon.transform.position + newIconPosition;
            iconRectTransform.anchoredPosition = statusEffectIconPrefab.GetComponent<RectTransform>().anchoredPosition;
            iconRectTransform.anchoredPosition = iconRectTransform.anchoredPosition + new Vector2(iconXPositionDiscplacement, 0);

            activeStatusEffectsCount++;
        }
    }
}
