<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
		<sitdap:DynamicImage runat="server" ImageFormat="Png">
			<Layers>
				<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
					<Filters>
						<sitdap:CurvesFilter PhotoshopCurvesFileName="~/Assets/Misc/CrossProcess-Curves.acv" />
					</Filters>
				</sitdap:ImageLayer>
			</Layers>
		</sitdap:DynamicImage>
    </form>
</body>
</html>
