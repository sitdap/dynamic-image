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
					<sitdap:RectangleShapeLayer Width="500" Height="400" Fill-BackgroundColour="#AECDEF"
						StrokeDashStyle="DashDotDot" StrokeWidth="3" StrokeFill-BackgroundColour="#345678">
						<Filters>
							<sitdap:DropShadowFilter Size="10" Distance="10" />
						</Filters>
					</sitdap:RectangleShapeLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
