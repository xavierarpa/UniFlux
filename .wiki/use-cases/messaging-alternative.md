---
layout:
  title:
    visible: true
  description:
    visible: false
  tableOfContents:
    visible: true
  outline:
    visible: false
  pagination:
    visible: true
---

# Messaging Alternative

A common case in event handling is usually the use of UnityEvents or Actions (in case you use them by System)

Here is an example introduction:

```csharp
using System;
using UnityEngine;
public class Element : MonoBehaviour
{
    public Action<Element> OnInteract;
    private void Start()
    {
        Interact();
    }
    private void Interact()
    {
        OnInteract?.Invoke(this);
    }
}
public class Manager : MonoBehaviour
{
    [SerializeField] private Element[] elements;
    private void OnEnable()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].OnInteract += OnInteract;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].OnInteract -= OnInteract;
        }
    }
    private void OnInteract(Element element)
    {
        // Something Happens
    }
}
```

In the case that the communication that we want to implement can be treated as global, an event like this can be created directly:

```csharp
using System;
using UnityEngine;
using UniFlux;
public class Element : MonoBehaviour
{
    private void Interact()
    {
        "OnInteract".Dispatch(this);
    }
}
public class Manager : MonoFlux
{
    [MethodFlux("OnInteract")] private void OnInteract(Element element)
    {
        // Something Happens
    }
}
```

In this example we avoid having direct dependencies and we also do not have to maintain variables with the list of elements
