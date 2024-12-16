<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductAttributes.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductAttributes" %>

<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="products-row row">
    <div class="col-lg-12 col-md-12 col-sm-12">
		<div class="carousel-heading" style="<%=string.IsNullOrEmpty(Skin.HeaderBackground) ? "" : ";background-color:" + this.Skin.HeaderBackground %>">
			<h4 style="<%=string.IsNullOrEmpty(this.Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h4>
		</div>				
	</div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">
<ul class="row module_colors <%=this.ComponentProducts%>">
    <%foreach (var item in this.Data)
        {%>
    <li>
        <a href="<%=HREF.LinkComponent(ComponentProducts,item.Value.ConvertToUnSign(), true, SettingsManager.Constants.SendAttributeId, this.SourceId, SettingsManager.Constants.SendAttributeValue, item.Key)%>" title="<%=item.Value%>">
                <input type="radio" id="<%=item.Key%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" <%=item.Key == this.GetValueRequest<string>(SettingsManager.Constants.SendAttributeValue) ? "checked='checked'" : "" %>/>
                <label class="form-label"><%=item.Value%></label>
            </a>
        </li>
    <%} %>
</ul>
    <div class="clear"></div>
    </div>
</div>



                


                

