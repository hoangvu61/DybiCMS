<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Medias.ascx.cs" Inherits="Web.FrontEnd.Modules.Medias" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<!-- info section -->
<section class="info_section layout_padding-top layout_padding2-bottom">
    <div class="container">
        <div class="social_container">
        <h2>
        <%=Title %>
        </h2>
        <div class="social_box"> 
            <%foreach(var item in this.Data) 
            {%> 
                <a href="<%=item.Url%>" target="<%=item.Target%>">
                    <img src="/Templates/T22/images/<%=item.Title%>.png" alt="" />
                </a>
            <%} %>
        </div>
    </div>
    </div>
</section>
<!-- end info_section -->

<script type="application/ld+json">
{
    "@context": "https://schema.org",
    "@type": "ProfilePage",
    "dateCreated": "<%=Component.Company.CreateDate%>",
    "mainEntity": {
    "@type": "Person",
    "name": "<%=Component.Company.FullName %>",
     <%if(!string.IsNullOrEmpty(Component.Company.TaxCode)){ %>
        "identifier": "<%=Component.Company.TaxCode %>",
    <%} %>
    "description": "<%=Component.Company.Motto %>",
    "image": "<%=HREF.DomainStore + this.Component.Company.Image.FullPath %>",
    "sameAs": [
        <%foreach(var item in this.Data) 
        {%> 
            "<%=item.Url%>",
        <%} %>
        "<%=HREF.DomainLink %>"
        ]
    }
}
</script>