<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color:Red;width:200px;height:200px;padding-left:10px;padding-top:10px">
			<sitdap:DynamicImage runat="server" ImageFormat="Png" Fill-BackgroundColour="Transparent">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/bmw.jpg">
						<Filters>
							<sitdap:ColorKeyFilter Color="White" ColorTolerance="5" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
