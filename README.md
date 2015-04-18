# Dextem

**Dextem** is a simple .NET library to convert Visual Studio XML Documentation to GitHub Markdown. It is based on this nice, succinct Gist: [dx2md](https://gist.github.com/formix/515d3d11ee7c1c252f92). 

## Dextem.Build

Meant to run Post-build, **Dextem.Build** is a console application that uses the default **Dextem** API to create a markdown file based on a project's XML Documentation file.

Simply add a line to your Post-Build event:

```
Dextem.Build.exe $(TargetDir)$(ProjectName).XML
```

> **Note**: The target project must be configured to output its XML documentation file. [More Info](https://msdn.microsoft.com/en-us/library/3260k4x7.aspx)
