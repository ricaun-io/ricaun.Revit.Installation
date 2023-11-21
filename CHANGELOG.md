# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

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
[1.1.1]: ../../compare/1.1.0...1.1.1
[1.1.0]: ../../compare/1.0.3...1.1.0
[1.0.3]: ../../compare/1.0.2...1.0.3
[1.0.2]: ../../compare/1.0.1...1.0.2
[1.0.1]: ../../compare/1.0.0...1.0.1
[1.0.0]: ../../compare/1.0.0