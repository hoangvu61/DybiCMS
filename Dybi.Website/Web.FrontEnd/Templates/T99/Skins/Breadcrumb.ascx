<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CategoryBackLink.ascx.cs" Inherits="Web.FrontEnd.Modules.CategoryBackLink" %>
<%@ Import Namespace="Library"%>

<%if(HREF.CurrentComponent.EndsWith("s")){ %>
<div class="row"> 
    <div class="col-md-10 col-md-offset-1">
        <ul class="breadcrumb" style="margin-bottom:0px">
            <li><a href="/"><%=Language["home"] %></a></li>
            <%for (int i = 0 ; i< Categories.Count; i++) { %>
                <li>
                    <a href="<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(),Categories[i].Id, "sCat", Categories[i].Id) %>">
                        <%=Categories[i].Title %>       
                    </a>
                </li>
            <%} %>    
            <%if(!string.IsNullOrEmpty(this.ItemTitle)){ %>
            <li>
                <%= ItemTitle %>
            </li>    
            <%} %>
        </ul>
    </div>
</div>
<%} else { %>
<ul class="breadcrumb" style="margin-bottom:30px">
    <li><a href="/"><%=Language["home"] %></a></li>
    <%for (int i = 0 ; i< Categories.Count; i++) { %>
        <li>
            <a href="<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(),Categories[i].Id, "sCat", Categories[i].Id) %>">
                <%=Categories[i].Title %>       
            </a>
        </li>
    <%} %>    
    <%if(!string.IsNullOrEmpty(this.ItemTitle)){ %>
    <li>
        <%= ItemTitle %>
    </li>    
    <%} %>
</ul>
<%} %>
 
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