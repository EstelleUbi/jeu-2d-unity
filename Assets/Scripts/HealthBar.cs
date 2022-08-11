using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Responsable entre l'affichage et l'�volution de la healthbar via le slider
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        // La couleur correspond � la couleur situ� � 1 sur le gradient 
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        // La couleur correspond � la valeur du slider (entre 0 et 100) mais normaliz� � l'interval du gradient (0 � 1) 50 => 0,5
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
}
