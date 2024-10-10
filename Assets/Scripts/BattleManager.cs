using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private PlayerInputUI playerInput;

    public PlayerUnit player;
    public EnemyUnit enemy;
    public UnitHealthBarUI playerHealthBar;
    public UnitHealthBarUI enemyHealthBar;
    public Button restartButton; // Добавим кнопку перезапуска

    private bool playerTurn;
    private ClientManager clientManager;
    private int turnCounter; // Новый общий счетчик ходов

    private void Start()
    {
        turnCounter = 0;
        playerTurn = true;
        clientManager = new ClientManager();
        clientManager.ConnectToServer();
        StartBattle();

        // Назначаем методы кнопкам
        playerInput.attackButton.onClick.AddListener(() => PlayerUseAbility(0));
        playerInput.fireballButton.onClick.AddListener(() => PlayerUseAbility(1));
        playerInput.purifyButton.onClick.AddListener(() => PlayerUseAbility(2));
        playerInput.regenerationButton.onClick.AddListener(() => PlayerUseAbility(3));
        playerInput.barrierButton.onClick.AddListener(() => PlayerUseAbility(4));
        restartButton.onClick.AddListener(RestartBattle); // Привязываем метод к кнопке перезапуска
    }

    private void StartBattle()
    {
        turnCounter = 0;
        player.Reset();
        enemy.Reset();
        UpdateUI();
    }

    private void Update()
    {
        if (!playerTurn)
        {
            int randomAbility = UnityEngine.Random.Range(0, enemy.abilities.Length);
            enemy.UseAbility(randomAbility, player);
            EndTurn();
        }
    }

    public void PlayerUseAbility(int abilityIndex)
    {
        if (playerTurn)
        {
            Debug.Log("Player is using ability: " + abilityIndex);
            if (player.abilities[abilityIndex] is IStatusEffectAbility)
                clientManager.SendAbilityChoice(abilityIndex, (player.abilities[abilityIndex] as IStatusEffectAbility).StatusEffectIconColor);
            else
                clientManager.SendAbilityChoice(abilityIndex, Color.clear);
            player.UseAbility(abilityIndex, enemy);
            EndTurn();
        }
    }

    public async void EndTurn()
    {
        playerTurn = !playerTurn;

        if (playerTurn)
        {
            // Завершаем ход обоих юнитов только после того, как оба сходили
            turnCounter++;
            player.EndTurn();
            enemy.EndTurn();
            UpdateUI();

            if (player.currentHealth <= 0 || enemy.currentHealth <= 0)
            {
                RestartBattle();
                return;
            }

            string serverResponse = await clientManager.ReceiveServerResponseAsync();
            Debug.Log("Ответ сервера: " + serverResponse);
            ProcessServerResponse(serverResponse);
        }
    }

    private void UpdateUI()
    {
        playerHealthBar.UpdateUI(player.currentHealth);
        enemyHealthBar.UpdateUI(enemy.currentHealth);
        playerInput.UpdateAbilityButtonsUI(player);
        playerHealthBar.UpdateStatusEffects(player.ActiveStatusEffects);
        enemyHealthBar.UpdateStatusEffects(enemy.ActiveStatusEffects);
    }

    private void ProcessServerResponse(string response)
    {
        string[] data = response.Split(',');

        if (data[0] == "UPDATE_HEALTH")
        {
            Unit target = (data[1] == "PLAYER") ? player : enemy;
            int newHealth = int.Parse(data[2]);
            target.currentHealth = newHealth;
            Debug.Log($"{target.name} здоровье обновлено до {newHealth}");
        }

        if (data[0] == "UPDATE_EFFECT")
        {
            Unit target = (data[1] == "PLAYER") ? player : enemy;
            StatusEffectType effectType = (StatusEffectType)Enum.Parse(typeof(StatusEffectType), data[2]);
            int duration = int.Parse(data[3]);
            int power = int.Parse(data[4]);
            Color effectColor = clientManager.Client.ConvertStringToColor(data[5]); // Получение цвета эффекта

            target.AddStatusEffect(new StatusEffect(effectColor, effectType, duration, power));
            Debug.Log($"{target.name} получил статусный эффект {effectType} на {duration} ходов с цветом {effectColor}");
        }

        UpdateUI();
    }

    public void RestartBattle()
    {
        StartBattle();
    }
}