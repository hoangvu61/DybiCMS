<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CategoryBackLink.ascx.cs" Inherits="Web.FrontEnd.Modules.CategoryBackLink" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<nav aria-label="breadcrumb">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="/"><%=this.GetValueParam<string>("BeginName") %></a></li>
        <%for (int i = 0 ; i< Categories.Count; i++) { %>
            <%if(i != Categories.Count - 1){ %>
                <li class="breadcrumb-item">
                    <a href="<%=HREF.LinkComponent(Categories[i].ComponentList, Categories[i].Title.ConvertToUnSign(), Categories[i].Id, SettingsManager.Constants.SendCategory, Categories[i].Id) %>">
                        <%=Categories[i].Title %>
                    </a>
                </li>
            <%} else if(string.IsNullOrEmpty(ItemTitle)) {%>
                <li class="breadcrumb-item active" aria-current="page">
                    <%=Categories[i].Title %>
                </li>
            <%} else { %>
                <li class="breadcrumb-item">
                    <a href="<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(), Categories[i].Id, "sCat", Categories[i].Id) %>">
                        <%=Categories[i].Title %>
                    </a>
                </li>
            <%} %>
        <%} %>
         <%if (!string.IsNullOrEmpty(ItemTitle))
         {%>
             <li class="breadcrumb-item active" aria-current="page">
                 <%=this.ItemTitle %>
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
            "item": "<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(),Categories[i].Id, "sCat", Categories[i].Id) %>"
        }
        <%} %>
        <%if(!string.IsNullOrEmpty(this.ItemTitle) && this.ItemTitle != Categories[Categories.Count - 1].Title){ %>
        ,{
            "@type": "ListItem",
            "position": <%= Categories.Count + 2%>,
            "name": "<%= ItemTitle %>"
        }
        <%} %>
        ]
    }
</script>