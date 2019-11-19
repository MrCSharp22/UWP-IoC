# UWP IoC

A simple library that provides support for IoC containers to integrate with UWP applications. The goal is to allow the container to perform dependency injection using the property injection approach when the app navigates to new pages with minimal effort from the developer.

This library is in ALPHA phase and not recommended for production.

## Build Status

-- TODO

## Using this library

### Setup

Start by installing the [nuget package](https://www.nuget.org/packages/UWPIoC/1.0.0-alpha01) in the UWP application:

```
Install-Package UWPIoC -Version 1.0.0-alpha01
```

... and then make the ```App.Xaml.cs``` look like this:

```CSharp
public static Host ApplicationHost; // (1)

/* ... */

protected override void OnLaunched(LaunchActivatedEventArgs e)
{
    IServiceProvider serviceProvider = /* ... */ // (2)
    ApplicationHost = new Host(serviceProvider);

    var rootFrame = Window.Current.Content as Frame;

    // Do not repeat app initialization when the Window already has content,
    // just ensure that the window is active
    if (rootFrame == null)
    {
        rootFrame = ApplicationHost.CreateNewHostedUwpFrame(); // (3)

        /* ... */

        // Place the frame in the current Window
        Window.Current.Content = rootFrame;
    }

    /* ... */
}
```

Here's what the code above is doing:

(1) Initializes a new instance of the ```Host``` class. This class holds a reference to a ```IServiceProvider``` instance and ```Frame``` instance.

(2) Depending on what IoC container being used, this line gets a reference to its service provider which is used by ```IoCFrame```.

(3) Requests a new instance of ```IoCFrame``` from the ```Host``` instance. This is done when the window has no current frame instance (this is always true when the app has just been launched).

Now, whenever a call is made to ```Frame.Navigate``` from any page, the ```IoCFrame``` will take care of injecting the new page dependencies.

### Dependency Injection in pages

So how does a page code-behind file looks like when using ```IoCFrame``` to inject properties?

Here's an example:

```CSharp
public sealed partial class MyPage : Page
{
    [ViewModel] // (1)
    public MyPageViewModel ViewModel { get; set; }

    [Dependency] // (2)
    public ILogger<MyPage> Logger { get; set; }

    public MyPage()
    {
        /* ... */
    }

    /* ... */
}
```

That's it! Really. That's all there is to it!

Here's what's going on in this snippet:

(1) This the public auto-property holding a reference to a view model instance. the ```[ViewModel]``` attribute tells the ```IoCFrame``` this is a view model and that it should manually construct an instance because it won't be registered in the IoC container. ```IoCFrame``` will use a bit of reflection to find the view model's dependencies, request them from the IoC container and then create the instance.

(2) The ```[Dependency]``` attribute tells the ```IoCFrame``` that this is a typical service/dependency that is registered in the IoC container. ```IoCFrame``` will then request an instance of this service type from the IoC container and inject it in this property.

As you can see, the constructor has no custom code, no additional work from you as a developer is required here.

Now, when some code makes a call to ```Frame.Navigate(typeof(MyPage))``` the app will navigate to the new page, and all of its dependencies will be there and ready to be used as expected.

## Getting started with the code base

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

Start by cloning this repo then proceed to install the following development software on your machine:

```
- Visual Studio 2017 or later with UWP workload.
- Windows 10 SDK. Min. version: 10240. Target version: 17763.
```

### Building

Simply build in Visual Studio (F6).

## Running the tests

There are no included Unit-Tests in the code-bas *yet*.

## coding style

Strictly following Microsoft's C# style conventions and guidelines as outlined in the official [Microsoft Docs page](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)

## Deployment

No details on how to perform deployments yet.

## Built With

- UWP

## Contributing

Feedback, issues and pull requests are welcome.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the tags on this repository.
