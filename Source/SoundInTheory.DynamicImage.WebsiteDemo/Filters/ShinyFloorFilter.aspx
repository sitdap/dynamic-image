<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<sitdap:DynamicImage ID="DynamicImage1" runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Width="500" Height="500" />
							<sitdap:ShinyFloorFilter ReflectionPercentage="50" ReflectionOpacity="100" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<br /><br />

			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Width="300" Height="300" />
							<sitdap:ShinyFloorFilter ReflectionPercentage="100" ReflectionOpacity="25" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg" X="100">
						<Filters>
							<sitdap:ResizeFilter Width="300" Height="300" />
							<sitdap:ShinyFloorFilter ReflectionPercentage="100" ReflectionOpacity="25" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<br /><br />
			
			<sitdap:DynamicImage runat="server" ImageFormat="Jpeg">
				<Layers>
					<sitdap:TextLayer Text="Hello world." Font-Size="40" Font-Bold="true" StrokeWidth="2" StrokeColour="Red">
						<Filters>
							<sitdap:ShinyFloorFilter ReflectionPercentage="100" ReflectionOpacity="50" ReflectionPositionY="40" />
						</Filters>
					</sitdap:TextLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
