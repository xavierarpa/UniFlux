# Changelog
All notable changes to this package will be documented in this file.

## [1.2.1] - 2023-04-17

### Fixed
- The Store methods have been fixed so that when unsubscribing they remove it from the storage, before it allowed to add the first one but not to subscribe or unsubscribe the following ones.

## [1.2.0] - 2023-04-09

Now UniFlux is more optimized than before

### Added
- Added in ActionFlux dictionary_read for optimization purposes
- Added in ActionParamFlux dictionary_read for optimization purposes
- Added in FuncFLux dictionary_read for optimization purposes
- Added in FuncParamFLux dictionary_read for optimization purposes

### Removed
- Removed Dictionary contract in IStore
- Removed Test PlayMode for no test included yet

### Optimized
- Optimized Dispatching from ~100.000 iteration string key => 25ms to ~15ms
- Optimized Storing Add from ~10.000 iteration string key => [300ms GC.Alloc 380MB] to [~15ms GC.Alloc 2.9MB]
- Optimized Storing Remove from ~10.000 iteration string key => [300ms GC.Alloc 380MB] to [~15ms GC.Alloc 2.9MB]

## [1.1.1] - 2023-04-09

### Added
- Added "Architecture.io" to get a tiny view of perspective
- Added Service Template
- Added Samples of how to use the Package
- Added Unit testing for Unity EditMode (WIP PlayMode)
- Added a tiny documentation

### Changed
- Changed FluxAttribute.cs in Subscribe Method (And also removing the old structure)
- Changed Scripts with more of one class into a single script to use different scripts

## [1.1.0] - 2023-04-06

UniFlux for Unity is now available ! You can use MonoFlux and [Flux("Hello World")] to create your own Action, then use "Hello World".Dispatch() and see the magic!
Summary:
- using Kingdox.UniFlux
- YourMonoBehaviour : MonoFlux
- [Flux("Key")] void MethodExamples() => Debug.Log("Hello World");
- "Key".Dispatch();

### Fixed
- Fixed Bug with Kingdox.UniFlux.Core.Internal.Flux<T, T2> where it create ActionFluxParam and FuncFlux innecessarily, now only instantiate the specified

### Changed
- Removed ISubscribe
- Removed IDictionary
- Changed Internal classes to 'internal' access
- Added dictionaries as 'readonly' property
- Removed ITriggers and implemented in each IFlux interface
- Renamed Methods to keep standard design conventions (in extension classes we keep @IEnumerator, @ITask, etc.. for compatibility)

### Added
- Added IStore to do what ISubscribe and IDictionary does, simplified
- Added Kingdox.UniFlux.Core.Flux as public static class to access internal Flux class, like a pipeline
- Added UniFlux Extension for string and int types
- Added ScriptTemplate to create your own UniFlux Extension key type

## [1.0.0] - 2023-03-24
This is the first release of *UniFlux*.

### Added
- Added Flux capabilities
- Added Extensions to Invoke and subscribe with string and int as keys
- Added support for UniTask from 'Cysharp'
- Editor to Invoke Flux Methods by Inspector (no available with parameters or return value)




<!-- 
    Template 
    ## [1.0.0] - 2023-12-31 
    This is the a commit

    ### Added
    ### Removed
    ### Fixed
    ### Changed
    ### Unreleased
    # Changelog
-->
