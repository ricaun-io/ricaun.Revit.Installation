# ricaun.Revit.Installation

[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](https://github.com/ricaun-io/ricaun.Revit.Installation)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](https://github.com/ricaun-io/ricaun.Revit.Installation/actions/workflows/Build.yml/badge.svg)](https://github.com/ricaun-io/ricaun.Revit.Installation/actions)
[![nuget](https://img.shields.io/nuget/v/ricaun.Revit.Installation?logo=nuget&label=nuget&color=blue)](https://www.nuget.org/packages/ricaun.Revit.Installation)

This library provides tools to easily interact with the Revit installation folder and manage `ApplicationPlugins`.

## Features
### ApplicationPluginsUtils

`ApplicationPluginsUtils` provides tools to download `.bundle.zip` application and install/unistall in the `applicationPluginsFolder`.

```C#
ApplicationPluginsUtils.DownloadBundle(applicationPluginsFolder, bundleUrl);
ApplicationPluginsUtils.DeleteBundle(applicationPluginsFolder, bundleName);
```

### RevitInstallationUtils

`RevitInstallationUtils` provides tools to get the installed Revit versions location in the machine.

```C#
RevitInstallationUtils.InstalledRevit;
```

### RevitUtils

`RevitUtils` provides tools to get the `ApplicationPlugins` and `Addin` folder.

```C#
RevitUtils.GetCurrentUserApplicationPluginsFolder();
RevitUtils.GetCurrentUserAddInFolder();
RevitUtils.GetCurrentUserAddInFolder(version);
```
```C#
RevitUtils.GetAllUsersApplicationPluginsFolder();
RevitUtils.GetAllUsersAddInFolder();
RevitUtils.GetAllUsersAddInFolder(version);
```

### TryGetRevitVersion

`TryGetRevitVersion` allow to get the Revit version from the assembly file.

```C#
RevitUtils.TryGetRevitVersion(assemblyFile, out int revitVersion);
```

## Release

* [Latest release](https://github.com/ricaun-io/ricaun.Revit.Installation/releases/latest)

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](https://github.com/ricaun-io/ricaun.Revit.Installation/stargazers)!