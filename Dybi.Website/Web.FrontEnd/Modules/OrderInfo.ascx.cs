namespace Web.FrontEnd.Modules
{
    using log4net;
    using System;
    using Library.Web;
    using Library;
    using Web.Asp.UI;
    using Web.Model;
    using Web.Business;
    using Web.Asp.Provider;
    using System.Collections.Generic;
    using System.Linq;

    public partial class OrderInfo : VITModule
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(OrderInfo));

        private ProductBLL productBLL;
        protected OrderModel Order;
        protected OrderDeliveryModel OrderDelivery;
        protected List<OrderProductModel> Products;

        protected Guid OrderId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.productBLL = new ProductBLL();
            
            OrderId = this.GetValueRequest<Guid>(SettingsManager.Constants.SendOrder);
            if (OrderId != Guid.Empty)
            {
                Order = this.productBLL.GetOrder(OrderId, this.Config.Id);
                Products = productBLL.GetOrderProducts(OrderId, Config.Language).ToList();
                OrderDelivery = productBLL.GetOrderDelivery(OrderId);
            }
        }
    }
}