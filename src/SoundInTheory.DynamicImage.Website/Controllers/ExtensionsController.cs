using System.Web.Mvc;

namespace SoundInTheory.DynamicImage.Website.Controllers
{
	public class ExtensionsController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult ContentAwareResizing()
		{
			return View();
		}

		public ActionResult Pdf()
		{
			return View();
		}

		public ActionResult Rendered3D()
		{
			return View();
		}

		public ActionResult RenderedCar()
		{
			return View();
		}

		public ActionResult RenderedCathedral()
		{
			return View();
		}

		public ActionResult RenderedInline()
		{
			var scene = new InlineSceneSource();
			scene.Meshes.Add(new Mesh
			{
				Positions = new Point3DCollection(new[] { new Point3D(-15, 15, -5), new Point3D(10, 15, 0), new Point3D(-20, 0, -5), new Point3D(10, 0, 10) }),
				TextureCoordinates = new Point2DCollection(new[] { new Point2D(0, 0), new Point2D(1, 0), new Point2D(0, 1), new Point2D(1, 1) }),
				Indices = new IndexCollection(new Index[] { 0, 1, 2, 2, 1, 3 }),
				Material = new Material { TextureFileName = "~/Assets/Images/Koala.jpg", DiffuseColor = Colors.White }
			});
			scene.Meshes.Add(new Mesh
			{
				Positions = new Point3DCollection(new[] { new Point3D(5, 0, -2), new Point3D(10, 10, 0), new Point3D(15, 0, -2) }),
				Normals = new Vector3DCollection(new[] { new Vector3D(0, 0, 1), new Vector3D(0, 0, 1), new Vector3D(0, 0, 1), new Vector3D(0, 0, 1) }),
				Indices = new IndexCollection(new Index[] { 0, 1, 2 }),
				Material = new Material { DiffuseColor = Colors.Blue }
			});
			ViewBag.Scene = scene;

			return View();
		}

		public ActionResult RenderedTank()
		{
			return View();
		}

		public ActionResult WebsiteScreenshot()
		{
			return View();
		}
	}
}