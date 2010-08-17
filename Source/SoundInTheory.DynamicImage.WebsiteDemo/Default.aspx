<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DemoWebsite.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>DynamicImage</title>
</head>
<body>
	<coolite:ScriptManager runat="server" ID="csmScriptManager" />
	
	<coolite:Menu runat="server" ID="mnuContextMenu">
		<Items>
			<coolite:MenuItem Text="New Layer" OnClientClick="return false;">
				<Menu>
					<coolite:Menu>
						<Items>
							<coolite:MenuItem Text="Image Layer" />
						</Items>
					</coolite:Menu>
				</Menu>
			</coolite:MenuItem>
		</Items>
	</coolite:Menu>
	
	<coolite:ViewPort runat="server">
		<Body>
			<coolite:BorderLayout runat="server">
				<West Split="true">
					<coolite:TreePanel runat="server" Title="Image Structure" Width="200">
						<Root>
							<coolite:TreeNode Text="Image">
								<Nodes>
									<coolite:TreeNode Text="Layers">
										<Listeners>
											<ContextMenu Fn="function(node, e) { mnuContextMenu.showAt(e.getPoint()); }" />
										</Listeners>
									</coolite:TreeNode>
								</Nodes>
							</coolite:TreeNode>
						</Root>
					</coolite:TreePanel>
				</West>
				<Center>
					<coolite:Panel runat="server" Title="Image Preview" />
				</Center>
				<East Split="true">
					<coolite:PropertyGrid runat="server" ID="prgProperties" Title="Properties" Width="300">
						<Source>
							<coolite:PropertyGridParameter Name="version" Value="0.01" >
								<Editor>
										<coolite:ComboBox runat="server" TriggerAction="All" Mode="Local">
												<Items>
														<coolite:ListItem Text="0.01" />
														<coolite:ListItem Text="0.02" />
														<coolite:ListItem Text="0.03" />
														<coolite:ListItem Text="0.04" />
												</Items>
										</coolite:ComboBox>
								</Editor>
							</coolite:PropertyGridParameter>
						</Source>
					</coolite:PropertyGrid>
				</East>
			</coolite:BorderLayout>
		</Body>
	</coolite:ViewPort>
</body>
</html>
