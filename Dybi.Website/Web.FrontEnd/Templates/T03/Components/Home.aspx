<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Templates/T03/js/t03.js"></script>
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- about section -->
    <section class="about_section py-5">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <VIT:Position runat="server" ID="psTop"></VIT:Position>
                </div>
            <div class="col-md-4 pl-md-0" style="padding-left:0px">
                <div class="img-box">
                    <%if (Template.Config.WebImage.FileExtension == ".webp")
                    { %>
                        <img src="<%= HREF.DomainStore + Template.Config.WebImage.FullPath%>" alt="<%=Company.FullName %>"/>
                    <%}
                    else
                    { %>
                        <picture>
				            <source srcset="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>.webp" type="image/webp">
				            <source srcset="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>" type="image/jpeg"> 
                            <img src="<%=HREF.DomainStore + Template.Config.WebImage.FullPath%>" alt="<%=Company.FullName %>"/>
                        </picture>
                    <%} %>
                </div>
            </div>
            <div class="col-md-8">
                <div class="p-0 p-lg-4 p-md-3 p-sm-2">
                    <div class="heading_container ">
                        <h1 title="<%=Company.FullName %>">
                            Về <%=Company.DisplayName %>
                        </h1>
                    </div>
                    <p>
                        <%=Company.Brief.Replace("\n","<br />") %>
                    </p>
                </div>
            </div>
            </div>
        </div>
    </section>
    <!-- end about section -->
    
    <VIT:Position runat="server" ID="psContent"></VIT:Position>

    <input type="hidden" id="ProdicyId" value="" />
    <div id="buyModal" class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title fs-5" >Thêm vào giỏ hàng</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div style="padding:20px">
                    <div class="row">
                        <div class="col-md-5">
                            <img id="muaImage" style="width:100%"/>
                        </div>
                        <div class="col-md-7">
                            <div><b id="muaTitle"></b></div>
                            <hr />
                            <div id="muaPrice"></div>
                            <br />
                            <div id="muaBrief"></div>
                            <br />
                            <label>Số lượng muốn mua:</label> <input type="number" value="1" id="txtQuantity" placeholder="Nhập số lượng" class="form-control" style="display:unset"/>
                        </div>
                    </div>
                </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-info" onclick="AddToCart(false);" data-bs-dismiss="modal">Tiếp tục mua hàng</button>
                    <button type="button" class="btn btn-warning" onclick="AddToCart(true);">Vào giỏ hàng</button>
                </div>
            </div>
        </div>
    </div>

    <div id="orderModal" class="modal fade" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <VIT:Form ID="frmMain" runat="server">
                    <div class="modal-header">
                        <h5 class="modal-title fs-5" id="orderTitle">Đặt hàng</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div style="padding:15px">
                            <input type="hidden" name="infoLable" value="Nội dung"/>
                            <div class="row">
                                <div class="col-md-5">
                                    <img id="order_Image" style="width:100%"/>
                                </div>
                                <div class="col-md-7">
                                    <div><b id="order_Title"></b></div>
                                    <hr />
                                    <div id="order_Price"></div>
                                    <br />
                                    <div style="display:none">
                                        Upload <asp:FileUpload ID="flu" runat="server" /><br />
                                    </div>
                                    <div class="form-floating mb-3">
                                        <input type="number" value="1" id="order_txtQuantity" placeholder="Nhập số lượng" class="form-control"/>
                                        <label>Số lượng muốn đặt:</label> 
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-floating mb-3">
                                        <input id="customerName" type="text" class="form-control" placeholder="Họ và tên" maxlength="300"/> 
                                        <label>Họ và tên</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <input id="customerPhone" type="text" class="form-control" placeholder="Số điện thoại" maxlength="300"/> 
                                        <label>Số điện thoại</label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <textarea id="customerNote" placeholder="Ghi chú" class="form-control" style="height: 100px"></textarea>
                                        <label>Ghi chú</label>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Nhập mã xác nhận</span>
                                            </div> 
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="DyBiCaptcha"></span>
                                            </div> 
                                            <input id="DyBiCaptchaInput" type="text" class="form-control"/>    
                                            <div class="input-group-prepend">
                                                <button type="button" class="btn btn-secondary" onclick="Captcha(5);">Đổi mã</button>
                                            </div>
                                        </div>
                                        <label id="capcharerror" style="display:none">Mã xác nhân không đúng</label>
                                    </div>	
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnOrder" type="button" class="btn btn-light" onclick="SendOrder();">Gửi đi</button>
                    </div>	
                </VIT:Form>
            </div>
        </div>
    </div>
   
    <script type="application/ld+json">
    {
        "@context": "https://schema.org",
        "@type": "<%=Company.Type %>",
        "name": "<%=Company.DisplayName %>",
        "legalName": "<%=Company.FullName %>",
        "url": "<%=HREF.DomainLink %>",
        "logo": "<%=HREF.DomainStore + this.Company.Image.FullPath %>",
        <%if(Config.WebImage != null && !string.IsNullOrEmpty(Config.WebImage.FullPath)){ %>
        "image": "<%=HREF.DomainStore + Config.WebImage.FullPath %>",
        <%} %>
        <%if(!string.IsNullOrEmpty(Company.Slogan)){ %>
        "slogan":"<%=Company.Slogan %>",
        <%} %>
        <%if(!string.IsNullOrEmpty(Company.TaxCode)){ %>
        "taxID":"<%=Company.TaxCode %>",
        <%} %>
        <%if(!string.IsNullOrEmpty(Company.JobTitle)){ %>
        "keywords":"<%=Company.JobTitle %>",
        <%} %>
        <%if(Company.PublishDate != null){ %>
        "foundingDate":"<%=Company.PublishDate %>",
        <%} %>
        <%if(Company.Branches != null && Company.Branches.Count > 0){ %>
            <%if(!string.IsNullOrEmpty(Company.Branches[0].Email)){ %>
                "email": "<%=Company.Branches[0].Email %>",
            <%} %>
            <%if(!string.IsNullOrEmpty(Company.Branches[0].Phone)){ %>
                "telephone": "<%=Company.Branches[0].Phone %>",
            <%} %>
            <%if(!string.IsNullOrEmpty(Company.Branches[0].Address)){ %>
                "address": "<%=Company.Branches[0].Address %>",
            <%} %>
        <%} %>
        <%if(!string.IsNullOrEmpty(Company.Brief)){ %>
        "description": "<%=Company.Brief %>"
        <%} %>
    }
    </script>
</asp:Content>