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
					<sitdap:TextLayer Text="Generic message." Font-Size="40" Font-Bold="true" StrokeWidth="2" StrokeColour="Red" />
				</Layers>
			</sitdap:DynamicImage>
			<br />

			<sitdap:DynamicImage runat="server" Fill-BackgroundColour="LightGray">
				<Layers>
					<sitdap:TextLayer Text="Padded text" Font-Size="40" Padding-Left="15" Padding-Right="15" />
				</Layers>
			</sitdap:DynamicImage>
			<br />

			<sitdap:DynamicImage runat="server" Fill-BackgroundColour="LightGray">
				<Layers>
					<sitdap:TextLayer Text="Padded fixed width text" Font-Size="40" Padding-Left="15" Padding-Right="15" Width="600" HorizontalTextAlignment="Center" />
				</Layers>
			</sitdap:DynamicImage>
			<br />
			
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:TextLayer Text="Message using a custom font." Font-Size="50" StrokeColour="Blue"
						Font-CustomFontFile="~/Assets/Fonts/ADamnMess.ttf" Font-Name="A Damn Mess" />
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
