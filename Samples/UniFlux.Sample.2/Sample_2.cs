using UnityEngine;
using Kingdox.UniFlux;
public sealed class Sample_2 : MonoFlux
{
    protected override void OnFlux(in bool condition)
    {
        "Sample_2".Store(Method, condition);
    }
    private void Start() 
    {
        "Sample_2".Dispatch();
    }
    private void Method() 
    {
        Debug.Log("Sample_2 !");
    }
}