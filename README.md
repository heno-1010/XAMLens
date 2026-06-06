# XAMLens
**Avalonia XAML live preview tool**  
Instantly render XAML without building or running a project.  
You shouldn't need to build an application just to experiment with a View.

---

In Avalonia development, even simple UI experiments often require:

- Creating a project
- Building the application
- Preparing a ViewModel
- Running the app

But often, you're just trying to answer questions like:

- Does this layout work?
- Is this Binding correct?
- How does this control look?

XAMLens removes this friction.

Write XAML and see it instantly.

## Features

### Instant Preview

Write XAML and see the result immediately.

No project setup
No build step
No app restart required

### Avalonia Runtime Preview
XAMLens uses Avalonia’s runtime XAML loader to instantiate UI from XAML.

### Binding Path Detection

XAMLens extracts Binding Paths from XAML markup.

```xaml
<TextBlock Text="{Binding User.Name}" />
<Button Command="{Binding SaveCommand}" />
```

Detected Bindings:
```
User.Name
SaveCommand
```
Understand what data your UI expects before implementing a ViewModel.

## License
MIT

