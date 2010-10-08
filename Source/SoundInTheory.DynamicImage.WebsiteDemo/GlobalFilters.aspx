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
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Width="500" Height="500" />
							<sitdap:BorderFilter Width="30" Fill-BackgroundColour="NavajoWhite" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:TextLayer Text="Happy Birthday" Anchor="BottomCenter" AnchorPadding="5" />
				</Layers>
				<Filters>
					<sitdap:ResizeFilter Width="300" Height="200" />
				</Filters>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
