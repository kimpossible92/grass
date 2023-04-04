using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DayTimeC : MonoBehaviour
{
    const float SecondsInDay = 86400f;
    public float time;
    [SerializeField] Text TimeDisplay;
    [SerializeField] float TimeScale;
    [SerializeField] float LightTransition = 0.0001f;
    public int hungerUpdaterCounter;
    public int healthUpdaterCounter;
    public int temperatureUpdateCounter;
    public int day;

    private void Start()
    {
        day = 0;
        time = 25200f;
        hungerUpdaterCounter = 0;
        healthUpdaterCounter = 0;
        temperatureUpdateCounter = 0;
        TemperatureC.currentTemperature = 100;
    }
    private float getHours
    {
        get { return time / 3600f; }
    }

    public float GetTime
    {
        get { return time; }
    }


    void Update()
    {
        if (Time.timeScale == 0)
            return;

        //licznik wskaźnika głodu i temperatury
        hungerUpdaterCounter += 1;
        temperatureUpdateCounter += 1;
        //tutaj dostosowac jak szybko maleje wskaznik najedzenia
        if (hungerUpdaterCounter == 250)
        {
            HungerC.currentHunger -= 1;
            hungerUpdaterCounter = 0;
        }
        //gdy wskaźnik najedzenia lub temperatury jest niższy niż 10, zaczyna ubywać zdrowia:
        if(HungerC.currentHunger < 10 || TemperatureC.currentTemperature < 10)
        {
            healthUpdaterCounter += 1;
            //tutaj dostosowac jak szybko maleje wskaznik zdrowia
            if (healthUpdaterCounter == 100)
            {
                HealthC.currentHealth -= 1;
                healthUpdaterCounter = 0;
            }
        }
        

        //Kontrola czasu i wyświetlanie
        time += Time.deltaTime * TimeScale;
        int hours = (int)getHours;
        TimeDisplay.text = hours.ToString("00") + ":00";
        UnityEngine.Rendering.Universal.Light2D light = transform.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        //Światło dzienne od 4 do 20
        if (time > 25200f && time < 72000f)
        {
            light.intensity = 1f;
            TemperatureC.currentTemperature = 100;
        }


        //Rozjaśnia się w godzinach 20 - 4
        if ((time > 72000f && time < 86400f) || ((time > 0f && time < 18000f)))
        {
            if (light.intensity > 0.3f)
            {
                light.intensity -= LightTransition;
            }
            if (temperatureUpdateCounter > 50)
            {
                TemperatureC.currentTemperature -= 1;
                temperatureUpdateCounter = 0;
            }
        }
        
        //Lights up 4 - 7
        if (time > 18000f && time < 25200f)
        {
            if (light.intensity < 1f)
                light.intensity += LightTransition;
            if (temperatureUpdateCounter > 50)
            {
                TemperatureC.currentTemperature -= 1;
                temperatureUpdateCounter = 0;
            }
        }

        //Zmiana dnia na nowy
        if (time > SecondsInDay)
        {
            time = 0;
            day += 1;
            //codzienna dostawa punktow
            MoneyMan.money += 200;
        }
        //Jesli zdrowie spranie do 0 zmiana na scene game over
        if(HealthC.currentHealth < 1)
        {
            Application.LoadLevel(3);
        }
        if(day == 9 && time > 25200f)
        {
            Application.LoadLevel(4);
        }
    }

}
