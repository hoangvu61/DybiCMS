<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
 
<aside class="products_section p-5">
    <div class="container text-center font-weight-bold" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
        <h2 title="Các sản phẩm <%=Category.Title %> khác">Các sản phẩm <%=Category.Title %> khác</h2>
    </div>
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
                             <a href="<%=HREF.LinkComponent(Category.ComponentDetail, item.Title.ConvertToUnSign(), item.Id, SettingsManager.Constants.SendProduct, item.Id)%>" title="<%=item.Title %>">
                                 <%=item.Title %>
                             </a>
     
                             <%if(item.GetAttributeValueId("PRPDUCT_TYPE") == "INSTORE") {%>
                                 <div class="price">
                                     <%if(item.DiscountType > 0) {%>
                                     <del>
                                         <%=item.Price.ToString("N0") %><sup>đ</sup> 
                                     </del>&nbsp;
                                     <%} %>
                                     <span>
                                         <%if(item.Price > 0) {%>
                                             <%=item.PriceAfterDiscount.ToString("N0") %><sup>đ</sup>
                                         <%} else {%>
                                             <button data-bs-toggle="modal" data-bs-target="#orderModal" class="btn btn-light" onclick="SelectOrderProduct('<%= item.GetAttributeValueId("PRPDUCT_TYPE")%>','<%=item.Title %>','<%=HREF.DomainStore + item.Image.FullPath%>','<%=string.IsNullOrEmpty(item.GetAttributeValueId("SALEMIN")) ? "1" : item.GetAttributeValueId("SALEMIN") %>','<%=item.PriceAfterDiscount.ToString("N0")%>')">Liên hệ</button>
                                         <%} %>
                                     </span>
                                 </div>
 
                                 <%if(item.Price > 0) {%>
                                     <button type="button" class="btn btn-light btn-sm" data-bs-toggle="modal" data-bs-target="#buyModal" onclick="SelectBuyProduct('<%=item.Id%>','<%=item.Title%>','<%=HREF.DomainStore + item.Image.FullPath%>','<%=string.IsNullOrEmpty(item.GetAttribute("SALEMIN")) ? "1" : item.GetAttribute("SALEMIN") %>','<%=item.Brief.Replace(",", " ").Replace("'", "\"").Replace("\n", ".").Replace("\r", ".").Trim()%>','<%=item.PriceAfterDiscount.ToString("N0")%>')">Thêm vào giỏ</button>	
                                 <%} %>
                             <%} else if(item.GetAttributeValueId("PRPDUCT_TYPE") == "OUTOFSTOCK") {%>
                                 <%if(item.Price > 0) {%>
                                 <div class="price">
                                     <%if(item.DiscountType > 0) {%>
                                     <del>
                                         <%=item.Price.ToString("N0") %><sup>đ</sup> 
                                     </del>&nbsp;
                                     <%} %>
                                     <span>
                                         <%if(item.Price > 0) {%>
                                             <%=item.PriceAfterDiscount.ToString("N0") %><sup>đ</sup>
                                         <%} else {%>
                                             <button data-bs-toggle="modal" data-bs-target="#orderModal" class="btn btn-light btn-sm" onclick="SelectOrderProduct('<%= item.GetAttributeValueId("PRPDUCT_TYPE")%>','<%=item.Title %>','<%=HREF.DomainStore + item.Image.FullPath%>','<%=string.IsNullOrEmpty(item.GetAttributeValueId("SALEMIN")) ? "1" : item.GetAttributeValueId("SALEMIN") %>','<%=item.PriceAfterDiscount.ToString("N0")%>')">Liên hệ</button>
                                         <%} %>
                                     </span>
                                 </div>
                                 <%}%>
                                 <p>Hết hàng</p>
                                 <button data-bs-toggle="modal" data-bs-target="#orderModal" class="btn btn-light btn-sm" onclick="SelectOrderProduct('<%= item.GetAttributeValueId("PRPDUCT_TYPE")%>','<%=item.Title %>','<%=HREF.DomainStore + item.Image.FullPath%>','<%=string.IsNullOrEmpty(item.GetAttributeValueId("SALEMIN")) ? "1" : item.GetAttributeValueId("SALEMIN") %>','<%=item.PriceAfterDiscount.ToString("N0")%>')">Đặt hàng ngay</button>
                             <%} else if(item.GetAttributeValueId("PRPDUCT_TYPE") == "ORDER") {%>
                                 <%if(item.Price > 0) {%>
                                 <div class="price">
                                     <%if(item.DiscountType > 0) {%>
                                     <del>
                                         <%=item.Price.ToString("N0") %><sup>đ</sup> 
                                     </del>&nbsp;
                                     <%} %>
                                     <span>
                                         <%if(item.Price > 0) {%>
                                             <%=item.PriceAfterDiscount.ToString("N0") %><sup>đ</sup>
                                         <%} else {%>
                                             <button data-bs-toggle="modal" data-bs-target="#orderModal" class="btn btn-light btn-sm" onclick="SelectOrderProduct('<%= item.GetAttributeValueId("PRPDUCT_TYPE")%>','<%=item.Title %>','<%=HREF.DomainStore + item.Image.FullPath%>','<%=string.IsNullOrEmpty(item.GetAttributeValueId("SALEMIN")) ? "1" : item.GetAttributeValueId("SALEMIN") %>','<%=item.PriceAfterDiscount.ToString("N0")%>')">Liên hệ</button>
                                         <%} %>
                                     </span>
                                 </div>
                                 <%}%>
                                 <p>Hàng đặt (Từ 15 - 20 ngày)</p>
                                 <button data-bs-toggle="modal" data-bs-target="#orderModal" class="btn btn-light btn-sm" onclick="SelectOrderProduct('<%= item.GetAttributeValueId("PRPDUCT_TYPE")%>','<%=item.Title %>','<%=HREF.DomainStore + item.Image.FullPath%>','<%=string.IsNullOrEmpty(item.GetAttributeValueId("SALEMIN")) ? "1" : item.GetAttributeValueId("SALEMIN") %>','<%=item.PriceAfterDiscount.ToString("N0")%>')">Đặt hàng ngay</button>
                             <%} else {%>
                                 <p>Hàng thiết kế - sản xuất theo yêu cầu</p>
                                 <button data-bs-toggle="modal" data-bs-target="#orderModal" class="btn btn-light btn-sm" onclick="SelectOrderProduct('<%= item.GetAttributeValueId("PRPDUCT_TYPE")%>','<%=item.Title %>','<%=HREF.DomainStore + item.Image.FullPath%>','<%=string.IsNullOrEmpty(item.GetAttributeValueId("SALEMIN")) ? "1" : item.GetAttributeValueId("SALEMIN") %>','<%=item.PriceAfterDiscount.ToString("N0")%>')">Nhận báo giá</button>
                             <%} %>
                         </div>
                    </div>
                </div>
            </div>
        <%} %>	
    </div>
</aside>