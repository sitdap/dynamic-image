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
					<sitdap:TextLayer Text="This is a test" Font-Size="90" ForeColour="Red">
						<Filters>
							<sitdap:OuterGlowFilter Size="10" Color="Black" Opacity="100" />
						</Filters>
					</sitdap:TextLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
