namespace SoundInTheory.DynamicImage
{
	public class IndexCollection : DirtyTrackingCollection<Index>
	{
		public IndexCollection()
		{

		}

		public IndexCollection(Index[] collection)
			: base(collection)
		{

		}
	}
}