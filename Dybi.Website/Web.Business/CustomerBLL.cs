namespace Web.Business
{
    using System;
    using System.Linq;
    using log4net;
    using Data.DataAccess;
    using Data;
    using Library;
    using Web.Model;
    using System.Collections.Generic;
    using System.IO;

    public class CustomerBLL : BaseBLL
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(CustomerBLL));

        private readonly ICustomerDAL _customerDAL;
        private readonly IWebConfigDAL _webconfigDAL;
        private readonly IOrderDAL orderDAL;
        private readonly IOrderProductDAL orderProductDAL;

        #region Constructor

        public CustomerBLL()
            : this(null)
        {
        }

        public CustomerBLL(string connectionString)
            : base(connectionString)
        {
            this._customerDAL = new CustomerDAL(DatabaseFactory);
            this._webconfigDAL = new WebConfigDAL(DatabaseFactory);
            orderDAL = new OrderDAL(DatabaseFactory);
            orderProductDAL = new OrderProductDAL(DatabaseFactory);
        }

        #endregion

        #region Function
        public void InsertCustomer(Guid companyId, string name, string phone, string address, List<CustomerInfoModel> customerInfos)
        {
            if (this._customerDAL.GetAll().Any(e => e.CompanyId == companyId && e.CustomerPhone == phone)) throw new BusinessException("Số điện thoại đã có người khác sử dụng");

            var customer = new Customer();
            customer.CustomerPhone = phone.Replace(" ","").Replace(".", "").Replace("-", "").Replace("(", "").Replace(")", "");
            customer.CompanyId = companyId;
            customer.CustomerName = name.Trim();
            customer.CustomerAddress = address.Trim();
            this._customerDAL.Add(customer);
            this.SaveChanges();
        }
        
        public IQueryable<CustomerModel> GetCustomers(Guid companyId)
        {
            var cuss = this._customerDAL.GetAll().Where(e => e.CompanyId == companyId)
                            .Select(e => new CustomerModel
                            {
                                Id = e.Id,
                                Address = e.CustomerAddress,
                                Name = e.CustomerName,
                                Phone = e.CustomerPhone
                            });

            return cuss;
        }

        public CustomerModel GetCustomer(Guid id)
        {
            var cus = this._customerDAL.GetAll().Where(e => e.Id == id)
                            .Select(e => new CustomerModel
                            {
                                Id = e.Id,
                                Address = e.CustomerAddress,
                                Name = e.CustomerName,
                                Phone = e.CustomerPhone,
                            }).FirstOrDefault();

            return cus;
        }
        public CustomerModel GetCustomer(Guid companyId, string phone)
        {
            var cus = this._customerDAL.GetAll().Where(e => e.CustomerPhone == phone && e.CompanyId == companyId)
                            .Select(e => new CustomerModel
                            {
                                Id = e.Id,
                                Address = e.CustomerAddress,
                                Name = e.CustomerName,
                                Phone = e.CustomerPhone                              
                            }).FirstOrDefault();

            return cus;
        }
        
        #endregion
        
        #region private method

        private void SendEmail(string host, bool enableSSL, string SendFrom, string SendTo, string Password, int port, string Subject, string Body, Stream stream)
        {
            try
            {
                MailManager mail = new MailManager();

                mail.FileStream = stream;
                mail.EnableSSL = enableSSL;
                mail.Host = host;
                mail.Port = port;
                mail.From = SendFrom;
                mail.Password = Password;
                mail.To = SendTo;
                mail.Title = Subject;
                mail.Content = Body;
                mail.SendEmail();
            }
            catch (Exception ex)
            {
                throw new BusinessException("Lỗi Email: " + ex.Message);
            }
        }

        #endregion
    }
}