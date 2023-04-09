using UnityEngine;
using Kingdox.UniFlux;
public sealed class Sample_3 : MonoFlux
{
    [SerializeField] private int _life;
    public int Life
    {
        [Flux("Get_Life")] get => _life;
        [Flux("Set_Life")] set 
        {
            _life = value;
            "OnChange_Life".Dispatch(value);
        }
    }
    private void Start() 
    {
        "Set_Life".Dispatch(10);
    }
    private void Update()
    {
        (Time.frameCount % 60).Dispatch();
    }
    [Flux(0)] private void OnUpdate() 
    {
        if("Get_Life".Dispatch<int>() > 0)
        {
            "Set_Life".Dispatch("Get_Life".Dispatch<int>()-1);
        }
    }
    [Flux("OnChange_Life")] private void OnChange_Life(int life) 
    {
        if(life == 0)
        {
            "OnDeath".Dispatch();
        }   
    }
    [Flux("OnDeath")] private void OnDeath()
    {
        Debug.Log("You're Dead !");
    }
}