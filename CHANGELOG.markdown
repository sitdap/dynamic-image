# Overview

## 3.2.0 (branch: master)

### New Features

* \#14 Changed `AnchorPadding` property on `Layer` class from `int` to `Unit`, to support percentage-based
  anchor padding (Mikhail Fiadosenka)  

### Resolved Issues

* \#13 Signed ContentAwareResizing, Pdf and WebsiteScreenshot extension assemblies

## 3.1.1 - 2012-08-02

### Resolved Issues

* \#4 BorderFilter doesn't work properly if used with OpacityFilter

* \#3 RoundCornersFilter should work without a border 

* \#2 Cache key doesn't change when fill colour is changed 

## 3.1.0 - 2012-08-02

### New Features

* Add `RemoteImageSource`, and corresponding `LayerBuilder.Image.SourceUrl(...)` fluent API method,
  to allow use of remotely hosted images in `ImageLayer`s.

## 3.0.1 - 2012-06-22

### Major Changes (Backwards Incompatible)

* DynamicImage now has its own `Color` and `Colors` classes, to replace the `System.Windows.Media.Color` class that it previously used.

* There were uses of both Color and Colour throughout the API - this has now been standardised to Color.

* `VideoLayer` has been removed, because it didn't work.

### Resolved Issues

* \#14 `DynamicImageModule` no longer needs to be registered in web.config: the NuGet package now adds a `DynamicImage.cs`
  initializer that adds the module using WebActivator.

* \#13 Allow DynamicImage to work in WPF applications (dieron)

## 2.1.2 - 2012-05-20

### Resolved Issues

* Default value of custom font file fixed (mzywitza).

* Fix memory leak in `LayerBlenderEffect` (jaytwo).

* Fix memory leak in other shader effects.

* `XmlCacheProvider` now checks for null values in cache dependencies (Mikhail-Fiadosenka).

* `DirtyTrackingObject` now deals correctly with arrays (Mikhail-Fiadosenka).

## 2.1.1 - 2012-03-28

### New Features

* Cached images are now sorted into sub-directories, using the first two characters of the filename

### Resolved Issues

* Resize filter width and height weren't added to the cache key correctly

## 2.1.0 - 2012-02-17

### New Features

* Image cache directory can now be configured in web.config (instead of being hard-coded to ~/App_Data/DynamicImage):
  <add name="XmlCacheProvider"
       type="SoundInTheory.DynamicImage.Caching.XmlCacheProvider, SoundInTheory.DynamicImage"
       cachePath="~/MyCacheDir" />

## 2.0.1 and 2.0.2 - 2012-02-11

### Resolved Issues

* NuGet package fixes

## 2.0.0 - 2012-02-11

### New Features

* Removed all WebForms-specific controls and functionality

* Replaced `DynamicImageBuilder` with `CompositionBuilder`

* Added SoundInTheory.DynamicImage.Mvc with `HtmlHelper` extension for generating image tag

* Added `ImageUrlGenerator` for generating a (cached) URL for an image. Also available through
  the `CompositionBuilder.Url` property.