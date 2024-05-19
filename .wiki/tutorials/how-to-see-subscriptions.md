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

# How To See Subscriptions ?

In order to view the subscriptions of our game we must go to the UniFlux panel and search for "Open Debugger"

<figure><img src="../.gitbook/assets/Captura de pantalla 2024-05-20 a las 1.41.23.png" alt=""><figcaption></figcaption></figure>

You will be able to view a list with the classes that have the attributes of MethodFluxAttribute and StateFluxAttribute,&#x20;

<figure><img src="../.gitbook/assets/Captura de pantalla 2024-05-20 a las 1.43.29.png" alt=""><figcaption></figcaption></figure>

However, manual subscriptions cannot be viewed directly from this mode as they are dynamic.

<figure><img src="../.gitbook/assets/Captura de pantalla 2024-05-20 a las 1.45.58.png" alt=""><figcaption><p>Remember enabling UNIFLUX_DEBUG mode</p></figcaption></figure>

We will touch the Play button to view the dynamic subscriptions

<figure><img src="../.gitbook/assets/Captura de pantalla 2024-05-20 a las 1.48.02.png" alt=""><figcaption></figcaption></figure>

Note that unlike Runtime code, editor code is not fully optimized, so enabling the debugger in game mode can be resource intensive when it is open.
