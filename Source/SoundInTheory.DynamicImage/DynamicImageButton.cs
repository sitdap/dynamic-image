using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web;
using System.Reflection;

namespace SoundInTheory.DynamicImage
{
	public class DynamicImageButton : DynamicImage, IPostBackDataHandler, IPostBackEventHandler, IButtonControl
	{
		#region Events

		public event ImageClickEventHandler Click;
		public event CommandEventHandler Command;
		private event EventHandler ButtonClick;

		event EventHandler IButtonControl.Click
		{
			add
			{
				lock (ButtonClick)
				{
					ButtonClick += value;
				}
			}
			remove
			{
				lock (ButtonClick)
				{
					ButtonClick -= value;
				}
			}
		}

		#endregion

		// Fields
		private int x;
		private int y;

		// Methods
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
				page.VerifyRenderingInServerForm(this);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "image");
			string uniqueID = this.UniqueID;
			PostBackOptions postBackOptions = this.GetPostBackOptions();
			if ((uniqueID != null) && ((postBackOptions == null) || (postBackOptions.TargetControl == this)))
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			bool isEnabled = base.IsEnabled;
			string firstScript = string.Empty;
			if (isEnabled)
			{
				firstScript = Util.Util.EnsureEndWithSemiColon(this.OnClientClick);
				if (base.HasAttributes)
				{
					string str3 = base.Attributes["onclick"];
					if (str3 != null)
					{
						firstScript = firstScript + Util.Util.EnsureEndWithSemiColon(str3);
						base.Attributes.Remove("onclick");
					}
				}
			}
			if (this.Enabled && !isEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.AddAttributesToRender(writer);
			if ((page != null) && (postBackOptions != null))
			{
				page.ClientScript.RegisterForEventValidation(postBackOptions);
				if (isEnabled)
				{
					string postBackEventReference = page.ClientScript.GetPostBackEventReference(postBackOptions, false);
					if (!string.IsNullOrEmpty(postBackEventReference))
						firstScript = Util.Util.MergeScript(firstScript, postBackEventReference);
				}
			}
			if (firstScript.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, firstScript);
				/*if (base.EnableLegacyRendering)
				{
					writer.AddAttribute("language", "javascript", false);
				}*/
			}
		}

		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions options = new PostBackOptions(this, string.Empty);
			options.ClientSubmit = false;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				options.ActionUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
			}
			if ((this.CausesValidation && (this.Page != null)) && (this.Page.GetValidators(this.ValidationGroup).Count > 0))
			{
				options.PerformValidation = true;
				options.ValidationGroup = this.ValidationGroup;
			}
			return options;
		}

		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string uniqueID = this.UniqueID;
			string s = postCollection[uniqueID + ".x"];
			string str3 = postCollection[uniqueID + ".y"];
			if (((s != null) && (str3 != null)) && ((s.Length > 0) && (str3.Length > 0)))
			{
				this.x = int.Parse(s, CultureInfo.InvariantCulture);
				this.y = int.Parse(str3, CultureInfo.InvariantCulture);
				if (this.Page != null)
				{
					this.Page.RegisterRequiresRaiseEvent(this);
				}
			}
			return false;
		}

		protected virtual void OnClick(ImageClickEventArgs e)
		{
			if (Click != null)
				Click(this, e);
			if (ButtonClick != null)
				ButtonClick(this, e);
		}

		protected virtual void OnCommand(CommandEventArgs e)
		{
			if (Command != null)
				Command(this, e);
			base.RaiseBubbleEvent(this, e);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null)
			{
				this.Page.RegisterRequiresPostBack(this);
				if (base.IsEnabled && ((this.CausesValidation && (this.Page.GetValidators(this.ValidationGroup).Count > 0)) || !string.IsNullOrEmpty(this.PostBackUrl)))
				{
					// HACK!!!
					MethodInfo m = this.Page.GetType().GetMethod("RegisterWebFormsScript", BindingFlags.Instance | BindingFlags.NonPublic);
					m.Invoke(Page, new object[] { });
					//this.Page.RegisterWebFormsScript();
				}
			}
		}

		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			//base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
				this.Page.Validate(this.ValidationGroup);
			this.OnClick(new ImageClickEventArgs(this.x, this.y));
			this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
		}

		protected virtual void RaisePostDataChangedEvent()
		{
		}

		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Properties
		[Themeable(false), DefaultValue(true), Category("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				object obj2 = this.ViewState["CausesValidation"];
				if (obj2 != null)
				{
					return (bool) obj2;
				}
				return true;
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		[Bindable(true), DefaultValue(""), Themeable(false), Category("Behavior")]
		public string CommandArgument
		{
			get
			{
				string str = (string) this.ViewState["CommandArgument"];
				if (str != null)
				{
					return str;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		[DefaultValue(""), Themeable(false), Category("Behavior")]
		public string CommandName
		{
			get
			{
				string str = (string) this.ViewState["CommandName"];
				if (str != null)
				{
					return str;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		[DefaultValue(true), Browsable(true), Category("Behavior"), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		[Themeable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool GenerateEmptyAlternateText
		{
			get
			{
				return base.GenerateEmptyAlternateText;
			}
			set
			{
				throw new NotSupportedException(string.Format("Setting the {0} property of {1} is not supported.", "GenerateEmptyAlternateText", base.GetType().ToString()));
			}
		}

		[Category("Behavior"), DefaultValue(""), Themeable(false)]
		public virtual string OnClientClick
		{
			get
			{
				string str = (string) this.ViewState["OnClientClick"];
				if (str == null)
				{
					return string.Empty;
				}
				return str;
			}
			set
			{
				this.ViewState["OnClientClick"] = value;
			}
		}

		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DefaultValue(""), Themeable(false), UrlProperty("*.aspx"), Category("Behavior")]
		public virtual string PostBackUrl
		{
			get
			{
				string str = (string) this.ViewState["PostBackUrl"];
				if (str != null)
				{
					return str;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		string IButtonControl.Text
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		protected virtual string Text
		{
			get
			{
				return this.AlternateText;
			}
			set
			{
				this.AlternateText = value;
			}
		}

		[Category("Behavior"), Themeable(false), DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				string str = (string) this.ViewState["ValidationGroup"];
				if (str != null)
				{
					return str;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}
	}
}
