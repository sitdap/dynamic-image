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
							<sitdap:ResizeFilter Mode="UseWidth" Width="200" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<br /><br />
			
			<sitdap:DynamicImage runat="server" ImageFormat="Jpeg">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseHeight" Height="200" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<br /><br />
			
			<sitdap:DynamicImage runat="server" ImageFormat="Jpeg">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="Fill" Width="200" Height="200" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<br /><br />
			
			<sitdap:DynamicImage runat="server" ImageFormat="Jpeg">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="Uniform" Height="200" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<br /><br />
			
			<sitdap:DynamicImage runat="server" ImageFormat="Jpeg">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UniformFill" Width="200" Height="200" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<br /><br />
			
			<sitdap:DynamicImage runat="server" ImageFormat="Png">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UniformFill" Width="50%" Height="50%" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<br /><br />
			
			<sitdap:DynamicImage runat="server" ImageFormat="Png">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/phone.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="55" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
