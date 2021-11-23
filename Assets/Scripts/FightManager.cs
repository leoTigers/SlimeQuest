using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class FightManager : MonoBehaviour
{

    // Start is called before the first frame update
    static public List<FightingEntity> enemies;
    static public FightingEntity player;
    public GameObject contentComponent;
    void Start()
    {
        enemies = new List<FightingEntity>();
        player = new FightingEntity("Slime", 6, 69, 6, 25, Status.CONFUSED);
        enemies.Add(new FightingEntity("Plant", 10, 10, 10, 10, Status.NONE,
            "Assets/Resource/flower_enemy_v1.png"));
        enemies.Add(new FightingEntity("Bird", 10, 10, 10, 10, Status.NONE,
            "Assets/Resource/fire_bird_enemy_v1.png"));
        enemies.Add(new FightingEntity("Lion", 10, 10, 10, 10, Status.NONE,
            "Assets/Resource/lion_enemy.png"));
        enemies.Add(new FightingEntity("Meduse", 10, 10, 10, 10, Status.NONE,
            "Assets/Resource/meduse_enemy.png"));
        enemies.Add(new FightingEntity("Siren", 10, 10, 10, 10, Status.NONE,
            "Assets/Resource/siren_enemy.png"));
        enemies.Add(new FightingEntity("I AM GROOT", 10, 10, 10, 10, Status.NONE,
            "Assets/Resource/treant_enemy_v1.png"));

        foreach (FightingEntity Fe in FightManager.enemies)
        {
            if (Fe.Sp != null)
            {
                GameObject go = new GameObject(Fe.Name);

                Image renderer = go.AddComponent<Image>();
                Texture2D tex = LoadPNG(Fe.Sp);
                Sprite sprite = Sprite.Create(tex, 
                    new Rect(0, 0, tex.width, tex.height), 
                    new Vector2(0.5f, 0.5f));
                renderer.sprite = sprite;
                go.transform.SetParent(contentComponent.transform);
                go.transform.localScale *= 3;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;
        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
