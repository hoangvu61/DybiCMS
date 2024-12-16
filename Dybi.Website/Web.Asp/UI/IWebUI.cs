namespace Web.Asp.UI
{
    using ObjectData;
    using Security;
    using Web.Asp.Provider;
    using Web.Model;

    interface IWebUI
    {
        URLProvider HREF { get; }
        LanguageHelper Language { get; }
        UserPrincipal UserContext { get; }

        CompanyConfigModel Config { get; }
        Message Message { get; set; }
    }
}
