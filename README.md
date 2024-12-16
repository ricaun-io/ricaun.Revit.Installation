# ricaun.Revit.Installation

[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](https://github.com/ricaun-io/ricaun.Revit.Installation)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](https://github.com/ricaun-io/ricaun.Revit.Installation/actions/workflows/Build.yml/badge.svg)](https://github.com/ricaun-io/ricaun.Revit.Installation/actions)
[![.NET Framework 4.5](https://img.shields.io/badge/.NET%20Framework%204.5-blue.svg)](https://github.com/ricaun-io/ricaun.Revit.Installation)
[![.NET Standard 2.0](https://img.shields.io/badge/-.NET%20Standard%202.0-blue)](https://github.com/ricaun-io/ricaun.Revit.Installation)
[![.NET 5.0](https://img.shields.io/badge/-.NET%205.0-blue)](https://github.com/ricaun-io/ricaun.Revit.Installation)
[![nuget](https://img.shields.io/nuget/v/ricaun.Revit.Installation?logo=nuget&label=nuget&color=blue)](https://www.nuget.org/packages/ricaun.Revit.Installation)

## Features
### ApplicationPluginsUtils
```C#
ApplicationPluginsUtils.DownloadBundle(applicationPluginsFolder, bundleUrl);
ApplicationPluginsUtils.DownloadBundleAsync(applicationPluginsFolder, bundleUrl);
ApplicationPluginsUtils.DeleteBundle(applicationPluginsFolder, bundleName);
```

### RevitInstallationUtils
```C#
RevitInstallationUtils.InstalledRevit;
```

### RevitUtils
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
```C#
RevitUtils.TryGetRevitVersion(assemblyFile, out int revitVersion);
```

## Release

* [Latest release](https://github.com/ricaun-io/ricaun.Revit.Installation/releases/latest)

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](https://github.com/ricaun-io/ricaun.Revit.Installation/stargazers)!