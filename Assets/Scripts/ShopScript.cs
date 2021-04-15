using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public VendingMachine weapons;
    Text text;
    Text[] textObj;
    GameObject[] weapon;
    GunScript weaponScript;
    public bool shop;
    GameObject[] buyButton;
    GameObject[] ownedButton;
    GameObject[] coinImage;
    int textTimer = 200;

    public float yPos;

    // Start is called before the first frame update
    void Start()
    {
        shop = false;
        yPos = 361.67f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject canvas = GameObject.Find("PauseMenu");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript playerScript = player.GetComponent<PlayerScript>();

        if (transform.parent && !shop && player)
        {
            weapons = transform.parent.GetComponent<VendingMachine>();

            weapon = new GameObject[weapons.weapons.Length];
            textObj = new Text[weapons.weapons.Length];
            buyButton = new GameObject[weapons.weapons.Length];
            ownedButton = new GameObject[weapons.weapons.Length];
            coinImage = new GameObject[weapons.weapons.Length];

            for (int i = 0; i < weapons.weapons.Length; i++)
            {
                Sprite weaponSprite = weapons.weapons[i].GetComponent<SpriteRenderer>().sprite;

                float xDist = (weaponSprite.bounds.size.x - (weaponSprite.border.x + weaponSprite.border.z) / weaponSprite.pixelsPerUnit);

                weapon[i] = Instantiate(weapons.weapons[i], transform.position + new Vector3(-2.5f + xDist, 2.8f - i * 1.8f, 0), Quaternion.identity);

                bool buyable = true;

                for (int j = 0; j < playerScript.weaponInventory.Count; j++)
                {
                    if (playerScript.weaponInventory[j] == weapons.weapons[i])
                    {
                        buyable = false;
                    }
                }

                if (buyable)
                {
                    buyButton[i] = Instantiate(weapons.buyButton, transform.position + new Vector3(1.5f, 2.6f - i * 1.8f, 0), Quaternion.identity);
                }
                else
                {
                    buyButton[i] = Instantiate(weapons.ownedButton, transform.position + new Vector3(2.5f, 2.6f - i * 1.8f, 0), Quaternion.identity);
                }

                textObj[i] = Instantiate(weapons.text, transform.position + new Vector3(596, yPos - i * 80, 0), Quaternion.identity);
                coinImage[i] = Instantiate(weapons.coinImage, transform.position + new Vector3(-1.4f, 2.13f - i * 1.79f, 0), Quaternion.identity);
                textObj[i].transform.parent = canvas.transform;
                textObj[i].text = weapons.prices[i].ToString();
                weapon[i].GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                weapon[i].GetComponent<SpriteRenderer>().sortingOrder = 100;
                buyButton[i].GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                buyButton[i].GetComponent<SpriteRenderer>().sortingOrder = 100;
                weaponScript = weapon[i].GetComponent<GunScript>();

                weaponScript.enabled = false;
            }

            shop = true;
        }

        for (int i = 0; i < weapons.weapons.Length; i++)
        {
            //textObj[i].transform.position = transform.position + new Vector3(590, yPos - i * 80, 0);
            if (buyButton[i])
            {

                if (buyButton[i].GetComponent<ClickEvent>().clicked)
                {
                    int deny = 0;
                    if (player)
                    {
                        /*if (playerScript.money < weapons.prices[i])///////////////////////////////////////////////////////////ADD
                        {
                            deny = 1  
                        }*/

                        for (int j = 0; j < playerScript.weaponInventory.Count; j++)
                        {
                            if (playerScript.weaponInventory[j] == weapons.weapons[i])
                            {
                                deny = 2;
                            }
                        }

                        if (deny == 0)
                        {
                            if (text)
                                Destroy(text);

                            playerScript.weaponInventory.Add(weapons.weapons[i]);
                            text = Instantiate(weapons.text, transform.position + new Vector3(600, 460, 0), Quaternion.identity);
                            text.transform.parent = canvas.transform;
                            text.text = "Purchased!";
                            //playerScript.money -= weapons.prices[i];///////////////////////////////////////////////////////////ADD
                        }
                        else if (deny == 1)
                        {
                            if (text)
                                Destroy(text);

                            text = Instantiate(weapons.text, transform.position + new Vector3(550, 460, 0), Quaternion.identity);
                            text.transform.parent = canvas.transform;
                            text.text = "Not Enough Money!";
                        }
                        else if (deny == 2)
                        {
                            if (text)
                                Destroy(text);

                            text = Instantiate(weapons.text, transform.position + new Vector3(580, 460, 0), Quaternion.identity);
                            text.transform.parent = canvas.transform;
                            text.text = "Already Owned!";
                        }
                    }
                    buyButton[i].GetComponent<ClickEvent>().clicked = false;
                }
            }
        }

        if (shop && Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < weapons.weapons.Length; i++)
            {
                Destroy(textObj[i].gameObject);
                Destroy(weapon[i]);
                Destroy(buyButton[i]);
                //if (ownedButton[i])
                    //Destroy(ownedButton[i]);
                Destroy(coinImage[i]);
            }

            if (text)
                Destroy(text);

            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (text)
        {
            textTimer--;

            if (textTimer <= 0)
            {
                Destroy(text);
                textTimer = 200;
            }
        }
        else
        {
            textTimer = 200;
        }
    }
}
