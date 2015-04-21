# Dextem

**Dextem** is a simple .NET library to convert Visual Studio XML Documentation to GitHub Markdown. It is based on this nice, succinct Gist: [dx2md](https://gist.github.com/formix/515d3d11ee7c1c252f92). 

# Dextem API Reference

See the result of Dextem at work. It processed its xml documentation and produced the following results: [Dextem API Reference](https://github.com/GraphExec/Dextem/wiki/Dextem-API-Reference).

## Dextem.Build

Meant to run Post-build, **Dextem.Build** is a console application that uses the default **Dextem** API to create a markdown file based on a project's XML Documentation file. See the full [Dextem.Build Command-Line Reference](https://github.com/GraphExec/Dextem/wiki/Dextem.Build-Command-Line-Reference).

Simply add a line to your Post-Build event:

```
Dextem.Build.exe $(TargetDir)$(ProjectName).XML <filename>.md
```

> **Note**: The target project must be configured to output its XML documentation file. [More Info](https://msdn.microsoft.com/en-us/library/3260k4x7.aspx)
