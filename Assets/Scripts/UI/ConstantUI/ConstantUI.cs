using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ConstantUI : MonoBehaviour
{
    private GameObject avatar;

    private GameObject rightAvatarPanel;
    private GameObject rightAvatarList;

    private void Start()
    {
        Managers.Canvas.AddModule(this.gameObject);
        CreateLeftPanel();
        CreateAvatar();
        CreateRightAvatarPanel();
    }

    private void CreateLeftPanel()
    {
        var rect = this.gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(1, 1);
        rect.offsetMin = new Vector2(0, 1);
        rect.offsetMax = new Vector2(0, 1);
    }

    private void CreateAvatar()
    {
        avatar = new GameObject("Avatar", typeof(RectTransform), typeof(Image));
        
        var rect = avatar.GetComponent<RectTransform>();
        rect.SetParent(this.gameObject.transform, false);
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(1, 1);
        rect.offsetMin = new Vector2(15, -145);
        rect.offsetMax = new Vector2(115, -15);
    }
    private void CreateRightAvatarPanel()
    {
        rightAvatarPanel = new GameObject("RightAvatarPanel", typeof(RectTransform));

        var avatarRect = avatar.GetComponent<RectTransform>();

        var rect = rightAvatarPanel.GetComponent<RectTransform>();

        rect.SetParent(this.gameObject.transform, false);
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(1, 1);
        rect.offsetMin = new Vector2(avatarRect.offsetMax.x + 15, avatarRect.offsetMax.y + -130);
        rect.offsetMax = new Vector2(avatarRect.offsetMax.x + 65,avatarRect.offsetMax.y);

        CreateRightAvatarList(rect);
    }

    private void CreateRightAvatarList(RectTransform parentPanel)
    {
        rightAvatarList = new GameObject("RightAvatarList", typeof(RectTransform), typeof(GridLayoutGroup));

        var rect = rightAvatarList.GetComponent<RectTransform>();
        rect.SetParent(parentPanel);
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(1, 1);
        rect.offsetMin = new Vector2(0, -130);
        rect.offsetMax = new Vector2(65, 0);

        var layout = rightAvatarList.GetComponent<GridLayoutGroup>();
        layout.cellSize = new Vector2(32, 32);
        layout.spacing = new Vector2(5, 5);
        layout.padding.top = 0;
        layout.padding.left = 0;

        CreateSkillButton(rect);
        CreateInventoryButton(rect);
    }

    private void CreateSkillButton(RectTransform parentRect)
    {
        var skillButton = new GameObject("SkillButton", typeof(RectTransform), typeof(Image),typeof(LayoutElement),typeof(Button));

        var rect = skillButton.GetComponent<RectTransform>();
        rect.SetParent(parentRect);

        var image = skillButton.GetComponent<Image>().sprite = Managers.Resources.DownloadData(IconType.Skill);

        var button = skillButton.GetComponent<Button>();
        //button.image = Managers.Resources.DownloadData(IconType.Skill);
    }

    private void CreateInventoryButton(RectTransform parentRect)
    {
        var inventoryButton = new GameObject("InventoryButton", typeof(RectTransform), typeof(Image), typeof(LayoutElement), typeof(Button));

        var rect = inventoryButton.GetComponent<RectTransform>();
        rect.SetParent(parentRect);

        var image = inventoryButton.GetComponent<Image>().sprite = Managers.Resources.DownloadData(IconType.Inventory);

        var button = inventoryButton.GetComponent<Button>();
        button.onClick.AddListener(OpenInventory);
    }

    private void OpenInventory()
    {
        CanvasUI.Inventory.Enable();
    }
}
