using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Building : MonoBehaviour
{
    public float                HP = 200f;
    public Transform            building_NavTargetPoint;
    public List<VisualEffect>   vfx_smoke_Lsit;
    public void Setting()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "NavTarget")
                building_NavTargetPoint = child;
        }
    }

    public void GetDamage(float _damage)
    {
        HP -= _damage;
        foreach(VisualEffect vfx in vfx_smoke_Lsit)
        {
            vfx.gameObject.SetActive(true);
            vfx.Play();
        }
        if(HP <= 0)
        {
            Destroy();
        }
    }
    public void Destroy()
    {
        print("건물이 부셔졌습니다 ! ");
        CityControlData.Instance.safety_Rating -= 1f;
        foreach (VisualEffect vfx in vfx_smoke_Lsit)
        {
            vfx.gameObject.SetActive(false);
            vfx.Stop();
        }
        LeanPool.Despawn(this.gameObject);
        // 건물 부셔지는거 구현 하기
    }
}
