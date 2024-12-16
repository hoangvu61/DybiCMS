<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
 
<section class="products_section p-5" <%=!string.IsNullOrEmpty(Skin.HeaderBackground) ? "style='background:" + Skin.HeaderBackground + "25'" : ""%>>
    <div class="container" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
        <%if(HREF.CurrentComponent == "home"){ %>
        <h2 title="<%=Category.Title %>">
            <%=Title %>
        </h2>
        <%} else {%>
            <h1 class="text-center" title="<%=Title %>">
                <%=Page.Title %>
                <%=!string.IsNullOrEmpty(AttributeName) ? " - " + AttributeName : "" %> 
                <%=!string.IsNullOrEmpty(AttributeValueName) ? " : " + AttributeValueName.Split('|')[0] : "" %>
                <%=!string.IsNullOrEmpty(TagName) ? " : " + TagName : "" %>
            </h1>
        <%} %>
        <p class="py-4 text-center">
            <%=Category.Brief %>
            <%=!string.IsNullOrEmpty(AttributeValueName) ? AttributeValueName.Split('|').Length > 1 ? AttributeValueName.Split('|')[1] : "" : "" %>
        </p>
        <%if(HREF.CurrentComponent != "home" && !string.IsNullOrEmpty(Category.Content)){ %>
            <div class="cat-content pb-5 text-left">
                <%=Category.Content %>
            </div>
        <%} %>
    
        <div class="row">
	        <%foreach(var item in Data){ %>
                <div class="col-6 col-sm-6 col-md-4 col-lg-3 col-xl-2">
                    <div class="product-item">
                        <div class="box">
                            <div class="product-image">
			                    <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id) %>">
                                    <%if(item.Image.FileExtension == ".webp"){ %>
                                        <img src="<%= HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                    <%} else { %>
                                        <picture>
						                    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>.webp" type="image/webp">
						                    <source srcset="<%=HREF.DomainStore + item.Image.FullPath%>" type="image/jpeg"> 
                                            <img src="<%=HREF.DomainStore +  item.Image.FullPath%>" alt="<%=item.Title %>"/>
                                        </picture>
                                    <%} %>
                                </a>
		                    </div>
											
		                    <div class="detail-box">
                                <a href="<%=HREF.LinkComponent(item.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id)%>" title="<%=item.Title %>">
                                    <%=item.Title %>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            <%} %>	
        </div>
        <%if(HREF.CurrentComponent == "home") {%>
            <%if(TotalItems > 60) {%>
                <div class="btn-box">
                    <a class="btn btn-light btn-lg" href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>">
                        Xem tất cả sản phẩm
                    </a>
                </div>
            <%} %>
        <%} else {%>
            <%if(Top > 0 && TotalItems > Top) {%>
                <div class="btn-box">
                    <a href="<%=LinkPage(1)%>">Trang đầu</a> 
                    <%for(int i = 1; i <= TotalPages; i++){ %>
                    <a style="padding: 10px;<%= i == CurrentPage ? "background:" + Config.Background + "75 !important" : "" %>;" href="<%=LinkPage(i)%>"><%=i %></a> 
                    <%} %>
                    <a href="<%=LinkPage(TotalPages)%>">Trang cuối</a>
                </div>
            <%} %>
        <%} %>
    </div>
</section>