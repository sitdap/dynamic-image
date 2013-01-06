# Overview

## 2.2.0 (branch: master)

### Resolved Issues

* \#13 Allow DynamicImage to work in WPF applications (dieron)

## 2.1.2 - 2012-05-20

### Resolved Issues

* Default value of custom font file fixed (mzywitza).

* Fix memory leak in LayerBlenderEffect (jaytwo).

* Fix memory leak in other shader effects.

* XmlCacheProvider now checks for null values in cache dependencies (Mikhail-Fiadosenka).

* DirtyTrackingObject now deals correctly with arrays (Mikhail-Fiadosenka).

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

* Replaced DynamicImageBuilder with CompositionBuilder

* Added SoundInTheory.DynamicImage.Mvc with HtmlHelper extension for generating image tag

* Added ImageUrlGenerator for generating a (cached) URL for an image. Also available through
  the CompositionBuilder.Url property.