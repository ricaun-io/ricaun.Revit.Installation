# ricaun.Revit.Installation

[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)

## Features
### ApplicationPluginsUtils
```C#
ApplicationPluginsUtils.DownloadBundle(applicationPluginsFolder, bundleUrl);
ApplicationPluginsUtils.DownloadBundleAsync(applicationPluginsFolder, bundleUrl);
ApplicationPluginsUtils.ExtractBundle(applicationPluginsFolder, bundleZipPath);
ApplicationPluginsUtils.DeleteBundleFiles(applicationPluginsFolder, bundleName);
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

## Release

* [Latest release](../../releases/latest)

## License

This project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!