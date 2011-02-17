<%@ Page Language="C#" %>
<%@ Import Namespace="SoundInTheory.DynamicImage.Fluent" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">
	protected void Page_Load(object sender, EventArgs e)
	{
 		byte[] bytes = File.ReadAllBytes(Server.MapPath("~/Assets/Images/AutumnLeaves.jpg"));
 		imgDynamicImage.ImageUrl = new DynamicImageBuilder()
 			.WithLayer(LayerBuilder.Image.SourceBytes(bytes))
 			.Url;
	}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<asp:Image runat="server" ID="imgDynamicImage" />
    </div>
    </form>
</body>
</html>
