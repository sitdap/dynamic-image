<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<h1>Blend Modes</h1>
			
			<h2>Source Images</h2>
			
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h2>Blended Images</h2>
			
			<h3>Normal</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Normal">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Dissolve</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Dissolve">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
							<sitdap:OpacityAdjustmentFilter Opacity="50" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Multiply</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Multiply">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Screen</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Screen">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Overlay</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Overlay">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Darken</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Darken">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Lighten</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Lighten">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Color Dodge</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="ColorDodge">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Color Burn</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="ColorBurn">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Linear Dodge</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="LinearDodge">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Linear Burn</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="LinearBurn">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Lighter Color</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="LighterColor">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Darker Color</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="DarkerColor">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Hard Light</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="HardLight">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Soft Light</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="SoftLight">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Linear Light</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="LinearLight">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Pin Light</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="PinLight">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Difference</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Difference">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Exclusion</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Exclusion">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Color</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Color">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			
			<h3>Luminosity</h3>
			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/Forest.jpg" BlendMode="Luminosity">
						<Filters>
							<sitdap:ResizeFilter Mode="UseWidth" Width="250" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
