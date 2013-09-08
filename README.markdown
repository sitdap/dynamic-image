# DynamicImage

This is the source code repository for DynamicImage, an open source image manipulation library for ASP.NET.
DynamicImage helps you simplify the way you deal with images in your ASP.NET websites.

### Links

* [DynamicImage Website and Documentation](http://dynamicimage.apphb.com)
  * [Getting Started](http://dynamicimage.apphb.com/gettingstarted/installation)
  * [Layers](http://dynamicimage.apphb.com/layers)
  * [Filters](http://dynamicimage.apphb.com/filters)
* [DynamicImage NuGet packages](http://nuget.org/packages?q=DynamicImage)
* [DynamicImage Google Group](https://groups.google.com/forum/#!forum/dynamicimage-net)

### API

DynamicImage allows images to be created in two ways:

1. Programmatically, using the object model:

		Composition composition = new Composition();
		composition.Layers.Add(new ImageLayer
		{
			SourceFileName = "~/Assets/Images/AutumnLeaves.jpg",
			Filters =
			{
				new ResizeFilter { Width = Unit.Pixel(800), Mode = ResizeMode.UseWidth }
			}
		});
		composition.Layers.Add(new TextLayer
		{
			Text = "Hello World",
			Filters =
			{
				new OuterGlowFilter()
			}
		});
		string url = ImageUrlGenerator.GetImageUrl(composition);

2. Programmatically, using a fluent interface:

		string imageUrl = new CompositionBuilder()
			.WithLayer(LayerBuilder.Image.SourceFile("myimage.png")
				.WithFilter(FilterBuilder.Resize.ToWidth(800))
			)
			.WithLayer(LayerBuilder.Text.Text("Hello World")
				.WithFilter(FilterBuilder.OuterGlow)
			).Url;


### Layers

Images in DynamicImage are composed of one or more layers, and each layer can have zero or more filters applied.
DynamicImage includes several built-in layer types, and it is straightforward to create your own.

* Image Layer
* Fractal Layer (Julia and Mandelbrot)
* Polygon Shape Layer
* Rectangle Shape Layer
* Text Layer


### Image Sources

Image Layers accept input from a variety of sources, and it is also straightforward to write your own ImageSource.
The image sources included with DynamicImage let you load images from:

* Raw bytes
* Binary database field
* File
* Remote URL
* System.Windows.Media.Imaging.BitmapSource object


### Filters

Filters are applied to layers to modify them in some way. DynamicImage provides more than 15 filters you can apply to your images, including:

* Brightness Adjustment
* Clipping Mask
* Colour Key
* Colour Tint
* Contrast Adjustment
* Crop
* Distort Corners
* Drop Shadow
* Emboss
* Feather
* Gaussian Blur
* Grayscale
* Inversion
* Opacity Adjustment
* Outer Glow
* Resize
* Rotation
* Sepia
* Shiny Floor


### Caching

Output images can be cached, based on settings in web.config. You can write your own cache provider,
and the built-in cache providers are:

* In-memory
* XML file


### Underpinnings

DynamicImage uses Windows Presentation Foundation (WPF) internally for bitmap manipulation.
Most of the filters are written as WPF shader effects, which are compiled into fast SSE instructions,
and run with good performance in a server environment.


### More information

The [DynamicImage website](http://dynamicimage.apphb.com) includes a getting started guide, as well as
examples of every layer and filter.

If you get stuck, you can try:

* asking on StackOverflow, and then [tweeting](http://twitter.com/roastedamoeba) me a link to the question
* asking a question on the [Google Group](https://groups.google.com/forum/#!forum/dynamicimage-net)
* [tweeting](http://twitter.com/roastedamoeba) me
* [emailing](mailto:tim@timjones.tw) me

Preferably in that order, please :)


### Acknowledgements

DynamicImage was created by Sound in Theory Ltd, a web design company based in Exeter, United Kingdom.  
[Sound in Theory Ltd](http://www.soundintheory.co.uk)