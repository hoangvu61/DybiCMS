<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductAttributes.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductAttributes" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
                

<%foreach (var item in this.Data)
{%>
<div class="form-row">
    <a href="<%=HREF.LinkComponent(ComponentProducts, SettingsManager.Constants.SendAttributeId + "/" + item.CategoryId + "/" + SettingsManager.Constants.SendAttributeValue + "/" + item.Id + "/" + item.Value.ConvertToUnSign())%>" title="<%=item.Value%>">
        <input type="radio" id="<%=item.Id%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" <%=item.Id == this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue) ? "checked='checked'" : "" %>/>
        <label class="form-label"><%=item.Value%></label>
    </a>
</div> 
<%}%>