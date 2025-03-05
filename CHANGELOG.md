# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.3.2] / 2025-03-05
### Features
### Updates
- Add `FinalPathNameExtension` to fix localized `InstallLocation`. (Fix: #13)
- Update typo related to `Journal`.
### Tests
- Add `FinalPathName_Tests` with `english`, `portuguese` and `german` tests.

## [1.3.1] / 2024-12-16
### Features
- `ApplicationPluginsUtils` with `Mutex` to prevent multiple instances. (Fix: #11)
### Updates
- Update `ApplicationPluginsUtils` to download with `Mutex`.
### Tests
- Add `ApplicationPluginsUtils_Test_Download_Async` to test download.

## [1.3.0] / 2024-12-16
### Features
- `RevitInstallationUtils.InstalledRevit` without `componentGuid`. (Fix: #9)
### Updates
- Update `InstalledRevit` without `componentGuid`.

## [1.2.0] / 2024-05-23
### Features
- Feature `ProductInfoUtils` to get `ProductInfo` using `msi.dll`.
### Fixed
- Fix Revit not loading `.bundle` files properly when is already open. (Fix #7)
### Updated
- Update `ApplicationPluginsUtils` to reverse extract bundle files.
- Update `RevitInstallationUtils` to expose `GetRevitInstallations` with string component.
- Update `RevitInstallationUtils` to use `ProductInfoUtils` to get `ProductInfo`.
### Tests
- Test `ProductInfoUtils`.

## [1.1.2] / 2024-04-02
### Updated
- Update `ApplicationPluginsUtils` with `Exception` and `Log`.
### Tests
- Update `ApplicationPluginsUtils_Tests` with `Log`.

## [1.1.1] / 2023-11-21
### Updated
- Update `REVIT_COMPONENT` to work with Revit 2025.
### Tests
- Add Revit 2025 file test.

## [1.1.0] / 2023-10-02
### Features
- Package for version `net5.0+`
### Updated
- Force `DownloadBundleAsync` to create `Directory` if not exist.
- Update `ReferenceLoaderUtils` for NET and NETFRAMEWORK
### Removed
- Remove methods with `Obsolete` attribute
### Tests
- Update `ReferenceLoaderUtils_Tests` for NET and NETFRAMEWORK
- Update `RevitInstallationUtils_Tests` count to chech string.Copy obsolete

## [1.0.3] / 2023-03-16
### Fixed
- Fix `Costura` problem with `RevitInstallationUtils.CreateInstanceAndUnwrap`.

## [1.0.2] / 2023-03-16
### Features
- `ReferenceLoaderUtils` - Unload Domain with NETFRAMEWORK or Load Assembly bytes.
### Tests
- `RevitInstallationUtils_Tests`
- `RevitUtils_Test_GetReferencesRevit` - `RevitAddin1.dll` - Version 2017
### Fixed
- `GetMainModuleFileName` change to `string.Empty` by default.

## [1.0.1] / 2023-03-14
### Features
- `StartWithJornal`
### Tests
- `InstalledRevit_Test_StartWithJornal`

## [1.0.0] / 2023-02-20
### Features
- `ApplicationPluginsUtils`
- `RevitInstallationUtils`
- `RevitUtils`
### Tests
- `ricaun.Revit.Installation.Tests`

[vNext]: ../../compare/1.0.0...HEAD
[1.3.2]: ../../compare/1.3.1...1.3.2
[1.3.1]: ../../compare/1.3.0...1.3.1
[1.3.0]: ../../compare/1.2.0...1.3.0
[1.2.0]: ../../compare/1.1.2...1.2.0
[1.1.2]: ../../compare/1.1.1...1.1.2
[1.1.1]: ../../compare/1.1.0...1.1.1
[1.1.0]: ../../compare/1.0.3...1.1.0
[1.0.3]: ../../compare/1.0.2...1.0.3
[1.0.2]: ../../compare/1.0.1...1.0.2
[1.0.1]: ../../compare/1.0.0...1.0.1
[1.0.0]: ../../compare/1.0.0