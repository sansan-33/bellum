using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text killText = null;
    [SerializeField] private Health health = null;
    [SerializeField] private GameObject healthBarParent = null;
    [SerializeField] private Image healthBarImage = null;
    [SerializeField] private Image healthBarImageLast = null;
    [SerializeField] private GameObject leaderFrame = null;
    [SerializeField] private TMP_Text currentHealthText = null;
    [SerializeField] private TMP_Text levelText = null;

    public int kills;
    private Quaternion startRotation;
    private float lerpDuration = 5f;

    private void Awake()
    {
        health.ClientOnHealthUpdated += HandleHealthUpdated;
        healthBarParent.SetActive(false);
        startRotation = healthBarParent.transform.rotation;
    }
    void Update()
    {
        healthBarParent.transform.rotation = startRotation;
    }
    private void OnDestroy()
    {
        health.ClientOnHealthUpdated -= HandleHealthUpdated;
    }

    public void EnableLeaderIcon()
    {
        leaderFrame.SetActive(true);
    }

    private void HandleHealthUpdated(int currentHealth, int maxHealth, int lastDamageDeal)
    {
        float timeElapsed = 0f;
        float valueToLerp = 0f;
        int startHealth = currentHealth + lastDamageDeal;
        int endHealth = currentHealth;

        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startHealth, endHealth, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            healthBarImage.fillAmount = valueToLerp / maxHealth;
        }
        healthBarImageLast.fillAmount = (float )currentHealth / maxHealth;
        currentHealthText.text = currentHealth.ToString();
        if (currentHealth < maxHealth) {
            healthBarParent.SetActive(true);
        }
    }
    IEnumerator LerpHealthBar(string userid)
    {
        yield return null;
    }
    public void SetHealthBarColor (Color newColor)
    {
        healthBarImage.color = newColor;
    }
    public void HandleKillText()
    {
        killText.text = kills.ToString();
        kills++;
    }
    public void SetUnitLevel(int level)
    {
        health.SetUnitLevel(level);
        levelText.text = health.GetUnitLevel().ToString();
    }
}
