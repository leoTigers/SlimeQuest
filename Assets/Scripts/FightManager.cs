using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;



public class FightManager : MonoBehaviour
{

    // Start is called before the first frame update
    static public List<Entity> enemies;
    static public List<GameObject> enemiesObjects;
    static public Entity player;
    public GameObject contentComponent;

    private List<Entity> turnList;
    private int turn;
    static public bool playerInMenu, playerTurnEnd;
    void Start()
    {
        enemies = new List<Entity>();
        enemiesObjects = new List<GameObject>();
        turnList = new List<Entity>();
        player =    new Entity("Slime",      69, 25, 10, 1000, 10, 10, 10, 10, 1, "");
        enemies.Add(new Entity("Plant",      20, 10, 10, 10, 10, 10, 10, 10, 1, "Sprites/flower_enemy_v1"));
        enemies.Add(new Entity("Bird",       20, 10, 10, 10, 10, 10, 10, 10, 1, "Sprites/fire_bird_enemy_v1"));
        enemies.Add(new Entity("Lion",       20, 10, 10, 10, 10, 10, 10, 10, 1, "Sprites/lion_enemy"));
        enemies.Add(new Entity("Meduse",     20, 10, 10, 10, 10, 10, 10, 10, 1, "Sprites/meduse_enemy"));
        enemies.Add(new Entity("Siren",      20, 10, 10, 10, 10, 10, 10, 10, 1, "Sprites/siren_enemy"));
        enemies.Add(new Entity("I AM GROOT", 20, 10, 10, 10, 10, 10, 10, 10, 1, "Sprites/treant_enemy_v1"));

        turnList.Add(player);
        foreach (Entity Fe in enemies)
        {
            if (Fe.spriteLocation != null)
            {
                GameObject go = new GameObject(Fe.name);

                Image renderer = go.AddComponent<Image>();
                Sprite sprite = Resources.Load<Sprite>(Fe.spriteLocation);
                renderer.sprite = sprite;
                go.transform.SetParent(contentComponent.transform);
                go.transform.localScale /= 25;
                enemiesObjects.Add(go);
            }
            turnList.Add(Fe);
        }
        turn = 0;
        playerInMenu = false;
        playerTurnEnd = false;
        StartCoroutine(ManageTurn());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    IEnumerator ManageTurn()
    {
        while (true)
        {
            if (turn != 0)
            {
                // IA playing
                yield return EnemyTurn();
            }
            else
            {
                // player
                yield return PlayerTurn();
            }
            turn = (turn + 1) % turnList.Count;
        }
    }

    IEnumerator EnemyTurn()
    {
        if (enemies[turn - 1].hp != 0)
        {
            //Debug.Log("Turn: " + turn);
            Attack(enemies[turn - 1]);
            yield return new WaitForSecondsRealtime(1);
        }
    }

    IEnumerator PlayerTurn()
    {
        playerInMenu = true;
        FindObjectOfType<VerticalNavigationMenuBehavior>().SetActive(true);
        yield return new WaitWhile(() => playerInMenu);

    }

    private void Attack(Entity attacker)
    {
        // player targeted
        Entity target = player;
        float armorDamageMult = (float)(target.physicalDef < 0 ?
            2 - 100.0 / (100.0 - target.physicalDef) :
            100.0 / (100.0 + target.physicalDef));
        int damage = (int)((float)attacker.physicalAtt * armorDamageMult);
        damage = damage==0 ? 1 : damage;
        bool dead = target.TakeDamage(damage);
        if (dead)
        {
            // print fail screen
            Debug.Log("DEAD");
        }
    }

    public void Attack(Entity attacker, int targetId)
    {
        Entity target = enemies[targetId];
        float armorDamageMult = (float)(target.physicalDef < 0 ?
            2 - 100.0 / (100.0 - target.physicalDef):
            100.0 / (100.0 + target.physicalDef));
        int damage = (int)((float)attacker.physicalAtt * armorDamageMult);
        damage = damage == 0 ? 1 : damage;
        bool dead = target.TakeDamage(damage);
        if (dead)
        {
            enemiesObjects[targetId].GetComponent<Image>().enabled = false;

            int enemiesAlive = 0;
            foreach (Entity e in enemies)
                if (e.hp > 0)
                    enemiesAlive++;
            if (enemiesAlive == 0)
            {
                // end fight anim
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Fight"));
            }
        }
    }
}
