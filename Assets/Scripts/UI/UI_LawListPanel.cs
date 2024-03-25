using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LawListPanel : MonoBehaviour
{
    [Header("City Laws Information")]
    public List<UI_Slot> slot_List;
    // ----------- CITY Law List ------------
    // -----Bad Law
    public Sprite martial_Law;
    public Sprite travel_Law;
    public Sprite romance_Law;
    public Sprite game_Law;
    public Sprite prohibition_Law; // ±ÝÁÖ·É
    public Sprite cigarette_Law;
    public Sprite pets_Law;
    public Sprite tax_Law;
    // -----Good Law
    public Sprite sportEvent_Law;
    public Sprite freeHair_Law;
    public Sprite gym_Law;   
    public Sprite parade_Law;
    public Sprite mayorsMovie_Law;
    public Sprite hospital_Law;
    // -----Comic Law : approval Rating Not Change
    public Sprite marshmallow_Law;
    public Sprite sleep_Law;
    public Sprite chocolate_Law;
    public Sprite pizza_Law;
    public Sprite goodMorning_Law;
    public Sprite rockScissorsPaper_Law;
    public Sprite cola_Law;
    public Sprite handstand_Law;
    public Sprite brushingTeeth_Law;

    public void LawList_Setting()
    {
       ResetSlotList();
        int currentSlot=0;
        if(UI_Manager.Instance.currentLawIcon_List.Count > 0)
        {
            foreach (var slotImage in UI_Manager.Instance.currentLawIcon_List)
            {
                if (slotImage != null && currentSlot < UI_Manager.Instance.currentLawIcon_List.Count)
                {
                    slot_List[currentSlot].GetComponent<Image>();
                    slot_List[currentSlot].lawImageIcon.sprite      = slotImage;
                    slot_List[currentSlot].gameObject.SetActive(true);
                    currentSlot++;
                }
            }
        }
    }
    public void ResetSlotList()
    {
        foreach(var slot in slot_List)
        {
            slot.gameObject.SetActive(false);
            slot.GetComponent<Image>().sprite = null;
        }
    }
}
