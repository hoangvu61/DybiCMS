<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<!-- testimonial section start -->
<div class="testimonial_section layout_padding"  style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
    <div id="my_slider" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
            <div class="carousel-item <%=i==0?"active":"" %>">
                <div class="row">
                <div class="col-sm-12">
                    <h1 class="testimonial_taital" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=Title %></h1>
                </div>
                </div>
                <div class="testimonial_section_2">
                <div class="row"> 
                    <div class="col-md-6">
                        <div class="testimonial_box">
                            <div class="client_img">
                                <%if(!string.IsNullOrEmpty(this.Data[i].ImageName)){%>
                                    <picture>
					                    <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp" type="image/webp">
					                    <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" alt="<%=this.Data[i].Title %>"/>
                                    </picture>
			                    <%}%>
                            </div>
                            <h4 class="customer_text" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=this.Data[i].Title %></h4>
                            <p class="lorem_text" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=this.Data[i].Brief %></p>
                        </div>
                    </div>
                        
                    <%if(++i < this.Data.Count){ %>
                    <div class="col-md-6">
                        <div class="testimonial_box">
                            <div class="client_img">
                                <%if(!string.IsNullOrEmpty(this.Data[i].ImageName)){%>
                                    <picture>
					                    <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>.webp" type="image/webp">
					                    <source srcset="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" type="image/jpeg"> 
                                        <img src="<%=HREF.DomainStore + this.Data[i].Image.FullPath%>" alt="<%=this.Data[i].Title %>"/>
                                    </picture>
			                    <%}%>
                            </div>
                            <h4 class="customer_text" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>"><%=this.Data[i].Title %></h4>
                            <p class="lorem_text" style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>"><%=this.Data[i].Brief %></p>
                        </div>
                    </div>
                    <%} %>
                </div>
                </div>
            </div>
            <%} %>
        </div>
        <a class="carousel-control-prev" href="#my_slider" role="button" data-slide="prev">
        <i class="fa fa-angle-left"></i>
        </a>
        <a class="carousel-control-next" href="#my_slider" role="button" data-slide="next">
        <i class="fa fa-angle-right"></i>
        </a>
    </div>
    </div>
</div>
<!-- testimonial section end -->