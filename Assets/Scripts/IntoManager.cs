using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntoManager : MonoBehaviour
{
    public GameObject go_step1;
    public GameObject go_step2;

    public Text txt_console;
    public Button bt_confirm;
    public Button bt_start;
    public Button bt_export;
    public Button bt_back;

    public Dropdown dd_scene;
    public InputField if_input;

    private void Start()
    {
        bt_confirm.onClick.AddListener(OnClickConfirm);
        bt_start.onClick.AddListener(OnClickStart);
        bt_back.onClick.AddListener(OnClickBack);
        bt_export.onClick.AddListener(OnClickExport);
        dd_scene.onValueChanged.AddListener(delegate { DropdownValueChanged(dd_scene); });

        for (int i = 0; i < dd_scene.options.Count; i++)
        {
            if (dd_scene.options[i].text.Equals(Goble.moudleName))
                dd_scene.value = i;
        }

        go_step1.SetActive(true);
        go_step2.SetActive(false);

        Shinn.Common.CsvTools.eventExportCallback += ExportEvent;
    }

    private void OnDestroy()
    {
        Shinn.Common.CsvTools.eventExportCallback -= ExportEvent;
    }


    public void SetUserName(string name)
    {
        Goble.userName = name;
    }

    public void OnClickExport()
    {
        string now = System.DateTime.Now.ToString("yyyyMMdd_HHmm");
        Shinn.Common.CsvTools.WriteToCsvStr(Goble.csv_content, "output_" + now);
    }

    private void OnClickConfirm()
    {
        go_step1.SetActive(false);
        go_step2.SetActive(true);

        int num_1 = Random.Range(1, 9);
        int num_2 = Random.Range(0, 9);
        int num_3 = Random.Range(0, 9);

        SetUserName($"User{num_1 * 100 + num_2 * 10 + num_3}");
        if_input.text = Goble.userName;
    }

    private void OnClickStart()
    {
        Application.LoadLevel(1);
    }

    private void OnClickBack()
    {
        go_step1.SetActive(true);
        go_step2.SetActive(false);
    }

    private void DropdownValueChanged(Dropdown dropdown)
    {
        Goble.moudleName = dropdown.options[dropdown.value].text;
    }

    private void ExportEvent()
    {
        if (Shinn.Common.CsvTools.ExportStatus())
        {
            txt_console.text = $"[Console] Export success! \n[Path]{Shinn.Common.CsvTools.GetFullname}";
        }
    }
}
