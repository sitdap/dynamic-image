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
				<Fill BackgroundColour="#c6c6c6" />
				<Layers>
					<sitdap:TextLayer Text="NEXT BLAH" Height="17" Font-Name="Arial" Font-Size="9"
						Padding-Left="10" Padding-Top="8" Padding-Right="24" Padding-Bottom="4" />
					<sitdap:ImageLayer SourceFileName="~/assets/images/arrowrightlight.png"
						Anchor="MiddleRight" AnchorPadding="10" />
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
