namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class BaseLayerBuilder<TLayer, TLayerBuilder> : LayerBuilder
		where TLayer : Layer, new()
		where TLayerBuilder : BaseLayerBuilder<TLayer, TLayerBuilder>
	{
		protected readonly TLayer _layer;

		protected BaseLayerBuilder()
		{
			_layer = new TLayer();
		}

		protected TLayer Layer
		{
			get { return _layer; }
		}

		public override Layer ToLayer()
		{
			return _layer;
		}

		public TLayerBuilder X(int x)
		{
			Layer.X = x;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder Y(int y)
		{
			Layer.Y = y;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder PaddingLeft(int padding)
		{
			Layer.Padding.Left = padding;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder PaddingTop(int padding)
		{
			Layer.Padding.Top = padding;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder PaddingRight(int padding)
		{
			Layer.Padding.Right = padding;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder PaddingBottom(int padding)
		{
			Layer.Padding.Bottom = padding;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder Anchor(AnchorStyles anchorStyles)
		{
			Layer.Anchor = anchorStyles;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder AnchorPadding(int padding)
		{
			Layer.AnchorPadding = padding;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder BlendMode(BlendMode blendMode)
		{
			Layer.BlendMode = blendMode;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder WithFilter(FilterBuilder filterBuilder)
		{
			_layer.Filters.Add(filterBuilder.ToFilter());
			return (TLayerBuilder)this;
		}
	}
}