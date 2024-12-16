<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CategoryBackLink.ascx.cs" Inherits="Web.FrontEnd.Modules.CategoryBackLink" %>
<%@ Import Namespace="Library"%>

<section class="breacrum">
    <div class="min_wrap">
        <ul class="ul_breacrum">
            <li>
                <a href="/">
                    <i class="glyphicon glyphicon-home"></i> <%=this.GetValueParam<string>("BeginName") %>
                </a>
            </li>
            <%for (int i = 0 ; i< Categories.Count; i++) { %>
            <li>
                <%if(string.IsNullOrEmpty(ItemTitle) && i == Categories.Count - 1){ %>
                    <%=Categories[i].Title %>
                <%} else { %>
                <a href="<%=HREF.LinkComponent(Categories[i].ComponentList,Categories[i].Title.ConvertToUnSign(),Categories[i].Id, "sCat", Categories[i].Id) %>">
                    <%=Categories[i].Title %>
                </a>
                <%} %>
            </li>
            <%} %>        
            <%if(!string.IsNullOrEmpty(ItemTitle))  { %>
            <li>
                <%= ItemTitle %>
            </li>    
            <%} %>       
        </ul><!-- End .ul-breacrum -->
    </div><!-- End .min_wrap -->
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