<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<sitdap:DynamicImage runat="server" Fill-BackgroundColour="White">
				<Layers>
					<sitdap:ImageLayer X="100" Y="100">
						<Source>
							<sitdap:FileImageSource FileName="~/Assets/Images/AutumnLeaves.jpg" />
						</Source>
						<Filters>
							<sitdap:DropShadowFilter Size="10" Angle="315" Distance="10" Color="Red" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer>
						<Source>
							<sitdap:FileImageSource FileName="~/Assets/Images/AutumnLeaves.jpg" />
						</Source>
						<Filters>
							<sitdap:DropShadowFilter Size="10" Angle="315" Distance="10" Color="Blue" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
