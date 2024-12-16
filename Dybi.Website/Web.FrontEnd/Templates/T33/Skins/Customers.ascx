<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<script>
    $("#<%=Category.Id %>").addClass("active");
</script>
<!-- shop section -->
<section class="customer_section layout_padding">
    <h1>
        <%=Title %> 
    </h1>
    <span>
        (<%= 900 + this.Data.Count %> <%=Title %>)
    </span>
    <div class="category">
        <%=Category.Brief %>
    </div>
    <div class="websites">
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Website</th>
                    <th><%=Language["content"]%></th>
                    <th><%=Language["createdate"]%></th>
                    <th><%=Language["links"]%></th>
                    <th style="width:200px"><%=Language["keyword"]%></th>
                </tr>
            </thead>
            <tbody> 
                <%foreach(var item in this.Data) 
                {%>
                    <tr>
                        <td>
                            <a href="https://<%=item.Title %>" target="_blank"><%=item.Title %></a>
                        </td>
                        <td>
                            <%=item.Brief.Replace("\n","<br />") %>
                        </td>
                        <td>
                            <a href="https://www.google.com/search?q=site:<%=item.Title %>" target="_blank">
                            <%=item.GetAttribute("Years")%>
                            </a>
                        </td>
                        <td>
                            <%=item.Content %>
                        </td>
                        <td>
                            <%=item.GetAttribute("Google") %>
                        </td>
                    </tr>
                <%} %>
            </tbody>
        </table>  
    </div>
    <nav aria-label="Page navigation">
        <ul class="pagination">
            <li>
                <a href="<%=HREF.PathAndQuery %>" aria-label="Previous">
                <span aria-hidden="true">&laquo;</span>
                </a>
            </li>
            <li><a href="<%=HREF.PathAndQuery %>">1</a></li>
            <li><a href="<%=HREF.PathAndQuery %>">2</a></li>
            <li><a href="<%=HREF.PathAndQuery %>">3</a></li>
            <li><a href="<%=HREF.PathAndQuery %>">4</a></li>
            <li><a href="<%=HREF.PathAndQuery %>">5</a></li>
            <li><a href="#">...</a></li>
            <li><a href="<%=HREF.PathAndQuery %>">92</a></li>
            <li><a href="<%=HREF.PathAndQuery %>">93</a></li>
            <li><a href="<%=HREF.PathAndQuery %>">94</a></li>
            <li><a href="<%=HREF.PathAndQuery %>">95</a></li>
            <li><a href="<%=HREF.PathAndQuery %>">96</a></li>
            <li>
                <a href="<%=HREF.PathAndQuery %>" aria-label="Next">
                <span aria-hidden="true">&raquo;</span>
                </a>
            </li>
        </ul>
    </nav>
</section>

<script type="application/ld+json">
{
    "@context": "https://schema.org",
    "@type": "ItemList", 
    "numberOfItems": "<%=Data.Count() %>",
    "itemListElement": [
        <%for (int i = 0; i < Data.Count(); i++)
		{%>
        <%= i > 0 ? "," : "" %>
        {
            "@type": "Website",
            "name": "<%=Data[i].Title %>",
            <%if(!string.IsNullOrEmpty(Data[i].ImageName) && Data[i].Image != null){ %>
            "thumbnailUrl": "<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp",
            "image": "<%=HREF.DomainStore + Data[i].Image.FullPath%>.webp",
            <%} %>
            <%if(!string.IsNullOrEmpty(Data[i].Brief)){ %>
            "about": "<%=Data[i].Brief.Replace("\"","")%>",
            "description": "<%=Data[i].Brief.Replace("\"","")%>",
            <%} %>
            <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("Years"))){ %>
            "copyrightYear": "<%=Data[i].GetAttribute("Years")%>",
            <%} %>
            <%if(!string.IsNullOrEmpty(Data[i].GetAttribute("Google"))){ %>
            "keywords": "<%=Data[i].GetAttribute("Google")%>",
            <%} %>
            "url": "https://<%=Data[i].Title %>"
        }
        <%} %>
    ],
    <%if(!string.IsNullOrEmpty(Category.Brief)){ %>
    "description": "<%=Category.Brief.Replace("\"","")%>"
    <%} %>
}
</script>