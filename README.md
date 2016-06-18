PrettyBars
====

Tune your UWP app's titlebar or statusbar by simply way!

How to use
----

```
PM> Install-Package PrettyBars
```

```xml
<Page
...
    xmlns:extensions="using:PrettyBars.Extensions"
    extensions:TitleBar.ExtendViewInTitleBar="True"
    extensions:TitleBar.ForegroundColor="Black"
    extensions:TitleBar.BackgroundColor="White"
    extensions:TitleBar.AccentColor="Read">
...
</Page>
```

License
----
The library released under MIT license.
