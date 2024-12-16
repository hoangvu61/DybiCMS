<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Reviews.ascx.cs" Inherits="Web.FrontEnd.Modules.Reviews" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="col-xs-12 col-sm-12 col-md-6">
    <div class="mx=auto news_h" style="clear:both">
        <h2 class="t_h" style="text-align:left;padding:0px">
            <%=Title %>
        </h2>
        <div class="comment">
            <div class="comment-box">
                <div class="comment-box__content">
                    <div class="comment-box__list">
                        <%foreach(var review in Data.Where(e => !string.IsNullOrEmpty(e.ReviewForType) && e.ReviewForType != "CAT").ToList()){ %>
                        <div class="comment-item">
                            <div class="comment-item__top">
                                <div class="comment-item__info">
                                    <div class="comment-user__info ">
                                        <div class="user-info__name">
                                            <strong><%=review.Name %></strong>&nbsp; đã đánh giá &nbsp;  
                                            <%= review.Vote%> 
                                            <i class="glyphicon glyphicon-star" style="color:#ffc107"></i>&nbsp; 
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
                        </div>    
                        <%} %>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="col-xs-6 col-sm-6 col-md-3">
    <%if(Skin.HeaderBackgroundFile != null){ %>
    <picture>
		<source srcset="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>.webp" type="image/webp">
		<source srcset="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>" type="image/jpeg"> 
        <img src="<%=HREF.DomainStore + Skin.HeaderBackgroundFile.FullPath%>" style="width:100%" alt="<%=Component.Company.FullName %>"/>
    </picture>
    <%} %>
</div>
<div class="col-xs-6 col-sm-6 col-md-3">
    <%if(Skin.BodyBackgroundFile != null){ %>
    <picture>
		<source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>.webp" type="image/webp">
		<source srcset="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" type="image/jpeg"> 
        <img src="<%=HREF.DomainStore + Skin.BodyBackgroundFile.FullPath%>" style="width:100%" alt="<%=Component.Company.FullName %>"/>
    </picture>
    <%} %>
</div>