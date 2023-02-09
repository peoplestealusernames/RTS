using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Icons : MonoBehaviour
{
    public Canvas canvas;
    public Transform Unit_Holder;
    public Camera Cam;
    public Transform IconPrefab;

    public Selected_Units Selected;

    private Dictionary<Transform, Transform> UiDict = new Dictionary<Transform, Transform>();

    void Update()
    {
        //TODO: Event for selection changed to update ui
        foreach (Transform UnitTran in Unit_Holder)
        {
            Vector2 pos = Cam.WorldToScreenPoint(UnitTran.position);
            Transform Ui;
            UiDict.TryGetValue(UnitTran, out Ui);
            if (!Ui)
            {
                Ui = Transform.Instantiate(IconPrefab, canvas.transform);
                UiDict[UnitTran] = Ui;
                Button _Button = Ui.GetComponent<Button>();
                _Button.onClick.AddListener(() => { Selected.Set(UnitTran); });
            }

            Ui.position = pos;
        }
    }
}
