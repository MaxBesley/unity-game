using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    private RectTransform sliderTranform;
    [SerializeField] GameObject enemy;
    [SerializeField] float enemyHealthOffset;

    private void Start()
    {
        sliderTranform = healthSlider.GetComponent<RectTransform>();
        healthSlider.value = healthSlider.maxValue;

    }

    void Update()
    {
        sliderTranform.position = enemy.transform.position + new Vector3(0,enemyHealthOffset,0); 
    }

    //change bar value based on enemies current health
    public void updateBar()
    {
        this.healthSlider.value = (float)enemy.GetComponent<EnemyStats>().getHealth()/(float)enemy.GetComponent<EnemyStats>().getMaxHealth();
    }
}
