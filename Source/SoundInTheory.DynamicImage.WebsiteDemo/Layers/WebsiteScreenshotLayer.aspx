<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<sitdap:DynamicImage runat="server" ImageFormat="Jpeg">
				<Layers>
					<sitdap:WebsiteScreenshotLayer WebsiteUrl="http://www.microsoft.com" Timeout="3000">
						<Filters>
							<sitdap:ResizeFilter Width="500" Mode="UseWidth" />
							<sitdap:CropFilter Width="500" Height="500" />
						</Filters>
					</sitdap:WebsiteScreenshotLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
