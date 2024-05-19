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
    visible: false
---

# How to use a custom Key?

In this example I will show the most likely use for the custom key, creating your own enumerator and implementing it to the UniFlux system

We create the 'Key' enumerator to use it

```csharp
public enum Key
{
    Event_1,
    ResetApp,
    OnPlayerDead
}
```

We go to the Generator Key tab

<figure><img src="../.gitbook/assets/Captura de pantalla 2024-05-20 a las 1.31.52.png" alt=""><figcaption></figcaption></figure>

We write the name of the type we are going to use, in this case "Key". Note that it is Key Sensitive.

<figure><img src="../.gitbook/assets/Captura de pantalla 2024-05-20 a las 1.32.19.png" alt=""><figcaption></figcaption></figure>

We choose the folder where we left the new Extender code and that's it!

```csharp
Key.Event_1.Dispatch();
Key.ResetApp.Dispatch();
Key.OnPlayerDead.Dispatch();
```

Now you can use the "Key" enum to create new keys!

On a personal note, I usually use strings to reduce dependencies completely, by using a custom key that is not primitive you will generate a dependency, if that does not affect your project then go ahead, _without fear of success_
