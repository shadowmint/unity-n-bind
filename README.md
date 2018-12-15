# n-bind

This is a simple property based IoC container specifically for Unity3d with
no external dependencies.

## What?

Unity `GameObjects` cannot be constructed, so traditional IoC libraries are
largely not suitable for use with them.

This library allows interfaces to be bound to a registry and injected, on
demand, into instances of a class. It also works to manufacture instances
of 'normal' C# classes if required.

### Why?

There are other solutions to this problem, but broadly they fall into three
categories:

- Don't work on Unity3d.

- Work on Unity3d, but are complex and effectively entire frameworks.

- Work on Unity3d but on constructor bases, and not suitable for `GameObjects`.

[StrangeIoC](https://strangeioc.wordpress.com/) in particular is an excellent
project, but sometimes it's a bit heavy to drop into a new game. It even
includes an MVCS architecture, which is interesting, but I feel... somewhat
irrelevant to the core goal of dependency injection.

The problem is largely that packages try to be 'everything and the kitchen sink'
because there's no good package management solution for unity. This package uses
`npm` to solve that problem ([read more about that](https://github.com/shadowmint/unity-package-template/blob/master/docs/npm.md) here if you want).

As such, all it aims to do, is very specifically, inject instances into objects.

That is all.

### Can you...?

**Can you use this with constructor based normal classes like `MyClass(IService service)`?**

No. This library only supports binding properties.

**Can you bind private properties?**

No. Only public properties are supported.

**Does this library include helpers to use IoC in for UI / Animation / XXX?**

No. The only thing this library does is dependency injection.

**Can I use this to inject `GameObject` instances into a class?**

Yes. Use `registry.Register<TInterface, TImpl>(instance)` where instance is
`TImpl : MonoBehaviour, TInterface`.

**If I have to explicitly pick `TImpl` for binding instances, doesn't that
mean I can't really bind to instances as runtime?**

Have a look at the `demo` folder for an example that allows you to select
a prefab in your `ServiceModule` component and inject it into another
`GameObject` that uses it.

**Does this library need any of your other weird `n-foo` libraries to run?**

No. It uses `n-core` for tests, so if you want to run the tests you'll need
that, but it has no other dependencies.

### Related...

See also the excellent [StrangeIoC] (https://strangeioc.wordpress.com/) project,
and [Zenject](https://github.com/modesttree/Zenject) as other solutions to this
problem.

## Usage

Implement `IServiceModule` and register the module with a `ServiceRegistry`.

For convenience the `Registry.Default` instance can be used.

    using N.Package.Bind;
    using UnityEngine;

    public class ServiceModule : MonoBehaviour, IServiceModule
    {
        public BlockBase blockType;

        public void Start()
        {
            Registry.Default.Register(this);
        }

        public void Register(ServiceRegistry registry)
        {
            registry.Register<IBlock, BlockBase>(blockType.GetComponent<BlockBase>());
            registry.Register<ISpawnService, SpawnService>();
        }
    }

You may want to ensure your service module is invoked before any other scripts
under `Edit > Project Settings > Script Execution Order`:

<img src="https://raw.github.com/shadowmint/unity-n-bind/master/docs/images/order.png" width="250px"/>

Then call one of the three magic methods:

- `CreateInstance` creates an instance and binds its properties and returns it.

- `Bind` binds the properties of an instance (eg. `GameObject` instances).

- `Resolve` resolves an interface to a binding if possible.

A typical usage might look like:

    using N.Package.Bind;
    using UnityEngine;

    public class UsesObjects : MonoBehaviour
    {
        public IBlock Block { get; set; }
        public ISpawnService Spawner { get; set; }

        public void Start()
        {
            Registry.Default.Bind(this);
            spawned = Spawner.SpawnPrefab(block);
        }
    }

You can create a custom `ServiceRegistry` for complex purposes.

See the tests in the `Editor/` folder for each class for usage examples.

## Install

From your unity project folder:

    npm init
    npm install shadowmint/unity-n-bind --save
    echo Assets/pkg-all >> .gitignore
    echo Assets/pkg-all.meta >> .gitignore

The package and all its dependencies will be installed in
your Assets/pkg-all folder.

## Development

Setup and run tests:

    npm install
    npm install ..
    cd test
    npm install
    gulp

Remember that changes made to the test folder are not saved to the package
unless they are copied back into the source folder.

To reinstall the files from the src folder, run `npm install ..` again.

### Tests

All tests are wrapped in `#if ...` blocks to prevent test spam.

You can enable tests in: Player settings > Other Settings > Scripting Define Symbols

The test key for this package is: N_BIND_TESTS
