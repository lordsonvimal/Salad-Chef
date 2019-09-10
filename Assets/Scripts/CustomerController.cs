using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomerController : MonoBehaviour
{
    [SerializeField]List<int> availableTables = new List<int>() { 0, 1, 2, 3, 4, 5 };
    [SerializeField] List<SpriteRenderer> customerSpriteRenderers = new List<SpriteRenderer>();
    [SerializeField] List<Image> customerTimerUI = new List<Image>();
    [SerializeField]List<Table> tables = new List<Table>();
    [SerializeField]List<Ingredient> ingredients = new List<Ingredient>();
    [SerializeField]int maxIngredients;
    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        StartCoroutine(StartSpawningCustomers());
    }

    IEnumerator StartSpawningCustomers()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            SpawnRandomCustomers();
        }
    }

    // Spawn Customers in Random Tables
    void SpawnRandomCustomers()
    {
        int noOfCustomers = Random.Range(1, availableTables.Count);
        for (int i=0; i<noOfCustomers; i++)
        {
            if(availableTables.Count > 0)
            {
                int tableindex = Random.Range(0, availableTables.Count);
                Customer cust = tables[availableTables[tableindex]].customer;
                if (cust)
                {
                    cust.ingredients.Clear();
                    cust.waitTime = 20;
                    cust.originalTime = 20;
                    cust.ingredients.Clear();
                    cust.ingredients = GetRandomIngredients();
                    foreach(Ingredient ing in tables[availableTables[tableindex]].customer.ingredients)
                    {
                        cust.waitTime += ing.pickUpTime;
                        cust.originalTime = tables[availableTables[tableindex]].customer.waitTime;
                    }
                    CreateSpriteRenderer(cust, availableTables[tableindex]);
                    customerTimerUI[availableTables[tableindex]].gameObject.SetActive(true);
                    customerTimerUI[availableTables[tableindex]].fillAmount = 1;
                }
                else
                {
                    //tables[availableTables[tableindex]].customer = new Customer(GetRandomIngredients());
                }
                StartCoroutine(StartTimer(tables[availableTables[tableindex]].customer, availableTables[tableindex]));
                customerSpriteRenderers[availableTables[tableindex]].gameObject.SetActive(true);
                availableTables.RemoveAt(tableindex);
            }
        }
    }

    // Return List of Random Ingredients for each Customer
    List<Ingredient> GetRandomIngredients()
    {
        List<Ingredient> ings = new List<Ingredient>();
        List<Ingredient> tempIngredients = new List<Ingredient>(ingredients);
        int numberOfIngredients = Random.Range(1, maxIngredients+1);
        int index;
        for (int i = 0; i < numberOfIngredients; i++)
        {
            index = Random.Range(0, tempIngredients.Count);
            ings.Add(tempIngredients[index]);
            tempIngredients.RemoveAt(index);
        }
        return ings;
    }

    // Create Ingredient Sprite Renderer
    void CreateSpriteRenderer(Customer customer, int index)
    {
        int transformIndex = 0;
        float startVal = -6f;
        foreach(Ingredient i in customer.ingredients)
        {
            GameObject go = new GameObject("IngredientUI");
            Transform parent = customerSpriteRenderers[index].transform.GetChild(0).transform;
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            go.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            renderer.sortingOrder = 6;
            renderer.sprite = i.sprite;
            go.transform.SetParent(parent, true);
            go.transform.localPosition = new Vector3(startVal+(Mathf.Abs(startVal)*transformIndex),0,0);
            
            customer.ingredientSpriteObject.Add(go);
            ++transformIndex;
        }
    }

    // Controls the Customer Time
    IEnumerator StartTimer(Customer cust, int index)
    {
        while (true)
        {
            customerTimerUI[index].fillAmount = cust.waitTime / cust.originalTime;
            yield return new WaitForSeconds(1);
            cust.waitTime -= 1;
            if(cust.waitTime <= 0)
            {
                customerSpriteRenderers[index].gameObject.SetActive(false);
                availableTables.Add(index);
                customerTimerUI[index].fillAmount = 0;
                customerTimerUI[index].gameObject.SetActive(false);
                DestroyIngredients(index);
                yield break;
            }
        }
    }

    // Destroy ingredients for a particular table
    void DestroyIngredients(int index)
    {
        foreach(Transform t in customerSpriteRenderers[index].transform.GetChild(0).transform)
        {
            Destroy(t.gameObject);
        }
    }

    public void CustomerServerSuccessfully(int index)
    {
        DestroyIngredients(index);
        customerTimerUI[index].gameObject.SetActive(false);
        customerSpriteRenderers[index].gameObject.SetActive(false);
    }

    private void OnPause()
    {
        //StopCoroutine(StartSpawningCustomers());
    }

    public void OnGameOver()
    {
        StopAllCoroutines();
        for(int i=0; i<customerSpriteRenderers.Count;i++)
        {
            DestroyIngredients(i);
            customerTimerUI[i].gameObject.SetActive(false);
            customerSpriteRenderers[i].gameObject.SetActive(false);
        }
        availableTables = new List<int>() { 0, 1, 2, 3, 4, 5 };
    }

    private void OnRetry()
    {
        Restart();
    }
}
