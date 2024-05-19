---
layout:
  title:
    visible: true
  description:
    visible: false
  tableOfContents:
    visible: true
  outline:
    visible: true
  pagination:
    visible: false
---

# StateFluxAttribute

StateFlux allows you to subscribe to a state and, if it has a current state, it will emit the message that we are subscribing to be updated with the latest state, in this case there is only one way to deal with this attribute (unlike MethodFluxAttribute)

Here is a simple example

<pre class="language-csharp" data-line-numbers data-full-width="true"><code class="lang-csharp"><strong>using UniFlux;
</strong>public sealed class TestFlux : MonoFlux 
{
  // "Test_BeOrNotToBeState".DispatchState(true);
  [StateFlux("Test_BeOrNotToBeState")] private void OnExampleState(bool state)
  {
    Debug.Log(state);
  }
}
</code></pre>
