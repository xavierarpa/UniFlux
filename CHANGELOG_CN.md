# 更改日志
这个软件包的所有显著变化都将记录在这个文件中。
## [1.3.1] - 2023-04-30
### 固定
- 修正了Internal.State在商店中调用的问题, 如果它之前被派发过的话。
- 修正了Internal.State在状态改变时在调度中调用的问题
## [1.3.0] - 2023-04-23
### 已添加
- 添加了FluxState、State和StateFlux来处理有状态的存储。
- 在Core.Flux中添加了StoreState和DispatchState来处理状态管理方法
- 在扩展中实现了StoreState和DispatchState
### 实验性
- 允许使用Scriptable对象作为Store和Dispatch的关键。这使得模块变得更加模块化, 但也带来了更多的模板。
### 已删除
- 删除了ActionFluxParam的dictionary_read。
## [1.2.2] - 2023-04-20
### 修正了
- FuncFlux和FuncFluxParam的设计是每个键只添加一个, 但你可以添加多个键, 这就是为什么我们减少了优化的复杂性, 只使用Func< TResult >作为TStorage。
### 删除了
- 删除了ActionFlux、ActionFluxParam、FuncFlux和FuncFluxParam中的dictionary_read。
## [1.2.1] - 2023-04-17
### 修正了
- 对Store方法进行了修正, 以便在取消订阅时将其从存储中删除, 之前允许添加第一个, 但不允许订阅或取消订阅后面的。
## [1.2.0] - 2023-04-09
现在UniFlux比以前更优化了
### 添加
- 为优化目的, 在ActionFlux中添加了dictionary_read
- 为优化目的, 在 ActionParamFlux 中添加了 dictionary_read。
- 为优化目的, 在 FuncFLux 中添加了 dictionary_read。
- 为优化目的, 在 FuncParamFLux 中添加了 dictionary_read。
### 删除了
- 删除了 IStore 中的字典合约
- 删除了Test PlayMode, 因为还没有包含测试。
### 优化了
- 优化了调度, 从 ~100.000 迭代字符串键 => 25ms 到 ~15ms
- 优化存储添加, 从~10.000迭代字符串键=> [300ms GC.Alloc 380MB]到[~15ms GC.Alloc 2.9MB]
- 优化的存储删除从~10.000迭代字符串键=> [300ms GC.Alloc 380MB]到[~15ms GC.Alloc 2.9MB]
## [1.1.1] - 2023-04-09
### 已添加
- 添加了 "Architecture.io "以获得一个微小的视角
- 添加了服务模板
- 添加了如何使用该包的示例
- 添加了Unity EditMode的单元测试(WIP PlayMode)。
- 添加了一个小小的文档
### 改变了
- 更改了订阅方法中的FluxAttribute.cs (同时删除了旧的结构)
- 改变了将多个类的脚本放入一个脚本中, 以使用不同的脚本
## [1.1.0] - 2023-04-06
Unity的UniFlux现在可以使用了 ! 你可以使用MonoFlux和[MethodFlux("Hello World")]来创建你自己的Action, 然后使用 "Hello World".Dispatch(), 就可以看到神奇的效果了!
摘要:
- 使用UniFlux
- YourMonoBehaviour : MonoFlux
- [MethodFlux("Key")] void MethodExamples() => Debug.Log("Hello World");
- "Key".Dispatch();
### 固定
- 修正了UniFlux.Core.Internal.Flux<T, T2>的问题, 它不必要地创建ActionFluxParam和FuncFlux, 现在只实例化指定的Flux。
### 改变了
- 删除了 ISubscribe
- 删除了 IDictionary
- 将内部类改为 "内部 "访问
- 增加了字典的 "只读 "属性
- 删除了ITriggers, 并在每个IFlux接口中实现。
- 重新命名了方法, 以保持标准的设计惯例(在扩展类中, 我们保留了@IEnumerator, @ITask, 等等, 以保持兼容性)。
### 添加
- 添加了IStore, 做了ISubscribe和IDictionary的工作, 简化了。
- 添加了UniFlux.Core.Flux作为公共静态类来访问内部Flux类, 就像管道一样。
- 添加了UniFlux扩展的字符串和int类型
- 添加了ScriptTemplate来创建你自己的UniFlux扩展键类型
## [1.0.0] - 2023-03-24
这是*UniFlux*的第一个版本。
### 添加
- 增加了Flux功能
- 增加了以字符串和int为键的Invoke和订阅的扩展功能
- 增加了对 "Cysharp "的UniTask的支持
- 编辑器可以通过检查器调用 Flux 方法(没有参数或返回值)。