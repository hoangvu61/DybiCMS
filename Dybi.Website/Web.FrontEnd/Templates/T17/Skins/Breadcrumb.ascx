<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CategoryBackLink.ascx.cs" Inherits="Web.FrontEnd.Modules.CategoryBackLink" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<nav aria-label="breadcrumb">
  <ol class="breadcrumb">
    <li class="breadcrumb-item"><a href="/"><%=this.GetValueParam<string>("BeginName") %></a></li>
    <%for (int i = 0 ; i< Categories.Count; i++) { %>
        <li class="breadcrumb-item">
            <a href="<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(), Categories[i].Id, SettingsManager.Constants.SendCategory, Categories[i].Id) %>">
                <%=Categories[i].Title %>       
            </a>
        </li>
    <%} %>    
    <%if(!string.IsNullOrEmpty(this.ItemTitle)){ %>
    <li class="breadcrumb-item active" aria-current="page">
        <%= ItemTitle %>
    </li>    
    <%} %>
  </ol>
</nav>
 
<script type="application/ld+json">
    {
        "@context": "https://schema.org",
        "@type": "BreadcrumbList",
        "itemListElement": [{
        "@type": "ListItem",
        "position": 1, 
        "name": "<%=this.GetValueParam<string>("BeginName") %>",
        "item": "<%=HREF.DomainLink %>"
        }
        <%for (int i = 0 ; i < Categories.Count; i++) { %>
        ,{
            "@type": "ListItem",
            "position": <%=i + 2 %>,
            "name": "<%=Categories[i].Title %>",
            "item": "<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(), Categories[i].Id, SettingsManager.Constants.SendCategory, Categories[i].Id) %>"
        }
        <%} %>
        <%if(!string.IsNullOrEmpty(this.ItemTitle)){ %>
        ,{
            "@type": "ListItem",
            "position": <%= Categories.Count + 2%>,
            "name": "<%= ItemTitle %>"
        }
        <%} %>
        ]
    }
</script>