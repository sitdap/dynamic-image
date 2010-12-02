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
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/sunset.jpg">
						<Filters>
							<sitdap:ResizeFilter Width="550" Mode="UseWidth" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			<br />

			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/sunset.jpg">
						<Filters>
							<sitdap:ResizeFilter Width="550" Mode="UseWidth" />
							<sitdap:ResizeFilter Width="350" Height="412" Mode="Fill" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			<br />

			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/sunset.jpg">
						<Filters>
							<sitdap:ResizeFilter Width="550" Mode="UseWidth" />
							<sitdap:ContentAwareResizeFilter Width="350" ConvolutionType="V1" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>