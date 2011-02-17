<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/GrayscaleAutumnLeaves.png">
						<Filters>
							<sitdap:ResizeFilter Width="200" Height="150" />
							<sitdap:FeatherFilter Radius="10" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
