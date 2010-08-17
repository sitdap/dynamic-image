<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
  <form runat="server">
  <div>
		<h1>Opacity</h1>
		
		<sitdap:DynamicImage runat="server" ImageFormat="Png">
			<Layers>
				<sitdap:RectangleShapeLayer X="100" Y="200" Width="500" Height="500" Fill-BackgroundColour="Red">
					<Filters>
						<sitdap:OpacityAdjustmentFilter Opacity="50" />
					</Filters>
				</sitdap:RectangleShapeLayer>
				<sitdap:RectangleShapeLayer X="200" Y="250" Width="200" Height="200" Fill-BackgroundColour="Blue">
					<Filters>
						<sitdap:OpacityAdjustmentFilter Opacity="50" />
					</Filters>
				</sitdap:RectangleShapeLayer>
				<sitdap:RectangleShapeLayer X="50" Y="0" Width="200" Height="400" Fill-BackgroundColour="Green">
					<Filters>
						<sitdap:OpacityAdjustmentFilter Opacity="75" />
					</Filters>
				</sitdap:RectangleShapeLayer>
			</Layers>
		</sitdap:DynamicImage>
  </div>
  </form>
</body>
</html>
