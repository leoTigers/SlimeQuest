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
    public GameObject actionBox;
    public Text actionText;
    public GameObject playerHitAnimation;
    public GameObject playerHealAnimation;
    public GameObject fireballAnimation;
    public GameObject slashAnimation;

    private List<Entity> turnList;
    private int turn;
    static public bool playerInMenu, playerTurnEnd;
    void Start()
    {
        enemies = new List<Entity>();
        enemiesObjects = new List<GameObject>();
        turnList = new List<Entity>();
        player = MapSceneManager.player;
        if (player == null)
            player = new Entity("Slime", 100, 20, 20, 20, 20, 20, 10, 10, 1, "");

        int enemyCount = Random.Range(1, 4);
        for(int i = 0; i < enemyCount; i++)
        {
            switch(Random.Range(0, 6))
            {
                case 0:
                    enemies.Add(new Entity("Plant", 20, 10, 5, 5, 5, 5, 10, 10, 1, "Sprites/flower_enemy_v1"));
                    break;
                case 1:
                    enemies.Add(new Entity("Bird", 20, 10, 5, 5, 5, 5, 10, 10, 1, "Sprites/fire_bird_enemy_v1"));
                    break;
                case 2:
                    enemies.Add(new Entity("Lion", 20, 10, 7, 7, 5, 5, 10, 10, 1, "Sprites/lion_enemy"));
                    break;
                case 3:
                    enemies.Add(new Entity("Meduse", 20, 10, 5, 5, 10, 10, 10, 10, 1, "Sprites/meduse_enemy"));
                    break;
                case 4:
                    enemies.Add(new Entity("Siren", 40, 10, 3, 10, 10, 10, 10, 10, 1, "Sprites/siren_enemy"));
                    break;
                case 5:
                    enemies.Add(new Entity("I AM GROOT", 20, 10, 2, 2, 10, 10, 10, 10, 1, "Sprites/treant_enemy_v1"));
                    break;
            }
        }

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
            enemies[turn - 1].isDefending = false;
            yield return Attack(enemies[turn - 1]);
        }
    }

    IEnumerator PlayerTurn()
    {
        player.isDefending = false;
        playerInMenu = true;
        FindObjectOfType<VerticalNavigationMenuBehavior>().SetActive(true);
        yield return new WaitWhile(() => playerInMenu);

    }

    public IEnumerator Warning(string warning)
    {
        actionText.text = warning;
        actionBox.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        actionBox.SetActive(false);
    }

    IEnumerator MakeAction(Entity attacker, string spellName, float waitTime)
    {
        actionText.text = attacker.name + " uses " + spellName;
        actionBox.SetActive(true);
        yield return new WaitForSecondsRealtime(waitTime);
        actionBox.SetActive(false);
    }

    IEnumerator MakeAction(Entity attacker, string spellName)
    {
        yield return MakeAction(attacker, spellName, 0.5f);
    }

    public IEnumerator Heal(Entity user)
    {
        playerHealAnimation.SetActive(true);
        yield return MakeAction(user, "heal", 3);
        playerHealAnimation.SetActive(false);
        yield return new WaitForSecondsRealtime(0.2f);
        user.hp += user.magicalAtt*2;
        if (user.hp > user.hpMax)
            user.hp = user.hpMax;
        user.mp -= 4;
    }

    public IEnumerator MagicAttack(Entity attacker, int targetId)
    {
        attacker.mp -= 5;
        Entity target = enemies[targetId];
        fireballAnimation.transform.position = enemiesObjects[targetId].transform.position;
        fireballAnimation.SetActive(true);
        yield return MakeAction(attacker, "fireball", 3);
        fireballAnimation.SetActive(false);
        yield return new WaitForSecondsRealtime(0.2f);
        float armorDamageMult = (float)(target.magicalDef < 0 ?
            2 - 100.0 / (100.0 - target.magicalDef) :
            100.0 / (100.0 + target.magicalDef));
        float damage = (attacker.magicalAtt * armorDamageMult);
        if (target.isDefending)
            damage /= 2;
        damage = damage < 1 ? 1 : damage;
        bool dead = target.TakeDamage((int)damage);
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
                FindObjectOfType<MapSceneManager>().SetSceneActive(true);
            }
        }
    }

    public IEnumerator Attack(Entity attacker)
    {
        playerHitAnimation.SetActive(true);
        yield return MakeAction(attacker, "slash");
        playerHitAnimation.SetActive(false);
        // player targeted
        Entity target = player;
        float armorDamageMult = target.physicalDef < 0 ?
            2 - 100.0f / (100.0f - target.physicalDef) :
            100.0f / (100.0f + target.physicalDef);
        float damage = (attacker.physicalAtt * armorDamageMult);
        if (target.isDefending)
            damage /= 2;
        damage = damage<1 ? 1 : damage;
        bool dead = target.TakeDamage((int)damage);
        if (dead)
        {
            // print fail screen
            Debug.Log("DEAD");
            SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
        }
    }

    public IEnumerator Attack(Entity attacker, int targetId)
    {
        Entity target = enemies[targetId];
        slashAnimation.transform.position = enemiesObjects[targetId].transform.position;
        slashAnimation.SetActive(true);
        yield return MakeAction(attacker, "slash", 1);
        slashAnimation.SetActive(false);
        yield return new WaitForSecondsRealtime(0.2f);
        float armorDamageMult = (float)(target.physicalDef < 0 ?
            2 - 100.0 / (100.0 - target.physicalDef):
            100.0 / (100.0 + target.physicalDef));
        float damage = (attacker.physicalAtt * armorDamageMult);
        if (target.isDefending)
            damage /= 2;
        damage = damage < 1 ? 1 : damage;
        bool dead = target.TakeDamage((int)damage);
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
                FindObjectOfType<MapSceneManager>().SetSceneActive(true);
            }
        }
    }

    public void Defend()
    {
        player.isDefending = true;
    }

}
