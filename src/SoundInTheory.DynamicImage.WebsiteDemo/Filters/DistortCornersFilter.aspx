<%@ Page Language="C#" %>
<%@ Register TagPrefix="sitdap" Namespace="SoundInTheory.DynamicImage.Layers" Assembly="SoundInTheory.DynamicImage, Version=1.0.6.0, Culture=neutral, PublicKeyToken=fa44558110383067" %>
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
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/win7_rtm_homepremium_16.jpg">
						<Filters>
							<sitdap:ShinyFloorFilter ReflectionOpacity="75" ReflectionPercentage="30" />
							<sitdap:DistortCornersFilter X1="0" Y1="-50" X2="550" Y2="0" X3="550" Y3="500" X4="0" Y4="600" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/win7_rtm_homepremium_08.jpg" X="100">
						<Filters>
							<sitdap:ShinyFloorFilter ReflectionOpacity="75" ReflectionPercentage="30" />
							<sitdap:DistortCornersFilter X1="0" Y1="-50" X2="550" Y2="0" X3="550" Y3="500" X4="0" Y4="600" />
						</Filters>
					</sitdap:ImageLayer>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/win7_rtm_homepremium_04.jpg" X="200">
						<Filters>
							<sitdap:ShinyFloorFilter ReflectionOpacity="75" ReflectionPercentage="30" />
							<sitdap:DistortCornersFilter X1="0" Y1="-50" X2="550" Y2="0" X3="550" Y3="500" X4="0" Y4="600" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>

			<sitdap:DynamicImage runat="server">
				<Layers>
					<sitdap:WebsiteScreenshotLayer WebsiteUrl="http://www.microsoft.com">
						<Filters>
							<sitdap:ShinyFloorFilter ReflectionOpacity="75" ReflectionPercentage="30" />
							<sitdap:DistortCornersFilter X1="0" Y1="-50" X2="550" Y2="0" X3="550" Y3="500" X4="0" Y4="600" />
						</Filters>
					</sitdap:WebsiteScreenshotLayer>
					<sitdap:WebsiteScreenshotLayer WebsiteUrl="http://www.apple.com" X="100">
						<Filters>
							<sitdap:ShinyFloorFilter ReflectionOpacity="75" ReflectionPercentage="30" />
							<sitdap:DistortCornersFilter X1="0" Y1="-50" X2="550" Y2="0" X3="550" Y3="500" X4="0" Y4="600" />
						</Filters>
					</sitdap:WebsiteScreenshotLayer>
					<sitdap:WebsiteScreenshotLayer WebsiteUrl="http://www.adobe.com" X="200">
						<Filters>
							<sitdap:ShinyFloorFilter ReflectionOpacity="75" ReflectionPercentage="30" />
							<sitdap:DistortCornersFilter X1="0" Y1="-50" X2="550" Y2="0" X3="550" Y3="500" X4="0" Y4="600" />
						</Filters>
					</sitdap:WebsiteScreenshotLayer>
				</Layers>
			</sitdap:DynamicImage>
    </div>
    </form>
</body>
</html>
