<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Reviews.ascx.cs" Inherits="Web.FrontEnd.Modules.Reviews" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="section-all layout_padding">
    <div class="container">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-8">
                <div class="heading_container heading_center">
                    <h3 style="text-align:left;width:100%;padding-top:40px">
                        <%=Title %>
                    </h3>
                </div>
 
                <div class="comment-box__list">
                    <%foreach(var review in Data){ %>
                    <div class="comment-item">
                        <div class="comment-item__top">
                            <div class="comment-item__info">
                                <div class="comment-user__info ">
                                    <div class="user-info__name">
                                        <strong><%=review.Name %></strong>&nbsp; đã đánh giá &nbsp;  
                                        <%= review.Vote%> 
                                        <i class="fa fa-star" style="color:#ffc107"></i>&nbsp; 
                                        <%=review.ReviewForType == "PRO" ? " cho sản phẩm " : " cho bài viết " %> 
                                        &nbsp; <a href="<%=this.LinkItem(review.ReviewForId, review.ReviewForTitle, review.ReviewForType) %>">
                                            <%=review.ReviewForTitle %>
                                        </a>
                                    </div>
                                </div>
                                <%if(review.IsBuyer){ %>
                                <div class="d-flex align-items-center">
                                    <div class="me-3">
                                        <span class="check-buyked">Đã mua hàng tại <%=Component.Company.NickName %></span>
                                    </div>
                                </div>
                                <%} %>
                            </div>
                        </div>
                        <div class="comment-item__content">
                            <p>
                                <%= review.Comment%>
                            </p>    
                        </div>
                        <%if(review.Images != null && review.Images.Count > 0){ %>
                        <div class="comment-item__imgs">
                            <%foreach(var image in review.Images){ %>
                            <picture>
		                        <source srcset="<%=HREF.DomainStore + image.FullPath%>.webp" type="image/webp">
		                        <source srcset="<%=HREF.DomainStore + image.FullPath%>" type="image/jpeg"> 
                                <img src="<%=HREF.DomainStore + image.FullPath%>"/>
                            </picture>
                            <%} %>
                        </div>
                        <%} %>
                        <%if(review.Replies != null && review.Replies.Count > 0){ %>
                        <div class="comment-childs">
                            <%foreach(var reply in review.Replies){ %>
                            <div class="comment-item">
                                <div class="comment-item__top">
                                    <div class="comment-item__info">
                                        <div class="comment-user__info mb-0  ">
                                            <div class="user-info__name">
                                                <%=reply.Name %>
                                            </div>
                                        </div>
                                        <div class="comment-item__content">
                                            <%=reply.Comment %>
                                        </div>
                                    </div>
                                </div>
                            </div>   
                            <%} %> 
                        </div>
                        <%} %>
                    </div>    
                    <%} %>
                </div>
            </div>
            <div class="col-12 col-sm-12 col-md-4">
                <%if(Skin.HeaderBackgroundFile != null){ %>
                <picture>
		            <source srcset="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>.webp" type="image/webp">
		            <source srcset="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>" type="image/jpeg"> 
                    <img src="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>" style="width:100%"/>
                </picture>
                <%} %>
            </div>
        </div>
    </div>
</section>