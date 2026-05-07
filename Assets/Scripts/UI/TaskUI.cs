using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static DeliveryCounter;

public class TaskUI : MonoBehaviour
{
    [SerializeField] private Transform TaskTemplate;
    [SerializeField] private Transform ContainerTransform;

    [SerializeField] private float card_space = 200f;   
    [SerializeField] private float move_speed = 10f;  

    private List<(_Recipe order, Transform obj)> TaskList;

    private void Start()
    {
        DeliveryCounter.Instance.OnOrder += SetTask;
        DeliveryCounter.Instance.OnOrderConfirm += RemoveTask;

        TaskTemplate.gameObject.SetActive(false);
        TaskList = new List<(_Recipe, Transform)>();
    }

    private void Update() 
    {
        for (int i = 0; i < TaskList.Count; i++)
        {
            RectTransform rt = TaskList[i].obj as RectTransform;

            Vector2 target_pos = new Vector2(0, -i * card_space);

            rt.anchoredPosition = Vector2.Lerp(
                rt.anchoredPosition,
                 target_pos,
                Time.deltaTime * move_speed
            );
        }
    }


    private void SetTask(object sender, OrderData e)
    {
        Transform new_task = Instantiate(TaskTemplate, ContainerTransform);
        RectTransform card_rt = new_task.GetComponent<RectTransform>();


        //calculate the starting position based on the current number of tasks
        int index = TaskList.Count;
        float startY = index == 0 ? card_space : -(index - 1) * card_space;
        card_rt.anchoredPosition = new Vector2(0, startY);
        /**/


        foreach (IconUI child in new_task.GetComponentsInChildren<IconUI>(true))
        {
            foreach (_BaseItem item in e.current_order.ItemList)
            {
                Transform icon_obj = Instantiate(child.transform, child.transform.parent);
                icon_obj.GetComponent<Image>().sprite = item.Icon;
            }
            if (child.gameObject.activeSelf)
                child.gameObject.SetActive(false);
        }
        new_task.GetComponentInChildren<TextMeshProUGUI>().text = Regex.Replace(e.current_order.Category.ToString(), "(\\B[A-Z])", " $1");
        new_task.gameObject.SetActive(true);
        TaskList.Add((e.current_order, new_task));
    }

    private void RemoveTask(object sender, OrderData e)
    {
        for (int i = 0; i < TaskList.Count; i++)
        {
            if(TaskList[i].order.Category  == e.current_order.Category)
            {
                StartCoroutine(AnimateTaskCard(i));
                return;
            }

        }
    }

    private IEnumerator AnimateTaskCard(int index)
    {
        Transform obj = TaskList[index].obj;


        float initial = 0f;
        float duration = 0.2f;

        Vector3 start_scale = obj.localScale;

        while (initial < duration)
        {
            initial += Time.deltaTime;
            obj.localScale = Vector3.Lerp(start_scale, Vector3.zero, initial / duration);
            yield return null;
        }

        TaskList.RemoveAt(index);
        Destroy(obj.gameObject);
    }
}