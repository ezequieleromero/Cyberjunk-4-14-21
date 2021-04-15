using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachine : MonoBehaviour
{
    public GameObject proximityKey;
    GameObject eKey;
    public GameObject shopUI;
    GameObject shop;
    public Text text;
    Text textObj;

    public GameObject coinImage;
    public GameObject buyButton;
    public GameObject ownedButton;
    public GameObject[] weapons;
    public int[] prices;

    GameObject player;
    Renderer eKeyRender;

    bool active;
    bool open;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        open = false;

        eKey = Instantiate(proximityKey, new Vector3(transform.position.x, transform.position.y + 2, 0), Quaternion.identity);
        eKeyRender = eKey.GetComponent<Renderer>();
        eKeyRender.material.color = new Color(eKeyRender.material.color.r, eKeyRender.material.color.g, eKeyRender.material.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        eKey.transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y + 1.7f, 0);

        player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            if(Vector2.Distance(transform.position, player.transform.position) < 4)
            {
                eKeyRender.material.color = new Color(eKeyRender.material.color.r, eKeyRender.material.color.g, eKeyRender.material.color.b, 1);
                active = true;
            }
            else
            {
                eKeyRender.material.color = new Color(eKeyRender.material.color.r, eKeyRender.material.color.g, eKeyRender.material.color.b, 0);
                active = false;
            }
        }

        //Shop UI

        if (active && Input.GetKeyDown(KeyCode.E) && !open)
        {
            open = true;
            shop = Instantiate(shopUI, transform.position + new Vector3(4, 2, 0), Quaternion.identity);
            shop.GetComponent<ShopScript>().transform.parent = this.transform;

            Global.shopActive = true;
            Global.shopObject = shop.gameObject;
        }
        else if ((Input.GetKeyDown(KeyCode.E) && open) || !active)
        {
            open = false;
            //if (shop)
                //Destroy(shop);
            Global.shopActive = false;
        }
    }
}
