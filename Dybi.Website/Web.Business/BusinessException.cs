namespace Web.Business
{ 
    using log4net;
    using System;
    using System.Diagnostics;
    using Library;

    /// <summary>
    /// The Provider exception.
    /// </summary>
    [DebuggerNonUserCode]
    [Serializable]
    public class BusinessException : ApplicationException
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(BusinessException));

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerException"/> class.
        /// </summary>
        [Obsolete("This constructor need for generic usages, use constructor with parameters instead.", true)]
        public BusinessException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BusinessException(string message)
            : base(message)
        {
            log.Error(message);
            throw new Exception(message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BusinessException(string formatMessage, params object[] args)
            : base(formatMessage)
        {
            log.Error(string.Format(formatMessage, args));
            throw new Exception(string.Format(formatMessage, args));
        }

        public BusinessException(Exception ex)
            : base()
        {
            log.Error(ex.TraceInformation());
            throw ex;
        }
    }
}