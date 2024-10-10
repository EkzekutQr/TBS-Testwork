using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputUI : MonoBehaviour
{
    public Button          attackButton;
    public TextMeshProUGUI attackCooldownText;

    public Button          barrierButton;
    public TextMeshProUGUI barrierCooldownText;

    public Button          regenerationButton;
    public TextMeshProUGUI regenerationCooldownText;

    public Button          fireballButton;
    public TextMeshProUGUI fireballCooldownText;

    public Button          purifyButton;
    public TextMeshProUGUI purifyCooldownText;

    public void UpdateAbilityButtonsUI(PlayerUnit player)
    {
        UpdateButtonUI(attackButton, attackCooldownText, player.abilities[0]);
        UpdateButtonUI(fireballButton, fireballCooldownText, player.abilities[1]);
        UpdateButtonUI(purifyButton, purifyCooldownText, player.abilities[2]);
        UpdateButtonUI(regenerationButton, regenerationCooldownText, player.abilities[3]);
        UpdateButtonUI(barrierButton, barrierCooldownText, player.abilities[4]);
    }

    private void UpdateButtonUI(Button button, TextMeshProUGUI cooldownText, Ability ability)
    {
        if (ability.IsOffCooldown())
        {
            button.interactable = true;
            cooldownText.text = "";
        }
        else
        {
            button.interactable = false;
            cooldownText.text = ability.CurrentCooldown.ToString();
        }
    }
}
