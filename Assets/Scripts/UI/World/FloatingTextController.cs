using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextController : MonoBehaviour {

    private static DamageText damageText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!damageText)
            damageText = Resources.Load<DamageText>("Prefabs/DamageText");
    }

    public static IEnumerator CreateDamageText(Damage damage, Vector2 location, bool isPlayer = false)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.x, location.y ));
        TMP_ColorGradient colorGradient = ScriptableObject.CreateInstance<TMP_ColorGradient>();
        if (damage.MagicalAttack > 0 || damage.PhysicalAttack > 0)
        {
            if (!isPlayer)
            {

                int damageLeader;
                Color damageLeaderColor;
                if (damage.MagicalAttack >= damage.PhysicalAttack)
                {
                    damageLeader = damage.MagicalAttack;
                    damageLeaderColor = Color.blue;
                }
                else
                {
                    damageLeader = damage.PhysicalAttack;
                    damageLeaderColor = Color.red;
                }
            
                colorGradient.topLeft = damageLeaderColor;
                colorGradient.topRight = damageLeaderColor;
                colorGradient.bottomLeft = Color.yellow;
                colorGradient.bottomRight = Color.yellow;

                float leaderPercentage = (damageLeader / (float)damage.GetTotalDamage()) * 100;

                if(leaderPercentage > 70)
                {
                    colorGradient.bottomLeft = damageLeaderColor;
                }
                if (leaderPercentage > 90)
                {
                    colorGradient.bottomRight = damageLeaderColor;
                }
                InstantiateDamageText(screenPosition, damage.GetTotalDamage().ToString(), colorGradient);
            }
            else
            {
                colorGradient.topLeft = Color.magenta;
                colorGradient.topRight= Color.magenta;
                colorGradient.bottomLeft = Color.magenta;
                colorGradient.bottomRight = Color.magenta;
                InstantiateDamageText(screenPosition, damage.GetTotalDamage().ToString(), colorGradient);
            }
        }
        else
        {
            colorGradient.topLeft = Color.gray;
            colorGradient.topRight = Color.gray;
            colorGradient.bottomLeft = Color.gray;
            colorGradient.bottomRight = Color.gray;
            InstantiateDamageText(screenPosition, "MISS", colorGradient);
            yield return null;
        }
    }



    public static DamageText InstantiateDamageText(Vector2 screenPosition, string damage, TMP_ColorGradient color)
    {
        DamageText instance = Instantiate(damageText);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(damage);
        instance.SetColorGradient(color);
        return instance;
    }
}
