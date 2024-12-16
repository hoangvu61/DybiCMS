<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CategoryBackLink.ascx.cs" Inherits="Web.FrontEnd.Modules.CategoryBackLink" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="breadcrumb_section">
    <nav class="container" style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='%236c757d'/%3E%3C/svg%3E&#34;);" aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/"><%=this.GetValueParam<string>("BeginName") %></a></li>
            <%for (int i = 0 ; i< Categories.Count; i++) { %>
                <%if(i != Categories.Count - 1){ %>
                    <li class="breadcrumb-item">
                        <a href="<%=HREF.LinkComponent(Categories[i].ComponentList, Categories[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Categories[i].Id) %>">
                            <%=Categories[i].Title %>
                        </a>
                    </li>
                <%} else if(string.IsNullOrEmpty(ItemTitle)) {%>
                    <li class="breadcrumb-item active" aria-current="page">
                        <%=Categories[i].Title %>
                    </li>
                <%} else { %>
                    <li class="breadcrumb-item">
                        <a href="<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Categories[i].Id) %>">
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
</section>

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
            "item": "<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(),true, "sCat", Categories[i].Id) %>"
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
