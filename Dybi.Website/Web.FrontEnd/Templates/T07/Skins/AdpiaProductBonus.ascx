<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/AdpiaBonus.ascx.cs" Inherits="Web.FrontEnd.Modules.AdpiaBonus" %>

<script>
    var adpiaProductBonus = '';
    <%if(!string.IsNullOrEmpty(Data)){ %>
        adpiaProductBonus = '<%=Data.Replace("Copy","Mua").Replace("Share","Chia sẻ").Replace("'","|").Replace("\n","") %>';
    <%}%>
    adpiaProductBonus = adpiaProductBonus.replaceAll('|', '\'');
</script>
