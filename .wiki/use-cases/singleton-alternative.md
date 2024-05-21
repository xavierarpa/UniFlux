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

# Singleton Alternative

When we work in Unity, a very common case is usually the use of the Singleton pattern like this:

<pre class="language-csharp"><code class="lang-csharp"><strong>using UnityEngine;
</strong>public class UIManager : MonoBehaviour
{
    private static UIManager _instance;   
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealthDisplay()
    {
        // ...
    }
}

public class Player : MonoBehaviour
{
    private int health;
    public void TakeDamage(int damage)
    {
        health -= damage;
        UIManager.Instance.UpdateHealthDisplay(health);
    }
}
</code></pre>

The singleton pattern usually comes with problems in how it is managed, it brings with it problems such as a lot of coupling, little flexibility and high long-term dependencies.

With UniFlux we will try to approach the problem in a different way, instead of programming in a declarative way, what we will do will be in a reactive way:

```csharp
using UnityEngine;
public class UIManager : MonoFlux
{
    [StateFlux("Player.Life")] private void OnChangePlayerLifeState(int state)
    {
        UpdateHealthDisplay(state);
    }
    private void UpdateHealthDisplay(int life)
    {
        // ...
    }
}
public class GameManager : MonoFlux
{
    [StateFlux("Player.Life")] private void OnChangePlayerLifeState(int state)
    {
        CheckIfEndGame(state);
    }
    private void CheckIfEndGame(int life)
    {
        if(life > 0)
        {
            // nothing...
        }
        else
        {
            // Run End of game
        }
    }
}
public class CameraManager : MonoFlux
{
    int last_PlayerLife = default;
    [StateFlux("Player.Life")] private void OnChangePlayerLifeState(int state)
    {
        // if player lose life
        if(last_PlayerLife > state)
        {
            ApplyScrrenShake();
        }
        // update last state
        last_PlayerLife = state;
    }
    private void ApplyScrrenShake()
    {
        // ...
    }
}
public class Player : MonoBehaviour
{
    private int Health
    {
        get => "Player.Life".GetState<int>(out var state) ? state : default;
        set => "Player.Life".DispatchState(value);
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
```

This way you can unify data in one place but even more elements can subscribe to the current state. This allows you to not depend on the use of Singletons and to maintain low coupling in your projects.
